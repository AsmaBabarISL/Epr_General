using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{

    public class ContactInfo
    {
        #region Contacts
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

        private String _firstName;

        public String FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        private String _middleName;

        public String MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }
        private String _lastName;

        public String LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private String _email;

        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private Boolean _isPrimary;

        public Boolean IsPrimary
        {
            get { return _isPrimary; }
            set { _isPrimary = value; }
        }
        private Boolean _isActive;

        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private int _languageId;

        public int LanguageId
        {
            get { return _languageId; }
            set { _languageId = value; }
        }

        #endregion

        #region ContactTypes

        private int _contactType;

        public int ContactType
        {
            get { return _contactType; }
            set { _contactType = value; }
        }
        #endregion

        #region ContactTitle

        private int _contactTitleId;
        public int ContactTitleId
        {
            get { return _contactTitleId; }
            set { _contactTitleId = value; }
        }

        #endregion

        #region Contact_Phone
        private int _phoneId;

        public int PhoneId
        {
            get { return _phoneId; }
            set { _phoneId = value; }
        }

        #endregion

        #region Language

        private String _language;

        public String Language
        {
            get { return _language; }
            set { _language = value; }
        }
        private String _specific;

        public String Specific
        {
            get { return _specific; }
            set { _specific = value; }
        }
        #endregion

        public ContactInfo()
        {

        }
        public ContactInfo(int contactId)
        {
            Load(contactId);

        }

        private void Load(int contactId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@contactId", SqlDbType.Int, 0, contactId);
                    reader = db.GetDataReader("up_Contacts_getById", prams);
                    if (reader.Read())
                        Load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "ContactInfo.Load", e);
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
                _contactId = Conversion.ParseDBNullInt(reader["ContactId"]);
                _contactTypeId = Conversion.ParseDBNullInt(reader["ContactTypeId"]);
                _firstName = Conversion.ParseDBNullString(reader["FirstName"]);
                _middleName = Conversion.ParseDBNullString(reader["MiddleName"]);
                _lastName = Conversion.ParseDBNullString(reader["LastName"]);
                _email = Conversion.ParseDBNullString(reader["Email"]);
                _isPrimary = Conversion.ParseDBNullBool(reader["IsPrimary"]);
                _isActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                _languageId = Conversion.ParseDBNullInt(reader["LanguageId"]);
                _contactType = Conversion.ParseDBNullInt(reader["ContactType"]);
                _language = Conversion.ParseDBNullString(reader["Langauge"]);
                _specific = Conversion.ParseDBNullString(reader["Specific"]);
                _phoneId = Conversion.ParseDBNullInt(reader["PhoneId"]);




            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "ContactInfo.Load", ex);
            }
        }

        public enum ContactTypes
        {
            Business = 1,
            Billing = 2
        }

        public enum ContactTitleTypes
        {
            PrimaryContact = 1,
            BillingContact = 2,
            LocationContact = 3
        }

    }
}
