using DefaceWebsite.DFWService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Timers;
using Imagio;
using Newtonsoft.Json.Linq;
using DefaceWebsite.Class;
namespace DefaceWebsite.AutoTimer
{
    public class ProcessChecking
    {
        private System.Windows.Forms.ProgressBar prcProcess;
        public int totalProcess = 0;
        List<ProcessChecking> _listProcess;
        public DateTime _scheduleDate;
        public string _term;
        public TimeSpan _eventTime;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Label _lblRunMode;
        public ProcessChecking()
        {
            this._listProcess = new List<ProcessChecking>();
            this.prcProcess = new ProgressBar();
        }
        public ProcessChecking(Label lblRunMode)
        {
            this._listProcess = new List<ProcessChecking>();
            this.prcProcess = new ProgressBar();
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
        
        public void SetValue(int index, List<string> domains, List<string> links, string _key)
        {
            this.totalLinks = links.Count;
            this.totalDomains = domains.Count;
            this.lstDomain = domains;
            this.lstLink = links;

            this.key = _key;
            this.LoadData();
        }
        Thread thr;
        private int totalDomains = 0;
        private int totalLinks = 0;
        private List<string> lstDomain;
        private List<string> lstLink;
        private string key = "";
        //Rename of TProcess_Load
        private void LoadData()
        {
            thr = new Thread(CheckDomain);
            prcProcess.Maximum = this.totalLinks;
            prcProcess.Step = 1;
        }
        delegate void SetTextCallback(int val);
        delegate void SetMessageCallback(string val);
        delegate void SetNonPar();

        private void CheckDomain()
        {
            log.Info("ProcessChecking.CheckDomain - Thuc hien kiem tra domain");
            while (prcProcess.Value < prcProcess.Maximum)
            {
                
                this.CheckAction(this.lstLink[this.prcProcess.Value]);
                
                ScheduleClient client = null;
                try
                {
                    client = new ScheduleClient();
                    var result = client.ScheduleDt_UpdExecute(_scheduleDate, _term, this.lstLink[this.prcProcess.Value]);
                    log.Info("ProcessChecking.CheckDomain - Hoàn thành kiểm tra cập nhật trạng thái Executed = 1");
                }
                catch (Exception ex)
                {
                    log.Error("ProcessChecking.CheckDomain -Lỗi cập nhật trạng thái  (Executed)-" + ex.Message);
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
               // if (this.prcProcess.InvokeRequired)
                //{
                    PerStep();
                    
                //}                
                _pauseEvent.WaitOne(Timeout.Infinite);
                //goi stored update trang thai thanh executed =1
                
            }
  
                
                StaticClass.countProcess++;
                if (StaticClass.countProcess == totalProcess)
                {
                    StaticClass.countProcess = 0;
                    totalProcess = 0;
                    if(this._listProcess != null)
                        this._listProcess.Clear();
                    StaticClass.isAutoRunning = false;
                    if (StaticClass.isAutoMode) //Kiem tra neu che do tu dong van con bat thi cho chay tiep
                    {
                        log.Info("Chạy auto xong 1 đợt khởi tạo đợt tiếp theo");
                        InitNewProcessTerm();
                    }
                    GC.Collect();
                    //Goi ham tick dot moi SetTimer
                }
                //thuc hien xong tien trinh
                //SetNonPar dt = new SetNonPar(DisButton);
                //this.Invoke(dt);                
            //}            
            Stop();
        }
        public void InitNewProcessTerm()
        {
            ScheduleClient client = null;
            try
            {
                client = new ScheduleClient();
                Schedules_GetByDateResult[] scheduleResult = client.Schedules_GetByDate(DateTime.Now);
                Schedules_CalResult createScheduleResult = null;
                if (scheduleResult == null || (scheduleResult.Count() == 0 && scheduleResult != null))
                {
                    createScheduleResult = client.Schedules_Cal(DateTime.Now.ToString(StaticClass.formatShortDate), "thieuvq", DateTime.Now);

                    if (createScheduleResult.Result.Equals("0"))
                    {
                        Schedules_GetByDateResult[] currentDateSchedule = client.Schedules_GetByDate(DateTime.Now);
                        Schedules_GetByDateResult[] searchcurrentTerm = currentDateSchedule.Where(sc => sc.EVENT_TIME.Value >= DateTime.Now.TimeOfDay).OrderBy(sc => sc.EVENT_TIME).ToArray();
                        if (currentDateSchedule != null)
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

                            log.Info("InitNewTermProcess-Khởi động đợt mới thành công");
                            //Schedules_GetByDateResult[] searchcurrentTerm = currentDateSchedule.Where(sc => sc.EVENT_TIME.Value >= DateTime.Now.TimeOfDay).OrderBy(sc => sc.EVENT_TIME).ToArray();
                            //if (searchcurrentTerm != null)
                            //{
                            //    Schedules_DTResult[] listExecuteLink = client.Schedules_DT(searchcurrentTerm[0].SCH_DATE.Value, searchcurrentTerm[0].SCH_TERM);
                            //    if (listExecuteLink != null)
                            //    {
                            //        DivideProcess(listExecuteLink, searchcurrentTerm[0]);
                            //    }
                            //}
                        }
                    }
                }
                else
                {
                    Schedules_GetByDateResult[] searchcurrentTerm = scheduleResult.Where(sc => sc.EVENT_TIME.Value >= DateTime.Now.TimeOfDay).OrderBy(sc => sc.EVENT_TIME).ToArray();
                    //if (searchcurrentTerm != null)
                    //{
                    //    Schedules_DTResult[] listExecuteLink = client.Schedules_DT(searchcurrentTerm[0].SCH_DATE.Value, searchcurrentTerm[0].SCH_TERM);
                    //    if (listExecuteLink != null)
                    //    {
                    //        DivideProcess(listExecuteLink, searchcurrentTerm[0]);
                    //    }
                    //}
                    foreach (var item in searchcurrentTerm)
                    {
                        Schedules_DTResult[] listExecuteLink = client.Schedules_DT(item.SCH_DATE.Value, item.SCH_TERM);
                        if (listExecuteLink != null && listExecuteLink.Count() > 0)
                        {
                            log.Info("Số link: " + listExecuteLink.Count().ToString() +  " Đợt chạy: " +item.SCH_TERM.ToString());
                            this.DivideProcess(listExecuteLink, item);
                            log.Info("InitNewTermProcess-Khởi động đợt mới thành công");
                            break;
                        }
                    }
                }
                scheduleResult = null;
               

            }
            catch (Exception ex)
            {
                log.Error("Lỗi ProcessChecking.InitNewProcessTerm- " + ex.Message);
                //if (client != null)
                    //client.Close();
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
        public void DivideProcess(Schedules_DTResult[] listexecutelink, Schedules_GetByDateResult currentTerm)
        {
            try
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

                var timeSpan = currentTerm.EVENT_TIME.Value.TotalMilliseconds - DateTime.Now.TimeOfDay.TotalMilliseconds;
                System.Timers.Timer processTimer = new System.Timers.Timer(timeSpan);
                //System.Timers.Timer processTimer = new System.Timers.Timer(10*1000);
                processTimer.AutoReset = false;
                processTimer.Elapsed += new ElapsedEventHandler(ExecuteChecking);
                processTimer.Start();
                GC.Collect();
                this.SetLabelRunModeText("Đợt chạy tự động tiếp theo lúc: " + currentTerm.EVENT_TIME + " Ngày: " + currentTerm.SCH_DATE.Value.Date.ToShortDateString());
                //this.SetLabelRunModeText("Đợt chạy tiếp theo lúc: " + currentTerm.EVENT_TIME);
                log.Info("Thời gian bắt đầu kiểm tra là - " + currentTerm.EVENT_TIME);
                log.Info("ProcessChecking.DivideProcess - Khởi động timer");
                
            }
            catch(Exception ex)
            {
                log.Error("Lỗi ProcessChecking.DivideProcess- " + ex.Message);
            }


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
                log.Info("ProcessChecking.ExecuteChecking - Danh sách ProcessChecking đang chạy");
            }
            catch(Exception ex)
            {
                log.Error("Lỗi ProcessChecking.ExecuteChecking- " + ex.Message);
            }
            
        }
        //private void DisButton()
        //{
        //    //this.btnActive.Enabled = false;
        //    //this.Cursor = Cursors.Default;
        //}       

        private void PerStep()
        {
            this.prcProcess.PerformStep();
            //this.lblDone.Text = "Xong: " + this.prcProcess.Value.ToString();
            Application.DoEvents();
        }

        //private void SetText(int val)
        //{
        //  //  this.prcProcess.Value += val;
        //}

        private void SetMessageInv(string val)
        {
          //  this.tbxMessage.AppendText("\r\n" + val);
        }

        //private void SetWarnInv()
        //{
        //    this.totalWrn++;
        //  //  this.lblWarning.Text = "Cảnh báo: " + this.totalWrn.ToString();
        //}
        // Rename of btnActive_Click
        public void RunChecking()
        {
            log.Info("Bắt đầu quá trình chạy tự động");
                if (thr.ThreadState == ThreadState.Unstarted)
                    thr.Start();
                else 
                    Resume();
          

        }
        ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        //public void Pause()
        //{
        //    _pauseEvent.Reset();
        //}

        public void Resume()
        {
            _pauseEvent.Set();
        }

        public void Stop()
        {
            // Signal the shutdown event
            _shutdownEvent.Set();

            // Make sure to resume any paused threads
            _pauseEvent.Set();

            // Wait for the thread to exit
            thr.Abort();
            thr.Join();
            
        }
        
        private void SetMessage(string message)
        {
            log.Info(message);
            message = DateTime.Now.ToString(StaticClass.formatDate) + ": " + message;
         //   if (this.tbxMessage.InvokeRequired)
         //   {
                SetMessageCallback d = new SetMessageCallback(SetMessageInv);
            //    this.Invoke(d, new object[] { message });
          //  }
          //  else
                this.SetMessageInv(message);
        }

        private int totalWrn = 0;
        /// <summary>
        /// SetWarning with icon: info, warning, alert
        /// </summary>
        /// <param name="res"></param>
        /// <param name="domain"></param>
        /// <param name="type"></param>
        private void SetWarning(CheckResult res, string domain, string icon = "warning")
        {            
           // if (this.lblWarning.InvokeRequired)
            //{
            //    SetNonPar d = new SetNonPar(SetWarnInv);
            //    this.Invoke(d);
           // }
            //else
           // {
           //     this.SetWarnInv();
           // }

            //Gui thong bao den nguoi dung
            string title = "[DEFACE] Website bị thay đổi";
            string content = res.Message;
            //Luu thong bao
            MessagesClient client = null;
            try
            {
                client = new MessagesClient();
                Messages_NotifyInsResult[] lstuser = client.Messages_NotifyIns(title, content, "", "", domain, DateTime.Now.ToString(StaticClass.formatDate), res.Status, DateTime.Now.ToString(StaticClass.formatDate) + ";" + this.key + ";" + domain, icon);
                if (lstuser != null && lstuser.Count() > 0)
                {
                    //Gui email
                    this.SetMessage("Gửi email thông báo...");
                    DefaceWebsite.SendEmailClass.EmailInfo emailInfo = new DefaceWebsite.SendEmailClass.EmailInfo();
                    emailInfo.ToEmail = new List<string>();
                    emailInfo.Subj = title;
                    emailInfo.Message =
                       "<span style=\"font - size:12px;\"><span style=\"font-family:times new roman,times,serif;\">Dear All" +
                        ",<br/>\n<br/>\nWebsite: " + domain + " đã bị thay đổi." +
                        "<br/>\n<br/>\nNội dung thay đổi: " + content +
                       "<br/>\n<br/>\n<em>Lưu ý: Quý khách vui lòng không phản hồi email này. Đây là email được tạo ra bởi hệ thống tự động.</em></span></span><br/>"
                       + "<br /><strong>Thanks and Best Regards</strong>";
                    foreach (var item in lstuser)
                    {
                        emailInfo.ToEmail.Add(item.Email);
                    }
                    bool sendstatus = SendEmailClass.SendEmail(emailInfo);
                    this.SetMessage("Gửi email" + sendstatus);
                    this.SetMessage("Send notification...");
                    //Gui Notify    
                    foreach (var item in lstuser)
                    {
                        if (item.APPTOKEN != null && item.APPTOKEN != "")
                        {
                            
                            SendResult sendNotify = Send(item.APPTOKEN, title, content, icon);
                            if (sendNotify.Status)
                            {
                                JArray result = JsonConvert.DeserializeObject<JArray>(sendNotify.Message);
                                List<MessageObject> messageObject = result.ToObject<List<MessageObject>>();
                                this.SetMessage("Send notification: " + item.UserName + " thành công");
                                MessagesClient messsageclient = null;
                                try
                                {
                                    messsageclient = new MessagesClient();
                                    Messages_UpdFBIDResult rs = messsageclient.Messages_UpdFBID(item.MESSAGE_ID.Value, messageObject[0].message_id);
                                    if (rs.Result.Equals("0"))
                                    {
                                        this.SetMessage("Cập nhật trạng thái message thành công");
                                    }
                                    else
                                    {
                                        this.SetMessage("Đã có lỗi xảy ra - " + rs.ErrorDesc);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.SetMessage("Lỗi khi update lại Message trong DB - " + ex.Message);

                                }
                                finally
                                {
                                    if (messsageclient != null)
                                        messsageclient.Close();
                                }
                            }
                            else this.SetMessage("Send notification: " + item.UserName + " thất bại - " + sendNotify.Message);
                        }
                    }
                }
                GC.Collect(GC.GetGeneration(lstuser));
                lstuser = null;
            }
            catch (Exception ex)
            {
                this.SetMessage("Lỗi: " + ex.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
            
        }
        
       /// <summary>
       /// Kiem tra domain
       /// </summary>
        private void CheckAction(string _domain)
        {
            string domain = _domain;
            this.SetMessage("=======Kiểm tra Domain: " + domain + "=======");
          //  log.Info("------ProcessChecking.CheckAction------Begin");
          //  log.Info("=======Kiem tra Domain: " + domain + "=======");
            CheckResult res;
            //1. KIEM TRA TIMEOUT || NOT EXISTS
            //2. KIEM TRA IP - CHANGE IP
            this.SetMessage("Kiểm tra Domain, IP");
          //  log.Info("Kiem tra Domain, IP");
            res = this.CheckIpTimeOut(domain);
            //KIEM TRA IP && DOMAIN && ERROR CODE
            if (res.Status == DomainType.Ok)
            {
                //1. KIEM TRA DOMAIN - REDIRECT                
                //2. KIEM TRA ERRORCODE
                this.SetMessage("Kiểm tra Redicrect - Source Code");
               // log.Info("Kiem tra Redicrect - Source Code");
                res = this.GetHttpStatusCode(domain);
                //KIEM TRA GIAO DIEN - COMPARE IMAGE
                //1. COMPARE IMAGE
               // this.SetMessage("Kiểm tra giao diện");
               //// log.Info("Kiem tra giao dien");
               // CheckResult resImage = this.CompareImage(domain);
               // if(res.Status == DomainType.Ok)
               // {
               //     float dif = 0;
               //     float.TryParse(resImage.Values, out dif);
               //     if (resImage.Status == DomainType.Ok || dif < 30)
               //     {
               //         res.Status = DomainType.Ok;
               //         res.Message = resImage.Message;
               //         res.Values = resImage.Values;
               //     }
               //     else
               //     {
               //         res.Status = resImage.Status;
               //         res.Message = (resImage.Message + (resImage.Values == "" ? "" : (" Tỉ lệ khác nhau: " + resImage.Values)));
               //         res.Values = resImage.Values;
               //     }                   
               //     //2. COMPARE CONTENT - SOURCE
               // }
               // else if (resImage.Status != DomainType.Ok && (resImage.Message == "NotSameSize" || resImage.Values == null || float.Parse(resImage.Values) > 30))
               // {
               //     res.Status = DomainType.Image;
               //     res.Message = res.Message + "; " + resImage.Message;
               // }
            }

            //CheckResult re = CheckIpTimeOut(this.tbxUser.Text);
            if (res.Status != DomainType.Ok)
            {
                //MessageBox.Show(res.Message);
                this.SetMessage("Kết quả: Cảnh báo - " + res.Status + ": " + res.Message);
             //   log.Info("Ket qua: Canh bao - " + res.Status + ": " + res.Message);
                this.SetWarning(res, _domain);
            }
            else
            {
                this.SetMessage("Kết quả: Không phát hiện bất thường");
            ///    log.Info("Ket qua: Khong phat hien bat thuong");
            }//MessageBox.Show("Không phát hiện bất thường");
            this.SetMessage("=======Kiểm tra xong=======");
          //  log.Info("=======Kiem tra xong=======");
         //   log.Info("------ProcessChecking.CheckAction------End");
        }

        public CheckResult CheckIpTimeOut(string hostname)
        {
            CheckDomainClient client = null;
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(StaticClass.GetDomainName(hostname));
                //List<string> lstIp = new List<string>();
                //Console.WriteLine("GetHostEntry({0}) returns:", hostname);
                client = new CheckDomainClient();
                //Lay du lieu hien tai                
                DomainProfile_SearchResult[] result = client.DomainProfile_Search(new DomainProfile_SearchResult() { DOMAIN = hostname, TYPE = DomainType.Ip });
                string currentIp = "";
                if (result != null && result.Count() > 0)
                    currentIp = result[0].VALUES;
                this.SetMessage("Lấy IP...");
                //Lay danh sach IP hien tai
                string lstIp = "";
                //Neu lan dau thi luu danh sach
                if (currentIp == "")
                    foreach (IPAddress ip in host.AddressList)
                    {
                        //  Console.WriteLine("    {0}", ip);
                        //lstIp.Add(ip.ToString());
                        if (ip.ToString().Contains("."))
                            lstIp += ip.ToString() + ";";
                    }
                //Chi lay IP dang hoat dong
                else lstIp = host.AddressList.FirstOrDefault().ToString();
                this.SetMessage("IP: " + lstIp);
                //luu lai IP vua kiem tra
                client.DomainChange_Ins(new DomainChange_SearchResult() { DOMAIN = hostname, TYPE = DomainType.Ip, VALUES = lstIp });

                CheckResult checkresult = new CheckResult() { Status = DomainType.Ok, Message = "" };

                if (currentIp !="" && !currentIp.Contains(lstIp))
                {
                    checkresult.Status = DomainType.Ip;
                    checkresult.Message = lstIp + " không khớp IP đã khai báo";
                    this.SetMessage("Không khớp IP đã khai báo " + currentIp);
                }
                result = null;
                
                this.SetMessage(checkresult.Status + " " + checkresult.Message);
                return checkresult;
            }
            catch (Exception ex)
            {
                this.SetMessage(DomainType.Timeout + " - Lỗi: " + ex.Message);
                return new CheckResult() { Status = DomainType.Timeout, Message = ex.Message };
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
            
        }

        private CheckResult GetHttpStatusCode(string domain)
        {
            CheckDomainClient client = null;
            //phai kiem tra timeout truoc khi check status code
            try
            {
                client = new CheckDomainClient();
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest
                                            .Create(domain);
                webRequest.AllowAutoRedirect = true;
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                CheckResult checkresult = new CheckResult() { Status = DomainType.Ok, Message = "" };

                string currentDomain = response.ResponseUri.AbsoluteUri;
                this.SetMessage("Domain request: " + currentDomain);
                //luu lai IP vua kiem tra
                client.DomainChange_Ins(new DomainChange_SearchResult() { DOMAIN = domain, TYPE = DomainType.Redirect, VALUES = currentDomain });
                //Lay du lieu hien tai                
                DomainProfile_SearchResult[] result = client.DomainProfile_Search(new DomainProfile_SearchResult() { DOMAIN = domain, TYPE = DomainType.Redirect });
                string oldDomain = "";
                if (result != null && result.Count() > 0)
                    oldDomain = result[0].VALUES;
                if (currentDomain != oldDomain)
                {
                    checkresult.Status = DomainType.Redirect;
                    checkresult.Message = "Domain bị chuyển hướng về địa chỉ: " + currentDomain;
                    this.SetMessage(checkresult.Message);
                }
                result = null;
                //Returns "MovedPermanently", not 301 which is what I want.
                //Console.Write(response.StatusCode.ToString());
                this.SetMessage(checkresult.Status + " " + checkresult.Message);
                return checkresult;
            }
            catch (WebException we)
            {
                try
                {
                    HttpStatusCode statusCode = ((HttpWebResponse)we.Response).StatusCode;
                    string message = "Lỗi: " + ((int)statusCode).ToString() + " - " + statusCode.ToString();
                    // Console.Write("Error: " + ((int)statusCode).ToString() + ":" + statusCode.ToString());
                    this.SetMessage(message);
                    //luu lai IP vua kiem tra
                    client.DomainChange_Ins(new DomainChange_SearchResult() { DOMAIN = domain, TYPE = DomainType.Resource, VALUES = message });

                    //wRespStatusCode = ((HttpWebResponse)we.Response).StatusCode;
                    return new CheckResult() { Status = DomainType.Resource, Message = message };
                }
                catch (Exception ex)
                {
                    return new CheckResult() { Status = DomainType.Resource, Message = "Môi trường mạng không ổn định.\n\r" + ex.Message };
                }                
            }
            finally
            {
                if (client != null)
                    client.Close();
            }

        }
        private CheckResult CompareImage(string domain)
        {

            CheckDomainClient client = null;
            try
            {
                client = new CheckDomainClient();
                CheckResult checkresult = new CheckResult() { Status = DomainType.Ok, Message = "" };
                string filename = StaticClass.GetDomainName(domain);

                this.SetMessage("Lấy dữ liệu ảnh đã lưu...");
                //Kiem tra file ảnh đã lưu                                
                DomainProfile_SearchResult[] result = client.DomainProfile_Search(new DomainProfile_SearchResult() { DOMAIN = domain, TYPE = DomainType.Image });
                string fullpath = "";
                if (result != null && result.Count() > 0)
                    fullpath = result[0].VALUES;
                Bitmap bm1 = null;
                if (File.Exists(fullpath))
                    bm1 = new Bitmap(fullpath);
                this.SetMessage("Lấy ảnh website hiện tại...");
                //Lay ảnh hiện tại         
                ImageItem imageItem = new ImageItem();
                Bitmap bm2 = imageItem.CaptureImage(domain);

                //luu lai anh
                client.DomainChange_Ins(new DomainChange_SearchResult() { DOMAIN = domain, TYPE = DomainType.Image, VALUES = imageItem.FullPathImage });
                Application.DoEvents();

                if (bm1 == null)
                {
                    //this.lblResult.Text = "Dữ liệu đã được lưu. Lần đầu tiên nên chưa có dữ liệu compare";
                    checkresult.Status = DomainType.Image;
                    checkresult.Message = "Dữ liệu đã được lưu. Lần đầu tiên nên chưa có dữ liệu compare";
                    this.SetMessage(checkresult.Message);
                    return checkresult;
                }

                //this.Cursor = Cursors.WaitCursor;
                //Application.DoEvents();
                this.SetMessage("So sánh ảnh...");
                ComparingImages.CompareResult cr = ComparingImages.CompareSHA256(bm1, bm2);
                if (cr == ComparingImages.CompareResult.ciCompareOk)
                {
                    checkresult.Message = "OK";
                    checkresult.Status = DomainType.Ok;
                    this.SetMessage(checkresult.Message);
                }
                if (cr == ComparingImages.CompareResult.ciSizeMismatch)
                {
                    checkresult.Status = DomainType.Image;
                    checkresult.Message = "NotSameSize";
                    this.SetMessage(checkresult.Message);

                }
                if (cr == ComparingImages.CompareResult.ciPixelMismatch)
                {
                    checkresult.Values = ComparingImages.DifCount;
                    checkresult.Status = DomainType.Image;
                    checkresult.Message = "NotSameSize";
                    this.SetMessage(checkresult.Message);
                }
                result = null;
                bm1.Dispose();
                bm2.Dispose();
                GC.Collect(GC.GetGeneration(bm1));
                GC.Collect(GC.GetGeneration(bm2));
                
                return checkresult;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
                this.SetMessage(DomainType.Image + " - Lỗi: " + ex.Message);
                return new CheckResult() { Status = DomainType.Image, Message = "CompareImage: " + ex.Message };
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }


        private SendResult Send(string deviceId, string _title, string _body, string _icon)
        {
            try
            {
                var applicationID = StaticClass.ApplicationID;
                var senderId = StaticClass.SenderId;
                //string deviceId = "esf0lWXzoug:APA91bEuNRkseTqPH-tVl61882ad2oqOSlJaKHzVJLjh-8hAQkRLAmrtSfVpoOdfP_c73Pw-hha5avDzBaWKu4KmwB-Yj_Xr-tSExn5HCH6UJ2IPQysBABrQmuqqCfPiO68IIboqNwHe";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = _body,
                        title = _title,
                        icon = _icon
                    },
                    priority = "high"
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                return this.convertReponse(sResponseFromServer);
                                // MessageBox.Show(sResponseFromServer);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                log.Error("Loi ProcessChecking.Send- " + ex.Message);
                return new SendResult() { Status = false, Message = ex.Message };
            }
        }

        private SendResult convertReponse(string sResponseFromServer)
        {
            try
            {
                Dictionary<string, object> entryDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(sResponseFromServer);
                return new SendResult() { Status = entryDict["success"].ToString() == "1" ? true : false, Message = entryDict["results"].ToString() };
            }
            catch (Exception ex)
            {
                return new SendResult() { Status = false, Message = ex.Message };
            }

            //MessageBox.Show(entryDict);           
        }
    }
}
