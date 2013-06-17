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
            //new AlphabetPrinter('▀').Print("1-(123)-j");
            Console.BufferWidth = 150;
            //new AlphabetPrinter('╔').Print("1-(818)-360-2428");
            new AlphabetPrinter('←').Print("0123456789");
            Console.ReadKey(true);
            
        }
    }
}
