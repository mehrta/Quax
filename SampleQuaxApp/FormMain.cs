using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuaxMiddleware;

namespace SampleQuaxApp
{
    public partial class FormMain : Form
    {
        Quax _quax;
        String _jobExecutableFilePath = "";
        String _jobInputFilePath = "";
        int _createdJobs = 0;
        int _doneJobs = 0;

        public FormMain()
        {
            InitializeComponent();

            // Initialize Quax Object
            _quax = new Quax();
            _quax.NewLogItem += new Quax.NewLogItemEventHandler(_quax_OnNewLogItem);
            _quax.JobResultRecive += new Quax.JobResultReciveEventHandler(_quax_OnJobResultRecive);
            _quax.ControlChannelPortNumber = 40001;
            _quax.DataChannelPortNumber = 40000;
        }

        void _quax_OnJobResultRecive(Quax.JobResult result)
        {
            Color timeColor = Color.Green;
            Color messageColor = Color.Blue;
            AppendText(DateTime.Now.ToLongTimeString() + "  ", Color.Green);
            AppendText("Result of a job received.",messageColor);
            rtbLogs.AppendText(Environment.NewLine);

            //Job ID:" + result.JobID + "   (from worker "+ result.WorkerID.ToString()+ ")"
            //    , );
            //rtbLogs.AppendText(Environment.NewLine);
        }

        void _quax_OnNewLogItem(Quax.Log log)
        {
            Color timeColor = Color.Green;
            Color messageColor = Color.Blue;
            Color errorMessageColor = Color.Red;
            const int INDENT_L1 = 20; // Level 1 indent size = 20;

            AppendText(DateTime.Now.ToLongTimeString() + "  ", timeColor);

            if (log is Quax.Log.JobDispatcherStartedLog) {
                #region JobDispatcherStartedLog
                AppendText(log.Message, messageColor);
                ChangeUIByJobServerStatus(true);

                // Start timer
                timCreateJob.Interval = (int)numJobsCreateInterval.Value;
                timCreateJob.Start();

                #endregion
            }
            else if (log is Quax.Log.JobDispatcherStoppedLog) {
                #region JobDispatcherStoppedLog

                AppendText(log.Message, messageColor);
                ChangeUIByJobServerStatus(false);
                
                // Stop timer
                timCreateJob.Stop();

                #endregion
            }
            else if (log is Quax.Log.BatchOfJobsSentLog) {
                #region BatchOfJobsSentLog
                AppendText(log.Message, messageColor);
                #endregion
            }
            else if (log is Quax.Log.WorkerRegisterationRequestLog) {
                #region BatchOfJobsSentLog
                Quax.Log.WorkerRegisterationRequestLog l = log as Quax.Log.WorkerRegisterationRequestLog;
                
                AppendText(log.Message, Color.Black);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Worker ID: " + l.Sender.ID.ToString() , Color.Black, INDENT_L1);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Worker IP: " + l.Sender.IP.ToString() , Color.Black, INDENT_L1);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Worker Data Channel Port Number: " + l.Sender.DataChannelPortNumber.ToString() , Color.Black, INDENT_L1);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Worker Control Channel Port Number: " + l.Sender.ControlChannelPortNumber.ToString() , Color.Black, INDENT_L1);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("--------------------------", Color.Black);
                rtbLogs.AppendText(Environment.NewLine);
                
                #endregion
            }
            else  {
                AppendText(log.Message, messageColor);
            }

            lblSendQueueJobs.Text = _quax.SendQueueLength.ToString();

            // Move cursor to next line of rtbLogs
            rtbLogs.AppendText(Environment.NewLine);
            rtbLogs.ScrollToCaret();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Quax.Log readyLog = new Quax.Log();
            readyLog.Message = "Ready.";
            _quax_OnNewLogItem(readyLog);
        }

        private void timCreateJob_Tick(object sender, EventArgs e)
        {
            
            // Create a job
            Quax.Job j = new Quax.Job();
            j.ExecuteableFileName = txtExecutablePath.Text;
            j.InputFileContent = System.IO.File.ReadAllBytes(txtInputPath.Text);
            j.ExpectedTimeToComplete = TimeSpan.FromSeconds((double)numJobExpectedTime.Value);
            j.MaxTimeToComplete = TimeSpan.FromSeconds((double)numJobMaxTime.Value);

            _quax.AddJob(j);
            _createdJobs++;

            lblSendQueueJobs.Text = _quax.SendQueueLength.ToString();

            if (_createdJobs == (int)numJobMaximumCreate.Value) {
                timCreateJob.Enabled = false;
            }
        }

        private void ChangeUIByJobServerStatus(bool jobServerStarted)
        {
            if (jobServerStarted) {
                lblStatus.Text = "Started.";
                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
                grpJob.Enabled = false;
            }
            else {
                lblStatus.Text = "Stopped";
                startToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
                grpJob.Enabled = true;
            }
        }

        private void AppendText(string str, Color TextColor, int indent = 0)
        {
            int startIndex = rtbLogs.TextLength;
            rtbLogs.AppendText(str);
            rtbLogs.Select(startIndex, str.Length);
            rtbLogs.SelectionColor = TextColor;
            rtbLogs.SelectionIndent = indent;
            rtbLogs.DeselectAll();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check job xecutable file
            if (txtExecutablePath.Text.Trim() == "") {
                MessageBox.Show("Please select executable file of the job.", "Select Job Executable");
                txtExecutablePath.Focus();
                return;
            }
            if (!System.IO.File.Exists(txtExecutablePath.Text)) {
                MessageBox.Show("Path of Job executable file is incorect or file does not exist.", "Select Job Executable File");
                txtInputPath.Focus();
                return;
            }

            // Check job input file
            if (txtInputPath.Text.Trim() == "") {
                MessageBox.Show("Please select input file of the job.", "Select Job Executable");
                return;
            }
            if (!System.IO.File.Exists(txtExecutablePath.Text)) {
                MessageBox.Show("Path of Job input file is incorect or file does not exist.", "Select Job Input File");
                return;
            }
            //

            // Initialize variables
            _createdJobs = 0;

            // Start Dispatching
            _quax.StartDispatch();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _quax.StopDispatch();
        }

        private void btnBrowseJobExecutable_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select Job Executable File";
            openFileDialog1.ShowDialog();
            txtExecutablePath.Text = openFileDialog1.FileName;
        }

        private void btnBrowseJobInput_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select Job Input File";
            openFileDialog1.ShowDialog();
            txtInputPath.Text = openFileDialog1.FileName;
        }

        private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbLogs.Text = "";
        }
    }
}
