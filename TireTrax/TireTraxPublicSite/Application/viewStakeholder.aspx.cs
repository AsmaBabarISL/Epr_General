using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Threading;

public partial class Application_viewStakeHolder : BasePage
{
    public int GlobalCountryID = 235; // USA CountryId
    public int OrganizationID
    {
        get
        {
            if (!string.IsNullOrEmpty(Conversion.ParseString(Request.QueryString["OrganizationId"])))
            {
                return Conversion.ParseInt(Request.QueryString["OrganizationId"]);
            }
            return 0;
        }
    }
    public string pageId = "";
    string standardstewardshipIds = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStakeholders','{0}');", ResourceMgr.GetMessage("Stakeholders")), true);
        if (!IsPostBack)
        {
            standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
                BindDropDowns();
                viewStakeholdersForApproval();
                lblinfo.Visible = false;
        }

        pageId = Request.QueryString.Get("PageId");
        if (pageId == "1")
        {
            pageId = "Stakeholders";           
        }
        else
        {
            pageId = "Applications";
        }
    }

    private void BindDropDowns()
    {
        Utils.GetLookUpData<DropDownList>(ref ddlCountry, LookUps.CountryIdandName);
        Utils.GetLookUpData<DropDownList>(ref ddlState, LookUps.State, new SqlParameter[] { new SqlParameter("@CountryId", Conversion.ParseInt(ddlCountry.SelectedValue)) });
    }
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(pageId);
    }
    protected void viewStakeholdersForApproval()
    {
        OrganizationInfo organizationInfo = new OrganizationInfo(OrganizationID, true);
        int status = OrganizationInfo.getStewardshipStatusByOrganizationId(OrganizationID, standardstewardshipIds, organizationInfo.OrganizationTypeId);
        OrganizationStatus s = (OrganizationStatus)status;
        if (status == 1)
            imgBtnPending.Visible = false;
        else if (status == 2)
            imgbtnApprove.Visible = false;
        else if (status == 3)
            imgbtnReject.Visible = false;
        else if (status == 4)
            imgbtnDelete.Visible = false;

        
        lblStatusNotes.Text = (OrganizationInfo.getLatestNotesStatusByOrganizationId(OrganizationID));
        lblStatus.Text = s.ToString();
        ltrBusinessName.Text = organizationInfo.LegalName;
        txtBusinessName.Text = organizationInfo.LegalName;
        ltrDBAName.Text = organizationInfo.DBAName;
        txtDBAName.Text = organizationInfo.DBAName;
        ltrFirstName.Text = organizationInfo.FirstName;
        txtFirstName.Text = organizationInfo.FirstName;
        ltrLastName.Text = organizationInfo.LastName;
        txtLastName.Text = organizationInfo.LastName;
        ltrprimaryEmail.Text = organizationInfo.Email;
        txtPrimaryEmail.Text = organizationInfo.Email;
        ltrPhoneNumber.Text = organizationInfo.BusinessNumber;
        txtPhoneNumber.Text = organizationInfo.BusinessNumber;
        ltrPhoneExtension.Text = organizationInfo.BusinessExtension;
        txtPhoneExtension.Text = organizationInfo.BusinessExtension;
        ltrCellPhoneNumber.Text = organizationInfo.CellNumber;
        txtCellPhoneNumber.Text = organizationInfo.CellNumber;
        ltrCellPhoneExtension.Text = organizationInfo.CellExtension;
        txtCellPhoneExtension.Text = organizationInfo.CellExtension;
        ltrZipCode.Text = organizationInfo.ZipCode;
        txtZipCode.Text = organizationInfo.ZipCode;
        txtZipCode.Enabled = false;
        hdnBusinessZipCodeId.Value = Conversion.ParseString(organizationInfo.ZipCodeID);
        ltrState.Text = organizationInfo.StateName;
        ltrCountry.Text = organizationInfo.CountryName;
        ddlCountry.SelectedValue = Conversion.ParseString(organizationInfo.CountryID);
        ddlState.SelectedValue = Conversion.ParseString(organizationInfo.StateID);

        ltrCity.Text = organizationInfo.City;
        txtCity.Text = organizationInfo.City;
        txtCity.Enabled = false;
        ltrOrganization.Text = organizationInfo.OrganizationType;
        txtOrganization.Text = organizationInfo.OrganizationType;
        ltrContactTitle.Text = organizationInfo.ContactTitleName;
        txtContactTitle.Text = organizationInfo.ContactTitleName;
        ddlCountry.Enabled = false;
        ltrBusinessType.Text = organizationInfo.OrganizationSubType;
        txtBusinessType.Text = organizationInfo.OrganizationSubType;
        
        ltrwebsite.Text = organizationInfo.Website;
        txtWebsite.Text = organizationInfo.Website;
        ltrBusinessAddress1.Text = organizationInfo.BusinessAddress;
        txtBusinessAddress1.Text = organizationInfo.BusinessAddress;
        ltrBusinessAddress2.Text = organizationInfo.AlternativeAddress;
        txtBusinessAddress2.Text = organizationInfo.AlternativeAddress;
        ltrBusinessPhoneType.Text = organizationInfo.BusinessPhoneType;
        txtBusinessPhoneType.Text = organizationInfo.BusinessPhoneType;
        lrtBusinessTextMessage.Text = (organizationInfo.AcceptTextMessages) ? "Yes" : "No";
        if (organizationInfo.AcceptTextMessages) ddlbusinesstextmsgs.SelectedValue = "1"; else ddlbusinesstextmsgs.SelectedValue = "2";


        ltrAddress.Text = organizationInfo.Address;
        txtAddress.Text = organizationInfo.Address;
        ltrBillingContact.Text = organizationInfo.BillingContact;
        txtBillingContact.Text = organizationInfo.BillingContact;
        ltrFax.Text = organizationInfo.Fax;
        txtFax.Text = organizationInfo.Fax;
        ltrCountryAbbreviation.Text = organizationInfo.Abbreviation;
        txtCountryAbbreviation.Text = organizationInfo.Abbreviation;
        ltrCellTextMessage.Text = (organizationInfo.CellAcceptTextMessages) ? "Yes" : "No";
        if (organizationInfo.CellAcceptTextMessages) dddlcelltextmsgs.SelectedValue = "1"; else dddlcelltextmsgs.SelectedValue = "2";
        ltrCellPhoneType.Text = organizationInfo.CellPhoneType;
        txtCellPhoneType.Text = organizationInfo.CellPhoneType;
        ltrBillingMailAddress.Text = organizationInfo.BillMailAddress;
        txtBillingMailAddress.Text = organizationInfo.BillMailAddress;

    }


    protected void lnkbtnEdit_Click(object sender, EventArgs e)
    {
        pnlDisplay.Visible = false;
        pnlEdit.Visible = true;
        lblinfo.Visible = false;

       
    }



    protected void lnkbtnApprove_Click(object sender, EventArgs e)
    { lblinfo.Visible = false;
        standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
        OrganizationInfo o = new OrganizationInfo(OrganizationID);
        OrganizationInfo.SetStatus(OrganizationStatus.Accepted, OrganizationID, txtNotes.Text, o.OrganizationTypeId, standardstewardshipIds);
     DataSet ds =   UserInfo.getDefaultUsers(OrganizationID);
     if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
     {

         UserInfo user = new UserInfo(Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"].ToString()));
         if (!user.IsApproved)
         {
             Emails email = new Emails();
             email.To = user.Login;
             email.URL = ConfigurationManager.AppSettings["EmailUrl"].ToString() + "ChangePassword.aspx?userId=" + Encryption.Encrypt(user.UserId.ToString());
             email.From = "noreply@eprts.com";
             email.Subject = "Registration Approval Email";
             Thread Email_Thread = new Thread(() => SendEmails(email, Emails.EmailType.ApplicationApprovedEmail.ToString()));
             Email_Thread.Start();
         }
     }
        Response.Redirect(pageId);
        lblinfo.Text = "Successfully Approved";
        lblinfo.Visible = true;

    }
    private void SendEmails(Emails email, string type)
    {
        try
        {
            Emails.SendEmail(email, type);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewUsers.aspx SendEmails", ex);
        }

    }
    protected void lnkbtnReject_Click(object sender, EventArgs e)
    {
        try{
        OrganizationInfo o = new OrganizationInfo(OrganizationID);
        standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
        OrganizationInfo.SetStatus(OrganizationStatus.Rejected, OrganizationID, txtNotes.Text, o.OrganizationTypeId, standardstewardshipIds);

        Response.Redirect(pageId);
        lblinfo.Visible = true;
        lblinfo.Text = " Successfully rejected";
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(currentUserInfo.UserId, "viewStakeHolder.lnkbtnReject_Click", ex);
        }

    }
    protected void lnkbtnPending_Click(object sender, EventArgs e)
    {
        try{
        OrganizationInfo o = new OrganizationInfo(OrganizationID);
        standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
        OrganizationInfo.SetStatus(OrganizationStatus.Pending, OrganizationID, txtNotes.Text, o.OrganizationTypeId, standardstewardshipIds);
        Response.Redirect(pageId);
        lblinfo.Text = "Successfully deleted";
        lblinfo.Visible = true;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(currentUserInfo.UserId, "viewStakeHolder.lnkbtnPending_click", ex);
        }

    }
    protected void lnkbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtNotes.Text))
            {
                OrganizationInfo o = new OrganizationInfo(OrganizationID);
                standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
                OrganizationInfo.SetStatus(OrganizationStatus.Deleted, OrganizationID, txtNotes.Text, o.OrganizationTypeId, standardstewardshipIds);
                Response.Redirect(pageId);
                lblinfo.Text = "Successfully deleted";
                lblinfo.Visible = true;
            }
            else
            {
                string script = string.Format("alert('{0}');", "Cannot delete please provide notes for deleting.");
                if (Page != null && !Page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "alert", script, true /* addScriptTags */);
                }
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(currentUserInfo.UserId, "viewStakeHolder.lnkbtnDelete_Click", ex);
        }

    }

    protected void lnkbtnUpdate_Click(object sender, EventArgs e)
    {


        OrganizationInfo objOrg = new OrganizationInfo();

        objOrg.OrganizationId = OrganizationID;

        objOrg.LegalName = txtBusinessName.Text.Trim();
        objOrg.DBAName = txtDBAName.Text.Trim();
        objOrg.Website = txtWebsite.Text.Trim();
        objOrg.IsActive = true;
        objOrg.IsOrganization = true;
        objOrg.LanguageId = LanguageId;

        objOrg.RoleId = Conversion.ParseInt(UserInfo.UserRole.Stewardship);

        ContactInfo objPrimaryContact = new ContactInfo();

        objPrimaryContact.FirstName = txtFirstName.Text.Trim();
        objPrimaryContact.LastName = txtLastName.Text.Trim();
        objPrimaryContact.Email = txtPrimaryEmail.Text.Trim();

        objPrimaryContact.IsActive = true;
        objPrimaryContact.IsPrimary = true;
        objPrimaryContact.LanguageId = LanguageId;
        objPrimaryContact.ContactTypeId = Convert.ToInt32(LookupsManagement.LookupType.ContactTypes_Business);

        Phones objPrimaryContactBusinessPhone = new Phones();

        objPrimaryContactBusinessPhone.Number = txtPhoneNumber.Text.Trim().ToString();
        objPrimaryContactBusinessPhone.Extension = txtPhoneExtension.Text.Trim().ToString();
        objPrimaryContactBusinessPhone.IsActive = true;
        objPrimaryContactBusinessPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Business);

        Phones objPrimaryContactCellPhone = new Phones();

        objPrimaryContactCellPhone.Number = txtCellPhoneNumber.Text.Trim();
        objPrimaryContactCellPhone.IsActive = true;
        objPrimaryContactCellPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Cell);

        ContactInfo objBillingContact = new ContactInfo();
        objBillingContact.Email = txtBillingMailAddress.Text.Trim();
        objBillingContact.IsActive = true;
        objBillingContact.IsPrimary = false;
        objBillingContact.LanguageId = LanguageId;
        objBillingContact.ContactTypeId = Convert.ToInt32(ContactInfo.ContactTypes.Billing);
        objBillingContact.ContactTypeId = Convert.ToInt32(LookupsManagement.LookupType.ContactTypes_Billing);

        Phones objBillingContactBusinessPhone = new Phones();
        objBillingContactBusinessPhone.IsActive = true;
        objBillingContactBusinessPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Business);

        Phones objBillingContactCellPhone = new Phones();

        objBillingContactCellPhone.IsActive = true;
        objBillingContactCellPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Cell);

        OrganizationInfo.Organization_Address objBusinessOrganization_Address = new OrganizationInfo.Organization_Address();

        objBusinessOrganization_Address.ZipCodeID = Convert.ToInt32(hdnBusinessZipCodeId.Value);
        objBusinessOrganization_Address.ZipPostalCode = txtZipCode.Text.Trim().ToString();
        objBusinessOrganization_Address.Address1 = txtBusinessAddress1.Text.Trim();
        objBusinessOrganization_Address.Address2 = txtBusinessAddress2.Text.Trim();
        objBusinessOrganization_Address.City = txtCity.Text.Trim().ToString();
        objBusinessOrganization_Address.StateID = Convert.ToInt32(ddlState.SelectedValue);
        objBusinessOrganization_Address.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        objBusinessOrganization_Address.DateCreated = DateTime.Now;
        objBusinessOrganization_Address.IsActive = true;
        objBusinessOrganization_Address.Organization_AddressTypeId = Convert.ToInt32(LookupsManagement.LookupType.OrganizationAddressType_Business);

        OrganizationInfo.Organization_Address objMailingOrganization_Address = new OrganizationInfo.Organization_Address();
        objMailingOrganization_Address.DateCreated = DateTime.Now;
        objMailingOrganization_Address.IsActive = true;
        objMailingOrganization_Address.Organization_AddressTypeId = Convert.ToInt32(LookupsManagement.LookupType.OrganizationAddressType_Mailing);

        OrganizationInfo.UpdateStewardshipInfo(objOrg, objPrimaryContact, objPrimaryContactBusinessPhone, objPrimaryContactCellPhone, objBillingContact, objBillingContactBusinessPhone, objBillingContactCellPhone, objBusinessOrganization_Address, objMailingOrganization_Address);
        lblinfo.Visible = true;
        lblinfo.Text = "Successfully updated";
        lblinfo.Style.Add("color", "green");
        viewStakeholdersForApproval();
        pnlDisplay.Visible = true;
        pnlEdit.Visible = false;

    }

    protected void lnkbtnCancel_Click(object sender, EventArgs e)
    {
        pnlEdit.Visible = false;
        pnlDisplay.Visible = true;
        lblinfo.Visible = false;
    }


    protected void txtBusinessZipCode_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = null;
        dt = OrganizationInfo.GetCityStateAndCountryByZipCode(txtZipCode.Text.Trim(), GlobalCountryID);

        if (dt.Rows.Count > 0)
        {
            hdnBusinessZipCodeId.Value = Convert.ToString(dt.Rows[0]["ZipcodeID"]);

            txtCity.Text = Convert.ToString(dt.Rows[0]["CityName"]);

            if (ddlCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["CountryId"])) != null)
                ddlCountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryId"]);

            System.Data.SqlClient.SqlParameter[] prm;

            prm = new System.Data.SqlClient.SqlParameter[1];
            prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId", ddlCountry.SelectedValue);
            Utils.GetLookUpData<DropDownList>(ref ddlState, LookUps.State, prm);
            prm = null;

            if (ddlState.Items.FindByValue(Convert.ToString(dt.Rows[0]["StateId"])) != null)
                ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["StateId"]);

            txtBusinessAddress1.Focus();
            lblBusinessZipCode.Text = "";
        }
        else
        {
            lblBusinessZipCode.Text = "* Zipcode does not exist.";
        }
        lblinfo.Visible = false;
    }

    protected void dllState_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dllCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}