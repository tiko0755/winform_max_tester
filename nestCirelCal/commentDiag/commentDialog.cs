using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nestCirelCal.commentDiag
{
    class CommentDialog
    {
        Form frm;
        Control[] ctrl;
        TextBox txtBx_input;
        Button btn_confirm;

        public CommentDialog(Form frmWindow, Control[] ctrl)
        {
            this.frm = frmWindow;
            this.ctrl = ctrl;
            frm.Load += new System.EventHandler(this.Frm_Load);
            frm.FormClosing += new FormClosingEventHandler(FrmMain_FormClosing);
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            foreach (Control x in ctrl)
            {
                if (x == null) break;
                switch (x.Name)
                {
                    case "textBox1": txtBx_input = (TextBox)x; break;
                    case "button1": btn_confirm = (Button)x; break;
                }
            }
        }

        private void FrmMain_FormClosing(object sender, EventArgs e)
        {
        }



    }
}
