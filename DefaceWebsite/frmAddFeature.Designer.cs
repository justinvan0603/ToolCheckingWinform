namespace DefaceWebsite
{
    partial class frmAddFeature
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
            this.btnInsert = new System.Windows.Forms.Button();
            this.txbLevel = new System.Windows.Forms.TextBox();
            this.txbResouce = new System.Windows.Forms.TextBox();
            this.txbContent = new System.Windows.Forms.TextBox();
            this.txbFeaType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chbClose = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(345, 225);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 7;
            this.btnInsert.Text = "Ok";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // txbLevel
            // 
            this.txbLevel.Location = new System.Drawing.Point(95, 203);
            this.txbLevel.Name = "txbLevel";
            this.txbLevel.Size = new System.Drawing.Size(203, 20);
            this.txbLevel.TabIndex = 15;
            this.txbLevel.Text = "1";
            // 
            // txbResouce
            // 
            this.txbResouce.Location = new System.Drawing.Point(95, 178);
            this.txbResouce.Name = "txbResouce";
            this.txbResouce.Size = new System.Drawing.Size(325, 20);
            this.txbResouce.TabIndex = 14;
            this.txbResouce.Text = "SYS";
            // 
            // txbContent
            // 
            this.txbContent.Location = new System.Drawing.Point(95, 42);
            this.txbContent.Multiline = true;
            this.txbContent.Name = "txbContent";
            this.txbContent.Size = new System.Drawing.Size(325, 130);
            this.txbContent.TabIndex = 13;
            // 
            // txbFeaType
            // 
            this.txbFeaType.Location = new System.Drawing.Point(95, 17);
            this.txbFeaType.Name = "txbFeaType";
            this.txbFeaType.Size = new System.Drawing.Size(203, 20);
            this.txbFeaType.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Mức độ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Nguồn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nội dung";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Loại đặc điểm";
            // 
            // chbClose
            // 
            this.chbClose.AutoSize = true;
            this.chbClose.Checked = true;
            this.chbClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbClose.Location = new System.Drawing.Point(95, 229);
            this.chbClose.Name = "chbClose";
            this.chbClose.Size = new System.Drawing.Size(167, 17);
            this.chbClose.TabIndex = 16;
            this.chbClose.Text = "Đóng form khi tạo thành công";
            this.chbClose.UseVisualStyleBackColor = true;
            // 
            // frmAddFeature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 278);
            this.Controls.Add(this.chbClose);
            this.Controls.Add(this.txbLevel);
            this.Controls.Add(this.txbResouce);
            this.Controls.Add(this.txbContent);
            this.Controls.Add(this.txbFeaType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInsert);
            this.Name = "frmAddFeature";
            this.Text = "Add feature";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.TextBox txbLevel;
        private System.Windows.Forms.TextBox txbResouce;
        private System.Windows.Forms.TextBox txbContent;
        private System.Windows.Forms.TextBox txbFeaType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbClose;
    }
}