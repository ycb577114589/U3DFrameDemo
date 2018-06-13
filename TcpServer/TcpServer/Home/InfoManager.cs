using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class InfoManager
    {
        private static readonly InfoManager _Instance = new InfoManager();
        public static InfoManager Instance
        {
            get
            {
                return _Instance;
            }
        }

        public void CSFrameMessage(Socket socket,CSFrameNotify data)
        {   
            HomeMessage homeMessage = HomeLogicManager.Instance._personToHome[socket];
            ProtocolSCFrameNotify protocol = new ProtocolSCFrameNotify();
            protocol.data.Keys.Add(data);
            homeMessage._switchQueue.Push(protocol);
        }
        public void CSRequestStart(Socket socket)
        {
            PersonManager.Instance.RequsetStart(socket);
            HomeLogicManager.Instance.ResponseStart(socket);
        }
        public void ClientClose( Socket socket)
        {
            PersonManager.Instance._listPersonStats[socket] = PersonManager.PersonStats.CLOSE;
            HomeLogicManager.Instance.ClosePerson(socket);
        }
    }
}   