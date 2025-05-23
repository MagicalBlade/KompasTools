﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using KompasAPI7;
using System.IO;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using KompasTools.Utils;
using System.Threading;

namespace KompasTools.ViewModels.WorkingWithFiles
{
    internal partial class EditStampViewModel : ObservableObject
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
        /// Ячейка названия заказа
        /// </summary>
        [ObservableProperty]
        string _cell_1 = "";
        /// <summary>
        /// Изменять ячейку названия заказа
        /// </summary>
        [ObservableProperty]
        bool _isCell_1 = false;
        /// <summary>
        /// Высота шрифта в ячейке названия заказа
        /// </summary>
        [ObservableProperty]
        double _cell_1Height = 7;
        /// <summary>
        /// Указать? высоту шрифта в ячейке названия заказа
        /// </summary>
        [ObservableProperty]
        bool _isCell_1Height = false;

        /// <summary>
        /// Изменять ячейку основной надписи
        /// </summary>
        [ObservableProperty]
        bool _isCell_2 = false;
        /// <summary>
        /// Высота шрифта в ячейке основной надписи
        /// </summary>
        [ObservableProperty]
        double _cell_2Height = 7;
        /// <summary>
        /// Указать? высоту шрифта в ячейке основной надписи
        /// </summary>
        [ObservableProperty]
        bool _isCell_2Height = false;

        /// <summary>
        /// Ячейка заказа
        /// </summary>
        [ObservableProperty]
        string _cell_16003 = "";
        /// <summary>
        /// Изменять ячейку заказа
        /// </summary>
        [ObservableProperty]
        bool _isCell_16003 = false;
        /// <summary>
        /// Ячейка инвентарного номера
        /// </summary>
        [ObservableProperty]
        string _cell_16002 = "";
        /// <summary>
        /// Изменять ячейку инвентарного номера
        /// </summary>
        [ObservableProperty]
        bool _isCell_16002 = false;
        /// <summary>
        /// Ячейка номера листа
        /// </summary>
        [ObservableProperty]
        string _cell_16001 = "";
        /// <summary>
        /// Изменять ячейку номера листа
        /// </summary>
        [ObservableProperty]
        bool _isCell_16001 = false;

