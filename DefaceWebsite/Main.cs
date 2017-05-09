using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DefaceWebsite.DFWService;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Imagio;
using System.Security.Cryptography;

namespace DefaceWebsite
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //this.clientService = new MessagesClient();
        }

        private MessagesClient clientService;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //SendResult res = convertReponse(this.textBox1.Text);
                //if (res.Status) MessageBox.Show("thành công: " + res.Message);
                //else MessageBox.Show("thất bại: " + res.Message);

                MessageBox.Show(Dns.GetHostAddresses(StaticClass.GetDomainName(this.tbxUser.Text)).FirstOrDefault().ToString());
                ////List<Messages_SearchResult> result = this.clientService.Messages_Search(this.tbxUser.Text, this.tbxDomain.Text, "").ToList();                
                //ListDomainClient clit = new ListDomainClient();
                //List<Listdomain_SearchResult> result = clit.Listdomain_Search(this.tbxUser.Text, this.tbxDomain.Text).ToList();                
                
                //this.dtgLstMessage.DataSource = result;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetDetail(string content)
        {
            //string pattern = @"tel:.*";
            string pattern = "tel:[^\"]*\"";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> lstPhone = new List<string>();
            List<string> lstEmail = new List<string>();
            string _value = "";
            int index = 1;
            MatchCollection matches = rgx.Matches(content);
            if (matches.Count > 0)
            {
                //Console.WriteLine("({1} matches):", matches.Count);
                foreach (Match match in matches)
                {
                    _value = match.Value.Replace("tel:", "");
                    _value = _value.Replace("\"", "");
                    _value = _value.Replace(" ", "");

                    if (!lstPhone.Contains(_value))
                    {
                        lstPhone.Add(_value);
                        Console.WriteLine(index + ": " + _value);
                    }

                    index++;
                }

            }
            pattern = "mailto:[^\"]*\"";
            rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            matches = rgx.Matches(content);
            if (matches.Count > 0)
            {
                //Console.WriteLine("({1} matches):", matches.Count);
                foreach (Match match in matches)
                {
                    _value = match.Value.Replace("mailto:", "");
                    _value = _value.Replace("\"", "");
                    _value = _value.Replace(" ", "");

                    if (!lstEmail.Contains(_value))
                    {
                        lstEmail.Add(_value);
                        Console.WriteLine(index + ": " + _value);
                    }

                    index++;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //string pattern = @"tel:.*";
            string pattern = "tel:[^\"]*\"";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> lstPhone = new List<string>();
            List<string> lstEmail = new List<string>();
            string _value = "";
            int index = 1;
            MatchCollection matches = rgx.Matches(this.textBox1.Text);
            if (matches.Count > 0)
            {
                //Console.WriteLine("({1} matches):", matches.Count);
                foreach (Match match in matches)
                {
                    _value = match.Value.Replace("tel:", "");
                    _value = _value.Replace("\"", "");
                    _value = _value.Replace(" ", "");

                    if (!lstPhone.Contains(_value))
                    {
                        lstPhone.Add(_value);
                        Console.WriteLine(index + ": " + _value);
                    }

                    index++;
                }

            }
            pattern = "mailto:[^\"]*\"";
            rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            matches = rgx.Matches(this.textBox1.Text);
            if (matches.Count > 0)
            {
                //Console.WriteLine("({1} matches):", matches.Count);
                foreach (Match match in matches)
                {
                    _value = match.Value.Replace("mailto:", "");
                    _value = _value.Replace("\"", "");
                    _value = _value.Replace(" ", "");

                    if (!lstEmail.Contains(_value))
                    {
                        lstEmail.Add(_value);
                        Console.WriteLine(index + ": " + _value);
                    }

                    index++;
                }

            }
            //this.textBox1.SelectAll();

            //this.textBox1.Focus();
            //ImageComparer img = new ImageComparer();
            //DefaceWebsite.ImageComparer.ImageRelation resul = img.CompareImages((Bitmap)picImage1.Image, (Bitmap)picImage2.Image);
            //MessageBox.Show(resul.ToString());

            //this.picResult.Image = StaticClass.CaptureImage(this.tbxUser.Text);

            //this.Cursor = Cursors.WaitCursor;
            //Application.DoEvents();

            //// Load the images.
            //Bitmap bm1 = (Bitmap)picImage1.Image;
            //Bitmap bm2 = (Bitmap)picImage2.Image;

            //// Make a difference image.
            //int wid = Math.Min(bm1.Width, bm2.Width);
            //int hgt = Math.Min(bm1.Height, bm2.Height);
            //Bitmap bm3 = new Bitmap(wid, hgt);

            //// Create the difference image.
            //bool are_identical = true;
            //Color eq_color = Color.White;
            //Color ne_color = Color.Red;
            //for (int x = 0; x < wid; x++)
            //{
            //    for (int y = 0; y < hgt; y++)
            //    {
            //        if (bm1.GetPixel(x, y).Equals(bm2.GetPixel(x, y)))
            //            bm3.SetPixel(x, y, eq_color);
            //        else
            //        {
            //            bm3.SetPixel(x, y, ne_color);
            //            are_identical = false;
            //        }
            //    }
            //}

            //// Display the result.
            //picResult.Image = bm3;

            //this.Cursor = Cursors.Default;
            //if ((bm1.Width != bm2.Width) || (bm1.Height != bm2.Height)) are_identical = false;
            //if (are_identical)
            //    lblResult.Text = "The images are identical";
            //else
            //    lblResult.Text = "The images are different";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ////Users dt = new Users();
            ////dt.Show();
            //this.textBox1.Text = this.GetDocumentContents(this.tbxUser.Text);
            //this.textBox1.SelectAll();
            //this.textBox1.Focus();

            using (ConfigsClient client = new ConfigsClient())
            {
                MessageBox.Show(client.Userconfig_Upd_A("thieu1234", @"<Root>
                      <Config>
                        <CONF_TYPE>TOUT</CONF_TYPE>
                        <CONF_VALUE>1</CONF_VALUE>    
                      </Config>
                      <Config>
                        <CONF_TYPE>CIP</CONF_TYPE>
                        <CONF_VALUE>1</CONF_VALUE>    
                      </Config>
                      <Config>
                        <CONF_TYPE>RDOM</CONF_TYPE>
                        <CONF_VALUE>1</CONF_VALUE>    
                      </Config>
                      <Config>
                        <CONF_TYPE>ECODE</CONF_TYPE>
                        <CONF_VALUE>1</CONF_VALUE>    
                      </Config>
                      <Config>
                        <CONF_TYPE>CCON</CONF_TYPE>
                        <CONF_VALUE>1</CONF_VALUE>    
                      </Config>
                    </Root>").ErrorDesc);
            }
        }

        private string GetDocumentContents(string domain)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(domain);

            WebResponse response = request.GetResponse();
            string result;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd(); // do something fun...
            }
            
            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = this.CaptureWeb();
            if (result == "0")
                MessageBox.Show("Capture thành công");
            else MessageBox.Show("Lỗi: " + result);

           
        }


        private string path = System.IO.Directory.GetCurrentDirectory() + @"\DownloadFile\";

        private string CaptureWebpage(string page, string name)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(page) as HttpWebRequest;
                Bitmap bitmap;
                using (Stream stream = request.GetResponse().GetResponseStream())
                {
                    bitmap = new Bitmap(stream);
                }

                if (bitmap != null)
                    bitmap.Save(path + name, ImageFormat.Jpeg);
                return "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string CapturePage()
        {
            try
            {
                int width = 800;
                int height = 600;

                using (WebBrowser browser = new WebBrowser())
                {
                    browser.Width = width;
                    browser.Height = height;
                    browser.ScrollBarsEnabled = true;

                    // This will be called when the page finishes loading
                    browser.DocumentCompleted += OnDocumentCompleted;

                    browser.Navigate("http://stackoverflow.com/");

                    // This prevents the application from exiting until
                    // Application.Exit is called
                    //Application.Run();
                }

                return "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        private void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Now that the page is loaded, save it to a bitmap
            WebBrowser browser = (WebBrowser)sender;

            using (Graphics graphics = browser.CreateGraphics())
            using (Bitmap bitmap = new Bitmap(browser.Width, browser.Height, graphics))
            {
                Rectangle bounds = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                browser.DrawToBitmap(bitmap, bounds);
                bitmap.Save(path+"screenshot.jpg", ImageFormat.Jpeg);
            }

            // Instruct the application to exit
           // Application.Exit();
        }

        private string CaptureWeb()
        {
            try
            {
                WebBrowser browser = new WebBrowser();
                browser.Width = 800;
                browser.Height = 600;
                browser.ScrollBarsEnabled = true;

                browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(loadCompleted);
                browser.Navigate("http://google.com.vn");
                
                return "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void loadCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            while (browser.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
                Thread.Sleep(2);
            }
            using (Graphics graphics = browser.CreateGraphics())
            using (Bitmap bitmap = new Bitmap(browser.Width, browser.Height, graphics))
            {
                Rectangle bounds = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                browser.DrawToBitmap(bitmap, bounds);
                bitmap.Save(path + "screenshot.jpg", ImageFormat.Jpeg);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //WebBrowser browser = sender as WebBrowser;
            //using (Bitmap bitmap = new Bitmap(webBrowser1.Width, webBrowser1.Height))
            //{
            //    webBrowser1.DrawToBitmap(bitmap, new Rectangle(0, 0, webBrowser1.Width, webBrowser1.Height));
            //    using (MemoryStream stream = new MemoryStream())
            //    {
            //        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            //        //byte[] bytes = stream.ToArray();
            //        //imgScreenShot.Visible = true;
            //        //imgScreenShot.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
            //    }
            //}

            //WebsitesScreenshot.WebsitesScreenshot _Obj;
            //_Obj = new WebsitesScreenshot.WebsitesScreenshot();
            //WebsitesScreenshot.WebsitesScreenshot.Result _Result;
            //_Result = _Obj.CaptureWebpage("http://24h.com.vn");
            //if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
            //{
            //    _Obj.ImageFormat = WebsitesScreenshot.
            //        WebsitesScreenshot.ImageFormats.JPG;
            //    _Obj.SaveImage(path + "screenshot.jpg");
            //}
            //_Obj.Dispose();

            WebBrowser bro = new WebBrowser();
            bro.AllowNavigation = true;
            bro.ScriptErrorsSuppressed = true;
            bro.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(loadDocCompleted);
            bro.Navigate("http://24h.com.vn");
            
            
        }

        private void loadDocCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                WebBrowser bro = (WebBrowser)sender;
                if (bro != null)
                    this.textBox1.Text = bro.DocumentText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            SendResult res = Send("fyk_PP-CF_8:APA91bGrz2Ws_X59i6z1oVMG2neY5gqiC0fshtMbUUEsBaVqeE8RpIgOiCxMMDtj2m-OZZ2BXr2PsSX8pGLvIBT5xhRCm-amsNjQ_HRPi69PmW6UpzXstChwdcbbuv6Z7dtC3ntIaLWT", "Title 123", "Nội dung thông báoNội dung thông báoNội dung thông báoNội dung thông báoNội dung thông báoNội dung thông báoNội dung thông báoNội dung thông báoNội dung thông báo", "myicon");
            if (res.Status) MessageBox.Show("thành công: " + res.Message);
            else MessageBox.Show("thất bại: " + res.Message);
            // MessageBox.Show(this.SendNotification("esf0lWXzoug:APA91bFVTRfu5jOQPrtzjd7MzA7SptotrjuUSipqHbjk7sMEqmET-Rt0cF5-pfO3Bwt737ltIFCckq95AvLIyDwD_2mxLlJ7QGYwsZSOUvbkAZeAaXZoTVPXUFcecaBEfcuZKBAKT-gh", "Hello app"));
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
                       // icon = _icon
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
                                this.textBox1.Text = sResponseFromServer;
                                                               
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

        private void button6_Click(object sender, EventArgs e)
        {
            //GetHttpStatusCode(this.tbxUser.Text);
            //CheckResult re = CheckIpTimeOut(this.tbxUser.Text);
            //if (re.Status != DomainType.Ok)
            //    MessageBox.Show(re.Message);
            //else MessageBox.Show("Không phát hiện bất thường");

            IPHostEntry host = Dns.GetHostEntry(StaticClass.GetDomainName("http://vnexpress.net"));
            //List<string> lstIp = new List<string>();
            //Console.WriteLine("GetHostEntry({0}) returns:", hostname);
            CheckDomainClient client = new CheckDomainClient();
            //Lay du lieu hien tai                
            DomainProfile_SearchResult[] result = client.DomainProfile_Search(new DomainProfile_SearchResult() { DOMAIN = "http://vnexpress.net", TYPE = DomainType.Ip });
            string currentIp = "";
            if (result != null && result.Count() > 0)
                currentIp = result[0].VALUES;

            //Lay danh sach IP hien tai
            string lstIp = "";
            //Neu lan dau thi luu danh sach
            if (currentIp == "")
            {
                foreach (IPAddress ip in host.AddressList)
                {
                    //  Console.WriteLine("    {0}", ip);
                    //lstIp.Add(ip.ToString());
                    if (ip.ToString().Contains("."))
                        lstIp += ip.ToString() + ";";
                }
                MessageBox.Show("null: " + lstIp);
            }
            else
            {
                host.AddressList.FirstOrDefault().ToString();
                MessageBox.Show("not null: " + lstIp);
            }
        }

        public void DisplayIPv4NetworkInterfaces()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            Console.WriteLine("IPv4 interface information for {0}.{1}",
               properties.HostName, properties.DomainName);
            Console.WriteLine();

            foreach (NetworkInterface adapter in nics)
            {
                // Only display informatin for interfaces that support IPv4.
                if (adapter.Supports(NetworkInterfaceComponent.IPv4) == false)
                {
                    continue;
                }
                Console.WriteLine(adapter.Description);
                // Underline the description.
                Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                // Try to get the IPv4 interface properties.
                IPv4InterfaceProperties p = adapterProperties.GetIPv4Properties();

                if (p == null)
                {
                    Console.WriteLine("No IPv4 information is available for this interface.");
                    Console.WriteLine();
                    continue;
                }
                // Display the IPv4 specific data.
                Console.WriteLine("  Index ............................. : {0}", p.Index);
                Console.WriteLine("  MTU ............................... : {0}", p.Mtu);
                Console.WriteLine("  APIPA active....................... : {0}",
                    p.IsAutomaticPrivateAddressingActive);
                Console.WriteLine("  APIPA enabled...................... : {0}",
                    p.IsAutomaticPrivateAddressingEnabled);
                Console.WriteLine("  Forwarding enabled................. : {0}",
                    p.IsForwardingEnabled);
                Console.WriteLine("  Uses WINS ......................... : {0}",
                    p.UsesWins);
                Console.WriteLine();
            }
        }

        public void ShowInterfaceSummary()
        {

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in interfaces)
            {
                Console.WriteLine("Name: {0}", adapter.Name);
                Console.WriteLine(adapter.Description);
                Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                Console.WriteLine("  Operational status ...................... : {0}",
                    adapter.OperationalStatus);
                string versions = "";

                // Create a display string for the supported IP versions.
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    versions = "IPv4";
                }
                if (adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    if (versions.Length > 0)
                    {
                        versions += " ";
                    }
                    versions += "IPv6";
                }
                Console.WriteLine("  IP version .............................. : {0}", versions);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        //private void Ping()
        //{
        //    Ping pingSender = new Ping();
        //    PingReply reply = pingSender.Send("www.google.com.vn");
        //    if (reply.Status == IPStatus.Success)
        //    {
        //        Console.WriteLine("Address: {0}", reply.Address.ToString());
        //        Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
        //       // Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
        //       // Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
        //       // Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
        //    }
        //    else
        //    {
        //        Console.WriteLine(reply.Status);
        //    }
        //}

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
                
                //luu lai IP vua kiem tra
                client.DomainChange_Ins(new DomainChange_SearchResult() { DOMAIN = hostname, TYPE = DomainType.Ip, VALUES = lstIp });

                CheckResult checkresult = new CheckResult() { Status = DomainType.Ok, Message = "" };

                if (!currentIp.Contains(lstIp))
                {
                    checkresult.Status = DomainType.Ip;
                    checkresult.Message = "IP: " + lstIp + " không khớp IP đã khai báo";
                }
                return checkresult;
            }
            catch (Exception ex)
            {
                return new CheckResult() { Status = DomainType.Timeout, Message = ex.Message };
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }

        //private bool RemoteFileExists(string url)
        //{
        //    try
        //    {
        //        //Creating the HttpWebRequest
        //        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //        //Setting the Request method HEAD, you can also use GET too.
        //        request.Method = "HEAD";
        //        //Getting the Web Response.
        //        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        //        //Returns TRUE if the Status code == 200
        //        response.Close();
        //        return (response.StatusCode == HttpStatusCode.OK);
        //    }
        //    catch
        //    {
        //        //Any exception will returns false.
        //        return false;
        //    }
        //}

        //private void CheckError(string domain)
        //{
        //    try
        //    {
        //        using (var client = new WebClient())
        //        {

        //            // fine, no content downloaded
        //            string s1 = client.DownloadString(domain);
        //            // throws 404
        //            // string s2 = client.DownloadString("http://google.com/silly");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
            
        //}

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
                }
                //Returns "MovedPermanently", not 301 which is what I want.
                //Console.Write(response.StatusCode.ToString());

                return checkresult;
            }
            catch (WebException we)
            {
                HttpStatusCode statusCode = ((HttpWebResponse)we.Response).StatusCode;
                string message = "Lỗi: " + ((int)statusCode).ToString() + " - " + statusCode.ToString();
               // Console.Write("Error: " + ((int)statusCode).ToString() + ":" + statusCode.ToString());
                //luu lai IP vua kiem tra
                client.DomainChange_Ins(new DomainChange_SearchResult() { DOMAIN = domain, TYPE = DomainType.Resource, VALUES = message });
                
                //wRespStatusCode = ((HttpWebResponse)we.Response).StatusCode;
                return new CheckResult() { Status = DomainType.Resource, Message = message };
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

                //Kiem tra file ảnh đã lưu                                
                DomainProfile_SearchResult[] result = client.DomainProfile_Search(new DomainProfile_SearchResult() { DOMAIN = domain, TYPE = DomainType.Image });
                string fullpath = "";
                if (result != null && result.Count() > 0)
                    fullpath = result[0].VALUES;
                Bitmap bm1 = null;
                if (File.Exists(fullpath))
                    bm1 = new Bitmap(fullpath);                

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
                    return checkresult;
                }
                
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();               

                // Make a difference image.
                int wid = Math.Min(bm1.Width, bm2.Width);
                int hgt = Math.Min(bm1.Height, bm2.Height);
                Bitmap bm3 = (Bitmap)bm1;//new Bitmap(wid, hgt);                
                
                int dif = 0;
                // Create the difference image.
                bool are_identical = true;
                Color eq_color = Color.White;
                Color ne_color = Color.Red;
                for (int x = 0; x < wid; x++)
                {
                    for (int y = 0; y < hgt; y++)
                    {
                        if (bm1.GetPixel(x, y).Equals(bm2.GetPixel(x, y)))
                        {
                            bm3.SetPixel(x, y, bm1.GetPixel(x, y));
                        }                            
                        else
                        {
                            bm3.SetPixel(x, y, ne_color);
                            are_identical = false;
                            dif++;
                        }                        
                    }
                }
                //luu file anh khac biet
                bm3.Save(StaticClass.PathDif + imageItem.FileName + StaticClass.Extension);
                // Display the result.
                //picResult.Image = bm3;

                this.Cursor = Cursors.Default;
                if ((bm1.Width != bm2.Width) || (bm1.Height != bm2.Height))
                {
                    are_identical = false;
                    checkresult.Status = DomainType.Image;
                    checkresult.Message = "NotSameSize";
                    //checkresult.Values = "NotSameSize";
                    //return checkresult;
                }
                if (are_identical)
                {
                    checkresult.Status = DomainType.Ok;
                    checkresult.Message = "OK";
                }                    
                else
                {
                    if (checkresult.Message == "")
                        checkresult.Message = "The images are different " + Math.Round(1.00 * dif / (wid * hgt), 2) * 100;
                    checkresult.Status = DomainType.Image;
                    //checkresult.Message = "The images are different " + Math.Round(1.00 * dif / wid * hgt, 2) * 100;
                    checkresult.Values = (Math.Round(1.00 * dif / (wid * hgt), 2) * 100).ToString();
                }

                return checkresult;
            }
            catch (Exception ex)
            {
                return new CheckResult() { Status = DomainType.Image, Message = "CompareImage: " + ex.Message };
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string domain = this.tbxUser.Text;
            CheckResult res;
            //1. KIEM TRA TIMEOUT || NOT EXISTS
            //2. KIEM TRA IP - CHANGE IP
            res = this.CheckIpTimeOut(domain);
            //KIEM TRA IP && DOMAIN && ERROR CODE
            if (res.Status == DomainType.Ok)
            {
                //1. KIEM TRA DOMAIN - REDIRECT                
                //2. KIEM TRA ERRORCODE
                res = this.GetHttpStatusCode(domain);
                //KIEM TRA GIAO DIEN - COMPARE IMAGE
                //1. COMPARE IMAGE
                CheckResult resImage = this.CompareImage(domain);
                if(res.Status == DomainType.Ok)
                {
                    if (resImage.Status == DomainType.Ok || float.Parse(resImage.Values) < 30)
                    {
                        res.Status = DomainType.Ok;
                        res.Message = resImage.Message;
                    }
                    else
                    {
                        res.Status = resImage.Status;
                        res.Message = resImage.Message;
                    }                   
                    //2. COMPARE CONTENT - SOURCE
                }
                else if(resImage.Status != DomainType.Ok && (resImage.Message == "NotSameSize" || float.Parse(resImage.Values) > 30))
                {
                    res.Status = DomainType.Image;
                    res.Message = res.Message + "; " + resImage.Message;
                }
            }

            //CheckResult re = CheckIpTimeOut(this.tbxUser.Text);
            if (res.Status != DomainType.Ok)
                MessageBox.Show(res.Message);
            else MessageBox.Show("Không phát hiện bất thường");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DefaceWebsite.SendEmailClass.EmailInfo emailInfo = new DefaceWebsite.SendEmailClass.EmailInfo();
            emailInfo.ToEmail = new List<string>();            
            emailInfo.Subj ="[DEFACE] Thông báo website bị thay đổi";
            emailInfo.Message = "Nội dung email thông báo giao diện";
            List<string> ListEmail = new List<string>();
            ListEmail.Add("vqthieuuit@gmail.com");
            ListEmail.Add("thieuvq@gsoft.com.vn");
            foreach (var item in ListEmail)
            {
                emailInfo.ToEmail.Add(item);
            }
            SendEmailClass.SendEmail(emailInfo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
            
           // this.lbstart.Text = DateTime.Now.ToLongTimeString();
            ComparingImages.CompareResult cr = ComparingImages.CompareMD5((Bitmap)picImage1.Image, (Bitmap)picImage2.Image);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            this.lbstart.Text = elapsedMs.ToString();
            this.lbend.Text = ComparingImages.DifCount; //Enum.GetName(typeof(ComparingImages.CompareResult), cr);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here

            // this.lbstart.Text = DateTime.Now.ToLongTimeString();
            ComparingImages.CompareResult cr = ComparingImages.CompareSHA256((Bitmap)picImage1.Image, (Bitmap)picImage2.Image);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            this.lbresult.Text = elapsedMs.ToString();
            this.lbresul2.Text = ComparingImages.DifCount;//Enum.GetName(typeof(ComparingImages.CompareResult), cr);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Console.WriteLine("SEQUENCE EQUAL: " + (HashImageSHA256((Bitmap)picImage1.Image).SequenceEqual(HashImageSHA256((Bitmap)picImage2.Image)) ? "TRUE" : "FALSE") + " (easiest way)");
            Console.WriteLine("UTF8 STRING:    " + (System.Text.Encoding.UTF8.GetString(HashImageSHA256((Bitmap)picImage1.Image)) == System.Text.Encoding.UTF8.GetString(HashImageSHA256((Bitmap)picImage2.Image)) ? "TRUE" : "FALSE") + " (conversion to utf string - not good for display or hash, good only for data from UTF8 range)");
            Console.WriteLine("HASH STRING:    " + (Convert.ToBase64String(HashImageSHA256((Bitmap)picImage1.Image)) == Convert.ToBase64String(HashImageSHA256((Bitmap)picImage2.Image)) ? "TRUE" : "FALSE") + " (best to display)");

            Console.WriteLine("1: " + Convert.ToBase64String(HashImageSHA256((Bitmap)picImage1.Image)));
            Console.WriteLine("2: " + Convert.ToBase64String(HashImageSHA256((Bitmap)picImage2.Image)));
        }

        public byte[] HashImage(Bitmap image)
        {
            var sha256 = SHA256.Create();

            var rect = new Rectangle(0, 0, image.Width, image.Height);
            var data = image.LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);

            var dataPtr = data.Scan0;

            var totalBytes = (int)Math.Abs(data.Stride) * data.Height;
            var rawData = new byte[totalBytes];
            System.Runtime.InteropServices.Marshal.Copy(dataPtr, rawData, 0, totalBytes);

            image.UnlockBits(data);

            return sha256.ComputeHash(rawData);
        }

        public byte[] HashImageSHA256(Bitmap bmp1)
        {
                        
            System.Drawing.ImageConverter ic = new System.Drawing.ImageConverter();
            byte[] btImage1 = new byte[1];
            btImage1 = (byte[])ic.ConvertTo(bmp1, btImage1.GetType());
          
            //Compute a hash for each image
            SHA256Managed shaM = new SHA256Managed();
            byte[] hash1 = shaM.ComputeHash(btImage1);

            return hash1;
        }



    }

    
}
