using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DefaceWebsite.DFWService;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Imagio;
using DefaceWebsite.Class;
using Newtonsoft.Json.Linq;

namespace DefaceWebsite
{
    public partial class TProcess : UserControl
    {
        public DateTime _scheduleDate;
        public string _term;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public TProcess()
        {
            InitializeComponent();
        }

        public void SetValue(int index, List<string> domains, List<string> links, string _key)
        {
            this.totalLinks = links.Count;
            this.totalDomains = domains.Count;
            this.lstDomain = domains;
            this.lstLink = links;
            this.lblTitle.Text = "Tiến trình " + index.ToString() + ":";
            this.lblDomain.Text = "Số domains: " + this.totalDomains.ToString();
            this.lblLinks.Text = "Số links: " + this.totalLinks.ToString();
            this.lblDone.Text = "Xong: 0";
            this.lblWarning.Text = "Cảnh báo: 0";
            this.key = _key;
        }
        Thread thr;
        private int totalDomains = 0;
        private int totalLinks = 0;
        private List<string> lstDomain;
        private List<string> lstLink;
        private string key = "";
        private void TProcess_Load(object sender, EventArgs e)
        {
            thr = new Thread(CheckDomain);
            //thr.IsBackground = true;
            prcProcess.Maximum = this.totalLinks;
            prcProcess.Step = 1;
        }
        delegate void SetTextCallback(int val);
        delegate void SetMessageCallback(string val);
        delegate void SetNonPar();

