using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Drawing;

public partial class Creditcard_AddCreditCard : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetPermission(ResourceType.AddCreditCardHome, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liCC','{0}');", ResourceMgr.GetMessage("Credit Card")), true);
        if (!IsPostBack)
        {
            if (Request.QueryString["CreditCardId"] != null)
            {
                lblCreditCardTitle.Text = "Update Credit Card";
                lnkbtnUpdateCreditCard.Visible = true;
                lnkbtnAddCreditCard.Visible = false;
                
                Utils.GetLookUpData<DropDownList>(ref ddlCardType, LookUps.CreditCardType);
                int creditcardid = Convert.ToInt32(Request.QueryString["CreditCardId"]);
                CreditCard bankaccountObj = new CreditCard(creditcardid);
                ddlCardType.SelectedValue = (bankaccountObj.Intcardtypeid).ToString();
                txtcardNo.Text = bankaccountObj.Cardnumber;
                txtCV2Code.Text = bankaccountObj.Cv2code;
                txtCardName.Text = bankaccountObj.Vchcreditcardname;
                string expirationDate = bankaccountObj.ExpirationDate;
                if (expirationDate != string.Empty)  //because of old data which does not have expirataion date
                {

                    string[] expiration = expirationDate.Split('/');
                    ddlMonth.SelectedValue = expiration[0];
                    ddlYear.SelectedValue = expiration[1];
                }
                //txtExpiryDate.Text = (bankaccountObj.Dtmexpirydate).ToString("MM/dd/yyyy");

            }
            else
            {
                lblCreditCardTitle.Text = "Add Credit Card";
                Utils.GetLookUpData<DropDownList>(ref ddlCardType, LookUps.CreditCardType);
                ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);

            }
           

        }

    }
    //protected void TextValidate(object source, ServerValidateEventArgs args)
    //{
    //    args.IsValid = (args.Value.Length >= 8);
    //}
    private void AddCreditCardInfo()
    {
        try
        {
            DateTime Date = DateTime.Now;
            int month = Conversion.ParseInt(Date.ToString("MM"));
            int year = Conversion.ParseInt(Date.ToString("yy"));


            if ((Conversion.ParseInt(ddlYear.SelectedValue) > year))
            {
                int CreditType = Int32.Parse(ddlCardType.SelectedValue.Trim());
                String CreditCardNumber = txtcardNo.Text.Trim();
                String VC2Code = txtCV2Code.Text.Trim();
                String CreditCardName = txtCardName.Text.Trim();
                //DateTime ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text.Trim());

                string expirationDate = ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();


                CreditCard.addCreditInfo(CreditType, CreditCardNumber, VC2Code, CreditCardName, expirationDate, LoginMemberId);
            }
            else if (Conversion.ParseInt(ddlYear.SelectedValue) == year)
            {
                if (Conversion.ParseInt(ddlMonth.SelectedValue) >= month)
                {
                    int CreditType = Int32.Parse(ddlCardType.SelectedValue.Trim());
                    String CreditCardNumber = txtcardNo.Text.Trim();
                    String VC2Code = txtCV2Code.Text.Trim();
                    String CreditCardName = txtCardName.Text.Trim();
                    //DateTime ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text.Trim());

                    string expirationDate = ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();


                    CreditCard.addCreditInfo(CreditType, CreditCardNumber, VC2Code, CreditCardName, expirationDate, LoginMemberId);
                }
            }


            else
            {
                lblerror.Text = "Expiry date is less than current date. Please enter future date";
                dverror.Visible = true;
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddCreditCardInfo.AddCreditCardInfo", ex);
        }


    }


    protected void lnkbtnAddInventory_Click(object sender, EventArgs e)
        {
            int status = CreditCard.GetCreditCardNumber(txtcardNo.Text, LoginMemberId);
    if (status == 0)
    {

        AddCreditCardInfo();
        Response.Redirect("creditcard");


    }
        else
        {
            lblerrorcredit.Text = "Card number already exist.";
            lblerrorcredit.ForeColor = Color.Red;
            return;


        }
         }


    protected void setCV2codeRange()
    {

        if ((Convert.ToInt32(ddlCardType.SelectedValue.Trim()) == 52) || (Convert.ToInt32(ddlCardType.SelectedValue.Trim()) == 53) || (Convert.ToInt32(ddlCardType.SelectedValue.Trim()) == 55))
        {
            txtCV2Code.MaxLength = 3;
        }
        else
        {
            txtCV2Code.MaxLength = 4;
        }
    }

    protected void lnkbtnCancelInventory_Click(object sender, EventArgs e)
    {
        ddlCardType.SelectedValue = "0";
        txtcardNo.Text = "";
        txtCV2Code.Text = "";
        txtCardName.Text = "";
        //txtExpiryDate.Text = "";
        Response.Redirect("creditcard");

    }

    protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
    {

        setCV2codeRange();
    }
    protected void lnkbtnUpdateCreditInfo_Click(object sender, EventArgs e)
    {
        int creditcardid = Convert.ToInt32(Request.QueryString["CreditCardId"]);
          DateTime Date = DateTime.Now;
            int month = Conversion.ParseInt(Date.ToString("MM"));
            int year = Conversion.ParseInt(Date.ToString("yy"));


            if (Conversion.ParseInt(ddlYear.SelectedValue) > year)
            {
                // ddlCardType.SelectedValue = "4";
                string expirationDate = ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();
                CreditCard.editCreditCardInfo(creditcardid, Int32.Parse(ddlCardType.SelectedValue.Trim()), txtcardNo.Text, txtCV2Code.Text, txtCardName.Text, expirationDate, LoginMemberId);
                Response.Redirect("creditcard");
            }

            else if (Conversion.ParseInt(ddlYear.SelectedValue) == year)
            {
                if (Conversion.ParseInt(ddlMonth.SelectedValue) > month){
                    string expirationDate = ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();
                    CreditCard.editCreditCardInfo(creditcardid, Int32.Parse(ddlCardType.SelectedValue.Trim()), txtcardNo.Text, txtCV2Code.Text, txtCardName.Text, expirationDate, LoginMemberId);
                    Response.Redirect("creditcard");
                }
            }
            else
            {
                lblerror.Text = "Expiry date is less than current date. Please enter future date";
                dverror.Visible = true;
            }
    }
}