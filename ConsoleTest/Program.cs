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
            new AlphabetPrinter('%').Print("vickie!");
            Console.ReadKey(true);
            
        }
    }
}
