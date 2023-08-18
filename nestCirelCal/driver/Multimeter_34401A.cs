using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using nestCirelCal.Common;

namespace nestCirelCal.dmm34401a
{
    /// <summary>
    /// 负责DMM操作的的接口
    /// </summary>
    public class Multimeter_34401A
    {
        /// <summary>
        /// 实例一个万用表
        /// </summary>
        /// <param name="portName">串口名字</param>
        /// <param name="baudrate">波特率</param>
        /// <param name="parity">校验位</param>
        public Multimeter_34401A(string portName)
        {
            sPort = new SerialCom(portName, 9600, Parity.None, 8, StopBits.One);
        }

        /// <summary>
        /// 配置测试模式
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public int ConfigJson(string cfg)
        {
            return 0;
        }


        public double Measure(string type)
        {


            return 0;
        }

        SerialCom sPort;
    }
}
