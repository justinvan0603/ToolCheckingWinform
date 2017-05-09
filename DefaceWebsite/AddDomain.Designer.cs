namespace DefaceWebsite
{
    partial class AddDomain
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbDomain = new System.Windows.Forms.TextBox();
            this.txbDesc = new System.Windows.Forms.TextBox();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.chbClose = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Người quản lý";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên domain";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Thêm mới domain";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Mô tả";
            // 
            // txbDomain
            // 
            this.txbDomain.Location = new System.Drawing.Point(128, 63);
            this.txbDomain.Name = "txbDomain";
            this.txbDomain.Size = new System.Drawing.Size(270, 20);
            this.txbDomain.TabIndex = 3;
            // 
            // txbDesc
            // 
            this.txbDesc.Location = new System.Drawing.Point(128, 127);
            this.txbDesc.Multiline = true;
            this.txbDesc.Name = "txbDesc";
            this.txbDesc.Size = new System.Drawing.Size(270, 138);
            this.txbDesc.TabIndex = 3;
            // 
            // cbUser
            // 
            this.cbUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Location = new System.Drawing.Point(128, 93);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(270, 21);
            this.cbUser.TabIndex = 4;
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(323, 271);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 5;
            this.btnInsert.Text = "Tạo";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // chbClose
            // 
            this.chbClose.AutoSize = true;
            this.chbClose.Checked = true;
            this.chbClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbClose.Location = new System.Drawing.Point(150, 275);
            this.chbClose.Name = "chbClose";
            this.chbClose.Size = new System.Drawing.Size(167, 17);
            this.chbClose.TabIndex = 6;
            this.chbClose.Text = "Đóng form khi tạo thành công";
            this.chbClose.UseVisualStyleBackColor = true;
            // 
            // AddDomain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 321);
            this.Controls.Add(this.chbClose);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.cbUser);
            this.Controls.Add(this.txbDesc);
            this.Controls.Add(this.txbDomain);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "AddDomain";
            this.Text = "AddDomain";
            this.Load += new System.EventHandler(this.AddDomain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbDomain;
        private System.Windows.Forms.TextBox txbDesc;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.CheckBox chbClose;
    }
}