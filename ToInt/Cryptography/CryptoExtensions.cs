using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


namespace Glib.Cryptography
{
    /// <summary>
    /// A class providing cryptographic extensions on strings.
    /// </summary>
    public static class CryptoExtensions
    {
        /// <summary>
        /// Compute the hexadecimal MD5 hash of the specified string.
        /// </summary>
        /// <remarks>
        /// Uses a MD5CryptoServiceProvider and a BitConverter.
        /// </remarks>
        /// <param name="s">The string to hash.</param>
        /// <returns>The MD5 hash of the input string.</returns>
        public static string MD5Hash(this string s)
        {
            MD5CryptoServiceProvider hasha = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hasha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s))).Replace("-", "");
        }

        /// <summary>
        /// Compute the hexadecimal SHA256 hash of the specified string.
        /// </summary>
        /// <remarks>
        /// Uses a SHA256CryptoServiceProvider and a BitConverter.
        /// </remarks>
        /// <param name="s">The string to hash.</param>
        /// <returns>The SHA256 hash of the input string.</returns>
        public static string SHA256Hash(this string s)
        {
            SHA256CryptoServiceProvider hasha = new SHA256CryptoServiceProvider();
            return BitConverter.ToString(hasha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s))).Replace("-", "");
        }

        /// <summary>
        /// Compute the hexadecimal SHA1 hash of the specified string.
        /// </summary>
        /// <param name="s">The string to hash.</param>
        /// <returns>The SHA1 hash of the input string.</returns>
        public static string SHA1Hash(this string s)
        {
            SHA1CryptoServiceProvider hasha = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(hasha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s))).Replace("-", "");
        }

        /// <summary>
        /// Compute the hexadecimal SHA512 hash of the specified string.
        /// </summary>
        /// <remarks>
        /// Uses a SHA512CryptoServiceProvider and a BitConverter.
        /// </remarks>
        /// <param name="s">The string to hash.</param>
        /// <returns>The hexadecimal SHA512 hash of the specified string.</returns>
        public static string SHA512Hash(this string s)
        {
            System.Security.Cryptography.SHA512CryptoServiceProvider hasha = new System.Security.Cryptography.SHA512CryptoServiceProvider();
            return BitConverter.ToString(hasha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s))).Replace("-", "");
        }
    }
}
