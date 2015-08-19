using OpenTK;
using SageCS.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO:
// fix the ReadString method

// unknown chunks:
//   64 size of 4 bytes
//   96 vertex normals for bump mapping? (specular and diffuse normals)
//   97 vertex normals for bump mapping? (specular and diffuse normals)
//   643 compressed animation data

namespace SageCS.Core.Loaders
{
    class W3DLoader
    {
        private static string modelName = "";
        private static Model model;
        private static string hierarchyName = "";
        private static Hierarchy hierarchy;
        private static W3DMesh mesh;
        private static string texName = "";
        private static string matName = "";

        //######################################################################################
        //# structs
        //######################################################################################

        struct Version
        {
            public long major;
            public long minor;
        }

        //######################################################################################
        //# basic methods
        //######################################################################################
        private static string ReadString(BinaryReader br)
        {
            List<byte> data = new List<byte>();
            byte b = br.ReadByte();
            while (b != 0)
            {
                data.Add(b);
                b = br.ReadByte();
            }
            return System.Text.Encoding.UTF8.GetString(data.ToArray<byte>());
        }

        private static string ReadFixedString(BinaryReader br)
        {
            byte[] data = br.ReadBytes(16);
            return System.Text.Encoding.UTF8.GetString(data);
        }

        private static string ReadLongFixedString(BinaryReader br)
        {
            byte[] data = br.ReadBytes(32);
            return System.Text.Encoding.UTF8.GetString(data);
        }

        private static Vector4 ReadRGBA(BinaryReader br)
        {
            return new Vector4(br.ReadByte(), br.ReadByte(), br.ReadByte(), br.ReadByte());
        }

        private static uint getChunkSize(uint data)
        {
            return (data & 0x7FFFFFFF);
        }

        private static uint ReadLong(BinaryReader br)
        {
            return br.ReadUInt32();
        }

        private static uint[] ReadLongArray(BinaryReader br, uint ChunkEnd)
        {
            List<uint> data = new List<uint>();
            while (br.BaseStream.Position < ChunkEnd)
            {
                data.Add(ReadLong(br));
            }
            return data.ToArray();
        }

        private static uint ReadShort(BinaryReader br)
        {
            return br.ReadUInt16();
        }

        private static float ReadFloat(BinaryReader br)
        {
            return (float)br.ReadSingle();
        }

        private static byte ReadByte(BinaryReader br)
        {
            return br.ReadByte();
        }

        private static Vector3 ReadVector(BinaryReader br)
        {

            return new Vector3(ReadFloat(br), ReadFloat(br), ReadFloat(br));
        }

        private static Quaternion ReadQuaternion(BinaryReader br)
        {
            return new Quaternion(ReadFloat(br), ReadFloat(br), ReadFloat(br), ReadFloat(br));
        }

        private static Version GetVersion(long data)
        {
            Version v = new Version();
            v.major = data >> 16;
            v.minor = data & 0xFFFF;
            return v;
        }

        //#######################################################################################
        //# Hierarchy
        //#######################################################################################

        private static void ReadHierarchyHeader(BinaryReader br)
        {
            Version version = GetVersion(ReadLong(br));
            hierarchyName = ReadFixedString(br);
            long pivotCount = ReadLong(br);
            Vector3 centerPos = ReadVector(br);

            hierarchy = new Hierarchy(pivotCount, centerPos);
        }

        private static void ReadPivots(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                string name = ReadFixedString(br);
                long parentID = ReadLong(br);
                Vector3 position = ReadVector(br);
                Vector3 eulerAngles = ReadVector(br);
                Quaternion rotation = ReadQuaternion(br);

                hierarchy.addPivot(name, parentID, position, eulerAngles, rotation);
            }
        }

