using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using nestCirelCal.Common;

namespace nestCirelCal.mx373_control
{
    /// <summary>
    /// 负责DMM操作的的接口
    /// </summary>
    public class Mx373_controlBoard
    {

        public event StrEventHandler evntConnectChanged;

        /// <summary>
        /// 实例一个万用表
        /// </summary>
        /// <param name="portName">串口名字</param>
        /// <param name="baudrate">波特率</param>
        /// <param name="parity">校验位</param>
        public Mx373_controlBoard(string portName)
        {

        }

        public void connect(string portName, int baud)
        {
            sPort = new SerialCom(portName, baud, Parity.None, 8, StopBits.One);
            sPort.Connect();
            sPort.ConnectChanged += SPort_ConnectChanged;
        }

        private void SPort_ConnectChanged(object sender, EventArgs e)
        {
            evntConnectChanged?.Invoke(this, "ConnectChanged");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 配置测试模式
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public int configJson(string cfg)
        {
            return 0;
        }

        public double measure(string type)
        {
            return 0;
        }

        public UInt32 readInput()
        {
            return 0;
        }

        public UInt32 readOutput()
        {
            return 0;
        }

        public UInt32 writeOutput(UInt16 indx, bool level)
        {
            string cmd = String.Format("output.writepin {0:d} {1:d}\r\n", indx, level);
            sPort.Send(cmd);
            return 0;
        }
        public UInt32 writeOutput(UInt16 indx0, bool level0, UInt16 indx1, bool level1)
        {
            sPort.Send(String.Format("output.writepin {0:d} {1:d} {2:d} {3:d}\r\n", indx0, level0, indx1, level1));
            return 0;
        }
        public UInt32 writeOutput(UInt32 hex)
        {
            sPort.Send(String.Format("output.writepin 0x{0:8x}\r\n", hex));
            return 0;
        }

        SerialCom sPort;
    }
}
