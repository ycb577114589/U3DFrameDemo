//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Net;
//using System.Net.Sockets;
//using UnityEngine;
//using ProtoBuf;
//using cs;

//namespace TcpClinet
//{
//    class MyClient
//    {
//        static byte[] data = new byte[1024];
//        public static Socket socketClient;
//        static Message msg = new Message();

//        public static void Connect(string ip, int port)
//        {

//            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            socketClient.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
//            socketClient.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, socketClient);
//        }

//        static void ReceiveCallBack(IAsyncResult ar)
//        {
//            //            ar.AsyncState 可以获得之前传递的object
//            Socket socketClient = null;
//            try
//            {
//                socketClient = ar.AsyncState as Socket;
//                int count = socketClient.EndReceive(ar);
//                if (count == 0)
//                {
//                    //即便是空数据也不会是0，客户端不会发送空数据，  只有在结束时才有0的可能性
//                    socketClient.Close();
//                    return;
//                }
//                msg.AddCount(count);
//                msg.ReadProtoMessage();
//                socketClient.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, socketClient);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                if (socketClient != null)
//                {
//                    socketClient.Close();
//                }
//            }
//        }
//        //protobuf
//        public static void Send(int typeId, IExtensible msgStr)
//        {
//            byte[] typeBytes = BitConverter.GetBytes(typeId);
//            byte[] dataBytes = PackCodec.Serialize(msgStr);
//            int msgLen = typeBytes.Length + dataBytes.Length;
//            byte[] lengthBytes = BitConverter.GetBytes(msgLen);
//            byte[] newBytes = lengthBytes.Concat(typeBytes).ToArray().Concat(dataBytes).ToArray();
//            socketClient.Send(newBytes);
//            //总长度(int32) + typeid(int32) + 报文    
//        }
//        //非protobuf
//        public static void Send(string msgStr)
//        {
//            int count = socketClient.Receive(msg.Data);
//            byte[] dataBytes = Encoding.UTF8.GetBytes(msgStr);
//            int msgLen = msgStr.Length;
//            byte[] lengthBytes = BitConverter.GetBytes(msgLen);
//            byte[] newBytes = lengthBytes.Concat(dataBytes).ToArray();
//            socketClient.Send(newBytes);

//        }

//        public static void Close()
//        {
//            socketClient.Close();
//        }
//    }
//}