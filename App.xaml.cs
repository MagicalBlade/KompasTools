using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KompasTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Запрет на запуск нескольких экземпляров приложения
        private static Mutex? mutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            const string appName = "KompasTools";

            mutex = new Mutex(true, appName, out bool createdNew);

            if (!createdNew)
            {
                // Приложение уже запущено
                MessageBox.Show("Приложение уже запущено!");
                Current.Shutdown();
                return;
            }

            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            mutex?.ReleaseMutex();
            mutex?.Dispose();
            base.OnExit(e);
        } 
        #endregion
    }
}
