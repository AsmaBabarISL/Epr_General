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
using System;
using TireTraxLib.Security;

namespace TireTraxLib
{
    /// <summary>
    /// Summary description for BaseControl
    /// </summary>
    public class BaseControl : System.Web.UI.UserControl
    {
        public int pageId = 1;
        protected int pageSize = 10;
        public int totalRows;
        protected string rawUrl = string.Empty;
        private string redirectURL = string.Empty;
        private int loginmemberid;
        public bool canView = false;
        public bool canAdd = false;
        public bool canUpdate = false;
        public bool canDelete = false;
        protected UserInfo currentUserInfo = null;


        public string CatName { get; private set; }
        public int CatId { get; private set; }

        

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

        public void GetPermission(ResourceType resourceType, int memberId, ref bool canView, ref bool canAdd, ref bool canUpdate, ref bool canDelete)
        {

            UserInfo user = UserInfo.UserTopRole(memberId);

            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("securityToken");
            string securityToken = string.Empty;

            if (user != null)
            {
                securityToken = RoleManagement.GetSecurityTokenByRoleId(user.RoleId);
                //DataSet ds= RoleManagement.GetSecurityTokenByGroupIds(user.UserGroupsCommaSeprated);
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow row in ds.Tables[0].Rows)
                //    {
                //securityToken = Conversion.ParseDBNullString(row["vchSecurityCode"]);
                PermissionManagement.GetPermission(resourceType, ref canView, ref canAdd, ref canUpdate, ref canDelete, securityToken);
                //    }
                //}
            }
        }

        public void GetPermission(ResourceType resourceType, ref bool canView, ref bool canAdd, ref bool canUpdate, ref bool canDelete)
        {

            UserInfo user = UserInfo.GetCurrentUserInfo();


            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("securityToken");
            string securityToken = string.Empty;
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {


                securityToken = RoleManagement.GetSecurityTokenByRoleId(user.RoleId);// (Convert.ToInt32(user.UserGroupsCommaSeprated));
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
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("securityToken");
            string securityToken = string.Empty;
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                securityToken = RoleManagement.GetSecurityToken(Convert.ToInt32(groupid));
                HttpCookie securityCookie = new HttpCookie("securityToken");
                securityCookie.Value = Server.UrlEncode(securityToken);
                HttpContext.Current.Response.Cookies.Add(securityCookie);
            }
            else
            {
                securityToken = Server.UrlDecode(cookie.Value);
            }
            PermissionManagement.GetPermission(resourceType, ref canView, ref canAdd, ref canUpdate, ref canDelete, securityToken);
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

        public BaseControl()
        {

            currentUserInfo = new UserInfo(GetLogonUser());
            //if (currentUserInfo.UserId <= 0)
            //{
            //    HttpContext.Current.Response.Redirect("/default.aspx");
            //}
            LoginMemberId = currentUserInfo.UserId;
            //InitializeCulture();
        }
        //protected override void OnPreLoad(EventArgs e)
        //{
        //    // Stop Caching in IE
        //    Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

        //    // Stop Caching in Firefox
        //    Response.Cache.SetNoStore();
        //    base.OnPreLoad(e);
        //}
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
        private const string ViewState_TotalItems = "ViewState_TotalItems";
        private const string ViewState_TotalItemsR = "ViewState_TotalItemsR";



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
    }
}
