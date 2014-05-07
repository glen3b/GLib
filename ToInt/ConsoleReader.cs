using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib
{
    /// <summary>
    /// A class allowing customized reading of specialized values.
    /// </summary>
    public static class ConsoleReader
    {
        /// <summary>
        /// Does not return before the user presses a key.
        /// </summary>
        public static void WaitResponse()
        {
            Console.ReadKey(true);
        }

        /// <summary>
        /// Prompts for a string from the console. This is done by printing the prompt, no newline added, and reading in the resulting user input. The characters typed by the user are not displayed to the console.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user. Null is interpreted as the empty string.</param>
        /// <returns>The input of the user from the console after displaying the prompt.</returns>
        public static String PromptHidden(string prompt)
        {
            return PromptHidden(prompt, NullChar);
        }

        /// <summary>
        /// Prompts for a string from the console. This is done by printing the prompt with a newline added and reading in the resulting user input. The characters typed by the user are not displayed to the console.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user. Null is interpreted as the empty string.</param>
        /// <returns>The input of the user from the console after displaying the prompt.</returns>
        public static String PromptLineHidden(string prompt)
        {
            return PromptLineHidden(prompt, NullChar);
        }

        /// <summary>
        /// Prompts for a string from the console. This is done by printing the prompt, no newline added, and reading in the resulting user input. The characters typed by the user are not displayed to the console.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user. Null is interpreted as the empty string.</param>
        /// <param name="placeholderValue">The value to display instead of inputted characters. If UTF null ('\0') is specified, this character is not printed.</param>
        /// <returns>The input of the user from the console after displaying the prompt.</returns>
        public static String PromptHidden(string prompt, char placeholderValue)
        {
            Console.Write(prompt == null ? String.Empty : prompt);
            return ReadLineHidden(placeholderValue);
        }

        /// <summary>
        /// Prompts for a string from the console. This is done by printing the prompt with a newline added and reading in the resulting user input. The characters typed by the user are not displayed to the console.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user. Null is interpreted as the empty string.</param>
        /// <param name="placeholderValue">The value to display instead of inputted characters. If UTF null ('\0') is specified, this character is not printed.</param>
        /// <returns>The input of the user from the console after displaying the prompt.</returns>
        public static String PromptLineHidden(string prompt, char placeholderValue)
        {
            Console.WriteLine(prompt == null ? String.Empty : prompt);
            return ReadLineHidden(placeholderValue);
        }

        /// <summary>
        /// Prompts for a string from the console. This is done by printing the prompt, no newline added, and reading in the resulting user input.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user. Null is interpreted directly by the Console.Write method.</param>
        /// <returns>The input of the user from the console after displaying the prompt.</returns>
        public static String Prompt(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        /// <summary>
        /// Prompts for a string from the console. This is done by printing the prompt with a newline added and reading in the resulting user input.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user. Null is interpreted directly by the Console.WriteLine method.</param>
        /// <returns>The input of the user from the console after displaying the prompt.</returns>
        public static String PromptLine(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        /// <summary>
        /// Represents the UTF8 null character.
        /// </summary>
        public const char NullChar = '\0';

        /// <summary>
        /// Reads a line from console, not displaying the characters as they are typed.
        /// </summary>
        /// <returns>The string of characters inputted by the user.</returns>
        public static String ReadLineHidden()
        {
            return ReadLineHidden(NullChar);
        }

        /// <summary>
        /// Reads a line from console, displaying placeholderValue instead of the characters as they are typed.
        /// </summary>
        /// <param name="placeholderValue">The value to display instead of inputted characters. If UTF null ('\0') is specified, this character is not printed.</param>
        /// <returns>The string of characters inputted by the user.</returns>
        public static String ReadLineHidden(char placeholderValue)
        {
            StringBuilder result = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Delete)
                {
                    if (result.Length > 0)
                    {
                        // Backspace last character and continue
                        if (placeholderValue != NullChar)
                        {
                            Console.Write("\b \b");
                        }
                        result.Remove(result.Length - 1, 1);
                    }
                    continue;
                }
                
                char input = Char.ToLowerInvariant(key.KeyChar);
                
                
                if (Char.IsControl(input) || Char.IsLowSurrogate(input) || Char.IsHighSurrogate(input) || key.Key == ConsoleKey.Enter)
                {
                    // Ignore
                    continue;
                }
                if (key.Modifiers.HasFlag(ConsoleModifiers.Shift))
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.D0:
                            input = ')';
                            break;
                        case ConsoleKey.D1:
                            input = '!';
                            break;
                        case ConsoleKey.D2:
                            input = '@';
                            break;
                        case ConsoleKey.D3:
                            input = '#';
                            break;
                        case ConsoleKey.D4:
                            input = '$';
                            break;
                        case ConsoleKey.D5:
                            input = '%';
                            break;
                        case ConsoleKey.D6:
                            input = '^';
                            break;
                        case ConsoleKey.D7:
                            input = '&';
                            break;
                        case ConsoleKey.D8:
                            input = '*';
                            break;
                        case ConsoleKey.D9:
                            input = '(';
                            break;
                        case ConsoleKey.OemPlus:
                            input = '+';
                            break;
                        default:
                            input = Char.ToUpperInvariant(input);
                            break;
                    }
                }
                result.Append(input);
                if (placeholderValue != NullChar)
                {
                    Console.Write(placeholderValue);
                }
            } while (key.Key != ConsoleKey.Enter);


            Console.WriteLine();
            return result.ToString();
        }
    }
}
