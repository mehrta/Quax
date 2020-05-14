using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Worker
{
    public class WorkerSettings
    {
        public bool AutoStartListening { set; get; }
        public WorkerAgent Worker { set; get; }

        public void LoadFromFile(string fullpath)
        {
            System.IO.StreamReader sr = null;

            try {
                sr = new System.IO.StreamReader(fullpath);

                while (!sr.EndOfStream) {
                    string line;
                    string[] lineParts;
                    string key, value;

                    line = sr.ReadLine();
                    lineParts = line.Split('=');
                    key = lineParts[0].Trim().ToLower();
                    value = lineParts[1].Trim().ToLower();

                    switch (key) {
                        case "workerid":
                            Worker.ID = UInt16.Parse(value);
                            break;
                        case "jobserveripaddress":
                            Worker.JobServerIPAddress = IPAddress.Parse(value);
                            break;
                        case "autostartlistening":
                            AutoStartListening = (value == "true" ? true : false);
                            break;
                        case "listendatachannelportnumber":
                            Worker.ListenDataChannelPortNumber = Int32.Parse(value);
                            break;
                        case "listencontrolchannelportnumber":
                            Worker.ListenControlChannelPortNumber = Int32.Parse(value);
                            break;
                        case "jobserverdatachannelportnumber":
                            Worker.JobServerDataChannelPortNumber = Int32.Parse(value);
                            break;
                        case "jobservercontrolchannelportnumber":
                            Worker.JobServerControlChannelPortNumber = Int32.Parse(value);
                            break;
                    }

                }
            }
            catch {
                LoadDefaults();
            }
            finally {
                if (sr != null)
                    sr.Close();
            }

        }

        public void SaveToFile(string fullpath)
        {
            System.IO.StreamWriter sw = null;

            try {
                sw = new System.IO.StreamWriter(fullpath);

                // Line 1 : WorkerID
                sw.WriteLine("WorkerID=" + Worker.ID.ToString());

                // Line 2 : JobServerIPAddress
                sw.WriteLine("JobServerIPAddress=" + Worker.JobServerIPAddress.ToString());
                
                // Line 3 : AutoStartListening
                sw.WriteLine("AutoStartListening=" + (AutoStartListening ? "True" : "False"));

                // Line 4 : ListenDataChannelPortNumber
                sw.WriteLine("ListenDataChannelPortNumber=" + Worker.ListenDataChannelPortNumber.ToString());

                // Line 5 : ListenControlChannelPortNumber
                sw.WriteLine("ListenControlChannelPortNumber=" + Worker.ListenControlChannelPortNumber.ToString());

                // Line 6 : JobServerDataChannelPortNumber
                sw.WriteLine("JobServerDataChannelPortNumber=" + Worker.JobServerDataChannelPortNumber.ToString());

                // Line 7 : JobServerControlChannelPortNumber
                sw.WriteLine("JobServerControlChannelPortNumber=" + Worker.JobServerControlChannelPortNumber.ToString());
            }
            catch {
            }
            finally {
                if (sw != null)
                    sw.Close();
            }
        }

        public void LoadDefaults()
        {
            AutoStartListening = false;
            Worker.ID = 1;
            Worker.JobServerDataChannelPortNumber = 40000;
            Worker.JobServerControlChannelPortNumber = 40001;
            Worker.ListenDataChannelPortNumber = 20000;
            Worker.ListenControlChannelPortNumber = 20001;
            Worker.JobServerIPAddress = IPAddress.Loopback;
        }

        public static void SetRegistryProgramAutoStartKey(bool autoStart)
        {

        }
    }
}
