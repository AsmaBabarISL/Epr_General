using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TireTraxLib
{
    public class Lots
    {




        #region Lots
        private int _lotId;

        public int LotId
        {
            get { return _lotId; }
            set { _lotId = value; }
        }
        private String _lotNumber;

        public String LotNumber
        {
            get { return _lotNumber; }
            set { _lotNumber = value; }
        }
        private string _facilityName;

        public string FacilityName
        {
            get { return _facilityName; }
            set { _facilityName = value; }
        }
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
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
        private Boolean _isActive;

        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private int _spaceId;

        public int SpaceId
        {
            get { return _spaceId; }
            set { _spaceId = value; }
        }
        private int _userID;

        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        private int _roleID;

        public int RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }

        private bool _permanent;

        public bool Permanent
        {
            get { return _permanent; }
            set { _permanent = value; }
        }

        private int _ProductCategoryId;

        public int ProductCategoryId
        {
            get { return _ProductCategoryId; }
            set { _ProductCategoryId = value; }
        }





        #endregion

        #region Lot_Tire

        private String _location;

        public String Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private DateTime _dateEntered;

        public DateTime DateEntered
        {
            get { return _dateEntered; }
            set { _dateEntered = value; }
        }

        private int _enteredBy;

        public int EnteredBy
        {
            get { return _enteredBy; }
            set { _enteredBy = value; }
        }

        private bool _completed;

        public bool IsCompleted
        {
            get { return _completed; }
            set { _completed = value; }
        }

        private bool _subLot;

        public bool SubLot
        {
            get { return _subLot; }
            set { _subLot = value; }
        }


        #endregion


        #region Tire

        private int _tireId;


        private int _tX_BarCodeId;

        public int TX_BarCodeId
        {
            get { return _tX_BarCodeId; }
            set { _tX_BarCodeId = value; }
        }
        private int _createdById;

        public int CreatedById
        {
            get { return _createdById; }
            set { _createdById = value; }
        }


        private String _dOTNumber;

        public String DOTNumber
        {
            get { return _dOTNumber; }
            set { _dOTNumber = value; }
        }
        private String _sizeNumber;

        public String SizeNumber
        {
            get { return _sizeNumber; }
            set { _sizeNumber = value; }
        }
        private String _c_BarCode;

        public String C_BarCode
        {
            get { return _c_BarCode; }
            set { _c_BarCode = value; }
        }
        private String _plantNumber;

        public String PlantNumber
        {
            get { return _plantNumber; }
            set { _plantNumber = value; }
        }
        private String _tireType;

        public String TireType
        {
            get { return _tireType; }
            set { _tireType = value; }
        }
        private int _brandId;

        public int BrandId
        {
            get { return _brandId; }
            set { _brandId = value; }
        }

        private int _brandId2;

        public int BrandId2
        {
            get { return _brandId2; }
            set { _brandId2 = value; }
        }

        private String _monthCode;

        public String MonthCode
        {
            get { return _monthCode; }
            set { _monthCode = value; }
        }

        private String _yearCode;

        public String YearCode
        {
            get { return _yearCode; }
            set { _yearCode = value; }
        }


        #endregion


        #region Tire_Recycle_State



        private int _recycleStateId;

        public int StateId
        {
            get { return _recycleStateId; }
            set { _recycleStateId = value; }
        }



        #endregion


        #region Tire_RetradeState


        private int _retreadID;

        public int RetreadID
        {
            get { return _retreadID; }
            set { _retreadID = value; }
        }



        #endregion

        #region Space

        private String _spaceTypeId;

        public String SpaceTypeId
        {
            get { return _spaceTypeId; }
            set { _spaceTypeId = value; }
        }
        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        #endregion



        #region User
        private String _login;

        public String Login
        {
            get { return _login; }
            set { _login = value; }
        }
        private String _pwd;

        public String Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        private String _pwdSalt;

        public String PwdSalt
        {
            get { return _pwdSalt; }
            set { _pwdSalt = value; }
        }
        private String _tX_UserId;

        public String TX_UserId
        {
            get { return _tX_UserId; }
            set { _tX_UserId = value; }
        }
        private int _languageId;

        public int LanguageId
        {
            get { return _languageId; }
            set { _languageId = value; }
        }

        private int _timeZoneID;

        public int TimeZoneID
        {
            get { return _timeZoneID; }
            set { _timeZoneID = value; }
        }

        private int _contactId;

        public int ContactId
        {
            get { return _contactId; }
            set { _contactId = value; }
        }
        #endregion

        #region Role
        private String _roleName;

        public String RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
        private Boolean _isOrganization;

        public Boolean IsOrganization
        {
            get { return _isOrganization; }
            set { _isOrganization = value; }
        }



        #endregion

        #region Brand

        private String _brandName;

        public String BrandName
        {
            get { return _brandName; }
            set { _brandName = value; }
        }
        private String _company;

        public String Company
        {
            get { return _company; }
            set { _company = value; }
        }

        private int _countryId;

        public int CountryId
        {
            get { return _countryId; }
            set { _countryId = value; }
        }


        #endregion

        #region TX-BarCodes

        private String _barCodeNumber;

        public String BarCodeNumber
        {
            get { return _barCodeNumber; }
            set { _barCodeNumber = value; }
        }
        private Boolean _isUsed;

        public Boolean IsUsed
        {
            get { return _isUsed; }
            set { _isUsed = value; }
        }
        private int _barcodeId;
        public int BarCodeId
        {
            get { return _barcodeId; }
            set { _barcodeId = value; }
        }



        #endregion
        public Lots()
        {

        }

        public Lots(int lotid)
        {
            load(lotid);

        }
        private void load(int lotId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@lotId", SqlDbType.Int, 0, lotId);
                    reader = db.GetDataReader("up_Lots_getById", prams);
                    if (reader.Read())
                        load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo.Load", e);
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
                _lotId = Conversion.ParseDBNullInt(reader["LotId"]);
                _lotNumber = Conversion.ParseDBNullString(reader["LotNumber"]);
                _quantity = Conversion.ParseDBNullInt(reader["Quantity"]);
                _organizationId = Conversion.ParseDBNullInt(reader["OrganizationId"]);
                _dateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
                _isActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                _spaceId = Conversion.ParseDBNullInt(reader["SpaceId"]);
                _facilityName = Conversion.ParseDBNullString(reader["vchFacilityName"]);
                _userID = Conversion.ParseDBNullInt(reader["UserID"]);
                _roleID = Conversion.ParseDBNullInt(reader["RoleID"]);
                _spaceTypeId = Conversion.ParseDBNullString(reader["intSpaceId"]);
                _name = Conversion.ParseDBNullString(reader["vchSpaceName"]);
                _organizationId = Conversion.ParseDBNullInt(reader["OrganizationId"]);
                _login = Conversion.ParseDBNullString(reader["Login"]);
                _pwd = Conversion.ParseDBNullString(reader["Pwd"]);
                _pwdSalt = Conversion.ParseDBNullString(reader["PwdSalt"]);

                _languageId = Conversion.ParseDBNullInt(reader["LanguageId"]);
                _timeZoneID = Conversion.ParseDBNullInt(reader["TimeZoneID"]);
                _contactId = Conversion.ParseDBNullInt(reader["ContactId"]);
                _roleName = Conversion.ParseDBNullString(reader["RoleName"]);
                _isOrganization = Conversion.ParseDBNullBool(reader["IsOrganization"]);
                _tireId = Conversion.ParseDBNullInt(reader["Productid"]);

                _dateEntered = Conversion.ParseDBNullDateTime(reader["DateEntered"]);
                _tX_BarCodeId = Conversion.ParseDBNullInt(reader["BarCodeId"]);
                _createdById = Conversion.ParseDBNullInt(reader["CreatedById"]);
                _dOTNumber = Conversion.ParseDBNullString(reader["DOTNumber"]);
                _sizeNumber = Conversion.ParseDBNullString(reader["SizeNumber"]);
                _c_BarCode = Conversion.ParseDBNullString(reader["C-BarCode"]);
                _plantNumber = Conversion.ParseDBNullString(reader["PlantNumber"]);
                _tireType = Conversion.ParseDBNullString(reader["TireType"]);
                _brandId = Conversion.ParseDBNullInt(reader["BrandId1"]);
                _brandId2 = Conversion.ParseDBNullInt(reader["BrandId2"]);
                _monthCode = Conversion.ParseDBNullString(reader["MonthCode"]);
                _yearCode = Conversion.ParseDBNullString(reader["YearCode"]);
                _barCodeNumber = Conversion.ParseDBNullString(reader["SerialNumber"]);
                _isUsed = Conversion.ParseDBNullBool(reader["IsUsed"]);
                _brandName = Conversion.ParseDBNullString(reader["BrandName"]);

                _company = Conversion.ParseDBNullString(reader["Manufacturer"]);

                _countryId = Conversion.ParseDBNullInt(reader["CountryId"]);

                _ProductCategoryId = Conversion.ParseDBNullInt(reader["ProductCategoryid"]);





            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo.Load(reader)", ex);
            }
        }
        public static DataSet lotInfo(int pageId, int pageSize, out int iTotalrows)
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

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_lot_Info", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.LotInfo", ex);
            }
            return ds;
        }
        public static void updateLotInfo(int lotId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@lotid", SqlDbType.Int, 0, lotId));

                    DB.GetDataSet("up_lot_Info_Update", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);

                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.LotInfo", ex);
            }

        }

        public static string insertLot(Lots lot, out int Id, string guid, int facilityId = 0)
        {
            Id = 0;
            try
            {

                string lotId;
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    if (!string.IsNullOrEmpty(lot.LotNumber))
                        prams.Add(Db.MakeInParam("@LotNumber", SqlDbType.NVarChar, 100, lot.LotNumber));
                    if (lot.Quantity > 0)
                        prams.Add(Db.MakeInParam("@Quantity", SqlDbType.Int, 4, lot.Quantity));
                    prams.Add(Db.MakeInParam("@OrganizationId", SqlDbType.Int, 4, lot.OrganizationId));
                    if (facilityId == 0)
                        prams.Add(Db.MakeInParam("@intfacilityid", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(Db.MakeInParam("@intfacilityid", SqlDbType.Int, 0, facilityId));

                    prams.Add(Db.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, lot.DateCreated));
                    prams.Add(Db.MakeInParam("@IsActive", SqlDbType.Bit, 0, lot.IsActive));
                    if (lot.SpaceId > 0)
                        prams.Add(Db.MakeInParam("@SpaceId", SqlDbType.Int, 4, lot.SpaceId));
                    if (lot.Permanent)
                        prams.Add(Db.MakeInParam("@bitPermanent", SqlDbType.Bit, 0, lot.Permanent));
                    prams.Add(Db.MakeInParam("@UserID", SqlDbType.Int, 4, lot.UserID));
                    prams.Add(Db.MakeInParam("@RoleID", SqlDbType.Int, 4, lot.RoleID));
                    prams.Add(Db.MakeInParam("@bitCompleted", SqlDbType.Bit, 0, lot.IsCompleted));
                    prams.Add(Db.MakeInParam("@bitSubLot", SqlDbType.Bit, 0, lot.SubLot));
                    prams.Add(Db.MakeInParam("@pintBarCodeId", SqlDbType.Int, 4, lot.BarCodeId));
                    prams.Add(Db.MakeInParam("@vchrGuid", SqlDbType.NVarChar, 500, guid));
                    prams.Add(Db.MakeInParam("@intProductCatId", SqlDbType.Int, 4, lot.ProductCategoryId));
                    DataTable dt = Db.GetDataSet("up_LotsInsertUpdate", prams.ToArray()).Tables[0];
                    Id = Convert.ToInt32(dt.Rows[0]["LotId"]);
                    return dt.Rows[0]["SerialNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo InsertLot", ex);
            }
            return null;
        }
        public static DataSet SearchInventory(int pageId, int pageSize, out int iTotalrows, String TX_barcode, int OrganizationId, string PlantCode,
              string SizeCode, int TireState, int status, string userName, int quantity, string space, string lane, string guid, string lat, string log
              , DateTime frmDate, DateTime toDate, string lot, string size, string brand,int ProductCatId)
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
                        prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.NVarChar, 10, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@PlantCode", SqlDbType.NVarChar, 10, PlantCode));

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
                    prm.Add(DB.MakeInParam("@ProductCatId", SqlDbType.Int, 4, ProductCatId));


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
                new SqlLog().InsertSqlLog(0, "Inventory.SearchInventory", ex);
            }
            return ds;
        }
        public static DataSet loadsInfo(int pageId, int pageSize, out int iTotalrows, string userName, int quantity, string space, string lane, string guid, string lat, string log
             , DateTime frmDate, DateTime toDate, int organizationId)
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

                    if (organizationId == 0)
                        prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 4, organizationId));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));


                    ds = DB.GetDataSet("up_loadsinfo", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.LoadsInfo", ex);
            }
            return ds;
        }



        public static bool updateLot(Lots lot)
        {
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@LotId", SqlDbType.Int, 4, lot.LotId));
                    prams.Add(Db.MakeInParam("@LotNumber", SqlDbType.NVarChar, 10, lot.LotNumber));
                    prams.Add(Db.MakeInParam("@Quantity", SqlDbType.Int, 4, lot.Quantity));
                    prams.Add(Db.MakeInParam("@OrganizationId", SqlDbType.Int, 4, lot.OrganizationId));
                    prams.Add(Db.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, lot.DateCreated));
                    prams.Add(Db.MakeInParam("@IsActive", SqlDbType.Bit, 0, lot.IsActive));
                    prams.Add(Db.MakeInParam("@SpaceId", SqlDbType.Int, 4, lot.SpaceId));
                    prams.Add(Db.MakeInParam("@UserID", SqlDbType.Int, 4, lot.UserID));
                    prams.Add(Db.MakeInParam("@RoleID", SqlDbType.Int, 4, lot.RoleID));
                    prams.Add(Db.MakeInParam("@bitCompleted", SqlDbType.Bit, 0, lot.IsCompleted));
                    prams.Add(Db.MakeInParam("@pintBarCodeId", SqlDbType.Int, 4, lot.BarCodeNumber));

                    int exec = Db.RunProc("[up_LotsInsertUpdate]", prams.ToArray());
                    return true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo UpdateLot", ex);
            }
            return false;
        }

        public static bool insertLotsTires(int lotId, int tireId, int parentLotId, DateTime datecreated, int enteredBy, bool active, bool transfered, bool delivered, int spaceId, int laneId)
        {
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {

                    prams.Add(Db.MakeInParam("@LotId", SqlDbType.Int, 4, lotId));
                    prams.Add(Db.MakeInParam("@TireId", SqlDbType.Int, 4, tireId));
                    prams.Add(Db.MakeInParam("@ParentLotid", SqlDbType.Int, 4, parentLotId));
                    prams.Add(Db.MakeInParam("@bitActive", SqlDbType.Bit, 0, active));
                    prams.Add(Db.MakeInParam("@DateEntered", SqlDbType.DateTime, 4, datecreated));
                    prams.Add(Db.MakeInParam("@EnteredBy", SqlDbType.Int, 4, enteredBy));
                    prams.Add(Db.MakeInParam("@bitDelivered", SqlDbType.Bit, 0, delivered));
                    prams.Add(Db.MakeInParam("@bitTransfered", SqlDbType.Bit, 0, transfered));
                    if (spaceId > 0)
                        prams.Add(Db.MakeInParam("@intSpaceId", SqlDbType.Int, 4, spaceId));
                    if (laneId > 0)
                        prams.Add(Db.MakeInParam("@intLaneId", SqlDbType.Int, 4, laneId));
                    int exec = Db.RunProc("[up_Lot_TiresInsert]", prams.ToArray());
                    if (exec > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo InsertLotsTires", ex);
            }
            return false;
        }

        public static bool insertLotProduct(int lotId, int productId, int parentLotId, DateTime datecreated, int enteredBy, bool active, bool transfered, bool delivered, int spaceId, int laneId)
        {
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {

                    prams.Add(Db.MakeInParam("@LotId", SqlDbType.Int, 4, lotId));
                    prams.Add(Db.MakeInParam("@ProductId", SqlDbType.Int, 4, productId));
                    prams.Add(Db.MakeInParam("@ParentLotid", SqlDbType.Int, 4, parentLotId));
                    prams.Add(Db.MakeInParam("@bitActive", SqlDbType.Bit, 0, active));
                    prams.Add(Db.MakeInParam("@DateEntered", SqlDbType.DateTime, 4, datecreated));
                    prams.Add(Db.MakeInParam("@EnteredBy", SqlDbType.Int, 4, enteredBy));
                    prams.Add(Db.MakeInParam("@bitDelivered", SqlDbType.Bit, 0, delivered));
                    prams.Add(Db.MakeInParam("@bitTransfered", SqlDbType.Bit, 0, transfered));
                    if (spaceId > 0)
                        prams.Add(Db.MakeInParam("@intSpaceId", SqlDbType.Int, 4, spaceId));
                    if (laneId > 0)
                        prams.Add(Db.MakeInParam("@intLaneId", SqlDbType.Int, 4, laneId));
                    int exec = Db.RunProc("up_Lot_ProductInsert", prams.ToArray());
                    if (exec > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo InsertLotsTires", ex);
            }
            return false;
        }

        public static bool finishedLot(int lotId, bool completed)
        {
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {

                    prams.Add(Db.MakeInParam("@intLotId", SqlDbType.Int, 4, lotId));
                    prams.Add(Db.MakeInParam("@bitFinished", SqlDbType.Bit, 0, completed));

                    int exec = Db.RunProc("[up_UpdateLotsFinished]", prams.ToArray());
                    return true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo FinishedLot", ex);
            }
            return false;
        }

        public static DataTable getUnFinishedLots(int orgId)
        {
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@intOrgId", SqlDbType.Int, 4, orgId));
                    DataTable dt = Db.GetDataSet("[up_getunfinishedLots]", prams.ToArray()).Tables[0];
                    return dt;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo GetUnFinishedLots", ex);
            }
            return null;
        }

        public static string getLotNumberByLotId(int lotId)
        {
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@intLotId", SqlDbType.Int, 4, lotId));
                    DataTable dt = Db.GetDataSet("[up_GetLotNumber]", prams.ToArray()).Tables[0];
                    return dt.Rows[0]["LotNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo GetLotNumberByLotId", ex);
            }
            return null;
        }


        public static void updateLotQuantity(int lotId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@lotid", SqlDbType.Int, 0, lotId));

                    DB.GetDataSet("up_count_lots_quantity", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);

                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.UpdateLotQuantity", ex);
            }

        }
        public static bool updateSubLotTires(int parentLotId, int subLotId, string tireIds)
        {
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager Db = DbManager.GetDbManager())
                {
                    prams.Add(Db.MakeInParam("@intParentLotId", SqlDbType.Int, 4, parentLotId));
                    prams.Add(Db.MakeInParam("@intSubLotId", SqlDbType.Int, 4, subLotId));
                    prams.Add(Db.MakeInParam("@vchTireIds", SqlDbType.NVarChar, 255, tireIds));
                    int exec = Db.RunProc("[up_updatelottires]", prams.ToArray());
                    return true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo UpdateSubLotTires", ex);
            }
            return false;
        }





        public static DataSet getPermanentLot(int pageId, int pageSize, out int iTotalrows, int orgId,int CatId)
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
                    prm.Add(DB.MakeInParam("@intOrgId", SqlDbType.Int, 4, orgId));
                    prm.Add(DB.MakeInParam("@intproductcatID", SqlDbType.Int, 4, CatId));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_GetAllPermanentLots", prm.ToArray());
                     if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getPermanentLot", ex);
            }
            return ds;
        }

        public static DataSet getPermanentLotSpace(int LotId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intPermanentLotId", SqlDbType.Int, 4, LotId));
                    ds = DB.GetDataSet("up_GetAllSpace", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getPermanentLotSpace", ex);
            }
            return ds;
        }
        public static DataSet getPermanentLotSpaceLane(int SpaceId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intspaceid", SqlDbType.Int, 0, SpaceId));
                    ds = DB.GetDataSet("up_getlanebyspaceId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getPermanentLotSpaceLanes", ex);
            }
            return ds;
        }



        //public static DataSet searchLotInventory(int pageId, int pageSize, int lotId, out int iTotalrows, String TX_barcode, string Organization, string PlantCode, string SizeCode, int TireState, int status, int organizationId)
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
        //        new SqlLog().InsertSqlLog(0, "Lots.searchLotInventory", ex);
        //    }
        //    return ds;
        //}


        public static DataSet searchTireInventoryByLotIds(int pageId, int pageSize, string lotIds, out int iTotalrows, String TX_barcode, string Organization, string PlantCode, string SizeCode, int TireState, int status)
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
                new SqlLog().InsertSqlLog(0, "Lots.searchTireInventoryByLotIds", ex);
            }
            return ds;
        }


        public static bool transferTemporaryLot(int TempLotId, int spaceId, int laneId)
        {
            bool flag = false;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intTempraryLotId", SqlDbType.Int, 4, TempLotId));
                    prm.Add(DB.MakeInParam("@intSpaceId", SqlDbType.Int, 4, spaceId));
                    prm.Add(DB.MakeInParam("@intLaneId", SqlDbType.Int, 4, laneId));
                    int exec = DB.RunProc("up_UpdatePermanentLot", prm.ToArray());
                    return true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.transferTemporaryLot", ex);
            }
            return flag;
        }
        public static string getPermanentLotName(int lotId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLotId", SqlDbType.Int, 4, lotId));
                    ds = DB.GetDataSet("up_getPermanentLotName", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds.Tables[0].Rows[0]["LotNumber"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getPermanentLotName", ex);
            }
            return string.Empty;
        }

        public static int shiftTireByLotId(int oldlotid, int newlotid, int spaceid, int laneid)
        {


            List<SqlParameter> prm = new List<SqlParameter>();
            int id = 0;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@oldlotid", SqlDbType.Int, 0, oldlotid));
                    prm.Add(DB.MakeInParam("@newlotid", SqlDbType.Int, 0, newlotid));
                    prm.Add(DB.MakeInParam("@spaceid", SqlDbType.Int, 0, spaceid));
                    prm.Add(DB.MakeInParam("@laneid", SqlDbType.Int, 0, laneid));


                    id = DB.RunProc("up_shiftTireByLotId", prm.ToArray());
                    return id;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.shiftTireByLotId", ex);
            }
            return id;
        }

        public static DataSet getParkingLotsByFacilityId(int facilityId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intFacilityID", SqlDbType.Int, 0, facilityId));
                    ds = DB.GetDataSet("Up_Lot_getLotLookUp", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getParkingLotsByFacilityId", ex);
            }
            return ds;
        }




        public static DataSet getBarcodeByLotId(int lotid)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intlotid", SqlDbType.Int, 0, lotid));
                    ds = DB.GetDataSet("up_getLotBarcodebyLotId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getBarcodeByLotid", ex);
            }
            return ds;
        }
        public static DataSet getBarcodeByTireId(int tireid)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@inttireid", SqlDbType.Int, 0, tireid));
                    ds = DB.GetDataSet("up_getLotBarcodebyTireId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getBarcideByTireId", ex);
            }
            return ds;
        }



        public static DataSet getParkingLot(int pageId, int pageSize, int organizationId, out int iTotalrows, string LotNumber, int facilityId = 0)
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
                    if (string.IsNullOrEmpty(LotNumber))
                        prm.Add(DB.MakeInParam("@vchParkingLotName", SqlDbType.NVarChar, 200, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchParkingLotName", SqlDbType.NVarChar, 200, LotNumber));

                    if (facilityId == 0)
                        prm.Add(DB.MakeInParam("@intfacilityid", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@intfacilityid", SqlDbType.Int, 0, facilityId));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_parkingLot_landing", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots getParkingLot", ex);
            }
            return ds;
        }

        public static DataSet getParkingLotByName(int pageId, int pageSize, int organizationId, out int iTotalrows, string parkingLotName)
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
                    if (string.IsNullOrEmpty(parkingLotName))
                        prm.Add(DB.MakeInParam("@vchParkingLotName", SqlDbType.NVarChar, 200, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchParkingLotName", SqlDbType.NVarChar, 200, parkingLotName));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_parkingLot_landing", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots getParkingLotByName", ex);
            }
            return ds;
        }
        public static DataSet getParkingLotNumberByLotId(int lotId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intlotid", SqlDbType.Int, 0, lotId));
                    ds = DB.GetDataSet("up_LotbyLotId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots getParkingLot", ex);
            }
            return ds;
        }


        public static DataSet getParkingLotBackup(int pageId, int pageSize, int organizationId, out int iTotalrows)
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

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_parking_lot_backup", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots getParkingLotBackUp", ex);
            }
            return ds;
        }



        public static void deleteLot(int lotid)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intlotid", SqlDbType.Int, 0, lotid));


                    ds = DB.GetDataSet("up_DeleteLotbyLotId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots deleteLot", ex);
            }

        }



        public static void updateLotByLotId(int lotid, string lotnumber)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intlotid", SqlDbType.Int, 0, lotid));
                    prm.Add(DB.MakeInParam("@lotnumber", SqlDbType.NVarChar, 100, lotnumber));


                    ds = DB.GetDataSet("up_Lot_UpdateByLotId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots updateLotByLotId", ex);
            }

        }
        public static int gettirecount(int lotid)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            int Id = 0;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intlotid", SqlDbType.Int, 0, lotid));


                    Id = DB.RunProc("up_tire_counter", prm.ToArray());


                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Lane Insert", e);
            }
            return Id;
        }

        public static int gettirecountrow(int spaceId)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            int Id = 0;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intSpaceId", SqlDbType.Int, 0, spaceId));


                    Id = DB.RunProc("up_tire_counter_Row", prm.ToArray());


                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Lane Insert", e);
            }
            return Id;
        }

        public static int gettirecountspace(int laneId)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            int Id = 0;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intLaneId", SqlDbType.Int, 0, laneId));


                    Id = DB.RunProc("up_tire_counter_Space", prm.ToArray());


                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Lane Insert", e);
            }
            return Id;
        }

        public static DataSet SearchLotInventory(int pageId, int pageSize, int lotId, out int iTotalrows, String TX_barcode, string Organization, string PlantCode, string SizeCode, int TireState, int status, int organizationId)
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
                    if (organizationId > 0)
                        prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 4, organizationId));
                    else
                        prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 4, DBNull.Value));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("[up_Inventory_SearchLotInventory]", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Inventory.SearchLotInventory", ex);
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
                new SqlLog().InsertSqlLog(0, "Inventory.SearchTireInventoryByLotIds", ex);
            }
            return ds;
        }

        public static DataSet GetOrganizationsbyStewardship(int ID, int catid)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intParentID", SqlDbType.Int, 4, ID));
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 4, catid));
                    ds = DB.GetDataSet("Up_Organization_getOrganizationLookups", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.GetOrganizationsbyStewardship", ex);
            }
            return ds;
        }

        public static DataSet GetAllHaulerbyOrganizationId(int ID)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intorganizationId", SqlDbType.Int, 4, ID));
                    ds = DB.GetDataSet("up_getAllHaulerbyOrganizationId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.GetAllHaulerbyOrganizationId", ex);
            }
            return ds;
        }

        public static DataSet getAllLotsByOrganizationId(int pageId, int pageSize, out int iTotalrows, int OrganizationId, int CatId)
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
                    prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 0, OrganizationId));
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 0, CatId));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_getAllLotsByOrganizationId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getAllLotsByOrganizationId", ex);
            }
            return ds;
        }
        public static int getAllLotStatusByOrganizationIdAndLotNumber(int OrganizationId, int FacilityId, string lotnumber)
        {
            DataSet ds = null;
            int count = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 0, OrganizationId));
                    prm.Add(DB.MakeInParam("@intFacilityId", SqlDbType.Int, 0, FacilityId));
                    prm.Add(DB.MakeInParam("@LotName", SqlDbType.NVarChar, 500, lotnumber));



                    ds = DB.GetDataSet("up_getStatusLotByOrganizationIdAndLotName", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return count + 1;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getAllLotStatusByOrganizationIdAndLotNumber", ex);
            }
            return count;
        }
        /// <summary>
        /// Wajid
        /// 12/11/2014
        /// This function is used to get the companies under one Stewarship.
        /// 
        /// </summary>
        /// <param name="ID">stewardship id</param>
        /// <returns>dataset</returns>
        public static DataSet GetAllCompanieswithAddressbyStewardshipId(int ID)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intorganizationId", SqlDbType.Int, 4, ID));
                    ds = DB.GetDataSet("up_getAllCompaniesbyStewardshipId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.GetAllCompaniesbyStewardshipId", ex);
            }
            return ds;
        }

        /// <summary>
        /// Wajid
        /// 12/11/2014
        /// This function is used to get the company id and search for its stewardship companies and find other companies in that stewardship
        /// </summary>
        /// <param name="ID">Company ID through </param>
        /// <returns></returns>
        public static DataSet GetAllCompanieswithAddressWithinSameStewardship(int ID,int CatId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intOrgID", SqlDbType.Int, 4, ID));
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 4, CatId));
                    ds = DB.GetDataSet("up_getAllCompaniesWithinSameStewardshipByOrgID", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.GetAllCompaniesWithinSameStewardship", ex);
            }
            return ds;
        }

        public static DataSet GetAllTransporterCompanieswithAddressWithinSameStewardship(int ID)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intOrgID", SqlDbType.Int, 4, ID));
                    ds = DB.GetDataSet("up_getAllTransporterCompaniesWithinSameStewardshipByOrgID", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.up_getAllTransporterCompaniesWithinSameStewardshipByOrgID", ex);
            }
            return ds;
        }


        public static DataSet GetLotInfo(int PageNo, int PageSize, int UserOrgId, out int count, int ProductCatId, string UserName, string LotNumber, DateTime FromDate, DateTime ToDate,int Units, string BarCode)
        {
            DataSet ds = null;
            count = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intPageNo", SqlDbType.Int, 4, PageNo));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 4, PageSize));
                    prm.Add(DB.MakeInParam("@intUserOrgId", SqlDbType.Int, 4, UserOrgId));
                    prm.Add(DB.MakeInParam("@ProductCatId", SqlDbType.Int, 4, ProductCatId));
                    if (string.IsNullOrEmpty(UserName))
                        prm.Add(DB.MakeInParam("@UserName", SqlDbType.NVarChar, 2, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@UserName", SqlDbType.NVarChar, 2, UserName));
                    if (string.IsNullOrEmpty(LotNumber))
                        prm.Add(DB.MakeInParam("@Lot", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@Lot", SqlDbType.NVarChar, 255, LotNumber));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    if (Units == 0)
                        prm.Add(DB.MakeInParam("@Quantity", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@Quantity", SqlDbType.Int, 4, Units));
                    if (FromDate == DateTime.MinValue || ToDate == DateTime.MinValue)
                    {
                        prm.Add(DB.MakeInParam("@fromDate", SqlDbType.DateTime, 8, DBNull.Value));
                        prm.Add(DB.MakeInParam("@toDate", SqlDbType.DateTime, 8, DBNull.Value));
                    }
                    else
                    {
                        prm.Add(DB.MakeInParam("@fromDate", SqlDbType.DateTime, 8, FromDate));
                        prm.Add(DB.MakeInParam("@toDate", SqlDbType.DateTime, 8, ToDate));
                    }
                    prm.Add(DB.MakeInParam("@BarCode", SqlDbType.NVarChar, 20, BarCode));


                    //prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    prm.Add(DB.MakeOutParam("@total",SqlDbType.Int,4));
                    ds = DB.GetDataSet("GetLotInfoForProduct", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        count = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.GetLotInfo", ex);
            }
            return ds;

        }


        public static DataSet GetProductSubCats(int CatId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@ProductCategoryId", SqlDbType.Int, 4, CatId));
                    ds = DB.GetDataSet("up_GetProductProductSubCategories", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.GetProductSubCats", ex);
            }
            return ds;
        }

        public static DataSet GetAllProductSizes(int LanguageId, int CatId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@ProductCategoryId", SqlDbType.Int, 4, CatId));
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, LanguageId));
                    ds = DB.GetDataSet("up_GetProductSizes", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.GetAllProductSizes", ex);
            }
            return ds;
        }

        public static DataSet GetAllProductShapes(int LanguageId, int CatId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@ProductCategoryId", SqlDbType.Int, 4, CatId));
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, LanguageId));
                    ds = DB.GetDataSet("up_GetProductShapes", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.GetAllProductShapes", ex);
            }
            return ds;
        }

        public static DataSet GetAllProductMaterials(int LanguageId, int CatId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@ProductCategoryId", SqlDbType.Int, 4, CatId));
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, LanguageId));
                    ds = DB.GetDataSet("up_GetProductMaterials", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.GetAllProductMaterials", ex);
            }
            return ds;
        }

        public static DataSet GetAllProductBrands(int LanguageId, int CatId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@ProductCategoryId", SqlDbType.Int, 4, CatId));
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, LanguageId));
                    ds = DB.GetDataSet("up_GetProductBrands", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.GetAllProductBrands", ex);
            }
            return ds;
        }

        public static void ChangeModifiedDate(int LotId)
        {
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@LotId", SqlDbType.Int, 4, LotId));
                    db.RunProc("up_UpdateLotModifiedTime", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.ChangeModifiedDate", ex);
            }
        }

        public static DataSet GetBarCodeByProductId(int productId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@ProductId", SqlDbType.Int, 0, productId));
                    ds = DB.GetDataSet("up_GetProductBarCodeAndLotInfo", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getBarcideByTireId", ex);
            }
            return ds;
        }
    }
}
