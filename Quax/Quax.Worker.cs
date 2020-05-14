using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace QuaxMiddleware
{
    partial class Quax
    {
        public class Worker
        {
            public ushort ID { set; get; }
            public IPAddress IP { set; get; }
            public int DataChannelPortNumber {set; get;}    // Worker is listening on this port for data messages
            public int ControlChannelPortNumber {set; get;} // Worker is listening on this port for control messages
            public bool IsUnresponsive {set; get;}

            public Worker()
            {
                DataChannelPortNumber = 20000;
                ControlChannelPortNumber = 20001;
                IsUnresponsive = false;
            }
        }
    }

}
