using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
    public class Commission
    {

        #region Commission

        private int _commissionId;

        public int CommissionId
        {
            get { return _commissionId; }
            set { _commissionId = value; }
        }

        private int _countryId;

        public int CountryId
        {
            get { return _countryId; }
            set { _countryId = value; }
        }
        private int _organizationId;

        public int OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }
        private int _organizationSubTypeId;

        public int OrganizationSubTypeId
        {
            get { return _organizationSubTypeId; }
            set { _organizationSubTypeId = value; }
        }

        private int _lookuptypeId;

        public int TypeId
        {
            get { return _lookuptypeId; }
            set { _lookuptypeId = value; }
        }
        private int _tntCommissionType;

        public int TntCommissionType
        {
            get { return _tntCommissionType; }
            set { _tntCommissionType = value; }
        }


        private decimal _percentage;

        public decimal Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }
        private decimal _amount;

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private int _createdById;

        public int CreatedById
        {
            get { return _createdById; }
            set { _createdById = value; }
        }

        private DateTime _dtmdatemodified;

        public DateTime Dtmdatemodified
        {
            get { return _dtmdatemodified; }
            set { _dtmdatemodified = value; }
        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private int _StewardshipID;

        public int StewardshipID
        {
            get { return _StewardshipID; }
            set { _StewardshipID = value; }
        }
        private string _OrganizationSubType;

        public string OrganizationSubType
        {
            get { return _OrganizationSubType; }
            set { _OrganizationSubType = value; }
        }
        private String _lookupTypeName;

        public String LookupTypeName
        {
            get { return _lookupTypeName; }
            set { _lookupTypeName = value; }
        }

        private String _countryName;

        public String CountryName
        {
            get { return _countryName; }
            set { _countryName = value; }
        }
        #endregion

        #region Methods


        public Commission()
        {

        }


        public static bool CommissionSetting(Commission commission)
        {
            try
            {


                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[8];

                    prams[0] = db.MakeInParam("@intcommissionId", SqlDbType.Int, 0, commission.CommissionId);
                    prams[1] = db.MakeInParam("@intcountryId", SqlDbType.Int, 0, commission.CountryId);
                    prams[2] = db.MakeInParam("@intorganizationSubTypeId", SqlDbType.Int, 0, commission.OrganizationSubTypeId);

                    prams[3] = db.MakeInParam("@intStewardshipID", SqlDbType.Int, 0, commission.StewardshipID);
                    prams[4] = db.MakeInParam("@tntcommissiontype", SqlDbType.Int, 0, commission.TntCommissionType);
                    prams[5] = db.MakeInParam("@amount", SqlDbType.Decimal, 0, commission.Amount);
                    prams[6] = db.MakeInParam("@percentage", SqlDbType.Decimal, 0, commission.Percentage);
                    prams[7] = db.MakeInParam("@intcreatedby", SqlDbType.Int, 0, commission.CreatedById);




                    int exec = db.RunProc("[up_commission_insert]", prams);
                    return true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "commission.UpdateCommission", ex);
                return false;
            }
            return true;
        }
        public static DataSet GetCommissionInfo(int countryId, int stewardshipId)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {



                    prams.Add(db.MakeInParam("@intcountryId", SqlDbType.Int, 0, countryId));
                    prams.Add(db.MakeInParam("@intstewardshipId", SqlDbType.Int, 0, stewardshipId));
                    //prams.Add(db.MakeInParam("@tntCommissionType", SqlDbType.Int, 0, typeid));

                    ds = db.GetDataSet("up_commission_getbyStewardshipId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Commission.GetCommissionInfo", ex);

            }
            return ds;
        }

        public static DataSet GetCommissionOfStewardship(int stewardshipId, int OrganizationSubTypeid)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intstewardshipId", SqlDbType.Int, 0, stewardshipId));
                    prams.Add(db.MakeInParam("@OrganizationSubTypeid", SqlDbType.Int, 0, OrganizationSubTypeid));
                    ds = db.GetDataSet("up_getComissionbyorgandsubtypeid", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Commission.GetCommissionOfStewardship", ex);

            }
            return ds;
        }

        public static DataSet GetRevenue_Summary(String fromdate, String todate)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {

                    if (fromdate == "")
                    {
                        prams.Add(db.MakeInParam("@fromdate", SqlDbType.DateTime, 0, DBNull.Value));
                    }
                    else
                    {
                        prams.Add(db.MakeInParam("@fromdate", SqlDbType.DateTime, 0, Convert.ToDateTime(fromdate)));
                    }
                    if (todate == "")
                    {
                        prams.Add(db.MakeInParam("@todate", SqlDbType.DateTime, 0, DBNull.Value));
                    }
                    else
                    {
                        prams.Add(db.MakeInParam("@todate", SqlDbType.DateTime, 0, Convert.ToDateTime(todate)));
                    }

                    ds = db.GetDataSet("up_RevenueByCommission_summary", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Commission.GetRevenue_Summary", ex);

            }
            return ds;
        }
        public static DataSet GetRevenue_Detail(String fromdate, String todate)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {


                    if (fromdate == "")
                    {
                        prams.Add(db.MakeInParam("@fromdate", SqlDbType.DateTime, 0, DBNull.Value));
                    }
                    else
                    {
                        prams.Add(db.MakeInParam("@fromdate", SqlDbType.DateTime, 0, Convert.ToDateTime(fromdate)));
                    }
                    if (todate == "")
                    {
                        prams.Add(db.MakeInParam("@todate", SqlDbType.DateTime, 0, DBNull.Value));
                    }
                    else
                    {
                        prams.Add(db.MakeInParam("@todate", SqlDbType.DateTime, 0, Convert.ToDateTime(todate)));
                    }



                    ds = db.GetDataSet("up_RevenueByCommission_detail", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Commission.GetRevenue_Detail", ex);

            }
            return ds;
        }




        #endregion
    }
}
