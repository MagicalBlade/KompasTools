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
        [ObservableProperty]
        private string? _orderRequest = "";
        // TODO 0: Удалить путь, удалить само свойство
        [ObservableProperty]
        private string? _pathOrder = "\\\\auxserver\\ОГК\\0. Чертежи компас";
        /// <summary>
        /// Список заказов
        /// </summary>
        [ObservableProperty]
        private IEnumerable? _ordersPath;
        /// <summary>
        /// Выбранная закладка
        /// </summary>
        [ObservableProperty]
        private int? _orderSelectPath;
        /// <summary>
        /// Выбранный заказ из списка
        /// </summary>
        [ObservableProperty]
        private string? _orderSelected;
        [ObservableProperty]
        private IEnumerable? _drawings;
        #endregion

        /// <summary>
        /// Поиск заказа по введенным данным
        /// </summary>
        /// <param name="value"></param>
        partial void OnOrderRequestChanging(string? value)
        {
            OrdersPath = SearchUtils.SearchFolder(value, PathOrder);
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

        partial void OnOrderSelectedChanging(string? value)
        {
            Drawings = SearchUtils.SearchFile("*", value);

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
                }
                catch (Exception)
                {
                    MainSettings = new ConfigData();
                    FileUtils.WriteGlobalLog($"{DateTime.Now} - Проблема с десериализацией. Взяты стандартные настройки.");
                    return;
                }
            }
            else
            {
                FileUtils.WriteGlobalLog($"{DateTime.Now} - Не найден файл настроек");
                return;
            }
            if (MainSettings == null)
            {
                FileUtils.WriteGlobalLog($"{DateTime.Now} - Настройки пусты");
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

            JsonUtils.Serialize("Settings.json", MainSettings);
        }

        #endregion
    }
}
// TODO: Создать класс для хранения путей к папкам из которых буду получать списки файлов
// TODO: Проблема с подпапками, например Архив в нулевой. Как заходить во внутрь? Что отображать при его выделении?
// TODO: Двойной клик по заказу открывает папку с заказом. Кнопка для открытия папки?
// TODO: В папке "Завершенные чертежи" внутри заказа искать по всем папкам