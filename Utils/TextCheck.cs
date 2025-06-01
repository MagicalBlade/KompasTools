using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KompasTools.Utils
{
    internal static class TextCheck
    {
        private static readonly Regex _regex = new("[^0-9.]+"); //regex that matches disallowed text
        /// <summary>
        /// Ввод только чисел. В том числе дробных
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsTextAllowed(string text)
        {
            return _regex.IsMatch(text);
        }
    }
}
