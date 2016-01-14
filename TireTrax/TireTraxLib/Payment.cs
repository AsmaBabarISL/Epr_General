using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireTraxLib
{
    public class Payment
    {
        #region Payment

        private int _PaymentId;

        public int PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; }
        }

        private int _PaymentTypeId;

        public int PaymentTypeId
        {
            get { return _PaymentTypeId; }
            set { _PaymentTypeId = value; }
        }

        private string _CheckNumber;

        public string CheckNumber
        {
            get { return _CheckNumber; }
            set { _CheckNumber = value; }
        }

        private decimal _PaymentAmount;

        public decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }

        private DateTime _PaymentDate;

        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }

        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private decimal _BalanceAmount;

        public decimal BalanceAmount
        {
            get { return _BalanceAmount; }
            set { _BalanceAmount = value; }
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

        private int _InvoiceId;

        public int InvoiceId
        {
            get { return _InvoiceId; }
            set { _InvoiceId = value; }
        }

        private string _CreditCardNumber;

        public string CreditCardNumber
        {
            get { return _CreditCardNumber; }
            set { _CreditCardNumber = value; }
        }
        


        #endregion

        #region Load

        public Payment() { }
        public Payment(int PaymentId)
        {
            LoadPayment(PaymentId);
        }

        private void LoadPayment(int PaymentId)
        {
            IDataReader reader = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@paymentId", SqlDbType.Int, 0, PaymentId));
                    reader = db.GetDataReader("up_Invoice_getInvoiceId", prams.ToArray());
                    if (reader.Read())
                        LaodPayment(reader);
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Invoice.invoice", ex);
            }
        }

        private void LaodPayment(IDataReader reader)
        {
            try
            {
                _PaymentId = Conversion.ParseDBNullInt(reader["Paymentid"]);
                _PaymentTypeId = Conversion.ParseDBNullInt(reader["PaymentTypeID"]);
                _CheckNumber = Conversion.ParseDBNullString(reader["ChequeNumber"]);
                _PaymentAmount = Conversion.ParseDBNullDecimal(reader["PaymentAmount"]);
                _PaymentDate = Conversion.ParseDBNullDateTime(reader["PaymentDate"]);
                _Status = Conversion.ParseDBNullString(reader["Status"]);
                _BalanceAmount = Conversion.ParseDBNullDecimal(reader["BalanceAmount "]);
                _CreatedBy = Conversion.ParseDBNullInt(reader["CreatedBy"]);
                _DateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
                _ModifiedBy = Conversion.ParseDBNullInt(reader["Modifiedby"]);
                _DateModified = Conversion.ParseDBNullDateTime(reader["DateModified"]);
                _Active = Conversion.ParseDBNullBool(reader["Active"]);
                _InvoiceId = Conversion.ParseDBNullInt(reader["InvoiceId"]);

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Payment.LoadPayment with reader overload", ex);
            }
        }

        #endregion

        public static bool UpdatePayment(Payment payment,int isPaid)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@Paymentid", SqlDbType.Int, 4, payment._PaymentId));
                    if (string.IsNullOrEmpty(payment._CheckNumber))
                        prm.Add(DB.MakeInParam("@ChequeNumber", SqlDbType.VarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@ChequeNumber", SqlDbType.VarChar, 50, payment._CheckNumber));
                    prm.Add(DB.MakeInParam("@DateModified ", SqlDbType.Date, 8, payment.DateModified));
                    prm.Add(DB.MakeInParam("@BalanceAmount ", SqlDbType.Int, 8, payment.BalanceAmount));
                    prm.Add(DB.MakeInParam("@IsPaid", SqlDbType.Int, 4, isPaid));
                    prm.Add(DB.MakeInParam("@InvoiceId", SqlDbType.Int, 4, payment._InvoiceId));
                    int result = DB.RunProc("up_Payment_Update", prm.ToArray());

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Payment.Insert", ex);
            }
            return false;
        }

        public static bool InsertPayment(Payment objPayment, int isPaid)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@Paymentid", SqlDbType.Int, 4, objPayment._PaymentId));
                    prm.Add(DB.MakeInParam("@PaymentTypeID", SqlDbType.Int, 4, objPayment._PaymentTypeId));
                    if (string.IsNullOrEmpty(objPayment._CheckNumber))
                        prm.Add(DB.MakeInParam("@ChequeNumber", SqlDbType.VarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@ChequeNumber", SqlDbType.VarChar, 50, objPayment._CheckNumber));
                    if (string.IsNullOrEmpty(objPayment._Status))
                        prm.Add(DB.MakeInParam("@Status", SqlDbType.VarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@Status", SqlDbType.VarChar, 50, objPayment._Status));
                    prm.Add(DB.MakeInParam("@PaymentDate", SqlDbType.Date, 8, objPayment._PaymentDate));
                    prm.Add(DB.MakeInParam("@BalanceAmount", SqlDbType.Money, 0, objPayment.BalanceAmount));
                    prm.Add(DB.MakeInParam("@PaymentAmount", SqlDbType.Money, 0, objPayment._PaymentAmount));
                    prm.Add(DB.MakeInParam("@CreatedBy", SqlDbType.Int, 4, objPayment._CreatedBy));
                    prm.Add(DB.MakeInParam("@DateCreated", SqlDbType.Date, 8, objPayment._DateCreated));
                    prm.Add(DB.MakeInParam("@Modifiedby", SqlDbType.Int, 4, DBNull.Value));
                    prm.Add(DB.MakeInParam("@DateModified ", SqlDbType.Date, 8, DBNull.Value));
                    prm.Add(DB.MakeInParam("@Active", SqlDbType.Bit, 1, objPayment.Active));
                    prm.Add(DB.MakeInParam("@InvoiceId", SqlDbType.Int, 4, objPayment._InvoiceId));
                    if (string.IsNullOrEmpty(objPayment._CreditCardNumber))
                        prm.Add(DB.MakeInParam("@CreditCardNumber", SqlDbType.VarChar, 50, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@CreditCardNumber", SqlDbType.VarChar, 50, objPayment._CreditCardNumber));
                    prm.Add(DB.MakeInParam("@IsPaid", SqlDbType.Int, 4, isPaid));
                    int result = DB.RunProc("up_Payment_Insert", prm.ToArray());
                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Payment.Insert", ex);
            }
            return false;


        }

        public static Payment GetPaymentDetailByInvoiceId(int InvoiceID)
        {
            Payment payment = new Payment();
            IDataReader reader = null;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@InvoiceId", SqlDbType.Int, 0, InvoiceID));
                    reader = db.GetDataReader("GET_PaymentDetailByInvoiceId", prams.ToArray());
                    if (reader.Read())
                    {
                        payment.PaymentId = Conversion.ParseDBNullInt(reader["Paymentid"]);
                        payment.PaymentTypeId = Conversion.ParseDBNullInt(reader["PaymentTypeID"]);
                        payment.CheckNumber = Conversion.ParseDBNullString(reader["ChequeNumber"]);
                        payment.PaymentAmount = Conversion.ParseDBNullDecimal(reader["PaymentAmount"]);
                        payment.PaymentDate = Conversion.ParseDBNullDateTime(reader["PaymentDate"]);
                        payment.Status = Conversion.ParseDBNullString(reader["Status"]);
                        payment.BalanceAmount = Conversion.ParseDBNullDecimal(reader["BalanceAmount"]);
                        payment.CreatedBy = Conversion.ParseDBNullInt(reader["CreatedBy"]);
                        payment.DateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
                        payment.ModifiedBy = Conversion.ParseDBNullInt(reader["Modifiedby"]);
                        payment.DateModified = Conversion.ParseDBNullDateTime(reader["DateModified"]);
                        payment.Active = Conversion.ParseDBNullBool(reader["Active"]);
                        payment.CreditCardNumber = Conversion.ParseDBNullString(reader["CreditCardNumber"]);
                    }
                    return payment;
                }
            }

            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Payment.GetPaymentDetailByInvoiceId", ex);
                return null;
            }
        }

    }
}
