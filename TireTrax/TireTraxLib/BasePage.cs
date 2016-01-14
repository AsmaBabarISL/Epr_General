using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;
using TireTraxLib.Security;
using System.Security.AccessControl;

namespace TireTraxLib
{
    public class BasePage : System.Web.UI.Page
    {
        public int pageId = 1;
        protected int pageSize = 25;
        public int totalRows;
        protected string rawUrl = string.Empty;
        private string redirectURL = string.Empty;
        private int loginmemberid;
        public bool canView = false;
        public bool canAdd = false;
        public bool canUpdate = false;
        public bool canDelete = false;
        protected UserInfo currentUserInfo = null;
        public int LanguageId
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["CultureCookie"] == null ||
                    HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"] == null ||
                    HttpContext.Current.Request.Cookies["CultureCookie"]["Culture"] == null)
                {
                    Utils.SetCulture("en-US", "en-US");
                }

                return Convert.ToInt32(Utils.GetLangaugeByCulture(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"]));
            }
        }
        public int LoginMemberId
        {
            set
            {

                loginmemberid = UserInfo.GetCurrentUserInfo().UserId;
            }
            get
            {

                return loginmemberid;
            }
        }
        public int UserOrganizationId
        {
            get
            {
                UserInfo User = UserInfo.GetCurrentUserInfo();
                return User.OrganizationId == null ? 0 : User.OrganizationId;
            }
        }
        public int UserOrganizationSubTypeId
        {
            get
            {
                UserInfo User = UserInfo.GetCurrentUserInfo();
                return User.UserOrganizationSubTypeId == null ? 0 : User.UserOrganizationSubTypeId;
            }
        }
        public int UserOrganizationRoleId
        {
            get
            {
                UserInfo User = UserInfo.GetCurrentUserInfo();
                return User.UserOrganizationRoleId == null ? 0 : User.UserOrganizationRoleId;
            }
        }

