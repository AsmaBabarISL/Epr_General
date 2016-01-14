using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TireTraxLib;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

public partial class Stewardship_ViewDetailStewardship : BasePage
{
     public int GlobalCountryID = 235; // USA CountryId
     string standardstewardshipIds = "";
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
     string pageId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        int status = 0;

        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liStewardship');", true);
        if (!IsPostBack)
        {
            standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            OrganizationInfo o = new OrganizationInfo(OrganizationID);
            status = OrganizationInfo.getStewardshipStatusByOrganizationId(OrganizationID, standardstewardshipIds, o.OrganizationTypeId);
            if (status == 1)
                imgBtnPending.Visible = false;
            else if (status == 2)
                imgbtnApprove.Visible = false;
            else if (status == 3)
                imgbtnReject.Visible = false;
            else if (status == 4)
                imgbtnDelete.Visible = false;
            BindDropDowns();
            viewStakeholdersForApproval();
            lblinfo.Visible = false;
            dlg.Visible = false;
        }
        pageId = Request.QueryString.Get("PageId");
        if (pageId == "1")
        {
            pageId = "/Application/PendingApplication.aspx";
        }
        else
        {
            pageId = "/Stewardship/ViewStewardship.aspx";
        }

    }

    private void BindDropDowns()
    {
        Utils.GetLookUpData<DropDownList>(ref ddlCountry, LookUps.CountryIdandName);
        Utils.GetLookUpData<DropDownList>(ref ddlState, LookUps.State, new SqlParameter[] { new SqlParameter("@CountryId", Conversion.ParseInt(ddlCountry.SelectedValue)) });
    }
    protected void viewStakeholdersForApproval()
    {
       // txtZipCode.Enabled = false;
        txtCity.Enabled = false;
        ddlCountry.Enabled = false;
        OrganizationStatus s;
        standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
        OrganizationInfo o = new OrganizationInfo(OrganizationID);
        s = (OrganizationStatus)(OrganizationInfo.getStewardshipStatusByOrganizationId(OrganizationID, standardstewardshipIds, o.OrganizationTypeId));
        lblStatus.Text = s.ToString();
        lblStatusNotes.Text = (OrganizationInfo.getLatestNotesStatusByOrganizationId(OrganizationID));
        lblStatus.Text = s.ToString();
        OrganizationInfo organizationInfo = new OrganizationInfo(OrganizationID, true);
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
      //  ltrCellPhoneExtension.Text = organizationInfo.CellExtension;
    //    txtCellPhoneExtension.Text = organizationInfo.CellExtension;
        ltrZipCode.Text = organizationInfo.ZipCode;
        txtZipCode.Text = organizationInfo.ZipCode;
        hdnBusinessZipCodeId.Value = Conversion.ParseString(organizationInfo.ZipCodeID);
        ltrState.Text = organizationInfo.StateName;       
        ltrCountry.Text = organizationInfo.CountryName;
        ddlCountry.SelectedValue = Conversion.ParseString(organizationInfo.CountryID);
        ddlState.SelectedValue = Conversion.ParseString(organizationInfo.StateID);
        
        ltrCity.Text = organizationInfo.City;
        txtCity.Text = organizationInfo.City;
        ltrOrganization.Text = organizationInfo.Description;
        txtOrganization.Text = organizationInfo.Description;
        ltrContactTitle.Text = organizationInfo.ContactTitleName;
        txtContactTitle.Text = organizationInfo.ContactTitleName;

    //    ltrBusinessType.Text = organizationInfo.BusinesType;
    //    txtBusinessType.Text = organizationInfo.BusinesType;
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


    //    ltrAddress.Text = organizationInfo.Address;
   //     txtAddress.Text = organizationInfo.Address;
    //    ltrBillingContact.Text = organizationInfo.BillingContact;
   //     txtBillingContact.Text = organizationInfo.BillingContact;
//ltrFax.Text = organizationInfo.Fax;
   //     txtFax.Text = organizationInfo.Fax;
        ltrCountryAbbreviation.Text = organizationInfo.Abbreviation;
        txtCountryAbbreviation.Text = organizationInfo.Abbreviation;
        ltrCellTextMessage.Text = (organizationInfo.CellAcceptTextMessages) ?"Yes" :  "No";
        if(organizationInfo.CellAcceptTextMessages)  dddlcelltextmsgs.SelectedValue= "1"; else dddlcelltextmsgs.SelectedValue= "2";
        ltrCellPhoneType.Text = organizationInfo.CellPhoneType;
        txtCellPhoneType.Text = organizationInfo.CellPhoneType;
 //       ltrBillingMailAddress.Text = organizationInfo.BillMailAddress;
  //      txtBillingMailAddress.Text = organizationInfo.BillMailAddress;

    }


    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(pageId);
    }
    protected void lnkbtnEdit_Click(object sender, EventArgs e)
    {
        pnlDisplay.Visible = false;
        pnlEdit.Visible = true;
        lblinfo.Visible = false;
    }

    protected void lnkbtnApprove_Click(object sender, EventArgs e)
    {
        lblinfo.Visible = false;
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
             email.From = "noreply@EPRTS.com";
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

        OrganizationInfo o = new OrganizationInfo(OrganizationID);
        standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
        OrganizationInfo.SetStatus(OrganizationStatus.Rejected, OrganizationID, txtNotes.Text, o.OrganizationTypeId, standardstewardshipIds);

        Response.Redirect(pageId);
        lblinfo.Visible = true;
        lblinfo.Text = " Successfully rejected";

    }
    protected void lnkbtnPending_Click(object sender, EventArgs e)
    {

        OrganizationInfo o = new OrganizationInfo(OrganizationID);
        standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
        OrganizationInfo.SetStatus(OrganizationStatus.Pending, OrganizationID, txtNotes.Text, o.OrganizationTypeId, standardstewardshipIds);
        Response.Redirect(pageId);
        lblinfo.Text = "Successfully deleted";
        lblinfo.Visible = true;

    }
    protected void lnkbtnDelete_Click(object sender, EventArgs e)
    {
        DataSet ds;
        OrganizationInfo oi = new OrganizationInfo(OrganizationID);
        string standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
        string[] standanrdIds = standardstewardshipIds.Split(',');
        foreach (string st in standanrdIds)
        {

            ds = OrganizationInfo.SearchStakeholdersByStewardShip(1, 100, out totalRows, OrganizationID, Conversion.ParseInt(st), true, "", "", "", "", DateTime.MinValue, DateTime.MinValue, LanguageId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvApplicationApproved.DataSource = ds;
                gvApplicationApproved.DataBind();
               
                dlg.Visible = true;
                return;
            }

            else
                if (!string.IsNullOrEmpty(txtNotes.Text))
                {
                    dlgcnfdel.Visible = true;
                    

                }
                else
                {
                    string script = string.Format("alert('{0}');", "Cannot delete please provide notes for deleting.");
                    if (Page != null && !Page.ClientScript.IsClientScriptBlockRegistered("function"))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "function", script, true /* addScriptTags */);
                    }
                }
        }

    }

    protected void btndelYes_Click(object sender, EventArgs e)
    {
        OrganizationInfo o = new OrganizationInfo(OrganizationID);

        OrganizationInfo.SetStatus(OrganizationStatus.Deleted, OrganizationID, txtNotes.Text, o.OrganizationTypeId,standardstewardshipIds);
        Response.Redirect(pageId);
        lblinfo.Text = "Successfully deleted";
        lblinfo.Visible = true;
        dlgcnfdel.Visible = false;
    }
     protected void btndelCancel_Click(object sender, EventArgs e)
    {
       //dlg.Visible = false;
         dlgcnfdel.Visible= false;
    }
    protected void btnPopupOK_Click(object sender, EventArgs e)
    {
        dlg.Visible = false;
    }
    protected void lnkbtnUpdate_Click(object sender, EventArgs e)
    {
       // int stateId = OrganizationInfo.getStateId(OrganizationID);
       //int  organizationIdWithSimilerEmail = OrganizationInfo.getOrganizationIdByEmail(txtPrimaryEmail.Text.Trim(), stateId);
       //if (organizationIdWithSimilerEmail > 0)
       //{
       //    lblemailalreadyexists.Text = "Email already exists for this state. Choose another";
       //    lblemailalreadyexists.Visible = true;
       //    return;
       //}
        DataTable dt = null;
        double stateId = OrganizationInfo.getStateId(OrganizationID);
        dt = OrganizationInfo.GetCityStateAndCountryByZipCode(txtZipCode.Text.Trim(), GlobalCountryID, stateId);

        if (dt.Rows.Count > 0)
        {
           
        }
        else
        {
            txtZipCode.Text = "";
            txtZipCode.Focus();
            lblBusinessZipCode.Text = "* Zipcode does not exist in this state.";
            return;
        }
        OrganizationInfo objOrg = new OrganizationInfo(OrganizationID);
        if (dddlcelltextmsgs.SelectedValue == "1")
            objOrg.CellAcceptTextMessages = true;
        else
            objOrg.CellAcceptTextMessages = false;
        if (ddlbusinesstextmsgs.SelectedValue == "1")
            objOrg.AcceptTextMessages = true;
        else
            objOrg.AcceptTextMessages = false;
        objOrg.OrganizationId = OrganizationID;
   //     objOrg.Fax = txtFax.Text.Trim();
 //       objOrg.Address = txtAddress.Text.Trim();
      //  objOrg.BusinessType = txtBusinessType.Text.Trim();
 //       objOrg.BillMailAddress = txtBillingMailAddress.Text.Trim();
  //      objOrg.BillingContact = txtBillingContact.Text.Trim();
 //       objOrg.CellExtension = txtCellPhoneExtension.Text.Trim();
        objOrg.LegalName = txtBusinessName.Text.Trim();
        objOrg.DBAName = txtDBAName.Text.Trim();
        objOrg.Website = txtWebsite.Text.Trim();
        objOrg.IsActive = true;
        objOrg.IsOrganization = true;
        objOrg.LanguageId =LanguageId;
        objOrg.ContactTitleName = txtContactTitle.Text.Trim();
        objOrg.RoleId = Conversion.ParseInt(UserInfo.UserRole.Stewardship);
        objOrg.UpdateOrganizationInfo();
        ContactInfo objPrimaryContact = new ContactInfo();

        objPrimaryContact.FirstName = txtFirstName.Text.Trim();
        objPrimaryContact.LastName = txtLastName.Text.Trim();
        objPrimaryContact.Email = txtPrimaryEmail.Text.Trim();
        
        objPrimaryContact.IsActive = true;
        objPrimaryContact.IsPrimary = true;
        objPrimaryContact.LanguageId = LanguageId;
        objPrimaryContact.ContactTypeId = Convert.ToInt32(LookupsManagement.LookupType.ContactTypes_Business);
        
        Phones objPrimaryContactBusinessPhone = new Phones();

        objPrimaryContactBusinessPhone.Number =txtPhoneNumber.Text.Trim().ToString();
        objPrimaryContactBusinessPhone.Extension = txtPhoneExtension.Text.Trim().ToString();
        objPrimaryContactBusinessPhone.IsActive = true;
        objPrimaryContactBusinessPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Business);

        Phones objPrimaryContactCellPhone = new Phones();

        objPrimaryContactCellPhone.Number = txtCellPhoneNumber.Text.Trim();
        objPrimaryContactCellPhone.IsActive = true;
        objPrimaryContactCellPhone.PhoneTypeId = Convert.ToInt32(LookupsManagement.LookupType.PhoneType_Cell);

        ContactInfo objBillingContact = new ContactInfo();
   //      objBillingContact.Email = txtBillingMailAddress.Text.Trim();
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
        double stateId = OrganizationInfo.getStateId(OrganizationID);
        dt = OrganizationInfo.GetCityStateAndCountryByZipCode(txtZipCode.Text.Trim(), GlobalCountryID, stateId);

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
            txtZipCode.Text = "";
            txtZipCode.Focus();
                        lblBusinessZipCode.Text = "* Zipcode does not exist in this state.";
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
