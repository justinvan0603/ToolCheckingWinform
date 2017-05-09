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

namespace DefaceWebsite
{
    public partial class frmListDomain : Form
    {
        public frmListDomain()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ListDomainClient client = null;
            try
            {                
                this.Cursor = Cursors.WaitCursor;
                client = new ListDomainClient();
                DateTime? date = null;
                if (!this.chbDate.Checked)
                    date = this.dpDate.Value;
                Listdomain_SearchResult[] data = client.Listdomain_Search(this.txbUser.Text, this.txbDomain.Text, date, this.chbDeleted.Checked ? "0" : "1");
                if (this.dtgData.Columns != null)
                    this.dtgData.Columns.Clear();

                DataGridViewImageColumn cl = new DataGridViewImageColumn();
                cl.HeaderText = "Lưu";
                cl.Name = "SAVE";
                cl.Width = 30;
                cl.Image = Properties.Resources.approve_icon;
                this.dtgData.Columns.Add(cl);

                //cl = new DataGridViewImageColumn();
                //cl.HeaderText = "Chỉnh sửa";
                //cl.Name = "COUNT";
                //cl.Image = Properties.Resources.edit;
                //cl.Width = 55;
                //this.dtgData.Columns.Add(cl);

                cl = new DataGridViewImageColumn();
                cl.HeaderText = "Xóa";
                cl.Name = "DELETE";
                cl.Image = Properties.Resources.deletered;
                cl.Width = 55;
                this.dtgData.Columns.Add(cl);

                if (data != null)
                {
                    this.dtgData.DataSource = data;
                    this.dtgData.Columns["ID"].Visible = false;
                    this.dtgData.Columns["USER_ID"].Visible = false;
                    this.dtgData.Columns["AUTH_STATUS"].Visible = false;
                    this.dtgData.Columns["APPROVE_DT"].Visible = false;
                    this.dtgData.Columns["MAKER_ID"].Visible = false;
                    this.dtgData.Columns["CHECKER_ID"].Visible = false;
                    this.dtgData.Columns["EDITOR_ID"].Visible = false;
                    
                    
                    //this.dtgData.Columns["SCH_DATE"].Width = 90;
                    //this.dtgData.Columns["EVENT_TIME"].Width = 90;
                    //this.dtgData.Columns["SCH_TERM"].Width = 70;
                }                           

            }
            catch (Exception ex)
            {
                MessageBox.Show("btnLoad_Click" + ex.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
                this.Cursor = Cursors.Default;
            }
        }

        private void dtgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)//luu
            {
                DialogResult result = MessageBox.Show("Bạn có muốn lưu không? Các thông tin liên quan đến domain này sẽ thay đổi!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    ListDomainClient client = null;
                    try
                    {
                        Listdomain_SearchResult data = new Listdomain_SearchResult();
                        data.ID = int.Parse(this.dtgData["ID", e.RowIndex].Value.ToString());
                        data.DOMAIN = this.dtgData["DOMAIN", e.RowIndex].Value.ToString();
                        data.USERNAME = this.dtgData["USERNAME", e.RowIndex].Value.ToString();
                        data.EDIT_DT = DateTime.Now;
                        data.DESCRIPTION = this.dtgData["DESCRIPTION", e.RowIndex].Value.ToString();
                        //data.USER_ID = this.dtgData["USER_ID", e.RowIndex].Value.ToString();
                        data.RECORD_STATUS = this.dtgData["RECORD_STATUS", e.RowIndex].Value.ToString();
                        data.CREATE_DT = DateTime.Parse(this.dtgData["CREATE_DT", e.RowIndex].Value.ToString());

                        client = new ListDomainClient();
                        Listdomain_UpdResult res = client.Listdomain_Upd(data);
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
            else if (e.ColumnIndex == 1)//xoa
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xóa không? Các thông tin liên quan đến domain này sẽ bị xóa!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    ListDomainClient client = null;
                    try
                    {
                        client = new ListDomainClient();
                        Listdomain_DelResult res = client.Listdomain_Del(int.Parse(this.dtgData["ID", e.RowIndex].Value.ToString()));
                        if (res.Result == "0")
                            MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Xóa thất bại: " + res.ErrorDesc, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            AddDomain frm = new AddDomain();
            frm.ShowDialog();
        }
    }
}
