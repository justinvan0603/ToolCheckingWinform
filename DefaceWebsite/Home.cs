using DefaceWebsite.DFWService;
using DefaceWebsite.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DefaceWebsite.AutoTimer;
namespace DefaceWebsite
{
    public partial class Home : Form
    {
        private readonly string filename = "config.sys";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool _isAutoChecking;
        public Home()
        {
            InitializeComponent();
        }
        private void LoadAppConfig()
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    
                    String line = sr.ReadToEnd();
                    string[] lst = line.Split(';');
                    StaticClass.LimitLink = int.Parse(lst[0]);
                    this._isAutoChecking = lst[1] == "A" ? true : false;//a- auto; c-customer
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Home_Load: " + ex.Message);
            }
        }
        private void Home_Load(object sender, EventArgs e)
        {
            this.LoadAppConfig();
            //log.Info("Home_Load--Khoi dong man hinh chinh");
            //log.Info("Khoi dong Lap lich tu dong cho ngay ke tiep");


            if (this._isAutoChecking)
            {
                AutoCreateScheduleTimer timer = new AutoCreateScheduleTimer();
                timer.InitSchedule();
                StaticClass.isAutoMode = true;
                AutoCheckingDomain autoCheckDomain = new AutoCheckingDomain();
                autoCheckDomain.InitAutoChecking();
                log.Info("Đã cài đặt tự động lập lịch và kiểm tra");
            }
            else
            {
                StaticClass.isAutoMode = false;
            }
            this.grpToday.Text = "Lịch thực hiện hôm nay " + DateTime.Now.ToString(StaticClass.formatShortDate);

            this.tmCurrentTime.Tick += new EventHandler(setCurrentTime);
            this.tmCurrentTime.Start();
            this.Cursor = Cursors.WaitCursor;

            int posi = 0;
            int top = 5;
            ScheduleClient client = null;
            UserClient userClient = null;
            ListDomainClient domainClient = null;
            try
            {
                client = new ScheduleClient();
                Schedules_GetLastResult[] lastSche = client.Schedules_GetLast(DateTime.Now, top);
                TLastSche it;
                foreach (var item in lastSche)
                {
                    it = new TLastSche();
                    it.Location = new Point(0, posi * it.Height);
                    it.SetValues(item.SCH_DATE.Value, item.TIMES, item.TOTAL_DOMAIN, item.TOTAL_LINK);
                    this.pnLastSche.Controls.Add(it);
                    posi++;
                }

                Schedules_GetByDateResult[] scheDate = client.Schedules_GetByDate(DateTime.Now);
                TSchedule sch;
                posi = 0;
                foreach(var item in scheDate)
                {
                    sch = new TSchedule();
                    sch.Location = new Point(0, posi * sch.Height);
                    sch.SetValues(item.EVENT_TIME, item.TOTAL_DOMAIN, item.TOTAL_LINK, item.P_DONE, item.P_WARNING, item.P_ALERT);
                    this.pnSchedule.Controls.Add(sch);
                    posi++;
                }

                posi = 0;
                userClient = new UserClient();
                Users_SearchResult[] lstUser = userClient.Users_Search(new Users_SearchResult(), top);
                TNewUser us;
                foreach (var item in lstUser)
                {
                    us = new TNewUser();
                    us.Location = new Point(0, posi * us.Height);
                    us.SetValues(item.CREATE_DT, item.UserName, item.FULLNAME);
                    this.pnLstUser.Controls.Add(us);
                    posi++;
                }

                posi = 0;
                domainClient = new ListDomainClient();
                Listdomain_SearchResult[] lstDomain = domainClient.Listdomain_Search("", "", null, "1");
                foreach (var item in lstDomain)
                {
                    us = new TNewUser();
                    us.Location = new Point(0, posi * us.Height);
                    us.SetValues(item.CREATE_DT, item.DOMAIN, item.USERNAME);
                    this.pnlstDomain.Controls.Add(us);
                    posi++;
                }

                //load config
                try
                {
                    using (StreamReader sr = new StreamReader("config.sys"))
                    {
                        String line = sr.ReadToEnd();
                        string[] lst = line.Split(';');

                        StaticClass.LimitLink = int.Parse(lst[0]);//so luong link
                        //this.rbtAuto.Checked = lst[1] == "A" ? true : false;//a- auto; c-customer
                        //this.chbRegisterWin.Checked = bool.Parse(lst[2]);//khoi dong cung window
                        //this.dpTimeStart.Value = DateTime.Parse(lst[3]);//thoi gian start chuong trinh
                        //this.chbSendApp.Checked = bool.Parse(lst[4]);
                        //this.chbSendEmail.Checked = bool.Parse(lst[5]);
                        //this.chbSchedule.Checked = bool.Parse(lst[6]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Home_Load config: " + ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            finally
            {
                if (client != null)
                    client.Close();
                if (userClient != null)
                    userClient.Close();
                if(domainClient != null)
                    domainClient.Close();
                this.Cursor = Cursors.Default;
            }
        }

        private void setCurrentTime(object sender, EventArgs e)
        {
            this.lblcurrentTime.Text = "Ngày: " + DateTime.Now.ToString(StaticClass.formatDate);
        }

        private void thưcHiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process frmProcess = new Process();
            frmProcess.Show();
        }

        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Schedule frmSchedule = new Schedule();
            frmSchedule.Show();
        }

        private void lstDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDomain frm = new frmListDomain();
            frm.Show();
        }

        private void addDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDomain frm = new AddDomain();
            frm.Show();
        }

        private void lstUserToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmLstUsers frm = new frmLstUsers();
            frm.Show();
        }

        private void addUserToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUser frm = new frmAddUser();
            frm.Show();
        }

        private void userOptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDomainOption frm = new frmDomainOption();
            frm.Show();
        }

        private void addFeatureToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddFeature frm = new frmAddFeature();
            frm.Show();
        }

        private void lstFeartureToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmListFeature frm = new frmListFeature();
            frm.Show();
        }

        private void OptionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmOptions frm = new frmOptions();
            frm.ShowDialog();
        }


    }
}
