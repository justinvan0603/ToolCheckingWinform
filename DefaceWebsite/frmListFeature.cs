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
    public partial class frmListFeature : Form
    {
        public frmListFeature()
        {
            InitializeComponent();
        }

        private void frmListFeature_Load(object sender, EventArgs e)
        {
            this.cbAuth.Items.Add(new ItemCombo() { Id = "A", Name = "Đã duyệt" });
            this.cbAuth.Items.Add(new ItemCombo() { Id = "U", Name = "Chưa duyệt" });
            this.cbAuth.Items.Add(new ItemCombo() { Id = "", Name = "---Tất cả---" });
            this.cbAuth.SelectedIndex = 2;

            this.cbAuth.DisplayMember = "Name";
            this.cbAuth.ValueMember = "Id";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FeaturesClient client = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                client = new FeaturesClient();
                Features_SearchResult search = new Features_SearchResult();
                search.AUTH_STATUS = (this.cbAuth.SelectedItem as ItemCombo).Id;
                search.CONTENTS = this.txbContent.Text;
                search.FEA_TYPE = this.txbFeaType.Text;
                int level;
                if (int.TryParse(this.txbLevel.Text, out level))
                    search.LEVEL = level;
                search.RESOURCE = this.txbResouce.Text;
                search.RECORD_STATUS = this.chbDeleted.Checked ? "0" : "1";
                Features_SearchResult[] data = client.Features_Search(search);
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
                DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    FeaturesClient client = null;
                    try
                    {
                        Features_SearchResult data = new Features_SearchResult();
                        data.ID = int.Parse(this.dtgData["ID", e.RowIndex].Value.ToString());
                        data.FEA_TYPE = this.dtgData["FEA_TYPE", e.RowIndex].Value.ToString();
                        data.CONTENTS = this.dtgData["CONTENTS", e.RowIndex].Value.ToString();
                        data.RESOURCE = this.dtgData["RESOURCE", e.RowIndex].Value.ToString();
                        data.EDIT_DT = DateTime.Now;
                        int level;
                        if (int.TryParse(this.dtgData["LEVEL", e.RowIndex].Value.ToString(), out level))
                            data.LEVEL = level;
                        data.AUTH_STATUS = this.dtgData["AUTH_STATUS", e.RowIndex].Value.ToString();
                        data.RECORD_STATUS = this.dtgData["RECORD_STATUS", e.RowIndex].Value.ToString();
                        if (this.dtgData["CREATE_DT", e.RowIndex].Value != null)
                        data.CREATE_DT = DateTime.Parse(this.dtgData["CREATE_DT", e.RowIndex].Value.ToString());

                        client = new FeaturesClient();
                        Features_UpdResult res = client.Features_Upd(data);
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
                DialogResult result = MessageBox.Show("Bạn có muốn xóa không? Các thông tin liên quan sẽ bị xóa!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    FeaturesClient client = null;
                    try
                    {
                        client = new FeaturesClient();
                        Features_DelResult res = client.Features_Del(int.Parse(this.dtgData["ID", e.RowIndex].Value.ToString()));
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

        private void tbnInsert_Click(object sender, EventArgs e)
        {
            frmAddFeature frm = new frmAddFeature();
            frm.ShowDialog();
        }
    }
}
