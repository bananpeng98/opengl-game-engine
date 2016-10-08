using Engine.Core;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Components
{
    public class FPController : Component
    {
        public float Sensitivity { get; set; } = 100.0f;

        public override void Update()
        {
            Parent.Rotate(Vector3.UnitY, -Input.DeltaMouse.X / Sensitivity);
            Parent.Rotate(Parent.Right, -Input.DeltaMouse.Y / Sensitivity);
            Input.CenterMouse();
        }

        public override void Render()
        {
            Debug.DrawVector(Parent.WorldPosition, Vector3.UnitZ * 100.0f);
        }
    }
}
