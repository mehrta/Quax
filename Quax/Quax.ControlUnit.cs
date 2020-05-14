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
        class ControlUnit
        {
            // Fields
            private TcpListener _listener;

            // Events
            public event NewLogItemEventHandler NewLogItem;

            // Properties
            public int ControlChannelPortNumber { set; get; }
            public bool IsListening;
            public JobDispatcher JobDispatcher { set; get; }

            // Methods

            public void Start()
            {
                // Create TcpListener object
                _listener = new TcpListener(IPAddress.Any, ControlChannelPortNumber);

                // Start Listening
                // No exception handling required. exceptions go upward to caller function 
                _listener.Start();
                _listener.BeginAcceptTcpClient(AcceptConnectionCallBack, _listener);
                IsListening = true;
            }

            public void Stop()
            {
                try {
                    IsListening = false; // This line should comes before "_listener.stop()"
                    _listener.Stop();
                }
                catch {
                }

            }

            private void AcceptConnectionCallBack(IAsyncResult ar)
            {
                TcpClient client;
                NetworkStream netStream = null;

                // After invoking TcpListener.Stop(), this method is invoked and we must return from this method
                // if IsListening is false.
                if (IsListening)
                    client = _listener.EndAcceptTcpClient(ar);
                else
                    return;

                #region Start reciving data (control message) from worker
                byte fieldCode = 0;
                UInt16 fieldWorkerID = 0;
                UInt16 fieldDataChannelPortNumber = 0;
                UInt16 fieldControlChannelPortNumber = 0;

                try {
                    netStream = client.GetStream();
                    netStream.ReadTimeout = 3000;

                    // Read field "Code"
                    fieldCode = (byte)netStream.ReadByte();

                    if (fieldCode == 1) // Registeration Request
                    {
                        byte[] buffer = new byte[2];

                        // Read field "WorkerID"
                        netStream.Read(buffer, 0, 2);
                        fieldWorkerID = BitConverter.ToUInt16(buffer, 0);

                        // Read field "Data Channel Port Number"
                        netStream.Read(buffer, 0, 2);
                        fieldDataChannelPortNumber = BitConverter.ToUInt16(buffer, 0);

                        // Read field "Control Channel Port Number"
                        netStream.Read(buffer, 0, 2);
                        fieldControlChannelPortNumber = BitConverter.ToUInt16(buffer, 0);

                        //--------- Write Reply message
                        byte[] replyMessage = new byte[1];
                        //if (JobDispatcher.WorkerExist(fieldWorkerID))
                        //    replyMessage[0] = 3;  // A worker with this WorkerID was registered before, registration failed.
                        //else
                        replyMessage[0] = 2; // Worker registered.

                        netStream.Write(replyMessage, 0, 1);
                        //---------

                        // Generate "" Log
                        Quax.Log.WorkerRegisterationRequestLog log = new Log.WorkerRegisterationRequestLog();
                        log.Sender = new Worker();
                        log.Sender.ID = fieldWorkerID;
                        log.Sender.DataChannelPortNumber = fieldDataChannelPortNumber;
                        log.Sender.ControlChannelPortNumber = fieldControlChannelPortNumber;
                        log.Sender.IP = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                        log.Message = "A registeration request recived.";

                        // Raise "NewLogItem" event
                        NewLogItem(log);
                    }

                }
                catch {
                }
                finally {
                    netStream.Close();
                    client.Close();
                }
                #endregion


                // Start listening for new connections
                try {
                    _listener.BeginAcceptTcpClient(AcceptConnectionCallBack, _listener);
                }
                catch {

                }
            }

        }

    }

}
