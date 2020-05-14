using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worker
{
    partial class WorkerAgent
    {
        public class Log
        {
            public string Message { set; get; }

            public Log()
            {
                Message = "";
            }
        }

        public class WorkerStartedLog : Log
        {

        }

        public class WorkerStoppedLog : Log
        {

        }

        public class WorkerCouldNotStartLog : Log
        {
            public string ErrorMessage { set; get; }
        }

        public class WorkerCouldNotStopLog : Log
        {
            public string ErrorMessage { set; get; }
        }

        public class JobsRecivedLog : Log
        {
            public Job[] Jobs { set; get; }
        }

        public class JobResultSentLog : Log
        {
            public Job Job { set; get; }
        }

        public class ErrorInConnectingToJobServerLog : Log
        {
            public string ErrorMessage { set; get; }
        }

        public class ErrorInWritingJobToDiskLog : Log
        {
            public Job Job { set; get; }
            public string ErrorMessage { set; get; }
        }

        public class ErrorInJobExecutionLog : Log
        {
            public Job Job { set; get; }
            public bool IsWin32Error { set; get; }
            public int Win32ErrorCode { set; get; }
            public string ErrorMessage { set; get; }
        }

        public class JobExecutionStartedLog : Log
        {
            public Job Job { set; get; }
        }

        public class ErrorInReadingJobOutputFile : Log
        {
            public Job Job { set; get; }
            public string ErrorMessage { set; get; }
        }

        public class JobExecutedSuccessfullyLog : Log
        {
            public Job Job { set; get; }
        }

        public class WorkerRegisterdLog : Log
        {

        }

        public class WorkerRegisterationStartLog : Log
        {

        }

        public class WorkerRegisterationFailedLog : Log
        {
        }

    }
}
