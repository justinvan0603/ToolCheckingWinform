using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DefaceWebsite.DFWService;
using DefaceWebsite.Items;
namespace DefaceWebsite.AutoTimer
{
    public class AutoCreateScheduleTimer
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        List<ProcessChecking> _listProcess;
        private Label _lblRunMode;
        public AutoCreateScheduleTimer()
        {
            this._listProcess = new List<ProcessChecking>();
        }
        public AutoCreateScheduleTimer(Label lblRunMode)
        {
            this._listProcess = new List<ProcessChecking>();
            this._lblRunMode = lblRunMode;
        }
        delegate void UpdateLabelRunMode(string text);
        private void SetLabelText(string text)
        {
            this._lblRunMode.Text = text;
        }

        private void SetLabelRunModeText(string text)
        {
            if (this._lblRunMode.InvokeRequired)
            {
                this._lblRunMode.BeginInvoke(new UpdateLabelRunMode(SetLabelText), new object[] { text });

            }
            else
            {
                this._lblRunMode.Text = text;
            }
        }
        public void InitSchedule(DateTime? starttime= null)
        {
            //TimeSpan a = new TimeSpan(10, 30, 0);
            //DateTime current = DateTime.Now;
            //double rs = a.TotalMilliseconds - current.TimeOfDay.TotalMilliseconds;
            //int x = 1;
            var dialyTime = "23:40:00";
            var timeParts = dialyTime.Split(new char[1] { ':' });
            var currentDate = DateTime.Now;
            var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
            TimeSpan timespan;
            // timespan =  currentDate -targetDate;
            if (targetDate > currentDate)
            {
                timespan = targetDate - currentDate;
            }
            else
            {
                targetDate = targetDate.AddDays(1);
                timespan = targetDate - currentDate;

            }
            System.Timers.Timer timer = new System.Timers.Timer(timespan.TotalMilliseconds);
            timer.Enabled = true;
            timer.AutoReset = false;
            timer.Elapsed += new ElapsedEventHandler(Schedule);
            timer.Start();
            log.Info("Còn " + TimeSpan.FromMilliseconds(timespan.TotalMilliseconds) + " đến thời điểm lập lịch cho ngày tiếp theo");
            log.Info("Đã hẹn giờ lập lịch cho ngày kế tiếp lúc 23:40:00 - Ngày: " + targetDate.Date);
        }
        private void Schedule(object source, ElapsedEventArgs e)
        {
            
            this.CreateSchedule();
            //MessageBox.Show("Interval");
        }
        public void DivideProcess(Schedules_DTResult[] listexecutelink, Schedules_GetByDateResult currentTerm)
        {
            string key = currentTerm.SCH_TERM + ";";
            int totalLinks = 0;
            totalLinks = listexecutelink.Count();
            int totalProcess = 0;
            int limitLink = StaticClass.LimitLink;

            totalProcess = totalLinks / limitLink;
            if (totalLinks % limitLink > 0)
                totalProcess++;
            int index = 0;
            int pro = 0;
            while (pro < totalProcess)
            {
                int count = 0;
                List<string> lstLink = new List<string>();
                while (index < totalLinks)
                {
                    lstLink.Add(listexecutelink[index].LINK_ID);
                    index++;
                    count++;
                    //Du so luong Hoac het link thi tao moi process 
                    if (count >= limitLink || index >= totalLinks)
                    {
                        //TProcess pr1 = new TProcess();
                        ProcessChecking prc = new ProcessChecking(this._lblRunMode);
                        prc._scheduleDate = listexecutelink[0].SCH_DATE.Value;
                        prc._term = listexecutelink[0].SCH_TERM;
                        prc.totalProcess = totalProcess;
                        prc.SetValue(pro + 1, lstLink, lstLink, (key + index.ToString()));
                       // _listProcess = new List<ProcessChecking>();
                        _listProcess.Add(prc);


                        //pr1.totalProcess = totalProcess;
                        //pr1.SetValue(pro + 1, lstLink, lstLink, (key + index.ToString()));
                        break;
                    }
                }
                pro++;
            }
            var x = DateTime.Now.AddDays(1);

            x = x.Date + currentTerm.EVENT_TIME.Value;
            var a = x.Subtract(DateTime.Now).TotalMilliseconds;
            log.Info("Đợt chạy tiếp theo cách - " + TimeSpan.FromMilliseconds(a));
            //var timeSpan = currentTerm.EVENT_TIME.Value.TotalMilliseconds - DateTime.Now.TimeOfDay.TotalMilliseconds;
            System.Timers.Timer processTimer = new System.Timers.Timer(a);
            processTimer.AutoReset = false;
            processTimer.Elapsed += new ElapsedEventHandler(ExecuteChecking);
            processTimer.Start();
            //this.SetLabelRunModeText("Đợt chạy tiếp theo lúc: " + currentTerm.EVENT_TIME);
            this.SetLabelRunModeText("Đợt chạy tự động tiếp theo lúc: " + currentTerm.EVENT_TIME + " Ngày: " + currentTerm.SCH_DATE.Value.Date.ToShortDateString());
            log.Info("Đợt chạy tiếp theo là - " + currentTerm.EVENT_TIME);
            log.Info("Khởi động timer chạy tự động");
            GC.Collect();
        }
        private void ExecuteChecking(object source, ElapsedEventArgs e)
        {
            try
            {
                if (!StaticClass.isAutoMode)
                {
                    return;
                }
                List<ProcessChecking> listProcess = new List<ProcessChecking>();
                foreach (var item in _listProcess)
                {
                    listProcess.Add(item);
                }
                _listProcess.Clear();
                foreach (var item in listProcess)
                {
                    item.RunChecking();
                }
                StaticClass.isAutoRunning = true;
                GC.Collect();
                this.SetLabelRunModeText("Đang chạy đợt: " + listProcess[0]._term + " Thời gian: " + listProcess[0]._eventTime);
                log.Info("AutoCreateScheduleTimer.ExecuteChecking - Danh sách ProcessChecking đang chạy");
            }
            catch(Exception ex)
            {
                log.Fatal("AutoCreateScheduleTimer.ExecuteChecking - " + ex.Message);
            }
        }
        private void CreateSchedule()
        {
            ScheduleClient client = null;
            try
            {
                client = new ScheduleClient();
                    
                    Schedules_CalResult data = client.Schedules_Cal(DateTime.Now.AddDays(1).ToString(StaticClass.formatShortDate), "thieuvq", DateTime.Now);
                    if (data.Result.Equals("0"))
                    {
                        this.InitSchedule();
                        log.Info("Lập lịch tự động cho ngày tiếp theo thành công");
                        log.Info("Khởi tạo kiểm tra tự động");
                        Schedules_GetByDateResult[] scheduleResult = client.Schedules_GetByDate(DateTime.Now.AddDays(1));
                        //Schedules_GetByDateResult[] searchcurrentTerm = scheduleResult.Where(sc => sc.EVENT_TIME.Value >= DateTime.Now.TimeOfDay).OrderBy(sc => sc.EVENT_TIME).ToArray();
                        Schedules_GetByDateResult[] searchcurrentTerm = scheduleResult.OrderBy(sc => sc.EVENT_TIME).ToArray();
                        foreach (var item in searchcurrentTerm)
                        {
                            Schedules_DTResult[] listExecuteLink = client.Schedules_DT(item.SCH_DATE.Value, item.SCH_TERM);
                            if (listExecuteLink != null && listExecuteLink.Count() > 0 && !StaticClass.isAutoRunning)
                            {
                                this.DivideProcess(listExecuteLink, item);
                                break;
                            }
                        }
                    }
                    else
                    {
                        this.InitSchedule();
                        log.Error("Lỗi khi lập lịch - " + data.ErrorDesc);
                        if(StaticClass.isAutoMode == true && StaticClass.isAutoRunning == false)
                        {
                            log.Info("Khởi tạo kiểm tra tự động");
                            Schedules_GetByDateResult[] scheduleResult = client.Schedules_GetByDate(DateTime.Now.AddDays(1));
                            //Schedules_GetByDateResult[] searchcurrentTerm = scheduleResult.Where(sc => sc.EVENT_TIME.Value >= DateTime.Now.TimeOfDay).OrderBy(sc => sc.EVENT_TIME).ToArray();
                            Schedules_GetByDateResult[] searchcurrentTerm = scheduleResult.OrderBy(sc => sc.EVENT_TIME).ToArray();
                            foreach (var item in searchcurrentTerm)
                            {
                                Schedules_DTResult[] listExecuteLink = client.Schedules_DT(item.SCH_DATE.Value, item.SCH_TERM);
                                if (listExecuteLink != null && listExecuteLink.Count() > 0 && !StaticClass.isAutoRunning)
                                {
                                    this.DivideProcess(listExecuteLink, item);
                                    break;
                                }
                            }
                        }
                    }
                    //Khoi tao xong dot cu dat gio cho ngay ke tiep
                    
            }
            catch (Exception ex)
            {
                log.Error("Lỗi khi tiến hành lập lịch - " + ex.Message);
            }
            finally
            {
                try
                {
                    if (client != null)
                    {
                        client.Close();
                        log.Info("Đóng kết nối WCF - trạng thái WCF Client: " + client.State);
                    }

                }
                catch(Exception ex)
                {
                    log.Error("Lỗi khi đóng kết nối WCF " + ex.Message);
                    client.Abort();
                    log.Info("Trạng thái WCF Client: " + client.State);
                }
                
            }
        }

    }
}
