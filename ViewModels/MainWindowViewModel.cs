using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

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

        #region Команды
        [RelayCommand]
        private void ClosingMainWindow()
        {
            Properties.Settings.Default.HeightMainWindow = HeightMainWindow;
            Properties.Settings.Default.WidthMainWindow = WidthMainWindow;
        }
        #endregion
    }
}
