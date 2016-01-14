using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Drawing;

public partial class BankAccount_AddBankAccount : BasePage
{
    public int _GlobalCountryID = 235;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblLocationZipCode.Visible = false;
        lblerrorbankacc.Visible = false;
        lblerrorbankacc.Text = string.Empty;
        GetPermission(ResourceType.AddBankAccountHome, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }



        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liBA','{0}');", ResourceMgr.GetMessage("Bank Account")), true);
        if (!IsPostBack)
        {
            if (Request.QueryString["BankAccountId"] != null)
            {
                lnkbtnAddBankAccount.Visible = false;
                lnkbtnUpdateBankAccount.Visible = true;
                Utils.GetLookUpData<DropDownList>(ref ddlState, LookUps.StateIdAndName);
                Utils.GetLookUpData<DropDownList>(ref ddlIAcountType, LookUps.BankAccountType);
                int accountid = Convert.ToInt32(Request.QueryString["BankAccountId"]);
                BankAccounts bankaccountObj = new BankAccounts(accountid);
                ddlIAcountType.SelectedValue = (bankaccountObj.Bankaccounttypeid).ToString();
                txtAccountTitle.Text = bankaccountObj.AccountTitle;
                txtAccountNum.Text = bankaccountObj.AccountNumber;
                txtRoutingNum.Text = bankaccountObj.RoutingNumber;
                txtBankName.Text = bankaccountObj.BankName;
                txtBranch.Text = bankaccountObj.BranchName;
                txtIBANNum.Text = bankaccountObj.IBANNumber;
                txtSwiftCode.Text = bankaccountObj.SwiftCode;
                txtStreetNum.Text = bankaccountObj.StreetNumber;
                txtStreetName.Text = bankaccountObj.StreetName;
                txtZipCode.Text = bankaccountObj.ZipCode;
                txtCity.Text = bankaccountObj.City;
                txtCity.CssClass = "form-control";
                ddlState.SelectedValue = (bankaccountObj.StateId).ToString();
                txtPhoneNum.Text = bankaccountObj.PrimaryPhone;
                chkboxetf.Checked = bankaccountObj.IsETF;
                ddlState.Enabled = false;
                txtCity.Enabled = false;



            }
            else
            {

                lnkbtnAddBankAccount.Visible = true;
                lnkbtnUpdateBankAccount.Visible = false;

                Utils.GetLookUpData<DropDownList>(ref ddlState, LookUps.StateIdAndName);
                Utils.GetLookUpData<DropDownList>(ref ddlIAcountType, LookUps.BankAccountType);
            }
        }
    }

    protected void lnkbtnCancelBankAccount_Click(object sender, EventArgs e)
    {
        ddlIAcountType.SelectedValue = "0";
        txtAccountTitle.Text = "";
        txtAccountNum.Text = "";
        txtRoutingNum.Text = "";
        txtIBANNum.Text = "";
        txtSwiftCode.Text = "";
        txtBankName.Text = "";
        txtBranch.Text = "";
        txtStreetName.Text = "";
        txtStreetNum.Text = "";
        txtZipCode.Text = "";
        txtCity.Text = "";
        ddlState.SelectedValue = "0";
        txtPhoneNum.Text = "";
        chkboxetf.Checked = false;
        Response.Redirect("bankaccount");

    }
    protected void lnkbtnAddBankAccount_Click(object sender, EventArgs e)
    {
        int status = BankAccounts.GetBankAccountNumber(txtAccountNum.Text, LoginMemberId);
        if (status == 0)
        {
            BankAccounts ObjBA = new BankAccounts();

            ObjBA.Bankaccounttypeid = Convert.ToInt32(ddlIAcountType.SelectedValue);
            ObjBA.AccountTitle = txtAccountTitle.Text;
            ObjBA.AccountNumber = txtAccountNum.Text;
            ObjBA.RoutingNumber = txtRoutingNum.Text;
            ObjBA.IBANNumber = txtIBANNum.Text;
            ObjBA.SwiftCode = txtSwiftCode.Text;
            ObjBA.BankName = txtBankName.Text;
            ObjBA.BranchName = txtBranch.Text;
            ObjBA.StreetName = txtStreetName.Text;
            ObjBA.StreetNumber = txtStreetNum.Text;
            ObjBA.ZipCode = txtZipCode.Text;
            ObjBA.City = txtCity.Text;
            ObjBA.StateId = Conversion.ParseInt(ddlState.SelectedValue);
            ObjBA.PrimaryPhone = txtPhoneNum.Text;
            ObjBA.IsETF = chkboxetf.Checked;

            DataTable dt = BankAccounts.GetSateAndCityOfZipcode(txtZipCode.Text.ToString().Trim()).Tables[0];
            for (int row = 1; row <= dt.Rows.Count; row++)
            {
                if (dt.Rows[0]["CityName"].ToString().Trim() == txtCity.Text.ToString().Trim() && dt.Rows[0]["StateName"].ToString().Trim() == ddlState.SelectedItem.Text.ToString().Trim())
                {
                    BankAccounts.insertAccountInfo(ObjBA, LoginMemberId);
                    Session["BankAdded"] = "Bank added successfully";
                    Response.Redirect("bankaccount");
                }
            }
            lblerrorbankacc.Visible = true;
            lblerrorbankacc.Text = "Please do not change the information.";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        else
        {
            lblerrorbankacc.Visible = true;
            lblerrorbankacc.Text = "Account# already exists.";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            return;


        }
    }

    protected void lnkbtnUpdateBankAccount_Click(object sender, EventArgs e)
    {
        int accountid = Convert.ToInt32(Request.QueryString["BankAccountId"]);
        BankAccounts.editBankAccountInfo(accountid, Convert.ToInt32(ddlIAcountType.SelectedValue), txtBankName.Text.Trim(), txtBranch.Text.Trim(), txtAccountTitle.Text.Trim(), txtAccountNum.Text.Trim(), txtRoutingNum.Text.Trim(), txtIBANNum.Text.Trim(), txtSwiftCode.Text.Trim(), txtStreetName.Text.Trim(), txtStreetNum.Text.Trim(), txtCity.Text.Trim(), txtZipCode.Text.Trim(), txtPhoneNum.Text.Trim(), Convert.ToInt32(ddlState.SelectedValue), Convert.ToBoolean(chkboxetf.Checked), LoginMemberId);
        Response.Redirect("bankaccount");
    }

    protected void txtLocationZipCode_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = null;
        dt = OrganizationInfo.GetCityStateAndCountryByZipCode(txtZipCode.Text.Trim(), _GlobalCountryID);

        if (dt.Rows.Count > 0)
        {
            hdnLocationZipCodeID.Value = Convert.ToString(dt.Rows[0]["ZipcodeID"]);
            //  ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "txtLocationZipCode_TextChanged", String.Format("SetHiddenFieldValue('{0}','{1}');", hdnLocationZipCodeID.ClientID, Convert.ToString(dt.Rows[0]["ZipcodeID"])), true);

            txtCity.Text = Convert.ToString(dt.Rows[0]["CityName"]);

            //if (ddlLocationCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["CountryId"])) != null)
            //    ddlLocationCountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryId"]);

            System.Data.SqlClient.SqlParameter[] prm;

            prm = new System.Data.SqlClient.SqlParameter[1];
            prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId", _GlobalCountryID);
            Utils.GetLookUpData<DropDownList>(ref ddlState, LookUps.State, prm);
            prm = null;

            if (ddlState.Items.FindByValue(Convert.ToString(dt.Rows[0]["StateId"])) != null)
                ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["StateId"]);

            txtPhoneNum.Focus();
            lblLocationZipCode.Text = "";

        }
        else
        {
            lblLocationZipCode.Text = "Zipcode does not exist.";
            //lblLocationZipCode.Style.Add("margin-left", "45px");
            //lblLocationZipCode.Style.Add("margin-top", "15px");
            lblLocationZipCode.Visible = true;
            txtCity.Text = string.Empty;
            ddlState.SelectedValue = "0";

        }
    }
}