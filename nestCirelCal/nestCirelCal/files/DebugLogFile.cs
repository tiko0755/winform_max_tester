using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace LaserMarking.Files
{
    class DebugLogFile : FileBase
    {
        private static DebugLogFile debugLogFile = new DebugLogFile();
        /// <summary>
        /// 创建一个静态实例
        /// </summary>
        public static DebugLogFile Instance
        {
            get { return debugLogFile; }
        }

        public DebugLogFile()
        {
            StrFileName = ConfigFile.Instance.DicLogFile[ConfigFile.strDebugLogFilePathClass][ConfigFile.strLogFileNameKey]
                +DateTime.Today.ToShortDateString()+".csv";
            StrDirPath = System.Windows.Forms.Application.StartupPath + "\\" + ConfigFile.Instance.DicLogFile
                [ConfigFile.strDebugLogFilePathClass][ConfigFile.strLogDirPathKey];
            StrFilePath = StrDirPath + "\\" + StrFileName;
        }

        /// <summary>
        /// 创建log文件
        /// </summary>
        /// <param name="strLogHeader"></param>
        public void CreateLogFile()
        {
            //如果文件夹不存在则创建
            if (!IsDirExist())
                CreateDir();

            //流能创建文件如果文件不存在的话
            string strItem = @"RegionIndex\PointIndex";

            for (int i = 0; i < ConfigFile.Instance.IntOffSetPointRow; i++)
            {
                for (int j = 0; j < ConfigFile.Instance.IntOffSetPointColumn; j++)
                {
                    strItem += "," + "Point" + (i + 1).ToString() + (j + 1).ToString();
                }
            }

            strItem += "\r\n";
            WriteFile(strItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstLogValue"></param>
        public void WriteLogFileValue(Dictionary<string,Dictionary<string, WorkStatusLogFlag>> dicLogValue)
        {
            //如果文件夹不存在则创建
            if (!IsDirExist())
            {
                CreateDir();
            }

            string strDebugLogFileValue = "";

            for (int i = 1; i <= ConfigFile.Instance.IntRegionCenterPointRow; i++)
            {
                for (int j = 1; j <= ConfigFile.Instance.IntRegionCenterPointColumn;j++ )
                {
                    Dictionary<string, WorkStatusLogFlag> dicWorkStatusFlag = dicLogValue[i.ToString()+j.ToString()];
                    strDebugLogFileValue += dicWorkStatusFlag["11"].RegionIndex+",";

                    for (int row = 1; row <= ConfigFile.Instance.IntOffSetPointRow; row++)
                    {
                        for (int column = 1; column <= ConfigFile.Instance.IntOffSetPointColumn; column++)
                        {
                            WorkStatusLogFlag workStatus = dicWorkStatusFlag[row.ToString() + column.ToString()];

                            if (!workStatus.IsCameraGetOffsetError && !workStatus.IsLaserOpenLightError
                                && !workStatus.IsLaserMoveToWorkPlaceError && !workStatus.IsLaserPositionCompensationError)
                            {
                                strDebugLogFileValue += workStatus.strCameraOffsetPoint+",";
                            }
                            else if (workStatus.IsCameraGetOffsetError)
                            {
                                strDebugLogFileValue += workStatus.strCameraOffsetPoint + ",";
                            }
                            else if (workStatus.IsLaserMoveToWorkPlaceError)
                            {
                                strDebugLogFileValue += "LaserMoveToWorkPlaceError"+",";
                            }
                            else if (workStatus.IsLaserOpenLightError)
                            {
                                strDebugLogFileValue += "LaserOpenLightError"+",";
                            }
                            else if (workStatus.IsLaserPositionCompensationError)
                            {
                                strDebugLogFileValue += "LaserPositionCompensationError"+",";
                            }
                         }
                     }

                     strDebugLogFileValue += "\r\n";
                }
            }

            WriteFile(strDebugLogFileValue, true);
        }
    }
}
