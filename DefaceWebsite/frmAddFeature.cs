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
    public partial class frmAddFeature : Form
    {
        public frmAddFeature()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (this.txbFeaType.Text == "")
            {
                MessageBox.Show("Vui lòng nhập loại đặc điểm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.txbContent.Text == "")
            {
                MessageBox.Show("Vui lòng nhập nội dung.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }            

            FeaturesClient client = null;
            try
            {
                client = new FeaturesClient();
                Features_SearchResult data = new Features_SearchResult();
                //data.ID = int.Parse(this.dtgData["ID", e.RowIndex].Value.ToString());
                data.FEA_TYPE = this.txbFeaType.Text;
                data.CONTENTS = this.txbContent.Text;
                data.CREATE_DT = DateTime.Now;
                data.RESOURCE = this.txbResouce.Text;
                int level;
                if (int.TryParse(this.txbLevel.Text, out level))
                    data.LEVEL = level;
                data.RECORD_STATUS = "1";                
                data.AUTH_STATUS = "A";

                Features_InsResult res = client.Features_Ins(data);

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
