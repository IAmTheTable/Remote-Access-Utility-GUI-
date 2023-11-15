namespace Remote_Access_Utility_GUI_
{
    public partial class Form1 : Form
    {
        public static byte[] Bytes(string text) => System.Text.Encoding.ASCII.GetBytes(text);
        public static string Txt(byte[] text) => System.Text.Encoding.ASCII.GetString(text);
        public Form1()
        {
            InitializeComponent();
            Logger.form = this;
            Logger.rt = richTextBox1;
            Logger.lb = label1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Server.ServerManager.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Server.ServerManager.connectedClients.Values.First().Write(Bytes("1"));
        }
    }
}