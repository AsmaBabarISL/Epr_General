using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TireTraxLib
{
    /// <summary>
    /// Wajid Shah
    /// 17-11-2014
    /// use to get or set the delivery data into or from Database.
    /// </summary>
    public class Delivery
    {

        #region Properties
        private int _deliveryID;

        public int DeliveryID
        {
            get { return _deliveryID; }
            set { _deliveryID = value; }
        }
        private string _deliveryName;

        public string DeliveryName
        {
            get { return _deliveryName; }
            set { _deliveryName = value; }
        }
        private int _Organizationid;

        public int Organizationid
        {
            get { return _Organizationid; }
            set { _Organizationid = value; }
        
        }
        private string _OrganizationName;

        public string OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }
        private int _OrganizationShipToId;

        public int OrganizationShipToId
        {
            get { return _OrganizationShipToId; }
            set { _OrganizationShipToId = value; }
        }
        private DateTime _DeliveryDate;

        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }
        private int _TranspotationType;

        public int TranspotationType
        {
            get { return _TranspotationType; }
            set { _TranspotationType = value; }
        }
        private int _OrganizationTransporterId;

        public int OrganizationTransporterId
        {
            get { return _OrganizationTransporterId; }
            set { _OrganizationTransporterId = value; }
        }
        private string _vehicleDetails;

        public string VehicleDetails
        {
            get { return _vehicleDetails; }
            set { _vehicleDetails = value; }
        }

        
        private DateTime _DeliveryEstimateDates;
        public DateTime DeliveryEstimateDates
        {
            get { return _DeliveryEstimateDates; }
            set { _DeliveryEstimateDates = value; }
        }
        private decimal _weight;

        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private int _createdby;

        public int Createdby
        {
            get { return _createdby; }
            set { _createdby = value; }
        }
        private DateTime _datecreated;

        public DateTime Datecreated
        {
            get { return _datecreated; }
            set { _datecreated = value; }
        }
        private int _modifiedby;

        public int Modifiedby
        {
            get { return _modifiedby; }
            set { _modifiedby = value; }
        }
        private DateTime _datemodified;

        public DateTime Datemodified
        {
            get { return _datemodified; }
            set { _datemodified = value; }
        }
        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private int _loadid;

        public int Loadid
        {
            get { return _loadid; }
            set { _loadid = value; }
        }
        private string _loadIds;

        public string LoadIds
        {
            get { return _loadIds; }
            set { _loadIds = value; }
        }
        private string _OrganizationShipTo;

        public string OrganizationShipTo
        {
            get { return _OrganizationShipTo; }
            set { _OrganizationShipTo = value; }
        }
        private string _OrganizationTransporter;

        public string OrganizationTransporter
        {
            get { return _OrganizationTransporter; }
            set { _OrganizationTransporter = value; }
        
        }

        private bool _IsDelivered;

        public bool IsDelivered
        {
            get { return _IsDelivered; }
            set { _IsDelivered = value; }
        }
        private bool _IsShipToAccepted;

        public bool IsShipToAccepted
        {
            get { return _IsShipToAccepted; }
            set { _IsShipToAccepted = value; }
        }
        private bool _IsShipToRejected;

        public bool IsShipToRejected
        {
            get { return _IsShipToRejected; }
            set { _IsShipToRejected = value; }
        }
        private bool _IsTranspoterAccepted;

        public bool IsTranspoterAccepted
        {
            get { return _IsTranspoterAccepted; }
            set { _IsTranspoterAccepted = value; }
        }
        private bool _IsTranspoterRejected;

        public bool IsTranspoterRejected
        {
            get { return _IsTranspoterRejected; }
            set { _IsTranspoterRejected = value; }
        }
        #endregion
        #region Constructor
        public Delivery() { }

        /// <summary>
        /// use to get the delivery data by id
        /// </summary>
        /// <param name="deliveryId"></param>
        public Delivery(int deliveryId)
        {
            Load(deliveryId);
        }

        #endregion
        #region Data Load Methods

        /// <summary>
        /// use to get the delivery data from Database and set the properties of Delivery class
        /// </summary>
        /// <param name="deliveryId"></param>
        private void Load(int deliveryId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var parms = new SqlParameter[1];
                    parms[0] = db.MakeInParam("@intDeliveryID", SqlDbType.Int, 0, deliveryId);
                    reader = db.GetDataReader("up_delivery_getbyDeliveryId", parms);
                    if (reader.Read())
                        LoadDelivery(reader);  //use to set the value of properties
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Delivery.Load", ex);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// Use to set the properties of Delivery Class
        /// </summary>
        /// <param name="reader"></param>
        private void LoadDelivery(IDataReader reader)
        {
            try
            {
                _isActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                _createdby = Conversion.ParseDBNullInt(reader["createdby"]);
                _datecreated = Conversion.ParseDBNullDateTime(reader["datecreated"]);
                _datemodified = Conversion.ParseDBNullDateTime(reader["datemodified"]);
                _DeliveryDate = Conversion.ParseDBNullDateTime(reader["DeliveryDate"]);
                _DeliveryEstimateDates = Conversion.ParseDBNullDateTime(reader["DeliveryEstimateDates"]);
                _deliveryID = Conversion.ParseDBNullInt(reader["deliveryID"]);
                _deliveryName = Conversion.ParseDBNullString(reader["deliveryName"]);
                _modifiedby = Conversion.ParseDBNullInt(reader["modifiedby"]);
                _Organizationid = Conversion.ParseDBNullInt(reader["Organizationid"]);
                _OrganizationName = Conversion.ParseDBNullString(reader["OrganizationName"]);
                _OrganizationTransporterId = Conversion.ParseDBNullInt(reader["OrganizationTransporterId"]);
                _OrganizationShipToId = Conversion.ParseDBNullInt(reader["OrganizationShipToId"]);
                _status = Conversion.ParseDBNullString(reader["status"]);
                _TranspotationType = Conversion.ParseDBNullInt(reader["TranspotationType"]);
                _vehicleDetails = Conversion.ParseDBNullString(reader["vehicleDetails"]);
                _weight = Conversion.ParseDBNullDecimal(reader["weight"]);
                _OrganizationTransporter = Conversion.ParseDBNullString(reader["OrganizationTransporter"]);
                _OrganizationShipTo = Conversion.ParseDBNullString(reader["OrganizationShipTo"]);
                _IsDelivered = Conversion.ParseDBNullBool(reader["IsDelivered"]);
                _IsShipToAccepted = Conversion.ParseDBNullBool(reader["IsShipToAccepted"]);
                _IsShipToRejected = Conversion.ParseDBNullBool(reader["IsShipToRejected"]);
                _IsTranspoterAccepted = Conversion.ParseDBNullBool(reader["IsTranspoterAccepted"]);
                _IsTranspoterRejected = Conversion.ParseDBNullBool(reader["IsTranspoterRejected"]);
                _loadIds = Conversion.ParseDBNullString(reader["loadIds"]);
                
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Delivery.LoadDelivery", e);
            }


        }

        public static DataSet LoadAllDeliveriesByOrgID(int OrgId, int pageId, int pageSize, out int iTotalrows, DateTime frmDate, DateTime toDate, string ShipToName, string DeliveryName, int CatId)
        {
            DataSet ds = null;
            iTotalrows = 0;
            try
            {
                List<SqlParameter> prams=new List<SqlParameter>();
                using (DbManager db=DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.BigInt, 0, pageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.BigInt, 0, pageSize));
                    if (string.IsNullOrEmpty(ShipToName))
                        prams.Add(db.MakeInParam("@ShipToName", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@ShipToName", SqlDbType.NVarChar, 255, ShipToName));

                    if (string.IsNullOrEmpty(DeliveryName))
                        prams.Add(db.MakeInParam("@DeliveryName", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@DeliveryName", SqlDbType.NVarChar, 255, DeliveryName));

                    
                    if (frmDate == DateTime.MinValue)
                        prams.Add(db.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 8, frmDate));
                    if (toDate == DateTime.MinValue)
                        prams.Add(db.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 8, toDate));

                    prams.Add(db.MakeInParam("@productCategoryId", SqlDbType.Int, 0, CatId));
                    prams.Add(db.MakeInParam("@Organizationid", SqlDbType.BigInt, 0, OrgId));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 4));
                    ds = db.GetDataSet("up_Delivery_GetAllDeliveriesByOrgid", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }

            }
            catch(Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Delivery.LoadAddDEliveriesByOrgID", ex);
            }
            return ds;
            
        }

        public static DataSet LoadAllReceivedDeliveriesByOrgID(int OrgId, int pageId, int pageSize, out int iTotalrows, DateTime frmDate, DateTime toDate, string ShipFromName, string DeliveryName, int CatId)
        {
            DataSet ds = null;
            iTotalrows = 0;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.BigInt, 0, pageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.BigInt, 0, pageSize));
                    if (string.IsNullOrEmpty(ShipFromName))
                        prams.Add(db.MakeInParam("@ShipFromName", SqlDbType.NVarChar, 2000, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@ShipFromName", SqlDbType.NVarChar, 2000, ShipFromName));

                    if (string.IsNullOrEmpty(DeliveryName))
                        prams.Add(db.MakeInParam("@DeliveryName", SqlDbType.NVarChar, 2000, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@DeliveryName", SqlDbType.NVarChar, 2000, DeliveryName));


                    if (frmDate == DateTime.MinValue)
                        prams.Add(db.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 8, frmDate));
                    if (toDate == DateTime.MinValue)
                        prams.Add(db.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 8, toDate));

                    prams.Add(db.MakeInParam("@Organizationid", SqlDbType.Int, 0, OrgId));

                    prams.Add(db.MakeInParam("@productCategoryId", SqlDbType.Int, 0, CatId));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 4));
                    ds = db.GetDataSet("up_Delivery_GetAllReceivedDeliveriesByOrgid", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Delivery.LoadAllReceivedDeliveriesByOrgID", ex);
            }
            return ds;

        }
        #endregion
        #region Insert Methods

        /// <summary>
        /// Use to insert or update the delivery data into database.
        /// </summary>
        /// <param name="objDelivery">Object of Delivery class which containt the data for insert update</param>
        /// <returns>return -1 if there is any error otherwise return greater then 0 values </returns>
        public static int InsertUpdateDelivery(Delivery objDelivery)
        {
            int result=-1;
            try
            {
                 List<SqlParameter> pram = new List<SqlParameter>();
               using (DbManager db=DbManager.GetDbManager())
               {
                   pram.Add(db.MakeInParam("@pDeliveryID", SqlDbType.Int, 0, objDelivery.DeliveryID));
                   pram.Add(db.MakeInParam("@pDeliveryName", SqlDbType.NVarChar, 0, objDelivery.DeliveryName));
                   pram.Add(db.MakeInParam("@pOrganizationid", SqlDbType.Int, 0, objDelivery.Organizationid));
                   pram.Add(db.MakeInParam("@pDeliveryDate", SqlDbType.DateTime, 0, objDelivery.DeliveryDate));
                   pram.Add(db.MakeInParam("@pTranspotationType", SqlDbType.Int, 0, objDelivery.TranspotationType));
                   pram.Add(db.MakeInParam("@pOrganizationShipToId", SqlDbType.Int, 0, objDelivery.OrganizationShipToId));
                   pram.Add(db.MakeInParam("@pOrganizationTransporterId", SqlDbType.Int, 0, objDelivery.OrganizationTransporterId));
                   pram.Add(db.MakeInParam("@pvehicleDetails", SqlDbType.NVarChar, 0, objDelivery.VehicleDetails));
                   pram.Add(db.MakeInParam("@pDeliveryEstimateDates", SqlDbType.DateTime, 0, objDelivery.DeliveryEstimateDates));
                   pram.Add(db.MakeInParam("@pweight", SqlDbType.Int, 0, objDelivery.Weight));
                   pram.Add(db.MakeInParam("@pstatus", SqlDbType.NVarChar, 0, objDelivery.Status));
                   pram.Add(db.MakeInParam("@loadids", SqlDbType.VarChar, 0, objDelivery.LoadIds));
                   pram.Add(db.MakeInParam("@pactive", SqlDbType.Int, 0, objDelivery.IsActive));
                   pram.Add(db.MakeInParam("@pcreatedby", SqlDbType.Int, 0, objDelivery.Createdby));
                   pram.Add(db.MakeInParam("@pdatecreated", SqlDbType.DateTime, 0, objDelivery.Datecreated));
                   pram.Add(db.MakeInParam("@pmodifiedby", SqlDbType.Int, 0, objDelivery.Modifiedby));
                   pram.Add(db.MakeInParam("@pdatemodified", SqlDbType.DateTime, 0, objDelivery.Datemodified));
                   result = db.RunProc("UP_DeliveryNotes_InsertUpdate", pram.ToArray());
               }

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Delivery.InsertDelivery", ex);
            }
            return result;
        }

        public static DataSet GetAllLoadsForDelivery(int pageId, int pageSize, out int iTotalrows, string userName
            , DateTime frmDate, DateTime toDate, int organizationId, int LoadTypeId, string OrganizationName,int CatId)
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
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 4, CatId));
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

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));


                    ds = DB.GetDataSet("up_Load_getAllLoadsDelivery", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Delivery.GetAllLoadsForDelivery", ex);
            }
            return ds;
        }
        #endregion


        #region Received Delivery
        
        public static void acceptRejectDeliveryByDeliveryIdOrgID(int DeliveryId,int OrgID,bool IsAccepted,bool IsRejected)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {

                    prm.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrgID));
                    prm.Add(db.MakeInParam("@DeliveryID", SqlDbType.Int, 4, DeliveryId));

                    if (IsAccepted)
                        prm.Add(db.MakeInParam("@IsAccepted", SqlDbType.Bit, 0, IsAccepted));
                    else
                        prm.Add(db.MakeInParam("@IsAccepted", SqlDbType.Bit, 0, DBNull.Value));

                    if (IsRejected)
                        prm.Add(db.MakeInParam("@IsRejected", SqlDbType.Bit, 0, IsRejected));
                    else
                        prm.Add(db.MakeInParam("@IsRejected", SqlDbType.Bit, 0, DBNull.Value));

                    db.RunProc("up_Delivery_AcceptedRejectedbyDeliveryId", prm.ToArray());


                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Delivery acceptRejectDeliveryByDeliveryIdOrgID", ex);
            }

        }
        #endregion
    }
}
