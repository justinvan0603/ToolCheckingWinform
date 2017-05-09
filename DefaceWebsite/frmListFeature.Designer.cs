namespace DefaceWebsite
{
    partial class frmListFeature
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
            this.tbnInsert = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chbDeleted = new System.Windows.Forms.CheckBox();
            this.cbAuth = new System.Windows.Forms.ComboBox();
            this.txbLevel = new System.Windows.Forms.TextBox();
            this.txbResouce = new System.Windows.Forms.TextBox();
            this.txbContent = new System.Windows.Forms.TextBox();
            this.txbFeaType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgData = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbnInsert);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.chbDeleted);
            this.groupBox1.Controls.Add(this.cbAuth);
            this.groupBox1.Controls.Add(this.txbLevel);
            this.groupBox1.Controls.Add(this.txbResouce);
            this.groupBox1.Controls.Add(this.txbContent);
            this.groupBox1.Controls.Add(this.txbFeaType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 133);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Điều kiện lọc";
            // 
            // tbnInsert
            // 
            this.tbnInsert.Location = new System.Drawing.Point(421, 104);
            this.tbnInsert.Name = "tbnInsert";
            this.tbnInsert.Size = new System.Drawing.Size(75, 23);
            this.tbnInsert.TabIndex = 7;
            this.tbnInsert.Text = "Thêm mới";
            this.tbnInsert.UseVisualStyleBackColor = true;
            this.tbnInsert.Click += new System.EventHandler(this.tbnInsert_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(502, 104);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chbDeleted
            // 
            this.chbDeleted.AutoSize = true;
            this.chbDeleted.Location = new System.Drawing.Point(385, 78);
            this.chbDeleted.Name = "chbDeleted";
            this.chbDeleted.Size = new System.Drawing.Size(60, 17);
            this.chbDeleted.TabIndex = 6;
            this.chbDeleted.Text = "Đã xóa";
            this.chbDeleted.UseVisualStyleBackColor = true;
            // 
            // cbAuth
            // 
            this.cbAuth.FormattingEnabled = true;
            this.cbAuth.Location = new System.Drawing.Point(385, 25);
            this.cbAuth.Name = "cbAuth";
            this.cbAuth.Size = new System.Drawing.Size(192, 21);
            this.cbAuth.TabIndex = 4;
            // 
            // txbLevel
            // 
            this.txbLevel.Location = new System.Drawing.Point(385, 50);
            this.txbLevel.Name = "txbLevel";
            this.txbLevel.Size = new System.Drawing.Size(192, 20);
            this.txbLevel.TabIndex = 5;
            // 
            // txbResouce
            // 
            this.txbResouce.Location = new System.Drawing.Point(115, 76);
            this.txbResouce.Name = "txbResouce";
            this.txbResouce.Size = new System.Drawing.Size(203, 20);
            this.txbResouce.TabIndex = 3;
            // 
            // txbContent
            // 
            this.txbContent.Location = new System.Drawing.Point(115, 50);
            this.txbContent.Name = "txbContent";
            this.txbContent.Size = new System.Drawing.Size(203, 20);
            this.txbContent.TabIndex = 2;
            // 
            // txbFeaType
            // 
            this.txbFeaType.Location = new System.Drawing.Point(115, 25);
            this.txbFeaType.Name = "txbFeaType";
            this.txbFeaType.Size = new System.Drawing.Size(203, 20);
            this.txbFeaType.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(324, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Mức độ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Trạng thái";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nguồn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nội dung";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loại đặc điểm";
            // 
            // dtgData
            // 
            this.dtgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgData.Location = new System.Drawing.Point(12, 169);
            this.dtgData.Name = "dtgData";
            this.dtgData.Size = new System.Drawing.Size(637, 283);
            this.dtgData.TabIndex = 1;
            this.dtgData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgData_CellContentClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Danh sách đặc điểm";
            // 
            // frmListFeature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 464);
            this.Controls.Add(this.dtgData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Name = "frmListFeature";
            this.Text = "Danh sách đặc điểm";
            this.Load += new System.EventHandler(this.frmListFeature_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbAuth;
        private System.Windows.Forms.TextBox txbLevel;
        private System.Windows.Forms.TextBox txbResouce;
        private System.Windows.Forms.TextBox txbContent;
        private System.Windows.Forms.TextBox txbFeaType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chbDeleted;
        private System.Windows.Forms.DataGridView dtgData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button tbnInsert;
    }
}