        private void CheckDomain()
        {
            try
            {
                while (prcProcess.Value < prcProcess.Maximum)
                {
                    this.CheckAction(this.lstLink[this.prcProcess.Value]);
                    ScheduleClient client = null;
                    try
                    {
                        client = new ScheduleClient();
                        var result = client.ScheduleDt_UpdExecute(_scheduleDate, _term, this.lstLink[this.prcProcess.Value]);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        if (client != null)
                            client.Close();
                        
                    }
                    if (this.prcProcess.InvokeRequired)
                    {
                        SetNonPar d = new SetNonPar(PerStep);
                        this.Invoke(d);
                        //SetTextCallback d = new SetTextCallback(SetText);
                        //this.Invoke(d, new object[] { 1 });
                    }
                   
                    _pauseEvent.WaitOne(Timeout.Infinite);
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    
                    //goi stored update trang thai thanh executed =1
                }
                if (this.prcProcess.InvokeRequired)
                {
                    //thuc hien xong tien trinh
                    SetNonPar dt = new SetNonPar(DisButton);
                    this.Invoke(dt);
                    //Kiem tra hoan thanh 1 dot
                    //StaticClass.countProcess++;
                    //if(StaticClass.countProcess == totalProcess)
                    //{
                    //    //Goi ham tick dot moi SetTimer
                    //}

                }
                Stop();
            }
            catch(Exception ex)
            {
                log.Fatal(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
       // public int totalProcess=0;
        private void DisButton()
        {
            this.btnActive.Enabled = false;
            this.Cursor = Cursors.Default;
        }       

        private void PerStep()
        {
            this.prcProcess.PerformStep();
            this.lblDone.Text = "Xong: " + this.prcProcess.Value.ToString();
            Application.DoEvents();
        }

        private void SetText(int val)
        {
            this.prcProcess.Value += val;
        }

        private void SetMessageInv(string val)
        {
            this.tbxMessage.AppendText("\r\n" + val);
        }

        private void SetWarnInv()
        {
            this.totalWrn++;
            this.lblWarning.Text = "Cảnh báo: " + this.totalWrn.ToString();
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            if (this.btnActive.Text == "Start")
            {
                this.btnActive.Text = "Stop";
                if (thr.ThreadState == ThreadState.Unstarted)
                    thr.Start();
                else //if (thr.ThreadState == ThreadState.Aborted)
                    Resume();
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.btnActive.Text = "Start";
                Pause();
                this.Cursor = Cursors.Default;
            }
        }
        ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        public void Pause()
        {
            _pauseEvent.Reset();
        }

        public void Resume()
        {
            _pauseEvent.Set();
        }

        public void Stop()
        {
            try
            {
                // Signal the shutdown event
                _shutdownEvent.Set();

                // Make sure to resume any paused threads
                _pauseEvent.Set();
                thr.Interrupt();
                // Wait for the thread to exit
                thr.Abort();
                thr.Join();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        
        private void SetMessage(string message)
        {
            log.Info(message);
            message = DateTime.Now.ToString(StaticClass.formatDate) + ": " + message;
            if (this.tbxMessage.InvokeRequired)
            {
                SetMessageCallback d = new SetMessageCallback(SetMessageInv);
                this.Invoke(d, new object[] { message });
            }
            else
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
            if (this.lblWarning.InvokeRequired)
            {
                SetNonPar d = new SetNonPar(SetWarnInv);
                this.Invoke(d);
            }
            else
            {
                this.SetWarnInv();
            }

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
                    //bool sendstatus = SendEmailClass.SendEmail(emailInfo);
                    //this.SetMessage("Gửi email" + sendstatus);
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
                                //JToken rs = result.First;
                                
                                this.SetMessage("Send notification: " + item.UserName + " thành công");
                                //Goi store Update MessageID tu firebase
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
                                catch(Exception ex)
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
                //lstuser = null;
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
            try
            {
                string domain = _domain;
                this.SetMessage("=======Kiểm tra Domain: " + domain + "=======");
                CheckResult res;
                //1. KIEM TRA TIMEOUT || NOT EXISTS
                //2. KIEM TRA IP - CHANGE IP
                this.SetMessage("Kiểm tra Domain, IP");
                res = this.CheckIpTimeOut(domain);
                //KIEM TRA IP && DOMAIN && ERROR CODE
                if (res.Status == DomainType.Ok)
                {
                    //1. KIEM TRA DOMAIN - REDIRECT                
                    //2. KIEM TRA ERRORCODE
                    //this.SetMessage("Kiểm tra Redicrect - Source Code");
                    res = this.GetHttpStatusCode(domain);
                    //KIEM TRA GIAO DIEN - COMPARE IMAGE
                    //1. COMPARE IMAGE
                    //this.SetMessage("Kiểm tra giao diện");
                    //CheckResult resImage = this.CompareImage(domain);
                    //if (res.Status == DomainType.Ok)
                    //{
                    //    float dif = 0;
                    //    float.TryParse(resImage.Values, out dif);
                    //    if (resImage.Status == DomainType.Ok || dif < 30)
                    //    {
                    //        res.Status = DomainType.Ok;
                    //        res.Message = resImage.Message;
                    //        res.Values = resImage.Values;
                    //    }
                    //    else
                    //    {
                    //        res.Status = resImage.Status;
                    //        res.Message = (resImage.Message + (resImage.Values == "" ? "" : (" Tỉ lệ khác nhau: " + resImage.Values)));
                    //        res.Values = resImage.Values;
                    //    }
                    //    //2. COMPARE CONTENT - SOURCE
                    //}
                    //else if (resImage.Status != DomainType.Ok && (resImage.Message == "NotSameSize" || resImage.Values == null || float.Parse(resImage.Values) > 30))
                    //{
                    //    res.Status = DomainType.Image;
                    //    res.Message = res.Message + "; " + resImage.Message;
                    //}
                }

                //CheckResult re = CheckIpTimeOut(this.tbxUser.Text);
                if (res.Status != DomainType.Ok)
                {
                    //MessageBox.Show(res.Message);
                    this.SetMessage("Kết quả: Cảnh báo - " + res.Status + ": " + res.Message);
                    this.SetWarning(res, _domain);
                }
                else this.SetMessage("Kết quả: Không phát hiện bất thường");  //MessageBox.Show("Không phát hiện bất thường");
                
              
                this.SetMessage("=======Kiểm tra xong=======");
                //log.Info("=======Kiểm tra xong=======");
            }
            catch(Exception ex)
            {
                log.Fatal(ex.Message);
            }
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

                this.SetMessage(checkresult.Status + " " + checkresult.Message);
                result = null;
               // Array.Clear(result, 0, result.Count());
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
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        private CheckResult GetHttpStatusCode(string domain)
        {
            CheckDomainClient client = null;
            //phai kiem tra timeout truoc khi check status code
            try
            {
                client = new CheckDomainClient();
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest
                                            .Create(domain);
                webRequest.Credentials = CredentialCache.DefaultCredentials;  

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
                //res
                //Returns "MovedPermanently", not 301 which is what I want.
                //Console.Write(response.StatusCode.ToString());
                this.SetMessage(checkresult.Status + " " + checkresult.Message);
            //    Array.Clear(result, 0, result.Count());
                result = null;
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
                bool are_identical = true;
                //this.Cursor = Cursors.WaitCursor;
                //Application.DoEvents();
                this.SetMessage("So sánh ảnh...");
                ComparingImages.CompareResult cr = ComparingImages.CompareSHA256(bm1, bm2);
                if( cr == ComparingImages.CompareResult.ciCompareOk)
                {
                    checkresult.Message = "OK";
                    checkresult.Status = DomainType.Ok;
                    this.SetMessage(checkresult.Message);
                }
                if (cr == ComparingImages.CompareResult.ciSizeMismatch || (bm1.Width != bm2.Width) || (bm1.Height != bm2.Height))
                {
                        checkresult.Status = DomainType.Image;
                        checkresult.Message = "NotSameSize";
                        checkresult.Values = "NotSameSize";
                        this.SetMessage(checkresult.Message);
                        are_identical = false;
                }
                if(cr == ComparingImages.CompareResult.ciPixelMismatch)
                {
                    checkresult.Values = ComparingImages.DifCount;
                    checkresult.Status = DomainType.Image;
                    checkresult.Message = "NotSameSize";
                    this.SetMessage(checkresult.Message);
                }
                if (are_identical)
                {
                    checkresult.Status = DomainType.Ok;
                    checkresult.Message = "OK";
                    this.SetMessage(checkresult.Message);
                }
                else
                {
                    if (checkresult.Message == "")
                        checkresult.Message = "The images are different " + ComparingImages.DifCount;
                    checkresult.Status = DomainType.Image;
                    //checkresult.Message = "The images are different " + Math.Round(1.00 * dif / wid * hgt, 2) * 100;
                    checkresult.Values = ComparingImages.DifCount;
                    this.SetMessage(checkresult.Status + " " + checkresult.Message);
                }
                result = null;
                bm1.Dispose();
                bm2.Dispose();
                GC.Collect(GC.GetGeneration(bm1));
                GC.Collect(GC.GetGeneration(bm2));
                //Array.Clear(result, 0, result.Count());
                
                //// Make a difference image.
                //int wid = Math.Min(bm1.Width, bm2.Width);
                //int hgt = Math.Min(bm1.Height, bm2.Height);
                //Bitmap bm3 = (Bitmap)bm1;//new Bitmap(wid, hgt);                

                //int dif = 0;
                //// Create the difference image.
                //bool are_identical = true;
                //Color eq_color = Color.White;
                //Color ne_color = Color.Red;
                //for (int x = 0; x < wid; x++)
                //{
                //    for (int y = 0; y < hgt; y++)
                //    {
                //        if (bm1.GetPixel(x, y).Equals(bm2.GetPixel(x, y)))
                //        {
                //            bm3.SetPixel(x, y, bm1.GetPixel(x, y));
                //        }
                //        else
                //        {
                //            bm3.SetPixel(x, y, ne_color);
                //            are_identical = false;
                //            dif++;
                //        }
                //    }
                //}
                ////luu file anh khac biet
                //bm3.Save(StaticClass.PathDif + imageItem.FileName + StaticClass.Extension);
                //// Display the result.
                ////picResult.Image = bm3;

                ////this.Cursor = Cursors.Default;
                //if ((bm1.Width != bm2.Width) || (bm1.Height != bm2.Height))
                //{
                //    are_identical = false;
                //    checkresult.Status = DomainType.Image;
                //    checkresult.Message = "NotSameSize";
                //    this.SetMessage(checkresult.Message);
                //    //checkresult.Values = "NotSameSize";
                //    //return checkresult;
                //}
                //if (are_identical)
                //{
                //    checkresult.Status = DomainType.Ok;
                //    checkresult.Message = "OK";
                //    this.SetMessage(checkresult.Message);
                //}
                //else
                //{
                //    if (checkresult.Message == "")
                //        checkresult.Message = "The images are different " + Math.Round(1.00 * dif / (wid * hgt), 2) * 100;
                //    checkresult.Status = DomainType.Image;
                //    //checkresult.Message = "The images are different " + Math.Round(1.00 * dif / wid * hgt, 2) * 100;
                //    checkresult.Values = (Math.Round(1.00 * dif / (wid * hgt), 2) * 100).ToString();
                //    this.SetMessage(checkresult.Status + " " + checkresult.Message);
                //}

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
                    GC.Collect(GC.GetGeneration(byteArray));
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
