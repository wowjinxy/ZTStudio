using System;
using System.Data;
using System.Linq;
using Microsoft.VisualBasic;

namespace ZTStudio
{
    static class StringExtensions
    {

        /// <summary>
    /// Reverse hex method to allow for easier switching around of bytes
    /// </summary>
    /// <remarks>In computing, endianness refers to the order of bytes (or sometimes bits) within a binary representation of a number</remarks>
    /// <param name="StrInput">String - bytes/hex values to reverse</param>
    /// <returns></returns>
        public static string ReverseHex(this string StrInput)
        {
            string StrReturn;
            var LstStrings = new List<string>();
            LstStrings.AddRange(Enumerable.Range(0, (int)Math.Round(StrInput.Length / 2d)).Select(x => StrInput.Substring(x * 2, 2)).ToList());
            LstStrings.Reverse();
            StrReturn = Strings.Join(LstStrings.ToArray(), " ");

            // return reverse, with spaces
            return Strings.Trim(StrReturn);
        }
    }
}