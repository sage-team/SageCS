using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;
using SageCS.Graphics.GraphicsTK;

namespace SageCS
{
    class TKWindow : GameWindow
    {
        TKRenderer renderer; 

        public TKWindow() : base(512, 512, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4))
        {
            Run();
        }

        public TKWindow(double frames_per_second)
        {
            Run(frames_per_second);
        }

        public TKWindow(double updates_per_second, double frames_per_second)
        {
            Run(updates_per_second, frames_per_second);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            renderer = new TKRenderer(Width, Height);
            initProgram();
            Title = "Hello OpenTK!";

            VSync = VSyncMode.On;

            renderer.clearColor();

            //add some meshes
            renderer.addMesh(new TKCube());
            renderer.addMesh(new TKCube());
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            renderer.resize(Width, Height);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            renderer.render();
        }

        float time;
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            // add game logic, input handling  
            if (Keyboard[Key.Escape])
            {
                Exit();
            }
            time += (float)e.Time;

            renderer.update(time);
            SwapBuffers();
        }

        void initProgram()
        {
            renderer.initProgram();
        }
    }
}
