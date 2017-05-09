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
    public partial class frmDomainOption : Form
    {
        public frmDomainOption()
        {
            InitializeComponent();
        }

        private void frmDomainOption_Load(object sender, EventArgs e)
        {            
            this.cbLimit.Items.Add(new ItemCombo() { Id = "Y", Name = "Giới hạn" });
            this.cbLimit.Items.Add(new ItemCombo() { Id = "N", Name = "Không giới hạn" });
            this.cbLimit.Items.Add(new ItemCombo() { Id = "", Name = "---Tất cả---" });
            this.cbLimit.DisplayMember = "Name";
            this.cbLimit.ValueMember = "Id";

            this.cbLimit.SelectedIndex = 2;

            this.diclstLink = new Dictionary<string, XElement>();
            this.diclstUser = new Dictionary<string, XElement>();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OptionsClient client = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                client = new OptionsClient();
                Option_SearchResult search = new Option_SearchResult();
                search.USERNAME = this.txbUser.Text;
                search.DOMAIN_ID = this.txbDomain.Text;
                search.IS_LIMIT = (this.cbLimit.SelectedItem != null ? "" : (this.cbLimit.SelectedItem as ItemCombo).Id);
                int times;
                int.TryParse(this.txbTimes.Text, out times);
                search.TIMES = times;                
                search.RECORD_STATUS = this.chbDeleted.Checked ? "0" : "1";
                Option_SearchResult[] data = client.Option_Search(search);
                if (this.dtgData.Columns != null)
                    this.dtgData.Columns.Clear();

                DataGridViewImageColumn cl = new DataGridViewImageColumn();
                cl.HeaderText = "Lưu";
                cl.Name = "SAVE";
                cl.Width = 30;
                cl.Image = Properties.Resources.approve_icon;
                this.dtgData.Columns.Add(cl);

                cl = new DataGridViewImageColumn();
                cl.HeaderText = "Sửa Link";
                cl.Name = "COUNT";
                cl.Image = Properties.Resources.edit;
                cl.Width = 55;
                this.dtgData.Columns.Add(cl);

                cl = new DataGridViewImageColumn();
                cl.HeaderText = "Sửa User";
                cl.Name = "USERDT";
                cl.Image = Properties.Resources.edit;
                cl.Width = 65;
                this.dtgData.Columns.Add(cl);

                if (data != null)
                {
                    this.dtgData.DataSource = data;
                    this.dtgData.Columns["ID"].Visible = false;
                    this.dtgData.Columns["AUTH_STATUS"].Visible = false;
                    this.dtgData.Columns["APPROVE_DT"].Visible = false;
                    this.dtgData.Columns["MAKER_ID"].Visible = false;
                    this.dtgData.Columns["CHECKER_ID"].Visible = false;
                    this.dtgData.Columns["EDITOR_ID"].Visible = false;                    
                    this.dtgData.Columns["PARENT_ID"].Visible = false;

                    this.dtgData.Columns["USERNAME"].DisplayIndex = 3;
                    this.dtgData.Columns["FULLNAME"].DisplayIndex = 4;
                    this.dtgData.Columns["DOMAIN_ID"].DisplayIndex = 5;
                    this.dtgData.Columns["TIMES"].DisplayIndex = 6;
                    
                    //this.dtgData.Columns["SCH_DATE"].Width = 90;
                    //this.dtgData.Columns["EVENT_TIME"].Width = 90;
                    //this.dtgData.Columns["SCH_TERM"].Width = 70;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("btnSearch_Click" + ex.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
                this.Cursor = Cursors.Default;
            }
        }

        private void txbTimes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        
        public Dictionary<string, XElement> diclstUser;
        public Dictionary<string, XElement> diclstLink;
        private frmDomainUser frmDUser;
        private frmDomainLink frmDLink;
        private void dtgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)//luu
            {
                DialogResult result = MessageBox.Show("Bạn có muốn lưu không? Các thông tin liên quan sẽ thay đổi!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    OptionsClient client = null;
                    try
                    {
                        Option_SearchResult data = new Option_SearchResult();
                        data.ID = int.Parse(this.dtgData["ID", e.RowIndex].Value.ToString());
                        data.DOMAIN_ID = this.dtgData["DOMAIN_ID", e.RowIndex].Value.ToString();
                        data.IS_LIMIT = this.dtgData["IS_LIMIT", e.RowIndex].Value.ToString();
                        int times = 1;
                        if (this.dtgData["TIMES", e.RowIndex].Value != null)
                            int.TryParse(this.dtgData["TIMES", e.RowIndex].Value.ToString(), out times);
                        data.TIMES = times;
                        if (this.dtgData["DESCRIPTION", e.RowIndex].Value != null)
                            data.DESCRIPTION = this.dtgData["DESCRIPTION", e.RowIndex].Value.ToString();
                        data.EDIT_DT = DateTime.Now;
                        if (this.dtgData["CREATE_DT", e.RowIndex].Value != null)
                            data.CREATE_DT = DateTime.Parse(this.dtgData["CREATE_DT", e.RowIndex].Value.ToString());
                        if (this.dtgData["RECORD_STATUS", e.RowIndex].Value != null)
                            data.RECORD_STATUS = this.dtgData["RECORD_STATUS", e.RowIndex].Value.ToString();

                        XElement xmllink = new XElement("Root");
                        XElement xmluser = new XElement("Root");
                        string isUserEdit = "0";
                        string isLinkEdit = "0";
                        if (this.diclstUser.ContainsKey(data.ID.ToString()))
                        {
                            xmluser = this.diclstUser[data.ID.ToString()];
                            isUserEdit = "1";//co chinh sua chi tiet
                        }
                        if (this.diclstLink.ContainsKey(data.ID.ToString()))
                        {
                            xmllink = this.diclstLink[data.ID.ToString()];
                            isLinkEdit = "1";//co chinh sua chi tiet
                        }
                        client = new OptionsClient();
                        Options_UpdResult res = client.Options_Upd(data, xmluser, isUserEdit, xmllink, isLinkEdit);
                        if (res.Result == "0")
                            MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Cập nhật thất bại: " + res.ErrorDesc, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("dtgData_CellContentClick: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (client != null)
                            client.Close();
                    }
                }
            }
            else if (e.ColumnIndex == 1)//chi tiet link
            {

                if (this.frmDLink == null)
                {
                    this.frmDLink = new frmDomainLink();
                }
                this.frmDLink.OptionsId = this.dtgData["ID", e.RowIndex].Value.ToString();
                this.frmDLink.dicChiTiet = this.diclstLink;
                this.frmDLink.UserName = this.dtgData["USERNAME", e.RowIndex].Value.ToString();
                this.frmDLink.FullName = this.dtgData["FULLNAME", e.RowIndex].Value.ToString();
                this.frmDLink.domainId = int.Parse(this.dtgData["ID", e.RowIndex].Value.ToString());
                this.frmDLink.Domain = this.dtgData["DOMAIN_ID", e.RowIndex].Value.ToString();
                
                this.frmDLink.SetValues();
                this.frmDLink.ShowDialog();
            }
            else if (e.ColumnIndex == 2)// chi tiet user
            {
                if (this.frmDUser == null)
                {
                    this.frmDUser = new frmDomainUser();
                }

                this.frmDUser.dicChiTiet = this.diclstUser;
                this.frmDUser.OptionsId = this.dtgData["ID", e.RowIndex].Value.ToString();
                this.frmDUser.UserName = this.dtgData["USERNAME", e.RowIndex].Value.ToString();
                this.frmDUser.FullName = this.dtgData["FULLNAME", e.RowIndex].Value.ToString();
                this.frmDUser.domainId = int.Parse(this.dtgData["ID", e.RowIndex].Value.ToString());
                this.frmDUser.Domain = this.dtgData["DOMAIN_ID", e.RowIndex].Value.ToString();

                this.frmDUser.SetValues();
                this.frmDUser.ShowDialog();
            }
        }

        
    }
}
