using OpenTK;
using SageCS.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Core.Graphics
{
    class Sprite : Mesh
    {
        public Sprite()
        : base()
        {
            VertCount = 4;
            IndiceCount = 6;
            TextureCoordsCount = 4;
            IsTextured = true;
            Renderer.meshes.Add(this);
        }

        public Sprite(int texID)
        : base()
        {
            TextureID = texID;
            VertCount = 4;
            IndiceCount = 6;
            TextureCoordsCount = 4;
            IsTextured = true;
            Renderer.meshes.Add(this);
        }

        public Sprite(string tex)
        : base()
        {
            TextureID = Renderer.textures[tex];
            VertCount = 4;
            IndiceCount = 6;
            TextureCoordsCount = 4;
            IsTextured = true;
            Renderer.meshes.Add(this);
        }

        public override Vector3[] GetVerts()
        {
            return new Vector3[]
            {
                new Vector3(-1.0f, 1.0f, 0f),
                new Vector3(1.0f, 1.0f, 0f),
                new Vector3(1.0f, -1.0f, 0f),
                new Vector3(-1.0f, -1.0f, 0f),
            };
        }

        public override int[] GetIndices(int offset = 0)
        {
            int[] inds = new int[] {0,1,2,0,2,3};

            if (offset != 0)
            {
                for (int i = 0; i < inds.Length; i++)
                {
                    inds[i] += offset;
                }
            }
            return inds;
        }

        public override Vector2[] GetTextureCoords()
        {
            return new Vector2[] {
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 0.0f),
            };
        }

        public override void CalculateModelMatrix()
        {
           
        }

        public override Vector3[] GetColorData()
        {
            return new Vector3[] {
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f, 1f, 0f),
            };
        }
    }
}
