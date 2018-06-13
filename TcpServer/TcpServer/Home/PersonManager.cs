using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class PersonManager
    {
        private static readonly PersonManager _Instance = new PersonManager();
        private List<Socket> _listPerson = new List<Socket>();
        public Dictionary<Socket, PersonStats> _listPersonStats = new Dictionary<Socket, PersonStats>();

        public static PersonManager Instance
        {
            get
            {
                return _Instance;
            }
        }
        public enum Stats
        {
            CONNECTED,
            CONNECTING,
            CONNECTCLOSED
        }
        public enum PersonStats
        {   
            CLOSE,
            IDLE,
            WAIT,
            BATTLE,
        }   
        public void RequsetStart(Socket socket)
        {   
            _listPersonStats[socket] = PersonStats.WAIT;
            return;
        }   
    }
}
