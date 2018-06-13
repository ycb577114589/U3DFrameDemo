//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using UnityEngine;
//using ProtoBuf;
////using cs;

//namespace TcpClinet
//{
//    class Message
//    {
//        private byte[] data = new byte[1024];
//        private int startIndex = 0;

//        public byte[] Data
//        {
//            get { return data; }
//        }
//        public int StartIndex
//        {
//            get { return startIndex; }
//        }
//        public int RemainSize
//        {
//            get { return data.Length - startIndex; }
//        }
//        public void AddCount(int count)
//        {
//            startIndex += count;
//        }
//        /// <summary>
//        /// 解析数据
//        /// </summary>
//        public void ReadMessage()
//        {
//            while (true)
//            {
//                if (startIndex <= 4)
//                {
//                    return;
//                }
//                int count = BitConverter.ToInt32(data, 0);
//                if ((startIndex - 4) >= count)
//                {
//                    string s = System.Text.Encoding.UTF8.GetString(data, 4, count);
//                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
//                    startIndex -= count + 4;
//                    //Debug.Log(count+" "+s); 
//                }
//                else break;
//            }
//        }
//        public void ReadProtoMessage()
//        {
//            while (true)
//            {
//                if (startIndex <= 4)
//                    return;
//                int count = BitConverter.ToInt32(data, 0);
//                if ((startIndex - 4) >= count)
//                {
//                    int typeId = BitConverter.ToInt32(data, 4);
//                    if (typeId == (int)EnmCmdID.CS_LOGIN_RES)
//                    {
//                        byte[] msg = new byte[count - 4];
//                        Array.Copy(data, 8, msg, 0, count - 4);
//                        CSLoginRes ServerRes = PackCodec.Deserialize<CSLoginRes>(msg);
//                        UInt32 res = ServerRes.result_code;
//                        Debug.Log("数据内容：res =" + res);
//                        Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
//                        startIndex -= count + 4;
//                    }
//                }
//            }
//        }
//    }
//}
