using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Web.Security;

public partial class Login_Logout : System.Web.UI.Page
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

        HttpCookie oldCookie = new HttpCookie(".ASPXAUTH");
        oldCookie.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(oldCookie);

        //// clear authentication cookie
        HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
        cookie1.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(cookie1);

        // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
        HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
        cookie2.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(cookie2);
        cookie2 = new HttpCookie("AdminRememberUserCookieTime", DateTime.MinValue.ToString());
        cookie2.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(cookie2);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();

        Response.Redirect("adminLogin.aspx", false);

    }
}