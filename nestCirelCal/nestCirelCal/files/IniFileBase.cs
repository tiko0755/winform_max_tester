using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace goldenman.Files
{
    class IniFileBase
    {
        /// <summary>
        /// Windows API 对INI文件写方法
        /// </summary>
        /// <param name="lpAppName">要在其中写入新字串的段名。这个字串不区分大小写</param>
        /// <param name="lpKeyName">要设置的项名或条目名。这个字串不区分大小写。用null可删除这个小节的所有设置项</param>
        /// <param name="lpString">指定为这个项写入的字串值。用null表示删除这个项现有的字串</param>
        /// <param name="lpFileName">初始化文件的名字。如果没有指定完整路径名，则windows会在windows目录查找文件。如果文件没有找到，则函数会创建它</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string strPath);

        /// Windows API 对INI文件字符串的读方法
        /// </summary>
        /// <param name="lpAppName">要在其中读取字符串的段名。这个字串不区分大小写</param>
        /// <param name="lpKeyName">要读取数据的关键字名称。这个字串不区分大小写。</param>
        /// <param name="lpDefault">当不存在指定关键字名称时，默认返回的字符串</param>
        /// <param name="lpReturnedString">返回的字符串</param>
        /// <param name="nSize">lpReturnedString参数的空间字节大小</param>
        /// <param name="lpFileName">初始化文件的名字。如果没有指定完整路径名，则windows会在windows目录查找文件。如果文件没有找到，则函数会创建它</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def,
                                                         StringBuilder sb, int size, string strPath);

        /// <summary>
        /// Windows API 对INI文件整数的读方法
        /// </summary>
        /// <param name="lpAppName">要在其中读取字符串的段名。这个字串不区分大小写</param>
        /// <param name="lpKeyName">要读取数据的关键字名称。这个字串不区分大小写。用null可删除这个小节的所有设置项</param>
        /// <param name="nDefault">当未能找到指定的关键字名称时，则返回的默认值</param>
        /// <param name="lpFileName">初始化文件的名字。如果没有指定完整路径名，则windows会在windows目录查找文件。如果文件没有找到，则函数会创建它</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileInt", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileInt(
            string lpAppName,
            string lpKeyName,
            int nDefault,
            string lpFileName);


        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="strClassName"></param>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        protected void WriteToIni(string strClassName, string strKey, string strValue,string strPath)
        {
            WritePrivateProfileString(strClassName, strKey, strValue, strPath);
        }

        /// <summary>
        /// 从配置文件中读取
        /// </summary>
        /// <param name="strClassName"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        protected string ReadStrFromIni(string strClassName, string strKey, string strPath)
        {
            StringBuilder stringBuilder = new StringBuilder(500);
            GetPrivateProfileString(strClassName.Trim(), strKey.Trim(), "", stringBuilder, 500, strPath);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 从配置文件中读取
        /// </summary>
        /// <param name="strClassName"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        protected int ReadIntFromIni(string strClassName, string strKey, string strPath)
        {
            int defualt = 0;
            return  GetPrivateProfileInt(strClassName.Trim(), strKey.Trim(), defualt, strPath);
        }
    }
}
