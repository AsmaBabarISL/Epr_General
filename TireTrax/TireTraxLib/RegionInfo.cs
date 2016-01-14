using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
    public class RegionInfo
    {

        #region Country
        private int _countryId;

        public int CountryId
        {
            get { return _countryId; }
            set { _countryId = value; }
        }
        private String _countryName;

        public String CountryName
        {
            get { return _countryName; }
            set { _countryName = value; }
        }




        #endregion

        #region Currency
        private int _currencyId;

        public int CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value; }
        }
        private String _currencyCode;

        public String CurrencyCode
        {
            get { return _currencyCode; }
            set { _currencyCode = value; }
        }
        private String _currencyName;

        public String CurrencyName
        {
            get { return _currencyName; }
            set { _currencyName = value; }
        }
        private Boolean _isActive;

        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        #endregion

        #region City
        private String _cityName;

        public String CityName
        {
            get { return _cityName; }
            set { _cityName = value; }
        }


        #endregion

        #region Language
        private int _languageId;

        public int LanguageId
        {
            get { return _languageId; }
            set { _languageId = value; }
        }
        private String _language;

        public String Language1
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


        #region State
        private int _stateId;

        public int StateId
        {
            get { return _stateId; }
            set { _stateId = value; }
        }
        private String _state;

        public String State
        {
            get { return _state; }
            set { _state = value; }
        }
        private String _stateName;

        public String StateName
        {
            get { return _stateName; }
            set { _stateName = value; }
        }



        #endregion


        #region ZipCode
        private int _zipcodeID;

        public int ZipcodeID
        {
            get { return _zipcodeID; }
            set { _zipcodeID = value; }
        }
        private String _zipcode;

        public String Zipcode
        {
            get { return _zipcode; }
            set { _zipcode = value; }
        }
        private int _cityId;

        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }
        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        #endregion

        #region TimeZone
        private int _timezoneID;

        public int TimezoneID
        {
            get { return _timezoneID; }
            set { _timezoneID = value; }
        }

        private String _abbreviation;

        public String Abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; }
        }
        private int _timeStart;

        public int TimeStart
        {
            get { return _timeStart; }
            set { _timeStart = value; }
        }

        private int _gmtOffset;

        public int GmtOffset
        {
            get { return _gmtOffset; }
            set { _gmtOffset = value; }
        }
        private String _dst;

        public String Dst
        {
            get { return _dst; }
            set { _dst = value; }
        }
        #endregion

        #region Zone

        private int _zoneId;

        public int ZoneId
        {
            get { return _zoneId; }
            set { _zoneId = value; }
        }

        private String _countryCode;

        public String CountryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; }
        }

        private String _zoneName;

        public String ZoneName
        {
            get { return _zoneName; }
            set { _zoneName = value; }
        }

        #endregion

        public RegionInfo()
        {

        }
        public RegionInfo(int ZipcodeId)
        {
            LoadZipcode(ZipcodeId);
        }
        public RegionInfo(int CityId, string cityName = "")
        {
            LoadCity(CityId);
        }
        private void LoadZipcode(int ZipcodeId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@zipcodeID", SqlDbType.Int, 0, ZipcodeId);
                    reader = db.GetDataReader("up_Zipcode_getById", prams);
                    if (reader.Read())
                        LoadZipcode(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "RegionInfo.Load", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        private void LoadZipcode(IDataReader reader)
        {
            try
            {
                _zipcodeID = Conversion.ParseDBNullInt(reader["ZipcodeID"]);
                _zipcode = Conversion.ParseDBNullString(reader["Zipcode"]);
                _cityId = Conversion.ParseDBNullInt(reader["CityId"]);
                _isActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                _dateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
                _cityName = Conversion.ParseDBNullString(reader["CityName"]);
                _stateId = Conversion.ParseDBNullInt(reader["StateId"]);
                _state = Conversion.ParseDBNullString(reader["State"]);
                _stateName = Conversion.ParseDBNullString(reader["StateName"]);
                _countryId = Conversion.ParseDBNullInt(reader["CountryId"]);
                _countryName = Conversion.ParseDBNullString(reader["CountryName"]);
                _abbreviation = Conversion.ParseDBNullString(reader["Abbreviation"]);
                _languageId = Conversion.ParseDBNullInt(reader["LanguageId"]);
                _language = Conversion.ParseDBNullString(reader["Language"]);
                _specific = Conversion.ParseDBNullString(reader["Specific"]);




            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RegionInfo.Load", ex);
            }
        }

        private void LoadCity(int cityId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@cityId", SqlDbType.Int, 0, cityId);
                    reader = db.GetDataReader("up_City_getById", prams);
                    if (reader.Read())
                        LoadCity(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "RegionInfo.Load", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        private void LoadCity(IDataReader reader)
        {
            try
            {
                _cityId = Conversion.ParseDBNullInt(reader["CityId"]);
                _cityName = Conversion.ParseDBNullString(reader["CityName"]);
                _stateId = Conversion.ParseDBNullInt(reader["StateId"]);
                _isActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                _dateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);

                _state = Conversion.ParseDBNullString(reader["State"]);
                _stateName = Conversion.ParseDBNullString(reader["StateName"]);
                _countryId = Conversion.ParseDBNullInt(reader["CountryId"]);
                _countryName = Conversion.ParseDBNullString(reader["CountryName"]);
                _abbreviation = Conversion.ParseDBNullString(reader["Abbreviation"]);
                _languageId = Conversion.ParseDBNullInt(reader["LanguageId"]);
                _language = Conversion.ParseDBNullString(reader["Language"]);
                _specific = Conversion.ParseDBNullString(reader["Specific"]);

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "RegionInfo.Load", ex);
            }
        }

    }
}
