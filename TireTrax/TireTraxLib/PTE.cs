using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
    public class PTE
    {
        #region PTE

        private int _stateId;

        public int StateId
        {
            get { return _stateId; }
            set { _stateId = value; }
        }

        private int _pteId;

        public int PteId
        {
            get { return _pteId; }
            set { _pteId = value; }
        }
        private String _stakeholderType;

        public String StakeholderType
        {
            get { return _stakeholderType; }
            set { _stakeholderType = value; }
        }

        private int _OrganizationSubTypeId;

        public int OrganizationSubTypeId
        {
            get { return _OrganizationSubTypeId; }
            set { _OrganizationSubTypeId = value; }
        }

        private int _SizeId;

        public int SizeId
        {
            get { return _SizeId; }
            set { _SizeId = value; }
        }
        private int _ShapeId;

        public int ShapeId
        {
            get { return _ShapeId; }
            set { _ShapeId = value; }
        }
        private int _MaterialId;

        public int MaterialId
        {
            get { return _MaterialId; }
            set { _MaterialId = value; }
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

        private string _TireSize;

        public string TireSize
        {
            get { return _TireSize; }
            set { _TireSize = value; }
        }

        private int _OrganizationId;

        public int OrganizationId
        {
            get { return _OrganizationId; }
            set { _OrganizationId = value; }
        }

        private int _ProductCategoryId;

        public int ProductCategoryId
        {
            get { return _ProductCategoryId; }
            set { _ProductCategoryId = value; }
        }

        private int _ProductSubCategoryId;

        public int ProductSubCategoryId
        {
            get { return _ProductSubCategoryId; }
            set { _ProductSubCategoryId = value; }
        }

        #endregion

        public PTE()
        {

        }
        public PTE(int pteId)
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
                    prams[0] = db.MakeInParam("@PTEId", SqlDbType.Int, 0, pteId);
                    reader = db.GetDataReader("up_PTE_getById", prams);
                    if (reader.Read())
                        Load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "PTE.Load", e);
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
                _pteId = Conversion.ParseDBNullInt(reader["PTEId"]);
                _OrganizationSubTypeId = Conversion.ParseDBNullInt(reader["OrganizationSubTypeId"]);
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
                _stakeholderType = Conversion.ParseDBNullString(reader["StakeholderType"]);
                _TireSize = Conversion.ParseDBNullString(reader["TireSize"]);

            }
            catch (Exception ex)
            {

                new SqlLog().InsertSqlLog(0, "PTE.Load", ex);
            }


        }


        public static DataSet getSetting(int LanguageId, int stateid, int PageId, int pageSize ,out int iTotalrows)
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
                    //prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 0, CatId));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_PTE_Select", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "PTE.getSetting", exp);
            }
            return ds;
        }

        public static DataSet getSettingForProduct(int LanguageId, int stateid, int PageId, int pageSize,int CatId, out int iTotalrows)
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
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 0, CatId));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_PTE_Select_Product", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "PTE.getSettingForProduct", exp);
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
                    prm.Add(DB.MakeInParam("@PTEId", SqlDbType.Int, 4, pteID));
                    prm.Add(DB.MakeInParam("@UpdatedByUserId", SqlDbType.Int, 4, UpdateByUserId));
                    prm.Add(DB.MakeInParam("@UpdatedDate", SqlDbType.DateTime, 8, UpdatedDate));

                    DB.RunProc("up_PTE_delete", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "PTE.DeleteSetting", ex);
            }

        }

        public static int AddSetting(PTE objPTE)
        {
            int checkBit;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, objPTE.StateId));
                    prm.Add(DB.MakeInParam("@OrganizationSubTypeId", SqlDbType.Int, 0, objPTE.OrganizationSubTypeId));
                    prm.Add(DB.MakeInParam("@SizeId", SqlDbType.Int, 0, objPTE.SizeId));
                    //if (objPTE.ShapeId != 0)
                    //    prm.Add(DB.MakeInParam("@ShapeId", SqlDbType.Int, 0, objPTE.ShapeId));
                    //else
                    //    prm.Add(DB.MakeInParam("@ShapeId", SqlDbType.Int, 0, DBNull.Value));

                    //if (objPTE.MaterialId != 0)
                    //    prm.Add(DB.MakeInParam("@MaterialId", SqlDbType.Int, 0, objPTE.MaterialId));
                    //else
                    //    prm.Add(DB.MakeInParam("@MaterialId", SqlDbType.Int, 0, DBNull.Value));
                    
                   
                    prm.Add(DB.MakeInParam("@EffectiveDate", SqlDbType.DateTime, 0, objPTE.EffectiveDate));
                    prm.Add(DB.MakeInParam("@ExpirationDate", SqlDbType.DateTime, 0, objPTE.ExpirationDate));
                    prm.Add(DB.MakeInParam("@DollarValue", SqlDbType.Float, 0, objPTE.DollarValue));
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 0, objPTE.LanguageId));
                    prm.Add(DB.MakeInParam("@CreatedDate", SqlDbType.DateTime, 0, objPTE.CreatedDate));
                    prm.Add(DB.MakeInParam("@CreatedByUserId", SqlDbType.Int, 0, objPTE.CreatedByUserId));
                   // prm.Add(DB.MakeReturnParam(SqlDbType.Int, 0));
                    //prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 0, CatId));
                    prm.Add(DB.MakeOutParam("@bit", SqlDbType.Int, 4));
                checkBit= DB.RunProc("up_PTE_add", prm.ToArray());
                 checkBit  = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                   
                }
            }
            catch (Exception ex)
            {
                checkBit = 0;
                new SqlLog().InsertSqlLog(0, "PTE.AddSetting", ex);
            }
            return checkBit;

        }

        public static int AddSettingProduct(PTE objPTE)
        {
            int checkBit;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@PteId", SqlDbType.Int, 4, objPTE.PteId));
                    prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, objPTE.StateId));
                    prm.Add(DB.MakeInParam("@OrganizationSubTypeId", SqlDbType.Int, 0, objPTE.OrganizationSubTypeId));
                    prm.Add(DB.MakeInParam("@SizeId", SqlDbType.Int, 0, objPTE.SizeId));
                    //if (objPTE.ShapeId != 0)
                    prm.Add(DB.MakeInParam("@ShapeId", SqlDbType.Int, 0, objPTE.ShapeId));
                    //else
                    //    prm.Add(DB.MakeInParam("@ShapeId", SqlDbType.Int, 0, DBNull.Value));

                    //if (objPTE.MaterialId != 0)
                    prm.Add(DB.MakeInParam("@MaterialId", SqlDbType.Int, 0, objPTE.MaterialId));
                    //else
                    //    prm.Add(DB.MakeInParam("@MaterialId", SqlDbType.Int, 0, DBNull.Value));


                    prm.Add(DB.MakeInParam("@EffectiveDate", SqlDbType.DateTime, 0, objPTE.EffectiveDate));
                    prm.Add(DB.MakeInParam("@ExpirationDate", SqlDbType.DateTime, 0, objPTE.ExpirationDate));
                    prm.Add(DB.MakeInParam("@DollarValue", SqlDbType.Float, 0, objPTE.DollarValue));
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 0, objPTE.LanguageId));
                    prm.Add(DB.MakeInParam("@CreatedDate", SqlDbType.DateTime, 0, objPTE.CreatedDate));
                    prm.Add(DB.MakeInParam("@CreatedByUserId", SqlDbType.Int, 0, objPTE.CreatedByUserId));
                    // prm.Add(DB.MakeReturnParam(SqlDbType.Int, 0));
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 0, objPTE.ProductCategoryId));
                    prm.Add(DB.MakeInParam("@SubCatId", SqlDbType.Int, 0, objPTE.ProductSubCategoryId));
                    prm.Add(DB.MakeOutParam("@bit", SqlDbType.Int, 4));
                    checkBit = DB.RunProc("up_PTE_add_Product", prm.ToArray());
                    checkBit = Conversion.ParseInt(prm.Last<SqlParameter>().Value);

                }
            }
            catch (Exception ex)
            {
                checkBit = 0;
                new SqlLog().InsertSqlLog(0, "PTE.AddSettingProduct", ex);
            }
            return checkBit;

        }
        public static int UpdateSetting(PTE objPTE)
        {
            int chkBit;


            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@PTEId", SqlDbType.Int, 4, objPTE.PteId));
                    prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, objPTE.StateId));
                    prm.Add(DB.MakeInParam("@OrganizationSubTypeId", SqlDbType.Int, 4, objPTE.OrganizationSubTypeId));
                    prm.Add(DB.MakeInParam("@SizeId", SqlDbType.Int, 4, objPTE.SizeId));
                    prm.Add(DB.MakeInParam("@EffectiveDate", SqlDbType.DateTime, 8, objPTE.EffectiveDate));
                    prm.Add(DB.MakeInParam("@ExpirationDate", SqlDbType.DateTime, 8, objPTE.ExpirationDate));
                    prm.Add(DB.MakeInParam("@DollarValue", SqlDbType.Float, 8, objPTE.DollarValue));
                    prm.Add(DB.MakeInParam("@UpdatedDate", SqlDbType.DateTime, 8, objPTE.UpdatedDate));
                    prm.Add(DB.MakeInParam("@UpdatedByUserId", SqlDbType.Int, 4, objPTE.UpdatedByUserId));
                    
                    prm.Add(DB.MakeOutParam("@bit", SqlDbType.Int, 4));
                    
                    chkBit = DB.RunProc("up_PTE_Edit", prm.ToArray());
                    chkBit = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                }
            }
            catch (Exception ex)
            {
                chkBit = 0;
                new SqlLog().InsertSqlLog(0, "PTE.UpdateSetting", ex);
            }

            return chkBit;
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

        //Commented by Muhammad Omer
        //public static DataSet GetAllSizes(int CatId)
        //{
        //    DataSet ds = null;
        //    List<SqlParameter> prm = new List<SqlParameter>();
        //    try
        //    {
        //        using (DbManager Db = DbManager.GetDbManager())
        //        {
        //            prm.Add(Db.MakeInParam("@CategoryId", SqlDbType.Int, 0, CatId));
        //            ds = Db.GetDataSet("up_GetSizeCodes", prm.ToArray());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new SqlLog().InsertSqlLog(0, "PTE.GetAllSizes", ex);
        //    }

        //    return ds;
        //}


        public static DataSet GetDatasetForProductAndSubtype(int productId, int SubtypeId,int stateId)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@productId", SqlDbType.Int, 4, productId));
                    prm.Add(DB.MakeInParam("@SubtypeId", SqlDbType.Int, 4, SubtypeId));
                    prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, stateId));
                    ds = DB.GetDataSet("Up_GetDatasetForProductAndSubtype", prm.ToArray());
                }
            }
            catch(Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "PTE.GetDatasetForProductAndSubtype", ex);
            }
            return ds;
        }

        public static DataSet getSettingForProductAndSubtype(int LanguageId, int stateid, int PageId, int pageSize, int CatId,int SubtypeId, out int iTotalrows)
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
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 0, CatId));
                    prm.Add(DB.MakeInParam("@SubtypeId", SqlDbType.Int, 0, SubtypeId));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_PTE_Select_Product_Subtype", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "PTE.getSettingForProduct", exp);
            }
            return ds;
        }
    }
}
