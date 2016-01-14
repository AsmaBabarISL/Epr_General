using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;


public partial class Stakeholder_ViewDetailStakeholder : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String organizationIdstr = Request.QueryString["OrganizationId"];

            int organizationId = Int32.Parse(organizationIdstr);

            viewStakeholdersForApproval(organizationId);

        }

    }
    protected void viewStakeholdersForApproval(int organizationId)
    {


        DataSet ds = null;

        ds = OrganizationInfo.getViewStakeholderForApproval(organizationId);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            ltrBusinessName.Text = Convert.ToString(ds.Tables[0].Rows[0]["LegalName"]);
            ltrDBAName.Text = Convert.ToString(ds.Tables[0].Rows[0]["DBAName"]);
            ltrFirstName.Text = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
            ltrLastName.Text = Convert.ToString(ds.Tables[0].Rows[0]["LastName"]);
            ltrprimaryEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
            ltrPhoneNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["BusinessNumber"]);
            ltrPhoneExtension.Text = Convert.ToString(ds.Tables[0].Rows[0]["BusinessExtension"]);
            ltrCellPhoneNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["CellNumber"]);
            ltrCellPhoneExtension.Text = Convert.ToString(ds.Tables[0].Rows[0]["CellExtension"]);
            ltrZipCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["Zipcode"]);
            ltrState.Text = Convert.ToString(ds.Tables[0].Rows[0]["StateName"]);
            ltrCountry.Text = Convert.ToString(ds.Tables[0].Rows[0]["CountryName"]);
            ltrCity.Text = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
            ltrOrganization.Text = Convert.ToString(ds.Tables[0].Rows[0]["Description"]);
            ltrContactTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactTitleName"]);

            ltrBusinessType.Text = Convert.ToString(ds.Tables[0].Rows[0]["BusinessType"]);
            ltrwebsite.Text = Convert.ToString(ds.Tables[0].Rows[0]["Website"]);
            ltrBusinessAddress1.Text = Convert.ToString(ds.Tables[0].Rows[0]["BusinessAddress1"]);
            ltrBusinessAddress2.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address2"]);
            ltrBusinessPhoneType.Text = Convert.ToString(ds.Tables[0].Rows[0]["BusinessPhoneType"]);
            lrtBusinessTextMessage.Text = Convert.ToString(ds.Tables[0].Rows[0]["BusinessIsAccepTextMessages"]);


            ltrAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
            ltrBillingContact.Text = Convert.ToString(ds.Tables[0].Rows[0]["BilingContact"]);
            ltrFax.Text = Convert.ToString(ds.Tables[0].Rows[0]["Fax"]);
            ltrCountryAbbreviation.Text = Convert.ToString(ds.Tables[0].Rows[0]["Abbreviation"]);
            ltrCellTextMessage.Text = Convert.ToString(ds.Tables[0].Rows[0]["CellIsAcceptTextMessages"]);
            ltrCellPhoneType.Text = Convert.ToString(ds.Tables[0].Rows[0]["CellPhoneType"]);
            ltrBillingMailAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["BillMailAddress"]);
        }
        if (ds.Tables[0].Rows.Count > 1)
        {
            RptrsupplierInfo.DataSource = ds.Tables[1];
            RptrsupplierInfo.DataBind();
        }
        if (ds.Tables[0].Rows.Count > 2)
        {
            RptrClntInfo.DataSource = ds.Tables[2];
            RptrClntInfo.DataBind();
        }
        if (ds.Tables[0].Rows.Count > 3)
        {
            RptrCertificationInfo.DataSource = ds.Tables[3];
            RptrCertificationInfo.DataBind();
        }



    }

}