﻿using Google.Protobuf;
using System.IO;

namespace TcpServer
{
    public class ProtocolSCFrameNotify : Protocol
    {
        public SCFrameNotify data = new SCFrameNotify();

        public override ushort GetMessageID()
        {
            return 4;
        } 
        public override void Serialize(MemoryStream stream)
        {
            data.WriteTo(stream);
        }
        public override void DeSerialize(MemoryStream stream)
        {
            data = SCFrameNotify.Parser.ParseFrom(stream);
        }
    }
}