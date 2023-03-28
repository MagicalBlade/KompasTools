using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KompasTools.Utils
{
    static class SearchUtils
    {
        /// <summary>
        /// Поиск папки
        /// </summary>
        static public IEnumerable SearchFolder(string? orderRequest, string? searchPath)
        {
            // TODO 1: При попытки поиска с символом * выдается ошибка, разобраться
            Regex reg = new($@"{orderRequest}\d*\.\D");
            if (!Directory.Exists(searchPath))
            {
                return "";
            }
            IEnumerable<string> folders = Directory.EnumerateDirectories(searchPath, "*")
                                            .Where(s => reg.IsMatch(s));
            return folders;
        }
    }
}
