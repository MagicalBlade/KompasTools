﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection;
using KompasTools.Classes;
using KompasTools.Utils;
using System.Text.Json;
using System.Windows.Shapes;
using System.Windows;
using System.IO;
using Path = System.IO.Path;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.Windows.Forms;
using KompasAPI7;
using Kompas6API5;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;

namespace KompasTools.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        #region Параметры окна
        /// <summary>
        /// Высота основного окна
        /// </summary>
        [ObservableProperty]
        private double _heightMainWindow = Properties.Settings.Default.HeightMainWindow;
        /// <summary>
        /// Ширина основного окна
        /// </summary>
        [ObservableProperty]
        private double _widthMainWindow = Properties.Settings.Default.WidthMainWindow;
        #endregion

        #region Свойства общие для окна
        /// <summary>
        /// Титул основного окна
        /// </summary>
        [ObservableProperty]
        private string _titleMainWindow = $"Набор инструментов для Компас 3D. Версия приложения: " +
            $"{Assembly.GetExecutingAssembly().GetName().Version?.ToString(3)}";
        /// <summary>
        /// Статус бар основного окна
        /// </summary>
        [ObservableProperty]
        private string? _statusBar;
        /// <summary>
        /// Настройки приложения
        /// </summary>
        [ObservableProperty]
        private ConfigData _mainSettings = new();
        #endregion

        #region TabControl - Заказ
        /// <summary>
        /// Заказ который необходимо найти
        /// </summary>
        [ObservableProperty]
        private string? _orderRequest = "";
        /// <summary>
        /// Позиция которую необходимо найти
        /// </summary>
        [ObservableProperty]
        private string _posRequest = "";
        /// <summary>
        /// Марка которую необходимо найти
        /// </summary>
        [ObservableProperty]
        private string _markRequest = "";
        /// <summary>
        /// Список заказов
        /// </summary>
        [ObservableProperty]
        private List<FileInfo>? _ordersPath;
        /// <summary>
        /// Выбранная закладка
        /// </summary>
        [ObservableProperty]
        private int? _orderSelectPath;
        /// <summary>
        /// Выбранный заказ из списка
        /// </summary>
        [ObservableProperty]
        private FileInfo? _orderSelected;
        [ObservableProperty]
        private IEnumerable<FileInfo>? _drawingCompleted;
        [ObservableProperty]
        private IEnumerable<FileInfo>? _drawingKompasAssembly;
        [ObservableProperty]
        private IEnumerable<FileInfo>? _drawingKompasPart;
        [ObservableProperty]
        private IEnumerable<FileInfo>? _model3DAssembly;
        [ObservableProperty]
        private IEnumerable<FileInfo>? _model3DPart;
        #endregion

        #region TabControl - Получить позиции
        /// <summary>
        /// Путь к папке с чертежами
        /// </summary>
        [ObservableProperty]
        string? _pathFolderAllCdw;
        /// <summary>
        /// Информация по позициям
        /// </summary>
        [ObservableProperty]
        List<string[]> _posinfo = new List<string[]>();

        #endregion

        /// <summary>
        /// Поиск заказа по введенным данным
        /// </summary>
        /// <param name="value"></param>
        partial void OnOrderRequestChanging(string? value)
        {
            // TODO: Может стоит искать не в "папке" в уже в сформированом списке папок? Только проблема: список может быть устаревшим
            switch (OrderSelectPath)
            {
                case 0:
                    OrdersPath = SearchUtils.SearchFolder(value, MainSettings.PathDrawingKompas);
                    break;
                case 1:
                    OrdersPath = SearchUtils.SearchFolder(value, MainSettings.PathCompletedDrawing);
                    break;
            }
        }

        /// <summary>
        /// При переключении вкладок изменяется путь поиска и перестраивается список заказов.
        /// </summary>
        /// <param name="value"></param>
        partial void OnOrderSelectPathChanged(int? value)
        {
            //Обнуление списка найденных файлов.
            DrawingKompasAssembly = null;
            DrawingKompasPart = null;
            DrawingCompleted = null;
            Model3DAssembly = null;
            Model3DPart = null;
            FileInfo? oldOrderSelected = OrderSelected;
            switch (OrderSelectPath) 
            {
                case 0:
                    OrdersPath = SearchUtils.SearchFolder(OrderRequest, MainSettings.PathDrawingKompas);
                    foreach (FileInfo item in OrdersPath)
                    {
                        if (item.Name == oldOrderSelected?.Name)
                        {
                            OrderSelected = item;
                            break;
                        }
                    }
                    break;
                case 1:
                    OrdersPath = SearchUtils.SearchFolder(OrderRequest, MainSettings.PathCompletedDrawing);
                    foreach (FileInfo item in OrdersPath)
                    {
                        if (item.Name == oldOrderSelected?.Name)
                        {
                            OrderSelected = item;
                            break;
                        }
                    }
                    break;
                case 2:
                    OrdersPath = SearchUtils.SearchFolder(OrderRequest, MainSettings.PathModel3D);
                    foreach (FileInfo item in OrdersPath)
                    {
                        if (item.Name == oldOrderSelected?.Name)
                        {
                            OrderSelected = item;
                            break;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Выбран новый заказ
        /// </summary>
        /// <param name="value"></param>
        partial void OnOrderSelectedChanging(FileInfo? value)
        {
            // TODO: В зависимости от вкладки задавать разные пути. Например в чертежах компаса будет два пути: сборка и деталировка

            switch (OrderSelectPath)
            {
                case 0:
                    // TODO: Подумать как не хардкодить "Сборка" и "Деталировка"
                    DrawingKompasAssembly = null;
                    DrawingKompasPart = null;
                    Task DrawingKompasAssemblyTask = Task.Run(() => DrawingKompasAssembly = SearchUtils.SearchFile(MarkRequest, $"{value}\\Сборка"));
                    Task DrawingKompasPartTask = Task.Run(() => DrawingKompasPart = SearchUtils.SearchFile(PosRequest, $"{value}\\Деталировка"));
                    break;
                case 1:
                    DrawingCompleted = null;
                    Task DrawingCompletedTask = Task.Run(() => DrawingCompleted = SearchUtils.SearchFile(MarkRequest, value?.FullName));
                    break;
                case 2:
                    DrawingCompleted = null;
                    Task Model3DAssemblyTask = Task.Run(() => Model3DAssembly = SearchUtils.SearchFile(MarkRequest, value?.FullName));
                    Task Model3DPartTask = Task.Run(() => Model3DPart = SearchUtils.SearchFile(PosRequest, $"{value}\\2_Деталировка"));
                    break;
            }
        }

        /// <summary>
        /// Происходит ввод запроса на поиск позиции
        /// </summary>
        /// <param name="value"></param>
        partial void OnPosRequestChanged(string value)
        {
            DrawingKompasPart = null;
            Model3DPart = null;
            switch (OrderSelectPath)
            {
                case 0:
                    Task DrawingKompasPartTask = Task.Run(() => DrawingKompasPart = SearchUtils.SearchFile(value, $"{OrderSelected}\\Деталировка"));
                    break;
                case 1:
                    break;
                case 2:
                    Task Model3DPartTask = Task.Run(() => Model3DPart = SearchUtils.SearchFile(value, $"{OrderSelected}\\2_Деталировка"));
                    break;
            }
        }
       
        /// <summary>
        /// Происходит ввод запроса на поиск марки
        /// </summary>
        /// <param name="value"></param>
        partial void OnMarkRequestChanged(string value)
        {
            DrawingKompasAssembly = null;
            DrawingCompleted = null;
            Model3DAssembly = null;
            switch (OrderSelectPath)
            {
                case 0:
                    Task DrawingKompasAssemblyTask = Task.Run(() => DrawingKompasAssembly= SearchUtils.SearchFile(value, $"{OrderSelected}\\Сборка"));
                    break;
                case 1:
                    Task DrawingCompletedAssemblyTask = Task.Run(() => DrawingCompleted = SearchUtils.SearchFile(value, $"{OrderSelected}"));
                    break;
                case 2:
                    Task Model3DAssemblyTask = Task.Run(() => Model3DAssembly = SearchUtils.SearchFile(value, $"{OrderSelected}"));
                    break;
            }
        }

        #region Команды
        /// <summary>
        /// Событие при загрузке окна
        /// </summary>
        [RelayCommand]
        private void LoadedMainWindow()
        {
            string path_settings = Path.Combine(Environment.CurrentDirectory, "Settings.json");
            if (File.Exists(path_settings))
            {
                try
                {
                    MainSettings = JsonUtils.Deserialize<ConfigData>("Settings.json");
                    OrdersPath = SearchUtils.SearchFolder(OrderRequest, MainSettings.PathDrawingKompas);
                }
                catch (Exception)
                {
                    MainSettings = new ConfigData();
                    FileUtils.WriteGlobalLog($"{DateTime.Now} - Проблема с десериализацией. Взяты стандартные настройки.");
                    // TODO: Придумать как записывать настройки если не удалось их прочитать, боеле красиво
                    JsonUtils.Serialize("Settings.json", MainSettings);
                    return;
                }
            }
            else
            {
                FileUtils.WriteGlobalLog($"{DateTime.Now} - Не найден файл настроек");
                JsonUtils.Serialize("Settings.json", MainSettings);
                return;
            }
            if (MainSettings == null)
            {
                FileUtils.WriteGlobalLog($"{DateTime.Now} - Настройки пусты");
                JsonUtils.Serialize("Settings.json", MainSettings);
                return;
            }

            UpdateUtils.CheckUpdate(MainSettings);
        }
        /// <summary>
        /// Событие при закрытии окна
        /// </summary>
        [RelayCommand]
        private void ClosingMainWindow()
        {
            Properties.Settings.Default.HeightMainWindow = HeightMainWindow;
            Properties.Settings.Default.WidthMainWindow = WidthMainWindow;
            Properties.Settings.Default.Save();
            // TODO: Раскомендить когда добавлю позможность именять настройки из программы
            //JsonUtils.Serialize("Settings.json", MainSettings);
        }

        [RelayCommand]
        private void RunProcess(string path)
        {
            Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
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

        [RelayCommand]
        private void GetPos()
        {
            // TODO: Создать файл логов и записать туда
            if (PathFolderAllCdw == null) return;
            // TODO: Добавить выбор поиск по всей дериктории или толкьо в верхнем уровне
            string[] cdwFiles = Directory.GetFiles(PathFolderAllCdw, "*.cdw", SearchOption.AllDirectories);
            Type? kompasType = Type.GetTypeFromProgID("Kompas.Application.5", true);
            KompasObject? kompas = Activator.CreateInstance(kompasType) as KompasObject; //Запуск компаса
            if (kompas == null)
            {
                return;
            }
            IApplication application = (IApplication)kompas.ksGetApplication7();
            IDocuments documents = application.Documents;

            foreach (string path in cdwFiles)
            {
                IKompasDocument2D kompasDocuments2D = (IKompasDocument2D)documents.Open(path, false, false);
                string mark = "";
                #region Получение имени марки из штампа
                ILayoutSheets layoutSheets = kompasDocuments2D.LayoutSheets;
                foreach (ILayoutSheet layoutSheet in layoutSheets)
                {
                    IStamp stamp = layoutSheet.Stamp;
                    IText text2 = stamp.Text[2]; //Текст из ячейки "Обозначения документа"
                    string[] text2Split = text2.Str.Split(" ");
                    mark = text2Split[^1];
                    break;
                }
                #endregion

                IViewsAndLayersManager viewsAndLayersManager = kompasDocuments2D.ViewsAndLayersManager;
                IViews views = viewsAndLayersManager.Views;
                foreach (IView view in views)
                {
                    ISymbols2DContainer symbols2DContainer = (ISymbols2DContainer)view;
                    IDrawingTables drawingTables = symbols2DContainer.DrawingTables;
                    foreach (ITable table in drawingTables)
                    {
                        if (((IText)table.Cell[0,0].Text).Str.IndexOf("Спецификация") != -1)
                        {
                            for (int i = 3; i < table.RowsCount; i++)
                            {
                                if (table.Cell[i, 0] != null && ((IText)table.Cell[i, 0].Text).Str.Trim() != "" && ((IText)table.Cell[i, 0].Text).Str.IndexOf("швы") == -1)
                                {
                                    Posinfo.Add(new string[] { ((IText)table.Cell[i, 0].Text).Str, mark });

                                }
                            }
                        }
                    }
                }
            }
            application.Quit();
        }


        /// <summary>
        /// Сохранить файл отчёта
        /// </summary>
        [RelayCommand]
        private void SaveExcel()
        {


            XLWorkbook workbook = new();
            #region Лист "Позиции"
            IXLWorksheet worksheetPos = workbook.Worksheets.Add("Позиции");
            int incrementRow = 3; //Начальная строка
            #region Формирование шапки листа
            worksheetPos.Cell(1, 1).SetValue("Поз.");
            worksheetPos.Cell(1, 2).SetValue("Марок");
            #endregion

            if (worksheetPos != null)
            {
                for (int i = 0; i < Posinfo.Count; i++)
                {
                    worksheetPos.Cell(i + incrementRow, 1).SetValue(Posinfo[i][0]); //Позиция
                    worksheetPos.Cell(i + incrementRow, 2).SetValue(Posinfo[i][1]); //марка

                }
                //Ширина колонки по содержимому
                #region Объединение ячеек
                worksheetPos.Range("B1:C1").Row(1).Merge();
                worksheetPos.Range("D1:F1").Row(1).Merge();
                worksheetPos.Range("G1:H1").Row(1).Merge();
                worksheetPos.Range("A1:A2").Column(1).Merge();
                worksheetPos.Range("I1:I2").Column(1).Merge();
                worksheetPos.Range("J1:J2").Column(1).Merge();
                worksheetPos.Range("K1:K2").Column(1).Merge();
                #endregion
                worksheetPos.Columns(1, Posinfo.Count).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheetPos.Columns(1, Posinfo.Count).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                try
                {
                    workbook.SaveAs($"D:\\Отчёт.xlsx");
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("Ну удалось сохранить эксель файл");;
                    return;
                }

            }
            #endregion


            #endregion
        }
    }
}
// TODO: Создать класс для хранения путей к папкам из которых буду получать списки файлов
// TODO: Проблема с подпапками, например Архив в нулевой. Как заходить во внутрь? Что отображать при его выделении?
// TODO: Двойной клик по заказу открывает папку с заказом. Кнопка для открытия папки?
// TODO: В папке "Завершенные чертежи" внутри заказа искать по всем папкам