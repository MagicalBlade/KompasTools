using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace KompasTools.Utils
{
    static class SearchUtils
    {
        /// <summary>
        /// Поиск папки
        /// </summary>
        static public List<FileInfo> SearchFolder(string? orderRequest, string? searchPath)
        {
            // TODO 1: При попытки поиска с символом * выдается ошибка, разобраться
            List<FileInfo> foldersInfo = new List<FileInfo>();
            Regex reg = new($@"{orderRequest}\d*\.\D");
            if (!Directory.Exists(searchPath))
            {
                return foldersInfo;
            }
            IEnumerable<string> folders = Directory.EnumerateDirectories(searchPath, "*")
                                            .Where(s => reg.IsMatch(s));
            foreach (string folder in folders)
            {
                foldersInfo.Add(new FileInfo(folder));
                
            }
            return foldersInfo;
        }

        static public IEnumerable<FileInfo> SearchFile(string? orderRequest, string? searchPath)
        {
            List<FileInfo> filesInfo = new List<FileInfo>();
            if (!Directory.Exists(searchPath))
            {
                return filesInfo;
            }
            IEnumerable<string> files = Directory.EnumerateFiles(searchPath, "*");
            foreach (string folder in files)
            {
                FileInfo fileInfo = new FileInfo(folder);
                if (!fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    filesInfo.Add(fileInfo);
                }
            }
            return filesInfo;
        }
    }
}
