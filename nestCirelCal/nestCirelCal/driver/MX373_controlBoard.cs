using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using nestCirelCal.Common;
using nestCirelCal.Log;

namespace nestCirelCal.mx373_control
{

    class NewReceived
    {
        public bool isReceived { set; get; }
        public byte[] byteArry { set; get; }
    }
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
        public Mx373_controlBoard()
        {
            newReceived = new NewReceived();
        }

        public void connect(string portName, int baud)
        {
            sPort = new SerialCom(portName, baud, Parity.None, 8, StopBits.One);
            sPort.Connect();
            sPort.ConnectChanged += SPort_ConnectChanged;
            sPort.DataReceived += onDataReceived;
        }

        private void SPort_ConnectChanged(object sender, EventArgs e)
        {
            evntConnectChanged?.Invoke(this, "ConnectChanged");
            throw new NotImplementedException();
        }


        string lineBuff;
        void onDataReceived(object sender, EventArgs e) {
            bytesEventArgs x = (bytesEventArgs)e;
            lineBuff += Encoding.Default.GetString(x.byteArry);
            lineBuff = lineBuff.ToLower();

            //char[] sep = new char[2] { '\r','\n'};
            //string[] ss = lineBuff.Split(sep);
            //foreach(string xs in ss)
            //{
            //    Console.WriteLine("<{0}>", xs);
            //}

            int idx = lineBuff.IndexOf("\r\n");
            if (idx > 0)
            {
                Console.WriteLine("recvLine: {0}", lineBuff);
                newReceived.isReceived = true;
                newReceived.byteArry = Encoding.ASCII.GetBytes(lineBuff);
                lineBuff = "";
            }
        }

        NewReceived newReceived;
        SerialCom sPort;

        /// <summary>
        /// 配置测试模式
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public int configJson(string cfg)
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


        public bool measureVolt(UInt16 ch, ref Int32 volt)
        {
            newReceived.isReceived = false;
            newReceived.byteArry = null;
            sPort.Send(String.Format("adc.read {0:d}\r\n", ch));
            for (int i=0;i<1000;i++)
            {
                if (newReceived.isReceived == true) {
                    char[] sep = new char[2] { ',',' '};
                    string rsp = Encoding.Default.GetString(newReceived.byteArry);
                    string[] rslt = rsp.Split(sep);

                    if (rslt[0].IndexOf("+ok@adc.read") == 0)
                    {
                        int chX = Convert.ToInt32(rslt[1]);
                        if (chX == ch) {
                            volt = Convert.ToInt32(rslt[2]);
                            return true;
                        }
                        break;
                    }
                    else if (rslt[0].IndexOf("+err@adc.read") == 0) {
                        break;
                    }
                    newReceived.isReceived = false;
                }
                Thread.Sleep(10);
            }
            return false;
        }

        public bool battInfo(ref Int32 uA)
        {
            newReceived.isReceived = false;
            newReceived.byteArry = null;
            sPort.Send(String.Format("batt.info\r\n"));
            for (int i = 0; i < 1000; i++)
            {
                if (newReceived.isReceived == true)
                {
                    char[] sep = new char[4] { ',', ' ', '(', ')'};
                    string rsp = Encoding.Default.GetString(newReceived.byteArry);
                    string[] rslt = rsp.Split(sep);

                    if ((rslt[0].IndexOf("+ok@0.batt.info") == 0)|| (rslt[0].IndexOf("+ok@batt.info") == 0))
                    {
                        uA = Convert.ToInt32(rslt[2]);
                        return true;
                    }
                    else if (rslt[0].IndexOf("+err@batt.info") == 0)
                    {
                        break;
                    }
                    newReceived.isReceived = false;
                }
                Thread.Sleep(10);
            }
            return false;
        }

    }
}
