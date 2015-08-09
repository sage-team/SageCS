using OpenTK;
using OpenTK.Input;
using SageCS.Core.Graphics;
using System;
using System.IO;

using SageCS.Audio;

namespace SageCS.Core
{
    class Engine : GameWindow
    {
        protected override void OnLoad(EventArgs e)
        {
            base.WindowBorder = WindowBorder.Hidden;
            base.OnLoad(e);
            FileSystem.Init();
            AudioSystem.Init();     
            var s = FileSystem.Open("language.ini");
            StreamReader sr = new StreamReader(s);
            string content = sr.ReadToEnd();

            Renderer.shaders.Add("textured", new ShaderProgram(Resource.GetShader("tex.vert"), Resource.GetShader("tex.frag")));
            Renderer.activeShader = "textured";

            Renderer.textures.Add("germanSplash", Texture.load(File.Open("GermanSplash.jpg",FileMode.Open)));

            Sprite background = new Sprite();
            background.TextureID = Renderer.textures["germanSplash"];
            Renderer.meshes.Add(background);
            Renderer.initProgram(Width, Height);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            Renderer.render();
            base.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (Keyboard[Key.Escape])
            {
                Exit();
            }
            Renderer.update();
        }
    }
}
