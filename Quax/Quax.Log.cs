using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaxMiddleware
{
    partial class Quax
    {
        public class Log
        {
            public string Message { set; get; }

            public class JobDispatcherStartedLog : Log
            {
            }

            public class JobDispatcherCouldNotStartLog : Log
            {
                public string ErrorMessage { set; get; }
            }

            public class JobDispatcherStoppedLog : Log
            {
            }

            public class JobDispatcherShutdownLog : Log
            {
            }

            public class BatchOfJobsSentLog : Log
            {
                public Job[] Jobs { set; get; }
                public Worker TargetWorker { set; get; }
            }

            public class JobTooBigLog : Log
            {
                public Job Job { set; get; }
            }

            public class JobExecutableFileReadErrorLog : Log
            {
            }

            public class UnresponsiveWorkerLog : Log
            {
            }

            public class WorkerRegisterationRequestLog : Log
            {
                public Worker Sender { set; get; }
            }

            public class ErrorInSendingJobToWorkerLog : Log
            {
                public string ErrorMessage { set; get; }
                public Worker TargetWorker { set; get; }
            }

        }



    }
}
