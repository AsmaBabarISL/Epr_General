using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace TireTraxLib.Security
{
    public class RoleManagement
    {
        private int createdBy;
        private int modifiedBy;
        private int roleId;
        private string roleName;
        private string securityToken;

        public RoleManagement()
        {
        }
        public static string GetSecurityToken(int GroupId)
        {
            DataSet dataSet = null;
            try
            {
                using (DbManager manager = DbManager.GetDbManager())
                {
                    SqlParameter[] prams = new SqlParameter[] { manager.MakeInParam("@groupid", SqlDbType.VarChar, 0, GroupId) };
                    dataSet = manager.GetDataSet("up_role_gettokenbyId", prams);
                    if (((dataSet != null) && (dataSet.Tables.Count > 0)) && (dataSet.Tables[0].Rows.Count > 0))
                    {
                        return dataSet.Tables[0].Rows[0]["vchSecurityCode"].ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                new SqlLog().InsertSqlLog(0, "rolemanagement.GetSecurityToken", exception);
            }
            finally
            {
                if (dataSet != null)
                {
                    dataSet.Dispose();
                }
            }
            return string.Empty;
        }
        public static string GetSecurityTokenByRoleId(int roleId)
        {
            DataSet dataSet = null;
            try
            {
                using (DbManager manager = DbManager.GetDbManager())
                {
                    SqlParameter[] prams = new SqlParameter[] { manager.MakeInParam("@intRoleId", SqlDbType.Int, 0, roleId) };
                    dataSet = manager.GetDataSet("up_role_gettokenbyRoleId", prams);
                    if (((dataSet != null) && (dataSet.Tables.Count > 0)) && (dataSet.Tables[0].Rows.Count > 0))
                    {
                        return dataSet.Tables[0].Rows[0]["vchSecurityCode"].ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                new SqlLog().InsertSqlLog(0, "rolemanagement.GetSecurityToken", exception);
            }
            finally
            {
                if (dataSet != null)
                {
                    dataSet.Dispose();
                }
            }
            return string.Empty;
        }
        public static DataSet GetSecurityTokenByGroupIds(string GroupIds)
        {
            DataSet dataSet = null;
            try
            {
                using (DbManager manager = DbManager.GetDbManager())
                {
                    SqlParameter[] prams = new SqlParameter[] { manager.MakeInParam("@groupid", SqlDbType.VarChar, 0, GroupIds) };
                    dataSet = manager.GetDataSet("up_role_gettokenbyId", prams);
                    
                }
            }
            catch (Exception exception)
            {
                new SqlLog().InsertSqlLog(0, "rolemanagement.GetSecurityToken", exception);
            }
            finally
            {
                if (dataSet != null)
                {
                    dataSet.Dispose();
                }
            }
            return dataSet;
        }

        public static DataTable GetRoles(int organizationTypeId)
        {
            DataTable dt = null;
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    SqlParameter[] prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@intOrganizationTypeId", SqlDbType.Int, 4, organizationTypeId);
                    dt = db.GetDataSet("[up_getRolesByOrganizationTypeId]", prams).Tables[0];
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


        public static DataTable GetRoleSubroleNGroupByIds(int RoleId, int SubRoleId)
        {
            DataTable dt = null;
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    SqlParameter[] prams = new SqlParameter[2];
                    prams[0] = db.MakeInParam("@roleid", SqlDbType.Int, 0, RoleId);
                    prams[1] = db.MakeInParam("@subroleid", SqlDbType.Int, 0, SubRoleId);
                    dt = db.GetDataSet("up_GetRoleSubroleNGroup ", prams).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "RoleManagment  GetRoleSubroleNGroup", ex);
                }
                return null;
            }
        }
        public static DataTable getDistinctRolesByGroupId(int groupId)
        {
            DataTable dt = null;
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    SqlParameter[] prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@grpId", SqlDbType.Int, 0, groupId);

                    dt = db.GetDataSet("up_getDistinctRolesByGroupId ", prams).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "RoleManagment  getDistinctRolesByGroupId", ex);
                }
                return null;
            }
        }
        public static int UpdateSecurityToken(int roleId, string securityToken)
        {
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    SqlParameter[] parameter = new SqlParameter[2];

                    parameter[0] = db.MakeInParam("@intRoleId", SqlDbType.Int, 0, roleId);
                    parameter[1] = db.MakeInParam("@vchSecurityToken", SqlDbType.VarChar, 8000, securityToken);

                    int exec = db.RunProc("up_role_updatetoken", parameter);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "rolemanagement.UpdateSecurityToken", ex);
            }
            return 0;
        }
         public static int UpdateSecurityTokenbyRoleId(int roleId, string securityToken)
        {
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    SqlParameter[] parameter = new SqlParameter[2];

                    parameter[0] = db.MakeInParam("@intRoleId", SqlDbType.Int, 0, roleId);
                    parameter[1] = db.MakeInParam("@vchSecurityToken", SqlDbType.VarChar, 130, securityToken);

                    int exec = db.RunProc("up_role_updatetokenbyRoleId", parameter);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "rolemanagement.UpdateSecurityTokenbyRoleId", ex);
            }
            return 0;
        }

    }
}
