using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Text;
using TireTraxLib.Security;
using System.Data;

public partial class Login_login : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["SSID"]))
            {
                DataSet ds = OrganizationInfo.GetStateCodeByStateId(Conversion.ParseInt(Request.QueryString["SSID"]));
                lblState.Text = ds.Tables[0].Rows[0][1].ToString();
            }
            string[] Urls = Request.Url.AbsolutePath.Split('/');

            if (Urls.Length < 3)
            {
                Response.Redirect("/registration");
            }

            if (Request.Cookies["ClientRememberUserCookie"] != null && Request.Cookies["ClientRememberUserCookie"].Value != "")
            {
                string login = Utils.CleanStringForSpecialCharacters(txtLogin.Text.Trim());
                login = Request.Cookies["ClientRememberUserCookie"].Value;
                chkremember.Checked = true;
            }
            //if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"]))
            //{
                
            //    Utils.SetCulture(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"].ToString(), HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"].ToString());
            //}
        }
        if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"]))
        {
            Utils.SetCulture(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"].ToString(), HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"].ToString());
        }
    }
    protected void registerNow_Click(object sender, EventArgs e)
    {
        int StewardShipID = Convert.ToInt32(Request.QueryString["SSID"]);
        string CountryCode = "";
        string StateCode = "";

        string[] Urls = Request.Url.AbsolutePath.Split('/');

        if (Urls.Length == 3)
        {
            CountryCode = Urls[1];
        }
        else if (Urls.Length == 4)
        {
            CountryCode = Urls[1];
            StateCode = Urls[2];
        }

        if (CountryCode == "ca")
        {
            if (StewardShipID <= 0)
            {
                Response.Redirect("/ca/registrationform");
            }
            else
            {
                Response.Redirect(String.Format("/ca/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
            }
        }
        else if (CountryCode == "mx")
        {
            if (StewardShipID <= 0)
            {
                Response.Redirect("/mx/registrationform");
            }
            else
            {
                Response.Redirect(String.Format("/mx/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
            }
        }
        else if (CountryCode == "us")
        {
            if (StewardShipID <= 0)
            {
                Response.Redirect("/us/registrationform");
            }
            else
            {
                Response.Redirect(String.Format("/us/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
            }
        }
        else if (CountryCode == "ja")
        {
            if (StewardShipID <= 0)
            {
                Response.Redirect("/ja/registrationform");
            }
            else
            {
                Response.Redirect(String.Format("/ja/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
            }
        }
        else if (CountryCode == "sk")
        {
            if (StewardShipID <= 0)
            {
                Response.Redirect("/sk/registrationform");
            }
            else
            {
                Response.Redirect(String.Format("/sk/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
            }
        }
        else if (CountryCode == "cn")
        {
            if (StewardShipID <= 0)
            {
                Response.Redirect("/cn/registrationform");
            }
            else
            {
                Response.Redirect(String.Format("/cn/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
            }
        }
        else if (CountryCode == "au")
        {
            if (StewardShipID <= 0)
            {
                Response.Redirect("/au/registrationform");
            }
            else
            {
                Response.Redirect(String.Format("/au/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
            }
        }
        else
        {
            Response.Redirect("/registration");
        }
    }
    protected void btnAdminDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://localhost:62325/adminLogin.aspx", false);
        //http://localhost:62325/adminDashboard.aspx
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string decryptedPassword = Encryption.Decrypt("v5s0O4YpWkATU/WOBnf0qw==");
            // CreateSideMap();
             HttpContext.Current.Request.Cookies.Remove(System.Web.Security.FormsAuthentication.FormsCookieName);
            string pwd = Utils.CleanHTML(txtPassword.Text.Trim());

            string login = Utils.CleanStringForSpecialCharacters(txtLogin.Text.Trim());
            UserInfo member = UserInfo.AuthenticateMember(login, Encryption.Encrypt(pwd), Convert.ToInt32(Request.QueryString["SSID"]));
            if (member != null)
            {
                if (!UserInfo.CheckUserCountryState(member.UserId, Convert.ToInt32(Request.QueryString["SSID"])))
                {
                    lblError.Text = "* Select correct State or enter valid credentials";
                    return;
                }
                else
                {
                    Session["member"] = member;
                    rblProducts.DataSource = OrganizationInfo.GetProductCategoryByOrgId(member.OrganizationId);
                    rblProducts.DataBind();
                    dvProducts.Visible = true;
                    
                }
                
            }
            else
            {
                lblError.Text = "* Invalid Username/Password";
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().Contains("A network-related or instance-specific"))
            {
                lblError.Text = "* Error in SQL Server connection";
            }
            else
                new SqlLog().InsertSqlLog(0, "Default.aspx.Member_login", ex);
        }
    }

    
    private void CreateSideMap()
    {
        string[] Urls = Request.Url.AbsolutePath.Split('/');
        StringBuilder sitemap = new StringBuilder(500);
        sitemap.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
        sitemap.Append("<siteMap xmlns=\"http://schemas.microsoft.com/AspNet/SiteMap-File-1.0\" enableLocalization=\"true\">\r\n");
        sitemap.Append(" <siteMapNode url=\"" + Urls[1] + "/" + Urls[2] + "/dashboard\" title=\"$resources:SiteMapLocalizations,Home\" > \r\n");
        sitemap.Append(" <siteMapNode url=\"" + Urls[1] + "/" + Urls[2] + "/Stakeholders\" title=\"$resources:SiteMapLocalizations,Stakeholders\" /> \r\n");
        sitemap.Append(" <siteMapNode url=\"" + Urls[1] + "/" + Urls[2] + "/Applications\" title=\"$resources:SiteMapLocalizations,Applications\" /> \r\n");
        sitemap.Append(" <siteMapNode url=\"" + Urls[1] + "/" + Urls[2] + "/Inventory\" title=\"$resources:SiteMapLocalizations,Inventory\" /> \r\n");
        sitemap.Append(" <siteMapNode url=\"" + Urls[1] + "/" + Urls[2] + "/Revenue\" title=\"$resources:SiteMapLocalizations,Revenue\" /> \r\n");
        sitemap.Append(" <siteMapNode url=\"" + Urls[1] + "/" + Urls[2] + "/Reports\" title=\"$resources:SiteMapLocalizations,Reports\" /> \r\n");
        sitemap.Append(" <siteMapNode url=\"" + Urls[1] + "/" + Urls[2] + "/Users\" title=\"$resources:SiteMapLocalizations,Users\" /> \r\n");
        sitemap.Append(" <siteMapNode url=\"" + Urls[1] + "/" + Urls[2] + "/Settings\" title=\"$resources:SiteMapLocalizations,Settings\" /> \r\n");
        sitemap.Append(" <siteMapNode url=\"" + Urls[1] + "/" + Urls[2] + "/logosetting\" title=\"$resources:SiteMapLocalizations,logosetting\" /> \r\n");
        sitemap.Append(" <siteMapNode url=\"" + Urls[1] + "/" + Urls[2] + "/invoices\" title=\"$resources:SiteMapLocalizations,invoices\" /> \r\n");
        sitemap.Append(" </siteMapNode></siteMap>\r\n");
        System.IO.File.WriteAllText(MapPath(".") + "/Web.sitemap", sitemap.ToString());
    }
    protected void lnkContinue_Click(object sender, EventArgs e)
    {
        if (rblProducts.SelectedItem != null)
        {
            UserInfo member = (UserInfo)Session["member"];
            string CatName = rblProducts.SelectedItem.Text.Trim();
            string CatId = rblProducts.SelectedItem.Value;
            string login = txtLogin.Text.Trim();
            SetCookies(login, member, CatId, CatName);
        }
       
    }

    private void SetCookies(string login, UserInfo member,string PCategoryId,string PCatName)
    {
        UserInfo.GetAuthenticationTicket(member, chkremember.Checked,PCategoryId,PCatName);
        HttpCookie cookie1 = new HttpCookie("ClientRememberUserCookie", login);
        cookie1.Expires = DateTime.Now.AddDays(1);
        HttpContext.Current.Response.Cookies.Add(cookie1);
        string loginTime = null;
        if (chkremember.Checked)
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
            cookie = new HttpCookie("ClientRememberUserCookieTime");
            cookie.Value = DateTime.Now.AddMinutes(Convert.ToInt16(loginTime)).ToString();
            cookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt16(loginTime));
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        else
        {
            loginTime = System.Configuration.ConfigurationManager.AppSettings["LoginTime"];
            UserInfo user = UserInfo.GetCurrentUserInfo();
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("securityToken");
            string securityToken = string.Empty;
            securityToken = RoleManagement.GetSecurityTokenByRoleId(user.RoleId);
            HttpCookie securityCookie = new HttpCookie("securityToken");
            securityCookie.Value = Server.UrlEncode(securityToken);
            securityCookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt16(loginTime));
            securityCookie.Expires = DateTime.Now.AddMinutes(30);
            HttpContext.Current.Response.Cookies.Add(securityCookie);
            HttpCookie myCookie = new HttpCookie("ClientRememberUserCookieTime", DateTime.Now.AddMinutes(Convert.ToInt16(loginTime)).ToString());
            myCookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt16(loginTime));
            Response.Cookies.Add(myCookie);

        }

        System.Data.DataSet ds = OrganizationInfo.GetCountryAndStateCodeByOrganizationId(member.OrganizationId);
        string url = string.Empty;
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
                url = "/" + ds.Tables[0].Rows[0][0].ToString().ToLower() + "/" + ds.Tables[0].Rows[0][1].ToString().ToLower() + "/dashboard";
            else
                url = "/dashboard";
        }
        ds.Dispose();
        ds = null;

        Response.Redirect(url, false);
    }
}