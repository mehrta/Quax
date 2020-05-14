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
        private class JobResultReciver
        {
            // Fields
            private TcpListener _listener;

            // Events
            public event JobResultReciveEventHandler JobResultRecive;

            // Properties
            public int DataChannelPortNumber { set; get; }
            public bool IsListening;

            // Methods

            public JobResultReciver()
            {
            }

            public void Start()
            {
                // Create TcpListener object
                _listener = new TcpListener(IPAddress.Any, DataChannelPortNumber);

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
                JobResult jobResult = new JobResult();
                byte[] payload = null;

                // After invoking TcpListener.Stop(), this method is invoked and we must return from this method
                // if IsListening is false.
                if (IsListening)
                    client = _listener.EndAcceptTcpClient(ar);
                else
                    return;

                #region Start reciving data (job result) from network and store it in buffer
                bool dataRecivedSuccessfully = true;
                byte flags;
                UInt32 payloadSize;
                UInt16 workerID = 0;
                UInt64 jobID = 0;

                try {
                    netStream = client.GetStream();

                    // Read field "Flags" (Reserved field)
                    flags = (byte)netStream.ReadByte();

                    // Read field "Payload Size"
                    byte[] payloadSizeBytes = new byte[4];
                    netStream.Read(payloadSizeBytes, 0, 4);

                    // Read field "WorkerID"
                    byte[] workerIDBytes = new byte[2];
                    netStream.Read(workerIDBytes, 0, 2);

                    // Read field "JobID"
                    byte[] jobIDBytes = new byte[8];
                    netStream.Read(jobIDBytes, 0, 8);

                    // Check Endianness
                    if (!BitConverter.IsLittleEndian) {
                        // Big Endian Architecture
                        payloadSizeBytes.Reverse();
                        workerIDBytes.Reverse();
                        jobIDBytes.Reverse();
                    }

                    payloadSize = BitConverter.ToUInt32(payloadSizeBytes, 0);
                    workerID = BitConverter.ToUInt16(workerIDBytes, 0);
                    jobID = BitConverter.ToUInt64(jobIDBytes, 0);

                    // Read payload of message (job's output file)
                    payload = new byte[payloadSize];
                    netStream.Read(payload, 0, (int)payloadSize);
                }
                catch {
                    dataRecivedSuccessfully = false;
                }
                finally {
                    netStream.Close();
                    client.Close();
                }
                #endregion

                if (dataRecivedSuccessfully) {
                    jobResult.JobID = jobID;
                    jobResult.WorkerID = workerID;
                    jobResult.Data = payload;

                    // Fire JobResultRecive event
                    JobResultRecive(jobResult);
                }
                else {

                }

                // Start listening for new connections
                try {
                    _listener.BeginAcceptTcpClient(AcceptConnectionCallBack, _listener);
                }
                catch {
                    // Generate WorkerCouldNotStartLog log
                }
            }
        }
    }
}
