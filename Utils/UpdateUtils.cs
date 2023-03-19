using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using KompasTools.Classes;
using KompasTools.ViewModels;

namespace KompasTools.Utils
{
    internal static class UpdateUtils
    {
        public static void CheckUpdate(ConfigData configData)
        {
            string? exeSelfPath = Environment.ProcessPath;
            string? nameSelf = Assembly.GetExecutingAssembly().GetName().Name;
            int processId = Environment.ProcessId;
            string curSelfDir = Environment.CurrentDirectory;
            string readverPath = Path.Combine(configData.DirUpdate, "Version.txt");
            string pathUpdateProgram = Path.Combine(configData.DirUpdate, "Update.exe");
            string pathAchiveProgram = Path.Combine(configData.DirUpdate, $"{nameSelf}.zip");
            Version? curver = Assembly.GetExecutingAssembly().GetName().Version;
            Version? readver = new(File.ReadAllText(readverPath));
            if (!File.Exists(pathUpdateProgram))
            {
                FileUtils.WriteGlobalLog($"{DateTime.Now} - Не найден Update.exe");
                return;
            }
            if (!File.Exists(readverPath))
            {
                FileUtils.WriteGlobalLog($"{DateTime.Now} - Не найден файл с версией обновления.");
                return;
            }
            if (!File.Exists(pathAchiveProgram))
            {
                FileUtils.WriteGlobalLog($"{DateTime.Now} - Не найден архив обновления.");
                return;
            }
            if (curver != readver)
            {
                if (MessageBox.Show($"Доступна новая версия v.{readver}. Обновить?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    DirectoryInfo updateDirectory = Directory.CreateDirectory($@"{curSelfDir}\Update\"); //Создаем папку для хранения апдейтера
                    File.Copy(pathUpdateProgram, $@"{curSelfDir}\Update\Update.exe", true); //Скопировали апдейтер
                    using (Process process = new())
                    {
                        process.StartInfo.FileName = @$"{curSelfDir}\Update\Update.exe";
                        process.StartInfo.Arguments = @$"{nameSelf} {processId} {curSelfDir} {configData.DirUpdate}";
                        process.Start();
                    };
                    Environment.Exit(0);
                }
            }
        }
    }
}
