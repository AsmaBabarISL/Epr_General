using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.IO;
using System.Configuration;
using AjaxControlToolkit;
using System.Data.SqlClient;
using System.Threading;
using System.Net.Mail;

public partial class Registration_RegistrationFormUS : BasePage
{
    public int _GlobalCountryID = 235; // USA CountryId
    private int StewardshipCertifications = 0;
    private int StakeholderCertifications = 0;
    public int GlobalCountryID
    {
        get
        {
            string Culture = Convert.ToString(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"]);
            switch (Culture.ToLower())
            {
                case "en-us":
                    _GlobalCountryID = 235;
                    StewardshipCertifications = Convert.ToInt32(OrganizationCertificationTypes.StewardshipCertificationsEnglish);
                    StakeholderCertifications = Convert.ToInt32(OrganizationCertificationTypes.StakeholderCertificationsEnglish);
                    break;
                case "es-mx":
                    _GlobalCountryID = 159;
                    StewardshipCertifications = Convert.ToInt32(OrganizationCertificationTypes.StewardshipCertificationsSpanish);
                    StakeholderCertifications = Convert.ToInt32(OrganizationCertificationTypes.StakeholderCertificationsSpanish);
                    break;
                case "fr-fr":
                    _GlobalCountryID = 39;
                    StewardshipCertifications = Convert.ToInt32(OrganizationCertificationTypes.StewardshipCertificationsFrench);
                    StakeholderCertifications = Convert.ToInt32(OrganizationCertificationTypes.StakeholderCertificationsFrench);
                    break;
                case "fr-ca":
                    _GlobalCountryID = 39;
                    StewardshipCertifications = Convert.ToInt32(OrganizationCertificationTypes.StewardshipCertificationsCanadian);
                    StakeholderCertifications = Convert.ToInt32(OrganizationCertificationTypes.StakeholderCertificationsCanadian);
                    break;
                case "en-ca":
                    _GlobalCountryID = 39;
                    StewardshipCertifications = Convert.ToInt32(OrganizationCertificationTypes.StewardshipCertificationsCanadian);
                    StakeholderCertifications = Convert.ToInt32(OrganizationCertificationTypes.StakeholderCertificationsCanadian);
                    break;
                case "ja-jp":
                    _GlobalCountryID = 116;
                    StewardshipCertifications = Convert.ToInt32(OrganizationCertificationTypes.StewardshipCertificationsJapanese);
                    StakeholderCertifications = Convert.ToInt32(OrganizationCertificationTypes.StakeholderCertificationsJapanese);
                    break;
                case "ko-kr":
                    _GlobalCountryID = 124;
                    StewardshipCertifications = Convert.ToInt32(OrganizationCertificationTypes.StewardshipCertificationsKorean);
                    StakeholderCertifications = Convert.ToInt32(OrganizationCertificationTypes.StakeholderCertificationsKorean);
                    break;
                case "en-au":
                    _GlobalCountryID = 14;
                    StewardshipCertifications = Convert.ToInt32(OrganizationCertificationTypes.StewardshipCertificationsAustralia);
                    StakeholderCertifications = Convert.ToInt32(OrganizationCertificationTypes.StakeholderCertificationsAustralia);
                    break;
                case "zh-cn":
                    _GlobalCountryID = 49;
                    StewardshipCertifications = Convert.ToInt32(OrganizationCertificationTypes.StewardshipCertificationsChinese);
                    StakeholderCertifications = Convert.ToInt32(OrganizationCertificationTypes.StakeholderCertificationsChinese);
                    break;
                default:
                    _GlobalCountryID = 235;
                    StewardshipCertifications = Convert.ToInt32(OrganizationCertificationTypes.StewardshipCertificationsEnglish);
                    StakeholderCertifications = Convert.ToInt32(OrganizationCertificationTypes.StakeholderCertificationsEnglish);
                    break;
            }


            return _GlobalCountryID;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //chk
            DataBindInputControls();
            LoadDropDown();

            OrganizationInfo ObjOrg = new OrganizationInfo();

            rptStakeCertificates.DataSource = ObjOrg.GetAllCertificatesByType(StewardshipCertifications); //old value is 1 
            rptStakeCertificates.DataBind();

            //rptTireCertificates.DataSource = ObjOrg.GetAllCertificatesByType(StakeholderCertifications); // older value is 2 
            //rptTireCertificates.DataBind();

            DataSet ds = Product.GetProductNames();
            chkProductId.DataSource = ds;
            chkProductId.DataBind();
            //DataSet dss = OrganizationInfo.GetOrganizationSubTypes();
            //ddlOrganizationSubType.DataSource = dss;
            //ddlOrganizationSubType.DataBnd();

            if (Request.QueryString["OrganizationId"] != null && Utils.IsNumeric(Request.QueryString["OrganizationId"]))
            {
                LoadInfo(Convert.ToInt32(Request.QueryString["OrganizationId"]));
            }
            else
            {
                DataTable dt = new DataTable();
                gvSupplier.DataSource = dt;
                gvSupplier.DataBind();
                LoadHeaderTextForSupplier();

                gvClient.DataSource = dt;
                gvClient.DataBind();
                LoadHeaderTextForClient();

                gvAdditionLocations.DataSource = dt;
                gvAdditionLocations.DataBind();
                LoadHeaderTextForAddLocations();

                dt.Dispose();
                dt = null;
            }
            chkLocationTemporary.Attributes.Add("onclick", String.Format("return ValidateChecked(this, '{0}', true, true, false);", chkLocationPermanent.ClientID));
            chkLocationPermanent.Attributes.Add("onclick", String.Format("return ValidateChecked(this, '{0}', true, true, false);", chkLocationTemporary.ClientID));
            //.Enabled = false;
            //txtLocationToDate.Enabled = false;
            loadAgreement();
            if (ddlOrganizationType.SelectedItem.Text.ToString().Trim().ToLower() == "stewardship")
            {
                orgsubType.Visible = false;
            }
        }
        if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"]))
        {
            Utils.SetCulture(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"].ToString(), HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"].ToString());
        }
        //if (hdnOrganizationID.Value != null)
        //{
        //    if(Conversion.ParseInt(hdnOrganizationID.Value)>0)
        //    Utils.GetLookUpData<DropDownList>(ref ddlRoleList, LookUps.RoleName, Conversion.ParseInt(hdnOrganizationID.Value));
        //} 
        lblStewardshipExists.Visible = false;
        lblSupplierStatus.Visible = false;
    }
    private void loadAgreement()
    {
        DataSet ds = Agreement.getAgreement(Convert.ToInt16(AgreementTypes.US_StewardshipPrivacyAgreement));
        ltrlstewardshipPrivacyAgreement.Text = ds.Tables[0].Rows[0]["vchAgreementText"].ToString();
        ds = Agreement.getAgreement(Convert.ToInt16(AgreementTypes.US_TireTraxPrivacyAgreement));
        ltrlTireTraxPrivacyAgreement.Text = ds.Tables[0].Rows[0]["vchAgreementText"].ToString();
    }
    public void LoadHeaderTextForSupplier()
    {

        gvSupplier.HeaderRow.Cells[0].Text = ResourceMgr.GetMessage("Company Name");
        gvSupplier.HeaderRow.Cells[1].Text = ResourceMgr.GetMessage("Country");
        gvSupplier.HeaderRow.Cells[2].Text = ResourceMgr.GetMessage("State");
        gvSupplier.HeaderRow.Cells[3].Text = ResourceMgr.GetMessage("City");
        gvSupplier.HeaderRow.Cells[4].Text = ResourceMgr.GetMessage("Contact Name");
        gvSupplier.HeaderRow.Cells[5].Text = ResourceMgr.GetMessage("Business Phone");
        gvSupplier.HeaderRow.Cells[6].Text = ResourceMgr.GetMessage("Email");
        gvSupplier.HeaderRow.Cells[7].Text = ResourceMgr.GetMessage("Delete");

    }

    public void LoadHeaderTextForClient()
    {

        gvClient.HeaderRow.Cells[0].Text = ResourceMgr.GetMessage("Company");
        gvClient.HeaderRow.Cells[1].Text = ResourceMgr.GetMessage("Country");
        gvClient.HeaderRow.Cells[2].Text = ResourceMgr.GetMessage("Contact Name");
        gvClient.HeaderRow.Cells[3].Text = ResourceMgr.GetMessage("Business ");
        gvClient.HeaderRow.Cells[4].Text = ResourceMgr.GetMessage("Extention");
        gvClient.HeaderRow.Cells[5].Text = ResourceMgr.GetMessage("Email");
        gvClient.HeaderRow.Cells[6].Text = ResourceMgr.GetMessage("Actions");

    }

    public void LoadHeaderTextForAddLocations()
    {

        gvAdditionLocations.HeaderRow.Cells[0].Text = ResourceMgr.GetMessage("Location Name");
        gvAdditionLocations.HeaderRow.Cells[1].Text = ResourceMgr.GetMessage("Status");
        gvAdditionLocations.HeaderRow.Cells[2].Text = ResourceMgr.GetMessage("Contact Name");
        gvAdditionLocations.HeaderRow.Cells[3].Text = ResourceMgr.GetMessage("Business Phone");
        gvAdditionLocations.HeaderRow.Cells[4].Text = ResourceMgr.GetMessage("Email");
        gvAdditionLocations.HeaderRow.Cells[5].Text = ResourceMgr.GetMessage("ZIP Code");
        gvAdditionLocations.HeaderRow.Cells[6].Text = ResourceMgr.GetMessage("Permit #");
        gvAdditionLocations.HeaderRow.Cells[7].Text = ResourceMgr.GetMessage("Delete");

    }


