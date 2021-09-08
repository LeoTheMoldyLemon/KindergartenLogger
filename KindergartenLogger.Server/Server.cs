using SimpleTcp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenLogger.Server
{
    class Server
    {
        public static SimpleTcpServer server;

        Server(string ServerIP)
        {
            server = new SimpleTcpServer(ServerIP);
            server.Start();
            server.Logger = ServerLogger;
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.ClientDisconnected += Events_ClientDisconnected;
            server.Events.DataReceived += Events_DataReceived;
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {

        }

        private void Events_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {

        }

        private void Events_ClientConnected(object sender, ClientConnectedEventArgs e)
        {

        }

        static void ServerLogger(string msg)
        {
            using (StreamWriter sw = File.AppendText("./log.txt"))
            {
                sw.WriteLine(msg);
            }
        }








    }
}




