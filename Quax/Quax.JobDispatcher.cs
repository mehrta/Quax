using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace QuaxMiddleware
{
    public partial class Quax
    {
        class JobDispatcher
        {
            // Types
            class PendingJobRecord
            {
                public Worker Worker { set; get; }
                public List<Job> Jobs { set; get; }
                public DateTime StartTime { set; get; }
                public TimeSpan MaxTimeToComplete { set; get; }
                public TimeSpan ExpectedTimeToComplete { set; get; }

                public PendingJobRecord()
                {
                    Jobs = new List<Job>();
                }
            }

            // Fields
            private Queue<Job> _sendQueue;
            private Queue<Worker> _avialableWorkers;
            private List<Worker> _activeWorkers;  // Workers that are removed from _availableWorkers queue and are dispatching jobs. 
            private List<PendingJobRecord> _pendingJobs;
            private int _numberOfDispatcherThreads;
            private object _numberOfDispatcherThreadsLock;
            private volatile bool _isDispatching;
            private volatile bool _dispatcherThreadsLife;
            private volatile int _jobsPerWorker;
            private bool _shutdown;
            private System.Timers.Timer _periodicPendingJobsCheckTimer;

            // Events
            public event NewLogItemEventHandler NewLogItem;

            // Properties
            public int MaxDispatcherThreads { set; get; }
            public bool IsDispatching
            {
                get
                {
                    return _isDispatching;
                }
            }
            public int SendQueueLength
            {
                get
                {
                    int size =0;
                    lock (_sendQueue)
                        size = _sendQueue.Count;
                    return size;
                }
            }
            // Methods
            public JobDispatcher()
            {
                _numberOfDispatcherThreadsLock = new object();
                _sendQueue = new Queue<Job>(50);
                _avialableWorkers = new Queue<Worker>(10);
                _activeWorkers = new List<Worker>(10);
                _pendingJobs = new List<PendingJobRecord>(50);
                _periodicPendingJobsCheckTimer = new System.Timers.Timer(8000);
                _periodicPendingJobsCheckTimer.Elapsed += new System.Timers.ElapsedEventHandler(_periodicPendingJobsCheckTimer_Elapsed);
                MaxDispatcherThreads = 4;
                _isDispatching = false;
                _dispatcherThreadsLife = true;
            }

            public void AddJob(Job j)
            {
                Job[] arr = new Job[1];
                arr[0] = j;
                AddJobs(arr);
            }

            public void AddJobs(Job[] jobs)
            {
                lock (_sendQueue) {
                    // Add jobs to send queue
                    for (int i = 0; i < jobs.Length; i++)
                        _sendQueue.Enqueue(jobs[i]);

                    //
                    lock (_avialableWorkers)
                        if (_avialableWorkers.Count > 0) {
                            // Recalculate _jobsPerWorker
                            RecalculateJobsPerWorker();
                        }

                    // Notify dipatcher threads about new jobs
                    if (_isDispatching)
                        Monitor.PulseAll(_sendQueue);
                }
            }

            public void ClearJobsQueue()
            {
                lock (_sendQueue)
                    _sendQueue.Clear();
            }

            public void StartDispatch()
            {
                bool firstCall = false; // Is it the first call of this method?

                if (_shutdown)
                    throw new InvalidOperationException("Quax object is shutdown. the object is not usable any more.");

                if (_isDispatching)
                    return;
                _isDispatching = true;

                lock (_numberOfDispatcherThreadsLock)
                    if (_numberOfDispatcherThreads == 0)
                        firstCall = true;

                if (firstCall) {
                    // this Call to StartDispatching() is for first time, we should create dispatcher threads

                    // Recalculate _jobsPerWorker
                    RecalculateJobsPerWorker();

                    // Create one Dispatcher Thread
                    _numberOfDispatcherThreads = 1;
                    CreateNewDispatcherThread();

                    // Enable timer for periodicaly check pending jobs
                    _periodicPendingJobsCheckTimer.Enabled = true;
                }
                else {
                    // Dispatcher threads probably are in a wait state, we should send a signal to them to wake up
                    lock (_sendQueue)
                        Monitor.PulseAll(_sendQueue);
                }
            }

            public void StopDispatch()
            {
                _isDispatching = false;
            }

            public void Shutdown()
            {
                if (_shutdown)
                    return;
                _shutdown = true;
                _dispatcherThreadsLife = false;
                lock (_sendQueue) {
                    Monitor.PulseAll(_sendQueue);
                }
                _periodicPendingJobsCheckTimer.Close();
            }

            public void JobResultRecived(Quax.JobResult result)
            {
                // This method is invoked by "Quax" object whenever a result recived from a worker
                // "Quax" object is informed by "JobResultReciver" object

                // Update pending jobs table
                PendingJobRecord record = null;
                Job pendingJob = null;

                lock (_pendingJobs) {
                    for (int i = 0; i < _pendingJobs.Count; i++)
                        if (_pendingJobs[i].Worker.ID == result.WorkerID)
                            for (int j = 0; j < _pendingJobs[i].Jobs.Count; j++)
                                if (_pendingJobs[i].Jobs[j].ID == result.JobID) {
                                    record = _pendingJobs[i];
                                    pendingJob = _pendingJobs[i].Jobs[j];
                                }

                    if (pendingJob != null) {
                        // Pending job found, we should remove it from table and update its fields
                        record.Jobs.Remove(pendingJob);
                        record.ExpectedTimeToComplete -= pendingJob.ExpectedTimeToComplete;
                        record.MaxTimeToComplete -= pendingJob.MaxTimeToComplete;

                        // If record has no associated job, we can remove it from table 
                        if (record.Jobs.Count == 0)
                            _pendingJobs.Remove(record);
                    }
                    /* If (pendingJob == null) ===> pending job is removed in the past (2 job result
                          is recived for 1 unique job by to diffrent worker!) */
                }
            }

            private void _periodicPendingJobsCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                lock (_pendingJobs)
                    for (int i = 0; i < _pendingJobs.Count; i++) {
                        PendingJobRecord record = _pendingJobs[i];

                        if ((DateTime.Now - record.StartTime) > record.MaxTimeToComplete) {
                            // "_pendingJobs[i].Worker" is unresponsive!
                            // We should restore it's associated jobs to the send queue
                            // and remove this worker from _availableWorkers (if it is currently in it)
                            // if it is not in _availableWorkers, so it is being used by a dispatcher thread, we should flag it
                            // to be removed from _availableWorkers.

                            // Restore jobs to the send queue
                            AddJobs(record.Jobs.ToArray());

                            // Remove record
                            _pendingJobs.Remove(record);

                            //
                            record.Worker.IsUnresponsive = true;

                            // Generate UnresponsiveWorkerLog
                        }
                    }
            }

            private void JobDispatchLoop()
            {
                const int MAX_PAYLOAD_SIZE = 4 * 1024 * 1024; // 4 Megabytes
                bool threadShouldContinue = true;
                bool readyToDispatch = false;
                Worker selectedWorker = null;
                List<Job> selectedJobs = new List<Job>(16);
                Job[] selectedJobsArray;
                long selectedJobsTotalSize = 0;

                while (threadShouldContinue && _dispatcherThreadsLife) {
                    readyToDispatch = false;
                    lock (_sendQueue) {
                        lock (_avialableWorkers) {
                            // Remove Unresponsive workers from the _available queue
                            bool doMore = true;
                            while ((_avialableWorkers.Count > 0) && doMore)
                                if (_avialableWorkers.Peek().IsUnresponsive)
                                    _avialableWorkers.Dequeue();
                                else
                                    doMore = false;
                            //

                            if ((_sendQueue.Count > 0) && (_avialableWorkers.Count > 0) && _isDispatching) {
                                // Select a worker and some jobs to dispatch to it
                                selectedWorker = _avialableWorkers.Dequeue();
                                _activeWorkers.Add(selectedWorker);

                                #region Select jobs
                                // Maximum number of jobs (per worker) = 256
                                // Maximum size of jobs: 16 Mega Bytes = 16*1024*1024 Bytes
                                selectedJobs.Clear(); // Cleare jobs in the queue (from last dispatch)
                                while ((selectedJobs.Count < _jobsPerWorker) && (_sendQueue.Count > 0)) {
                                    Job j;
                                    long jobTotalLength = 0;

                                    j = _sendQueue.Dequeue();
                                    try {
                                        jobTotalLength = j.GetTotalLength();
                                    }
                                    catch {
                                        // Error in reading job executable file info
                                        //JobExecutableFileReadErrorLog log;
                                    }

                                    // Check length of job
                                    if (jobTotalLength > MAX_PAYLOAD_SIZE) {
                                        // Invalid Job
                                        Log.JobTooBigLog log = new Log.JobTooBigLog();
                                        log.Message = "Total length of executable file and input file of the job is greater than 4 MB, this job will not send to workers.";
                                        log.Job = j;
                                        NewLogItem(log);
                                    }
                                    else if ((selectedJobsTotalSize + jobTotalLength) <= MAX_PAYLOAD_SIZE) {
                                        // Add job
                                        selectedJobsTotalSize += jobTotalLength;
                                        selectedJobs.Add(j);
                                    }
                                    else {
                                        // There is no space for other jobs, restore job to the send queue and break loop
                                        _sendQueue.Enqueue(j);
                                        break;
                                    }
                                }

                                if (selectedJobs.Count > 0)
                                    readyToDispatch = true;
                                #endregion

                                #region Control number of dispatcher threads
                                if (_numberOfDispatcherThreads > _avialableWorkers.Count && _numberOfDispatcherThreads > 1) {
                                    // This Thread should terminate after this iteration (after sending the job)
                                    threadShouldContinue = false;
                                    _numberOfDispatcherThreads--;
                                }
                                else if (_numberOfDispatcherThreads < _avialableWorkers.Count && _numberOfDispatcherThreads < MaxDispatcherThreads) {
                                    // Create a new dispatcher thread
                                    CreateNewDispatcherThread();
                                }
                                #endregion
                            } // end of if
                        } // end of lock(_availableWorkers)

                        if (!readyToDispatch && threadShouldContinue)
                            Monitor.Wait(_sendQueue, 2000);
                    } // end of lock(_sendQueue)

                    if (readyToDispatch) {
                        #region Start dispatching jobs
                        bool successfullySent;
                        bool networkSendError;

                        //Start dispatching selected jobs to worker "selectedWorker"
                        selectedJobsArray = selectedJobs.ToArray();
                        successfullySent = DispatchJobs(selectedJobsArray, selectedWorker, (uint)selectedJobsTotalSize, out networkSendError);

                        // Restore "selectedWorker" to available worekrs ((if it was working properly))
                        _activeWorkers.Remove(selectedWorker); // "selectedWorker" is not active any more
                        if (successfullySent || (!successfullySent && !networkSendError))
                            lock (_avialableWorkers)
                                _avialableWorkers.Enqueue(selectedWorker);

                        if (successfullySent) {
                            #region Create or update pending job record
                            PendingJobRecord record = null;

                            lock (_pendingJobs) {
                                // Try to find a pending job record 
                                for (int i = 0; i < _pendingJobs.Count; i++)
                                    if (_pendingJobs[i].Worker.ID == selectedWorker.ID) {
                                        record = _pendingJobs[i];
                                        break;
                                    }

                                // Record not found, we must create one new recorde
                                if (record == null) {
                                    record = new PendingJobRecord();
                                    record.Worker = selectedWorker;
                                    record.StartTime = DateTime.Now;
                                    _pendingJobs.Add(record);
                                }

                                // Update the record
                                //TimeSpan expectedTimeIncrease = TimeSpan.Zero;
                                //TimeSpan MaxTimeIncrease = TimeSpan.Zero;
                                for (int i = 0; i < selectedJobsArray.Length; i++) {
                                    record.Jobs.Add(selectedJobsArray[i]);
                                    record.ExpectedTimeToComplete = record.ExpectedTimeToComplete.Add(selectedJobsArray[i].ExpectedTimeToComplete);
                                    record.MaxTimeToComplete = record.MaxTimeToComplete.Add(selectedJobsArray[i].MaxTimeToComplete);
                                }
                            }
                            #endregion
                        }
                        else {
                            // Send failed, we should restore jobs to send queue
                            AddJobs(selectedJobsArray);
                        }

                        #endregion
                    }
                } // End of while
            }

            private bool DispatchJobs(Job[] jobs, Worker w, uint JobsTotalSize, out bool networkSendError)
            {
                networkSendError = false;

                #region Load jobs into "message"
                // Define size of header's fields
                const int HEADER_FLAGS_FIELD_SIZE = 1; // 1 Byte
                const int HEADER_NUMBER_OF_JOBS_FIELD_SIZE = 1; // 1 Byte 
                const int HEADER_PAYLOAD_SIZE_FIELD_SIZE = 4; // 4 Byte, UInt32
                const int HEADER_JOB_ID_FIELD_SIZE = 8; // 8 Byte
                const int HEADER_JOB_EXECUTABLE_ADDRESS_FIELD_SIZE = 4; // 4 Byte
                const int HEADER_JOB_INPUT_ADDRESS_FIELD_SIZE = 4; // 4 Byte
                const int JOB_HEADER_SIZE = HEADER_JOB_ID_FIELD_SIZE + HEADER_JOB_EXECUTABLE_ADDRESS_FIELD_SIZE + HEADER_JOB_INPUT_ADDRESS_FIELD_SIZE;
                const int FIRST_JOB_HEADER_START_LOCATION = HEADER_FLAGS_FIELD_SIZE + HEADER_NUMBER_OF_JOBS_FIELD_SIZE + HEADER_PAYLOAD_SIZE_FIELD_SIZE;

                // Allocate memory for the message 
                long messageSize;
                messageSize = HEADER_NUMBER_OF_JOBS_FIELD_SIZE + HEADER_PAYLOAD_SIZE_FIELD_SIZE + HEADER_FLAGS_FIELD_SIZE +
                    (jobs.Length * JOB_HEADER_SIZE) + JobsTotalSize;
                byte[] message = new byte[messageSize];
                //

                // Set some fields of the message
                // Flags
                message[0] = (byte)(BitConverter.IsLittleEndian ? 0 : 1);  // 0: Little Endian   1: Big Endian
                // Number of Jobs field
                message[1] = (byte)jobs.Length;
                // Pyaload size
                byte[] payloadSizeBytes = BitConverter.GetBytes(JobsTotalSize);
                if (BitConverter.IsLittleEndian) {
                    message[2] = payloadSizeBytes[0];
                    message[3] = payloadSizeBytes[1];
                    message[4] = payloadSizeBytes[2];
                    message[5] = payloadSizeBytes[3];
                }
                else {
                    // Big Endian Machine
                    message[2] = payloadSizeBytes[3];
                    message[3] = payloadSizeBytes[2];
                    message[4] = payloadSizeBytes[1];
                    message[5] = payloadSizeBytes[0];
                }
                //

                // Write jobs to the message
                uint payloadAddressSpaceCurrentWriteIndex = 0;
                int messageAddressSpaceCurrentWriteIndex = FIRST_JOB_HEADER_START_LOCATION + (jobs.Length * JOB_HEADER_SIZE);


                for (int i = 0; i < jobs.Length; i++) {
                    // Open executable file of job
                    System.IO.FileStream fs = System.IO.File.OpenRead(jobs[i].ExecuteableFileName);

                    // Calculate start location of "job header" record
                    int jobHeaderBase = FIRST_JOB_HEADER_START_LOCATION + (i * JOB_HEADER_SIZE);

                    // Write "Job ID" field
                    byte[] jobIDBytes = BitConverter.GetBytes(jobs[i].ID);
                    for (int j = 0; j < 8; j++)
                        message[jobHeaderBase + j] = jobIDBytes[j];

                    // Write "Address (in Payload Address Space) of Executable File" field
                    byte[] jobExecutableStartAddressBytes = BitConverter.GetBytes(payloadAddressSpaceCurrentWriteIndex);
                    for (int j = 8; j < 12; j++)
                        message[jobHeaderBase + j] = jobExecutableStartAddressBytes[j - 8];

                    // Write "Job Executable File" bytes
                    fs.Read(message, (int)messageAddressSpaceCurrentWriteIndex, (int)fs.Length);
                    // Increase write indexes
                    payloadAddressSpaceCurrentWriteIndex += (uint)fs.Length;
                    messageAddressSpaceCurrentWriteIndex += (int)fs.Length;
                    fs.Close();
                    //

                    // Write "Address of Input File" field
                    byte[] jobInputStartAddressBytes = BitConverter.GetBytes(payloadAddressSpaceCurrentWriteIndex);
                    for (int j = 12; j < 16; j++)
                        message[jobHeaderBase + j] = jobInputStartAddressBytes[j - 12];

                    // Write "Job Input File" bytes
                    for (int j = 0; j < jobs[i].InputFileContent.Length; j++) {
                        message[messageAddressSpaceCurrentWriteIndex] = jobs[i].InputFileContent[j];
                        messageAddressSpaceCurrentWriteIndex++;
                    }
                    payloadAddressSpaceCurrentWriteIndex += (uint)jobs[i].InputFileContent.Length;

                } // End of for
                #endregion

                #region Send message to the worekr
                TcpClient client = new TcpClient();
                NetworkStream netStream = null;
                bool successfullySent = false;

                for (int i = 0; (i < 3) && !successfullySent; i++) {
                    try {
                        client.Connect(w.IP, w.DataChannelPortNumber);
                        netStream = client.GetStream();
                        netStream.Write(message, 0, (int)messageSize);
                        successfullySent = true;

                        // Generate "JobSentLog" Log
                        Log.BatchOfJobsSentLog log = new Log.BatchOfJobsSentLog();
                        log.Message = "A batch of jobs sent to one worker.";
                        log.TargetWorker = w;
                        log.Jobs = jobs;
                        NewLogItem(log);
                    }
                    catch (Exception e) {
                        networkSendError = true;

                        // Generate "ErrorInConnectingToWorkerLog" Log
                        Log.ErrorInSendingJobToWorkerLog log = new Log.ErrorInSendingJobToWorkerLog();
                        log.Message = "An error has occured in connecting to the worker.";
                        log.ErrorMessage = e.Message;
                        log.TargetWorker = w;
                        NewLogItem(log);
                    }
                }

                // Close connection
                if (netStream != null)
                    netStream.Close();
                client.Close();
                #endregion

                return successfullySent;
            }


            private void CreateNewDispatcherThread()
            {
                Thread t = new Thread(JobDispatchLoop);
                t.Name = "JobDispatcher";
                t.IsBackground = true;
                t.Start();
            }

            private void RecalculateJobsPerWorker()
            {
                _jobsPerWorker = 1;
                //_jobsPerWorker = (int)Math.Ceiling((decimal)_sendQueue.Count / _avialableWorkers.Count);
                //if (_jobsPerWorker > 255)
                //    _jobsPerWorker = 255;
            }

            public void HandleWorkerRegistrationRequest(Worker sender)
            {
                bool workerFound = false;
                lock (_avialableWorkers) {
                    // Search for worker in _availableWorkers
                    for (int i = 0; (i < _avialableWorkers.Count) && (!workerFound); i++)
                        if (_avialableWorkers.ElementAt(i).ID == sender.ID)
                            workerFound = true;

                    // Search for worker in _activeWorkers
                    for (int i = 0; (i < _activeWorkers.Count) && (!workerFound); i++)
                        if (_activeWorkers[i].ID == sender.ID)
                            workerFound = true;

                    if (!workerFound) {
                        // Registration is from a new worker, we should add this worker to "_avialableWorkers" queue
                        _avialableWorkers.Enqueue(sender);
                    }
                }

                lock (_sendQueue)
                    Monitor.Pulse(_sendQueue);
            }

            //public bool WorkerExist(UInt16 workerID)
            //{
            //    bool workerFound = false;
            //    lock (_avialableWorkers) {
            //        // Search for worker in _availableWorkers
            //        for (int i = 0; (i < _avialableWorkers.Count) && (!workerFound); i++)
            //            if (_avialableWorkers.ElementAt(i).ID == workerID)
            //                workerFound = true;

            //        // Search for worker in _activeWorkers
            //        for (int i = 0; (i < _activeWorkers.Count) && (!workerFound); i++)
            //            if (_activeWorkers[i].ID == workerID)
            //                workerFound = true;
            //    }

            //    return workerFound;
            //}

        }
    }
}

