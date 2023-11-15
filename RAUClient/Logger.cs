using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAUClient
{
    public static class Logger
    {
        private static ConsoleColor def = Console.ForegroundColor;
        
        private static void reset()
        {
            Console.ForegroundColor = def;
        }
        
        public static void Log(params object[] items)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[INFO] {String.Join(", ", items)}");
            reset();
        }

        public static void Warn(params object[] items)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARN] {String.Join(", ", items)}");
            reset();
        }

        public static void Error(params object[] items)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERRO] {String.Join(", ", items)}");
            reset();
        }
    }
}
