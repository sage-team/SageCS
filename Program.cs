using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace SageCS
{
    class Program
    {
        static void Main(string[] args)
        {
            //mein renderer
            TKWindow win = new TKWindow(30, 30);

            using (var game = new Core.Engine())
            {             
                game.Run(60.0);
            }
        }
    }
}
