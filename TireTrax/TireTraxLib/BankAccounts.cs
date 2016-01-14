using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{

    public class BankAccounts
    {
        #region bankAccounts

        private int _bankAccountId;

        public int BankAccountId
        {
            get { return _bankAccountId; }
            set { _bankAccountId = value; }
        }

        private String _bankName;

        public String BankName
        {
            get { return _bankName; }
            set { _bankName = value; }
        }

        private String _branchName;

        public String BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }

        private String _accountTitle;

        public String AccountTitle
        {
            get { return _accountTitle; }
            set { _accountTitle = value; }
        }

        private String _accountNumber;

        public String AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }



        private String _rountingNumber;

        public String RoutingNumber
        {
            get { return _rountingNumber; }
            set { _rountingNumber = value; }
        }

        private String _IBANNumber;

        public String IBANNumber
        {
            get { return _IBANNumber; }
            set { _IBANNumber = value; }
        }

        private String _swiftCode;

        public String SwiftCode
        {
            get { return _swiftCode; }
            set { _swiftCode = value; }
        }

        private String _streetName;

        public String StreetName
        {
            get { return _streetName; }
            set { _streetName = value; }
        }
        private String _streetNumber;

        public String StreetNumber
        {
            get { return _streetNumber; }
            set { _streetNumber = value; }
        }

        private String _city;

        public String City
        {
            get { return _city; }
            set { _city = value; }
        }
        private String _zipcode;

        public String ZipCode
        {
            get { return _zipcode; }
            set { _zipcode = value; }
        }
        private int _bankaccounttypeid;

        public int Bankaccounttypeid
        {
            get { return _bankaccounttypeid; }
            set { _bankaccounttypeid = value; }
        }

        private String _primaryPhone;

        public String PrimaryPhone
        {
            get { return _primaryPhone; }
            set { _primaryPhone = value; }
        }

        private int _stateId;

        public int StateId
        {
            get { return _stateId; }
            set { _stateId = value; }
        }

        private Boolean _isActive;

        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        private int _createdById;

        public int CreatedByID
        {
            get { return _createdById; }
            set { _createdById = value; }
        }

        private bool _isetf;

        public bool IsETF
        {
            get { return _isetf; }
            set { _isetf = value; }
        }

        private int _userid;

        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }



        #endregion




        public BankAccounts() { }


        public BankAccounts(int acctId)
        {
            loadBankAccountInfo(acctId);


        }


        private void loadBankAccountInfo(int accountId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@accountId", SqlDbType.Int, 0, accountId);
                    reader = db.GetDataReader("up_Account_getById", prams);
                    if (reader.Read())
                        loadBankAccountInfo(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "BankAccountInfo.Load", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }



        private void loadBankAccountInfo(IDataReader reader)
        {
            try
            {
                _bankAccountId = Conversion.ParseDBNullInt(reader["intBankAccountId"]);
                _bankName = Conversion.ParseDBNullString(reader["vchBankName"]);
                _branchName = Conversion.ParseDBNullString(reader["vchBranch"]);
                _accountTitle = Conversion.ParseDBNullString(reader["vchAccountTitle"]);
                _accountNumber = Conversion.ParseDBNullString(reader["vchAccountNumber"]);
                _rountingNumber = Conversion.ParseDBNullString(reader["vchRoutingNumber"]);
                _IBANNumber = Conversion.ParseDBNullString(reader["vchIBANNumber"]);
                _swiftCode = Conversion.ParseDBNullString(reader["vchSwiftCode"]);
                _streetName = Conversion.ParseDBNullString(reader["vchStreetName"]);
                _streetNumber = Conversion.ParseDBNullString(reader["vchStreetNumber"]);
                _city = Conversion.ParseDBNullString(reader["vchCity"]);
                _primaryPhone = Conversion.ParseDBNullString(reader["vchPrimaryPhone"]);
                _bankaccounttypeid = Conversion.ParseDBNullInt(reader["intAccountType"]);
                _zipcode = Conversion.ParseDBNullString(reader["vchZipCode"]);
                _isActive = Conversion.ParseDBNullBool(reader["bitIsActive"]);
                _stateId = Conversion.ParseDBNullInt(reader["intStateId"]);
                _createdById = Conversion.ParseDBNullInt(reader["intCreatedbyId"]);
                _dateCreated = Conversion.ParseDBNullDateTime(reader["dtmDateCreated"]);
                _isetf = Conversion.ParseDBNullBool(reader["IsETF"]);
            }

            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "BankAccountInfo.LoadbankAccountInfo", ex);
            }
        }



        public static int insertAccountInfo(BankAccounts ObjBAcc, int UserId = 0)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intbankaccounttypeid", SqlDbType.Int, 0, ObjBAcc._bankaccounttypeid));
                    prm.Add(DB.MakeInParam("@bankName", SqlDbType.NVarChar, 120, ObjBAcc.BankName));
                    prm.Add(DB.MakeInParam("@branch", SqlDbType.NVarChar, 120, ObjBAcc.BranchName));
                    prm.Add(DB.MakeInParam("@accountTitle", SqlDbType.NVarChar, 120, ObjBAcc.AccountTitle));
                    prm.Add(DB.MakeInParam("@accountNumber", SqlDbType.NVarChar, 120, ObjBAcc.AccountNumber));
                    prm.Add(DB.MakeInParam("@routingNumber", SqlDbType.NVarChar, 120, ObjBAcc.RoutingNumber));
                    prm.Add(DB.MakeInParam("@IBANNumber", SqlDbType.NVarChar, 120, ObjBAcc.IBANNumber));
                    prm.Add(DB.MakeInParam("@swiftCode", SqlDbType.NVarChar, 120, ObjBAcc.SwiftCode));
                    prm.Add(DB.MakeInParam("@streetName", SqlDbType.NVarChar, 150, ObjBAcc.StreetName));
                    prm.Add(DB.MakeInParam("@streetNumber", SqlDbType.NVarChar, 50, ObjBAcc.StreetNumber));
                    prm.Add(DB.MakeInParam("@city", SqlDbType.NVarChar, 75, ObjBAcc.City));
                    prm.Add(DB.MakeInParam("@zipCode", SqlDbType.NVarChar, 10, ObjBAcc.ZipCode));
                    prm.Add(DB.MakeInParam("@primaryPhone", SqlDbType.NVarChar, 20, ObjBAcc.PrimaryPhone));
                    prm.Add(DB.MakeInParam("@stateid", SqlDbType.Int, 4, ObjBAcc.StateId));
                    prm.Add(DB.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));
                    prm.Add(DB.MakeInParam("@IsETF", SqlDbType.Bit, 0, ObjBAcc._isetf));


                    DB.RunProc("up_InsertBankAccounts", prm.ToArray());
                }
            }
            catch (Exception ex)
            {

                new SqlLog().InsertSqlLog(0, "BankAccounts.InsertAccountInfo", ex);
            }

            return 0;
        }


        public static void editBankAccountInfo(int bankaccountid, int accounttypeid, String bankname, String branch, String accounttitle, String accountnumber, String routingnumber, String ibnnumber, String swiftcode, String streetname, String streetnumber, String city, String zipcode, String primaryphone, int stateid, bool isetf, int UserId = 0)
        {
            DataSet ds = null;

            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {

                    prams.Add(db.MakeInParam("@intbankaccountid", SqlDbType.Int, 0, bankaccountid));
                    prams.Add(db.MakeInParam("@intaccounttypeid", SqlDbType.Int, 0, accounttypeid));
                    prams.Add(db.MakeInParam("@vchbankname", SqlDbType.NVarChar, 120, bankname));
                    prams.Add(db.MakeInParam("@vchbranch", SqlDbType.NVarChar, 120, branch));
                    prams.Add(db.MakeInParam("@vchaccounttitle", SqlDbType.NVarChar, 120, accounttitle));
                    prams.Add(db.MakeInParam("@vchaccountnumber", SqlDbType.NVarChar, 120, accountnumber));
                    prams.Add(db.MakeInParam("@vchrountingnumber", SqlDbType.NVarChar, 120, routingnumber));
                    prams.Add(db.MakeInParam("@vchibnnumber", SqlDbType.NVarChar, 120, ibnnumber));
                    prams.Add(db.MakeInParam("@vchswiftcode", SqlDbType.NVarChar, 120, swiftcode));
                    prams.Add(db.MakeInParam("@vchstreetname", SqlDbType.NVarChar, 150, streetname));
                    prams.Add(db.MakeInParam("@vchstreetnumber", SqlDbType.NVarChar, 50, streetnumber));
                    prams.Add(db.MakeInParam("@vchcity", SqlDbType.NVarChar, 75, city));
                    prams.Add(db.MakeInParam("@vchzipcode", SqlDbType.NVarChar, 10, zipcode));
                    prams.Add(db.MakeInParam("@vchprimaryphone", SqlDbType.NVarChar, 20, primaryphone));
                    prams.Add(db.MakeInParam("@intstateid", SqlDbType.Int, 0, stateid));
                    prams.Add(db.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));
                    prams.Add(db.MakeInParam("@isetf", SqlDbType.Bit, 0, isetf));

                    ds = db.GetDataSet("up_bankAcountEdit", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "BankAccounts.EditBankAccountInfo", ex);

            }

        }

        public static DataSet getBankAccountsInfo(int bankStatusTypeId, int UserId = 0)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));

                    prams.Add(db.MakeInParam("@intBankStatus", SqlDbType.Int, 0, bankStatusTypeId));

                    ds = db.GetDataSet("up_getBankAccountInfo", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "BankAccounts.GetBankAccountInfo", ex);

            }
            return ds;
        }

        public static DataSet GetPrimaryBankAccount(int UserId)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));

                    ds = db.GetDataSet("up_getPrimaryBankAccountInfo", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "BankAccounts.GetPrimaryBankAccount", ex);

            }
            return ds;
        }


        public static int GetBankAccountNumber(string accountnumber, int UserId)
        {
            DataSet ds = null;
            int count = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@accountNumber", SqlDbType.NVarChar, 50, accountnumber));
                    prm.Add(DB.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));




                    ds = DB.GetDataSet("up_accountexistornot", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return count + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "BankAccount.GetBankAccountNumber", ex);
            }
            return count;
        }


        public static void deleteBankAccountInfo(int bankaccountid)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {

                    prams.Add(db.MakeInParam("@intBankAccountId", SqlDbType.Int, 0, bankaccountid));

                    ds = db.GetDataSet("up_BankAccountDelete", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "BankAccounts.DeleteBankAccountInfo", ex);

            }

        }




        public static int updateBankAccountInfo(int bankaccountid,int UserId)
        {
            int result = 0;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intBankAccount", SqlDbType.Int, 0, bankaccountid));
                    prams.Add(db.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));

                    result =  db.RunProc("up_setPrimaryBankAccount", prams.ToArray());
                    return result;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "BankAccounts.UpdateBankAcountInfo", ex);

            }
            return result;

        }

        public static void DeleteBankAccountInfo(int bankaccountid)
        {
            DataSet ds = null;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intBankAccountId", SqlDbType.Int, 0, bankaccountid));
                    ds = db.GetDataSet("up_BankAccountDelete", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "BankAccounts.DeleteBankAccountInfo", ex);
            }
        }

        public static void ActivateBankAccountInfo(int bankaccountid)
        {
            DataSet ds = null;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intBankAccountId", SqlDbType.Int, 0, bankaccountid));
                    ds = db.GetDataSet("up_BankAccountActive", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "BankAccounts.ActivateBankAccountInfo", ex);
            }
        }


        public static DataSet GetSateAndCityOfZipcode(string Zipcode)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@Zipcode", SqlDbType.NVarChar, 20, Zipcode));

                    ds = db.GetDataSet("up_GetSateAndCityOfZipcode", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "BankAccounts.GetSateAndCityOfZipcode", ex);

            }
            return ds;
        }
    }

}
