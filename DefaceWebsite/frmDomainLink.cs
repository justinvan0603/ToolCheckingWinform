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
    public partial class frmDomainLink : Form
    {
        public frmDomainLink()
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
            OptionsClient clientOption = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                clientOption = new OptionsClient();                                 
                //danh sach optionlink
                Optionlinks_SearchResult[] data = clientOption.Optionlinks_Search(this.Domain);
                
                this.dtgData.Rows.Clear();
                int index = 0;

                foreach (Optionlinks_SearchResult item in data)
                {
                    this.dtgData.Rows.Add();
                    this.dtgData.Rows[index].Cells["ID"].Value = item.ID;
                    this.dtgData.Rows[index].Cells["OPTIONS_ID"].Value = item.OPTIONS_ID;
                    this.dtgData.Rows[index].Cells["LINK"].Value = item.LINK;
                    this.dtgData.Rows[index].Cells["DOMAIN_ID"].Value = item.DOMAIN_ID;
                    this.dtgData.Rows[index].Cells["RECORD_STATUS"].Value = item.RECORD_STATUS;
                    index++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetValues" + ex.Message);
            }
            finally
            {                
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
                        xel = new XElement("Link");
                        xel.Add(new XElement("OPTIONS_ID", (item.Cells["OPTIONS_ID"].Value == null ? this.OptionsId : item.Cells["OPTIONS_ID"].Value.ToString())));
                        xel.Add(new XElement("DOMAIN_ID", item.Cells["DOMAIN_ID"].Value.ToString()));
                        xel.Add(new XElement("LINK", item.Cells["LINK"].Value.ToString()));
                        xel.Add(new XElement("RECORD_STATUS", item.Cells["RECORD_STATUS"].Value));
                        data.Add(xel);
                    }
                }

                if (this.dicChiTiet.ContainsKey(this.OptionsId))
                    this.dicChiTiet[this.OptionsId] = data;
                else
                    this.dicChiTiet.Add(OptionsId, data);
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
