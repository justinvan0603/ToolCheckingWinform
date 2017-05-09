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
    public partial class frmDomainUser : Form
    {
        public frmDomainUser()
        {
            InitializeComponent();
        }

        public Dictionary<string, XElement> dicChiTiet;
        public int domainId;
        public string UserName;
        public string FullName;
        public string Domain;
        public string OptionsId;

        public void SetValues()
        {
            this.lblUserName.Text = this.UserName;
            this.lblFullname.Text = this.FullName;
            this.lblDomain.Text = this.Domain;
            UserClient client = null;
            OptionsClient clientOption = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                client = new UserClient();
                clientOption = new OptionsClient();
                //danh sach user
                Users_ByParentResult[] dataUser = client.Users_ByParent(null, this.UserName);
                //List<ItemCombo> lstUser = new List<ItemCombo>();
                                 
                //danh sach optionlink
                Optionsuser_SearchResult[] data = clientOption.Optionsuser_Search(this.Domain, "");
                if (this.dtgData.Columns.Contains("USER_ID"))
                    this.dtgData.Columns.Remove("USER_ID");
                if (this.dtgData.Columns.Contains("NOTES"))
                    this.dtgData.Columns.Remove("NOTES");
                this.dtgData.Rows.Clear();
                int index = 0;
                DataGridViewComboBoxColumn cbb;
                cbb = new DataGridViewComboBoxColumn();
                cbb.Name = "USER_ID";
                cbb.Width = 200;
                if (dataUser != null)
                {
                    foreach (var col in dataUser)
                        cbb.Items.Add(new ItemCombo() { Id = col.UserName, Name = col.UserName });
                }                  
                cbb.DisplayMember = "Name";
                cbb.ValueMember = "Name";
                
                DataGridViewTextBoxColumn note = new DataGridViewTextBoxColumn();
                note.Name = "NOTES";
                note.Width = 250;
                this.dtgData.Columns.Add(cbb);
                this.dtgData.Columns.Add(note);
                foreach (Optionsuser_SearchResult item in data)
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
                if (clientOption != null)
                    clientOption.Close();
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
                        xel = new XElement("User");
                        xel.Add(new XElement("DOMAIN", item.Cells["DOMAIN_ID"].Value.ToString()));
                        xel.Add(new XElement("USERNAME", item.Cells["USER_ID"].Value.ToString()));
                        xel.Add(new XElement("NOTES", item.Cells["NOTES"].Value));
                        data.Add(xel);
                    }
                }

                if (this.dicChiTiet.ContainsKey(this.OptionsId))
                    this.dicChiTiet[this.OptionsId] = data;
                else
                    this.dicChiTiet.Add(this.OptionsId, data);
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
