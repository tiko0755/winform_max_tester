using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using goldenman.RegexText;

namespace goldenman.Files
{
    class ConfigFile:IniFileBase
    {
        /// <summary>
        /// 定义一个静态实例
        /// </summary>
        private static ConfigFile configFile = new ConfigFile();
        public static ConfigFile Instance
        {
            get { return configFile; }
        }

        #region

        //偏移点类
        public const string strOFFSetPointClass = "OFFSetPoint";
        //补偿点类
        public const string strCompensatePointClass = "CompensatePoint";
        //区域点类
        public const string strRegionCenterPointClass = "RegionCenterPoint";
        //坐标点键
        public const string strPointKey = "Point";
        //坐标点列键
        public const string strPointColunmKey = "Colunm";
        //坐标点行键
        public const string strPointRowKey = "Row";
        //镭射端口类
        public const string strLaserPortClass = "LaserPort";
        //相机端口类
        public const string strCameraPortClass = "CameraPort";
        //串口名键
        public const string strPortNameKey = "PortName";
        //串口校验位键
        public const string strParityKey = "Parity";
        //串口数据位键
        public const string strDataBitsKey = "DataBits";
        //串口停止位键
        public const string strStopBitsKey = "StopBits";
        //串口波特率键
        public const string strBaudRateKey = "BaudRate";
        //laser打开光源命令
        public const string strLaserOpenShutterCmdKey = "LaserOpenShutterCmd";
        //laser移动到特定位命令
        public const string strLaserMoveToShootingCmdKey = "LaserMoveToShootingCmd";
        //laser位置补偿命令
        public const string strLaserPosCompensationCmdKey = "LaserPosCompensationCmd";
        //laser开始刻印命令
        public const string strLaserStartMarkingCmdKey = "LaserStartMarkingCmd";
        //laser错误移除命令
        public const string strLaserErrorRemoveCmdKey = "LaserErrorRemoveCmd";
        //laser错误请求命令
        public const string strLaserErrorRequestCmdKey = "LaserErrorRequestCmd";
        //laser停止打印特定位命令
        public const string strLaserStopMarkCmdKey = "LaserStopMarkCmd";
        //相机服务器IP地址键
        public const string strCameraPortIPKey = "IP";
        //相机端口号键
        public const string strCameraPortPortKey = "Port";
        //相机触发命令
        public const string strCameraTriggerCmdKey = "CameraTriggerCmd";
        //log文件类
        public const string strLogFilePathClass = "LogFilePath";
        //debuglog文件类，调试界面文件类
        public const string strDebugLogFilePathClass = "DebugLogFilePath";
        //log文件夹路径键
        public const string strLogDirPathKey = "LogDirPath";
        //log文件路径键
        public const string strLogFileNameKey = "LogFileName";
        //程序异常推出类
        public const string strAbnormalRecordClass = "AbnormalRecord";
        //标识程序是否异常退出键
        public const string strIsAbnormalExitKey = "IsAbnormalExit";
        //标识程序是否断电退出键
        public const string strIsOutageExitKey = "IsOutageExit";
        //记录异常退出的区域序号键
        public const string strAbnormalExit_RegionIndex = "AbnormalExit_RegionIndex";
        //记录异常退出的偏移点序号键
        public const string strAbnormalExit_OffsetIndex = "AbnormalExit_OffsetIndex";

        public const string strMotorParameterClass = "MotorParameter";

        public const string strXMoveOrignalSpeedKey = "XMoveOrignalSpeed";

        public const string strYMoveOrignalSpeedKey = "YMoveOrignalSpeed";

        public const string strAixsMoveWorkSpeedKey = "AixsMoveWorkSpeed";

        public const string strRatioKey = "Ratio";

        #endregion

        //
        private const string strRegexXPoint = @"(?<=\()(.*?)(?=,)";
        //
        private const string strRegexYPoint = @"(?<=,)(.*?)(?=\))";

