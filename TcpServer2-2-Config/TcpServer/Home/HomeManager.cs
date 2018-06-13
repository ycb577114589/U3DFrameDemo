using CommonLib;
using System;
using System.Collections.Generic;
using System.Net.Sockets; 
namespace TcpServer
{
    public class HomeMessage
    {
        public List<Socket> _listSocket = new List<Socket>();
        public Dictionary<Socket, ulong> _dirPersonToInt = new Dictionary<Socket, ulong>();

        public SwitchQueue<ProtocolSCFrameNotify> _switchQueue = new SwitchQueue<ProtocolSCFrameNotify>();

        public ulong num = 0;
        public static uint RoomIdSum = 0;
        public uint RoomId;
        public uint currentFrame;
        public uint nextFrame;
        public bool isStart = false;
        public HomeMessage()
        {
            RoomId = ++RoomIdSum;
        }
    }
    public class HomeLogicManager
    {
        private static readonly HomeLogicManager _Instance = new HomeLogicManager();
        public static HomeLogicManager Instance
        {
            get
            {
                return _Instance;
            }
        }

        public  Dictionary<Socket, HomeMessage> _personToHome = new Dictionary<Socket, HomeMessage>();
        public  Queue<Socket> _waitForStart = new Queue<Socket>();
        public  List<Socket> listPerson = new List<Socket>();

        public  void AddPerson(Socket socket)
        {
            listPerson.Add(socket);
        }
        public void TryStartBattle(Socket socket)
        {
            while (_waitForStart.Count > 0)
            {
                HomeMessage _hMessage=BeforStartBattle(1);
                StartBattle(_hMessage);
            }
        }
        public HomeMessage BeforStartBattle(int number)
        {   
            HomeMessage _hMessage = new HomeMessage();
            while ( number -- != 0)
            {   
                Socket person = _waitForStart.Dequeue();
                _hMessage._listSocket.Add(person);
                _personToHome[person] = _hMessage;
                PersonManager.Instance._listPersonStats[person] = PersonManager.PersonStats.BATTLE;
            }   
            return _hMessage;
        }
        public void ResponseStart(Socket socket)
        {
            _waitForStart.Enqueue(socket);
            TryStartBattle(socket);
        }
        public void StartBattle(HomeMessage homeMessage)
        {   
            ProtocolStartGameNotify pbSCStartNotify = new ProtocolStartGameNotify();
            foreach (var item in homeMessage._listSocket)
            {
                ProtoResponseStart pb = new ProtoResponseStart();
                pb.data.Uin = ++homeMessage.num;
                Program.Send(pb, item);
                pbSCStartNotify.data.Uins.Add(pb.data.Uin);
            }

            foreach(var item in homeMessage._listSocket)
            {
                Program.Send(pbSCStartNotify, item);
            }
            homeMessage.currentFrame = 0; 
            homeMessage.nextFrame = 1;   
            homeMessage.isStart = true;
            Logic.Instance._beginHome.Add(homeMessage);
        }

        public void ClosePerson(Socket socket)
        {
            _personToHome[socket]._listSocket.Remove(socket);
            _personToHome.Remove(socket);
        }
    } 
}
