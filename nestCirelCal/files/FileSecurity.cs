using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Collections;

namespace LaserMarking.Files
{
    class FileSecurity
    {
        /// <summary>
        /// 创建一个静态实例
        /// </summary>
        private static FileSecurity security = new FileSecurity();
        public static FileSecurity Instance
        {
            get { return security; }
        }

        #region 加密解密

        //这里最好是将密钥写到注册表中
        private string encryptkey = "Oyln";

        /// <summary>
        /// 对数据进行加密
        /// </summary>
        /// <param name="encryptstring">需要加密的数据</param>
        /// <returns></returns>
        public string DESEncrypt(string encryptstring)
        {
            string strRtn;

            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();//des进行加密
                byte[] key = System.Text.Encoding.Unicode.GetBytes(encryptkey);
                byte[] data = System.Text.Encoding.Unicode.GetBytes(encryptstring);
                MemoryStream ms = new MemoryStream();//存储加密后的数据
                CryptoStream cs = new CryptoStream(ms, desc.CreateEncryptor(key, key), CryptoStreamMode.Write);
                cs.Write(data, 0, data.Length);//进行加密
                cs.FlushFinalBlock();
                strRtn = Convert.ToBase64String(ms.ToArray());
                return strRtn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message, "错误消息提示框", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// 对数据进行解密
        /// </summary>
        /// <param name="decryptstring">需要解密的数据</param>
        /// <returns></returns>
        public string DESDecrypt(string decryptstring)
        {
            string strRtn;

            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                byte[] key = System.Text.Encoding.Unicode.GetBytes(encryptkey);
                byte[] data = Convert.FromBase64String(decryptstring);
                MemoryStream ms = new MemoryStream();//存储解密后的数据
                CryptoStream cs = new CryptoStream(ms, desc.CreateDecryptor(key, key), CryptoStreamMode.Write);
                cs.Write(data, 0, data.Length);//解密数据
                cs.FlushFinalBlock();
                strRtn = System.Text.Encoding.Unicode.GetString(ms.ToArray());
                return strRtn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message, "错误消息提示框", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return null;
            }
        }

        #endregion 
    }
}
