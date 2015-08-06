using OpenTK;
using System;
using System.IO;

namespace SageCS.Core
{
    class Engine : GameWindow
    {
        protected override void OnLoad(EventArgs e)
        {
            base.WindowBorder = WindowBorder.Hidden;
            base.OnLoad(e);
            FileSystem.Init();
            var s = FileSystem.Open("language.ini");
            StreamReader sr = new StreamReader(s);
            string content = sr.ReadToEnd();
            s = Resource.GetShader("tex.frag");
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            base.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }
    }
}
