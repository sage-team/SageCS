using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Core.Loaders
{
    class ConstFile
    {
        enum ConstItemType
        {
            TYPE_UNDEF = 0,
            TYPE_STRING = 1,
            TYPE_NUMBER = 4,
        };

        struct ConstItem
        {
            public ConstItemType type;
            public object value;   
        };

        struct ConstData
        {
            public UInt32 aptdataoffset;
            public UInt32 itemcount;
            public List<ConstItem> items;
        };

        private static UInt32 ReverseBytes(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }

        private ConstData data;

        public string ReadCString(BinaryReader br)
        {
            var offset = br.ReadUInt32();
            var oldPos = br.BaseStream.Position;            
            br.BaseStream.Seek(offset, SeekOrigin.Begin);
            StringBuilder sb = new StringBuilder();
            char c = br.ReadChar();
            while (c!=0)
            {
                sb.Append(c);
                c = br.ReadChar();
            }
            br.BaseStream.Seek(oldPos, SeekOrigin.Begin);
            return sb.ToString();
        }

        public void Load(Stream s)
        {
            data = new ConstData();
            data.items = new List<ConstItem>();
            BinaryReader br = new BinaryReader(s);
            var magic = new String(br.ReadChars(20));
            data.aptdataoffset = br.ReadUInt32();
            data.itemcount = br.ReadUInt32();
            s.Seek(4, SeekOrigin.Current);

            for(var i= 0;i< data.itemcount;++i)
            {
                ConstItem item = new ConstItem();
                item.type = (ConstItemType)br.ReadUInt32();
                if (item.type == ConstItemType.TYPE_NUMBER)
                    item.value = br.ReadUInt32();
                else if (item.type == ConstItemType.TYPE_STRING)
                    item.value = ReadCString(br);

                data.items.Add(item);
            }
        }
    }
}
