using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEC_OOP_Opgaver.Tools
{
    public static class ConsoleHelper
    {
        public static void PrintCol(string text, ConsoleColor color = ConsoleColor.White, bool newLine = true)
        {
            Console.ForegroundColor = color;
            if (newLine)
                Console.WriteLine(text);
            else
                Console.Write(text);
            Console.ResetColor();
        }
    }
}