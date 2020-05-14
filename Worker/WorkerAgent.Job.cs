using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worker
{
    partial class WorkerAgent
    {
        public class Job
        {
            // Properties
            public ulong ID { get; set; }
            public string ExecuteableFileName { set; get; }
            public string InputFileName { set; get; }
            public string OutputFileName { set; get; }
            public UInt32 ExecutableFileLength { set; get; }
            public UInt32 InputFileLength { set; get; }
        }
    }
}
