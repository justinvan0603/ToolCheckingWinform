using DefaceWebsite.DFWService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefaceWebsite
{
    public partial class frmCompareImage : Form
    {
        public frmCompareImage()
        {
            InitializeComponent();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {

            //CheckDomainClient client = null;
            try
            {
                //if (this.txbDomain.Text == null)
                //{
                //    MessageBox.Show("Cho nhập domain");
                //    return;
                //}

                //client = new CheckDomainClient();

                //string filename = StaticClass.GetDomainName(this.txbDomain.Text);

                ////Kiem tra file ảnh đã lưu
                //this.lblProcess.Text = "Lấy dữ liệu cũ";
                //Application.DoEvents();
                //DomainChange_SearchResult[] result = client.DomainChange_Search(new DomainChange_SearchResult() { DOMAIN = this.txbDomain.Text, TYPE = DomainType.Image, IS_LEAF = "Y" });
                //string fullpath = "";// StaticClass.Path + "r_" + filename + StaticClass.Extension;
                //if (result != null && result.Count() > 0)
                //    fullpath = result[0].VALUES;
                //if (File.Exists(fullpath))
                //    picImage2.Image = new Bitmap(fullpath);                
                //Application.DoEvents();

                ////Lay ảnh hiện tại
                //this.lblProcess.Text = "Lấy dữ liệu hiện tại";
                //Application.DoEvents();
                //picImage1.Image = StaticClass.CaptureImage(this.txbDomain.Text);
                ////luu lai anh
                //client.DomainChange_Ins(new DomainChange_SearchResult() { DOMAIN = this.txbDomain.Text, TYPE = DomainType.Image, VALUES = StaticClass.FullPathImage });
                //Application.DoEvents();

               // if (picImage2.Image == null)                    
               // {
               //     this.lblResult.Text = "Dữ liệu đã được lưu. Lần đầu tiên nên chưa có dữ liệu compare";
               //     return;
               // }

               // this.lblProcess.Text = "So sánh dữ liệu";
               //// Application.DoEvents();
               // this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                // Load the images.
                Bitmap bm1 = (Bitmap)picImage1.Image;
                Bitmap bm2 = (Bitmap)picImage2.Image;

                // Make a difference image.
                int wid = Math.Min(bm1.Width, bm2.Width);
                int hgt = Math.Min(bm1.Height, bm2.Height);
                Bitmap bm3 = (Bitmap)picImage2.Image;//new Bitmap(wid, hgt);

                this.progressBar1.Maximum = wid * hgt;
                this.progressBar1.Step = 1;

                int dif = 0;
                // Create the difference image.
               // bool are_identical = true;
                Color eq_color = Color.White;
                Color ne_color = Color.Red;
                for (int x = 0; x < wid; x++)
                {
                    for (int y = 0; y < hgt; y++)
                    {
                        if (bm1.GetPixel(x, y).Equals(bm2.GetPixel(x, y)))
                            bm3.SetPixel(x, y, bm1.GetPixel(x, y));
                        else
                        {
                            bm3.SetPixel(x, y, ne_color);
                            //are_identical = false;
                            dif++;
                        }
                       // this.progressBar1.Value += this.progressBar1.Step;
                    }
                }

                // Display the result.
                picResult.Image = bm3;
                
                //this.Cursor = Cursors.Default;
                //if ((bm1.Width != bm2.Width) || (bm1.Height != bm2.Height)) are_identical = false;
                //if (are_identical)
                //    lblResult.Text = "The images are identical";
                //else
                //    lblResult.Text = "The images are different " + Math.Round(1.00*dif/this.progressBar1.Maximum,2)*100;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //if (client != null)
                //    client.Close();                
            }
        }

        private void btnGetSource_Click(object sender, EventArgs e)
        {
            
            string urlAddress = "http://google.com";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                MessageBox.Show(data);
            }

            
        }

        
    }
}
