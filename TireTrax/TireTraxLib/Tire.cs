using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
    public class Tire
    {
        #region Properties
        private int _TireId;

        public int TireId
        {
            get { return _TireId; }
            set { _TireId = value; }
        }

        private int _TX_BarCodeId;

        public int TX_BarCodeId
        {
            get { return _TX_BarCodeId; }
            set { _TX_BarCodeId = value; }
        }

        private DateTime _DateCreated;

        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }

        private int _CreatedById;

        public int CreatedById
        {
            get { return _CreatedById; }
            set { _CreatedById = value; }
        }

        private string _DotNumber;

        public string DotNumber
        {
            get { return _DotNumber; }
            set { _DotNumber = value; }
        }

        private string _SizeNumber;

        public string SizeNumber
        {
            get { return _SizeNumber; }
            set { _SizeNumber = value; }
        }

        private int _C_BarCode;

        public int C_BarCode
        {
            get { return _C_BarCode; }
            set { _C_BarCode = value; }
        }

        private string _PlantCode;

        public string PlantCode
        {
            get { return _PlantCode; }
            set { _PlantCode = value; }
        }

        private string _TireType;

        public string TireType
        {
            get { return _TireType; }
            set { _TireType = value; }
        }

        private int _BrandId1;

        public int BrandId1
        {
            get { return _BrandId1; }
            set { _BrandId1 = value; }
        }

        private int _BrandId2;

        public int BrandId2
        {
            get { return _BrandId2; }
            set { _BrandId2 = value; }
        }
        private string _BrandCode;
        public string BrandCode
        {
            get { return _BrandCode; }
            set { _BrandCode = value; }
        }
        private string _MonthCode;

        public string MonthCode
        {
            get { return _MonthCode; }
            set { _MonthCode = value; }
        }

        private string _YearCode;

        public string YearCode
        {
            get { return _YearCode; }
            set { _YearCode = value; }
        }

        private int _LangaugeId;

        public int LangaugeId
        {
            get { return _LangaugeId; }
            set { _LangaugeId = value; }
        }

        private int _RecycleStateId;

        public int RecycleStateId
        {
            get { return _RecycleStateId; }
            set { _RecycleStateId = value; }
        }

        private int _RetreadStateId;

        public int RetreadStateId
        {
            get { return _RetreadStateId; }
            set { _RetreadStateId = value; }
        }

        private int _TireStateCategoryId;

        public int TireStateCategoryId
        {
            get { return _TireStateCategoryId; }
            set { _TireStateCategoryId = value; }
        }
        private int _TireClassId;

        public int TireClassId
        {
            get { return _TireClassId; }
            set { _TireClassId = value; }
        }
        private int _OrganizationId;

        public int OrganizationId
        {
            get { return _OrganizationId; }
            set { _OrganizationId = value; }
        }


        private byte[] _Image;

        public byte[] Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private string _SerialNumber;

        public string SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = value; }
        }

        private string _lane;

        public string Lane
        {
            get { return _lane; }
            set { _lane = value; }
        }

        private string _space;

        public string Space
        {
            get { return _space; }
            set { _space = value; }
        }
        private string _plantName;
        public string PlantName
        {
            get { return _plantName; }
            set { _plantName = value; }
        }

        private string _brand2Name;
        public string Brand2Name
        {
            get { return _brand2Name; }
            set { _brand2Name = value; }
        }
        private string _brand1Name;
        public string Brand1Name
        {
            get { return _brand1Name; }
            set { _brand1Name = value; }
        }
        private Byte[] _barcodeImage;

        public Byte[] BarcodeImage
        {
            get { return _barcodeImage; }
            set { _barcodeImage = value; }
        }

        private string _className;

        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        private string _tireStateCategory;

        public string TireStateCategory
        {
            get { return _tireStateCategory; }
            set { _tireStateCategory = value; }
        }

        private int _tireActionId;

        public int TireActionId
        {
            get { return _tireActionId; }
            set { _tireActionId = value; }
        }
        private string _tireActionName;

        public string TireActionName
        {
            get { return _tireActionName; }
            set { _tireActionName = value; }
        }
        private int _tireOutComeID;

        public int TireOutComeID
        {
            get { return _tireOutComeID; }
            set { _tireOutComeID = value; }
        }
        private string _tireOutComeName;

        public string TireOutComeName
        {
            get { return _tireOutComeName; }
            set { _tireOutComeName = value; }
        }
        private string _tireSize;

        public string TireSize
        {
            get { return _tireSize; }
            set { _tireSize = value; }
        }
        #endregion


        public Tire() { }
        public Tire(int tireID)
        {
            Load(tireID);
        }
        private void Load(int tireID)
        {
            IDataReader reader = null;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@tireID", SqlDbType.BigInt, 20, tireID));

                    reader = db.GetDataReader("up_GetTireInfo_ByTireID", prams.ToArray());
                    if (reader.Read())
                        Load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Tire.Load", e);
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

                _TireId = Conversion.ParseDBNullInt(reader["ProductId"]);
                _C_BarCode = Conversion.ParseDBNullInt(reader["C-BarCode"]);
                _DotNumber = Conversion.ParseDBNullString(reader["DOTNumber"]);
                _PlantCode = Conversion.ParseDBNullString(reader["PlantNumber"]);
                _SizeNumber = Conversion.ParseDBNullString(reader["SizeNumber"]);
                _plantName = Conversion.ParseDBNullString(reader["Brand"]);
                _BrandId2 = Conversion.ParseDBNullInt(reader["BrandId2"]);
                _brand1Name = Conversion.ParseDBNullString(reader["Brand"]);
                _BrandCode = Conversion.ParseDBNullString(reader["brandCode"]);
                _brand2Name = Conversion.ParseDBNullString(reader["Brand2"]);
                _MonthCode = Conversion.ParseDBNullString(reader["MonthCode"]);
                _YearCode = Conversion.ParseDBNullString(reader["YearCode"]);
                _TX_BarCodeId = Conversion.ParseDBNullInt(reader["BarCodeId"]);
                _barcodeImage = reader["BarcodeImage"] == null ? null : (Byte[])reader["BarcodeImage"];
                _SerialNumber = Conversion.ParseDBNullString(reader["SerialNumber"]);
                _TireClassId = Conversion.ParseDBNullInt(reader["intClassID"]);
                _className = Conversion.ParseDBNullString(reader["ClassName"]);
                //_TireStateCategoryId = Conversion.ParseDBNullInt(reader["RecycleActionID"]);
                //_tireStateCategory = Conversion.ParseDBNullString(reader["RecycleActionText"]);
                _tireActionId = Conversion.ParseDBNullInt(reader["intActionID"]);
                _tireActionName = Conversion.ParseDBNullString(reader["ActionName"]);
                _tireOutComeID = Conversion.ParseDBNullInt(reader["intOutcomeID"]);
                _tireOutComeName = Conversion.ParseDBNullString(reader["OutComeName"]);
                _tireSize = Conversion.ParseDBNullString(reader["ProductSize"]);
            }
            catch (Exception ex)
            {

                new SqlLog().InsertSqlLog(0, "Tire.Load", ex);
            }


        }
        public static DataSet AdminAllInventory(int pageId, int pageSize, out int iTotalrows)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 0));

                    ds = DB.GetDataSet("up_Inventory_AllInventory", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.AdminAllInventory", ex);
            }
            return ds;


        }


        public static DataSet AdminInventoryRevenue(int pageId, int pageSize, out int iTotalrows, int tireid, String OrganizationName, String UserName)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    if (OrganizationName == "")
                        prm.Add(DB.MakeInParam("@organization", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@organization", SqlDbType.NVarChar, 100, OrganizationName));

                    if (UserName == "")
                        prm.Add(DB.MakeInParam("@username", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@username", SqlDbType.NVarChar, 100, UserName));

                    prm.Add(DB.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));
                    prm.Add(DB.MakeInParam("@TireId", SqlDbType.Int, 0, tireid));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 0));


                    ds = DB.GetDataSet("up_RevenueByTireId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.AdminInventoryRevenue", ex);
            }
            return ds;


        }

        public static DataSet SearchInventory(int pageId, int pageSize, out int iTotalrows, String TX_barcode, string Organization, string PlantCode,
             string SizeCode, int TireState, int status, String lot = "", String size = "", String brand = "")
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

                    if (TX_barcode == "")
                        prm.Add(DB.MakeInParam("@tx_barcodenumber", SqlDbType.NVarChar, 200, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@tx_barcodenumber", SqlDbType.NVarChar, 200, TX_barcode));

                    if (Organization == "")
                        prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.NVarChar, 255, Organization));

                    if (PlantCode == "")
                        prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.Char, 10, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.Char, 10, PlantCode));

                    if (SizeCode == "")
                        prm.Add(DB.MakeInParam("@SizeCode", SqlDbType.NVarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@SizeCode", SqlDbType.NVarChar, 50, SizeCode));

                    if (TireState == 0)
                        prm.Add(DB.MakeInParam("@TireState", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@TireState", SqlDbType.Int, 4, TireState));

                    if (status == 0)
                        prm.Add(DB.MakeInParam("@status", SqlDbType.Bit, 1, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@status", SqlDbType.Bit, 1, status));
                    if (String.IsNullOrEmpty(lot))
                        prm.Add(DB.MakeInParam("@vchlot", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchlot", SqlDbType.NVarChar, 100, lot));
                    if (String.IsNullOrEmpty(lot))
                        prm.Add(DB.MakeInParam("@vchsize", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchsize", SqlDbType.NVarChar, 100, size));
                    if (String.IsNullOrEmpty(lot))
                        prm.Add(DB.MakeInParam("@vchbrand", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchbrand", SqlDbType.NVarChar, 100, brand));


                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_Inventory_SearchInventory", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.SearchInventory1", ex);
            }
            return ds;
        }

        public static DataSet SearchInventory(int pageId, int pageSize, out int iTotalrows, String TX_barcode, int OrganizationId, string PlantCode,
            string SizeCode, int TireState, int status, string userName, int quantity, string space, string lane, string guid, string lat, string log
            , DateTime frmDate, DateTime toDate, string lot, string size, string brand)
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

                    if (TX_barcode == "")
                        prm.Add(DB.MakeInParam("@tx_barcodenumber", SqlDbType.NVarChar, 200, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@tx_barcodenumber", SqlDbType.NVarChar, 200, TX_barcode));

                    if (OrganizationId == 0)
                        prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));

                    if (PlantCode == "")
                        prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.Char, 10, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.Char, 10, PlantCode));

                    if (SizeCode == "")
                        prm.Add(DB.MakeInParam("@SizeCode", SqlDbType.NVarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@SizeCode", SqlDbType.NVarChar, 50, SizeCode));

                    if (TireState == 0)
                        prm.Add(DB.MakeInParam("@TireState", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@TireState", SqlDbType.Int, 4, TireState));

                    if (status == 0)
                        prm.Add(DB.MakeInParam("@status", SqlDbType.Bit, 1, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@status", SqlDbType.Bit, 1, status));

                    if (string.IsNullOrEmpty(userName))
                        prm.Add(DB.MakeInParam("@UserName", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@UserName", SqlDbType.NVarChar, 255, userName));
                    if (string.IsNullOrEmpty(space))
                        prm.Add(DB.MakeInParam("@vchSpace", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchSpace", SqlDbType.NVarChar, 100, space));
                    if (string.IsNullOrEmpty(lane))
                        prm.Add(DB.MakeInParam("@vchLane", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchLane", SqlDbType.NVarChar, 100, lane));
                    if (string.IsNullOrEmpty(lat))
                        prm.Add(DB.MakeInParam("@vchLat", SqlDbType.Decimal, 8, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchLat", SqlDbType.Decimal, 8, Convert.ToDateTime(lat)));
                    if (string.IsNullOrEmpty(log))
                        prm.Add(DB.MakeInParam("@vchLong", SqlDbType.Decimal, 8, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchLong", SqlDbType.Decimal, 8, Convert.ToDateTime(log)));
                    if (string.IsNullOrEmpty(guid))
                        prm.Add(DB.MakeInParam("@vchGuid", SqlDbType.NVarChar, 500, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchGuid", SqlDbType.NVarChar, 500, guid));

                    if (frmDate == DateTime.MinValue)
                        prm.Add(DB.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 8, frmDate));
                    if (toDate == DateTime.MinValue)
                        prm.Add(DB.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 8, toDate));

                    if (quantity == 0)
                        prm.Add(DB.MakeInParam("@intQuantity", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@intQuantity", SqlDbType.Int, 4, quantity));
                    if (String.IsNullOrEmpty(lot))
                        prm.Add(DB.MakeInParam("@vchlot", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchlot", SqlDbType.NVarChar, 100, lot));
                    if (String.IsNullOrEmpty(lot))
                        prm.Add(DB.MakeInParam("@vchsize", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchsize", SqlDbType.NVarChar, 100, size));
                    if (String.IsNullOrEmpty(lot))
                        prm.Add(DB.MakeInParam("@vchbrand", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchbrand", SqlDbType.NVarChar, 100, brand));


                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_Inventory_SearchInventory", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.SearchInventory2", ex);
            }
            return ds;
        }

        //public static DataSet SearchLotInventory(int pageId, int pageSize, int lotId, out int iTotalrows, String TX_barcode, string Organization, string PlantCode, string SizeCode, int TireState, int status, int organizationId)
        //{
        //    DataSet ds = null;
        //    iTotalrows = 0;
        //    List<SqlParameter> prm = new List<SqlParameter>();
        //    try
        //    {
        //        using (DbManager DB = DbManager.GetDbManager())
        //        {
        //            prm.Add(DB.MakeInParam("@intPageId", SqlDbType.Int, 4, pageId));
        //            prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 4, pageSize));
        //            if (lotId > 0)
        //                prm.Add(DB.MakeInParam("@intlotId", SqlDbType.Int, 4, lotId));
        //            else
        //                prm.Add(DB.MakeInParam("@intlotId", SqlDbType.Int, 4, DBNull.Value));
        //            if (string.IsNullOrEmpty(TX_barcode))
        //                prm.Add(DB.MakeInParam("@tx_barcodenumber", SqlDbType.NVarChar, 200, DBNull.Value));
        //            else
        //                prm.Add(DB.MakeInParam("@tx_barcodenumber", SqlDbType.NVarChar, 200, TX_barcode));

        //            if (string.IsNullOrEmpty(Organization))
        //                prm.Add(DB.MakeInParam("@Organization", SqlDbType.NVarChar, 255, DBNull.Value));
        //            else
        //                prm.Add(DB.MakeInParam("@Organization", SqlDbType.NVarChar, 255, Organization));

        //            if (string.IsNullOrEmpty(PlantCode))
        //                prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.Char, 10, DBNull.Value));
        //            else
        //                prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.Char, 10, PlantCode));

        //            if (string.IsNullOrEmpty(SizeCode))
        //                prm.Add(DB.MakeInParam("@SizeCode", SqlDbType.NVarChar, 50, DBNull.Value));
        //            else
        //                prm.Add(DB.MakeInParam("@SizeCode", SqlDbType.NVarChar, 50, SizeCode));

        //            if (TireState == 0)
        //                prm.Add(DB.MakeInParam("@TireState", SqlDbType.Int, 4, DBNull.Value));
        //            else
        //                prm.Add(DB.MakeInParam("@TireState", SqlDbType.Int, 4, TireState));

        //            if (status == 0)
        //                prm.Add(DB.MakeInParam("@status", SqlDbType.Bit, 1, DBNull.Value));
        //            else
        //                prm.Add(DB.MakeInParam("@status", SqlDbType.Bit, 1, status));
        //            if (organizationId > 0)
        //                prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 4, organizationId));
        //            else
        //                prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 4, DBNull.Value));

        //            prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

        //            ds = DB.GetDataSet("[up_Inventory_SearchLotInventory]", prm.ToArray());
        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
        //                return ds;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new SqlLog().InsertSqlLog(0, "Tire.SearchLotInventory", ex);
        //    }
        //    return ds;
        //}

        public static DataSet SearchTireInventory(int pageId, int pageSize, int lotId, out int iTotalrows, String TX_barcode, string Organization, string PlantCode, string SizeCode, int TireState, int status)
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
                    if (lotId > 0)
                        prm.Add(DB.MakeInParam("@intlotId", SqlDbType.Int, 4, lotId));
                    else
                        prm.Add(DB.MakeInParam("@intlotId", SqlDbType.Int, 4, DBNull.Value));
                    if (string.IsNullOrEmpty(TX_barcode))
                        prm.Add(DB.MakeInParam("@tx_barcodenumber", SqlDbType.NVarChar, 200, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@tx_barcodenumber", SqlDbType.NVarChar, 200, TX_barcode));

                    if (string.IsNullOrEmpty(Organization))
                        prm.Add(DB.MakeInParam("@Organization", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@Organization", SqlDbType.NVarChar, 255, Organization));

                    if (string.IsNullOrEmpty(PlantCode))
                        prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.Char, 10, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.Char, 10, PlantCode));

                    if (string.IsNullOrEmpty(SizeCode))
                        prm.Add(DB.MakeInParam("@SizeCode", SqlDbType.NVarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@SizeCode", SqlDbType.NVarChar, 50, SizeCode));

                    if (TireState == 0)
                        prm.Add(DB.MakeInParam("@TireState", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@TireState", SqlDbType.Int, 4, TireState));

                    if (status == 0)
                        prm.Add(DB.MakeInParam("@status", SqlDbType.Bit, 1, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@status", SqlDbType.Bit, 1, status));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("[up_Inventory_SearchTireInventory]", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.SearchTireInventory", ex);
            }
            return ds;
        }

        public static DataSet SearchTireInventoryByLotIds(int pageId, int pageSize, string lotIds, out int iTotalrows, String TX_barcode, string Organization, string PlantCode, string SizeCode, int TireState, int status)
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
                    if (!string.IsNullOrEmpty(lotIds))
                        prm.Add(DB.MakeInParam("@intlotIds", SqlDbType.NVarChar, 255, lotIds));
                    else
                        prm.Add(DB.MakeInParam("@intlotIds", SqlDbType.NVarChar, 255, DBNull.Value));
                    if (string.IsNullOrEmpty(TX_barcode))
                        prm.Add(DB.MakeInParam("@tx_barcodenumber", SqlDbType.NVarChar, 200, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@tx_barcodenumber", SqlDbType.NVarChar, 200, TX_barcode));

                    if (string.IsNullOrEmpty(Organization))
                        prm.Add(DB.MakeInParam("@Organization", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@Organization", SqlDbType.NVarChar, 255, Organization));

                    if (string.IsNullOrEmpty(PlantCode))
                        prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.Char, 10, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.Char, 10, PlantCode));

                    if (string.IsNullOrEmpty(SizeCode))
                        prm.Add(DB.MakeInParam("@SizeCode", SqlDbType.NVarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@SizeCode", SqlDbType.NVarChar, 50, SizeCode));

                    if (TireState == 0)
                        prm.Add(DB.MakeInParam("@TireState", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@TireState", SqlDbType.Int, 4, TireState));

                    if (status == 0)
                        prm.Add(DB.MakeInParam("@status", SqlDbType.Bit, 1, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@status", SqlDbType.Bit, 1, status));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("[up_SearchTireInventoryByLotIds]", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.SearchTireInventoryByLotIds", ex);
            }
            return ds;
        }


        public static DataSet getTireBySpaceId(int spaceid)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intlaneid", SqlDbType.Int, 0, spaceid));


                    ds = DB.GetDataSet("up_getTiresbylaneId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.getTirebySpaceId", ex);
            }
            return ds;
        }

        public static int addNewInventory(Tire objInventory)
        {
            int returnValue = 0;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("TX_BarCodeId", SqlDbType.BigInt, 8, objInventory.TX_BarCodeId));
                    prams.Add(Db.MakeInParam("SerialNumber", SqlDbType.NVarChar, 12, objInventory.SerialNumber));
                    prams.Add(Db.MakeInParam("DateCreated", SqlDbType.DateTime, 8, objInventory.DateCreated));
                    prams.Add(Db.MakeInParam("CreatedById", SqlDbType.Int, 4, objInventory.CreatedById));
                    prams.Add(Db.MakeInParam("C_BarCode", SqlDbType.BigInt, 8, objInventory.C_BarCode));
                    prams.Add(Db.MakeInParam("SizeNumber", SqlDbType.NVarChar, 12, objInventory.SizeNumber));
                    prams.Add(Db.MakeInParam("DotNumber", SqlDbType.NVarChar, 12, objInventory.DotNumber));
                    prams.Add(Db.MakeInParam("PlantNumber", SqlDbType.Char, 10, objInventory.PlantCode));
                    prams.Add(Db.MakeInParam("TireType", SqlDbType.NVarChar, 10, objInventory.TireType));
                    prams.Add(Db.MakeInParam("BrandId", SqlDbType.Int, 4, objInventory.BrandId1));
                    prams.Add(Db.MakeInParam("BrandId2", SqlDbType.Int, 4, objInventory.BrandId2));
                    prams.Add(Db.MakeInParam("MonthCode", SqlDbType.NVarChar, 2, objInventory.MonthCode));
                    prams.Add(Db.MakeInParam("YearCode", SqlDbType.NVarChar, 4, objInventory.YearCode));
                    prams.Add(Db.MakeInParam("LangaugeId", SqlDbType.Int, 4, objInventory.LangaugeId));
                    //prams.Add(Db.MakeInParam("TireStateCategoryId", SqlDbType.Int, 4, objInventory.TireStateCategoryId));

                    //if (objInventory.RecycleStateId <= 0)
                    //    prams.Add(Db.MakeInParam("RecycleStateId", SqlDbType.Int, 4, DBNull.Value));
                    //else
                    //    prams.Add(Db.MakeInParam("RecycleStateId", SqlDbType.Int, 4, objInventory.RecycleStateId));

                    //if (objInventory.RetreadStateId <= 0)
                    //    prams.Add(Db.MakeInParam("RetreadStateId", SqlDbType.Int, 4, DBNull.Value));
                    //else
                    //    prams.Add(Db.MakeInParam("RetreadStateId", SqlDbType.Int, 4, objInventory.RetreadStateId));

                    prams.Add(Db.MakeInParam("OrganizationId", SqlDbType.Int, 4, objInventory.OrganizationId));
                    prams.Add(Db.MakeInParam("Image", SqlDbType.VarBinary, -1, objInventory.Image));
                    if (string.IsNullOrEmpty(objInventory.Space))
                        prams.Add(Db.MakeInParam("@vchSpace", SqlDbType.NVarChar, 300, DBNull.Value));
                    else
                        prams.Add(Db.MakeInParam("@vchSpace", SqlDbType.NVarChar, 300, objInventory.Space));
                    if (string.IsNullOrEmpty(objInventory.Lane))
                        prams.Add(Db.MakeInParam("@vchLane", SqlDbType.NVarChar, 300, DBNull.Value));
                    else
                        prams.Add(Db.MakeInParam("@vchLane", SqlDbType.NVarChar, 300, objInventory.Lane));
                    prams.Add(Db.MakeInParam("TireClassId", SqlDbType.Int, 4, objInventory.TireClassId));
                    prams.Add(Db.MakeInParam("@TireActionId", SqlDbType.Int, 0, objInventory.TireActionId));
                    prams.Add(Db.MakeInParam("@TireOutcomeId", SqlDbType.Int, 0, objInventory.TireOutComeID));

                    returnValue = Db.RunProc("up_Inventory_AddNew", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "AddNewInventory", ex);
            }
            return returnValue;
        }



        public static byte[] GetBarCodeImage(long BarCodeId)
        {
            byte[] image = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("BarCodeId", SqlDbType.BigInt, 8, BarCodeId));
                    image = Db.ExecuteScalar<byte[]>("up_BarCode_GetImageByBarCodeId", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.GetBarCodeImage", ex);
            }
            return image;
        }

        public static bool ValidatePlantCode(string PlantCode, out string PlantName)
        {
            bool IsPlantCodeValid = false;
            DataSet ds = null;
            int iTotalrows = 0;
            PlantName = string.Empty;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("PlantCode", SqlDbType.NChar, 20, PlantCode));
                    prams.Add(Db.MakeReturnParam(SqlDbType.Int, 4));

                    //IsPlantCodeValid = Convert.ToBoolean(Db.RunProc("up_Inventory_ValidatePlantCode", prams.ToArray()));

                    ds = Db.GetDataSet("up_Inventory_ValidatePlantCode", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        PlantName = Conversion.ParseDBNullString(ds.Tables[0].Rows[0]["PlantShortName"]);
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        if (iTotalrows > 0)
                            IsPlantCodeValid = true;
                        else
                            IsPlantCodeValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.ValidatePlantCode", ex);
            }
            return IsPlantCodeValid;
        }

        public static bool ValidateSizeCode(string SizeCode, out string Size)
        {
            bool IsPlantCodeValid = false;
            DataSet ds = null;
            int iTotalrows = 0;
            Size = string.Empty;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("SizeCode", SqlDbType.NVarChar, 20, SizeCode));
                    prams.Add(Db.MakeReturnParam(SqlDbType.Int, 4));
                    //IsPlantCodeValid = Convert.ToBoolean(Db.RunProc("up_Inventory_ValidateSizeCode", prams.ToArray()));
                    ds = Db.GetDataSet("up_Inventory_ValidateSizeCode", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Size = Conversion.ParseDBNullString(ds.Tables[0].Rows[0]["ProductSize"]);
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        if (iTotalrows > 0)
                            IsPlantCodeValid = true;
                        else
                            IsPlantCodeValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.ValidateSizeCode", ex);
            }
            return IsPlantCodeValid;
        }
        public static bool ValidateBrandCode(string BrandCode, out int BrandId, out string BrandName)
        {
            bool IsPlantCodeValid = false;
            DataSet ds = null;
            int iTotalrows = 0;
            BrandId = 0;
            BrandName = string.Empty;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@BrandCode", SqlDbType.NVarChar, 20, BrandCode));
                    prams.Add(Db.MakeReturnParam(SqlDbType.Int, 4));
                    //IsPlantCodeValid = Convert.ToBoolean(Db.RunProc("up_Inventory_ValidateBrandCode", prams.ToArray()));
                    ds = Db.GetDataSet("up_Inventory_ValidateBrandCode", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        BrandId = Conversion.ParseDBNullInt(ds.Tables[0].Rows[0]["BrandId"]);
                        BrandName = Conversion.ParseDBNullString(ds.Tables[0].Rows[0]["BrandName"]);
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        if (iTotalrows > 0)
                            IsPlantCodeValid = true;
                        else
                            IsPlantCodeValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.ValidateBrandCode", ex);
            }
            return IsPlantCodeValid;
        }

        public static bool ValidateBrandCodeAndInsertBrandCode(string BrandCode, string brandname, int countryid, out int BrandId, out string BrandName)
        {
            bool IsPlantCodeValid = false;
            DataSet ds = null;
            int iTotalrows = 0;
            BrandId = 0;
            BrandName = string.Empty;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@BrandCode", SqlDbType.NVarChar, 20, BrandCode));
                    prams.Add(Db.MakeInParam("@BrandName", SqlDbType.NVarChar, 99, brandname));
                    prams.Add(Db.MakeInParam("@countryId", SqlDbType.Int, 0, countryid));


                    prams.Add(Db.MakeReturnParam(SqlDbType.Int, 4));

                    ds = Db.GetDataSet("up_ValidateBrandCodeAndInsertBrand", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        BrandId = Conversion.ParseDBNullInt(ds.Tables[0].Rows[0]["BrandId"]);
                        BrandName = Conversion.ParseDBNullString(ds.Tables[0].Rows[0]["BrandName"]);
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        if (iTotalrows > 0)
                            IsPlantCodeValid = true;
                        else
                            IsPlantCodeValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.ValidateBrandCodeAndInsertBrandCode", ex);
            }
            return IsPlantCodeValid;
        }



        public static DataSet getTireStateCategory()
        {
            DataSet ds = null;
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    ds = Db.GetDataSet("up_Inventory_GetTireStateCategory");
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.GetTireStateCategory", ex);
            }

            return ds;
        }


        public static DataSet getInventoryTires(int pageId, int pageSize, int tireId, bool Islot, out int iTotalrows, String Dotnum, String Companyname, String Username, String todate, String fromdate, String lane, String space)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    if (String.IsNullOrEmpty(Dotnum))
                        prm.Add(DB.MakeInParam("@dotnumber", SqlDbType.NVarChar, 12, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@dotnumber", SqlDbType.NVarChar, 12, Dotnum));
                    if (String.IsNullOrEmpty(Companyname))
                        prm.Add(DB.MakeInParam("@organization", SqlDbType.NVarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@organization", SqlDbType.NVarChar, 50, Companyname));
                    if (String.IsNullOrEmpty(Username))
                        prm.Add(DB.MakeInParam("@username", SqlDbType.NVarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@username", SqlDbType.NVarChar, 50, Username));


                    if (String.IsNullOrEmpty(fromdate))
                        prm.Add(DB.MakeInParam("@fromdate", SqlDbType.DateTime, 0, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@fromdate", SqlDbType.DateTime, 0, Convert.ToDateTime(fromdate)));

                    if (String.IsNullOrEmpty(todate))
                        prm.Add(DB.MakeInParam("@todate", SqlDbType.DateTime, 0, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@todate", SqlDbType.DateTime, 0, Convert.ToDateTime(todate)));

                    //if (String.IsNullOrEmpty(lane))
                    //    prm.Add(DB.MakeInParam("@lane", SqlDbType.NVarChar, 50, DBNull.Value));
                    //else
                    //    prm.Add(DB.MakeInParam("@lane", SqlDbType.NVarChar, 50, lane));

                    //if (String.IsNullOrEmpty(space))
                    //    prm.Add(DB.MakeInParam("@space", SqlDbType.NVarChar, 50, DBNull.Value));
                    //else
                    //    prm.Add(DB.MakeInParam("@space", SqlDbType.NVarChar, 50, space));

                    prm.Add(DB.MakeInParam("@intPageId", SqlDbType.Int, 4, pageId));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 4, pageSize));
                    prm.Add(DB.MakeInParam("@tireid", SqlDbType.Int, 0, tireId));
                    prm.Add(DB.MakeInParam("@islot", SqlDbType.Bit, 0, Islot));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_Inventory_tire", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.GetInventoryTires", ex);
            }
            return ds;
        }



        public static DataSet inventoryTireByLotId(int LotId, int pageId, int pageSize, out int iTotalrows, string dotnumber, string tiresize, string brandname, string barcode, string spacename, string lotnumber, string lanename, DateTime fromtime, DateTime totime, string username)
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
                    prm.Add(DB.MakeInParam("@Lotid", SqlDbType.Int, 0, LotId));

                    if (String.IsNullOrEmpty(dotnumber))
                        prm.Add(DB.MakeInParam("@dotnumber", SqlDbType.NVarChar, 150, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@dotnumber", SqlDbType.NVarChar, 150, dotnumber));

                    if (String.IsNullOrEmpty(tiresize))
                        prm.Add(DB.MakeInParam("@tiresize", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@tiresize", SqlDbType.NVarChar, 100, tiresize));

                    if (String.IsNullOrEmpty(brandname))
                        prm.Add(DB.MakeInParam("@brandname", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@brandname", SqlDbType.NVarChar, 100, brandname));

                    if (String.IsNullOrEmpty(barcode))
                        prm.Add(DB.MakeInParam("@barcode", SqlDbType.NVarChar, 150, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@barcode", SqlDbType.NVarChar, 150, barcode));
                    if (String.IsNullOrEmpty(spacename))
                        prm.Add(DB.MakeInParam("@spacename", SqlDbType.NVarChar, 500, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@spacename", SqlDbType.NVarChar, 500, spacename));
                    if (String.IsNullOrEmpty(lotnumber))
                        prm.Add(DB.MakeInParam("@lotnumber", SqlDbType.NVarChar, 500, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@lotnumber", SqlDbType.NVarChar, 500, lotnumber));
                    if (String.IsNullOrEmpty(lanename))
                        prm.Add(DB.MakeInParam("@lanename", SqlDbType.NVarChar, 500, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@lanename", SqlDbType.NVarChar, 500, lanename));
                    if (String.IsNullOrEmpty(username))
                        prm.Add(DB.MakeInParam("@username", SqlDbType.NVarChar, 200, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@username", SqlDbType.NVarChar, 200, username));
                    if (fromtime == DateTime.MinValue || totime == DateTime.MinValue)
                    {
                        prm.Add(DB.MakeInParam("@fromdate", SqlDbType.DateTime, 0, DBNull.Value));
                        prm.Add(DB.MakeInParam("@todate", SqlDbType.DateTime, 0, DBNull.Value));
                    }
                    else
                    {
                        prm.Add(DB.MakeInParam("@fromdate", SqlDbType.DateTime, 0, fromtime));
                        prm.Add(DB.MakeInParam("@todate", SqlDbType.DateTime, 0, totime));


                    }

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_getinventories_tire_PagingbyLotId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.InventoryTireByTireId", ex);
            }
            return ds;
        }
        public static DataSet getAllTireByLotId(int LotId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intLotId", SqlDbType.Int, 0, LotId));



                    ds = DB.GetDataSet("up_getAllTirebyLotId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.getAllTireByLotId", ex);
            }
            return ds;
        }


        public static DataSet getAllTireClassTypes()
        {
            DataSet ds = null;


            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    ds = DB.GetDataSet("up_getInventoryClass_ddlLoad");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.getAllTireClassTypes", ex);
            }
            return ds;
        }



        public static DataSet getAllTireOutComeTypes()
        {
            DataSet ds = null;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    ds = DB.GetDataSet("up_getInventoryOutCome_ddlLoad");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.getAllTireOutComeTypes", ex);
            }
            return ds;
        }
        public static DataSet getAllTireActionTypes()
        {
            DataSet ds = null;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    ds = DB.GetDataSet("up_getInventoryAction_ddlLoad");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.getAllTireActionsTypes()", ex);
            }
            return ds;
        }


        public static DataSet getPermenentLotTiresCount(int OrganizationId,int CatId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 0, OrganizationId));
                    prm.Add(DB.MakeInParam("@intProductCatId", SqlDbType.Int, 4, CatId));
                    ds = DB.GetDataSet("up_getTotalPermenentLotTireCount", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.getPermenentLotTiresCount", ex);
            }
            return ds;
        }



        public static DataSet getTireInfoByTireID(int tireID)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@tireID", SqlDbType.BigInt, 20, tireID));
                    ds = Db.GetDataSet("up_GetTireInfo_ByTireID", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire GetTireInfoByTireID", ex);
            }
            return ds;
        }

        public static DataSet getCompleteTireInfo_ByLotID(int LotID)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@LotID", SqlDbType.BigInt, 20, LotID));
                    ds = Db.GetDataSet("up_GetCompleteTireInfo_ByLotID", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Inventory GetTireInfoByTireID", ex);
            }
            return ds;
        }
        public static int updateInventory(Tire objInventory, int OldTx_BarcodeID, int lotid, DateTime modifiedtime)
        {
            int returnValue = 0;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {


                    prams.Add(Db.MakeInParam("@TireId", SqlDbType.Int, 0, objInventory.TireId));
                    prams.Add(Db.MakeInParam("@OldTX_BarCodeId", SqlDbType.BigInt, 8, OldTx_BarcodeID));
                    prams.Add(Db.MakeInParam("@TX_BarCodeId", SqlDbType.BigInt, 8, objInventory.TX_BarCodeId));
                    prams.Add(Db.MakeInParam("@SerialNumber", SqlDbType.NVarChar, 12, objInventory.SerialNumber));
                    prams.Add(Db.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, objInventory.DateCreated));
                    prams.Add(Db.MakeInParam("@CreatedById", SqlDbType.Int, 4, objInventory.CreatedById));
                    prams.Add(Db.MakeInParam("@C_BarCode", SqlDbType.BigInt, 8, objInventory.C_BarCode));
                    prams.Add(Db.MakeInParam("@SizeNumber", SqlDbType.NVarChar, 12, objInventory.SizeNumber));
                    prams.Add(Db.MakeInParam("@DotNumber", SqlDbType.NVarChar, 12, objInventory.DotNumber));
                    prams.Add(Db.MakeInParam("@PlantNumber", SqlDbType.Char, 10, objInventory.PlantCode));
                    prams.Add(Db.MakeInParam("@TireType", SqlDbType.NVarChar, 10, objInventory.TireType));
                    prams.Add(Db.MakeInParam("@BrandId", SqlDbType.Int, 4, objInventory.BrandId1));
                    prams.Add(Db.MakeInParam("@BrandId2", SqlDbType.Int, 4, objInventory.BrandId2));
                    prams.Add(Db.MakeInParam("@MonthCode", SqlDbType.NVarChar, 2, objInventory.MonthCode));
                    prams.Add(Db.MakeInParam("@YearCode", SqlDbType.NVarChar, 4, objInventory.YearCode));
                    prams.Add(Db.MakeInParam("@LangaugeId", SqlDbType.Int, 4, objInventory.LangaugeId));
                    //prams.Add(Db.MakeInParam("TireStateCategoryId", SqlDbType.Int, 4, objInventory.TireStateCategoryId));

                    //if (objInventory.RecycleStateId <= 0)
                    //    prams.Add(Db.MakeInParam("RecycleStateId", SqlDbType.Int, 4, DBNull.Value));
                    //else
                    //    prams.Add(Db.MakeInParam("RecycleStateId", SqlDbType.Int, 4, objInventory.RecycleStateId));

                    //if (objInventory.RetreadStateId <= 0)
                    //    prams.Add(Db.MakeInParam("RetreadStateId", SqlDbType.Int, 4, DBNull.Value));
                    //else
                    //    prams.Add(Db.MakeInParam("RetreadStateId", SqlDbType.Int, 4, objInventory.RetreadStateId));

                    prams.Add(Db.MakeInParam("@OrganizationId", SqlDbType.Int, 4, objInventory.OrganizationId));
                    prams.Add(Db.MakeInParam("@Image", SqlDbType.VarBinary, 39578, objInventory.Image));
                    if (string.IsNullOrEmpty(objInventory.Space))
                        prams.Add(Db.MakeInParam("@vchSpace", SqlDbType.NVarChar, 300, DBNull.Value));
                    else
                        prams.Add(Db.MakeInParam("@vchSpace", SqlDbType.NVarChar, 300, objInventory.Space));
                    if (string.IsNullOrEmpty(objInventory.Lane))
                        prams.Add(Db.MakeInParam("@vchLane", SqlDbType.NVarChar, 300, DBNull.Value));
                    else
                        prams.Add(Db.MakeInParam("@vchLane", SqlDbType.NVarChar, 300, objInventory.Lane));
                    prams.Add(Db.MakeInParam("@TireClassId", SqlDbType.Int, 4, objInventory.TireClassId));
                    prams.Add(Db.MakeInParam("@TireActionId", SqlDbType.Int, 4, objInventory.TireActionId));

                    prams.Add(Db.MakeInParam("@TireOutComeId", SqlDbType.Int, 4, objInventory.TireOutComeID));
                    prams.Add(Db.MakeInParam("@LotId", SqlDbType.Int, 0, lotid));
                    prams.Add(Db.MakeInParam("@ModifiedDate", SqlDbType.DateTime, 0, modifiedtime));

                    returnValue = Db.RunProc("up_Inventory_Update", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UpdateInventory", ex);
            }
            return returnValue;
        }

        public static int updateTireForLoadRecieve(Tire objInventory, int OldTx_BarcodeID)
        {
            int returnValue = 0;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {


                    prams.Add(Db.MakeInParam("@TireId", SqlDbType.Int, 0, objInventory.TireId));
                    prams.Add(Db.MakeInParam("OldTX_BarCodeId", SqlDbType.BigInt, 8, OldTx_BarcodeID));
                    prams.Add(Db.MakeInParam("TX_BarCodeId", SqlDbType.BigInt, 8, objInventory.TX_BarCodeId));
                    prams.Add(Db.MakeInParam("SerialNumber", SqlDbType.NVarChar, 12, objInventory.SerialNumber));
                    prams.Add(Db.MakeInParam("DateCreated", SqlDbType.DateTime, 8, objInventory.DateCreated));
                    prams.Add(Db.MakeInParam("CreatedById", SqlDbType.Int, 4, objInventory.CreatedById));
                    prams.Add(Db.MakeInParam("C_BarCode", SqlDbType.BigInt, 8, objInventory.C_BarCode));
                    prams.Add(Db.MakeInParam("SizeNumber", SqlDbType.NVarChar, 12, objInventory.SizeNumber));
                    prams.Add(Db.MakeInParam("DotNumber", SqlDbType.NVarChar, 12, objInventory.DotNumber));
                    prams.Add(Db.MakeInParam("PlantNumber", SqlDbType.Char, 10, objInventory.PlantCode));
                    prams.Add(Db.MakeInParam("TireType", SqlDbType.NVarChar, 10, objInventory.TireType));
                    prams.Add(Db.MakeInParam("BrandId", SqlDbType.Int, 4, objInventory.BrandId1));
                    prams.Add(Db.MakeInParam("BrandId2", SqlDbType.Int, 4, objInventory.BrandId2));
                    prams.Add(Db.MakeInParam("MonthCode", SqlDbType.NVarChar, 2, objInventory.MonthCode));
                    prams.Add(Db.MakeInParam("YearCode", SqlDbType.NVarChar, 4, objInventory.YearCode));
                    prams.Add(Db.MakeInParam("LangaugeId", SqlDbType.Int, 4, objInventory.LangaugeId));


                    prams.Add(Db.MakeInParam("OrganizationId", SqlDbType.Int, 4, objInventory.OrganizationId));
                    prams.Add(Db.MakeInParam("Image", SqlDbType.VarBinary, -1, objInventory.Image));
                    if (string.IsNullOrEmpty(objInventory.Space))
                        prams.Add(Db.MakeInParam("@vchSpace", SqlDbType.NVarChar, 300, DBNull.Value));
                    else
                        prams.Add(Db.MakeInParam("@vchSpace", SqlDbType.NVarChar, 300, objInventory.Space));
                    if (string.IsNullOrEmpty(objInventory.Lane))
                        prams.Add(Db.MakeInParam("@vchLane", SqlDbType.NVarChar, 300, DBNull.Value));
                    else
                        prams.Add(Db.MakeInParam("@vchLane", SqlDbType.NVarChar, 300, objInventory.Lane));
                    prams.Add(Db.MakeInParam("TireClassId", SqlDbType.Int, 4, objInventory.TireClassId));
                    prams.Add(Db.MakeInParam("@TireActionId", SqlDbType.Int, 4, objInventory.TireActionId));

                    prams.Add(Db.MakeInParam("@TireOutComeId", SqlDbType.Int, 4, objInventory.TireOutComeID));



                    returnValue = Db.RunProc("up_updateTireForRecieveLoad", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "updateTireForLoadRecieve", ex);
            }
            return returnValue;
        }




        public static int updateInventorybyTireIdandLotId(Tire objInventory, int OldTx_BarcodeID, int lotid, DateTime modifieddate)
        {
            int returnValue = 0;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {


                    prams.Add(Db.MakeInParam("@TireId", SqlDbType.Int, 0, objInventory.TireId));
                    prams.Add(Db.MakeInParam("OldTX_BarCodeId", SqlDbType.BigInt, 8, OldTx_BarcodeID));
                    prams.Add(Db.MakeInParam("TX_BarCodeId", SqlDbType.BigInt, 8, objInventory.TX_BarCodeId));
                    prams.Add(Db.MakeInParam("SerialNumber", SqlDbType.NVarChar, 12, objInventory.SerialNumber));
                    prams.Add(Db.MakeInParam("DateCreated", SqlDbType.DateTime, 8, objInventory.DateCreated));
                    prams.Add(Db.MakeInParam("CreatedById", SqlDbType.Int, 4, objInventory.CreatedById));
                    prams.Add(Db.MakeInParam("C_BarCode", SqlDbType.BigInt, 8, objInventory.C_BarCode));
                    prams.Add(Db.MakeInParam("SizeNumber", SqlDbType.NVarChar, 12, objInventory.SizeNumber));
                    prams.Add(Db.MakeInParam("DotNumber", SqlDbType.NVarChar, 12, objInventory.DotNumber));
                    prams.Add(Db.MakeInParam("PlantNumber", SqlDbType.Char, 10, objInventory.PlantCode));
                    prams.Add(Db.MakeInParam("TireType", SqlDbType.NVarChar, 10, objInventory.TireType));
                    prams.Add(Db.MakeInParam("BrandId", SqlDbType.Int, 4, objInventory.BrandId1));
                    prams.Add(Db.MakeInParam("BrandId2", SqlDbType.Int, 4, objInventory.BrandId2));
                    prams.Add(Db.MakeInParam("MonthCode", SqlDbType.NVarChar, 2, objInventory.MonthCode));
                    prams.Add(Db.MakeInParam("YearCode", SqlDbType.NVarChar, 4, objInventory.YearCode));
                    prams.Add(Db.MakeInParam("LangaugeId", SqlDbType.Int, 4, objInventory.LangaugeId));
                    //prams.Add(Db.MakeInParam("TireStateCategoryId", SqlDbType.Int, 4, objInventory.TireStateCategoryId));

                    //if (objInventory.RecycleStateId <= 0)
                    //    prams.Add(Db.MakeInParam("RecycleStateId", SqlDbType.Int, 4, DBNull.Value));
                    //else
                    //    prams.Add(Db.MakeInParam("RecycleStateId", SqlDbType.Int, 4, objInventory.RecycleStateId));

                    //if (objInventory.RetreadStateId <= 0)
                    //    prams.Add(Db.MakeInParam("RetreadStateId", SqlDbType.Int, 4, DBNull.Value));
                    //else
                    //    prams.Add(Db.MakeInParam("RetreadStateId", SqlDbType.Int, 4, objInventory.RetreadStateId));

                    prams.Add(Db.MakeInParam("OrganizationId", SqlDbType.Int, 4, objInventory.OrganizationId));
                    prams.Add(Db.MakeInParam("Image", SqlDbType.VarBinary, -1, objInventory.Image));
                    if (string.IsNullOrEmpty(objInventory.Space))
                        prams.Add(Db.MakeInParam("@vchSpace", SqlDbType.NVarChar, 300, DBNull.Value));
                    else
                        prams.Add(Db.MakeInParam("@vchSpace", SqlDbType.NVarChar, 300, objInventory.Space));
                    if (string.IsNullOrEmpty(objInventory.Lane))
                        prams.Add(Db.MakeInParam("@vchLane", SqlDbType.NVarChar, 300, DBNull.Value));
                    else
                        prams.Add(Db.MakeInParam("@vchLane", SqlDbType.NVarChar, 300, objInventory.Lane));
                    prams.Add(Db.MakeInParam("TireClassId", SqlDbType.Int, 4, objInventory.TireClassId));
                    prams.Add(Db.MakeInParam("@TireActionId", SqlDbType.Int, 4, objInventory.TireActionId));

                    prams.Add(Db.MakeInParam("@TireOutComeId", SqlDbType.Int, 4, objInventory.TireOutComeID));
                    prams.Add(Db.MakeInParam("@LotId", SqlDbType.Int, 0, lotid));
                    prams.Add(Db.MakeInParam("@ModifiedDate", SqlDbType.DateTime, 0, modifieddate));

                    returnValue = Db.RunProc("up_Inventory_UpdatebyTireIdandLotId", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UpdateInventorybyTireIdandLotId", ex);
            }
            return returnValue;
        }




        public static bool deleteTireInfo(int TireID)
        {
            bool returnbol = false;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@TireId", SqlDbType.BigInt, 0, TireID));
                    int returnValue = Db.RunProc("up_Delete_inventory_byTireID", prams.ToArray());
                    if (returnValue > 0)
                    {
                        returnbol = true;
                        return true;
                    }
                    else
                    {
                        returnbol = true;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Inventory. DeleteTireInfo", ex);
            }
            return returnbol;
        }
        public static bool deleteTireInfoForRecieveLoad(int TireID)
        {
            bool returnbol = false;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@TireId", SqlDbType.BigInt, 0, TireID));
                    int returnValue = Db.RunProc("up_deleteTireForLoadReceive", prams.ToArray());
                    if (returnValue > 0)
                    {
                        returnbol = true;
                        return true;
                    }
                    else
                    {
                        returnbol = true;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Inventory. deleteTireInfoForRecieveLoad", ex);
            }
            return returnbol;
        }

        public static int getTireCBarCodeStatus(int CBarCode, int OrganizationId)
        {
            DataSet ds = null;
            int status = 0;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@intCBarCode", SqlDbType.BigInt, 0, CBarCode));
                    prams.Add(Db.MakeInParam("@intOrganizationId", SqlDbType.Int, 0, OrganizationId));
                    ds = Db.GetDataSet("up_BarcodeExistOrNot", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return status + 1;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire getTireCBarCodeStatus", ex);
            }
            return status;
        }
        public static DataSet getAllTireByLotId(int pageId, int pageSize, out int iTotalrows, string Lotids)
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
                    prm.Add(DB.MakeInParam("@intLotId", SqlDbType.NVarChar, 255, Lotids));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_getAllTiresByLotId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.getAllTireByLotId", ex);
            }
            return ds;
        }

        //Muhammad Omer
        public static DataSet getSizecodeForTireIds(string TireIds)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@TireIdArray", SqlDbType.NVarChar, 500, TireIds));
                    ds = DB.GetDataSet("Get_SizeCodeForTireId", prm.ToArray());
                }

            }
            catch(Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Tire.getSizecodeForTireIds", ex);
            }
            return ds;
        }

        //Muhammad Omer
        public static DataSet SearchProduct(int lotIds, int pageId, int pageSize, out int iTotalrows, string brandProduct, DateTime fromDateProduct, DateTime toDateProduct, string sizeProduct, string shapeProduct, string materialProduct)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@Lotid", SqlDbType.Int, 0, lotIds));
                    prm.Add(DB.MakeInParam("@intPageId", SqlDbType.Int, 4, pageId));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 4, pageSize));
                    if(string.IsNullOrEmpty(brandProduct))
                        prm.Add(DB.MakeInParam("@brandProduct", SqlDbType.NVarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@brandProduct", SqlDbType.NVarChar, 50, brandProduct));
                    if (string.IsNullOrEmpty(sizeProduct))
                        prm.Add(DB.MakeInParam("@sizeProduct", SqlDbType.NVarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@sizeProduct", SqlDbType.NVarChar, 50, sizeProduct));
                    if (string.IsNullOrEmpty(shapeProduct))
                        prm.Add(DB.MakeInParam("@shapeProduct", SqlDbType.NVarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@shapeProduct", SqlDbType.NVarChar, 50, shapeProduct));
                    if (string.IsNullOrEmpty(materialProduct))
                        prm.Add(DB.MakeInParam("@materialProduct", SqlDbType.NVarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@materialProduct", SqlDbType.NVarChar, 50, materialProduct));
                    if (fromDateProduct == DateTime.MinValue || toDateProduct == DateTime.MinValue)
                    {
                        prm.Add(DB.MakeInParam("@fromdate", SqlDbType.DateTime, 0, DBNull.Value));
                        prm.Add(DB.MakeInParam("@todate", SqlDbType.DateTime, 0, DBNull.Value));
                    }
                    else
                    {
                        prm.Add(DB.MakeInParam("@fromDate", SqlDbType.DateTime, 0, fromDateProduct));
                        prm.Add(DB.MakeInParam("@toDate", SqlDbType.DateTime, 0, toDateProduct));
                    }
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_GetProductInventoryByLotId",prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.SearchProduct", ex);
            }
            return ds;

        }


        

    }
}
