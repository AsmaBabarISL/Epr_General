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
                chkboxetf.Checked = bankaccountObj.IsETF;

                ddlState.SelectedValue = (bankaccountObj.StateId).ToString();
                txtPhoneNum.Text = bankaccountObj.PrimaryPhone;
                ddlState.Enabled = false;
                txtCity.Enabled = false;
                txtCity.CssClass = "form-control";


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
        
        Response.Redirect("/BankAccount/ViewBankAccount.aspx");

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
            ObjBA.UserId = LoginMemberId;
            ObjBA.IsETF = chkboxetf.Checked;
            

            BankAccounts.insertAccountInfo(ObjBA, LoginMemberId);
            Response.Redirect("/BankAccount/ViewBankAccount.aspx");
        }
        else
        {
            lblerrorbank.Text = "Account number already exist.";
            lblerrorbank.ForeColor = Color.Red;
            return;


        }
    }

    protected void lnkbtnUpdateBankAccount_Click(object sender, EventArgs e)
    {
        int accountid = Convert.ToInt32(Request.QueryString["BankAccountId"]);

        BankAccounts.editBankAccountInfo(accountid, Convert.ToInt32(ddlIAcountType.SelectedValue), txtBankName.Text.Trim(), txtBranch.Text.Trim(), txtAccountTitle.Text.Trim(), txtAccountNum.Text.Trim(), txtRoutingNum.Text.Trim(), txtIBANNum.Text.Trim(), txtSwiftCode.Text.Trim(), txtStreetName.Text.Trim(), txtStreetNum.Text.Trim(), txtCity.Text.Trim(), txtZipCode.Text.Trim(), txtPhoneNum.Text.Trim(), Convert.ToInt32(ddlState.SelectedValue), Convert.ToBoolean(chkboxetf.Checked), LoginMemberId);
        Response.Redirect("/BankAccount/ViewBankAccount.aspx");
    }


    protected void txtLocationZipCode_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = null;
        dt = OrganizationInfo.GetCityStateAndCountryByZipCode(txtZipCode.Text.Trim(),_GlobalCountryID);

        if (dt.Rows.Count > 0)
        {
            hdnLocationZipCodeID.Value = Convert.ToString(dt.Rows[0]["ZipcodeID"]);
          //  ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "txtLocationZipCode_TextChanged", String.Format("SetHiddenFieldValue('{0}','{1}');", hdnLocationZipCodeID.ClientID, Convert.ToString(dt.Rows[0]["ZipcodeID"])), true);

            txtCity.Text = Convert.ToString(dt.Rows[0]["CityName"]);

            //if (ddlLocationCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["CountryId"])) != null)
            //    ddlLocationCountry.SelectedValue = Convert.ToString(dt.Rows[0]["CountryId"]);

            //if (Convert.ToBoolean(dt.Rows[0]["IsETF"]) == true)
            //{
            //    chkboxetf.Checked = true;
            //}
            //else
            //{
            //    chkboxetf.Checked = false;
            //}

            System.Data.SqlClient.SqlParameter[] prm;

            prm = new System.Data.SqlClient.SqlParameter[1];
            prm[0] = new System.Data.SqlClient.SqlParameter("@CountryId",_GlobalCountryID);
            Utils.GetLookUpData<DropDownList>(ref ddlState, LookUps.State, prm);
            prm = null;

            if (ddlState.Items.FindByValue(Convert.ToString(dt.Rows[0]["StateId"])) != null)
                ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["StateId"]);

            txtPhoneNum.Focus();
            lblLocationZipCode.Text = "";
           
        }
        else
        {
            lblLocationZipCode.Text = "* Zipcode does not exist.";


        }
    }

}