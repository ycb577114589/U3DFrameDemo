using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public enum SocketState
    {
        STATE_CLOSED,
        STATE_CONNECTING,
        STATE_CONNECTED,
    }
    public enum NetErrorCode
    {
        NET_NO_ERROR,
        NET_SYSTEERROR,
        NET_CONNECTE_ERROR,
        NET_RECONNECT_FAILED,
        NET_RECEIVEBUFF_OVERFLOW,
        NET_SENDBUFF_OVERFLOW,
        NET_UNKNOW_EXCEPTION,
    }
    public enum ProtocolErrorCode
    {
        NO_ERROR,
        DESERIALIZED_ERROR,
        NULL_PROTOCOl,
        PROCESS_ERROR,
    }
}
