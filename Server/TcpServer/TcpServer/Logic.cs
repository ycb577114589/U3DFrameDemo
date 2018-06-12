using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace TcpServer
{
    class Logic
    {
        private static readonly Logic _Instance = new Logic();


        public List<HomeMessage> _beginHome = new List<HomeMessage>();
        public static Logic Instance
        {
            get
            {
                return _Instance;
            }
        }
        public void ResponseStart(Socket socket)
        {
            HomeLogicManager.Instance.TryStartBattle(socket);
        }

        public void Timer()
        {
            Thread timer = new Thread(new ThreadStart(DispatchMessage));
            timer.IsBackground = true;
            timer.Start();
        } 
        public void DispatchMessage()
        {
            while (true)
            {
                foreach (var item in _beginHome)
                {
                    ProtocolSCFrameNotify protocol = new ProtocolSCFrameNotify();
                    item._switchQueue.Push(protocol);
                    item._switchQueue.Switch();
                    while (!item._switchQueue.Empty())
                    {
                        protocol = item._switchQueue.pop();
                        protocol.data.NextFrame = item.nextFrame;
                        protocol.data.CurrentFrame = item.currentFrame;
                        foreach (var person in item._listSocket)
                        {
                            Program.Send(protocol, person);
                        }
                    }
                    item.nextFrame++;
                    item.currentFrame++;
                }
                Thread.Sleep(33);
            }
        }

    }
    public class GameLogic
    { 
    }

}
