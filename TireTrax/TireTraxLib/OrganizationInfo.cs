using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
	public class OrganizationInfo
	{
		#region Organization
         

		private int _organizationId;

		public int OrganizationId
		{
			get { return _organizationId; }
			set { _organizationId = value; }
		}
		private int _organizationTypeId;

		public int OrganizationTypeId
		{
			get { return _organizationTypeId; }
			set { _organizationTypeId = value; }
		}
		private String _legalName;

		public String LegalName
		{
			get { return _legalName; }
			set { _legalName = value; }
		}
		private String _dBAName;

		public String DBAName
		{
			get { return _dBAName; }
			set { _dBAName = value; }
		}
		private String _franchiseName;

		public String FranchiseName
		{
			get { return _franchiseName; }
			set { _franchiseName = value; }
		}
		private int _parentId;

		public int ParentId
		{
			get { return _parentId; }
			set { _parentId = value; }
		}
		private String _website;

		public String Website
		{
			get { return _website; }
			set { _website = value; }
		}
		private String _tX_ID;

		public String TX_ID
		{
			get { return _tX_ID; }
			set { _tX_ID = value; }
		}


       
		private List<int> _businessType;

		public List<int> BusinessType
		{
			get { return _businessType; }
			set { _businessType = value; }
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
		private int _agencyType;

		public int AgencyType
		{
			get { return _agencyType; }
			set { _agencyType = value; }
		}
		private int _accountingInterfaceId;

		public int AccountingInterfaceId
		{
			get { return _accountingInterfaceId; }
			set { _accountingInterfaceId = value; }
		}
		private int _inventoryInterfaceId;

		public int InventoryInterfaceId
		{
			get { return _inventoryInterfaceId; }
			set { _inventoryInterfaceId = value; }
		}
		private Boolean _isAutoFundTransfer;

		public Boolean IsAutoFundTransfer
		{
			get { return _isAutoFundTransfer; }
			set { _isAutoFundTransfer = value; }
		}
		private Boolean _isLocationEventPermanent;

		public Boolean IsLocationEventPermanent
		{
			get { return _isLocationEventPermanent; }
			set { _isLocationEventPermanent = value; }
		}
		private int _locationEventTypeId;

		public int LocationEventTypeId
		{
			get { return _locationEventTypeId; }
			set { _locationEventTypeId = value; }
		}
		private DateTime _locationEventStartDate;

		public DateTime LocationEventStartDate
		{
			get { return _locationEventStartDate; }
			set { _locationEventStartDate = value; }
		}
		private DateTime _locationEventEndDate;

		public DateTime LocationEventEndDate
		{
			get { return _locationEventEndDate; }
			set { _locationEventEndDate = value; }
		}
		private String _locationPermitNumber;

		public String LocationPermitNumber
		{
			get { return _locationPermitNumber; }
			set { _locationPermitNumber = value; }
		}
        private String _primaryEmail;

        public String PrimaryEmail
        {
            get { return _primaryEmail; }
            set { _primaryEmail = value; }
        }

        private int _OrganizationSubTypeID;

        public int OrganizationSubTypeID
        {
            get { return _OrganizationSubTypeID; }
            set { _OrganizationSubTypeID = value; }
        }
        private string _organizationType;

        public string OrganizationType
        {
            get { return _organizationType; }
            set { _organizationType = value; }
        }
        private string _OrganizationSubType;

        public string OrganizationSubType
        {
            get { return _OrganizationSubType; }
            set { _OrganizationSubType = value; }
        }
		#endregion

		#region Organization_Business

		private List<Organization_Business> _ObjOrgBusiness;

		public List<Organization_Business> ObjOrgBusiness
		{
			get { return _ObjOrgBusiness; }
			set { _ObjOrgBusiness = value; }
		}

		public class Organization_Business
		{
			private int _businessID;

			public int BusinessID
			{
				get { return _businessID; }
				set { _businessID = value; }
			}
			private Boolean _isNew;

			public Boolean IsNew
			{
				get { return _isNew; }
				set { _isNew = value; }
			}
		}
		#endregion

		#region Organization_Certification
		private List<int> _certificationID;

		public List<int> CertificationID
		{
			get { return _certificationID; }
			set { _certificationID = value; }
		}

		#endregion

		#region Organization_Client
		private int _clientid;

		public int Clientid
		{
			get { return _clientid; }
			set { _clientid = value; }
		}

		private Client _objClient;

		public Client objClient
		{
			get { return _objClient; }
			set { _objClient = value; }
		}

		#endregion

		#region Organization_Contacts


		private int _contactId;

		public int ContactId
		{
			get { return _contactId; }
			set { _contactId = value; }
		}

		#endregion 

		#region Organization_Address

		public class Organization_Address
		{
			private int _locationID;

			public int LocationID
			{
				get { return _locationID; }
				set { _locationID = value; }
			}
			private String _description;

			public String Description
			{
				get { return _description; }
				set { _description = value; }
			}
			private String _address;

			public String Address
			{
				get { return _address; }
				set { _address = value; }
			}
			private String _address1;

			public String Address1
			{
				get { return _address1; }
				set { _address1 = value; }
			}
			private String _address2;

			public String Address2
			{
				get { return _address2; }
				set { _address2 = value; }
			}
			private String _city;

			public String City
			{
				get { return _city; }
				set { _city = value; }
			}
            private double _cityId;

            public double CityId
            {
                get { return _cityId; }
                set { _cityId = value; }
            }
			private int _stateID;

			public int StateID
			{
				get { return _stateID; }
				set { _stateID = value; }
			}

			private int _countryID;

			public int CountryID
			{
				get { return _countryID; }
				set { _countryID = value; }
			}
			private int _zipCodeID;

			public int ZipCodeID
			{
				get { return _zipCodeID; }
				set { _zipCodeID = value; }
			}
			private String _zipPostalCode;

			public String ZipPostalCode
			{
				get { return _zipPostalCode; }
				set { _zipPostalCode = value; }
			}
			private DateTime _dateCreated;

			public DateTime DateCreated
			{
				get { return _dateCreated; }
				set { _dateCreated = value; }
			}
			private String _billMailAddress;

			public String BillMailAddress
			{
				get { return _billMailAddress; }
				set { _billMailAddress = value; }
			}
			private String _ownerManager;

			public String OwnerManager
			{
				get { return _ownerManager; }
				set { _ownerManager = value; }
			}
			private String _billingContact;

			public String BillingContact
			{
				get { return _billingContact; }
				set { _billingContact = value; }
			}
			private Boolean _isActive;

			public Boolean IsActive
			{
				get { return _isActive; }
				set { _isActive = value; }
			}
			private String _fax;

			public String Fax
			{
				get { return _fax; }
				set { _fax = value; }
			}
			private int _organization_AddressTypeId;

			public int Organization_AddressTypeId
			{
				get { return _organization_AddressTypeId; }
				set { _organization_AddressTypeId = value; }
			}

			public enum Organization_AddressType
			{
				Business = 1,
				Mailing = 2
			}

		}

		#endregion

		#region Organization_phones

		private int _phoneId;

		public int PhoneId
		{
			get { return _phoneId; }
			set { _phoneId = value; }
		}

		private Phones _objPhones;

		public Phones objPhones
		{
			get { return _objPhones; }
			set { _objPhones = value; }
		}

		#endregion

		#region Organization_Roles

		private int _roleId;

		public int RoleId
		{
			get { return _roleId; }
			set { _roleId = value; }
		}


		#endregion

		#region Organization_StakeHolder

		private int _stakeHolderId;

		public int StakeHolderId
		{
			get { return _stakeHolderId; }
			set { _stakeHolderId = value; }
		}

		#endregion

		#region Organization_supplier
		private int _supplierid;

		public int Supplierid
		{
			get { return _supplierid; }
			set { _supplierid = value; }
		}

		private Supplier _objSupplier;

		public Supplier objSupplier
		{
			get { return _objSupplier; }
			set { _objSupplier = value; }
		}

		#endregion

		#region OrganizationTypes

        //public static DataSet GetOrganizationSubTypes()
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        using (DbManager DB = DbManager.GetDbManager())
        //        {
        //            ds = DB.GetDataSet("up_Lookup_GetOrganizationSubType");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetOrganizationSubTypes", ex);
        //    }
        //    return ds;
        //}

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

		#region Country
		private String _countryName;

		public String CountryName
		{
			get { return _countryName; }
			set { _countryName = value; }
		}
		private String _abbreviation;

		public String Abbreviation
		{
			get { return _abbreviation; }
			set { _abbreviation = value; }
		}

		#endregion

		#region Roles
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

		#region Supplier

		public class Supplier
		{
            public Supplier()
            {
            }
            public Supplier(int supplierId)
            {
                IDataReader reader = null;
                try
                {
                    using (DbManager db = DbManager.GetDbManager())
                    {
                        
                        var prams = new SqlParameter[1];
                        prams[0] = db.MakeInParam("@SupplierId", SqlDbType.Int, 0, supplierId);
                        reader = db.GetDataReader("Up_GetSupplierBySupplierId", prams);
                        if (reader.Read())
                        {
                            try
                            {
                                SupplierID = Conversion.ParseDBNullInt(reader["Supplierid"]);

                                CompanyName = Conversion.ParseDBNullString(reader["CompanyName"]);
                                CountryID = Conversion.ParseDBNullInt(reader["CountryId"]);
                                StateId = Conversion.ParseDBNullInt(reader["StateId"]);
                                City = Conversion.ParseDBNullString(reader["City"]);
                                ZipCodeId = Conversion.ParseDBNullInt(reader["ZipcodeID"]);
                                ContactName = Conversion.ParseDBNullString(reader["ContactName"]);
                                Zipcode = Conversion.ParseDBNullString(reader["Zipcode"]);
                                BussinessPhone = Conversion.ParseDBNullString(reader["BussinessPhone"]);
                                Email = Conversion.ParseDBNullString(reader["OwnerManagerEmail"]);
                                IsActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                                BusinessPhoneExtention = Conversion.ParseDBNullString(reader["BusinessPhoneExtention"]);
                                CellPhone = Conversion.ParseDBNullString(reader["CellPhone"]);
                                OwnerManagerEmail = Conversion.ParseDBNullString(reader["OwnerManagerEmail"]);
                                LanguageID = Conversion.ParseDBNullInt(reader["LanguageID"]);
                                DateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
                                CreatedBy = Conversion.ParseDBNullInt(reader["CreatedBy"]);
                                Count = Conversion.ParseDBNullInt(reader["Count"]);
                            }
                            catch (Exception ex)
                            {
                                new SqlLog().InsertSqlLog(0, "Supplier(int SupplierId)", ex);
                            }
                           
                        }
                    }
                }
                catch (Exception e)
                {
                    new SqlLog().InsertSqlLog(0, "Supplier(int SupplierId)", e);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
            }
            string _zipcode;

            public string Zipcode
            {
                get { return _zipcode; }
                set { _zipcode = value; }
            }
			private int _SupplierID;
            private string _businessPhoneExtention;

            public string BusinessPhoneExtention
            {
                get { return _businessPhoneExtention; }
                set { _businessPhoneExtention = value; }
            }
            private string _cellPhone;

            public string CellPhone
            {
                get { return _cellPhone; }
                set { _cellPhone = value; }
            }
			public int SupplierID
			{
				get { return _SupplierID; }
				set { _SupplierID = value; }
			}
			private string _CompanyName;

			public string CompanyName
			{
				get { return _CompanyName; }
				set { _CompanyName = value; }
			}
			private int _CountryID;

			public int CountryID
			{
				get { return _CountryID; }
				set { _CountryID = value; }
			}
			private int _StateId;

			public int StateId
			{
				get { return _StateId; }
				set { _StateId = value; }
			}
			private string _City;

			public string City
			{
				get { return _City; }
				set { _City = value; }
			}
			private string _ContactName;

			public string ContactName
			{
				get { return _ContactName; }
				set { _ContactName = value; }
			}
			private string _BussinessPhone;

			public string BussinessPhone
			{
				get { return _BussinessPhone; }
				set { _BussinessPhone = value; }
			}
			private string _OwnerManagerEmail;

			public string OwnerManagerEmail
			{
				get { return _OwnerManagerEmail; }
				set { _OwnerManagerEmail = value; }
			}
			private bool _IsActive;

			public bool IsActive
			{
				get { return _IsActive; }
				set { _IsActive = value; }
			}
			private DateTime _DateCreated;

			public DateTime DateCreated
			{
				get { return _DateCreated; }
				set { _DateCreated = value; }
			}
			private int _LanguageID;

			public int LanguageID
			{
				get { return _LanguageID; }
				set { _LanguageID = value; }
			}
			private string _Email;

			public string Email
			{
				get { return _Email; }
				set { _Email = value; }
			}
			private float _Count;

			public float Count
			{
				get { return _Count; }
				set { _Count = value; }
			}
			private int _ZipCodeId;

			public int ZipCodeId
			{
				get { return _ZipCodeId; }
				set { _ZipCodeId = value; } 
			}
			private int _CreatedBy;

			public int CreatedBy 
			{
				get { return _CreatedBy; }
				set { _CreatedBy = value; }
			}
		}

		#endregion

		#region Client

		public class Client
		{
			private int _ClientID;

			public int ClientID
			{
				get { return _ClientID; }
				set { _ClientID = value; }
			}
			private string _CompanyName;

			public string CompanyName
			{
				get { return _CompanyName; }
				set { _CompanyName = value; }
			}
			private int _CountryID;

			public int CountryID
			{
				get { return _CountryID; }
				set { _CountryID = value; }
			}
			private int _StateId;

			public int StateId
			{
				get { return _StateId; }
				set { _StateId = value; }
			}
			private string _City;

			public string City
			{
				get { return _City; }
				set { _City = value; }
			}
			private int _ZipCodeId;

			public int ZipCodeId
			{
				get { return _ZipCodeId; }
				set { _ZipCodeId = value; }
			}
			private string _ContactName;

			public string ContactName
			{
				get { return _ContactName; }
				set { _ContactName = value; }
			}
			private string _BussinessPhone;

			public string BussinessPhone
			{
				get { return _BussinessPhone; }
				set { _BussinessPhone = value; }
			}
			private string _OwnerManagerEmail;

			public string OwnerManagerEmail
			{
				get { return _OwnerManagerEmail; }
				set { _OwnerManagerEmail = value; }
			}
			private bool _IsActive;

			public bool IsActive
			{
				get { return _IsActive; }
				set { _IsActive = value; }
			}
			private DateTime _DateCreated;

			public DateTime DateCreated
			{
				get { return _DateCreated; }
				set { _DateCreated = value; }
			}
			private int _LanguageId;

			public int LanguageId
			{
				get { return _LanguageId; }
				set { _LanguageId = value; }
			}
			private int _CreatedBy;

			public int CreatedBy
			{
				get { return _CreatedBy; }
				set { _CreatedBy = value; }
			}
		}

		#endregion

		#region Image

		private Image _objImage;

		public Image objImage
		{
			get { return _objImage; }
			set { _objImage = value; }
		}

		public class Image
		{
			private int _ImageID;

			public int ImageID
			{
				get { return _ImageID; }
				set { _ImageID = value; }
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
			private int _ModifiedBY;

			public int ModifiedBY
			{
				get { return _ModifiedBY; }
				set { _ModifiedBY = value; }
			}
			private DateTime _DateModified;

			public DateTime DateModified
			{
				get { return _DateModified; }
				set { _DateModified = value; }
			}
			private string _FileName;

			public string FileName
			{
				get { return _FileName; }
				set { _FileName = value; }
			}
			private int _ImageTypeID;

			public int ImageTypeID
			{
				get { return _ImageTypeID; }
				set { _ImageTypeID = value; }
			}
			private string _Description;

			public string Description
			{
				get { return _Description; }
				set { _Description = value; }
			}
			private bool _IsActive;

			public bool IsActive
			{
				get { return _IsActive; }
				set { _IsActive = value; }
			}
			private string _Extension;

			public string Extension
			{
				get { return _Extension; }
				set { _Extension = value; }
			}
			private string _HashCode;

			public string HashCode
			{
				get { return _HashCode; }
				set { _HashCode = value; }
			}
			private string _ImagePath;

			public string ImagePath
			{
				get { return _ImagePath; }
				set { _ImagePath = value; }
			}
			private bool _IsPrimary;

			public bool IsPrimary
			{
				get { return _IsPrimary; }
				set { _IsPrimary = value; }
			}
			private string _ThumbNailImageName;

			public string ThumbNailImageName
			{
				get { return _ThumbNailImageName; }
				set { _ThumbNailImageName = value; }
			}
		}

		#endregion


        #region
        /// <summary>
        /// Properties by sultan mehmood
        /// </summary>
        /// 

        private string _businestype;

        public string BusinesType
        {
            get { return _businestype; }
            set { _businestype = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _businessAddress;
        public string BusinessAddress
        {
            get { return _businessAddress; }
            set { _businessAddress = value; }
        }

        private string _alternativeaddress;
        public string AlternativeAddress
        {
            get { return _alternativeaddress; }
            set { _alternativeaddress = value; }
        }

        private string _billingcontact;
        public string BillingContact
        {
            get { return _billingcontact; }
            set { _billingcontact = value; }
        }

        private string _billMailAddress;

        public string BillMailAddress
        {
            get { return _billMailAddress; }
            set { _billMailAddress = value; }
        }

        private string _fax;
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private int _countryid;
        public int CountryID
        {
            get { return _countryid; }
            set { _countryid = value; }
        }

        private int _stateid;
        public int StateID
        {
            get { return _stateid; }
            set { _stateid = value; }
        }

        private string _statename;
        public string StateName
        {
            get { return _statename; }
            set { _statename = value; }
        }

        private string _zipcode;
        public string ZipCode
        {
            get { return _zipcode; }
            set { _zipcode = value; }
        }

        private int _zipcodeid;
        public int ZipCodeID
        {
            get { return _zipcodeid; }
            set { _zipcodeid = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _firstname;
        public string FirstName
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        private string _middlename;
        public string MiddleName
        {
            get { return _middlename; }
            set { _middlename = value; }
        }

        private string _lastname;
        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        private string _contacttitlename;
        public string ContactTitleName
        {
            get { return _contacttitlename; }
            set { _contacttitlename = value; }
        }

        private string _businessnumber;
        public string BusinessNumber
        {
            get { return _businessnumber; }
            set { _businessnumber = value; }
        }

        private string _businessExtension;
        public string BusinessExtension
        {
            get { return _businessExtension; }
            set { _businessExtension = value; }
        }

        private string _businessPhonetype;
        public string BusinessPhoneType
        {
            get { return _businessPhonetype; }
            set { _businessPhonetype = value; }
        }

        private bool _accepttextmessages;
        public bool AcceptTextMessages
        {
            get { return _accepttextmessages; }
            set { _accepttextmessages = value; }
        }

        private string _cellNumber;
        public string CellNumber
        {
            get { return _cellNumber; }
            set { _cellNumber = value; }
        }

        private string _cellextension;
        public string CellExtension
        {
            get { return _cellextension; }
            set { _cellextension = value; }
        }

        private bool _cellaccepttextmessages;
        public bool CellAcceptTextMessages
        {
            get { return _cellaccepttextmessages; }
            set { _cellaccepttextmessages = value; }
        }

        private string _cellphonetype;
        public string CellPhoneType
        {
            get { return _cellphonetype; }
            set { _cellphonetype = value; }
        }

        #endregion

        public OrganizationInfo() { }

		public OrganizationInfo(int OrgId)
		{
			Load(OrgId);
		}
        #region 
        /// <summary>
        /// Code by Sultan, Please dont change without consultation
        /// </summary>
        public OrganizationInfo(int OrganizationID, bool isNew = true)
        {
            Load(OrganizationID, true);
        }


        private void Load(int OrganizationID, bool isNew = true)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, OrganizationID);
                    reader = db.GetDataReader("up_Stewardship_getByOrganizationId", prams);
                    if (reader.Read())
                        Load(reader,true);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.Load", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void Load(IDataReader reader, bool isNew = true)
        {
            try
            {
                _organizationId =Conversion.ParseInt(reader["OrganizationId"]);
                _dBAName = Conversion.ParseDBNullString(reader["DBAName"]);
                _businestype = Conversion.ParseString(reader["BusinessType"]);
                _legalName = Conversion.ParseString(reader["LegalName"]);
                _website = Conversion.ParseString(reader["Website"]);
                _organizationType = Conversion.ParseDBNullString(reader["OrganizationType"]);
                _organizationTypeId = Conversion.ParseDBNullInt(reader["OrganizationTypeId"]);
                _OrganizationSubType = Conversion.ParseDBNullString(reader["OrganizationSubType"]);
                _OrganizationSubTypeID = Conversion.ParseDBNullInt(reader["OrganizationSubTypeID"]);
                _description = Conversion.ParseDBNullString(reader["OrganizationType"]);
                _address = Conversion.ParseString(reader["Address"]);
                _businessAddress = Conversion.ParseString(reader["BusinessAddress1"]);
                _alternativeaddress = Conversion.ParseString(reader["Address2"]);
                _billingcontact = Conversion.ParseString(reader["BilingContact"]);
                _billMailAddress = Conversion.ParseString(reader["BillMailAddress"]);
                _fax = Conversion.ParseString(reader["Fax"]);
                _city = Conversion.ParseString(reader["City"]);
                _countryid = Conversion.ParseInt(reader["CountryId"]);
                _countryName = Conversion.ParseString(reader["CountryName"]);
                _abbreviation = Conversion.ParseString(reader["Abbreviation"]);
                _statename = Conversion.ParseString(reader["StateName"]);
                _zipcode = Conversion.ParseString(reader["Zipcode"]);
                _email = Conversion.ParseString(reader["Email"]);
                _firstname = Conversion.ParseString(reader["FirstName"]);
                _lastname = Conversion.ParseString(reader["LastName"]);
                _middlename = Conversion.ParseString(reader["MiddleName"]);
                _contacttitlename = Conversion.ParseString(reader["ContactTitleName"]);
                _businessnumber = Conversion.ParseString(reader["BusinessNumber"]);
                _businessExtension = Conversion.ParseString(reader["BusinessExtension"]);
                _businessPhonetype = Conversion.ParseString(reader["BusinessPhoneType"]);
                _accepttextmessages = Conversion.ParseBool(reader["BusinessIsAccepTextMessages"]);
                _cellNumber = Conversion.ParseString(reader["CellNumber"]);
                _cellextension = Conversion.ParseString(reader["CellExtension"]);
                _cellphonetype = Conversion.ParseString(reader["CellPhoneType"]);
                _cellaccepttextmessages = Conversion.ParseBool(reader["CellIsAcceptTextMessages"]);
                _stateid = Conversion.ParseInt(reader["StateId"]);
                _zipcodeid = Conversion.ParseInt(reader["StateId"]);

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.Load", ex);
            }
        }

        #endregion

        private void Load(int OrganizationId)
		{
			IDataReader reader = null;
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					var prams = new SqlParameter[1];
					prams[0] = db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, OrganizationId);
					reader = db.GetDataReader("up_Organization_getById", prams);
					if (reader.Read())
						Load(reader);
				}
			}
			catch (Exception e)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.Load", e);
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
				_organizationId = Conversion.ParseDBNullInt(reader["OrganizationId"]);
				_organizationTypeId = Conversion.ParseDBNullInt(reader["OrganizationTypeId"]);
                _OrganizationSubTypeID = Conversion.ParseDBNullInt(reader["OrganizationSubTypeID"]);
                _organizationType = Conversion.ParseDBNullString(reader["OrganizationType"]);
                _OrganizationSubType = Conversion.ParseDBNullString(reader["OrganizationSubType"]);
				_legalName = Conversion.ParseDBNullString(reader["LegalName"]);
                _primaryEmail = Conversion.ParseDBNullString(reader["Email"]);
				_dBAName = Conversion.ParseDBNullString(reader["DBAName"]);
				_franchiseName = Conversion.ParseDBNullString(reader["FranchiseName"]);
				_parentId = Conversion.ParseDBNullInt(reader["ParentId"]);
				_website = Conversion.ParseDBNullString(reader["Website"]);
				_tX_ID = Conversion.ParseDBNullString(reader["TX_ID"]);
				//_businessType = Conversion.ParseDBNullInt(reader["BusinessType"]);
				_isActive = Conversion.ParseDBNullBool(reader["IsActive"]);
				_languageId = Conversion.ParseDBNullInt(reader["LanguageId"]);
				_agencyType = Conversion.ParseDBNullInt(reader["AgencyType"]);
				_specific = Conversion.ParseDBNullString(reader["Specific"]);

				_clientid = Conversion.ParseDBNullInt(reader["Clientid"]);
               
				_contactId = Conversion.ParseDBNullInt(reader["ContactId"]);
                //_locationID = Conversion.ParseDBNullInt(reader["LocationID"]);
                _description = Conversion.ParseDBNullString(reader["Description"]);
                _address = Conversion.ParseDBNullString(reader["Address"]);
                _city = Conversion.ParseDBNullString(reader["City"]);
                _statename = Conversion.ParseDBNullString(reader["StateName"]);
                //_stateID = Conversion.ParseDBNullInt(reader["StateProvince"]);
                //_countryID = Conversion.ParseDBNullInt(reader["CountryID"]);
				_countryName = Conversion.ParseDBNullString(reader["CountryName"]);
				_abbreviation = Conversion.ParseDBNullString(reader["Abbreviation"]);
				_languageId = Conversion.ParseDBNullInt(reader["LanguageId"]);

                _zipcode = Conversion.ParseDBNullString(reader["ZipPostalCode"]);

				//_dateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);

				//_ownerManager = Conversion.ParseDBNullString(reader["OwnerManager"]);
				//_billingContact = Conversion.ParseDBNullString(reader["BillingContact"]);
				_phoneId = Conversion.ParseDBNullInt(reader["PhoneId"]);
				_roleId = Conversion.ParseDBNullInt(reader["RoleId"]);
				_roleName = Conversion.ParseDBNullString(reader["RoleName"]);
                //_isOrganization = Conversion.ParseDBNullBool(reader["IsOrganization"]);
				_stakeHolderId = Conversion.ParseDBNullInt(reader["StakeHolderId"]);
				_supplierid = Conversion.ParseDBNullInt(reader["Supplierid"]);
				//_billMailAddress = Conversion.ParseDBNullString(reader["BillMailAddress"]);

				_language = Conversion.ParseDBNullString(reader["Language"]);

				_countryName = Conversion.ParseDBNullString(reader["CountryName"]);
                _OrganizationSubType = reader["OrganizationSubType"]==DBNull.Value?"": Conversion.ParseDBNullString(reader["OrganizationSubType"]);
				_objClient = new Client();

				_objClient.BussinessPhone = Conversion.ParseDBNullString(reader["ClientBussinessPhone"]);
				_objClient.City = Conversion.ParseDBNullString(reader["ClientCity"]);
				_objClient.ClientID = Conversion.ParseDBNullInt(reader["ClientID"]);
				_objClient.CompanyName = Conversion.ParseDBNullString(reader["ClientCompanyName"]);
				_objClient.ContactName = Conversion.ParseDBNullString(reader["ClientContactName"]);
				_objClient.CountryID = Conversion.ParseDBNullInt(reader["ClientCountryID"]);
				_objClient.DateCreated = Conversion.ParseDBNullDateTime(reader["ClientDateCreated"]);
				_objClient.IsActive = Conversion.ParseDBNullBool(reader["ClientIsActive"]);
				_objClient.LanguageId = Conversion.ParseDBNullInt(reader["ClientLanguageId"]);
				_objClient.OwnerManagerEmail = Conversion.ParseDBNullString(reader["ClientOwnerManagerEmail"]);
				_objClient.StateId = Conversion.ParseDBNullInt(reader["ClientStateId"]);

				_objSupplier = new Supplier();

				_objSupplier.BussinessPhone = Conversion.ParseDBNullString(reader["SupplierBussinessPhone"]);
				_objSupplier.City = Conversion.ParseDBNullString(reader["SupplierCity"]);
				_objSupplier.CompanyName = Conversion.ParseDBNullString(reader["SupplierCompanyName"]);
				_objSupplier.ContactName = Conversion.ParseDBNullString(reader["SupplierContactName"]);
				_objSupplier.Count = Conversion.ParseDBNullInt(reader["SupplierCount"]);
				_objSupplier.CountryID = Conversion.ParseDBNullInt(reader["SupplierCountryID"]);
				_objSupplier.DateCreated = Conversion.ParseDBNullDateTime(reader["SupplierDateCreated"]);
				_objSupplier.Email = Conversion.ParseDBNullString(reader["SupplierEmail"]);
				_objSupplier.IsActive = Conversion.ParseDBNullBool(reader["SupplierIsActive"]);
				_objSupplier.LanguageID = Conversion.ParseDBNullInt(reader["SupplierLanguageId"]);
				_objSupplier.OwnerManagerEmail = Conversion.ParseDBNullString(reader["SupplierOwnerManagerEmail"]);
				_objSupplier.StateId = Conversion.ParseDBNullInt(reader["SupplierStateId"]);
				_objSupplier.SupplierID = Conversion.ParseDBNullInt(reader["SupplierID"]);

				if (reader.NextResult())
				{
					_certificationID = new List<int>();
					while (reader.Read())
					{
						_certificationID.Add(Conversion.ParseDBNullInt(reader["certificationId"]));
					}
				}

				if (reader.NextResult())
				{
					ObjOrgBusiness = new List<Organization_Business>();
					Organization_Business obj;
					while (reader.Read())
					{
						obj = new Organization_Business();
						obj.BusinessID = Conversion.ParseDBNullInt(reader["BusinessId"]);
						obj.IsNew = Conversion.ParseDBNullBool(reader["isNew"]);
						ObjOrgBusiness.Add(obj);
					}
				}

				if (reader.NextResult())
				{
					_businessType = new List<int>();
					while (reader.Read())
					{
						_businessType.Add(Convert.ToInt32(reader["BusinessID"]));
					}
				}
               

                
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.Load", ex);
			}
		}

		public int InsertOrganizationInfo()
		{
			List<SqlParameter> prm = new List<SqlParameter>();
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					prm.Add(DB.MakeOutParam("@OrganizationId", SqlDbType.Int, 4));

					prm.Add(DB.MakeInParam("@OrganizationOrganizationTypeId", SqlDbType.Int, 4, OrganizationTypeId));
                    prm.Add(DB.MakeInParam("@OrganizationOrganizationSubTypeId", SqlDbType.Int, 4, OrganizationSubTypeID));
					prm.Add(DB.MakeInParam("@OrganizationLegalName", SqlDbType.NVarChar, 255, LegalName));
					prm.Add(DB.MakeInParam("@OrganizationDBAName", SqlDbType.NVarChar, 255, DBAName));
					prm.Add(DB.MakeInParam("@OrganizationFranchiseName", SqlDbType.NVarChar, 255, FranchiseName));
					prm.Add(DB.MakeInParam("@OrganizationParentId", SqlDbType.Int, 4, ParentId));
					prm.Add(DB.MakeInParam("@OrganizationWebsite", SqlDbType.NVarChar, 255, Website));
					prm.Add(DB.MakeInParam("@OrganizationTX_ID", SqlDbType.NVarChar, 90, TX_ID));
					prm.Add(DB.MakeInParam("@OrganizationBusinessType", SqlDbType.Int, 4, BusinessType));
					prm.Add(DB.MakeInParam("@OrganizationIsActive", SqlDbType.Bit, 1, IsActive));
					prm.Add(DB.MakeInParam("@OrganizationLanguageId", SqlDbType.Int, 4, LanguageId));
					prm.Add(DB.MakeInParam("@OrganizationAgencyType", SqlDbType.Int, 4, AgencyType));
					prm.Add(DB.MakeInParam("@OrganizationAddressDescription", SqlDbType.NVarChar, 100, ""));
					//prm.Add(DB.MakeInParam("@OrganizationAddressAddress", SqlDbType.NVarChar, 250, Address));
					//prm.Add(DB.MakeInParam("@OrganizationAddressCity", SqlDbType.NVarChar, 50, City));
					//prm.Add(DB.MakeInParam("@OrganizationAddressStateProvince", SqlDbType.NVarChar, 100, StateID));
					//prm.Add(DB.MakeInParam("@OrganizationCountryID", SqlDbType.Int, 4, CountryID));
					//prm.Add(DB.MakeInParam("@OrganizationAddressZipPostalCode", SqlDbType.NVarChar, 100, ZipPostalCode));
					prm.Add(DB.MakeInParam("@OrganizationAddressIsActive", SqlDbType.Bit, 1, IsActive));
					//prm.Add(DB.MakeInParam("@OrganizationAddressDateCreated", SqlDbType.DateTime, 8, DateCreated));
					//prm.Add(DB.MakeInParam("@OrganizationAddressBillMailAddress", SqlDbType.NVarChar, 250, BillMailAddress));
					//prm.Add(DB.MakeInParam("@OrganizationAddressOwnerManager", SqlDbType.NVarChar, 50, OwnerManager));
					//prm.Add(DB.MakeInParam("@OrganizationAddressBillingContact", SqlDbType.NVarChar, 50, BillingContact));
					prm.Add(DB.MakeInParam("@OrganizationAddressFix", SqlDbType.NVarChar, 20, "")); // TODO
					prm.Add(DB.MakeInParam("@OrganizationClientid", SqlDbType.Int, 4, DBNull.Value));
					prm.Add(DB.MakeInParam("@OrganizationContactId", SqlDbType.Int, 4, ContactId));
					prm.Add(DB.MakeInParam("@OrganizationBusinessIntrestid", SqlDbType.Int, 4, DBNull.Value));
					prm.Add(DB.MakeInParam("@OrganizationPhoneId", SqlDbType.Int, 4, DBNull.Value));
					prm.Add(DB.MakeInParam("@OrganizationRoleId", SqlDbType.Int, 4, RoleId));
					if (StakeHolderId == 0)
						prm.Add(DB.MakeInParam("@OrganizationStakeholderId", SqlDbType.Int, 4, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@OrganizationStakeholderId", SqlDbType.Int, 4, StakeHolderId));
					prm.Add(DB.MakeInParam("@OrganizationSupplierid", SqlDbType.Int, 4, DBNull.Value));
					prm.Add(DB.MakeInParam("@OrganizationInterstInterest", SqlDbType.NVarChar, 25, ""));
					prm.Add(DB.MakeInParam("@OrganizationTypeDescription", SqlDbType.NVarChar, 55, ""));
					prm.Add(DB.MakeInParam("@OrganizationBusinessIsActive", SqlDbType.Bit, 1, 1));

					string BusinessIDs = "";
					string BusinessisNewString = "";

					foreach (Organization_Business item in ObjOrgBusiness)
					{
						BusinessIDs += Convert.ToString(item.BusinessID) + ",";
						BusinessisNewString += Convert.ToString(Convert.ToInt32(item.IsNew)) + ",";
					}

					string CertificationIDs = "";
					foreach (var item in CertificationID)
					{
						CertificationIDs += Convert.ToString(item) + ",";
					}

					prm.Add(DB.MakeInParam("@OrganizationBusinessIDString", SqlDbType.VarChar, 500, BusinessIDs.TrimEnd(',')));
					prm.Add(DB.MakeInParam("@OrganizationBusinessIsNewString", SqlDbType.VarChar, 500, BusinessisNewString.TrimEnd(',')));
					prm.Add(DB.MakeInParam("@CertificationIDs", SqlDbType.VarChar, 500, CertificationIDs.TrimEnd(',')));

					prm.Add(DB.MakeInParam("@SupplierCompanyname", SqlDbType.NVarChar, 255, objSupplier.CompanyName));
					prm.Add(DB.MakeInParam("@SupplierCountryID", SqlDbType.Int, 4, objSupplier.CountryID));
					prm.Add(DB.MakeInParam("@SupplierStateId", SqlDbType.Int, 4, objSupplier.StateId));
					prm.Add(DB.MakeInParam("@SupplierCity", SqlDbType.VarChar, 100, objSupplier.City));
					prm.Add(DB.MakeInParam("@SupplierContactName", SqlDbType.NVarChar, 100, objSupplier.ContactName));
					prm.Add(DB.MakeInParam("@SupplierBussinessPhone", SqlDbType.NVarChar, 100, objSupplier.BussinessPhone));
					prm.Add(DB.MakeInParam("@SupplierOwnerManager", SqlDbType.NVarChar, 100, objSupplier.ContactName));
					prm.Add(DB.MakeInParam("@SupplierIsActive", SqlDbType.Bit, 1, objSupplier.IsActive));
					prm.Add(DB.MakeInParam("@SupplierDateCreated", SqlDbType.DateTime, 8, objSupplier.DateCreated));
					prm.Add(DB.MakeInParam("@SupplierEmail", SqlDbType.NVarChar, 20, objSupplier.Email));
					prm.Add(DB.MakeInParam("@SupplierCount", SqlDbType.Float, 4, objSupplier.Count));

					prm.Add(DB.MakeInParam("@ClientCompanyName", SqlDbType.NVarChar, 255, objClient.CompanyName));
					prm.Add(DB.MakeInParam("@ClientCountryID", SqlDbType.Int, 4, objClient.CountryID));
					prm.Add(DB.MakeInParam("@ClientStateId", SqlDbType.Int, 4, objClient.StateId));
					prm.Add(DB.MakeInParam("@ClientCity", SqlDbType.VarChar, 100, objClient.City));
					prm.Add(DB.MakeInParam("@ClientContactName", SqlDbType.NVarChar, 100, objClient.ContactName));
					prm.Add(DB.MakeInParam("@ClientBusinessPhone", SqlDbType.NVarChar, 100, objClient.BussinessPhone));
					prm.Add(DB.MakeInParam("@ClientOwnerManagerEmail", SqlDbType.NVarChar, 100, objClient.OwnerManagerEmail));
					prm.Add(DB.MakeInParam("@ClientIsActive", SqlDbType.Bit, 1, objClient.IsActive));
					prm.Add(DB.MakeInParam("@ClientDateCreated", SqlDbType.DateTime, 8, objClient.DateCreated));

					prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, 1)); // TODO:

					//prm.Add(DB.MakeInParam("@PhoneNumber", SqlDbType.NVarChar, 15, ""));
					//prm.Add(DB.MakeInParam("@PhoneExtension", SqlDbType.NVarChar, 7, ""));
					//prm.Add(DB.MakeInParam("@PhoneIsActive", SqlDbType.Bit, 1, 1));
					//prm.Add(DB.MakeInParam("@PhoneTypeId", SqlDbType.Int, 4, 1));

					DB.RunProc("up_Organization_Registration", prm.ToArray());
				}
			}
			catch (Exception ex)
			{

				new SqlLog().InsertSqlLog(0, "OrganizationInfo.Insert", ex);
			}

			return 0;
		}

		public int UpdateOrganizationInfo()
		{
			List<SqlParameter> prm = new List<SqlParameter>();
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));

					prm.Add(DB.MakeInParam("@OrganizationOrganizationTypeId", SqlDbType.Int, 4, OrganizationTypeId));
                    prm.Add(DB.MakeInParam("@OrganizationOrganizationSubTypeId", SqlDbType.Int, 4, OrganizationSubTypeID));
					prm.Add(DB.MakeInParam("@OrganizationLegalName", SqlDbType.NVarChar, 255, LegalName));
					prm.Add(DB.MakeInParam("@OrganizationDBAName", SqlDbType.NVarChar, 255, DBAName));
					prm.Add(DB.MakeInParam("@OrganizationFranchiseName", SqlDbType.NVarChar, 255, FranchiseName));
					prm.Add(DB.MakeInParam("@OrganizationParentId", SqlDbType.Int, 4, ParentId));
					prm.Add(DB.MakeInParam("@OrganizationWebsite", SqlDbType.NVarChar, 255, Website));
					prm.Add(DB.MakeInParam("@OrganizationTX_ID", SqlDbType.NVarChar, 90, TX_ID));
					prm.Add(DB.MakeInParam("@OrganizationBusinessType", SqlDbType.Int, 4, BusinessType));
					prm.Add(DB.MakeInParam("@OrganizationIsActive", SqlDbType.Bit, 1, IsActive));
					//prm.Add(DB.MakeInParam("@OrganizationLocationId", SqlDbType.Int, 4, LocationID));
					prm.Add(DB.MakeInParam("@OrganizationLanguageId", SqlDbType.Int, 4, LanguageId));
					prm.Add(DB.MakeInParam("@OrganizationAgencyType", SqlDbType.Int, 4, AgencyType));
					prm.Add(DB.MakeInParam("@OrganizationAddressDescription", SqlDbType.NVarChar, 100, "")); // TODO
					//prm.Add(DB.MakeInParam("@OrganizationAddressAddress", SqlDbType.NVarChar, 250, Address));
					//prm.Add(DB.MakeInParam("@OrganizationAddressCity", SqlDbType.NVarChar, 50, City));
					//prm.Add(DB.MakeInParam("@OrganizationAddressStateProvince", SqlDbType.NVarChar, 100, StateID));
					//prm.Add(DB.MakeInParam("@OrganizationCountryID", SqlDbType.Int, 4, CountryID));
					//prm.Add(DB.MakeInParam("@OrganizationAddressZipPostalCode", SqlDbType.NVarChar, 100, ZipPostalCode));
					prm.Add(DB.MakeInParam("@OrganizationAddressIsActive", SqlDbType.Bit, 1, IsActive));
					//prm.Add(DB.MakeInParam("@OrganizationAddressDateCreated", SqlDbType.DateTime, 8, DateCreated));
					//prm.Add(DB.MakeInParam("@OrganizationAddressBillMailAddress", SqlDbType.NVarChar, 250, BillMailAddress));
					//prm.Add(DB.MakeInParam("@OrganizationAddressOwnerManager", SqlDbType.NVarChar, 50, OwnerManager));
					//prm.Add(DB.MakeInParam("@OrganizationAddressBillingContact", SqlDbType.NVarChar, 50, BillingContact));
					prm.Add(DB.MakeInParam("@OrganizationAddressFix", SqlDbType.NVarChar, 20, "")); // TODO
					prm.Add(DB.MakeInParam("@OrganizationClientid", SqlDbType.Int, 4, DBNull.Value));
					prm.Add(DB.MakeInParam("@OrganizationContactId", SqlDbType.Int, 4, ContactId));
					prm.Add(DB.MakeInParam("@OrganizationBusinessIntrestid", SqlDbType.Int, 4, DBNull.Value));
					prm.Add(DB.MakeInParam("@OrganizationPhoneId", SqlDbType.Int, 4, DBNull.Value));
					prm.Add(DB.MakeInParam("@OrganizationRoleId", SqlDbType.Int, 4, RoleId));
					prm.Add(DB.MakeInParam("@OrganizationStakeholderId", SqlDbType.Int, 4, StakeHolderId));
					prm.Add(DB.MakeInParam("@OrganizationSupplierid", SqlDbType.Int, 4, DBNull.Value));
					prm.Add(DB.MakeInParam("@OrganizationInterstInterest", SqlDbType.NVarChar, 25, ""));
					prm.Add(DB.MakeInParam("@OrganizationTypeDescription", SqlDbType.NVarChar, 55, ""));
					prm.Add(DB.MakeInParam("@OrganizationBusinessIsActive", SqlDbType.Bit, 1, 1));

					string BusinessIDs = "";
					string BusinessisNewString = "";

					foreach (Organization_Business item in ObjOrgBusiness)
					{
						BusinessIDs += Convert.ToString(item.BusinessID) + ",";
						BusinessisNewString += Convert.ToString(Convert.ToInt32(item.IsNew)) + ",";
					}

					string CertificationIDs = "";
					foreach (var item in CertificationID)
					{
						CertificationIDs += Convert.ToString(item) + ",";
					}

					prm.Add(DB.MakeInParam("@OrganizationBusinessIDString", SqlDbType.VarChar, 500, BusinessIDs.TrimEnd(',')));
					prm.Add(DB.MakeInParam("@OrganizationBusinessIsNewString", SqlDbType.VarChar, 500, BusinessisNewString.TrimEnd(',')));
					prm.Add(DB.MakeInParam("@CertificationIDs", SqlDbType.VarChar, 500, CertificationIDs.TrimEnd(',')));

					prm.Add(DB.MakeInParam("@SupplierID", SqlDbType.Int, 4, objSupplier.SupplierID));
					prm.Add(DB.MakeInParam("@SupplierCompanyname", SqlDbType.NVarChar, 255, objSupplier.CompanyName));
					prm.Add(DB.MakeInParam("@SupplierCountryID", SqlDbType.Int, 4, objSupplier.CountryID));
					prm.Add(DB.MakeInParam("@SupplierStateId", SqlDbType.Int, 4, objSupplier.StateId));
					prm.Add(DB.MakeInParam("@SupplierCity", SqlDbType.NVarChar, 100, objSupplier.City));
					prm.Add(DB.MakeInParam("@SupplierContactName", SqlDbType.NVarChar, 100, objSupplier.ContactName));
					prm.Add(DB.MakeInParam("@SupplierBussinessPhone", SqlDbType.NVarChar, 100, objSupplier.BussinessPhone));
					prm.Add(DB.MakeInParam("@SupplierOwnerManager", SqlDbType.NVarChar, 100, objSupplier.ContactName));
					prm.Add(DB.MakeInParam("@SupplierIsActive", SqlDbType.Bit, 1, objSupplier.IsActive));
					prm.Add(DB.MakeInParam("@SupplierDateCreated", SqlDbType.DateTime, 8, objSupplier.DateCreated));
					prm.Add(DB.MakeInParam("@SupplierEmail", SqlDbType.NVarChar, 20, objSupplier.Email));
					prm.Add(DB.MakeInParam("@SupplierCount", SqlDbType.Float, 4, objSupplier.Count));

					prm.Add(DB.MakeInParam("@ClientID", SqlDbType.Int, 4, objClient.ClientID));
					prm.Add(DB.MakeInParam("@ClientCompanyName", SqlDbType.NVarChar, 255, objClient.CompanyName));
					prm.Add(DB.MakeInParam("@ClientCountryID", SqlDbType.Int, 4, objClient.CountryID));
					prm.Add(DB.MakeInParam("@ClientStateId", SqlDbType.Int, 4, objClient.StateId));
					prm.Add(DB.MakeInParam("@ClientCity", SqlDbType.NVarChar, 100, objClient.City));
					prm.Add(DB.MakeInParam("@ClientContactName", SqlDbType.NVarChar, 100, objClient.ContactName));
					prm.Add(DB.MakeInParam("@ClientBusinessPhone", SqlDbType.NVarChar, 100, objClient.BussinessPhone));
					prm.Add(DB.MakeInParam("@ClientOwnerManagerEmail", SqlDbType.NVarChar, 100, objClient.OwnerManagerEmail));
					prm.Add(DB.MakeInParam("@ClientIsActive", SqlDbType.Bit, 1, objClient.IsActive));
					prm.Add(DB.MakeInParam("@ClientDateCreated", SqlDbType.DateTime, 8, objClient.DateCreated));

					prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, 1)); // TODO:

					//prm.Add(DB.MakeInParam("@PhoneNumber", SqlDbType.NVarChar, 15, ""));
					//prm.Add(DB.MakeInParam("@PhoneExtension", SqlDbType.NVarChar, 7, ""));
					//prm.Add(DB.MakeInParam("@PhoneIsActive", SqlDbType.Bit, 1, 1));
					//prm.Add(DB.MakeInParam("@PhoneTypeId", SqlDbType.Int, 4, 1));

					DB.RunProc("up_Organization_Registration_Update", prm.ToArray());
				}
			}
			catch (Exception ex)
			{

				new SqlLog().InsertSqlLog(0, "OrganizationInfo.Update", ex);
			}

			return 0;
		}

		public DataTable GetAllNonPrimaryBusinessTypes()
		{
			DataTable dt = null;
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					dt = DB.GetDataSet("up_Business_GetAllNonPrimary", null).Tables[0];
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetAllNonPrimaryBusinessTypes", ex);
			}

			return dt;
		}

		public DataTable GetAllCertificatesByType(int CertificateType)
		{
			DataTable dt = null;
			List<SqlParameter> prm = new List<SqlParameter>();
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					prm.Add(DB.MakeInParam("@CertificateTypeID", SqlDbType.Int, 4, CertificateType));
					dt = DB.GetDataSet("up_Certificates_GetAllActiveByType", prm.ToArray()).Tables[0];
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetAllCertificatesByType", ex);
			}

			return dt;
		}

		public static DataSet getOrganizationAlerts(int pageId, int pageSize, out int iTotalrows)
		{
			DataSet ds = null;
			iTotalrows = 0;
			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
					prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));
					prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
					ds = db.GetDataSet("up_Organization_Alerts", prams.ToArray());
					if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
						return ds;
					}
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.getOrganizationAlerts", exp);
			}
			return ds;


		}
        public static int getStateId(int organizationID)
        {
           
            int state_id = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@OrganizatiionId", SqlDbType.Int, 4, organizationID));
                    state_id = db.ExecuteScalar<int>("up_getStateIdByOrganizationId", prams.ToArray());
                    return state_id;
                   
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getStateId", exp);
            }
            return 0;


        }
        public static DataSet getTransactionInfo(int pageId, int pageSize, out int iTotalrows,String organizationName,String userName,String location)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {

                    if(organizationName=="")
                    prams.Add(db.MakeInParam("@organizationName",SqlDbType.NVarChar,100,DBNull.Value)); 
                    else
                    prams.Add(db.MakeInParam("@organizationName", SqlDbType.NVarChar, 100, organizationName));

                    if(userName=="")
                    prams.Add(db.MakeInParam("@username", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                    prams.Add(db.MakeInParam("@username", SqlDbType.NVarChar, 100, userName));

                    if(location=="")
                    prams.Add(db.MakeInParam("@location", SqlDbType.NVarChar, 100, DBNull.Value));
                    else
                    prams.Add(db.MakeInParam("@location", SqlDbType.NVarChar, 100, location));

                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_Transaction_Data", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getTransactionInfo", exp);
            }
            return ds;


        }

		public static DataSet getLatestStewardship(int pageId, int pageSize, out int iTotalrows)
		{
			DataSet ds = null;
			iTotalrows = 0;
			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
					prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));
					prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
					ds = db.GetDataSet("up_Latest_Stewardship", prams.ToArray());
					if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
						return ds;
					}
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.getLatestStewardship", exp);
			}
			return ds;


		}

		public static DataSet getLatestStakeholder()
		{
			DataSet ds = null;
			//  iTotalrows = 0;
			// List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					//  prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
					//   prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));
					//   prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
					ds = db.GetDataSet("up_Latest_Stakeholder");
					if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						//iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
						return ds;
					}
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.getLatestStakeholder", exp);
			}
			return ds;


		}

		public static void UpdateStewardshipAsInactive(int OrganizationId)
		{
			List<SqlParameter> prm = new List<SqlParameter>();
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));
					DB.RunProc("up_UpdateStewardshipAsInactive", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.UpdateStewardshipAsInactive", ex);
			}
		}

		public int CheckStewardshipExists(string Stewardship)
		{
			int ID = -1;
			List<SqlParameter> prm = new List<SqlParameter>();
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					prm.Add(DB.MakeInParam("@Stewardship", SqlDbType.NVarChar, 500, Stewardship));
					ID = DB.ExecuteScalar<int>("up_CheckStewardshipExists", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.CheckStewardshipExists", ex);
			}
			return ID;
		}

		public static DataSet getStakeholder(int pageId, int pageSize, string businesstype, string FranchiseName, string StakeholderName, string StakeholderType, out int iTotalrows)
		{
			DataSet ds = null;
			iTotalrows = 0;
			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
					prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));
                    prams.Add(db.MakeInParam("@vchbusinessType", SqlDbType.NVarChar,250, businesstype));
                    prams.Add(db.MakeInParam("@vchFranchiseName", SqlDbType.NVarChar,250, FranchiseName));
                    prams.Add(db.MakeInParam("@vchStakeholderType", SqlDbType.NVarChar, 250, StakeholderType));
                    prams.Add(db.MakeInParam("@vchStakeholderName", SqlDbType.NVarChar,250, StakeholderName));
					prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
					ds = db.GetDataSet("up_Stakeholder_Paging", prams.ToArray());
					if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
						return ds;
					}
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.getStakholder", exp);
			}
			return ds;
		}

		public static DataSet SearchStewardshipByCriteria(string LegalName, string TX_ID, string Phone, string Email, string Contact, int LanguageID, int RoleID,int statusid, int PageId, int PageSize, out int total)
		{
			DataSet ds = null;
			total = 0;
			List<SqlParameter> prm = new List<SqlParameter>();
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					prm.Add(DB.MakeOutParam("@total", SqlDbType.Int, 4));
					prm.Add(DB.MakeInParam("@PageId", SqlDbType.Int, 4, PageId));
					prm.Add(DB.MakeInParam("@PageSize", SqlDbType.NVarChar, 4, PageSize));

					if (LegalName == "")
						prm.Add(DB.MakeInParam("@LegalName", SqlDbType.NVarChar, 255, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@LegalName", SqlDbType.NVarChar, 255, LegalName));

					if (TX_ID == "")
						prm.Add(DB.MakeInParam("@TX_ID", SqlDbType.NVarChar, 90, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@TX_ID", SqlDbType.NVarChar, 90, TX_ID));

					if (Phone == "")
						prm.Add(DB.MakeInParam("@Phone", SqlDbType.NVarChar, 15, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@Phone", SqlDbType.NVarChar, 15, Phone));

					if (Email == "")
						prm.Add(DB.MakeInParam("@Email", SqlDbType.NVarChar, 90, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@Email", SqlDbType.NVarChar, 90, Email));

					if (Contact == "")
						prm.Add(DB.MakeInParam("@Contact", SqlDbType.NVarChar, 30, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@Contact", SqlDbType.NVarChar, 30, Contact));

					prm.Add(DB.MakeInParam("@RoleID", SqlDbType.Int, 4, RoleID));
                    prm.Add(DB.MakeInParam("@intStatusId", SqlDbType.Int, 4, statusid));
					prm.Add(DB.MakeInParam("@LanguageID", SqlDbType.Int, 4, LanguageID));

					ds = DB.GetDataSet("up_SearchStewardshipByCriteria", prm.ToArray());

					total = (int)prm.First<SqlParameter>().Value;

				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.SearchStewardshipByCriteria", ex);
			}
			return ds;
		}

		public void LoadForAdmin()
		{
			IDataReader reader = null;
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					var prams = new SqlParameter[1];
					prams[0] = db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, OrganizationId);
					reader = db.GetDataReader("up_Organization_GetByIDForAdmin", prams);
					if (reader.Read())
					{
						_organizationTypeId = Conversion.ParseDBNullInt(reader["OrganizationTypeId"]);
						_legalName = Conversion.ParseDBNullString(reader["LegalName"]);
						_dBAName = Conversion.ParseDBNullString(reader["DBAName"]);

						_isActive = Conversion.ParseDBNullBool(reader["IsActive"]);
						//_address = Conversion.ParseDBNullString(reader["Address"]);

						//_stateID = Conversion.ParseDBNullInt(reader["StateProvince"]);
						//_countryID = Conversion.ParseDBNullInt(reader["CountryID"]);
						//_zipPostalCode = Conversion.ParseDBNullString(reader["ZipPostalCode"]);

						//_dateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
						//_billingContact = Conversion.ParseDBNullString(reader["BillingContact"]);
					}

					reader.NextResult();

					if (reader.Read())
					{
						_objImage = new Image();

						_objImage.CreatedBy = Conversion.ParseDBNullInt(reader["CreatedBy"]);
						_objImage.DateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
						_objImage.DateModified = Conversion.ParseDBNullDateTime(reader["DateModified"]);
						_objImage.Extension = Conversion.ParseDBNullString(reader["Extension"]);
						_objImage.FileName = Conversion.ParseDBNullString(reader["FileName"]);
						_objImage.HashCode = Conversion.ParseDBNullString(reader["HashCode"]);
						_objImage.ImageID = Conversion.ParseDBNullInt(reader["ImageID"]);
						_objImage.ImagePath = Conversion.ParseDBNullString(reader["ImagePath"]);
						_objImage.ImageTypeID = Conversion.ParseDBNullInt(reader["ImageTypeID"]);
						_objImage.IsActive = Conversion.ParseDBNullBool(reader["IsActive"]);
						_objImage.IsPrimary = Conversion.ParseDBNullBool(reader["IsPrimary"]);
						_objImage.ModifiedBY = Conversion.ParseDBNullInt(reader["ModifiedBy"]);
						_objImage.ThumbNailImageName = Conversion.ParseDBNullString(reader["ThumbNailImageName"]);
						_objImage.Description = Conversion.ParseDBNullString(reader["Description"]);

					}

					if (reader.NextResult())
					{
						_businessType = new List<int>();
						while (reader.Read())
						{
							_businessType.Add(Convert.ToInt32(reader["BusinessID"]));
						}
					}

				}
			}
			catch (Exception e)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.LoadForAdmin", e);
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		public static void SaveBasicInfo(OrganizationInfo ObjOrg, ContactInfo objContactInfo, Phones objPhones)
		{
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					if (ObjOrg.OrganizationId > 0)
						prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, ObjOrg.OrganizationId));
					else
						prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, DBNull.Value));

					prm.Add(DB.MakeInParam("@LegalName", SqlDbType.NVarChar, 255, ObjOrg.LegalName));
					prm.Add(DB.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, ObjOrg.DBAName));
					prm.Add(DB.MakeInParam("@FranchiseName", SqlDbType.NVarChar, 255, ObjOrg.FranchiseName));
					if(ObjOrg.BusinessType != null)
						prm.Add(DB.MakeInParam("@BusinessType", SqlDbType.VarChar, -1, String.Join<int>(",", ObjOrg.BusinessType)));
					else
						prm.Add(DB.MakeInParam("@BusinessType", SqlDbType.VarChar, -1, DBNull.Value));
					prm.Add(DB.MakeInParam("@OrgIsActive", SqlDbType.Bit, 1, ObjOrg.IsActive));
					prm.Add(DB.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, ObjOrg.OrganizationTypeId));
                    prm.Add(DB.MakeInParam("@OrganizationSubTypeId", SqlDbType.Int, 4, ObjOrg.OrganizationSubTypeID));
					prm.Add(DB.MakeInParam("@ParentId", SqlDbType.Int, 4, ObjOrg.ParentId));
					prm.Add(DB.MakeInParam("@Website", SqlDbType.NVarChar, 255, ObjOrg.Website));
					prm.Add(DB.MakeInParam("@TX_ID", SqlDbType.NVarChar, 90, ObjOrg.TX_ID));
					prm.Add(DB.MakeInParam("@OrgLanguageId", SqlDbType.Int, 4, ObjOrg.LanguageId));
					prm.Add(DB.MakeInParam("@AgencyType", SqlDbType.Int, 4, ObjOrg.AgencyType));
					prm.Add(DB.MakeInParam("@RoleId", SqlDbType.Int, 4, ObjOrg.RoleId));

					prm.Add(DB.MakeInParam("@PhoneId", SqlDbType.Int, 4, objPhones.PhoneId));
					prm.Add(DB.MakeInParam("@Number", SqlDbType.NVarChar, 15, objPhones.Number));
					prm.Add(DB.MakeInParam("@Extension", SqlDbType.NVarChar, 7, objPhones.Extension));
					prm.Add(DB.MakeInParam("@PhoneTypeId", SqlDbType.Int, 4, objPhones.PhoneTypeId));
					prm.Add(DB.MakeInParam("@PhoneIsActive", SqlDbType.Bit, 1, objPhones.IsActive));

					prm.Add(DB.MakeInParam("@ContactId", SqlDbType.Int, 4, objContactInfo.ContactId));
					prm.Add(DB.MakeInParam("@ContactTypeId", SqlDbType.Int, 4, objContactInfo.ContactTypeId));
					prm.Add(DB.MakeInParam("@FirstName", SqlDbType.NVarChar, 30, objContactInfo.FirstName));
					prm.Add(DB.MakeInParam("@MiddleName", SqlDbType.NVarChar, 10, objContactInfo.MiddleName));
					prm.Add(DB.MakeInParam("@LastName", SqlDbType.NVarChar, 30, objContactInfo.LastName));
					prm.Add(DB.MakeInParam("@Email", SqlDbType.NVarChar, 90, objContactInfo.Email));
					prm.Add(DB.MakeInParam("@IsPrimary", SqlDbType.Bit, 1, objContactInfo.IsPrimary));
					prm.Add(DB.MakeInParam("@ContactIsActive", SqlDbType.Bit, 1, objContactInfo.IsActive));
					prm.Add(DB.MakeInParam("@ContactLanguageId", SqlDbType.Int, 4, objContactInfo.LanguageId));

					//prm.Add(DB.MakeInParam("@LocationID", SqlDbType.Int, 4, ObjOrg.LocationID));
					//prm.Add(DB.MakeInParam("@Description", SqlDbType.NVarChar, 100, ObjOrg.Description));
					//prm.Add(DB.MakeInParam("@Address", SqlDbType.NVarChar, 250, ObjOrg.Address));
					//prm.Add(DB.MakeInParam("@City", SqlDbType.NVarChar, 100, ObjOrg.City));
					//prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, ObjOrg.StateID));
					//prm.Add(DB.MakeInParam("@CountryID", SqlDbType.Int, 4, ObjOrg.CountryID));
					//prm.Add(DB.MakeInParam("@ZipPostalCode", SqlDbType.NVarChar, 100, ObjOrg.ZipPostalCode));
					//prm.Add(DB.MakeInParam("@AddressIsActive", SqlDbType.Bit, 1, ObjOrg.IsActive));
					//prm.Add(DB.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, ObjOrg.DateCreated));
					//prm.Add(DB.MakeInParam("@BillMailAddress", SqlDbType.NVarChar, 250, ObjOrg.BillMailAddress));
					//prm.Add(DB.MakeInParam("@OwnerManager", SqlDbType.NVarChar, 50, ObjOrg.OwnerManager));
					//prm.Add(DB.MakeInParam("@BilingContact", SqlDbType.NVarChar, 50, ObjOrg.BillingContact));
					//prm.Add(DB.MakeInParam("@Fax", SqlDbType.NVarChar, 20, ObjOrg.Fax));

					ObjOrg.OrganizationId = DB.RunProc("up_OrganizationInsertUpdateBasicInfo", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.SaveBasicInfo", ex);
			}
		}

		public static void SavePrimaryInfo(OrganizationInfo objOrg, ContactInfo objPrimaryContact, Phones objPrimaryContactBusinessPhone, 
            Phones objPrimaryContactCellPhone, ContactInfo objBillingContact, Phones objBillingContactBusinessPhone, Phones objBillingContactCellPhone, 
            Organization_Address objBusinessOrganization_Address, Organization_Address objMailingOrganization_Address, string catIds)
		{
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					#region Organization Info

					if (objOrg.OrganizationId > 0)
						prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, objOrg.OrganizationId));
					else
						prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, DBNull.Value));
					prm.Add(DB.MakeInParam("@LegalName", SqlDbType.NVarChar, 255, objOrg.LegalName));
					prm.Add(DB.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, objOrg.DBAName));
					prm.Add(DB.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, objOrg.OrganizationTypeId));
                    prm.Add(DB.MakeInParam("@OrganizationSubTypeId", SqlDbType.Int, 0, objOrg.OrganizationSubTypeID));
					prm.Add(DB.MakeInParam("@Website", SqlDbType.NVarChar, 255, objOrg.Website));
					prm.Add(DB.MakeInParam("@AccountingInterfaceId", SqlDbType.Int, 4, objOrg.AccountingInterfaceId));
					prm.Add(DB.MakeInParam("@InventoryInterfaceId", SqlDbType.Int, 4, objOrg.InventoryInterfaceId));
					prm.Add(DB.MakeInParam("@IsAutoFundTransfer", SqlDbType.Bit, 1, objOrg.IsAutoFundTransfer));
					prm.Add(DB.MakeInParam("@OrgIsActive", SqlDbType.Bit, 1, objOrg.IsActive));
					prm.Add(DB.MakeInParam("@OrgLanguageId", SqlDbType.Int, 4, objOrg.LanguageId));
					prm.Add(DB.MakeInParam("@RoleId", SqlDbType.Int, 4, objOrg.RoleId));

					#endregion

					#region Primary Contact Info

					prm.Add(DB.MakeInParam("@PrimaryContactTypeId", SqlDbType.Int, 4, objPrimaryContact.ContactTypeId));
					prm.Add(DB.MakeInParam("@PrimaryContactFirstName", SqlDbType.NVarChar, 255, objPrimaryContact.FirstName));
					prm.Add(DB.MakeInParam("@PrimaryContactLastName", SqlDbType.NVarChar, 255, objPrimaryContact.LastName));
					prm.Add(DB.MakeInParam("@PrimaryContactTitleId", SqlDbType.Int, 4, objPrimaryContact.ContactTitleId));
					prm.Add(DB.MakeInParam("@PrimaryContactEmail", SqlDbType.NVarChar, 200, objPrimaryContact.Email));
					prm.Add(DB.MakeInParam("@PrimaryContactIsActive", SqlDbType.Bit, 1, objPrimaryContact.IsActive));
					prm.Add(DB.MakeInParam("@PrimaryContactIsPrimary", SqlDbType.Bit, 1, objPrimaryContact.IsPrimary));
					prm.Add(DB.MakeInParam("@PrimaryContactLanguageId", SqlDbType.Int, 4, objPrimaryContact.LanguageId));

					#endregion

					#region Primary Contact Business Phone Info

					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneNumber", SqlDbType.NVarChar, 15, objPrimaryContactBusinessPhone.Number));
					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneExtension", SqlDbType.NVarChar, 7, objPrimaryContactBusinessPhone.Extension));
					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneTypeId", SqlDbType.Int, 4, objPrimaryContactBusinessPhone.PhoneTypeId));
					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneIsActive", SqlDbType.Bit, 1, objPrimaryContactBusinessPhone.IsActive));

					#endregion

					#region Primary Contact Cell Phone Info

					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneNumber", SqlDbType.NVarChar, 15, objPrimaryContactCellPhone.Number));
					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneIsAcceptTextMessages", SqlDbType.Bit, 1, objPrimaryContactCellPhone.IsAcceptTextMessages));
					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneTypeId", SqlDbType.Int, 4, objPrimaryContactCellPhone.PhoneTypeId));
					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneIsActive", SqlDbType.Bit, 1, objPrimaryContactCellPhone.IsActive));

					#endregion

					#region Billing Contact Info

					prm.Add(DB.MakeInParam("@BillingContactTypeId", SqlDbType.Int, 4, objBillingContact.ContactTypeId));
					prm.Add(DB.MakeInParam("@BillingContactFirstName", SqlDbType.NVarChar, 255, objBillingContact.FirstName));
					prm.Add(DB.MakeInParam("@BillingContactLastName", SqlDbType.NVarChar, 255, objBillingContact.LastName));
					prm.Add(DB.MakeInParam("@BillingContactTitleId", SqlDbType.Int, 4, objBillingContact.ContactTitleId));
					prm.Add(DB.MakeInParam("@BillingContactEmail", SqlDbType.NVarChar, 200, objBillingContact.Email));
					prm.Add(DB.MakeInParam("@BillingContactIsActive", SqlDbType.Bit, 1, objBillingContact.IsActive));
					prm.Add(DB.MakeInParam("@BillingContactIsPrimary", SqlDbType.Bit, 1, objBillingContact.IsPrimary));
					prm.Add(DB.MakeInParam("@BillingContactLanguageId", SqlDbType.Int, 4, objBillingContact.LanguageId));

					#endregion

					#region Billing Contact Business Phone Info

					prm.Add(DB.MakeInParam("@BillingContactBusinessPhoneNumber", SqlDbType.NVarChar, 15, objBillingContactBusinessPhone.Number));
					prm.Add(DB.MakeInParam("@BillingContactBusinessPhoneExtension", SqlDbType.NVarChar, 7, objBillingContactBusinessPhone.Extension));
					prm.Add(DB.MakeInParam("@BillingContactBusinessPhoneTypeId", SqlDbType.Int, 4, objBillingContactBusinessPhone.PhoneTypeId));
					prm.Add(DB.MakeInParam("@BillingContactBusinessPhoneIsActive", SqlDbType.Bit, 1, objBillingContactBusinessPhone.IsActive));

					#endregion

					#region Billing Contact Cell Phone Info

					prm.Add(DB.MakeInParam("@BillingContactCellPhoneNumber", SqlDbType.NVarChar, 15, objBillingContactCellPhone.Number));
					prm.Add(DB.MakeInParam("@BillingContactCellPhoneIsAcceptTextMessages", SqlDbType.Bit, 1, objBillingContactCellPhone.IsAcceptTextMessages));
					prm.Add(DB.MakeInParam("@BillingContactCellPhoneTypeId", SqlDbType.Int, 4, objBillingContactCellPhone.PhoneTypeId));
					prm.Add(DB.MakeInParam("@BillingContactCellPhoneIsActive", SqlDbType.Bit, 1, objBillingContactCellPhone.IsActive));

					#endregion

					#region Business Organization Address Info

					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressZipCodeId", SqlDbType.Int, 4, objBusinessOrganization_Address.ZipCodeID));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressZipCode", SqlDbType.NVarChar, 100, objBusinessOrganization_Address.ZipPostalCode));
					prm.Add(DB.MakeInParam("@BusinessOrganizationAddress1", SqlDbType.NVarChar, 250, objBusinessOrganization_Address.Address1));
					prm.Add(DB.MakeInParam("@BusinessOrganizationAddress2", SqlDbType.NVarChar, 250, objBusinessOrganization_Address.Address2));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressCity", SqlDbType.NVarChar, 100, objBusinessOrganization_Address.City));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressStateId", SqlDbType.Int, 4, objBusinessOrganization_Address.StateID));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressCountryID", SqlDbType.Int, 4, objBusinessOrganization_Address.CountryID));
                    prm.Add(DB.MakeInParam("@BusinessOrganization_CityId", SqlDbType.BigInt, 10, objBusinessOrganization_Address.CityId));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressIsActive", SqlDbType.Bit, 1, objBusinessOrganization_Address.IsActive));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressDateCreated", SqlDbType.DateTime, 8, objBusinessOrganization_Address.DateCreated));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressTypeId", SqlDbType.Int, 4, objBusinessOrganization_Address.Organization_AddressTypeId));

					#endregion

					#region Mailing Organization Address Info

					prm.Add(DB.MakeInParam("@MailingOrganization_AddressZipCodeId", SqlDbType.Int, 4, objMailingOrganization_Address.ZipCodeID));
					prm.Add(DB.MakeInParam("@MailingOrganization_AddressZipCode", SqlDbType.NVarChar, 100, objMailingOrganization_Address.ZipPostalCode));
					prm.Add(DB.MakeInParam("@MailingOrganizationAddress1", SqlDbType.NVarChar, 250, objMailingOrganization_Address.Address1));
					prm.Add(DB.MakeInParam("@MailingOrganizationAddress2", SqlDbType.NVarChar, 250, objMailingOrganization_Address.Address2));
					prm.Add(DB.MakeInParam("@MailingOrganization_AddressCity", SqlDbType.NVarChar, 100, objMailingOrganization_Address.City));
                    prm.Add(DB.MakeInParam("@MailingOrganization_CityId", SqlDbType.BigInt, 10, objMailingOrganization_Address.CityId));
					prm.Add(DB.MakeInParam("@MailingOrganization_AddressStateId", SqlDbType.Int, 4, objMailingOrganization_Address.StateID));
					prm.Add(DB.MakeInParam("@MailingOrganization_AddressCountryID", SqlDbType.Int, 4, objMailingOrganization_Address.CountryID));
					prm.Add(DB.MakeInParam("@MailingOrganization_AddressIsActive", SqlDbType.Bit, 1, objMailingOrganization_Address.IsActive));
					prm.Add(DB.MakeInParam("@MailingOrganization_AddressDateCreated", SqlDbType.DateTime, 8, objMailingOrganization_Address.DateCreated));
					prm.Add(DB.MakeInParam("@MailingOrganization_AddressTypeId", SqlDbType.Int, 4, objMailingOrganization_Address.Organization_AddressTypeId));

					#endregion

                    //For making application generalized
                    prm.Add(DB.MakeInParam("@CategoryIds", SqlDbType.VarChar, 50, catIds));

					objOrg.OrganizationId = DB.RunProc("up_OrganizationInsertUpdatePrimaryInfo", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.SaveBasicInfo", ex);
			}
		}

        public static DataTable GetCityStateAndCountryByZipCode(string Zipcode, int CountryID)
		{
			DataTable dt = null;
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();
					prm.Add(DB.MakeInParam("@Zipcode", SqlDbType.NVarChar, 20, Zipcode));
                    prm.Add(DB.MakeInParam("@CountryID", SqlDbType.Int, 0, CountryID));
					dt = DB.GetDataSet("up_GetCityStateAndCountryByZipCode", prm.ToArray()).Tables[0];
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizaionInfo.GetCityStateAndCountryByZipCode", ex);
			}

			return dt;
		}
        public static DataTable GetCityStateAndCountryByZipCode(string Zipcode, int CountryID,double stateid)
        {
            DataTable dt = null;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    List<SqlParameter> prm = new List<SqlParameter>();
                    prm.Add(DB.MakeInParam("@Zipcode", SqlDbType.NVarChar, 20, Zipcode));
                    prm.Add(DB.MakeInParam("@CountryID", SqlDbType.Int, 0, CountryID));
                    prm.Add(DB.MakeInParam("@stateId", SqlDbType.Int, 0, stateid));
                    dt = DB.GetDataSet("up_GetCityStateAndCountryByZipCodeAndStateID", prm.ToArray()).Tables[0];
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizaionInfo.GetCityStateAndCountryByZipCode", ex);
            }

            return dt;
        }
        public static DataTable GetCityStateAndCountryByZipCode(string Zipcode)
        {
            DataTable dt = null;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    List<SqlParameter> prm = new List<SqlParameter>();
                    prm.Add(DB.MakeInParam("@Zipcode", SqlDbType.NVarChar, 20, Zipcode));
                    prm.Add(DB.MakeInParam("@CountryID", SqlDbType.Int, 0, 235));
                    dt = DB.GetDataSet("up_GetCityStateAndCountryByZipCode", prm.ToArray()).Tables[0];
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizaionInfo.GetCityStateAndCountryByZipCode", ex);
            }

            return dt;
        }
		public static DataTable GetCountryByStateID(int StateId)
		{
			DataTable dt = null;
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();
					prm.Add(DB.MakeInParam("@StateId", SqlDbType.NVarChar, 20, StateId));
					dt = DB.GetDataSet("up_GetCountryByStateId", prm.ToArray()).Tables[0];
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizaionInfo.GetCountryByStateID", ex);
			}

			return dt;
		}
        /// <summary>
        /// used to update status of organization.
        /// </summary>
        /// <param name="orgStatus"></param>
        /// <param name="organizationID"></param>
        public static void SetStatus(OrganizationStatus orgStatus,int organizationID,string vchNotes,int organizationTypeId,string standardStewardshipIds)
        {
            try 
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    List<SqlParameter> prm = new List<SqlParameter>();
                    prm.Add(DB.MakeInParam("@StatusId", SqlDbType.Int, 4, Convert.ToInt16(orgStatus)));
                    prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 10, Convert.ToInt16(organizationID)));
                    prm.Add(DB.MakeInParam("@intOrganizationTypeId", SqlDbType.Int, 5, Convert.ToInt16(organizationTypeId)));
                    prm.Add(DB.MakeInParam("@vchNotes", SqlDbType.VarChar, 2000, Convert.ToString(vchNotes)));
                    prm.Add(DB.MakeInParam("@StandardStewardshipIds", SqlDbType.VarChar, 2000, Convert.ToString(standardStewardshipIds)));
                    
                    DB.RunProc("Up_Organization_UpdateStatus", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.SetStatus", ex);
            }
        }
		public static void SaveCertifications(OrganizationInfo ObjOrg)
		{
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();
					prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, ObjOrg.OrganizationId));

					prm.Add(DB.MakeInParam("@CertificationIDs", SqlDbType.NVarChar, -1, string.Join(",", ObjOrg.CertificationID.ToArray())));

					DB.RunProc("Up_Organization_Certification_Insert", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.SaveCertifications", ex);
			}
		}

		public static int SaveSupplierByOrganizationId(int OrganizationId, Supplier objSupplier)
		{
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));
					prm.Add(DB.MakeInParam("@Supplierid", SqlDbType.Int, 4, objSupplier.SupplierID));
					prm.Add(DB.MakeInParam("@CompanyName", SqlDbType.NVarChar, 255, objSupplier.CompanyName));
					prm.Add(DB.MakeInParam("@CountryID", SqlDbType.Int, 4, objSupplier.CountryID));
					prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, objSupplier.StateId));
					prm.Add(DB.MakeInParam("@City", SqlDbType.VarChar, 100, objSupplier.City));
					prm.Add(DB.MakeInParam("@ZipcodeId", SqlDbType.Int, 4, objSupplier.ZipCodeId));
					prm.Add(DB.MakeInParam("@ContactName", SqlDbType.NVarChar, 100, objSupplier.ContactName));
					prm.Add(DB.MakeInParam("@BussinessPhone", SqlDbType.NVarChar, 100, objSupplier.BussinessPhone));
					prm.Add(DB.MakeInParam("@OwnerManagerEmail", SqlDbType.NVarChar, 100, objSupplier.Email));
					prm.Add(DB.MakeInParam("@IsActive", SqlDbType.Bit, 1, objSupplier.IsActive));
					prm.Add(DB.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, objSupplier.DateCreated));
					prm.Add(DB.MakeInParam("@CreatedBy", SqlDbType.Int, 4, objSupplier.CreatedBy));
					prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, objSupplier.LanguageID));
					prm.Add(DB.MakeInParam("@Email", SqlDbType.NVarChar, 20, objSupplier.Email)); 
					prm.Add(DB.MakeInParam("@Count", SqlDbType.Float, 4, objSupplier.Count));
                    prm.Add(DB.MakeInParam("@BusinessPhoneExtention", SqlDbType.NVarChar, 100, objSupplier.BusinessPhoneExtention));
                    prm.Add(DB.MakeInParam("@cellPhone", SqlDbType.NVarChar, 100, objSupplier.CellPhone));
                 return   objSupplier.SupplierID =DB.RunProc("Up_Organization_Suplier_Insert", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.SaveSupplierByOrganizationId", ex);
                return 0;
			}
		}


        public static DataSet editRegistartion(int OrganizationId)
        {
            DataSet ds = null;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    List<SqlParameter> prm = new List<SqlParameter>();

                    prm.Add(DB.MakeInParam("@orgid", SqlDbType.Int, 4, OrganizationId));

                    ds = DB.GetDataSet("up_editRegistrationInfo", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.editRegistartion", ex);
            }

            return ds;
        }


		public static DataSet GetSupplierByOrganizationId(int OrganizationId)
		{
			DataSet ds = null;
			try
			{
				using(DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));

					ds = DB.GetDataSet("Up_GetSupplierByOrganizationId", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetSupplierByOrganizationId", ex);
			}

			return ds;
		}

		public static void SaveClientByOrganizationId(int OrganizationId, Client objClient)
		{
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));
					prm.Add(DB.MakeInParam("@ClientId", SqlDbType.Int, 4, objClient.ClientID));
					prm.Add(DB.MakeInParam("@CompanyName", SqlDbType.NVarChar, 255, objClient.CompanyName));
					prm.Add(DB.MakeInParam("@CountryID", SqlDbType.Int, 4, objClient.CountryID));
					prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, objClient.StateId));
					prm.Add(DB.MakeInParam("@City", SqlDbType.VarChar, 100, objClient.City));
					prm.Add(DB.MakeInParam("@ZipcodeID", SqlDbType.Int, 4, objClient.ZipCodeId));
					prm.Add(DB.MakeInParam("@ContactName", SqlDbType.NVarChar, 100, objClient.ContactName));
					prm.Add(DB.MakeInParam("@BussinessPhone", SqlDbType.NVarChar, 100, objClient.BussinessPhone));
					prm.Add(DB.MakeInParam("@OwnerManagerEmail", SqlDbType.NVarChar, 100, objClient.OwnerManagerEmail));
					prm.Add(DB.MakeInParam("@IsActive", SqlDbType.Bit, 1, objClient.IsActive));
					prm.Add(DB.MakeInParam("@CreatedBy", SqlDbType.Int, 4, objClient.CreatedBy));
					prm.Add(DB.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, objClient.DateCreated));
					prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 4, objClient.LanguageId));

					DB.RunProc("Up_Organization_Client_Insert", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganziationInfo.SaveClientByOrganizationId", ex);
			}
		}

		public static DataSet GetClientByOrganizationId(int OrganizationId)
		{
			DataSet ds = null;
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));

					ds = DB.GetDataSet("Up_GetClientByOrganizationId", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetClientByOrganizationId", ex);
			}

			return ds;
		}

		public static void DeleteSupplierBySupplierId(int SupplierId)
		{
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					prm.Add(DB.MakeInParam("@SupplierId", SqlDbType.Int, 4, SupplierId));

					DB.RunProc("up_DeleteSupplierBySupplierId", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.DeleteSupplierBySupplierId", ex);
			}
		}

		public static void DeleteClientByClientId(int ClientId)
		{
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					prm.Add(DB.MakeInParam("@ClientId", SqlDbType.Int, 4, ClientId));

					DB.RunProc("up_DeleteClientByClientId", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.DeleteClientByClientId", ex);
			}
		}

        //public static bool SubmitApplication(int OrganizationId, int RoleId, int OrganizationTypeId, List<Organization_Business> lstOrganizationBusiness, int StewardshipId)
        //{
        //    bool returnFlag = true;
        //    try
        //    {
        //        string BusinessIDs = "";
        //        string BusinessisNewString = "";

        //        foreach (Organization_Business item in lstOrganizationBusiness)
        //        {
        //            BusinessIDs += Convert.ToString(item.BusinessID) + ",";
        //            BusinessisNewString += Convert.ToString(Convert.ToInt32(item.IsNew)) + ",";
        //        }

        //        using (DbManager DB = DbManager.GetDbManager())
        //        {
        //            List<SqlParameter> prm = new List<SqlParameter>();

        //            prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));
        //            prm.Add(DB.MakeInParam("@BusinessIDString", SqlDbType.VarChar, 500, BusinessIDs.TrimEnd(',')));
        //            prm.Add(DB.MakeInParam("@BusinessIsNewString", SqlDbType.VarChar, 500, BusinessisNewString.TrimEnd(',')));
        //            prm.Add(DB.MakeInParam("@RoleId", SqlDbType.Int, 4, RoleId));
        //            prm.Add(DB.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, OrganizationTypeId));
        //            prm.Add(DB.MakeInParam("@StewardshipId", SqlDbType.Int, 4 ,StewardshipId));

        //            DB.RunProc("up_Organization_ApplicationSubmit", prm.ToArray());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        returnFlag = false;
        //        new SqlLog().InsertSqlLog(0, "OrganizationInfo.SubmitApplication", ex);
        //    }
        //    return returnFlag;
        //}
        public static bool SubmitApplication(int OrganizationId, int OrganizationTypeId, List<Organization_Business> lstOrganizationBusiness, int StewardshipId,string standardstewardshipIds)
        {
            bool returnFlag = true;
            try
            {
                //string BusinessIDs = "";
                //string BusinessisNewString = "";

                //foreach (Organization_Business item in lstOrganizationBusiness)
                //{
                //    BusinessIDs += Convert.ToString(item.BusinessID) + ",";
                //    BusinessisNewString += Convert.ToString(Convert.ToInt32(item.IsNew)) + ",";
                //}

                using (DbManager DB = DbManager.GetDbManager())
                {
                    List<SqlParameter> prm = new List<SqlParameter>();

                    prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));
                    prm.Add(DB.MakeInParam("@BusinessIDString", SqlDbType.VarChar, 500, null));
                    prm.Add(DB.MakeInParam("@BusinessIsNewString", SqlDbType.VarChar, 500, null));
                    //prm.Add(DB.MakeInParam("@RoleId", SqlDbType.Int, 4, RoleId));
                    prm.Add(DB.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, OrganizationTypeId));
                    prm.Add(DB.MakeInParam("@StewardshipId", SqlDbType.Int, 4, StewardshipId));
                    prm.Add(DB.MakeInParam("@StandardStewardshipIds", SqlDbType.VarChar, 1000, standardstewardshipIds));
                    DB.RunProc("up_Organization_ApplicationSubmit", prm.ToArray());
                }

            }
            catch (Exception ex)
            {
                returnFlag = false;
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.SubmitApplication", ex);
            }
            return returnFlag;
        }
		public static DataSet getStakeholderByStateId(int pageId, int pageSize, int StateId, out int iTotalrows)
		{
			DataSet ds = null;
			iTotalrows = 0;
			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
					prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));
					prams.Add(db.MakeInParam("@stateid", SqlDbType.Int, 0, StateId));

					prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
					ds = db.GetDataSet("up_Stakeholder_getByStateId", prams.ToArray());
					if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
						return ds;
					}
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.getStakeholderByStateId", exp);
			}
			return ds;
		}

		public static DataSet getOrganizationsByIdAndType(int pageId, int pageSize, int OrganizationId, int OrganizationTypeId, bool IsApproved, string OrganizationName, out int iTotalrows)
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
					prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));
					prams.Add(db.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, OrganizationTypeId));
					prams.Add(db.MakeInParam("@IsApproved", SqlDbType.Int, 4, IsApproved));

					if (OrganizationName == "")
						prams.Add(db.MakeInParam("@OrganizationName", SqlDbType.NVarChar, 255, DBNull.Value));
					else
						prams.Add(db.MakeInParam("@OrganizationName", SqlDbType.NVarChar, 255, OrganizationName));

					prams.Add(db.MakeReturnParam(SqlDbType.Int, 4));
					ds = db.GetDataSet("up_Organization_getByIdAndType", prams.ToArray());
					if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
						return ds;
					}
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.getOrganizationsByIdAndType", exp);
			}
			return ds;
		}

		public static DataSet getAllStakeholder(int pageId, int pageSize, out int iTotalrows)
		{
			DataSet ds = null;
			iTotalrows = 0;
			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
					prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));
                  

					prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
					ds = db.GetDataSet("up_Stakeholders_All", prams.ToArray());
					if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
						return ds;
					}
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.getAllStakeholder", exp);
			}
			return ds;
		}

		public static DataSet getAllNotApprovedStakeholder(int pageId, int pageSize, int StateId, out int iTotalrows)
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
					if(StateId == 0)
						prams.Add(db.MakeInParam("@StateId", SqlDbType.Int, 4, DBNull.Value));
					else
						prams.Add(db.MakeInParam("@StateId", SqlDbType.Int, 4, StateId));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_Stakeholder_allNotApproved", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getAllNotApprovedStakeholder", exp);
            }
            return ds;
        }

		public static DataSet getAllApprovedStakeholder(int pageId, int pageSize, out int iTotalrows)
		{
			DataSet ds = null;
			iTotalrows = 0;
			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
					prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));


					prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
					ds = db.GetDataSet("up_Stakeholder_allApproved", prams.ToArray());
					if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
						return ds;
					}
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.getAllApprovedStakeholder", exp);
			}
			return ds;
		}
        public static DataSet getAllStakeholderNotApprovedClient(int pageId, int pageSize, out int iTotalrows)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 0, pageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));


                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_Stakholder_AllClientNotApproved", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getAllStakeholderNotApprovedClient", exp);
            }
            return ds;
        }

		public static void ApprovedStakeholderInActive(int stakeholderapprovalid)
		{
			DataSet ds = null;

			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					prams.Add(db.MakeInParam("@stakeholderApprovalId", SqlDbType.Int, 0, stakeholderapprovalid));
					ds = db.GetDataSet("up_ApprovedStakeholder_InActive", prams.ToArray());
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.ApprovedStakeholderInActive", exp);
			}

		}

		public static void InApprovedStakeholderInActive(int stakeholderapprovalid)
		{
			DataSet ds = null;

			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					prams.Add(db.MakeInParam("@stakeholderApprovalId", SqlDbType.Int, 0, stakeholderapprovalid));
					ds = db.GetDataSet("up_ApprovedStakeholder_InActive", prams.ToArray());
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.InApprovedStakeholderInActive", exp);
			}

		}

		public static DataSet GetStewardshipByCountryID(int ConutryID)
		{
			DataSet ds = null;
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					var prams = new System.Data.SqlClient.SqlParameter[1];
					if(ConutryID <= 0)
						prams[0] = db.MakeInParam("@CountryId", SqlDbType.Int, 4, DBNull.Value);
					else
						prams[0] = db.MakeInParam("@CountryId", SqlDbType.Int, 4, ConutryID);
                    ds = db.GetDataSet("up_Organization_GetStewardshipByCountryID", prams);
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "", ex);
			}
			return ds;
		}

		public static void ApprovedStakeholderByUser(int stakeholderapprovalid,int modifiedby,DateTime datemodified)
		{
			DataSet ds = null;

			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					prams.Add(db.MakeInParam("@datemodified", SqlDbType.DateTime, 0, datemodified));
					prams.Add(db.MakeInParam("@modifiedby", SqlDbType.Int, 0,modifiedby));
					prams.Add(db.MakeInParam("@stakeholderapprovalId", SqlDbType.Int, 0,stakeholderapprovalid));


					ds = db.GetDataSet("up_User_ApprovedApplication", prams.ToArray());
					if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{

					}
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.ApprovedStakeholderByUser", exp);
			}

		}

		public static void ApprovedStakeholderByAdmin(int stakeholderapprovalid, int adminId, DateTime adminapprovaldate)
		{
			DataSet ds = null;

			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{ 


					prams.Add(db.MakeInParam("@stakeholderapprovalId", SqlDbType.Int, 0, stakeholderapprovalid));
					prams.Add(db.MakeInParam("@adminId", SqlDbType.Int, 0, adminId));
					prams.Add(db.MakeInParam("@adminapprovaldate", SqlDbType.DateTime, 0, adminapprovaldate));
					ds = db.GetDataSet("up_Stakeholder_ApproveByAdmin", prams.ToArray());
					if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{

					}
				}
			} 
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.ApprovedStakeholderByAdmin", exp);
			}

		}

		public static void SaveAdditionalLocationInfo(OrganizationInfo objOrg, ContactInfo objPrimaryContact, Phones objPrimaryContactBusinessPhone, Phones objPrimaryContactCellPhone, OrganizationInfo.Organization_Address objBusinessOrganization_Address)
		{
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					#region Organization Info

					prm.Add(DB.MakeInParam("@LegalName", SqlDbType.NVarChar, 255, objOrg.LegalName));
					prm.Add(DB.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, objOrg.DBAName));
					prm.Add(DB.MakeInParam("@IsLocationEventPermanent", SqlDbType.Bit, 1, objOrg.IsLocationEventPermanent));

					if (objOrg.IsLocationEventPermanent == false)
					{
						prm.Add(DB.MakeInParam("@LocationEventStartDate", SqlDbType.DateTime, 8, objOrg.LocationEventStartDate));
						prm.Add(DB.MakeInParam("@LocationEventEndDate", SqlDbType.DateTime, 8, objOrg.LocationEventEndDate));
						prm.Add(DB.MakeInParam("@LocationEventTypeId", SqlDbType.Int, 4, objOrg.LocationEventTypeId));
					}
					else
					{
						prm.Add(DB.MakeInParam("@LocationEventStartDate", SqlDbType.DateTime, 8, DBNull.Value));
						prm.Add(DB.MakeInParam("@LocationEventEndDate", SqlDbType.DateTime, 8, DBNull.Value));
						prm.Add(DB.MakeInParam("@LocationEventTypeId", SqlDbType.Int, 4, DBNull.Value));
					}

					prm.Add(DB.MakeInParam("@LocationPermitNumber", SqlDbType.NVarChar, 255, objOrg.LocationPermitNumber));
					prm.Add(DB.MakeInParam("@OrgIsActive", SqlDbType.Bit, 1, objOrg.IsActive));
					prm.Add(DB.MakeInParam("@OrgLanguageId", SqlDbType.Int, 4, objOrg.LanguageId));
					prm.Add(DB.MakeInParam("@ParentId", SqlDbType.Int, 4, objOrg.ParentId));

					#endregion

					#region Primary Contact Info

					prm.Add(DB.MakeInParam("@PrimaryContactTypeId", SqlDbType.Int, 4, objPrimaryContact.ContactTypeId));
					prm.Add(DB.MakeInParam("@PrimaryContactFirstName", SqlDbType.NVarChar, 255, objPrimaryContact.FirstName));
					prm.Add(DB.MakeInParam("@PrimaryContactLastName", SqlDbType.NVarChar, 255, objPrimaryContact.LastName));
					prm.Add(DB.MakeInParam("@PrimaryContactTitleId", SqlDbType.Int, 4, objPrimaryContact.ContactTitleId));
					prm.Add(DB.MakeInParam("@PrimaryContactEmail", SqlDbType.NVarChar, 200, objPrimaryContact.Email));
					prm.Add(DB.MakeInParam("@PrimaryContactIsActive", SqlDbType.Bit, 1, objPrimaryContact.IsActive));
					prm.Add(DB.MakeInParam("@PrimaryContactIsPrimary", SqlDbType.Bit, 1, objPrimaryContact.IsPrimary));
					prm.Add(DB.MakeInParam("@PrimaryContactLanguageId", SqlDbType.Int, 4, objPrimaryContact.LanguageId));

					#endregion

					#region Primary Contact Business Phone Info

					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneNumber", SqlDbType.NVarChar, 15, objPrimaryContactBusinessPhone.Number));
					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneExtension", SqlDbType.NVarChar, 7, objPrimaryContactBusinessPhone.Extension));
					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneTypeId", SqlDbType.Int, 4, objPrimaryContactBusinessPhone.PhoneTypeId));
					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneIsActive", SqlDbType.Bit, 1, objPrimaryContactBusinessPhone.IsActive));

					#endregion

					#region Primary Contact Cell Phone Info

					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneNumber", SqlDbType.NVarChar, 15, objPrimaryContactCellPhone.Number));
					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneIsAcceptTextMessages", SqlDbType.Bit, 1, objPrimaryContactCellPhone.IsAcceptTextMessages));
					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneTypeId", SqlDbType.Int, 4, objPrimaryContactCellPhone.PhoneTypeId));
					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneIsActive", SqlDbType.Bit, 1, objPrimaryContactCellPhone.IsActive));

					#endregion

					#region Business Organization Address Info

					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressZipCodeId", SqlDbType.Int, 4, objBusinessOrganization_Address.ZipCodeID));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressZipCode", SqlDbType.NVarChar, 100, objBusinessOrganization_Address.ZipPostalCode));
					prm.Add(DB.MakeInParam("@BusinessOrganizationAddress1", SqlDbType.NVarChar, 250, objBusinessOrganization_Address.Address1));
					prm.Add(DB.MakeInParam("@BusinessOrganizationAddress2", SqlDbType.NVarChar, 250, objBusinessOrganization_Address.Address2));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressCity", SqlDbType.NVarChar, 100, objBusinessOrganization_Address.City));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressStateId", SqlDbType.Int, 4, objBusinessOrganization_Address.StateID));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressCountryID", SqlDbType.Int, 4, objBusinessOrganization_Address.CountryID));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressIsActive", SqlDbType.Bit, 1, objBusinessOrganization_Address.IsActive));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressDateCreated", SqlDbType.DateTime, 8, objBusinessOrganization_Address.DateCreated));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressTypeId", SqlDbType.Int, 4, objBusinessOrganization_Address.Organization_AddressTypeId));

					#endregion

					objOrg.OrganizationId = DB.RunProc("up_OrganizationAdditionalLocationAdd", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.SaveAdditionalLocationInfo", ex);
			}
		}

		public static DataSet GetAdditionalLocationsByOrganizationId(int OrganizationId)
		{
			DataSet ds = null;
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));

					ds = DB.GetDataSet("Up_GetAdditionalLocationsByOrganizationId", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetAdditionalLocationsByOrganizationId", ex);
			}

			return ds;
		}

		public static void DeleteAdditionalLocationByOrganizationId(int OrganizationId)
		{
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));

					DB.RunProc("up_DeleteAdditionalLocationByOrganizationId", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.DeleteAdditionalLocationByOrganizationId", ex);
			}
		}

		public static void StewardshipAdd(OrganizationInfo objOrg, ContactInfo objPrimaryContact, Phones objPrimaryContactBusinessPhone, Phones objPrimaryContactCellPhone, Organization_Address objBusinessOrganization_Address)
		{
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();

					#region Organization Info

					prm.Add(DB.MakeInParam("@LegalName", SqlDbType.NVarChar, 255, objOrg.LegalName));
					prm.Add(DB.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, objOrg.DBAName));
					prm.Add(DB.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, objOrg.OrganizationTypeId));
					prm.Add(DB.MakeInParam("@OrgIsActive", SqlDbType.Bit, 1, objOrg.IsActive));
					prm.Add(DB.MakeInParam("@OrgLanguageId", SqlDbType.Int, 4, objOrg.LanguageId));
					prm.Add(DB.MakeInParam("@RoleId", SqlDbType.Int, 4, objOrg.RoleId));

					#endregion

					#region Primary Contact Info

					prm.Add(DB.MakeInParam("@PrimaryContactTypeId", SqlDbType.Int, 4, objPrimaryContact.ContactTypeId));
					prm.Add(DB.MakeInParam("@PrimaryContactFirstName", SqlDbType.NVarChar, 255, objPrimaryContact.FirstName));
					prm.Add(DB.MakeInParam("@PrimaryContactLastName", SqlDbType.NVarChar, 255, objPrimaryContact.LastName));
					prm.Add(DB.MakeInParam("@PrimaryContactTitleId", SqlDbType.Int, 4, objPrimaryContact.ContactTitleId));
					prm.Add(DB.MakeInParam("@PrimaryContactEmail", SqlDbType.NVarChar, 200, objPrimaryContact.Email));
					prm.Add(DB.MakeInParam("@PrimaryContactIsActive", SqlDbType.Bit, 1, objPrimaryContact.IsActive));
					prm.Add(DB.MakeInParam("@PrimaryContactIsPrimary", SqlDbType.Bit, 1, objPrimaryContact.IsPrimary));
					prm.Add(DB.MakeInParam("@PrimaryContactLanguageId", SqlDbType.Int, 4, objPrimaryContact.LanguageId));

					#endregion

					#region Primary Contact Business Phone Info

					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneNumber", SqlDbType.NVarChar, 15, objPrimaryContactBusinessPhone.Number));
					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneExtension", SqlDbType.NVarChar, 7, objPrimaryContactBusinessPhone.Extension));
					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneTypeId", SqlDbType.Int, 4, objPrimaryContactBusinessPhone.PhoneTypeId));
					prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneIsActive", SqlDbType.Bit, 1, objPrimaryContactBusinessPhone.IsActive));

					#endregion

					#region Primary Contact Cell Phone Info

					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneNumber", SqlDbType.NVarChar, 15, objPrimaryContactCellPhone.Number));
					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneIsAcceptTextMessages", SqlDbType.Bit, 1, objPrimaryContactCellPhone.IsAcceptTextMessages));
					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneTypeId", SqlDbType.Int, 4, objPrimaryContactCellPhone.PhoneTypeId));
					prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneIsActive", SqlDbType.Bit, 1, objPrimaryContactCellPhone.IsActive));

					#endregion

					#region Business Organization Address Info

					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressZipCodeId", SqlDbType.Int, 4, objBusinessOrganization_Address.ZipCodeID));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressZipCode", SqlDbType.NVarChar, 100, objBusinessOrganization_Address.ZipPostalCode));
					prm.Add(DB.MakeInParam("@BusinessOrganizationAddress1", SqlDbType.NVarChar, 250, objBusinessOrganization_Address.Address1));
					prm.Add(DB.MakeInParam("@BusinessOrganizationAddress2", SqlDbType.NVarChar, 250, objBusinessOrganization_Address.Address2));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressCity", SqlDbType.NVarChar, 100, objBusinessOrganization_Address.City));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressStateId", SqlDbType.Int, 4, objBusinessOrganization_Address.StateID));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressCountryID", SqlDbType.Int, 4, objBusinessOrganization_Address.CountryID));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressIsActive", SqlDbType.Bit, 1, objBusinessOrganization_Address.IsActive));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressDateCreated", SqlDbType.DateTime, 8, objBusinessOrganization_Address.DateCreated));
					prm.Add(DB.MakeInParam("@BusinessOrganization_AddressTypeId", SqlDbType.Int, 4, objBusinessOrganization_Address.Organization_AddressTypeId));

					#endregion

					objOrg.OrganizationId = DB.RunProc("up_Organization_Steward_Add", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.StewardshipAdd", ex);
			}
		}


        public static DataSet getViewStakeholderForApproval(int organizationId)
        {
            DataSet ds = null;
              
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@organizationId", SqlDbType.Int, 0, organizationId));

                    ds = db.GetDataSet("up_Stewardship_getByOrganizationId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getViewStakeholderForApproval", exp);
            }
            return ds;
        }


		public static DataSet GetAllStewardships()
		{
			DataSet ds = null;

			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					ds = db.GetDataSet("up_Organization_GetAllStewardships");
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetAllStewardships", exp);
			}
			return ds;
		}



        public static DataSet getPendingApplicationByOrgIdAndLangId(int PageId, int PageSize, out int total, int OrganizationTypeId, int LanguageId, string OrganizationName, string DBAName, string ContactName, DateTime CreatedFromDate, DateTime CreatedToDate)
        {
            DataSet ds = null;
         total = 0;
    List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.Int, 4, PageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.Int, 4, PageSize));
                    prams.Add(db.MakeInParam("@intorgnazationtypeid", SqlDbType.Int, 4, OrganizationTypeId));
                    if (OrganizationName == "")
                        prams.Add(db.MakeInParam("@OrganizationName", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@OrganizationName", SqlDbType.NVarChar, 255, OrganizationName));

                    if (DBAName == "")
                        prams.Add(db.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, DBAName));

                    if (ContactName == "")
                        prams.Add(db.MakeInParam("@ContactName", SqlDbType.NVarChar, 30, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@ContactName", SqlDbType.NVarChar, 30, ContactName));

                   

                    if (CreatedFromDate == DateTime.MinValue)
                        prams.Add(db.MakeInParam("@CreatedFromDate", SqlDbType.Date, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CreatedFromDate", SqlDbType.Date, 8, CreatedFromDate.Date));

                    if (CreatedToDate == DateTime.MinValue)
                        prams.Add(db.MakeInParam("@CreatedToDate", SqlDbType.Date, 8, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@CreatedToDate", SqlDbType.Date, 8, CreatedToDate.Date));
                    prams.Add(db.MakeInParam("@intlanguageID", SqlDbType.Int, 4, LanguageId));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 4));
                    //   prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_getAllPendingApplication", prams.ToArray());




                    total = (int)prams.Last<SqlParameter>().Value;
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getInfoByOrganizationTypeAndLangId", exp);
            }
            return ds;


        }
        public static DataSet getInfoByOrganizationTypeAndLangId(string OrganizationTypeId,int LanguageId)
        {
            DataSet ds = null;
            //  iTotalrows = 0;
    List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intorgnazationtypeid", SqlDbType.VarChar, 2000, OrganizationTypeId));
                    prams.Add(db.MakeInParam("@intlanguageID", SqlDbType.Int, 4, LanguageId));
                    //   prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_organization_getByorgTypeIdandLanId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getInfoByOrganizationTypeAndLangId", exp);
            }
            return ds;


        }

        public static DataSet getStakeholderbyTypeAndLangId(string OrganizationTypeId, int LanguageId)
        {
            DataSet ds = null;
            //  iTotalrows = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intorgnazationtypeid", SqlDbType.VarChar, 2000, OrganizationTypeId));
                    prams.Add(db.MakeInParam("@intlanguageID", SqlDbType.Int, 4, LanguageId));
                    //   prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    //   prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_Stakeholder_getbyLanguageId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getStakeholderTypeAndLangId", exp);
            }
            return ds;


        }

		public static void ApprovedStewardshipByAdmin(int StewardshipApprovalId, int UserId, DateTime ApprovalDate)
		{
			List<SqlParameter> prams = new List<SqlParameter>();
			try
			{
				using (DbManager db = DbManager.GetDbManager())
				{
					prams.Add(db.MakeInParam("@StewardshipApprovalId", SqlDbType.Int, 4, StewardshipApprovalId));
					prams.Add(db.MakeInParam("@UserId", SqlDbType.Int, 4, UserId));
					prams.Add(db.MakeInParam("@ApprovalDate", SqlDbType.DateTime, 0, ApprovalDate));
					db.RunProc("up_Stewardship_ApproveByAdmin", prams.ToArray());
				}
			}
			catch (Exception exp)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.ApprovedStewardshipByAdmin", exp);
			}
		}

		public static int CheckStateAvailableForStewarship(int StateId)
		{
			int ID = -1;
			List<SqlParameter> prm = new List<SqlParameter>();
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					prm.Add(DB.MakeInParam("@StateId", SqlDbType.Int, 4, StateId));
					ID = DB.ExecuteScalar<int>("up_CheckStateAvailableForStewarship", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.CheckStateAvailableForStewarship", ex);
			}
			return ID;
		}

		public static DataSet GetApprovedStakeholdersByStewardshipId(int StewardshipId)
		{
			DataSet ds = null;
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					List<SqlParameter> prm = new List<SqlParameter>();
					prm.Add(DB.MakeInParam("@StewardshipId", SqlDbType.Int, 4, StewardshipId));
					ds = DB.GetDataSet("up_GetApprovedStakeholdersByStewardshipId", prm.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "GetApprovedStakeholdersByStewardshipId", ex);
			}
			return ds;
		}

		public static DataSet SearchStakeholdersByCriteria(int PageId, int PageSize, out int total, int OrganizationId, int OrganizationTypeId, bool IsApproved, string OrganizationName, string DBAName, string ContactName, string ZipCode, DateTime CreatedFromDate, DateTime CreatedToDate, int LangaugeId,int intStatusid, string email)
		{
			DataSet ds = null;
           
			total = 0;
			List<SqlParameter> prm = new List<SqlParameter>();
			try
			{
				using (DbManager DB = DbManager.GetDbManager())
				{
					prm.Add(DB.MakeInParam("@intPageId", SqlDbType.Int, 4, PageId));
                    prm.Add(DB.MakeInParam("@intStatus", SqlDbType.Int, 4, intStatusid));
					prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 4, PageSize));
					prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));
					if(OrganizationTypeId == 0)
						prm.Add(DB.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, OrganizationTypeId));
					prm.Add(DB.MakeInParam("@IsApproved", SqlDbType.Bit, 1, IsApproved));

					if (OrganizationName == "")
						prm.Add(DB.MakeInParam("@OrganizationName", SqlDbType.NVarChar, 255, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@OrganizationName", SqlDbType.NVarChar, 255, OrganizationName));

					if (DBAName == "")
						prm.Add(DB.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, DBAName));

					if (ContactName == "")
						prm.Add(DB.MakeInParam("@ContactName", SqlDbType.NVarChar, 30, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@ContactName", SqlDbType.NVarChar, 30, ContactName));

					if (ZipCode == "")
						prm.Add(DB.MakeInParam("@ZipCode", SqlDbType.NVarChar, 20, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@ZipCode", SqlDbType.NVarChar, 20, ZipCode));
                    if (email == "")
                        prm.Add(DB.MakeInParam("@Email", SqlDbType.NVarChar, 20, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@Email", SqlDbType.NVarChar, 20, email));

					if (CreatedFromDate == DateTime.MinValue)
						prm.Add(DB.MakeInParam("@CreatedFromDate", SqlDbType.Date, 8, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@CreatedFromDate", SqlDbType.Date, 8, CreatedFromDate.Date));

					if (CreatedToDate == DateTime.MinValue)
						prm.Add(DB.MakeInParam("@CreatedToDate", SqlDbType.Date, 8, DBNull.Value));
					else
						prm.Add(DB.MakeInParam("@CreatedToDate", SqlDbType.Date, 8, CreatedToDate.Date));
;
					prm.Add(DB.MakeInParam("@LanguageID", SqlDbType.Int, 4, LangaugeId));
					prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

					ds = DB.GetDataSet("up_SearchStakeholdersByCriteria", prm.ToArray());

					total = (int)prm.Last<SqlParameter>().Value;

				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.SearchStewardshipByCriteria", ex);
			}
			return ds;
		}

        public static DataSet SearchStakeholdersByStewardShip(int PageId, int PageSize, out int total, int OrganizationId, int OrganizationTypeId, bool IsApproved, string OrganizationName, string DBAName, string ContactName, string ZipCode, DateTime CreatedFromDate, DateTime CreatedToDate, int LangaugeId)
        {
            DataSet ds = null;
            total = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intPageId", SqlDbType.Int, 4, PageId));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 4, PageSize));
                    prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));
                    if (OrganizationTypeId == 0)
                        prm.Add(DB.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, OrganizationTypeId));
                    prm.Add(DB.MakeInParam("@IsApproved", SqlDbType.Bit, 1, IsApproved));

                    if (OrganizationName == "")
                        prm.Add(DB.MakeInParam("@OrganizationName", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@OrganizationName", SqlDbType.NVarChar, 255, OrganizationName));

                    if (DBAName == "")
                        prm.Add(DB.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, DBAName));

                    if (ContactName == "")
                        prm.Add(DB.MakeInParam("@ContactName", SqlDbType.NVarChar, 30, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@ContactName", SqlDbType.NVarChar, 30, ContactName));

                    if (ZipCode == "")
                        prm.Add(DB.MakeInParam("@ZipCode", SqlDbType.NVarChar, 20, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@ZipCode", SqlDbType.NVarChar, 20, ZipCode));

                    if (CreatedFromDate == DateTime.MinValue)
                        prm.Add(DB.MakeInParam("@CreatedFromDate", SqlDbType.Date, 8, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@CreatedFromDate", SqlDbType.Date, 8, CreatedFromDate.Date));

                    if (CreatedToDate == DateTime.MinValue)
                        prm.Add(DB.MakeInParam("@CreatedToDate", SqlDbType.Date, 8, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@CreatedToDate", SqlDbType.Date, 8, CreatedToDate.Date));
                    ;
                    prm.Add(DB.MakeInParam("@LanguageID", SqlDbType.Int, 4, LangaugeId));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_SearchStakeholdersByStewardShip", prm.ToArray());

                    total = (int)prm.Last<SqlParameter>().Value;

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.SearchStakeholdersByStewardShip", ex);
            }
            return ds;
        }

		public static DataSet GetCountryAndStateCodeByOrganizationId(int OrganizationId)
		{
			DataSet ds = null;
			try
			{
				using (DbManager Db = DbManager.GetDbManager())
				{
					List<SqlParameter> prams = new List<SqlParameter>();
					prams.Add(Db.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrganizationId));

					ds = Db.GetDataSet("up_Organization_GetCountryAndStateCodeByOrganizationId", prams.ToArray());
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetCountryAndStateCodeByOrganizationId", ex);
			}

			return ds;
		}

		public static int GetStewardshipByStakeholderId(int StakeholderId)
		{
			int StewardshipId = 0;
            DataSet ds = null;
			try
			{
				using (DbManager Db = DbManager.GetDbManager())
				{
					List<SqlParameter> prams = new List<SqlParameter>();
					prams.Add(Db.MakeInParam("@StakeholderId", SqlDbType.Int, 4, StakeholderId));

					ds = Db.GetDataSet("up_Organization_GetStewardshipByStakeholderId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        StewardshipId = Conversion.ParseInt(ds.Tables[0].Rows[0][0].ToString());
                    }
				}
			}
			catch (Exception ex)
			{
				new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetStewardshipByStakeholderId", ex);
			}

			return StewardshipId;
		}

        public static DataTable GetOrganizationAddress(string organizationName, string barcodeSerial)
        {
            DataTable dt = null;
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    if(!string.IsNullOrEmpty(organizationName))
                        prams.Add(Db.MakeInParam("@vchOrganizationName", SqlDbType.NVarChar, 510, organizationName));
                    if (!string.IsNullOrEmpty(barcodeSerial))
                        prams.Add(Db.MakeInParam("@vchBarCodeSerial", SqlDbType.NVarChar, 400, barcodeSerial));
                    dt = Db.GetDataSet("up_GetOrganizationAddressByLegalName", prams.ToArray()).Tables[0];
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetOrganizationAddress", ex);
            }

            return dt;
        }

        public static bool UpdateOrganizationLogoPath(int organizationId, string path)
        {
            DataTable dt = null;
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(Db.MakeInParam("@intOrganizationId", SqlDbType.Int, 4, organizationId));
                    prams.Add(Db.MakeInParam("@vchLogoPath", SqlDbType.NVarChar, 500, path));
                    int exec = Db.RunProc("up_UpdateOrganizationLogoPath", prams.ToArray());
                    return true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo UpdateOrganizationLogoPath", ex);
            }

            return false;
        }

        public static string GetOrganizationLogoPath(int orgId)
        {
            DataTable dt = null;
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(Db.MakeInParam("@intOrgId", SqlDbType.Int, 4, orgId));

                    dt = Db.GetDataSet("up_GetLogoImagePath", prams.ToArray()).Tables[0];
                    return dt.Rows[0]["vchLogoPath"].ToString();
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetOrganizationAddress", ex);
            }

            return null;
        }
        public static DataTable GetLogoPath(int userId)
        {
            DataTable dt = null;
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(Db.MakeInParam("@UserId", SqlDbType.Int, 4, userId));

                    dt = Db.GetDataSet("up_GetLogoImage", prams.ToArray()).Tables[0];
                    return dt;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetLogoPath", ex);
            }

            return null;
        }
        public static string GetOrgLegalNameByOrgId(int userId)
        {
            DataTable dt = null;
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(Db.MakeInParam("@intOrgId", SqlDbType.Int, 4, userId));

                    dt = Db.GetDataSet("up_GetOrgLegalNameByOrgId", prams.ToArray()).Tables[0];
                    if(dt != null && dt.Rows.Count > 0)
                        return dt.Rows[0]["LegalName"].ToString();
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetOrgLegalNameByOrgId", ex);
            }

            return null;
        }
        public static bool InsertUpdateOrganizationImage(int imageId, int orgId,string name, string vchPath, string thumbnailPath, bool bitActive, bool bitImage, int LookUpTypeId)
        {
            DataTable dt = null;
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    if (imageId > 0)
                    {
                        prams.Add(Db.MakeInParam("@intImageId", SqlDbType.Int, 4, imageId));
                    }
                    prams.Add(Db.MakeInParam("@intOrgId", SqlDbType.Int, 4, orgId));
                    prams.Add(Db.MakeInParam("@vchImagePath", SqlDbType.NVarChar, 100, vchPath));
                    prams.Add(Db.MakeInParam("@vchThumbnailPath", SqlDbType.NVarChar, 100, thumbnailPath));
                    prams.Add(Db.MakeInParam("@bitActive", SqlDbType.Bit, 0, bitActive));
                    prams.Add(Db.MakeInParam("@bitImage", SqlDbType.Bit, 0, bitImage));
                    prams.Add(Db.MakeInParam("@intLookUpTypeId", SqlDbType.Int, 4, LookUpTypeId));
                    prams.Add(Db.MakeInParam("@vchName", SqlDbType.NVarChar, 100, name));
                    DataSet ds=Db.GetDataSet("up_InsertUpdateOrganizationImages", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        //dt = ds.Tables[0];
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.ImageOrganizationImage", ex);
            }

            return false;
        }
        public static DataTable GetOrganizationLogo(int orgId, int lookupTypeId)
        {
            DataTable dt = null;
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(Db.MakeInParam("@intOrgId", SqlDbType.Int, 4, orgId));
                    prams.Add(Db.MakeInParam("@intLookUpTypeId", SqlDbType.Int, 4, lookupTypeId));

                    dt = Db.GetDataSet("up_GetImagesRecords", prams.ToArray()).Tables[0];
                    return dt;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetOrganizationLogo", ex);
            }

            return null;
        }

        public static void DeleteOrganizationLogo(int imageid)
        {
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(db.MakeInParam("@imageid", SqlDbType.Int, 0, imageid));


                    db.GetDataSet("up_DeleteOrganizationImage", prams.ToArray());
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.DeleteOrganizationLogo", exp);
            }
        }

        public static void ActiveDeactiveOrgImage(int imageid, int orgId, int lookuptypeId)
        {
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(db.MakeInParam("@imageid", SqlDbType.Int, 0, imageid));
                    prams.Add(db.MakeInParam("@orgId", SqlDbType.Int, 0, orgId));
                    prams.Add(db.MakeInParam("@lookuptype", SqlDbType.Int, 0, lookuptypeId));

                    db.GetDataSet("up_ActiveDeactiveOrgImage", prams.ToArray());
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.ActiveDeactiveOrgImage", exp);
            }
        }
        
        public static void PendingStewardshipChange(int stewardshipapprovalId)
        {
          
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(Db.MakeInParam("@stewardshipapprovalid", SqlDbType.Int, 0,stewardshipapprovalId));


                    Db.GetDataSet("up_PendingStewardship", prams.ToArray());
                   
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.PendingStewardshipChange", ex);
            }

       
        }

        public static void PendingApprovalChange(int stakholderapprovalId)
        {
          
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(Db.MakeInParam("@stakholderholderapprovalid", SqlDbType.Int, 0, stakholderapprovalId));


                    Db.GetDataSet("up_PendingApproval_Change", prams.ToArray());
                   
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.PendingApprovalChange", ex);
            }

       
        }





        public static DataSet GetStateCodeByStateId(int StateId)
        {
            DataSet ds = null;
            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(Db.MakeInParam("@intStateId", SqlDbType.Int, 0, StateId));

                    ds = Db.GetDataSet("up_getStateCodeByStateId", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetStateCodebyStateId", ex);
            }

            return ds;
        }
        public static string getLatestNotesStatusByOrganizationId(int OrganizationId)
        {
            DataSet ds = null;
            //  iTotalrows = 0;
            string Notes = null;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, OrganizationId));

                    //   prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_GetLatestNotesbyOrganizationId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        Notes = (ds.Tables[0].Rows[0]["vchNotes"]).ToString();
                    }

                    else
                    {
                        return Notes;

                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getLatestNotesStatusByOrganizationId", exp);
            }
            return Notes;


        }
        public static int getStewardshipStatusByOrganizationId(int OrganizationId,string standardstewardshipIds,int organizationTypeId)
        {
            DataSet ds = null;
            //  iTotalrows = 0;
            int datastatus = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, OrganizationId));
                    prams.Add(db.MakeInParam("@intOrganizationTypeId", SqlDbType.Int, 0, organizationTypeId));
                    prams.Add(db.MakeInParam("@StandardStewardshipIds", SqlDbType.VarChar, 2000, standardstewardshipIds));

                    //   prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("Up_GetStewardshipAprrovalbyOrganizationId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        datastatus =Conversion.ParseInt( ds.Tables[0].Rows[0]["intStatus"]);
                    }

                    else
                    {
                        return datastatus;

                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getStewardshipStatusByOrganizationId", exp);
            }
            return datastatus;


        }

        public static int getStewardshipStatusByStateId(int StateId)
        {
            DataSet ds = null;
            //  iTotalrows = 0;
            int datastatus=0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intStateId", SqlDbType.Int, 0,StateId));
                 
                    //   prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_getStewardshipStatusByStateId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return datastatus+1;
                    }

                    else
                    {
                        return datastatus;

                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getStewardshipStatusByStateId", exp);
            }
            return datastatus;


        }


        public static int getOrganizationIdByEmail(string email,int stateId)
        {
            DataSet ds = null;
            //  iTotalrows = 0;
            int datastatus = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@vchEmail", SqlDbType.NVarChar, 180, email));
                    prams.Add(db.MakeInParam("@intStateId", SqlDbType.BigInt, 18, stateId));
                    //prams.Add(db.MakeInParam("@SSID", SqlDbType.BigInt, 18, SSID));
                    
                    //   prams.Add(db.MakeReturnParam(SqlDbType.Int, 0));
                    ds = db.GetDataSet("up_getOrganizationId", prams.ToArray());
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        //iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }

                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.getStewardshipStatusByStateId", exp);
            }
            return 0;


        }





        public static void UpdateStewardshipInfo(OrganizationInfo objOrg, ContactInfo objPrimaryContact, Phones objPrimaryContactBusinessPhone, Phones objPrimaryContactCellPhone, ContactInfo objBillingContact, Phones objBillingContactBusinessPhone, Phones objBillingContactCellPhone, Organization_Address objBusinessOrganization_Address, Organization_Address objMailingOrganization_Address)
        {
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    List<SqlParameter> prm = new List<SqlParameter>();

                    #region Organization Info

                    if (objOrg.OrganizationId > 0)
                        prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, objOrg.OrganizationId));
                    else
                        prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, DBNull.Value));
                    prm.Add(DB.MakeInParam("@LegalName", SqlDbType.NVarChar, 255, objOrg.LegalName));
                    prm.Add(DB.MakeInParam("@DBAName", SqlDbType.NVarChar, 255, objOrg.DBAName));
                  //  prm.Add(DB.MakeInParam("@OrganizationTypeId", SqlDbType.Int, 4, objOrg.OrganizationTypeId));
                    prm.Add(DB.MakeInParam("@Website", SqlDbType.NVarChar, 255, objOrg.Website));
                  
                   
                    prm.Add(DB.MakeInParam("@OrgIsActive", SqlDbType.Bit, 1, objOrg.IsActive));
                    //prm.Add(DB.MakeInParam("@AcceptTextMessage", SqlDbType.Bit, 1, objOrg.AcceptTextMessages));
                    //prm.Add(DB.MakeInParam("@CellAcceptTextMessages", SqlDbType.Bit, 1, objOrg.CellAcceptTextMessages));
                    prm.Add(DB.MakeInParam("@OrgLanguageId", SqlDbType.Int, 4, objOrg.LanguageId));
                   // prm.Add(DB.MakeInParam("@RoleId", SqlDbType.Int, 4, objOrg.RoleId));

                    #endregion

                    #region Primary Contact Info

                   // prm.Add(DB.MakeInParam("@PrimaryContactTypeId", SqlDbType.Int, 4, objPrimaryContact.ContactTypeId));
                    prm.Add(DB.MakeInParam("@PrimaryContactFirstName", SqlDbType.NVarChar, 255, objPrimaryContact.FirstName));
                    prm.Add(DB.MakeInParam("@PrimaryContactLastName", SqlDbType.NVarChar, 255, objPrimaryContact.LastName));
                  //  prm.Add(DB.MakeInParam("@PrimaryContactTitleId", SqlDbType.Int, 4, objPrimaryContact.ContactTitleId));
                    prm.Add(DB.MakeInParam("@PrimaryContactEmail", SqlDbType.NVarChar, 200, objPrimaryContact.Email));
                    prm.Add(DB.MakeInParam("@PrimaryContactIsActive", SqlDbType.Bit, 1, objPrimaryContact.IsActive));
                    prm.Add(DB.MakeInParam("@PrimaryContactIsPrimary", SqlDbType.Bit, 1, objPrimaryContact.IsPrimary));
                   // prm.Add(DB.MakeInParam("@PrimaryContactLanguageId", SqlDbType.Int, 4, objPrimaryContact.LanguageId));

                    #endregion

                    #region Primary Contact Business Phone Info

                    prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneNumber", SqlDbType.NVarChar, 15, objPrimaryContactBusinessPhone.Number));
                    prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneExtension", SqlDbType.NVarChar, 7, objPrimaryContactBusinessPhone.Extension));
                  //  prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneTypeId", SqlDbType.Int, 4, objPrimaryContactBusinessPhone.PhoneTypeId));
                    prm.Add(DB.MakeInParam("@PrimaryContactBusinessPhoneIsActive", SqlDbType.Bit, 1, objPrimaryContactBusinessPhone.IsActive));

                    #endregion

                    //#region Primary Contact Cell Phone Info

                    //prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneNumber", SqlDbType.NVarChar, 15, objPrimaryContactCellPhone.Number));
                    //prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneIsAcceptTextMessages", SqlDbType.Bit, 1, objPrimaryContactCellPhone.IsAcceptTextMessages));
                    //prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneTypeId", SqlDbType.Int, 4, objPrimaryContactCellPhone.PhoneTypeId));
                    //prm.Add(DB.MakeInParam("@PrimaryContactCellPhoneIsActive", SqlDbType.Bit, 1, objPrimaryContactCellPhone.IsActive));

                    //#endregion

                    //#region Billing Contact Info

                    //prm.Add(DB.MakeInParam("@BillingContactTypeId", SqlDbType.Int, 4, objBillingContact.ContactTypeId));
                    //prm.Add(DB.MakeInParam("@BillingContactFirstName", SqlDbType.NVarChar, 255, objBillingContact.FirstName));
                    //prm.Add(DB.MakeInParam("@BillingContactLastName", SqlDbType.NVarChar, 255, objBillingContact.LastName));
                    //prm.Add(DB.MakeInParam("@BillingContactTitleId", SqlDbType.Int, 4, objBillingContact.ContactTitleId));
                    //prm.Add(DB.MakeInParam("@BillingContactEmail", SqlDbType.NVarChar, 200, objBillingContact.Email));
                    //prm.Add(DB.MakeInParam("@BillingContactIsActive", SqlDbType.Bit, 1, objBillingContact.IsActive));
                    //prm.Add(DB.MakeInParam("@BillingContactIsPrimary", SqlDbType.Bit, 1, objBillingContact.IsPrimary));
                    //prm.Add(DB.MakeInParam("@BillingContactLanguageId", SqlDbType.Int, 4, objBillingContact.LanguageId));

                    //#endregion

                    //#region Billing Contact Business Phone Info

                    //prm.Add(DB.MakeInParam("@BillingContactBusinessPhoneNumber", SqlDbType.NVarChar, 15, objBillingContactBusinessPhone.Number));
                    //prm.Add(DB.MakeInParam("@BillingContactBusinessPhoneExtension", SqlDbType.NVarChar, 7, objBillingContactBusinessPhone.Extension));
                    //prm.Add(DB.MakeInParam("@BillingContactBusinessPhoneTypeId", SqlDbType.Int, 4, objBillingContactBusinessPhone.PhoneTypeId));
                    //prm.Add(DB.MakeInParam("@BillingContactBusinessPhoneIsActive", SqlDbType.Bit, 1, objBillingContactBusinessPhone.IsActive));

                    //#endregion

                    //#region Billing Contact Cell Phone Info

                    //prm.Add(DB.MakeInParam("@BillingContactCellPhoneNumber", SqlDbType.NVarChar, 15, objBillingContactCellPhone.Number));
                    //prm.Add(DB.MakeInParam("@BillingContactCellPhoneIsAcceptTextMessages", SqlDbType.Bit, 1, objBillingContactCellPhone.IsAcceptTextMessages));
                    //prm.Add(DB.MakeInParam("@BillingContactCellPhoneTypeId", SqlDbType.Int, 4, objBillingContactCellPhone.PhoneTypeId));
                    //prm.Add(DB.MakeInParam("@BillingContactCellPhoneIsActive", SqlDbType.Bit, 1, objBillingContactCellPhone.IsActive));

                    //#endregion

                    #region Business Organization Address Info

                    prm.Add(DB.MakeInParam("@BusinessOrganization_AddressZipCodeId", SqlDbType.Int, 4, objBusinessOrganization_Address.ZipCodeID));
                    prm.Add(DB.MakeInParam("@BusinessOrganization_AddressZipCode", SqlDbType.NVarChar, 100, objBusinessOrganization_Address.ZipPostalCode));
                    prm.Add(DB.MakeInParam("@BusinessOrganizationAddress1", SqlDbType.NVarChar, 250, objBusinessOrganization_Address.Address1));
                    prm.Add(DB.MakeInParam("@BusinessOrganizationAddress2", SqlDbType.NVarChar, 250, objBusinessOrganization_Address.Address2));
                    prm.Add(DB.MakeInParam("@BusinessOrganization_AddressCity", SqlDbType.NVarChar, 100, objBusinessOrganization_Address.City));
                    prm.Add(DB.MakeInParam("@BusinessOrganization_AddressStateId", SqlDbType.Int, 4, objBusinessOrganization_Address.StateID));
                    prm.Add(DB.MakeInParam("@BusinessOrganization_AddressCountryID", SqlDbType.Int, 4, objBusinessOrganization_Address.CountryID));
                    prm.Add(DB.MakeInParam("@BusinessOrganization_AddressIsActive", SqlDbType.Bit, 1, objBusinessOrganization_Address.IsActive));
                    prm.Add(DB.MakeInParam("@BusinessOrganization_AddressDateCreated", SqlDbType.DateTime, 8, objBusinessOrganization_Address.DateCreated));
                    prm.Add(DB.MakeInParam("@BusinessOrganization_AddressTypeId", SqlDbType.Int, 4, objBusinessOrganization_Address.Organization_AddressTypeId));

                    #endregion

                    //#region Mailing Organization Address Info

                    //prm.Add(DB.MakeInParam("@MailingOrganization_AddressZipCodeId", SqlDbType.Int, 4, objMailingOrganization_Address.ZipCodeID));
                    //prm.Add(DB.MakeInParam("@MailingOrganization_AddressZipCode", SqlDbType.NVarChar, 100, objMailingOrganization_Address.ZipPostalCode));
                    //prm.Add(DB.MakeInParam("@MailingOrganizationAddress1", SqlDbType.NVarChar, 250, objMailingOrganization_Address.Address1));
                    //prm.Add(DB.MakeInParam("@MailingOrganizationAddress2", SqlDbType.NVarChar, 250, objMailingOrganization_Address.Address2));
                    //prm.Add(DB.MakeInParam("@MailingOrganization_AddressCity", SqlDbType.NVarChar, 100, objMailingOrganization_Address.City));
                    //prm.Add(DB.MakeInParam("@MailingOrganization_AddressStateId", SqlDbType.Int, 4, objMailingOrganization_Address.StateID));
                    //prm.Add(DB.MakeInParam("@MailingOrganization_AddressCountryID", SqlDbType.Int, 4, objMailingOrganization_Address.CountryID));
                    //prm.Add(DB.MakeInParam("@MailingOrganization_AddressIsActive", SqlDbType.Bit, 1, objMailingOrganization_Address.IsActive));
                    //prm.Add(DB.MakeInParam("@MailingOrganization_AddressDateCreated", SqlDbType.DateTime, 8, objMailingOrganization_Address.DateCreated));
                    //prm.Add(DB.MakeInParam("@MailingOrganization_AddressTypeId", SqlDbType.Int, 4, objMailingOrganization_Address.Organization_AddressTypeId));

                    //#endregion

                    objOrg.OrganizationId = DB.RunProc("up_updateStewardshipByOrgId", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.UpdateStewardshipInfo", ex);
            }
        }







        public static void PendingStewardshipStatusByStewardshipId(int stewardshipapprovalId,Boolean status)
        {

            try
            {
                using (DbManager Db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(Db.MakeInParam("@intStewardshipId", SqlDbType.Int, 0, stewardshipapprovalId));
                    prams.Add(Db.MakeInParam("@bitActive", SqlDbType.Bit, 0, status));


                    Db.GetDataSet("up_ApproveOrRejectStewardshipByIdandStatus", prams.ToArray());

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.PendingStewardshipStatusByStewardshipId", ex);
            }


        }

        public static DataSet GetProductCategoryByOrgId(int OrgId)
        {
            try
            {
                DataSet ds = null;
                using (DbManager db = DbManager.GetDbManager())
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(db.MakeInParam("@OrganizationId", SqlDbType.Int, 4, OrgId));
                    ds = db.GetDataSet("Up_GetProductTypesByOrgId", param.ToArray());
                }
                return ds;
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "OrganizationInfo.GetProductCategoryByOrgId", ex);
                return null;
            }
        }


    }
}
