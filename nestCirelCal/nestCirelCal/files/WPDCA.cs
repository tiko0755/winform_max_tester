using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace LaserMarking.Files
{
    class  DataType
    {
        public String Name;
        public String Unit;
        public String Lower;
        public String Upper;
        public String Value;
    };

    class AllData
    {
        public AllData()
        { }

        public String SN = "";
        public String Result;
        public String SW_Version;
        public String Reserved;

        public System.Collections.ObjectModel.Collection<DataType> DataItems
            = new System.Collections.ObjectModel.Collection<DataType>();
    }
    
    static class WPDCA
    {
        public static void WriteData(AllData data,String path)
        {
            StreamWriter writer = new StreamWriter(path);
            String strdata = "";
            String passItems = "";
            String failItems = "";

            foreach (DataType each in data.DataItems)
            {
                passItems += "," + each.Name + "," + each.Unit + "," + each.Lower + "," + each.Upper + "," + each.Value;

                if (each.Lower != "NA")
                {
                    if (double.Parse(each.Value) <= double.Parse(each.Lower) 
                        || double.Parse(each.Value) >= double.Parse(each.Upper))
                    {
                        failItems += each.Name + ";";
                    }
                }
            }

            strdata = data.SN + "," + data.Result + "," + failItems + ",," + data.SW_Version + "," + data.Reserved + passItems;
        
            writer.Write(strdata);
            writer.Flush();
            writer.Close();
        }

        public static string GetSnFromStart(String path)
        {
            StreamReader reader;
            String SN="";

            //if (!File.Exists(path))
            //{
            //    return SN;
            //}

            reader = new StreamReader(path);
            SN = reader.ReadToEnd();
            reader.Close();

            //File.Delete(path);

            return SN;
        }
    }

    /// <summary>
    /// 定制PDC文件类
    /// </summary>
    static class HBLPdc
    {
        private static string pathDir = "D:\\DropBox";

        static HBLPdc()
        {
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
        }

        /// <summary>
        /// 获得DropBox文件夹下文件中的SN
        /// </summary>
        /// <returns>产品SN</returns>
        public static string GetSn()
        {
            return WPDCA.GetSnFromStart(pathDir + "\\Start.txt");
        }

        /// <summary>
        /// 将数据写入PDC Log中.[数据格式：SN, Date, Time, GrapeColor，HB D-Value，Brkt D-Value, Shim Height, Shim, Type, Result]
        /// </summary>
        /// <param name="pdcData">需要写入的数据</param>
        public static void WritePdcLog(params string[] pdcData)
        {
            if (pdcData.Length != 13)
            {
                throw new Exception("输入的参数个数不正确");
            }

            string[] names = new string[] { "Grape SN", "Date", "Time", "Grape Color",
                "HB Z-Measurements", "Brkt Z-Measurements", "Shim Z-Heights", "Shim", "X-Error", "Y-Error", "Feeling", "Type" };
            AllData allData = new AllData();

            for (int i = 0; i < names.Length; i++)
            {
                DataType type = new DataType();

                if (names[i] == "Shim Z-Heights")
                {
                    type.Lower = "0.1";
                    type.Upper = "0.7";
                    type.Unit = "mm";
                }
                else if (names[i] == "X-Error" || names[i] == "Y_Error")
                {
                    type.Unit = "mm";
                    type.Lower = "-0.1";
                    type.Upper = "0.1";
                }
                else
                {
                    if (i == 4 || i == 5)
                    {
                        type.Unit = "mm";
                    }
                    else
                    {
                        type.Upper = "NA";
                    }

                    type.Lower = "NA";
                    type.Upper = "NA";
                }

                type.Name = names[i];
                type.Value = pdcData[i];
                allData.DataItems.Add(type);
            }

            allData.SN = pdcData[0];
            allData.Result = pdcData[pdcData.Length - 1];
            allData.SW_Version = AssemblyName.GetAssemblyName(Application.ExecutablePath).Version.ToString();
            allData.Reserved = "Reserved";

            WPDCA.WriteData(allData, pathDir + "\\Done.txt");
        }
    }
}
