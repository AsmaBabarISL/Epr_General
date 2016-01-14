using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TireTraxLib
{
    public class Loads
    {

        #region Load

        private int _loadId;

        public int LoadId
        {
            get { return _loadId; }
            set { _loadId = value; }
        }

        private string _loadNumber;

        public string LoadNumber
        {
            get { return _loadNumber; }
            set { _loadNumber = value; }
        }

        private int _loadTypeId;

        public int LoadTypeId
        {
            get { return _loadTypeId; }
            set { _loadTypeId = value; }
        }

        private string _lookUpTypeName;

        public string LookUpTypeName
        {
            get { return _lookUpTypeName; }
            set { _lookUpTypeName = value; }
        }
        private string _transferOrganization;

        public string TransferOrganization
        {
            get { return _transferOrganization; }
            set { _transferOrganization = value; }
        }

        private string _haulerOrganization;

        public string HaulerOrganization
        {
            get { return _haulerOrganization; }
            set { _haulerOrganization = value; }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        private string _pOnumber;

        public string POnumber
        {
            get { return _pOnumber; }
            set { _pOnumber = value; }
        }

        private string _invoiceNumber;

        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set { _invoiceNumber = value; }
        }

        private string _sealNumber;

        public string SealNumber
        {
            get { return _sealNumber; }
            set { _sealNumber = value; }
        }

        private int _facilityId;

        public int FacilityId
        {
            get { return _facilityId; }
            set { _facilityId = value; }
        }
        private string _trailerNumber;

        public string TrailerNumber
        {
            get { return _trailerNumber; }
            set { _trailerNumber = value; }
        }


        private int _haulerIDNumber;

        public int HaulerIDNumber
        {
            get { return _haulerIDNumber; }
            set { _haulerIDNumber = value; }
        }

        private string _weight;

        public string Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private string _billOfLandingNumber;

        public string BillOfLandingNumber
        {
            get { return _billOfLandingNumber; }
            set { _billOfLandingNumber = value; }
        }
        private Boolean _bitIsAccepted;

        public Boolean BitIsAccepted
        {
            get { return _bitIsAccepted; }
            set { _bitIsAccepted = value; }
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

        private bool _isActive;

        public bool IsActive
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


        private string _lane;

        public string Lane
        {
            get { return _lane; }
            set { _lane = value; }
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


        private decimal _latitude;

        public decimal Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }


        private decimal _longitude;

        public decimal Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }


        private bool _bitCompleted;

        public bool BitCompleted
        {
            get { return _bitCompleted; }
            set { _bitCompleted = value; }
        }


        private int _barcodeId;

        public int BarcodeId
        {
            get { return _barcodeId; }
            set { _barcodeId = value; }
        }


        private bool _subLoad;

        public bool SubLoad
        {
            get { return _subLoad; }
            set { _subLoad = value; }
        }



        private string _guid;

        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }


        private int _barcodeId2;

        public int BarcodeId2
        {
            get { return _barcodeId2; }
            set { _barcodeId2 = value; }
        }


        private int _lotID;

        public int LotID
        {
            get { return _lotID; }
            set { _lotID = value; }
        }

        private int _parentId;

        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        private bool _Reject;

        public bool Rejected
        {
            get { return _Reject; }
            set { _Reject = value; }
        }



        #endregion




        #region Load Tires


        private int _tireId;

        public int TireId
        {
            get { return _tireId; }
            set { _tireId = value; }
        }
        private int _parentLoadId;

        public int ParentLoadId
        {
            get { return _parentLoadId; }
            set { _parentLoadId = value; }
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
        private bool _bitActive;

        public bool BitActive
        {
            get { return _bitActive; }
            set { _bitActive = value; }
        }

        private bool _bitTransfer;

        public bool BitTransfer
        {
            get { return _bitTransfer; }
            set { _bitTransfer = value; }
        }

        private bool _bitDelivered;

        public bool BitDelivered
        {
            get { return _bitDelivered; }
            set { _bitDelivered = value; }
        }

        private int _inventoryId;

        public int InventoryId
        {
            get { return _inventoryId; }
            set { _inventoryId = value; }
        }


        #endregion


        #region Load Methods


        public Loads()
        {
        }


        public Loads(int loadid)
        {
            load(loadid);
        }


        private void load(int loadId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@intloadid", SqlDbType.Int, 0, loadId);
                    reader = db.GetDataReader("up_getLoadInfobyLoadId", prams);
                    if (reader.Read())
                        load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Loads.load", e);
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
                _loadId = Conversion.ParseDBNullInt(reader["LoadId"]);
                _loadNumber = Conversion.ParseDBNullString(reader["LoadNumber"]);
                _loadTypeId = Conversion.ParseDBNullInt(reader["LoadTypeId"]);
                _lookUpTypeName = Conversion.ParseDBNullString(reader["LoadTypeName"]);
                _quantity = Conversion.ParseDBNullInt(reader["Quantity"]);
                _pOnumber = Conversion.ParseDBNullString(reader["POnumber"]);
                _invoiceNumber = Conversion.ParseDBNullString(reader["InvoiceNumber"]);
                _sealNumber = Conversion.ParseDBNullString(reader["SealNumber"]);
                _trailerNumber = Conversion.ParseDBNullString(reader["TrailerNumber"]);
                _haulerIDNumber = Conversion.ParseDBNullInt(reader["HaulerIDNumber"]);
                _weight = Conversion.ParseDBNullString(reader["weight"]);
                _billOfLandingNumber = Conversion.ParseDBNullString(reader["BillOfLandingNumber"]);
                _organizationId = Conversion.ParseDBNullInt(reader["OrganizationId"]);
                _dateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
                _isActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                _spaceId = Conversion.ParseDBNullInt(reader["SpaceId"]);
                _lane = Conversion.ParseDBNullString(reader["Lane"]);
                _userID = Conversion.ParseDBNullInt(reader["UserID"]);

                _latitude = Conversion.ParseDBNullInt(reader["Lattitude"]);
                _longitude = Conversion.ParseDBNullDecimal(reader["Longitude"]);
                _bitCompleted = Conversion.ParseDBNullBool(reader["bitCompleted"]);
                _barcodeId = Conversion.ParseDBNullInt(reader["intBarcodeId"]);

                _guid = Conversion.ParseDBNullString(reader["vchGuid"]);
                _facilityId = Conversion.ParseDBNullInt(reader["FacilityId"]);
                _lotID = Conversion.ParseDBNullInt(reader["intLotID"]);
                _tireId = Conversion.ParseDBNullInt(reader["ProductId"]);
                _parentLoadId = Conversion.ParseDBNullInt(reader["intLoadParentId"]);
                _dateEntered = Conversion.ParseDBNullDateTime(reader["DateEntered"]);
                _enteredBy = Conversion.ParseDBNullInt(reader["EnteredBy"]);
                _bitActive = Conversion.ParseDBNullBool(reader["bitActive"]);
                _bitTransfer = Conversion.ParseDBNullBool(reader["bitTransfered"]);
                _bitDelivered = Conversion.ParseDBNullBool(reader["bitDelivered"]);
                _inventoryId = Conversion.ParseDBNullInt(reader["FacilityLotId"]);
                //_parentId = Conversion.ParseDBNullInt(reader["intParentId"]);
                _Reject = Conversion.ParseDBNullBool(reader["bitReject"]);
                _bitIsAccepted = Conversion.ParseDBNullBool(reader["bitIsAccepted"]);
                _transferOrganization = Conversion.ParseDBNullString(reader["TransferOrganization"]);

                _haulerOrganization = Conversion.ParseDBNullString(reader["HaulerOrganization"]);
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads.load", ex);
            }
        }


        public static DataSet loadsInfo(int pageId, int pageSize, out int iTotalrows, string userName
            , DateTime frmDate, DateTime toDate, int organizationId, int LoadTypeId, string OrganizationName,int catid)
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
                    if (string.IsNullOrEmpty(OrganizationName))
                        prm.Add(DB.MakeInParam("@vchOrganizationName", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@vchOrganizationName", SqlDbType.NVarChar, 255, OrganizationName));


                    if (frmDate == DateTime.MinValue)
                        prm.Add(DB.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 8, frmDate));
                    if (toDate == DateTime.MinValue)
                        prm.Add(DB.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 8, toDate));



                    if (organizationId == 0)
                        prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@intOrganizationId", SqlDbType.Int, 4, organizationId));
                    if (LoadTypeId == 0)
                        prm.Add(DB.MakeInParam("@intLoadTypeId", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@intLoadTypeId", SqlDbType.Int, 4, LoadTypeId));
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 4, catid));
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
                new SqlLog().InsertSqlLog(0, "Loads.LoadsInfo", ex);
            }
            return ds;
        }


        public static DataSet getLoadTireInfoByLoadId(int loadid)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intloadid", SqlDbType.Int, 0, loadid));
                    ds = DB.GetDataSet("up_load_tireinfobyloadid", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads.getLoadTireInfoByLoadId", ex);
            }
            return ds;
        }

        public static DataSet GetDeliveryAcceptanceByLoadId(int loadid)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@LoadId", SqlDbType.Int, 0, loadid));
                    ds = DB.GetDataSet("up_GetDeliveryAcceptanceByLoadId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads.getLoadTireInfoByLoadId", ex);
            }
            return ds;
        }

        public static DataSet getProductInfoByLoadId(int loadid,int catid)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intloadid", SqlDbType.Int, 0, loadid));
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 0, catid));
                    ds = DB.GetDataSet("up_load_productinfobyloadid", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads.getProductInfoByLoadId", ex);
            }
            return ds;
        }
        
        public static DataSet getTiresInfoByLoadIds(string loadids, int pageId, int pageSize, out int iTotalrows)
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

                    prm.Add(DB.MakeInParam("@intloadids", SqlDbType.VarChar, 0, loadids));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_load_tiresinfobyloadids", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads.getTiresInfoByLoadIds", ex);
            }
            return ds;
        }

        public static DataSet getProductInfoByLoadIds(string loadids,int CatId)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 4, CatId));
                    prm.Add(DB.MakeInParam("@loadids", SqlDbType.VarChar, 0, loadids));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_load_productinfobyloadids", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads.getProductInfoByLoadIds", ex);
            }
            return ds;
        }

        public static DataSet GetProductsInfoByLoadIds(string loadids, int PageNo, int PageSize, out int iTotalrows, int CatId)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intPageNo", SqlDbType.Int, 4, PageNo));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 4, PageSize));
                    prm.Add(DB.MakeInParam("@loadIds", SqlDbType.VarChar, 0, loadids));
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.VarChar, 0, CatId));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("GetProductsByLoadIds", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads.GetProductsInfoByLoadIds", ex);
            }
            return ds;
        }


        public static int getCountLoadByLoadId(int loadid)
        {


            List<SqlParameter> prm = new List<SqlParameter>();
            int id = 0;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intloadid", SqlDbType.Int, 0, loadid));



                    id = DB.RunProc("up_load_tire_loadcountbyloadid", prm.ToArray());
                    return id;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.getCountLoadByLoadId", ex);
            }
            return id;
        }



        public static int insertLoad(int loadtypeid, String ponumber, String invoicenumber, String sealnumber, String trailernumber, String hauleridnumber, String weight, String billofladingnumber, int organizationid, int barcodeid, string guid, string loadNumber)
        {


            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@loadtypeid", SqlDbType.Int, 0, loadtypeid));

                    prm.Add(DB.MakeInParam("@ponumber", SqlDbType.NVarChar, 100, ponumber));
                    prm.Add(DB.MakeInParam("@invoicenumber", SqlDbType.NVarChar, 100, invoicenumber));
                    prm.Add(DB.MakeInParam("@sealnumber", SqlDbType.NVarChar, 100, sealnumber));
                    prm.Add(DB.MakeInParam("@trailernumber", SqlDbType.NVarChar, 100, trailernumber));
                    prm.Add(DB.MakeInParam("@hauleridnumber", SqlDbType.NVarChar, 100, hauleridnumber));
                    prm.Add(DB.MakeInParam("@weight", SqlDbType.NVarChar, 100, weight));
                    prm.Add(DB.MakeInParam("@billofladingnumber", SqlDbType.NVarChar, 100, billofladingnumber));
                    prm.Add(DB.MakeInParam("@organizationId", SqlDbType.Int, 4, organizationid));
                    prm.Add(DB.MakeInParam("@intBarcodeId", SqlDbType.Int, 4, barcodeid));
                    prm.Add(DB.MakeInParam("@vchGuid", SqlDbType.NVarChar, 500, guid));
                    prm.Add(DB.MakeInParam("@vchLoadNumber", SqlDbType.NVarChar, 20, loadNumber));

                    int exec = DB.RunProc("up_load_insert", prm.ToArray());
                    return exec;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads.InsertLoad", ex);
            }
            return 0;
        }



        public static bool addLoadTires(int loadId, string tireIds, int userId)
        {

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));
                    prm.Add(DB.MakeInParam("@vchTireIds", SqlDbType.NVarChar, 255, tireIds));
                    prm.Add(DB.MakeInParam("@intUserId", SqlDbType.Int, 4, userId));
                    DB.RunProc("up_InsertLoadTires", prm.ToArray());
                    return true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads AddLoadTires", ex);
            }
            return false;
        }




        public static int InsertLoad(int loadtypeid, string ponumber, string invoicenumber, string sealnumber, string trailernumber,
             int hauleridnumber, string weight, string billofladingnumber, int organizationid, int barcodeid, string guid, string loadNumber, int UserId, int catid, int LotID = 0, int SpaceID = 0, int LaneID = 0)
        {


            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@loadtypeid", SqlDbType.Int, 4, loadtypeid));

                    if (LotID > 0)
                        prm.Add(DB.MakeInParam("@intLotID", SqlDbType.Int, 4, LotID));
                    else
                        prm.Add(DB.MakeInParam("@intLotID", SqlDbType.Int, 4, DBNull.Value));

                    if (SpaceID > 0)
                        prm.Add(DB.MakeInParam("@intSpaceID", SqlDbType.Int, 4, SpaceID));
                    else
                        prm.Add(DB.MakeInParam("@intSpaceID", SqlDbType.Int, 4, DBNull.Value));

                    if (LaneID > 0)
                        prm.Add(DB.MakeInParam("@intLaneID", SqlDbType.Int, 4, LaneID));
                    else
                        prm.Add(DB.MakeInParam("@intLaneID", SqlDbType.Int, 4, DBNull.Value));



                    if (!string.IsNullOrEmpty(ponumber))
                        prm.Add(DB.MakeInParam("@ponumber", SqlDbType.NVarChar, 100, ponumber));
                    else
                        prm.Add(DB.MakeInParam("@ponumber", SqlDbType.NVarChar, 100, DBNull.Value));


                    if (!string.IsNullOrEmpty(invoicenumber))
                        prm.Add(DB.MakeInParam("@invoicenumber", SqlDbType.NVarChar, 100, invoicenumber));
                    else
                        prm.Add(DB.MakeInParam("@invoicenumber", SqlDbType.NVarChar, 100, DBNull.Value));

                    if (!string.IsNullOrEmpty(sealnumber))
                        prm.Add(DB.MakeInParam("@sealnumber", SqlDbType.NVarChar, 100, sealnumber));
                    else
                        prm.Add(DB.MakeInParam("@sealnumber", SqlDbType.NVarChar, 100, DBNull.Value));


                    if (!string.IsNullOrEmpty(trailernumber))
                        prm.Add(DB.MakeInParam("@trailernumber", SqlDbType.NVarChar, 100, trailernumber));
                    else
                        prm.Add(DB.MakeInParam("@trailernumber", SqlDbType.NVarChar, 100, DBNull.Value));


                    if (hauleridnumber > 0)
                        prm.Add(DB.MakeInParam("@hauleridnumber", SqlDbType.Int, 4, hauleridnumber));
                    else
                        prm.Add(DB.MakeInParam("@hauleridnumber", SqlDbType.Int, 4, DBNull.Value));


                    if (!string.IsNullOrEmpty(weight))
                        prm.Add(DB.MakeInParam("@weight", SqlDbType.NVarChar, 100, weight));
                    else
                        prm.Add(DB.MakeInParam("@weight", SqlDbType.NVarChar, 100, DBNull.Value));

                    if (!string.IsNullOrEmpty(billofladingnumber))
                        prm.Add(DB.MakeInParam("@billofladingnumber", SqlDbType.NVarChar, 100, billofladingnumber));
                    else
                        prm.Add(DB.MakeInParam("@billofladingnumber", SqlDbType.NVarChar, 100, DBNull.Value));


                    prm.Add(DB.MakeInParam("@organizationId", SqlDbType.Int, 4, organizationid));

                    prm.Add(DB.MakeInParam("@intBarcodeId", SqlDbType.Int, 4, barcodeid));
                    prm.Add(DB.MakeInParam("@vchGuid", SqlDbType.NVarChar, 500, guid));
                    prm.Add(DB.MakeInParam("@intUserId", SqlDbType.Int, 4, UserId));
                    if (!string.IsNullOrEmpty(loadNumber))
                        prm.Add(DB.MakeInParam("@vchLoadNumber", SqlDbType.NVarChar, 100, loadNumber));
                    else
                        prm.Add(DB.MakeInParam("@vchLoadNumber", SqlDbType.NVarChar, 100, DBNull.Value));

                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 4, catid));

                    int exec = DB.RunProc("up_load_insert", prm.ToArray());
                    return exec;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads .InsertLoad", ex);
            }
            return 0;
        }



        public static bool InsertLoadTireForReceving(int LoadId, int ProductId, int UserId)
        {


            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 0, LoadId));

                    prm.Add(DB.MakeInParam("@intProductId", SqlDbType.Int, 0, ProductId));

                    prm.Add(DB.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));

                    int exec = DB.RunProc("up_insertLoadTiresForRecieving", prm.ToArray());
                    if (exec > 0)
                        return true;
                    else
                        return false;


                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.InsertRecieveLoad", ex);
            }
            return false;
        }


        public static int InsertRecieveLoad(int loadtypeid, String ponumber, String invoicenumber, String sealnumber, String trailernumber,
               String weight, String billofladingnumber, int organizationid, int UserId, int barcodeid, string guid, string loadNumber, int CatId,
            int LotID = 0, int SpaceID = 0, int LaneID = 0)
        {


            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@loadtypeid", SqlDbType.Int, 0, loadtypeid));

                    if (LotID > 0)
                        prm.Add(DB.MakeInParam("@intLotID", SqlDbType.Int, 0, LotID));
                    else
                        prm.Add(DB.MakeInParam("@intLotID", SqlDbType.Int, 0, DBNull.Value));

                    if (SpaceID > 0)
                        prm.Add(DB.MakeInParam("@intSpaceID", SqlDbType.Int, 0, SpaceID));
                    else
                        prm.Add(DB.MakeInParam("@intSpaceID", SqlDbType.Int, 0, DBNull.Value));

                    if (LaneID > 0)
                        prm.Add(DB.MakeInParam("@intLaneID", SqlDbType.Int, 0, LaneID));
                    else
                        prm.Add(DB.MakeInParam("@intLaneID", SqlDbType.Int, 0, DBNull.Value));



                    if (!string.IsNullOrEmpty(ponumber))
                        prm.Add(DB.MakeInParam("@ponumber", SqlDbType.NVarChar, 100, ponumber));
                    else
                        prm.Add(DB.MakeInParam("@ponumber", SqlDbType.NVarChar, 100, DBNull.Value));


                    if (!string.IsNullOrEmpty(invoicenumber))
                        prm.Add(DB.MakeInParam("@invoicenumber", SqlDbType.NVarChar, 100, invoicenumber));
                    else
                        prm.Add(DB.MakeInParam("@invoicenumber", SqlDbType.NVarChar, 100, DBNull.Value));

                    if (!string.IsNullOrEmpty(sealnumber))
                        prm.Add(DB.MakeInParam("@sealnumber", SqlDbType.NVarChar, 100, sealnumber));
                    else
                        prm.Add(DB.MakeInParam("@sealnumber", SqlDbType.NVarChar, 100, DBNull.Value));


                    if (!string.IsNullOrEmpty(trailernumber))
                        prm.Add(DB.MakeInParam("@trailernumber", SqlDbType.NVarChar, 100, trailernumber));
                    else
                        prm.Add(DB.MakeInParam("@trailernumber", SqlDbType.NVarChar, 100, DBNull.Value));




                    if (!string.IsNullOrEmpty(weight))
                        prm.Add(DB.MakeInParam("@weight", SqlDbType.NVarChar, 100, weight));
                    else
                        prm.Add(DB.MakeInParam("@weight", SqlDbType.NVarChar, 100, DBNull.Value));

                    if (!string.IsNullOrEmpty(weight))
                        prm.Add(DB.MakeInParam("@billofladingnumber", SqlDbType.NVarChar, 100, billofladingnumber));
                    else
                        prm.Add(DB.MakeInParam("@billofladingnumber", SqlDbType.NVarChar, 100, DBNull.Value));


                    prm.Add(DB.MakeInParam("@organizationId", SqlDbType.Int, 4, organizationid));
                    prm.Add(DB.MakeInParam("@UserId", SqlDbType.Int, 4, UserId));

                    prm.Add(DB.MakeInParam("@intBarcodeId", SqlDbType.Int, 4, barcodeid));
                    prm.Add(DB.MakeInParam("@vchGuid", SqlDbType.NVarChar, 500, guid));

                    if (!string.IsNullOrEmpty(loadNumber))
                        prm.Add(DB.MakeInParam("@vchLoadNumber", SqlDbType.NVarChar, 100, loadNumber));
                    else
                        prm.Add(DB.MakeInParam("@vchLoadNumber", SqlDbType.NVarChar, 100, DBNull.Value));
                    prm.Add(DB.MakeInParam("@categoryId", SqlDbType.Int, 0, CatId));


                    int exec = DB.RunProc("up_shipload_insert", prm.ToArray());
                    return exec;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Lots.InsertRecieveLoad", ex);
            }
            return 0;
        }


        public static bool AddLoadTires(int loadId, string tireIds, int userId)
        {

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));
                    prm.Add(DB.MakeInParam("@vchTireIds", SqlDbType.NVarChar, 255, tireIds));
                    prm.Add(DB.MakeInParam("@intUserId", SqlDbType.Int, 4, userId));
                    //prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 0, CatId));
                    DB.RunProc("up_InsertLoadTires", prm.ToArray());
                    return true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo AddLoadTires", ex);
            }
            return false;
        }

        public static bool transferLoadTiresToPermenentLot(int LoadId, int spaceId, int laneId)
        {
            bool flag = false;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, LoadId));
                    prm.Add(DB.MakeInParam("@intSpaceId", SqlDbType.Int, 4, spaceId));
                    prm.Add(DB.MakeInParam("@intLaneId", SqlDbType.Int, 4, laneId));
                    int exec = DB.RunProc("up_ShiftLoadTireToPermenentLot", prm.ToArray());
                    return true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads.transferLoadTiresToPermenentLot", ex);
            }
            return flag;
        }

        public static bool AddLoadTiresbyBarcode(int loadId, string barcode, int userId)
        {

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));
                    prm.Add(DB.MakeInParam("@vchTireBarcode", SqlDbType.NVarChar, 255, barcode));
                    prm.Add(DB.MakeInParam("@intUserId", SqlDbType.Int, 4, userId));
                    DB.RunProc("up_InsertLoadTiresFromBarcode", prm.ToArray());
                    return true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo AddLoadTiresbyBarcode", ex);
            }
            return false;
        }
        public static bool RejectLoadShip(int loadId, bool flag)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));
                    prm.Add(DB.MakeInParam("@bitReject", SqlDbType.Bit, 0, flag));

                    DB.RunProc("up_RejectLoadShip", prm.ToArray());
                    return true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotsInfo RejectLoadShip", ex);
            }
            return false;
        }


        public static void updateLoadByLoadTypeId(int loadId, int loadTypeId)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));
                    prm.Add(DB.MakeInParam("@intLoadTypeId", SqlDbType.Int, 4, loadTypeId));

                    DB.RunProc("up_updateLoadbyLoadIdandLoadTypeId", prm.ToArray());


                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads updateLoadByLoadTypeId", ex);
            }

        }


        public static void rejectLoadByLoadId(int loadId)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));

                    DB.RunProc("up_rejectLoadByLoadId", prm.ToArray());


                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads rejectLoadByLoadId", ex);
            }

        }
        public static void acceptLoadByLoadId(int loadId, int LoadTypeId = 74)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));
                    prm.Add(DB.MakeInParam("@intLoadTypeId", SqlDbType.Int, 4, LoadTypeId));

                    DB.RunProc("up_acceptLoadByLoadId", prm.ToArray());


                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads acceptLoadByLoadId", ex);
            }

        }


        public static DataSet getDeliveryLoadByLoadId(int loadId)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));
                    ds = DB.GetDataSet("up_loads_byDelivery", prm.ToArray());
                    return ds;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads acceptLoadByLoadId", ex);
            }
            return ds;

        }

        public static void updateRecieveLoad(int loadId, int loadtypeId, string PONumber, string InvoiceNumber, string SealNumber, string TrailerNumber, string Weight, string BillOfLadingNumber, int OrganizationId, int UserId, int BarCodeId, string GUID, string LoadNumber)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));
                    prm.Add(DB.MakeInParam("@intLoadTypeId", SqlDbType.Int, 4, loadtypeId));
                    prm.Add(DB.MakeInParam("@ponumber", SqlDbType.NVarChar, 100, PONumber));
                    prm.Add(DB.MakeInParam("@invoicenumber", SqlDbType.NVarChar, 100, InvoiceNumber));
                    prm.Add(DB.MakeInParam("@sealnumber", SqlDbType.NVarChar, 100, SealNumber));
                    prm.Add(DB.MakeInParam("@trailernumber", SqlDbType.NVarChar, 100, TrailerNumber));
                    prm.Add(DB.MakeInParam("@weight", SqlDbType.NVarChar, 100, Weight));
                    prm.Add(DB.MakeInParam("@billofladingnumber", SqlDbType.NVarChar, 100, BillOfLadingNumber));
                    prm.Add(DB.MakeInParam("@organizationId", SqlDbType.Int, 4, OrganizationId));
                    prm.Add(DB.MakeInParam("@UserId", SqlDbType.Int, 4, UserId));
                    prm.Add(DB.MakeInParam("@ModifiedDate", SqlDbType.DateTime, 0, DateTime.Now));
                    prm.Add(DB.MakeInParam("@intBarcodeId", SqlDbType.Int, 4, BarCodeId));
                    prm.Add(DB.MakeInParam("@vchGuid", SqlDbType.NVarChar, 500, GUID));
                    prm.Add(DB.MakeInParam("@vchLoadNumber", SqlDbType.NVarChar, 100, LoadNumber));




                    DB.RunProc("up_updateRecieveLoad", prm.ToArray());


                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads acceptLoadByLoadId", ex);
            }

        }
        public static void updateTransferLoad(int loadId, int loadtypeId, int lotid, int spaceId, int laneid, int OrganizationId, int UserId, int BarCodeId, string GUID)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));
                    prm.Add(DB.MakeInParam("@intLoadTypeId", SqlDbType.Int, 4, loadtypeId));
                    prm.Add(DB.MakeInParam("@intLotID", SqlDbType.Int, 4, lotid));
                    prm.Add(DB.MakeInParam("@intSpaceID", SqlDbType.Int, 4, spaceId));
                    prm.Add(DB.MakeInParam("@intLaneID", SqlDbType.Int, 4, laneid));

                    prm.Add(DB.MakeInParam("@organizationId", SqlDbType.Int, 4, OrganizationId));
                    prm.Add(DB.MakeInParam("@UserId", SqlDbType.Int, 4, UserId));
                    prm.Add(DB.MakeInParam("@ModifiedDate", SqlDbType.DateTime, 0, DateTime.Now));
                    prm.Add(DB.MakeInParam("@intBarcodeId", SqlDbType.Int, 4, BarCodeId));
                    prm.Add(DB.MakeInParam("@vchGuid", SqlDbType.NVarChar, 500, GUID));

                    DB.RunProc("up_updateTransferLoad", prm.ToArray());


                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads acceptLoadByLoadId", ex);
            }

        }
        public static void updatePendingAndShipLoad(int loadId, int loadtypeId, string PONumber, string InvoiceNumber, string SealNumber, string TrailerNumber, string Weight, string BillOfLadingNumber, int OrganizationId, int UserId, int HaulerNumber, int BarCodeId, string GUID, string LoadNumber)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLoadId", SqlDbType.Int, 4, loadId));
                    prm.Add(DB.MakeInParam("@intLoadTypeId", SqlDbType.Int, 4, loadtypeId));
                    prm.Add(DB.MakeInParam("@ponumber", SqlDbType.NVarChar, 100, PONumber));
                    prm.Add(DB.MakeInParam("@invoicenumber", SqlDbType.NVarChar, 100, InvoiceNumber));
                    prm.Add(DB.MakeInParam("@sealnumber", SqlDbType.NVarChar, 100, SealNumber));
                    prm.Add(DB.MakeInParam("@trailernumber", SqlDbType.NVarChar, 100, TrailerNumber));
                    prm.Add(DB.MakeInParam("@weight", SqlDbType.NVarChar, 100, Weight));
                    prm.Add(DB.MakeInParam("@billofladingnumber", SqlDbType.NVarChar, 100, BillOfLadingNumber));
                    prm.Add(DB.MakeInParam("@organizationId", SqlDbType.Int, 4, OrganizationId));
                    prm.Add(DB.MakeInParam("@UserId", SqlDbType.Int, 4, UserId));
                    prm.Add(DB.MakeInParam("@ModifiedDate", SqlDbType.DateTime, 0, DateTime.Now));
                    prm.Add(DB.MakeInParam("@inthaulerId", SqlDbType.Int, 4, HaulerNumber));
                    prm.Add(DB.MakeInParam("@intBarcodeId", SqlDbType.Int, 4, BarCodeId));
                    prm.Add(DB.MakeInParam("@vchGuid", SqlDbType.NVarChar, 500, GUID));
                    prm.Add(DB.MakeInParam("@vchLoadNumber", SqlDbType.NVarChar, 100, LoadNumber));




                    DB.RunProc("up_updatePendingAndShipLoad", prm.ToArray());


                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads acceptLoadByLoadId", ex);
            }

        }

        #endregion


        //Muhammad Omer
        public static DataSet GetPteByTireId(int StateId, int OrganizationSubTypeId, string TireIds)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prm = new List<SqlParameter>();
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, StateId));
                    prm.Add(DB.MakeInParam("@OrganiztionId", SqlDbType.Int, 4, OrganizationSubTypeId));
                    prm.Add(DB.MakeInParam("@TireIdArray", SqlDbType.NVarChar, 500, TireIds));

                    ds = DB.GetDataSet("Get_PteDetailByTireId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
                return ds;

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads GetPteByTireId", ex);
                return ds;
            }
           
        }
        public static DataSet GetPteByProductId(int StateId, int OrganizationSubTypeId, string TireIds, int catid)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prm = new List<SqlParameter>();
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, StateId));
                    prm.Add(DB.MakeInParam("@OrganiztionId", SqlDbType.Int, 4, OrganizationSubTypeId));
                    prm.Add(DB.MakeInParam("@ProductIdArray", SqlDbType.NVarChar, 500, TireIds));
                    prm.Add(DB.MakeInParam("@categoryId", SqlDbType.Int, 4, catid));
                    ds = DB.GetDataSet("Get_PteDetailByProductId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
                return ds;

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads Get_PteDetailByProductId", ex);
                return ds;
            }

        }

    }
}
