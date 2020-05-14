using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Worker
{
    public partial class FormSettings : Form
    {
        public FormMain MainForm { set; get; }

        public FormSettings()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Job Server IP Address
            IPAddress jobServerIP;
            if (IPAddress.TryParse(txtServerIpAddress.Text, out jobServerIP)) {
                MainForm.WorkerAgentObject.JobServerIPAddress = jobServerIP;
            }
            else {
                MessageBox.Show("Please enter a valid IP address.", "Invalid IP Address");
                txtServerIpAddress.Focus();
                return;
            }

            // Auto Start Listening
            MainForm.WorkerSettingsObject.AutoStartListening = chkAutoStartListening.Checked;

            // Worker ID
            MainForm.WorkerAgentObject.ID = (UInt16)numWorkerID.Value;
            MainForm.Text = "Worker Agent  (Worker ID = " + MainForm.WorkerAgentObject.ID.ToString() + ")";

            // Job Server Data Channel Port Number
            MainForm.WorkerAgentObject.JobServerDataChannelPortNumber = (int)numJobServerDataChannel.Value;

            // Job Server Control Channel Port Number
            MainForm.WorkerAgentObject.JobServerControlChannelPortNumber = (int)numJobServerControlChannel.Value;

            // Listen Data Channel Port Number
            MainForm.WorkerAgentObject.ListenDataChannelPortNumber = (int)numListenDataChannel.Value;

            // Listen Control Channel Port Number
            MainForm.WorkerAgentObject.ListenControlChannelPortNumber = (int)numListenControlChannel.Value;

            // Save settings to file
             MainForm.WorkerSettingsObject.SaveToFile(MainForm.ConfigFileFullPath);

            // Close the form
            this.Close();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            // Worker ID
            numWorkerID.Value = MainForm.WorkerAgentObject.ID;

            // Auto Start Listening
            chkAutoStartListening.Checked = MainForm.WorkerSettingsObject.AutoStartListening;

            // Job Server IP Address
            txtServerIpAddress.Text = MainForm.WorkerAgentObject.JobServerIPAddress.ToString();

            // Job Server Data Channel Port Number
            numJobServerDataChannel.Value = MainForm.WorkerAgentObject.JobServerDataChannelPortNumber;

            // Job Server Control Channel Port Number
            numJobServerControlChannel.Value = MainForm.WorkerAgentObject.JobServerControlChannelPortNumber;

            // Listen Data Channel Port Number
            numListenDataChannel.Value = MainForm.WorkerAgentObject.ListenDataChannelPortNumber;

            // Listen Control Channel Port Number
            numListenControlChannel.Value = MainForm.WorkerAgentObject.ListenControlChannelPortNumber;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
