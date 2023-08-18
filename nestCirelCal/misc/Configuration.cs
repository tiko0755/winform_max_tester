using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using nestCirelCal.Log;

namespace nestCirelCal.Common
{
    /// <summary>
    /// 配置文件类定义
    /// </summary>
    public class ConfigJson
    {
        //public ConfigJson()
        //{
        //    usrInfo = new ConfigJsonUsrInfo();
        //}
        public ConfigJsonSerialPort dmm_config { get; set; }
        public ConfigJsonSerialPort dut_config { get; set; }
        public ConfigJsonTest test { get; set; }
        public string usr_root { set; get; }
    }

    /// <summary>
    /// 配置文件的串口配置
    /// </summary>
    public class ConfigJsonSerialPort
    {
        public string com { get; set; }
        public decimal baud { get; set; }
        public decimal bits { get; set; }
        public decimal stop { get; set; }
        public decimal verify { get; set; }
    }
    /// <summary>
    /// 配置文件的测试相关配置
    /// </summary>
    public class ConfigJsonTest
    {
        public ConfigJsonTest() {
            table_col = new List<ConfigJsonTest_col>();
            table_row = new List<ConfigJsonTest_row>();
        }
        public string user_root { get; set; }
        public ConfigJsonTest_counter counter { get; set; }
        public List<ConfigJsonTest_col> table_col { get; set; }
        public List<ConfigJsonTest_row> table_row { get; set; }
    }

    /// <summary>
    /// 配置文件的测试相关配置,计数器类
    /// </summary>
    public class ConfigJsonTest_counter
    {
        /// <summary>
        /// 测试总计数
        /// </summary>
        public UInt16 total { get; set; }
        /// <summary>
        /// 测试失败计数
        /// </summary>
        public UInt16 failure { get; set; }
        public UInt16 success { get; set; }

    }
    /// <summary>
    /// 配置文件的测试相关配置,测试表格的列定义
    /// </summary>
    //public class ConfigJsonTest_col
    //{
    //    public ConfigJsonTest_col_element[] table_col { set; get; }
    //}

    public class ConfigJsonTest_col
    {
        public string name { get; set; }
        public UInt16 width { get; set; }
    }
    /// <summary>
    /// 配置文件的测试相关配置,测试表格的行(测项)定义
    /// </summary>
    //public class ConfigJsonTest_row
    //{
    //    public ConfigJsonTest_row_element[] table_row { get; set; }
    //}
    public class ConfigJsonTest_row
    {
        public string name { get; set; }
        public string description { get; set; }

        public float lower { get; set; }
        public float upper { get; set; }
        public string unit { get; set; }
    }
}
