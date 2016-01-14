using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using TireTraxLib;
using TireTraxLib.Security;

public partial class Login_login : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "EPR Technology Solutions > Login";
        lbl_ErrorMsg.Visible = false;
        if (Request["status"] != null)
            if (Int32.Parse(Request["status"]) == 1)
                lbl_ErrorMsg.Text = "Your password has been sent to the email provided.";
            else
                lbl_ErrorMsg.Text = string.Empty;


        if (!IsPostBack)
        {

            if (Request.Cookies["AdminRememberUserCookie"] != null && Request.Cookies["AdminRememberUserCookie"].Value != "")
            {
                txtusername.Text = Request.Cookies["AdminRememberUserCookie"].Value;
                chkBoxRemember.Checked = true;
            }
        }
    }




    private void Member_login(string username, string password)
    {
        try
        {

            HttpContext.Current.Request.Cookies.Remove(System.Web.Security.FormsAuthentication.FormsCookieName);
            UserInfo member = UserInfo.AuthenticateAdminMember(username, Encryption.Encrypt(password));
            if (member != null)
            {
                UserInfo.GetAuthenticationTicket(member, chkBoxRemember.Checked);

                if (chkBoxRemember.Checked)
                {
                    HttpCookie cookie = new HttpCookie("AdminRememberUserCookie", username);
                    cookie.Expires = DateTime.Now.AddDays(10);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    
                }
                else
                {
                    if (Request.Cookies["AdminRememberUserCookie"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("AdminRememberUserCookie");
                        myCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(myCookie);
                    }
                    

                }
                Response.Redirect("/Dashboard/adminDashboard.aspx", false);

                
               
            }
            else
            {
                lbl_ErrorMsg.Visible = true;
                lbl_ErrorMsg.CssClass = "custom-error";
                lbl_ErrorMsg.Text = "* Invalid Username/Password";
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().Contains("A network-related or instance-specific"))
            {
                lbl_ErrorMsg.Visible = true;
                lbl_ErrorMsg.CssClass = "custom-error";
                lbl_ErrorMsg.Text = "* Error in SQL Server connection";
            }
            else
                new SqlLog().InsertSqlLog(0, "Login.aspx.Member_login", ex);
        }
    }

    protected void btnAdminLogin_Click(object sender, EventArgs e)
    {

        // Member_login(txtusername.Text.Trim(), txtpassword.Text.Trim());
        Member_login(Utils.CleanHTML(txtusername.Text.Trim()), txtpassword.Text.Trim());
        //HttpCookie cookie1 = new HttpCookie("AdminRememberUserCookie", );
        //cookie1.Expires = DateTime.Now.AddDays(1);
        //HttpContext.Current.Response.Cookies.Add(cookie1);

        string loginTime = "";
        if (chkBoxRemember.Checked)
        {
            loginTime = System.Configuration.ConfigurationManager.AppSettings["LoginTimeRemember"];
            UserInfo user = UserInfo.GetCurrentUserInfo();
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("securityToken");
            string securityToken = string.Empty;
            securityToken = RoleManagement.GetSecurityTokenByRoleId(user.RoleId);
            HttpCookie securityCookie = new HttpCookie("securityToken");
            securityCookie.Value = Server.UrlEncode(securityToken);
            securityCookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt16(loginTime));
            HttpContext.Current.Response.Cookies.Add(securityCookie);
            cookie = new HttpCookie("AdminRememberUserCookieTime");
            cookie.Value = DateTime.Now.AddMinutes(Convert.ToInt16(loginTime)).ToString();
            cookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt16(loginTime));
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        else
        {
      //      new SqlLog().InsertSqlLog(0, "before getting key of logintTime from web.conf", new Exception());
            loginTime = System.Configuration.ConfigurationManager.AppSettings["LoginTime"];
     //       new SqlLog().InsertSqlLog(0, "after  getting key of logintTime from web.conf", new Exception());
            UserInfo user = UserInfo.GetCurrentUserInfo();
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("securityToken");
            string securityToken = string.Empty;
            securityToken = RoleManagement.GetSecurityTokenByRoleId(user.RoleId);
            HttpCookie securityCookie = new HttpCookie("securityToken");
            securityCookie.Value = Server.UrlEncode(securityToken);
       //     new SqlLog().InsertSqlLog(0, "after adding security token", new Exception());
            securityCookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt16(loginTime));
            HttpContext.Current.Response.Cookies.Add(securityCookie);
            cookie = new HttpCookie("AdminRememberUserCookieTime");
            cookie.Value = DateTime.Now.AddMinutes(Convert.ToInt16(loginTime)).ToString();
            cookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt16(loginTime));
            HttpContext.Current.Response.Cookies.Add(cookie);
       //     new SqlLog().InsertSqlLog(0, "after adding AdminRememberUserCookieTime", new Exception());

        }
       // Response.Redirect("/Dashboard/adminDashboard.aspx",true);
    }
}