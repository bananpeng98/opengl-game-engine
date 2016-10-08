using Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Components
{
    public class MeshRenderer : Renderer
    {
        public MeshRenderer()
        {

        }

        public Mesh Mesh { get; set; }

        public override void Render()
        {
            Bind();

            Material.Shader.SetUniform("M", Parent.Model);
            Material.Shader.SetUniform("V", Camera.MainCamera.View);
            Material.Shader.SetUniform("P", Camera.MainCamera.Projection);
            Material.Shader.SetUniform("MVP", Parent.Model * Camera.MainCamera.View * Camera.MainCamera.Projection);

            Mesh.Bind();
            Mesh.Render();
            Mesh.Unbind();

            Unbind();
        }
    }
}
