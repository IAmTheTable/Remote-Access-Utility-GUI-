using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote_Access_Utility_GUI_
{
    public static class Logger
    {
        public static Form1 form; // active form
        public static RichTextBox rt;// rich text box
        public static Label lb; // status label
        
        private static ConsoleColor def = Console.ForegroundColor;
   

        public static void WriteLb(params object[] items)
        {
            form.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                lb.Text = ($"Status: {string.Join(", ", items)}\n");
            });
        }
        
        public static void Log(params object[] items)
        {
            form.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                rt.AppendText($"[INFO] {string.Join(", ", items)}\n");
            });
        }

        public static void Warn(params object[] items)
        {
            form.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                rt.AppendText($"[WARN] {string.Join(", ", items)}\n");
            });
        }

        public static void Error(params object[] items)
        {
            form.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                rt.AppendText($"[ERRO] {string.Join(", ", items)}\n");
            });
        }
    }
}
