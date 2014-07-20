using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What qualifies as useful?");
            ConsoleReader.WaitResponse();
            Console.Clear();
            Console.BufferWidth = 500;
            Console.BufferHeight = 600;
            Console.ForegroundColor = ConsoleColor.Green;
            AlphabetPrinter prntr = new AlphabetPrinter('%');
            prntr.Print("Next To");
            Console.WriteLine();
            Thread.Sleep(750);
            prntr.Print("Nearly");
            Console.WriteLine();
            Thread.Sleep(750);
            prntr.Print("Almost");
            Console.WriteLine();
            Thread.Sleep(750);
            prntr.Print("Absolutely");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Cyan;
            new AlphabetPrinter('*').Print("Nothing!");
            Console.ForegroundColor = ConsoleColor.Gray;
            ConsoleReader.WaitResponse();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            ColoredConsoleWriter.WriteLine(ColoredConsoleWriter.TranslateAlternateColorCodes('&', "&00&11&22&33&44&55&66&77&88&99&aa&bb&cc&dd&ee&ff"));
            ConsoleReader.WaitResponse();
            Console.Clear();
            ColoredConsoleWriter.WriteLine(String.Format(ColoredConsoleWriter.TranslateAlternateColorCodes('&', "&aYou entered: &r'{0}'"), ConsoleReader.PromptHidden("Enter a password: ", '\u00B7')));
            ConsoleReader.WaitResponse();
        }
    }
}
