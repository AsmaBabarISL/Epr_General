using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Threading;
using System.Configuration;

public partial class Login_ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //var SSID = Request.QueryString["SSID"];
        if (!IsPostBack)
        {
          

            //Utils.GetLookUpData<DropDownList>(ref ddlCountry, LookUps.Country);
           

        }
        else
        {




            lblStatus.Visible = false;
            lblError.Visible = false;

        }
    }

    private void LoadInfo()
    { 
        try
        {
            int SSID = Conversion.ParseInt( Request.QueryString["SSID"] );
            int orgId = OrganizationInfo.getOrganizationIdByEmail(eMail.Text.Trim(), SSID);
            if (orgId > 0)
            {
                Emails email = new Emails();
                email.To = eMail.Text.Trim();
                string userId = new UserInfo(eMail.Text.Trim()).UserId.ToString();
                email.URL = ConfigurationManager.AppSettings["EmailUrl"].ToString() + "ChangePassword.aspx?userId=" + Encryption.Encrypt(new UserInfo(eMail.Text.Trim()).UserId.ToString());
                email.From = "noreply@eprts.com";
                email.Subject = "EPRTS Forget Password Email";
                //Thread Email_Thread = new Thread(() => SendEmails(email, Emails.EmailType.ForgetPassordEmail.ToString()));
                //Email_Thread.Start();
                SendEmails(email, Emails.EmailType.ForgetPassordEmail.ToString());
                dvthankyou.Visible = true;
                dvforgotpassword.Visible = false;
                eMail.Visible = false;
                submit.Visible = false;

                //lblTitleAddress.Visible = false;
                // Response.Redirect("/", false);
            }
            else
            {
                lblError.Text = "Email not matched with primary email.";
                lblError.Visible = true;
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ForgotPassword.aspx LoadInfo", ex);
        }
    }

    //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlStewardship.Items.Clear();
    //    if (ddlCountry.SelectedValue != "0")
    //    {
    //        // DataSet ds = OrganizationInfo.GetStewardshipByCountryID(Convert.ToInt32(ddlCountry.SelectedValue));

    //        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        //{
    //        //    foreach (DataRow dr in ds.Tables[0].Rows)
    //        //    {
    //        //        ListItem item = new ListItem();
    //        //        item.Value = dr["OrganizationId"].ToString() + "-" + dr["State"].ToString();
    //        //        item.Text = dr["StateName"].ToString();
    //        //        ddlStewardship.Items.Add(item);
    //        //    }
    //        //}
    //        Utils.GetLookUpData<DropDownList>(ref ddlStewardship, LookUps.StewardshipTypes, Convert.ToInt32(ddlCountry.SelectedValue));
    //        //if (ddlStewardship.Items.Count > 1)
    //        //    ddlStewardshipRequired.Enabled = true;
    //        //else
    //        //    ddlStewardshipRequired.Enabled = false;
    //    }
    //    //  ddlStewardship.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));
    //}

   


    private void SendEmails(Emails email, string type)
    {
        try
        {
            Emails.SendEmail(email, type);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ForgotPassword.aspx SendEmails", ex);
        }

    }
    protected void submit_Click(object sender, EventArgs e)
    {
        LoadInfo();
    }
    protected void lnkBtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/", false);
    
    }



   
}

