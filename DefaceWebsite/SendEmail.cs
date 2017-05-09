using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DefaceWebsite
{
    class SendEmailClass
    {
        public static bool SendEmail(EmailInfo EmailInfo)
        {
            string error = "";
            try
            {

                string smtpAddress = "smtp.gmail.com";
                int portNumber = 25;
                bool enableSSL = true;
                //int.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["PortNumber"].ToString(), out portNumber);

               // bool.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["EnableSSL"].ToString(), out enableSSL);

                string emailFrom = "vqthieuuit@gmail.com";
                error = "emailFrom: " + emailFrom;
                string password = "Thieu)#)!()";//EncryptDataGS.AppSettings("Password");

                string displayName = "DefaceWebsite";//System.Web.Configuration.WebConfigurationManager.AppSettings["DisplayName"].ToString();
                error += "; displayName: " + displayName;
                string subject = EmailInfo.Subj;
                string body = EmailInfo.Message;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom, displayName);
                    if (EmailInfo.ToEmail != null)
                    {
                        error += "; Emtail to: ";
                        foreach (var item in EmailInfo.ToEmail)
                        {
                            mail.To.Add(item);
                            error += item + ";";
                        }
                    }
                    if (EmailInfo.CcEmail != null)
                    {
                        foreach (var item in EmailInfo.CcEmail)
                        {
                            mail.CC.Add(item);
                        }
                    }
                    if (EmailInfo.BCCEmail != null)
                    {
                        foreach (var item in EmailInfo.BCCEmail)
                        {
                            mail.Bcc.Add(item);
                        }
                    }
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    //media type that is respective of the data attach file
                    int i = 0;
                    if (EmailInfo.isAttach)
                        mail.Attachments.Add(new Attachment(EmailInfo.dataAttach, EmailInfo.nameAttach, "text/plain"));
                    if (EmailInfo.dataMultiAttachs != null)
                    {
                        if (EmailInfo.dataMultiAttachs.isMulti)
                        {
                            foreach (MemoryStream item in EmailInfo.dataMultiAttachs.dataAttachs)
                            {
                                mail.Attachments.Add(new Attachment(item, EmailInfo.dataMultiAttachs.names[i], "text/plain"));
                                i++;
                            }
                        }

                    }
                    // Can set to false, if you are sending pure text.
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(emailFrom, password);
                        //smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = enableSSL;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        error += " smtpAddress: " + smtpAddress + "; portNumber" + portNumber.ToString();
                        smtp.Send(mail);
                        //smtp.Send(emailFrom, "thieuvq@gsoft.com.vn", subject, body);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {                
                return false;
            }
        }

        public class EmailInfo
        {
            public List<string> ToEmail { get; set; }

            public List<string> CcEmail { get; set; }
            public List<string> BCCEmail { get; set; }
            public string Subj { get; set; }
            public string Message { get; set; }
            public MemoryStream dataAttach { get; set; }
            public string nameAttach { get; set; }
            public bool isAttach { get; set; }
            public AttachFile dataMultiAttachs { get; set; }
        }
        public class AttachFile
        {
            public AttachFile()
            {
                this.names = new List<string>();
                this.dataAttachs = new List<MemoryStream>();
            }
            public List<string> names { get; set; }
            public bool isMulti { get; set; }
            public List<MemoryStream> dataAttachs { get; set; }
        }
    }
}
