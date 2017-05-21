using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DefaceWebsite
{
    public class ItemCombo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public static class StaticClass
    {
        //public static bool isAutoCheckingTimerSet = false;
        //Che do chay tu dong co bat khong
        public static bool isAutoMode = false;
        //Co Auto Process nao dang chay khong
        public static bool isAutoRunning = false;
        public static int countProcess = 0;
        public static string formatDate = "dd/MM/yyyy HH:mm:ss";
        public static string formatDateSQL = "yyyy-MM-dd HH:mm:ss";
        public static string formatShortDate = "dd/MM/yyyy";
        public static string formatTime = @"hh\:mm\:ss";
        public static string Path = System.IO.Directory.GetCurrentDirectory() + @"\ImageCapture\";
        public static string PathDif = System.IO.Directory.GetCurrentDirectory() + @"\ImageDif\";
        public static string Extension = ".png";
        public static string GetDomainName(string domain)
        {
            string res = domain.Replace("http://", "");
            res = res.Replace("https://", "");
            res = res.Replace("www.", "");

            res = res.Split('/')[0];

            return res;
        }

        public static string ApplicationID = "AAAAIO5wi_s:APA91bGi-8LbgTtxtOHsoanvbESmlUa1CtEOEEc9TiFsunY_1YHMyHdcdazoSO4Xu-5-lNtoDhEyq_iqKktzrA2StnoX4apokNIaWmcbfJbKieo33lyx6UUGTnri7DyOC9nLfRGFK7ty";
        public static string SenderId = "141439306747";

        public static int LimitLink = 30;

        public static string FullPathImage = "";
        public static Bitmap CaptureImage(string domain)
        {
            try
            {
                string filename = Guid.NewGuid().ToString();//StaticClass.GetDomainName(domain);
                
                WebsitesScreenshot.WebsitesScreenshot _Obj;
                _Obj = new WebsitesScreenshot.WebsitesScreenshot();
                WebsitesScreenshot.WebsitesScreenshot.Result _Result;
                _Result = _Obj.CaptureWebpage(domain);
                if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                {
                    _Obj.ImageFormat = WebsitesScreenshot.
                        WebsitesScreenshot.ImageFormats.PNG;
                    StaticClass.FullPathImage = StaticClass.Path + filename + StaticClass.Extension;
                    _Obj.SaveImage(FullPathImage);
                }
                Bitmap image = _Obj.GetImage();
                _Obj.Dispose();

                return image;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
    public static class DomainType
    {
        public const string Image = "CCON";//"IMAGE";
        public const string Ip = "CIP";//"IP";
        public const string Timeout = "TOUT";//"TIMEOUT";
        public const string Redirect = "RDOM";//"REDIRECT";
        public const string Resource = "ECODE";//"RESOURCE";
        public const string Ok = "OK";   
    }

    public class SendResult
    {
        public bool Status { get; set; }

        public string Message { get; set; }
    }

    public class CheckResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Values { get; set; }
    }
}
