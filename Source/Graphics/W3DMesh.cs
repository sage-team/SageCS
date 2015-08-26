using OpenTK;
using SageCS.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Graphics
{ 
    class W3DMesh
    {
        //if the texture type is bumMapped, the mesh has no material, shaders and texture chunks
        public enum textureType
        {
            standard = 0,
            animated = 1,
            bumpMapped = 2
        };

        public struct Texture
        {
            public textureType type;

            //for texture animation
            public uint attributes;
            public uint animType;
            public long frameCount;
            public float frameRate;

            //for bumpMapping
            public string normalMap;
            public float bumpScale;
            public Vector4 ambientColor;
            public Vector4 diffuseColor;
            public Vector4 specularColor;
            public float specularExponent;
            public byte alphaTestEnable;
        }

        public struct Material
        {
            public long vmAttributes;
            public Vector4 ambient;
            public Vector4 diffuse;
            public Vector4 specular;
            public Vector4 emissive;
            public float shininess;
            public float opacity;
            public float translucency;
            public string vmArgs0;
            public string vmArgs1;
        }

        public struct Shader
        {
            public byte depthCompare;
            public byte depthMask;
            public byte colorMask;
            public byte destBlend;
            public byte fogFunc;
            public byte priGradient;
            public byte secGradient;
            public byte srcBlend;
            public byte texturing;
            public byte detailColorFunc;
            public byte detailAlphaFunc;
            public byte shaderPreset;
            public byte alphaTest;
            public byte postDetailColorFunc;
            public byte postDetailAlphaFunc;
        }

        public uint numTris;
        public Vector3[] vertices;
        public uint[] vertIDs;
        public Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
        public List<Vector2[]> texCoords = new List<Vector2[]>();
        public Dictionary<string, Material> materials = new Dictionary<string, Material>();
        public List<Shader> shaders = new List<Shader>();

        public W3DMesh(uint numTris)
        {
            this.numTris = numTris;
        }
    }
}
