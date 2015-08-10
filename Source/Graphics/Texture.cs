using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using OpenTK.Graphics.OpenGL4;
using SageCS.Core.Loaders;

namespace SageCS.Core.Graphics
{
    class Texture
    {
        public static int loadImage(ImageData image)
        {
            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb,image.width, image.height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Rgb, PixelType.UnsignedByte, image.data);

            return texID;
        }

        public static int loadImage(Stream s)
        {
            var img = ImageLoader.Load(s);
            loadImage(img);
            return 0;
        }
    }
}
