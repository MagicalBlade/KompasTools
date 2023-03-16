using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection;
using System.IO;

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
        [ObservableProperty]
        static private Version? _versionAssembly = Assembly.GetExecutingAssembly().GetName().Version;
        [ObservableProperty]
        private string _titleMainWindow = $"Набор инструментов для Компас 3D. Версия приложения: " +
            $"{Assembly.GetExecutingAssembly().GetName().Version?.ToString(3)}";
        [ObservableProperty]
        private string? _statisBar;


        public string CurSelfDir { get => _curSelfDir; set => _curSelfDir = value; }
        private string _curSelfDir = Environment.CurrentDirectory;

        #endregion

        #region Команды
        [RelayCommand]
        private void LoadedMainWindow()
        {
            string pathSettings = Path.Combine(CurSelfDir, "Settings.ini");
            Dictionary<string, string> settings = new();
            if (File.Exists(pathSettings))
            {
                string? line;
                using (StreamReader readSettings = new(pathSettings))
                {
                    while ((line = readSettings.ReadLine()) != null)
                    {
                        string[] strings = line.Split("=", 2, StringSplitOptions.TrimEntries);
                        if (strings.Length != 2) { continue; }
                        settings.Add(strings[0], strings[1]);
                    }

                }
            }
            System.Windows.MessageBox.Show($"{settings["path_update"]}");
        }
        [RelayCommand]
        private void ClosingMainWindow()
        {
            Properties.Settings.Default.HeightMainWindow = HeightMainWindow;
            Properties.Settings.Default.WidthMainWindow = WidthMainWindow;
            Properties.Settings.Default.Save();

        }
        #endregion
    }
}
