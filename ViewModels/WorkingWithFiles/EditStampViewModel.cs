using CommunityToolkit.Mvvm.ComponentModel;
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
        /// Путь к папке с чертежами
        /// </summary>
        [ObservableProperty]
        string? _pathFolderAllCdw;
        [ObservableProperty]
        string _stamp_16003 = "";

        [RelayCommand(IncludeCancelCommand = true)]
        private async Task EditStampAsync(CancellationToken token)
        {
            InfoUtils.SetStatusBar("Началось заполнение штампа");
            InfoUtils.ClearProgressBar();
            InfoUtils.ClearLoggin();
            if (PathFolderAllCdw == null)
            {
                InfoUtils.SetStatusBar("Не указан путь к папке с чертежами");
                return;
            }
            string[] cdwFiles = Directory.GetFiles(PathFolderAllCdw, "*.cdw", SearchOption.TopDirectoryOnly);
            Type? kompasType = Type.GetTypeFromProgID("Kompas.Application.5", true);
            if (kompasType == null)
            {
                InfoUtils.SetStatusBar("Заполнение штампа отменено");
                return;
            }
            if (token.IsCancellationRequested)
            {
                InfoUtils.SetStatusBar("Не удалось запустить компас");
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
                foreach (string path in cdwFiles)
                {
                    if (token.IsCancellationRequested)
                    {
                        InfoUtils.SetStatusBar("Заполнение штампа отменено");
                        application.Quit();
                        return;
                    }
                    InfoUtils.SetProgressBar(progressbarriter += 90 / cdwFiles.Length);
                    if (documents.Open(path, false, false) is not IKompasDocument2D kompasDocuments2D)
                    {
                        InfoUtils.SetLoggin($"Не удалось открыть - {path}");
                        continue;
                    }
                    #region Изменение штампа
                    ILayoutSheets layoutSheets = kompasDocuments2D.LayoutSheets;
                    foreach (ILayoutSheet layoutSheet in layoutSheets)
                    {
                        IStamp stamp = layoutSheet.Stamp;
                        IText text = stamp.Text[16003];
                        ITextLine textLine = text.TextLine[0];
                        ITextItem textItem = textLine.TextItem[0];
                        ITextFont textFont = (ITextFont)textItem;
                        double height = textFont.Height;
                        text.Str = Stamp_16003; //Изменяем текст в ячейке заказа
                        ITextLine textLine1 = text.TextLine[0];
                        ITextItem textItem1 = textLine1.TextItem[0];
                        ITextFont textFont1 = (ITextFont)textItem1;
                        textFont1.Height = height;
                        textItem1.Update();
                        stamp.Update();
                        break;
                    }
                    kompasDocuments2D.Save();
                    if (kompasDocuments2D.Changed)
                    {
                        InfoUtils.SetLoggin($"Не удалось сохранить - {path}");
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
            if (!token.IsCancellationRequested)
            {
                InfoUtils.SetStatusBar("Заполнение штампа завершено");
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
            }
        }
    }
}
