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

            ColoredConsoleWriter.WriteLine(ColoredConsoleWriter.TranslateAlternateColorCodes('&', "&8Hello &aglen3b&8, I see your rank is &eserver owner&8."));
            Console.ReadKey(true);
        }
    }
}
