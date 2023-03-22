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
        private string? _statisBar;
        /// <summary>
        /// Настройки приложения
        /// </summary>
        [ObservableProperty]
        private ConfigData? _mainSettings = new();
        #endregion

        #region TabControl - Заказ
        [ObservableProperty]
        private string? _orderRequest;
        [ObservableProperty]
        private string? _pathOrder;
        #endregion

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

        /// <summary>
        /// Поиск и открытие папки с заказом
        /// </summary>
        [RelayCommand]
        private void OpenOrder()
        {
            SearchUtils.SearchFolder(OrderRequest, PathOrder);
        }
        #endregion
    }
}
