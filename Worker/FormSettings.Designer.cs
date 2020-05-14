namespace Worker
{
    partial class FormSettings
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkAutoStartListening = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numWorkerID = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numListenControlChannel = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numListenDataChannel = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtServerIpAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numJobServerControlChannel = new System.Windows.Forms.NumericUpDown();
            this.numJobServerDataChannel = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWorkerID)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numListenControlChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numListenDataChannel)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numJobServerControlChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobServerDataChannel)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(180, 249);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 28);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(261, 249);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(337, 243);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.chkAutoStartListening);
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.numWorkerID);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(329, 217);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkAutoStartListening
            // 
            this.chkAutoStartListening.AutoSize = true;
            this.chkAutoStartListening.Location = new System.Drawing.Point(11, 44);
            this.chkAutoStartListening.Name = "chkAutoStartListening";
            this.chkAutoStartListening.Size = new System.Drawing.Size(287, 17);
            this.chkAutoStartListening.TabIndex = 1;
            this.chkAutoStartListening.Text = "Automatically start listening for jobs when program starts";
            this.chkAutoStartListening.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(11, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(256, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Automatically start program when Windows starts";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numWorkerID
            // 
            this.numWorkerID.Location = new System.Drawing.Point(76, 147);
            this.numWorkerID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numWorkerID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWorkerID.Name = "numWorkerID";
            this.numWorkerID.Size = new System.Drawing.Size(88, 20);
            this.numWorkerID.TabIndex = 2;
            this.numWorkerID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(170, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "(Between 1 and 65535)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Worker ID:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(329, 217);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Connection Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numListenControlChannel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numListenDataChannel);
            this.groupBox2.Location = new System.Drawing.Point(8, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 86);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Recive Jobs";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(166, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "( For Control Channel ) ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "( For Data Channel )";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Listen On Port:";
            // 
            // numListenControlChannel
            // 
            this.numListenControlChannel.Location = new System.Drawing.Point(86, 50);
            this.numListenControlChannel.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numListenControlChannel.Minimum = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            this.numListenControlChannel.Name = "numListenControlChannel";
            this.numListenControlChannel.Size = new System.Drawing.Size(74, 20);
            this.numListenControlChannel.TabIndex = 1;
            this.numListenControlChannel.Value = new decimal(new int[] {
            20001,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Listen On Port:";
            // 
            // numListenDataChannel
            // 
            this.numListenDataChannel.Location = new System.Drawing.Point(86, 23);
            this.numListenDataChannel.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numListenDataChannel.Minimum = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            this.numListenDataChannel.Name = "numListenDataChannel";
            this.numListenDataChannel.Size = new System.Drawing.Size(74, 20);
            this.numListenDataChannel.TabIndex = 0;
            this.numListenDataChannel.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtServerIpAddress);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numJobServerControlChannel);
            this.groupBox1.Controls.Add(this.numJobServerDataChannel);
            this.groupBox1.Location = new System.Drawing.Point(8, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 113);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send Job Results";
            // 
            // txtServerIpAddress
            // 
            this.txtServerIpAddress.Location = new System.Drawing.Point(211, 23);
            this.txtServerIpAddress.Name = "txtServerIpAddress";
            this.txtServerIpAddress.Size = new System.Drawing.Size(98, 20);
            this.txtServerIpAddress.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Job Server Control Channel Port Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Job Server Data Channel Port Number:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Job Server IP Address:";
            // 
            // numJobServerControlChannel
            // 
            this.numJobServerControlChannel.Location = new System.Drawing.Point(211, 75);
            this.numJobServerControlChannel.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numJobServerControlChannel.Minimum = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            this.numJobServerControlChannel.Name = "numJobServerControlChannel";
            this.numJobServerControlChannel.Size = new System.Drawing.Size(98, 20);
            this.numJobServerControlChannel.TabIndex = 2;
            this.numJobServerControlChannel.Value = new decimal(new int[] {
            40001,
            0,
            0,
            0});
            // 
            // numJobServerDataChannel
            // 
            this.numJobServerDataChannel.Location = new System.Drawing.Point(211, 49);
            this.numJobServerDataChannel.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numJobServerDataChannel.Minimum = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            this.numJobServerDataChannel.Name = "numJobServerDataChannel";
            this.numJobServerDataChannel.Size = new System.Drawing.Size(98, 20);
            this.numJobServerDataChannel.TabIndex = 1;
            this.numJobServerDataChannel.Value = new decimal(new int[] {
            40000,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 180);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(257, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Notice: Each worker should has a unique Worker ID.";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(349, 289);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWorkerID)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numListenControlChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numListenDataChannel)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numJobServerControlChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobServerDataChannel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numListenControlChannel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numListenDataChannel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtServerIpAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numJobServerControlChannel;
        private System.Windows.Forms.NumericUpDown numJobServerDataChannel;
        private System.Windows.Forms.NumericUpDown numWorkerID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chkAutoStartListening;
        private System.Windows.Forms.Label label10;

    }
}