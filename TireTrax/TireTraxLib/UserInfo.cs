using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace TireTraxLib
{
    public class UserInfo
    {
        #region User
        public static string UserDomain { get { return string.Empty; } }
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _crypid;
        public string CrypID
        {
            get { return _crypid; }
            set { _crypid = value; }
        }
        private int _organizationId;

        public int OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }
        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        private string _pwd;

        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        private string _pwdSalt;

        public string PwdSalt
        {
            get { return _pwdSalt; }
            set { _pwdSalt = value; }
        }
        private Boolean _isActive;

        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private string _tX_UserId;

        public string TX_UserId
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
        private bool _IsApproved;

        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
        private string _UserRolesCommaSeprated;

        public string UserRolesCommaSeprated
        {
            get { return _UserRolesCommaSeprated; }
            set { _UserRolesCommaSeprated = value; }
        }
        private string _UserGroupsCommaSeprated;

        public string UserGroupsCommaSeprated
        {
            get { return _UserGroupsCommaSeprated; }
            set { _UserGroupsCommaSeprated = value; }
        }
        private DateTime _LastLoginDate;

        public DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }

        private int _UserOrganizationRoleId;

        public int UserOrganizationRoleId
        {
            get { return _UserOrganizationRoleId; }
            set { _UserOrganizationRoleId = value; }
        }
        private bool _isOrgAdmin;

        public bool IsOrgAdmin
        {
            get { return _isOrgAdmin; }
            set { _isOrgAdmin = value; }
        }

        private bool _setPassword;

        public bool bitSetPassword
        {
            get { return _setPassword; }
            set { _setPassword = value; }
        }
        private int _UserOrganizationSubTypeId;

        public int UserOrganizationSubTypeId
        {
            get { return _UserOrganizationSubTypeId; }
            set { _UserOrganizationSubTypeId = value; }
        }

        private byte[] _userProfileImage;

        public byte[] UserProfileImage
        {
            get { return _userProfileImage; }
            set { _userProfileImage = value; }
        }

        #endregion


        #region Role
        private int _roleId;

        public int RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }
        private string _roleName;

        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
        private string _subRoleName;

        public string SubRoleName
        {
            get { return _subRoleName; }
            set { _subRoleName = value; }
        }
        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }
        private int _createdByUserId;

        public int CreatedByUserId
        {
            get { return _createdByUserId; }
            set { _createdByUserId = value; }
        }
        private Boolean _isOrganization;

        public Boolean IsOrganization
        {
            get { return _isOrganization; }
            set { _isOrganization = value; }
        }
        private int _contactId;


        public int ContactId
        {
            get { return _contactId; }
            set { _contactId = value; }
        }

        private int _contactTypeId;

        public int ContactTypeId
        {
            get { return _contactTypeId; }
            set { _contactTypeId = value; }
        }
        private string _contactType;

        public string ContactType
        {
            get { return _contactType; }
            set { _contactType = value; }
        }

        private int _PhoneId;

        public int PhoneId
        {
            get { return _PhoneId; }
            set { _PhoneId = value; }
        }
        private int _PhoneTypeId;

        public int PhoneTypeId
        {
            get { return _PhoneTypeId; }
            set { _PhoneTypeId = value; }
        }
        private string _PhoneType;

        public string PhoneType
        {
            get { return _PhoneType; }
            set { _PhoneType = value; }
        }
        private string _Number;

        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        private string _Extension;

        public string Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
        }




        private string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _MiddleName;

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private bool _IsPrimary;

        public bool IsPrimary
        {
            get { return _IsPrimary; }
            set { _IsPrimary = value; }
        }


        private string _TXBarCodeNumber;


        private string _Language;
        private string _LanguageSpecific;

        public string LanguageSpecific
        {
            get { return _LanguageSpecific; }
            set { _LanguageSpecific = value; }
        }


        private int _TimeZone_ZoneId;

        public int TimeZone_ZoneId
        {
            get { return _TimeZone_ZoneId; }
            set { _TimeZone_ZoneId = value; }
        }
        private string _TimeZone_Abbreviation;

        public string TimeZone_Abbreviation
        {
            get { return _TimeZone_Abbreviation; }
            set { _TimeZone_Abbreviation = value; }
        }
        private string _TimeZone_TimeStart;

        public string TimeZone_TimeStart
        {
            get { return _TimeZone_TimeStart; }
            set { _TimeZone_TimeStart = value; }
        }
        private string _TimeZone_GmtOffset;

        public string TimeZone_GmtOffset
        {
            get { return _TimeZone_GmtOffset; }
            set { _TimeZone_GmtOffset = value; }
        }
        private string _TimeZone_Dst;

        public string TimeZone_Dst
        {
            get { return _TimeZone_Dst; }
            set { _TimeZone_Dst = value; }
        }



        private int _OrganizationTypeId;

        public int OrganizationTypeId
        {
            get { return _OrganizationTypeId; }
            set { _OrganizationTypeId = value; }
        }


        private int _OrganizationSubTypeId;

        public int OrganizationSubTypeId
        {
            get { return _OrganizationSubTypeId; }
            set { _OrganizationSubTypeId = value; }
        }
        private string _OrganizationSubType;

        public string OrganizationSubType
        {
            get { return _OrganizationSubType; }
            set { _OrganizationSubType = value; }
        }

        private string _OrganizationType;

        public string OrganizationType
        {
            get { return _OrganizationType; }
            set { _OrganizationType = value; }
        }

        private string _Organization_LegalName;

        public string Organization_LegalName
        {
            get { return _Organization_LegalName; }
            set { _Organization_LegalName = value; }
        }
        private string _Organization_DBAName;

        public string Organization_DBAName
        {
            get { return _Organization_DBAName; }
            set { _Organization_DBAName = value; }
        }
        private string _Organization_FranchiseName;

        public string Organization_FranchiseName
        {
            get { return _Organization_FranchiseName; }
            set { _Organization_FranchiseName = value; }
        }
        private int _Organization_ParentId;

        public int Organization_ParentId
        {
            get { return _Organization_ParentId; }
            set { _Organization_ParentId = value; }
        }
        private string _Organization_Website;

        public string Organization_Website
        {
            get { return _Organization_Website; }
            set { _Organization_Website = value; }
        }
        private int _Organization_TX_ID;

        public int Organization_TX_ID
        {
            get { return _Organization_TX_ID; }
            set { _Organization_TX_ID = value; }
        }
        private string _Organization_BusinessType;

        public string Organization_BusinessType
        {
            get { return _Organization_BusinessType; }
            set { _Organization_BusinessType = value; }
        }

        #endregion


        #region User_Role

        // primary key User_ID and Foreign Key Role Id Both are allready mentioned


        #endregion

        #region User_Group
        private int _groupid;

        public int GroupID
        {
            get { return _groupid; }
            set { _groupid = value; }
        }

        #endregion

        #region Product Category

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        #endregion


        public enum UserRole
        {
            NotSpecified = 0,
            Admin = 1,
            User = 2,
            Stewardship = 3,
            Stakeholder = 4
        }

        public UserInfo() { }

        public UserInfo(int UserId)
        {
            Load(UserId);
        }
        public UserInfo(string LogonName)
        {
            Load(LogonName);
        }


        public UserInfo(string crypid, bool isAppUser)
        {
            LoadUser(crypid);
        }
        private void Load(string LogonName)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@Login", SqlDbType.VarChar, 0, LogonName);
                    reader = db.GetDataReader("up_User_getByName", prams);
                    if (reader.Read())
                        Load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.Load1", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        
        public static int GetUserId(string loginName, double stateid)
        {
             IDataReader reader = null;
             int userid = 0;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@Login", SqlDbType.VarChar, 0, loginName);
                    prams[0] = db.MakeInParam("@stateId", SqlDbType.BigInt, 0, stateid);
                    reader = db.GetDataReader("up_User_getByNameAndSateId", prams);
                    if (reader.Read())
                          userid = Convert.ToInt32(reader["UserId"]);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.GetUserId", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return userid;
        }
        private void LoadUser(string crypId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    SqlParameter[] prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@vchCrypID", SqlDbType.VarChar, 50, crypId);
                    reader = db.GetDataReader("Up_getUserbyCrypID", prams);
                    if (reader.Read())
                    {
                        int memberId = Convert.ToInt32(reader["UserId"]);
                        Load(memberId);
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.LoadUser", ex);

            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        private void Load(int UserId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@UserId", SqlDbType.Int, 0, UserId);
                    reader = db.GetDataReader("up_User_getById", prams);
                    if (reader.Read())
                        Load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.Load2", e);
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
                _userId = Conversion.ParseDBNullInt(reader["UserId"]);
                _login = Conversion.ParseDBNullString(reader["Login"]);
                _pwd = Conversion.ParseDBNullString(reader["Pwd"]);
                _pwdSalt = Conversion.ParseDBNullString(reader["PwdSalt"]);
                _isActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                _IsApproved = Conversion.ParseDBNullBool(reader["IsApproved"]);
                _contactId = Conversion.ParseDBNullInt(reader["ContactId"]);
                _contactTypeId = Conversion.ParseDBNullInt(reader["ContactTypeId"]);
                _contactType = Conversion.ParseDBNullString(reader["ContactType"]);
                _PhoneId = Conversion.ParseDBNullInt(reader["PhoneId"]);
                _PhoneTypeId = Conversion.ParseDBNullInt(reader["PhoneTypeId"]);
                _PhoneType = Conversion.ParseDBNullString(reader["PhoneType"]);
                _Number = Conversion.ParseDBNullString(reader["Number"]);
                _Extension = Conversion.ParseDBNullString(reader["Extension"]);
                _fullName = Conversion.ParseDBNullString(reader["FullName"]);
                _FirstName = Conversion.ParseDBNullString(reader["FirstName"]);
                _MiddleName = Conversion.ParseDBNullString(reader["MiddleName"]);
                _LastName = Conversion.ParseDBNullString(reader["LastName"]);
                _Email = Conversion.ParseDBNullString(reader["Email"]);
                _IsPrimary = Conversion.ParseDBNullBool(reader["IsPrimary"]);
                _tX_UserId = Conversion.ParseDBNullString(reader["TX-UserId"]);
                _TXBarCodeNumber = Conversion.ParseDBNullString(reader["TXBarCodeNumber"]);
                _languageId = Conversion.ParseDBNullInt(reader["LanguageId"]);
                _Language = Conversion.ParseDBNullString(reader["Language"]);
                _LanguageSpecific = Conversion.ParseDBNullString(reader["LanguageSpecific"]);
                _timeZoneID = Conversion.ParseDBNullInt(reader["TimeZoneID"]);
                _TimeZone_ZoneId = Conversion.ParseDBNullInt(reader["TimeZone_ZoneId"]);
                _TimeZone_Abbreviation = Conversion.ParseDBNullString(reader["TimeZone_Abbreviation"]);
                _TimeZone_TimeStart = Conversion.ParseDBNullString(reader["TimeZone_TimeStart"]);
                _TimeZone_GmtOffset = Conversion.ParseDBNullString(reader["TimeZone_GmtOffset"]);
                _TimeZone_Dst = Conversion.ParseDBNullString(reader["TimeZone_Dst"]);
                _OrganizationTypeId = Conversion.ParseDBNullInt(reader["OrganizationTypeId"]);
                _OrganizationType = Conversion.ParseDBNullString(reader["OrganizationType"]);
                _organizationId = Conversion.ParseDBNullInt(reader["OrganizationId"]);
                _Organization_LegalName = Conversion.ParseDBNullString(reader["Organization_LegalName"]);
                _Organization_DBAName = Conversion.ParseDBNullString(reader["Organization_DBAName"]);
                _Organization_FranchiseName = Conversion.ParseDBNullString(reader["Organization_FranchiseName"]);
                _Organization_ParentId = Conversion.ParseDBNullInt(reader["Organization_ParentId"]);
                _Organization_Website = Conversion.ParseDBNullString(reader["Organization_Website"]);
                _Organization_TX_ID = Conversion.ParseDBNullInt(reader["Organization_TX_ID"]);
                _Organization_BusinessType = Conversion.ParseDBNullString(reader["Organization_BusinessType"]);
                _roleId = Conversion.ParseDBNullInt(reader["RoleId"]);
                _roleName = Conversion.ParseDBNullString(reader["RoleName"]);
                _subRoleName = Conversion.ParseDBNullString(reader["SubRoleName"]);
                _isOrganization = Conversion.ParseDBNullBool(reader["IsOrganization"]);

                _OrganizationSubTypeId = Conversion.ParseDBNullInt(reader["OrganizationSubTypeID"]);
                _OrganizationSubType = Conversion.ParseDBNullString(reader["OrganizationSubType"]);
                
               _setPassword = Conversion.ParseDBNullBool(reader["bitIsSetPassword"]);
               _isOrgAdmin = Conversion.ParseDBNullBool(reader["bitIsOrgAdmin"]);
               _userProfileImage = reader["profileimage"] != null ? (byte[])reader["profileimage"] : null;
                if (reader.NextResult())
                {
                    _UserRolesCommaSeprated = "";
                    while (reader.Read())
                    {
                        _UserRolesCommaSeprated = _UserRolesCommaSeprated + Convert.ToString(reader["RoleId"]) + ",";
                    }
                    _UserRolesCommaSeprated = _UserRolesCommaSeprated.TrimEnd(',');
                }

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.Load3", ex);
            }
        }

        public static int InsertMemberonly(UserInfo User)
        {
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    //prams.Add(db.MakeInParam("@login", SqlDbType.VarChar, 0, member.LoginId));
                    //prams.Add(db.MakeInParam("@Password", SqlDbType.VarChar, 0, member.Password));
                    //prams.Add(db.MakeInParam("@DateCreated", SqlDbType.DateTime, 0, member.DateCreated));
                    //prams.Add(db.MakeInParam("@BitActive", SqlDbType.Bit, 0, member.IsActive));
                    //prams.Add(db.MakeInParam("@intCreatedBy", SqlDbType.Int, 0, member.UserId));
                    //prams.Add(db.MakeInParam("@intPositionId", SqlDbType.Int, 0, member.PositionId));
                    //prams.Add(db.MakeInParam("@intResponsibilityId", SqlDbType.Int, 0, member.ResponsiblityId));
                    //prams.Add(db.MakeInParam("@intIndustryId", SqlDbType.Int, 0, member.IndustryId));
                    //prams.Add(db.MakeInParam("@BitIsConfirmterm", SqlDbType.Bit, 0, member.IsConfirmTerms));
                    //prams.Add(db.MakeInParam("@bitIsProductUpdate", SqlDbType.Bit, 0, member.IsProductUpdates));
                    //prams.Add(db.MakeInParam("@bitIsfeaturebyEmail", SqlDbType.Bit, 0, member.IsFeaturesByEmail));
                    //prams.Add(db.MakeInParam("@bitIsfeaturebyPost", SqlDbType.Bit, 0, member.IsFeatureByPost));
                    //prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    //int exec = db.RunProc("UP_member_InsertUpdate", prams.ToArray());
                    return Conversion.ParseDBNullInt(prams.Last<SqlParameter>().Value);
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "MemberInfo.InsertMember", ex);
            }
            return -1;
        }
        public static UserInfo AuthenticateMember(string login, string password,int stateid)
        {
            UserInfo User = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[3];
                 
                    prams[0] = db.MakeInParam("@login", SqlDbType.NVarChar, 150, login);
                    prams[1] = db.MakeInParam("@pasword", SqlDbType.NVarChar, 180, password);
                       prams[2] = db.MakeInParam("@stateid", SqlDbType.Int, 0, stateid);

                    using (IDataReader reader = db.GetDataReader("up_authenticateMember", prams))
                    {
                        if (reader.Read())
                        {
                            User = new UserInfo();
                            User.UserId = reader["UserId"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["UserId"]);
                            User.FirstName = Conversion.ParseDBNullString(reader["FirstName"]);
                            User.CrypID = reader["IsOrganization"] == DBNull.Value ? "" : Conversion.ParseDBNullString(reader["vchCrypId"]);
                            User.MiddleName = Conversion.ParseDBNullString(reader["MiddleName"]);
                            User.LastName = Conversion.ParseDBNullString(reader["LastName"]);
                            User.FullName = User.FirstName + User.MiddleName + User.LastName;
                            User.IsOrganization = reader["IsOrganization"] == DBNull.Value ? false : Conversion.ParseDBNullBool(reader["IsOrganization"]);
                            User.Login = Conversion.ParseDBNullString(reader["Login"]);
                            User.TX_UserId = Conversion.ParseDBNullString(reader["TX-UserId"]);
                            User.LastLoginDate = reader["LastLoginDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["LastLoginDate"], System.Globalization.CultureInfo.InvariantCulture);
                            User.OrganizationId = reader["OrganizationId"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["OrganizationId"]);
                            User.UserOrganizationRoleId = reader["OrganizationRoleID"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["OrganizationRoleID"]);
                            //User.GroupID = reader["intGroupID"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["intGroupID"]);
                            User.UserRolesCommaSeprated = reader["UserRolesCommaSeprated"] == DBNull.Value ? "" : Conversion.ParseDBNullString(reader["UserRolesCommaSeprated"]);
                            User.UserGroupsCommaSeprated = reader["UserGroupsCommaSeprated"] == DBNull.Value ? "" : Conversion.ParseDBNullString(reader["UserGroupsCommaSeprated"]);
                            User.RoleId = reader["RoleId"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["RoleId"]);
                            User.UserOrganizationSubTypeId = reader["OrganizationSubTypeID"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["OrganizationSubTypeID"]);
                        }

                        //if (User != null && reader.NextResult())
                        //{
                        //    User.UserRolesCommaSeprated = "";
                        //    while (reader.Read())
                        //    {
                        //        User.UserRolesCommaSeprated = User.UserRolesCommaSeprated + Convert.ToString(reader["RoleId"]) + ",";
                        //    }
                        //    User.UserRolesCommaSeprated = User.UserRolesCommaSeprated.TrimEnd(',');
                        //}

                        //if (User != null && reader.NextResult() && reader.Read())
                        //{
                        //    User.OrganizationId = reader["OrganizationId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrganizationId"]);
                        //    User.UserOrganizationRoleId = reader["RoleId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RoleId"]);
                        //}
                        //if (User != null && reader.NextResult() && reader.Read())
                        //{
                        //    User.GroupID = reader["intGroupID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["intGroupID"]);
                        //}

                        if (reader != null)
                            reader.Close();
                    }

                    if (User != null)
                    {
                        UpdateLastLoginDate(User.UserId);
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.AuthenticateMember", ex);
                return null;
            }
            return User;
        }

        public static UserInfo AuthenticateAdminMember(string login, string password)
        {
            UserInfo User = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[2];
                    prams[0] = db.MakeInParam("@login", SqlDbType.NVarChar, 150, login);
                    prams[1] = db.MakeInParam("@pasword", SqlDbType.NVarChar, 180, password);

                    using (IDataReader reader = db.GetDataReader("[up_authenticateAdminMember]", prams))
                    {
                        if (reader.Read())
                        {
                            User = new UserInfo();
                            User.UserId = reader["UserId"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["UserId"]);
                            User.FirstName = Conversion.ParseDBNullString(reader["FirstName"]);
                            User.CrypID = reader["IsOrganization"] == DBNull.Value ? "" : Conversion.ParseDBNullString(reader["vchCrypId"]);
                            User.MiddleName = Conversion.ParseDBNullString(reader["MiddleName"]);
                            User.LastName = Conversion.ParseDBNullString(reader["LastName"]);
                            User.FullName = User.FirstName + User.MiddleName + User.LastName;
                            User.IsOrganization = reader["IsOrganization"] == DBNull.Value ? false : Conversion.ParseDBNullBool(reader["IsOrganization"]);
                            User.Login = Conversion.ParseDBNullString(reader["Login"]);
                            User.TX_UserId = Conversion.ParseDBNullString(reader["TX-UserId"]);
                            User.LastLoginDate = reader["LastLoginDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["LastLoginDate"], System.Globalization.CultureInfo.InvariantCulture);
                            User.OrganizationId = reader["OrganizationId"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["OrganizationId"]);
                            User.UserOrganizationRoleId = reader["OrganizationRoleID"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["OrganizationRoleID"]);
                            User.GroupID = reader["intGroupID"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["intGroupID"]);
                            User.UserRolesCommaSeprated = reader["UserRolesCommaSeprated"] == DBNull.Value ? "" : Conversion.ParseDBNullString(reader["UserRolesCommaSeprated"]);
                            User.UserGroupsCommaSeprated = reader["UserGroupsCommaSeprated"] == DBNull.Value ? "" : Conversion.ParseDBNullString(reader["UserGroupsCommaSeprated"]);
                            User.RoleId = reader["RoleId"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["RoleId"]);
                            User.UserOrganizationSubTypeId = reader["OrganizationSubTypeID"] == DBNull.Value ? 0 : Conversion.ParseDBNullInt(reader["OrganizationSubTypeID"]);
                            
                            //User.UserId = reader["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserId"]);
                            //User.FirstName = reader["FirstName"].ToString();
                            //User.CrypID = reader["IsOrganization"] == DBNull.Value ? "" : Convert.ToString(reader["vchCrypId"]);
                            //User.MiddleName = reader["MiddleName"].ToString();
                            //User.LastName = reader["LastName"].ToString();
                            //User.FullName = User.FirstName + User.MiddleName + User.LastName;
                            //User.IsOrganization = reader["IsOrganization"] == DBNull.Value ? false : Conversion.ParseDBNullBool(reader["IsOrganization"]);
                            //User.Login = reader["Login"].ToString();
                            //User.TX_UserId = reader["TX-UserId"].ToString();
                            //User.LastLoginDate = reader["LastLoginDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["LastLoginDate"], System.Globalization.CultureInfo.InvariantCulture);
                            //User.OrganizationId = 0;
                            //User.UserOrganizationRoleId = 0;
                        }

                        //if (User != null && reader.NextResult())
                        //{
                        //    User.UserRolesCommaSeprated = "";
                        //    while (reader.Read())
                        //    {
                        //        User.UserRolesCommaSeprated = User.UserRolesCommaSeprated + Convert.ToString(reader["RoleId"]) + ",";
                        //    }
                        //    User.UserRolesCommaSeprated = User.UserRolesCommaSeprated.TrimEnd(',');
                        //}

                        //if (User != null && reader.NextResult() && reader.Read())
                        //{
                        //    User.OrganizationId = reader["OrganizationId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrganizationId"]);
                        //    User.UserOrganizationRoleId = reader["RoleId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RoleId"]);
                        //}

                        if (reader != null)
                            reader.Close();
                    }

                    if (User != null)
                    {
                        UpdateLastLoginDate(User.UserId);
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.AuthenticateAdminMember", ex);
                return null;
            }
            return User;
        }

        private static void UpdateLastLoginDate(int UserId)
        {
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[2];
                    prams[0] = db.MakeInParam("@UserId", SqlDbType.Int, 4, UserId);
                    prams[1] = db.MakeInParam("@Date", SqlDbType.DateTime, 8, DateTime.Now);

                    db.RunProc("up_User_UpdateLastLoginDate", prams);
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.UpdateLastLoginDate", ex);
            }
        }
        public static void GetAuthenticationTicket(UserInfo info, bool rememberMe,string CatId="",string CatName="")
        {
            System.Web.Security.FormsAuthentication.Initialize();
            StringBuilder builder = new StringBuilder(200);
            builder.Append(Encryption.Encrypt(Conversion.ParseString( info.UserId)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(info.FullName)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(info.RoleId)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(info.Login)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(info.UserRolesCommaSeprated)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(info.LastLoginDate)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(info.OrganizationId)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(info.UserOrganizationRoleId)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(info.UserGroupsCommaSeprated)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(info.UserOrganizationSubTypeId)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(CatName)));
            builder.Append("_!_");
            builder.Append(Encryption.Encrypt(Conversion.ParseString(CatId)));

            System.Web.Security.FormsAuthenticationTicket ticket = new System.Web.Security.FormsAuthenticationTicket(1, builder.ToString(), DateTime.Now, DateTime.Now.AddDays(5.0), rememberMe, "");
            string str = System.Web.Security.FormsAuthentication.Encrypt(ticket);
            System.Web.HttpCookie cookie = new System.Web.HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, str);
            if (rememberMe)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Path = System.Web.Security.FormsAuthentication.FormsCookiePath;
            cookie.Domain = SiteCookie.DomainCookie;
            if (System.Web.HttpContext.Current.Request.Url.Host.ToLower().Equals(UserDomain))
            {
                cookie.Domain = UserDomain;
            }
            else if (System.Web.HttpContext.Current.Request.Url.Host.ToLower().Equals("stage." + UserDomain))
            {
                cookie.Domain = "stage." + UserDomain;
            }
            else
            {
                cookie.Domain = SiteCookie.DomainCookie;
            }
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            SiteCookie.Update("TireTraxRndm", Guid.NewGuid().ToString(), 0,5);
        }

        public static UserInfo GetCurrentUserInfo()
        {
            UserInfo info = new UserInfo();
            if ((System.Web.HttpContext.Current.User.Identity.Name != null) && (System.Web.HttpContext.Current.User.Identity.Name.Length > 0))
            {
                info.GetUserFromCookie(System.Web.HttpContext.Current.User.Identity.Name);
            }
            return info;
        }
        private void GetUserFromCookie(string cookieValue)
        {
            try
            {
                string[] strArray = Regex.Split(cookieValue, "_!_");
                if (strArray.Length > 0)
                {
                    this._userId = Conversion.ParseInt(Encryption.Decrypt(strArray[0]));
                }
                if (strArray.Length > 1)
                {
                    this._fullName = Encryption.Decrypt(strArray[1]);
                }
                if (strArray.Length > 2)
                {
                    this._roleId = Conversion.ParseInt(Encryption.Decrypt(strArray[2]));
                }
                if (strArray.Length > 3)
                {
                    this._login = Encryption.Decrypt(strArray[3]);
                }
                if (strArray.Length > 4)
                {
                    this._UserRolesCommaSeprated = Encryption.Decrypt(strArray[4]);
                }
                if (strArray.Length > 5)
                {
                    try
                    {
                        string dd = Encryption.Decrypt(strArray[5]);
                        //if ()
                        //this._LastLoginDate = DateTime.ParseExact(Encryption.Decrypt(strArray[5]), "yyyy-MM-dd HH:mm:ss:fff", System.Globalization.CultureInfo.InvariantCulture);
                        //DateTime.TryParseExact(Encryption.Decrypt(strArray[5]), "yyyy-MM-dd HH:mm:ss:fff", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out this._LastLoginDate);
                        if(dd.Contains("p.m.") || dd.Contains("a.m."))
                            this._LastLoginDate = DateTime.Now;
                        else
                            this._LastLoginDate = Convert.ToDateTime(Encryption.Decrypt(strArray[5]), System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch(Exception e)
                    {
                        this._LastLoginDate = DateTime.Now;
                    }
                }
                if (strArray.Length > 6)
                {
                    this.OrganizationId = Conversion.ParseInt(Encryption.Decrypt(strArray[6]));
                }
                if (strArray.Length > 7)
                {
                    this.UserOrganizationRoleId = Conversion.ParseInt(Encryption.Decrypt(strArray[7]));
                }
                if (strArray.Length > 8)
                {
                    this._UserGroupsCommaSeprated =Conversion.ParseString( Encryption.Decrypt(strArray[8]));
                }
                if (strArray.Length >9)
                {
                    this._UserOrganizationSubTypeId = Conversion.ParseInt(Encryption.Decrypt(strArray[9]));
                }
                if (strArray.Length > 10)
                {
                    this.CategoryName =Conversion.ParseString(Encryption.Decrypt(strArray[10]));
                }
                if (strArray.Length > 11)
                {
                    this.CategoryId = Conversion.ParseInt(Encryption.Decrypt(strArray[11]));
                }
            }
            catch (Exception exception)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.GetUserFromCookie", exception);
            }
        }

        public static UserInfo GetMemberInfo(string login)
        {
            UserInfo member = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@vchLogin", SqlDbType.VarChar, 0, login);

                    using (IDataReader reader = db.GetDataReader("up_Member_getByName", prams))
                    {
                        if (reader.Read())
                        {

                            member = new UserInfo();
                            //member.Gender = Conversion.ParseDBNullInt(reader["bitGender"]);
                            //member.Pwd = Conversion.ParseDBNullstring(reader["vchPassword"]);
                            //member.FullName = Conversion.ParseDBNullstring(reader["FullName"]);
                            //member.IsActive = Conversion.ParseDBNullBool(reader["BitActive"]);
                            //member.Email = Conversion.ParseDBNullstring(reader["vchEmail"]);
                            //member.City = Conversion.ParseDBNullstring(reader["vchCity"]);
                            //member.StateName = Conversion.ParseDBNullstring(reader["StateName"]);
                            //member.StateId = Conversion.ParseDBNullInt(reader["intStateId"]);
                            //member.ZipCode = Conversion.ParseDBNullstring(reader["vchZipCode"]);
                            //member.CountryID = Conversion.ParseDBNullInt(reader["CountryID"]);
                            //member.CountyId = Conversion.ParseDBNullInt(reader["CountyID"]);
                            //member.AddressTypeId = Conversion.ParseDBNullInt(reader["AddressTypeID"]);
                            //member.DateOfBirth = Conversion.ParseDBNullDateTime(reader["dtmDateofBirth"]);
                            //member.HomeAddress = Conversion.ParseDBNullstring(reader["vchHomeAddress"]);
                            //member.fltLat = Conversion.ParseDBNullDouble(reader["fltLat"]);
                            //member.fltLng = Conversion.ParseDBNullDouble(reader["fltLng"]);
                            //member.Phone = Conversion.ParseDBNullstring(reader["vchPhone"]);
                            //member.MobilePhone = Conversion.ParseDBNullstring(reader["vchMobilePhone"]);
                            //member.BusinessAddress = Conversion.ParseDBNullstring(reader["vchBusinessAddress"]);
                            //member.IsActive = Conversion.ParseDBNullBool(reader["bitactive"]);
                            //member.DateCreated = Conversion.ParseDBNullDateTime(reader["dtmDateCreated"]);
                            //member.DateLastModified = Conversion.ParseDBNullDateTime(reader["dtmDateLastModified"]);
                            //member.LastModifiedBy = Conversion.ParseDBNullInt(reader["intLastModifiedBy"]);
                            //member.IsAdmin = Conversion.ParseDBNullBool(reader["isadmin"]);
                        }
                        if (reader != null)
                            reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "MemberInfo.GetMemberInfo", ex);
                return null;
            }
            return member;
        }
        public static int AdminLogin(String username, String password)
        {
            int errorcode = 0;

            List<SqlParameter> param = new List<SqlParameter>();

            try
            {

                using (DbManager db = DbManager.GetDbManager())
                {
                    param.Add(db.MakeInParam("@login", SqlDbType.VarChar, 75, username));
                    param.Add(db.MakeInParam("@pwd", SqlDbType.VarChar, 90, password));
                    errorcode = db.RunProc("up_Users_login", param.ToArray());

                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.AdminLogin", exp);

            }

            return errorcode;
        }

        public List<string> RemoveKeyandGetCredentials(string sGUID)
        {
            DataSet dataSet = null;
            List<string> list = new List<string>();
            try
            {
                using (DbManager manager = DbManager.GetDbManager())
                {
                    SqlParameter[] prams = new SqlParameter[] { manager.MakeInParam("@pKey", SqlDbType.VarChar, 50, sGUID) };
                    dataSet = manager.GetDataSet("UP_TempLogin_Delete", prams);
                    if (((dataSet != null) && (dataSet.Tables.Count > 0)) && (dataSet.Tables[0].Rows.Count > 0))
                    {
                        list.Add(Conversion.ParseDBNullString(dataSet.Tables[0].Rows[0]["UserName"]));
                        list.Add(Conversion.ParseDBNullString(dataSet.Tables[0].Rows[0]["Password"]));
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                new SqlLog().InsertSqlLog(0, "membermanagement.RemoveKeyandGetCredentials", exception);
            }
            return null;
        }

        public static DataSet getAdminUsers(int pageId, int pageSize, int organizationId, out int iTotalrows, string FirstName, string LastName, string LoginName, DateTime CreatedFromDate, DateTime CreatedToDate, int LanguageId, int LoginId, bool? status)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 4, pageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 4, pageSize));
                    prams.Add(db.MakeInParam("@organizationId", SqlDbType.Int, 4, organizationId));

                    if (FirstName == "")
                        prams.Add(db.MakeInParam("@FirstName", SqlDbType.NVarChar, 30, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@FirstName", SqlDbType.NVarChar, 30, FirstName));

                    if (LastName == "")
                        prams.Add(db.MakeInParam("@LastName", SqlDbType.NVarChar, 30, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@LastName", SqlDbType.NVarChar, 30, LastName));

                    if (LoginName == "")
                        prams.Add(db.MakeInParam("@LoginName", SqlDbType.NVarChar, 75, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@LoginName", SqlDbType.NVarChar, 75, LoginName));

                    if (CreatedFromDate.Date == DateTime.MinValue.Date)
                        prams.Add(db.MakeInParam("@CreatedFromDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CreatedFromDate", SqlDbType.DateTime, 8, CreatedFromDate));

                    if (CreatedToDate.Date == DateTime.MinValue.Date)
                        prams.Add(db.MakeInParam("@CreatedToDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CreatedToDate", SqlDbType.DateTime, 8, CreatedToDate));
                    if (status == null)
                        prams.Add(db.MakeInParam("@Status", SqlDbType.Bit, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@Status", SqlDbType.Bit, 0, status));



                    prams.Add(db.MakeInParam("@UserId", SqlDbType.Int, 0, LoginId));

                    prams.Add(db.MakeInParam("@LanguageId", SqlDbType.Int, 4, LanguageId));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));

                    ds = db.GetDataSet("up_adminUsers", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.getAdminUsers", exp);
            }
            return ds;


        }
        public static DataSet getDefaultUsers(int organizationId)
        {
            DataSet ds = null;
           // iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                   
                    prams.Add(db.MakeInParam("@organizationId", SqlDbType.Int, 4, organizationId));

                    ds = db.GetDataSet("up_GetDefaultAdminUser", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                       // iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.getUsers", exp);
            }
            return ds;


        }
        public static DataSet getUsers(int pageId, int pageSize, int organizationId, out int iTotalrows, string FirstName, string LastName, string LoginName, DateTime CreatedFromDate, DateTime CreatedToDate, int LanguageId,bool isActive)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 4, pageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 4, pageSize));
                    prams.Add(db.MakeInParam("@organizationId", SqlDbType.Int, 4, organizationId));
                    prams.Add(db.MakeInParam("@IsActive", SqlDbType.Bit, 0, isActive));


                    if (FirstName == "")
                        prams.Add(db.MakeInParam("@FirstName", SqlDbType.NVarChar, 30, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@FirstName", SqlDbType.NVarChar, 30, FirstName));

                    if (LastName == "")
                        prams.Add(db.MakeInParam("@LastName", SqlDbType.NVarChar, 30, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@LastName", SqlDbType.NVarChar, 30, LastName));

                    if (LoginName == "")
                        prams.Add(db.MakeInParam("@LoginName", SqlDbType.NVarChar, 75, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@LoginName", SqlDbType.NVarChar, 75, LoginName));

                    if (CreatedFromDate.Date == DateTime.MinValue.Date)
                        prams.Add(db.MakeInParam("@CreatedFromDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CreatedFromDate", SqlDbType.DateTime, 8, CreatedFromDate));

                    if (CreatedToDate.Date == DateTime.MinValue.Date)
                        prams.Add(db.MakeInParam("@CreatedToDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CreatedToDate", SqlDbType.DateTime, 8, CreatedToDate));

                    prams.Add(db.MakeInParam("@LanguageId", SqlDbType.Int, 4, LanguageId));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));

                    ds = db.GetDataSet("up_GetAllUsers", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.getUsers", exp);
            }
            return ds;


        }

        public static DataSet getAllAdminUsers(int pageId, int pageSize, out int iTotalrows, string FirstName, string LastName, string LoginName, DateTime CreatedFromDate, DateTime CreatedToDate, int LanguageId, bool isActive)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 4, pageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 4, pageSize));
                    prams.Add(db.MakeInParam("@active", SqlDbType.Bit, 0, isActive));

                    if (FirstName == "")
                        prams.Add(db.MakeInParam("@FirstName", SqlDbType.NVarChar, 30, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@FirstName", SqlDbType.NVarChar, 30, FirstName));

                    if (LastName == "")
                        prams.Add(db.MakeInParam("@LastName", SqlDbType.NVarChar, 30, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@LastName", SqlDbType.NVarChar, 30, LastName));

                    if (LoginName == "")
                        prams.Add(db.MakeInParam("@LoginName", SqlDbType.NVarChar, 75, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@LoginName", SqlDbType.NVarChar, 75, LoginName));

                    if (CreatedFromDate.Date == DateTime.MinValue.Date)
                        prams.Add(db.MakeInParam("@CreatedFromDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CreatedFromDate", SqlDbType.DateTime, 8, CreatedFromDate));

                    if (CreatedToDate.Date == DateTime.MinValue.Date)
                        prams.Add(db.MakeInParam("@CreatedToDate", SqlDbType.DateTime, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CreatedToDate", SqlDbType.DateTime, 8, CreatedToDate));

                    prams.Add(db.MakeInParam("@LanguageId", SqlDbType.Int, 4, LanguageId));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));

                    ds = db.GetDataSet("up_getAllAdminUserInfo", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.getAllAdminUsers", exp);
            }
            return ds;


        }

        public static void ApproveAdminUser(int userId)
        {
            DataSet ds = null;

            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@userId", SqlDbType.Int, 0, userId));


                    ds = db.GetDataSet("up_User_ApproveAdminUser", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.ApproveAdminUser", exp);
            }

        }
        public static void DeleteAdminUser(int userId)
        {
            DataSet ds = null;

            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@userId", SqlDbType.Int, 0, userId));


                    ds = db.GetDataSet("up_User_deleteAdminUser", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.DeleteAdminUser", exp);
            }

        }
        public static void DisApproveAdminUser(int userId)
        {
            DataSet ds = null;

            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@userId", SqlDbType.Int, 0, userId));


                    ds = db.GetDataSet("up_User_DisApprove", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.DisApproveAdminUser", exp);
            }

        }


        public static bool CheckLoginNameAvailable(string LoginName)
        {
            bool flag = false;
            try
            {
                List<SqlParameter> List = new List<SqlParameter>();
                using (DbManager DB = DbManager.GetDbManager())
                {
                    List.Add(DB.MakeInParam("@Login", SqlDbType.VarChar, 50, LoginName));

                    flag = Convert.ToBoolean(DB.RunProc("up_Users_CheckLoginNameAvailable", List.ToArray()));
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.CheckLoginNameAvailable", ex);
            }

            return flag;
        }

        public static void CreateStakeholderUser(UserInfo objUserInfo)
        {
            try
            {
                List<SqlParameter> List = new List<SqlParameter>();
                using (DbManager DB = DbManager.GetDbManager())
                {
                    if (objUserInfo.UserId > 0)
                        List.Add(DB.MakeInParam("@UserId", SqlDbType.Int, 4, objUserInfo.UserId));
                    else
                        List.Add(DB.MakeInParam("@UserId", SqlDbType.Int, 4, DBNull.Value));

                    List.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, objUserInfo.OrganizationId));
                    List.Add(DB.MakeInParam("@Login", SqlDbType.NVarChar, 75, objUserInfo.Login));
                    List.Add(DB.MakeInParam("@Pwd", SqlDbType.NVarChar, 90, objUserInfo.Pwd));
                    List.Add(DB.MakeInParam("@PwdSalt", SqlDbType.NVarChar, 90, objUserInfo.PwdSalt));
                    List.Add(DB.MakeInParam("@IsActive", SqlDbType.Bit, 1, objUserInfo.IsActive));
                    List.Add(DB.MakeInParam("@TX_UserId", SqlDbType.NVarChar, 20, objUserInfo.TX_UserId));
                    List.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, objUserInfo.LanguageId));
                    List.Add(DB.MakeInParam("@TimeZoneID", SqlDbType.Int, 4, objUserInfo.TimeZoneID));
                    List.Add(DB.MakeInParam("@ContactId", SqlDbType.Int, 4, objUserInfo.ContactId));
                    List.Add(DB.MakeInParam("@IsApproved", SqlDbType.Bit, 1, objUserInfo.IsApproved));
                    List.Add(DB.MakeInParam("@RoleId", SqlDbType.Int, 4, objUserInfo.RoleId));
                    List.Add(DB.MakeInParam("@bitIsOrgAdmin", SqlDbType.Bit, 1, objUserInfo.IsOrgAdmin));
                    List.Add(DB.MakeInParam("@DateCreated", SqlDbType.DateTime, 0, objUserInfo.DateCreated));
                    List.Add(DB.MakeInParam("@bitIsSetPassword", SqlDbType.Bit, 1, objUserInfo.bitSetPassword));
                    objUserInfo.UserId = DB.RunProc("up_Users_CreateStakeholderUser", List.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.CreateStakeholderUser", ex);
            }
        }

        public static int InsertUser(UserInfo objUser, int OrganizationId, string RoleIDs,Boolean bitIsSuperAdmin=false)
        {
            int userId = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));
                    prams.Add(db.MakeInParam("@Login", SqlDbType.NVarChar, 75, objUser.Login));
                    prams.Add(db.MakeInParam("@Password", SqlDbType.NVarChar, 90, objUser.Pwd));
                    prams.Add(db.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, objUser.DateCreated));
                    prams.Add(db.MakeInParam("@CreatedByUserId", SqlDbType.Int, 4, objUser.CreatedByUserId));
                    prams.Add(db.MakeInParam("@FirstName", SqlDbType.NVarChar, 30, objUser.FirstName));
                    prams.Add(db.MakeInParam("@MiddleName", SqlDbType.NVarChar, 10, objUser.MiddleName));
                    prams.Add(db.MakeInParam("@LastName", SqlDbType.NVarChar, 30, objUser.LastName));
                    prams.Add(db.MakeInParam("@Number", SqlDbType.NVarChar, 15, objUser.Number));
                    prams.Add(db.MakeInParam("@Email", SqlDbType.NVarChar, 90, objUser.Email));
                    prams.Add(db.MakeInParam("@LanguageId", SqlDbType.Int, 4, objUser.LanguageId));
                    prams.Add(db.MakeInParam("@IsApproved", SqlDbType.Bit, 1, objUser.IsApproved));
                    prams.Add(db.MakeInParam("@RoleIDs", SqlDbType.NVarChar, -1, RoleIDs));
                    prams.Add(db.MakeInParam("@bitIsSuperAdmin", SqlDbType.Bit, 1, bitIsSuperAdmin));
                   
                    objUser.UserId = db.RunProc("up_Users_Insert", prams.ToArray());
                    userId = objUser.UserId;
                    
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.up_Users_Insert", ex);
            }

            return userId;
        }

        public static DataSet GetAllActiveLanguages()
        {
            DataSet ds = null;
            try
            {

                using (DbManager DB = DbManager.GetDbManager())
                {
                    ds = DB.GetDataSet("up_Language_GetAllActive", null);
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.GetAllActiveLanguages", ex);
            }
            return ds;
        }

        public static DataSet GetAllCountryWithLangauage()
        {
            DataSet ds = null;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    ds = DB.GetDataSet("up_GetAllCountryWithLangauage");
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.GetAllCountryWithLangauage", ex);
            }
            return ds;
        }

        public static bool UpdateUser(UserInfo objUser)
        {
            bool UpdateSuccessfull = false;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(db.MakeInParam("@UserId", SqlDbType.Int, 4, objUser.UserId));
                    if (objUser.Pwd == "")
                        prams.Add(db.MakeInParam("@Password", SqlDbType.NVarChar, 90, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@Password", SqlDbType.NVarChar, 90, objUser.Pwd));
                    prams.Add(db.MakeInParam("@FirstName", SqlDbType.NVarChar, 30, objUser.FirstName));
                    prams.Add(db.MakeInParam("@MiddleName", SqlDbType.NVarChar, 10, objUser.MiddleName));
                    prams.Add(db.MakeInParam("@LastName", SqlDbType.NVarChar, 30, objUser.LastName));
                    prams.Add(db.MakeInParam("@Number", SqlDbType.NVarChar, 15, objUser.Number));
                    prams.Add(db.MakeInParam("@Email", SqlDbType.NVarChar, 90, objUser.Email));
                    prams.Add(db.MakeInParam("@RoleId", SqlDbType.Int, 4, objUser.RoleId));
                    prams.Add(db.MakeInParam("@IsActive", SqlDbType.Int, 4, objUser.IsActive));
                    db.RunProc("up_User_Update", prams.ToArray());

                    UpdateSuccessfull = true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.UpdateUser", ex);
            }

            return UpdateSuccessfull;
        }

        public static bool UpdateUserInfo(UserInfo objUserInfo)
        {
            try
            {
                List<SqlParameter> List = new List<SqlParameter>();
                using (DbManager DB = DbManager.GetDbManager())
                {
                    
                    List.Add(DB.MakeInParam("@UserId", SqlDbType.Int, 4, objUserInfo.UserId));
                    List.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, objUserInfo.OrganizationId));
                    List.Add(DB.MakeInParam("@Login", SqlDbType.NVarChar, 75, objUserInfo.Login));
                    List.Add(DB.MakeInParam("@Pwd", SqlDbType.NVarChar, 90, objUserInfo.Pwd));
                    List.Add(DB.MakeInParam("@PwdSalt", SqlDbType.NVarChar, 90, objUserInfo.PwdSalt));
                    List.Add(DB.MakeInParam("@IsActive", SqlDbType.Bit, 1, objUserInfo.IsActive));
                    List.Add(DB.MakeInParam("@TX_UserId", SqlDbType.NVarChar, 20, objUserInfo.TX_UserId));
                    List.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, objUserInfo.LanguageId));
                    List.Add(DB.MakeInParam("@TimeZoneID", SqlDbType.Int, 4, objUserInfo.TimeZoneID));
                    List.Add(DB.MakeInParam("@ContactId", SqlDbType.Int, 4, objUserInfo.ContactId));
                    List.Add(DB.MakeInParam("@IsApproved", SqlDbType.Bit, 1, objUserInfo.IsApproved));
                    List.Add(DB.MakeInParam("@RoleId", SqlDbType.Int, 4, objUserInfo.RoleId));
                    List.Add(DB.MakeInParam("@bitIsOrgAdmin", SqlDbType.Bit, 1, objUserInfo.IsOrgAdmin));
                    List.Add(DB.MakeInParam("@DateCreated", SqlDbType.DateTime, 0, objUserInfo.DateCreated));
                    List.Add(DB.MakeInParam("@bitIsSetPassword", SqlDbType.Bit, 1, objUserInfo.bitSetPassword));
                    objUserInfo.UserId = DB.RunProc("up_UpdateUserInfo", List.ToArray());
                    return true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.UpdateUserInfo", ex);
                return false;
            }
        }


        public static bool UpdateUserProfile(UserInfo objUser)
        {
            bool UpdateSuccessfull = false;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(db.MakeInParam("@UserId", SqlDbType.Int, 4, objUser.UserId));
                    if (objUser.Pwd == "")
                        prams.Add(db.MakeInParam("@Password", SqlDbType.NVarChar, 90, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@Password", SqlDbType.NVarChar, 90, objUser.Pwd));
                    prams.Add(db.MakeInParam("@FirstName", SqlDbType.NVarChar, 30, objUser.FirstName));
                    prams.Add(db.MakeInParam("@MiddleName", SqlDbType.NVarChar, 10, objUser.MiddleName));
                    prams.Add(db.MakeInParam("@LastName", SqlDbType.NVarChar, 30, objUser.LastName));
                    prams.Add(db.MakeInParam("@Number", SqlDbType.NVarChar, 15, objUser.Number));
                    prams.Add(db.MakeInParam("@Email", SqlDbType.NVarChar, 90, objUser.Email));
                    prams.Add(db.MakeInParam("@profileimage", SqlDbType.VarBinary, 5000, objUser.UserProfileImage));

                    db.RunProc("up_user_updateprofile", prams.ToArray());

                    UpdateSuccessfull = true;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.UpdateUserProfile", ex);
            }

            return UpdateSuccessfull;
        }

        public static DataSet ViewUserInfoById(int UserId)
        {
            DataSet ds = null;

            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@userId", SqlDbType.Int, 0, UserId));

                    ds = db.GetDataSet("up_User_Info_byUserId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.ViewUserInfoById", exp);
            }
            return ds;
        }

        public static UserInfo UserTopRole(int memberId)
        {
            UserInfo member = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@intUserId", SqlDbType.Int, 0, memberId);


                    using (IDataReader reader = db.GetDataReader("up_UserTopRole", prams))
                    {
                        if (reader.Read())
                        {
                            member = new UserInfo();
                            member.UserId = reader["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserId"]);
                            member.FirstName = reader["FirstName"].ToString();
                            member.MiddleName = reader["MiddleName"].ToString();
                            member.LastName = reader["LastName"].ToString();
                            member.FullName = member.FirstName + " " + member.MiddleName + " " + member.LastName;
                            member.RoleId = reader["RoleId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RoleId"]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.UserTopRole", e);
                return null;
            }
            return member;
        }

        public static int UserRoleTypeByOrgType(int OrgType)
        {
            int roleId = 0;
            // Role for Stewarship
            if (OrgType == 20 || OrgType == 115 || OrgType == 121 || OrgType == 127 || OrgType == 133 || OrgType == 139 || OrgType == 145 || OrgType == 151)
            {
                roleId = 100;
            }


            //Role for Local Steward
            if (OrgType == 21 || OrgType == 116 || OrgType == 122 || OrgType == 128 || OrgType == 134 || OrgType == 140 || OrgType == 146 || OrgType == 152)
            {
                roleId = 340;
            }
            //Role For Stakeholder
            if (OrgType == 22 || OrgType == 117 || OrgType == 123 || OrgType == 129 || OrgType == 135 || OrgType == 141 || OrgType == 147 || OrgType == 153)
            {
                roleId = 99;
            }


            //role for Government Agency             
            if (OrgType == 23 || OrgType == 118 || OrgType == 124 || OrgType == 130 || OrgType == 136 || OrgType == 142 || OrgType == 148 || OrgType == 154)
            {roleId =341;
            }

            //role for Law Enforcement Agency        
            if (OrgType == 24 || OrgType == 119 || OrgType == 125 || OrgType == 131 || OrgType == 137 || OrgType == 143 || OrgType == 149 || OrgType == 155)
            {roleId =342;
            }
             //role for Law Global Steward                   
            if (OrgType == 47 || OrgType == 120 || OrgType == 126 || OrgType == 132 || OrgType == 138 || OrgType == 144 || OrgType == 150 || OrgType == 156)
            {
                roleId = 339;
            }

                 return roleId;
        }

        public static bool CheckUserCountryState(int userId, int stateid)
        {
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                   
                    if (stateid > 0)
                        prams.Add(db.MakeInParam("@stateId", SqlDbType.Int, 4, stateid));
                    else
                        prams.Add(db.MakeInParam("@stateId", SqlDbType.Int, 4, DBNull.Value));
                    prams.Add(db.MakeInParam("@userId", SqlDbType.Int, 4, userId));

                    int exec = db.RunProc("up_checkUserIdBycountryState", prams.ToArray());
                    if (exec == 1)
                        return true;
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.CheckUserCountryState", exp);
            }
            return false;
        }

        public static DataSet getRolesByGroupId(int groupId)
        {
            DataSet ds = null;

            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intGroupId", SqlDbType.Int, 0, groupId));

                    ds = db.GetDataSet("up_getRolesByGroupIdForUser", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.getRolesByGroupId", exp);
            }
            return ds;
        }
   
        public static DataTable CheckUserLogin(string strLogin)
        {
            DataSet ds = null;
            DataTable dt = null;

            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@vchUserLogin", SqlDbType.VarChar, 100, strLogin));

                    ds = db.GetDataSet("up_CheckLogin", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
                }
            }
            catch (Exception exp)
            {
                throw;
            }
            return dt;

        }
        public static DataSet getUserPermissionByFilter(int pageId, int pageSize, int roleId, int organizationTypeId, string UserName, string LegalName, string LoginName, int countryId, int stateId, bool isActive, out int iTotalrows)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 8, pageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 8, pageSize));
                    
                    //if (organizationId == 0)
                    //    prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int,0, DBNull.Value));
                    //else
                    //    prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, organizationId));

                    if (UserName == "")
                        prams.Add(db.MakeInParam("@Username", SqlDbType.NVarChar, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@Username", SqlDbType.NVarChar, 0, UserName));

                    if (LegalName == "")
                        prams.Add(db.MakeInParam("@LegalName", SqlDbType.NVarChar, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@LegalName", SqlDbType.NVarChar, 0, LegalName));

                    if (LoginName == "")
                        prams.Add(db.MakeInParam("@Login", SqlDbType.NVarChar, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@Login", SqlDbType.NVarChar, 0, LoginName));

                    if (organizationTypeId == 0)
                        prams.Add(db.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 0, organizationTypeId));

                    if (roleId == 0)
                        prams.Add(db.MakeInParam("@RoleId", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@RoleId", SqlDbType.Int, 0, roleId));

                    if (countryId == 0)
                        prams.Add(db.MakeInParam("@CountryId", SqlDbType.Int,0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CountryId", SqlDbType.Int,0, countryId));

                    if (stateId == 0)
                        prams.Add(db.MakeInParam("@StateId", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@StateId", SqlDbType.Int, 0, stateId));

                    prams.Add(db.MakeInParam("@IsActive", SqlDbType.Bit, 0, isActive));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));

                    ds = db.GetDataSet("up_GetUsersPermission_Paging", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.getUserPermissionByFilter", exp);
            }
            return ds;


        }

        public static DataSet GetAdminEmail(int OrgId)
        {
            DataSet ds = null;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, OrgId));

                    ds = db.GetDataSet("Up_GetAdminEmail", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.GetAdminEmail", exp);
            }
            return ds;

        }


        public static DataSet GetAdminEmailAdmin(int UserId)
        {
            DataSet ds = null;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@UserId", SqlDbType.Int, 0, UserId));

                    ds = db.GetDataSet("Up_GetAdminEmailAdmin", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "UserInfo.GetAdminEmailAdmin", exp);
            }
            return ds;

        }

    }
}
