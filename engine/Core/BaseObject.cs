using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class BaseObject
    {
        public BaseObject() { }

        /// <summary>
        /// The name of the object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Virtual start method that is called once before the game starts.
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// Unload any loaded resources.
        /// </summary>
        public virtual void Unload() { }

        /// <summary>
        /// Virtual update method that is called every frame, before rendering.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Virtual render method that is called every frame to render the object.
        /// </summary>
        public virtual void Render() { }
    }
}
