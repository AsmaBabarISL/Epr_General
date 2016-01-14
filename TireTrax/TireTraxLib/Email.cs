using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;
using TireTraxLib;
using System.Collections.Generic;
using System.Data.SqlClient;
/// <summary>
//Created By Abaid on 06 Aug 
/// </summary>
public class Email
{
    private SqlLog sqllog = null;

    public Email()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Email(string strEmailFrom, string strEmailTo, string strEmailSubject,
     string strEmailMessageBody, string FileName = "", string strEmailBcc = "")
    {
        _strEmailFrom = strEmailFrom;
        _strEmailTo = strEmailTo;
        _strEmailBcc = strEmailBcc;
        _strEmailSubject = strEmailSubject;
        _strEmailMessageBody = strEmailMessageBody;
        _strSmtpServer = ConfigurationManager.AppSettings.Get("smtpServer");
        _strFileName = FileName;
        SendEmail();
    }
    private string _strSmtpServer = "";
    private string _strEmailFrom = "";
    private string _strEmailTo = "";
    private string _strEmailCc = "";
    private string _strEmailBcc = "";
    private string _strEmailSubject = "";
    private string _strEmailMessageBody = "";
    private string _strFileName = "";


    public void SendEmail()
    {
        if ((_strSmtpServer != "") && (_strSmtpServer != null))
        {
            try
            {
                
                MailMessage _objMail = new MailMessage(_strEmailFrom, _strEmailTo);

                _objMail.Subject = _strEmailSubject;
                _objMail.Body = _strEmailMessageBody;
                _objMail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["smtpServer"].ToString()]);
                smtpClient.Host = ConfigurationManager.AppSettings["smtpServer"].ToString();
                smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"].ToString(), ConfigurationManager.AppSettings["smtpPw"].ToString());
                if (!string.IsNullOrEmpty(_strFileName))
                {
                    Attachment item = new Attachment(_strFileName);
                    _objMail.Attachments.Add(item);
                }
                smtpClient.Send(_objMail);
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Email.cs", ex);
            }
        }
    }


    //public static bool CheckEmail(string email)
    //{
    //    bool exists = false;
    //    List<SqlParameter> prm = new List<SqlParameter>();
    //    try
    //    {
    //        using (DbManager DB = DbManager.GetDbManager())
    //        {

    //            prm.Add(DB.MakeInParam("@email", SqlDbType.VarChar , 50, email));
    //            if (DB.RunProc("Up_CheckEmail", prm.ToArray()) == 1)
    //                exists = true;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        new SqlLog().InsertSqlLog(0, "Email.cs", ex);
    //    }
    //    return exists;
    //}

}
