using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace Worker
{
    public partial class WorkerAgent
    {
        // Types
        public delegate void NewLogItemEventHandler(Log l);

        // Events
        public event NewLogItemEventHandler NewLogItem;

        // Fields
        TcpListener _listener;
        Queue<Job> _jobsQueue;
        ControlUnit _controlUnit;

        // Properties
        public ushort ID { set; get; } // Worker ID
        public IPAddress JobServerIPAddress { set; get; }
        public int JobServerDataChannelPortNumber { set; get; }
        public int JobServerControlChannelPortNumber { set; get; }
        public int ListenDataChannelPortNumber { set; get; }
        public int ListenControlChannelPortNumber { set; get; }
        public bool IsListening { private set; get; }
        public string JobsDirectoryName { set; get; }
        public int AttemptsToSendResult = 3; // Number of attempts to send job result to the job server, after this attempts worker agent will stop

        public WorkerAgent()
        {
            ID = 1; // ID is a member of [1..ushort.MaxValue];
            _jobsQueue = new Queue<Job>(50);
            JobServerDataChannelPortNumber = 40000;
            JobServerControlChannelPortNumber = 40001;
            ListenDataChannelPortNumber = 20000;
            ListenControlChannelPortNumber = 20001;
            JobServerIPAddress = IPAddress.Loopback;

            _controlUnit = new ControlUnit();
            _controlUnit.NewLogItem += new ControlUnit.NewLogItemEventHandler(_controlUnit_OnNewLogItem);
        }

        void _controlUnit_OnNewLogItem(WorkerAgent.Log log)
        {
            // Raise "this.NewLogItem" event
            EventExtensions.Raise(NewLogItem, new[] { log });
        }

        public void Start()
        {
            Log log = null;

            if (IsListening)
                throw new InvalidOperationException("Worker agent is started alerady.");

            try {
                // Start Listening
                _listener = new TcpListener(IPAddress.Any, ListenDataChannelPortNumber);
                _listener.Start();
                _listener.BeginAcceptTcpClient(AcceptConnectionCallBack, _listener);
                IsListening = true;

                // Create worker thread
                Thread t = new Thread(WorkerThread);
                t.IsBackground = true;
                t.Start();
                IsListening = true;

                // Generate Log
                log = new WorkerStartedLog();
                log.Message = "Worker started.";
                EventExtensions.Raise(NewLogItem, new[] { log });

            }
            catch (Exception e) {
                // Generate Log
                log = new WorkerCouldNotStartLog();
                log.Message = "Worker could not start.";
                ((WorkerCouldNotStartLog)log).ErrorMessage = e.Message;
                EventExtensions.Raise(NewLogItem, new[] { log });
            }

            // Try to register worker
            if (IsListening) {
                bool registerd;
                _controlUnit.JobServerControlUnit = new IPEndPoint(JobServerIPAddress, JobServerControlChannelPortNumber);
                registerd = _controlUnit.RegisterWorker(this.ID, this.ListenDataChannelPortNumber, this.ListenControlChannelPortNumber);
                
                // If registration has failed, we should stop listening
                if (!registerd)
                    try {
                        IsListening = false; // This line should comes before "_listener.stop()"
                        _listener.Stop();
                    }
                    catch {
                    }
            }
        }

        public void Stop()
        {
            Log log;
            try {
                IsListening = false; // This line should comes before "_listener.stop()"
                _listener.Stop();

                // Generate Log
                log = new WorkerStoppedLog();
                log.Message = "Worker stopped.";
            }
            catch (Exception e) {
                // Generate Log
                log = new WorkerCouldNotStopLog();
                log.Message = "Worker could not stop.";
                ((WorkerCouldNotStopLog)log).ErrorMessage = e.Message;
            }

            // Raise "NewLogItem" event
            EventExtensions.Raise(NewLogItem, new[] { log });
        }

        public void ClearJobsQueue()
        {
            lock (_jobsQueue)
                _jobsQueue.Clear();

            // Generate log
            Log log = new Log();
            log.Message = "All jobs removed from the jobs queue.";
            EventExtensions.Raise(NewLogItem, new[] { log });

        }

        private void WorkerThread()
        {
            while (IsListening) {
                Job j = null;
                lock (_jobsQueue) {
                    if (_jobsQueue.Count > 0)
                        j = _jobsQueue.Dequeue();
                    else
                        Monitor.Wait(_jobsQueue, 3000);
                }

                if (j != null) {
                    #region Run the job (if it is never executed before)
                    bool jobSuccessfullyExecuted = false;

                    if (System.IO.File.Exists(j.OutputFileName)) {
                        // job is executed in past,and becuse of a send error it returned to the jobs queue
                        // we shouldn't execute it again
                        jobSuccessfullyExecuted = true;
                    }
                    else {
                        // Execute the job
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = j.ExecuteableFileName;
                        startInfo.Arguments = "\"" + j.InputFileName + "\" \"" + j.OutputFileName + "\"";
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.CreateNoWindow = true;

                        // Raise
                        WorkerAgent.JobExecutionStartedLog executionStartedLog = new JobExecutionStartedLog();
                        executionStartedLog.Job = j;
                        executionStartedLog.Message = "Job execution has started.";
                        EventExtensions.Raise(NewLogItem, new object[] { executionStartedLog });

                        Log executionLog;
                        try {
                            // Try execute the job
                            Process proc = Process.Start(startInfo);
                            proc.WaitForExit();
                            jobSuccessfullyExecuted = true;
                            executionLog = new JobExecutedSuccessfullyLog();
                            ((JobExecutedSuccessfullyLog)executionLog).Job = j;
                            executionLog.Message = "Job successfully executed.";
                        }
                        catch (System.ComponentModel.Win32Exception win32Exception) {
                            executionLog = new ErrorInJobExecutionLog();
                            executionLog.Message = "An error occurred while running the job.";
                            ((ErrorInJobExecutionLog)executionLog).Job = j;
                            ((ErrorInJobExecutionLog)executionLog).IsWin32Error = true;
                            ((ErrorInJobExecutionLog)executionLog).Win32ErrorCode = win32Exception.ErrorCode;
                            ((ErrorInJobExecutionLog)executionLog).ErrorMessage = win32Exception.Message;
                        }
                        catch (Exception e) {
                            executionLog = new ErrorInJobExecutionLog();
                            executionLog.Message = "An error occurred while running the job.";
                            ((ErrorInJobExecutionLog)executionLog).Job = j;
                            ((ErrorInJobExecutionLog)executionLog).ErrorMessage = e.Message;
                        }
                        EventExtensions.Raise(NewLogItem, new object[] { executionLog });
                    }
                    #endregion

                    // Send job result to the job server
                    bool successfullySent = false;
                    bool errorInReadingOutputFile = false;
                    if (IsListening && jobSuccessfullyExecuted)
                        successfullySent = SendResultToJobServer(j, out errorInReadingOutputFile);

                    if (!successfullySent && jobSuccessfullyExecuted && !errorInReadingOutputFile && IsListening) {
                        // Sending job to the job server failed becuse of a network problem
                        // We should restore the job to the jobs queue and stop.
                        lock (_jobsQueue) {
                            _jobsQueue.Enqueue(j); // Restore the job to the jobs queue
                            Stop(); // Stop listening
                        }
                    }

                }
            }
        }

        private bool SendResultToJobServer(Job j, out bool errorInReadingOutputFile)
        {
            const int HEADER_FLAGS_FIELD_SIZE = 1; // 1 Byte
            const int HEADER_PAYLOAD_SIZE_FIELD_SIZE = 4; // 4 Byte
            const int HEADER_WORKER_ID_FIELD_SIZE = 2; // 2 Byte
            const int HEADER_JOB_ID_FIELD_SIZE = 8; // 8 Byte
            const int HEADER_PAYLOAD_START_INDEX = HEADER_FLAGS_FIELD_SIZE + HEADER_PAYLOAD_SIZE_FIELD_SIZE +
                HEADER_WORKER_ID_FIELD_SIZE + HEADER_JOB_ID_FIELD_SIZE;
            byte[] message = null;
            long messageSize = 0;
            bool jobOutputFileReadSuccessfully = true;
            uint jobOutputFileLength = 0;

            #region Read Job's output file
            System.IO.FileStream fs = null;
            ErrorInReadingJobOutputFile fileReadErrLog = null;
            try {
                // Open Job's result file for reading
                fs = System.IO.File.OpenRead(j.OutputFileName);

                // allocate memory for message
                jobOutputFileLength = (uint)fs.Length;
                messageSize = HEADER_FLAGS_FIELD_SIZE + HEADER_PAYLOAD_SIZE_FIELD_SIZE + HEADER_WORKER_ID_FIELD_SIZE +
                    HEADER_JOB_ID_FIELD_SIZE + jobOutputFileLength;
                message = new byte[messageSize];
                //

                // Read file and close it
                fs.Read(message, HEADER_PAYLOAD_START_INDEX, (int)jobOutputFileLength);
                errorInReadingOutputFile = false;
            }
            catch (System.IO.FileNotFoundException fileNotFoundExcp) {
                jobOutputFileReadSuccessfully = false;
                fileReadErrLog = new WorkerAgent.ErrorInReadingJobOutputFile();
                fileReadErrLog.Job = j;
                fileReadErrLog.Message = "Job's output file not found.";
                fileReadErrLog.ErrorMessage = fileNotFoundExcp.Message;
                errorInReadingOutputFile = true;
                EventExtensions.Raise(NewLogItem, new[] { fileReadErrLog });
            }
            catch (Exception e) {
                jobOutputFileReadSuccessfully = false;
                fileReadErrLog = new WorkerAgent.ErrorInReadingJobOutputFile();
                fileReadErrLog.Job = j;
                fileReadErrLog.Message = "Error in reading job's output file.";
                fileReadErrLog.ErrorMessage = e.Message;
                errorInReadingOutputFile = true;
                EventExtensions.Raise(NewLogItem, new[] { fileReadErrLog });
            }
            finally {
                if (fs != null)
                    fs.Close();
            }
            #endregion

            if (!jobOutputFileReadSuccessfully)
                return false;

            #region Send job result(message) to the job server
            // Set Header's fields of the message
            byte[] payloadSizeBytes = new byte[4]; // UInt32
            byte[] workerIDBytes = new byte[2]; // UInt16
            byte[] jobIDBytes = new byte[8]; // UInt64

            // Flags
            message[0] = 0; // This field is reserved for future use.

            // Payload Size, UInt32
            payloadSizeBytes = BitConverter.GetBytes(jobOutputFileLength);
            for (int i = 0; i < 4; i++)
                message[i + HEADER_FLAGS_FIELD_SIZE] = payloadSizeBytes[i];

            // Worker ID, UInt16
            workerIDBytes = BitConverter.GetBytes(ID);
            for (int i = 0; i < 2; i++)
                message[i + HEADER_FLAGS_FIELD_SIZE + HEADER_PAYLOAD_SIZE_FIELD_SIZE] = workerIDBytes[i];

            // JobID, UInt64
            jobIDBytes = BitConverter.GetBytes(j.ID);
            for (int i = 0; i < 8; i++)
                message[i + HEADER_FLAGS_FIELD_SIZE + HEADER_PAYLOAD_SIZE_FIELD_SIZE + HEADER_WORKER_ID_FIELD_SIZE] = jobIDBytes[i];

            // Try "AttemptsToSendResult" times to send the message
            bool successfullySent = false;
            TcpClient client = new TcpClient();
            NetworkStream netStream = null;

            for (int i = 1; (i <= AttemptsToSendResult) && (!successfullySent); i++) {
                try {
                    client.Connect(new IPEndPoint(JobServerIPAddress, JobServerDataChannelPortNumber));
                    netStream = client.GetStream();
                    netStream.Write(message, 0, (int)messageSize);
                    successfullySent = true;

                    // Generate log
                    WorkerAgent.JobResultSentLog log = new JobResultSentLog();
                    log.Job = j;
                    log.Message = "Job's output file sent to the job server.";
                    EventExtensions.Raise(NewLogItem, new[] { log });
                }
                catch (Exception e) {
                    successfullySent = false;
                    WorkerAgent.ErrorInConnectingToJobServerLog log = new ErrorInConnectingToJobServerLog();
                    log.Message = "[ Attempt " + (i).ToString() + " ]   Could not connect to the job server to transfer the results.";
                    log.ErrorMessage = e.Message;
                    EventExtensions.Raise(NewLogItem, new[] { log });
                }

                if ((!successfullySent) && (i < AttemptsToSendResult))
                    // if an error has occured in sending, Wait 1 second and then try to send again
                    System.Threading.Thread.Sleep(3000);
            }

            if (netStream != null)
                netStream.Close();
            client.Close();
            #endregion

            return successfullySent;
        }

        private void AcceptConnectionCallBack(IAsyncResult ar)
        {
            byte[] message = null; // buffer
            byte numberOfJobs = 0;
            byte flags;
            uint payloadSize;
            bool isLittleEndianArchitecture;

            // Accept Connection
            TcpClient client;
            // After invoking TcpListener.Stop(), this method is invoked and we must return from this method
            // if IsListening is false.
            if (IsListening)
                client = _listener.EndAcceptTcpClient(ar);
            else
                return;

            #region Start reciving data from network and store it in buffer

            const int HEADER_JOB_ID_FIELD_SIZE = 8; // 8 Byte
            const int HEADER_JOB_EXECUTABLE_ADDRESS_FIELD_SIZE = 4; // 4 Byte
            const int HEADER_JOB_INPUT_ADDRESS_FIELD_SIZE = 4; // 4 Byte
            const int JOB_HEADER_SIZE = HEADER_JOB_ID_FIELD_SIZE + HEADER_JOB_EXECUTABLE_ADDRESS_FIELD_SIZE + HEADER_JOB_INPUT_ADDRESS_FIELD_SIZE;
            NetworkStream netStream = null;
            bool dataRecivedSuccessfully = true;

            try {
                netStream = client.GetStream();

                // Reads field "Flags"  
                flags = (byte)netStream.ReadByte();
                isLittleEndianArchitecture = (flags == 0) ? true : false;

                // Reads field "Number Of Jobs"  
                numberOfJobs = (byte)netStream.ReadByte();

                // Reads field "Payload Size"
                byte[] payloadSizeBytes = new byte[4];
                netStream.Read(payloadSizeBytes, 0, 4);
                if (!isLittleEndianArchitecture)
                    payloadSizeBytes.Reverse();
                payloadSize = BitConverter.ToUInt32(payloadSizeBytes, 0);
                //

                // Allocate memory for the rest of message (without fileds "Flags", "Number of jobs" and "Payload Size")
                // And read rest of the message from the NetworkStream
                int messageSize = (numberOfJobs * JOB_HEADER_SIZE) + (int)payloadSize;
                message = new byte[messageSize];
                netStream.Read(message, 0, messageSize);
            }
            catch {
                dataRecivedSuccessfully = false;
            }
            finally {
                if (netStream != null)
                    netStream.Close();
                if (client != null)
                    client.Close();
            }

            // Start listening for new connections
            try {
                //_listener.Start(1);
                _listener.BeginAcceptTcpClient(AcceptConnectionCallBack, _listener);
            }
            catch {
                // Generate WorkerCouldNotStartLog log
                WorkerStoppedLog log = new WorkerStoppedLog();
                log.Message = "Worker could not continue to listen for new jobs, so it has stopped.";
                EventExtensions.Raise(NewLogItem, new[] { log });
            }

            #endregion

            #region Create job objects and save theam to the "JobsDirectoryName" directory

            // numberOfJobs = 1
            List<Job> recivedJobs = new List<Job>(10);
            if (dataRecivedSuccessfully)
                for (int i = 0; i < numberOfJobs; i++) {
                    #region Create job object
                    Job newJob = new Job();
                    UInt32 jobExecutableStartLocation;
                    UInt32 jobInputStartLocation;

                    // ID
                    newJob.ID = BitConverter.ToUInt64(message, 0);

                    // InputFileName
                    StringBuilder filename = new StringBuilder(50);
                    DateTime now = DateTime.Now;
                    Random rnd = new Random();

                    filename.Append(JobsDirectoryName);
                    filename.Append("\\job");
                    filename.Append(newJob.ID.ToString());
                    filename.Append("_");
                    filename.Append(now.Hour.ToString("D2"));
                    filename.Append(now.Minute.ToString("D2"));
                    filename.Append(now.Second.ToString("D2"));
                    filename.Append("_");
                    filename.Append(rnd.Next(1000000).ToString("D6"));

                    newJob.InputFileName = filename.ToString() + ".in";

                    // OutputFileName
                    newJob.OutputFileName = filename.ToString() + ".out";

                    // ExecuteableFileName
                    newJob.ExecuteableFileName = filename.ToString() + ".exe";

                    #endregion

                    #region Write job's Executable and input files to disk
                    jobExecutableStartLocation = BitConverter.ToUInt32(message, 8); // in payload address space
                    jobInputStartLocation = BitConverter.ToUInt32(message, 12); // in payload address space
                    jobExecutableStartLocation += 16;
                    jobInputStartLocation += 16;
                    newJob.ExecutableFileLength = jobInputStartLocation - jobExecutableStartLocation;
                    newJob.InputFileLength = (UInt32)message.Length - newJob.ExecutableFileLength - JOB_HEADER_SIZE;

                    System.IO.FileStream fsExe = null;
                    System.IO.FileStream fsInput = null;
                    try {
                        // Create "jobs directory" if it is not exists
                        if (!System.IO.Directory.Exists(JobsDirectoryName))
                            System.IO.Directory.CreateDirectory(JobsDirectoryName);

                        // Create job's executable file
                        fsExe = System.IO.File.Create(newJob.ExecuteableFileName);
                        fsExe.Write(message, (int)jobExecutableStartLocation, (int)newJob.ExecutableFileLength);

                        // Create job's input file
                        fsInput = System.IO.File.Create(newJob.InputFileName);
                        fsInput.Write(message, (int)jobInputStartLocation, (int)newJob.InputFileLength);

                        // Add jobs to the jobs queue
                        lock (_jobsQueue) {
                            _jobsQueue.Enqueue(newJob);
                        }

                        // Add jobs to the recieved jobs list
                        recivedJobs.Add(newJob);
                    }
                    catch (Exception e) {
                        // Error in writing jobs to the disk
                        // Generate log and Raise "NewLogItem" event
                        WorkerAgent.ErrorInWritingJobToDiskLog log = new WorkerAgent.ErrorInWritingJobToDiskLog();
                        log.Job = newJob;
                        log.Message = "Could not write the job to the jobs directory, please verify jobs directory.";
                        log.ErrorMessage = e.Message;
                        EventExtensions.Raise(NewLogItem, new[] { log });
                    }
                    finally {
                        if (fsExe != null)
                            fsExe.Close();
                        if (fsInput != null)
                            fsInput.Close();
                    }
                    #endregion
                }

            // Generate Log
            WorkerAgent.JobsRecivedLog recivelog = new WorkerAgent.JobsRecivedLog();
            ((WorkerAgent.JobsRecivedLog)recivelog).Jobs = recivedJobs.ToArray();
            recivelog.Message = "A batch of jobs recieved.";

            // Raise "NewLogItem" event
            EventExtensions.Raise(NewLogItem, new[] { recivelog });

            #endregion
        }
    }
}
