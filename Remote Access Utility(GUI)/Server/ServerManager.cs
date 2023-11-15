using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;


namespace Remote_Access_Utility_GUI_.Server
{
    public static class ServerManager
    {
        public static Dictionary<string, ConnectedClient> connectedClients = new();


        /// <summary>
        /// Server instance
        /// </summary>
        private static TcpListener server;
        /// <summary>
        /// internal value
        /// </summary>
        private static bool isListening = false;

        public static string ServerIP = "127.0.01";
        public static int ServerPort = 5000;
        public static bool IsListening
        {
            get => isListening;
        }

        private static Thread activeThread = null;


        public static void Start()
        {
            server = new(ServerPort);
            server.Start();
            Logger.Log($"Server is starting..");

            isListening = true;

            // initiate server listening
            activeThread = new Thread(() =>
            {
                while (isListening)
                {
                    server.BeginAcceptTcpClient(OnClientConnected, null);
                    Logger.WriteLb("Listening");
                }

                Logger.WriteLb("Not Listening");
            });

            activeThread.Start();
        }


        private static void OnClientConnected(IAsyncResult ar)
        {
            var client = server.EndAcceptTcpClient(ar);
            var clientId = Guid.NewGuid().ToString();

            Logger.Log($"{DateTime.Now}] Client connected from {client.Client.RemoteEndPoint} with id({clientId}).");
            
            // initialization will happen on instantiation
            connectedClients.Add(clientId, new(client));
        }


        public static void SendCommand(string clientId, byte[] cmd)
        {
            connectedClients[clientId].Write(cmd);
        }
    }
}

