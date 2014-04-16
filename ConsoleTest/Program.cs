using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 150;
            new AlphabetPrinter('%').Print("Glen!");
            Console.ReadKey(true);

            ColoredConsoleWriter.WriteLine(ColoredConsoleWriter.TranslateAlternateColorCodes('&', "&00&11&22&33&44&55&66&77&88&99&aa&bb&cc&dd&ee&ff"));
            Console.ReadKey(true);
        }
    }
}
