﻿using System;
using System.Collections.Generic;
using System.Threading;
using nestCirelCal.Common;
using nestCirelCal.Log;
using System.Text;
using System.Drawing;
using nestCirelCal.dmm34401a;
using nestCirelCal.mx373_control;

namespace nestCirelCal.work_environment
{
    public class Work_Environment
    {
        /// <summary>
        /// 产测控制板的实例
        /// </summary>
        public Mx373_controlBoard mx373ControlBoard;

        /// <summary>
        /// 万用表34410A的实例
        /// </summary>
        public Multimeter_34401A dmm_34401a;

        /// <summary>
        /// 配置
        /// </summary>
        public ConfigJson confJson;

        public Work_Environment(ConfigJson jConf)
        {
            confJson = jConf;
            // instance devices
            mx373ControlBoard = new Mx373_controlBoard("com1");
            dmm_34401a = new Multimeter_34401A("com2");
        }

        public void log(string content, Logger.LogType type) {
            Logger.Instance.WriteLog(content, type);
        }

    }
    

}
