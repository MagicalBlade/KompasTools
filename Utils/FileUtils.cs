using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KompasTools.Utils
{
    internal static class FileUtils
    {
        public static void WriteGlobalLog(string text)
        {
            using (StreamWriter sw = new("Global.log", true))
            {
                sw.WriteLine(text);
            }
            return;
        }
    }
}
