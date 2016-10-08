using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Graphics
{
    public struct Vertex
    {
        public Vertex(Vector3 position, Vector3 normal, Vector3 tangent, Color4 color, Vector2 uv)
        {
            Position = position;
            Normal = normal;
            Tangent = tangent;
            Color = color;
            UV = uv;
        }

        public Vertex(Vector3 position, Color4 color, Vector2 uv)
            : this(position, new Vector3(0, 0, 1), new Vector3(1, 0, 0), color, uv)
        { }

        public Vertex(Vector3 position, Vector2 uv)
            : this(position, new Color4(1.0f, 1.0f, 1.0f, 1.0f), uv)
        { }

        public Vector3 Position { get; set; }
        public Vector3 Normal { get; set; }
        public Vector3 Tangent { get; set; }
        public Color4 Color { get; set; }
        public Vector2 UV { get; set; }
    }
}
