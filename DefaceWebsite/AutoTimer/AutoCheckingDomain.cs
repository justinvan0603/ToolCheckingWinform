using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaceWebsite.DFWService;
using System.Timers;
namespace DefaceWebsite.AutoTimer
{
    public class AutoCheckingDomain
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //Thu tu dot dang chay hien tai
       // public static int TERM_ID = 0;
        List<ProcessChecking> _listProcess;
        public AutoCheckingDomain()
        {
            _listProcess = new List<ProcessChecking>();
        }
        public void InitAutoChecking()
        {
            ScheduleClient client = null;
            try
            {
                client = new ScheduleClient();
                //Lay lich chay ngay hien tai
                Schedules_GetByDateResult[] scheduleResult = client.Schedules_GetByDate(DateTime.Now);
                Schedules_CalResult createScheduleResult = null;
                //Neu chua co lich => thuc hien lap lich cho ngay hien tai
                if (scheduleResult.Count() == 0 || scheduleResult == null)
                {
                    //Lap lich cho ngay hien tai
                    createScheduleResult = client.Schedules_Cal(DateTime.Now.ToString(StaticClass.formatShortDate), "thieuvq", DateTime.Now);
                    //log.Info("AutoCheckingDomain - Lap lich cho ngay hien tai");
                    //Lap lich thanh cong
                    if (createScheduleResult.Result.Equals("0"))
                    {
                        //log.Info("AutoCheckingDomain - Lap lich thanh cong");
                        //Lay lich ngay hien tai
                        Schedules_GetByDateResult[] currentDateSchedule = client.Schedules_GetByDate(DateTime.Now);
                        if (currentDateSchedule != null)
                        {
                            //Lay danh sach cac dot chay co thoi gian chay sau thoi diem hien tai
                            Schedules_GetByDateResult[] searchcurrentTerm = currentDateSchedule.Where(sc => sc.EVENT_TIME.Value >= DateTime.Now.TimeOfDay).OrderBy(sc => sc.EVENT_TIME).ToArray();
                            //if (searchcurrentTerm != null)
                            //{
                            //    //Lay danh sach cac link can chay tai dot gan voi thoi diem hien tai nhat
                            //    Schedules_DTResult[] listExecuteLink = client.Schedules_DT(DateTime.Now, searchcurrentTerm[0].SCH_TERM);
                            //    if (listExecuteLink != null)
                            //    {
                            //        //Chia danh sach cac link thanh cac process
                            //        DivideProcess(listExecuteLink, searchcurrentTerm[0]);
                            //    }
                            //}
                            if (searchcurrentTerm != null)
                            {
                                foreach (var item in searchcurrentTerm)
                                {
                                    Schedules_DTResult[] listExecuteLink = client.Schedules_DT(item.SCH_DATE.Value, item.SCH_TERM);
                                    if (listExecuteLink != null && listExecuteLink.Count() > 0)
                                    {
                                        this.DivideProcess(listExecuteLink, item);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                    //Neu da co lich chay cho ngay hom nay
                else
                {
                    //Lay danh sach cac dot chay co thoi gian chay sau thoi diem hien tai
                    Schedules_GetByDateResult[] searchcurrentTerm = scheduleResult.Where(sc => sc.EVENT_TIME.Value >= DateTime.Now.TimeOfDay).OrderBy(sc => sc.EVENT_TIME).ToArray();
                    if (searchcurrentTerm != null)
                    {
                        //Lay danh sach cac link trong dot chay gan nhat
                        //Schedules_DTResult[] listExecuteLink = client.Schedules_DT(searchcurrentTerm[0].SCH_DATE.Value, searchcurrentTerm[0].SCH_TERM);
                        //if (listExecuteLink != null && listExecuteLink.Count() > 0)
                        //{
                        //    //Chia link cho cac process
                        //    DivideProcess(listExecuteLink, searchcurrentTerm[0]);
                        //}
                        //Lay danh sach cac link trong dot chay gan nhat trong tung dot, neu dot nay khong co thi tim dot ke tiep
                        foreach(var item in searchcurrentTerm)
                        {
                            Schedules_DTResult[] listExecuteLink = client.Schedules_DT(item.SCH_DATE.Value, item.SCH_TERM);
                            if(listExecuteLink != null && listExecuteLink.Count() >0)
                            {
                                this.DivideProcess(listExecuteLink,item);
                                break;
                            }
                        }
                        
                    }
                }
                scheduleResult = null;
                
            }
            catch(Exception ex)
            {
                log.Fatal("AutoCheckingDomain.InitAutoChecking - " + ex.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }
        public void DivideProcess(Schedules_DTResult[] listexecutelink,Schedules_GetByDateResult currentTerm)
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
                        ProcessChecking prc = new ProcessChecking();
                        prc._scheduleDate = listexecutelink[0].SCH_DATE.Value;
                        prc._term = listexecutelink[0].SCH_TERM;
                        prc.totalProcess = totalProcess;
                        prc.SetValue(pro + 1, lstLink, lstLink, (key + index.ToString()));
                        //_listProcess = new List<ProcessChecking>();
                        _listProcess.Add(prc);
                        
                        
                        //pr1.totalProcess = totalProcess;
                        //pr1.SetValue(pro + 1, lstLink, lstLink, (key + index.ToString()));
                        break;
                    }
                }
                pro++;
            }
            
            var timeSpan = currentTerm.EVENT_TIME.Value.TotalMilliseconds - DateTime.Now.TimeOfDay.TotalMilliseconds;
            Timer processTimer = new Timer(timeSpan);
           //Timer processTimer = new Timer();
            processTimer.AutoReset = false;
            
            processTimer.Elapsed += new ElapsedEventHandler(ExecuteChecking);
            processTimer.Start();
            
            log.Info("AutoCheckingDomain.DivideProcess - Khởi động Timer");
        }
        private void ExecuteChecking(object source, ElapsedEventArgs e)
        {
            try
            {
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
                log.Info("AutoCheckingDomain.ExecuteChecking - Danh sách ProcessChecking đang chạy");
            }
            catch(Exception ex)
            {
                log.Fatal("AutoCheckingDomain.ExecuteChecking - " + ex.Message);
            }
            //var timer= (Timer) source;
            //timer.Stop();
        }
    }
}
