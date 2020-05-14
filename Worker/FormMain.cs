using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Worker
{
    public partial class FormMain : Form
    {
        private struct JobsStatistics
        {
            public int RecivedJobs;
            public int DoneJobs;
            public int FailedJobs;
        }

        private JobsStatistics statistics;
        public WorkerAgent WorkerAgentObject { set; get; }
        public WorkerSettings WorkerSettingsObject { set; get; }
        public string ConfigFileFullPath { set; get; }

        public FormMain()
        {
            InitializeComponent();

            // Initialize worker agent
            WorkerAgentObject = new WorkerAgent();
            WorkerAgentObject.JobsDirectoryName = Application.StartupPath + "\\Jobs";
            WorkerAgentObject.NewLogItem += new WorkerAgent.NewLogItemEventHandler(WorkerAgentObject_NewLogItem);

            //
            ConfigFileFullPath = Application.StartupPath + "\\config.txt";
            WorkerSettingsObject = new Worker.WorkerSettings();
            WorkerSettingsObject.Worker = WorkerAgentObject;
        }

        void WorkerAgentObject_NewLogItem(WorkerAgent.Log log)
        {
            // This event handler is invoked in the context of UI thread.

            // Worker Start/Stop status check
            if ((log is WorkerAgent.WorkerStartedLog)) {
                // Change UI
                ChangeUIByWorkerAgentStatus(true);
            }
            else if (log is WorkerAgent.WorkerStoppedLog) {
                // Change UI
                ChangeUIByWorkerAgentStatus(false);
            }
            else if (log is WorkerAgent.JobResultSentLog) {
                statistics.DoneJobs += 1;
                lblDoneJobs.Text = statistics.DoneJobs.ToString();
            }
            else if (log is WorkerAgent.JobsRecivedLog) {
                statistics.RecivedJobs += ((WorkerAgent.JobsRecivedLog)log).Jobs.Length;
                lblRecivedJobs.Text = statistics.RecivedJobs.ToString();
            }
            else if (log is WorkerAgent.ErrorInWritingJobToDiskLog || (log is WorkerAgent.ErrorInReadingJobOutputFile)
                 || (log is WorkerAgent.ErrorInJobExecutionLog)) {
                statistics.FailedJobs++;
                lblFailedJobs.Text = statistics.FailedJobs.ToString();
            }


            // Add log to the textbox
            if (EnabledToolStripMenuItem.Checked)
                AddLogToTextBox(log);
        }

        private void AddLogToTextBox(WorkerAgent.Log log)
        {
            Color timeColor = Color.Gray;
            Color messageColor = Color.White;
            Color errorMessageColor = Color.Red;
            const int INDENT_L1 = 30; // Level 1 indent size = 30;

            AppendText(DateTime.Now.ToLongTimeString() + "  ", timeColor);

            if (log is WorkerAgent.JobsRecivedLog) {
                #region JobsRecivedLog
                WorkerAgent.JobsRecivedLog l = (WorkerAgent.JobsRecivedLog)log;
                AppendText(l.Jobs.Length.ToString() + " job(s) recived:", messageColor);
                rtbLogs.AppendText(Environment.NewLine);
                for (int i = 0; i < l.Jobs.Length; i++) {
                    AppendText("[" + (i + 1).ToString() + "]    Job ID: ", Color.Aqua, INDENT_L1);
                    AppendText(l.Jobs[i].ID.ToString(), Color.Aqua, 30);
                    AppendText("  Executable length: " + l.Jobs[i].ExecutableFileLength.ToString() + " Bytes", Color.Aqua, INDENT_L1);
                    rtbLogs.AppendText(Environment.NewLine);
                }
                #endregion
            }
            else if (log is WorkerAgent.JobExecutionStartedLog) {
                #region JobExecutionStartedLog
                WorkerAgent.JobExecutionStartedLog l = (WorkerAgent.JobExecutionStartedLog)log;
                AppendText(l.Message + "  (Job ID: " + l.Job.ID.ToString() + ")", messageColor);

                #endregion
            }
            else if (log is WorkerAgent.JobResultSentLog) {
                #region JobResultSentLog
                WorkerAgent.JobResultSentLog l = (WorkerAgent.JobResultSentLog)log;
                AppendText(l.Message + "  (Job ID: " + l.Job.ID.ToString() + ")", messageColor);
                #endregion
            }
            else if (log is WorkerAgent.JobExecutedSuccessfullyLog) {
                #region JobExecutedSuccessfullyLog
                WorkerAgent.JobExecutedSuccessfullyLog l = (WorkerAgent.JobExecutedSuccessfullyLog)log;
                AppendText(l.Message + "  (Job ID: " + l.Job.ID.ToString() + ")", messageColor);
                #endregion
            }
            else if (log is WorkerAgent.WorkerStartedLog) {
                #region WorkerStartedLog
                WorkerAgent.WorkerStartedLog l = (WorkerAgent.WorkerStartedLog)log;
                AppendText(l.Message, Color.GreenYellow);
                #endregion
            }
            else if (log is WorkerAgent.WorkerStoppedLog) {
                #region WorkerStoppedLog
                WorkerAgent.WorkerStoppedLog l = (WorkerAgent.WorkerStoppedLog)log;
                AppendText(l.Message, messageColor);
                #endregion
            }
            else if (log is WorkerAgent.WorkerCouldNotStartLog) {
                #region WorkerCouldNotStartLog
                WorkerAgent.WorkerCouldNotStartLog l = (WorkerAgent.WorkerCouldNotStartLog)log;
                AppendText(l.Message, errorMessageColor);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Error Message: " + l.ErrorMessage, errorMessageColor);
                #endregion
            }
            else if (log is WorkerAgent.WorkerCouldNotStopLog) {
                #region WorkerCouldNotStopLog
                WorkerAgent.WorkerCouldNotStopLog l = (WorkerAgent.WorkerCouldNotStopLog)log;
                AppendText(l.Message, errorMessageColor);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Error Message: " + l.ErrorMessage, errorMessageColor);
                #endregion
            }
            else if (log is WorkerAgent.ErrorInJobExecutionLog) {
                #region ErrorInJobExecutionLog
                WorkerAgent.ErrorInJobExecutionLog l = (WorkerAgent.ErrorInJobExecutionLog)log;
                AppendText(l.Message, errorMessageColor);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Job ID: " + l.Job.ID.ToString() + Environment.NewLine, errorMessageColor, INDENT_L1);
                AppendText("Job Executable: " + l.Job.ExecuteableFileName + Environment.NewLine, errorMessageColor, INDENT_L1);
                AppendText("Error: " + l.ErrorMessage, errorMessageColor, 30);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Is Win32 Error: " + (l.IsWin32Error ? "Yes" : "No"), errorMessageColor, INDENT_L1);
                if (l.IsWin32Error) {
                    rtbLogs.AppendText(Environment.NewLine);
                    AppendText("Win32 Error Code: " + l.Win32ErrorCode.ToString(), errorMessageColor, INDENT_L1);
                }
                #endregion
            }
            else if (log is WorkerAgent.ErrorInWritingJobToDiskLog) {
                #region ErrorInWritingJobToDiskLog
                WorkerAgent.ErrorInWritingJobToDiskLog l = (WorkerAgent.ErrorInWritingJobToDiskLog)log;
                AppendText(l.Message + "  (Job ID: " + l.Job.ID.ToString() + ")", errorMessageColor);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Error Message: " + l.ErrorMessage, errorMessageColor);
                #endregion
            }
            else if (log is WorkerAgent.ErrorInReadingJobOutputFile) {
                #region ErrorInReadingJobOutputFile
                WorkerAgent.ErrorInReadingJobOutputFile l = (WorkerAgent.ErrorInReadingJobOutputFile)log;
                AppendText(l.Message + "  (Job ID: " + l.Job.ID.ToString() + ")", errorMessageColor);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Error Message: " + l.ErrorMessage, errorMessageColor, INDENT_L1);
                #endregion
            }
            else if (log is WorkerAgent.ErrorInConnectingToJobServerLog) {
                #region ErrorInConnectingToJobServerLog
                WorkerAgent.ErrorInConnectingToJobServerLog l = (WorkerAgent.ErrorInConnectingToJobServerLog)log;
                AppendText(l.Message, errorMessageColor);
                rtbLogs.AppendText(Environment.NewLine);
                AppendText("Error Message: " + l.ErrorMessage, errorMessageColor, INDENT_L1);
                #endregion
            } else if (log is WorkerAgent.WorkerRegisterationFailedLog) {
                #region WorkerRegisterationFailedLog
                WorkerAgent.WorkerRegisterationFailedLog l = (WorkerAgent.WorkerRegisterationFailedLog)log;
                AppendText(l.Message, errorMessageColor);
                rtbLogs.AppendText(Environment.NewLine);
                #endregion
            } else if (log is WorkerAgent.WorkerRegisterationStartLog) {
                #region WorkerRegisterationStartLog
                WorkerAgent.WorkerRegisterationStartLog l = (WorkerAgent.WorkerRegisterationStartLog)log;
                AppendText(l.Message, Color.GreenYellow);
                #endregion
            } else if (log is WorkerAgent.WorkerRegisterdLog) {
                #region WorkerRegisterdLog
                WorkerAgent.WorkerRegisterdLog l = (WorkerAgent.WorkerRegisterdLog)log;
                AppendText(l.Message, Color.GreenYellow);
                rtbLogs.AppendText(Environment.NewLine);
                #endregion
            }

            else { // log is Log
                AppendText(log.Message, messageColor);
            }

            // Move cursor to next line
            rtbLogs.AppendText(Environment.NewLine);
            rtbLogs.ScrollToCaret();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings frm = new FormSettings();
            frm.MainForm = this;
            frm.ShowDialog();
        }

        private void startListiningForJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkerAgentObject.Start();
        }

        private void stopListiningForJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkerAgentObject.Stop();
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

        private void ChangeUIByWorkerAgentStatus(bool workerAgentStarted)
        {
            if (workerAgentStarted) {
                lblWorkerAgentStatus.Text = "Started";
                startListiningForJobsToolStripMenuItem.Enabled = false;
                stopListiningForJobsToolStripMenuItem.Enabled = true;
            }
            else {
                lblWorkerAgentStatus.Text = "Stopped";
                startListiningForJobsToolStripMenuItem.Enabled = true;
                stopListiningForJobsToolStripMenuItem.Enabled = false;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Load settings from "config.txt"
            if (System.IO.File.Exists(ConfigFileFullPath))
                WorkerSettingsObject.LoadFromFile(ConfigFileFullPath);
            else
                WorkerSettingsObject.LoadDefaults();

            this.Text = "Worker Agent  (Worker ID = " + WorkerAgentObject.ID.ToString() + ")";
            // Show settings on the log window
            AppendText("Quax Worker Agent v1.0\n", Color.Gray);
            AppendText("----------------------\n", Color.Gray);
            AppendText("Settings:\n", Color.Gray);
            AppendText("Worker ID: " + WorkerAgentObject.ID.ToString() + "\n", Color.Gray);
            AppendText("Job Server IP Address: " + WorkerAgentObject.JobServerIPAddress.ToString() + "\n", Color.Gray);
            AppendText("Job Server Data Channel Port Number: " + WorkerAgentObject.JobServerDataChannelPortNumber.ToString() + "\n", Color.Gray);
            AppendText("Job Server Control Channel PortNumber: " + WorkerAgentObject.JobServerControlChannelPortNumber.ToString() + "\n", Color.Gray);
            AppendText("Listen to recive jobs (from job server) on port: " + WorkerAgentObject.ListenDataChannelPortNumber.ToString() + "\n", Color.Gray);
            AppendText("Listen to recive control messages (from job server) on port: " + WorkerAgentObject.ListenControlChannelPortNumber + "\n", Color.Gray);
            AppendText("\n[You can change this settings from Worker/Settings menu.]\n\n", Color.Gray);

            // Auto Start Listening
            if (WorkerSettingsObject.AutoStartListening)
                startListiningForJobsToolStripMenuItem_Click(sender, e);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WorkerAgentObject.IsListening)
                WorkerAgentObject.Stop();
        }

        private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbLogs.Text = "";
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbLogs.Copy();
        }

        private void removePendingJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("All jobs in the jobs queue that are waiting for execution will remove from the queue, are you sure?", "Remove pending jobs from jobs queue", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes) {
                WorkerAgentObject.ClearJobsQueue();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WorkerAgentObject.IsListening)
                WorkerAgentObject.Stop();
            Application.Exit();
        }

        private void resetStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statistics.RecivedJobs = 0;
            statistics.DoneJobs = 0;
            statistics.FailedJobs = 0;
            lblRecivedJobs.Text = "0";
            lblDoneJobs.Text = "0";
            lblFailedJobs.Text = "0";
        }

    }
}
