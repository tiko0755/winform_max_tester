using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using nestCirelCal.Common;

namespace nestCirelCal.MaxCmdType
{

    public enum MAX_OPRATE
    {
        REQ_G_SENSOR_VERIFY     = 0x2A,
        REQ_NTC_TEMP            = 0x28,

        RESP_G_SENSOR_VERIFY    =  0X80|REQ_G_SENSOR_VERIFY,
        RESP_NTC_TEMP           =  0X80|REQ_NTC_TEMP,
    }

    public class maxCmdType
    {
        public byte[] head { set; get; }
        public MAX_OPRATE op { set; get; }
        public byte len { set; get; }
        public byte[] payload { set; get; }
        public byte crc8 { set; get; }
        public maxCmdType()
        {
            Reset();
            head = new byte[2] { 0x4d, 0x45 };
        }
        public maxCmdType(MAX_OPRATE OP, byte[] Payload)
        {
            Reset();
            head = new byte[2] { 0x4d, 0x45 };
            op = OP;
            if(Payload != null)
            {
                len = (byte)Payload.Length;
                payload = Payload;
            }
        }
        public void Reset()
        {
            op = 0;
            len = 0;
            payload = null;
            crc8 = 0;
        }

        public void MergeCRC8()
        {
            byte[] buff = new byte[4+len];
            buff[0] = head[0];
            buff[1] = head[1];
            buff[2] = (byte)op;
            buff[3] = len;
            if(payload != null)
            {
                Array.Copy(payload, 0, buff, 4, len);
            }
            crc8 = generateCRC8(buff);
        }

        public byte[] toByteArry()
        {
            MergeCRC8();
            byte[] buff = new byte[4 + len + 1];
            buff[0] = head[0];
            buff[1] = head[1];
            buff[2] = (byte)op;
            buff[3] = len;
            if(payload != null)
            {
                Array.Copy(payload, 0, buff, 4, len);
            }
            buff[4+len] = crc8;
            return buff;
        }

        public static byte generateCRC8(byte[] data)   //计算crc8的数据
        {
            int crc = 0x00; //crc 初始值
            int crc_poly = 0x07; //crc poly的值
            foreach (byte x in data)
            { //依次输入数据
                crc = crc ^ x; //crc的高八位异或输入数据
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x00000080) != 0)
                    {  //最高位计算
                        crc = (crc << 1) ^ crc_poly;
                    }
                    else
                    {
                        crc = (crc << 1);
                    }
                }
            }
            return (byte)((crc ^ 0x00) & 0xff);
        }
    }
}
