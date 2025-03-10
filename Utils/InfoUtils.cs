using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KompasTools.Utils
{
    internal static class InfoUtils
    {
        private static string logging = "";


        #region Работа со статус баром
        public class SendItemStatusBarMessage : ValueChangedMessage<string>
        {
            public SendItemStatusBarMessage(string text) : base(text)
            {
            }
        }
        /// <summary>
        /// Записать текст в статус бар
        /// </summary>
        /// <param name="text"></param>
        public static void SetStatusBar(string text)
        {
            WeakReferenceMessenger.Default.Send(new SendItemStatusBarMessage(text));
        }
        /// <summary>
        /// Очистить статус бар
        /// </summary>
        public static void ClearStatusBar()
        {
            WeakReferenceMessenger.Default.Send(new SendItemStatusBarMessage(""));
        }
        #endregion

        #region Работа с прогресс баром
        public class SendItemProgressBarMessage : ValueChangedMessage<int>
        {
            public SendItemProgressBarMessage(int number) : base(number)
            {
            }
        }
        /// <summary>
        /// Записать текст в статус бар
        /// </summary>
        /// <param name="text"></param>
        public static void SetProgressBar(int number)
        {
            WeakReferenceMessenger.Default.Send(new SendItemProgressBarMessage(number));
        }
        /// <summary>
        /// Очистить статус бар
        /// </summary>
        public static void ClearProgressBar()
        {
            WeakReferenceMessenger.Default.Send(new SendItemProgressBarMessage(0));
        }
        #endregion

        #region Работа с журналом работы приложения
        public class SendItemLoggingMessage : ValueChangedMessage<string>
        {
            public SendItemLoggingMessage(string text) : base(text)
            {
            }
        }
        /// <summary>
        /// Записать текст в журнал работы приложения
        /// </summary>
        /// <param name="text"></param>
        public static void SetLoggin(string text)
        {
            logging += text;
            WeakReferenceMessenger.Default.Send(new SendItemLoggingMessage($"{DateTime.Now:H:mm:ss [d/MM/yyyy]} - {logging}"));
        }
        /// <summary>
        /// Очистить журнал работы приложения
        /// </summary>
        public static void ClearLoggin()
        {
            logging = "";
            WeakReferenceMessenger.Default.Send(new SendItemLoggingMessage(""));
        }
        #endregion
    }
}
