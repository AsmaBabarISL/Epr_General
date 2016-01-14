using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Web.Security;

public partial class Logout_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Member_logOut();

    }
    private void Member_logOut()
    {
        HttpContext.Current.Session.Abandon();
        //Session.Clear();
        SiteCookie.RemoveAll();
        FormsAuthentication.SignOut();
        HttpCookie c = new HttpCookie("securityToken");
        c.Value = null;
        c.Expires = DateTime.Now.AddHours(-1);
        HttpContext.Current.Response.Cookies.Add(c);
        HttpCookie oldCookie = new HttpCookie(".ASPXAUTH");
        oldCookie.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(oldCookie);

      
        HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
        cookie2.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(cookie2);
        cookie2 = new HttpCookie("ClientRememberUserCookieTime", DateTime.MinValue.ToString());
        cookie2.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(cookie2);

        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();

        Response.Redirect("/", false);

    }
}