    private void LoadInfo(int OrgID)
    {
        DataSet regData = null;
        regData = OrganizationInfo.editRegistartion(OrgID);

        if (regData != null && regData.Tables.Count > 0 && regData.Tables[0].Rows.Count > 0)
        {
            txtBusinessLegalName.Text = regData.Tables[0].Rows[0]["LegalName"].ToString();
            txtDBAName.Text = regData.Tables[0].Rows[0]["DBAName"].ToString();
            txtBusinessWebsite.Text = regData.Tables[0].Rows[0]["website"].ToString();
            ddlOrganizationType.SelectedValue = regData.Tables[0].Rows[0]["OrganizationTypeId"].ToString();

            txtPrimaryContactFirstName.Text = regData.Tables[2].Rows[0]["FirstName"].ToString();
            txtPrimaryContactLastName.Text = regData.Tables[2].Rows[0]["LastName"].ToString();
            ddlPrimaryContactTitle.SelectedValue = regData.Tables[2].Rows[0]["ContactTitleId"].ToString();

            string B1Number = regData.Tables[3].Rows[0]["Number"].ToString();
            string[] B1Num = B1Number.Split('-');
            txtPrimaryContactBusinessPhone1.Text = B1Num[0].ToString();
            txtPrimaryContactBusinessPhone2.Text = B1Num[1].ToString();
            txtPrimaryContactBusinessPhone3.Text = B1Num[2].ToString();

            txtPrimaryContactBusinessPhoneExtension.Text = regData.Tables[3].Rows[0]["Extension"].ToString();


            string B2Number = regData.Tables[3].Rows[1]["Number"].ToString();
            string[] B2Num = B2Number.Split('-');
            txtPrimaryContactCellPhone1.Text = B2Num[0].ToString();
            txtPrimaryContactCellPhone2.Text = B2Num[1].ToString();
            txtPrimaryContactCellPhone3.Text = B2Num[2].ToString();


            if ((regData.Tables[3].Rows[0]["IsAcceptTextMessages"]) != DBNull.Value)
                chkPrimaryContactAcceptTextMessages.Checked = true;

            txtPrimaryContactEmail.Text = regData.Tables[2].Rows[0]["Email"].ToString();


            txtBillingContactFirstName.Text = regData.Tables[2].Rows[1]["FirstName"].ToString();
            txtBillingContactLastName.Text = regData.Tables[2].Rows[1]["LastName"].ToString();
            ddlBillingContactTitle.SelectedValue = regData.Tables[2].Rows[1]["ContactTitleId"].ToString();

            string M1Number = regData.Tables[4].Rows[0]["Number"].ToString();
            string[] M1Num = M1Number.Split('-');
            txtBillingContactPhoneNumber1.Text = M1Num[0].ToString();
            txtBillingContactPhoneNumber2.Text = M1Num[1].ToString();
            txtBillingContactPhoneNumber3.Text = M1Num[2].ToString();

            txtBillingContactPhoneExtension.Text = regData.Tables[3].Rows[1]["Extension"].ToString();

            string M2Number = regData.Tables[4].Rows[1]["Number"].ToString();
            string[] M2Num = M2Number.Split('-');
            txtBillingContactCellNumber1.Text = M2Num[0].ToString();
            txtBillingContactCellNumber2.Text = M2Num[1].ToString();
            txtBillingContactCellNumber3.Text = M2Num[2].ToString();

            if ((regData.Tables[3].Rows[1]["IsAcceptTextMessages"]) != DBNull.Value)
                chkBillingContactAcceptTextMessages.Checked = true;

            txtBillingContactEmail.Text = regData.Tables[2].Rows[1]["Email"].ToString();

            txtBusinessZipCode.Text = regData.Tables[1].Rows[0]["Zipcode"].ToString();
            txtBusinessAddress1.Text = regData.Tables[1].Rows[0]["Address1"].ToString();
            txtBusinessAddress2.Text = regData.Tables[1].Rows[0]["Address2"].ToString();
            txtBuinessCity.Text = regData.Tables[1].Rows[0]["City"].ToString();
            ddlBusinessProvince.SelectedValue = regData.Tables[1].Rows[0]["StateId"].ToString();
            ddlBusinessCountry.SelectedValue = regData.Tables[1].Rows[0]["CountryId"].ToString();
            txtMailingZipCode.Text = regData.Tables[1].Rows[1]["Zipcode"].ToString();
            txtMailingAddress1.Text = regData.Tables[1].Rows[1]["Address1"].ToString();
            txtMailingAddress2.Text = regData.Tables[1].Rows[1]["Address2"].ToString();
            txtMailingCity.Text = regData.Tables[1].Rows[1]["City"].ToString();
            ddlMailingState.SelectedValue = regData.Tables[1].Rows[1]["StateId"].ToString();
            ddlMailingCountry.SelectedValue = regData.Tables[1].Rows[1]["CountryId"].ToString();

            ddlBusinessAccountingInterface.SelectedValue = regData.Tables[0].Rows[0]["AccountingInterfaceID"].ToString();
            ddlBusinessInventoryInterface.SelectedValue = regData.Tables[0].Rows[0]["InventoryInterfaceId"].ToString();
            if ((regData.Tables[0].Rows[0]["IsAutoFundTransfer"]) != DBNull.Value)
                chkBusinessAcceptAutoFundTransfers.Checked = true;

            // For Additional Location
            gvAdditionLocations.DataSource = OrganizationInfo.GetAdditionalLocationsByOrganizationId(OrgID);
            gvAdditionLocations.DataBind();

            // For Stakeholders

            foreach (RepeaterItem rptItem in rptStakeCertificates.Items)
            {
                for (int rw = 0; rw < regData.Tables[5].Rows.Count; rw++)
                {
                    if (((HiddenField)rptItem.FindControl("hdnID")).Value == regData.Tables[5].Rows[rw]["CertificationID"].ToString())
                    {
                        CheckBox Editchk = (CheckBox)rptItem.FindControl("chk");
                        Editchk.Checked = true;
                    }
                }
            }
            DataSet dss = null;
            dss = OrganizationInfo.GetSupplierByOrganizationId(Convert.ToInt32(OrgID));
            gvSupplier.DataSource = dss;
            gvSupplier.DataBind();

            gvClient.DataSource = OrganizationInfo.GetClientByOrganizationId(Convert.ToInt32(OrgID));
            gvClient.DataBind();

        }


    }


    private void LoadDropDown()
    {
        Utils.GetLookUpData<DropDownList>(ref ddlclientsCountry, LookUps.Country);
        Utils.GetLookUpData<DropDownList>(ref ddlSuppliersCountry, LookUps.Country);
        Utils.GetLookUpData<DropDownList>(ref ddlBusinessCountry, LookUps.Country);
        Utils.GetLookUpData<DropDownList>(ref ddlMailingCountry, LookUps.Country);
        Utils.GetLookUpData<DropDownList>(ref ddlLocationCountry, LookUps.Country);

        if (ddlBusinessCountry.Items.FindByValue(GlobalCountryID.ToString()) != null)
            ddlBusinessCountry.SelectedValue = GlobalCountryID.ToString();
        if (ddlMailingCountry.Items.FindByValue(GlobalCountryID.ToString()) != null)
            ddlMailingCountry.SelectedValue = GlobalCountryID.ToString();
        if (ddlLocationCountry.Items.FindByValue(GlobalCountryID.ToString()) != null)
            ddlLocationCountry.SelectedValue = GlobalCountryID.ToString();
        if (ddlSuppliersCountry.Items.FindByValue(GlobalCountryID.ToString()) != null)
            ddlSuppliersCountry.SelectedValue = GlobalCountryID.ToString();
        if (ddlclientsCountry.Items.FindByValue(GlobalCountryID.ToString()) != null)
            ddlclientsCountry.SelectedValue = GlobalCountryID.ToString();

        System.Data.SqlClient.SqlParameter[] prm;

        prm = new System.Data.SqlClient.SqlParameter[1];
        prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId", ddlBusinessCountry.SelectedValue);
        Utils.GetLookUpData<DropDownList>(ref ddlBusinessProvince, LookUps.State, prm);
        prm = null;

        prm = new System.Data.SqlClient.SqlParameter[1];
        prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId", ddlMailingCountry.SelectedValue);
        Utils.GetLookUpData<DropDownList>(ref ddlMailingState, LookUps.State, prm);
        prm = null;

        prm = new System.Data.SqlClient.SqlParameter[1];
        prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId", ddlSuppliersCountry.SelectedValue);
        Utils.GetLookUpData<DropDownList>(ref ddlSuppliersProvince, LookUps.State, prm);
        prm = null;

        prm = new System.Data.SqlClient.SqlParameter[1];
        prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId", ddlclientsCountry.SelectedValue);
        Utils.GetLookUpData<DropDownList>(ref ddlclientsProvince, LookUps.State, prm);
        prm = null;

        prm = new System.Data.SqlClient.SqlParameter[1];
        prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId", ddlLocationCountry.SelectedValue);
        Utils.GetLookUpData<DropDownList>(ref ddlLocationState, LookUps.State, prm);
        prm = null;

        Utils.GetLookUpData<DropDownList>(ref ddlOrganizationType, LookUps.OrganizationType, LanguageId);
        ListItem li = ddlOrganizationType.Items.FindByValue("353");
        ddlOrganizationType.Items.Remove(li);
        ListItem li1 = ddlOrganizationType.Items.FindByValue("21");
        ddlOrganizationType.Items.Remove(li1);
        ListItem li2 = ddlOrganizationType.Items.FindByValue("23");
        ddlOrganizationType.Items.Remove(li2);
        ListItem li3 = ddlOrganizationType.Items.FindByValue("24");
        ddlOrganizationType.Items.Remove(li3);
        ListItem li4 = ddlOrganizationType.Items.FindByValue("47");
        ddlOrganizationType.Items.Remove(li4);
        ListItem li5 = ddlOrganizationType.Items.FindByValue("343");
        ddlOrganizationType.Items.Remove(li5);

       Utils.GetLookUpData<DropDownList>(ref ddlOrganizationSubType, LookUps.OrganizationSubType, LanguageId);

        if (Convert.ToInt32(Request.QueryString["SSID"]) > 0 && ddlOrganizationType.Items.FindByValue("20") != null
            && OrganizationInfo.getStewardshipStatusByStateId(Convert.ToInt32(Request.QueryString["SSID"])) > 0)
        {
            ddlOrganizationType.Items.Remove(ddlOrganizationType.Items.FindByValue("20"));
        }
        else if (ddlOrganizationType.Items.FindByValue("20") != null
            && OrganizationInfo.getStewardshipStatusByStateId(Convert.ToInt32(Request.QueryString["SSID"])) == 0)
        {
            ddlOrganizationType.SelectedValue = "20";
            ddlOrganizationType.Enabled = false;

            ToolkitScriptManager.RegisterStartupScript(this, GetType(), "SetWizard", "SetWizard(document.getElementById('ddlOrganizationType'));", true);

        }
        DataSet ds = OrganizationInfo.GetStateCodeByStateId(Conversion.ParseInt(Request.QueryString["SSID"]));
        if (ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0][0] != DBNull.Value)
            ViewState["StewardShipId"] = ds.Tables[1].Rows[0][0].ToString();
        else if (Request.QueryString["SSID"] != null)
            ViewState["StewardShipId"] = Request.QueryString["SSID"].ToString();

        Utils.GetLookUpData<DropDownList>(ref ddlLocationEventType, LookUps.OrganizationLocationEventType, LanguageId);
        Utils.GetLookUpData<DropDownList>(ref ddlBusinessAccountingInterface, LookUps.Interface, LanguageId);
        Utils.GetLookUpData<DropDownList>(ref ddlBusinessInventoryInterface, LookUps.Interface, LanguageId);

