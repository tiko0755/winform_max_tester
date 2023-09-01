using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using nestCirelCal.Common;

namespace nestCirelCal
{
    //public delegate void EventHandler(object sender, EventArgs e);
    public class SerialCom: ICommute
    {
        private string _portName = "";
        private bool _connected;
        //private byte[] _rawData;
        private readonly SerialPort _port;

        public string Id => _portName;

        public bool ConnectedSta
        {
            get { return _connected; }
            private set
            {
                if(_connected != value)
                {
                    _connected = value;
                    ConnectChanged?.Invoke(this, null);
                }
            }
        }

        public event EventHandler DataReceived;
        public event EventHandler ConnectChanged;

        public SerialCom(string portName, int baudrate, Parity parity, int dataBits, StopBits stopbits)
        {
            Console.WriteLine("new a serialport");
            _portName = portName;
            _port = new SerialPort(portName, baudrate, parity, dataBits, stopbits);
            _port.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Thread.Sleep(10);

            int c = _port.BytesToRead;

            byte[] _rawData = new byte[c];
            for (int i = 0; i < c; i++)
            {
                _rawData[i] = (byte)_port.ReadByte();
            }

            bytesEventArgs argx = new bytesEventArgs(_rawData);
            DataReceived?.Invoke(this, argx);
        }

        public bool Connect()
        {
            if (_port == null)
            {
                return false;
            }

            if (_port.IsOpen)
            {
                return true;
            }

            try
            {
                _port.Open();
                ConnectedSta = true;
                return true;
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "Open port {0} failed, {1}", _portName, ex.Message);
                Console.WriteLine("Open port {0} failed, {1}", _portName, ex.Message);
                return false;
            }
        }

        public void Disconnect()
        {
            if (_port == null)
            {
                return;
            }

            ConnectedSta = false;
            _port.Close();
        }

        public bool Send(string cmd)
        {
            if (!ConnectedSta)
            {
                return false;
            }

            return Send(Encoding.ASCII.GetBytes(cmd), cmd.Length);
        }

        public bool Send(byte[] cmd)
        {
            if (!ConnectedSta)
            {
                return false;
            }
            _port.Write(cmd, 0, cmd.Length);
            return true;
        }

        public bool Send(byte[] cmd, int len)
        {
            if (!ConnectedSta)
            {
                return false;
            }
            _port.Write(cmd, 0, len);
            return true;
        }
    }
}
