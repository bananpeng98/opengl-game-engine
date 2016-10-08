using Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class GameObject : BaseObject
    {
        /// <summary>
        /// Every game object will have components to add
        /// some kind of behavior or graphics.
        /// </summary>
        private List<Component> components = new List<Component>();

        /// <summary>
        /// List of this game object's children.
        /// </summary>
        private List<GameObject> children = new List<GameObject>();

        public GameObject()
        {
            Transform = AddComponent<Transform>();
        }

        /// <summary>
        /// This game object's transformation. Contains the position, rotation and scale of the object.
        /// </summary>
        public Transform Transform { get; set; }

        /// <summary>
        /// Start this game object, components and children.
        /// </summary>
        public override void Start()
        {
            for (int i = 0; i < components.Count; i++)
                components[i].Start();
            for (int i = 0; i < children.Count; i++)
                children[i].Start();
        }

        /// <summary>
        /// Unload this game object, components and children.
        /// </summary>
        public override void Unload()
        {
            for (int i = 0; i < components.Count; i++)
                components[i].Unload();
            for (int i = 0; i < children.Count; i++)
                children[i].Unload();
        }

        /// <summary>
        /// Update this game object, components and children.
        /// </summary>
        public override void Update()
        {
            for (int i = 0; i < components.Count && components[i].Enabled; i++)
                components[i].Update();
            for (int i = 0; i < children.Count; i++)
                children[i].Update();
        }

        /// <summary>
        /// Render this game object, components and children.
        /// </summary>
        public override void Render()
        {
            for (int i = 0; i < components.Count && components[i].Enabled; i++)
                components[i].Render();
            for (int i = 0; i < children.Count; i++)
                children[i].Render();
        }

        /// <summary>
        /// Add a premade component to this game object.
        /// </summary>
        /// <param name="component">The component to add.</param>
        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        /// <summary>
        /// Add a component by type.
        /// </summary>
        /// <typeparam name="T">The typename of the component to add.</typeparam>
        public T AddComponent<T>()
            where T : Component
        {
            T component = Activator.CreateInstance<T>();
            component.Parent = Transform;
            AddComponent(component);
            return component;
        }

        /// <summary>
        /// Check if this game object has a type of component.
        /// </summary>
        /// <typeparam name="T">The type to check against.</typeparam>
        /// <returns></returns>
        public bool HasComponent<T>()
            where T : Component
        {
            for (int i = 0; i < components.Count; i++)
                if (components[i].GetType() == typeof(T))
                    return true;
            return false;
        }
    }
}
