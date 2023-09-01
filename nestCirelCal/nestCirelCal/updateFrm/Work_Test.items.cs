using System;
using System.Collections.Generic;
using System.Threading;
using nestCirelCal.Common;
using nestCirelCal.Log;
using System.Text;
using System.Drawing;
using nestCirelCal.work_environment;
using nestCirelCal.Log;

namespace nestCirelCal
{
    partial class Work_Test
    {
        public delegate bool TestItem(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent);

        bool testItem_0(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("等待启动指令" != row.name)
            {
                return false;
            }
            Logger.Instance.WriteLog(String.Format("<{0}>",row.name), Logger.LogType.Board, "this is a log");

            // initial 
            rslt = "undefined";
            judgeMent = "fail";

            // wait for start
            Int32 volt_mv = 0;
            for (int i=0;i<100;i++)
            {
                // measureVolt is a blocking thread
                if (env.mx373ControlBoard.measureVolt(0, ref volt_mv))
                {
                    rslt = volt_mv.ToString();
                    if (volt_mv > 500)
                    {
                        judgeMent = "pass";
                        Logger.Instance.WriteLog(String.Format("<{0} judgeMent:{1}>", row.name, judgeMent), Logger.LogType.Board, "this is a log");
                        return true;
                    }
                }
                Thread.Sleep(100);
            }
            Logger.Instance.WriteLog(String.Format("<{0} judgeMent:{1}>", row.name, judgeMent), Logger.LogType.Board, "this is a log");
            return false;
        }
        bool testItem_1(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if("测量Buck电压" != row.name)
            {
                return false;
            }
            updateTips("测量Buck电压", 500, Color.LightGray);
            Thread.Sleep(1000);
            // initial 
            rslt = "undefined";
            judgeMent = "fail";

            // wait for start
            Int32 volt_mv = 0;
            for (int i = 0; i < 3; i++)
            {
                // measureVolt is a blocking thread
                if (env.mx373ControlBoard.measureVolt(0, ref volt_mv))
                {
                    float volt = volt_mv;
                    volt /= 1000;
                    rslt = volt.ToString();
                    if ((volt >= row.lower) && (volt<=row.upper))
                    {
                        judgeMent = "pass";
                        return true;
                    }
                }
                Thread.Sleep(100);
            }
            return false;
        }

        bool testItem_2(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("测量关机电流" != row.name)
            {
                return false;
            }
            // initial 
            rslt = "undefined";
            judgeMent = "fail";

            // wait for start
            double ua = 0;
            bool success = true;
            for (int i = 0; i < 50; i++)
            {
                Int32 uA = 0;
                success &= env.mx373ControlBoard.battInfo(ref uA);
                ua += uA;
                Thread.Sleep(100);
            }
            ua = Math.Round(ua / 50);
            rslt = ua.ToString();
            if (success && (ua <= row.upper))
            {
                judgeMent = "pass";
                return true;
            }

            return false;
        }

        bool testItem_3(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("等待开机指令" != row.name)
            {
                return false;
            }
            
            // initial 
            rslt = "undefined";
            judgeMent = "fail";
            // wait for start
            Int32 ua = 0;
            for (int i = 0; i < 15; i++)
            {
                updateTips("快速触摸两次感应片", 500, Color.Yellow);
                // measureVolt is a blocking thread
                if (env.mx373ControlBoard.battInfo(ref ua))
                {
                    rslt = (ua/1000).ToString();
                    if (ua >= row.lower * 1000)
                    {
                        updateTips("", 10, Color.LightGray);
                        judgeMent = "pass";
                        return true;
                    }
                }
                Thread.Sleep(1000);
            }
            judgeMent = "fail";
            return true;
        }

        bool testItem_4(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("测量工作电流" != row.name)
            {
                return false;
            }
            Thread.Sleep(1000);
            // initial 
            rslt = "undefined";
            judgeMent = "fail";

            // wait for start
            double ua = 0;
            bool success = true;
            for (int i = 0; i < 20; i++)
            {
                Int32 uA = 0;
                success &= env.mx373ControlBoard.battInfo(ref uA);
                ua += uA;
                Thread.Sleep(100);
            }
            ua = Math.Round(ua / (20 * 1000));
            rslt = ua.ToString();
            if (success && (ua <= row.upper))
            {
                judgeMent = "pass";
                return true;
            }
            return false;
        }

        bool testItem_5(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("gSensor功能检测" != row.name)
            {
                return false;
            }
            // initial 
            rslt = "undefined";
            judgeMent = "fail";
            
            // wait for start
            for (int i = 0; i < 15; i++)
            {
                updateTips("[操作员]产生震动以触发gSensor", 500, Color.Yellow);
                // measureVolt is a blocking thread
                int verify = 0;
                if (env.dut.gSensorCheck_willDispose(ref verify))
                {
                    rslt = verify.ToString();
                    if ((verify >= row.lower) && (verify <= row.upper))
                    {
                        updateTips("", 10, Color.Gray);
                        judgeMent = "pass";
                        return true;
                    }
                }
                Thread.Sleep(1000);
            }
            return false;
        }

        bool testItem_6(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("测量TX1打码电压" != row.name)
            {
                return false;
            }
            // initial 
            rslt = "undefined";
            judgeMent = "fail";

            // wait for start
            Int32 volt_mv = 0;
            for (int i = 0; i < 3; i++)
            {
                // measureVolt is a blocking thread
                if (env.mx373ControlBoard.measureVolt(6, ref volt_mv))
                {
                    float volt = volt_mv;
                    volt /= 1000;
                    rslt = volt.ToString();
                    if ((volt >= row.lower) && (volt <= row.upper))
                    {
                        judgeMent = "pass";
                        return true;
                    }
                }
                Thread.Sleep(100);
            }
            return false;
        }

        bool testItem_7(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("测量TX2打码电压" != row.name)
            {
                return false;
            }
            // initial 
            rslt = "undefined";
            judgeMent = "fail";

            // wait for start
            Int32 volt_mv = 0;
            for (int i = 0; i < 3; i++)
            {
                // measureVolt is a blocking thread
                if (env.mx373ControlBoard.measureVolt(5, ref volt_mv))
                {
                    float volt = volt_mv;
                    volt /= 1000;
                    rslt = volt.ToString();
                    if ((volt >= row.lower) && (volt <= row.upper))
                    {
                        judgeMent = "pass";
                        return true;
                    }
                }
                Thread.Sleep(100);
            }
            return false;
        }


        bool testItem_8(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_9(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_10(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_11(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_12(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_13(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_14(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_15(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_16(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_17(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_18(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_19(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_20(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_21(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_22(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_23(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_24(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("未定义" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

    }
    

}
