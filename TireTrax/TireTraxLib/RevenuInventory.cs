using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
    public class RevenuInventory
    {
        #region Transaction Properties

        private int _transactionId;

        public int TransactionId
        {
            get { return _transactionId; }
            set { _transactionId = value; }
        }

        private int _tireId;

        public int TireId
        {
            get { return _tireId; }
            set { _tireId = value; }
        }

        private decimal _transactAmount;

        public decimal TranscatAmount
        {
            get { return _transactAmount; }
            set { _transactAmount = value; }
        }

        private int _organizationId;

        public int OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }


        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        private int _userId;

        public int userId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private int _currencyId;

        public int CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value; }
        }

        private int _week;

        public int Week
        {
            get { return _week; }
            set { _week = value; }
        }

        private int _year;

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        #endregion

        #region PTE Properties

        private int _pteId;

        public int PteId
        {
            get { return _pteId; }
            set { _pteId = value; }
        }

        private decimal _dollarValue;

        public decimal DollarValue
        {
            get { return _dollarValue; }
            set { _dollarValue = value; }
        }

        #endregion

        #region Commision Properties

        private int _commissionId;

        public int CommiaaionId
        {
            get { return _commissionId; }
            set { _commissionId = value; }
        }

        private decimal _commissionAmount;

        public decimal CommissionAmount
        {
            get { return _commissionAmount; }
            set { _commissionAmount = value; }
        }

        private decimal _percentage;

        public decimal Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }

        #endregion

        #region Organization Properties


        private string _legalName;

        public string LegalName
        {
            get { return _legalName; }
            set { _legalName = value; }
        }

        #endregion

        #region Tire Properties

        private string _dotNumber;

        public string DotNumber
        {
            get { return _dotNumber; }
            set { _dotNumber = value; }
        }

        private string _sizeNumber;

        public string SizeNumber
        {
            get { return _sizeNumber; }
            set { _sizeNumber = value; }
        }

        #endregion

        #region SizeCodes Properties

        private int _sizeId;

        public int SizeId
        {
            get { return _sizeId; }
            set { _sizeId = value; }
        }

        private string _tireSize;

        public string TireSize
        {
            get { return _tireSize; }
            set { _tireSize = value; }
        }


        #endregion

        #region RevenuInventory Methods

        public RevenuInventory()
        {
        }

        public RevenuInventory(int transactionId)
        {
            Load(transactionId);
        }

        private void Load(int transact_id)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@transactionId", SqlDbType.Int, 0, transact_id);
                    reader = db.GetDataReader("up_GetTransaction_ById", prams);
                    if (reader.Read())
                        load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "RevenueInventory.Load", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void load(IDataReader reader)
        {
            try
            {
                _transactionId = Conversion.ParseDBNullInt(reader["TransactionId"]);
                _tireId = Conversion.ParseDBNullInt(reader["TireId"]);
                _transactAmount = Conversion.ParseDBNullDecimal(reader["Amount"]);
                _organizationId = Conversion.ParseDBNullInt(reader["OrganizationId"]);
                _dateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
                _currencyId = Conversion.ParseDBNullInt(reader["CurrencyId"]);
                _week = Conversion.ParseDBNullInt(reader["Week"]);
                _year = Conversion.ParseDBNullInt(reader["Year"]);
                _pteId = Conversion.ParseDBNullInt(reader["PTEId"]);
                _dollarValue = Conversion.ParseDBNullDecimal(reader["DollarValue"]);
                _commissionId = Conversion.ParseDBNullInt(reader["CommisionId"]);
                _commissionAmount = Conversion.ParseDBNullDecimal(reader["CommisionAmount"]);
                _percentage = Conversion.ParseDBNullDecimal(reader["Percentage"]);
                _legalName = Conversion.ParseDBNullString(reader["LegalName"]);
                _dotNumber = Conversion.ParseDBNullString(reader["DOTNumber"]);
                _sizeNumber = Conversion.ParseDBNullString(reader["SizeNumber"]);
                _sizeId = Conversion.ParseDBNullInt(reader["SizeId"]);
                _tireSize = Conversion.ParseDBNullString(reader["TireSize"]);

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.load", ex);
            }
        }

        public static int InsertTransaction(int loadId, int organizationId, int userId)
        {

            List<SqlParameter> param = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    param.Add(DB.MakeInParam("@loadId", SqlDbType.Int, 0, loadId));
                    param.Add(DB.MakeInParam("@organizationId", SqlDbType.Int, 0, organizationId));
                    param.Add(DB.MakeInParam("@userId", SqlDbType.Int, 0, userId));
                    param.Add(DB.MakeInParam("@dateCreated", SqlDbType.DateTime, 8, DateTime.Now));
                    param.Add(DB.MakeInParam("@week", SqlDbType.Int, 0, System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))); // pass week of the year
                    param.Add(DB.MakeInParam("@year", SqlDbType.Int, 0, DateTime.Now.Year));


                    int exec = DB.RunProc("up_InsertInTransaction", param.ToArray());
                    return exec;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.InsertTransaction", ex);
            }
            return 0;
        }

        public static DataSet getDetailsRevenueByOrganizationId(int organizationId, DateTime dateCreated)
        {
            DataSet ds = null;
            List<SqlParameter> pram = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    pram.Add(DB.MakeInParam("@organizationId", SqlDbType.Int, 0, organizationId));
                    pram.Add(DB.MakeInParam("@dateCreated", SqlDbType.Date, 8, dateCreated));

                    ds = DB.GetDataSet("up_GetTireInfo_ByOrganizationId", pram.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.getDetailsRevenueByOrganizationId", ex);
            }
            return ds;
        }

        public static DataSet GetRevenue(int OrganizationId, string RevenueType, string Year, DateTime StartDate, DateTime EndDate)
        {
            DataSet ds = null;
            List<SqlParameter> pram = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    pram.Add(DB.MakeInParam("@ORGID", SqlDbType.Int, 0, OrganizationId));
                    pram.Add(DB.MakeInParam("@RevType", SqlDbType.VarChar, 100, RevenueType));
                    if (string.IsNullOrEmpty(Year))
                        pram.Add(DB.MakeInParam("@Year", SqlDbType.VarChar, 500, DBNull.Value));
                    else
                        pram.Add(DB.MakeInParam("@Year", SqlDbType.VarChar, 500, Year));
                    if (StartDate == DateTime.MinValue)
                        pram.Add(DB.MakeInParam("@StartDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        pram.Add(DB.MakeInParam("@StartDate", SqlDbType.DateTime, 8, StartDate.ToString("yyyy-MM-dd")));
                    if (EndDate == DateTime.MinValue)
                        pram.Add(DB.MakeInParam("@EndDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        pram.Add(DB.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate.ToString("yyyy-MM-dd")));

                    ds = DB.GetDataSet("up_GetRevenue", pram.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.GetRevenue", ex);
            }
            return ds;
        }

        public static DataSet GetRevenueDetails(int pageNo, int PageSize, out int Count, int OrganizationId, string Period, string Year, string Datewise)
        {
            Count = 0;
            DataSet ds = null;
            List<SqlParameter> pram = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    pram.Add(DB.MakeInParam("@PageNo", SqlDbType.Int, 4, pageNo));
                    pram.Add(DB.MakeInParam("@PageSize", SqlDbType.Int, 4, PageSize));
                    pram.Add(DB.MakeInParam("@OrgId", SqlDbType.Int, 0, OrganizationId));
                    pram.Add(DB.MakeInParam("@Period", SqlDbType.VarChar, 200, Period));
                    pram.Add(DB.MakeInParam("@Year", SqlDbType.VarChar, 4, Year));
                    if (string.IsNullOrEmpty(Datewise))
                        pram.Add(DB.MakeInParam("@Datewise", SqlDbType.VarChar, 200, DBNull.Value));
                    else
                        pram.Add(DB.MakeInParam("@Datewise", SqlDbType.VarChar, 200, Datewise));
                    pram.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_GetInvoiceDetailsByRevType", pram.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Count = Conversion.ParseInt(pram.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.GetRevenueDetails", ex);
            }
            return ds;
        }

        public static DataSet GetRevenueByMonth(int OrganizationId, string Month, string Year)
        {
            DataSet ds = null;
            List<SqlParameter> pram = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    pram.Add(DB.MakeInParam("@OrgId", SqlDbType.Int, 0, OrganizationId));
                    pram.Add(DB.MakeInParam("@Year", SqlDbType.VarChar, 500, Year));
                    pram.Add(DB.MakeInParam("@Month", SqlDbType.VarChar, 20, Month));

                    ds = DB.GetDataSet("up_GetRevenueByMonth", pram.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.GetRevenueByMonth", ex);
            }
            return ds;
        }

        public static DataSet GetRevenueByHalfYear(int OrganizationId, int Half, string Year)
        {
            DataSet ds = null;
            List<SqlParameter> pram = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    pram.Add(DB.MakeInParam("@OrgId", SqlDbType.Int, 0, OrganizationId));
                    pram.Add(DB.MakeInParam("@Year", SqlDbType.VarChar, 500, Year));
                    pram.Add(DB.MakeInParam("@Half", SqlDbType.Int, 4, Half));

                    ds = DB.GetDataSet("up_GetRevenueByHalfYear", pram.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.GetRevenueByHalfYear", ex);
            }
            return ds;
        }

        public static DataSet GetRevenueByQuarter(int OrganizationId, string Quarter, string Year)
        {
            DataSet ds = null;
            List<SqlParameter> pram = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    pram.Add(DB.MakeInParam("@OrgId", SqlDbType.Int, 0, OrganizationId));
                    pram.Add(DB.MakeInParam("@Year", SqlDbType.VarChar, 500, Year));
                    pram.Add(DB.MakeInParam("@Quarter", SqlDbType.VarChar, 20, Quarter));

                    ds = DB.GetDataSet("up_GetRevenueByQuarter", pram.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.GetRevenueByQuarter", ex);
            }
            return ds;
        }

        public static DataSet GetRevenueByYear(int OrganizationId, string Year)
        {
            DataSet ds = null;
            List<SqlParameter> pram = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    pram.Add(DB.MakeInParam("@OrgId", SqlDbType.Int, 0, OrganizationId));
                    pram.Add(DB.MakeInParam("@Year", SqlDbType.VarChar, 500, Year));

                    ds = DB.GetDataSet("up_GetRevenueByYear", pram.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.GetRevenueByYear", ex);
            }
            return ds;
        }

        public static DataSet GetRevenueAdmin(int StateId, int OrganizationId, DateTime StartDate, DateTime EndDate,int CountryId)
        {
            DataSet ds = null;
            List<SqlParameter> pram = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    if (StateId == 0)
                        pram.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, DBNull.Value));
                    else
                        pram.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, StateId));
                    if(OrganizationId == 0)
                        pram.Add(DB.MakeInParam("@OrgId", SqlDbType.Int, 4, DBNull.Value));
                    else
                        pram.Add(DB.MakeInParam("@OrgId", SqlDbType.Int, 4, OrganizationId));
                    if (StartDate == DateTime.MinValue)
                        pram.Add(DB.MakeInParam("@StartDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        pram.Add(DB.MakeInParam("@StartDate", SqlDbType.DateTime, 8, StartDate.ToString("yyyy-MM-dd")));
                    if (EndDate == DateTime.MinValue)
                        pram.Add(DB.MakeInParam("@EndDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        pram.Add(DB.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate.ToString("yyyy-MM-dd")));
                    pram.Add(DB.MakeInParam("@CountryId", SqlDbType.Int, 4, CountryId));
                    ds = DB.GetDataSet("up_GetAdminRevenue", pram.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.GetRevenueAdmin", ex);
            }
            return ds;
        }

        public static DataSet GetRevenueDetailsAdmin(int pageNo, int PageSize, out int Count, int OrganizationId,DateTime StartDate, DateTime EndDate)
        {
            Count = 0;
            DataSet ds = null;
            List<SqlParameter> pram = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    pram.Add(DB.MakeInParam("@PageNo", SqlDbType.Int, 4, pageNo));
                    pram.Add(DB.MakeInParam("@PageSize", SqlDbType.Int, 4, PageSize));
                    pram.Add(DB.MakeInParam("@OrgId", SqlDbType.Int, 0, OrganizationId));

                    if (StartDate == DateTime.MinValue)
                        pram.Add(DB.MakeInParam("@StartDate", SqlDbType.DateTime, 0, DBNull.Value));
                    else
                        pram.Add(DB.MakeInParam("@StartDate", SqlDbType.DateTime, 0, StartDate));

                    if (EndDate == DateTime.MinValue)
                        pram.Add(DB.MakeInParam("@EndDate", SqlDbType.DateTime, 0, DBNull.Value));
                    else
                        pram.Add(DB.MakeInParam("@EndDate", SqlDbType.DateTime, 0, EndDate));

                    pram.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_GetRevenueDetailsForAdmin", pram.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Count = Conversion.ParseInt(pram.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RevenuInventory.GetRevenueDetailsAdmin", ex);
            }
            return ds;
        }

        #endregion


    }
}
