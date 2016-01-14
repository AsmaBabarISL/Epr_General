using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TireTraxLib.Security
{
    public class GroupPages
    {
        #region Properties

        private int _intresourceID;

        public int intResourceID
        {
            get { return _intresourceID; }
            set { _intresourceID = value; }
        }

        private string _vchname;

        public string vchName
        {
            get { return _vchname; }
            set { _vchname = value; }
        }

        private string _vchpath;

        public string vchPath
        {
            get { return _vchpath; }
            set { _vchpath = value; }
        }

        private DateTime _dtmdatecreated;

        public DateTime dtmDateCreated
        {
            get { return _dtmdatecreated; }
            set { _dtmdatecreated = value; }
        }

        private int _intcreatedBy;

        public int intCreatedBy
        {
            get { return _intcreatedBy; }
            set { _intcreatedBy = value; }
        }

        private DateTime _dtmdatemodified;

        public DateTime dtmDateModified
        {
            get { return _dtmdatemodified; }
            set { _dtmdatemodified = value; }
        }

        private int _intmodifiedBy;

        public int intModifiedBy
        {
            get { return _intmodifiedBy; }
            set { _intmodifiedBy = value; }
        }

        private bool _bitisdeleted;

        public bool bitIsDeleted
        {
            get { return _bitisdeleted; }
            set { _bitisdeleted = value; }
        }

        private int _intparentid;

        public int intParentID
        {
            get { return _intparentid; }
            set { _intparentid = value; }
        }

        #endregion

        public GroupPages()
        {
            loadGroupPages();
        }

        public GroupPages(int resourceid)
        {
            loadGroupPages(resourceid);
        }

        private void loadGroupPages(int resourceid)
        {
            SqlDataReader reader = null;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    parm.Add(db.MakeInParam("@pageid", System.Data.SqlDbType.Int, 0, resourceid));
                    reader = db.GetDataReader("up_getGroupPagesDetailByID", parm.ToArray());
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

        private void loadGroupPages()
        {
            SqlDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    reader = db.GetDataReader("up_getGroupResources");
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
            _intresourceID = reader["intResourceId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["intResourceId"]);
            _vchname = reader["vchName"] == DBNull.Value ? string.Empty : reader["vchName"].ToString();
            _vchpath = reader["vchPath"] == DBNull.Value ? string.Empty : reader["vchPath"].ToString();
            _dtmdatecreated = reader["dtmDateCreated"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["dtmDateCreated"]);
            _intcreatedBy = reader["intCreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["intCreatedBy"]);
            _dtmdatemodified = reader["dtmDateModified"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["dtmDateModified"]);
            _intmodifiedBy = reader["intModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["intModifiedBy"]);
            _bitisdeleted = reader["bitIsDeleted"] == DBNull.Value ? false : Convert.ToBoolean(reader["bitIsDeleted"]);
            _intparentid = reader["intparentid"] == DBNull.Value ? 0 : Convert.ToInt32(reader["intparentid"]);
        }

        public static DataSet DeactivatePages()
        {
            DataSet ds = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    ds = db.GetDataSet("up_getDeactivateResources");
                    return ds;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "GroupPages.DeactivateResources", exp);
                return null;
            }
        }

        public static int InsertUpdateResources(GroupPages grp)
        {
            int res = 0;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    if (grp.intResourceID == 0)
                    {
                        parm.Add(db.MakeInParam("@intResourceId", SqlDbType.Int, 0, 0));
                        parm.Add(db.MakeInParam("@dtmDateModified", SqlDbType.DateTime, 0, DBNull.Value));
                        parm.Add(db.MakeInParam("@intModifiedBy", SqlDbType.Int, 0, 0));
                        parm.Add(db.MakeInParam("@bitIsDeleted", SqlDbType.Bit, 0, 0));
                    }
                    else
                    {
                        parm.Add(db.MakeInParam("@intResourceId", SqlDbType.Int, 0, grp.intResourceID));
                        parm.Add(db.MakeInParam("@dtmDateModified", SqlDbType.DateTime, 0, DateTime.Now));
                        parm.Add(db.MakeInParam("@intModifiedBy", SqlDbType.Int, 0, 1));
                        parm.Add(db.MakeInParam("@bitIsDeleted", SqlDbType.Bit, 0, 0));
                    }
                    parm.Add(db.MakeInParam("@vchName", SqlDbType.VarChar, 250, grp.vchName));
                    parm.Add(db.MakeInParam("@intCreatedBy", SqlDbType.Int, 0, grp.intCreatedBy));

                    parm.Add(db.MakeInParam("@vchPath", SqlDbType.VarChar, 500, grp.vchPath));
                    parm.Add(db.MakeInParam("@intparentid", SqlDbType.Int, 0, grp.intParentID));

                    res = db.RunProc("up_InsertUpdateResources", parm.ToArray());
                    return res;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "GroupPages.Insert", exp);
                return 0;
            }
        }

        public static int ActiveDeactiveGroupPage(int pageid, bool bitdeactivate)
        {
            int result = 0;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    parm.Add(db.MakeInParam("@intresourceId", SqlDbType.Int, 0, pageid));
                    parm.Add(db.MakeInParam("@bitactive", SqlDbType.Bit, 0, bitdeactivate));

                    result = db.RunProc("up_DeactivateGroupPages", parm.ToArray());

                    return result;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "GroupPages.DeactivatePages", exp);
                return 0;
            }
        }

        public static DataSet GetPages()
        {
            DataSet ds = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    ds = db.GetDataSet("up_GetPages");
                    return ds;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "GroupPages.GetPages", exp);
                return null;
            }
        }

        public static int InsertGroupResources(int groupd, int pageid)
        {
            int result = 0;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    parm.Add(db.MakeInParam("@groupid", SqlDbType.Int, 0, groupd));
                    parm.Add(db.MakeInParam("@pageid", SqlDbType.Bit, 0, pageid));

                    result = db.RunProc("up_AddGroupResources", parm.ToArray());

                    return result;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "GroupPages.InsertGroupResources", exp);
                return result;
            }
        }

        public static DataTable GetDomain()
        {
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    ds = db.GetDataSet("up_GetMainDomains");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
                    return dt;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "GroupPages.GetDomain", exp);
                return null;
            }
        }

        public static DataSet GetDomainName(int parentid)
        {
            DataSet ds = null;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    parm.Add(db.MakeInParam("@parentid", SqlDbType.Int, 0, parentid));

                    ds = db.GetDataSet("up_getDomainName", parm.ToArray());

                    return ds;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "GroupPages.DomainName", exp);
                return null;
            }
        }

        public static DataSet GetDomainID(int resourceid)
        {
            DataSet ds = null;
            List<SqlParameter> parm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    parm.Add(db.MakeInParam("@resorceid", SqlDbType.Int, 0, resourceid));

                    ds = db.GetDataSet("up_getDomainIDByResourceID", parm.ToArray());

                    return ds;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "GroupPages.DomainID", exp);
                return null;
            }
        }

        public static DataTable GetAllResources()
        {
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    ds = db.GetDataSet("up_GetAllResources");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
                    return dt;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "GroupPages.AllResources", exp);
                return null;
            }
        }
    }
}
