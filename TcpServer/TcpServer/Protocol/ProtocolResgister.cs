using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class ProtocolResgister
    {
        public static void RegistProtocol()
        {
            Protocol.RegistProtocol(new ProtocolFrameNotify());
            Protocol.RegistProtocol(new ProtolRequestStart());
        }
    }
}
