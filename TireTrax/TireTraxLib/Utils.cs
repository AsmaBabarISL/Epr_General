using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading;
using System.Globalization;
using System.Net;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml.Serialization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using System.Web.UI;
//using System.Web.Script.Serialization;


//using System.Xml.Serialization;
using System.Security;
using System.Xml.Linq;
using System.Web.UI.HtmlControls;
using Zen.Barcode;

namespace TireTraxLib
{
    public enum AJAXGet
    {
        StakeHolders,
        Stewardship,
        Inventory,
        Nothing,
    }

    public enum AJAXModify
    {
        User,

    }
    public enum LookUps
    {
        RoleName,
        BusinessName,
        Business,
        Country,
        State,
        PropertyStreetDirections,
        AddressType,
        County,
        ListingTypes,
        Sections,
        Zones,
        Pages,
        Dimensions,
        NewsLetters,
        NewLetterRepeatFrequency,
        RepeatInterval,
        Albums,
        MedicineType,
        MedicineSubType,
        LookUp,
        Role,
        PrimaryBusinessType,
        City,
        PhoneType,
        ContactType,
        OrganizationType,
        ContactTitleType,
        OrganizationLocationEventType,
        Interface,
        PrimaryContactTitle,
        BillingContactTitle,
        LocationContactTitle,
        RecycleState,
        OrganizationSubType,
        CreditCardType,
        StateIdAndName,
        BankAccountType,
        LookUpType,
        LoadType,
        LoadInventoryAction,
        LoadInventoryClass,
        LoadInventoryOutcome,
        Groups,
        MainResources,
        LoadStakeholderTypes,
        OrgStakeholderByStewardshipIdAndOrgTypeId,
        Facility,
        Lot,
        Space,
        Lane,
        StewardshipTypes,
        RoleTypes,
        CountryIdandName,
        SubRolesTypes,
        Users,
        TemplateType,
        PaymentType,
        ProductCategory,
        ProductShape,
        ProductSize,
        ProductMaterial,
        SubCategory,
        
        ProductSubCategoryName,
        ProductName,
        BrandCat,
        ProductBrand,
        ProductType,
        SelectedSubCategory,
        AllOrganizationsOfState,
        GetStates
        
    }
    public enum ExpiryDate
    {
        Month,
        Year
    }
    public enum InvoiceType
    {
        Single = 1,
        Commulative = 2,
    }
    public enum OrganizationType
    {
        Stewardship = 1,
        Steward = 2,
        Stakeholder = 3,
        GovernmentAgency = 4,
        LawEnforcementAgency = 5
    }
    public enum AgreementTypes
    {
        US_StewardshipPrivacyAgreement = 1,
        US_TireTraxPrivacyAgreement = 2,
        CA_FR_StewardshipPrivacyAgreement = 3,
        CA_FR_TireTraxPrivacyAgreement = 4,
        CA_ENStewardshipPrivacyAgreement = 5,
        CA_ENTireTraxPrivacyAgreement = 6,
        AU_StewardshipPrivacyAgreement = 7,
        AU_TireTraxPrivacyAgreement = 8,
        JP_StewardshipPrivacyAgreement = 9,
        JP_TireTraxPrivacyAgreement = 10,
        SK_StewardshipPrivacyAgreement = 11,
        SK_TireTraxPrivacyAgreement = 12,
        CH_StewardshipPrivacyAgreement = 13,
        CH_TireTraxPrivacyAgreement = 14
       
    }

    public enum AdminOrganizationType
    {
        EPRAdmin = 2
    }

