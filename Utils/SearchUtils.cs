using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KompasTools.Utils
{
    static class SearchUtils
    {
        /// <summary>
        /// Поиск папки
        /// </summary>
        static public void SearchFolder(string? _orderRequest, string searchPath)
        {
            IEnumerable<string> folders =  Directory.EnumerateDirectories(searchPath, "*", SearchOption.AllDirectories).Where(s => s.EndsWith("123"));
            foreach (string folder in folders) { System.Windows.MessageBox.Show($"{folder}");}
            
        }
    }
}
