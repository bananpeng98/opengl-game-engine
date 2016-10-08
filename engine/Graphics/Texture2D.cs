using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using Engine.Core;

namespace Engine.Graphics
{
    public class Texture2D : Texture
    {
        public Texture2D(string imageName)
            : base()
        {
            LoadTexture(imageName);
        }

        private void LoadTexture(string imageName)
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap(imageName);
            }
            catch (Exception e)
            {
                Debug.Log($"Failed to load texture: {imageName}", MessageType.Error);
                Debug.Log(e.Message, MessageType.Error);
                return;
            }
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bind();
            
            Parameter(TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            Parameter(TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpData.Width, bmpData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            Parameter(TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            Parameter(TextureParameterName.TextureLodBias, -0.4f);

            Unbind();
            bitmap.UnlockBits(bmpData);
        }
    }
}
