using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Text;

public partial class Settings_CreditCard_ViewCreditCard : BasePage
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
        // ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liCC');", true);


        GetPermission(ResourceType.CreditCardHome, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
      if (!canAdd)
        {
            dvAdd.Visible = false;
        }
      if (!canUpdate)
      {
          gvCreditCardInfo.DataBind();
          gvCreditCardInfo.Columns[0].Visible=false;

      }
      if (!canDelete)
      {
          gvCreditCardInfo.DataBind();
          gvCreditCardInfo.Columns[8].Visible=false;
      }


        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liCreditCard','{0}');", ResourceMgr.GetMessage("Credit Card")), true);
        //   ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liCC','{0}');", ResourceMgr.GetMessage("Credit Card")), true);
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
            new SqlLog().InsertSqlLog(0, "CreditCard.SearchAdminCardsInfo", ex);
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
    protected void gvCreditCardInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            GetPermission(ResourceType.CreditCardHome, ref canView, ref canAdd, ref canUpdate, ref canDelete);

            HiddenField hdnCardNumber = (HiddenField)e.Row.FindControl("hdnCardNumber");
            Label lblCardNumber = (Label)e.Row.FindControl("lblCardNumber");
            string newcardnumber = new string(hdnCardNumber.Value.Select((c, i) => i < hdnCardNumber.Value.Length - 4 ? '*' : c).ToArray());
            lblCardNumber.Text = newcardnumber;
        }
    }
}