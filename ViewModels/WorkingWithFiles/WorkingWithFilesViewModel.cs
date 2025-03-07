using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KompasTools.ViewModels.WorkingWithFiles
{
    internal partial class WorkingWithFilesViewModel : ObservableObject
    {
        /// <summary>
        /// Прогресс бар извлечения данных позиций
        /// </summary>
        [ObservableProperty]
        private int _progresBarPos = 0;
    }
}
