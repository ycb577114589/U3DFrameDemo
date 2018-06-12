using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class ProtocolHead
    {
        public static ProtocolHead SharedHead = new ProtocolHead();
        public static Byte[] SharedUIntBuffer = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0,};
        public Int32 len;
        public UInt16 msgId;
        public ProtocolHead()
        {
            Reset();
        }
        public void Reset()
        {
            len = 0;
            msgId = 0;
        }
        public int Size
        {
            get
            {
                return 6;
            }
        }
        public UInt16 ToUInt16(byte[] bytes,int startIndex)
        {
            UInt16 res = 0;
            for(int i = 0; i < 2; i++)
            {
                res |= bytes[i + startIndex];
                res <<= 8;
            }
            return res;
        }
        public int ToInt32(byte[] bytes,int startIndex)
        {
            int res = 0;
            for(int i = 0; i < 4; i++)
            {
                res |= (bytes[i + startIndex]);
                res <<= 8;
            }
            return res;
        }
        public void Deserialize(byte [] bytes)
        {
            len = ToInt32(bytes, 0);
            msgId = ToUInt16(bytes, 4);
        }
        public byte[] GetBytes(int value)
        {
            for(int i = 0; i < 4; i++)
            {
                SharedUIntBuffer[i] = (byte)(value & 0xFF);
                value >>= 8;
            }
            return SharedUIntBuffer;
        }
        public void Seriliaze(MemoryStream stream)
        {

            stream.Write(GetBytes(len), 0, 4);

            stream.Write(GetBytes(msgId), 0, 2);
        }
    }
}
