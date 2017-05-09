using DefaceWebsite.DFWService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DefaceWebsite
{
    public partial class frmAddUser : Form
    {
        public frmAddUser()
        {
            InitializeComponent();
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            UserClient client = null;
            try
            {
                client = new UserClient();
                Users_SearchResult[] data = client.Users_Search(new Users_SearchResult(), 10000);

                if (data != null)
                {
                    this.cbUser.DataSource = data;
                    this.cbUser.DisplayMember = "USERNAME";
                    this.cbUser.ValueMember = "ID";
                    this.cbUser.AutoCompleteSource = AutoCompleteSource.ListItems;
                    this.cbUser.SelectedItem = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("AddDomain_Load: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (this.txbUsername.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.txbEmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var em = new System.Net.Mail.MailAddress(this.txbEmail.Text);
            }
            catch (Exception)
            {
                
                    MessageBox.Show("Email không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;                
            } 
//            Regex emailRegex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?
//                                ^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+
//                                [a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

//            if (emailRegex.IsMatch(this.txbEmail.Text))
//            {
//                MessageBox.Show("Email không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }
            
            Users_SearchResult use = (this.cbUser.SelectedItem as Users_SearchResult);
            //if (use == null)
            //{
            //    MessageBox.Show("Vui lòng nhập người quản lý.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            UserClient client = null;
            try
            {
                client = new UserClient();
                Users_SearchResult data = new Users_SearchResult();
                //data.ID = int.Parse(this.dtgData["ID", e.RowIndex].Value.ToString());
                data.UserName = this.txbUsername.Text;
                data.FULLNAME = this.txbFullname.Text;
                data.CREATE_DT = DateTime.Now;
                data.DESCRIPTION = this.txbDesc.Text;
                int phone;
                int.TryParse(this.txbPhone.Text, out phone);
                data.PHONE = phone;
                data.Email = this.txbEmail.Text;
                data.PASSWORD = StaticClass.MD5Hash(this.txbUsername.Text + "@123");
                data.RECORD_STATUS = "1";
                if (use != null)
                    data.PARENT_ID = use.Id;
                data.AUTH_STATUS = "A";

                XElement dataXML = new XElement("Root");
                XElement xel;
                foreach (DataGridViewRow item in this.dtgData.Rows)
                {
                    if (!item.IsNewRow)
                    {
                        xel = new XElement("Domain");
                        xel.Add(new XElement("DOMAIN", item.Cells["DOMAIN_ID"].Value.ToString()));
                        xel.Add(new XElement("DESCRIPTION", item.Cells["NOTES"].Value));
                        dataXML.Add(xel);
                    }
                }
                client = new UserClient();
                Users_InsResult res = client.Users_Ins(data.UserName, data.FULLNAME, data.PASSWORD, data.Email, data.PHONE, data.PARENT_ID,
                    data.DESCRIPTION, data.RECORD_STATUS, data.AUTH_STATUS, data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(StaticClass.formatDate),
                    data.APPROVE_DT == null ? "" : data.APPROVE_DT.Value.ToString(StaticClass.formatDate),
                     data.EDIT_DT == null ? "" : data.EDIT_DT.Value.ToString(StaticClass.formatDate), data.MAKER_ID, data.CHECKER_ID, data.EDITOR_ID, dataXML);
                
                if (res.Result == "0")
                {
                    MessageBox.Show("Tạo mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (this.chbClose.Checked)
                    {
                        this.Dispose();
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Tạo mới thất bại: " + res.ErrorDesc, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnInsert_Click: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }

        
    }
}
