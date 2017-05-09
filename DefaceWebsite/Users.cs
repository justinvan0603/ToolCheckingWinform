using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefaceWebsite
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            //DataGridViewImageColumn cl = new DataGridViewImageColumn();
            //cl.HeaderText = "Lưu";
            //cl.Name = "SAVE";
            //cl.Width = 30;
            //cl.Image = new Bitmap("approve-icon.png");
            //this.dataGridView1.Columns.Insert(0, cl);

            //cl = new DataGridViewImageColumn();
            //cl.HeaderText = "Chi tiết tiền";
            //cl.Name = "COUNT";
            //cl.Image = new Bitmap("edit.png"); 
            //cl.Width = 55;
            //this.dataGridView1.Columns.Insert(1, cl);
        }
    }
}
