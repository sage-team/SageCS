using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;
using System.IO;
using SageCS.Graphics;
using SageCS.Graphics.GraphicsTK;

namespace SageCS
{
    class TKRenderer
    {
        int width, height;

        Dictionary<string, TKShaderProgram> shaders = new Dictionary<string, TKShaderProgram>();
        string activeShader = "default";

        Vector3[] vertdata;
        Vector3[] coldata;
        Vector2[] texcoorddata;
        int[] indicedata;

        int ibo_elements;

        List<TKMesh> objects = new List<TKMesh>();

        public TKRenderer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void addMesh(TKMesh mesh)
        {
            objects.Add(mesh);
        }

        public void update(float time)
        {
            List<Vector3> verts = new List<Vector3>();
            List<int> inds = new List<int>();
            List<Vector3> colors = new List<Vector3>();
            List<Vector2> texcoords = new List<Vector2>();

            int vertcount = 0;

            foreach (TKMesh v in objects)
            {
                verts.AddRange(v.GetVerts().ToList());
                inds.AddRange(v.GetIndices(vertcount).ToList());
                colors.AddRange(v.GetColorData().ToList());
                texcoords.AddRange(v.GetTextureCoords());
                vertcount += v.VertCount;
            }

            vertdata = verts.ToArray();
            indicedata = inds.ToArray();
            coldata = colors.ToArray();
            texcoorddata = texcoords.ToArray();


            GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vPosition"));
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vPosition"), 3, VertexAttribPointerType.Float, false, 0, 0);

            if (shaders[activeShader].GetAttribute("vColor") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vColor"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), coldata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vColor"), 3, VertexAttribPointerType.Float, true, 0, 0);
            }

            if (shaders[activeShader].GetAttribute("texcoord") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("texcoord"));
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(texcoorddata.Length * Vector2.SizeInBytes), texcoorddata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("texcoord"), 2, VertexAttribPointerType.Float, true, 0, 0);
            }

            objects[0].Position = new Vector3(0.3f, -0.5f + (float)Math.Sin(time), -3.0f);
            objects[0].Rotation = new Vector3(0.55f * time, 0.25f * time, 0);
            objects[0].Scale = new Vector3(0.1f, 0.1f, 0.1f);

            objects[1].Position = new Vector3(-1f, 0.5f + (float)Math.Cos(time), -2.0f);
            objects[1].Rotation = new Vector3(-0.25f * time, -0.35f * time, 0);
            objects[1].Scale = new Vector3(0.25f, 0.25f, 0.25f);

            foreach (TKMesh v in objects)
            {
                v.CalculateModelMatrix();
                v.ViewProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(1.3f, width / height, 1.0f, 40.0f);
                v.ModelViewProjectionMatrix = v.ModelMatrix * v.ViewProjectionMatrix;
                GL.UniformMatrix4(shaders[activeShader].GetUniform("modelview"), false, ref v.ModelViewProjectionMatrix);
            }

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(int)), indicedata, BufferUsageHint.StaticDraw);

            GL.UseProgram(shaders[activeShader].ProgramID);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void render()
        {
            clear();
            GL.Enable(EnableCap.DepthTest);

            shaders[activeShader].EnableVertexAttribArrays();

            int indiceat = 0;
            foreach (TKMesh v in objects)
            {
                GL.BindTexture(TextureTarget.Texture2D, v.TextureID);
                GL.UniformMatrix4(shaders[activeShader].GetUniform("modelview"), false, ref v.ModelViewProjectionMatrix);

                if (shaders[activeShader].GetAttribute("maintexture") != -1)
                {
                    GL.Uniform1(shaders[activeShader].GetAttribute("maintexture"), v.TextureID);
                }

                //GL.DrawElements(BeginMode.Triangles, v.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
                indiceat += v.IndiceCount;
            }

            shaders[activeShader].DisableVertexAttribArrays();

            GL.Flush();
        }

        public void clear()
        {
            //GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void clearColor()
        {
            GL.ClearColor(Color.CornflowerBlue);
            GL.PointSize(5f); //what is this doing?
        }

        public void resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }

        public static int createProgram()
        {
            return GL.CreateProgram();
        }

        public void initProgram()
        {
            //shaders.Add("default", new TKShaderProgram("svs.glsl", "fs.glsl", true));
            shaders.Add("textured", new TKShaderProgram("shaders/vs_tex.glvs", "shaders/fs_tex.glfs", true));

            activeShader = "textured";

            Texture.textures.Add("opentksquare.png", TKTexture.loadImage("GermanSplash.jpg"));
            Texture.textures.Add("opentksquare2.png", TKTexture.loadImage("EnglishSplash.jpg"));

            TKTexturedCube tc = new TKTexturedCube();
            tc.TextureID = Texture.textures["opentksquare.png"];
            objects.Add(tc);

            TKTexturedCube tc2 = new TKTexturedCube();
            tc2.Position += new Vector3(1f, 1f, 1f);
            tc2.TextureID = Texture.textures["opentksquare2.png"];
            objects.Add(tc2);
        }
    }
}