    public enum Languages
    {
        Default = 1,
        English = 1,
        Spanish = 2,
        French = 3,
        German = 4,
        ChineseMandarin = 5,
        ChineseCantonese = 6,
        Canadian = 7,
        Australia = 8,
        Japanese = 9,
        Korean = 10,
        Chinese = 11,
    }
    public enum OrganizationStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Deleted = 4,

       
    
    }
    public enum TemplateTypes
    {
        Invoice = 351,
        Email = 352,
    }

    public enum ResourceType
    {
        #region Menu
        Admin = 100,
        Home = 105,
        Inventory = 110,
        Stakeholders = 115,
        Revenue = 120,
        Applications = 125,
        Reports = 130,
        Users = 135,
        PTE = 140,
        Settings = 145,
        AccountManagement = 285,

        #endregion
        #region Submenu
        Lots = 215,
        Loads = 220,
        Facility = 225,
        LogoSettings = 230,
        BankAccount = 235,
        CreditCard = 240,
        Profile = 245,
        PTEStandard = 250,
        AddDeliveryNote = 255,
        DeliveryNotes = 260,
        DeliveryReceipt = 265,
        EditDeliveryNote = 270,
        Templates = 275,
        AddTemplate = 280,
        Invoices = 290,
        AddInvoice = 295,
        ProductSelection = 300,
        #endregion
        #region Pages
        LotsInventory = 150,
        PagePermissions = 155,
        FacilityInventory = 160,
        LoadsInventory = 165,
        AccountingUser = 170,
        AddBankAccountHome = 175,
        AddCreditCardHome = 180,
        AddInventoryInventory = 185,
        BankAccountHome = 190,
        CreateLoadInventory = 195,
        CreditCardHome = 200,
        EditBankAccountHome = 205,
        EditCreditCardHome = 210,
        LogoSetting = 230,
        ProfileSetting = 220,

        #endregion

    }
    public enum RoleTypes
    {
        Stewardship = 20,
        LocalSteward = 21,
        Stakeholder = 22,
        GovernmentAgency = 23,
        LawEnforcementAgency = 24,
        GlobalSteward = 47,

    }
    public enum OrganizationCertificationTypes
    {
        StewardshipCertificationsEnglish = 7,
        StakeholderCertificationsEnglish = 8,
        StewardshipCertificationsSpanish = 101,
        StakeholderCertificationsSpanish = 102,
        StewardshipCertificationsFrench = 103,
        StakeholderCertificationsFrench = 104,
        StewardshipCertificationsCanadian = 105,
        StakeholderCertificationsCanadian = 106,
        StewardshipCertificationsAustralia = 107,
        StakeholderCertificationsAustralia = 108,
        StewardshipCertificationsJapanese = 109,
        StakeholderCertificationsJapanese = 110,
        StewardshipCertificationsKorean = 111,
        StakeholderCertificationsKorean = 112,
        StewardshipCertificationsChinese = 113,
        StakeholderCertificationsChinese = 114,
    }

    public enum ProductCategory
    {
        Tire = 357,
        Bottle = 358,
        Glass = 359
    }
    //===================================================================================================
    /// <summary>
    /// Lookup routines
    /// Author: Asad Aziz
    /// Date: 2/9/2010
    /// </summary>
    public class Utils
    {

        private const string SECRET_SALT = "A5ghMk980CFg42Ws";
        protected static string[] restrictedUsernameSubstrings = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RestrictedUsernameSubstrings"]) ? null : ConfigurationManager.AppSettings["RestrictedUsernameSubstrings"].Split(','));
        protected static string[] restrictedUsernames = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RestrictedUsernames"]) ? null : ConfigurationManager.AppSettings["RestrictedUsernames"].Split(','));
        protected static string[] restrictedTags = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RestrictedTags"]) ? null : ConfigurationManager.AppSettings["RestrictedTags"].Split(','));
        protected static string[] restrictedWords = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RestrictedWords"]) ? null : ConfigurationManager.AppSettings["RestrictedWords"].Split(','));
        protected static string[] restrictedHitBoxCharacters = new string[] { "‘", "\"", "&", "|", "#", "$", "%", "^", "*", ":", "!", @"\", "<", ">", "~", ";" };


        public static NumberFormatInfo SizeNFI
        {
            get
            {
                NumberFormatInfo s = new NumberFormatInfo();
                s.CurrencyDecimalDigits = 0;
                s.CurrencySymbol = "";
                s.CurrencyGroupSeparator = ",";
                return s;
            }

        }
        public static NumberFormatInfo SizeNFITwoDecimal
        {
            get
            {
                NumberFormatInfo s = new NumberFormatInfo();
                s.CurrencyDecimalDigits = 2;
                s.CurrencySymbol = "";
                s.CurrencyGroupSeparator = ",";
                return s;
            }

        }

        public static NumberFormatInfo MoneyNFI
        {
            get
            {
                NumberFormatInfo s = new NumberFormatInfo();
                s.CurrencyDecimalDigits = 2;
                s.CurrencySymbol = "";
                return s;

            }

        }

        public static NumberFormatInfo MoneyNFIZeroDecimal
        {
            get
            {
                NumberFormatInfo s = new NumberFormatInfo();
                s.CurrencyDecimalDigits = 0;
                s.CurrencySymbol = "";
                return s;

            }

        }

        public static Languages GetLangaugeByCulture(string culture)
        {
            Languages lang = Languages.Default;

            if (culture.ToLower() == "en-us")
                lang = Languages.English;
            else if (culture.ToLower() == "es-mx")
                lang = Languages.Spanish;
            else if (culture.ToLower() == "fr-fr" || culture.ToLower() == "fr-ca")
                lang = Languages.French;
            else if (culture.ToLower() == "ja-jp")
                lang = Languages.Japanese;
            else if (culture.ToLower() == "ko-kr")
                lang = Languages.Korean;
            else if (culture.ToLower() == "zh-cn")
                lang = Languages.Chinese;
            else if (culture.ToLower() == "en-au")
                lang = Languages.Australia;
            else if (culture.ToLower() == "en-ca")
                lang = Languages.Canadian;

            return lang;
        }

        public static int GetCountryIdByCulture(string culture)
        {
            int CountryID = 235;

            if (culture.ToLower() == "en-us")
                CountryID = 235;
            else if (culture.ToLower() == "es-mx")
                CountryID = 159;
            else if (culture.ToLower() == "fr-fr" || culture.ToLower() == "fr-ca")
                CountryID = 39;
            else if (culture.ToLower() == "ja-jp")
                CountryID = 116;
            else if (culture.ToLower() == "ko-kr")
                CountryID = 124;
            else if (culture.ToLower() == "zh-cn")
                CountryID = 49;
            else if (culture.ToLower() == "en-au")
                CountryID = 14;
            else if (culture.ToLower() == "en-ca")
                CountryID = 39;

            return CountryID;
        }

        public static void SetCulture(string culture, string uiCulture)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(uiCulture);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            HttpContext.Current.Response.Cookies["CultureCookie"]["UICulture"] = Thread.CurrentThread.CurrentUICulture.ToString();
            HttpContext.Current.Response.Cookies["CultureCookie"]["Culture"] = Thread.CurrentThread.CurrentCulture.ToString();
        }

        public static void BindDropDownListToEnum(DropDownList ddl, Type enumType)
        {
            BindDropDownListToEnum(ddl, enumType, "");
        }
        public static void BindDropDownListToEnum(DropDownList ddl, Type enumType, object selectedEnumValue)
        {
            if (selectedEnumValue != null)
                BindDropDownListToEnum(ddl, enumType, Enum.GetName(enumType, selectedEnumValue));
        }
        public static void BindDropDownListToEnum(DropDownList ddl, Type enumType, string selectedValue)
        {
            ddl.Items.Clear();
            ddl.DataSource = Enum.GetNames(enumType);
            ddl.DataBind();
            if (selectedValue.Length > 0)
                ddl.SelectedValue = selectedValue;
        }
        public static void BindDropDownListToEnumNamesAndVal(DropDownList ddl, Type enumType, string selectedValue)
        {
            ddl.Items.Clear();
            Hashtable ht = GetEnumForBind(enumType);
            ddl.DataSource = ht;
            ddl.DataTextField = "value";
            ddl.DataValueField = "key";

            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));
            if (selectedValue.Length > 0)
                ddl.SelectedValue = selectedValue;
        }

        public static void BindDropDownListToEnumDesc(DropDownList ddl, Type enumType, object selectedEnumValue)
        {
            ddl.Items.Clear();
            System.Reflection.FieldInfo[] enumFields = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);

            try
            {
                for (int i = 0; i < enumFields.Length; i++)
                {
                    string value = Convert.ToString((Enum)enumFields[i].GetValue(enumType));
                    string name = enumFields[i].Name;

                    // Use description
                    System.ComponentModel.DescriptionAttribute[] ea =
                        (System.ComponentModel.DescriptionAttribute[])enumFields[i].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                    if (ea.Length > 0)
                        name = ea[0].Description;

                    ddl.Items.Add(new ListItem(name, value));
                }

                if (selectedEnumValue != null)
                    ddl.SelectedValue = Enum.GetName(enumType, selectedEnumValue);
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Utils.BindDropDownListToEnumDesc", ex);
                ddl.Items.Clear();
            }
        }
        public static bool CheckTopLevelDomaiReferrer(HttpContext context, string checkvalue)
        {
            try
            {
                string url = context.Request.UrlReferrer.Host.ToString().ToLower();
                return url.EndsWith(checkvalue.ToLower());
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Utils.CheckTopLevelDomaiReferrer", ex);
                return false;
            }
        }
        public static string RemoveDuplicateWords(string v)
        {
            var d = new Dictionary<string, bool>();
            StringBuilder b = new StringBuilder();
            string[] a = v.Split(new char[] { ' ', ',', ';', '.' },
                StringSplitOptions.RemoveEmptyEntries);
            foreach (string current in a)
            {
                string lower = current.ToLower();
                if (!d.ContainsKey(lower))
                {
                    b.Append(current).Append(' ');
                    d.Add(lower, true);
                }
            }
            return b.ToString().Trim();
        }


        public static string replaceCommans(string value)
        {
            return value.Replace("'", "''");
        }


        public static void GetTimeSelector<T>(ref T t)
        {
            DropDownList ddl = null;
            if (t is DropDownList)
                ddl = t as DropDownList;

            ListItem item = null;
            for (int hour = 0; hour < 24; hour++)
            {
                for (int minute = 0; minute < 60; minute++)
                {
                    if (minute % 15 == 0)
                    {
                        item = new ListItem(string.Format("{0:00}:{1:00}", hour, minute), string.Format("{0:00}:{1:00}", hour, minute));
                        ddl.Items.Add(item);
                    }
                }
            }
        }


        public static DataTable GetAllCountries()
        {
            SqlParameter[] prams = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams = new SqlParameter[1];
                    return db.GetDataSet("up_getCountry", null).Tables[0];
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Utils.GetAllCountries", ex);
            }
            return null;
        }

        public static void GetLookUpData<T>(ref T t, LookUps lookup, bool addSelect = true)
        {
            GetLookUpData<T>(ref t, lookup, null, 0, addSelect);
        }

        public static void GetLookUpData<T>(ref T t, LookUps lookup, int firstlevelId, bool addSelect = true)
        {

            SqlParameter[] prams = null;
            GetLookUpData<T>(ref t, lookup, prams, firstlevelId, addSelect);
        }
        public static void GetLookUpData<T>(ref T t, LookUps lookup, int firstlevelId, int SecValue, bool addSelect = true)
        {

            SqlParameter[] prams = null;
            GetLookUpData<T>(ref t, lookup, prams, firstlevelId, addSelect, SecValue);
        }

        public static void GetLookUpData<T>(ref T t, LookUps lookup, SqlParameter[] prams, bool addSelect = true)
        {
            GetLookUpData<T>(ref t, lookup, prams, 0, addSelect);
        }
        public static void GetLookUpData<T>(ref T t, LookUps lookup, string value, bool addSelect = true)
        {
            var prams = new SqlParameter[1];
            using (DbManager db = DbManager.GetDbManager())
            {
                prams[0] = db.MakeInParam("@userID", SqlDbType.VarChar, 0, value);
            }
            GetLookUpData<T>(ref t, lookup, prams, 0, addSelect);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="lookup"></param>
        public static void GetLookUpData<T>(ref T t, LookUps lookup, SqlParameter[] prams, int firstlevelId, bool addSelect = true, int SecValue = -1)
        {

            SqlDataReader reader = null;
            DropDownList dropDownList = null;
            RadioButtonList radioList = null;
            CheckBoxList chkList = null;
            ListBox lstList = null;
            bool _IsNagitiveValueRequired = false;

            if (t is DropDownList)
                dropDownList = t as DropDownList;

            else if (t is RadioButtonList)
                radioList = t as RadioButtonList;

            else if (t is CheckBoxList)
                chkList = t as CheckBoxList;

            else if (t is ListBox)
                lstList = t as ListBox;

            //added by sheraz to clear the items to avoid dulpication,must be handled in the page
            if (dropDownList != null && dropDownList.Items.Count > 0)
            {
                dropDownList.Items.Clear();
            }
            if (radioList != null && radioList.Items.Count > 0)
            {
                radioList.Items.Clear();
            }
            if (chkList != null && chkList.Items.Count > 0)
            {
                chkList.Items.Clear();
            }
            if (lstList != null && lstList.Items.Count > 0)
            {
                lstList.Items.Clear();
            }



            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    switch (lookup)
                    {

                        case LookUps.LoadInventoryClass:
                            reader = db.GetDataReader("up_getInventoryClass_ddlLoad");
                            break;
                        case LookUps.LoadInventoryOutcome:
                            reader = db.GetDataReader("up_getInventoryOutCome_ddlLoad");
                            break;
                        case LookUps.LoadInventoryAction:
                            reader = db.GetDataReader("up_getInventoryAction_ddlLoad");
                            break;
                        case LookUps.Country:
                            reader = db.GetDataReader("up_country_get");
                            break;
                        case LookUps.CreditCardType:
                            reader = db.GetDataReader("up_GetCreditIdAndName");
                            break;
                        case LookUps.BankAccountType:
                            reader = db.GetDataReader("up_BankAccountTypesBYLookupType");
                            break;
                        case LookUps.LoadType:
                            reader = db.GetDataReader("up_Load_getloadtypes");
                            break;

                        case LookUps.RoleName:
                            prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@OrgTypeID", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_Role_RoleIdandRoleName", prams);
                            break;
                        case LookUps.Business:
                            reader = db.GetDataReader("up_getBusinessNameAndId");
                            break;
                        case LookUps.BusinessName:
                            reader = db.GetDataReader("up_Business_IDandName", prams);
                            break;
                        case LookUps.County:
                            reader = db.GetDataReader("up_county_get");
                            break;
                        case LookUps.State:
                            reader = db.GetDataReader("up_getStateByCountryId", prams);
                            break;
                        case LookUps.City:
                            reader = db.GetDataReader("up_getCity");
                            break;
                        case LookUps.AddressType:
                            reader = db.GetDataReader("up_getAddressType");
                            break;
                        case LookUps.ListingTypes:
                            reader = db.GetDataReader("up_getListingType");
                            break;
                        case LookUps.Sections:
                            reader = db.GetDataReader("up_getAllSectionsbyZoneID", prams);
                            break;
                        case LookUps.Zones:
                            reader = db.GetDataReader("up_getAllZones");
                            break;
                        case LookUps.Pages:
                            reader = db.GetDataReader("up_getAllPages");
                            break;
                        case LookUps.Dimensions:
                            reader = db.GetDataReader("up_getDimensionByZoneID", prams);
                            break;
                        case LookUps.NewsLetters:
                            reader = db.GetDataReader("up_GetAllNewsLetterNames", prams);
                            break;
                        case LookUps.NewLetterRepeatFrequency:
                            reader = db.GetDataReader("up_GetNewsLetterRepeateFrequency", prams);
                            break;
                        case LookUps.Albums:
                            reader = db.GetDataReader("up_getAlbums", prams);
                            break;
                        case LookUps.MedicineType:
                            reader = db.GetDataReader("up_getMedicineType");
                            break;
                        case LookUps.MedicineSubType:
                            reader = db.GetDataReader("up_getMedicineSubTypeByTypeID", prams);
                            break;
                        case LookUps.PrimaryBusinessType:
                            reader = db.GetDataReader("up_getPrimaryBusinessType");
                            break;
                        case LookUps.LookUp:
                            reader = db.GetDataReader("up_getLookup", prams);
                            break;
                        case LookUps.Role:
                            reader = db.GetDataReader("up_getRole");
                            break;
                        case LookUps.PhoneType:
                            reader = db.GetDataReader("up_getPhoneTypes");
                            break;
                        case LookUps.ContactType:
                            reader = db.GetDataReader("up_getContactType");
                            break;

                        case LookUps.LoadStakeholderTypes:
                            prams = new SqlParameter[2];
                            prams[0] = db.MakeInParam("@langId", SqlDbType.Int, 0, firstlevelId);
                            prams[1] = db.MakeInParam("@OrgID", SqlDbType.Int, 0, RoleTypes.Stakeholder);


                            reader = db.GetDataReader("up_getStakeholderTypesbyId", prams);
                            break;

                        case LookUps.OrganizationType:
                             prams = new SqlParameter[1];
                             prams[0] = db.MakeInParam("@intLanguageId", SqlDbType.Int, 0, firstlevelId);

                            reader = db.GetDataReader("up_Lookup_GetOrganizationType", prams);
                            break;
                        case LookUps.OrganizationSubType:
                             prams = new SqlParameter[1];
                             prams[0] = db.MakeInParam("@intLanguageId", SqlDbType.Int, 0, firstlevelId);

                            reader = db.GetDataReader("up_Lookup_GetOrganizationSubType", prams);
                            break;
                            
                        case LookUps.ContactTitleType:
                            prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@ContactTitleTypeId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("UP_GetContactTitleByType", prams);
                            break;
                        case LookUps.OrganizationLocationEventType:
                            prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@intLanguageId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_Lookup_GetLocationEventType", prams);
                            break;
                        case LookUps.Interface:
                            prams = new SqlParameter[1];
                             prams[0] = db.MakeInParam("@intLanguageId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_Lookup_GetInterfaceType", prams);
                            break;
                        case LookUps.PrimaryContactTitle:
                            reader = db.GetDataReader("up_Lookup_GetPrimaryContactTitle", prams);
                            break;
                        case LookUps.BillingContactTitle:
                            prams = new SqlParameter[1];
                             prams[0] = db.MakeInParam("@intLanguageId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_Lookup_GetBillingContactTitle", prams);
                            break;
                        case LookUps.LocationContactTitle:
                            prams = new SqlParameter[1];
                             prams[0] = db.MakeInParam("@intLanguageId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_Lookup_GetLocationContactTitle", prams);
                            break;
                        case LookUps.RecycleState:
                            reader = db.GetDataReader("up_Lookup_GetRecycleState", prams);
                            break;
                        //case LookUps.OrganizationSubType:
                        //    reader = db.GetDataReader("up_GetOrganizationSubTypeByOrganizationType", prams);
                        //    break;
                        case LookUps.StateIdAndName:
                            reader = db.GetDataReader("up_getStateIdAndStateName", prams);
                            break;
                        case LookUps.LookUpType:
                            reader = db.GetDataReader("up_GetLookUpTypeById", prams);
                            break;
                        case LookUps.Groups:
                            reader = db.GetDataReader("up_getGroups");
                            break;
                        case LookUps.MainResources:
                            reader = db.GetDataReader("up_GetMainDomains");
                            break;
                        case LookUps.OrgStakeholderByStewardshipIdAndOrgTypeId:
                            prams = new SqlParameter[2];
                            prams[0] = db.MakeInParam("@organizationTypeId", SqlDbType.Int, 0, firstlevelId);

                            prams[1] = db.MakeInParam("@organizationID", SqlDbType.Int, 0, SecValue);
                            reader = db.GetDataReader("up_getStakeholdersByStewardshipIdAndOrgTypeID", prams);
                            break;
                        case LookUps.Facility:
                            reader = db.GetDataReader("Up_Facility_getFacilityLookUp", prams);
                            break;
                        case LookUps.Lot:
                            reader = db.GetDataReader("Up_Lot_getLotLookUp", prams);
                            break;
                        case LookUps.Space:
                            reader = db.GetDataReader("Up_Space_getSpaceLookUp", prams);
                            break;
                        case LookUps.Lane:
                            reader = db.GetDataReader("Up_Lane_getLaneLookUp", prams);
                            break;

                        case LookUps.StewardshipTypes:
                            prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@intCountryId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_getAllStatesByCountryId", prams);
                            break;
                        case LookUps.CountryIdandName:
                            reader = db.GetDataReader("up_getCountryAndCountryId");
                            break;
                        case LookUps.Users:
                              prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_User_getByOrganizationId", prams);
                            break;
                        case LookUps.SubRolesTypes:
                            prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@intRoleTypeId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_getSubRoleTypeByRoleTypeId", prams);
                            break;
                        case LookUps.TemplateType:
                            reader = db.GetDataReader("up_getTemplateType_ddlLoad");
                            break;
                        case LookUps.PaymentType:
                            reader = db.GetDataReader("up_getPaymentType");
                            break;
                        case LookUps.ProductSize:
                            prams = new SqlParameter[2];
                            prams[0] = db.MakeInParam("@ProductCategoryId", SqlDbType.Int, 0, firstlevelId);
                            prams[1] = db.MakeInParam("@LanguageId", SqlDbType.Int, 0, SecValue);
                            reader = db.GetDataReader("up_GetProductSizes", prams);
                            break;
                        case LookUps.ProductShape:
                             prams = new SqlParameter[2];
                            prams[0] = db.MakeInParam("@ProductCategoryId", SqlDbType.Int, 0, firstlevelId);
                            prams[1] = db.MakeInParam("@LanguageId", SqlDbType.Int, 0, SecValue);
                            reader = db.GetDataReader("up_GetProductShapes", prams);
                            break;
                        case LookUps.ProductMaterial:
                            prams = new SqlParameter[2];
                            prams[0] = db.MakeInParam("@ProductCategoryId", SqlDbType.Int, 0, firstlevelId);
                            prams[1] = db.MakeInParam("@LanguageId", SqlDbType.Int, 0, SecValue);
                            reader = db.GetDataReader("up_GetProductMaterials", prams);
                            break;
                        case LookUps.ProductBrand:
                            prams = new SqlParameter[2];
                            prams[0] = db.MakeInParam("@ProductCategoryId", SqlDbType.Int, 0, firstlevelId);
                            prams[1] = db.MakeInParam("@LanguageId", SqlDbType.Int, 0, SecValue);
                            reader = db.GetDataReader("up_GetProductBrands", prams);
                            break;
                        case LookUps.SubCategory:
                            prams = new SqlParameter[2];
                            prams[0] = db.MakeInParam("@LookupTypeID", SqlDbType.Int, 0, firstlevelId);
                            prams[1] = db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, SecValue);
                            reader = db.GetDataReader("Get_productCategory_details", prams);
                            break;
                        case LookUps.ProductName:
                            reader = db.GetDataReader("up_GetProductNames");
                            break;
                        case LookUps.ProductSubCategoryName:
                            prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@CategoryId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_GetProductSubCategoryNames", prams);
                            break;
                        case LookUps.BrandCat:
                            prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@CatId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_GetBrandsByCatId", prams);
                            break;
                        case LookUps.ProductType:
                            prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("Up_GetProductTypesByOrgId", prams);
                            break;
                        case LookUps.SelectedSubCategory:
                            prams = new SqlParameter[2];
                            prams[0] = db.MakeInParam("@LookupTypeID", SqlDbType.Int, 0, firstlevelId);
                            prams[1] = db.MakeInParam("@OrganizationId", SqlDbType.Int, 0, SecValue);
                            reader = db.GetDataReader("Get_SelectedproductCategory_details", prams);
                            break;
                        case LookUps.AllOrganizationsOfState:
                            prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@StateId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_GetAllOrganizationByStateId", prams);
                            break;
                        case LookUps.GetStates:
                            prams = new SqlParameter[1];
                            prams[0] = db.MakeInParam("@CountryId", SqlDbType.Int, 0, firstlevelId);
                            reader = db.GetDataReader("up_GetStatesByCountryId", prams);
                            break;

                    }

                    if (t is DropDownList && addSelect)
                    {


                        if (_IsNagitiveValueRequired)
                        {
                            ListItem i = new ListItem(ResourceMgr.GetMessage("Select"), "-1");
                            dropDownList.Items.Insert(0, i);
                        }
                        else
                        {
                            ListItem i = new ListItem(ResourceMgr.GetMessage("Select"), "0");
                            dropDownList.Items.Insert(0, i);
                        }

                    }

                    while (reader.Read())
                    {
                        ListItem item = new ListItem();
                        item.Value = reader[0].ToString();
                        item.Text = reader[1].ToString();
                        if (t is DropDownList)
                        {
                            dropDownList.Items.Add(item);
                        }
                        else if (t is RadioButtonList)
                        {
                            radioList.Items.Add(item);
                        }
                        else if (t is ListBox)
                        {
                            lstList.Items.Add(item);
                        }
                        else if (t is CheckBoxList)
                        {
                            chkList.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Utils.GetLookUpData", ex);

            }
            finally
            {
                if (null != reader) { reader.Close(); }
            }
        }

        public static void GetExpiryDate<T>(ref T t, ExpiryDate expiryDate)
        {
            DropDownList ddl = null;
            if (t is DropDownList)
                ddl = t as DropDownList;

            ListItem item = null;

            switch (expiryDate)
            {
                case ExpiryDate.Month:
                    ddl.Items.Insert(0, new ListItem("Month", "0"));
                    for (int x = 1; x <= 12; x++)
                    {
                        if (x < 10)
                            item = new ListItem("0" + x.ToString(), "0" + x.ToString());
                        else
                            item = new ListItem(x.ToString(), x.ToString());
                        ddl.Items.Add(item);
                    }
                    break;
                case ExpiryDate.Year:
                    ddl.Items.Insert(0, new ListItem("Year", "0"));
                    for (int x = 2012; x <= 2025; x++)
                    {
                        item = new ListItem(x.ToString(), x.ToString());
                        ddl.Items.Add(item);
                    }
                    break;


            }
        }

        public static void GetProductProperties<T>(ref T t,LookUps lookup, params int[] arr)
        {
            List<SqlParameter> prams = new List<SqlParameter>();
            SqlDataReader reader = null;
            bool addSelect = true;
            DropDownList dropDownList = null;
            RadioButtonList radioList = null;
            CheckBoxList chkList = null;
            ListBox lstList = null;
            bool _IsNagitiveValueRequired = false;

            if (t is DropDownList)
                dropDownList = t as DropDownList;

            else if (t is RadioButtonList)
                radioList = t as RadioButtonList;

            else if (t is CheckBoxList)
                chkList = t as CheckBoxList;

            else if (t is ListBox)
                lstList = t as ListBox;
            if (dropDownList != null && dropDownList.Items.Count > 0)
            {
                dropDownList.Items.Clear();
            }
            if (radioList != null && radioList.Items.Count > 0)
            {
                radioList.Items.Clear();
            }
            if (chkList != null && chkList.Items.Count > 0)
            {
                chkList.Items.Clear();
            }
            if (lstList != null && lstList.Items.Count > 0)
            {
                lstList.Items.Clear();
        }


            try
            {

                using (DbManager db = DbManager.GetDbManager())
                {
                    if (arr.Length == 2)
                    {
                        prams.Add(db.MakeInParam("@ProductCategoryId", SqlDbType.Int, 0, arr[0]));
                        prams.Add(db.MakeInParam("@LanguageId", SqlDbType.Int, 0, arr[1]));
                        prams.Add(db.MakeInParam("@SubCat", SqlDbType.Int, 0, DBNull.Value));
                    }
                    else if (arr.Length == 3)
                    {
                        prams.Add(db.MakeInParam("@ProductCategoryId", SqlDbType.Int, 0, arr[0]));
                        prams.Add(db.MakeInParam("@LanguageId", SqlDbType.Int, 0, arr[1]));
                        prams.Add(db.MakeInParam("@SubCat", SqlDbType.Int, 0, arr[2]));
                    }
                    switch (lookup)
                    {
                        case LookUps.ProductShape:
                            reader = db.GetDataReader("up_GetProductShapes", prams.ToArray());
                            break;
                        case LookUps.ProductSize:
                            reader = db.GetDataReader("up_GetProductSizes", prams.ToArray());
                            break;
                        case LookUps.ProductMaterial:
                            reader = db.GetDataReader("up_GetProductMaterials", prams.ToArray());
                            break;
                        case LookUps.ProductBrand:
                            reader = db.GetDataReader("up_GetProductBrands", prams.ToArray());
                            break;
                        default:
                            break;
                    }
                    if (t is DropDownList && addSelect)
                    {


                        if (_IsNagitiveValueRequired)
                        {
                            ListItem i = new ListItem(ResourceMgr.GetMessage("Select"), "-1");
                            dropDownList.Items.Insert(0, i);
                        }
                        else
                        {
                            ListItem i = new ListItem(ResourceMgr.GetMessage("Select"), "0");
                            dropDownList.Items.Insert(0, i);
                        }

                    }

                    while (reader.Read())
                    {
                        ListItem item = new ListItem();
                        item.Value = reader[0].ToString();
                        item.Text = reader[1].ToString();
                        if (t is DropDownList)
                        {
                            dropDownList.Items.Add(item);
                        }
                        else if (t is RadioButtonList)
                        {
                            radioList.Items.Add(item);
                        }
                        else if (t is ListBox)
                        {
                            lstList.Items.Add(item);
                        }
                        else if (t is CheckBoxList)
                        {
                            chkList.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Utils.GetLookUpData", ex);

            }
            finally
            {
                if (null != reader) { reader.Close(); }
            }




        }



        /// <summary>
        /// Wajid Shah
        /// Clear page controls
        /// </summary>
        /// <param name="Page"></param>
        public static void ClearPageControls(Control Page)
        {

            foreach (Control ctrl in Page.Controls)
            {
                if (ctrl is HiddenField)
                {
                    ((HiddenField)(ctrl)).Value = "";

                }
                if (ctrl is DropDownList)
                {
                    DropDownList lst = (DropDownList)ctrl;
                    if (lst.ID != "ddlProptype" && lst.ID != "ddlPropertyType")
                    {
                        if (lst.Items.Count > 0)
                            ((DropDownList)(ctrl)).SelectedIndex = 0;
                    }
                }
                else if (ctrl is TextBox)
                {
                    ((TextBox)(ctrl)).Text = "";
                }
                else if (ctrl is CheckBox)
                {
                    ((CheckBox)(ctrl)).Checked = false;
                }
                else if (ctrl is CheckBoxList)
                {
                    CheckBoxList Cblist = ((CheckBoxList)(ctrl));
                    for (int i = 0; i < Cblist.Items.Count; i++)
                    {
                        Cblist.Items[i].Selected = false;

                    }
                }
                else if (ctrl is ListBox)
                {
                    ListBox listBox = ((ListBox)(ctrl));
                    if (listBox.ID != "lstMarket")
                    {
                        for (int i = 0; i < listBox.Items.Count; i++)
                        {
                            listBox.Items[i].Selected = false;

                        }
                    }
                }
                else if (ctrl is RadioButton)
                {
                    ((RadioButton)(ctrl)).Checked = false;
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        ClearPageControls(ctrl);
                    }

                }

            }

        }

        public static void ClearTenantSearchPageControls(Control Page)
        {

            foreach (Control ctrl in Page.Controls)
            {
                if (ctrl is HiddenField)
                {
                    ((HiddenField)(ctrl)).Value = "";

                }
                if (ctrl is DropDownList)
                {
                    DropDownList lst = (DropDownList)ctrl;
                    if (lst.ID != "ddlProptype")
                    {
                        if (lst.Items.Count > 0)
                            ((DropDownList)(ctrl)).SelectedIndex = 0;
                    }

                }
                else if (ctrl is TextBox)
                {
                    ((TextBox)(ctrl)).Text = "";
                }
                else if (ctrl is CheckBox)
                {
                    ((CheckBox)(ctrl)).Checked = false;
                }
                else if (ctrl is CheckBoxList)
                {
                    CheckBoxList Cblist = ((CheckBoxList)(ctrl));
                    for (int i = 0; i < Cblist.Items.Count; i++)
                    {
                        Cblist.Items[i].Selected = false;

                    }
                }
                else if (ctrl is ListBox)
                {
                    ListBox listBox = ((ListBox)(ctrl));
                    if (listBox.ID != "lstMarket" && listBox.ID != "lstSubMarket")
                    {
                        for (int i = 0; i < listBox.Items.Count; i++)
                        {
                            listBox.Items[i].Selected = false;

                        }
                    }


                }
                else if (ctrl is RadioButton)
                {
                    ((RadioButton)(ctrl)).Checked = false;
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        ClearTenantSearchPageControls(ctrl);
                    }

                }

            }

        }


        public static string GetMediaAgeString(DateTime MediaDate)
        {
            string returnStr = "";
            TimeSpan dateDiff = DateTime.Now.Subtract(MediaDate);
            int mediaAgeMinutes = Convert.ToInt32(dateDiff.TotalMinutes);

            if (mediaAgeMinutes < 60)
            {
                if (mediaAgeMinutes <= 1)
                    returnStr = "1 minute ago";
                else
                    returnStr = mediaAgeMinutes.ToString() + " minutes ago";
            }
            else if (mediaAgeMinutes < 1440)
            {
                int hoursAgo = Convert.ToInt32(Math.Floor(Convert.ToDecimal(mediaAgeMinutes) / 60));
                if (hoursAgo == 1)
                    returnStr = "1 hour ago";
                else
                    returnStr = hoursAgo.ToString() + " hours ago";
            }
            else
            {
                int daysAgo = Convert.ToInt32(Math.Floor(Convert.ToDecimal(mediaAgeMinutes) / 1440));
                if (daysAgo == 1)
                    returnStr = "1 day ago";
                else if (daysAgo <= 7)
                    returnStr = daysAgo.ToString() + " days ago";
                else
                    returnStr = MediaDate.ToShortDateString();
            }
            return returnStr;
        }



        public static bool IsValidURL(string url)
        {
            string regExPattern = @"^^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";
            return Regex.IsMatch(url, regExPattern);
        }
        public static bool IsValidZipCode(string country, string zip)
        {
            // USA, Canda and UK required to enter postal code
            if (country == "US" || country == "CA" || country == "UK")
            {
                if (string.IsNullOrEmpty(zip))
                    return false;
                if (zip.Length >= 5 && zip.Length <= 10)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }




        public static string ConvertZipToState(string zip)
        {
            string[] temp = zip.Split('-');
            return ConvertZipToState(Convert.ToInt32(temp[0]));
        }

        public static string ConvertZipToState(int zip)
        {
            string state = "";
            if ((zip >= 600 && zip <= 799) || (zip >= 900 && zip <= 999)) // Puerto Rico (00600-00799 and 900--00999 ranges)
                state = "PR";
            else if (zip >= 800 && zip <= 899) // US Virgin Islands (00800-00899 range)            
                state = "VI";
            else if (zip >= 1000 && zip <= 2799) // Massachusetts (01000-02799 range)
                state = "MA";
            else if (zip >= 2800 && zip <= 2999) // Rhode Island (02800-02999 range)
                state = "RI";
            else if (zip >= 3000 && zip <= 3899) // New Hampshire (03000-03899 range)
                state = "NH";
            else if (zip >= 3900 && zip <= 4999) // Maine (03900-04999 range)
                state = "ME";
            else if (zip >= 5000 && zip <= 5999) // Vermont (05000-05999 range)
                state = "VT";
            else if ((zip >= 6000 && zip <= 6999) && zip != 6390) // Connecticut (06000-06999 range excluding 6390)
                state = "CT";
            else if (zip >= 70000 && zip <= 8999) // New Jersey (07000-08999 range)
                state = "NJ";
            else if ((zip >= 10000 && zip <= 14999) || zip == 6390 || zip == 501 || zip == 544) // New York (10000-14999 range and 6390, 501, 544)
                state = "NY";
            else if (zip >= 15000 && zip <= 19699) // Pennsylvania (15000-19699 range)
                state = "PA";
            else if (zip >= 19700 && zip <= 19999) // Delaware (19700-19999 range)
                state = "DE";
            else if ((zip >= 20000 && zip <= 20099) || (zip >= 20200 && zip <= 20599) || (zip >= 56900 && zip <= 56999)) // District of Columbia (20000-20099, 20200-20599, and 56900-56999 ranges)
                state = "DC";
            else if (zip >= 20600 && zip <= 21999) // Maryland (20600-21999 range)            
                state = "MD";
            else if ((zip >= 20100 && zip <= 20199) || (zip >= 22000 && zip <= 24699)) // Virginia (20100-20199 and 22000-24699 ranges, also some taken from 20000-20099 DC range)
                state = "VA";
            else if (zip >= 24700 && zip <= 26999) // West Virginia (24700-26999 range)
                state = "WV";
            else if (zip >= 27000 && zip <= 28999) // North Carolina (27000-28999 range)
                state = "NC";
            else if (zip >= 29000 && zip <= 29999) // South Carolina (29000-29999 range)            
                state = "SC";
            else if ((zip >= 30000 && zip <= 31999) || (zip >= 39800 && zip <= 39999)) // Georgia (30000-31999, 39901[Atlanta] range)
                state = "GA";
            else if (zip >= 32000 && zip <= 34999) // Florida (32000-34999 range)
                state = "FL";
            else if (zip >= 35000 && zip <= 36999) // Alabama (35000-36999 range)
                state = "AL";
            else if (zip >= 37000 && zip <= 38599) // Tennessee (37000-38599 range)
                state = "TN";
            else if (zip >= 38600 && zip <= 39799) // Mississippi (38600-39999 range)
                state = "MS";
            else if (zip >= 40000 && zip <= 42799) // Kentucky (40000-42799 range)
                state = "KY";
            else if (zip >= 43000 && zip <= 45999) // Ohio (43000-45999 range)
                state = "OH";
            else if (zip >= 46000 && zip <= 47999) // Indiana (46000-47999 range)
                state = "IN";
            else if (zip >= 48000 && zip <= 49999) // Michigan (48000-49999 range)
                state = "MI";
            else if (zip >= 50000 && zip <= 52999) // Iowa (50000-52999 range)
                state = "IA";
            else if (zip >= 53000 && zip <= 54999) // Wisconsin (53000-54999 range)
                state = "WI";
            else if (zip >= 55000 && zip <= 56799) // Minnesota (55000-56799 range)
                state = "MN";
            else if (zip >= 57000 && zip <= 57999) // South Dakota (57000-57999 range)
                state = "SD";
            else if (zip >= 58000 && zip <= 58999) // North Dakota (58000-58999 range)
                state = "ND";
            else if (zip >= 59000 && zip <= 59999) // Montana (59000-59999 range)
                state = "MT";
            else if (zip >= 60000 && zip <= 62999) // Illinois (60000-62999 range)
                state = "IL";
            else if (zip >= 63000 && zip <= 65999) // Missouri (63000-65999 range)
                state = "MO";
            else if (zip >= 66000 && zip <= 67999) // Kansas (66000-67999 range)
                state = "KS";
            else if (zip >= 68000 && zip <= 69999) // Nebraska (68000-69999 range)
                state = "NE";
            else if (zip >= 70000 && zip <= 71599) // Louisiana (70000-71599 range)
                state = "LA";
            else if (zip >= 71600 && zip <= 72999) // Arkansas (71600-72999 range)
                state = "AR";
            else if (zip >= 73000 && zip <= 74999) // Oklahoma (73000-74999 range)
                state = "OK";
            else if ((zip >= 75000 && zip <= 79999) || (zip >= 88500 && zip <= 88599)) // Texas (75000-79999 and 88500-88599 ranges)
                state = "TX";
            else if (zip >= 80000 && zip <= 81999) // Colorado (80000-81999 range)
                state = "CO";
            else if (zip >= 82000 && zip <= 83199) // Wyoming (82000-83199 range)
                state = "WY";
            else if (zip >= 83200 && zip <= 83999) // Idaho (83200-83999 range)
                state = "ID";
            else if (zip >= 84000 && zip <= 84999) // Utah (84000-84999 range)
                state = "UT";
            else if (zip >= 85000 && zip <= 86999) // Arizona (85000-86999 range)
                state = "AZ";
            else if (zip >= 87000 && zip <= 88499) // New Mexico (87000-88499 range)
                state = "NM";
            else if (zip >= 88900 && zip <= 89999) // Nevada (88900-89999 range)
                state = "NV";
            else if (zip >= 90000 && zip <= 96199) // California (90000-96199 range)
                state = "CA";
            else if (zip >= 96700 && zip <= 96899) // Hawaii (96700-96899 range)  
                state = "HI";
            else if (zip >= 97000 && zip <= 97999) // Oregon (97000-97999 range)
                state = "OR";
            else if (zip >= 98000 && zip <= 99499) // Washington (98000-99499 range)
                state = "WA";
            else if (zip >= 99500 && zip <= 99999) // Alaska (99500-99999 range)
                state = "AK";
            return state;
        }



        public static DataRow GetCurrentRow(Repeater sender, RepeaterItemEventArgs e)
        {
            return ((DataTable)(sender.DataSource)).Rows[e.Item.ItemIndex];
        }

        public static string Ellipses(string input, int cutoff)
        {
            if (cutoff < 1 || input.Length <= cutoff)
                return input;
            else
                return input.Substring(0, cutoff) + "...";
        }

        public static string SetPagingQueryString(string pageName)
        {
            string url = pageName.Substring(pageName.LastIndexOf("/") + 1);
            if (url.IndexOf("?") > 0)
            {
                url = url.Substring(0, url.IndexOf("?"));
            }
            return url + "?i=";
        }

        public static string GetPagingString(int curPage, int pageSize, int rowCount, string url, bool IsAjaxCall)
        {
            StringBuilder sb = new StringBuilder(200);
            int startPage, endPage;
            // Previous 
            if (curPage > 1)
            {
                if (IsAjaxCall)
                    LoadPageForAjaxCall(sb, curPage, curPage - 1, "<<", url);
                else
                    LoadPage(sb, curPage, curPage - 1, "<<", url);
                //"<img src=/image/nav/arrow_left.gif border=0 align=absmiddle>", url);
                //LoadPage(sb, curPage, curPage - 1, "Prev", url); 
            }
            // Calculate page range 
            if (curPage <= 6)
                startPage = 1;
            else
                startPage = curPage - 5;
            endPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(rowCount) / Convert.ToDecimal(pageSize)));
            // Load pages 
            if (endPage > 1)
            {
                while (startPage <= endPage)
                {
                    if (IsAjaxCall)
                        LoadPageForAjaxCall(sb, curPage, startPage, "", url);
                    else
                        LoadPage(sb, curPage, startPage, "", url);
                    startPage++;
                }
            }
            else if (endPage == 1)
            {
                if (IsAjaxCall)
                    LoadPageForAjaxCall(sb, curPage, startPage, "", "");
                else
                    LoadPage(sb, curPage, startPage, "", "");
            }
            // Next 
            if (endPage > curPage)
            {
                //LoadPage(sb, curPage, curPage + 1, "Next", url); 
                if (IsAjaxCall)
                    LoadPageForAjaxCall(sb, curPage, curPage + 1, ">>", url);
                else
                    LoadPage(sb, curPage, curPage + 1, ">>", url);
                //"<img src=/image/nav/arrow_right.gif border=0 align=absmiddle>", url);
            }
            return sb.ToString();
        }
        private static void LoadPage(StringBuilder sb, int curPage, int PageNum, string Caption, string url)
        {
            sb.Append(
            "&nbsp;&nbsp;");
            if (PageNum == curPage)
            {
                sb.Append(
                "<span class='spanPaging'><b>");
                sb.Append(PageNum.ToString());
                sb.Append(
                "</b></span>");
            }
            else
            {
                sb.Append(
                @"<a href=""");
                //sb.Append("javascript:ListPaging("+PageNum+");");
                sb.Append(url);
                if (url.IndexOf("?") > 0)
                    sb.Append(
                    "&p=");
                else
                    sb.Append(
                    "?p=");
                sb.Append(PageNum.ToString());
                sb.Append(
                @""">");
                if (Caption.Length > 0)
                    sb.Append(Caption);
                else
                    sb.Append(PageNum.ToString());
                sb.Append(
                "</a>");
            }
        }
        private static void LoadPageForAjaxCall(StringBuilder sb, int curPage, int PageNum, string Caption, string url)
        {
            sb.Append(
            "&nbsp;&nbsp;");
            if (PageNum == curPage)
            {
                sb.Append(
                "<span class='spanPaging'><b>");
                sb.Append(PageNum.ToString());
                sb.Append(
                "</b></span>");
            }
            else
            {
                sb.Append(
                @"<a href=""");
                sb.Append("javascript:ListPaging(" + PageNum + ");");
                //sb.Append(url);
                //if (url.IndexOf("?") > 0)
                //    sb.Append(
                //    "&p=");
                //else
                //    sb.Append(
                //    "?p=");
                //sb.Append(PageNum.ToString());
                sb.Append(
                @""">");
                if (Caption.Length > 0)
                    sb.Append(Caption);
                else
                    sb.Append(PageNum.ToString());
                sb.Append(
                "</a>");
            }
        }

        public static void ShowResults(System.Web.UI.MasterPage page, string message, bool blnError)
        {
            StringBuilder msg = new StringBuilder();

            if (blnError)
            {
                msg.Append("<table width=792 border=1 cellspacing=1 cellpadding=1 bgcolor=#CCCCCC align=left><tr bgcolor=\"#EEEEEE\"><td><table width=100% border=0 cellspacing=0 cellpadding=0 align=left><tr><td width=50 align=center valign=top></td><td valign=middle><b><font color=#FF0000>");
                msg.Append(message);
                msg.Append("</font></b></td></tr></table></td></tr></table>");
            }
            else
            {
                msg.Append("<table width=792 border=0 cellspacing=1 cellpadding=10 bgcolor=#92B0DD align=left><tr bgcolor=\"#E2EAF8\"><td><table width=100% border=0 cellspacing=0 cellpadding=0 align=center><tr><td width=50 align=center valign=top></td><td valign=middle><b>");
                msg.Append(message);
                msg.Append("</b></td></tr></table></td></tr></table>");
            }

            ((Panel)page.FindControl("pnlResults")).Visible = true;
            if (blnError)
                ((Literal)page.FindControl("sResults")).Text = msg.ToString();
            else
                ((Literal)page.FindControl("sResults")).Text = msg.ToString();


        }

        public static string GetMD5Hash(string filename)
        {
            StringBuilder sBuilder = new StringBuilder();
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                try
                {
                    byte[] data = md5Hasher.ComputeHash(fs);
                    for (int i = 0; i < data.Length; i++)
                        sBuilder.Append(data[i].ToString("x2"));
                }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            return sBuilder.ToString();
        }


        public static string GetMediaPlayer(string videoFile)
        {
            StringBuilder sb = new StringBuilder(4096);
            sb.Append("<object ID=WMPlay width=461 height=380 classid=\"CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6\" codebase=\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701\"");
            sb.Append("<PARAM name=URL value=\"" + videoFile + "\"");
            sb.Append("<PARAM name=AllowChangeDisplaySize value=True>");
            sb.Append("<PARAM NAME=ShowControls VALUE=1>");
            sb.Append("<PARAM NAME=ShowDisplay VALUE=1>");
            sb.Append("<PARAM NAME=ShowStatusBar VALUE=1>");
            sb.Append("<PARAM NAME=AutoStart VALUE=TRUE>");
            sb.Append("<embed name=WMplay width=461 height=380 type=\"application/x-mplayer2\" pluginspage=\"http://www.microsoft.com/Windows/Downloads/Contents/Products/MediaPlayer/\"");
            sb.Append("src=\"" + videoFile + "\" AutoStart=True></embed>");
            sb.Append("</object>");
            return sb.ToString();
        }


        public static string GetPlayerCode(string path)
        {
            StringBuilder sb = new StringBuilder(4096);
            sb.Append("<OBJECT  classid=\"clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95\" VIEWASTEXT height=\"350\" width=\"400\">");
            sb.Append("<PARAM NAME=\"AutoStart\" VALUE=\"false\">");
            sb.Append("<PARAM NAME=\"Filename\" VALUE=\"" + path + "\">");
            sb.Append("<PARAM NAME=\"ShowStatusBar\" VALUE=\"true\">");
            sb.Append("<PARAM NAME=\"EnableContextMenu\" VALUE=\"false\">");
            sb.Append("<PARAM NAME=\"Loop\" VALUE=\"false\">");
            sb.Append("<EMBED SRC=\"" + path + "\" WIDTH=\"460\" HEIGHT=\"350\"  AutoStart=\"false\"  AUTOPLAY=\"false\" CONTROLLER=\"true\" ");
            sb.Append("	KIOSKMODE=\"true\" type=\"application/x-mplayer2\" PLUGINSPAGE=\"http://www.microsoft.com/windows/mediaplayer/\"> </EMBED>");
            sb.Append("</OBJECT>");
            return sb.ToString();
        }


        public static string GetPlayerCodeUsingExtension(string path, string extension)
        {
            StringBuilder sb = new StringBuilder(4096);
            extension = extension.ToLower();
            switch (extension)
            {
                case ".wmv": sb.Append("<OBJECT  classid=\"clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95\" VIEWASTEXT height=\"350\" width=\"400\">"); break;
                case ".mov": sb.Append("<OBJECT  classid=\"clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B\" codebase=\"http://www.apple.com/qtactivex/qtplugin.cab\" height=\"350\" width=\"400\">"); break;

            }


            sb.Append("<PARAM NAME=\"AutoStart\" VALUE=\"false\">");
            sb.Append("<PARAM NAME=\"SRC\" VALUE=\"" + path + "\">");
            sb.Append("<PARAM NAME=\"ShowStatusBar\" VALUE=\"true\">");
            sb.Append("<PARAM NAME=\"EnableContextMenu\" VALUE=\"false\">");
            sb.Append("<PARAM NAME=\"Loop\" VALUE=\"false\">");

            switch (extension)
            {
                case ".wmv": sb.Append("<EMBED SRC=\"" + path + "\" WIDTH=\"400\" HEIGHT=\"350\"  AutoStart=\"false\"  AUTOPLAY=\"false\" CONTROLLER=\"true\" ");
                    sb.Append("	KIOSKMODE=\"true\" type=\"application/x-mplayer2\" PLUGINSPAGE=\"http://www.microsoft.com/windows/mediaplayer/\"> </EMBED>"); break;
                case ".mov": sb.Append("<EMBED SRC=\"" + path + "\" WIDTH=\"400\" HEIGHT=\"350\"  AutoStart=\"false\"  AUTOPLAY=\"false\" CONTROLLER=\"true\" ");
                    sb.Append(" PLUGINSPAGE=\"http://www.apple.com/quicktime/download/\"> </EMBED>"); break;

            }


            sb.Append("</OBJECT>");
            return sb.ToString();
        }

        public static bool IsValidFileExtension(string extension)
        {
            string[] allowed_extensions = ConfigurationManager.AppSettings["PhotoExtensions"].Split(new char[] { ',' });
            extension = extension.ToLower().Trim();
            for (int i = 0; i < allowed_extensions.Length; i++)
            {
                if (allowed_extensions[i].Equals(extension)) return true;
            }
            return false;
        }

        public static bool IsCrypId(string test)
        {
            if (string.IsNullOrEmpty(test))
                return false;
            else if (test.Length < 32)
                return false;
            else
                return IsAlphaNumeric(test);
        }

        public static bool IsNumeric(char cToTest)
        {
            bool bNumeric = false;
            Regex regexNumeric = new Regex("[^0-9]");
            bNumeric = regexNumeric.IsMatch(cToTest.ToString());
            return !bNumeric;
        }
        public static bool IsNumeric(string sToTest)
        {
            Regex regexNumeric = new Regex(@"^\d+$");
            Match m = regexNumeric.Match(sToTest);
            return m.Success;
        }

        public static bool IsAlpha(char cToTest)
        {
            bool bAlpha = false;
            Regex regexAlpha = new Regex("[^a-zA-Z]");
            bAlpha = regexAlpha.IsMatch(cToTest.ToString());
            return !bAlpha;
        }

        public static bool IsAlphaNumeric(char cToTest)
        {
            bool bAlpha = false;
            Regex regexAlpha = new Regex("[^a-zA-Z0-9]");
            bAlpha = regexAlpha.IsMatch(cToTest.ToString());
            return !bAlpha;
        }

        public static bool IsAlphaNumeric(string ToTest)
        {
            bool bAlpha = false;
            Regex regexAlpha = new Regex("[^a-zA-Z0-9]");
            bAlpha = regexAlpha.IsMatch(ToTest);
            return !bAlpha;
        }

        public static string filterembedCode(string input, int width, int height)
        {
            string chkEmbedCode = "(?i)<(object|embed)[=\"\'/0-9a-zA-Z#;:&-.?<>_ ]*(object|embed)>";
            bool isMatch = Regex.IsMatch(input, chkEmbedCode);
            Regex regex = new Regex(chkEmbedCode, RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(input);
            foreach (Match m in matches)
                input = m.Value.ToString();
            return ResetEmbedWidthHeight(input, width, height);
        }

        public static bool validateembedCode(string input, int width, int height)
        {
            string chkEmbedCode = "(?i)<(object|embed)[=\"\'/0-9a-zA-Z#;:&-.?<>_ ]*(object|embed)>";
            bool isMatch = Regex.IsMatch(input, chkEmbedCode);

            if (isMatch)
                return true;
            else
                return false;
        }


        public static string ResetEmbedWidthHeight(string input, int width, int height)
        {
            string exp = "(width|height)=[\"|\']?[0-9]+[\"|\']?[%|px]*[\"|\']?";
            Regex regex = new Regex(exp, RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(input);
            foreach (Match m in matches)
            {
                string s = string.Empty;
                if (m.Value.ToLower().IndexOf("height") > -1)
                {
                    s = m.Value.Substring(m.Value.IndexOf("=") + 1);
                    input = input.Replace(s, "\"" + height.ToString() + "\"");
                }
                else if (m.Value.ToLower().IndexOf("width") > -1)
                {
                    s = m.Value.Substring(m.Value.IndexOf("=") + 1);
                    input = input.Replace(s, "\"" + width.ToString() + "\"");
                }
            }
            return input;
        }

        public static bool IsSafari(string browser)
        {
            string checkBrowser = browser.ToLower();
            if (checkBrowser.IndexOf("safari") >= 0)
                return true;
            else
                return false;
        }
        public static string GetFileNameURL(string filename)
        {
            return GetFileNameURL(filename, false);
        }
        public static string GetFileNameURL(string filename, bool urlEncode)
        {
            // Allows word characters [A-Za-z0-9_], dash
            string regExPattern = @"^[A-Za-z0-9]";
            string RPLCHR = " - ";
            int maxFilename = 30;
            filename = filename.Trim();
            if (filename.Length > maxFilename)
                filename = filename.Substring(0, maxFilename);

            StringBuilder filenameURL = new StringBuilder();
            char[] chars = filename.ToCharArray();
            string prevChr = "";
            foreach (char c in chars)
            {
                string chr = c.ToString();
                if ((chr == RPLCHR && prevChr != RPLCHR) || Regex.IsMatch(chr, regExPattern))
                {
                    filenameURL.Append(c);
                }
                else
                {
                    if (prevChr != RPLCHR)
                        filenameURL.Append(RPLCHR);
                    chr = RPLCHR;
                }
                prevChr = chr;
            }

            if (urlEncode)
                return HttpUtility.UrlEncode(filenameURL.ToString().ToLower().Trim());
            else
                return filenameURL.ToString().ToLower().Trim();
        }
        public static string SafeHTML(string strText)
        {
            strText = strText.Replace("<script", "&lt;script");
            return strText;
        }

        public static StringBuilder CleanString(string strBuffer)
        {
            bool bTag = false;
            string strToken = "";
            StringBuilder sbClean = new StringBuilder();

            foreach (char cBuffer in strBuffer)
            {
                // Check whether a token is completed
                if (!IsAlphaNumeric(cBuffer))
                {

                    // Check whether the token is clean
                    sbClean.Append(GetClean(strToken, bTag));
                    sbClean.Append(cBuffer);
                    strToken = "";

                    switch (cBuffer)
                    {
                        case '<':
                            bTag = true;
                            break;
                        case '>':
                            bTag = false;
                            break;
                    }
                }
                else
                {

                    strToken += cBuffer;
                }
            }

            return sbClean;
        }
        public static StringBuilder GetClean(string strText, bool bTag)
        {
            StringBuilder sbClean = new StringBuilder();

            if (bTag)
            {
                bool bInvalidTag = false;
                foreach (string strTag in restrictedTags)
                {
                    if (0 == strTag.CompareTo(strText.ToLower()))
                    {
                        bInvalidTag = true;
                        break;
                    }
                }
                bTag = bInvalidTag;

                if (bTag)
                {
                    sbClean.Append("!-- RESTRICTED --");
                }
            }


            if (!bTag)
            {
                bool bClean = true;
                foreach (string strCurr in restrictedWords)
                {
                    if (0 == strCurr.CompareTo(strText.ToLower()))
                    {
                        bClean = false;
                        break;
                    }
                }
                if (!bClean)
                {
                    foreach (char cIndex in strText)
                    {
                        sbClean.Append('*');
                    }
                }
                else
                {
                    sbClean.Append(strText);
                }
            }

            return sbClean;
        }
        public static string CleanStringForSpecialCharacters(string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
                for (int x = 0; x < restrictedHitBoxCharacters.Length; x++)
                {
                    if (strInput.Contains(restrictedHitBoxCharacters[x]))
                        strInput = strInput.Replace(restrictedHitBoxCharacters[x], "");
                }
            return strInput;
        }
        /// <summary>
        /// wajid shah
        /// </summary>
        /// <param name="str">string from which we will remove special chars</param>
        /// <returns>clean string return</returns>
        public static string RemoveSpecialChars(string str)
        {
            string[] chars = new string[] { " ", "~", ",", ".", "/", "!", "+", "=", "?", ">", "<", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", " - ", "_", "(", ")", ":", "|", "[", "]" };
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }
        public static bool CheckBoolValue(object objvalue)
        {
            string blStringvalue = Convert.ToString(objvalue);
            bool blReturnValue = false;
            if (!string.IsNullOrEmpty(blStringvalue))
            {
                bool.TryParse(blStringvalue, out blReturnValue);
            }
            return blReturnValue;

        }

        public static string TruncateContent(string s, int lenToget)
        {
            if (lenToget >= s.Length || lenToget < 0)
            {
                return s;
            }
            while (s.Substring(0, lenToget).LastIndexOf(" ") != lenToget - 1)
            {
                lenToget++;
                if (s.Length == lenToget)
                {
                    break;
                }
            }
            return s.Substring(0, lenToget).Trim();
        }

        public static string TruncateWords(string text)
        {
            return TruncateWords(text, text.Length);
        }

        public static string TruncateWords(string text, int maxLen)
        {
            int i = 0, len = 0, lastI = 0;
            while (i < text.Length && (len = NextWord(text, len, ref i)) <= maxLen)
                lastI = i;
            if (lastI < 1) lastI = Math.Min(maxLen, text.Length);
            return text.Substring(0, lastI).Trim();
        }
        private static int NextWord(string text, int len, ref int pos)
        {
            int i = pos;
            while (i < text.Length && Char.IsWhiteSpace(text, i))
            {
                i++;
                len++;
            }
            if (!ParseLink(text, ref i, ref len))
            {
                while (i < text.Length && !IsLink(text, i) && !Char.IsWhiteSpace(text, i))
                {
                    i++;
                    len++;
                }
            }
            pos = i;
            return len;
        }
        private static bool IsLink(string text, int i)
        {

            if (i + 1 < text.Length)
            {
                return text[i] == '<' && i + 1 < text.Length && Char.ToLower(text[i + 1]) == 'a';
            }
            else
            {
                return false;
            }
        }
        private static bool ParseLink(string text, ref int i, ref int len)
        {
            if (IsLink(text, i))
            {
                int startText = text.IndexOf('>', i + 1) + 1;
                int end = text.ToLower().IndexOf("</a>", i + 1);
                if (end == -1)
                {
                    string innerText = text.Substring(startText).Trim();
                    len += innerText.Length;
                    i = end +
                    "</a>".Length;
                }
                if (startText >= 0 && end >= 0)
                {
                    string innerText = text.Substring(startText, end - startText).Trim();
                    len += innerText.Length;
                    i = end +
                    "</a>".Length;
                    return true;
                }
            }
            return false;
        }


        public static string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
        public static string FixNullString(Object objValue)
        {
            if (objValue != null && objValue.ToString().Equals("Null")) return "";
            if (System.Convert.ToString(objValue).Trim().Equals("")) return "";
            return System.Convert.ToString(objValue);
        }

        public static string UpdateWidthHeight(string input, string width, string Height)
        {
            string chkEmbedCode = "^<(object|embed)[.]*(object|embed)>$";
            bool isMatch = Regex.IsMatch(input, chkEmbedCode);

            string patternWidth = "(width)=[\"|\'|( )* ]?[0-9]+[\"|\'|( )*]?";
            string patternHeight = "(height)=[\"|\'|( )* ]?[0-9]+[\"|\'|( )*]?";

            MatchCollection mcWidth = Regex.Matches(input, patternWidth, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection mcHeight = Regex.Matches(input, patternHeight, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            foreach (Match match in mcWidth)
                input = input.Replace(match.Value.ToString(), "width=\"" + width + "\"");

            foreach (Match match in mcHeight)
                input = input.Replace(match.Value.ToString(), "height=\"" + Height + "\"");

            return input;
        }

        public static string CleanHTML(string input)
        {
            return Regex.Replace(input, @"</?(?i:script|div|frameset|frame|iframe|meta|link|style|body|BODY)(.|\n)*?>", "");//todo
        }
        public static string CleanBlogHTML(string input)
        {
            return Regex.Replace(input, @"</?(?i:script|div|frameset|frame|iframe|meta|link|style|body|BODY|P|p)(.|\n)*?>", "");
        }
        public static string CleanVideoHTML(string input)
        {
            return Regex.Replace(input, @"</?(?i:script|SCRIPT|DIV|div|frameset|frame|iframe|meta|link|style|body|BODY|P|p|SPAN|span)(.|\n)*?>", "");
        }


        public static void SaveReferalSource(string source)
        {
            HttpCookie ck = new HttpCookie("refSource", source);
            HttpContext.Current.Response.Cookies.Add(ck); ;
            ck.Expires = DateTime.Now.AddDays(1);
        }

        public static string GetReferalSouce()
        {
            HttpCookie cookies = HttpContext.Current.Request.Cookies.Get("refSource");
            if (cookies != null)
                return cookies.Value;
            else
                return "";

        }



        public static string GetMonthName(int monthNum)
        {
            DateTime date = new DateTime(1, monthNum, 1);
            return date.ToString("MMM");

        }



        public static string RemoveHTML(string body)
        {
            return Regex.Replace(body, @"<(.|\n)*?>", "");
        }
        private Bitmap CropImage(Bitmap orignalImage, Point topleft, Point bottomRight)
        {
            Bitmap croppedImage = new Bitmap((bottomRight.Y - topleft.Y), (bottomRight.X - topleft.X));
            Graphics g = Graphics.FromImage(croppedImage);
            g.DrawImage(orignalImage, new Rectangle(0, 0, croppedImage.Width, croppedImage.Height), topleft.X, topleft.Y,
                                                                    croppedImage.Width, croppedImage.Height, GraphicsUnit.Pixel);

            g.Dispose();
            return croppedImage;
        }
        public static ArrayList ParseLinks(string HTML)
        {

            System.Text.RegularExpressions.Regex objRegEx = default(System.Text.RegularExpressions.Regex);
            System.Text.RegularExpressions.Match objMatch = default(System.Text.RegularExpressions.Match);
            System.Collections.ArrayList arrLinks = new System.Collections.ArrayList();
            objRegEx = new System.Text.RegularExpressions.Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);
            objMatch = objRegEx.Match(HTML);
            while (objMatch.Success)
            {
                string strMatch = null;
                strMatch = objMatch.Groups[1].ToString();
                arrLinks.Add(strMatch);
                objMatch = objMatch.NextMatch();
            }
            return arrLinks;
        }

        public static Hashtable GetEnumForBind(Type enumeration)
        {
            string[] names = Enum.GetNames(enumeration);
            Array values = Enum.GetValues(enumeration);
            Hashtable ht = new Hashtable();


            for (int i = 0; i < names.Length; i++)
            {
                ht.Add(Convert.ToInt32(values.GetValue(i)).ToString(), names[i]);
            }
            return ht;
        }


        public static bool IsValidExtension(string fileName)
        {
            bool isValid = false;
            string[] extensions = new string[] { ".gif", ".jpg", ".jpeg", ".tiff", ".bmp", ".png" };
            if (!string.IsNullOrEmpty(fileName))
            {
                string ext = System.IO.Path.GetExtension(fileName);
                foreach (string s in extensions)
                {
                    if (ext.ToLower() == s)
                    {
                        isValid = true;
                        break;
                    }
                }
            }
            return isValid;
        }
        public static bool IsValidExtension(string fileName, bool isTeamApplication)
        {
            bool isValid = false;
            string[] extensions = new string[] { ".gif", ".jpg", ".jpeg", ".tiff", ".bmp", ".png", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".csv", ".ppt", ".pptx", ".txt", ".rtf" };
            if (!string.IsNullOrEmpty(fileName))
            {
                string ext = System.IO.Path.GetExtension(fileName);
                foreach (string s in extensions)
                {
                    if (ext.ToLower() == s)
                    {
                        isValid = true;
                        break;
                    }
                }
            }
            return isValid;
        }
        public static bool IsValidExtensionFlash(string fileName)
        {

            bool isValid = false;
            string[] extensions = new string[] { ".flv", ".swf" };
            if (!string.IsNullOrEmpty(fileName))
            {
                string ext = System.IO.Path.GetExtension(fileName);
                foreach (string s in extensions)
                {
                    if (ext.ToLower() == s)
                    {
                        isValid = true;
                        break;
                    }
                }
            }
            return isValid;
        }

        public static string GetSelectedItemsValueList(ref CheckBoxList CheckBoxList)
        {
            string list = string.Empty;
            if (CheckBoxList != null && CheckBoxList.Items.Count > 0)
            {
                foreach (ListItem item in CheckBoxList.Items)
                {
                    if (item.Selected)
                    {
                        if (!string.IsNullOrEmpty(list))
                        {
                            list += ",";
                        }
                        list += item.Value;
                    }
                }
            }
            if (!string.IsNullOrEmpty(list))
            {
                list = list.Trim();
            }
            return list;
        }









        /// <summary>
        /// use to update the query string for integer value
        /// </summary>
        /// </summary>
        /// <param name="query">url</param>
        /// <param name="valToUpdate">name of parameter</param>
        /// <param name="val">value of parameter</param>
        /// <returns></returns>
        public static string UpdateQuerystringForInt(string query, string valToUpdate, int val)
        {
            string url = query;
            url = System.Text.RegularExpressions.Regex.Replace(url, "(&" + valToUpdate + "=[0-9]+)", "&" + valToUpdate + "=" + val);
            url = System.Text.RegularExpressions.Regex.Replace(url, "(\\?" + valToUpdate + "=[0-9]+)", "?" + valToUpdate + "=" + val);
            return url;
        }

        /// <summary>
        /// use to update the query string for string value
        /// </summary>
        /// <param name="query">url</param>
        /// <param name="valToUpdate">name of parameter</param>
        /// <param name="val">value of parameter</param>
        /// <returns></returns>
        public static string UpdateQuerystringForString(string query, string valToUpdate, string val)
        {
            string url = query;

            url = System.Text.RegularExpressions.Regex.Replace(url, "(&" + valToUpdate + "=[a-z]+)", "&" + valToUpdate + "=" + val);
            url = System.Text.RegularExpressions.Regex.Replace(url, "(\\?" + valToUpdate + "=[a-z]+)", "?" + valToUpdate + "=" + val);
            url = System.Text.RegularExpressions.Regex.Replace(url, "(&" + valToUpdate + "=+)", "&" + valToUpdate + "=" + val);
            url = System.Text.RegularExpressions.Regex.Replace(url, "(\\?" + valToUpdate + "=+)", "?" + valToUpdate + "=" + val);
            return url;
        }

        /// <summary>
        /// use to delete the integer paramter from query string for 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="valToUpdate"></param>
        /// <returns></returns>
        public static string DeleteQuerystringForIntParam(string query, string valToUpdate)
        {
            string url = query;

            url = System.Text.RegularExpressions.Regex.Replace(url, "(&" + valToUpdate + "=[0-9]+)", "");
            url = System.Text.RegularExpressions.Regex.Replace(url, "(\\?" + valToUpdate + "=[0-9]+)", "");
            url = System.Text.RegularExpressions.Regex.Replace(url, "(&" + valToUpdate + "=+)", "");
            url = System.Text.RegularExpressions.Regex.Replace(url, "(\\?" + valToUpdate + "=+&)", "?");
            return url;
        }

        /// <summary>
        /// use to delete the string paramter from query string for 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="valToUpdate"></param>
        /// <returns></returns>
        public static string DeleteQuerystringForStringParam(string query, string valToUpdate)
        {
            string url = query;

            url = System.Text.RegularExpressions.Regex.Replace(url, "(&" + valToUpdate + "=[a-z]+)", "");
            url = System.Text.RegularExpressions.Regex.Replace(url, "(\\?" + valToUpdate + "=[a-z]+)", "");
            url = System.Text.RegularExpressions.Regex.Replace(url, "(&" + valToUpdate + "=+)", "");
            url = System.Text.RegularExpressions.Regex.Replace(url, "(\\?" + valToUpdate + "=+&)", "?");
            return url;
        }
        /// <summary>
        /// use to delete the string paramter from query string for 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="valToUpdate"></param>
        /// <returns></returns>
        public static string DeleteQuerystringForEmptyParam(string query, string valToUpdate)
        {
            string url = query;
            url = System.Text.RegularExpressions.Regex.Replace(url, "(&" + valToUpdate + "=+)", "");
            url = System.Text.RegularExpressions.Regex.Replace(url, "(\\?" + valToUpdate + "=+)", "");
            return url;
        }
        /// <summary>
        /// use to update the query string for string value
        /// </summary>
        /// <param name="query">url</param>
        /// <param name="valToUpdate">name of parameter</param>
        /// <param name="val">value of parameter</param>
        /// <returns></returns>
        public static string AddStringParameterToQuerystring(string query, string valToAdd, string val)
        {
            string url = query;
            if (query.IndexOf(valToAdd) > 0)
            {
                url = UpdateQuerystringForString(query, valToAdd, HttpContext.Current.Server.UrlEncode(val));

            }
            else
            {
                if (query.IndexOf("?") > 0)
                    url = query + "&" + valToAdd + "=" + HttpContext.Current.Server.UrlEncode(val);
                else
                    url = query + "?" + valToAdd + "=" + HttpContext.Current.Server.UrlEncode(val);
            }
            return url;
        }
        /// <summary>
        /// use to update the query string for string value
        /// </summary>
        /// <param name="query">url</param>
        /// <param name="valToUpdate">name of parameter</param>
        /// <param name="val">value of parameter</param>
        /// <returns></returns>
        public static string AddIntParameterToQuerystring(string query, string valToAdd, int val)
        {
            string url = query;
            if (query.IndexOf(valToAdd) > 0)
            {
                url = UpdateQuerystringForInt(query, valToAdd, val);
            }
            else
            {
                if (query.IndexOf("?") > 0)
                    url = query + "&" + valToAdd + "=" + val;
                else
                    url = query + "?" + valToAdd + "=" + val;
            }
            return url;
        }



        public static void LoadPager(int PageNumber, int iTotalRecords, ImageButton imgNext, ImageButton imgprevious, Label lblStart, Label lblEnd)
        {
            try
            {
                LoadPager(PageNumber, iTotalRecords, imgNext, imgprevious, lblStart, lblEnd, 10);
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Utils.LoadPager", ex);
            }
        }
        public static void LoadPager(int PageNumber, int iTotalRecords, DropDownList ddPageNumbers, ImageButton imgNext, ImageButton imgprevious, Label lblStart, Label lblEnd)
        {
            try
            {
                LoadPager(PageNumber, iTotalRecords, ddPageNumbers, imgNext, imgprevious, lblStart, lblEnd, 10);
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Utils.LoadPager", ex);
            }
        }
        public static void LoadPager(int PageNumber, int iTotalRecords, DropDownList ddPageNumbers, ImageButton imgNext, ImageButton imgprevious, Label lblStart, Label lblEnd, int PageSize)
        {
            if (PageNumber == 1)
                imgprevious.Enabled = false;
            else
                imgprevious.Enabled = true;

            double dTotalPages = Math.Ceiling((double)iTotalRecords / PageSize);
            if (PageNumber >= dTotalPages)
                imgNext.Enabled = false;
            else
                imgNext.Enabled = true;

            lblStart.Text = PageNumber.ToString();
            lblEnd.Text = dTotalPages.ToString();

            ddPageNumbers.Items.Clear();

            for (int i = 0; i < Conversion.ParseInt(dTotalPages.ToString()); i++)
            {
                ddPageNumbers.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
            }

            if (ddPageNumbers.Items.Count > 0)
                ddPageNumbers.Items.FindByValue(PageNumber.ToString()).Selected = true;
        }
        public static void LoadPager(int PageNumber, int iTotalRecords, ImageButton imgNext, ImageButton imgprevious, Label lblStart, Label lblEnd, int PageSize)
        {
            if (PageNumber == 1)
                imgprevious.Enabled = false;
            else
                imgprevious.Enabled = true;

            double dTotalPages = Math.Ceiling((double)iTotalRecords / PageSize);
            if (PageNumber >= dTotalPages)
                imgNext.Enabled = false;
            else
                imgNext.Enabled = true;

            lblStart.Text = PageNumber.ToString();
            lblEnd.Text = dTotalPages.ToString();

        }

        /// Recursive FindControl method, to search a control and all child
        /// controls for a control with the specified ID.
        /// </summary>
        /// <returns>Control if found or null</returns>
        public static Control FindControlRecursive(Control root, string id)
        {
            if (id == string.Empty)
                return null;

            if (root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }
        /// <summary>
        /// traverse all controls of type listbox or dropdownlist on a page and save values in session
        /// </summary>
        /// <param name="ctl"></param>
        public static void TraverseAndSetSessionValues(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {
                //System.Diagnostics.Debug.WriteLine(c.GetType().ToString());
                //Response.Write(c.GetType().ToString() + " : " + c.ID.ToString() + "<br />"); 

                if (c.GetType() == typeof(DropDownList))
                {
                    string controlID = ((DropDownList)(c)).ID;
                    HttpContext.Current.Session[controlID] = ((DropDownList)(c)).SelectedItem;
                }

                else if (c.GetType() == typeof(ListBox))
                {
                    string controlID = ((ListBox)(c)).ID;
                    HttpContext.Current.Session[controlID] = Utils.GetMultipleSelectedValueInListBox(((ListBox)(c)));
                }
                TraverseAndSetSessionValues(c);
            }
        }

        public static void TraverseAndGetSessionValues(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {
                //System.Diagnostics.Debug.WriteLine(c.GetType().ToString());
                //Response.Write(c.GetType().ToString() + " : " + c.ID.ToString() + "<br />"); 

                if (c.GetType() == typeof(DropDownList))
                {
                    DropDownList ddl = (DropDownList)(c);
                    string controlID = ddl.ID;
                    if (HttpContext.Current.Session[controlID] != null)
                    {
                        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(HttpContext.Current.Session[controlID].ToString()));
                    }

                }

                else if (c.GetType() == typeof(ListBox))
                {
                    string controlID = ((ListBox)(c)).ID;
                    if (HttpContext.Current.Session[controlID] != null)
                    {
                        Utils.MultipleSelectInListBox(((ListBox)(c)), HttpContext.Current.Session[controlID].ToString());
                    }
                }
                TraverseAndGetSessionValues(c);
            }
        }

        public static Bitmap WaterMarkToImage(System.Drawing.Image image, string watermark)
        {
            int opacity = 127; // 1  to 255
            Color myWaterMarkColor = Color.Blue;
            Brush wmBrush = new SolidBrush(Color.FromArgb(opacity, myWaterMarkColor));

            //FileStream fs = new FileStream(ImagePath.ToLower(), FileMode.Open, FileAccess.Read, FileShare.Read);
            Bitmap bmp;
            bmp = new Bitmap(image);
            //fs.Close();
            Graphics graphicsObject;
            int x, y;
            try
            {
                //create graphics object from bitmap
                graphicsObject = Graphics.FromImage(bmp);
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Utils.WaterMarkToImage", ex);
                Bitmap bmpNew = new Bitmap(bmp.Width, bmp.Height);
                graphicsObject = Graphics.FromImage(bmpNew);

                graphicsObject.DrawImage(bmp, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel);

                bmp = bmpNew;
            }

            int startsize = (bmp.Width / watermark.Length);//get the font size with respect to length of the string

            //x and y cordinates to draw a string
            x = 0;
            y = bmp.Height - (4 * startsize);

            //System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat(StringFormatFlags.DirectionVertical); -> draws a vertical string for watermark

            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat(StringFormatFlags.NoWrap);

            //drawing string on Image
            graphicsObject.DrawString(watermark, new Font("Verdana", startsize == 0 ? 1 : startsize, FontStyle.Bold), wmBrush, x, y, drawFormat);

            //return a water marked image
            return (bmp);
        }



        /// <summary>
        /// Wajid Shah
        /// This function is use to multiple selection in listbox by sending control id and values to select
        /// </summary>
        /// <param name="Listbx">listbox control id</param>
        /// <param name="IDsToSelect">values to select</param>
        public static void MultipleSelectInListBox(ListBox Listbx, string IDsToSelect)
        {

            if (!string.IsNullOrEmpty(IDsToSelect))
            {
                if (IDsToSelect.Contains(","))
                {
                    string[] MajorMaketIDs = IDsToSelect.Split(',');
                    if (MajorMaketIDs.Length > 0)
                    {
                        for (int i = 0; i < MajorMaketIDs.Length; i++)
                        {
                            for (int j = 0; j < Listbx.Items.Count; j++)
                            {
                                if (Listbx.Items[j].Value == MajorMaketIDs[i].ToString())
                                    Listbx.Items[j].Selected = true;

                            }
                        }
                    }
                }
                else
                {
                    string[] MajorMaketIDs = IDsToSelect.Split(',');
                    for (int j = 0; j < Listbx.Items.Count; j++)
                    {
                        if (Listbx.Items[j].Value == MajorMaketIDs[0].ToString())
                            Listbx.Items[j].Selected = true;

                    }
                }
            }
        }


        /// <summary>
        /// Wajid Shah
        /// use to get multiple selected values from listbox
        /// </summary>
        /// <param name="Listbx">listbox name</param>
        /// <returns>IDs</returns>
        public static string GetMultipleSelectedValueInListBox(ListBox Listbx)
        {
            string IDs = string.Empty;
            for (int i = 0; i < Listbx.Items.Count; i++)
            {
                if (Listbx.Items[i].Selected)
                    IDs += Listbx.Items[i].Value.ToString() + ",";
            }
            IDs = IDs.Trim(',');
            return IDs;
        }




        public static void removeListItem(ref DropDownList ddl, string itemText)
        {
            if (ddl.Items.Count > 0)
            {
                foreach (ListItem li in ddl.Items)
                {
                    if (li.Text.ToLower().Contains(itemText.ToLower()))
                    {
                        ddl.Items.Remove(li);
                        break;
                    }
                }

            }

        }
        public static void removeListItemSelect(ref DropDownList ddl)
        {
            if (ddl.Items.Count > 1)
            {
                ddl.Items.Remove(ddl.Items[0]);
                ddl.Items[0].Selected = true;

            }
        }
        public static string GetDbTypeForDisplay(string p)
        {
            switch (p)
            {
                case "Float":
                case "float":
                    return "decimal";
                    break;
                case "Integer":
                case "integer":
                    return "integer";
                    break;
                case "date":
                    return "mm/dd/yyyy";
                    break;
                case "varchar":
                    return "text";
                case "Money":
                case "money":
                    return "money";
                default:
                    break;
            }
            return string.Empty;
        }



        /// <summary>
        /// Author:sheraz
        /// Used to clean the xml 
        /// </summary>
        /// <param name="query">XML string</param>
        /// <returns></returns>
        public static object CleanXML(string query)
        {
            //UTF32Encoding utf= new UTF32Encoding();
            //Byte[] binaryData= utf.GetBytes(query);
            //MemoryStream ms = new MemoryStream(binaryData);

            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.InnerXml = query;

            return query.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "");
        }

        public static string getXMLElementByTagName(string tagName, string xmlString)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            XmlNodeList propTypes = xmlDoc.GetElementsByTagName(tagName);
            string tagValue = propTypes[0].InnerText;
            return tagValue;
        }

        public static string FormatOrdinalNumber(int number)
        {
            if (number == 0) return "0";
            switch (number % 100)
            {
                case 11:
                case 12:
                case 13:
                    return number + "th";
            }
            switch (number % 10)
            {
                case 1: return number + "st";
                case 2: return number + "nd";
                case 3: return number + "rd";
            }
            return number + "th";
        }





        public static string GetLINQConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["PIXConnectionString"].ConnectionString;
            //return "Data Source=(local);Initial Catalog=powerbar;UID=oicuser;PWD=oic2009;";
        }

        protected bool isCMYKJpeg(System.Drawing.Image image)
        {
            System.Drawing.Imaging.ImageFlags flagValues = (System.Drawing.Imaging.ImageFlags)Enum.Parse(typeof(System.Drawing.Imaging.ImageFlags), image.Flags.ToString());
            if (flagValues.ToString().ToLower().IndexOf("ycck") == -1)
            { return false; }
            return true;
        }


        /// <summary>
        /// wajid shah
        /// </summary>
        /// <param name="OriginalStr">Original String</param>
        /// <param name="listToRemove">Two dimensional String list to be remove from the original string </param>
        /// <param name="Counter">Number of item in list. This is use for loop purpose.  i.e.  string[9,2] then you should send counter value to 9</param>
        /// <returns></returns>
        public static string RemoveSubStringFromString(string OriginalStr, string[,] listToRemove, int Counter)
        {
            string FinalStr = string.Empty;
            bool Flag = false;
            string[] FirstStr;
            string[] LastStr;

            for (int i = 0; i <= Counter - 1; i++)
            {

                if (OriginalStr.Contains(listToRemove[i, 0].ToString()))
                {
                    if (!Flag)
                    {
                        Flag = true;

                        FirstStr = OriginalStr.Split(new string[] { listToRemove[i, 0] }, StringSplitOptions.RemoveEmptyEntries);

                        LastStr = FirstStr[1].ToString().Split(new string[] { listToRemove[i, 1] }, StringSplitOptions.RemoveEmptyEntries);

                        FinalStr = FirstStr[0].ToString() + LastStr[1].ToString();
                    }
                    else
                    {
                        FirstStr = FinalStr.Split(new string[] { listToRemove[i, 0] }, StringSplitOptions.RemoveEmptyEntries);

                        LastStr = FirstStr[1].ToString().Split(new string[] { listToRemove[i, 1] }, StringSplitOptions.RemoveEmptyEntries);

                        FinalStr = FirstStr[0].ToString() + LastStr[1].ToString();
                    }
                }
            }
            return FinalStr;
        }

        public static string ImageSave(FileUpload fileUpload, string extension, string pathInconfig, double width, double height)
        {
            string ImagePath = string.Empty;
            try
            {

                if (fileUpload.HasFile)
                {

                    string path = Convert.ToString(ConfigurationManager.AppSettings[pathInconfig]) + @"/";
                    ImagePath = path + fileUpload.FileName;
                    path = System.Web.HttpContext.Current.Server.MapPath(path);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    fileUpload.SaveAs(ImagePath);
                    ImageResize imr = new ImageResize();
                    imr.Width = width;
                    imr.Height = height;
                    imr.File = ImagePath;
                    imr.PreserveAspectRatio = true;
                    System.Drawing.Image img = imr.GetThumbnail();
                    img.Save(ImagePath);
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Advertisement.UploadAdSource(FileUpload fileUpload, ref Advertisement ads)", ex);
            }
            return ImagePath;
        }

        public static string GetlatLongFormattedResult(string lat, string lng)
        {
            string address = "";
            try
            {
                string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
                string requestUri = string.Format(baseUri, lat, lng);
                string jsonString = new System.Net.WebClient().DownloadString(requestUri);

                var xmlElm = XElement.Parse(jsonString);
                var status = (from elm in xmlElm.Descendants()
                              where elm.Name == "status"
                              select elm).FirstOrDefault();


                if (status.Value.ToLower() == "ok")
                {
                    var res = (from elm in xmlElm.Descendants()
                               where elm.Name == "formatted_address"
                               select elm).FirstOrDefault();


                    address = Convert.ToString(res).Replace("<formatted_address>", "").Replace("</formatted_address>", "");
                }
                else
                {
                    address = "";
                }
            }
            catch (Exception)
            {
                address = "";
            }
            return address;
        }

        public static bool IsValidImageExtension(string fileName)
        {
            bool isValid = false;
            string[] extensions = new string[] { ".gif", ".jpg", ".jpeg", ".bmp", ".png" };
            if (!string.IsNullOrEmpty(fileName))
            {
                string ext = System.IO.Path.GetExtension(fileName);
                foreach (string s in extensions)
                {
                    if (ext.ToLower() == s)
                    {
                        isValid = true;
                        break;
                    }
                }
            }
            return isValid;
        }
        /// <summary>
        /// wajid shah
        /// You can use this function to verify checkbox or radiobutton is selected in gridview or not
        /// </summary>
        /// <param name="gv">Gridveiw control object</param>
        /// <param name="chkboxName">Checkbox control name if you using checkbox</param>
        /// <param name="radiobutName">Radiobutton control name if you are using radio button</param>
        /// <param name="IsRadioButton">if you are using the radio button then send true and for checkbox send false</param>
        /// <returns></returns>
        public static bool CheckValueIsSelectedInGridview(GridView gv, string checkboxName, string radiobuttonName, bool IsRadioButton)
        {
            bool result = false;
            foreach (GridViewRow row in gv.Rows)
            {
                if (IsRadioButton)
                {
                    HtmlInputRadioButton rd = (HtmlInputRadioButton)row.FindControl(radiobuttonName);
                    if (rd != null && rd.Checked)
                    {
                        result = true;
                    }
                }
                else
                {
                    HtmlInputCheckBox chk = (HtmlInputCheckBox)row.FindControl(checkboxName);
                    if (chk != null && chk.Checked)
                    {
                        result = true;
                    }
                }

            }

            return result;
        }
        /// <summary>
        /// wajid shah
        /// You can use this function to get the selected ID from gridview by sending some information 
        /// </summary>
        /// <param name="gv">Gridveiw control object</param>
        /// <param name="chkboxName">Checkbox control name if you want to get the checked value of checkbox</param>
        /// <param name="radiobutName">Radiobutton control name if you are using radio button</param>
        /// <param name="HiddenFieldName">Hiddendfieldname in which you have store the ids</param>
        /// <param name="IsRadioButton">if you are using the radio button then send true and for checkbox send false</param>
        /// <returns></returns>
        public static string GetSelectedIdsGridView(GridView gv, string chkboxName, string radiobutName, string HiddenFieldName, bool IsRadioButton)
        {

            StringBuilder sb = new StringBuilder();
            string strID = string.Empty;

            //Loop through GridView rows to find checked rows 
            foreach (GridViewRow row in gv.Rows)
            {
                HiddenField hfID = (HiddenField)row.FindControl(HiddenFieldName);
                HtmlInputCheckBox chkBx = (HtmlInputCheckBox)row.FindControl("chkbox");
                if (IsRadioButton)
                {
                    HtmlInputRadioButton rd = (HtmlInputRadioButton)row.FindControl(radiobutName);
                    if (rd != null && rd.Checked)
                    {
                        strID = strID + "," + hfID.Value.ToString();
                    }
                }
                else
                {
                    if (chkBx != null && chkBx.Checked)
                    {

                        strID = strID + "," + hfID.Value.ToString();
                    }
                }
            }
            strID = strID.Trim(',');
            return strID;
        }

        /// <summary>
        /// wajid shah
        /// This function is use to set the gridview checkbox or radio button value by send some information to this fucntion
        /// </summary>
        /// <param name="gv">Gridview control object</param>
        /// <param name="chkboxName">Checkbox control name if you are using checkbox otherwise send empty string</param>
        /// <param name="radiobutName">Radio Button control name if you are using radiobutton otherwise send empty string</param>
        /// <param name="HiddenFieldName">the name of hiddenfield which contain the ID or value to compare if you are using Checkbox or radio button but if you are using the radio button and use its value then you just send this as empty string</param>
        /// <param name="IsRadioButton">if you are using radio button</param>
        /// <param name="sID">value you want to select in Gridview</param>
        public static void SetSelectedIdsGridView(ref GridView gv, string chkboxName, string radiobutName, string HiddenFieldName, bool IsRadioButton, string sID)
        {

            StringBuilder sb = new StringBuilder();
            string strID = string.Empty;

            //Loop through GridView rows to find checked rows 
            foreach (GridViewRow row in gv.Rows)
            {

                HiddenField hfID = (HiddenField)row.FindControl(HiddenFieldName);

                if (IsRadioButton)
                {
                    HtmlInputRadioButton rd = (HtmlInputRadioButton)row.FindControl(radiobutName);
                    if (!string.IsNullOrEmpty(HiddenFieldName))
                    {
                        if (rd != null && hfID.Value == sID)
                        {
                            rd.Checked = true;
                        }
                    }
                    else if (rd != null && rd.Value == sID)
                    {
                        rd.Checked = true;
                    }
                }
                else
                {
                    HtmlInputCheckBox chkBx = (HtmlInputCheckBox)row.FindControl(chkboxName);
                    if (chkBx != null && hfID.Value == sID)
                    {

                        chkBx.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Wajid Shah
        /// This function is use to get the current week of date.
        /// </summary>
        /// <param name="dtPassed">date</param>
        /// <returns></returns>
        public static int GetWeekNumber(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }




        public static string ConvertTemplate(string body, int organizationId)
        {
            StringBuilder str = new StringBuilder();


            return str.ToString();

        }

        public static int GetAdminOrganization()
        {
            return (int)AdminOrganizationType.EPRAdmin;
        }
    }
}
