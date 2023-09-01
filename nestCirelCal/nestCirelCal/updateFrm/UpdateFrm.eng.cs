using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nestCirelCal.Common;
using nestCirelCal.Log;
using Newtonsoft.Json;
using nestCirelCal.commentDiag;
using System.Drawing;
using System.Threading;

namespace nestCirelCal
{
    partial class UpdateFrm
    {

        List<Eng_cmdCombine> cmdLst;
        Eng_cmdCombine lasCmdCombinetFocus;
        List<Eng_cmdCombineData> cmbDataLst;
        string[] doubleClickNameArry;
        string[] clickNameArry;

        Form_commentDialog commentDiag;

        public event StrEventHandler evntSendCommand;
        public event StrEventHandler evntSerialConnectDisconnect;

        public int InitUI_eng(Form frmWindow, Control[] control, ref Work_Eng workObj)
        {
            clickNameArry = new string[8];
            doubleClickNameArry = new string[8];

            int count = 0;
            foreach (Control x in control)
            {
                if (x == null) break;
                bool got = false;
                switch (x.Name)
                {
                    case "rchTxtBx_log": richTxtBoxLog = (RichTextBox)x; got = true; break;
                    case "chckBx_sendHex": chckBxHexSend = (CheckBox)x; got = true; break;
                    case "chckBx_recvHex": chckBxHexReceive = (CheckBox)x; got = true; break;
                    case "chckBx_interval": chckBxInterval = (CheckBox)x; got = true; break;
                    case "tBx_interval": tBxInterval = (TextBox)x; got = true; break;
                    case "chckBx_newLine": chckBxNewLine = (CheckBox)x; got = true; break;
                    case "cbBx_cmd": cbBxCmd = (ComboBox)x; got = true; break;
                    case "btn_send": btnSend = (Button)x; got = true; break;

                    case "cbBx_serial": cbBx_serial = (ComboBox)x; got = true; break; 
                    case "cbBx_baud": cbBx_baud = (ComboBox)x; got = true; break; 
                    case "tbPg_eng": tbPg_eng = (TabPage)x; got = true; break; 
                    case "chckBx_loop": chckBx_loop = (CheckBox)x; got = true; break; 
                    case "btn_moveLeft": btn_moveLeft = (Button)x; got = true; break; 
                    case "btn_moveRight": btn_moveRight = (Button)x; got = true; break; 
                    case "btn_squRec": btn_squRec = (Button)x; got = true; break; 
                    case "btn_load": btn_load = (Button)x; got = true; break; 
                    case "btn_save": btn_save = (Button)x; got = true; break; 
                    case "btn_serial_more": btn_serialMore = (Button)x; got = true; break;
                    case "btn_serialConnect": btn_serialConnect = (Button)x; got = true; break;

                    case "chckBx_hex0": chckBx_hex0 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex1": chckBx_hex1 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex2": chckBx_hex2 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex3": chckBx_hex3 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex4": chckBx_hex4 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex5": chckBx_hex5 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex6": chckBx_hex6 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex7": chckBx_hex7 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex8": chckBx_hex8 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex9": chckBx_hex9 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex10": chckBx_hex10 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex11": chckBx_hex11 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex12": chckBx_hex12 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex13": chckBx_hex13 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex14": chckBx_hex14 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex15": chckBx_hex15 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex16": chckBx_hex16 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex17": chckBx_hex17 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex18": chckBx_hex18 = (CheckBox)x; got = true; break; 
                    case "chckBx_hex19": chckBx_hex19 = (CheckBox)x; got = true; break; 

                    case "txtBx_line0": txtBx_line0 = (TextBox)x; got = true; break; 
                    case "txtBx_line1": txtBx_line1 = (TextBox)x; got = true; break; 
                    case "txtBx_line2": txtBx_line2 = (TextBox)x; got = true; break; 
                    case "txtBx_line3": txtBx_line3 = (TextBox)x; got = true; break; 
                    case "txtBx_line4": txtBx_line4 = (TextBox)x; got = true; break; 
                    case "txtBx_line5": txtBx_line5 = (TextBox)x; got = true; break; 
                    case "txtBx_line6": txtBx_line6 = (TextBox)x; got = true; break; 
                    case "txtBx_line7": txtBx_line7 = (TextBox)x; got = true; break; 
                    case "txtBx_line8": txtBx_line8 = (TextBox)x; got = true; break; 
                    case "txtBx_line9": txtBx_line9 = (TextBox)x; got = true; break; 
                    case "txtBx_line10": txtBx_line10 = (TextBox)x; got = true; break; 
                    case "txtBx_line11": txtBx_line11 = (TextBox)x; got = true; break; 
                    case "txtBx_line12": txtBx_line12 = (TextBox)x; got = true; break; 
                    case "txtBx_line13": txtBx_line13 = (TextBox)x; got = true; break; 
                    case "txtBx_line14": txtBx_line14 = (TextBox)x; got = true; break; 
                    case "txtBx_line15": txtBx_line15 = (TextBox)x; got = true; break; 
                    case "txtBx_line16": txtBx_line16 = (TextBox)x; got = true; break; 
                    case "txtBx_line17": txtBx_line17 = (TextBox)x; got = true; break; 
                    case "txtBx_line18": txtBx_line18 = (TextBox)x; got = true; break; 
                    case "txtBx_line19": txtBx_line19 = (TextBox)x; got = true; break; 

                    case "btn_send0": btn_send0 = (Button)x; got = true; break; 
                    case "btn_send1": btn_send1 = (Button)x; got = true; break; 
                    case "btn_send2": btn_send2 = (Button)x; got = true; break; 
                    case "btn_send3": btn_send3 = (Button)x; got = true; break; 
                    case "btn_send4": btn_send4 = (Button)x; got = true; break; 
                    case "btn_send5": btn_send5 = (Button)x; got = true; break; 
                    case "btn_send6": btn_send6 = (Button)x; got = true; break; 
                    case "btn_send7": btn_send7 = (Button)x; got = true; break; 
                    case "btn_send8": btn_send8 = (Button)x; got = true; break; 
                    case "btn_send9": btn_send9 = (Button)x; got = true; break; 
                    case "btn_send10": btn_send10 = (Button)x; got = true; break; 
                    case "btn_send11": btn_send11 = (Button)x; got = true; break; 
                    case "btn_send12": btn_send12 = (Button)x; got = true; break; 
                    case "btn_send13": btn_send13 = (Button)x; got = true; break; 
                    case "btn_send14": btn_send14 = (Button)x; got = true; break; 
                    case "btn_send15": btn_send15 = (Button)x; got = true; break; 
                    case "btn_send16": btn_send16 = (Button)x; got = true; break; 
                    case "btn_send17": btn_send17 = (Button)x; got = true; break; 
                    case "btn_send18": btn_send18 = (Button)x; got = true; break; 
                    case "btn_send19": btn_send19 = (Button)x; got = true; break; 

                    case "btn_squ0": btn_squ0 = (Button)x; got = true; break; 
                    case "btn_squ1": btn_squ1 = (Button)x; got = true; break; 
                    case "btn_squ2": btn_squ2 = (Button)x; got = true; break; 
                    case "btn_squ3": btn_squ3 = (Button)x; got = true; break;
                    case "btn_squ4": btn_squ4 = (Button)x; got = true; break;
                    case "btn_squ5": btn_squ5 = (Button)x; got = true; break;
                    case "btn_squ6": btn_squ6 = (Button)x; got = true; break; 
                    case "btn_squ7": btn_squ7 = (Button)x; got = true; break; 
                    case "btn_squ8": btn_squ8 = (Button)x; got = true; break; 
                    case "btn_squ9": btn_squ9 = (Button)x; got = true; break; 
                    case "btn_squ10": btn_squ10 = (Button)x; got = true; break; 
                    case "btn_squ11": btn_squ11 = (Button)x; got = true; break; 
                    case "btn_squ12": btn_squ12 = (Button)x; got = true; break; 
                    case "btn_squ13": btn_squ13 = (Button)x; got = true; break; 
                    case "btn_squ14": btn_squ14 = (Button)x; got = true; break; 
                    case "btn_squ15": btn_squ15 = (Button)x; got = true; break; 
                    case "btn_squ16": btn_squ16 = (Button)x; got = true; break; 
                    case "btn_squ17": btn_squ17 = (Button)x; got = true; break; 
                    case "btn_squ18": btn_squ18 = (Button)x; got = true; break; 
                    case "btn_squ19": btn_squ19 = (Button)x; got = true; break; 

                    case "txtBx_delay0": txtBx_delay0 = (TextBox)x; got = true; break; 
                    case "txtBx_delay1": txtBx_delay1 = (TextBox)x; got = true; break; 
                    case "txtBx_delay2": txtBx_delay2 = (TextBox)x; got = true; break; 
                    case "txtBx_delay3": txtBx_delay3 = (TextBox)x; got = true; break; 
                    case "txtBx_delay4": txtBx_delay4 = (TextBox)x; got = true; break; 
                    case "txtBx_delay5": txtBx_delay5 = (TextBox)x; got = true; break; 
                    case "txtBx_delay6": txtBx_delay6 = (TextBox)x; got = true; break; 
                    case "txtBx_delay7": txtBx_delay7 = (TextBox)x; got = true; break; 
                    case "txtBx_delay8": txtBx_delay8 = (TextBox)x; got = true; break; 
                    case "txtBx_delay9": txtBx_delay9 = (TextBox)x; got = true; break; 
                    case "txtBx_delay10": txtBx_delay10 = (TextBox)x; got = true; break; 
                    case "txtBx_delay11": txtBx_delay11 = (TextBox)x; got = true; break; 
                    case "txtBx_delay12": txtBx_delay12 = (TextBox)x; got = true; break; 
                    case "txtBx_delay13": txtBx_delay13 = (TextBox)x; got = true; break; 
                    case "txtBx_delay14": txtBx_delay14 = (TextBox)x; got = true; break; 
                    case "txtBx_delay15": txtBx_delay15 = (TextBox)x; got = true; break; 
                    case "txtBx_delay16": txtBx_delay16 = (TextBox)x; got = true; break; 
                    case "txtBx_delay17": txtBx_delay17 = (TextBox)x; got = true; break; 
                    case "txtBx_delay18": txtBx_delay18 = (TextBox)x; got = true; break; 
                    case "txtBx_delay19": txtBx_delay19 = (TextBox)x; got = true; break;

                    case "lbl_lstDelay": lbl_lstDelay = (Label)x; got = true; break;
                    case "lbl_lstSqu":  lbl_lstSqu = (Label)x; got = true; break;
                    case "lbl_lstSend": lbl_lstSend = (Label)x; got = true; break;
                    case "lbl_lstCmd":  lbl_lstCmd = (Label)x; got = true; break;
                    case "lbl_lstHex":  lbl_lstHex = (Label)x; got = true; break;
                    case "lbl_baud":    lbl_baud = (Label)x; got = true; break;
                    case "lbl_serialName":   lbl_serialname = (Label)x; got = true; break;  
                }
                if (got)
                {
                    x.Click += any_Click;
                    x.DoubleClick += any_DoubleClick;
                }
            }

            // 读回配置
            string text = System.IO.File.ReadAllText(".\\conf\\eng.json");
            Console.WriteLine(text);
            cmbDataLst = JsonConvert.DeserializeObject<List<Eng_cmdCombineData>>(text);
            int dIndx = 0;

            cmdLst = new List<Eng_cmdCombine>();
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex0, txtBx_line0, btn_send0, btn_squ0, txtBx_delay0, cmbDataLst[dIndx++])); 
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex1, txtBx_line1, btn_send1, btn_squ1, txtBx_delay1, cmbDataLst[dIndx++])); 
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex2, txtBx_line2, btn_send2, btn_squ2, txtBx_delay2, cmbDataLst[dIndx++])); 
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex3, txtBx_line3, btn_send3, btn_squ3, txtBx_delay3, cmbDataLst[dIndx++])); 
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex4, txtBx_line4, btn_send4, btn_squ4, txtBx_delay4, cmbDataLst[dIndx++])); 
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex5, txtBx_line5, btn_send5, btn_squ5, txtBx_delay5, cmbDataLst[dIndx++])); 
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex6, txtBx_line6, btn_send6, btn_squ6, txtBx_delay6, cmbDataLst[dIndx++])); 
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex7, txtBx_line7, btn_send7, btn_squ7, txtBx_delay7, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex8, txtBx_line8, btn_send8, btn_squ8, txtBx_delay8, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex9, txtBx_line9, btn_send9, btn_squ9, txtBx_delay9, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex10, txtBx_line10, btn_send10, btn_squ10, txtBx_delay10, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex11, txtBx_line11, btn_send11, btn_squ11, txtBx_delay11, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex12, txtBx_line12, btn_send12, btn_squ12, txtBx_delay12, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex13, txtBx_line13, btn_send13, btn_squ13, txtBx_delay13, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex14, txtBx_line14, btn_send14, btn_squ14, txtBx_delay14, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex15, txtBx_line15, btn_send15, btn_squ15, txtBx_delay15, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex16, txtBx_line16, btn_send16, btn_squ16, txtBx_delay16, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex17, txtBx_line17, btn_send17, btn_squ17, txtBx_delay17, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex18, txtBx_line18, btn_send18, btn_squ18, txtBx_delay18, cmbDataLst[dIndx++]));
            cmdLst.Add(new Eng_cmdCombine(chckBx_hex19, txtBx_line19, btn_send19, btn_squ19, txtBx_delay19, cmbDataLst[dIndx++]));

            // 注册事件
            lbl_lstHex.DoubleClick += lbl_lstHex_DoubleClick; 
            lbl_lstSqu.DoubleClick += lbl_lstSqu_DoubleClick;
            lbl_lstDelay.DoubleClick += lbl_lstDelay_DoubleClick;
            lbl_serialname.DoubleClick += lbl_serialName_DoubleClick;
            lbl_baud.DoubleClick += lbl_buad_DoubleClick;

            richTxtBoxLog.DoubleClick += onDClick_richTxtBoxLog;

            workObj.evnt_updateUI_log += LogNewLine;
            evntSendCommand += workObj.onSend;

            workObj.evnt_connected += onConnected;
            workObj.evnt_disconnected += onDisconnected;

            btn_serialConnect.Click += clickConnectDisconnect;
            evntSerialConnectDisconnect += workObj.onConnectDisconnect;

            btn_moveLeft.Click += Btn_moveLeft_Click;
            btn_moveRight.Click += Btn_moveRight_Click;
            btn_save.Click += Btn_save_Click;
            btn_load.Click += Btn_load_Click;
            btn_squRec.Click += Btn_squRec_Click;
            
            return count;
        }
        System.Threading.Timer recTimer;
        int recSqu = 0;
        private void Btn_squRec_Click(object sender, EventArgs e)
        {
            if(recTimer == null)
            {
                recSqu = 1;
                cmdLst.ForEach((x)=> {
                    x.btn_squ.Text = "00";
                });
                recTimer = new System.Threading.Timer(new System.Threading.TimerCallback(delegate (object val) {
                    if (btn_squRec.BackColor == Color.Red)
                    {
                        btn_squRec.BackColor = SystemColors.Control;
                    }
                    else
                    {
                        btn_squRec.BackColor = Color.Red;
                    }
                }), null, 0, 500);  //最后两个参数依次为：多久后开始，隔多久执行一次。
            }
            else
            {
                stopRecTimer();
            }
        }

        private void stopRecTimer()
        {
            if (recTimer != null)
            {
                recTimer.Dispose();
                recTimer = null;
                btn_squRec.BackColor = SystemColors.Control;
                sequenceCmdLst();
            }
        }

        private void sequenceCmdLst()
        {
            sync_cmdLst();
            // seqence cmdList according to squ
            List<Eng_cmdCombineData> lst = new List<Eng_cmdCombineData>();
            int count = cmdLst.Count;
            while (true)
            {
                int min = 999999;
                Eng_cmdCombine xRec = null;
                cmdLst.ForEach((x) => {
                    if(x.data != null)
                    {
                        if((x.data.squ > 0)&& (x.data.squ <= min))
                        {
                            min = x.data.squ;
                            xRec = x;
                        }
                    }
                });
                if (xRec == null)
                {
                    break;
                }
                if (xRec != null)
                {
                    lst.Add(xRec.data);
                    xRec.data = null;
                }
            }
            // add the "00" ones
            cmdLst.ForEach((x)=> {
                if(x.data != null)
                {
                    lst.Add(x.data);
                }
            });
            cmbDataLst = lst;
            for (int i = 0; i < cmdLst.Count; i++)
            {
                cmdLst[i].data = cmbDataLst[i];
            }

            // redraw cmdList
            redrawCmdLst();
        }

        private void redrawCmdLst()
        {
            cmdLst.ForEach((x) => {
                x.txtBx_cmd.Text = x.data.cmd;
                x.btn_send.Text = x.data.comment;
                x.btn_squ.Text = String.Format("{0:d2}", x.data.squ);
                x.txtBx_delay.Text = String.Format("{0:d}", x.data.delay);
                x.txtBx_cmd.Width = x.data.width_cmd;
                x.btn_send.Width = x.data.width_comment;
                x.btn_send.Left = x.txtBx_cmd.Left + x.txtBx_cmd.Width + x.txtBx_cmd.Margin.Right+ x.btn_send.Margin.Left;
            });
        }

        private void Btn_load_Click(object sender, EventArgs e)
        {
            // redraw cmdList
        }

        private void CommentDiag_evntConfirm(object sender, string str)
        {
            Console.WriteLine("confirm: " + str);
            // save to last double click
            cmdLst.ForEach((x)=> {
                if(x.txtBx_cmd.Name == doubleClickNameArry[0])
                {
                    x.btn_send.Text = str;
                }   
            });
        }

        private void sync_cmdLst() {
            cmdLst.ForEach((x)=> {
                x.data.isHexChecked = x.chckBx_hex.Checked;
                x.data.cmd = x.txtBx_cmd.Text;
                x.data.comment = x.btn_send.Text;
                x.data.squ = Convert.ToInt32(x.btn_squ.Text);
                x.data.delay = Convert.ToInt32(x.txtBx_delay.Text);
                x.data.width_cmd = x.txtBx_cmd.Width;
                x.data.width_comment = x.btn_send.Width;
            });
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            sync_cmdLst();
            string contents = JsonConvert.SerializeObject(cmbDataLst);
            System.IO.File.WriteAllText("conf\\eng.json", contents);
        }

        private void Btn_moveRight_Click(object sender, EventArgs e)
        {
            if (lasCmdCombinetFocus != null)
            {
                lasCmdCombinetFocus.updateWidth(-10);
            }
        }

        private void Btn_moveLeft_Click(object sender, EventArgs e)
        {
            if (lasCmdCombinetFocus != null)
            {
                lasCmdCombinetFocus.updateWidth(10);
            }
        }

        void pushIntoStringArry(string[] arry, string incomming)
        {
            for (int i = arry.Length - 1; i > 0; i--)
            {
                arry[i] = arry[i - 1];
            }
            arry[0] = incomming;
        }
        private void any_Click(object sender, EventArgs e)
        {
            Control x = (Control)sender;
            pushIntoStringArry(clickNameArry, x.Name);

            // update last focus on cmd list, to adjust the width of between command line and send button. 
            if ((x.Name != btn_moveLeft.Name) && (x.Name != btn_moveRight.Name))
            {
                lasCmdCombinetFocus = null;
                for (int i = 0; i < cmdLst.Count; i++)
                {
                    if (cmdLst[i].txtBx_cmd.Name != x.Name)
                    {
                        continue;
                    }
                    lasCmdCombinetFocus = cmdLst[i];
                }
            }

            // publish "send" event
            if (evntSendCommand != null)
            {
                if (x.Name == btnSend.Name)
                {
                    string cmd = cbBxCmd.Text;
                    if (chckBxNewLine.Checked)
                    {
                        cmd += "\r\n";
                    }
                    evntSendCommand(this, cmd);
                }
                else
                {
                    for (int i = 0; i < cmdLst.Count; i++)
                    {
                        if (cmdLst[i].btn_send.Name != x.Name)
                        {
                            continue;
                        }
                        string cmd = cmdLst[i].txtBx_cmd.Text;
                        if (chckBxNewLine.Checked)
                        {
                            cmd += "\r\n";
                        }
                        evntSendCommand(this, cmd);
                    }
                }
            }

            // any unexpert click will also stop the rec timer
            if(recTimer != null)
            {
                bool ignoredClick = false;
                if ((lbl_lstSqu.Name == x.Name)|| (btn_squRec.Name == x.Name))
                {
                    ignoredClick = true;
                }
                for (int i = 0; (i < cmdLst.Count) && (ignoredClick == false); i++)
                {
                    if (cmdLst[i].btn_squ.Name == x.Name)
                    {
                        ignoredClick |= true;
                        if(cmdLst[i].btn_squ.Text == "00")
                        {
                            cmdLst[i].btn_squ.Text = String.Format("{0:d2}", recSqu);
                            recSqu++;
                        }
                    }
                }
                if(ignoredClick == false)
                {
                    stopRecTimer();
                }
            }


        }

        private void any_DoubleClick(object sender, EventArgs e)
        {
            Control x = (Control)sender;
            pushIntoStringArry(doubleClickNameArry, x.Name);

            for (int i = 0; i < cmdLst.Count; i++)
            {
                if (cmdLst[i].txtBx_cmd.Name != x.Name)
                {
                    continue;
                }
                commentDiag = new Form_commentDialog();
                commentDiag.Show();
                commentDiag.evntConfirm += CommentDiag_evntConfirm;
            }
        }

        /// <summary>
        /// connect or disconnect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickConnectDisconnect(object sender, EventArgs e)
        {
            evntSerialConnectDisconnect?.Invoke(this, cbBx_serial.Text);
        }
        

        /// <summary>
        /// To reset all the HEX checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_lstHex_DoubleClick(object sender, EventArgs e)
        {
            cmbDataLst.ForEach((rec)=> {
                rec.isHexChecked = false;
            });
            for (int i = 0; i < cmdLst.Count; i++) {
                cmdLst[i].chckBx_hex.Checked = false;
            }
        }
        /// <summary>
        /// To reset all the send sequence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_lstSqu_DoubleClick(object sender, EventArgs e)
        {
            cmbDataLst.ForEach((x) => {
                x.squ = 0;
            });
            for (int i = 0; i < cmdLst.Count; i++)
            {
                cmdLst[i].btn_squ.Text = "00";
            }
            recSqu = 1;
        }
        /// <summary>
        /// To reset all the command delay to 1000ms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_lstDelay_DoubleClick(object sender, EventArgs e)
        {
            cmbDataLst.ForEach((rec) => {
                rec.delay = 1000;
            });
            for (int i = 0; i < cmdLst.Count; i++)
            {
                cmdLst[i].txtBx_delay.Text = "1000";
            }
        }

        private void lbl_serialName_DoubleClick(object sender, EventArgs e)
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames(); //重新获取串口
            Console.WriteLine("found {0:d} ports", ports.Length);
            cbBx_serial.Items.Clear();
            for (int i = 0; i < ports.Length; i++)
            {
                cbBx_serial.Items.Add(ports[i]);
                Console.WriteLine("Found {0}", ports[i]);
            }
            
            cbBx_serial.Text = ports[0];
        }

        private void lbl_buad_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("to config more");
        }
        /// <summary>
        /// to add a new log line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogNewLine(Object sender, string e)
        {
            richTxtBoxLog.BeginInvoke(new EventHandler(delegate {
                richTxtBoxLog.AppendText(e);
                richTxtBoxLog.ScrollToCaret();
            }));
        }

        /// <summary>
        /// 双击clear事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onDClick_richTxtBoxLog(object sender, EventArgs e)
        {
            richTxtBoxLog.BeginInvoke(new EventHandler(delegate {
                richTxtBoxLog.Text = "";
            }));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onConnected(object sender, EventArgs e)
        {
            btn_serialConnect.BeginInvoke(new EventHandler(delegate {
                btn_serialConnect.Text = "关闭串口";
            }));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onDisconnected(object sender, EventArgs e)
        {
            btn_serialConnect.BeginInvoke(new EventHandler(delegate {
                btn_serialConnect.Text = "打开串口";
            }));
        }



        #region

        /// <summary>
        /// log框
        /// </summary>
        private RichTextBox richTxtBoxLog;

        /// <summary>
        /// 十六进制发送
        /// </summary>
        private CheckBox chckBxHexSend;

        /// <summary>
        /// 十六进制接收
        /// </summary>
        private CheckBox chckBxHexReceive;

        /// <summary>
        /// 周期循环发送
        /// </summary>
        private CheckBox chckBxInterval;

        /// <summary>
        /// 周期循环发送时间(ms)
        /// </summary>
        private TextBox tBxInterval;

        /// <summary>
        /// 发送新行
        /// </summary>
        private CheckBox chckBxNewLine;


        /// <summary>
        /// 命令列表
        /// </summary>
        private ComboBox cbBxCmd;

        /// <summary>
        /// 发送命令
        /// </summary>
        private Button btnSend;

        /// <summary>
        /// 串口号列表
        /// </summary>
        private System.Windows.Forms.ComboBox cbBx_serial;
        /// <summary>
        /// 串口波特率
        /// </summary>
        private System.Windows.Forms.ComboBox cbBx_baud;
        /// <summary>
        /// 指令调试页面
        /// </summary>
        private System.Windows.Forms.TabPage tbPg_eng;
        /// <summary>
        /// 循环发送
        /// </summary>
        private System.Windows.Forms.CheckBox chckBx_loop;
        /// <summary>
        /// 命令行缩短，注释行变长
        /// </summary>
        private System.Windows.Forms.Button btn_moveLeft;
        /// <summary>
        /// 命令行变长，注释行缩短
        /// </summary>
        private System.Windows.Forms.Button btn_moveRight;
        /// <summary>
        /// 开始录制发送顺序
        /// </summary>
        private System.Windows.Forms.Button btn_squRec;
        /// <summary>
        /// 加载
        /// </summary>
        private System.Windows.Forms.Button btn_load;
        /// <summary>
        /// 保存
        /// </summary>
        private System.Windows.Forms.Button btn_save;
        /// <summary>
        /// 更多串口设置
        /// </summary>
        private System.Windows.Forms.Button btn_serialMore;

        /// <summary>
        /// 串口连接/断开
        /// </summary>
        private System.Windows.Forms.Button btn_serialConnect;

        private System.Windows.Forms.Label lbl_lstDelay;
        private System.Windows.Forms.Label lbl_lstSqu;
        private System.Windows.Forms.Label lbl_lstSend;
        private System.Windows.Forms.Label lbl_lstCmd;
        private System.Windows.Forms.Label lbl_lstHex;
        private System.Windows.Forms.Label lbl_baud;
        private System.Windows.Forms.Label lbl_serialname;

        private System.Windows.Forms.CheckBox chckBx_hex0;
        private System.Windows.Forms.CheckBox chckBx_hex1;
        private System.Windows.Forms.CheckBox chckBx_hex2;
        private System.Windows.Forms.CheckBox chckBx_hex3;
        private System.Windows.Forms.CheckBox chckBx_hex4;
        private System.Windows.Forms.CheckBox chckBx_hex5;
        private System.Windows.Forms.CheckBox chckBx_hex6;
        private System.Windows.Forms.CheckBox chckBx_hex7;
        private System.Windows.Forms.CheckBox chckBx_hex8;
        private System.Windows.Forms.CheckBox chckBx_hex9;
        private System.Windows.Forms.CheckBox chckBx_hex10;
        private System.Windows.Forms.CheckBox chckBx_hex11;
        private System.Windows.Forms.CheckBox chckBx_hex12;
        private System.Windows.Forms.CheckBox chckBx_hex13;
        private System.Windows.Forms.CheckBox chckBx_hex14;
        private System.Windows.Forms.CheckBox chckBx_hex15;
        private System.Windows.Forms.CheckBox chckBx_hex16;
        private System.Windows.Forms.CheckBox chckBx_hex17;
        private System.Windows.Forms.CheckBox chckBx_hex18;
        private System.Windows.Forms.CheckBox chckBx_hex19;

        private System.Windows.Forms.TextBox txtBx_line0;
        private System.Windows.Forms.TextBox txtBx_line1;
        private System.Windows.Forms.TextBox txtBx_line2;
        private System.Windows.Forms.TextBox txtBx_line3;
        private System.Windows.Forms.TextBox txtBx_line4;
        private System.Windows.Forms.TextBox txtBx_line5;
        private System.Windows.Forms.TextBox txtBx_line6;
        private System.Windows.Forms.TextBox txtBx_line7;
        private System.Windows.Forms.TextBox txtBx_line8;
        private System.Windows.Forms.TextBox txtBx_line9;
        private System.Windows.Forms.TextBox txtBx_line10;
        private System.Windows.Forms.TextBox txtBx_line11;
        private System.Windows.Forms.TextBox txtBx_line12;
        private System.Windows.Forms.TextBox txtBx_line13;
        private System.Windows.Forms.TextBox txtBx_line14;
        private System.Windows.Forms.TextBox txtBx_line15;
        private System.Windows.Forms.TextBox txtBx_line16;
        private System.Windows.Forms.TextBox txtBx_line17;
        private System.Windows.Forms.TextBox txtBx_line18;
        private System.Windows.Forms.TextBox txtBx_line19;

        private System.Windows.Forms.Button btn_send0;
        private System.Windows.Forms.Button btn_send1;
        private System.Windows.Forms.Button btn_send2;
        private System.Windows.Forms.Button btn_send3;
        private System.Windows.Forms.Button btn_send4;
        private System.Windows.Forms.Button btn_send5;
        private System.Windows.Forms.Button btn_send6;
        private System.Windows.Forms.Button btn_send7;
        private System.Windows.Forms.Button btn_send8;
        private System.Windows.Forms.Button btn_send9;
        private System.Windows.Forms.Button btn_send10;
        private System.Windows.Forms.Button btn_send11;
        private System.Windows.Forms.Button btn_send12;
        private System.Windows.Forms.Button btn_send13;
        private System.Windows.Forms.Button btn_send14;
        private System.Windows.Forms.Button btn_send15;
        private System.Windows.Forms.Button btn_send16;
        private System.Windows.Forms.Button btn_send17;
        private System.Windows.Forms.Button btn_send18;
        private System.Windows.Forms.Button btn_send19;

        private System.Windows.Forms.Button btn_squ0;
        private System.Windows.Forms.Button btn_squ1;
        private System.Windows.Forms.Button btn_squ2;
        private System.Windows.Forms.Button btn_squ3;
        private System.Windows.Forms.Button btn_squ4;
        private System.Windows.Forms.Button btn_squ5;
        private System.Windows.Forms.Button btn_squ6;
        private System.Windows.Forms.Button btn_squ7;
        private System.Windows.Forms.Button btn_squ8;
        private System.Windows.Forms.Button btn_squ9;
        private System.Windows.Forms.Button btn_squ10;
        private System.Windows.Forms.Button btn_squ11;
        private System.Windows.Forms.Button btn_squ12;
        private System.Windows.Forms.Button btn_squ13;
        private System.Windows.Forms.Button btn_squ14;
        private System.Windows.Forms.Button btn_squ15;
        private System.Windows.Forms.Button btn_squ16;
        private System.Windows.Forms.Button btn_squ17;
        private System.Windows.Forms.Button btn_squ18;
        private System.Windows.Forms.Button btn_squ19;

        private System.Windows.Forms.TextBox txtBx_delay0;
        private System.Windows.Forms.TextBox txtBx_delay1;
        private System.Windows.Forms.TextBox txtBx_delay2;
        private System.Windows.Forms.TextBox txtBx_delay3;
        private System.Windows.Forms.TextBox txtBx_delay4;
        private System.Windows.Forms.TextBox txtBx_delay5;
        private System.Windows.Forms.TextBox txtBx_delay6;
        private System.Windows.Forms.TextBox txtBx_delay7;
        private System.Windows.Forms.TextBox txtBx_delay8;
        private System.Windows.Forms.TextBox txtBx_delay9;
        private System.Windows.Forms.TextBox txtBx_delay10;
        private System.Windows.Forms.TextBox txtBx_delay11;
        private System.Windows.Forms.TextBox txtBx_delay12;
        private System.Windows.Forms.TextBox txtBx_delay13;
        private System.Windows.Forms.TextBox txtBx_delay14;
        private System.Windows.Forms.TextBox txtBx_delay15;
        private System.Windows.Forms.TextBox txtBx_delay16;
        private System.Windows.Forms.TextBox txtBx_delay17;
        private System.Windows.Forms.TextBox txtBx_delay18;
        private System.Windows.Forms.TextBox txtBx_delay19;


        #endregion
    }

    class Eng_cmdCombineData {
        public int width_cmd { set; get; }
        public int width_comment { set; get; }
        /// <summary>
        /// 命令行的内容
        /// </summary>
        public string cmd { set; get; }
        /// <summary>
        /// 命令行的注释
        /// </summary>
        public string comment { set; get; }
        /// <summary>
        /// 命令行的延时(ms)
        /// </summary>
        public int delay { set; get; }
        /// <summary>
        /// 命令行的发送顺序
        /// </summary>
        public int squ { set; get; }
        /// <summary>
        /// 命令行的HEX标识
        /// </summary>
        public bool isHexChecked { set; get; }
        /// <summary>
        ///  命令/注释的位置
        /// </summary>
        public int pos { set; get; }
    }

    class Eng_cmdCombine
    {
        /// <summary>
        /// 测试总计数
        /// </summary>
        public CheckBox chckBx_hex;
        public TextBox txtBx_cmd;
        public Button btn_send;
        public Button btn_squ;
        public TextBox txtBx_delay;

        public Eng_cmdCombineData data;

        public Eng_cmdCombine(
            CheckBox chckBx_hex,
            TextBox txtBx_cmd,
            Button btn_send,
            Button btn_squ,
            TextBox txtBx_delay,
            Eng_cmdCombineData dx
        ) {
            this.chckBx_hex = chckBx_hex;
            this.txtBx_cmd = txtBx_cmd;
            this.btn_send = btn_send;
            this.btn_squ = btn_squ;
            this.txtBx_delay = txtBx_delay;
            this.data = dx;

            if (data.isHexChecked)
            {
                this.chckBx_hex.Checked = true;
            }
            else
            {
                this.chckBx_hex.Checked = false;
            }

            this.txtBx_cmd.Text = data.cmd;
            this.btn_send.Text = data.comment;
            this.btn_squ.Text = String.Format("{0:d2}", data.squ);
            this.txtBx_delay.Text = String.Format("{0:d}", data.delay);

            this.data.width_cmd = txtBx_cmd.Width;
            this.data.width_comment = btn_send.Width;
            int total_width = this.data.width_cmd + this.data.width_comment;

            this.txtBx_cmd.Width -= data.pos;
            this.btn_send.Width += data.pos;
            this.btn_send.Left -= data.pos;
        }

        public void updateWidth(int width)
        {
            this.txtBx_cmd.Width -= width;
            this.btn_send.Width += width;
            this.btn_send.Left -= width;

            this.data.width_cmd = this.txtBx_cmd.Width;
            this.data.width_comment = this.btn_send.Width;
            this.data.pos += width;
        }

    }

}
