
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace TireTraxLib
{
    public class Emails
    {

        private string _to;
        public string To
        {
            get { return _to; }
            set { _to = value; }
        }
        private string _from;
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }
        private string _cc;
        public string CC
        {
            get { return _cc; }
            set { _cc = value; }
        }
        private string _bcc;
        public string BCC
        {
            get { return _bcc; }
            set { _bcc = value; }
        }
        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        private string _url;
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }





        public enum EmailType
        {
            RegistrationSubmissionEmail,
            ApplicationApprovedEmail,
            ForgetPassordEmail

        }

        private const string RegistrationSubmissionEmail = @"<div style='font-size:11px; font-family:Verdana,Arial,Helvetica,sans-serif;'>
                       <p>Dear Concern,</p><p> Your request is under review.</p>                    
                       </div>";

        private const string ApplicationApprovedEmail = @"<div style='font-size:11px; font-family:Verdana,Arial,Helvetica,sans-serif;'>
                       <p>Dear Concern,</p><p> Your request have been approved click on following URL to continue your application.</p> <br />
                       <a target='_blank' href='[URL]'>Click</a>                    
                       </div>";

        private const string ForgetPassordEmail = @"<div style='font-size:11px; font-family:Verdana,Arial,Helvetica,sans-serif;'>
                       <p>Dear Concern,</p><p> You can set your new password by clicking on following URL.</p> <br />
                       <a target='_blank' href='[URL]'>Click</a>                    
                       </div>";


        public Emails()
        {
        }

        public static bool SendEmail(Emails email, string type)
        {
            MailMessage mailMsg = new MailMessage();
            SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);
            smtpClient.Port = 587;
            //smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"].ToString(), ConfigurationManager.AppSettings["smtpPw"].ToString());
            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            //smtpClient.Credentials = new System.Net.NetworkCredential("malikrizwan4202@gmail.com", "malik420");
            //smtpClient.EnableSsl = false;
            try
            {

                string WelcomeNote = string.Empty;
                string EmailText = string.Empty;
                mailMsg.From = new MailAddress(email.From, email.From);
                mailMsg.To.Add(new MailAddress(email.To, email.To));
                mailMsg.Subject = email.Subject;
                //mailMsg.CC.Add(new MailAddress(email.CC, email.CC));
                //mailMsg.Bcc.Add(new MailAddress(email.BCC, email.BCC));

                switch (type)
                {
                    case "RegistrationSubmissionEmail":
                        {
                            mailMsg.Subject = "EPRTS Registration Submission Email.";
                            EmailText = RegistrationSubmissionEmail.ToString();
                        }
                        break;
                    case "ApplicationApprovedEmail":
                        {
                            EmailText = ApplicationApprovedEmail.ToString();
                            EmailText = EmailText.Replace("[URL]", email.URL.ToString());
                        }
                        break;
                    case "ForgetPassordEmail":
                        {
                            EmailText = ForgetPassordEmail.ToString();
                            EmailText = EmailText.Replace("[URL]", email.URL.ToString());
                        }
                        break;
                }
                mailMsg.Body = EmailText;
                mailMsg.IsBodyHtml = true;
                smtpClient.Send(mailMsg);
            }
            catch (Exception ex)
            {
                SmtpException smtpEx = new SmtpException(ex.ToString());
                new SqlLog().InsertSqlLog(0, "Emails.cs  SendEmail", ex);
                return false;
            }
            finally
            {
                if (smtpClient != null)
                    smtpClient = null;
                if (mailMsg != null)
                    mailMsg = null;
            }
            return true;

        }


    }
}