        private static void ReadPivotFixups(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                Vector3 pivot_fixup = ReadVector(br);
            }
        }

        private static void ReadHierarchy(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 257:
                        ReadHierarchyHeader(br);
                        break;
                    case 258:
                        ReadPivots(br, subChunkEnd);
                        break;
                    case 259:
                        ReadPivotFixups(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in Hierarchy");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
            Hierarchy.AddHierarchy(hierarchyName, hierarchy);
        }

        //#######################################################################################
        //# Animation
        //#######################################################################################

        private static void ReadAnimationHeader(BinaryReader br)
        {
            Version version = GetVersion(ReadLong(br));
            string name = ReadFixedString(br);
            string hieraName = ReadFixedString(br);
            long numFrames = ReadLong(br);
            long frameRate = ReadLong(br);
        }

        private static void ReadAnimationChannel(BinaryReader br, uint ChunkEnd)
        {
            uint firstFrame = ReadShort(br);
            uint lastFrame = ReadShort(br);
            uint vectorLen = ReadShort(br);
            uint type = ReadShort(br);
            uint pivot = ReadShort(br);
            uint pad = ReadShort(br);

            switch (vectorLen)
            {
                case 1:
                    while (br.BaseStream.Position < ChunkEnd)
                    {
                        ReadFloat(br);
                    }
                    break;
                case 4:
                    while (br.BaseStream.Position < ChunkEnd)
                    {
                        ReadQuaternion(br);
                    }
                    break;
                default:
                    Console.WriteLine("invalid vector len: " + vectorLen + "in AnimationChannel");
                    while (br.BaseStream.Position < ChunkEnd)
                    {
                        ReadByte(br);
                    }
                    break;
            }
        }

        private static void ReadAnimation(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 513:
                        ReadAnimationHeader(br);
                        break;
                    case 514:
                        ReadAnimationChannel(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in Animation");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
        }

        private static void ReadCompressedAnimationHeader(BinaryReader br)
        {
            Version version = GetVersion(ReadLong(br));
            string name = ReadFixedString(br);
            string hieraName = ReadFixedString(br);
            long numFrames = ReadLong(br);
            uint frameRate = ReadShort(br);
            uint flavor = ReadShort(br);
        }

        private static void ReadTimeCodedAnimVector(BinaryReader br, uint ChunkEnd)
        {
            // A time code is a uint32 that prefixes each vector
            // the MSB is used to indicate a binary (non interpolated) movement
            uint magigNum = ReadShort(br);  //0 or 256 or 512 -> interpolation type? /compression of the Q-Channels?  (0, 256, 512) -> (0, 8, 16 bit)
            byte vectorLen = ReadByte(br);
            byte flag = ReadByte(br); //is x or y or z or quat
            uint timeCodesCount = ReadShort(br);
            uint pivot = ReadShort(br);

            // will be (NumTimeCodes * ((VectorLen * 4) + 4)) -> works if the magic num is 0
            // so only the Q-Channels are compressed?

            switch (vectorLen)
            {
                case 1:
                    while (br.BaseStream.Position < ChunkEnd)
                    {
                        ReadByte(br);
                    }
                    break;
                case 4:
                    while (br.BaseStream.Position < ChunkEnd)
                    {
                        ReadByte(br);
                    }
                    break;
                default:
                    Console.WriteLine("invalid vector len: " + vectorLen + "in TimeCodedAnimVector");
                    while (br.BaseStream.Position < ChunkEnd)
                    {
                        ReadByte(br);
                    }
                    break;
            }
        }

        private static void ReadCompressedAnimation(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 641:
                        ReadCompressedAnimationHeader(br);
                        break;
                    case 642:
                        br.ReadBytes((int)Chunksize);
                        break;
                    case 643:
                        br.ReadBytes((int)Chunksize);
                        break;
                    case 644:
                        ReadTimeCodedAnimVector(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in CompressedAnimation");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
        }

        //#######################################################################################
        //# HLod
        //#######################################################################################

        private static void ReadHLodHeader(BinaryReader br)
        {
            Version version = GetVersion(ReadLong(br));
            long lodCount = ReadLong(br);
            string modelName = ReadFixedString(br);
            string HTreeName = ReadFixedString(br);
        }

        private static void ReadHLodArrayHeader(BinaryReader br)
        {
            long modelCount = ReadLong(br);
            float maxScreenSize = ReadFloat(br);
        }

        private static void ReadHLodSubObject(BinaryReader br)
        {
            long boneIndex = ReadLong(br);
            string name = ReadLongFixedString(br);
        }

        private static void ReadHLodArray(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 1795:
                        ReadHLodArrayHeader(br);
                        break;
                    case 1796:
                        ReadHLodSubObject(br);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in HLodArray");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
        }

        private static void ReadHLod(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 1793:
                        ReadHLodHeader(br);
                        break;
                    case 1794:
                        ReadHLodArray(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in HLod");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
        }

        //#######################################################################################
        //# Box
        //#######################################################################################	

        private static void ReadBox(BinaryReader br)
        {
            Model.Box box = new Model.Box();
            Version version = GetVersion(ReadLong(br));
            long attributes = ReadLong(br);
            string name = ReadLongFixedString(br);
            Vector4 color = ReadRGBA(br);
            box.center = ReadVector(br);
            box.extend = ReadVector(br);

            model.box = box;
        }

        //#######################################################################################
        //# Texture
        //#######################################################################################	

        private static void ReadTexture(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                W3DMesh.Texture tex = new W3DMesh.Texture();
                switch (Chunktype)
                {
                    case 50:
                        texName = ReadString(br);
                        tex.type = W3DMesh.textureType.standard;
                        break;
                    case 51:
                        tex.attributes = ReadShort(br);
                        tex.animType = ReadShort(br);
                        tex.frameCount = ReadLong(br);
                        tex.frameRate = ReadFloat(br);
                        tex.type = W3DMesh.textureType.animated;
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in MeshTexture");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
                mesh.textures.Add(texName, tex);
            }
        }

        private static void ReadTextureArray(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 49:
                        ReadTexture(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in MeshTextureArray");
                        br.ReadBytes((int)Chunksize);
                        break; 
                }
            }
        }

        //#######################################################################################
        //# Material
        //#######################################################################################	

        private static void ReadMeshTextureCoordArray(BinaryReader br, uint ChunkEnd)
        {
            List<Vector2> txCoords = new List<Vector2>();
            while (br.BaseStream.Position < ChunkEnd)
            {
                txCoords.Add(new Vector2(ReadFloat(br), ReadFloat(br)));
            }
            mesh.texCoords.Add(txCoords.ToArray<Vector2>());
        }

        private static void ReadMeshTextureStage(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 73:
                        //texture ids
                        ReadLongArray(br, subChunkEnd);
                        break;
                    case 74:
                        ReadMeshTextureCoordArray(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in MeshTextureStage");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
        }

        private static void ReadMeshMaterialPass(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 57:
                        //vertex material ids
                        ReadLongArray(br, subChunkEnd);
                        break;
                    case 58:
                        //shader ids
                        ReadLongArray(br, subChunkEnd);
                        break;
                    case 59:
                        //vertex colors
                        //do nothing with them 
                        br.ReadBytes((int)Chunksize);
                        break;
                    case 63:
                        //unknown chunk
                        //size seems to be always 4
                        br.ReadBytes((int)Chunksize);
                        break;
                    case 72:
                        ReadMeshTextureStage(br, subChunkEnd);
                        break;
                    case 74:
                        ReadMeshTextureCoordArray(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in MeshMaterialPass");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
        }

        private static void ReadMaterial(BinaryReader br, uint ChunkEnd)
        {
            W3DMesh.Material mat = new W3DMesh.Material();
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 44:
                        matName = ReadString(br);
                        break;
                    case 45:
                        mat.vmAttributes = ReadLong(br);
                        mat.ambient = ReadRGBA(br);
                        mat.diffuse = ReadRGBA(br);
                        mat.specular = ReadRGBA(br);
                        mat.emissive = ReadRGBA(br);
                        mat.shininess = ReadFloat(br);
                        mat.opacity = ReadFloat(br);
                        mat.translucency = ReadFloat(br);
                        break;
                    case 46:
                        mat.vmArgs0 = ReadString(br);
                        break;
                    case 47:
                        mat.vmArgs1 = ReadString(br);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in MeshMaterial");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
            mesh.materials.Add(matName, mat);
        }

        private static void ReadMeshMaterialArray(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 43:
                        ReadMaterial(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in MeshMaterialArray");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
        }

        //#######################################################################################
        //# Vertices
        //#######################################################################################

        private static Vector3[] ReadMeshVerticesArray(BinaryReader br, uint ChunkEnd)
        {
            List<Vector3> vecs = new List<Vector3>();
            while (br.BaseStream.Position < ChunkEnd)
            {
                vecs.Add(ReadVector(br));
            }
            return vecs.ToArray<Vector3>();
        }
    
        private static void ReadMeshVertexInfluences(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint boneIdx = ReadShort(br);
                uint xtraIdx = ReadShort(br);
                uint boneInf = ReadShort(br)/100;
                uint xtraInf = ReadShort(br)/100;
            }
        }

        //#######################################################################################
        //# Faces
        //#######################################################################################	

        private static void ReadMeshFaceArray(BinaryReader br, uint ChunkEnd)
        {
            List<uint> vertIds = new List<uint>();
            while (br.BaseStream.Position < ChunkEnd)
            {
                vertIds.Add(ReadLong(br));
                vertIds.Add(ReadLong(br));
                vertIds.Add(ReadLong(br));
                uint attrs = ReadLong(br);
                Vector3 normal = ReadVector(br);
                float distance = ReadFloat(br);
            }
            mesh.vertIDs = vertIds.ToArray<uint>();
        }

        //#######################################################################################
        //# Shader
        //#######################################################################################

        private static void ReadMeshShaderArray(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                W3DMesh.Shader shader = new W3DMesh.Shader();
                shader.depthCompare = ReadByte(br);
                shader.depthMask = ReadByte(br);
                shader.colorMask = ReadByte(br);
                shader.destBlend = ReadByte(br);
                shader.fogFunc = ReadByte(br);
                shader.priGradient = ReadByte(br);
                shader.secGradient = ReadByte(br);
                shader.srcBlend = ReadByte(br);
                shader.texturing = ReadByte(br);
                shader.detailColorFunc = ReadByte(br);
                shader.detailAlphaFunc = ReadByte(br);
                shader.shaderPreset = ReadByte(br);
                shader.alphaTest = ReadByte(br);
                shader.postDetailColorFunc = ReadByte(br);
                shader.postDetailAlphaFunc = ReadByte(br);
                byte pad = ReadByte(br);

                mesh.shaders.Add(shader);
            }
        }

        //#######################################################################################
        //# Bump Maps
        //#######################################################################################

        private static void ReadNormalMapHeader(BinaryReader br)
        {
            byte number = ReadByte(br);
            string typeName = ReadLongFixedString(br);
            long reserved = ReadLong(br);
        }

        private static void ReadNormalMapEntryStruct(BinaryReader br, uint ChunkEnd, W3DMesh.Texture tex)
        {
            long type = ReadLong(br); //1 texture, 2 bumpScale/ specularExponent, 5 color, 7 alphaTest
            long size = ReadLong(br);
            string name = ReadString(br); 

            switch (name)
            {
                case "DiffuseTexture":
                    long unknown = ReadLong(br);
                    texName = ReadString(br);
                    break;
                case "NormalMap":
                    long unknown_nrm = ReadLong(br);
                    tex.normalMap = ReadString(br);
                    break;
                case "BumpScale":
                    tex.bumpScale = ReadFloat(br);
                    break;
                case "AmbientColor":
                    tex.ambientColor = ReadRGBA(br);
                    break;
                case "DiffuseColor":
                    tex.diffuseColor = ReadRGBA(br);
                    break;
                case "SpecularColor":
                    tex.specularColor = ReadRGBA(br);
                    break;
                case "SpecularExponent":
                    tex.specularExponent = ReadFloat(br);
                    break;
                case "AlphaTestEnable":
                    tex.alphaTestEnable = ReadByte(br);
                    break;
                default:
                    //Console.WriteLine("##W3D: unknown entryStruct: " + name + "   in MeshNormalMapEntryStruct (size: " + size + ")");
                    while (br.BaseStream.Position < ChunkEnd)
                        ReadByte(br);
                    break;
            }
        }

        private static void ReadNormalMap(BinaryReader br, uint ChunkEnd)
        {
            W3DMesh.Texture tex = new W3DMesh.Texture();
            tex.type = W3DMesh.textureType.bumpMapped;
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 82:
                        ReadNormalMapHeader(br);
                        break;
                    case 83:
                        ReadNormalMapEntryStruct(br, subChunkEnd, tex);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + " in MeshNormalMap");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
            mesh.textures.Add(texName, tex);
        }

        private static void ReadBumpMapArray(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 81:
                        ReadNormalMap(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + " in MeshBumpMapArray");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
        }

        //#######################################################################################
        //# AABTree (Axis-aligned-bounding-box)
        //#######################################################################################	

        private static void ReadAABTreeHeader(BinaryReader br, uint ChunkEnd)
        {
            long nodeCount = ReadLong(br);
            long polyCount = ReadLong(br);

            //padding of the header 
            while (br.BaseStream.Position < ChunkEnd)
            {
                br.ReadBytes(4);
            }
        }

        private static void ReadAABTreePolyIndices(BinaryReader br, uint ChunkEnd)
        {
            List<long> polyIndices = new List<long>();
            while (br.BaseStream.Position < ChunkEnd)
            {
                polyIndices.Add(ReadLong(br));
            }
        }

        private static void ReadAABTreeNodes(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                Vector3 min = ReadVector(br);
                Vector3 max = ReadVector(br);
                long frontOrPoly0 = ReadLong(br);
                long backOrPoly = ReadLong(br);
                // if within these, check their children
                // etc bis du irgendwann angekommen bist wos nur noch poly eintraege gibt dann hast du nen index und nen count parameter der dir sagt wo die polys die von dieser bounding box umschlossen sind liegen und wie viele es sind
                // die gehst du dann alle durch wo du halt einfach nen test machst ob deine position xyz in dem poly liegt oder ausserhalb        
            }
        }

        private static void ReadAABTree(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 145:
                        ReadAABTreeHeader(br, subChunkEnd);
                        break;
                    case 146:
                        ReadAABTreePolyIndices(br, subChunkEnd);
                        break;
                    case 147:
                        ReadAABTreeNodes(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in MeshAABTree");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
        }

        //######################################################################################
        //# mesh
        //######################################################################################

        private static void ReadMeshHeader(BinaryReader br)
        {
            Version version = GetVersion(ReadLong(br));
            uint attrs = ReadLong(br);
            string meshName = ReadFixedString(br);
            string containerName = ReadFixedString(br);
            uint faceCount = ReadLong(br);
            uint vertCount = ReadLong(br);
            uint matlCount = ReadLong(br);
            uint damageStageCount = ReadLong(br);
            uint sortLevel = ReadLong(br);
            uint prelitVersion = ReadLong(br);
            uint futureCount = ReadLong(br);
            uint vertChannelCount = ReadLong(br);
            uint faceChannelCount = ReadLong(br);
            Vector3 minCorner = ReadVector(br);
            Vector3 maxCorner = ReadVector(br);
            Vector3 sphCenter = ReadVector(br);
            float sphRadius = ReadFloat(br);

            mesh = new W3DMesh(faceCount);
            modelName = containerName;
        }

        private static void ReadMesh(BinaryReader br, uint ChunkEnd)
        {
            while (br.BaseStream.Position < ChunkEnd)
            {
                uint Chunktype = ReadLong(br);
                uint Chunksize = getChunkSize(ReadLong(br));
                uint subChunkEnd = (uint)br.BaseStream.Position + Chunksize;

                switch (Chunktype)
                {
                    case 2:
                        //mesh vertices
                        mesh.vertices = ReadMeshVerticesArray(br, subChunkEnd);
                        break;
                    case 3072:
                        //mesh vertices copy
                        //unused
                        br.ReadBytes((int)Chunksize);
                        break;
                    case 3:
                        //mesh normals
                        ReadMeshVerticesArray(br, subChunkEnd);
                        break;
                    case 3073:
                        //mesh normals copy
                        //unused
                        br.ReadBytes((int)Chunksize);
                        break;
                    case 12:
                        //mesh user text -> contains sometimes shader settings?
                        //unused
                        br.ReadBytes((int)Chunksize);
                        break;
                    case 14:
                        //mesh vertices infs
                        ReadMeshVertexInfluences(br, subChunkEnd);
                        break;
                    case 31:
                        //mesh header
                        ReadMeshHeader(br);
                        break;
                    case 32:
                        //mesh faces
                        ReadMeshFaceArray(br, subChunkEnd);
                        break;
                    case 34:
                        //mesh shade indices
                        ReadLongArray(br, subChunkEnd);
                        break;
                    case 40:
                        //mesh material set info
                        //unused
                        br.ReadBytes((int)Chunksize);
                        break;
                    case 41:
                        //mesh shader array
                        ReadMeshShaderArray(br, subChunkEnd);
                        break;
                    case 42:
                        //mesh material array
                        ReadMeshMaterialArray(br, subChunkEnd);
                        break;
                    case 48:
                        //mesh texture array
                        ReadTextureArray(br, subChunkEnd);
                        break;
                    case 56:
                        //mesh material pass
                        ReadMeshMaterialPass(br, subChunkEnd);
                        break;
                    case 80:
                        //mesh bump map array
                        ReadBumpMapArray(br, subChunkEnd);
                        break;
                    case 96:
                        //unknown chunk -> specular or diffuse normals??
                        br.ReadBytes((int)Chunksize);
                        break;
                    case 97:
                        //unknown chunk -> specular or diffuse normals??
                        br.ReadBytes((int)Chunksize);
                        break;
                    case 144:
                        //mesh aabtree
                        ReadAABTree(br, subChunkEnd);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in Mesh");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
            model.meshes.Add(mesh);
        }

        //######################################################################################
        //# main import
        //######################################################################################

        public static void Load(Stream s)
        {
            BinaryReader br = new BinaryReader(s);

            long filesize = s.Length;

            while (s.Position < filesize)
            {
                uint Chunktype = ReadLong(br);   
                uint Chunksize = getChunkSize(ReadLong(br));
                uint ChunkEnd = (uint)s.Position + Chunksize;

                switch (Chunktype)
                {
                    case 0:
                        //mesh
                        //if the w3d file contains mesh data create a new model object
                        if (model == null)
                        {
                            model = new Model();
                            Model.AddModel(modelName, model);
                        } 
                        ReadMesh(br, ChunkEnd);
                        break;
                    case 256:
                        //Hierarchy
                        ReadHierarchy(br, ChunkEnd);
                        break;
                    case 512:
                        //Animation
                        ReadAnimation(br, ChunkEnd);
                        break;
                    case 640:
                        //CompressedAnimation
                        ReadCompressedAnimation(br, ChunkEnd);
                        break;
                    case 1792:
                        //HLod
                        ReadHLod(br, ChunkEnd);
                        break;
                    case 1856:
                        //Box
                        ReadBox(br);
                        break;
                    default:
                        Console.WriteLine("unknown chunktype: " + Chunktype + "   in File");
                        br.ReadBytes((int)Chunksize);
                        break;
                }
            }
        }
    }
}
