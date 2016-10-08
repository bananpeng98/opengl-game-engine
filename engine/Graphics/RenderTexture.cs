using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics
{
    public class RenderTexture : Texture
    {
        private int frameBuffer;

        public RenderTexture(int width, int height, int depth)
            : base()
        {
            Target = TextureTarget.Texture2D;

            frameBuffer = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBuffer);

            base.Bind();

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        }

        public override void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Location);
        }
    }
}
