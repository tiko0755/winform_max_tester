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
    public delegate string DoCommand(string str);

    class TagVal
    {
        public string tag { set; get; }
        public string val { set; get; }
    }

    partial class Work_Test
    {
        public delegate void UpdateTestLine(object sender, int lineIndx, string result, string judgement, double itemTim, Color itemColor, Color rsltColor);
        public delegate void UpdateTestCounter(object sender, uint total, uint pass, uint fail);
        public delegate void UpdateTestTips(object sender, string tips);
        public delegate void UpdateTestTotalTime(object sender, double tTime);

        /// <summary>
        /// 更新测试计数器的事件
        /// </summary>
        public event UpdateTestCounter evnt_updateTestCounter;

        /// <summary>
        /// update test line的事件
        /// </summary>
        public event UpdateTestLine evnt_updateTestLine;

        /// <summary>
        /// update test tips的事件
        /// </summary>
        public event UpdateTestTips evnt_updateTestTips;

        /// <summary>
        /// 更新测试时间的事件
        /// </summary>
        public event UpdateTestTotalTime evnt_updateTestTime;


        /// <summary>
        /// 工作线程
        /// </summary>
        Thread thrd;

        /// <summary>
        /// 环境配置
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

        /// <summary>
        /// 测项清单的实现方法
        /// </summary>
        public List<TestItem> lst_testItem;

        public Work_Test(Work_Environment env)
        {
            this.env = env;

            dmmPort = new SerialCom("com3", 115200, 0, 8, System.IO.Ports.StopBits.One);

            dmmPort.Connect();
            dmmPort.DataReceived += onRcvd_dmmPort;

            lst_testItem = new List<TestItem>();
            lst_testItem.Add(testItem_0);
            lst_testItem.Add(testItem_1);
            lst_testItem.Add(testItem_2);
            lst_testItem.Add(testItem_3);
            lst_testItem.Add(testItem_4);
            lst_testItem.Add(testItem_5);
            lst_testItem.Add(testItem_6);
            lst_testItem.Add(testItem_7);
            lst_testItem.Add(testItem_8);
            lst_testItem.Add(testItem_9);
            lst_testItem.Add(testItem_10);
            lst_testItem.Add(testItem_11);
            lst_testItem.Add(testItem_12);
            lst_testItem.Add(testItem_13);
            lst_testItem.Add(testItem_14);
            lst_testItem.Add(testItem_15);
            lst_testItem.Add(testItem_16);
            lst_testItem.Add(testItem_17);
            lst_testItem.Add(testItem_18);
            lst_testItem.Add(testItem_19);
            lst_testItem.Add(testItem_20);
            lst_testItem.Add(testItem_21);
            lst_testItem.Add(testItem_22);
            lst_testItem.Add(testItem_23);
            lst_testItem.Add(testItem_24);

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

        public void StartTask(object sender)
        {
            thrd = new Thread(new ParameterizedThreadStart(taskFlow));
            thrd.IsBackground = true;
            thrd.Start();
        }
        void taskFlow(object arg)
        {
            // wait for main ready
            while (evnt_updateTestLine == null)
            {
                Thread.Sleep(100);
            }

            List<ConfigJsonTest_row> table_row = env.confJson.test.table_row;

            for (int i = 0; i < table_row.Count; i++)
            {
                evnt_updateTestLine(this, i, "", "", 0,  Color.LightGray, Color.LightGray);
            }

            // start run time counter
            double totalTim = 0;
            Timer threadTimer = new Timer(new System.Threading.TimerCallback(delegate (object val) {
                totalTim += 50;
                evnt_updateTestTime(this, totalTim);
            }), null, 0, 50);  //最后两个参数依次为：多久后开始，隔多久执行一次。

            //Thread thread1 = new Thread(delegate () {
            //    while (true)
            //    {
            //        evnt_updateTestTime(this, totalTim);
            //        Thread.Sleep(50);
            //        totalTim += 50;
            //    }
            //});
            //thread1.Start();

            double t;
            int slot = 0;
            for (int i = 0; i < table_row.Count; i++)
            {
                // initial test
                t = totalTim;
                string result = "undefined", judgeMent = "warning";
                Color colorRslt = Color.Green;
                Color colorX = Color.Yellow;
                // do test
                bool haveTested = false;
                for (int j=0;(j< lst_testItem.Count) && (haveTested==false); j++)
                {
                    TestItem testMethod = lst_testItem[j];
                    haveTested |= testMethod(table_row[i], slot, ref result, ref judgeMent);
                }
                //if(haveTested == false)
                //{
                //    result = "undefined";
                //}
                // update test result
                if (judgeMent.ToLower() == "pass") {
                    colorX = Color.Green;
                }
                else if (judgeMent.ToLower() == "fail")
                {
                    colorX = Color.Red;
                    colorRslt = Color.Red;
                }
                else
                {
                    if (colorRslt != Color.Red) {
                        colorRslt = Color.Yellow;
                    }
                }

                evnt_updateTestLine(this, i, result, judgeMent, totalTim-t, colorX, colorRslt);
                // check test end
                if(i == table_row.Count-1) {
                    totalCount++;
                    passCount++;
                    evnt_updateTestCounter(this, totalCount, passCount, failCount);
                    threadTimer.Dispose();
                    thrd.Abort();
                }
            }
        }

        public void updateTips(string tips, int tim)
        {
            Thread thread1 = new Thread(delegate () {
                evnt_updateTestTips(this, tips);
                Thread.Sleep(tim);
                evnt_updateTestTips(this, "");
            });
            thread1.Start();
        }


        /// <summary>
        /// 响应串口接收到数据事件
        /// </summary>
        private void onRcvd_dmmPort(object sender, EventArgs e)
        {
            bytesEventArgs x = (bytesEventArgs)e;
            log(Encoding.ASCII.GetString(x.byteArry), Logger.LogType.DMM);
        }

        /// <summary>
        /// 响应测试界面[开始]按键的单击事件
        /// </summary>
        public void onClick_btnStart(object sender, EventArgs e) {
            Console.WriteLine("onEvent_btnStart");
            if ((thrd == null) || (thrd.IsAlive == false))
            {
                StartTask(this);
                updateTips("Start processing", 1000);
            }
            else
            {
                updateTips("Wait! Processing...", 1000);
            }
        }

        /// <summary>
        /// 响应工程界面[发送]按键的单击事件
        /// </summary>
        public void onClick_btnSend(object sender, EventArgs e)
        {
            Console.WriteLine("onClick_btnSend");
            dmmPort.Send("hello world\r\n");
        }

        /// <summary>
        /// 响应工程界面[发送]按键的单击事件
        /// </summary>
        public void onDClick_richTxtBoxLog(object sender, EventArgs e)
        {
            Console.WriteLine("onClick_btnSend");
        }


        /// <summary>
        /// 界面实例
        /// </summary>
        UpdateFrm mFrm;

        /// <summary>
        /// DMM串口
        /// </summary>
        SerialCom dmmPort;

        /// <summary>
        /// DUT串口
        /// </summary>
        SerialCom dutPort;


    }
    

}