        Utils.GetLookUpData<DropDownList>(ref ddlPrimaryContactTitle, LookUps.PrimaryContactTitle, LanguageId);
        Utils.GetLookUpData<DropDownList>(ref ddlBillingContactTitle, LookUps.BillingContactTitle, LanguageId);
        Utils.GetLookUpData<DropDownList>(ref ddlLocationContactTitle, LookUps.LocationContactTitle, LanguageId);
        //Utils.GetLookUpData<DropDownList>(ref ddlRoleList, LookUps.Role, LanguageId);




    }

    private void DataBindInputControls()
    {
        txtPrimaryContactLastName.DataBind();
        txtBusinessLegalName.DataBind();
        txtDBAName.DataBind();
        txtPrimaryContactFirstName.DataBind();
        txtBillingContactFirstName.DataBind();
        txtBusinessZipCode.DataBind();
        txtBusinessAddress1.DataBind();
        txtBusinessAddress2.DataBind();
        txtMailingZipCode.DataBind();
        txtMailingAddress1.DataBind();
        txtLocationBusinessName.DataBind();
        txtLocationDBAName.DataBind();
        txtLocationFromDate.DataBind();
        txtLocationContactFirstName.DataBind();
        txtLocationAddress1.DataBind();
        txtLocationAddress2.DataBind();
        txtSupplierZipCode.DataBind();
        txtClientZipCode.DataBind();
        txtLocationToDate.DataBind();
        txtLocationContactLastName.DataBind();
        txtBillingContactLastName.DataBind();
        txtSupplierscontactFirstName.DataBind();
        txtSupplierscontactLastName.DataBind();
        txtclientsContactFirstName.DataBind();
        txtclientsContactLastName.DataBind();
    }

    #region CommentedCode
    /*

		private void UpdateData(int OrganizationId)
		{
			OrganizationInfo ObjOrg = new OrganizationInfo(OrganizationId);
			ObjOrg.Address = txtBusinessAddress1.Text;
			
			ObjOrg.BillMailAddress = txtMailingAddress1.Text;

			ObjOrg.BusinessType = new List<int>();

			ObjOrg.CountryID = Convert.ToInt32(ddlBusinessCountry.SelectedValue);
			ObjOrg.CountryName = ddlBusinessCountry.Text;

			ObjOrg.DBAName = txtDBAName.Text;

			ObjOrg.IsActive = true;
			ObjOrg.IsOrganization = true;

			ObjOrg.LegalName = txtBusinessLegalName.Text;
			ObjOrg.OwnerManager = txtPrimaryContactFirstName.Text;
			ObjOrg.StateID = Convert.ToInt32(ddlBusinessProvince.SelectedValue);
			ObjOrg.ZipPostalCode = txtBusinessZipCode.Text;

			OrganizationInfo.Supplier ObjOrgSupplier = ObjOrg.objSupplier;

			ObjOrgSupplier.BussinessPhone = txtSupplierBusinessPhone1.Text.Trim() + "-" + txtSupplierBusinessPhone2.Text.Trim() + "-" + txtSupplierBusinessPhone3.Text.Trim();
			ObjOrgSupplier.City = txtSupplierCity.Text;
			ObjOrgSupplier.CompanyName = txtSupplierCompanyName.Text;
			ObjOrgSupplier.ContactName = txtSupplierscontactName.Text;
			ObjOrgSupplier.CountryID = int.Parse(ddlSuppliersCountry.SelectedValue);
			ObjOrgSupplier.Email = txtSuppliersownerEmail.Text;
			ObjOrgSupplier.IsActive = true;
			ObjOrgSupplier.OwnerManagerEmail = txtSuppliersownerEmail.Text;
			ObjOrgSupplier.StateId = Convert.ToInt32(ddlSuppliersProvince.SelectedValue);

			ObjOrg.objSupplier = ObjOrgSupplier;

			OrganizationInfo.Client objOrgClient = ObjOrg.objClient;

			objOrgClient.BussinessPhone = txtClientBusinessPhone1.Text.Trim() + "-" + txtClientBusinessPhone2.Text.Trim() + "-" + txtClientBusinessPhone3.Text.Trim();
			objOrgClient.City = txtClientCity.Text;
			objOrgClient.CompanyName = txtclientsCompanyName.Text;
			objOrgClient.ContactName = txtclientsContactName.Text;
			objOrgClient.CountryID = int.Parse(ddlclientsCountry.SelectedValue);
			objOrgClient.IsActive = true;
			objOrgClient.OwnerManagerEmail = txtclientsEmail.Text;
			objOrgClient.StateId = Convert.ToInt32(ddlclientsProvince.SelectedValue);

			ObjOrg.objClient = objOrgClient;

			ObjOrg.ObjOrgBusiness = new List<OrganizationInfo.Organization_Business>();

			//OrganizationInfo.Organization_Business objOrgBusiness;

			//foreach (RepeaterItem rptItem in rptBusiness.Items)
			//{
			//    objOrgBusiness = new OrganizationInfo.Organization_Business();

			//    CheckBox chkY = (CheckBox)rptItem.FindControl("chkY");
			//    CheckBox chkN = (CheckBox)rptItem.FindControl("chkN");

			//    if (chkY.Checked || chkN.Checked)
			//    {
			//        objOrgBusiness.BusinessID = Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value);
			//        objOrgBusiness.IsNew = chkY.Checked;

			//        ObjOrg.ObjOrgBusiness.Add(objOrgBusiness);

			//    }
			//}

			ObjOrg.CertificationID = new List<int>();

			foreach (RepeaterItem rptItem in rptStakeCertificates.Items)
			{
				if (((CheckBox)rptItem.FindControl("chk")).Checked)
				{
					ObjOrg.CertificationID.Add(Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value));
				}
			}

			foreach (RepeaterItem rptItem in rptTireCertificates.Items)
			{
				if (((CheckBox)rptItem.FindControl("chk")).Checked)
				{
					ObjOrg.CertificationID.Add(Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value));
				}
			}

			ObjOrg.UpdateOrganizationInfo();
		}

		private void SaveData()
		{
			OrganizationInfo ObjOrg = new OrganizationInfo();
			ObjOrg.Abbreviation = "";
			ObjOrg.Address = txtBusinessAddress1.Text;
			//ObjOrg.BillingContact = txtbillingContact.Text;
			ObjOrg.BillMailAddress = txtMailingAddress1.Text;

			ObjOrg.BusinessType = new List<int>();

			ObjOrg.City = "";
			ObjOrg.Clientid = 0;
			ObjOrg.ContactId = 0;
			ObjOrg.CountryID = Convert.ToInt32(ddlBusinessCountry.SelectedValue);
			ObjOrg.CountryName = ddlBusinessCountry.Text;
			ObjOrg.DateCreated = DateTime.Now.Date;
			ObjOrg.DBAName = txtDBAName.Text;
			ObjOrg.Description = "";
			ObjOrg.IsActive = true;

			ObjOrg.IsOrganization = true;
			ObjOrg.Language = "";
			ObjOrg.LanguageId = LanguageId;
			ObjOrg.LegalName = txtBusinessLegalName.Text;
			ObjOrg.LocationID = 0;
			ObjOrg.OrganizationId = 0;
			ObjOrg.OrganizationTypeId = 1;
			ObjOrg.OwnerManager = txtPrimaryContactFirstName.Text;
			ObjOrg.ParentId = 0;
			ObjOrg.PhoneId = 0;
			ObjOrg.RoleId = 0;
			ObjOrg.RoleName = "";
			ObjOrg.Specific = "";
			if (Request.QueryString["StakeHolderID"] != null && Utils.IsNumeric(Request.QueryString["StakeHolderID"]))
			{
				ObjOrg.StakeHolderId = Convert.ToInt32(Request.QueryString["StakeHolderID"]);
			}
			else
			{
				ObjOrg.StakeHolderId = 0;
			}

			ObjOrg.StateID = Convert.ToInt32(ddlBusinessProvince.SelectedValue);
			ObjOrg.Supplierid = 0;
			ObjOrg.TX_ID = "";
			ObjOrg.Website = "";
			ObjOrg.ZipPostalCode = txtBusinessZipCode.Text;

			OrganizationInfo.Supplier ObjOrgSupplier = new OrganizationInfo.Supplier();

			ObjOrgSupplier.BussinessPhone = txtSupplierBusinessPhone1.Text.Trim() + "-" + txtSupplierBusinessPhone2.Text.Trim() + "-" + txtSupplierBusinessPhone3.Text.Trim();
			ObjOrgSupplier.City = txtSupplierCity.Text;
			ObjOrgSupplier.CompanyName = txtSupplierCompanyName.Text;
			ObjOrgSupplier.ContactName = txtSupplierscontactName.Text;
			ObjOrgSupplier.Count = 0;
			ObjOrgSupplier.CountryID = int.Parse(ddlSuppliersCountry.SelectedValue);
			ObjOrgSupplier.DateCreated = DateTime.Now;
			ObjOrgSupplier.Email = txtSuppliersownerEmail.Text;
			ObjOrgSupplier.IsActive = true;
			ObjOrgSupplier.LanguageID = LanguageId;
			ObjOrgSupplier.OwnerManagerEmail = txtSuppliersownerEmail.Text;
			ObjOrgSupplier.StateId = Convert.ToInt32(ddlSuppliersProvince.SelectedValue);
			ObjOrgSupplier.SupplierID = 0;

			ObjOrg.objSupplier = ObjOrgSupplier;

			OrganizationInfo.Client objOrgClient = new OrganizationInfo.Client();

			objOrgClient.BussinessPhone = txtClientBusinessPhone1.Text.Trim() + "-" + txtClientBusinessPhone2.Text.Trim() + "-" + txtClientBusinessPhone3.Text.Trim();
			objOrgClient.City = txtClientCity.Text;
			objOrgClient.ClientID = 0;
			objOrgClient.CompanyName = txtclientsCompanyName.Text;
			objOrgClient.ContactName = txtclientsContactName.Text;
			objOrgClient.CountryID = int.Parse(ddlclientsCountry.SelectedValue);
			objOrgClient.DateCreated = DateTime.Now;
			objOrgClient.IsActive = true;
			objOrgClient.LanguageId = LanguageId;
			objOrgClient.OwnerManagerEmail = txtclientsEmail.Text;
			objOrgClient.StateId = Convert.ToInt32(ddlclientsProvince.SelectedValue);

			ObjOrg.objClient = objOrgClient;

			ObjOrg.ObjOrgBusiness = new List<OrganizationInfo.Organization_Business>();

			//OrganizationInfo.Organization_Business objOrgBusiness;

			//foreach (RepeaterItem rptItem in rptBusiness.Items)
			//{
			//    objOrgBusiness = new OrganizationInfo.Organization_Business();

			//    CheckBox chkY = (CheckBox)rptItem.FindControl("chkY");
			//    CheckBox chkN = (CheckBox)rptItem.FindControl("chkN");

			//    if (chkY.Checked || chkN.Checked)
			//    {
			//        objOrgBusiness.BusinessID = Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value);
			//        objOrgBusiness.IsNew = chkY.Checked;

			//        ObjOrg.ObjOrgBusiness.Add(objOrgBusiness);

			//    }
			//}

			ObjOrg.CertificationID = new List<int>();

			foreach (RepeaterItem rptItem in rptStakeCertificates.Items)
			{
				if (((CheckBox)rptItem.FindControl("chk")).Checked)
				{
					ObjOrg.CertificationID.Add(Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value));
				}
			}

			foreach (RepeaterItem rptItem in rptTireCertificates.Items)
			{
				if (((CheckBox)rptItem.FindControl("chk")).Checked)
				{
					ObjOrg.CertificationID.Add(Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value));
				}
			}

			ObjOrg.InsertOrganizationInfo();
		}

		private void LoadInfo(int OrganizationID)
		{
			OrganizationInfo ObjOrg = new OrganizationInfo(OrganizationID);

			txtBusinessLegalName.Text = ObjOrg.LegalName;
			txtDBAName.Text = ObjOrg.DBAName;
			txtBusinessAddress1.Text = ObjOrg.Address;
			ddlBusinessProvince.SelectedValue = Convert.ToString(ObjOrg.StateID);
			ddlBusinessCountry.SelectedValue = Convert.ToString(ObjOrg.CountryID);
			txtBusinessZipCode.Text = ObjOrg.ZipPostalCode;
			txtMailingAddress1.Text = ObjOrg.BillMailAddress;
			txtPrimaryContactFirstName.Text = ObjOrg.OwnerManager;
			//txtbillingContact.Text = ObjOrg.BillingContact;

			//ddlownertitle.SelectedValue = "";
			//ddlbillingContact1.SelectedValue = "";
			//txtBusinessNumber1.Text = "";
			//txtBusinessNumber2.Text = "";
			//txtBusinessNumber3.Text = "";

			if (ObjOrg.objSupplier != null)
			{
				txtSupplierCompanyName.Text = ObjOrg.objSupplier.CompanyName;
				ddlSuppliersCountry.SelectedValue = Convert.ToString(ObjOrg.objSupplier.CountryID);
				ddlSuppliersProvince.SelectedValue = Convert.ToString(ObjOrg.objSupplier.StateId);
				txtClientCity.Text = ObjOrg.objSupplier.City;
				txtSupplierscontactName.Text = ObjOrg.objSupplier.ContactName;

				txtSupplierBusinessPhone1.Text = ObjOrg.objSupplier.BussinessPhone.Split('-')[0];
				txtSupplierBusinessPhone2.Text = ObjOrg.objSupplier.BussinessPhone.Split('-')[1];
				txtSupplierBusinessPhone3.Text = ObjOrg.objSupplier.BussinessPhone.Split('-')[2];
				txtSuppliersownerEmail.Text = ObjOrg.objSupplier.OwnerManagerEmail;
			}

			if (ObjOrg.objClient != null)
			{
				txtclientsCompanyName.Text = ObjOrg.objClient.CompanyName;
				ddlclientsCountry.SelectedValue = Convert.ToString(ObjOrg.objClient.CountryID);
				ddlclientsProvince.SelectedValue = Convert.ToString(ObjOrg.objClient.StateId);
				txtClientCity.Text = Convert.ToString(ObjOrg.objClient.City);
				txtclientsContactName.Text = ObjOrg.objClient.ContactName;
				txtClientBusinessPhone1.Text = ObjOrg.objClient.BussinessPhone.Split('-')[0];
				txtClientBusinessPhone2.Text = ObjOrg.objClient.BussinessPhone.Split('-')[1];
				txtClientBusinessPhone3.Text = ObjOrg.objClient.BussinessPhone.Split('-')[2];
				txtclientsEmail.Text = ObjOrg.objClient.OwnerManagerEmail;
			}

			chkotsPrivacy.Checked = true;
			chktiretraxPrivacy.Checked = true;

			foreach (int item in ObjOrg.CertificationID)
			{
				foreach (RepeaterItem rptItem in rptStakeCertificates.Items)
				{
					if (item == Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value))
					{
						((CheckBox)rptItem.FindControl("chk")).Checked = true;
					}
				}
			}

			foreach (int item in ObjOrg.CertificationID)
			{
				foreach (RepeaterItem rptItem in rptTireCertificates.Items)
				{
					if (item == Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value))
					{
						((CheckBox)rptItem.FindControl("chk")).Checked = true;
					}
				}
			}

			//foreach (OrganizationInfo.Organization_Business item in ObjOrg.ObjOrgBusiness)
			//{
			//    foreach (RepeaterItem rptItem in rptBusiness.Items)
			//    {
			//        if (item.BusinessID == Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value))
			//        {
			//            if (item.IsNew)
			//                ((CheckBox)rptItem.FindControl("chkY")).Checked = true;
			//            else
			//                ((CheckBox)rptItem.FindControl("chkN")).Checked = true;
			//        }
			//    }
			//}
		}
		*/
    #endregion

    #region AddBasicInfo

    protected void lnkbtnStep1_Click(object sender, EventArgs e)
    {
        try
        {
            string standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            string[] arr = standardstewardshipIds.Split(',');
            for (int i = 0; i < arr.Count(); i++)
            {

                if (ddlOrganizationType.SelectedValue == arr[i]) //LookupsManagement.LookupType.OrganizationTypes_Stewardship
                {
                    if (OrganizationInfo.CheckStateAvailableForStewarship(Conversion.ParseInt(ddlBusinessProvince.SelectedValue)) > 0)
                    {
                        lblStewardshipExists.Visible = true;
                        return;
                    }
                }
            }
            int organizationIdWithSimilerEmail = 0;
            organizationIdWithSimilerEmail = OrganizationInfo.getOrganizationIdByEmail(txtPrimaryContactEmail.Text.Trim(), Conversion.ParseInt(ddlBusinessProvince.SelectedValue));
            if (organizationIdWithSimilerEmail > 0)
            {
                lblemailalreadyexists.Text = ResourceMgr.GetError("Email already exists for this state. Choose another");
                lblemailalreadyexists.Visible = true;
                return;
            }
            else
            {
                lblemailalreadyexists.Visible = false;
            }
            OrganizationInfo objOrg = new OrganizationInfo();

            if (Utils.IsNumeric(hdnOrganizationID.Value) == true)
                objOrg.OrganizationId = Convert.ToInt32(hdnOrganizationID.Value);
            else
                objOrg.OrganizationId = -1;

            objOrg.LegalName = txtBusinessLegalName.Text.Trim();
            objOrg.DBAName = txtDBAName.Text.Trim();
            objOrg.OrganizationTypeId = Convert.ToInt32(ddlOrganizationType.SelectedValue);
            ViewState["OrganizationTypeId"] = objOrg.OrganizationTypeId.ToString();
            objOrg.OrganizationSubTypeID = Convert.ToInt32(ddlOrganizationSubType.SelectedValue);
            //string SubIDs = "";
            //foreach (ListItem item in ddlOrganizationSubType.Items)
            //{
            //    if (item.Selected)
            //    {
            //        SubIDs += item.Value + ",";
            //    }
            //}

            //SubIDs = SubIDs.TrimEnd(',');
            ViewState["OrganizationSubTypeId"] = objOrg.OrganizationSubTypeID.ToString();

            objOrg.Website = txtBusinessWebsite.Text.Trim();

            objOrg.AccountingInterfaceId = Convert.ToInt32(ddlBusinessAccountingInterface.SelectedValue);
            objOrg.InventoryInterfaceId = Convert.ToInt32(ddlBusinessInventoryInterface.SelectedValue);
            objOrg.IsAutoFundTransfer = chkBusinessAcceptAutoFundTransfers.Checked;
            objOrg.IsActive = true;
            objOrg.IsOrganization = true;
            objOrg.LanguageId = LanguageId;

            int RoleId = Convert.ToInt32(UserInfo.UserRole.Stakeholder);

            if (Convert.ToInt32(ddlOrganizationType.SelectedValue) == Convert.ToInt32(LookupsManagement.LookupType.OrganizationTypes_Stewardship) ||
                Convert.ToInt32(ddlOrganizationType.SelectedValue) == Convert.ToInt32(LookupsManagement.LookupType.OrganizationTypes_GlobalSteward) ||
                Convert.ToInt32(ddlOrganizationType.SelectedValue) == Convert.ToInt32(LookupsManagement.LookupType.OrganizationTypes_LocalSteward)
                )
            {
                RoleId = Convert.ToInt32(UserInfo.UserRole.Stewardship);
            }

            objOrg.RoleId = RoleId;

            ContactInfo objPrimaryContact = new ContactInfo();

            objPrimaryContact.FirstName = txtPrimaryContactFirstName.Text.Trim();
            objPrimaryContact.LastName = txtPrimaryContactLastName.Text.Trim();
            objPrimaryContact.ContactTitleId = Convert.ToInt32(ddlPrimaryContactTitle.SelectedValue);
            objPrimaryContact.Email = txtPrimaryContactEmail.Text.Trim();
            ViewState["PrimaryEmail"] = txtPrimaryContactEmail.Text.Trim();
            objPrimaryContact.IsActive = true;
            objPrimaryContact.IsPrimary = true;
            objPrimaryContact.LanguageId = LanguageId;
            //objPrimaryContact.ContactTypeId = Convert.ToInt32(ContactInfo.ContactTypes.Business);
            objPrimaryContact.ContactTypeId = Convert.ToInt32(LookupsManagement.LookupType.ContactTypes_Business);

            Phones objPrimaryContactBusinessPhone = new Phones();

            objPrimaryContactBusinessPhone.Number = txtPrimaryContactBusinessPhone1.Text.Trim() + "-" + txtPrimaryContactBusinessPhone2.Text.Trim() + "-" + txtPrimaryContactBusinessPhone3.Text.Trim();
            objPrimaryContactBusinessPhone.Extension = txtPrimaryContactBusinessPhoneExtension.Text.Trim();
            objPrimaryContactBusinessPhone.IsActive = true;
            //objPrimaryContactBusinessPhone.PhoneTypeId = Convert.ToInt32(Phones.PhoneType.Business);
            objPrimaryContactBusinessPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Business);

            Phones objPrimaryContactCellPhone = new Phones();

            objPrimaryContactCellPhone.Number = txtPrimaryContactCellPhone1.Text.Trim() + "-" + txtPrimaryContactCellPhone2.Text.Trim() + "-" + txtPrimaryContactCellPhone3.Text.Trim();
            objPrimaryContactCellPhone.IsAcceptTextMessages = chkPrimaryContactAcceptTextMessages.Checked;
            objPrimaryContactCellPhone.IsActive = true;
            //objPrimaryContactCellPhone.PhoneTypeId = Convert.ToInt32(Phones.PhoneType.Cell);
            objPrimaryContactCellPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Cell);

            ContactInfo objBillingContact = new ContactInfo();

            objBillingContact.FirstName = txtBillingContactFirstName.Text.Trim();
            objBillingContact.LastName = txtBillingContactLastName.Text.Trim();
            objBillingContact.ContactTitleId = Convert.ToInt32(ddlBillingContactTitle.SelectedValue);
            objBillingContact.Email = txtBillingContactEmail.Text.Trim();
            objBillingContact.IsActive = true;
            objBillingContact.IsPrimary = false;
            objBillingContact.LanguageId = LanguageId;
            //objBillingContact.ContactTypeId = Convert.ToInt32(ContactInfo.ContactTypes.Billing);
            objBillingContact.ContactTypeId = Convert.ToInt32(LookupsManagement.LookupType.ContactTypes_Billing);

            Phones objBillingContactBusinessPhone = new Phones();

            objBillingContactBusinessPhone.Number = txtBillingContactPhoneNumber1.Text.Trim() + "-" + txtBillingContactPhoneNumber2.Text.Trim() + "-" + txtBillingContactPhoneNumber3.Text.Trim();
            objBillingContactBusinessPhone.Extension = txtBillingContactPhoneExtension.Text.Trim();
            objBillingContactBusinessPhone.IsActive = true;
            //objBillingContactBusinessPhone.PhoneTypeId = Convert.ToInt32(Phones.PhoneType.Business);
            objBillingContactBusinessPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Business);

            Phones objBillingContactCellPhone = new Phones();

            objBillingContactCellPhone.Number = txtBillingContactCellNumber1.Text.Trim() + "-" + txtBillingContactCellNumber2.Text.Trim() + "-" + txtBillingContactCellNumber3.Text.Trim();
            objBillingContactCellPhone.IsAcceptTextMessages = chkBillingContactAcceptTextMessages.Checked;
            objBillingContactCellPhone.IsActive = true;
            //objBillingContactCellPhone.PhoneTypeId = Convert.ToInt32(Phones.PhoneType.Cell);
            objBillingContactCellPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Cell);

            OrganizationInfo.Organization_Address objBusinessOrganization_Address = new OrganizationInfo.Organization_Address();

            objBusinessOrganization_Address.ZipCodeID = Convert.ToInt32(hdnBusinessZipCodeId.Value);
            objBusinessOrganization_Address.ZipPostalCode = txtBusinessZipCode.Text.Trim();
            objBusinessOrganization_Address.Address1 = txtBusinessAddress1.Text.Trim();
            objBusinessOrganization_Address.Address2 = txtBusinessAddress2.Text.Trim();
            objBusinessOrganization_Address.City = txtBuinessCity.Text;
            objBusinessOrganization_Address.CityId = Convert.ToDouble(hfCityId.Value);
            objBusinessOrganization_Address.StateID = Convert.ToInt32(ddlBusinessProvince.SelectedValue);
            objBusinessOrganization_Address.CountryID = Convert.ToInt32(ddlBusinessCountry.SelectedValue);
            objBusinessOrganization_Address.DateCreated = DateTime.Now;
            objBusinessOrganization_Address.IsActive = true;
            //objBusinessOrganization_Address.Organization_AddressTypeId = Convert.ToInt32(OrganizationInfo.Organization_Address.Organization_AddressType.Business);
            objBusinessOrganization_Address.Organization_AddressTypeId = Convert.ToInt32(LookupsManagement.LookupType.OrganizationAddressType_Business);

            OrganizationInfo.Organization_Address objMailingOrganization_Address = new OrganizationInfo.Organization_Address();

            objMailingOrganization_Address.ZipCodeID = Convert.ToInt32(hdnMailingZipCodeId.Value);
            objMailingOrganization_Address.ZipPostalCode = txtMailingZipCode.Text.Trim();
            objMailingOrganization_Address.Address1 = txtMailingAddress1.Text.Trim();
            objMailingOrganization_Address.Address2 = txtMailingAddress2.Text.Trim();
            objMailingOrganization_Address.City = txtMailingCity.Text;
            objMailingOrganization_Address.CityId = Convert.ToDouble(hfmailingCityId.Value);
            objMailingOrganization_Address.StateID = Convert.ToInt32(ddlMailingState.SelectedValue);
            objMailingOrganization_Address.CountryID = Convert.ToInt32(ddlMailingCountry.SelectedValue);
            objMailingOrganization_Address.DateCreated = DateTime.Now;
            objMailingOrganization_Address.IsActive = true;
            //objMailingOrganization_Address.Organization_AddressTypeId = Convert.ToInt32(OrganizationInfo.Organization_Address.Organization_AddressType.Mailing);
            objMailingOrganization_Address.Organization_AddressTypeId = Convert.ToInt32(LookupsManagement.LookupType.OrganizationAddressType_Mailing);

            string catIDs = "";
            foreach (ListItem item in chkProductId.Items)
            {
                if (item.Selected)
                {
                    catIDs += item.Value + ",";
                }
            }

            catIDs = catIDs.TrimEnd(',');

            OrganizationInfo.SavePrimaryInfo(objOrg, objPrimaryContact, objPrimaryContactBusinessPhone, objPrimaryContactCellPhone, objBillingContact, objBillingContactBusinessPhone, objBillingContactCellPhone, objBusinessOrganization_Address, objMailingOrganization_Address, catIDs);


            
            foreach (ListItem item in chkProductId.Items)
            {
                DataSet Check = Product.GetAllSubCategories(Convert.ToInt32(item.Value));
                if (Check != null && Check.Tables[0].Rows.Count > 0 && item.Selected)
                {
                    string SubIds = "";
                    foreach(DataRow row in Check.Tables[0].Rows)
                    {
                        SubIds += row["SubProductId"] + ",";
                    }
                    SubIds = SubIds.TrimEnd(',');
                    Product.InsertProductTypes(Convert.ToInt32(objOrg.OrganizationId), Convert.ToInt32(item.Value), SubIds);
                }
            }

            hdnOrganizationID.Value = Convert.ToString(objOrg.OrganizationId);
            int organizationtypeid = Convert.ToInt32(ddlOrganizationType.SelectedItem.Value);
            // OrganizationInfo.SetStatus(OrganizationStatus.Pending, objOrg.OrganizationId, "Registered", organizationtypeid);
            // Utils.GetLookUpData<DropDownList>(ref ddlRoleList, LookUps.RoleName, Conversion.ParseInt(ddlOrganizationType.SelectedValue));

            if (objOrg.OrganizationId > 0)
            {
                //if (Page.IsValid)
                //{
                //    string keyfor = ddlOrganizationType.SelectedItem.Text.Trim();
                //    string tabName = "";

                //    switch (keyfor)
                //    {
                //        case "Consumer":
                //            tabName = "tab-7";
                //            break;
                //        default:
                //            break;
                //    }

                //    ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Step2", String.Format("GotoNextStep('{2}');SetHiddenFieldValue('{0}','{1}');", hdnOrganizationID.ClientID, objOrg.OrganizationId,tabName), true);
                //}

            }



            //Utils.GetLookUpData<DropDownList>(ref ddlRoleList, LookUps.RoleTypes, Conversion.ParseInt(ddlOrganizationType.SelectedValue));//Role Types by organizationTypeId

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "RegistrationForm.lnkbtnStep1_Click", ex);
        }
    }

    #endregion

    #region Additional Location

    protected void lnkBtnLocationAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //ddlRoleList.Items.Clear();
            //Utils.GetLookUpData<DropDownList>(ref ddlRoleList, LookUps.RoleName, Conversion.ParseInt(ddlOrganizationType.SelectedValue));

            OrganizationInfo objOrg = new OrganizationInfo();

            objOrg.LegalName = txtLocationBusinessName.Text.Trim();
            objOrg.DBAName = txtLocationDBAName.Text.Trim();
            objOrg.IsLocationEventPermanent = chkLocationPermanent.Checked;

            if (objOrg.IsLocationEventPermanent == false)
            {
                try
                {
                    objOrg.LocationEventTypeId = Convert.ToInt32(ddlLocationEventType.SelectedValue);
                    objOrg.LocationEventStartDate = Convert.ToDateTime(txtLocationFromDate.Text, System.Globalization.CultureInfo.InvariantCulture);
                    objOrg.LocationEventEndDate = Convert.ToDateTime(txtLocationToDate.Text, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    objOrg.IsLocationEventPermanent = true;
                }
            }

            objOrg.LocationPermitNumber = txtLocationPermitNumber.Text.Trim();
            objOrg.IsActive = true;
            objOrg.IsOrganization = false;
            objOrg.LanguageId = LanguageId;
            objOrg.ParentId = Convert.ToInt32(hdnOrganizationID.Value);

            ContactInfo objContact = new ContactInfo();

            objContact.FirstName = txtLocationContactFirstName.Text.Trim();
            objContact.LastName = txtLocationContactLastName.Text.Trim();
            objContact.ContactTitleId = Convert.ToInt32(ddlLocationContactTitle.SelectedValue);
            objContact.Email = txtLocationContactEmail.Text.Trim();
            objContact.IsActive = true;
            objContact.IsPrimary = true;
            objContact.LanguageId = LanguageId;
            //objContact.ContactTypeId = Convert.ToInt32(ContactInfo.ContactTypes.Business);
            objContact.ContactTypeId = Convert.ToInt32(LookupsManagement.LookupType.ContactTypes_Business);

            Phones objBusinessPhone = new Phones();

            objBusinessPhone.Number = txtLocationBusinessPhone1.Text.Trim() + "-" + txtLocationBusinessPhone2.Text.Trim() + "-" + txtLocationBusinessPhone3.Text.Trim();
            objBusinessPhone.Extension = txtLocationBusinessPhoneExtension.Text.Trim();
            objBusinessPhone.IsActive = true;
            //objBusinessPhone.PhoneTypeId = Convert.ToInt32(Phones.PhoneType.Business);
            objBusinessPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Business);

            Phones objCellPhone = new Phones();

            objCellPhone.Number = txtLocationCellPhone1.Text.Trim() + "-" + txtLocationCellPhone2.Text.Trim() + "-" + txtLocationCellPhone3.Text.Trim();
            objCellPhone.IsAcceptTextMessages = chkLocationContactAcceptTextMessages.Checked;
            objCellPhone.IsActive = true;
            //objCellPhone.PhoneTypeId = Convert.ToInt32(Phones.PhoneType.Cell);
            objCellPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Cell);

            OrganizationInfo.Organization_Address objOrganization_Address = new OrganizationInfo.Organization_Address();

            objOrganization_Address.ZipCodeID = Convert.ToInt32(hdnLocationZipCodeID.Value);
            objOrganization_Address.ZipPostalCode = txtLocationZipCode.Text.Trim();
            objOrganization_Address.Address1 = txtLocationAddress1.Text.Trim();
            objOrganization_Address.Address2 = txtLocationAddress2.Text.Trim();
            objOrganization_Address.City = txtLocationCity.Text;
            objOrganization_Address.StateID = Convert.ToInt32(ddlLocationState.SelectedValue);
            objOrganization_Address.CountryID = Convert.ToInt32(ddlLocationCountry.SelectedValue);
            objOrganization_Address.DateCreated = DateTime.Now;
            objOrganization_Address.IsActive = true;
            //objOrganization_Address.Organization_AddressTypeId = Convert.ToInt32(OrganizationInfo.Organization_Address.Organization_AddressType.Business);
            objOrganization_Address.Organization_AddressTypeId = Convert.ToInt32(LookupsManagement.LookupType.OrganizationAddressType_Business);

            OrganizationInfo.SaveAdditionalLocationInfo(objOrg, objContact, objBusinessPhone, objCellPhone, objOrganization_Address);

            gvAdditionLocations.DataSource = OrganizationInfo.GetAdditionalLocationsByOrganizationId(Convert.ToInt32(hdnOrganizationID.Value));
            gvAdditionLocations.DataBind();
            LoadHeaderTextForAddLocations();

            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "ClearAdditionalLocationFields", "ClearAdditionalLocationFields();", true);

            txtLocationZipCode.Text = "";
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "RegistrationForm.lnkBtnAdditionalLocationAdd_Click", ex);
        }
    }

    #endregion

    #region Add Stakeholder Certifications

    protected void lnkBtnStep2_Click(object sender, EventArgs e)
    {

        //ddlRoleList.Items.Clear();
        //Utils.GetLookUpData<DropDownList>(ref ddlRoleList, LookUps.RoleName, Conversion.ParseInt(ddlOrganizationType.SelectedValue));

        if (Utils.IsNumeric(hdnOrganizationID.Value) == false)
            return;
        OrganizationInfo ObjOrg = new OrganizationInfo();
        ObjOrg.OrganizationId = Convert.ToInt32(hdnOrganizationID.Value);
        ObjOrg.CertificationID = new List<int>();

        bool IsCertifiedInspector = false;
        foreach (RepeaterItem rptItem in rptStakeCertificates.Items)
        {
            if (((CheckBox)rptItem.FindControl("chk")).Checked)
            {
                HiddenField hid = (HiddenField)rptItem.FindControl("hdnID");
                if (hid.Value == "16")
                    IsCertifiedInspector = true;
                ObjOrg.CertificationID.Add(Convert.ToInt32(hid.Value));
            }
        }

        //foreach (RepeaterItem rptItem in rptTireCertificates.Items)
        //{
        //    if (((CheckBox)rptItem.FindControl("chk")).Checked)
        //    {
        //        ObjOrg.CertificationID.Add(Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value));
        //    }
        //}

        OrganizationInfo.SaveCertifications(ObjOrg);

        /////////////////////////////////////Commented for testing //////////////////////////////////////////////

        if (IsCertifiedInspector)
            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Step3", "DisableInspectorTab();", true);
        else
        {
            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Step3", "EnableInspectorTab();", true);
            //    //ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Step3", "GotoNextStep();", true);
        }

        //////////////////////// End of Commented for testing here /////////////////////////////////////////////////
    }

    #endregion

    #region Add StewardShip Certifications

    protected void lnkBtnStepStewardship_Click(object sender, EventArgs e)
    {


        ///we need to add this fields in database via Lookup managament. Right now we are sending Hardcode id 1
        if (Utils.IsNumeric(hdnOrganizationID.Value) == false)
            return;
        OrganizationInfo ObjOrg = new OrganizationInfo();
        ObjOrg.OrganizationId = Convert.ToInt32(hdnOrganizationID.Value);
        ObjOrg.CertificationID = new List<int>();

        //        bool IsCertifiedInspector = false;
        //foreach (RepeaterItem rptItem in rptStakeCertificates.Items)
        //{
        //    if (((CheckBox)rptItem.FindControl("chk")).Checked)
        //    {
        //        HiddenField hid = (HiddenField)rptItem.FindControl("hdnID");
        //        if (hid.Value == "16")
        //            IsCertifiedInspector = true;
        ObjOrg.CertificationID.Add(1);
        //    }
        //}

        //foreach (RepeaterItem rptItem in rptTireCertificates.Items)
        //{
        //    if (((CheckBox)rptItem.FindControl("chk")).Checked)
        //    {
        //        ObjOrg.CertificationID.Add(Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value));
        //    }
        //}

        OrganizationInfo.SaveCertifications(ObjOrg);
        //if (IsCertifiedInspector)
        //    ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Step3", "DisableInspectorTab();", true);
        //else
        //{
        //ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Step3", "DisableInspectorTab();", true);
        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Step3", "GotoNextStep(3,'Stewardships');", true);
        //}


        //Utils.GetLookUpData<DropDownList>(ref ddlRoleList, LookUps.RoleName, Conversion.ParseInt(ddlOrganizationType.SelectedValue));
    }

    #endregion

    #region Add Supplier

    protected void lnkAddSupplier_Click(object sender, EventArgs e)
    {
        if (Utils.IsNumeric(hdnOrganizationID.Value) == false)
            return;

        OrganizationInfo.Supplier ObjOrgSupplier = new OrganizationInfo.Supplier();

        ObjOrgSupplier.SupplierID = Conversion.ParseInt(hdnSupplierId.Value);

        ObjOrgSupplier.CompanyName = txtSupplierCompanyName.Text;
        ObjOrgSupplier.CountryID = int.Parse(ddlSuppliersCountry.SelectedValue);
        ObjOrgSupplier.StateId = Convert.ToInt32(ddlSuppliersProvince.SelectedValue);
        ObjOrgSupplier.City = txtSupplierCity.Text;
        ObjOrgSupplier.ZipCodeId = Convert.ToInt32(hdnSupplierZipCode.Value);
        ObjOrgSupplier.ContactName = (txtSupplierscontactFirstName.Text.Trim() + " " + txtSupplierscontactLastName.Text.Trim()).Trim();
        ObjOrgSupplier.BussinessPhone = txtSupplierBusinessPhone1.Text.Trim() + txtSupplierBusinessPhone2.Text.Trim() + txtSupplierBusinessPhone3.Text.Trim();
        ObjOrgSupplier.Email = txtSuppliersownerEmail.Text;
        ObjOrgSupplier.IsActive = true;
        ObjOrgSupplier.BusinessPhoneExtention = txtSupplierBusinessPhoneExt.Text;
        ObjOrgSupplier.CellPhone = txtSupplierContactCellPhone1.Text.Trim() + txtSupplierContactCellPhone2.Text.Trim() + txtClientCellPhone3.Text.Trim();
        ObjOrgSupplier.OwnerManagerEmail = txtSuppliersownerEmail.Text;
        ObjOrgSupplier.LanguageID = LanguageId;
        ObjOrgSupplier.DateCreated = DateTime.Now;
        ObjOrgSupplier.CreatedBy = currentUserInfo.UserId;
        ObjOrgSupplier.Count = 0;


        if (OrganizationInfo.SaveSupplierByOrganizationId(Convert.ToInt32(hdnOrganizationID.Value), ObjOrgSupplier) == 0)
        {
            dvsuppliererrormsg.Visible = true;
        }
        else
        {
            dvsuppliererrormsg.Visible = false;
        }
        hdnSupplierId.Value = "0";
        gvSupplier.DataSource = OrganizationInfo.GetSupplierByOrganizationId(Convert.ToInt32(hdnOrganizationID.Value));
        gvSupplier.DataBind();
        LoadHeaderTextForSupplier();

        hdnSupplierCount.Value = Convert.ToString(gvSupplier.Rows.Count);

        if (gvSupplier.Rows.Count < 3)
        {
            //ClearSuppliersFields();
        }

        //ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "ClearSupplierFields", String.Format("ClearSupplierFields();SetHiddenFieldValue('{0}','{1}');", hdnSupplierCount.ClientID, gvSupplier.Rows.Count), true);

        //txtSupplierZipCode.Text = "";

    }

    #region Clear Fields

    private void ClearSuppliersFields()
    {
        txtSupplierCompanyName.Text = string.Empty;

        ddlSuppliersProvince.Items.Clear();
        txtSupplierCity.Text = string.Empty;

        txtSupplierscontactFirstName.Text = string.Empty;
        txtSupplierscontactLastName.Text = string.Empty;
        txtSupplierBusinessPhone1.Text = string.Empty;
        txtSupplierBusinessPhone2.Text = string.Empty;
        txtSupplierBusinessPhone3.Text = string.Empty;
        txtSuppliersownerEmail.Text = string.Empty;

        txtSupplierBusinessPhoneExt.Text = string.Empty;
        txtSupplierContactCellPhone1.Text = string.Empty;
        txtSupplierContactCellPhone2.Text = string.Empty;
        txtSupplierContactCellPhone3.Text = string.Empty;
        txtSupplierZipCode.Text = string.Empty;
    }

    #endregion

    #endregion

    #region Add Client

    protected void lnkAddClient_Click(object sender, EventArgs e)
    {
        if (Utils.IsNumeric(hdnOrganizationID.Value) == false)
            return;

        OrganizationInfo.Client objOrgClient = new OrganizationInfo.Client();

        objOrgClient.ClientID = -1;
        objOrgClient.CompanyName = txtclientsCompanyName.Text;
        objOrgClient.CountryID = int.Parse(ddlclientsCountry.SelectedValue);
        objOrgClient.StateId = Convert.ToInt32(ddlclientsProvince.SelectedValue);
        objOrgClient.City = txtClientCity.Text;
        objOrgClient.ZipCodeId = Convert.ToInt32(hdnClientZipCode.Value);
        objOrgClient.ContactName = (txtclientsContactFirstName.Text.Trim() + " " + txtclientsContactLastName.Text.Trim()).Trim();
        objOrgClient.BussinessPhone = txtClientBusinessPhone1.Text.Trim() + "-" + txtClientBusinessPhone2.Text.Trim() + "-" + txtClientBusinessPhone3.Text.Trim();
        objOrgClient.IsActive = true;
        objOrgClient.OwnerManagerEmail = txtclientsEmail.Text;
        objOrgClient.LanguageId = LanguageId;
        objOrgClient.DateCreated = DateTime.Now;
        objOrgClient.CreatedBy = currentUserInfo.UserId;

        OrganizationInfo.SaveClientByOrganizationId(Convert.ToInt32(hdnOrganizationID.Value), objOrgClient);

        gvClient.DataSource = OrganizationInfo.GetClientByOrganizationId(Convert.ToInt32(hdnOrganizationID.Value));
        gvClient.DataBind();
        LoadHeaderTextForClient();

        hdnClientCount.Value = Convert.ToString(gvClient.Rows.Count);
        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "ClearClientFields", String.Format("ClearClientFields();SetHiddenFieldValue('{0}','{1}');", hdnClientCount.ClientID, gvClient.Rows.Count), true);
        //txtClientZipCode.Text = "";
    }

    #endregion

    #region Submit Application

    protected void submit_Click(object sender, EventArgs e)
    {
        if (Utils.IsNumeric(hdnOrganizationID.Value) == false)
            return;

        List<OrganizationInfo.Organization_Business> lstOrganizationBusiness = new List<OrganizationInfo.Organization_Business>();

        //OrganizationInfo.Organization_Business objOrgBusiness;

        //foreach (RepeaterItem rptItem in rptBusiness.Items)
        //{
        //    objOrgBusiness = new OrganizationInfo.Organization_Business();

        //    CheckBox chkY = (CheckBox)rptItem.FindControl("chkY");
        //    CheckBox chkN = (CheckBox)rptItem.FindControl("chkN");

        //    if (chkY.Checked || chkN.Checked)
        //    {
        //        objOrgBusiness.BusinessID = Convert.ToInt32(((HiddenField)rptItem.FindControl("hdnID")).Value);
        //        objOrgBusiness.IsNew = chkY.Checked;

        //        lstOrganizationBusiness.Add(objOrgBusiness);
        //    }
        //}

        //Commented by wajid shah 09-04-2013

        int RoleId = Convert.ToInt32(UserInfo.UserRole.Stakeholder);

        //if (Convert.ToInt32(ddlOrganizationType.SelectedValue) == Convert.ToInt32(LookupsManagement.LookupType.OrganizationTypes_Stewardship))
        //{
        //    RoleId = Convert.ToInt32(UserInfo.UserRole.Stewardship);
        //}

        //if (OrganizationInfo.SubmitApplication(Convert.ToInt32(hdnOrganizationID.Value), RoleId, Convert.ToInt32(ddlOrganizationType.SelectedValue), lstOrganizationBusiness, Convert.ToInt32(Request.QueryString["SSID"])) == true)

        ////end comment

        UserInfo objUserInfo = null;

        //if (ViewState["UserName"] != null && Convert.ToString(ViewState["UserName"]) == txtLoginName.Text.Trim())
        //{
        //    ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Step6", "GotoNextStep();", true);
        //    return;
        //}

        //if (UserInfo.CheckLoginNameAvailable(txtLoginName.Text.Trim()) == true)
        // {
        objUserInfo = new UserInfo();

        objUserInfo.OrganizationId = Convert.ToInt32(hdnOrganizationID.Value);

        objUserInfo.Login = ViewState["PrimaryEmail"].ToString();// txtLoginName.Text.Trim();
        objUserInfo.Pwd = Encryption.Encrypt("000000");

        objUserInfo.PwdSalt = String.Empty;
        objUserInfo.IsActive = true;
        objUserInfo.TX_UserId = String.Empty;
        objUserInfo.LanguageId = LanguageId;
        objUserInfo.TimeZoneID = 1;
        objUserInfo.ContactId = 1;
        objUserInfo.IsApproved = false;
        objUserInfo.IsOrgAdmin = true;
        objUserInfo.DateCreated = DateTime.Now;

        objUserInfo.RoleId = 0;// "Role ID select from stored Procedure"  Conversion.ParseInt(ViewState["OrganizationTypeId"].ToString());
        objUserInfo.bitSetPassword = false;

        UserInfo.CreateStakeholderUser(objUserInfo);


        Emails email = new Emails();
        email.To = ViewState["PrimaryEmail"].ToString();
        email.From = "noreply@EPRTS.com";
        email.Subject = "Registration Submitted Email";
        Thread Email_Thread = new Thread(() => SendEmails(email, Emails.EmailType.RegistrationSubmissionEmail.ToString()));
        Email_Thread.Start();


        string standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
        if (OrganizationInfo.SubmitApplication(Convert.ToInt32(hdnOrganizationID.Value), Convert.ToInt32(ddlOrganizationType.SelectedValue), lstOrganizationBusiness, Convert.ToInt32(ViewState["StewardShipId"].ToString()), standardstewardshipIds) == true)
            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Thankyou", "ShowThankYou();", true);



        //OrganizationInfo org=new OrganizationInfo(Convert.ToInt32(hdnOrganizationID.Value));
        //Emails email = new Emails();
        //email.To = org.objSupplier.OwnerManagerEmail;
        //email.From = org.objSupplier.OwnerManagerEmail;
        //email.Subject = "Test It";
        //email.CC = org.objSupplier.OwnerManagerEmail;
        //email.BCC = org.objSupplier.OwnerManagerEmail;


    }

    private void SendEmails(Emails email, string type)
    {
        try
        {
            Emails.SendEmail(email, type);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "RegistrationFormUS SendEmails", ex);
        }

    }

    #endregion

    #region Login Info
    protected void txtLoginName_TextChanged(object sender, EventArgs e)
    {
        //dvUserRole.Style.Add("display", "block");
        //Utils.GetLookUpData<DropDownList>(ref ddlRoleList, LookUps.RoleName, Conversion.ParseInt(ddlOrganizationType.SelectedValue));
        //  if (UserInfo.CheckLoginNameAvailable(txtLoginName.Text.Trim()) == false)
        //{
        //    ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "ShowLoginExistsError", "ShowLoginExistsError();", true);
        //}
    }

    //protected void lnkBtnLoginInfo_Click(object sender, EventArgs e)
    //{
    //    UserInfo objUserInfo = null;

    //    if (ViewState["UserName"] != null && Convert.ToString(ViewState["UserName"]) == txtLoginName.Text.Trim())
    //    {
    //        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Step6", "GotoNextStep();", true);
    //        return;
    //    }

    //    //if (UserInfo.CheckLoginNameAvailable(txtLoginName.Text.Trim()) == true)
    //    // {
    //    objUserInfo = new UserInfo();

    //    objUserInfo.OrganizationId = Convert.ToInt32(hdnOrganizationID.Value);

    //    objUserInfo.Login = txtLoginName.Text.Trim();
    //    objUserInfo.Pwd = Encryption.Encrypt(txtPassword.Text.Trim());

    //    objUserInfo.PwdSalt = String.Empty;
    //    objUserInfo.IsActive = true;
    //    objUserInfo.TX_UserId = String.Empty;
    //    objUserInfo.LanguageId = LanguageId;
    //    objUserInfo.TimeZoneID = 1;
    //    objUserInfo.ContactId = 1;
    //    objUserInfo.IsApproved = false;
    //    objUserInfo.IsOrgAdmin = true;
    //    objUserInfo.DateCreated = DateTime.Now;


    //    //int RoleId = Convert.ToInt32(UserInfo.UserRole.Stakeholder);

    //    //if (Convert.ToInt32(ddlOrganizationType.SelectedValue) == Convert.ToInt32(LookupsManagement.LookupType.OrganizationTypes_Stewardship))
    //    //{
    //    //    RoleId = Convert.ToInt32(UserInfo.UserRole.Stewardship);
    //    //}

    //    //objUserInfo.RoleId = RoleId;
    //    objUserInfo.RoleId = Conversion.ParseInt(ddlRoleList.SelectedValue);
    //    //Utils.GetLookUpData<DropDownList>(ref ddlRoleList, LookUps.RoleName, new SqlParameter[] { new SqlParameter("@OrgTypeID", Conversion.ParseInt(ddlOrganizationType.SelectedValue)) });

    //    //objUserInfo.RoleId =UserInfo.UserRoleTypeByOrgType(Convert.ToInt32(ddlOrganizationType.SelectedValue));

    //    ////objUserInfo.RoleId = Convert.ToInt32(ddlOrganizationType.SelectedValue);
    //    UserInfo.CreateStakeholderUser(objUserInfo);

    //    if (objUserInfo.UserId > 0)
    //    {
    //        ViewState["UserName"] = txtLoginName.Text.Trim();
    //        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Step6", "GotoNextStep();", true);
    //    }
    //    //}
    //    //else
    //    //{
    //    //    ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "ShowLoginExistsError", "ShowLoginExistsError();", true);
    //    //}

    //}

    #endregion

    #region Event Handlers

    //private bool TriggerCountrySelectedIndexChangedEvent = true;
    //private bool TriggerStateSelectedIndexChangedEvent = true;

    protected void txtBusinessZipCode_TextChanged(object sender, EventArgs e)
    {
        DataTable dt; double stateid = 0.0;
        if (Convert.ToInt32(Request.QueryString["SSID"]) > 0)
            stateid = Convert.ToInt32(Request.QueryString["SSID"]);

        dt = OrganizationInfo.GetCityStateAndCountryByZipCode(txtBusinessZipCode.Text.Trim(), GlobalCountryID, stateid);

        if (dt.Rows.Count > 0)
        {
            loadDataFromZipcodeChanged(ZipCodeOperations.BusinessZipCode);
            txtPrimaryContactEmail_TextChanged(null, null);
        }
        else
        {
            txtBusinessAddress1.Text = "";
            lblBusinessZipCode.Text = ResourceMgr.GetError("* Zip Code does not exist in this state.");
        }
    }
    protected void txtMailingZipCode_TextChanged(object sender, EventArgs e)
    {
        loadDataFromZipcodeChanged(ZipCodeOperations.MailingZipCode);
    }
    protected void txtLocationZipCode_TextChanged(object sender, EventArgs e)
    {
        loadDataFromZipcodeChanged(ZipCodeOperations.LocationZipCode);
    }
    protected void txtSupplierZipCode_TextChanged(object sender, EventArgs e)
    {
        loadDataFromZipcodeChanged(ZipCodeOperations.SupplierZipCode);
    }
    protected void txtClientZipCode_TextChanged(object sender, EventArgs e)
    {
        loadDataFromZipcodeChanged(ZipCodeOperations.ClientZipCode);
    }
    ///HANDLES ZIPCODE CHANGED EVENT
    ///
    private enum ZipCodeOperations
    {
        BusinessZipCode,
        MailingZipCode,
        LocationZipCode,
        SupplierZipCode,
        ClientZipCode
    }
    private void loadDataFromZipcodeChanged(ZipCodeOperations zco)
    {
        // DataTable dt = null;
        switch (zco)
        {
            case ZipCodeOperations.BusinessZipCode:
                loadDatafromZipcode(txtBusinessZipCode, hdnBusinessZipCodeId, txtBuinessCity, ddlBusinessCountry, ddlBusinessProvince, txtBusinessAddress1, lblBusinessZipCode);
                break;
            case ZipCodeOperations.SupplierZipCode:
                loadDatafromZipcode(txtSupplierZipCode, hdnSupplierZipCode, txtSupplierCity, ddlSuppliersCountry, ddlSuppliersProvince, txtSupplierscontactFirstName, lblSupplierZipCode);
                break;
            case ZipCodeOperations.LocationZipCode:
                loadDatafromZipcode(txtLocationZipCode, hdnLocationZipCodeID, txtLocationCity, ddlLocationCountry, ddlLocationState, txtLocationAddress1, lblLocationZipCode);
                break;
            case ZipCodeOperations.MailingZipCode:
                loadDatafromZipcode(txtMailingZipCode, hdnMailingZipCodeId, txtMailingCity, ddlMailingCountry, ddlMailingState, txtMailingAddress1, lblMailingZipCode);
                break;
            case ZipCodeOperations.ClientZipCode:
                loadDatafromZipcode(txtClientZipCode, hdnClientZipCode, txtClientCity, ddlclientsCountry, ddlclientsProvince, txtclientsContactFirstName, lblClientZipCode);
                break;
        }
    }
    private void loadDatafromZipcode(TextBox txtzipcode, HiddenField hdnZipCode, TextBox txtCity, DropDownList ddlcountry, DropDownList ddlprovince, TextBox txtfirstName, Label lblZipCode)
    {
        DataTable dt;
        dt = OrganizationInfo.GetCityStateAndCountryByZipCode(txtzipcode.Text.Trim(), GlobalCountryID);

        if (dt.Rows.Count > 0)
        {
            hdnZipCode.Value = Convert.ToString(dt.Rows[0]["ZipcodeID"]);
            txtCity.Text = Convert.ToString(dt.Rows[0]["CityName"]);
            if (ddlcountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["CountryId"])) != null)
                ddlcountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryId"]);
            System.Data.SqlClient.SqlParameter[] prm;
            prm = new System.Data.SqlClient.SqlParameter[1];
            prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId", ddlcountry.SelectedValue);
            Utils.GetLookUpData<DropDownList>(ref ddlprovince, LookUps.State, prm);
            prm = null;
            if (ddlprovince.Items.FindByValue(Convert.ToString(dt.Rows[0]["StateId"])) != null)
                ddlprovince.SelectedValue = Convert.ToString(dt.Rows[0]["StateId"]);
            txtfirstName.Focus();
            txtfirstName.Text = "";
            lblZipCode.Text = "";
        }
        else
        {
            txtfirstName.Text = "";
            lblZipCode.Text = ResourceMgr.GetError("* Zipcode does not exist.");
        }
    }

    //protected void dllState_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (TriggerStateSelectedIndexChangedEvent == false)
    //    {
    //        return;
    //    }
    //    ListControl ddl = ((ListControl)sender);

    //    if (Utils.IsNumeric(ddl.SelectedValue) && Convert.ToInt32(ddl.SelectedValue) > 0)
    //    {
    //        DataTable dt = null;
    //        dt = OrganizationInfo.GetCountryByStateID(Convert.ToInt32(ddl.SelectedValue));

    //        if (dt.Rows.Count > 0)
    //        {
    //            if (sender.Equals(ddlMailingState))
    //            {
    //                txtMailingCity.Text = "";
    //                txtMailingZipCode.Text = "";
    //                if (ddlMailingCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["CountryId"])) != null)
    //                    ddlMailingCountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryId"]);
    //            }

    //            if (sender.Equals(ddlBusinessProvince))
    //            {
    //                txtBuinessCity.Text = "";
    //                txtBusinessZipCode.Text = "";
    //                if (ddlBusinessCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["CountryId"])) != null)
    //                    ddlBusinessCountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryId"]);
    //            }

    //            if (sender.Equals(ddlSuppliersProvince))
    //            {
    //                txtSupplierCity.Text = "";
    //                txtSupplierZipCode.Text = "";
    //                if (ddlSuppliersCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["CountryId"])) != null)
    //                    ddlSuppliersCountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryId"]);
    //            }

    //            if (sender.Equals(ddlclientsProvince))
    //            {
    //                txtClientCity.Text = "";
    //                txtClientZipCode.Text = "";
    //                if (ddlclientsCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["CountryId"])) != null)
    //                    ddlclientsCountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryId"]);
    //            }

    //            if (sender.Equals(ddlLocationState))
    //            {
    //                txtLocationCity.Text = "";
    //                txtLocationZipCode.Text = "";
    //                if (ddlLocationCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["CountryId"])) != null)
    //                    ddlLocationCountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryId"]);
    //            }
    //        }
    //    }
    //}

    //protected void dllCountry_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (TriggerCountrySelectedIndexChangedEvent == false)
    //    {
    //        return;
    //    }
    //    ListControl ddl = ((ListControl)sender);

    //    if (Utils.IsNumeric(ddl.SelectedValue) && Convert.ToInt32(ddl.SelectedValue) > 0)
    //    {
    //        System.Data.SqlClient.SqlParameter[] prm;

    //        prm = new System.Data.SqlClient.SqlParameter[1];
    //        prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId", Convert.ToInt32(ddl.SelectedValue));

    //        if (sender.Equals(ddlMailingCountry))
    //        {
    //            txtMailingCity.Text = "";
    //            txtMailingZipCode.Text = "";
    //            Utils.GetLookUpData<DropDownList>(ref ddlMailingState, LookUps.State, prm);
    //        }

    //        if (sender.Equals(ddlBusinessCountry))
    //        {
    //            txtBuinessCity.Text = "";
    //            txtBusinessZipCode.Text = "";
    //            Utils.GetLookUpData<DropDownList>(ref ddlBusinessProvince, LookUps.State, prm);
    //        }

    //        if (sender.Equals(ddlSuppliersCountry))
    //        {
    //            txtSupplierCity.Text = "";
    //            txtSupplierZipCode.Text = "";
    //            Utils.GetLookUpData<DropDownList>(ref ddlSuppliersProvince, LookUps.State, prm);
    //        }

    //        if (sender.Equals(ddlclientsCountry))
    //        {
    //            txtClientCity.Text = "";
    //            txtClientZipCode.Text = "";
    //            Utils.GetLookUpData<DropDownList>(ref ddlclientsProvince, LookUps.State, prm);
    //        }

    //        if (sender.Equals(ddlLocationCountry))
    //        {
    //            txtLocationCity.Text = "";
    //            txtLocationZipCode.Text = "";
    //            Utils.GetLookUpData<DropDownList>(ref ddlLocationState, LookUps.State, prm);
    //        }

    //        prm = null;
    //    }
    //}

    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditSupplier")
        {
            hdnSupplierId.Value = (e.CommandArgument).ToString();
            OrganizationInfo.Supplier ObjOrgSupplier = new OrganizationInfo.Supplier(Conversion.ParseInt(hdnSupplierId.Value));

            hdnSupplierId.Value = ObjOrgSupplier.SupplierID.ToString();
            txtSupplierCompanyName.Text = ObjOrgSupplier.CompanyName;
            ddlSuppliersCountry.SelectedValue = ObjOrgSupplier.CountryID.ToString();
            (ddlSuppliersProvince.SelectedValue) = ObjOrgSupplier.StateId.ToString();
            txtSupplierCity.Text = ObjOrgSupplier.City;
            txtSupplierZipCode.Text = ObjOrgSupplier.Zipcode;
            //   
            DataTable dt;
            dt = OrganizationInfo.GetCityStateAndCountryByZipCode(ObjOrgSupplier.Zipcode);

            if (dt.Rows.Count > 0)
            {
                //  hdnZipCode.Value = Convert.ToString(dt.Rows[0]["ZipcodeID"]);
                txtSupplierCity.Text = Convert.ToString(dt.Rows[0]["CityName"]);
                if (ddlSuppliersCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["CountryId"])) != null)
                    ddlSuppliersCountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryId"]);
                System.Data.SqlClient.SqlParameter[] prm;
                prm = new System.Data.SqlClient.SqlParameter[1];
                prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId", ddlSuppliersCountry.SelectedValue);
                Utils.GetLookUpData<DropDownList>(ref ddlSuppliersProvince, LookUps.State, prm);
                prm = null;
                if (ddlSuppliersProvince.Items.FindByValue(Convert.ToString(dt.Rows[0]["StateId"])) != null)
                    ddlSuppliersProvince.SelectedValue = Convert.ToString(dt.Rows[0]["StateId"]);
            }
            hdnSupplierZipCode.Value = ObjOrgSupplier.ZipCodeId.ToString();
            string[] contactnamearray = ObjOrgSupplier.ContactName.Split(' ');
            txtSupplierscontactFirstName.Text = contactnamearray[0]; txtSupplierscontactLastName.Text = contactnamearray[1];
            txtSupplierBusinessPhone1.Text = ObjOrgSupplier.BussinessPhone.Substring(0, 3);
            txtSupplierBusinessPhone2.Text = ObjOrgSupplier.BussinessPhone.Substring(3, 3);
            txtSupplierBusinessPhone3.Text = ObjOrgSupplier.BussinessPhone.Substring(6, 4);
            txtSuppliersownerEmail.Text = ObjOrgSupplier.Email;
            txtSupplierBusinessPhoneExt.Text = ObjOrgSupplier.BusinessPhoneExtention;
            txtSupplierContactCellPhone1.Text = ObjOrgSupplier.CellPhone.Substring(0, 3);
            txtSupplierContactCellPhone2.Text = ObjOrgSupplier.CellPhone.Substring(3, 3);
            txtSupplierContactCellPhone3.Text = ObjOrgSupplier.CellPhone.Substring(6, 4);
            txtSuppliersownerEmail.Text = ObjOrgSupplier.OwnerManagerEmail;
        }
        else
            if (e.CommandName == "DeleteSupplier")
            {
                OrganizationInfo.DeleteSupplierBySupplierId(Convert.ToInt32(e.CommandArgument));
                lblSupplierStatus.Visible = true;
                lblSupplierStatus.Text = "Successfully deleted";
            }
            else
                lblSupplierStatus.Visible = false;

        gvSupplier.DataSource = OrganizationInfo.GetSupplierByOrganizationId(Convert.ToInt32(hdnOrganizationID.Value));
        gvSupplier.DataBind();
        LoadHeaderTextForSupplier();

        hdnSupplierCount.Value = Convert.ToString(gvSupplier.Rows.Count);

        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "SetHiddenFieldValue", String.Format("SetHiddenFieldValue('{0}','{1}');", hdnSupplierCount.Value, gvSupplier.Rows.Count), true);
    }

    protected void gvClient_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteClient")
            OrganizationInfo.DeleteClientByClientId(Convert.ToInt32(e.CommandArgument));

        gvClient.DataSource = OrganizationInfo.GetClientByOrganizationId(Convert.ToInt32(hdnOrganizationID.Value));
        gvClient.DataBind();
        LoadHeaderTextForClient();

        hdnClientCount.Value = Convert.ToString(gvClient.Rows.Count);

        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "SetHiddenFieldValue", String.Format("SetHiddenFieldValue('{0}','{1}');", hdnClientCount.ClientID, gvClient.Rows.Count), true);
    }

    protected void gvAdditionLocations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteLocation")
            OrganizationInfo.DeleteAdditionalLocationByOrganizationId(Convert.ToInt32(e.CommandArgument));

        gvAdditionLocations.DataSource = OrganizationInfo.GetAdditionalLocationsByOrganizationId(Convert.ToInt32(hdnOrganizationID.Value));
        gvAdditionLocations.DataBind();
        LoadHeaderTextForAddLocations();
    }

    protected void rslnkbtnBillingSameAsFacility_Click(object sender, EventArgs e)
    {
        txtMailingZipCode.Text = txtBusinessZipCode.Text.Trim();
        txtMailingZipCode_TextChanged(null, null);

        txtMailingAddress1.Text = txtBusinessAddress1.Text.Trim() == txtBusinessAddress1.Attributes["WaterMarkText"] ? txtMailingAddress1.Attributes["WaterMarkText"] : txtBusinessAddress1.Text.Trim();
        txtMailingAddress2.Text = txtBusinessAddress2.Text.Trim() == txtBusinessAddress2.Attributes["WaterMarkText"] ? txtMailingAddress2.Attributes["WaterMarkText"] : txtBusinessAddress2.Text.Trim();

        txtMailingCity.Text = txtBuinessCity.Text.Trim();

        if (ddlBusinessProvince.SelectedItem != null && ddlMailingState.Items.FindByValue(ddlBusinessProvince.SelectedValue) != null)
        {
            ddlMailingState.SelectedValue = ddlBusinessProvince.SelectedValue;
            //TriggerStateSelectedIndexChangedEvent = false;
        }

        if (ddlBusinessCountry.SelectedItem != null && ddlMailingCountry.Items.FindByValue(ddlBusinessCountry.SelectedValue) != null)
        {
            ddlMailingCountry.SelectedValue = ddlBusinessCountry.SelectedValue;
            //TriggerCountrySelectedIndexChangedEvent = false;
        }
    }

    #endregion

    protected void resourceBTNGO_Click(object sender, EventArgs e)
    {
        Response.Redirect("login");
    }




    protected void txtPrimaryContactEmail_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtPrimaryContactEmail.Text) && !string.IsNullOrWhiteSpace(txtPrimaryContactEmail.Text))
        {

            int organizationIdWithSimilerEmail = 0;
            organizationIdWithSimilerEmail = OrganizationInfo.getOrganizationIdByEmail(txtPrimaryContactEmail.Text.Trim(), Conversion.ParseInt(ddlBusinessProvince.SelectedValue));
            if (organizationIdWithSimilerEmail > 0)
            {
                lblemailalreadyexists.Visible = true;
            }
            else
            {
                lblemailalreadyexists.Visible = false;
            }
        }
        else if(string.IsNullOrEmpty(txtPrimaryContactEmail.Text))
        {
            lblemailalreadyexists.Visible = false;
        }
    }
}
