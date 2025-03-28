using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.Spreadsheet;
using Kompas6API5;
using KompasAPI7;
using KompasTools.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KompasTools.ViewModels.WorkingWithFiles
{
    internal partial class TitleBlockToFileName : ObservableObject
    {
        /// <summary>
        /// Получить чертежи из папки
        /// </summary>
        [ObservableProperty]
        bool _isFolderAllCdw = true;
        /// <summary>
        /// Путь к папке с чертежами
        /// </summary>
        [ObservableProperty]
        string? _pathFolderAllCdw;

        /// <summary>
        /// Получить чертежи выбором файлов
        /// </summary>
        [ObservableProperty]
        bool _isPathsFileCdw = false;
        /// <summary>
        /// Список путей файлов чертежей
        /// </summary>
        [ObservableProperty]
        string[]? _pathsFileCdw;

        #region Команды
        /// <summary>
        /// Записать основную надпись в имя файла
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task WriteTitleBlockToFileName(CancellationToken token)
        {
            int errorcounter = 0;
            InfoUtils.ClearStatusBar();
            InfoUtils.ClearProgressBar();
            InfoUtils.ClearLoggin();
            if (PathsFileCdw == null)
            {
                InfoUtils.SetStatusBar("Не указаны файлы для изменения");
                return;
            }
            InfoUtils.SetLoggin("Началась запись основной надписи в имя файла");
            Type? kompasType = Type.GetTypeFromProgID("Kompas.Application.5", true);
            if (kompasType == null)
            {
                InfoUtils.SetStatusBar("Не найден компас на компьютере");
                return;
            }
            if (token.IsCancellationRequested)
            {
                InfoUtils.SetStatusBar("Запись основной надписи в имя файла отменена");
                return;
            }
            //Запуск компаса
            await Task.Run(() =>
            {
                InfoUtils.SetProgressBar(2);
                if (Activator.CreateInstance(kompasType) is not KompasObject kompas)
                {
                    InfoUtils.SetStatusBar("Не удалось запустить компас");
                    return;
                }
                InfoUtils.SetProgressBar(5);
                IApplication application = (IApplication)kompas.ksGetApplication7();
                IDocuments documents = application.Documents;
                int progressbarriter = 5;
                foreach (string path in PathsFileCdw)
                {
                    if (token.IsCancellationRequested)
                    {
                        InfoUtils.SetStatusBar("Запись основной надписи в имя файла отменена");
                        application.Quit();
                        return;
                    }
                    InfoUtils.SetProgressBar(progressbarriter += 90 / PathsFileCdw.Length);
                    if (!File.Exists(path))
                    {
                        InfoUtils.SetLoggin($"Не найден файл - {path}");
                        errorcounter++;
                        continue;
                    }
                    if (documents.Open(path, false, false) is not IKompasDocument2D kompasDocuments2D)
                    {
                        InfoUtils.SetLoggin($"Не удалось открыть - {path}");
                        errorcounter++;
                        continue;
                    }
                    #region Изменение штампа
                    ILayoutSheets layoutSheets = kompasDocuments2D.LayoutSheets;
                    IStamp? stamp = null;
                    foreach (ILayoutSheet layoutSheet in layoutSheets)
                    {
                        stamp = layoutSheet.Stamp;
                        break;
                    }
                    if (stamp == null)
                    {
                        InfoUtils.SetLoggin($"Не удалось получить штамп - {path}");
                        errorcounter++;
                        continue;
                    }
                    IText text = stamp.Text[2];
                    string? pathDirectory = Path.GetDirectoryName(path);
                    if (pathDirectory == null)
                    {
                        InfoUtils.SetLoggin($"Не удалось получить путь к папке где находится файл - {path}");
                        errorcounter++;
                        continue;
                    }
                    string newFileFullPath = Path.Combine(pathDirectory, $"{text.Str.Trim()}.cdw");
                    newFileFullPath = newFileFullPath.Replace("\n", " ");
                    if (newFileFullPath == null)
                    {
                        InfoUtils.SetLoggin($"Не удалось создать новый путь взамен - {path}");
                        errorcounter++;
                        continue;
                    }
                    if (File.Exists(newFileFullPath))
                    {
                        InfoUtils.SetLoggin($"Файл с новым именем {newFileFullPath} уже существует. Имя файла который изменяли - {path}");
                        errorcounter++;
                        continue;
                    }
                    kompasDocuments2D.Close(Kompas6Constants.DocumentCloseOptions.kdDoNotSaveChanges);
                    try
                    {
                        File.Move(path, newFileFullPath);
                    }
                    catch (Exception e)
                    {
                        InfoUtils.SetLoggin($"Ошибка при изменении имени файла - {path}");
                        InfoUtils.SetLoggin($"{e}");
                        errorcounter++;
                        continue;
                    }
                    #endregion
                }
                application.Quit();
                InfoUtils.SetProgressBar(100);
            }, token);
            if (!token.IsCancellationRequested && errorcounter == 0)
            {
                InfoUtils.SetLoggin("Запись основной надписи в имя файла завершена");
            }
            if (errorcounter != 0)
            {
                InfoUtils.SetLoggin("Запись основной надписи в имя файла завершена с ошибками, проверьте журнал.");
                InfoUtils.SetLoggin($"Количество ошибок = {errorcounter}");
                MessageBox.Show($"Запись основной надписи в имя файла завершена с ошибками, проверьте журнал.\nКоличество ошибок = {errorcounter}");
            }
        }
        /// <summary>
        /// Указать папку
        /// </summary>
        [RelayCommand]
        private void OpenFolder()
        {
            FolderBrowserDialog dialog = new();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PathFolderAllCdw = dialog.SelectedPath;
                if (PathFolderAllCdw == null || PathFolderAllCdw == "")
                {
                    InfoUtils.SetStatusBar("Не указан путь к папке с чертежами");
                    return;
                }
                PathsFileCdw = Directory.GetFiles(PathFolderAllCdw, "*.cdw", SearchOption.TopDirectoryOnly);
                Array.Sort(PathsFileCdw);
            }
        }

        /// <summary>
        /// Выбрать файлы
        /// </summary>
        [RelayCommand]
        private void GetPathFiles()
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Выберите файлы для изменения",
                Multiselect = true,
                DefaultExt = ".cdw",
                Filter = "КОМПАС-Чертежи(*.cdw)|*.cdw"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PathsFileCdw = openFileDialog.FileNames;
            }
        } 
        #endregion
    }
}
