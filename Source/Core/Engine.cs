using OpenTK;
using OpenTK.Input;
using SageCS.Core.Graphics;
using System;
using System.IO;

using SageCS.Audio;
using SageCS.Core.Loaders;
using System.Diagnostics;

namespace SageCS.Core
{
    class Engine : GameWindow
    {
        ~Engine()
        {
            Renderer.DeleteBuffers();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.WindowBorder = WindowBorder.Hidden;
            base.OnLoad(e);

            Title = "SageCS - BFME II";

            Renderer.shaders.Add("textured", new Shader(Resource.GetShader("tex.vert"), Resource.GetShader("tex.frag")));
            Renderer.activeShader = "textured";

            try
            {
                Texture t = new Texture();
                t.Load(File.Open("GermanSplash.jpg", FileMode.Open));
                Renderer.textures.Add("splash", t);
            }
            catch
            {
                Texture t = new Texture();
                t.Load(File.Open("EnglishSplashjpg", FileMode.Open));
                Renderer.textures.Add("splash", t);
            }
            
            Sprite background = new Sprite("splash");

            Renderer.initProgram(Width, Height);

            Renderer.render();
            base.SwapBuffers();

            FileSystem.Init();
            AudioSystem.Init();
            var s = FileSystem.Open("language.ini");
            StreamReader sr = new StreamReader(s);
            string content = sr.ReadToEnd();

            Stopwatch stopwatch = Stopwatch.StartNew();

            Texture tex = new Texture();
            var texS = FileSystem.Open("art\\compiledtextures\\al\\all_faction_banners.dds");
            tex.Load(texS);
            W3DLoader.Load(FileSystem.Open("art\\w3d\\gu\\gumaarms_skn.w3d"));
            W3DLoader.Load(FileSystem.Open("art\\w3d\\gu\\gumaarms_runa.w3d"));
            W3DLoader.Load(FileSystem.Open("art\\w3d\\gu\\gumaarms_skl.w3d"));

            new INIParser(FileSystem.Open("data\\ini\\gamedata.ini"));
            //new INIParser(FileSystem.Open("data\\ini\\mappedimages\\aptimages\\aptimages.ini"));
            //new INIParser(FileSystem.Open("data\\ini\\mappedimages\\aptimages\\aptcomponents.ini"));

            //new INIParser(FileSystem.Open("data\\ini\\object\\goodfaction\\units\\men\\aragorn.ini"));

            stopwatch.Stop();
            Console.WriteLine("total loading time: " + stopwatch.ElapsedMilliseconds + "ms");
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
