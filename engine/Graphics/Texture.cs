using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using Engine.Core;

namespace Engine.Graphics
{
    public class Texture : BaseObject
    {
        public Texture()
        {
            Location = GL.GenTexture();
        }

        /// <summary>
        /// The location of the texture.
        /// </summary>
        protected int Location { get; set; }

        /// <summary>
        /// The texture target.
        /// </summary>
        protected TextureTarget Target { get; set; }

        /// <summary>
        /// Bind the texture.
        /// </summary>
        public virtual void Bind()
        {
            Bind(Location, 0);
        }

        /// <summary>
        /// Binds a texture with a specified texture unit.
        /// </summary>
        /// <param name="texture">The texture location.</param>
        /// <param name="unit">The texture unit.</param>
        protected virtual void Bind(int texture, int unit)
        {
            if (unit < 0 || unit > 31) return;

            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            GL.BindTexture(Target, texture);
        }

        protected void Parameter(TextureParameterName paramenterName, int parameter)
        {
            GL.TexParameter(Target, paramenterName, parameter);
        }

        protected void Parameter(TextureParameterName paramenterName, float parameter)
        {
            GL.TexParameter(Target, paramenterName, parameter);
        }

        /// <summary>
        /// Unbind the texture.
        /// </summary>
        public virtual void Unbind()
        {
            GL.BindTexture(Target, 0);
        }
        
        public override void Unload()
        {
            GL.DeleteTexture(Location);
        }
    }
}
