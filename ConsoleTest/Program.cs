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
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            new AlphabetPrinter('%').Print("Glen!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadKey(true);

            ColoredConsoleWriter.WriteLine(ColoredConsoleWriter.TranslateAlternateColorCodes('&', "&00&11&22&33&44&55&66&77&88&99&aa&bb&cc&dd&ee&ff"));
            Console.ReadKey(true);
            Console.Clear();
            ColoredConsoleWriter.WriteLine(String.Format(ColoredConsoleWriter.TranslateAlternateColorCodes('&', "&aYou entered: &r'{0}'"), ConsoleReader.PromptHidden("Enter a password: ", '\u00B7')));
            Console.ReadKey(true);
        }
    }
}
