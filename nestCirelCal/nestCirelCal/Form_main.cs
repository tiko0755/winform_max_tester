﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nestCirelCal.Common;


namespace nestCirelCal
{
    public partial class Form_main : Form
    {
        UpdateFrm mainFrm;
        public Form_main()
        {
            InitializeComponent();
            this.MaximizeBox = false;   // 禁止最大化
            mainFrm = new UpdateFrm(this, WinFrmControl());
        }

        private Control[] WinFrmControl()
        {
            Control[] ctrl = new Control[200];
            int i = 0;
            ctrl[i++] = tBx_total;
            ctrl[i++] = tBx_fail;
            ctrl[i++] = label3;
            ctrl[i++] = dtGrdVw_test;
            ctrl[i++] = tbCtrl_main;
            ctrl[i++] = tbPg_test;
            ctrl[i++] = tBx_time;
            ctrl[i++] = label6;
            ctrl[i++] = label5;
            ctrl[i++] = tBx_pass;
            ctrl[i++] = label4;
            ctrl[i++] = tBx_tips;
            ctrl[i++] = btn_start;
            ctrl[i++] = tabPage1;
            ctrl[i++] = tbPg_eng;
            ctrl[i++] = btn_moveRight;
            ctrl[i++] = chckBx_loop;
            ctrl[i++] = btn_moveLeft;
            ctrl[i++] = groupBox2;
            ctrl[i++] = lbl_lstSqu;
            ctrl[i++] = lbl_lstDelay;
            ctrl[i++] = lbl_lstSend;
            ctrl[i++] = lbl_lstCmd;
            ctrl[i++] = lbl_lstHex;
            ctrl[i++] = txtBx_delay0;
            ctrl[i++] = btn_squ0;
            ctrl[i++] = btn_send0;
            ctrl[i++] = txtBx_line0;
            ctrl[i++] = chckBx_hex0;
            ctrl[i++] = btn_squRec;
            ctrl[i++] = panel1;
            ctrl[i++] = chckBx_timeStamp;
            ctrl[i++] = label7;
            ctrl[i++] = tBx_interval;
            ctrl[i++] = chckBx_interval;
            ctrl[i++] = chckBx_sendHex;
            ctrl[i++] = chckBx_recvHex;
            ctrl[i++] = cbBx_cmd;
            ctrl[i++] = btn_send;
            ctrl[i++] = chckBx_newLine;
            ctrl[i++] = groupBox1;
            ctrl[i++] = lbl_baud;
            ctrl[i++] = cbBx_baud;
            ctrl[i++] = lbl_serialName;
            ctrl[i++] = cbBx_serial;
            ctrl[i++] = rchTxtBx_log;
            ctrl[i++] = txtBx_delay19;
            ctrl[i++] = btn_squ19;
            ctrl[i++] = btn_send19;
            ctrl[i++] = txtBx_line19;
            ctrl[i++] = chckBx_hex19;
            ctrl[i++] = txtBx_delay18;
            ctrl[i++] = btn_squ18;
            ctrl[i++] = btn_send18;
            ctrl[i++] = txtBx_line18;
            ctrl[i++] = chckBx_hex18;
            ctrl[i++] = txtBx_delay17;
            ctrl[i++] = btn_squ17;
            ctrl[i++] = btn_send17;
            ctrl[i++] = txtBx_line17;
            ctrl[i++] = chckBx_hex17;
            ctrl[i++] = txtBx_delay16;
            ctrl[i++] = btn_squ16;
            ctrl[i++] = btn_send16;
            ctrl[i++] = txtBx_line16;
            ctrl[i++] = chckBx_hex16;
            ctrl[i++] = txtBx_delay15;
            ctrl[i++] = btn_squ15;
            ctrl[i++] = btn_send15;
            ctrl[i++] = txtBx_line15;
            ctrl[i++] = chckBx_hex15;
            ctrl[i++] = txtBx_delay14;
            ctrl[i++] = btn_squ14;
            ctrl[i++] = btn_send14;
            ctrl[i++] = txtBx_line14;
            ctrl[i++] = chckBx_hex14;
            ctrl[i++] = txtBx_delay13;
            ctrl[i++] = btn_squ13;
            ctrl[i++] = btn_send13;
            ctrl[i++] = txtBx_line13;
            ctrl[i++] = chckBx_hex13;
            ctrl[i++] = txtBx_delay12;
            ctrl[i++] = btn_squ12;
            ctrl[i++] = btn_send12;
            ctrl[i++] = txtBx_line12;
            ctrl[i++] = chckBx_hex12;
            ctrl[i++] = txtBx_delay11;
            ctrl[i++] = btn_squ11;
            ctrl[i++] = btn_send11;
            ctrl[i++] = txtBx_line11;
            ctrl[i++] = chckBx_hex11;
            ctrl[i++] = txtBx_delay10;
            ctrl[i++] = btn_squ10;
            ctrl[i++] = btn_send10;
            ctrl[i++] = txtBx_line10;
            ctrl[i++] = chckBx_hex10;
            ctrl[i++] = txtBx_delay9;
            ctrl[i++] = btn_squ9;
            ctrl[i++] = btn_send9;
            ctrl[i++] = txtBx_line9;
            ctrl[i++] = chckBx_hex9;
            ctrl[i++] = txtBx_delay8;
            ctrl[i++] = btn_squ8;
            ctrl[i++] = btn_send8;
            ctrl[i++] = txtBx_line8;
            ctrl[i++] = chckBx_hex8;
            ctrl[i++] = txtBx_delay7;
            ctrl[i++] = btn_squ7;
            ctrl[i++] = btn_send7;
            ctrl[i++] = txtBx_line7;
            ctrl[i++] = chckBx_hex7;
            ctrl[i++] = txtBx_delay6;
            ctrl[i++] = btn_squ6;
            ctrl[i++] = btn_send6;
            ctrl[i++] = txtBx_line6;
            ctrl[i++] = chckBx_hex6;
            ctrl[i++] = txtBx_delay5;
            ctrl[i++] = btn_squ5;
            ctrl[i++] = btn_send5;
            ctrl[i++] = txtBx_line5;
            ctrl[i++] = chckBx_hex5;
            ctrl[i++] = txtBx_delay4;
            ctrl[i++] = btn_squ4;
            ctrl[i++] = btn_send4;
            ctrl[i++] = txtBx_line4;
            ctrl[i++] = chckBx_hex4;
            ctrl[i++] = txtBx_delay3;
            ctrl[i++] = btn_squ3;
            ctrl[i++] = btn_send3;
            ctrl[i++] = txtBx_line3;
            ctrl[i++] = chckBx_hex3;
            ctrl[i++] = txtBx_delay2;
            ctrl[i++] = btn_squ2;
            ctrl[i++] = btn_send2;
            ctrl[i++] = txtBx_line2;
            ctrl[i++] = chckBx_hex2;
            ctrl[i++] = txtBx_delay1;
            ctrl[i++] = btn_squ1;
            ctrl[i++] = btn_send1;
            ctrl[i++] = txtBx_line1;
            ctrl[i++] = chckBx_hex1;
            ctrl[i++] = btn_load;
            ctrl[i++] = btn_save;
            ctrl[i++] = btn_serial_more;
            ctrl[i++] = tabPage2;
            ctrl[i++] = btn_serialConnect;
            ctrl[i++] = lbl_baud;
            ctrl[i++] = lbl_serialName;

            return ctrl;
        }

        private void dtGrdVw_test_SelectionChanged(object sender, EventArgs e)
        {
            this.dtGrdVw_test.ClearSelection();//清空表格控件选择项
        }
    }
}