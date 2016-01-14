using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TireTraxLib.Security
{
    public class Groups
    {
        #region Properties

        private int _intgroupid;

        public int intGroupID
        {
            get { return _intgroupid; }
            set { _intgroupid = value; }
        }

        private string _vchname;

        public string vchName
        {
            get { return _vchname; }
            set { _vchname = value; }
        }

        private DateTime _dtmdatecreated;

        public DateTime dtmDateCreated
        {
            get { return _dtmdatecreated; }
            set { _dtmdatecreated = value; }
        }

        private int _intcreatedby;

        public int intCreatedBy
        {
            get { return _intcreatedby; }
            set { _intcreatedby = value; }
        }

        private DateTime _dtmdatemodified;

        public DateTime dtmDateModified
        {
            get { return _dtmdatemodified; }
            set { _dtmdatemodified = value; }
        }

        private int _intmodifiedby;

        public int intModifiedBy
        {
            get { return _intmodifiedby; }
            set { _intmodifiedby = value; }
        }

        private bool _bitisDeleted;

        public bool bitIsDeleted
        {
            get { return _bitisDeleted; }
            set { _bitisDeleted = value; }
        }

        private string _vchsecurityCode;

        public string vchSecurityCode
        {
            get { return _vchsecurityCode; }
            set { _vchsecurityCode = value; }
        }

        #endregion

        public Groups()
        {
            loadGroups();
        }

        public Groups(int groupid) 
        {
            loadGroups(groupid);
        }

        private void loadGroups(int groupid)
        {
            SqlDataReader reader = null;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    parm.Add(db.MakeInParam("@grpId", System.Data.SqlDbType.Int, 0, groupid));
                    reader = db.GetDataReader("up_getGroupDetailByID", parm.ToArray());
                    if (reader.Read())
                        loaddetail(reader);
                }
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void loadGroups()
        {
            SqlDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    reader = db.GetDataReader("up_getGroups");
                    if (reader.Read())
                        loaddetail(reader);
                }
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void loaddetail(SqlDataReader reader)
        {
            _intgroupid = reader["intGroupID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["intGroupID"]);
            _vchname = reader["vchName"] == DBNull.Value ? string.Empty : reader["vchName"].ToString();
            _dtmdatecreated = reader["dtmDateCreated"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["dtmDateCreated"]);
            _intcreatedby = reader["intCreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["intCreatedBy"]);
            _dtmdatemodified = reader["dtmDateModified"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["dtmDateModified"]);
            _intmodifiedby = reader["intModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["intModifiedBy"]);
            _bitisDeleted = reader["bitIsDeleted"] == DBNull.Value ? false : Convert.ToBoolean(reader["bitIsDeleted"]);
            _vchsecurityCode = reader["vchSecurityCode"] == DBNull.Value ? string.Empty : reader["vchSecurityCode"].ToString();
        }

        public static DataSet GetDeactivatedGroups()
        {
            DataSet ds = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    ds = db.GetDataSet("up_getDeactivateGroups");
                    return ds;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Groups.DeactivatedGroups", exp);
                return null;
            }
        }

        public static int InsertUpdateGroups(Groups grp)
        {
            int res = 0;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    if (grp.intGroupID == 0)
                    {
                        parm.Add(db.MakeInParam("@intGroupID", SqlDbType.Int, 0, 0));
                        parm.Add(db.MakeInParam("@dtmDateModified", SqlDbType.DateTime, 0, DBNull.Value));
                        parm.Add(db.MakeInParam("@intModifiedBy", SqlDbType.Int, 0, 0));
                    }
                    else
                    {
                        parm.Add(db.MakeInParam("@intGroupID", SqlDbType.Int, 0, grp.intGroupID));
                        parm.Add(db.MakeInParam("@dtmDateModified", SqlDbType.DateTime, 0, DateTime.Now));
                        parm.Add(db.MakeInParam("@intModifiedBy", SqlDbType.Int, 0, 1));
                    }
                    parm.Add(db.MakeInParam("@bitIsDeleted", SqlDbType.Bit, 0, 0));
                    parm.Add(db.MakeInParam("@intCreatedBy", SqlDbType.Int, 0, 1));

                    parm.Add(db.MakeInParam("@vchName", SqlDbType.VarChar, 250, grp.vchName));
                    parm.Add(db.MakeInParam("@vchSecurityCode", SqlDbType.VarChar, 500, DBNull.Value));

                    res = db.RunProc("up_InsertUpdateGroups", parm.ToArray());
                    return res;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Groups.Insert", exp);
                return 0;
            }
        }

        public static int ActiveDeactiveGroup(int grpId, bool bitdeactivate)
        {
            int result = 0;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    parm.Add(db.MakeInParam("@intgroupid", SqlDbType.Int, 0, grpId));
                    parm.Add(db.MakeInParam("@bitactive", SqlDbType.Bit, 0, bitdeactivate));

                    result = db.RunProc("up_DeactivateGroup", parm.ToArray());

                    return result;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Groups.DeactivateGroup", exp);
                return 0;
            }
        }

        public static DataSet GetGroupResources(int groupid)
        {
            DataSet ds = null;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    parm.Add(db.MakeInParam("@intgrpID", SqlDbType.Int, 0, groupid));

                    ds = db.GetDataSet("up_GetGroupsResources", parm.ToArray());

                    return ds;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Groups.GroupsResources", exp);
                return null;
            }
        }

        public static DataSet GetGroups()
        {
            DataSet ds = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    ds = db.GetDataSet("GetGroups");
                    return ds;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Groups.GetGroups", exp);
                return null;
            }
        }

        public static int AddRoleGroup(int groupid, int roleid, int subroleid)
        {
            int result = 0;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    parm.Add(db.MakeInParam("@intRoleID", SqlDbType.Int, 0, roleid));
                    parm.Add(db.MakeInParam("@intGroupID", SqlDbType.Int, 0, groupid));
                    parm.Add(db.MakeInParam("@SubRoleID", SqlDbType.Int, 0, subroleid));

                    result = db.RunProc("up_InsertRoleGroups", parm.ToArray());

                    return result;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Groups.InsertRoleGroups", exp);
                return result;
            }
        }

        //public static int UpdateRoleGroup(int groupid, int roleid, int subroleid)
        //{
        //    int result = 0;
        //    List<SqlParameter> parm = new List<SqlParameter>();
        //    try
        //    {
        //        using (DbManager db = DbManager.GetDbManager())
        //        {
        //            parm.Add(db.MakeInParam("@introleid", SqlDbType.Int, 0, roleid));
        //            parm.Add(db.MakeInParam("@groupid", SqlDbType.Int, 0, groupid));
        //            parm.Add(db.MakeInParam("@subroleid", SqlDbType.Int, 0, subroleid));

        //            result = db.RunProc("up_UpdateUserGroupOnRoleBases", parm.ToArray());

        //            return result;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        new SqlLog().InsertSqlLog(0, "Groups.UpdateUserGroupOnRoleBases", exp);
        //        return result;
        //    }
        //}
        /// <summary>
        /// generated for multiple groups to single role
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="roleid"></param>
        /// <param name="subroleid"></param>
        /// <returns></returns>
        public static int UpdateRoleGroup(string groupid, int roleid, int subroleid)
        {
            int result = 0;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    parm.Add(db.MakeInParam("@introleid", SqlDbType.Int, 0, roleid));
                    parm.Add(db.MakeInParam("@groupid", SqlDbType.VarChar,1000, groupid));
                    parm.Add(db.MakeInParam("@subroleid", SqlDbType.Int, 0, subroleid));

                    result = db.RunProc("up_UpdateUserGroupOnRoleBases", parm.ToArray());

                    return result;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Groups.UpdateUserGroupOnRoleBases", exp);
                return result;
            }
        }
        public static DataTable GetMainRoles(int id)
        {
            DataSet ds = null;
            DataTable dt = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LookupID", SqlDbType.Int, 4, id));
                    ds = DB.GetDataSet("up_getMainRoles", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups.GetMainRoles", ex);
            }

            return dt;
        }

        public static DataTable GetSubRolesLevel1(int id)
        {
            DataSet ds = null;
            DataTable dt = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@roleid", SqlDbType.Int, 4, id));
                    ds = DB.GetDataSet("up_getSubRolesLevel1", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups.SubRolesLevel1", ex);
            }

            return dt;
        }

        public static bool AddGroupRole(int groupId, string Role)
        {
            try
            {
                List<SqlParameter> prm = new List<SqlParameter>();
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intGroupId", SqlDbType.Int, 4, groupId));
                    prm.Add(DB.MakeInParam("@vchRoleName", SqlDbType.NVarChar, 100, Role));
                    int exec = DB.RunProc("up_AddGroupRole", prm.ToArray());
                    return true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups.GetMainRoles", ex);
            }
            return false;
        }


        public static DataSet getRolesByGroupId(int pageId, int pageSize, int groupId, out int iTotalrows)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intPageId", SqlDbType.Int, 4, pageId));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 4, pageSize));
                    prm.Add(DB.MakeInParam("@intGroupId", SqlDbType.Int, 0, groupId));
                    
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_getRolesByGroupId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups getRolesByGroupId", ex);
            }
            return ds;
        }


        public static void updateRoleInfoByRoleId(int roleid,string rolename)
        {
            DataSet ds = null;
           
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intRoleId", SqlDbType.Int, 0, roleid));
                    prm.Add(DB.MakeInParam("@RoleName", SqlDbType.NVarChar, 100, rolename));


                    ds = DB.GetDataSet("up_UpdateRoleInfoByRoleId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                     
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups updateRoleInfoByRoleId", ex);
            }
       
        }
        public static void deleteRolebyRoleId(int roleid)
        {
            DataSet ds = null;
         
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intRoleId", SqlDbType.Int, 0, roleid));

                    ds = DB.GetDataSet("up_deleteRoleInfoByRoleId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                     
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups deleteRolebyRoleId", ex);
            }
            
        }

        public static DataSet getRolesStatusByGroupId(int groupId)
        {
            DataSet ds = null;
     
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intgroupid", SqlDbType.Int, 0, groupId));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_getRolesStatusByGroupId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                       

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups getRolesStatusByGroupId", ex);
            }
            return ds;
        }

        public static DataSet getUserRolesStatusByRoleId(int roleId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intRoleId", SqlDbType.Int, 0, roleId));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_getUserRoleStatusByRoleId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {


                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups getUserRolesStatusByRoleId", ex);
            }
            return ds;
        }

        public static int getRoleNameStatus(string RoleName, int lookupTypeid)
        {
            DataSet ds = null;
            int count = 0;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@RoleName", SqlDbType.NVarChar, 200, RoleName));
                    prm.Add(DB.MakeInParam("@lookupTypeid", SqlDbType.Int, 0, lookupTypeid));

                    ds = DB.GetDataSet("up_getRoleNameStatus", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return count + 1;
                       
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups getRoleNameStatus", ex);
            }
            return count;
        }

        public static int getGroupNameStatus(string GroupName)
        {
            DataSet ds = null;
            int count = 0;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@grpname", SqlDbType.NVarChar, 200, GroupName));

                    ds = DB.GetDataSet("up_GroupExists", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return count + 1;

                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups GroupExists", ex);
            }
            return count;
        } 



        public static int UpdateSubRoleByRoleIdandUserId(int UserId,int RoleId)
        {
         
            int count = 0;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intRoleId", SqlDbType.Int, 0, RoleId));
                    prm.Add(DB.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));

                    count = DB.RunProc("up_UpdateRoleByRoleIdandUserId", prm.ToArray());
 

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Groups GroupExists", ex);
            }
            return count;
        }


    }
}
