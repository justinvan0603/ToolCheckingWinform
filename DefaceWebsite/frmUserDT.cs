using DefaceWebsite.DFWService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DefaceWebsite
{
    public partial class frmUserDT : Form
    {
        public frmUserDT()
        {
            InitializeComponent();
        }

        public Dictionary<string, XElement> dicChiTiet;
        public string UserId;
        public string UserName;
        public string FullName;

        public void SetValues()
        {
            this.lblUserName.Text = this.UserName;
            this.lblFullname.Text = this.FullName;
            UserClient client = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                client = new UserClient();

                Userdomain_SearchResult[] data = client.Userdomain_Search(this.UserName, "");
                this.dtgData.Rows.Clear();

                //if (data != null)
                //{
                //    this.dtgData.DataSource = data;
                //    this.dtgData.Columns["ID"].Visible = false;
                //    //this.dtgData.Columns["USER_ID"].Visible = false;
                //    //this.dtgData.Columns["DOMAIN_ID"].Visible = false;                    
                //    //this.dtgData.Columns["NOTES"].Visible = false;   
                //}
                //DataTable dt = this.dtgData.DataSource as DataTable;
                //DataRow dr = new DataRow();

                int index = 0;
                foreach (Userdomain_SearchResult item in data)
                {
                    this.dtgData.Rows.Add();
                    this.dtgData.Rows[index].Cells["ID"].Value = item.ID;
                    this.dtgData.Rows[index].Cells["USER_ID"].Value = item.USER_ID;
                    this.dtgData.Rows[index].Cells["DOMAIN_ID"].Value = item.DOMAIN_ID;
                    this.dtgData.Rows[index].Cells["NOTES"].Value = item.NOTES;
                    index++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetValues" + ex.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
                this.Cursor = Cursors.Default;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //Tao xml chi tiet tien
                XElement data = new XElement("Root");
                XElement xel;
                foreach (DataGridViewRow item in this.dtgData.Rows)
                {
                    if (!item.IsNewRow)
                    {
                        xel = new XElement("Domain");
                        xel.Add(new XElement("DOMAIN", item.Cells["DOMAIN_ID"].Value.ToString()));
                        xel.Add(new XElement("DESCRIPTION", item.Cells["NOTES"].Value));
                        data.Add(xel);
                    }
                }

                if (this.dicChiTiet.ContainsKey(this.UserName))
                    this.dicChiTiet[this.UserName] = data;
                else
                    this.dicChiTiet.Add(this.UserName, data);
                //close form
                this.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnOk_Click" + ex.Message);
            }
        }
    }
}
