using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TcpServer
{
    class Message
    {
        private byte[] data = new byte[10024];
        private int startIndex = 0;
        private MemoryStream _recvStream = new MemoryStream(65536 * 20);

        public byte[] Data
        {
            get { return data; }
        }
        public int StartIndex
        {
            get { return startIndex; }
        }
        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }
        public void AddCount(int count)
        {
            startIndex += count;
        }
        /// <summary>
        /// 解析数据
        /// </summary>
        ///googole.protobuf
        public void ReadProtobufMessage(Socket socket)
        {
            while (true)
            {
                if (startIndex < 4)
                    return;
                int count = BitConverter.ToInt32(data, 0);
                if ((startIndex - 4) >= count)
                {
                    int typeId = BitConverter.ToInt16(data, 4);
                    Protocol protocol = Protocol.GetProtocolThreadSafe(typeId);
                    if (protocol == null)
                    {
                        Console.WriteLine("protocol is null");
                        return;
                    }
                    else
                    {
                        protocol.ThreadErrorCode = ProtocolErrorCode.NO_ERROR;

                        _recvStream.Seek(0, SeekOrigin.Begin);
                        _recvStream.SetLength(0);
                        _recvStream.Write(data, 6, count - 2);
                        _recvStream.Seek(0, SeekOrigin.Begin);
                        protocol.DeSerialize(_recvStream);    
                        protocol.Process(socket);
                    }
                    Protocol.ReturnProtocolThreadSafe(protocol);
                    startIndex -= count + 4;
                }
                else break;
            }
        }
    }
}
