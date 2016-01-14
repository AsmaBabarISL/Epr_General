using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace TireTraxLib
{
    public class Facility
    {
        private int _facilityid;
        public int FacilityID
        {
            get { return _facilityid; }
            set { _facilityid = value; }
        }

        private int _organisationid;
        public int OrganizationID
        {
            get { return _organisationid; }
            set { _organisationid = value; }
        }

        private int _userid;
        public int UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }

        private int _roleid;
        public int RoleID
        {
            get { return _roleid; }
            set { _roleid = value; }
        }

        public string _facilityname;
        public string FacilityName
        {
            get { return _facilityname; }
            set { _facilityname = value; }
        }

        private bool _isactive;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }

        private bool _isdeleted;
        public bool IsDeleted
        {
            get { return _isdeleted; }
            set { _isdeleted = value; }
        }

        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        private int _ProductCategoryID;

        public int ProductCategoryID
        {
            get { return _ProductCategoryID; }
            set { _ProductCategoryID = value; }
        }
        public Facility()
        {

        }

        public Facility(long FacilityID)
        {
            this.loadFacility(FacilityID);
        }

        private void loadFacility(long FacilityID)
        {
            IDataReader reader = null;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {

                    prams.Add(db.MakeInParam("@intFacilityID", SqlDbType.BigInt, 0, FacilityID));
                    reader = db.GetDataReader("up_Lots_getById", prams.ToArray());
                    if (reader.Read())
                        Load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Facility.loadFacility", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void Load(IDataReader reader)
        {
            _facilityid = Conversion.ParseDBNullInt(reader["intFacilityID"]);
            _organisationid = Conversion.ParseDBNullInt(reader["OrganizationId"]);
            _userid = Conversion.ParseInt(reader["UserID"]);
            _roleid = Conversion.ParseInt(reader["RoleID"]);
            _dateCreated = Conversion.ParseDateTime(Conversion.ParseString(reader["dtmDateCreated"]));
            _facilityname = Conversion.ParseString(reader["vchFacilityName"]);
            _isactive = Conversion.ParseBool(reader["bitActive"]);
            _isdeleted = Conversion.ParseBool(reader["bitDeleted"]);
        }



        public static int InsertUpdate(Facility facility)
        {
            int FacilityID = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intFacilityId", SqlDbType.BigInt, 0, facility.FacilityID));
                    prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 0, facility.OrganizationID));
                    prm.Add(DB.MakeInParam("@UserID", SqlDbType.Int, 0, facility.UserID));
                    prm.Add(DB.MakeInParam("@RoleID", SqlDbType.Int, 0, facility.RoleID));
                    prm.Add(DB.MakeInParam("@vchFacilityName", SqlDbType.NVarChar, 50, facility.FacilityName));
                    prm.Add(DB.MakeInParam("@ProductCategoryID", SqlDbType.Int, 4, facility.ProductCategoryID));
                    FacilityID = DB.RunProc("up_Facility_InsertUpdate", prm.ToArray());

                }
            }
            catch (Exception e)
            {
                FacilityID = 0;
            }
            return FacilityID;
        }

        public static DataSet GetFacilityByOrganizationId(int organizationId)
        {
            DataSet ds = null;
            
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intOrganizationID", SqlDbType.Int, 4, organizationId));


                    ds = DB.GetDataSet("Up_Facility_getFacilityLookUp", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                       
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Facility.GetFacilityByOrganizationId", ex);
            }
            return ds;
        }


        public static DataSet GetFacility(int pageId, int pageSize, int organizationId, out int iTotalrows, string FacilityName,int CatId)
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
                    prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 4, organizationId));
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 4, CatId));
                    if (string.IsNullOrEmpty(FacilityName))
                        prm.Add(DB.MakeInParam("@vchFacilityName", SqlDbType.NVarChar, 200, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchFacilityName", SqlDbType.NVarChar, 200, FacilityName));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));


                    
                    ds = DB.GetDataSet("Up_Facility_getPagedData", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Facility.GetFacility", ex);
            }
            return ds;
        }
        public static DataSet GetFacilityNameByFacilityId(int facilityid)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intfacilityid", SqlDbType.Int, 0, facilityid));




                    ds = DB.GetDataSet("up_getfacilitynameByFacilityId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Facility.GetFacilityNameByFaclitId", ex);
            }
            return ds;
        }


        public static void UpdateFacility(long FacilityID, string FacilityName)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intFacilityid", SqlDbType.BigInt, 0, FacilityID));
                    prm.Add(DB.MakeInParam("@vchFacilityName", SqlDbType.NVarChar, 50, FacilityName));
                    DB.RunProc("up_Facility_UpdateFacilitybyID", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Facility.UpdateFacility", ex);
            }

        }


         

      public static int GetFacilityNameStatus(string facilityname,int OrganizationId) 
        {
            DataSet ds = null;
            int count = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@FacilityName", SqlDbType.NVarChar, 500, facilityname));
                    prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int,0,OrganizationId));
                     



                    ds = DB.GetDataSet("up_getFacilityNameExistOrNot", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                       return count+1;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Facility.GetFacilityNameStatus", ex);
            }
            return count;
        }

      public static void DeleteFacilityByFacilityId(int facilityId)
      {
          DataSet ds = null;

          List<SqlParameter> prm = new List<SqlParameter>();
          try
          {
              using (DbManager DB = DbManager.GetDbManager())
              {
                  prm.Add(DB.MakeInParam("@intfacilityid", SqlDbType.BigInt, 0, facilityId));

                  DB.RunProc("up_deleteFacilitybyFacilityId", prm.ToArray());
              }
          }
          catch (Exception ex)
          {
              new SqlLog().InsertSqlLog(0, "Facility.DeleteFacilityByFacilityId", ex);
          }

      }


    }

}
