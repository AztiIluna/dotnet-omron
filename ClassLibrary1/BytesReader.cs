using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class BytesReader
    {
        private List<byte> bytes;
        private int pointer = 0;
        public BytesReader(IEnumerable<byte> bytes)
        {
            this.bytes = new List<byte>(bytes);
        }

        public void AdvancePointer(int count) { pointer += count; }

        public byte PeekOne() 
        {
            return bytes[pointer];
        }

        public byte PeekOneAt(int position)
        {
            return bytes[pointer + position];
        }

        public short ReadShort()
        {
            pointer += 2;
            return BitConverter.ToInt16(bytes.Slice(pointer - 2, 2).ToArray(), 0);
        }

        public byte ReadOne()
        {
            pointer++;
            return bytes[pointer - 1];
        }

        public List<byte> PeekMany(int count)
        {
            return bytes.Slice(pointer, count);
        }

        public List<byte> ReadMany(int count)
        {
            pointer += count;
            return bytes.Slice(pointer - count, count);
        }

        public bool CanReadCount(int count) => pointer + count <= bytes.Count;
    }
}
