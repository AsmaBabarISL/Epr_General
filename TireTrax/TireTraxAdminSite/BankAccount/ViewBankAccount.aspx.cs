using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class Settings_BankAccount_ViewBankAccount : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liBA','{0}');", ResourceMgr.GetMessage("Bank Account")), true);
        if (!IsPostBack)
        {
            BankAcountInfo();
        }

    }



    protected void BankAcountInfo()
    {
        try
        {
            gvBankAccountInfo.DataSource = BankAccounts.getBankAccountsInfo(Conversion.ParseInt(ddlBankAccountStatus.SelectedValue), LoginMemberId);
            gvBankAccountInfo.DataBind();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "BankAccount.BankAcountInfo", ex);
        }
    }

    //public string CustomFormatter(string input)
    //{
    //    if (input.Length < 5) return input;
    //    char[] characters = input.ToCharArray();
    //    for (int i = characters.Length - 5; i <= 0; i--)
    //    {
    //        characters[i] = '*';
    //    }
    //    return new string(characters);
    //}

    protected void chkboxPrimary_CheckedChanged(object sender, EventArgs e)
    {


        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            string hdnfldId = ((HiddenField)chk.Parent.FindControl("HdnfldAcountId")).Value;

            BankAccounts.updateBankAccountInfo(Conversion.ParseInt(hdnfldId), UserInfo.GetCurrentUserInfo().UserId);
        }

        BankAcountInfo();

    }



    //protected void gvBankAccountInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    BankAccounts.deleteBankAccountInfo(Convert.ToInt32(e.CommandArgument));
    //    BankAcountInfo();
    //}
    //protected void gvBankAccountInfo_RowDeleted(object sender, GridViewDeletedEventArgs e)
    //{

    //}
    //protected void gvBankAccountInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //}

     protected void gvBankAccountInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GetPermission(ResourceType.BankAccountHome, ref canView, ref canAdd, ref canUpdate, ref canDelete);
            if (!canUpdate)
            {
                ImageButton hrfedit = (ImageButton)e.Row.FindControl("imgbtnEditSetting");
                hrfedit.Visible = false;
            }
            if (!canDelete)
            {
                ImageButton imgdelete = (ImageButton)e.Row.FindControl("imgbtnDeleteSetting");
                imgdelete.Visible = false;
            }


            HiddenField hdnAccountNumber = (HiddenField)gvBankAccountInfo.FindControl("hdnAccountNumber");
            Label lblAccountNumber = (Label)gvBankAccountInfo.FindControl("lblAccountNumber");

            string newaccountnumber = new string(hdnAccountNumber.Value.Select((c, i) => i < hdnAccountNumber.Value.Length - 4 ? '*' : c).ToArray());
            lblAccountNumber.Text = newaccountnumber;




        }
    }

    protected void gvBankAccountInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //BankAccounts.deleteBankAccountInfo(Convert.ToInt32(e.CommandArgument));
        //BankAcountInfo();
        if (e.CommandName == "Delete")
        {
            BankAccounts.DeleteBankAccountInfo(Convert.ToInt32(e.CommandArgument)); 
            BankAcountInfo();
        }
        if (e.CommandName == "Edit")
        {
            Response.Redirect("/BankAccount/AddBankAccount.aspx?BankAccountId=" + Convert.ToInt32(e.CommandArgument));
            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = gvBankAccountInfo.Rows[index];
            //ImageButton imgedit = (ImageButton)row.FindControl("imgbtnEditSetting");
            //imgedit.PostBackUrl = "/editBankAccount.aspx?BankAccountId=" + Convert.ToInt32(e.CommandArgument);
        }
        if (e.CommandName == "Active")
        {
            BankAccounts.ActivateBankAccountInfo(Convert.ToInt32(e.CommandArgument));
            BankAcountInfo();
    }
    }
    protected void gvBankAccountInfo_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void gvBankAccountInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvBankAccountInfo_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hd = (HiddenField)e.Row.FindControl("hdnActiveDeactive");
            GetPermission(ResourceType.BankAccountHome, ref canView, ref canAdd, ref canUpdate, ref canDelete);
            if (!canUpdate)
            {
                ImageButton hrfedit = (ImageButton)e.Row.FindControl("imgbtnEditSetting");
                hrfedit.Visible = false;
            }
            if (!canDelete)
            {
                ImageButton imgdelete = (ImageButton)e.Row.FindControl("imgbtnDeleteSetting");
                imgdelete.Visible = false;
            }
            else
            {
                if (hd.Value == "False")
                {
                    LinkButton imgActive = (LinkButton)e.Row.FindControl("imgbtnActiveSetting");
                    imgActive.Visible = true;
                    LinkButton imgdelete = (LinkButton)e.Row.FindControl("imgbtnDeactiveSetting");
                    imgdelete.Visible = false;
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkboxPrimary");
                    chk.Enabled = false;
                }
                else
                {
                    LinkButton imgActive = (LinkButton)e.Row.FindControl("imgbtnActiveSetting");
                    imgActive.Visible = false;
                    LinkButton imgdelete = (LinkButton)e.Row.FindControl("imgbtnDeactiveSetting");
                    imgdelete.Visible = true;
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkboxPrimary");
                    chk.Enabled = true;
            }
            }

            HiddenField hdnAccountNumber = (HiddenField)e.Row.FindControl("hdnAccountNumber");
            Label lblAccountNumber = (Label)e.Row.FindControl("lblAccountNumber");

            string newaccountnumber = new string(hdnAccountNumber.Value.Select((c, i) => i < hdnAccountNumber.Value.Length - 4 ? '*' : c).ToArray());
            lblAccountNumber.Text = newaccountnumber;
        }

        }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BankAcountInfo();
    }
  
}
  