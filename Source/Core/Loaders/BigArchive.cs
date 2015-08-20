using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Core.Loaders
{
    class BigArchive
    {
        class BigLoadException : Exception
        {
            public BigLoadException(string message)
            {
            }

        }
        private static UInt32 ReverseBytes(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }


        private static UInt32 ReadUint32LE(BinaryReader br)
        {
            UInt32 result = br.ReadUInt32();
            if (BitConverter.IsLittleEndian)
                result = ReverseBytes(result);

            return result;
        }

        public static SortedDictionary<string,Stream> GetEntries(string archive)
        {
            SortedDictionary<string, Stream> result = new SortedDictionary<string, Stream>();
            BinaryReader br = new BinaryReader(File.OpenRead(archive));
            StringBuilder sb = new StringBuilder();

            var magic = new String(br.ReadChars(4));
            if (magic != "BIG4" && magic != "BIGF")
            {
                throw new BigLoadException(archive + " is not a valid BIG4 File");
            }

            UInt32 filesize = br.ReadUInt32();
            UInt32 numEntries = ReadUint32LE(br);
            UInt32 offset = ReadUint32LE(br);

            for (var i = 0;i<numEntries;++i)
            {                             
                var entryOffset = ReadUint32LE(br);
                var entrySize = ReadUint32LE(br);

                char c = br.ReadChar();
                while(c!=0)
                {
                    sb.Append(c);
                    c = br.ReadChar();
                }
                var bs = new BigStream(br.BaseStream, entryOffset, entrySize, sb.ToString());
                result.Add(sb.ToString(), bs);
                sb.Clear();
            }
                                 
            return result;  
        }
    }
}
