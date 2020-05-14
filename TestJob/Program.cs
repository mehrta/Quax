using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestJob
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.File.Create(args[1]).Close();
            System.Threading.Thread.Sleep(3000);
        }
    }
}
