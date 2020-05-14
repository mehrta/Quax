namespace SampleQuaxApp
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
            this.components = new System.ComponentModel.Container();
            this.lstWorkers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.jobServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearSendQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpJob = new System.Windows.Forms.GroupBox();
            this.numJobMaxTime = new System.Windows.Forms.NumericUpDown();
            this.numJobExpectedTime = new System.Windows.Forms.NumericUpDown();
            this.numJobMaximumCreate = new System.Windows.Forms.NumericUpDown();
            this.numJobsCreateInterval = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowseJobInput = new System.Windows.Forms.Button();
            this.btnBrowseJobExecutable = new System.Windows.Forms.Button();
            this.txtInputPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExecutablePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSendQueueJobs = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.timCreateJob = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.grpJob.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numJobMaxTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobExpectedTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobMaximumCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobsCreateInterval)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstWorkers
            // 
            this.lstWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstWorkers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader2});
            this.lstWorkers.HideSelection = false;
            this.lstWorkers.Location = new System.Drawing.Point(12, 243);
            this.lstWorkers.Name = "lstWorkers";
            this.lstWorkers.Size = new System.Drawing.Size(412, 146);
            this.lstWorkers.TabIndex = 0;
            this.lstWorkers.UseCompatibleStateImageBehavior = false;
            this.lstWorkers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Worker ID";
            this.columnHeader1.Width = 76;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Recived Jobs";
            this.columnHeader3.Width = 104;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Done Jobs";
            this.columnHeader4.Width = 99;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP";
            this.columnHeader2.Width = 111;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jobServerToolStripMenuItem,
            this.logToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(850, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // jobServerToolStripMenuItem
            // 
            this.jobServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearSendQueueToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.jobServerToolStripMenuItem.Name = "jobServerToolStripMenuItem";
            this.jobServerToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.jobServerToolStripMenuItem.Text = "Program";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.startToolStripMenuItem.Text = "Start Dispatching";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.stopToolStripMenuItem.Text = "Stop Dispatching";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // clearSendQueueToolStripMenuItem
            // 
            this.clearSendQueueToolStripMenuItem.Name = "clearSendQueueToolStripMenuItem";
            this.clearSendQueueToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.clearSendQueueToolStripMenuItem.Text = "Clear Send Queue";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogsToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.logToolStripMenuItem.Text = "Log";
            // 
            // clearLogsToolStripMenuItem
            // 
            this.clearLogsToolStripMenuItem.Name = "clearLogsToolStripMenuItem";
            this.clearLogsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.clearLogsToolStripMenuItem.Text = "Clear Logs";
            this.clearLogsToolStripMenuItem.Click += new System.EventHandler(this.clearLogsToolStripMenuItem_Click);
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
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.aboutToolStripMenuItem.Text = "About Quax";
            // 
            // grpJob
            // 
            this.grpJob.Controls.Add(this.numJobMaxTime);
            this.grpJob.Controls.Add(this.numJobExpectedTime);
            this.grpJob.Controls.Add(this.numJobMaximumCreate);
            this.grpJob.Controls.Add(this.numJobsCreateInterval);
            this.grpJob.Controls.Add(this.label10);
            this.grpJob.Controls.Add(this.label8);
            this.grpJob.Controls.Add(this.label6);
            this.grpJob.Controls.Add(this.label4);
            this.grpJob.Controls.Add(this.label9);
            this.grpJob.Controls.Add(this.label7);
            this.grpJob.Controls.Add(this.label5);
            this.grpJob.Controls.Add(this.label3);
            this.grpJob.Controls.Add(this.btnBrowseJobInput);
            this.grpJob.Controls.Add(this.btnBrowseJobExecutable);
            this.grpJob.Controls.Add(this.txtInputPath);
            this.grpJob.Controls.Add(this.label2);
            this.grpJob.Controls.Add(this.txtExecutablePath);
            this.grpJob.Controls.Add(this.label1);
            this.grpJob.Location = new System.Drawing.Point(12, 35);
            this.grpJob.Name = "grpJob";
            this.grpJob.Size = new System.Drawing.Size(412, 202);
            this.grpJob.TabIndex = 2;
            this.grpJob.TabStop = false;
            this.grpJob.Text = "Job";
            // 
            // numJobMaxTime
            // 
            this.numJobMaxTime.Location = new System.Drawing.Point(192, 162);
            this.numJobMaxTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numJobMaxTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numJobMaxTime.Name = "numJobMaxTime";
            this.numJobMaxTime.Size = new System.Drawing.Size(92, 20);
            this.numJobMaxTime.TabIndex = 4;
            this.numJobMaxTime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // numJobExpectedTime
            // 
            this.numJobExpectedTime.Location = new System.Drawing.Point(192, 137);
            this.numJobExpectedTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numJobExpectedTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numJobExpectedTime.Name = "numJobExpectedTime";
            this.numJobExpectedTime.Size = new System.Drawing.Size(92, 20);
            this.numJobExpectedTime.TabIndex = 4;
            this.numJobExpectedTime.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // numJobMaximumCreate
            // 
            this.numJobMaximumCreate.Location = new System.Drawing.Point(192, 112);
            this.numJobMaximumCreate.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numJobMaximumCreate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numJobMaximumCreate.Name = "numJobMaximumCreate";
            this.numJobMaximumCreate.Size = new System.Drawing.Size(92, 20);
            this.numJobMaximumCreate.TabIndex = 4;
            this.numJobMaximumCreate.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numJobsCreateInterval
            // 
            this.numJobsCreateInterval.Location = new System.Drawing.Point(192, 88);
            this.numJobsCreateInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numJobsCreateInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numJobsCreateInterval.Name = "numJobsCreateInterval";
            this.numJobsCreateInterval.Size = new System.Drawing.Size(92, 20);
            this.numJobsCreateInterval.TabIndex = 4;
            this.numJobsCreateInterval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(290, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "second(s).";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(290, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "second(s).";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "jobs.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "milli second(s).";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Job Max Time To Complete";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Job Expected Time To Complete";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Maximum create ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Insert a job to the send queue every ";
            // 
            // btnBrowseJobInput
            // 
            this.btnBrowseJobInput.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnBrowseJobInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnBrowseJobInput.Location = new System.Drawing.Point(343, 49);
            this.btnBrowseJobInput.Name = "btnBrowseJobInput";
            this.btnBrowseJobInput.Size = new System.Drawing.Size(58, 23);
            this.btnBrowseJobInput.TabIndex = 2;
            this.btnBrowseJobInput.Text = "Browse";
            this.btnBrowseJobInput.UseVisualStyleBackColor = true;
            this.btnBrowseJobInput.Click += new System.EventHandler(this.btnBrowseJobInput_Click);
            // 
            // btnBrowseJobExecutable
            // 
            this.btnBrowseJobExecutable.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnBrowseJobExecutable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnBrowseJobExecutable.Location = new System.Drawing.Point(343, 23);
            this.btnBrowseJobExecutable.Name = "btnBrowseJobExecutable";
            this.btnBrowseJobExecutable.Size = new System.Drawing.Size(58, 23);
            this.btnBrowseJobExecutable.TabIndex = 2;
            this.btnBrowseJobExecutable.Text = "Browse";
            this.btnBrowseJobExecutable.UseVisualStyleBackColor = true;
            this.btnBrowseJobExecutable.Click += new System.EventHandler(this.btnBrowseJobExecutable_Click);
            // 
            // txtInputPath
            // 
            this.txtInputPath.Location = new System.Drawing.Point(100, 49);
            this.txtInputPath.Name = "txtInputPath";
            this.txtInputPath.Size = new System.Drawing.Size(237, 20);
            this.txtInputPath.TabIndex = 1;
            this.txtInputPath.Text = "D:\\Development\\My Projects\\Quax\\TestJob\\bin\\Debug\\job.txt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Input File Path:";
            // 
            // txtExecutablePath
            // 
            this.txtExecutablePath.Location = new System.Drawing.Point(100, 23);
            this.txtExecutablePath.Name = "txtExecutablePath";
            this.txtExecutablePath.Size = new System.Drawing.Size(237, 20);
            this.txtExecutablePath.TabIndex = 1;
            this.txtExecutablePath.Text = "D:\\Development\\My Projects\\Quax\\TestJob\\bin\\Debug\\TestJob.exe";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Executable Path:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.lblStatus,
            this.toolStripStatusLabel1,
            this.lblSendQueueJobs});
            this.statusStrip1.Location = new System.Drawing.Point(0, 407);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(850, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel3.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.Margin = new System.Windows.Forms.Padding(0, 3, 25, 2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 17);
            this.lblStatus.Text = "Ready";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(135, 17);
            this.toolStripStatusLabel1.Text = "Jobs In The Send Queue:";
            // 
            // lblSendQueueJobs
            // 
            this.lblSendQueueJobs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSendQueueJobs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSendQueueJobs.Name = "lblSendQueueJobs";
            this.lblSendQueueJobs.Size = new System.Drawing.Size(14, 17);
            this.lblSendQueueJobs.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.rtbLogs);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(430, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(407, 354);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // rtbLogs
            // 
            this.rtbLogs.BackColor = System.Drawing.Color.White;
            this.rtbLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLogs.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rtbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogs.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLogs.ForeColor = System.Drawing.Color.Indigo;
            this.rtbLogs.Location = new System.Drawing.Point(10, 23);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.ReadOnly = true;
            this.rtbLogs.Size = new System.Drawing.Size(387, 321);
            this.rtbLogs.TabIndex = 5;
            this.rtbLogs.Text = "";
            // 
            // timCreateJob
            // 
            this.timCreateJob.Tick += new System.EventHandler(this.timCreateJob_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(850, 429);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpJob);
            this.Controls.Add(this.lstWorkers);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(550, 310);
            this.Name = "FormMain";
            this.Text = "QUAX (A Simple Job Dispatcher)";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpJob.ResumeLayout(false);
            this.grpJob.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numJobMaxTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobExpectedTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobMaximumCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobsCreateInterval)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstWorkers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem jobServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpJob;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblSendQueueJobs;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.TextBox txtInputPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExecutablePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseJobInput;
        private System.Windows.Forms.Button btnBrowseJobExecutable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numJobsCreateInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numJobMaximumCreate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbLogs;
        private System.Windows.Forms.ToolStripMenuItem clearSendQueueToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer timCreateJob;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem clearLogsToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numJobMaxTime;
        private System.Windows.Forms.NumericUpDown numJobExpectedTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
    }
}

