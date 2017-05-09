namespace DefaceWebsite
{
    partial class TProcess
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnActive = new System.Windows.Forms.Button();
            this.lblWarning = new System.Windows.Forms.Label();
            this.lblDone = new System.Windows.Forms.Label();
            this.lblLinks = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDomain = new System.Windows.Forms.Label();
            this.prcProcess = new System.Windows.Forms.ProgressBar();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnActive
            // 
            this.btnActive.Location = new System.Drawing.Point(599, 13);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(71, 23);
            this.btnActive.TabIndex = 11;
            this.btnActive.Text = "Start";
            this.btnActive.UseVisualStyleBackColor = true;
            this.btnActive.Click += new System.EventHandler(this.btnActive_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Location = new System.Drawing.Point(152, 42);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(65, 13);
            this.lblWarning.TabIndex = 6;
            this.lblWarning.Text = "Cảnh báo: 0";
            // 
            // lblDone
            // 
            this.lblDone.AutoSize = true;
            this.lblDone.Location = new System.Drawing.Point(73, 42);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(50, 13);
            this.lblDone.TabIndex = 7;
            this.lblDone.Text = "Xong: 10";
            // 
            // lblLinks
            // 
            this.lblLinks.AutoSize = true;
            this.lblLinks.Location = new System.Drawing.Point(531, 18);
            this.lblLinks.Name = "lblLinks";
            this.lblLinks.Size = new System.Drawing.Size(62, 13);
            this.lblLinks.TabIndex = 8;
            this.lblLinks.Text = "Số links: 15";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(7, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(63, 13);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Tiến trình 1:";
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Location = new System.Drawing.Point(440, 18);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(69, 13);
            this.lblDomain.TabIndex = 10;
            this.lblDomain.Text = "Số domain: 3";
            // 
            // prcProcess
            // 
            this.prcProcess.Location = new System.Drawing.Point(76, 13);
            this.prcProcess.Name = "prcProcess";
            this.prcProcess.Size = new System.Drawing.Size(356, 23);
            this.prcProcess.TabIndex = 5;
            // 
            // tbxMessage
            // 
            this.tbxMessage.BackColor = System.Drawing.Color.White;
            this.tbxMessage.Location = new System.Drawing.Point(10, 80);
            this.tbxMessage.Multiline = true;
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.ReadOnly = true;
            this.tbxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxMessage.Size = new System.Drawing.Size(660, 87);
            this.tbxMessage.TabIndex = 13;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 64);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 12;
            this.label16.Text = "Logs:";
            // 
            // TProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxMessage);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnActive);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.lblDone);
            this.Controls.Add(this.lblLinks);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDomain);
            this.Controls.Add(this.prcProcess);
            this.Name = "TProcess";
            this.Size = new System.Drawing.Size(682, 179);
            this.Load += new System.EventHandler(this.TProcess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnActive;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label lblDone;
        private System.Windows.Forms.Label lblLinks;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.ProgressBar prcProcess;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Label label16;
    }
}
