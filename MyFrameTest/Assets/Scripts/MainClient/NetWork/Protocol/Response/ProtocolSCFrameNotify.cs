using System.IO;
using Google.Protobuf;



namespace MainClient
{
    class ProtocolSCFrameNotify : Protocol
    {
        public SCFrameNotify data = new SCFrameNotify();

        public override ushort GetMessageID()
        {
            return 4;
        }
        public override void Process()
        {
            GameSyncManager.Instance.OnFrameAsyn(data);
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