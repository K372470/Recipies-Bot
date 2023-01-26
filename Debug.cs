
using System;

namespace ReceipeBot
{
    public static class Debug
    {

#if DEBUG
        public static void Log(object message)
        {
            Console.ResetColor();
            Console.WriteLine(message.ToString());
        }
#else
    public static void Log(object message)
        {
        }

#endif
        public static void Warn(object message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message.ToString());
            Console.ResetColor();
        }
        public static void Error(object message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]" + message.ToString());
            Console.ResetColor();
        }
    }
}
