namespace DefaceWebsite
{
    partial class frmListDomain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dpDate = new System.Windows.Forms.DateTimePicker();
            this.chbDate = new System.Windows.Forms.CheckBox();
            this.chbDeleted = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txbUser = new System.Windows.Forms.TextBox();
            this.txbDomain = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgData = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.btnInsert = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dpDate);
            this.groupBox1.Controls.Add(this.chbDate);
            this.groupBox1.Controls.Add(this.chbDeleted);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txbUser);
            this.groupBox1.Controls.Add(this.txbDomain);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(676, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Điều kiện lọc";
            // 
            // dpDate
            // 
            this.dpDate.CustomFormat = "dd/MM/yyyy";
            this.dpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpDate.Location = new System.Drawing.Point(451, 27);
            this.dpDate.Name = "dpDate";
            this.dpDate.Size = new System.Drawing.Size(95, 20);
            this.dpDate.TabIndex = 4;
            // 
            // chbDate
            // 
            this.chbDate.AutoSize = true;
            this.chbDate.Checked = true;
            this.chbDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbDate.Location = new System.Drawing.Point(552, 30);
            this.chbDate.Name = "chbDate";
            this.chbDate.Size = new System.Drawing.Size(66, 17);
            this.chbDate.TabIndex = 3;
            this.chbDate.Text = "Bỏ chọn";
            this.chbDate.UseVisualStyleBackColor = true;
            // 
            // chbDeleted
            // 
            this.chbDeleted.AutoSize = true;
            this.chbDeleted.Location = new System.Drawing.Point(451, 57);
            this.chbDeleted.Name = "chbDeleted";
            this.chbDeleted.Size = new System.Drawing.Size(60, 17);
            this.chbDeleted.TabIndex = 3;
            this.chbDeleted.Text = "Đã xóa";
            this.chbDeleted.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(580, 50);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txbUser
            // 
            this.txbUser.Location = new System.Drawing.Point(122, 57);
            this.txbUser.Name = "txbUser";
            this.txbUser.Size = new System.Drawing.Size(220, 20);
            this.txbUser.TabIndex = 1;
            // 
            // txbDomain
            // 
            this.txbDomain.Location = new System.Drawing.Point(122, 27);
            this.txbDomain.Name = "txbDomain";
            this.txbDomain.Size = new System.Drawing.Size(220, 20);
            this.txbDomain.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(374, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ngày tạo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Người quản lý";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên domain";
            // 
            // dtgData
            // 
            this.dtgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgData.Location = new System.Drawing.Point(12, 133);
            this.dtgData.Name = "dtgData";
            this.dtgData.Size = new System.Drawing.Size(676, 239);
            this.dtgData.TabIndex = 1;
            this.dtgData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgData_CellContentClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Danh sách domains";
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(613, 107);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 2;
            this.btnInsert.Text = "Thêm mới";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // frmListDomain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 384);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.dtgData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Name = "frmListDomain";
            this.Text = "Danh sách domains";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbDeleted;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txbUser;
        private System.Windows.Forms.TextBox txbDomain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dpDate;
        private System.Windows.Forms.DataGridView dtgData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chbDate;
        private System.Windows.Forms.Button btnInsert;
    }
}