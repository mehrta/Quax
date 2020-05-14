namespace Worker
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startListiningForJobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopListiningForJobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removePendingJobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWorkerAgentStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRecivedJobs = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel10 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDoneJobs = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFailedJobs = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel11 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.resetStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.logToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(626, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startListiningForJobsToolStripMenuItem,
            this.stopListiningForJobsToolStripMenuItem,
            this.removePendingJobsToolStripMenuItem,
            this.toolStripSeparator1,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.programToolStripMenuItem.Text = "Worker";
            // 
            // startListiningForJobsToolStripMenuItem
            // 
            this.startListiningForJobsToolStripMenuItem.Name = "startListiningForJobsToolStripMenuItem";
            this.startListiningForJobsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.startListiningForJobsToolStripMenuItem.Text = "Start";
            this.startListiningForJobsToolStripMenuItem.Click += new System.EventHandler(this.startListiningForJobsToolStripMenuItem_Click);
            // 
            // stopListiningForJobsToolStripMenuItem
            // 
            this.stopListiningForJobsToolStripMenuItem.Enabled = false;
            this.stopListiningForJobsToolStripMenuItem.Name = "stopListiningForJobsToolStripMenuItem";
            this.stopListiningForJobsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.stopListiningForJobsToolStripMenuItem.Text = "Stop";
            this.stopListiningForJobsToolStripMenuItem.Click += new System.EventHandler(this.stopListiningForJobsToolStripMenuItem_Click);
            // 
            // removePendingJobsToolStripMenuItem
            // 
            this.removePendingJobsToolStripMenuItem.Name = "removePendingJobsToolStripMenuItem";
            this.removePendingJobsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.removePendingJobsToolStripMenuItem.Text = "Remove pending jobs";
            this.removePendingJobsToolStripMenuItem.Click += new System.EventHandler(this.removePendingJobsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnabledToolStripMenuItem,
            this.clearLogsToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem,
            this.toolStripSeparator3,
            this.resetStatisticsToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.logToolStripMenuItem.Text = "Log";
            // 
            // EnabledToolStripMenuItem
            // 
            this.EnabledToolStripMenuItem.Checked = true;
            this.EnabledToolStripMenuItem.CheckOnClick = true;
            this.EnabledToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnabledToolStripMenuItem.Name = "EnabledToolStripMenuItem";
            this.EnabledToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.EnabledToolStripMenuItem.Text = "Enabled";
            // 
            // clearLogsToolStripMenuItem
            // 
            this.clearLogsToolStripMenuItem.Name = "clearLogsToolStripMenuItem";
            this.clearLogsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.clearLogsToolStripMenuItem.Text = "Clear Logs";
            this.clearLogsToolStripMenuItem.Click += new System.EventHandler(this.clearLogsToolStripMenuItem_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy To Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.aboutToolStripMenuItem.Text = "About Quax";
            // 
            // rtbLogs
            // 
            this.rtbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLogs.BackColor = System.Drawing.Color.Black;
            this.rtbLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLogs.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rtbLogs.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLogs.ForeColor = System.Drawing.Color.White;
            this.rtbLogs.Location = new System.Drawing.Point(8, 29);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.ReadOnly = true;
            this.rtbLogs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbLogs.ShowSelectionMargin = true;
            this.rtbLogs.Size = new System.Drawing.Size(614, 304);
            this.rtbLogs.TabIndex = 1;
            this.rtbLogs.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Gainsboro;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel8,
            this.lblWorkerAgentStatus,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.lblRecivedJobs,
            this.toolStripStatusLabel10,
            this.toolStripStatusLabel6,
            this.lblDoneJobs,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.lblFailedJobs,
            this.toolStripStatusLabel11});
            this.statusStrip1.Location = new System.Drawing.Point(0, 336);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(626, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel8.Text = "Status:";
            // 
            // lblWorkerAgentStatus
            // 
            this.lblWorkerAgentStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblWorkerAgentStatus.Margin = new System.Windows.Forms.Padding(0, 3, 30, 2);
            this.lblWorkerAgentStatus.Name = "lblWorkerAgentStatus";
            this.lblWorkerAgentStatus.Size = new System.Drawing.Size(39, 17);
            this.lblWorkerAgentStatus.Text = "Ready";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(40, 17);
            this.toolStripStatusLabel1.Text = "Jobs: [";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel2.Text = "Recived=";
            // 
            // lblRecivedJobs
            // 
            this.lblRecivedJobs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRecivedJobs.Name = "lblRecivedJobs";
            this.lblRecivedJobs.Size = new System.Drawing.Size(13, 17);
            this.lblRecivedJobs.Text = "0";
            this.lblRecivedJobs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel10
            // 
            this.toolStripStatusLabel10.Name = "toolStripStatusLabel10";
            this.toolStripStatusLabel10.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel10.Text = ",";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(43, 17);
            this.toolStripStatusLabel6.Text = "Done=";
            // 
            // lblDoneJobs
            // 
            this.lblDoneJobs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDoneJobs.Name = "lblDoneJobs";
            this.lblDoneJobs.Size = new System.Drawing.Size(13, 17);
            this.lblDoneJobs.Text = "0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel3.Text = ",";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(46, 17);
            this.toolStripStatusLabel4.Text = "Failed=";
            // 
            // lblFailedJobs
            // 
            this.lblFailedJobs.Name = "lblFailedJobs";
            this.lblFailedJobs.Size = new System.Drawing.Size(13, 17);
            this.lblFailedJobs.Text = "0";
            // 
            // toolStripStatusLabel11
            // 
            this.toolStripStatusLabel11.Name = "toolStripStatusLabel11";
            this.toolStripStatusLabel11.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel11.Text = "]";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(171, 6);
            // 
            // resetStatisticsToolStripMenuItem
            // 
            this.resetStatisticsToolStripMenuItem.Name = "resetStatisticsToolStripMenuItem";
            this.resetStatisticsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.resetStatisticsToolStripMenuItem.Text = "Reset Statistics";
            this.resetStatisticsToolStripMenuItem.Click += new System.EventHandler(this.resetStatisticsToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(626, 358);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.rtbLogs);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Worker Agent";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startListiningForJobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopListiningForJobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtbLogs;
        private System.Windows.Forms.ToolStripMenuItem clearLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EnabledToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblRecivedJobs;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel lblDoneJobs;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel lblWorkerAgentStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel10;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel11;
        private System.Windows.Forms.ToolStripMenuItem removePendingJobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lblFailedJobs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem resetStatisticsToolStripMenuItem;
    }
}

