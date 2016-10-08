using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    static class Screen
    {
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static Vector2 Center { get { return new Vector2(Width / 2, Height / 2); } }
    }
}
