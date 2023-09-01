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
        public delegate void UpdateTestLine(object sender, int lineIndx, string result, string judgement, double itemTim, Color itemColor);
        public delegate void UpdateTestCounter(object sender, uint total, uint pass, uint fail);
        public delegate void UpdateTestTips(object sender, string tips, Color itemColor);
        public delegate void UpdateTestTotalTime(object sender, double tTime);

        public delegate void UpdateButtonBC(object sender, Color bc);
        public delegate void UpdateButtonText(object sender, string txt);
        public delegate void UpdateButtonEnabled(object sender, bool enabled);

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

        public event UpdateButtonBC evnt_updateBtnStartBG;
        public event UpdateButtonText evnt_updateBtnStartTxt;
        //public event UpdateButtonEnabled evnt_updateBtnStartEnabled;

        /// <summary>
        /// 工作线程
        /// </summary>
        Thread thrd;

        /// <summary>
        /// 自动启动线程
        /// </summary>
        Thread thrdAutoStart;

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

            env.dut.connect(env.confJson.dut_config.com, 115200);
            env.mx373ControlBoard.connect(env.confJson.board_config.com, 115200);

            //dmmPort = new SerialCom("com3", 115200, 0, 8, System.IO.Ports.StopBits.One);

            //dmmPort.Connect();
            //dmmPort.DataReceived += onRcvd_dmmPort;

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

            // run auto start
            autoStart();
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
        Timer threadTimer;
        void taskFlow(object arg)
        {
            evnt_updateBtnStartBG?.Invoke(this, Color.Yellow);
            evnt_updateBtnStartTxt?.Invoke(this, "测试中");
            // wait for main ready
            while (evnt_updateTestLine == null)
            {
                Thread.Sleep(100);
            }

            List<ConfigJsonTest_row> table_row = env.confJson.test.table_row;

            for (int i = 0; i < table_row.Count; i++)
            {
                evnt_updateTestLine?.Invoke(this, i, "", "", 0,  Color.LightGray);
            }

            // start run time counter
            double totalTim = 0;
            int startBtnTick = 0;
            Color btnBC = Color.LightGray;
            threadTimer = new Timer(new System.Threading.TimerCallback(delegate (object val) {
                totalTim += 50;
                evnt_updateTestTime?.Invoke(this, totalTim);

                startBtnTick += 50;
                if (startBtnTick > 300)
                {
                    startBtnTick = 0;
                    if (btnBC != Color.LightGray)
                    {
                        evnt_updateBtnStartBG?.Invoke(this, Color.LightGray);
                        btnBC = Color.LightGray;
                    }
                    else
                    {
                        evnt_updateBtnStartBG?.Invoke(this, Color.Yellow);
                        btnBC = Color.LightYellow;
                    }
                }
            }), null, 0, 50);  //最后两个参数依次为：多久后开始，隔多久执行一次。

            double t;
            int slot = 0;
            bool success = true;

            env.dut.CleanCommandLine(); // clean up for new comming test

            for (int i = 0; i < table_row.Count; i++)
            {
                // initial test
                t = totalTim;
                string result = "undefined", judgeMent = "warning";
                Color colorX = Color.Yellow;
                // do test
                bool haveTested = false;
                for (int j=0;(j< lst_testItem.Count) && (haveTested==false); j++)
                {
                    TestItem testMethod = lst_testItem[j];
                    haveTested |= testMethod(table_row[i], slot, ref result, ref judgeMent);
                }

                // update test result
                if (judgeMent.ToLower() == "pass") {
                    colorX = Color.Green;
                    success &= true;
                }
                else if (judgeMent.ToLower() == "fail")
                {
                    colorX = Color.Red;
                }
                evnt_updateTestLine(this, i, result, judgeMent, totalTim - t, colorX);

                success &= (judgeMent.ToLower() == "pass");
                // check test end
                if(i == table_row.Count-1) {
                    threadTimer.Dispose();
                    totalCount++;
                    if (success)
                    {
                        passCount++;
                        evnt_updateBtnStartBG?.Invoke(this, Color.Green);
                        evnt_updateBtnStartTxt?.Invoke(this, "PASS");
                    }
                    else{
                        failCount++;
                        evnt_updateBtnStartBG?.Invoke(this, Color.Red);
                        evnt_updateBtnStartTxt?.Invoke(this, "Fail");
                    }
                    evnt_updateTestCounter(this, totalCount, passCount, failCount);
                    thrd.Abort();
                }
            }
        }

        public void stopTest(object sender)
        {
            threadTimer?.Dispose();
            thrd.Abort();
            evnt_updateBtnStartBG?.Invoke(this, Color.LightGray);
            evnt_updateBtnStartTxt?.Invoke(this, "开始");
        }

        public void updateTips(string tips, int tim, Color bc)
        {
            Thread thread1 = new Thread(delegate () {
                evnt_updateTestTips(this, tips, bc);
                Thread.Sleep(tim);
                evnt_updateTestTips(this, "", Color.LightGray);
            });
            thread1.Start();
        }


        /// <summary>
        /// 响应串口接收到数据事件
        /// </summary>
        private void onRcvd_dmmPort(object sender, EventArgs e)
        {
            bytesEventArgs x = (bytesEventArgs)e;
            log(Encoding.ASCII.GetString(x.byteArry), Logger.LogType.Board);
        }

        /// <summary>
        /// 响应测试界面[开始]按键的单击事件
        /// </summary>
        public void onClick_btnStart(object sender, EventArgs e) {
            Console.WriteLine("onEvent_btnStart");
            if ((thrd == null) || (thrd.IsAlive == false))
            {
                StartTask(this);
                updateTips("请装载...", 1000, Color.LightGray);
            }
            else
            {
                stopTest(this);
                updateTips("请取走...", 1000, Color.Yellow);
            }
        }

        void autoStart()
        {
            thrdAutoStart = new Thread(new ParameterizedThreadStart(taskAutoStart));
            thrdAutoStart.IsBackground = true;
            thrdAutoStart.Start();
        }
        void taskAutoStart(object arg)
        {
            bool needCleanUp = true;
            while (true)
            {
                if ((thrd != null) && thrd.IsAlive == false)
                {
                    int mv = 0;
                    if (needCleanUp == false)
                    {
                        updateTips("请装载...", 500, Color.LightGray);
                        if (env.mx373ControlBoard.measureVolt(0, ref mv)) {
                            if(mv > 1000)
                            {
                                onClick_btnStart(this, null);
                                needCleanUp = true;
                            }
                        }
                    }
                    else
                    {
                        updateTips("请取走...", 500, Color.Yellow);
                        if (env.mx373ControlBoard.measureVolt(0, ref mv))
                        {
                            if (mv < 100)
                            {
                                needCleanUp = false;
                            }
                        }
                    }

                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 界面实例
        /// </summary>
        UpdateFrm mFrm;


    }
    

}