        /// <summary>
        /// 配置文件路径
        /// </summary>
        private  string strConfigPath = System.Windows.Forms.Application.StartupPath
            + @"\ConfigFile.ini";//配置文件所在路径
        public string StrConfigPath
        {
            get { return strConfigPath; }
            set
            {
                strConfigPath = value;
            }
        }

        private Dictionary<string, Dictionary<string, string>> dicMotorSpeedParamter
            = new Dictionary<string, Dictionary<string, string>>();
        /// <summary>
        /// 从配置文件读出的有关电机的信息
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> DicMotorSpeedParamter
        {
            get { return dicMotorSpeedParamter; }
        }

        private Dictionary<string, Dictionary<string, string>> dicLaserPort
            =new Dictionary<string,Dictionary<string,string>>();
        /// <summary>
        /// 从配置文件读出的有关laserPort的信息
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> DicLaserPort
        {
            get { return dicLaserPort;}
        }

        private Dictionary<string, Dictionary<string, string>> dicCameraPort 
            = new Dictionary<string, Dictionary<string, string>>();
        /// <summary>
        /// 从配置文件读出的有关CameraPort的信息
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> DicCameraPort
        {
            get { return dicCameraPort; }
        }

        public Dictionary<string, Dictionary<string, string>> dicAbnormalRecord
            = new Dictionary<string, Dictionary<string, string>>();
        /// <summary>
        /// 记录异常退出
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> DicAbnormalRecord
        {
            get { return dicAbnormalRecord; }
        }

        private Dictionary<string, PointF> dicMarkRegionCenterPoint 
            = new Dictionary<string, PointF>();
        /// <summary>
        /// 镭射电机中心点
        /// </summary>
        public Dictionary<string, PointF> DicMarkRegionCenterPoint
        {
            get { return dicMarkRegionCenterPoint; }
        }

        private Dictionary<string, PointF> dicOffSetPoint = new Dictionary<string, PointF>();
        /// <summary>
        /// 镭射偏移点
        /// </summary>
        public Dictionary<string, PointF> DicOffSetPoint
        {
            get { return dicOffSetPoint; }
        }

        private Dictionary<string, Dictionary<string, string>> dicLogFile;
        /// <summary>
        /// 从配置文件读出的有关Log文件的信息
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> DicLogFile
        {
            get
            {
                return dicLogFile;
            }
        }

        #region

        private int intOffSetPointColumn;
        /// <summary>
        /// 偏移点的列数
        /// </summary>
        public int IntOffSetPointColumn
        {
            get
            {
                return intOffSetPointColumn;
            }
        }

        private int intOffSetPointRow;
        /// <summary>
        /// 偏移点的列数
        /// </summary>
        public int IntOffSetPointRow
        {
            get
            {
                return intOffSetPointRow;
            }
        }

        private int intRegionCenterPointColumn;
        /// <summary>
        /// 区域中心点的列数
        /// </summary>
        public int IntRegionCenterPointColumn
        {
            get
            {
                return intRegionCenterPointColumn;
            }
        }

        private int intRegionCenterPointRow;
        /// <summary>
        /// 区域中心点的行数
        /// </summary>
        public int IntRegionCenterPointRow
        {
            get
            {
                return intRegionCenterPointRow;
            }
        }

        #endregion

