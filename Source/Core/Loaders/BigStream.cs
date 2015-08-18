using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SageCS.Core.Loaders
{
    class BigStream : Stream
    {
        private int offset;
        private int length;
        private Stream parent;
        private int position = 0;

        public override bool CanRead
        {
            get
            {
                return (parent.CanRead && position<length);
            }
        }

        public override bool CanSeek
        {
            get
            {
                return parent.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return parent.CanWrite;
            }
        }

        public override long Length
        {
            get
            {
                return length;
            }
        }

        public override long Position
        {
            get
            {
                return position;
            }

            set
            {
                position = (int)value;
            }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            parent.Seek(this.offset+position,SeekOrigin.Begin);
            int result = parent.Read(buffer, offset, count);
            position += result;
            return result;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch(origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;              
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = length+offset;
                    break;
            }

            if (Position > Length)
                Position = Length;

            return Position;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public BigStream(Stream p,uint off, uint len)
        {
            parent = p;
            offset = (int)off;
            length = (int)len;
            position = 0;
        }
    }
}
