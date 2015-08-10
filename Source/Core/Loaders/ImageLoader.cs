using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Pfim;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace SageCS.Core.Loaders
{
    struct ImageData
    {
        public int width;
        public int height;
        public byte[] data;
        public PixelFormat format;
    }

    class ImageLoader
    {
        private static ImageData FromPfimImg(IImage image)
        {
            ImageData img = new ImageData();
            img.data = image.Data;
            img.width = image.Width;
            img.height = image.Height;
            img.format = FromPfimFormat(image.Format);
            return img;
        }

        private static PixelFormat FromPfimFormat(ImageFormat f)
        {
            if (f == ImageFormat.Rgb24)
                return PixelFormat.Rgb;
            else if (f == ImageFormat.Rgba32)
                return PixelFormat.Rgba;

            throw new Exception("Unknown Image format");
        }

        private static ImageData FromBitmap(System.Drawing.Bitmap image)
        {
            ImageData img = new ImageData();
            img.data = BitmapToByteArray(image);
            img.width = image.Width;
            img.height = image.Height;
            img.format = FromPixelformat(image.PixelFormat);

            return img;
        }

        private static PixelFormat FromPixelformat(System.Drawing.Imaging.PixelFormat pf)
        {
            if (pf == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                return PixelFormat.Rgba;
            else if (pf == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                return PixelFormat.Rgb;

            throw new Exception("Unknown Image format");
        }

        private static byte[] BitmapToByteArray(System.Drawing.Bitmap bitmap)
        {

            System.Drawing.Imaging.BitmapData bmpdata = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int numbytes = bmpdata.Stride * bitmap.Height;
            byte[] bytedata = new byte[numbytes];
            IntPtr ptr = bmpdata.Scan0;

            Marshal.Copy(ptr, bytedata, 0, numbytes);

            bitmap.UnlockBits(bmpdata);

            return bytedata;
        }

        public static ImageData Load(Stream s)
        {
            ImageData img;

            try
            {
                IImage image = Targa.Create(s);
                img = FromPfimImg(image);
            }
            catch
            {
                try
                {
                    IImage image = Dds.Create(s);
                    img = FromPfimImg(image);
                }
                catch
                {
                    System.Drawing.Bitmap image = new System.Drawing.Bitmap(s);
                    img = FromBitmap(image);
                }

            }

            return img;
        }
    }
}
