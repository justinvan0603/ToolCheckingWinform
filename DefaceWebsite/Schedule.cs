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
    public partial class Schedule : Form
    {
        public Schedule()
        {
            InitializeComponent();
        }

        private void Schedule_Load(object sender, EventArgs e)
        {
            this.cbStatus.Items.Add(new ItemStatus() { Id = "P", Name = "Chờ thực hiện" });
            this.cbStatus.Items.Add(new ItemStatus() { Id = "S", Name = "Đã thực hiện" });
            this.cbStatus.Items.Add(new ItemStatus() { Id = "", Name = "---Tất cả---" });
            this.cbStatus.DisplayMember = "Name";
            this.cbStatus.ValueMember = "Id";
            this.cbStatus.SelectedIndex = 2;
            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ScheduleClient client = null;
            try
            {
                if (this.dpDate.Value == null || (this.cbStatus.SelectedItem as ItemStatus) == null)
                {
                    MessageBox.Show("Vui lòng chọn ngày hoặc tình trạng cần xem.");
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                client = new ScheduleClient();
                Schedules_SearchResult[] data = client.Schedules_Search(new Schedules_SearchResult() { SCH_DATE = this.dpDate.Value, PROCESS_STATUS = (this.cbStatus.SelectedItem as ItemStatus).Id }, 1000);
                if (this.dtgData.Columns != null)
                    this.dtgData.Columns.Clear();

                DataGridViewImageColumn cl = new DataGridViewImageColumn();
                cl.HeaderText = "Lưu";
                cl.Name = "SAVE";
                cl.Width = 30;
                cl.Image = Properties.Resources.approve_icon;
                this.dtgData.Columns.Add(cl);

                cl = new DataGridViewImageColumn();
                cl.HeaderText = "Chi tiết";
                cl.Name = "COUNT";
                cl.Image = Properties.Resources.edit;
                cl.Width = 55;
                this.dtgData.Columns.Add(cl);

                if (data != null)
                {
                    this.dtgData.DataSource = data;
                    this.dtgData.Columns["ID"].Visible = false;
                    this.dtgData.Columns["STT"].Visible = false;
                   // this.dtgData.Columns["STT"].Width = 50;
                    this.dtgData.Columns["SCH_DATE"].Width = 90;
                    this.dtgData.Columns["EVENT_TIME"].Width = 90;
                    this.dtgData.Columns["SCH_TERM"].Width = 70;                    
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

        private class ItemStatus
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        private Schedule_Detail fChiTiet;
        private void dtgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //Luu xuong database
                //dbCashTool db = new dbCashTool();
                //CASH_BILL data = new CASH_BILL();
                //data.BILL_ID = this.dtgData["ID1", e.RowIndex].Value.ToString();
                //data.BILL_DATE = Convert.ToDateTime(this.dtgData["DateCreate", e.RowIndex].Value);
                //data.COMPANY = this.dtgData["SiteID", e.RowIndex].Value.ToString();
                //data.CREATE_DATE = DateTime.Now;
                //data.AMOUNT = Convert.ToDecimal(this.dtgData["TotalAmountVND", e.RowIndex].Value);
                //data.DESCRIPTION = this.dtgData["Note", e.RowIndex].Value.ToString();
                //data.BILL_MAKER = "";
                //data.BILL_TYPE = this.billType;
                //data.BILL_OPTION = this.billOption;
                //data.MAKER_ID = "thieuvq";
                //data.NOTES = "";

                //XElement xmlCashcount = null;
                //bool isInsert = true;
                //if (!this.dicChiTiet.ContainsKey(data.BILL_ID))
                //{
                //    DialogResult result = MessageBox.Show("Chưa cập nhật chi tiết tiền. Bạn có muốn lưu không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    if (result == DialogResult.No)
                //        isInsert = false;
                //}
                //else xmlCashcount = this.dicChiTiet[data.BILL_ID];
                //if (isInsert)
                //{
                //    CASH_BILL_InsResult res = db.CASH_BILL_Ins(data, xmlCashcount);
                //    if (res.RESULT == "0")
                //        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    else
                //        MessageBox.Show("Lưu thất bại: " + res.ERROR, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else if (e.ColumnIndex == 1)
            {
                if (this.fChiTiet == null)
                {
                    this.fChiTiet = new Schedule_Detail();
                }
                this.fChiTiet.SetValue(Convert.ToDateTime(this.dtgData["SCH_DATE", e.RowIndex].Value), this.dtgData["SCH_TERM", e.RowIndex].Value.ToString());
                this.fChiTiet.ShowDialog();
            }
        }

        private void btnDoSchedule_Click(object sender, EventArgs e)
        {
            ScheduleClient client = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                client = new ScheduleClient();
                Schedules_CalResult data = client.Schedules_Cal(this.dpDate.Value.ToString(StaticClass.formatShortDate), "thieuvq", DateTime.Now);
                if (data.Result == "0")
                    MessageBox.Show("Lập lịch thành công.");
                else
                    MessageBox.Show("Lập lịch thất bại. " + data.ErrorDesc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnDoSchedule_Click " + ex.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
                this.Cursor = Cursors.Default;
            }
        }
    }
}
