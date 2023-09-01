using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using nestCirelCal.Common;
using nestCirelCal.MaxCmdType;

namespace nestCirelCal.app152
{

    /// <summary>
    /// 负责DMM操作的的接口
    /// </summary>
    public class APP152
    {
        public event StrEventHandler evntConnectChanged;

        /// <summary>
        /// List save the serial port incoming.
        /// </summary>
        List<byte[]> rcvLst;
        /// <summary>
        /// fetch the MAXEYE command, save to this list
        /// </summary>
        List<maxCmdType> lineLst;
        /// <summary>
        /// temp buffer for maxeye command
        /// </summary>
        byte[] rcvPool;
        /// <summary>
        /// point to 'rcvPool'
        /// </summary>
        int poolIndx;
        /// <summary>
        /// somewhere, need string command
        /// </summary>
        string strPool;
        /// <summary>
        /// the result of string command line
        /// </summary>
        List<string> strCmdLst;

        Thread thrd_pollingLine;

        /// <summary>
        /// 实例APP152接口
        /// </summary>
        /// <param name="portName">串口名字</param>
        /// <param name="baudrate">波特率</param>
        /// <param name="parity">校验位</param>
        public APP152()
        {
            rcvLst = new List<byte[]>();
            lineLst = new List<maxCmdType>();
            rcvPool = new byte[4096];
            poolIndx = 0;

            strCmdLst = new List<string>();

            StartPollingLineTask();
        }

