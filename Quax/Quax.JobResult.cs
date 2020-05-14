using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaxMiddleware
{
    partial class Quax
    {
        public class JobResult
        {
            public UInt64 JobID { set; get; }
            public byte[] Data { set; get; } // Job result data
            public UInt16 WorkerID; // Id of worker that calculated this job
        }
    }

}
