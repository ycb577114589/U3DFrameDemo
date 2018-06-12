﻿using System.IO;
using System.Net.Sockets;
using Google.Protobuf;
using CommonLib;
namespace TcpServer
{
    class ProtocolFrameNotify : Protocol
    { 
        public CSFrameNotify data = new CSFrameNotify();

        public override ushort GetMessageID()
        {
            return 3;
        }
        public override void Serialize(MemoryStream stream)
        {
            data.WriteTo(stream);
        }
        public override void DeSerialize(MemoryStream stream)
        {
            data = CSFrameNotify.Parser.ParseFrom(stream);
        }

        public override void Process(Socket socket )
        {
            HomeMessage homeMessage = HomeLogicManager.Instance._personToHome[socket];

            ProtocolSCFrameNotify protocol =new ProtocolSCFrameNotify();
            protocol.data.Keys.Add( data ); 
            homeMessage._switchQueue.Push(protocol);
        }   
    }
}
