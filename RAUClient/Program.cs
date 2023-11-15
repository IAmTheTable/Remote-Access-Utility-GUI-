using System.Net.Sockets;
using System.Diagnostics;
namespace RAUClient
{
    internal class Program
    {
        private static byte[] buffer;

        public static TcpClient client = new();

        public static byte[] Bytes(string text) => System.Text.Encoding.ASCII.GetBytes(text);
        public static string Txt(byte[] text) => System.Text.Encoding.ASCII.GetString(text);

        static async Task Main(string[] args)
        {
            client.Connect("127.0.0.1", 5000);
            buffer = new byte[client.ReceiveBufferSize];

            new Thread(() =>
            {
                int avail = client.Available;
                client.GetStream().BeginRead(buffer, 0, client.Available, OnDataRead, avail);
                
            }).Start();
            
            while (true)
            {
                client.GetStream().Write(Bytes(Console.ReadLine()));
            }
            await Task.Delay(-1);
        }

        private static void OnDataRead(IAsyncResult ar)
        {
            int avail = (int)ar.AsyncState;
            // amt of bytes recieved
            int amtRead = client.GetStream().EndRead(ar);

            if (avail == amtRead)
            {
                Logger.Log($"Recieved packet\n{Txt(buffer)}");

                if(Txt(buffer).Replace("\0", "") == "1")
                {
                    var p = Process.Start(new ProcessStartInfo()
                    {
                        FileName = "notepad.exe",
                        RedirectStandardInput = true
                    });
                    p.StandardInput.Write("HELLOOO LOSER");
                }
            }
            else
            {
                Logger.Error("packet size mismatch", avail, amtRead);
            }

            client.GetStream().BeginRead(buffer, 0, client.Available, OnDataRead, client.Available);
        }
    }
}