
namespace MainClient
{
	class PtcRegister
	{   
        /// <summary>
        /// 注册所有从服务端接收到的协议类型
        /// </summary>
		public static void RegistProtocol()
        {
            Protocol.RegistProtocol(new ProtoResponseStart());
            Protocol.RegistProtocol(new ProtocolStartGameNotify());
            Protocol.RegistProtocol(new ProtocolSCFrameNotify());
        }
	}
}
