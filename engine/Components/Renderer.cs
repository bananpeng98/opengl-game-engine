using Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Components
{
    public abstract class Renderer : Component
    {
        public Renderer()
        {
            Material = Material.Default;
        }

        public Material Material { get; private set; }

        public virtual void Bind()
        {
            Material.Bind();
        }
        
        public virtual void Unbind()
        {
            Material.Unbind();
        }
    }
}
