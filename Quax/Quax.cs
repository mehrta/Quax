using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaxMiddleware
{
    partial class Quax
    {
        // Types

        public delegate void JobResultReciveEventHandler(JobResult result);
        public delegate void NewLogItemEventHandler(Log log);

        // Fields
        private JobDispatcher _jobDispatcher;
        private JobResultReciver _jobResultReciver;
        private ControlUnit _controlUnit;

        // Events
        public event JobResultReciveEventHandler JobResultRecive;
        public event NewLogItemEventHandler NewLogItem;

        // Properties
        public int MaxDispatcherThreads { set; get; }
        public int DataChannelPortNumber // Job server listens to this port for incoming job results.
        {
            set
            {
                _jobResultReciver.DataChannelPortNumber = value;
            }
            get
            {
                return _jobResultReciver.DataChannelPortNumber;
            }

        }

        public int ControlChannelPortNumber // Job server listens to this port for incoming control messages.
        {
            set
            {
                _controlUnit.ControlChannelPortNumber = value;
            }
            get
            {
                return _controlUnit.ControlChannelPortNumber;
            }
        }
        public int SendQueueLength
        {
            get
            {
                return _jobDispatcher.SendQueueLength;
            }
        }
        public bool IsDispatching
        {
            get
            {
                return _jobDispatcher.IsDispatching;
            }
        }

        public Version AssemblyVersion
        {
            get
            {
                System.Reflection.Assembly assem = System.Reflection.Assembly.GetExecutingAssembly();
                System.Reflection.AssemblyName assemName = assem.GetName();
                return assemName.Version;
            }
        }

        // Methods
        public Quax()
        {
            _jobDispatcher = new JobDispatcher();
            _jobResultReciver = new JobResultReciver();
            _controlUnit = new ControlUnit();

            // Default values
            MaxDispatcherThreads = 5;
            DataChannelPortNumber = 40000;
            ControlChannelPortNumber = 40001;

            // Event handlers
            _jobDispatcher.NewLogItem += new NewLogItemEventHandler(_jobDispatcher_OnNewLogItem);
            _jobResultReciver.JobResultRecive += new JobResultReciveEventHandler(_jobResultReciver_OnJobResultRecive);
            _controlUnit.NewLogItem += new NewLogItemEventHandler(_controlUnit_OnNewLogItem);

            //
            _controlUnit.JobDispatcher = _jobDispatcher;
        }

        void _controlUnit_OnNewLogItem(Quax.Log log)
        {
            // Inform JobDispatcher for this event
            if (log is Log.WorkerRegisterationRequestLog)
                _jobDispatcher.HandleWorkerRegistrationRequest(((Log.WorkerRegisterationRequestLog)log).Sender);

            // Raise "Quax.JobResultRecive" event
            EventExtensions.Raise(NewLogItem, new object[] { log });
        }

        void _jobResultReciver_OnJobResultRecive(Quax.JobResult result)
        {
            // Inform JobDispatcher for this event
            _jobDispatcher.JobResultRecived(result);

            // Raise "Quax.JobResultRecive" event
            EventExtensions.Raise(JobResultRecive, new object[] { result });
        }

        void _jobDispatcher_OnNewLogItem(Quax.Log l)
        {
            // Raise "Quax.NewLogItem" event
            EventExtensions.Raise(NewLogItem, new object[] { l });
        }

        /// <summary>
        /// Adds an job to the end of the jobs queue.
        /// </summary>
        /// <param name="j">The job to be added to the end of the jobs queue.</param>
        public void AddJob(Job j)
        {
            _jobDispatcher.AddJob(j);
        }

        /// <summary>
        /// Adds the elements of an array of jobs to the end of the send queue.
        /// </summary>
        /// <param name="jobs">The jobs array whose elements should be added to the end of the jobs queue.</param>
        public void AddJobs(Job[] jobs)
        {
            _jobDispatcher.AddJobs(jobs);
        }

        /// <summary>
        /// Removes all jobs from send queue.
        /// </summary>
        public void ClearJobsQueue()
        {
            _jobDispatcher.ClearJobsQueue();
        }

        /// <summary>
        /// Starts the process of dispatching jobs to the workers for computing.
        /// </summary>
        /// <exception cref="">
        /// InvalidOperationException
        /// </exception>
        public void StartDispatch()
        {
            _jobResultReciver.DataChannelPortNumber = this.DataChannelPortNumber;

            try {
                _jobResultReciver.Start();  // This method may throw exception.
                _controlUnit.Start(); // This method may throw exception.
                _jobDispatcher.StartDispatch();

                // Generate JobDispatcherStartedLog Log
                Log.JobDispatcherStartedLog log = new Log.JobDispatcherStartedLog();
                log.Message = "Job dispatcher started.";
                EventExtensions.Raise(NewLogItem, new object[] { log });
            }
            catch (Exception e) {
                // Generate JobDispatcherCouldNotStartLog Log
                Log.JobDispatcherCouldNotStartLog log = new Log.JobDispatcherCouldNotStartLog();
                log.Message = "Job dispatcher could not start.";
                log.ErrorMessage = e.Message;
                EventExtensions.Raise(NewLogItem, new object[] { log });
            }
        }

        /// <summary>
        /// Stops the process of dispatching jobs. the process can be resumed by calling StartDispatch().
        /// </summary>
        public void StopDispatch()
        {
            _controlUnit.Stop();
            _jobResultReciver.Stop();
            _jobDispatcher.StopDispatch();

            // Generate JobDispatcherStoppedLog Log
            Log.JobDispatcherStoppedLog log = new Log.JobDispatcherStoppedLog();
            log.Message = "Stop request sent to the job dispatcher.";
            EventExtensions.Raise(NewLogItem, new object[] { log });
        }

        public void Shutdown()
        {
            _jobDispatcher.Shutdown();
            _jobResultReciver.Stop();

            // Generate JobDispatcherShutdownLog Log
            Log.JobDispatcherShutdownLog log = new Log.JobDispatcherShutdownLog();
            log.Message = "Shutdown request sent to the job dispatcher.";
            EventExtensions.Raise(NewLogItem, new object[] { log });
        }

        ~Quax()
        {
            _jobDispatcher.Shutdown();
        }

    }

}
