using Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Components
{
    public class Component : BaseObject
    {
        public Component() { }

        /// <summary>
        /// The parent game object of this component.
        /// </summary>
        public Transform Parent { get; set; }

        /// <summary>
        /// Enables or disables this component from updating and rendering.
        /// </summary>
        public bool Enabled { get; set; } = true;
    }
}
