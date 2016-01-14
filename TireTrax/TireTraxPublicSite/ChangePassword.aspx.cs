using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class Registration_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInfo();
            lblStatus.Visible = false;
        }
    }

    private void LoadInfo()
    {
        try
        {
            if (Request.QueryString["userId"] != null && Request.QueryString["userId"] != "")
            {
                UserInfo user = new UserInfo(Conversion.ParseDBNullInt(Encryption.Decrypt(Request.QueryString["userId"])));
                
                //OrganizationInfo org=new OrganizationInfo(user.OrganizationId);
                //txtUserName.Text=new ContactInfo(org.ContactId).Email;
                txtUserName.Text = user.Login;
                txtUserName.ReadOnly = true;
                ViewState["UserId"] = Encryption.Decrypt(Request.QueryString["userId"]);

            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ChangePassword  LoadInfo", ex);
        }
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        try
        {
            UserInfo objUserInfo = new UserInfo(Conversion.ParseDBNullInt(ViewState["UserId"]));
            objUserInfo.Pwd = Encryption.Encrypt(txtRePwd.Text.Trim());
            objUserInfo.IsApproved = true;
            objUserInfo.DateCreated = DateTime.Now;
            UserInfo.UpdateUserInfo(objUserInfo);
            
           
            dvthankyou.Visible = true;
            dvChangePassword.Visible = false;
           // Response.Redirect("/Default.aspx");
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ChangePassword  btnContinue_Click", ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/", false);
    }
    
    }