using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using goldenman.hen;
using goldenman.hen.f10;
using goldenman.Log;

namespace goldenman
{
    /// <summary>
    /// 更新字符串的委托
    /// </summary>
    /// <param name="strStatusInfo"></param>
    /// <param name="backgroundColor"></param>
    public delegate void UpdateString(string str);

    public delegate void UpdateStringList(List<string> sLst);

    public delegate void UpdateTitle(string code, string name, string major);

    partial class UpdateFrm
    {
        public int InitData(Form frmWindow, Control[] control)
        {
            int count = -1;
            //foreach (Control x in control)
            //{
            //    if (x == null) break;
            //    switch (x.Name)
            //    {
            //        case "labelKUpdate": lblUpDate = (Label)x; break;
            //        case "button_kUpdate": btnKUpdate = (Button)x; break;
            //        case "richTextBox_log": rictTxtBoxLog = (RichTextBox)x; break;
            //    }
            //}

            //btnKUpdate.Click += new System.EventHandler(onClick_BtnKUpdate);
            //rictTxtBoxLog.DoubleClick += new System.EventHandler(onDoubleClick_RichTxtBxbLog);

           

            return count;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //private void updateDGV_codes()
        //{
        //    for (int i=0;i<code.Count;i++)
        //    {
        //        if (name[i].IndexOf("--") == 0)
        //            continue;
        //        int index = this.dtgrdvwCodes.Rows.Add();
        //        this.dtgrdvwCodes.Rows[index].Cells[0].Value = code[i].Substring(2);
        //        this.dtgrdvwCodes.Rows[index].Cells[1].Value = name[i];
        //        switch (this.block[i])
        //        {
        //            case "深交所主板A股":
        //                indexBlckSz.Add(i);
        //                break;
        //            case "上交所主板A股":
        //                indexBlckSh.Add(i);
        //                break;
        //            case "深交所创业板A股":
        //                indexBlckCy.Add(i);
        //                break;
        //            case "上交所科创板A股":
        //                indexBlckKc.Add(i);
        //                break;
        //        }
        //    }
        //}

 



        #region

        #endregion

    }
}
