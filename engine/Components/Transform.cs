using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Components
{
    public class Transform : Component
    {
        public Transform()
        {
            Scale = Matrix4.CreateScale(Vector3.One);
            Rotation = Quaternion.Identity;
        }
        
        /// <summary>
        /// The translation matrix of this transform.
        /// </summary>
        public Matrix4 Translation { get { return Matrix4.CreateTranslation(WorldPosition); } }

        /// <summary>
        /// The scale matriix of this transform.
        /// </summary>
        public Matrix4 Scale { get; private set; }

        /// <summary>
        /// The rotation quaternion of this transform.
        /// </summary>
        public Quaternion Rotation { get; private set; }

        /// <summary>
        /// The model matrix. Contains translation, rotation and scale.
        /// </summary>
        public Matrix4 Model
        {
            get
            {
                return Scale * Matrix4.CreateFromQuaternion(Rotation) * Translation;
            }
        }

        public Vector3 Right { get { return Vector3.Transform(Vector3.UnitX, Rotation); } }
        public Vector3 Up { get { return Vector3.Transform(Vector3.UnitY, Rotation); } }
        public Vector3 Forward { get { return Vector3.Transform(-Vector3.UnitZ, Rotation); } }
        public Vector3 Left { get { return -Right; } }
        public Vector3 Down { get { return -Up; } }
        public Vector3 Back { get { return -Forward; } }

        /// <summary>
        /// Gets the local position to the parent.
        /// </summary>
        public Vector3 LocalPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the world position of this transform.
        /// </summary>
        public Vector3 WorldPosition { get; set; }

        public void Rotate(Vector3 axis, float angle)
        {
            Rotation = Rotation * Quaternion.FromAxisAngle(axis, angle);
        }

        public void Rotate(Vector3 eulerAngles)
        {
            Rotation = Rotation * Quaternion.FromEulerAngles(eulerAngles);
        }

        public void SetRotation(Vector3 eulerAngles)
        {
            Rotation = Quaternion.FromEulerAngles(eulerAngles);
        }

        public void SetRotation(Vector3 axis, float angle)
        {
            Rotation = Quaternion.FromAxisAngle(axis, angle);
        }

        public void SetScale(Vector3 scale)
        {
            Scale = Matrix4.CreateScale(scale);
        }
    }
}
