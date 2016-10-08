using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Graphics
{
    public class Material
    {
        static Material()
        {
            Default = new Material(new Shader("color"));
        }

        public Material(Shader shader)
        {
            Shader = shader;
        }

        public static Material Default { get; }

        public Shader Shader { get; set; }
        public Color4 Color { get; set; }

        public virtual void Bind()
        {
            Shader.Bind();
        }

        public virtual void Unbind()
        {
            Shader.Unbind();
        }
    }
}
