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
    public partial class Process : Form
    {
        public Process()
        {
            InitializeComponent();
        }
        public Button getButton()
        {
            return this.btnLoad;
        }
        private void Process_Load(object sender, EventArgs e)
        {
            this.LoadTerm();      
        }

        public void btnLoad_Click(object sender, EventArgs e)
        {
            ScheduleClient client = null;
            try
            {
                ItemTerm Shedule = (this.cbTerm.SelectedItem as ItemTerm);
                if(StaticClass.isAutoRunning || StaticClass.isAutoMode)
                {
                    MessageBox.Show("Đang có tiến trình chạy tự động thực hiện. Vui lòng thực hiện lại sau!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }
                //if(StaticClass.isAutoMode)
                //{
                //    MessageBox.Show("Bạn đã bật chế độ tự động. Việc chạy thủ công có thể ảnh hưởng đến kết quả!", "Chú ý",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                //}
                if (this.dtpDate.Value == null || Shedule == null)
                {
                    MessageBox.Show("Vui lòng chọn ngày và đợt thực hiện.");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                //Lay danh sách domain
                //ListDomainClient client = new ListDomainClient();
                //Listdomain_SearchResult[] res = client.Listdomain_Search("", "");
                
                client = new ScheduleClient();
                Schedules_DTResult[] res = client.Schedules_DT(this.dtpDate.Value, Shedule.TermId);
                string key = Shedule.TermId + ";";
                int totalLinks = 0;
                if (Shedule != null)
                {
                    var totalDomain = res.Select(dt => dt.DOMAIN_ID).Distinct().Count();
                    var totalLink = res.Select(dt => dt.LINK_ID).Distinct().Count();
                    totalLinks = res.Count();
                    this.lblTotalDomain.Text = "Số domain: " + totalDomain;//Shedule.TermItem.TOTAL_DOMAIN.Value.ToString();
                    this.lblTotalLink.Text = "Số link: " + totalLink;//Shedule.TermItem.TOTAL_LINK.Value.ToString();
                    this.lblScheTime.Text = "Lịch trình lúc: " + Shedule.TermItem.EVENT_TIME.Value.ToString(StaticClass.formatTime);
                }
                int totalProcess = 0;
                int limitLink = StaticClass.LimitLink;

                totalProcess = totalLinks / limitLink;
                if (totalLinks % limitLink > 0)
                    totalProcess++;

                this.lblTotalProcess.Text = "Số tiến trình: " + totalProcess.ToString();
                this.pnContent.Controls.Clear();
                int index = 0;
                int pro = 0;
                while (pro < totalProcess)
                {
                    int count = 0;
                    List<string> lstLink = new List<string>();
                    while (index < totalLinks)
                    {
                        lstLink.Add(res[index].LINK_ID);
                        index++;
                        count++;
                        //Du so luong Hoac het link thi tao moi process 
                        if (count >= limitLink || index >= totalLinks)
                        {
                            TProcess pr1 = new TProcess();
                            pr1._scheduleDate = res[0].SCH_DATE.Value;
                            pr1._term = res[0].SCH_TERM;
                           // pr1.totalProcess = totalProcess;
                            pr1.SetValue(pro + 1, lstLink, lstLink, (key + index.ToString()));
                            pr1.Location = new Point(0, pr1.Height * pro);
                            this.pnContent.Controls.Add(pr1);
                            break;
                        }
                    }
                    pro++;
                }
                //this.Cursor = Cursors.Default;
                //TProcess pr1 = new TProcess();
                //pr1.SetValue(1, new List<string>(new string[] { "http://gsoft.com.vn", "http://ravenshop.vn", "http://phanmemquanlytaisan.com" }), new List<string>(new string[] { "http://gsoft.com.vn", "http://ravenshop.vn", "http://phanmemquanlytaisan.com" }));
                //TProcess pr2 = new TProcess();
                //pr2.SetValue(2, new List<string>(new string[] { "http://24h.com.vn", "http://vnexpress.net" }), new List<string>(new string[] { "http://24h.com.vn", "http://vnexpress.net" }));
                //pr2.Location = new Point(0, pr1.Height);
                //TProcess pr3 = new TProcess();
                //pr3.SetValue(3, new List<string>(), new List<string>());
                //pr3.Location = new Point(0, pr1.Height * 2);
                //pnContent.Controls.Add(pr1);
                //pnContent.Controls.Add(pr2);
                //pnContent.Controls.Add(pr3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (client != null)
                    client.Close();
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            this.LoadTerm();   
        }

        private void LoadTerm()
        {
            ScheduleClient client = null;
            try
            {
                client = new ScheduleClient();
                this.cbTerm.Items.Clear();
                Schedules_GetByDateResult[] lst = client.Schedules_GetByDate(this.dtpDate.Value);
                foreach (var item in lst)
                {
                    this.cbTerm.Items.Add(new ItemTerm() { TermId = item.SCH_TERM, TermName = ("Đợt " + item.SCH_TERM), TermItem = item });
                }

                this.cbTerm.DisplayMember = "TermName";
                this.cbTerm.ValueMember = "TermId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("dtpDate_ValueChanged" + ex.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }

        public class ItemTerm
        {
            public string TermId { get; set; }
            public string TermName { get; set; }
            public Schedules_GetByDateResult TermItem { get; set; }
        }
    }
}
