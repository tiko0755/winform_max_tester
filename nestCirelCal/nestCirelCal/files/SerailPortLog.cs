using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaserMarking.Files
{
    class SerailPortLog : FileBase
    {
        private static SerailPortLog serailPortLogFile = new SerailPortLog();
        /// <summary>
        /// 创建一个静态实例
        /// </summary>
        public static SerailPortLog Instance
        {
            get { return serailPortLogFile; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SerailPortLog()
        {
            StrFileName = "SerailPortLog.txt";
            StrDirPath = System.Windows.Forms.Application.StartupPath + "\\" + ConfigFile.Instance.DicLogFile
                [ConfigFile.strDebugLogFilePathClass][ConfigFile.strLogDirPathKey];
            StrFilePath = StrDirPath + "\\" + StrFileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        public void WriteLogValue(string strValue)
        {
            //如果文件夹不存在则创建
            if (!IsDirExist())
            {
                CreateDir();
            }

            string strDebugLogFileValue = DateTime.Now.ToString() + "," + strValue;
            WriteFile(strDebugLogFileValue, true);
        }
    }
}
