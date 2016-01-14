using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
    public class Templates
    {
        #region Properties
        #region Template Table

        private int _Templateid;

        public int Templateid
        {
            get { return _Templateid; }
            set { _Templateid = value; }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private int _TemplateTypeID;

        public int TemplateTypeID
        {
            get { return _TemplateTypeID; }
            set { _TemplateTypeID = value; }
        }
        private string _TemplateType;

        public string TemplateType
        {
            get { return _TemplateType; }
            set { _TemplateType = value; }
        }
        private string _Body;

        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }
        private bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        private int _CreatedBy;

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        private int _ModifiedBy;

        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        private DateTime _DateCreated;

        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }
        private DateTime _DateModified;

        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }
        private int _OrganizationId;

        public int OrganizationId
        {
            get { return _OrganizationId; }
            set { _OrganizationId = value; }
        }
        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }
        #endregion

        #region OrganizationSentEmail table

        private int _OrganizationSentEmailid;

        public int OrganizationSentEmailid
        {
            get { return _OrganizationSentEmailid; }
            set { _OrganizationSentEmailid = value; }
        }
        private int _ToOrganizationId;

        public int ToOrganizationId
        {
            get { return _ToOrganizationId; }
            set { _ToOrganizationId = value; }
        }
        private string _ToOrganizationName;

        public string ToOrganizationName
        {
            get { return _ToOrganizationName; }
            set { _ToOrganizationName = value; }
        }
        private string _ToOrganizationAddress;

        public string ToOrganizationAddress
        {
            get { return _ToOrganizationAddress; }
            set { _ToOrganizationAddress = value; }
        }
        private int _ToUserId;

        public int ToUserId
        {
            get { return _ToUserId; }
            set { _ToUserId = value; }
        }
        private string _ToUserName;

        public string ToUserName
        {
            get { return _ToUserName; }
            set { _ToUserName = value; }
        }
        private string _ToUserEmail;

        public string ToUserEmail
        {
            get { return _ToUserEmail; }
            set { _ToUserEmail = value; }
        }
        private int _FromOrganizationId;

        public int FromOrganizationId
        {
            get { return _FromOrganizationId; }
            set { _FromOrganizationId = value; }
        }
        private string _FromOrganizationName;

        public string FromOrganizationName
        {
            get { return _FromOrganizationName; }
            set { _FromOrganizationName = value; }
        }
        private string _FromOrganizationAddress;

        public string FromOrganizationAddress
        {
            get { return _FromOrganizationAddress; }
            set { _FromOrganizationAddress = value; }
        }
        private int _FromUserId;

        public int FromUserId
        {
            get { return _FromUserId; }
            set { _FromUserId = value; }
        }
        private string _FromUserName;

        public string FromUserName
        {
            get { return _FromUserName; }
            set { _FromUserName = value; }
        }
        private string _FromUserEmail;

        public string FromUserEmail
        {
            get { return _FromUserEmail; }
            set { _FromUserEmail = value; }
        }
        private string _EmailBody;

        public string EmailBody
        {
            get { return _EmailBody; }
            set { _EmailBody = value; }
        }
        private string _Subject;

        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        private string _FromEmailAddress;

        public string FromEmailAddress
        {
            get { return _FromEmailAddress; }
            set { _FromEmailAddress = value; }
        }
        private string _ToEmailAddress;

        public string ToEmailAddress
        {
            get { return _ToEmailAddress; }
            set { _ToEmailAddress = value; }
        }
        private string _CCEmailAddress;

        public string CCEmailAddress
        {
            get { return _CCEmailAddress; }
            set { _CCEmailAddress = value; }
        }
        private int _InvoiceType;

        public int InvoiceType
        {
            get { return _InvoiceType; }
            set { _InvoiceType = value; }
        }
        private bool _IsPrimary;

        public bool IsPrimary
        {
            get { return _IsPrimary; }
            set { _IsPrimary = value; }
        }


        #endregion

        #endregion
        public Templates() { }
        public Templates(int templateid)
        {
            Load(templateid);
        }

        #region Load Function
        /// <summary>
        /// use to get the delivery data from Database and set the properties of Delivery class
        /// </summary>
        /// <param name="deliveryId"></param>
        private void Load(int templateid)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var parms = new SqlParameter[1];
                    parms[0] = db.MakeInParam("@templateId", SqlDbType.Int, 0, templateid);
                    reader = db.GetDataReader("up_template_getbytemplateId", parms);
                    if (reader.Read())
                        LoadTemplate(reader);  //use to set the value of properties
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Temaplate.Load", ex);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        private void LoadTemplate(IDataReader reader)
        {
            try
            {
                _Templateid = Conversion.ParseDBNullInt(reader["Templateid"]);
                _TemplateType = Conversion.ParseDBNullString(reader["LookupTypeName"]);
                _TemplateTypeID = Conversion.ParseDBNullInt(reader["TemplateTypeID"]);
                _Name = Conversion.ParseDBNullString(reader["Name"]);
                _Body = reader["Body"] == DBNull.Value ? "" : Conversion.ParseDBNullString(reader["Body"]); 
                _InvoiceType = Conversion.ParseDBNullInt(reader["InvoiceType"]);
                _IsActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                _IsPrimary = Conversion.ParseDBNullBool(reader["Isprimary"]);
                _ModifiedBy = Conversion.ParseDBNullInt(reader["ModifiedBy"]);
                _CreatedBy = Conversion.ParseDBNullInt(reader["CreatedBy"]);
                _DateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
                _OrganizationId = Conversion.ParseDBNullInt(reader["Organizationid"]);
                _DateModified = Conversion.ParseDBNullDateTime(reader["DateModified"]);
                _isAdmin = Conversion.ParseDBNullBool(reader["IsAdmin"]);
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Temaplate.LoadTempalte", ex);
            }

        }


        public static DataSet LoadAllTemplatesByOrgID(int OrgId, int pageId, int pageSize, out int iTotalrows, string TemplateName,int TemplateTypeId,int InvoiceTypeID)
        {
            DataSet ds = null;
            iTotalrows = 0;
            try
            {
                List<SqlParameter> prams = new List<SqlParameter>();
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intPageId", SqlDbType.BigInt, 0, pageId));
                    prams.Add(db.MakeInParam("@intPageSize", SqlDbType.BigInt, 0, pageSize));
                    if (string.IsNullOrEmpty(TemplateName))
                        prams.Add(db.MakeInParam("@TemplateName", SqlDbType.NVarChar, 2000, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@TemplateName", SqlDbType.NVarChar, 2000, TemplateName));

                    if (TemplateTypeId<=0)
                        prams.Add(db.MakeInParam("@TemplateTypeID", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@TemplateTypeID", SqlDbType.Int, 0, TemplateTypeId));


                    if (InvoiceTypeID<=0)
                        prams.Add(db.MakeInParam("@InvoiceTypeID", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@InvoiceTypeID", SqlDbType.Int, 0, InvoiceTypeID));


                    prams.Add(db.MakeInParam("@Organizationid", SqlDbType.BigInt, 0, OrgId));
                    prams.Add(db.MakeReturnParam(SqlDbType.Int, 4));
                    ds = db.GetDataSet("up_Template_getAllTemplateByOrganizationId", prams.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prams.Last<SqlParameter>().Value);
                        return ds;
                    }
                }

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Template.LoadAllTemplatesByOrgID", ex);
            }
            return ds;

        }
        #endregion


        #region Insert Update Functions
        public static int TemplateInsertUpdate(Templates objTemp)
        {int result = -1;
        try
        {
            SqlParameter[] prams = null;

            using (DbManager db = DbManager.GetDbManager())
            {
                List<SqlParameter> param = new List<SqlParameter>();

                param.Add(db.MakeInParam("@Templateid", SqlDbType.Int, 0, objTemp.Templateid));
                param.Add(db.MakeInParam("@Organizationid", SqlDbType.Int, 0, objTemp.OrganizationId));
                param.Add(db.MakeInParam("@Name", SqlDbType.NVarChar, 1000, objTemp.Name));
                param.Add(db.MakeInParam("@TemplateTypeID", SqlDbType.Int, 0, objTemp.TemplateTypeID));
                param.Add(db.MakeInParamWithoutFormat("@Body", SqlDbType.NVarChar, 10000, objTemp.Body));
                param.Add(db.MakeInParam("@IsActive", SqlDbType.Bit, 0, objTemp.IsActive));
                param.Add(db.MakeInParam("@IsAdmin", SqlDbType.Bit, 0, objTemp.IsAdmin));
                if (objTemp.CreatedBy == 0)
                    param.Add(db.MakeInParam("@CreatedBy", SqlDbType.Int, 0, DBNull.Value));
                else
                    param.Add(db.MakeInParam("@CreatedBy", SqlDbType.Int, 0, objTemp.CreatedBy));

                if (objTemp.ModifiedBy == 0)
                    param.Add(db.MakeInParam("@ModifiedBy", SqlDbType.Int, 0, DBNull.Value));
                else
                    param.Add(db.MakeInParam("@ModifiedBy", SqlDbType.Int, 0, objTemp.ModifiedBy));

                if (objTemp.DateCreated == DateTime.MinValue)
                    param.Add(db.MakeInParam("@DateCreated", SqlDbType.DateTime, 0, DBNull.Value));
                else
                    param.Add(db.MakeInParam("@DateCreated", SqlDbType.DateTime, 0, objTemp.DateCreated));

                if (objTemp.DateModified == DateTime.MinValue)
                    param.Add(db.MakeInParam("@DateModified", SqlDbType.DateTime, 0, DBNull.Value));
                else
                    param.Add(db.MakeInParam("@DateModified", SqlDbType.DateTime, 0, objTemp.DateModified));

                param.Add(db.MakeInParam("@Isprimary", SqlDbType.Bit, 0, objTemp.IsPrimary));
                param.Add(db.MakeInParam("@InvoiceType", SqlDbType.Int, 0, objTemp.InvoiceType));

                result = db.RunProc("up_Template_insertupdate", param.ToArray());
            }
        }
            catch(Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Templates.TemplateInsertUpdate", ex);
        }
            return result;
        
        }


        public static int makeTemplatePrimary(int Templateid, int TemplateTypeId)
        {
            int result = -1;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@Templateid", SqlDbType.Int, 0, Templateid));
                    prams.Add(db.MakeInParam("@TemplateTypeId", SqlDbType.Int, 0, TemplateTypeId));
                    result = db.RunProc("up_Template_makePrimarybyTemplateID", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Templates.makeTemplatePrimary", ex);
            }
            return result;

        }

        public static int ActivateDeActivateTemplate(int Templateid,bool IsActive )
        {
            int result = -1;


            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@Templateid", SqlDbType.Int, 0, Templateid));
                    prams.Add(db.MakeInParam("@IsActive", SqlDbType.Bit, 0, IsActive));
                    
                    result = db.RunProc("up_Template_deletebytemplateID", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Templates.DeleteTemplate", ex);
            }
            return result;

        }

        #endregion
    }
}
