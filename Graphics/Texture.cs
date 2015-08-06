using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Graphics
{
    public abstract class Texture
    {
        public static Dictionary<string, int> textures = new Dictionary<string, int>();

        public static int loadImage(Bitmap image)
        {
            Console.WriteLine("ERROR: not implemented in Texture.cs");
            return -1;
        }

        public static int loadImage(string filename)
        {
            try
            {
                Bitmap file = new Bitmap(filename);
                return loadImage(file);
            }
            catch (FileNotFoundException e)
            {
                return -1;
            }
        }
    }
}
