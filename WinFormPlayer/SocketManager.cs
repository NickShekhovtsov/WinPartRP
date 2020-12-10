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
        public SocketManager(string ip,int port)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(ip, port);
                Console.WriteLine("Connected to server");
            }
            catch (Exception e)
            {
                Console.WriteLine("Server doesn't work");
            }
        }
    }
}