        /// <summary>
        /// 吸引异步数据
        /// </summary>
        /// <param name="arg"></param>
        void pollingLineTask(object arg)
        {
            int pollingTim = 20;
            while (true) {
                if (rcvLst.Count > 0)
                {
                    // accept incomming
                    Console.WriteLine("poolIndx:{0}", poolIndx);
                    rcvLst[0].CopyTo(rcvPool, poolIndx);
                    poolIndx += rcvLst[0].Length;
                    Console.WriteLine("poolIndx:{0}", poolIndx);

                    byte lastByte = rcvPool[poolIndx - 1];
                    int headIndx = 0;
                    bool possible = false;
                    maxCmdType cmd = new maxCmdType();
                    for (int ii = 0; ii < poolIndx; ii++)
                    {
                        // meet head[0]
                        if (rcvPool[ii + 0] == cmd.head[0])
                        {
                            cmd.Reset();
                            headIndx = ii;
                            possible = true;
                        }
                        else
                        {
                            possible = false;
                            continue;
                        }

                        // meet head[1]
                        if ((ii + 1) < poolIndx)
                        {
                            if (rcvPool[ii + 1] == cmd.head[1])
                            {
                                Console.WriteLine("Fetch HEAD {0}", cmd.head[1]);
                            }
                            else
                            {
                                possible = false;
                                continue;
                            }
                        }
                        else
                        {
                            break;
                        }

                        // meet cmd
                        if ((ii + 2) < poolIndx)
                        {
                            cmd.op = (MAX_OPRATE)rcvPool[ii + 2];
                        }
                        else
                        {
                            break;
                        }

                        // meet len
                        if ((ii + 3) < poolIndx)
                        {
                            cmd.len = rcvPool[ii + 3];
                        }
                        else
                        {
                            break;
                        }

                        // meet payload
                        if (cmd.len > 0)
                        {
                            if((ii + 4 + cmd.len-1)< poolIndx)
                            {
                                // copy payload
                                cmd.payload = new byte[cmd.len];
                                for (int jj = 0; jj < cmd.len; jj++)
                                {
                                    cmd.payload[jj] = rcvPool[ii + 4 + jj];
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        
                        // meet CRC
                        if ((ii + 4 + cmd.len) < poolIndx)
                        {
                            cmd.crc8 = rcvPool[ii + 4 + cmd.len];

                            Console.Write("cmd: 0x{0:x02} {1:x02} {2:x02} {3:x02}", cmd.head[0], cmd.head[1], (byte)cmd.op, cmd.len);
                            for(int iii = 0; iii < cmd.len; iii++)
                            {
                                Console.Write(" {0:x02}", cmd.payload[iii]);
                            }
                            Console.Write("\nCRC: {0:x02}\r\n", cmd.crc8);

                            // finally check
                            byte[] crcBuf = new byte[4+cmd.len];
                            crcBuf[0] = cmd.head[0];
                            crcBuf[1] = cmd.head[1];
                            crcBuf[2] = (byte)cmd.op;
                            crcBuf[3] = cmd.len;
                            Array.Copy(cmd.payload, 0, crcBuf, 4, cmd.len);
                            byte crc8 = maxCmdType.generateCRC8(crcBuf);
                            if(crc8 == cmd.crc8)
                            {
                                Console.WriteLine("CRC8[0x{0:x02}] check pass", crc8);
                                //lineLst.Add(cmd);
                            }
                            else
                            {
                                Console.WriteLine("CRC8[0x{0:x02}] check fail", crc8);
                            }
                            lineLst.Add(cmd);

                            possible = false;
                            ii += (4 + cmd.len);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (possible)
                    {
                        byte[] bArry = new byte[4096];
                        bArry.Initialize();
                        Array.Copy(rcvPool, headIndx, bArry, 0, poolIndx - headIndx);
                        rcvPool = bArry;
                        poolIndx -= headIndx;
                    }
                    else
                    {
                        rcvPool.Initialize();
                        poolIndx = 0;
                    }
                    cmd = null;
                }

                if (rcvLst.Count > 0)
                {
                    // accept for string commander
                    strPool += Encoding.Default.GetString(rcvLst[0]);
                    rcvLst.RemoveAt(0);

                    char[] sep = new char[2] {'\r','\n' };
                    string[] xLine = strPool.Split(sep);
                    foreach (string rec in xLine)
                    {
                        if(rec.Length > 0)
                            strCmdLst.Add(rec);
                    }
                    // test last line
                    if((strPool.Last() == '\r') || (strPool.Last() == '\n'))
                    {
                        strPool = "";   // clear
                    }
                    else
                    {
                        strPool = xLine[xLine.Length - 1];
                    }
                }
                Thread.Sleep(pollingTim);
            }
        }

        public void CleanCommandLine()
        {
            rcvPool.Initialize();
            strPool = "";
            lineLst.Clear();
            strCmdLst.Clear();
        }

        public void StartPollingLineTask()
        {
            thrd_pollingLine = new Thread(new ParameterizedThreadStart(pollingLineTask));
            thrd_pollingLine.IsBackground = true;
            thrd_pollingLine.Start();
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

        void onDataReceived(object sender, EventArgs e) {
            bytesEventArgs x = (bytesEventArgs)e;
            rcvLst.Add(x.byteArry);
        }

        SerialCom sPort;

        bool request(MAX_OPRATE OP, byte[] Payload, ref byte[] rslt, int timeout)
        {
            MAX_OPRATE reqOP = OP;
            MAX_OPRATE resOP = (MAX_OPRATE)((int)reqOP | 0x80);

            // remove old respose first if there was
            for (int j = 0; j < lineLst.Count; j++)
            {
                if (lineLst[j].op == resOP)
                {
                    rslt = lineLst[j].payload;
                    lineLst.RemoveAt(j);
                }
            }

            // send out
            maxCmdType cmd = new maxCmdType(reqOP, Payload);
            sPort.Send(cmd.toByteArry());

            // 将异步转换为同步
            for (int i = 0; i < timeout/20; i++)
            {
                for (int j = 0; j < lineLst.Count; j++)
                {
                    if (lineLst[j].op == resOP)
                    {
                        rslt = lineLst[j].payload;
                        lineLst.RemoveAt(j);
                        return true;
                    }
                }
                Thread.Sleep(20);
            }

            return false;
        }

        public bool getNTC_temp(ref Int32 rslt)
        {
            byte[] resPayload = null;
            if(request(MAX_OPRATE.REQ_NTC_TEMP, null, ref resPayload, 100) == false)
            {
                return false;
            }

            rslt = resPayload[1];
            return true;
        }

        public bool gSensorCheck(ref Int32 rslt)
        {
            byte[] resPayload = null;
            if (request(MAX_OPRATE.REQ_G_SENSOR_VERIFY, null, ref resPayload, 100) == false)
            {
                return false;
            }
            if(resPayload == null)
            {
                return false;
            }
            rslt = resPayload[1];
            return true;
        }

        public bool gSensorCheck_willDispose(ref Int32 rslt)
        {
            rslt = 0;
            foreach (string rec in strCmdLst)
            {
                if(rec.IndexOf("sensor_wakeup_key_test:") > 0)
                {
                    rslt = 1;
                    break;
                }
            }
            return true;
        }




    }
}
