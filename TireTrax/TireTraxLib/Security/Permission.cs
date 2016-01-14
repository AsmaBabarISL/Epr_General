using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TireTraxLib.Security
{
    public class Permission
    {
        private int _permissionId;

        public int PermissionId
        {
            get { return _permissionId; }
            set { _permissionId = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _path;

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private int _lookupTypeid;

        public int LookUpTypeId
        {
            get { return _lookupTypeid; }
            set { _lookupTypeid = value; }
        }



        private bool _isMainNav;

        public bool IsMainNav
        {
            get { return _isMainNav; }
            set { _isMainNav = value; }
        }

        private int _intOrderBy;

        public int IntOrderBy
        {
            get { return _intOrderBy; }
            set { _intOrderBy = value; }
        }

        private string _moduleName;

        public string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        private bool _active;

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        private bool _isDefaultUrl;

        public bool IsDefaultUrl
        {
            get { return _isDefaultUrl; }
            set { _isDefaultUrl = value; }
        }
        private bool _bitadmin;

        public bool AdminSide
        {
            get { return _bitadmin; }
            set { _bitadmin = value; }
        }

        private int _intparentId;

        public int ParentId
        {
            get { return _intparentId; }
            set { _intparentId = value; }
        }

        private void Load(int permissionId)
        {
            SqlDataReader reader = null;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@pintPermissionId", SqlDbType.Int, 4, permissionId));
                    reader = db.GetDataReader("UP_Permission_GetByID", prams.ToArray());
                    if (reader.Read())
                        Load(reader);
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Generic Error", ex);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void Load(SqlDataReader reader)
        {
            _permissionId = reader["intPermissionId"] == DBNull.Value ? 0 : (int)reader["intPermissionId"];
            _name = reader["vchName"] == DBNull.Value ? null : (string)reader["vchName"];
            _moduleName = reader["vchModuleName"] == DBNull.Value ? null : (string)reader["vchModuleName"];
            _path = reader["vchPath"] == DBNull.Value ? null : (string)reader["vchPath"];
            _lookupTypeid = reader["intLookUpTypeId"] == DBNull.Value ? 0 : (int)reader["intLookUpTypeId"];
            // _isMainNav = reader["bitIsMainNav"] == DBNull.Value ? false : (bool) reader["bitIsMainNav"];

            _active = reader["bitActive"] == DBNull.Value ? false : (bool)reader["bitActive"];
            _dateCreated = (DateTime)reader["dtmdateCreated"];
            _intOrderBy = reader["intorderby"] == DBNull.Value ? 0 : (int)reader["intorderby"];
            _isDefaultUrl = reader["bitIsDefaultUrl"] == DBNull.Value ? false : (bool)reader["bitIsDefaultUrl"];
            _bitadmin = reader["bitAdminSide"] == DBNull.Value ? false : (bool)reader["bitAdminSide"];
            _intparentId = reader["intParentId"] == DBNull.Value ? 0 : (int)reader["intParentId"];
        }

        public Permission()
        {
        }

        public Permission(int permissionId)
        {
            this.Load(permissionId);
        }

        public static int Insert(Permission permission)
        {
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@pintPermissionId", SqlDbType.Int, 4, 0));
                    prams.Add(db.MakeInParam("@pvchName", SqlDbType.VarChar, 250, permission.Name));
                    prams.Add(db.MakeInParam("@pvchModuleName", SqlDbType.NVarChar, 250, permission.ModuleName));
                    prams.Add(db.MakeInParam("@pvchPath", SqlDbType.NVarChar, 500, permission.Path));
                    prams.Add(db.MakeInParam("@pintLookUpTypeId", SqlDbType.Int, 4, permission.LookUpTypeId));
                    prams.Add(db.MakeInParam("@pbitAdminSide", SqlDbType.Bit, 1, false));

                    prams.Add(db.MakeInParam("@pbitActive", SqlDbType.Bit, 1, permission.Active));
                    prams.Add(db.MakeInParam("@pdtmDateCreated", SqlDbType.Date, 8, permission.DateCreated));

                    if (!permission.IntOrderBy.Equals(0))
                    {
                        prams.Add(db.MakeInParam("@pintOrderBy", SqlDbType.Int, 4, permission.IntOrderBy));
                    }
                    else
                    {
                        prams.Add(db.MakeInParam("@pintOrderBy", SqlDbType.Int, 0, DBNull.Value));
                    }
                    if (permission.ParentId > 0)
                        prams.Add(db.MakeInParam("@pintParentId", SqlDbType.Int, 0, permission.ParentId));
                    else
                        prams.Add(db.MakeInParam("@pintParentId", SqlDbType.Int, 0, 0));

                    prams.Add(db.MakeInParam("@pbitIsDefaultUrl", SqlDbType.Bit, 1, permission.IsDefaultUrl));

                    int permissionId = db.RunProc("UP_Permission_InsertUpdate", prams.ToArray());
                    return permissionId;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Permission.Insert", ex);
                return 0;
            }
        }


        public static bool Update(Permission permission)
        {
            List<SqlParameter> prams = new List<SqlParameter>();

            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@pintPermissionId", SqlDbType.Int, 4, permission.PermissionId));
                    prams.Add(db.MakeInParam("@pvchName", SqlDbType.VarChar, 250, permission.Name));
                    prams.Add(db.MakeInParam("@pvchModuleName", SqlDbType.NVarChar, 250, permission.ModuleName));
                    prams.Add(db.MakeInParam("@pvchPath", SqlDbType.NVarChar, 250, permission.Path));
                    prams.Add(db.MakeInParam("@pintLookUpTypeId", SqlDbType.Int, 4, permission.LookUpTypeId));
                    prams.Add(db.MakeInParam("@pbitAdminSide", SqlDbType.Bit, 1, false));

                    prams.Add(db.MakeInParam("@pbitActive", SqlDbType.Bit, 1, permission.Active));
                    prams.Add(db.MakeInParam("@pdtmDateCreated", SqlDbType.Date, 8, permission.DateCreated));


                    if (!permission.IntOrderBy.Equals(0))
                    {
                        prams.Add(db.MakeInParam("@pintOrderBy", SqlDbType.Int, 4, permission.IntOrderBy));
                    }
                    else
                    {
                        prams.Add(db.MakeInParam("@pintOrderBy", SqlDbType.Int, 0, DBNull.Value));
                    }
                    if (permission.ParentId > 0)
                        prams.Add(db.MakeInParam("@pintParentId", SqlDbType.Int, 0, permission.ParentId));
                    else
                        prams.Add(db.MakeInParam("@pintParentId", SqlDbType.Int, 0, 0));
                    prams.Add(db.MakeInParam("@pbitIsDefaultUrl", SqlDbType.Bit, 1, permission.IsDefaultUrl));
                    int permissionId = db.RunProc("UP_Permission_InsertUpdate", prams.ToArray());
                    return true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Permission.Update", ex);
                return false;
            }
        }

        public static DataSet getOrderBy(int lookupTypeId, int permissionId)
        {

            try
            {
                DataSet ds = null;
                List<SqlParameter> prams = new List<SqlParameter>();

                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@pintLooUpTypeId", SqlDbType.Int, 4, lookupTypeId));
                    if (!permissionId.Equals(0))
                    {
                        prams.Add(db.MakeInParam("@pintPermissionId", SqlDbType.Int, 4, permissionId));
                    }
                    else
                    {
                        prams.Add(db.MakeInParam("@pintPermissionId", SqlDbType.Int, 0, DBNull.Value));
                    }
                    ds = db.GetDataSet("Up_getOrder_By_PermissionId", prams.ToArray());
                    return ds;

                }
            }
            catch (Exception ex)
            {

                new SqlLog().InsertSqlLog(0, "Generic Error", ex);
                return null;
            }
        }

        public static DataSet GetHeaderWithPermissions(string GroupId)
        {
            DataSet ds = null;
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    SqlParameter[] prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@groupids", SqlDbType.VarChar, 250, GroupId);
                    ds = db.GetDataSet("up_getHeader", prams);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "Permission GetHeader", ex);
                }
                return null;
            }
        }

        public static DataSet GetSubHeaderWithPermissions(string GroupId)
        {
            DataSet ds = null;
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    SqlParameter[] prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@groupids", SqlDbType.VarChar, 250, GroupId.Replace("\"",""));
                    ds = db.GetDataSet("Up_GetSubHeader", prams);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "Permission GetHeader", ex);
                }
                return null;
            }
        }


        /// <summary>
        /// Written by Muhammad Bashir Sub Menu Navigation in LeftNavigation control
        /// </summary>
        /// <param name="GroupId"></param>
        /// <param name="ParentId"></param>
        /// <returns>Sub-Menu Contents based on parent and required group permissions</returns>
        /// 

        public static DataSet GetSubHeaderWithPermissions(string GroupId, int ParentId)
        {
            DataSet ds = null;
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    SqlParameter[] prams = new SqlParameter[2];
                    prams[0] = db.MakeInParam("@groupids", SqlDbType.VarChar, 250, GroupId.Replace("\"", ""));
                    prams[1] = db.MakeInParam("@parentId", SqlDbType.Int, 4,ParentId);
                    ds = db.GetDataSet("Up_GetSubHeaderBy_ParentId", prams);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "Permission GetHeader", ex);
                }
                return null;
            }
        }

        public static DataTable GetHeaderPermissions(string RoleIds, int lookuptypeId)
        {
            DataTable dt = null;
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    SqlParameter[] prams = new SqlParameter[2];
                    prams[0] = db.MakeInParam("@vchRoleIds", SqlDbType.NVarChar, 500, RoleIds);
                    prams[1] = db.MakeInParam("@intLookUpTypeId", SqlDbType.Int, 4, lookuptypeId);
                    dt = db.GetDataSet("up_GetHeaderPermission", prams).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "Permission GetHeaderPermissions", ex);
                }
                return null;
            }
        }

        public static DataTable GetSubHeaderPermissions(string RoleIds, int lookuptypeId, int parentId)
        {
            DataTable dt = null;
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    SqlParameter[] prams = new SqlParameter[3];
                    prams[0] = db.MakeInParam("@vchRoleIds", SqlDbType.NVarChar, 500, RoleIds);
                    prams[1] = db.MakeInParam("@intLookUpTypeId", SqlDbType.Int, 4, lookuptypeId);
                    prams[2] = db.MakeInParam("@intParentId", SqlDbType.Int, 4, parentId);
                    dt = db.GetDataSet("[up_GetSubHeaderPermission]", prams).Tables[0];
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    return dt;
                    // }
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "Permission GetSubHeaderPermissions", ex);
                }
                return null;
            }
        }

        public static DataTable GetAllPermissionByPermissionType(int LookUpTypeId)
        {
            DataTable dt = null;
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    SqlParameter[] prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@pintTypeId", SqlDbType.Int, 4, LookUpTypeId);
                    dt = db.GetDataSet("[Up_getPermissionByLookUpTypeId]", prams).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "Permission GetAllPtsPermissionByPermissionType", ex);
                }
                return null;
            }
        }

        public static DataSet getAllRolePermissions(int roleId, int permissionId)
        {
            List<SqlParameter> prams = new List<SqlParameter>();
            DataSet ds = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@pintRoleId", SqlDbType.Int, 4, roleId));

                    if (permissionId != 0)
                        prams.Add(db.MakeInParam("@pintPermissionId", SqlDbType.Int, 4, permissionId));
                    else
                        prams.Add(db.MakeInParam("@pintPermissionId", SqlDbType.Int, 4, DBNull.Value));
                    ds = db.GetDataSet("[Up_getRolePermission]", prams.ToArray());
                    return ds;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Permission.getAllRolePermissions", ex);
                return null;
            }
        }

        public static bool Delete(int _roleId)
        {
            List<SqlParameter> prams = new List<SqlParameter>();

            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intRoleId", SqlDbType.Int, 4, _roleId));
                    int permissionid = db.RunProc("UP_Role_Permission_Delete_by_RoleId", prams.ToArray());
                    return true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Permission.Delete", ex);
                return false;
            }
        }

        public static bool InsertRolePermissions(int RoleId, string permissionIds)
        {
            List<SqlParameter> prams = new List<SqlParameter>();

            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@pintRoleId", SqlDbType.Int, 4, RoleId));
                    prams.Add(db.MakeInParam("@vchPermissionIds", SqlDbType.VarChar, 1000, permissionIds));

                    int groupPermissionID = db.RunProc("[UP_Role_Permission_Insert_All_Permissions]", prams.ToArray());
                    return true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Permission.InsertRolePermissions", ex);
                return false;
            }
        }



        public static DataSet GetPermissionInfo(int pageId, int pageSize, out int iTotalrows, String moduleName, String pageName)
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
                    if (moduleName == "")
                        prams.Add(db.MakeInParam("@moduleName", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@moduleName", SqlDbType.NVarChar, 100, moduleName));

                    if (pageName == "")
                        prams.Add(db.MakeInParam("@pageName", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@pageName", SqlDbType.NVarChar, 100, pageName));

                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));


                    ds = db.GetDataSet("up_PermissionInfo", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Permission.GetPermissionInfo", ex);

            }
            return ds;
        }
        public static void DeletePermissionInfo(int permissionid)
        {


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intpermissionId", SqlDbType.Int, 0, permissionid));

                    db.RunProc("up_deletePermission", prams.ToArray());

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Permission.DeletePermissionInfo", ex);

            }

        }



    }
}
