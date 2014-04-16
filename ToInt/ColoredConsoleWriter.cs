using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.Collections;

namespace Glib
{
    /// <summary>
    /// A static class allowing for writing of colors to the console output.
    /// </summary>
    public static class ColoredConsoleWriter
    {
        /// <summary>
        /// The character that precedes color formatting characters.
        /// </summary>
        public static readonly char FormatterChar = '\u00A7';

        /// <summary>
        /// Writes a string, which will be color formatted, to console. It is followed by a newline.
        /// </summary>
        /// <param name="output">The string to output.</param>
        public static void WriteLine(string output)
        {
            Write(output + Environment.NewLine);
        }

        private static Dictionary<Char, ConsoleColor> _colors;
        private static ReadOnlyDictionary<Char, ConsoleColor> _publicColors;

        /// <summary>
        /// Internal static initializer.
        /// </summary>
        static ColoredConsoleWriter()
        {
            _colors = new Dictionary<char, ConsoleColor>();
            _colors.Add('A', ConsoleColor.Green);
            _colors.Add('B', ConsoleColor.Cyan);
            _colors.Add('C', ConsoleColor.Red);
            _colors.Add('D', ConsoleColor.Magenta);
            _colors.Add('E', ConsoleColor.Yellow);
            _colors.Add('F', ConsoleColor.White);
            _colors.Add('0', ConsoleColor.Black);
            _colors.Add('1', ConsoleColor.DarkBlue);
            _colors.Add('2', ConsoleColor.DarkGreen);
            _colors.Add('3', ConsoleColor.DarkCyan);
            _colors.Add('4', ConsoleColor.DarkRed);
            _colors.Add('5', ConsoleColor.DarkMagenta);
            _colors.Add('6', ConsoleColor.DarkYellow);
            _colors.Add('7', ConsoleColor.Gray);
            _colors.Add('8', ConsoleColor.DarkGray);
            _colors.Add('9', ConsoleColor.Blue);
            _colors.Add('R', ConsoleColor.Gray);
            _publicColors = new ReadOnlyDictionary<char, ConsoleColor>(_colors);
        }

        /// <summary>
        /// Gets a read only view of the dictionary mapping characters following <see cref="FormatterChar"/> to console colors.
        /// </summary>
        public static IDictionary<Char, ConsoleColor> ColorMapping
        {
            get
            {
                return _publicColors;
            }
        }

        /// <summary>
        /// Convert messages using color codes indicated by a nonstandard character to using the standard character.
        /// </summary>
        /// <param name="alternateCode">The alternate color code indicator.</param>
        /// <param name="message">The message to translate.</param>
        /// <returns>The translated message.</returns>
        public static string TranslateAlternateColorCodes(Char alternateCode, String message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            char[] chars = message.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i + 1 < chars.Length)
                {
                    if (chars[i].Equals(alternateCode) && ColorMapping.ContainsKey(Char.ToUpperInvariant(chars[i + 1])))
                    {
                        // Replace the formatting character.
                        chars[i] = ColoredConsoleWriter.FormatterChar;
                    }
                }
            }

            return new String(chars);
        }

        /// <summary>
        /// Writes a string, which will be color formatted, to console.
        /// </summary>
        /// <param name="output">The string to output.</param>
        public static void Write(string output)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            char[] outputChars = output.ToCharArray();

            Console.ForegroundColor = ConsoleColor.Gray;

            for (int i = 0; i < outputChars.Length; i++)
            {
                if (i + 1 < outputChars.Length)
                {
                    if (outputChars[i].Equals(FormatterChar) && ColorMapping.ContainsKey(Char.ToUpperInvariant(outputChars[i + 1])))
                    {
                        // Format with this formatting character.
                        Console.ForegroundColor = ColorMapping[Char.ToUpperInvariant(outputChars[i + 1])];
                        i += 2;
                    }
                }

                if (i >= outputChars.Length)
                {
                    break;
                }

                Console.Write(outputChars[i]);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
