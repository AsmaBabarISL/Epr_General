using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace TireTraxLib
{
    public class Invoice
    {
        #region Invoice
        private int _InvoiceID;

        public int InvoiceID
        {
            get { return _InvoiceID; }
            set { _InvoiceID = value; }
        }
        private DateTime _InvoiceDate;

        public DateTime InvoiceDate
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }
        private decimal _InvoiceAmount;

        public decimal InvoiceAmount
        {
            get { return _InvoiceAmount; }
            set { _InvoiceAmount = value; }
        }
        private DateTime _DueDate;

        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        private int _Organizationid;

        public int Organizationid
        {
            get { return _Organizationid; }
            set { _Organizationid = value; }
        }
        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private int _CreatedBy;

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        private DateTime _DateCreated;

        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }
        private int _ModifiedBy;

        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        private DateTime _DateModified;

        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }
        private bool _Active;

        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        private bool _IsPaid;

        public bool IsPaid
        {
            get { return _IsPaid; }
            set { _IsPaid = value; }
        }
        private int _OrganizationForId;

        public int OrganizationForId
        {
            get { return _OrganizationForId; }
            set { _OrganizationForId = value; }
        }
        private string _DeliveryIDs;

        public string DeliveryIDs
        {
            get { return _DeliveryIDs; }
            set { _DeliveryIDs = value; }
        }
        private bool _IsSent;

        public bool IsSent
        {
            get { return _IsSent; }
            set { _IsSent = value; }
        }

        private string _InvoiceNumber;

        public string InvoiceNumber
        {
            get { return _InvoiceNumber; }
            set { _InvoiceNumber = value; }
        }

        private int _ProductCategoryId;

        public int ProductCategoryId
        {
            get { return _ProductCategoryId; }
            set { _ProductCategoryId = value; }
        }


        #endregion
        #region Invoice_Aggregate
        private int _InvoiceaggregateID;

        public int InvoiceaggregateID
        {
            get { return _InvoiceaggregateID; }
            set { _InvoiceaggregateID = value; }
        }
        private decimal _PaymentAmount;

        public decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
        private decimal _BalanceAmount;

        public decimal BalanceAmount
        {
            get { return _BalanceAmount; }
            set { _BalanceAmount = value; }
        }
        private decimal _comission;

        public decimal Comission
        {
            get { return _comission; }
            set { _comission = value; }
        }
        private int _Deliveryid;

        public int Deliveryid
        {
            get { return _Deliveryid; }
            set { _Deliveryid = value; }
        }

        private string _deliveryIds;

        public string DeliveryIds
        {
            get { return _deliveryIds; }
            set { _deliveryIds = value; }
        }
        private string _OrganizationForName;

        public string OrganizationForName
        {
            get { return _OrganizationForName; }
            set { _OrganizationForName = value; }
        }
        #endregion


        #region Load Function
        public Invoice() { }
        public Invoice(int invoiceId)
        {
            Load(invoiceId);
        }
        private void Load(int invoiceId)
        {
            IDataReader reader = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@invoiceId", SqlDbType.Int, 0, invoiceId));
                    reader = db.GetDataReader("up_Invoice_getInvoiceId", prams.ToArray());
                    if (reader.Read())
                        LoadInvoice(reader);
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.invoice", ex);
            }
        }
        private void LoadInvoice(IDataReader reader)
        {
            try
            {
                _InvoiceID = Conversion.ParseDBNullInt(reader["InvoiceID"]);
                _InvoiceDate = Conversion.ParseDBNullDateTime(reader["InvoiceDate"]);
                _InvoiceAmount = Conversion.ParseDBNullDecimal(reader["InvoiceAmount"]);
                _DueDate = Conversion.ParseDBNullDateTime(reader["DueDate"]);
                _Organizationid = Conversion.ParseDBNullInt(reader["Organizationid"]);
                _status = Conversion.ParseDBNullString(reader["status"]);
                _CreatedBy = Conversion.ParseDBNullInt(reader["CreatedBy"]);
                _DateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
                _ModifiedBy = Conversion.ParseDBNullInt(reader["ModifiedBy"]);
                _DateModified = Conversion.ParseDBNullDateTime(reader["DateModified"]);
                _Active = Conversion.ParseDBNullBool(reader["Active"]);
                _IsPaid = Conversion.ParseDBNullBool(reader["IsPaid"]);
                _OrganizationForId = Conversion.ParseDBNullInt(reader["OrganizationForId"]);
                _IsSent = Conversion.ParseDBNullBool(reader["IsSent"]);
                //_deliveryIds = Conversion.ParseDBNullString(reader["Deliveryids"]);
                _OrganizationForName = Conversion.ParseDBNullString(reader["OrganizationForName"]);
                _InvoiceNumber = Conversion.ParseDBNullString(reader["InvoiceNumber"]);
                _ProductCategoryId = Conversion.ParseDBNullInt(reader["ProductCategoryid"]);
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.LoadInvoice", ex);
            }
        }

        public static DataSet getAllInvoices(int OrgId, int pageNo, int pagesize, out int iTotalrows, DateTime dateFrom, DateTime dateTo, string orgName, string invoiceNo, int CatId, int status)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {

                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@Organizationid", SqlDbType.Int, 0, OrgId));
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageNo));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pagesize));
                    if (dateFrom == DateTime.MinValue)
                        prams.Add(db.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 0, dateFrom));
                    if (dateTo == DateTime.MinValue)
                        prams.Add(db.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 0, dateTo));

                    if (string.IsNullOrEmpty(orgName))
                        prams.Add(db.MakeInParam("@OrgName", SqlDbType.NVarChar, 1000, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@OrgName", SqlDbType.NVarChar, 1000, orgName));
                    if (string.IsNullOrEmpty(invoiceNo))
                        prams.Add(db.MakeInParam("@invoicenum", SqlDbType.VarChar, 1000, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@invoicenum", SqlDbType.VarChar, 1000, invoiceNo));

                    if (CatId == 0)
                        prams.Add(db.MakeInParam("@CategoryId", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CategoryId", SqlDbType.Int, 0, CatId));
                    prams.Add(db.MakeInParam("@Status", SqlDbType.Int, 0, status));

                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_InvoicesForPublic_getAllInvoicesByOrgID", prams.ToArray());
                    //ds = db.GetDataSet("up_Invoices_getAllInvoicesByOrgID", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            { new SqlLog().InsertSqlLog(0, "Invoice.GetAllInvoices", ex); }

            return ds;
        }

        public static DataSet getAllInvoicesAdmin(int OrgId, int pageNo, int pagesize, out int iTotalrows, DateTime dateFrom, DateTime dateTo, string orgName, string invoiceNo, int CatId, int status)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {

                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@Organizationid", SqlDbType.Int, 0, OrgId));
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageNo));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pagesize));
                    if (dateFrom == DateTime.MinValue)
                        prams.Add(db.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@dtmFrmDate", SqlDbType.DateTime, 0, dateFrom));
                    if (dateTo == DateTime.MinValue)
                        prams.Add(db.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@dtmToDateTime", SqlDbType.DateTime, 0, dateTo));

                    if (string.IsNullOrEmpty(orgName))
                        prams.Add(db.MakeInParam("@OrgName", SqlDbType.NVarChar, 1000, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@OrgName", SqlDbType.NVarChar, 1000, orgName));
                    if (string.IsNullOrEmpty(invoiceNo))
                        prams.Add(db.MakeInParam("@invoicenum", SqlDbType.VarChar, 1000, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@invoicenum", SqlDbType.VarChar, 1000, invoiceNo));

                    if (CatId == 0)
                        prams.Add(db.MakeInParam("@CategoryId", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CategoryId", SqlDbType.Int, 0, CatId));
                    prams.Add(db.MakeInParam("@Status", SqlDbType.Int, 0, status));

                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_InvoicesForAdmin_getAllInvoicesByOrgID", prams.ToArray());
                    //ds = db.GetDataSet("up_Invoices_getAllInvoicesByOrgID", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            { new SqlLog().InsertSqlLog(0, "Invoice.getAllInvoicesAdmin", ex); }

            return ds;
        }

        public static DataSet getAllUnAssignedDeliveries(int OrgId)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@Organizationid", SqlDbType.Int, 0, OrgId));
                    ds = db.GetDataSet("up_delivery_getunassignedDeliveries", prams.ToArray());
                }

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.getAllUnAssignedDeliveries", ex);
            }
            return ds;
        }
        public static DataSet getInvoiceTiresInfo(int OrgId, string invoiceIds)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@Organizationid", SqlDbType.Int, 0, OrgId));
                    prams.Add(db.MakeInParam("@invoiceid", SqlDbType.Int, 0, invoiceIds));

                    ds = db.GetDataSet("Get_PTEamount_aggregated_Byinvoiceid", prams.ToArray());
                }

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.getInvoiceTiresInfo", ex);
            }
            return ds;
        }

        public static DataSet GetInvoiceProductInfo(int OrgId, int invoiceId)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@OrgId", SqlDbType.Int, 0, OrgId));
                    prams.Add(db.MakeInParam("@invoiceId", SqlDbType.Int, 0, invoiceId));
                    ds = db.GetDataSet("up_GetInvoiceForProductByInvoiceId", prams.ToArray());
                }

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.GetInvoiceProductInfo", ex);
            }
            return ds;
        }

        public static DataSet getDeliveryInvoices(int OrgId, string deliveryIds)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@Organizationid", SqlDbType.Int, 0, OrgId));
                    prams.Add(db.MakeInParam("@deliveryIds", SqlDbType.VarChar, 1000, deliveryIds));
                    ds = db.GetDataSet("Get_PTEamount_aggregated", prams.ToArray());
                    return ds;
                }
            }
            catch (Exception ex)
            { new SqlLog().InsertSqlLog(0, "Invoice.getDeliveryInvoices", ex); }

            return ds;
        }


        public static DataSet getDeliveryInvoicesAggreatedAmount(string deliveryIds)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@deliveryIds", SqlDbType.VarChar, 1000, deliveryIds));
                    ds = db.GetDataSet("Get_PTEamount_aggregated_forinvoice", prams.ToArray());
                    return ds;
                }
            }
            catch (Exception ex)
            { new SqlLog().InsertSqlLog(0, "Invoice.getDeliveryInvoices", ex); }

            return ds;
        }
        #endregion



        public static bool Invoice_InsertUpdate(Invoice objInvoice)
        {

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@pInvoiceID", SqlDbType.Int, 0, objInvoice.InvoiceID));
                    prm.Add(DB.MakeInParam("@pInvoiceDate", SqlDbType.DateTime, 0, objInvoice.InvoiceDate));
                    prm.Add(DB.MakeInParam("@pInvoiceAmount", SqlDbType.Money, 0, objInvoice.InvoiceAmount));
                    prm.Add(DB.MakeInParam("@pDueDate", SqlDbType.DateTime, 0, objInvoice.DueDate));
                    prm.Add(DB.MakeInParam("@pOrganizationid", SqlDbType.Int, 0, objInvoice.Organizationid));
                    prm.Add(DB.MakeInParam("@pstatus", SqlDbType.VarChar, 0, objInvoice.Status));
                    prm.Add(DB.MakeInParam("@pCreatedBy", SqlDbType.Int, 0, objInvoice.CreatedBy));
                    prm.Add(DB.MakeInParam("@pDateCreated", SqlDbType.DateTime, 0, objInvoice.DateCreated));
                    prm.Add(DB.MakeInParam("@pModifiedBy", SqlDbType.Int, 0, objInvoice.ModifiedBy));
                    prm.Add(DB.MakeInParam("@pDateModified", SqlDbType.DateTime, 0, objInvoice.DateModified));
                    prm.Add(DB.MakeInParam("@pActive", SqlDbType.Bit, 0, objInvoice.Active));
                    prm.Add(DB.MakeInParam("@pIsPaid", SqlDbType.Bit, 0, objInvoice.IsPaid));
                    prm.Add(DB.MakeInParam("@pOrganizationForId", SqlDbType.Int, 0, objInvoice.OrganizationForId));
                    prm.Add(DB.MakeInParam("@pDeliveryIds", SqlDbType.VarChar, 0, objInvoice.DeliveryIDs));
                    prm.Add(DB.MakeInParam("@pIsSent", SqlDbType.Bit, 0, objInvoice.IsSent));
                    prm.Add(DB.MakeInParam("@pInvoiceNumber", SqlDbType.VarChar, 50, objInvoice._InvoiceNumber));
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 0, objInvoice._ProductCategoryId));
                    int result = DB.RunProc("UP_invoice_InsertUpdate", prm.ToArray());
                    return true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice Invoice_InsertUpdate", ex);
            }
            return false;
        }
        public static bool Invoice_IsPaid(int invoiceID)
        {

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@InvoiceID", SqlDbType.Int, 0, invoiceID));
                    int result = DB.RunProc("up_invoice_IsPaid ", prm.ToArray());
                    return true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice Invoice_IsPaid", ex);
            }
            return false;
        }
        public static bool Invoice_IsSend(int invoiceID)
        {

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@InvoiceID", SqlDbType.Int, 0, invoiceID));
                    int result = DB.RunProc("up_invoice_IsSend ", prm.ToArray());
                    return true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice Invoice_IsSend", ex);
            }
            return false;
        }

        public static DataSet GetInvoiceForReport(int InvoiceId)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@invoiceId", SqlDbType.Int, 0, InvoiceId));
                    ds = db.GetDataSet("up_Invoice_getInvoiceId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.GetInvoiceForReport", ex);
            }
            return ds;
        }




        public static DataSet GetInvoiceAmountForProduct(int deliveryId)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@DeliverId", SqlDbType.Int, 0, deliveryId));
                    ds = db.GetDataSet("Up_Invoice_GetAmountForProduct", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.GetInvoiceAmountForProduct", ex);
            }
            return ds;
        }

        public static DataSet GetInvoiceDetailsByInvoiceId(int InvoiceID)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@invoiceId", SqlDbType.Int, 0, InvoiceID));
                    ds = db.GetDataSet("up_Invoice_GetDetailsByInvoiceId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.GetInvoiceDetailsByInvoiceId", ex);
            }
            return ds;
        }

        public static DataSet GetAggregateInvoiceForPublic(int intPageId, int intPageSize, int OrgId, int CatId, int Month, int Year, out int Totalrows)
        {
            DataSet ds = null;
            Totalrows = 0;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, OrgId));
                    prams.Add(db.MakeInParam("@CategoryId", SqlDbType.Int, 0, DBNull.Value));
                    if (Month == 0)
                        prams.Add(db.MakeInParam("@Month", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@Month", SqlDbType.Int, 0, Month));
                    prams.Add(db.MakeInParam("@Year", SqlDbType.Int, 0, Year));


                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, intPageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, intPageSize));

                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    


                    ds = db.GetDataSet("up_InvoicesForPublic_getAggregate", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Totalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.GetAggregateInvoiceForPublic", ex);
            }
            return ds;
        }



        public static DataSet GetAggregateInvoiceForPublicReport(int OrgId, int Month, int Year)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, OrgId));
                    //prams.Add(db.MakeInParam("@CategoryId", SqlDbType.Int, 0, CatId));
                    if (Month == 0)
                        prams.Add(db.MakeInParam("@Month", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@Month", SqlDbType.Int, 0, Month));
                    prams.Add(db.MakeInParam("@Year", SqlDbType.Int, 0, Year));


                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));



                    ds = db.GetDataSet("up_InvoicesForPublic_getAggregateReport", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.GetAggregateInvoiceForPublicReport", ex);
            }
            return ds;
        }

        public static DataSet GetAggregateInvoiceForAdmin(int intPageId, int intPageSize, int Month, int Year, out int Totalrows)
        {
            DataSet ds = null;
            Totalrows = 0;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, 2));
                    prams.Add(db.MakeInParam("@CategoryId", SqlDbType.Int, 0, DBNull.Value));
                    if (Month == 0)
                        prams.Add(db.MakeInParam("@Month", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@Month", SqlDbType.Int, 0, Month));

                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, intPageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, intPageSize));

                    prams.Add(db.MakeInParam("@Year", SqlDbType.Int, 0, Year));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    
                    ds = db.GetDataSet("up_InvoicesForAdmin_getAggregate", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Totalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.GetAggregateInvoiceForAdmin", ex);
            }
            return ds;
        }
        public static DataSet GetAggregateInvoiceForAdminReport(int Month, int Year)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, 2));
                    prams.Add(db.MakeInParam("@CategoryId", SqlDbType.Int, 0, DBNull.Value));
                    if (Month == 0)
                        prams.Add(db.MakeInParam("@Month", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@Month", SqlDbType.Int, 0, Month));
                    prams.Add(db.MakeInParam("@Year", SqlDbType.Int, 0, Year));


                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));



                    ds = db.GetDataSet("up_InvoicesForAdmin_getAggregateReport", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.GetAggregateInvoiceForAdminReport", ex);
            }
            return ds;
        }
    }
}
