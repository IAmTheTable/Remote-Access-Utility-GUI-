using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace Remote_Access_Utility_GUI_
{
    public class ConnectedClient
    {
        private byte[] buffer;
        private TcpClient client;

        public ConnectedClient(TcpClient baseClient)
        {
            this.client = baseClient;
            buffer = new byte[this.client.ReceiveBufferSize];
            int avail = this.client.Available;
            this.client.GetStream().BeginRead(buffer, 0, this.client.Available, OnDataRead, avail);
        }
        public static string Txt(byte[] text) => System.Text.Encoding.ASCII.GetString(text);

        private void OnDataRead(IAsyncResult ar)
        {
            int avail = (int)ar.AsyncState;
            // amt of bytes recieved
            int amtRead = this.client.GetStream().EndRead(ar);
            
            if(avail == amtRead)
            {
                Logger.Log($"Recieved packet\n{Txt(buffer)}");
            }
            else
            {
                Logger.Error("packet size mismatch", avail, amtRead);
            }

            this.client.GetStream().BeginRead(buffer, 0, this.client.Available, OnDataRead, this.client.Available);
        }

        public void Write(byte[] data)
        {
            this.client.GetStream().Write(data, 0, data.Length);
        }

    }
}
