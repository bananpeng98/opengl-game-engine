using Engine.Core;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Components
{
    public class Camera : Component
    {
        private static Camera mainCamera;
        
        public Camera()
        {
            if (Parent == null)
                Parent = new Transform();
        }

        public float FOV { get; set; } = 1.3f;

        public float Aspect { get { return Screen.Height / (float)Screen.Width; } }

        public float Near { get; set; } = 0.01f;

        public float Far { get; set; } = 100.0f;

        public Vector3 Target { get { return Vector3.Transform(-Vector3.UnitZ, Parent.Rotation); } }
        public Vector3 Up { get { return Vector3.Transform(Vector3.UnitY, Parent.Rotation); } }

        public Matrix4 Projection
        {
            get { return Matrix4.CreatePerspectiveFieldOfView(FOV, Aspect, Near, Far); }
        }

        public Matrix4 View
        {
            get { return Matrix4.LookAt(Parent.WorldPosition, Parent.WorldPosition + Target, Up); }
        }

        public static Camera MainCamera
        {
            get
            {
                if (mainCamera == null)
                    mainCamera = new Camera();
                return mainCamera;
            }
            set { mainCamera = value; }
        }
    }
}
