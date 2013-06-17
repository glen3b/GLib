using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib
{
    /// <summary>
    /// A class for printing alphabetical letters to a string and the console.
    /// </summary>
    public class AlphabetPrinter
    {
        /// <summary>
        /// The character to make letters out of.
        /// </summary>
        public char LetterChar { get; set; }

        /// <summary>
        /// Create a new AlphabetPrinter creating letters made out of the specified character.
        /// </summary>
        /// <param name="printChar">The character to make letters out of.</param>
        public AlphabetPrinter(char printChar)
        {
            LetterChar = printChar;
        }

        /// <summary>
        /// The character A.
        /// </summary>
        public static bool[,] A
        {
            get
            {
                return new bool[5, 5]
                {
                    { false, true, true, true, false },
                    { true, false, false, false, true },
                    { true, true, true, true, true },
                    { true, false, false, false, true },
                    { true, false, false, false, true }
                };
            }
        }

        /// <summary>
        /// The character B.
        /// </summary>
        public static bool[,] B
        {
            get
            {
                return new bool[5, 5]
                {
                    { true, true, true, false, false },
                    { true, false, false, true, false },
                    { true, true, true, true, false },
                    { true, false, false, false, true },
                    { true, true, true, true, false }
                };
            }
        }

        /// <summary>
        /// The character C.
        /// </summary>
        public static bool[,] C
        {
            get
            {
                return new bool[5, 5]
                {
                    /*  ^^^^
                     * ^
                     * ^
                     * ^
                     *  ^^^^
                     */
                    { false, true, true, true, true },
                    { true, false, false, false, false },
                    { true, false, false, false, false },
                    { true, false, false, false, false },
                    { false, true, true, true, true }
                };
            }
        }

        /// <summary>
        /// The character D.
        /// </summary>
        public static bool[,] D
        {
            get
            {
                return new bool[5, 5]
                {
                    /* ^^^^
                     * ^   ^
                     * ^   ^
                     * ^   ^
                     * ^^^^
                     */
                    { true, true, true, true, false },
                    { true, false, false, false, true },
                    { true, false, false, false, true },
                    { true, false, false, false, true },
                    { true, true, true, true, false }
                };
            }
        }

        /// <summary>
        /// The character E.
        /// </summary>
        public static bool[,] E
        {
            get
            {
                return new bool[5, 5]
                {
                    /* ^^^^^
                     * ^    
                     * ^^^^^
                     * ^    
                     * ^^^^^
                     */
                    { true, true, true, true, true },
                    { true, false, false, false, false },
                    { true, true, true, true, true },
                    { true,false, false, false, false },
                    { true, true, true, true, true }
                };
            }
        }

        /// <summary>
        /// The character F.
        /// </summary>
        public static bool[,] F
        {
            get
            {
                return new bool[5, 5]
                {
                    /* ^^^^^
                     * ^    
                     * ^^^^^
                     * ^    
                     * ^
                     */
                    { true, true, true, true, true },
                    { true, false, false, false, false },
                    { true, true, true, true, true },
                    { true,false, false, false, false },
                    { true, false, false, false, false }
                };
            }
        }

        /// 
        /// <summary>
        /// The character G.
        /// </summary>
        public static bool[,] G
        {
            get
            {
                return new bool[5, 5]
                {
                    /*  ^^^^
                     * ^  
                     * ^ ^^^
                     * ^  ^
                     *  ^^^
                     */
                    { false, true, true, true, true },
                    { true, false, false, false, false },
                    { true, false, true, true, true },
                    { true,false, false, true, false },
                    { false, true, true, true, false }
                };
            }
        }
        /// <summary>
        /// The character H.
        /// </summary>
        public static bool[,] H
        {
            get
            {
                return new bool[5, 5]
                {
                    /* ^   ^
                     * ^   ^
                     * ^^^^^
                     * ^   ^
                     * ^   ^
                     */
                    { true, false, false, false, true },
                    { true, false, false, false, true },
                    { true, true, true, true, true },
                    { true,false, false, false, true },
                    { true, false, false, false, true }
                };
            }
        }

        /// <summary>
        /// The character I.
        /// </summary>
        public static bool[,] I
        {
            get
            {
                return new bool[5, 5]
                {
                    /* ^^^^^
                     *   ^  
                     *   ^  
                     *   ^  
                     * ^^^^^
                     */
                    { true, true, true, true, true },
                    { false, false, true, false, false },
                    { false, false, true, false, false },
                    { false,false, true, false, false },
                    { true, true, true, true, true }
                };
            }
        }

        /// <summary>
        /// The character J.
        /// </summary>
        public static bool[,] J
        {
            get
            {
                return new bool[5, 5]
                {
                    /* ^^^^^
                     *     ^ 
                     *     ^
                     * ^  ^
                     *  ^^
                     */
                    { true, true, true, true, true },
                    { false, false, false, false, true },
                    { false, false, false, false, true },
                    { true,false, false, true, false },
                    { false, true, true, false, false }
                };
            }
        }

        /// <summary>
        /// A space character.
        /// </summary>
        public static bool[,] Space
        {
            get
            {
                // All false
                return new bool[5, 5];
            }
        }

        /// <summary>
        /// An exclamation mark.
        /// </summary>
        public static bool[,] ExclamationMark
        {
            get
            {
                return new bool[5, 1]{
                    {true},
                    {true},
                    {true},
                    {false},
                    {true}
                    
                };
            }
        }

        /// <summary>
        /// The pound sign.
        /// </summary>
        public static bool[,] Pound
        {
            get
            {
                return new bool[5, 5]{
                    { false, true, false, true, false },
                    { true, true, true, true, true },
                    { false, true, false, true, false },
                    { true, true, true, true, true },
                    { false, true, false, true, false }
                };
            }
        }

        /// <summary>
        /// A hyphen character.
        /// </summary>
        public static bool[,] Hyphen
        {
            get
            {
                /* |
                 * |
                 * |****
                 * |
                 * |
                 */
                return new bool[5, 4]{
                    { false,false,false,false },
                    { false,false,false,false },
                    { true, true, true, true },
                    { false,false,false,false },
                    { false,false,false,false }
                };
            }
        }

        /// <summary>
        /// The character representing left parentheses.
        /// </summary>
        public static bool[,] LeftParentheses
        {
            get
            {
                return new bool[5, 4]{
                    { false,false,true,false },
                    { false,true,false,false },
                    { false, true, false, false },
                    { false,true,false,false},
                    { false,false,true,false }
                };
            }
        }

        /// <summary>
        /// The character representing right parentheses.
        /// </summary>
        public static bool[,] RightParentheses
        {
            get
            {
                return new bool[5, 4]{
                    { false,true,false,false },
                    { false,false,true,false },
                    { false, false, true, false },
                    { false,false,true,false},
                    { false,true,false,false }
                };
            }
        }

        /// <summary>
        /// The number one.
        /// </summary>
        public static bool[,] One
        {
            get
            {
                /* |  *
                 * | **
                 * |* *
                 * |  *
                 * |*****
                 */
                return new bool[5, 5]{
                    { false,false,true,false,false },
                    { false,true,true,false,false },
                    { true, false, true, false, false },
                    { false,false,true,false,false },
                    { true,true,true,true,true }
                };
            }
        }

        /// <summary>
        /// The number two.
        /// </summary>
        public static bool[,] Two
        {
            get
            {
                /* |***
                 * |   *
                 * |   *
                 * |  *
                 * |*****
                 */
                return new bool[5, 5]{
                    { true,true,true,false,false },
                    { false,false,false,true,false },
                    { false, false, false, true, false },
                    { false,false,true,false,false },
                    { true,true,true,true,true }
                };
            }
        }

        /// <summary>
        /// The number three.
        /// </summary>
        public static bool[,] Three
        {
            get
            {
                /* |****
                 * |    *
                 * |****
                 * |    *
                 * |****
                 */
                return new bool[5, 5]{
                    { true,true,true,true,false },
                    { false,false,false,false,true },
                    { true,true,true,true,false },
                    { false,false,false,false,true },
                    { true,true,true,true,false }
                };
            }
        }

        /// <summary>
        /// Gets a list of custom characters specific to this instance, in the format of key: input letter, value: bool[,] representing the letter.
        /// </summary>
        public List<KeyValuePair<string, bool[,]>> CustomChars
        {
            get
            {
                return _customChars;
            }
        }

        private List<KeyValuePair<string, bool[,]>> _customChars  = new List<KeyValuePair<string, bool[,]>>();

        /// <summary>
        /// The number four.
        /// </summary>
        public static bool[,] Four
        {
            get
            {
                /* |*  * 
                 * |*  *
                 * |*****
                 * |   *
                 * |   *
                 */
                return new bool[5, 5]{
                    { true,false,false,true,false },
                    { true,false,false,true,false },
                    { true,true,true,true,true },
                    { false,false,false,true,false },
                    { false,false,false,true,false }
                };
            }
        }

        /// <summary>
        /// The number five.
        /// </summary>
        public static bool[,] Five
        {
            get
            {
                /* |*****
                 * |*    
                 * |*****
                 * |    *
                 * |****
                 */
                return new bool[5, 5]{
                    { true,true,true,true,true },
                    { true,false,false,false,false },
                    { true,true,true,true,true },
                    { false,false,false,false,true },
                    { true,true,true,true,false }
                };
            }
        }

        /// <summary>
        /// The character S.
        /// </summary>
        public static bool[,] S
        {
            get
            {
                /* | ****
                 * |*    
                 * | *** 
                 * |    *
                 * |****
                 */
                return new bool[5, 5]{
                    { false,true,true,true,true },
                    { true,false,false,false,false },
                    { false,true,true,true,false },
                    { false,false,false,false,true },
                    { true,true,true,true,false }
                };
            }
        }

        /// <summary>
        /// Get the large alphabet string for the specified word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The word as a multiline alphabet string.</returns>
        public string GetAlphabetString(string word)
        {
            StringBuilder returnValue = new StringBuilder();
            //int letterSize = 5 + 2;         //5 is the letter size; 2 is the space between letters
            StringBuilder[] rows = new StringBuilder[5];
            for (int letterCount = 0; letterCount < word.Length; letterCount++)
            {
                char letter = word[letterCount];

                bool[,] printArray = new bool[5, 5];
                switch (letter.ToString().ToUpper())
                {
                    case "A":
                        printArray = A;
                        break;

                    case "B":
                        printArray = B;
                        break;
                    case "C":
                        printArray = C;
                        break;
                    case "D":
                        printArray = D;
                        break;
                    case "E":
                        printArray = E;
                        break;
                    case "F":
                        printArray = F;
                        break;
                    case "G":
                        printArray = G;
                        break;
                    case "H":
                        printArray = H;
                        break;
                    case "I":
                        printArray = I;
                        break;
                    case "J":
                        printArray = J;
                        break;
                    case "S":
                        printArray = S;
                        break;
                    case "1":
                        printArray = One;
                        break;
                    case "2":
                        printArray = Two;
                        break;
                    case "3":
                        printArray = Three;
                        break;
                    case "4":
                        printArray = Four;
                        break;
                    case "5":
                        printArray = Five;
                        break;
                    case "(":
                        printArray = LeftParentheses;
                        break;
                    case ")":
                        printArray = RightParentheses;
                        break;
                    case " ":
                        printArray = Space;
                        break;
                    case "-":
                        printArray = Hyphen;
                        break;
                    case "!":
                        printArray = ExclamationMark;
                        break;
                    case "#":
                        printArray = Pound;
                        break;
                    default:
                        foreach (KeyValuePair<string, bool[,]> kvp in CustomChars)
                        {
                            if (kvp.Key.ToUpper() == letter.ToString().ToUpper())
                            {
                                printArray = kvp.Value;
                                break;
                            }
                        }
                        throw new NotSupportedException("The specified word contains an unsupported character.");
                }

                for (int row = 0; row < printArray.GetLength(0); row++)
                {
                    StringBuilder crow = rows[row] == null ? new StringBuilder() : rows[row];


                    for (int col = 0; col < printArray.GetLength(1); col++)
                    {
                        string output = " ";
                        if (printArray[row, col])
                        {
                            output = LetterChar.ToString();
                        }

                        crow.Append(output);
                    }
                    crow.Append("  ");
                    rows[row] = crow;
                    //rows.Add(crow);
                }
            }
            foreach (StringBuilder sb in rows)
            {
                returnValue.AppendLine(sb.ToString());
            }
            return returnValue.ToString();
        }

        /// <summary>
        /// Print the specified word to console.
        /// </summary>
        /// <param name="word">The word to print.</param>
        public void Print(string word)
        {
            /*
            int letterSize = 5 + 2;         //5 is the letter size; 2 is the space between letters

            for (int letterCount = 0; letterCount < word.Length; letterCount++)
            {
                char letter = word[letterCount];

                bool[,] printArray = new bool[5, 5];
                switch (letter.ToString().ToUpper())
                {
                    case "A":
                        printArray = A;
                        break;

                    case "B":
                        printArray = B;
                        break;
                }

                for (int row = 0; row < 5; row++)
                {
                    Console.SetCursorPosition(letterSize * letterCount, row);

                    for (int col = 0; col < 5; col++)
                    {
                        string output = " ";
                        if (printArray[row, col])
                        {
                            output = LetterChar.ToString();
                        }

                        Console.Write(output);
                    }

                    Console.WriteLine();
                }
            }
            */
            Console.Write(GetAlphabetString(word));
        }
    }
}
