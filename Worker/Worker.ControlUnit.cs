using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Worker
{
    partial class WorkerAgent
    {
        public class ControlUnit
        {
            // Types
            public delegate void NewLogItemEventHandler(Log log);

            // Events
            public event NewLogItemEventHandler NewLogItem;

            // Properties
            public IPEndPoint JobServerControlUnit { set; get; }

            // Methods
            public bool RegisterWorker(UInt16 WorkerID, int DataChannelPortNumber, int ControlChannelPortNumber)
            {
                const int MESSAGE_SIZE = 7;
                //const int REPLY_CODE_FAILURE = 0;
                const int REPLY_CODE_SUCCESSFULLY_REGISTERED = 2;
                const int REPLY_CODE_REGISTRATION_FAILED_DUPLICATE_ID_ = 3;

                bool registered;
                TcpClient client = new TcpClient();
                NetworkStream netStream;
                byte[] message = new byte[MESSAGE_SIZE];
                byte[] reply = new byte[1];

                #region set message fields
                byte[] workerIDBytes, dataChannelPortNumberBytes, controlChannelPortNumberBytes;

                // Code = 1
                message[0] = 1;

                // Worker ID
                workerIDBytes = BitConverter.GetBytes(WorkerID);
                message[1] = workerIDBytes[0];
                message[2] = workerIDBytes[1];

                // DataChannelPortNumber
                dataChannelPortNumberBytes = BitConverter.GetBytes((UInt16)DataChannelPortNumber);
                message[3] = dataChannelPortNumberBytes[0];
                message[4] = dataChannelPortNumberBytes[1];

                // ControlChannelPortNumberBytes
                controlChannelPortNumberBytes = BitConverter.GetBytes((UInt16)ControlChannelPortNumber);
                message[5] = controlChannelPortNumberBytes[0];
                message[6] = controlChannelPortNumberBytes[1];
                #endregion

                // Generate WorkerRegisterationStartedLog log
                WorkerRegisterationStartLog registerationStartedlog = new WorkerRegisterationStartLog();
                registerationStartedlog.Message = "Registering worker...";
                NewLogItem(registerationStartedlog);

                // Connect to job server and send message
                registered = false;
                try {
                    client.Connect(JobServerControlUnit);
                    netStream = client.GetStream();
                    netStream.ReadTimeout = 3000;  // Wait 3 seconds for reply from server

                    netStream.Write(message, 0, MESSAGE_SIZE);
                    netStream.Read(reply, 0, 1);

                    if (reply[0] == REPLY_CODE_SUCCESSFULLY_REGISTERED) {
                        registered = true;

                        // Generate WorkerRegisterdLog log
                        WorkerRegisterdLog log = new WorkerRegisterdLog();
                        log.Message = "Worker registerd.";
                        NewLogItem(log);
                    }
                    else if (reply[0] == REPLY_CODE_REGISTRATION_FAILED_DUPLICATE_ID_)  {
                        // Generate WorkerRegisterationFailedLog log
                        WorkerRegisterationFailedLog log = new WorkerRegisterationFailedLog();
                        log.Message = "Registeration failed. a worker with ID=" + WorkerID.ToString() + " is currently registered. please change Worker ID.";
                        NewLogItem(log);
                    }
                }
                catch (Exception e) {
                    // Generate ErrorInConnectingToJobServerLog log
                    ErrorInConnectingToJobServerLog log = new ErrorInConnectingToJobServerLog();
                    log.ErrorMessage = e.Message;
                    log.Message = "Error in connecting to job server.";
                    NewLogItem(log);
                }
                finally {
                    client.Close();
                }

                return registered;
            }
        }
    }
}
