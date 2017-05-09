using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceWebsite
{
    class CompareImage
    {
        public string CompareImgWeb(string domain)
        {
            try
            {
                
                return "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        
        private Bitmap image;
        public Bitmap GetImage()
        {
            return this.image;
        }
        public Bitmap CaptureImage(string domain)
        {
            try
            {
                string filename = StaticClass.GetDomainName(domain);
                WebsitesScreenshot.WebsitesScreenshot _Obj;
                _Obj = new WebsitesScreenshot.WebsitesScreenshot();
                //_Obj.NoScripts = true;
                
                WebsitesScreenshot.WebsitesScreenshot.Result _Result;
                _Result = _Obj.CaptureWebpage(domain);
                if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                {
                    _Obj.ImageFormat = WebsitesScreenshot.
                        WebsitesScreenshot.ImageFormats.JPG;
                    _Obj.SaveImage(StaticClass.Path + filename + StaticClass.Extension);
                }
                this.image = _Obj.GetImage();
                _Obj.Dispose();

                return this.image;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}
