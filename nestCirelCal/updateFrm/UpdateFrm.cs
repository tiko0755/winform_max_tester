using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using nestCirelCal.Common;
using nestCirelCal.work_environment;

namespace nestCirelCal
{
    partial class UpdateFrm
    {

        public UpdateFrm(Form frmWindow, Control[] ctrl)
        {
            string text = System.IO.File.ReadAllText(".\\user\\config.json");
            Console.WriteLine(text);
            configJson = JsonConvert.DeserializeObject<ConfigJson>(text);

            Console.WriteLine("working dir: " + System.Environment.CurrentDirectory);
            root = System.Environment.CurrentDirectory;

            this.frmMain = frmWindow;
            this.ctrl = ctrl;
            
            work_env = new Work_Environment(configJson);    // 实例工作环境
            work_test = new Work_Test(work_env);    // 实例测试模块
            work_eng = new Work_Eng(work_env);      // 实例工程调试模块

            frmMain.Load += new System.EventHandler(this.FrmMain_Load);
            frmMain.FormClosing += new FormClosingEventHandler(FrmMain_FormClosing);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitUI_test(frmMain, ctrl, ref work_test);     // initial TEST user interface form
            InitUI_eng(frmMain, ctrl, ref work_eng);      // initial TEST user interface form
        }

        private void FrmMain_FormClosing(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 从配置文件读回的配置
        /// </summary>
        public ConfigJson configJson { get; set; }

        Form frmMain;
        Control[] ctrl;
        Work_Environment work_env;
        Work_Test work_test;
        Work_Eng work_eng;
        string root;

    }

}