        public int CountryIDByLanguageId
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["CultureCookie"] == null ||
                    HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"] == null ||
                    HttpContext.Current.Request.Cookies["CultureCookie"]["Culture"] == null)
                {
                    Utils.SetCulture("en-US", "en-US");
                }

                return Utils.GetCountryIdByCulture(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"]);
            }
        }

      
        public string CatName { get; private set; }
        public int CatId { get; private set; }

        public void GetPermission(ResourceType resourceType, int memberId, ref bool canView, ref bool canAdd, ref bool canUpdate, ref bool canDelete)
        {

            UserInfo user = UserInfo.UserTopRole(memberId);

            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("securityToken");
            string securityToken = string.Empty;

            if (user != null)
            {
                securityToken = RoleManagement.GetSecurityTokenByRoleId(user.RoleId);

                PermissionManagement.GetPermission(resourceType, ref canView, ref canAdd, ref canUpdate, ref canDelete, securityToken);
            }
        }

        public void GetPermission(ResourceType resourceType, ref bool canView, ref bool canAdd, ref bool canUpdate, ref bool canDelete)
        {

            UserInfo user = UserInfo.GetCurrentUserInfo();


            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("securityToken");
            string securityToken = string.Empty;
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                //string[] GroupsArray =  user.UserGroupsCommaSeprated.Split(',');
                //int groupid = Convert.ToInt32(Convert.ToString(GroupsArray[0]));

                securityToken = RoleManagement.GetSecurityTokenByRoleId(user.RoleId);
                //StringBuilder sb = new StringBuilder(250);
                //sb.Append(user.RoleId);
                //sb.Append("|");
                //sb.Append(user.MemberId);
                //sb.Append("|");
                //sb.Append(securityToken);
                HttpCookie securityCookie = new HttpCookie("securityToken");
                //Decoder d= Encoding.UTF8.GetDecoder();

                ///securityCookie.Value = Encryption.Encrypt(securityToken);
                //////securityCookie.Value = Server.UrlEncode(sb.ToString());
                securityCookie.Value = Server.UrlEncode(securityToken);

                HttpContext.Current.Response.Cookies.Add(securityCookie);
            }
            else
            {

                //securityToken = Encryption.Decrypt(cookie.Value);
                securityToken = Server.UrlDecode(cookie.Value);
                //string cookieSecurityToken = string.Empty;
                //cookieSecurityToken = Server.UrlDecode(cookie.Value);
                //string[] cookieValue = GetInfoFromCookie(cookieSecurityToken);
                //if (cookieValue != null)
                //{

                //    if (user.MemberId == Convert.ToInt32(cookieValue[0]))
                //    { }
                //    if (user.RoleId == Convert.ToInt32(cookieValue[1]))
                //    { }
                //    securityToken = cookieValue[2].ToString();
                //}

            }



            PermissionManagement.GetPermission(resourceType, ref canView, ref canAdd, ref canUpdate, ref canDelete, securityToken);
        }

        public void GetPermission(ResourceType resourceType, ref bool canView, ref bool canAdd, ref bool canUpdate, ref bool canDelete, string groupid)
        {
            //HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("securityToken");
            string securityToken = string.Empty;
            // if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            // {
            securityToken = RoleManagement.GetSecurityToken(Convert.ToInt32(groupid));
            HttpCookie securityCookie = new HttpCookie("securityToken");
            securityCookie.Value = Server.UrlEncode(securityToken);
            HttpContext.Current.Response.Cookies.Add(securityCookie);
            // }
            // else
            // {
            //     securityToken = Server.UrlDecode(cookie.Value);
            // }
            //PermissionManagement.GetPermission(resourceType, ref canView, ref canAdd, ref canUpdate, ref canDelete, securityToken);
        }

        //protected override void InitializeCulture()
        //{

        //    if (HttpContext.Current.Request.Cookies["CultureCookie"] != null)
        //    {
        //        if (HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"] != null &&
        //            HttpContext.Current.Request.Cookies["CultureCookie"]["Culture"] != null)
        //        {
        //            Thread.CurrentThread.CurrentCulture =
        //                new CultureInfo(HttpContext.Current.Request.Cookies["CultureCookie"]["Culture"]);
        //            Thread.CurrentThread.CurrentUICulture =
        //                new CultureInfo(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"]);
        //        }
        //        else
        //        {
        //            Utils.SetCulture("en-US", "en-US");
        //        }
        //    }
        //    else
        //    {
        //        Utils.SetCulture("en-US", "en-US");
        //    }

        //    base.InitializeCulture();
        //}

        public BasePage()
        {

            currentUserInfo = new UserInfo(GetLogonUser());
            //if (currentUserInfo.UserId <= 0)
            //{
            //    HttpContext.Current.Response.Redirect("/default.aspx");
            //}
            LoginMemberId = currentUserInfo.UserId;
            InitializeCulture();
            checkcookie();
        }
        private void checkcookie()
        {
            string cookiename = "ClientRememberUserCookieTime";
            string domainUrlAdmin = System.Configuration.ConfigurationManager.AppSettings["DomainURLAdmin"];
            string domainUrlPublic = System.Configuration.ConfigurationManager.AppSettings["DomainURLPublic"];
            string url = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
            //   new SqlLog().InsertSqlLog(0, "AppURL:" + url.ToLower() + ",DomainURLFromWeb.config:" + domainUrlPublic.ToLower() + @"default.aspx", new Exception());
            if (url.Contains(domainUrlAdmin))
            {
                cookiename = "AdminRememberUserCookieTime";
                //    new SqlLog().InsertSqlLog(0, "Inside (if (url.Contains(domainUrlAdmin)))", new Exception());

            }
            if (!url.ToLower().Contains(domainUrlPublic.ToLower() + @"default.aspx") && (!url.Contains("login")) && (!url.Contains("registration")) && (!url.Contains("changepassword")) && (!url.Contains("forgotpassword")) && (!url.Contains("logout")))
            {
                try
                {
                    if (HttpContext.Current.Request.Cookies.Get(cookiename) != null)
                    {
                        HttpCookie c = HttpContext.Current.Request.Cookies.Get(cookiename);
                        if (((DateTime.Parse(c.Value)) < DateTime.Now))
                        {
                            if (url.Contains(domainUrlAdmin))
                            {
                                HttpContext.Current.Response.Redirect("~/Login/login.aspx");
                                //            new SqlLog().InsertSqlLog(0, "After IF invalid time Inside (if (url.Contains(domainUrlAdmin)))", new Exception());
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect("/");
                                //             new SqlLog().InsertSqlLog(0, "if invalid time Inside else of (if (url.Contains(domainUrlAdmin)))", new Exception());
                            }
                        }
                        else
                        {
                            // successfully stuff here.
                        }
                    }
                    else
                    {
                        if (url.Contains(domainUrlAdmin))
                        {
                            HttpContext.Current.Response.Redirect("~/Login/login.aspx");
                            //          new SqlLog().InsertSqlLog(0, " if cookie is null Inside (if (url.Contains(domainUrlAdmin)))", new Exception());
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("/");
                            //              new SqlLog().InsertSqlLog(0, "if cookie is null Inside else of(if (url.Contains(domainUrlAdmin)))", new Exception());
                        }
                    }
                }
                catch
                {
                    //         new SqlLog().InsertSqlLog(0, "Inside catch", new Exception());
                    if (url.Contains(domainUrlAdmin))
                    {
                        HttpContext.Current.Response.Redirect("~/Login/login.aspx");
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("/");
                    }
                }
            }
        }

        protected override void OnPreLoad(EventArgs e)
        {
            // Stop Caching in IE
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            // Stop Caching in Firefox
            Response.Cache.SetNoStore();
            base.OnPreLoad(e);
        }
        private string[] GetInfoFromCookie(string cookieValue)
        {
            try
            {
                string[] ckvalues = cookieValue.Split('|');
                return ckvalues;
            }
            catch
            {
                // cookie is corrupted, 
                // TODO: reload memberinfo 
                return null;
            }

        }
        public string LogonName
        {
            get
            {
                return currentUserInfo.Login;

            }
        }
        public string GetLogonUser()
        {
            string _loginName = "";

            if (HttpContext.Current.User.Identity.Name != null && HttpContext.Current.User.Identity.Name.Length > 0)
            {
                string cookievalue = (string)HttpContext.Current.User.Identity.Name;

                string[] ckvalues = Regex.Split(cookievalue, "_!_");
                if (ckvalues.Length > 3)
                    _loginName = Encryption.Decrypt(ckvalues[3]);
                if (ckvalues.Length > 10)
                    this.CatName = Encryption.Decrypt(ckvalues[10]);
                if (ckvalues.Length > 11)
                {
                    this.CatId = Conversion.ParseInt(Encryption.Decrypt(ckvalues[11]));
                   // Session["CatId"] = this.CatId;
                }
            }

            return _loginName;
        }
        protected int MaxPagesToShow = 5;
        private const string ViewState_CurrentPage = "ViewState_CurrentPage";
        private const string ViewState_CurrentPageR = "ViewState_CurrentPageR";
        private const string ViewState_CurrentPageR2 = "ViewState_CurrentPageR2";
        private const string ViewState_TotalItems = "ViewState_TotalItems";
        private const string ViewState_TotalItemsR = "ViewState_TotalItemsR";
        private const string ViewState_TotalItemsR2 = "ViewState_TotalItemsR2";
        public int CurrentPage
        {
            get
            {
                int currentPage = 1;
                if (ViewState[ViewState_CurrentPage] != null)
                {
                    currentPage = (int)ViewState[ViewState_CurrentPage];
                }
                return currentPage;
            }
            set
            {
                ViewState[ViewState_CurrentPage] = value;
            }
        }
        public int CurrentPageR
        {
            get
            {
                int currentPage = 1;
                if (ViewState[ViewState_CurrentPageR] != null)
                {
                    currentPage = (int)ViewState[ViewState_CurrentPageR];
                }
                return currentPage;
            }
            set
            {
                ViewState[ViewState_CurrentPageR] = value;
            }
        }
        public int CurrentPageR2
        {
            get
            {
                int currentPage = 1;
                if (ViewState[ViewState_CurrentPageR2] != null)
                {
                    currentPage = (int)ViewState[ViewState_CurrentPageR2];
                }
                return currentPage;
            }
            set
            {
                ViewState[ViewState_CurrentPageR2] = value;
            }
        }
        public int TotalItems
        {
            get
            {
                int totalItems = 0;
                if (ViewState[ViewState_TotalItems] != null)
                {
                    totalItems = (int)ViewState[ViewState_TotalItems];
                }
                return totalItems;
            }
            set
            {
                ViewState[ViewState_TotalItems] = value;
            }
        }
        public int TotalItemsR
        {
            get
            {
                int totalItems = 0;
                if (ViewState[ViewState_TotalItemsR] != null)
                {
                    totalItems = (int)ViewState[ViewState_TotalItemsR];
                }
                return totalItems;
            }
            set
            {
                ViewState[ViewState_TotalItemsR] = value;
            }
        }
        public int TotalItemsR2
        {
            get
            {
                int totalItems = 0;
                if (ViewState[ViewState_TotalItemsR2] != null)
                {
                    totalItems = (int)ViewState[ViewState_TotalItemsR2];
                }
                return totalItems;
            }
            set
            {
                ViewState[ViewState_TotalItemsR2] = value;
            }
        }

    }
}
