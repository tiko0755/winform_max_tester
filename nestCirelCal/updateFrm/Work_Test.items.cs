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
    partial class Work_Test
    {
        public delegate bool TestItem(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent);

        bool testItem_0(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("连接蓝牙" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }
        bool testItem_1(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if("读取SN" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_2(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("PCBA自检" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_3(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("RSSI判断" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_4(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("笔尖压力测试" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_5(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
        {
            if ("休眠电流" != row.name)
            {
                return false;
            }
            rslt = "3.14";
            judgeMent = "pass";
            Thread.Sleep(500);
            return true;
        }

        bool testItem_6(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
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

        bool testItem_7(ConfigJsonTest_row row, int slot, ref string rslt, ref string judgeMent)
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
