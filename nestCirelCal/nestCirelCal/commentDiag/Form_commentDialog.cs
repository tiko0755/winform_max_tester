using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nestCirelCal.Common;

namespace nestCirelCal.commentDiag
{
    public partial class Form_commentDialog : Form
    {
        CommentDialog commentDialog;
        public event StrEventHandler evntConfirm;
        public Form_commentDialog()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            commentDialog = new CommentDialog(this, WinFrmControl());

            this.Load += new System.EventHandler(this.Form_commentDialog_Load);
            this.button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            evntConfirm?.Invoke(this, textBox1.Text);
            this.Dispose();
        }

        private void Form_commentDialog_Load(object sender, EventArgs e)
        {
        }

        private void FrmMain_FormClosing(object sender, EventArgs e)
        {
        }

        private Control[] WinFrmControl()
        {
            Control[] ctrl = new Control[200];
            int i = 0;
            ctrl[i++] = textBox1;
            ctrl[i++] = button1;
            return ctrl;
        }

    }
}
