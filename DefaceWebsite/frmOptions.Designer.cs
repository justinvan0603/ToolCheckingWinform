namespace DefaceWebsite
{
    partial class frmOptions
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtCustome = new System.Windows.Forms.RadioButton();
            this.rbtAuto = new System.Windows.Forms.RadioButton();
            this.chbRegisterWin = new System.Windows.Forms.CheckBox();
            this.nrLinks = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dpTimeStart = new System.Windows.Forms.DateTimePicker();
            this.chbSchedule = new System.Windows.Forms.CheckBox();
            this.chbSendApp = new System.Windows.Forms.CheckBox();
            this.chbSendEmail = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nrLinks)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số lượng link/thread";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Thời gian bắt đầu";
            // 
            // rbtCustome
            // 
            this.rbtCustome.AutoSize = true;
            this.rbtCustome.Checked = true;
            this.rbtCustome.Location = new System.Drawing.Point(29, 29);
            this.rbtCustome.Name = "rbtCustome";
            this.rbtCustome.Size = new System.Drawing.Size(71, 17);
            this.rbtCustome.TabIndex = 1;
            this.rbtCustome.TabStop = true;
            this.rbtCustome.Text = "Thủ công";
            this.rbtCustome.UseVisualStyleBackColor = true;
            // 
            // rbtAuto
            // 
            this.rbtAuto.AutoSize = true;
            this.rbtAuto.Location = new System.Drawing.Point(136, 29);
            this.rbtAuto.Name = "rbtAuto";
            this.rbtAuto.Size = new System.Drawing.Size(65, 17);
            this.rbtAuto.TabIndex = 1;
            this.rbtAuto.Text = "Tự dộng";
            this.rbtAuto.UseVisualStyleBackColor = true;
            // 
            // chbRegisterWin
            // 
            this.chbRegisterWin.AutoSize = true;
            this.chbRegisterWin.Location = new System.Drawing.Point(29, 52);
            this.chbRegisterWin.Name = "chbRegisterWin";
            this.chbRegisterWin.Size = new System.Drawing.Size(141, 17);
            this.chbRegisterWin.TabIndex = 2;
            this.chbRegisterWin.Text = "Khởi động cùng window";
            this.chbRegisterWin.UseVisualStyleBackColor = true;
            this.chbRegisterWin.CheckedChanged += new System.EventHandler(this.chbRegisterWin_CheckedChanged);
            // 
            // nrLinks
            // 
            this.nrLinks.Location = new System.Drawing.Point(121, 24);
            this.nrLinks.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nrLinks.Name = "nrLinks";
            this.nrLinks.Size = new System.Drawing.Size(68, 20);
            this.nrLinks.TabIndex = 3;
            this.nrLinks.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dpTimeStart);
            this.groupBox1.Controls.Add(this.chbSchedule);
            this.groupBox1.Controls.Add(this.chbRegisterWin);
            this.groupBox1.Controls.Add(this.rbtCustome);
            this.groupBox1.Controls.Add(this.rbtAuto);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(15, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 135);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chế độ thực hiện";
            // 
            // dpTimeStart
            // 
            this.dpTimeStart.CustomFormat = "hh:mm";
            this.dpTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dpTimeStart.Location = new System.Drawing.Point(130, 101);
            this.dpTimeStart.Name = "dpTimeStart";
            this.dpTimeStart.Size = new System.Drawing.Size(85, 20);
            this.dpTimeStart.TabIndex = 6;
            this.dpTimeStart.Value = new System.DateTime(2017, 2, 13, 0, 0, 0, 0);
            // 
            // chbSchedule
            // 
            this.chbSchedule.AutoSize = true;
            this.chbSchedule.Checked = true;
            this.chbSchedule.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSchedule.Location = new System.Drawing.Point(29, 78);
            this.chbSchedule.Name = "chbSchedule";
            this.chbSchedule.Size = new System.Drawing.Size(103, 17);
            this.chbSchedule.TabIndex = 2;
            this.chbSchedule.Text = "Tự động lập lịch";
            this.chbSchedule.UseVisualStyleBackColor = true;
            // 
            // chbSendApp
            // 
            this.chbSendApp.AutoSize = true;
            this.chbSendApp.Checked = true;
            this.chbSendApp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSendApp.Location = new System.Drawing.Point(15, 191);
            this.chbSendApp.Name = "chbSendApp";
            this.chbSendApp.Size = new System.Drawing.Size(135, 17);
            this.chbSendApp.TabIndex = 2;
            this.chbSendApp.Text = "Gửi thống báo qua app";
            this.chbSendApp.UseVisualStyleBackColor = true;
            // 
            // chbSendEmail
            // 
            this.chbSendEmail.AutoSize = true;
            this.chbSendEmail.Checked = true;
            this.chbSendEmail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSendEmail.Location = new System.Drawing.Point(15, 215);
            this.chbSendEmail.Name = "chbSendEmail";
            this.chbSendEmail.Size = new System.Drawing.Size(141, 17);
            this.chbSendEmail.TabIndex = 2;
            this.chbSendEmail.Text = "Gửi thống báo qua email";
            this.chbSendEmail.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(215, 232);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 263);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chbSendEmail);
            this.Controls.Add(this.chbSendApp);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nrLinks);
            this.Controls.Add(this.label1);
            this.Name = "frmOptions";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nrLinks)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbtCustome;
        private System.Windows.Forms.RadioButton rbtAuto;
        private System.Windows.Forms.CheckBox chbRegisterWin;
        private System.Windows.Forms.NumericUpDown nrLinks;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dpTimeStart;
        private System.Windows.Forms.CheckBox chbSendApp;
        private System.Windows.Forms.CheckBox chbSendEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chbSchedule;
    }
}