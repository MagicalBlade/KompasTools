using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using static KompasTools.Utils.InfoUtils;

namespace KompasTools.ViewModels.WorkingWithFiles
{
    internal partial class WorkingWithFilesViewModel : ObservableObject
    {
        /// <summary>
        /// Прогресс бар
        /// </summary>
        [ObservableProperty]
        private int _progress_Bar = 0;
        /// <summary>
        /// Журнал работы приложения
        /// </summary>
        [ObservableProperty]
        private string _logging = "";

        public WorkingWithFilesViewModel()
        {
            //Подписка на сообщения для прогресс бара
            WeakReferenceMessenger.Default.Register<SendItemProgressBarMessage>(this, (r, m) =>
            {
                Progress_Bar = m.Value;
            });
            //Подписка на сообщения для журнала работы приложения
            WeakReferenceMessenger.Default.Register<SendItemLoggingMessage>(this, (r, m) =>
            {
                Logging = m.Value;
            });
        }
    }
}