        /// <summary>
        /// 装载配置文件
        /// </summary>
        public void LoadConfigFile()
        {
            dicLaserPort = GetConfigInfo(new string[] { strLaserPortClass }, new string[] { 
                strPortNameKey, strParityKey, strDataBitsKey, strStopBitsKey, strBaudRateKey,
                strLaserErrorRemoveCmdKey,strLaserErrorRequestCmdKey,strLaserMoveToShootingCmdKey,
                strLaserOpenShutterCmdKey,strLaserPosCompensationCmdKey,strLaserStartMarkingCmdKey,
                strLaserStopMarkCmdKey});
            dicCameraPort = GetConfigInfo(new string[] { strCameraPortClass }, new string[] { 
                strCameraPortIPKey,strCameraPortPortKey,strCameraTriggerCmdKey});
            dicAbnormalRecord = GetConfigInfo(new string[] { strAbnormalRecordClass }, new string[] {
                strIsAbnormalExitKey,strIsOutageExitKey,strAbnormalExit_RegionIndex });
            dicLogFile = GetConfigInfo(new string[] { strLogFilePathClass, strDebugLogFilePathClass },
              new string[] { strLogDirPathKey, strLogFileNameKey });
            dicMotorSpeedParamter = GetConfigInfo(new string[]{strMotorParameterClass},new string[]{
                strXMoveOrignalSpeedKey,strYMoveOrignalSpeedKey,strAixsMoveWorkSpeedKey,strRatioKey});
            dicOffSetPoint = GetConfigPoint(new string[] { strOFFSetPointClass });
            intOffSetPointColumn = ReadIntFromIni(strOFFSetPointClass, strPointColunmKey, strConfigPath);
            intOffSetPointRow=ReadIntFromIni(strOFFSetPointClass, strPointRowKey, strConfigPath);
            intRegionCenterPointColumn=ReadIntFromIni(strRegionCenterPointClass, strPointColunmKey, strConfigPath);
            intRegionCenterPointRow = ReadIntFromIni(strRegionCenterPointClass, strPointRowKey, strConfigPath); 

            PointF point11 = GetConfigPoint(strRegionCenterPointClass, strPointKey + "11");
            PointF point12 = GetConfigPoint(strRegionCenterPointClass, strPointKey + "12");
            PointF point21 = GetConfigPoint(strRegionCenterPointClass, strPointKey + "21");
            float rowGrap = point21.Y - point11.Y;
            float columnGrap = point12.X - point11.X;
            dicMarkRegionCenterPoint.Clear();

            for (int i = 0; i < intRegionCenterPointRow ; i++)
            {
                for (int j = 0; j < intRegionCenterPointColumn; j++)
                {
                    PointF point = new PointF();

                    if (i == 0 && j == 0)
                        point = point11;
                    else
                    {
                        point.X = point11.X + columnGrap * j;
                        point.Y = point11.Y + rowGrap * i;
                    }

                    dicMarkRegionCenterPoint.Add((i+1).ToString() + (j+1).ToString(), point);
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigFile()
        {
            //装载配置文件
            LoadConfigFile();
        }

        /// <summary>
        /// 检查配置文件是否有损坏
        /// </summary>
        /// <returns></returns>
        public bool CheckConfigFile()
        {
            if (!System.IO.File.Exists(strConfigPath))
            {
                return false;
            }
            else
            {
                if (intRegionCenterPointColumn <= 0 || intRegionCenterPointRow <= 0 || intOffSetPointColumn <= 0
                    || intOffSetPointRow <= 0 || dicLaserPort.Count <= 0 || dicCameraPort.Count <= 0
                    || DicMarkRegionCenterPoint.Count != intRegionCenterPointColumn * intRegionCenterPointRow
                    || DicOffSetPoint.Count != intOffSetPointRow * intOffSetPointColumn)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        for (int i = 1; i <= intRegionCenterPointRow; i++)
                        {
                            for (int j = 1; j <= intRegionCenterPointColumn; j++)
                            {
                                PointF pointTemp= DicMarkRegionCenterPoint[i.ToString() + j.ToString()];
                            }
                        }

                        for (int i = 1; i <= intOffSetPointRow; i++)
                        {
                            for (int j = 1; j <= intOffSetPointColumn; j++)
                            {
                                PointF pointTemp = DicOffSetPoint[i.ToString() + j.ToString()];
                            }
                        }

                        string[] strLaserPortKey = new string[] {strPortNameKey, strParityKey, strDataBitsKey, 
                                strStopBitsKey, strBaudRateKey,strLaserErrorRemoveCmdKey,strLaserErrorRequestCmdKey,
                                strLaserMoveToShootingCmdKey,strLaserOpenShutterCmdKey,strLaserPosCompensationCmdKey,
                                strLaserStartMarkingCmdKey,strLaserStopMarkCmdKey };

                        for (int i = 0; i < dicLaserPort[strLaserPortClass].Count; i++)
                        {
                            if (dicLaserPort[strLaserPortClass][strLaserPortKey[i]] == null
                                || dicLaserPort[strLaserPortClass][strLaserPortKey[i]].Length < 1)
                            {
                                return false;
                            }
                        }

                        string[] strCameraPortKeyArray = new string[] { strCameraPortIPKey, strCameraPortPortKey,strCameraTriggerCmdKey };

                        for (int i = 0; i < dicCameraPort[strCameraPortClass].Count; i++)
                        {
                            if (dicCameraPort[strCameraPortClass][strCameraPortKeyArray[i]] == null
                                || dicCameraPort[strCameraPortClass][strCameraPortKeyArray[i]].Length < 1)
                            {
                                return false;
                            }
                        }

                        string[] strMotorSpeedParamterKeyArray = new string[] { strXMoveOrignalSpeedKey,strYMoveOrignalSpeedKey,
                             strAixsMoveWorkSpeedKey,strRatioKey };
                         
                        for (int i = 0; i < dicMotorSpeedParamter.Count; i++)
                        {
                             if (dicMotorSpeedParamter[strMotorParameterClass][strMotorSpeedParamterKeyArray[i]] == null
                                || dicMotorSpeedParamter[strMotorParameterClass][strMotorSpeedParamterKeyArray[i]].Length < 1)
                            {
                                 return false;
                            }
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 写镭射端口信息
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        public void WriteLaserPort(string strKey,string strValue)
        {
            if (!System.IO.File.Exists(strConfigPath))
                System.IO.File.Create(strConfigPath);

            WriteToIni(strLaserPortClass,strKey ,strValue, strConfigPath);
        }

        /// <summary>
        /// 写相机端口信息
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        public void WriteCameraPort(string strKey, string strValue)
        {
            if (!System.IO.File.Exists(strConfigPath))
                System.IO.File.Create(strConfigPath);

            WriteToIni(strCameraPortClass, strKey, strValue, strConfigPath);
        }

        /// <summary>
        /// 写相机端口信息
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        public void WriteMotorSpeedParameter(string strKey, string strValue)
        {
            if (!System.IO.File.Exists(strConfigPath))
                System.IO.File.Create(strConfigPath);

            WriteToIni(strMotorParameterClass, strKey, strValue, strConfigPath);
        }

        /// <summary>
        /// 写异常记录
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        public void WriteAbnormalRecord(string strKey, string strValue)
        {
            lock (this)
            {
                try
                {
                    if (!System.IO.File.Exists(strConfigPath))
                        System.IO.File.Create(strConfigPath);

                    WriteToIni(strAbnormalRecordClass, strKey, strValue, strConfigPath);
                }
                catch 
                {
                    System.Windows.Forms.MessageBox.Show("LogFile error!");
                }
            }
        }

        /// <summary>
        /// 将对应键值下的区域中心点，也就是电机走点写入文件
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="regionCenterPoint"></param>
        public void WriteRegionCenterPoint(string strKey, PointF regionCenterPoint)
        {
            if (!System.IO.File.Exists(strConfigPath))
                System.IO.File.Create(strConfigPath);

            WriteToIni(strRegionCenterPointClass, strPointKey.Trim() + strKey.Trim(), "(" + regionCenterPoint.X.ToString()
                + "," + regionCenterPoint.Y.ToString() + ")", strConfigPath);
        }
        
        /// <summary>
        /// 将对应键值下的偏移点写入文件
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="offsetPoint"></param>
        public void WriteOFFSetPoint(string strKey,PointF offsetPoint)
        {
            if (!System.IO.File.Exists(strConfigPath))
                System.IO.File.Create(strConfigPath);

            WriteToIni(strOFFSetPointClass, strPointKey.Trim() + strKey.Trim(),"("+ offsetPoint.X.ToString()
                +","+offsetPoint.Y.ToString()+")", strConfigPath);
        }

         /// <summary>
        /// 从新加载配置文件异常退出信息
        /// </summary>
        public void ReloadConfigAbnormalExitInfo()
        {
            try
            {
                dicAbnormalRecord = GetConfigInfo(new string[] { strAbnormalRecordClass }, new string[] {
                strIsAbnormalExitKey,strIsOutageExitKey,strAbnormalExit_RegionIndex });
            }
            catch 
            {
                System.Windows.Forms.MessageBox.Show("read LogFile error!");
            }
        }

        /// <summary>
        /// 获取配置文件中运动点的信息
        /// </summary>
        /// <param name="strPointClass">类名</param>
        /// <returns>返回对应类名下的点的字典</returns>
        private Dictionary<string, PointF> GetConfigPoint(string[] strPointClass)
        {
            Dictionary<string, PointF> dicPoint = new Dictionary<string, PointF>();
           
            if (!System.IO.File.Exists(strConfigPath))
            {
                throw new Exception("File is not exists！");
            }

            for (int i = 0; i < strPointClass.Length; i++)
            {
                int column = ReadIntFromIni(strPointClass[i], strPointColunmKey, strConfigPath);
                int row = ReadIntFromIni(strPointClass[i], strPointRowKey, strConfigPath);

                for (int j = 1; j <= row; j++)
                {
                    for (int k = 1; k <= column; k++)
                    {
                        string strValueTemp = ReadStrFromIni(strPointClass[i], strPointKey + j.ToString() + k.ToString(),
                            strConfigPath);

                        PointF pointTemp = new PointF((float)RegexMatch.douRegexText(strValueTemp, strRegexXPoint),
                                (float)RegexMatch.douRegexText(strValueTemp, strRegexYPoint));
                        dicPoint.Add(j.ToString() + k.ToString(), pointTemp);
                    }
                }
            }

            return dicPoint;
        }

        /// <summary>
        /// 从配置文件中读取坐标点
        /// </summary>
        /// <param name="strPointClass"></param>
        /// <returns></returns>
        private PointF GetConfigPoint(string strPointClass, string strKey)
        {
            PointF pointResult = new PointF();

            if (!System.IO.File.Exists(strConfigPath))
            {
                throw new Exception("File is not exists！");
            }

            string strTemp = ReadStrFromIni(strPointClass, strKey, strConfigPath);

            if (strTemp.Length > 0)
            {
                pointResult.X = (float)RegexMatch.douRegexText(strTemp, strRegexXPoint);
                pointResult.Y = (float)RegexMatch.douRegexText(strTemp, strRegexYPoint);
            }
            else
            {
                throw new Exception("Config File Error");
            }

            return pointResult;
        }

        /// <summary>
        /// 读取配置文件中串口的配置信息
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, Dictionary<string, string>> GetConfigInfo(string[] strPortClass,
            string[] strPortKey)
        {
            Dictionary<string, Dictionary<string, string>> configResult = new Dictionary<string, Dictionary<string, string>>();
          
            if (!System.IO.File.Exists(strConfigPath))
            {
                throw new Exception("File is not exists！");
            }

            for (int j = 0; j < strPortClass.Length; j++)
            {
                Dictionary<string, string> strPortTemp = new Dictionary<string, string>();

                for (int i = 0; i < strPortKey.Length; i++)
                {
                    strPortTemp.Add(strPortKey[i], ReadStrFromIni(strPortClass[j], strPortKey[i], strConfigPath));
                }

                configResult.Add(strPortClass[j], strPortTemp);
            }

            return configResult;
        }

    }
}
