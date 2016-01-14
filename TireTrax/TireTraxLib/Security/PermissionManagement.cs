using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.AccessControl;

namespace TireTraxLib.Security
{
    public class PermissionManagement
    {
        public static string UserDomain;
        #region Permission
        private int _permissionId;

        public int PermissionId
        {
            get { return _permissionId; }
            set { _permissionId = value; }
        }
        private Boolean _add;

        public Boolean Add
        {
            get { return _add; }
            set { _add = value; }
        }

        private Boolean _update;

        public Boolean Update
        {
            get { return _update; }
            set { _update = value; }
        }
        private Boolean _delete;

        public Boolean Delete
        {
            get { return _delete; }
            set { _delete = value; }
        }
        private Boolean _view;

        public Boolean View
        {
            get { return _view; }
            set { _view = value; }
        }


        #endregion

        #region Permission_Module

        private int _moduleId;

        public int ModuleId
        {
            get { return _moduleId; }
            set { _moduleId = value; }
        }


        #endregion

        #region Module
        private String _moduleName;

        public String ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }


        #endregion

        #region Role

        private String _roleName;

        public String RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }

        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        private Boolean _isActive;

        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private Boolean _isOrganization;

        public Boolean IsOrganization
        {
            get { return _isOrganization; }
            set { _isOrganization = value; }
        }

        #endregion

        #region User
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private int _organizationId;

        public int OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }
        private String _login;

        public String Login
        {
            get { return _login; }
            set { _login = value; }
        }
        private String _pwd;

        public String Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        private String _pwdSalt;

        public String PwdSalt
        {
            get { return _pwdSalt; }
            set { _pwdSalt = value; }
        }

        private String _tX_UserId;

        public String TX_UserId
        {
            get { return _tX_UserId; }
            set { _tX_UserId = value; }
        }
        private int _languageId;

        public int LanguageId
        {
            get { return _languageId; }
            set { _languageId = value; }
        }
        private int _timeZoneID;

        public int TimeZoneID
        {
            get { return _timeZoneID; }
            set { _timeZoneID = value; }
        }


        #endregion

        #region User_Role
        private int _roleId;

        public int RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }

        #endregion


        public static void GetAuthenticationTicket(UserInfo info, bool rememberMe)
        {
            FormsAuthentication.Initialize();
            StringBuilder sb = new StringBuilder(200);
            sb.Append(Encryption.Encrypt(info.UserId.ToString()));
            sb.Append("_!_");
            sb.Append(Encryption.Encrypt(info.FullName));
            //sb.Append("_!_");
            //sb.Append(Encryption.Encrypt(info.IsOrganization.ToString()));
            sb.Append("_!_");
            sb.Append(Encryption.Encrypt(info.Login.ToString()));
            HttpCookie ck;

            FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, sb.ToString(), DateTime.Now, DateTime.Now.AddDays(5), rememberMe, "");
            string cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            if (rememberMe)
                ck.Expires = tkt.Expiration;
            ck.Path = FormsAuthentication.FormsCookiePath;
            ck.Domain = SiteCookie.DomainCookie;//HttpContext.Current.Request.Url.Host;

            if (HttpContext.Current.Request.Url.Host.ToLower().Equals(UserDomain))
                ck.Domain = UserInfo.UserDomain;
            else if (HttpContext.Current.Request.Url.Host.ToLower().Equals("stage." + UserDomain))
                ck.Domain = "stage." + UserDomain;
            else
                ck.Domain = SiteCookie.DomainCookie;

            HttpContext.Current.Response.Cookies.Add(ck);
            SiteCookie.Update(SiteCookieName.RandomUserCrypId, System.Guid.NewGuid().ToString(), 30);
        }

        public static DataSet getPermissionInfo(int pageId, int pageSize, out int iTotalrows)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_Permissions_Info", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "PermissionManagment.getPermissionInfo", exp);
            }
            return ds;


        }
        public static void UpdatePermissionInfo(int permissionId, Boolean Add, Boolean Update, Boolean Delete, Boolean View)
        {


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPermissionId", SqlDbType.Int, 0, permissionId));
                    prams.Add(db.MakeInParam("@bitAdd", SqlDbType.Bit, 0, Add));
                    prams.Add(db.MakeInParam("@bitUpdate", SqlDbType.Bit, 0, Update));
                    prams.Add(db.MakeInParam("@bitDelete", SqlDbType.Bit, 0, Delete));
                    prams.Add(db.MakeInParam("@bitView", SqlDbType.Bit, 0, View));
                    db.RunProc("up_UpdatePermissions", prams.ToArray());



                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "PermissionManagment.UpdatePermissionInfo", exp);
            }



        }
        public static int AddModule(String ModuleAlias, String ModuleName)
        {
            int result = 5;

            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@moduleAlias", SqlDbType.VarChar, 75, ModuleAlias));
                    prams.Add(db.MakeInParam("@moduleName", SqlDbType.VarChar, 75, ModuleName));

                    result = db.RunProc("up_Module_addModule", prams.ToArray());



                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "PermissionManagment.AddMoudle", exp);
            }


            return result;
        }

        public static DataTable GetHeaderPermissions(int RoleId)
        {
            DataTable dt = null;
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    SqlParameter[] prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@intRoleId", SqlDbType.Int, 0, RoleId);
                    dt = db.GetDataSet("up_GetHeaderPermission", prams).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "PermissionManagement GetHeaderPermissions", ex);
                }
                return null;
            }
        }

        public static void GetPermission(ResourceType resourceType, ref bool canView, ref bool canAdd, ref bool canUpdate, ref bool canDelete, string accessToken)
        {
            int resourceId = (int)resourceType;
            AccessToken token = new AccessToken();

            // View Permission
            int viewResourceId = resourceId;
            canView = token.CheckRights(viewResourceId, accessToken);

            // Add Permission
            int addResourceId = resourceId + 2;
            canAdd = token.CheckRights(addResourceId, accessToken);

            // Updated Permission
            int updateResourceId = resourceId + 3;
            canUpdate = token.CheckRights(updateResourceId, accessToken);

            // Delete Permission
            int deleteResourceId = resourceId + 4;
            canDelete = token.CheckRights(deleteResourceId, accessToken);
        }
        public static void GetPermissionOnlyFalsed(int resourceid, ref bool canView, ref bool canAdd, ref bool canUpdate, ref bool canDelete, string accessToken)
        {
            int resourceId = (int)resourceid;
            AccessToken token = new AccessToken();

            // View Permission
            if (!canView)
            {
                int viewResourceId = resourceId;
                canView = token.CheckRights(viewResourceId, accessToken);
            }

            // Add Permission
            if (!canAdd)
            {
                int addResourceId = resourceId + 2;
                canAdd = token.CheckRights(addResourceId, accessToken);
            }
            // Updated Permission
            if (!canUpdate)
            {
                int updateResourceId = resourceId + 3;
                canUpdate = token.CheckRights(updateResourceId, accessToken);
            }
            if (!canDelete)
            {
                // Delete Permission
                int deleteResourceId = resourceId + 4;
                canDelete = token.CheckRights(deleteResourceId, accessToken);
            }
        }

    }
}
