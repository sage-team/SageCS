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
            public UInt32 numvalue;
            public string strvalue;
        };

        struct ConstData
        {
            public UInt32 aptdataoffset;
            public UInt32 itemcount;
            public List<ConstItem> items;
        };

        private ConstData data;

        public void Load(Stream s)
        {
            data = new ConstData();
            BinaryReader br = new BinaryReader(s);
            s.Seek(20, SeekOrigin.Begin);
            data.aptdataoffset = br.ReadUInt32();
            data.itemcount = br.ReadUInt32();
            s.Seek(4, SeekOrigin.Current);

            foreach(var item in Enumerable.Range(0,(int)data.itemcount))
            {

            }
        }
    }
}
