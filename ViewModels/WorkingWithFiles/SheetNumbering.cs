using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
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
    internal partial class SheetNumbering : ObservableObject
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

        /// <summary>
        /// Начальный номер нумерации листов
        /// </summary>
        [ObservableProperty]
        string _startNumber = "1";


        #region Команды
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task SheetNumberingAsync(CancellationToken token)
        {
            int errorcounter = 0;
            ///TODO проверка на пустые ячейки. напомнить пользователю что ячейка будет очищена
            InfoUtils.ClearStatusBar();
            InfoUtils.ClearProgressBar();
            InfoUtils.ClearLoggin();
            if (!Double.TryParse(StartNumber, out double numberlist))
            {
                InfoUtils.SetStatusBar("В начальном номере листа указано не число");
                return;
            }
            if (PathsFileCdw == null)
            {
                InfoUtils.SetStatusBar("Не указаны файлы для изменения");
                return;
            }
            InfoUtils.SetLoggin("Началась нумерация листов");
            Type? kompasType = Type.GetTypeFromProgID("Kompas.Application.5", true);
            if (kompasType == null)
            {
                InfoUtils.SetStatusBar("Не найден компас на компьютере");
                return;
            }
            if (token.IsCancellationRequested)
            {
                InfoUtils.SetStatusBar("Нумерация листов отменена");
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
                        InfoUtils.SetStatusBar("Нумерация листов отменена");
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
                    #region Нумерация листов
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

                    IText text = stamp.Text[16001];
                    ITextLine textLine = text.TextLine[0];
                    ITextItem textItem = textLine.TextItem[0];
                    ITextFont textFont = (ITextFont)textItem;
                    double height = textFont.Height;
                    if (height == 5) height += 0.01;
                    text.Str = numberlist.ToString(); //Изменяем текст в ячейке заказа
                    ITextLine textLine1 = text.TextLine[0];
                    ITextItem textItem1 = textLine1.TextItem[0];
                    ITextFont textFont1 = (ITextFont)textItem1;
                    textFont1.Height = height;
                    textItem1.Update();
                    numberlist++;
                    stamp.Update();
                    kompasDocuments2D.Save();
                    if (kompasDocuments2D.Changed)
                    {
                        InfoUtils.SetLoggin($"Не удалось сохранить - {path}");
                        errorcounter++;
                    }
                    else
                    {
                        InfoUtils.SetLoggin($"Успешно изменён и сохранён - {path}");
                    }
                    kompasDocuments2D.Close(Kompas6Constants.DocumentCloseOptions.kdDoNotSaveChanges);
                    #endregion
                }
                application.Quit();
                InfoUtils.SetProgressBar(100);
            }, token);
            if (!token.IsCancellationRequested && errorcounter == 0)
            {
                InfoUtils.SetLoggin("Нумерация листов завершена");
            }
            if (errorcounter != 0)
            {
                InfoUtils.SetLoggin("Нумерация листов завершена с ошибками, проверьте журнал.");
                InfoUtils.SetLoggin($"Количество ошибок = {errorcounter}");
                MessageBox.Show($"Нумерация листов завершена с ошибками, проверьте журнал.\nКоличество ошибок = {errorcounter}");
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