        /// <summary>
        /// Изменить штамп
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task EditStampAsync(CancellationToken token)
        {
            int errorcounter = 0;
            ///TODO проверка на пустые ячейки. напомнить пользователю что ячейка будет очищена
            InfoUtils.ClearStatusBar();
            InfoUtils.ClearProgressBar();
            InfoUtils.ClearLoggin();
            if (!IsCell_16001 && !IsCell_16002 && !IsCell_16003 && !IsCell_1 && !IsCell_2)
            {
                InfoUtils.SetStatusBar("Не выбраны ячейки для изменения");
                return;
            }
            if (PathsFileCdw == null)
            {
                InfoUtils.SetStatusBar("Не указаны файлы для изменения");
                return;
            }
            InfoUtils.SetLoggin("Началось заполнение штампа");
            Type? kompasType = Type.GetTypeFromProgID("Kompas.Application.5", true);
            if (kompasType == null)
            {
                InfoUtils.SetStatusBar("Не найден компас на компьютере");
                return;
            }
            if (token.IsCancellationRequested)
            {
                InfoUtils.SetStatusBar("Заполнение штампа отменено");
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
                        InfoUtils.SetStatusBar("Заполнение штампа отменено");
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
                    if (IsCell_1)
                    {
                        ChangeStampMultyLine(stamp, 1, Cell_1.Split("\r\n"), Cell_1Height, IsCell_1Height);
                    }
                    if (IsCell_2)
                    {
                        string namefile = Path.GetFileNameWithoutExtension(kompasDocuments2D.Name);
                        if (IsCell_2Height)
                        {
                            ChangeStamp(stamp, 2, namefile, Cell_2Height);
                        }
                        else
                        {
                            ChangeStamp(stamp, 2, namefile, -1);
                        }

                        #region Вынести в отдельную вкладку
                        //TODO Вынести в отдельную вкладку
                        //if (Cell_2InFileName)
                        //{
                        //    IText text = stamp.Text[2];
                        //    string? pathDirectory = Path.GetDirectoryName(path);
                        //    if (pathDirectory == null)
                        //    {
                        //        InfoUtils.SetLoggin($"Не удалось получить путь к папке где находится файл - {path}");
                        //        errorcounter++;
                        //        continue;
                        //    }
                        //    string newFileFullPath = Path.Combine(pathDirectory, $"{text.Str.Trim()}.cdw");
                        //    newFileFullPath = newFileFullPath.Replace("\n", " ");
                        //    if (newFileFullPath == null)
                        //    {
                        //        InfoUtils.SetLoggin($"Не удалось создать новый путь взамен - {path}");
                        //        errorcounter++;
                        //        continue;
                        //    }
                        //    if (File.Exists(newFileFullPath))
                        //    {
                        //        InfoUtils.SetLoggin($"Файл с новым именем {newFileFullPath} уже существует. Имя файла который изменяли - {path}");
                        //        errorcounter++;
                        //        continue;
                        //    }
                        //    kompasDocuments2D.Close(Kompas6Constants.DocumentCloseOptions.kdDoNotSaveChanges);
                        //    try
                        //    {
                        //        //File.Delete("E:\\Temp\\Изменить штамп\\чертежи\\1.cdw");
                        //        File.Move(kompasDocuments2D.PathName, newFileFullPath);
                        //    }
                        //    catch (Exception e)
                        //    {
                        //        InfoUtils.SetLoggin($"Ошибка при изменении имени файла - {path}");
                        //        InfoUtils.SetLoggin($"{e}");
                        //        errorcounter++;
                        //        continue;
                        //    }
                        //} 
                        #endregion
                    }
                    if (IsCell_16001)
                    {
                        ChangeStamp(stamp, 16001, Cell_16001, -1);
                    }
                    if (IsCell_16002) ChangeStamp(stamp, 16002, Cell_16002, -1);
                    if (IsCell_16003) ChangeStamp(stamp, 16003, Cell_16003, -1);

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
                InfoUtils.SetLoggin("Заполнение штампа завершено");
            }
            if (errorcounter != 0)
            {
                InfoUtils.SetLoggin("Заполнение штампа завершено с ошибками, проверьте журнал.");
                InfoUtils.SetLoggin($"Количество ошибок = {errorcounter}");
                MessageBox.Show($"Заполнение штампа завершено с ошибками, проверьте журнал.\nКоличество ошибок = {errorcounter}");
            }
            #region Функции
            static void ChangeStamp(IStamp _stamp, int cellnumber, string celltext, double cellHeight)
            {
                IText text = _stamp.Text[cellnumber];
                ITextLine textLine = text.TextLine[0];
                ITextItem textItem = textLine.TextItem[0];
                ITextFont textFont = (ITextFont)textItem;
                double height;
                if (cellHeight == -1)
                {
                    height = textFont.Height;
                }
                else
                {
                    height = cellHeight;
                }
                if (height == 5) height += 0.01;
                text.Str = celltext; //Изменяем текст в ячейке заказа
                ITextLine textLine1 = text.TextLine[0];
                ITextItem textItem1 = textLine1.TextItem[0];
                ITextFont textFont1 = (ITextFont)textItem1;
                textFont1.Height = height;
                textItem1.Update();
            }
            static void ChangeStampMultyLine(IStamp _stamp, int cellnumber, string[] celltext, double cellHeight, bool iscellheight)
            {
                IText text = _stamp.Text[cellnumber];
                text.Clear();
                foreach (var item in celltext)
                {
                    ITextLine textLine2 = text.Add();
                    ITextItem textItem1 = textLine2.Add();
                    textItem1.Str = item;
                    ITextFont textFont1 = (ITextFont)textItem1;
                    if (iscellheight)
                    {
                        textFont1.Height = cellHeight;
                    }
                    textItem1.Update();
                }
            }
            #endregion
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
    }
}
