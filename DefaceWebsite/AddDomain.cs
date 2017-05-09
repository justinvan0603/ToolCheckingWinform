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
    public partial class AddDomain : Form
    {
        public AddDomain()
        {
            InitializeComponent();
        }

        private void AddDomain_Load(object sender, EventArgs e)
        {
            UserClient client = null;
            try
            {
                client = new UserClient();
                Users_SearchResult[] data = client.Users_Search(new Users_SearchResult(), 10000);

                if(data != null)
                {
                    this.cbUser.DataSource = data;
                    this.cbUser.DisplayMember = "USERNAME";
                    this.cbUser.ValueMember = "ID";
                    this.cbUser.AutoCompleteSource = AutoCompleteSource.ListItems;
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
            if(this.txbDomain.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên domain.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Users_SearchResult use = (this.cbUser.SelectedItem as Users_SearchResult);
            if (use == null)
            {
                MessageBox.Show("Vui lòng nhập người quản lý.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListDomainClient client = null;
            try
            {
                client = new ListDomainClient();
                Listdomain_SearchResult data = new Listdomain_SearchResult();
                data.DOMAIN = this.txbDomain.Text;
                data.RECORD_STATUS = "1";
                data.USER_ID = use.Id.ToString();
                data.USERNAME = use.UserName;
                data.CREATE_DT = DateTime.Now;
                data.AUTH_STATUS = "A";
                data.DESCRIPTION = this.txbDesc.Text;

                Listdomain_InsResult res = client.Listdomain_Ins(data);
                if (res.Result == "0")
                {
                    MessageBox.Show("Tạo mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if(this.chbClose.Checked)
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
                MessageBox.Show("AddDomain_Load: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }
    }
}
