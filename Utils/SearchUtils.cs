using System;
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
        static public void SearchFolder(string? orderRequest, string searchPath)
        {
            Regex reg = new($@"\D*{orderRequest}\D*");
            IEnumerable<string> folders =  Directory.EnumerateDirectories(searchPath, "*", SearchOption.AllDirectories)
                .Where(s => reg.IsMatch(s));
            foreach (string folder in folders) { System.Windows.MessageBox.Show($"{folder}");}
            
        }
    }
}
