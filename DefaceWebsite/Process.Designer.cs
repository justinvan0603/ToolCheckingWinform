namespace DefaceWebsite
{
    partial class Process
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
            if (disposing && (components != null))
            {
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
            this.lblScheTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalProcess = new System.Windows.Forms.Label();
            this.lblTotalDomain = new System.Windows.Forms.Label();
            this.lblTotalLink = new System.Windows.Forms.Label();
            this.pnContent = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTerm = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblScheTime
            // 
            this.lblScheTime.AutoSize = true;
            this.lblScheTime.Location = new System.Drawing.Point(323, 84);
            this.lblScheTime.Name = "lblScheTime";
            this.lblScheTime.Size = new System.Drawing.Size(70, 13);
            this.lblScheTime.TabIndex = 2;
            this.lblScheTime.Text = "Lịch trình lúc:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Danh sách tiến trình đang thực hiện";
            // 
            // lblTotalProcess
            // 
            this.lblTotalProcess.AutoSize = true;
            this.lblTotalProcess.Location = new System.Drawing.Point(29, 109);
            this.lblTotalProcess.Name = "lblTotalProcess";
            this.lblTotalProcess.Size = new System.Drawing.Size(75, 13);
            this.lblTotalProcess.TabIndex = 2;
            this.lblTotalProcess.Text = "Số tiến trình: 0";
            // 
            // lblTotalDomain
            // 
            this.lblTotalDomain.AutoSize = true;
            this.lblTotalDomain.Location = new System.Drawing.Point(120, 109);
            this.lblTotalDomain.Name = "lblTotalDomain";
            this.lblTotalDomain.Size = new System.Drawing.Size(69, 13);
            this.lblTotalDomain.TabIndex = 2;
            this.lblTotalDomain.Text = "Số domain: 0";
            // 
            // lblTotalLink
            // 
            this.lblTotalLink.AutoSize = true;
            this.lblTotalLink.Location = new System.Drawing.Point(210, 109);
            this.lblTotalLink.Name = "lblTotalLink";
            this.lblTotalLink.Size = new System.Drawing.Size(56, 13);
            this.lblTotalLink.TabIndex = 2;
            this.lblTotalLink.Text = "Số links: 0";
            // 
            // pnContent
            // 
            this.pnContent.AutoScroll = true;
            this.pnContent.Location = new System.Drawing.Point(32, 126);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(755, 392);
            this.pnContent.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.cbTerm);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(32, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(755, 69);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tùy chọn";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(63, 26);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(94, 20);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ngày:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Đợt:";
            // 
            // cbTerm
            // 
            this.cbTerm.FormattingEnabled = true;
            this.cbTerm.Location = new System.Drawing.Point(211, 26);
            this.cbTerm.Name = "cbTerm";
            this.cbTerm.Size = new System.Drawing.Size(121, 21);
            this.cbTerm.TabIndex = 3;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(370, 24);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Lấy dữ liệu";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Process
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 528);
            this.Controls.Add(this.lblScheTime);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnContent);
            this.Controls.Add(this.lblTotalLink);
            this.Controls.Add(this.lblTotalDomain);
            this.Controls.Add(this.lblTotalProcess);
            this.Name = "Process";
            this.Text = "Process";
            this.Load += new System.EventHandler(this.Process_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblScheTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalProcess;
        private System.Windows.Forms.Label lblTotalDomain;
        private System.Windows.Forms.Label lblTotalLink;
        private System.Windows.Forms.Panel pnContent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox cbTerm;
        private System.Windows.Forms.Label label3;
    }
}