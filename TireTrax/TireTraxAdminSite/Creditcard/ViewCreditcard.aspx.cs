using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Text;


public partial class Creditcard_ViewCreditcard : BasePage
{
    public int CurPageNum
    {
        get
        {
            if (Request.QueryString["p"] != null)
                return Conversion.ParseInt(Request.QueryString["p"]);
            else
                return 1;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liCC','{0}');", ResourceMgr.GetMessage("Credit Card")), true);
        if (!IsPostBack)
        {
            SearchAdminCardsInfo();
        }
    }

    





    protected void SearchAdminCardsInfo()
    {
        try
        {
            

            //int OrganizationId = Convert.ToInt32(Request.QueryString["OrganizationId"]);

            //string FirstName = txtFirstName.Text.Trim();
            //string LastName = txtLastName.Text.Trim();
            //string LoginName = txtLogin.Text.Trim();
            //DateTime CreatedFromDate = txtCreatedFromDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedFromDate.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);
            //DateTime CreatedToDate = txtCreatedToDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedToDate.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);

            gvCreditCardInfo.DataSource = CreditCard.getCreditCardInfo(LoginMemberId);
            gvCreditCardInfo.DataBind();
           
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AdminCreditCardInfo.SearchAdminCardsInfo", ex);
        }
    }


    


    protected void chkboxPrimary_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            string hdnfldId = ((HiddenField)chk.Parent.FindControl("hdnfldId")).Value;

            CreditCard.updateCreditCardInfo(Conversion.ParseInt(hdnfldId));
        }

        SearchAdminCardsInfo();

    }
    protected void gvCreditCardInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            CreditCard.deleteCreditCardInfo(Convert.ToInt32(e.CommandArgument));
            SearchAdminCardsInfo();

        }
    }
    protected void gvCreditCardInfo_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void gvCreditCardInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}