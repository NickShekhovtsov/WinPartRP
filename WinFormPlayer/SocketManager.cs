using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace WinFormPlayer
{
    class SocketManager
    {
        public Socket client;
        public string msg;
        public SocketManager()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect("127.0.0.1", 8000);
        }
        
    }
}
