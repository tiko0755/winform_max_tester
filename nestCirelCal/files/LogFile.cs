using System;
using System.IO;
using System.Reflection;
using nestCirelCal.Common;

namespace nestCirelCal.Log
{
    public class Logger
    {
        #region Instance

        private static object logLock;

        private static Logger _instance;

        private static string logFileName;
        private Logger() { }

        public static StrHandler WriteToUI_Handler = null;     //写到UI界面, 比如调试log输出界面

        public enum LogType
        {
            Error,
            All,
            Information,
            Debug,
            Success,
            Failure,
            Warning,
            Working,
            DUT,
            DMM,
            CTRL,
        }

        /// <summary>
        /// Logger instance
        /// </summary>
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                    logLock = new object();
                    logFileName = Guid.NewGuid() + ".csv";
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// Write log to log file
        /// </summary>
        /// <param name="logContent">Log content</param>
        /// <param name="logType">Log type</param>
        public void WriteLog(string logContent, LogType logType = LogType.Information, string fileName = null)
        {
            try
            {
                string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                //basePath = @"C:\Users\anyu\APILogs";
                if (!Directory.Exists(basePath + "\\Log"))
                {
                    Directory.CreateDirectory(basePath + "\\Log");
                }

                string dataString = DateTime.Now.ToString("yyyy-MM-dd");
                if (!Directory.Exists(basePath + "\\Log\\" + dataString))
                {
                    Directory.CreateDirectory(basePath + "\\Log\\" + dataString);
                }

                string txt = DateTime.Now.ToString("hh:mm:ss") + "," + logType.ToString() + "," + logContent;

                // write to UI
                WriteToUI_Handler(txt);

                // write to console
                Console.WriteLine(txt);

                // write to file
                string[] logText = new string[] { txt };
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = logFileName;
                }
                lock (logLock)
                {
                    File.AppendAllLines(basePath + "\\Log\\" + dataString + "\\" + fileName, logText);
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Write exception to log file
        /// </summary>
        /// <param name="exception">Exception</param>
        public void WriteException(Exception exception, string specialText = null)
        {
            if (exception != null)
            {
                Type exceptionType = exception.GetType();
                string text = string.Empty;
                if (!string.IsNullOrEmpty(specialText))
                {
                    text = text + specialText + Environment.NewLine;
                }
                text = "Exception: " + exceptionType.Name + Environment.NewLine;
                text += "               " + "Message: " + exception.Message + Environment.NewLine;
                text += "               " + "Source: " + exception.Source + Environment.NewLine;
                text += "               " + "StackTrace: " + exception.StackTrace + Environment.NewLine;
                WriteLog(text, LogType.Error);
            }
        }
    }
}