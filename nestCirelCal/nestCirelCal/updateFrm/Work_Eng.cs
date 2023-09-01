using System;
using System.Collections.Generic;
using System.Threading;
using nestCirelCal.Common;
using nestCirelCal.Log;
using System.Text;
using System.Drawing;
using nestCirelCal.work_environment;

namespace nestCirelCal
{
    /// <summary>
    /// 指令调试
    /// </summary>
    public class Work_Eng
    {
        //public delegate void UpdateTestCounter(object sender, uint total, uint pass, uint fail);
        //public delegate void UpdateTestTips(object sender, string tips);
        //public delegate void UpdateTestTotalTime(object sender, double tTime);

        /// <summary>
        /// 更新log窗口的事件
        /// </summary>
        public event StrEventHandler evnt_updateUI_log;

        /// <summary>
        /// connected event publish
        /// </summary>
        public event EventHandler evnt_connected;
        /// <summary>
        /// disconnected event publish
        /// </summary>
        public event EventHandler evnt_disconnected;

        /// <summary>
        /// 工作线程
        /// </summary>
        Thread thrd;

        /// <summary>
        /// 配置
        /// </summary>
        Work_Environment env;

        /// <summary>
        /// 生产总计数
        /// </summary>
        uint totalCount;

        /// <summary>
        /// 失败计数
        /// </summary>
        uint failCount;

        /// <summary>
        /// 成功计数
        /// </summary>
        uint passCount;


        

        SerialCom serial;

        public Work_Eng(Work_Environment env)
        {
            this.env = env;

            //log("DoWork initial done");
            //updateTips("Click [START] to start", 100);

        }

        void log(string content, Logger.LogType type) {
            Logger.Instance.WriteLog(content, type);
        }
        void log(string content)
        {
            Logger.Instance.WriteLog(content, Logger.LogType.Working);
        }

        public void onSend(object sender, string str)
        {
            if (serial == null)
            {
                Console.WriteLine("com{0} isNOT open", str);
                return;
            }
            serial.Send(str);
            Console.WriteLine("work.eng.send:{0}", str);
        }

        public void onConnectDisconnect(object sender, string com)
        {
            if (serial == null)
            {
                serial = new SerialCom(com, 115200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                serial.ConnectChanged += onConnectStatusChanged;
                serial.DataReceived += onDataReceived;
            }
            if(serial.ConnectedSta == false)
            {
                serial.Connect();
            }
            else
            {
                serial.Disconnect();
            }
            Console.WriteLine("onConnectDisconnect:{0}", com);
        }

        public void onDataReceived(object sender, EventArgs e)
        {
            bytesEventArgs argx = (bytesEventArgs)e;
            string rxStr = System.Text.Encoding.Default.GetString(argx.byteArry);
            Console.WriteLine("onDataReceived:{0}", rxStr);
            evnt_updateUI_log?.Invoke(this, rxStr);
        }

        public void onConnectStatusChanged(object sender, EventArgs e)
        {
            if (serial.ConnectedSta)
            {
                evnt_connected?.Invoke(this, null);
            }
            else
            {
                evnt_disconnected?.Invoke(this, null);
            }
            Console.WriteLine("onConnectStatusChanged:{0}", e);
        }

    }
    

}
