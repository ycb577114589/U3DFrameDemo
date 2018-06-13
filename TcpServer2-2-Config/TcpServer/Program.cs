using System;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace TcpServer
{
    class Program 
    {
        static Socket socketServer;
        static byte[] dataBuffer = new byte[1024];
         
        static void Main(string[] args)
        {
            Init();
            ProtocolResgister.RegistProtocol();
            StartServerAsync();
            
            Console.ReadKey();
        }
        public static void Init()
        {
            Logic.Instance.Timer();
        }
         
        static void StartServerAsync()
        {
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 5678);
            socketServer.Bind(iPEndPoint);

            socketServer.Listen(10);
            //Socket clientSocket = socketServer.Accept(); 下面支持多个客户端，改为异步， 之前c++服务器用的epoll这里似乎不需要关注
            socketServer.BeginAccept(AcceptCallBack, socketServer);
        }
        static Message msg = new Message();

        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket socketServer = ar.AsyncState as Socket;
            Socket socketClient = socketServer.EndAccept(ar);
            HomeLogicManager.Instance.AddPerson(socketClient);

            socketClient.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, socketClient);
            socketServer.BeginAccept(AcceptCallBack, socketServer);
        }   
        #region recv
        static void ReceiveCallBack(IAsyncResult ar)
        {
            //            ar.AsyncState 可以获得之前传递的object
            Socket socketClient = null;
            try
            {
                socketClient = ar.AsyncState as Socket;
                int count = socketClient.EndReceive(ar);
                if (count == 0)
                {
                     //即便是空数据也不会是0，客户端不会发送空数据，  只有在结束时才有0的可能性
                    Close(socketClient);
                    return;
                }
                msg.AddCount(count);
                msg.ReadProtobufMessage(socketClient);
                socketClient.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, socketClient);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (socketClient != null)
                {
                    Close(socketClient);
                }
            }
        }
        #endregion
        #region close
        public static void Close(Socket socket)
        {
            InfoManager.Instance.ClientClose(socket);
            socket.Close();
        }
        #endregion
        #region send

        private static MemoryStream _sendStream=new MemoryStream();
        private static ProtocolHead _prtHead = new ProtocolHead();
        public static bool Send(Protocol protocol,Socket socket)
        {
            _sendStream.SetLength(0);
            _sendStream.Position = 0;
            protocol.SerializeWithHead(_sendStream);

            if (Send(_sendStream.GetBuffer(), 0, (int)_sendStream.Length, socket))
            {
                return true;
            }
            return false;
        }   
        public static bool Send(Byte[] buffer,int start,int len,Socket socket)
        {
            if (buffer == null)
            {
                return false;
            }
            try
            {
                socket.BeginSend(buffer, 0, len, SocketFlags.None, null, socket);
            }
            catch
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}