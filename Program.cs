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
            using (var game = new Core.Engine())
            {             
                game.Run(60.0);
            }
        }
    }
}
