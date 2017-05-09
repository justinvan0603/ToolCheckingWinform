using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace DefaceWebsite
{
    public partial class frmTestSendNotify : Form
    {
        public frmTestSendNotify()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendResult res = Send(this.textBox1.Text, this.textBox3.Text, this.textBox4.Text, this.comboBox1.SelectedItem.ToString());
            if (res.Status) this.textBox2.Text = "thành công: " + res.Message;
            else this.textBox2.Text = "thất bại: " + res.Message;
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
                        icon = _icon// info, warning, alert
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
                                this.textBox2.Text = sResponseFromServer;

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

        private void frmTestSendNotify_Load(object sender, EventArgs e)
        {
            //warning, alert
            this.comboBox1.Items.Add("info");
            this.comboBox1.Items.Add("warning");
            this.comboBox1.Items.Add("alert");

            this.comboBox1.SelectedIndex = 0;
        }
    }
}
