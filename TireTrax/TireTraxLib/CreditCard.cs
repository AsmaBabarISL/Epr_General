using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
     public class CreditCard
    {
        #region CreditCardInfo

        private int intcreditcardid;

        public int Intcreditcardid
        {
            get { return intcreditcardid; }
            set { intcreditcardid = value; }
        }
        private int intcardtypeid;

        public int Intcardtypeid
        {
            get { return intcardtypeid; }
            set { intcardtypeid = value; }
        }


        private String cardnumber;

        public String Cardnumber
        {
            get { return cardnumber; }
            set { cardnumber = value; }
        }


        private String cv2code;

        public String Cv2code
        {
            get { return cv2code; }
            set { cv2code = value; }
        }


        private DateTime dtmexpirydate;

        public DateTime Dtmexpirydate
        {
            get { return dtmexpirydate; }
            set { dtmexpirydate = value; }
        }

        private String vchcreditcardname;

        public String Vchcreditcardname
        {
            get { return vchcreditcardname; }
            set { vchcreditcardname = value; }
        }

        private Boolean bitisActive;

        public Boolean BitisActive
        {
            get { return bitisActive; }
            set { bitisActive = value; }
        }

        private DateTime dtmDateCreated;

        public DateTime DtmDateCreated
        {
            get { return dtmDateCreated; }
            set { dtmDateCreated = value; }
        }

        private int intCreatedbyId;

        public int IntCreatedbyId
        {
            get { return intCreatedbyId; }
            set { intCreatedbyId = value; }
        }
        private Boolean bitIsPrimary;

        public Boolean BitIsPrimary
        {
            get { return bitIsPrimary; }
            set { bitIsPrimary = value; }
        }

        private string _expirationDate;

        public string ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }

        #endregion

        #region CreditCardType

        private String vchCreditCardType;

        public String VchCreditCardType
        {
            get { return vchCreditCardType; }
            set { vchCreditCardType = value; }
        }

        #endregion

        #region CreditCard Functions




        public CreditCard()
        {

        }

        public CreditCard(int CreaditCardId)
        {
            loadCreditCardInfo(CreaditCardId);
        }

        private void loadCreditCardInfo(int CreditCardId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@intCreditCardId", SqlDbType.Int, 0, CreditCardId);
                    reader = db.GetDataReader("up_CreditCardInfoById", prams);
                    if (reader.Read())
                        loadCreditCardInfo(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "CreditCard.CreditCardInfoLoad", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public static int GetCreditCardNumber(string creditcard, int UserId)
        {
            DataSet ds = null;
            int count = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@creditcardnumber", SqlDbType.NVarChar, 50, creditcard));
                    prm.Add(DB.MakeInParam("@userid", SqlDbType.Int, 0, UserId));




                    ds = DB.GetDataSet("up_creditexistornot", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return count + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "CreditCard.GetCreditCardNumber", ex);
            }
            return count;
        }


        private void loadCreditCardInfo(IDataReader reader)
        {
            try
            {
                intcreditcardid = Conversion.ParseDBNullInt(reader["intCreditCardid"]);
                intcardtypeid = Conversion.ParseDBNullInt(reader["intCardTypeId"]);
                cardnumber = Conversion.ParseDBNullString(reader["vchCardNumber"]);
                cv2code = Conversion.ParseDBNullString(reader["vchCV2"]);
                //dtmexpirydate = Conversion.ParseDBNullDateTime(reader["dtmExpiryDate"]);
                vchcreditcardname = Conversion.ParseDBNullString(reader["vchCreditCardName"]);
                bitisActive = Conversion.ParseDBNullBool(reader["bitIsActive"]);
                dtmDateCreated = Conversion.ParseDBNullDateTime(reader["dtmDateCreated"]);
                intCreatedbyId = Conversion.ParseDBNullInt(reader["intCreatedById"]);
                bitIsPrimary = Conversion.ParseDBNullBool(reader["bitIsPrimary"]);
                vchCreditCardType = Conversion.ParseDBNullString(reader["vchCreditCardType"]);
                _expirationDate = Conversion.ParseDBNullString(reader["ExpirationDate"]);

            }

            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "CreditCard.loadCreditCardInfo", ex);
            }
        }




        public static DataSet getCreditCardInfo(int UserId = 0)
        {
            DataSet ds = null;
      

            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {



              
                    prams.Add(db.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));


                    ds = db.GetDataSet("up_getCreditCardInfo", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "CreditCard.getCreditCardInfo", ex);

            }
            return ds;
        }

        public static DataSet GetPrimaryCreditCardInfo(int UserId)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));

                    ds = db.GetDataSet("up_getPrimaryCreditCardInfo", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "CreditCard.GetPrimaryCreditCardInfo", ex);

            }
            return ds;
        }

        public static void addCreditInfo(int CardType, String CardNumber, String CV2Code, String CreditCardName, string ExpirationDate, int UserId = 0)
        {
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intCardTypeId", SqlDbType.Int, 0, CardType));
                    prams.Add(db.MakeInParam("@vchCardNumber", SqlDbType.VarChar, 16, CardNumber));
                    prams.Add(db.MakeInParam("@vchCV2Code", SqlDbType.NVarChar, 4, CV2Code));
                    prams.Add(db.MakeInParam("@vchCreditCardName", SqlDbType.NVarChar, 120, CreditCardName));
                    prams.Add(db.MakeInParam("@expirationDate", SqlDbType.NVarChar, 6, ExpirationDate));
                    prams.Add(db.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));

                    db.RunProc("up_addCreditCardInfo", prams.ToArray());

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "CreditCard.addCreditInfo", ex);

            }
        }



        public static void editCreditCardInfo(int creditcardid, int cardtypeid, String cardnumber, String cv2code, String cardname, string expirationDate, int UserId = 0)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {



                    prams.Add(db.MakeInParam("@intcreditcardid", SqlDbType.Int, 0, creditcardid));
                    prams.Add(db.MakeInParam("@intcredittypeid", SqlDbType.Int, 0, cardtypeid));
                    prams.Add(db.MakeInParam("@vchcardnumber", SqlDbType.NVarChar, 16, cardnumber));
                    prams.Add(db.MakeInParam("@CV2code", SqlDbType.NVarChar, 4, cv2code));
                    prams.Add(db.MakeInParam("@vchcardname", SqlDbType.NVarChar, 120, cardname));
                    prams.Add(db.MakeInParam("@expirationDate", SqlDbType.NVarChar, 6, expirationDate));
                    prams.Add(db.MakeInParam("@intUserId", SqlDbType.Int, 0, UserId));
                    ds = db.GetDataSet("up_CreditCardinfoUpdate", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "CreditCard.editCreditCardInfo", ex);

            }

        }


        public static void updateCreditCardInfo(int creditcardid)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {



                    prams.Add(db.MakeInParam("@intcreditcardid", SqlDbType.Int, 0, creditcardid));





                    ds = db.GetDataSet("up_updateCreditCard", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "CreditCard.updateCreditCardInfo", ex);

            }

        }



        public static void deleteCreditCardInfo(int creditcardid)
        {
            DataSet ds = null;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intCreaditCardId", SqlDbType.Int, 0, creditcardid));

                    ds = db.GetDataSet("up_CreditCardInfoDelete", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "CreditCard.deleteCreditCardInfo", ex);

            }

        }



        #endregion 



    }
}
