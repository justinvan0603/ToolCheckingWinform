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
    public partial class Schedule_Detail : Form
    {
        public Schedule_Detail()
        {
            InitializeComponent();
        }

        public void SetValue(DateTime date, string term)
        {
            ScheduleClient client = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                client = new ScheduleClient();
                Schedules_DTResult[] data = client.Schedules_DT(date, term);
                if (this.dtgData.Columns != null)
                    this.dtgData.Columns.Clear();

                DataGridViewImageColumn cl = new DataGridViewImageColumn();
                cl.HeaderText = "Lưu";
                cl.Name = "SAVE";
                cl.Width = 30;
                cl.Image = Properties.Resources.approve_icon;
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
                    this.dtgData.Columns["ID"].Visible = false;                                        
                    this.dtgData.Columns["SCH_DATE"].Width = 90;                    
                    this.dtgData.Columns["SCH_TERM"].Width = 70;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetValue " + ex.Message);
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
