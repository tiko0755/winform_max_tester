using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goldenman
{
    partial class UpdateFrm
    {
        public int InitUser(Form frmWindow, Control[] control)
        {
            int count = -1;
            foreach (Control x in control)
            {
                if (x == null) break;
                //switch (x.Name)
                //{
                //    case "textBox_txdroot": txtbxTdxRoot = (TextBox)x; break;
                //    case "textBox_f10root": txtbxF10Root = (TextBox)x; break;
                //    case "textBox_userroot": txtbxUsrRoot = (TextBox)x; break;
                //}
            }
            return count;
        }

        #region
        private TextBox txtbxTdxRoot, txtbxF10Root, txtbxUsrRoot;
        #endregion

    }
}
