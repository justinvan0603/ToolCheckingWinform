using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefaceWebsite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Gecko.Xpcom.Initialize("xulrunner");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //gBrowser.Navigate("http://gsoft.com.vn");

            //this.button2.Enabled = false;

            //WebClient client = new WebClient();
            //client.OpenReadCompleted += new OpenReadCompletedEventHandler(clientOpenCompleted);
            //Uri uri = new Uri("http://24h.com.vn");
            //client.OpenReadAsync(uri);

            string filesoucre = this.getHTML(this.textBox1.Text);
            this.SaveFileRequets(filesoucre);
            this.richTextBox1.Text = filesoucre;
        }

        public string getHTML(string url)
        {
            //Create request for given url
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            //Create response-object
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //Take response stream
            StreamReader sr = new StreamReader(response.GetResponseStream());

            //Read response stream (html code)
            string html = sr.ReadToEnd();

            //Close streamreader and response
            sr.Close();
            response.Close();

            //return source
            return html;
        }

        private bool SaveFileRequets(string content)
        {
            try
            {
                string currentPath = System.IO.Directory.GetCurrentDirectory() + @"\DownloadFile\";
                if (!System.IO.Directory.Exists(currentPath))
                {
                    System.IO.Directory.CreateDirectory(currentPath);
                }
                string path = currentPath + @"\" + "abc" + ".txt";
                // using (StreamWriter sw = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(content);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void clientOpenCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
                MessageBox.Show(e.Error.Message);
            else
            {
                const string pattern = @"a href=""(?<link>.+)""";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                TextReader tr = new StreamReader(e.Result);
                string content = tr.ReadToEnd();
                MatchCollection mc = regex.Matches(content);

                foreach (Match match in mc)
                {
                    listBox1.Items.Add(match.Groups["link"]);
                }

                tr.Close();

                this.button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(gBrowser.ProductVersion);
        }
    }
}
