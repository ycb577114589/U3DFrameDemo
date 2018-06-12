using System.IO;
using Google.Protobuf;



namespace MainClient
{
    class ProtoResponseStart : Protocol
    {
        public SCResponseStart data = new SCResponseStart();

        public override ushort GetMessageID()
        {
            return 1;
        }
        public override void Process()
        {
            GameClient.Instance.OnLoginSuccess(data.Uin);
        }
        public override void Serialize(MemoryStream stream)
        {
            data.WriteTo(stream);
        }
        public override void DeSerialize(MemoryStream stream)
        {
            data = SCResponseStart.Parser.ParseFrom(stream);
        }
    }
}