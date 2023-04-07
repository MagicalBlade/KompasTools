using System;
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
        [ObservableProperty]
        private string _posRequest = "";
        [ObservableProperty]
        private string _markRequest = "";
        [ObservableProperty]
        private string? _pathOrder = "";
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
            switch (OrderSelectPath) 
            {
                case 0:
                    OrdersPath = SearchUtils.SearchFolder(OrderRequest, MainSettings.PathDrawingKompas);
                    break;
                case 1:
                    OrdersPath = SearchUtils.SearchFolder(OrderRequest, MainSettings.PathCompletedDrawing);
                    break;
            }
        }

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
                    Task DrawingCompletedTask = Task.Run(() => DrawingCompleted = SearchUtils.SearchFile(MarkRequest, value?));

                    break;
            }
        }

        partial void OnPosRequestChanged(string value)
        {
            Task DrawingKompasPartTask = Task.Run(() => DrawingKompasPart = SearchUtils.SearchFile(value, $"{OrderSelected}\\Деталировка"));
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


        #endregion
    }
}
// TODO: Создать класс для хранения путей к папкам из которых буду получать списки файлов
// TODO: Проблема с подпапками, например Архив в нулевой. Как заходить во внутрь? Что отображать при его выделении?
// TODO: Двойной клик по заказу открывает папку с заказом. Кнопка для открытия папки?
// TODO: В папке "Завершенные чертежи" внутри заказа искать по всем папкам