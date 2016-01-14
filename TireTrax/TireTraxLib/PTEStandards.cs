using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
    public class PTEStandards
    {

        #region PTEPTEStandards

        private int _stateId;

        public int StateId
        {
            get { return _stateId; }
            set { _stateId = value; }
        }

        private int _pteIdStandardId;

        public int PteStandardId
        {
            get { return _pteIdStandardId; }
            set { _pteIdStandardId = value; }
        }

        private int _SizeId;

        public int SizeId
        {
            get { return _SizeId; }
            set { _SizeId = value; }
        }
        private DateTime _effectiveDate;

        public DateTime EffectiveDate
        {
            get { return _effectiveDate; }
            set { _effectiveDate = value; }
        }
        private DateTime _expirationDate;

        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }
        private float _dollarValue;

        public float DollarValue
        {
            get { return _dollarValue; }
            set { _dollarValue = value; }
        }

        private int _LanguageId;

        public int LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }

        private bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        private DateTime _CreatedDate;

        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        private int _CreatedByUserId;

        public int CreatedByUserId
        {
            get { return _CreatedByUserId; }
            set { _CreatedByUserId = value; }
        }

        private DateTime _UpdatedDate;

        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        private int _UpdatedByUserId;

        public int UpdatedByUserId
        {
            get { return _UpdatedByUserId; }
            set { _UpdatedByUserId = value; }
        }







        #endregion

        public PTEStandards()
        {

        }
        public PTEStandards(int pteId)
        {
            Load(pteId);
        }
        private void Load(int pteId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@PTEStandardId", SqlDbType.Int, 0, pteId);
                    reader = db.GetDataReader("up_PTEStandard_getById", prams);
                    if (reader.Read())
                        Load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "PTEStandards.Load", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void Load(IDataReader reader)
        {
            try
            {
                _pteIdStandardId = Conversion.ParseDBNullInt(reader["PTEId"]);

                _SizeId = Conversion.ParseDBNullInt(reader["SizeId"]);
                _effectiveDate = Conversion.ParseDBNullDateTime(reader["EffectiveDate"]);
                _expirationDate = Conversion.ParseDBNullDateTime(reader["ExpirationDate"]);
                _dollarValue = Conversion.ParseDBNullInt(reader["DollarValue"]);
                _LanguageId = Conversion.ParseDBNullInt(reader["LanguageId"]);
                _IsActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                _CreatedDate = Conversion.ParseDBNullDateTime(reader["CreatedDate"]);
                _CreatedByUserId = Conversion.ParseDBNullInt(reader["CreatedByUserId"]);
                _UpdatedDate = Conversion.ParseDBNullDateTime(reader["UpdatedDate"]);
                _UpdatedByUserId = Conversion.ParseDBNullInt(reader["UpdatedByUserId"]);


            }
            catch (Exception ex)
            {

                new SqlLog().InsertSqlLog(0, "PTEStandards.Load", ex);
            }


        }


        public static DataSet getSetting(int LanguageId, int stateid, int PageId, int pageSize, out int iTotalrows)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prm = new List<SqlParameter>();


            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, LanguageId));
                    if (stateid == 0)
                        prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, stateid));
                    prm.Add(DB.MakeInParam("@intPageId", SqlDbType.Int, 4, PageId));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 4, pageSize));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_PTEStandard_Select", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "PTEStandards.getSetting", exp);
            }
            return ds;
        }
        public static void DeleteSetting(int pteID, DateTime UpdatedDate, int UpdateByUserId)
        {
            List<SqlParameter> prm = new List<SqlParameter>();

            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@PTEStandardId", SqlDbType.Int, 4, pteID));
                    prm.Add(DB.MakeInParam("@UpdatedByUserId", SqlDbType.Int, 4, UpdateByUserId));
                    prm.Add(DB.MakeInParam("@UpdatedDate", SqlDbType.DateTime, 8, UpdatedDate));

                    DB.RunProc("up_PTEStandards_delete", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "PTEStandard.DeleteSetting", ex);
            }

        }

        public static int AddSetting(PTEStandards objPTE)
        {
            int checkBit;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, objPTE.StateId));

                    prm.Add(DB.MakeInParam("@SizeId", SqlDbType.Int, 0, objPTE.SizeId));
                    prm.Add(DB.MakeInParam("@EffectiveDate", SqlDbType.DateTime, 0, objPTE.EffectiveDate));
                    prm.Add(DB.MakeInParam("@ExpirationDate", SqlDbType.DateTime, 0, objPTE.ExpirationDate));
                    prm.Add(DB.MakeInParam("@DollarValue", SqlDbType.Float, 0, objPTE.DollarValue));
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 0, objPTE.LanguageId));
                    prm.Add(DB.MakeInParam("@CreatedDate", SqlDbType.DateTime, 0, objPTE.CreatedDate));
                    prm.Add(DB.MakeInParam("@CreatedByUserId", SqlDbType.Int, 0, objPTE.CreatedByUserId));
                    // prm.Add(DB.MakeReturnParam(SqlDbType.Int, 0));
                    //prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 0, objPTE.OrganizationId));
                    prm.Add(DB.MakeOutParam("@bit", SqlDbType.Int, 4));
                    checkBit = DB.RunProc("up_PTEStandards_add", prm.ToArray());
                    checkBit = Conversion.ParseInt(prm.Last<SqlParameter>().Value);

                }
            }
            catch (Exception ex)
            {
                checkBit = 0;
                new SqlLog().InsertSqlLog(0, "PTEStandards.AddSetting", ex);
            }
            return checkBit;

        }
        public static void UpdateSetting(PTEStandards objPTE)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@PTEStandardId", SqlDbType.Int, 4, objPTE.PteStandardId));
                    prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, objPTE.StateId));
                    prm.Add(DB.MakeInParam("@SizeId", SqlDbType.Int, 4, objPTE.SizeId));
                    prm.Add(DB.MakeInParam("@EffectiveDate", SqlDbType.DateTime, 8, objPTE.EffectiveDate));
                    prm.Add(DB.MakeInParam("@ExpirationDate", SqlDbType.DateTime, 8, objPTE.ExpirationDate));
                    prm.Add(DB.MakeInParam("@DollarValue", SqlDbType.Float, 8, objPTE.DollarValue));
                    prm.Add(DB.MakeInParam("@UpdatedDate", SqlDbType.DateTime, 8, objPTE.UpdatedDate));
                    prm.Add(DB.MakeInParam("@UpdatedByUserId", SqlDbType.Int, 4, objPTE.UpdatedByUserId));

                    DB.RunProc("up_PTEStandard_Edit", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "PTEStandard.UpdateSetting", ex);
            }
        }

        public static DataSet GetAllSizes()
        {
            DataSet ds = null;
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    ds = Db.GetDataSet("up_GetSizeCodes");
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "PTE.GetAllSizes", ex);
            }

            return ds;
        }
    }

}
