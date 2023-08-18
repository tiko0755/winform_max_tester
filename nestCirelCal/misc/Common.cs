using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using nestCirelCal.Log;

namespace nestCirelCal.Common
{
    public delegate bool CommandLogic(string cmd, int dayRecIndx, ref bool cmdExcuted);
    public delegate bool CommandLogicMarket(string cmd, string code, uint date, ref bool cmdExcuted);
    public delegate void StrHandler(string str);
    public delegate void StrEventHandler(object sender, string str);
    public delegate void XEventHandler(Object sender, XEventArgs e);
    public class XEventArgs : EventArgs
    {
        public List<string> strList;

        public XEventArgs(string str)
        {
            this.strList = new List<string>();
            this.strList.Add(str);
        }
        public XEventArgs(List<string> sList)
        {
            this.strList = new List<string>();
            this.strList = sList;
        }
    }

    public class bytesEventArgs : EventArgs
    {
        public byte[] byteArry { get; }
        public bytesEventArgs(byte[] arry)
        {
            byteArry = arry;
        }
    }

    

}
