using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nestCirelCal.Common;
using nestCirelCal;
using System.Drawing;

namespace nestCirelCal
{
    partial class UpdateFrm
    {

        public int InitUI_test(Form frmWindow, Control[] control, ref Work_Test workObj)
        {
            int count = -1;
            foreach (Control x in control)
            {
                if (x == null) break;
                switch (x.Name)
                {
                    case "tBx_total": tBxTotal = (TextBox)x; break;
                    case "tBx_fail": tBxFailure = (TextBox)x; break;
                    case "tBx_pass": tBxSuccess = (TextBox)x; break;
                    case "tBx_time": tBxTime = (TextBox)x; break;
                    case "tBx_tips": tBxTips = (TextBox)x; break;
                    case "tBx_judgement": tBxJudgement = (TextBox)x; break;
                    case "btn_start": btnStart = (Button)x; break;
                    case "dtGrdVw_test":    dtgrdTestVw = (DataGridView)x; break;
                }
            }

            workObj.evnt_updateTestLine += updateTestLine;
            workObj.evnt_updateTestCounter += updateCounter;
            workObj.evnt_updateTestTips += updateTestTips;
            workObj.evnt_updateTestTime += updateTestTime;

            testTableInit();

            btnStart.Click += new System.EventHandler(workObj.onClick_btnStart);

            return count;
        }

        void testTableInit()
        {
            dtgrdTestVw.BeginInvoke(new EventHandler(delegate {
                dataTable = new DataTable();
                dtgrdTestVw.DataSource = dataTable;
                // 设备格式
                dtgrdTestVw.RowHeadersVisible = false;  // 首列隐藏
                dtgrdTestVw.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;    // 标题居中
                dtgrdTestVw.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;    // 内容文本居中
                dtgrdTestVw.AllowUserToAddRows = false; // 不显示最后一空行
                dtgrdTestVw.AllowUserToResizeColumns = false;      // 锁定列宽
                dtgrdTestVw.AllowUserToResizeRows = false;      // 锁定行高
                dtgrdTestVw.AllowUserToOrderColumns = false;    // 禁用排序
                dtgrdTestVw.RowTemplate.Height = 30;    // 设置行高

                // 加载内容
                dataTable.Clear();

                // 加载测试表单的列
                for (int i = 0; i < configJson.test.table_col.Count; i++)
                {
                    dataTable.Columns.Add(configJson.test.table_col[i].name, typeof(string));
                    dtgrdTestVw.Columns[dataTable.Columns.Count - 1].Width = configJson.test.table_col[i].width;
                    dtgrdTestVw.Columns[dataTable.Columns.Count - 1].SortMode = DataGridViewColumnSortMode.NotSortable; // 禁用排序
                }

                // // 设置测试表单的行
                List<ConfigJsonTest_row> rowLst = configJson.test.table_row;
                for (int i = 0; i < rowLst.Count; i++)
                {
                    dataTable.Rows.Add(new object[] { (i + 1).ToString(), rowLst[i].name, rowLst[i].description, rowLst[i].lower.ToString(), rowLst[i].upper.ToString(), rowLst[i].unit, "", "", "" });
                }
            }));
        }

        void updateTestTips(object sender, string tips)
        {
            tBxTips.BeginInvoke(new EventHandler(delegate {
                tBxTips.Text = tips;
            }));
        }

        void updateTestTime(object sender, double totalTim)
        {
            tBxTime.BeginInvoke(new EventHandler(delegate
            {
                tBxTime.Text = (totalTim/1000).ToString("F2");
            }));
        }
        void updateTestLine(object sender, int lineIndx, string result, string judgement, double itemTime, Color itemColor, Color rsltColor) {
            string theResult = configJson.test.table_col[6].name;
            string theJudgeMent = configJson.test.table_col[7].name;
            string theTime = configJson.test.table_col[8].name;

            tBxJudgement.BeginInvoke(new EventHandler(delegate {
                tBxJudgement.BackColor = rsltColor;
            }));

            dataTable.Rows[lineIndx][theResult] = result;
            dataTable.Rows[lineIndx][theJudgeMent] = judgement;
            dataTable.Rows[lineIndx][theTime] = (itemTime/1000).ToString("F2");

            // update line color
            dtgrdTestVw.BeginInvoke(
                new EventHandler(delegate {
                    DataGridViewRow dr = dtgrdTestVw.Rows[lineIndx];
                    dr.DefaultCellStyle.BackColor = itemColor;
                })
            );
        }

        void updateCounter(object sender, uint total, uint pass, uint fail)
        {
            tBxTotal.BeginInvoke(new EventHandler(delegate {
                tBxTotal.Text = total.ToString();
            }));
            tBxSuccess.BeginInvoke(new EventHandler(delegate {
                tBxSuccess.Text = pass.ToString();
            }));
            tBxFailure.BeginInvoke(new EventHandler(delegate {
                tBxFailure.Text = fail.ToString();
            }));
        }

        

        #region

        /// <summary>
        /// 显示总的测试计数
        /// </summary>
        private TextBox tBxTotal;

        /// <summary>
        /// 显示失败的测试计数
        /// </summary>
        private TextBox tBxFailure;

        /// <summary>
        /// 显示成功的测试计数
        /// </summary>
        private TextBox tBxSuccess;

        /// <summary>
        /// 显示测试时间(S)
        /// </summary>
        private TextBox tBxTime;

        /// <summary>
        /// 显示测试提示信息
        /// </summary>
        private TextBox tBxTips;

        /// <summary>
        /// 显示测试结果
        /// </summary>
        private TextBox tBxJudgement;

        /// <summary>
        /// 开始测试按钮
        /// </summary>
        private Button btnStart;

        /// <summary>
        /// 开始测试按钮
        /// </summary>
        private DataGridView dtgrdTestVw;

        /// <summary>
        /// 测试项目表格
        /// </summary>
        DataTable dataTable;


        #endregion
    }

}
