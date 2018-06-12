using Google.Protobuf;
using System.IO;
using System.Net;

namespace TcpServer
{
    class ProtocolStartGameNotify : Protocol
    {
        public SCStartGame data = new SCStartGame();

        public override ushort GetMessageID()
        {
            return 2;
        }
        public override void Serialize(MemoryStream stream)
        {
            data.WriteTo(stream);
        }
        public override void DeSerialize(MemoryStream stream)
        {
            data = SCStartGame.Parser.ParseFrom(stream);
        }
    }
}
