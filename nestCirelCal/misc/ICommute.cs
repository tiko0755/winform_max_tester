using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nestCirelCal.Common
{
    /// <summary>
    /// 负责通信处理的的接口
    /// </summary>
    public interface ICommute
    {
        /// <summary>
        /// 显示的Id
        /// </summary>
        string Id { get; }

        bool ConnectedSta { get; }

        bool Connect();

        void Disconnect();

        bool Send(string cmd);

        bool Send(byte[] cmd);

        event EventHandler DataReceived;
        event EventHandler ConnectChanged;
    }
}
