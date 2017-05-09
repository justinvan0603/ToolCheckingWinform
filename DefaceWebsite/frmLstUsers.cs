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
    public partial class frmLstUsers : Form
    {
        public frmLstUsers()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            UserClient client = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                client = new UserClient();
                Users_SearchResult search = new Users_SearchResult();
                search.UserName = this.txbUsername.Text;
                search.FULLNAME = this.txbFullname.Text;
                int phone;
                int.TryParse(this.txbPhone.Text, out phone);
                search.PHONE = phone;// this.txbPhone.Text == "" ? null : int.Parse(this.txbPhone.Text);
                search.Email = this.txbEmail.Text;
                search.RECORD_STATUS = this.chbDeleted.Checked ? "1" : "0";
                Users_SearchResult[] data = client.Users_Search(search, 10000);
                if (this.dtgData.Columns != null)
                    this.dtgData.Columns.Clear();

                DataGridViewImageColumn cl = new DataGridViewImageColumn();
                cl.HeaderText = "Lưu";
                cl.Name = "SAVE";
                cl.Width = 30;
                cl.Image = Properties.Resources.approve_icon;
                this.dtgData.Columns.Add(cl);

                cl = new DataGridViewImageColumn();
                cl.HeaderText = "Chỉnh sửa";
                cl.Name = "COUNT";
                cl.Image = Properties.Resources.edit;
                cl.Width = 55;
                this.dtgData.Columns.Add(cl);

                cl = new DataGridViewImageColumn();
                cl.HeaderText = "Xóa";
                cl.Name = "DELETE";
                cl.Image = Properties.Resources.deletered;
                cl.Width = 55;
                this.dtgData.Columns.Add(cl);

                if (data != null)
                {
                    this.dtgData.DataSource = data;
                    this.dtgData.Columns["Id"].Visible = false;                    
                    this.dtgData.Columns["AUTH_STATUS"].Visible = false;
                    this.dtgData.Columns["APPROVE_DT"].Visible = false;
                    this.dtgData.Columns["MAKER_ID"].Visible = false;
                    this.dtgData.Columns["CHECKER_ID"].Visible = false;
                    this.dtgData.Columns["EDITOR_ID"].Visible = false;
                    this.dtgData.Columns["PASSWORD"].Visible = false;
                    this.dtgData.Columns["PARENT_ID"].Visible = false;
                    this.dtgData.Columns["APPTOKEN"].Visible = false;
                    this.dtgData.Columns["ConcurrencyStamp"].Visible = false;
                    this.dtgData.Columns["EmailConfirmed"].Visible = false;
                    this.dtgData.Columns["LockoutEnabled"].Visible = false;
                    this.dtgData.Columns["LockoutEnd"].Visible = false;
                    this.dtgData.Columns["NormalizedEmail"].Visible = false;
                    this.dtgData.Columns["NormalizedUserName"].Visible = false;
                    this.dtgData.Columns["PasswordHash"].Visible = false;
                    this.dtgData.Columns["SecurityStamp"].Visible = false;
                    this.dtgData.Columns["PhoneNumberConfirmed"].Visible = false;
                    this.dtgData.Columns["TwoFactorEnabled"].Visible = false;
                    

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

        private void txbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            //// only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }
        public Dictionary<string, XElement> dicChiTiet;
        private frmUserDT fChiTiet;
        private frmAddUser fAddnew;

        private void dtgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)//luu
            {
                DialogResult result = MessageBox.Show("Bạn có muốn lưu không? Các thông tin liên quan đến user này sẽ thay đổi!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    UserClient client = null;
                    try
                    {
                        Users_SearchResult data = new Users_SearchResult();
                        data.Id = this.dtgData["Id", e.RowIndex].Value.ToString();
                        data.UserName = this.dtgData["UserName", e.RowIndex].Value.ToString();
                        data.FULLNAME = this.dtgData["FULLNAME", e.RowIndex].Value.ToString();
                        data.EDIT_DT = DateTime.Now;
                        data.DESCRIPTION = this.dtgData["DESCRIPTION", e.RowIndex].Value.ToString();
                        int phone;
                        int.TryParse(this.dtgData["PHONE", e.RowIndex].Value.ToString(), out phone);
                        data.PHONE = phone;// int.Parse(this.dtgData["PHONE", e.RowIndex].Value.ToString());
                        data.Email = this.dtgData["Email", e.RowIndex].Value.ToString();
                        data.PASSWORD = this.dtgData["PASSWORD", e.RowIndex].Value.ToString();
                        data.RECORD_STATUS = this.dtgData["RECORD_STATUS", e.RowIndex].Value.ToString();
                        if (this.dtgData["PARENT_ID", e.RowIndex].Value != null)
                            data.PARENT_ID = this.dtgData["PARENT_ID", e.RowIndex].Value.ToString();
                        data.CREATE_DT = DateTime.Parse(this.dtgData["CREATE_DT", e.RowIndex].Value.ToString());

                        XElement xml = new XElement("Root");
                        string isEditDetail = "0";
                        if (this.dicChiTiet.ContainsKey(data.UserName))
                        {
                            xml = this.dicChiTiet[this.dtgData["UserName", e.RowIndex].Value.ToString()];
                            isEditDetail = "1";//co chinh sua chi tiet
                        }
                        client = new UserClient();
                        Users_UpdResult res = client.Users_Upd(data.Id, data.UserName, data.FULLNAME, data.PASSWORD, data.Email, data.PHONE, data.PARENT_ID,
                            data.DESCRIPTION, data.RECORD_STATUS, data.AUTH_STATUS, data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(StaticClass.formatDate),
                            data.APPROVE_DT == null ? "" : data.APPROVE_DT.Value.ToString(StaticClass.formatDate),
                             data.EDIT_DT == null ? "" : data.EDIT_DT.Value.ToString(StaticClass.formatDate), data.MAKER_ID, data.CHECKER_ID, data.EDITOR_ID, xml, isEditDetail);
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
            else if (e.ColumnIndex == 1)//chi tiet
            {

                if (this.fChiTiet == null)
                {
                    this.fChiTiet = new frmUserDT();
                }

                this.fChiTiet.dicChiTiet = this.dicChiTiet;
                this.fChiTiet.UserId = this.dtgData["Id", e.RowIndex].Value.ToString(); 
                this.fChiTiet.UserName = this.dtgData["UserName", e.RowIndex].Value.ToString(); 
                this.fChiTiet.FullName = this.dtgData["FULLNAME", e.RowIndex].Value.ToString();
                this.fChiTiet.SetValues();
                this.fChiTiet.ShowDialog();
            }
            else if (e.ColumnIndex == 2)//xoa
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xóa không? Các thông tin liên quan đến user này sẽ bị xóa!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    UserClient client = null;
                    try
                    {
                        client = new UserClient();
                        Users_DelResult res = client.Users_Del(this.dtgData["Id", e.RowIndex].Value.ToString());
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

        private void frmLstUsers_Load(object sender, EventArgs e)
        {
            this.dicChiTiet = new Dictionary<string, XElement>();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.fAddnew == null)
            {
                this.fAddnew = new frmAddUser();
            }
            this.fAddnew.ShowDialog();
        }
    }
}
