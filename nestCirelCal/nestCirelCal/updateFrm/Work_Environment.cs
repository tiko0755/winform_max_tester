using System;
using System.Collections.Generic;
using System.Threading;
using nestCirelCal.Common;
using nestCirelCal.Log;
using System.Text;
using System.Drawing;
using nestCirelCal.mx373_control;
using nestCirelCal.app152;

namespace nestCirelCal.work_environment
{
    public class Work_Environment
    {
        /// <summary>
        /// 产测控制板的实例
        /// </summary>
        public Mx373_controlBoard mx373ControlBoard;

        /// <summary>
        /// DUT
        /// </summary>
        public APP152 dut;

        /// <summary>
        /// 配置
        /// </summary>
        public ConfigJson confJson;

        public Work_Environment(ConfigJson jConf)
        {
            confJson = jConf;
            // instance devices
            mx373ControlBoard = new Mx373_controlBoard();
            dut = new APP152();
        }

        public void log(string content, Logger.LogType type) {
            Logger.Instance.WriteLog(content, type);
        }

    }
    

}
