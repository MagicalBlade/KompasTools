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
            string readverPath = Path.Combine(configData.DirUpdate, "Version.txt");
            string pathUpdateProgram = Path.Combine(configData.DirUpdate, "Update.exe");

            if (!File.Exists(readverPath))
            {
                FileUtils.WriteGlobalLog($"{DateTime.Now} - Не найден Version.txt");
                return;
            }
            if (!File.Exists(pathUpdateProgram))
            {
                FileUtils.WriteGlobalLog($"{DateTime.Now} - Не найден Update.exe");
                return;
            }
            string? nameSelf = Assembly.GetExecutingAssembly().GetName().Name;
            string pathAchiveProgram = Path.Combine(configData.DirUpdate, $"{nameSelf}.zip");
            if (!File.Exists(pathAchiveProgram))
            {
                FileUtils.WriteGlobalLog($"{DateTime.Now} - Не найден архив обновления.");
                return;
            }
            int processId = Environment.ProcessId;
            string curSelfDir = Environment.CurrentDirectory;
            Version? curver = Assembly.GetExecutingAssembly().GetName().Version;
            Version? readver = new(File.ReadAllText(readverPath));
            if (curver != readver)
            {
                if (MessageBox.Show($"Доступна новая версия v.{readver}. Обновить?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    Directory.CreateDirectory($@"{curSelfDir}\Update\"); //Создаем папку для хранения апдейтера
                    File.Copy(pathUpdateProgram, $@"{curSelfDir}\Update\Update.exe", true); //Скопировали апдейтер
                    using (Process process = new())
                    {
                        process.StartInfo.FileName = @$"{curSelfDir}\Update\Update.exe";
                        process.StartInfo.Arguments = $"\"{nameSelf}\" {processId} \"{curSelfDir}\" \"{configData.DirUpdate}\"";
                        process.Start();
                    };
                    Environment.Exit(0);
                }
            }
        }
    }
}
