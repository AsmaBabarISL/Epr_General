using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
   public  class LookupsManagement
    {
        #region LookUp
        private int _lookupID;

        public int LookupID
        {
            get { return _lookupID; }
            set { _lookupID = value; }
        }
        private String _lookupName;

        public String LookupName
        {
            get { return _lookupName; }
            set { _lookupName = value; }
        }
        private String _description;

        public String Description
        {
            get { return _description; }
            set { _description = value; }
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


        #endregion


        #region LookUpType
        private int _lookupTypeID;

        public int LookupTypeID
        {
            get { return _lookupTypeID; }
            set { _lookupTypeID = value; }
        }
        private String _lookupTypeName;

        public String LookupTypeName
        {
            get { return _lookupTypeName; }
            set { _lookupTypeName = value; }
        }



        #endregion

        #region LookUpSubType

        private int _lookupSubTypeID;

        public int LookupSubTypeID
        {
            get { return _lookupSubTypeID; }
            set { _lookupSubTypeID = value; }
        }

        private String _lookupSubTypeName;

        public String LookupSubTypeName
        {
            get { return _lookupSubTypeName; }
            set { _lookupSubTypeName = value; }
        }

        #endregion

		public enum LookupType
		{
			PhoneType_Business = 3,
			PhoneType_Cell = 4,
			AddressType_Business = 5,
			AddressType_Mailing = 6,
			CertificationsType_StewardshipCertifications = 7,
			CertificationsType_StakeholderCertifications = 8,
			Interfaces_MSExcel = 9,
			Interfaces_QuickBooks = 10,
			Interfaces_PeachTree_Sage = 11,
			Interfaces_MaddenCoInc = 12,
			Interfaces_TCS_OpenRoad = 13,
			Interfaces_FranchiseDMS = 14,
			Interfaces_Other = 15,
			LocationEventTypes_CivicCollectionEvent = 16,
			LocationEventTypes_PrivateCollectionEvent = 17,
			LocationEventTypes_RemediationEvent = 18,
			LocationEventTypes_ProductStorageOnly = 19,
			OrganizationTypes_Stewardship = 20,
			OrganizationTypes_LocalSteward = 21,
			OrganizationTypes_Stakeholder = 22,
			OrganizationTypes_GovernmentAgency = 23,
			OrganizationTypes_LawEnforcementAgency = 24,
			ContactTypes_Business = 25,
			ContactTypes_Billing = 26,
			PrimaryContactTitle_Director = 27,
			PrimaryContactTitle_Owner_Principle = 28,
			PrimaryContactTitle_Manager = 29,
			PrimaryContactTitle_Administrator = 30,
			BillingContactTitle_CFO = 31,
			BillingContactTitle_CPA = 32,
			BillingContactTitle_Accountant = 33,
			BillingContactTitle_BusinessManager = 34,
			BillingContactTitle_OfficeManager = 35,
			BillingContactTitle_AP_ARManager = 36,
			LocationContactTitle_CompanyEmployee = 37,
			LocationContactTitle_CompanyManager = 38,
			LocationContactTitle_ClientEmployee = 39,
			LocationContactTitle_ClientManager = 40,
			LocationContactTitle_PrivatePartyRepresentative = 41,
			OrganizationAddressType_Business = 44,
			OrganizationAddressType_Mailing = 45,
			OrganizationTypes_GlobalSteward = 47,
            Organization_Logo_Image=64,
            Header_Menu=51,
            Sub_Menu=65,
            Logo_Image=64
		}

        public enum Lookup
        {
            Permissions = 121,
            Image = 138    
        }

        public static DataSet GetLookupsData(int LookupID, int LookupTypeID)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LookupID", SqlDbType.Int, 4, LookupID));

                    if (LookupTypeID > 0)
                        prm.Add(DB.MakeInParam("@LookupTypeID", SqlDbType.Int, 4, LookupTypeID));
                    else
                        prm.Add(DB.MakeInParam("@LookupTypeID", SqlDbType.Int, 4, DBNull.Value));

                    ds = DB.GetDataSet("up_Lookup_GetLookupsData", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.GetLookupsData", ex);
            }

            return ds;
        }

        public static DataTable GetLookupSubTypeData(int LookupTypeID, int parentId=0)
        {
            DataTable dt = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intLookUpTypeId", SqlDbType.Int, 4, LookupTypeID));
                    if(parentId > 0)
                        prm.Add(DB.MakeInParam("@intParentId", SqlDbType.Int, 4, parentId));
                    else
                        prm.Add(DB.MakeInParam("@intParentId", SqlDbType.Int, 4, 0));
                    dt = DB.GetDataSet("up_GetLookUpSubType", prm.ToArray()).Tables[0];
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.GetLookupSubTypeData", ex);
            }

            return dt;
        }


        public static void GetLookupSwapedData(int LookUpTypeId, int LookUpTypeId2)
        {
            
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@lookuptypeid", SqlDbType.Int, 4, LookUpTypeId));


                    prm.Add(DB.MakeInParam("@lookuptypeid2", SqlDbType.Int, 4, LookUpTypeId2));



                    DB.RunProc("up_Lookup_SwapSequence", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.GetLookupSwapedData", ex);
            }

           
        }

        public static void GetLookupSubTypeSwapedData(int LookUpSubTypeId, int LookUpSubTypeId2)
        {

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@lookupsubtypeid", SqlDbType.Int, 4, LookUpSubTypeId));


                    prm.Add(DB.MakeInParam("@lookupsubtypeid2", SqlDbType.Int, 4, LookUpSubTypeId2));



                    DB.RunProc("up_LookupSubType_SwapSequence", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.GetLookupSubTypeSwapedData", ex);
            }


        }


        public static int LookupInsert(string LookupName, string Description, DateTime DateCreated, bool IsActive)
        {
            int returnValue = 0;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeOutParam("@LookupID", SqlDbType.Int, 4));
                    prm.Add(DB.MakeInParam("@LookupName", SqlDbType.NChar, 30, LookupName));
                    prm.Add(DB.MakeInParam("@Description", SqlDbType.NChar, 50, Description));
                    prm.Add(DB.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, DateCreated));
                    prm.Add(DB.MakeInParam("@IsActive", SqlDbType.Bit, 1, IsActive));

                    DB.RunProc("up_LookupInsert", prm.ToArray());

                    returnValue = (int)prm.First<SqlParameter>().Value;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.LookupInsert", ex);
            }

            return returnValue;
        }

        public static bool InsertUpdateLookUpSubType(int SubtypeId, string LookupSubName, string Description, DateTime DateCreated, bool IsActive, int LookupTypeId,int parentId = 0)
        {
            int returnValue = 0;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    if (SubtypeId == 0)
                        prm.Add(DB.MakeInParam("@intSubTypeId", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@intSubTypeId", SqlDbType.Int, 4, SubtypeId));
                    prm.Add(DB.MakeInParam("@vchSubTypeName", SqlDbType.Char, 60, LookupSubName));
                    prm.Add(DB.MakeInParam("@vchDescription", SqlDbType.Char, 100, Description));
                    prm.Add(DB.MakeInParam("@dtmDateCreated", SqlDbType.DateTime, 8, DateCreated));
                    prm.Add(DB.MakeInParam("@bitActive", SqlDbType.Bit, 1, IsActive));
                    prm.Add(DB.MakeInParam("@intLookUpTypeId", SqlDbType.Int, 4, LookupTypeId));
                    if(parentId > 0)
                        prm.Add(DB.MakeInParam("@intLookUpTypeParentId", SqlDbType.Int, 4, parentId));
                    else
                        prm.Add(DB.MakeInParam("@intLookUpTypeParentId", SqlDbType.Int, 4, DBNull.Value));

                    DB.RunProc("[up_InsertUpdateLookUpSubType]", prm.ToArray());

                    return true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.InsertUpdateLookUpSubType", ex);
            }

            return false;
        }

        public static int LookupTypeInsert(int LookupID, string LookupTypeName, string Description, DateTime DateCreated, bool IsActive)
        {
            int returnValue = 0;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeOutParam("@LookupTypeID", SqlDbType.Int, 4));
                    prm.Add(DB.MakeInParam("@LookupTypeName", SqlDbType.NChar, 30, LookupTypeName));
                    prm.Add(DB.MakeInParam("@Description", SqlDbType.NChar, 50, Description));
                    prm.Add(DB.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, DateCreated));
                    prm.Add(DB.MakeInParam("@IsActive", SqlDbType.Bit, 1, IsActive));
                    prm.Add(DB.MakeInParam("@LookupID", SqlDbType.Int, 4, LookupID));

                    DB.RunProc("up_LookupTypeInsert", prm.ToArray());

                    returnValue = (int)prm.First<SqlParameter>().Value;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.LookupTypeInsert", ex);
            }

            return returnValue;
        }

        public static int LookupSubTypeInsert(int LookupTypeID, string LookupSubTypeName, string Description, DateTime DateCreated, bool IsActive)
        {
            int returnValue = 0;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeOutParam("@LookupSubTypeID", SqlDbType.Int, 4));
                    prm.Add(DB.MakeInParam("@LookupSubTypeName", SqlDbType.NChar, 30, LookupSubTypeName));
                    prm.Add(DB.MakeInParam("@Description", SqlDbType.NChar, 50, Description));
                    prm.Add(DB.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, DateCreated));
                    prm.Add(DB.MakeInParam("@IsActive", SqlDbType.Bit, 1, IsActive));
                    prm.Add(DB.MakeInParam("@LookupTypeID", SqlDbType.Int, 4, LookupTypeID));

                    DB.RunProc("up_LookupSubTypeInsert", prm.ToArray());

                    returnValue = (int)prm.First<SqlParameter>().Value;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.LookupSubTypeInsert", ex);
            }

            return returnValue;
        }

        public static void LookupUpdate(int LookupID, string LookupName, string Description)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LookupID", SqlDbType.Int, 4, LookupID));
                    prm.Add(DB.MakeInParam("@LookupName", SqlDbType.NChar, 30, LookupName));
                    prm.Add(DB.MakeInParam("@Description", SqlDbType.NChar, 50, Description));

                    DB.RunProc("up_LookupUpdate", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.LookupUpdate", ex);
            }
        }

        public static void LookupTypeUpdate(int LookupTypeID, string LookupTypeName, string Description)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LookupTypeID", SqlDbType.Int, 4, LookupTypeID));
                    prm.Add(DB.MakeInParam("@LookupTypeName", SqlDbType.NChar, 30, LookupTypeName));
                    prm.Add(DB.MakeInParam("@Description", SqlDbType.NChar, 50, Description));

                    DB.RunProc("up_LookupTypeUpdate", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.LookupTypeInsert", ex);
            }
        }

        public static void LookupSubTypeUpdate(int LookupSubTypeID, string LookupSubTypeName, string Description)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeOutParam("@LookupSubTypeID", SqlDbType.Int, 4));
                    prm.Add(DB.MakeInParam("@LookupSubTypeName", SqlDbType.NChar, 30, LookupSubTypeName));
                    prm.Add(DB.MakeInParam("@Description", SqlDbType.NChar, 50, Description));

                    DB.RunProc("up_LookupSubTypeUpdate", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.LookupSubTypeUpdate", ex);
            }
        }

        public static void LookupDelete(int LookupID)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LookupID", SqlDbType.Int, 4, LookupID));

                    DB.RunProc("up_LookupDelete", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.LookupDelete", ex);
            }
        }

        public static void LookupTypeDelete(int LookupTypeID)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LookupTypeID", SqlDbType.Int, 4, LookupTypeID));

                    DB.RunProc("up_LookupTypeDelete", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.LookupTypeDelete", ex);
            }
        }

        public static void LookupSubTypeDelete(int LookupSubTypeID)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LookupSubTypeID", SqlDbType.Int, 4, LookupSubTypeID));

                    DB.RunProc("up_LookupSubTypeDelete", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LookupsManagement.LookupSubTypeDelete", ex);
            }
        }

    }
}
