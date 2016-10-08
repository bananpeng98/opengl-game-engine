using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Graphics
{
    public class Quad : Mesh
    {
        private static List<Vertex> vertices = new List<Vertex>()
        {
            new Vertex(new Vector3(-0.5f, -0.5f, 0.0f), new Vector2(0.0f, 0.0f)),
            new Vertex(new Vector3(0.5f, -0.5f, 0.0f), new Vector2(1.0f, 0.0f)),
            new Vertex(new Vector3(0.5f, 0.5f, 0.0f), new Vector2(1.0f, 1.0f)),
            new Vertex(new Vector3(-0.5f, 0.5f, 0.0f), new Vector2(0.0f, 1.0f))
        };

        private static List<int> indices = new List<int>()
        {
            0, 1, 2,
            2, 0, 3
        };

        public Quad()
        {
            SetData(vertices, indices);
            Apply();
        }
    }
}
