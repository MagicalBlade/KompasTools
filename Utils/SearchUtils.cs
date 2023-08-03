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
        /// <summary>
        /// Поиск позиции
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <param name="searchPath"></param>
        /// <returns></returns>
        static public IEnumerable<FileInfo> SearchFile(string orderRequest, string? searchPath)
        {
            List<FileInfo> filesInfo = new List<FileInfo>();
            if (!Directory.Exists(searchPath))
            {
                return filesInfo;
            }
            // TODO: Решить проблему со спец сиволами по типу *. они вызывают ошибки
            Regex reg = new($@"\D{orderRequest}\D");
            IEnumerable<string> files = Directory.EnumerateFiles(searchPath, "*")
                .Where(s => reg.IsMatch(s));
            foreach (string folder in files)
            {
                FileInfo fileInfo = new FileInfo(folder);
                //filesInfo.Add(fileInfo);
                
                if (!fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    filesInfo.Add(fileInfo);
                }
                
            }
            return filesInfo;
        }

        /// <summary>
        /// Поиск Марки
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <param name="searchPath"></param>
        /// <returns></returns>
        static public IEnumerable<FileInfo> SearchMark(string orderRequest, string? searchPath)
        {
            List<FileInfo> filesInfo = new List<FileInfo>();
            if (!Directory.Exists(searchPath))
            {
                return filesInfo;
            }
            // TODO: Решить проблему со спец сиволами по типу *. они вызывают ошибки
            Regex reg = new($@"{orderRequest}");
            IEnumerable<string> files = Directory.EnumerateFiles(searchPath, "*")
                .Where(s => reg.IsMatch(s));
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
