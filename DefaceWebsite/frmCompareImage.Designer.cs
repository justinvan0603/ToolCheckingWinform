namespace DefaceWebsite
{
    partial class frmCompareImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompareImage));
            this.txbDomain = new System.Windows.Forms.TextBox();
            this.picImage1 = new System.Windows.Forms.PictureBox();
            this.picImage2 = new System.Windows.Forms.PictureBox();
            this.picResult = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCompare = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProcess = new System.Windows.Forms.Label();
            this.btnGetSource = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picImage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).BeginInit();
            this.SuspendLayout();
            // 
            // txbDomain
            // 
            this.txbDomain.Location = new System.Drawing.Point(83, 7);
            this.txbDomain.Name = "txbDomain";
            this.txbDomain.Size = new System.Drawing.Size(277, 20);
            this.txbDomain.TabIndex = 0;
            // 
            // picImage1
            // 
            this.picImage1.Image = ((System.Drawing.Image)(resources.GetObject("picImage1.Image")));
            this.picImage1.Location = new System.Drawing.Point(14, 50);
            this.picImage1.Name = "picImage1";
            this.picImage1.Size = new System.Drawing.Size(384, 218);
            this.picImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImage1.TabIndex = 1;
            this.picImage1.TabStop = false;
            // 
            // picImage2
            // 
            this.picImage2.Image = ((System.Drawing.Image)(resources.GetObject("picImage2.Image")));
            this.picImage2.Location = new System.Drawing.Point(413, 50);
            this.picImage2.Name = "picImage2";
            this.picImage2.Size = new System.Drawing.Size(384, 218);
            this.picImage2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImage2.TabIndex = 1;
            this.picImage2.TabStop = false;
            // 
            // picResult
            // 
            this.picResult.Location = new System.Drawing.Point(811, 50);
            this.picResult.Name = "picResult";
            this.picResult.Size = new System.Drawing.Size(384, 218);
            this.picResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picResult.TabIndex = 1;
            this.picResult.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Domain";
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(384, 6);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 3;
            this.btnCompare.Text = "Kiểm tra";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(487, 11);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(44, 13);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "Kết quả";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hình mới";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(411, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Hình cũ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(808, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Khác so với cũ";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 563);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(785, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // lblProcess
            // 
            this.lblProcess.AutoSize = true;
            this.lblProcess.Location = new System.Drawing.Point(818, 576);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(51, 13);
            this.lblProcess.TabIndex = 2;
            this.lblProcess.Text = "Tiến trình";
            // 
            // btnGetSource
            // 
            this.btnGetSource.Location = new System.Drawing.Point(732, 7);
            this.btnGetSource.Name = "btnGetSource";
            this.btnGetSource.Size = new System.Drawing.Size(103, 23);
            this.btnGetSource.TabIndex = 3;
            this.btnGetSource.Text = "Lấy mã nguồn";
            this.btnGetSource.UseVisualStyleBackColor = true;
            this.btnGetSource.Click += new System.EventHandler(this.btnGetSource_Click);
            // 
            // frmCompareImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 598);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnGetSource);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.lblProcess);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picResult);
            this.Controls.Add(this.picImage2);
            this.Controls.Add(this.picImage1);
            this.Controls.Add(this.txbDomain);
            this.Name = "frmCompareImage";
            this.Text = "So sánh ảnh";
            ((System.ComponentModel.ISupportInitialize)(this.picImage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbDomain;
        private System.Windows.Forms.PictureBox picImage1;
        private System.Windows.Forms.PictureBox picImage2;
        private System.Windows.Forms.PictureBox picResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.Button btnGetSource;
    }
}