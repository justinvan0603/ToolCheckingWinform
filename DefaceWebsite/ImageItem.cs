using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceWebsite
{
    class ImageItem
    {               
       
        public string FullPathImage = "";
        public string FileName = "";
        public Bitmap CaptureImage(string domain)
        {
            try
            {
                this.FileName = Guid.NewGuid().ToString();//StaticClass.GetDomainName(domain);

                WebsitesScreenshot.WebsitesScreenshot _Obj;
                _Obj = new WebsitesScreenshot.WebsitesScreenshot();
                _Obj.NoScripts = true;
                WebsitesScreenshot.WebsitesScreenshot.Result _Result;
                _Result = _Obj.CaptureWebpage(domain);
                if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                {
                    _Obj.ImageFormat = WebsitesScreenshot.
                        WebsitesScreenshot.ImageFormats.PNG;
                    this.FullPathImage = StaticClass.Path + this.FileName + StaticClass.Extension;
                    _Obj.SaveImage(FullPathImage);
                }
                Bitmap image = _Obj.GetImage();
                _Obj.Dispose();
                GC.Collect();
                return image;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
