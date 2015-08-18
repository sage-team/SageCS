using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using OpenTK;
using OpenTK.Graphics.OpenGL;

//multithreaded rendering??

namespace SageCS.Core.Graphics
{
    class Renderer
    {
        static int width;
        static int height;
        public static Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();
        public static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
        public static List<Mesh> meshes = new List<Mesh>();

        public static string activeShader = "default";

        private static Vector3[] vertdata;
        private static Vector3[] coldata;
        private static Vector2[] texcoorddata;
        private static int[] indicedata;

        private static int ibo_elements;

        public static void update()
        {
            List<Vector3> verts = new List<Vector3>();
            List<int> inds = new List<int>();
            List<Vector3> colors = new List<Vector3>();
            List<Vector2> texcoords = new List<Vector2>();

            int vertcount = 0;
            foreach (Mesh m in meshes)
            {
                verts.AddRange(m.GetVerts().ToList());
                inds.AddRange(m.GetIndices(vertcount).ToList());
                colors.AddRange(m.GetColorData().ToList());
                texcoords.AddRange(m.GetTextureCoords());
                vertcount += m.VertCount;
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

            foreach (Mesh m in meshes)
            {
                Matrix4[] mviewdata = new Matrix4[]{ Matrix4.Identity};

                GL.UniformMatrix4(shaders[activeShader].GetUniform("modelview"), false, ref mviewdata[0]);
                //m.CalculateModelMatrix();
                //m.ViewProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(1.3f, width / height, 1.0f, 40.0f);
                //m.ModelViewProjectionMatrix = m.ModelMatrix * m.ViewProjectionMatrix;
                //GL.UniformMatrix4(shaders[activeShader].GetUniform("modelview"), false, ref m.ModelViewProjectionMatrix);
            }

            GL.UseProgram(shaders[activeShader].ProgramID);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(int)), indicedata, BufferUsageHint.StaticDraw);
        }

        public static void render()
        {
            clear();
            clearColor();

            shaders[activeShader].EnableVertexAttribArrays();

            int indiceat = 0;
            foreach (Mesh m in meshes)
            {
                m.texture.Bind();
                GL.UniformMatrix4(shaders[activeShader].GetUniform("modelview"), false, ref m.ModelViewProjectionMatrix);

                if (shaders[activeShader].GetAttribute("maintexture") != -1)
                {
                    GL.Uniform1(shaders[activeShader].GetAttribute("maintexture"), m.texture.GetID());
                }

                GL.DrawElements(BeginMode.Triangles, m.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
                indiceat += m.IndiceCount;
            }

            shaders[activeShader].DisableVertexAttribArrays();

            GL.Flush();
        }

        public static void clear()
        {
            GL.Viewport(0, 0, width, height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
        }

        public static void clearColor()
        {
            GL.ClearColor(Color.CornflowerBlue);
            GL.PointSize(5f); //what is this doing?
        }

        public static void resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }

        public static void initProgram(int Width, int Height)
        {
            GL.GenBuffers(1, out ibo_elements);
            width = Width;
            height = Height;
            update();
        }

        public static void DeleteBuffers()
        {
            GL.DeleteBuffer(ibo_elements);
            foreach (KeyValuePair<string, Shader> s in shaders)
            {
                s.Value.DeleteBuffers();
            }
        }
    }
}
