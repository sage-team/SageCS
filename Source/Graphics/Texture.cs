using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using OpenTK.Graphics.OpenGL4;
using SageCS.Core.Loaders;
using System;

namespace SageCS.Core.Graphics
{
    public class Texture
    {
        int texID = -1;

        public Texture()
        {       
            texID = GL.GenTexture();      
        }

        public void Load(Stream s)
        {
            ImageData img = ImageLoader.Load(s);
            GL.BindTexture(TextureTarget.Texture2D, texID);

            //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.width, img.height, 0,
            //        OpenTK.Graphics.OpenGL4.PixelFormat.RgbaInteger, PixelType.UnsignedByte, img.data);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.width, img.height, 0,
                     img.format, PixelType.UnsignedByte, img.data);

            //needed to make the texture visible
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D,texID);
        }

        public int GetID()
        {
            return texID;
        }
    }
}
