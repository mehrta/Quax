using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaxMiddleware
{
    public partial class Quax
    {

        public class Job
        {
            // Fields
            private static ulong _id = 0;

            // Properties

            public ulong ID { get; set; }
            public string ExecuteableFileName { set; get; }
            public byte[] InputFileContent { set; get; }
            public TimeSpan ExpectedTimeToComplete { set; get; }
            public TimeSpan MaxTimeToComplete { set; get; }

            // Methods
            public Job()
            {
                // Auto Generate ID 
                _id++;
                ID = _id;
                if (_id == ulong.MaxValue)
                    _id = 0;

                //
                ExpectedTimeToComplete = TimeSpan.FromSeconds(30);
                MaxTimeToComplete = TimeSpan.FromSeconds(60);
            }

            public long GetTotalLength()
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(ExecuteableFileName);
                return (fi.Length + InputFileContent.LongLength);
            }
        }
    }
}
