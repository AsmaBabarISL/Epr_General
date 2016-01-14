using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class Commission_ViewCommission : BasePage
{
    bool Flag = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liVC','{0}');", ResourceMgr.GetMessage("Commission")), true);
       
        lblerror.Text = "";
        if (!IsPostBack)
        {

           
            Utils.GetLookUpData<DropDownList>(ref ddlcountry, LookUps.Country);
            ddlcountry_SelectedIndexChanged(null, null);


        }
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblerror.Text = "";
        ddlstewardshiptype.Items.Clear();
        if (ddlcountry.SelectedValue != "0")
        {
            DataSet ds = OrganizationInfo.GetStewardshipByCountryID(Convert.ToInt32(ddlcountry.SelectedValue));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem item = new ListItem();
                    item.Value = dr["OrganizationId"].ToString();
                    item.Text = dr["StateName"].ToString();
                    ddlstewardshiptype.Items.Add(item);
                }
            }
        }
        ddlstewardshiptype.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));
    }
    protected void ddlstewardshiptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblerror.Text = "";
        int countryid = Convert.ToInt32(ddlcountry.SelectedValue);

        int StewardshipStateID = Convert.ToInt32(ddlstewardshiptype.SelectedValue);
        LoadCommissionInfo(countryid, StewardshipStateID);
    }


    private void LoadCommissionInfo(int countryId, int StewardshipStateID)
    {
        try
        {

            gvCommissionType.DataSource = Commission.GetCommissionInfo(countryId, StewardshipStateID);
            gvCommissionType.DataBind();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminBankAcounts.BankAcountInfo", ex);
        }
    }
    

    protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rbList = (RadioButtonList)sender;
        DataControlFieldCell gridcell = (DataControlFieldCell)rbList.Parent;
        GridViewRow row = (GridViewRow)gridcell.Parent;
        TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
        //TextBox txtPercentage = (TextBox)row.FindControl("txtpercentage");


        if (rbList.SelectedValue == "3")
        {
            txtAmount.Text = "";
            txtAmount.Enabled = false;
            // txtPercentage.Enabled = false;
        }
        else if (rbList.SelectedValue == "2")
        {
            txtAmount.Enabled = true;
            txtAmount.Text = "0";
            // txtPercentage.Enabled = true;
        }
        else if (rbList.SelectedValue == "1")
        {
            txtAmount.Enabled = true;
            txtAmount.Text = "0";
            // txtPercentage.Enabled = false;
        }

    }

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
            TextBox txt = (TextBox)sender;
            DataControlFieldCell gridcell = (DataControlFieldCell)txt.Parent;
            GridViewRow row = (GridViewRow)gridcell.Parent;
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            if (txtAmount.Text == "")
                txtAmount.Text = "0";

            //txtAmount.Text = Utils.CleanHTML(txtAmount.Text);
            //txtAmount.Text = txtAmount.Text.Replace("$", " ").Trim();
            //txtAmount.Text = txtAmount.Text.Replace("%", " ").Trim();
            //try
            //{
            //    double i = 0.0;
            //    if (txtAmount.Text != "")
            //        i = double.Parse(txtAmount.Text);
            //    else if (txtAmount.Text.Trim() == "")
            //    {
            //        lblerror.Text = "Please enter numeric/decimal only";
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblerror.CssClass = "error";
            //    lblerror.Text = "Please enter numeric/decimal only";

            //    return;
            //}
            //if (ddlcountry.SelectedIndex == 0)
            //{
            //    lblerror.Text = "Please select the role first";
            //}

            //else
            //{


            ////TextBox txtPercentage = (TextBox)row.FindControl("txtpercentage");
            RadioButtonList rbList = (RadioButtonList)row.FindControl("rbType");

            //int feeTypeId = Conversion.ParseInt(gvCommissionType.DataKeys[row.RowIndex]["LookupTypeID"]);
            //int countryId = Int32.Parse(ddlcountry.SelectedValue);
            //int organizationId = Int32.Parse(ddlstewardshiptype.SelectedValue);
            //Commission com = new Commission();

            //com.CommissionId = 0;
            //com.Amount = Conversion.ParseDecimal(txtAmount.Text.Replace('$', ' ').Trim());
            //com.Percentage = Conversion.ParseDecimal(txtAmount.Text.Replace('%', ' ').Trim());
            //// com.Percentage = Conversion.ParseDecimal(txtPercentage.Text);
            //com.TypeId = feeTypeId;
            //com.CountryId = countryId;

            //com.OrganizationId = organizationId;
            //com.CreatedById = LoginMemberId;
            //com.TntCommissionType = Conversion.ParseInt(rbList.SelectedValue);

            //com.IsActive = true;

            //if (rbList.SelectedValue == "1")
            //{
            //    com.Percentage = 0;
            //}
            //else
            if (rbList.SelectedValue == "2")
            {
                //  com.Amount = 0;
                if (Conversion.ParseDouble(txtAmount.Text.Trim()) > 100)
                {
                    Response.Write("<script> alert('You have entered invalid percentage!'); </script>");
                    txtAmount.Text = "0";
                    //   ScriptManager.RegisterStartupScript(upnlsearch, upnlsearch.GetType(), "confirm", "return confirm('Changing the language will clear the text in the textboxes. Click OK to proceed.');", true);

                    //ScriptManager.RegisterClientScriptBlock
                }
                //    }
                //    else
                //    {
                //        com.Amount = 0;
                //        com.Percentage = 0;
                //    }

                //    if (Commission.CommissionSetting(com))
                //    {
                //        lblerror.Text = "Commission updated successfully.";
                //     LoadCommissionInfo(Int32.Parse(ddlcountry.SelectedValue), Int32.Parse(ddlstewardshiptype.SelectedValue), 2);
                //    }
                //}
            }
            //catch (Exception ex)
            //{
            //    new SqlLog().InsertSqlLog(0, "commission.txtAmount_TextChanged", ex);
            //}



          //  LoadCommissionInfo(countryid, organizationid);
        
    }

    protected void gvCommissionType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
            //TextBox txtPercentage = (TextBox)e.Row.FindControl("txtpercentage");
            RadioButtonList rbList = (RadioButtonList)e.Row.FindControl("rbType");
            rbList.SelectedValue = Conversion.ParseInt(row["tntCommssionType"].ToString()) == 0 ? "3" : Conversion.ParseInt(row["tntCommssionType"].ToString()).ToString();

            if (rbList.SelectedValue == "3")
            {
                txtAmount.Enabled = false;

                //  txtPercentage.Enabled = false;
            }
            else if (rbList.SelectedValue == "2")
            {
                txtAmount.Enabled = true;
                txtAmount.Text = Conversion.ParseString(row["Percentage"].ToString()) + "%";
                // txtPercentage.Enabled = true;
            }
            else if (rbList.SelectedValue == "1")
            {
                txtAmount.Enabled = true;
                txtAmount.Text = "$" + Conversion.ParseString(row["Amount"].ToString());
                //txtPercentage.Enabled = false;
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (IsPostBack) { 
        foreach (GridViewRow r in gvCommissionType.Rows)
        {
            Flag = true;

             saveCommission(r);
             if (!Flag)  break;
        }
        if (Flag)
        {
            lblerror.Text = "";
            int countryid = Convert.ToInt32(ddlcountry.SelectedValue);

            int StewardshipID = Convert.ToInt32(ddlstewardshiptype.SelectedValue);
            LoadCommissionInfo(countryid, StewardshipID);
            lblerror.Visible = true;
            lblerror.CssClass = "custom-absolute-alert alert-success";
            lblerror.Text = "Commission updated successfully.";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        }
    }
    private void saveCommission(  GridViewRow gr)
    {

        
            try
            {
                //TextBox txt = (TextBox)sender;
                //DataControlFieldCell gridcell = (DataControlFieldCell)txt.Parent;
                GridViewRow row = gr;
                TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
                if (txtAmount.Text == "")
                    txtAmount.Text = "0";

                txtAmount.Text = Utils.CleanHTML(txtAmount.Text);
                txtAmount.Text = txtAmount.Text.Replace("$", " ").Trim();
                txtAmount.Text = txtAmount.Text.Replace("%", " ").Trim();
                try
                {
                    double i = 0.0;
                    if (txtAmount.Text != "")
                        i = double.Parse(txtAmount.Text);
                    else if (txtAmount.Text.Trim() == "")
                    {
                        lblerror.Text = "Please enter numeric/decimal only";
                        Flag = false;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblerror.CssClass = "custom-absolute-alert alert-danger";
                    lblerror.Text = "Please enter numeric/decimal only";
                    ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                    Flag = false;
                    return;
                }
                if (ddlcountry.SelectedIndex == 0)
                {
                    Flag = false;
                    lblerror.Text = "Please select the country first";
                    ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                }

                else
                {


                    //TextBox txtPercentage = (TextBox)row.FindControl("txtpercentage");
                    RadioButtonList rbList = (RadioButtonList)row.FindControl("rbType");

                    int OrganizationSubTypeId = Conversion.ParseInt(gvCommissionType.DataKeys[row.RowIndex]["OrganizationSubTypeId"]);
                    int countryId = Int32.Parse(ddlcountry.SelectedValue);
                    int StewardshipID = Int32.Parse(ddlstewardshiptype.SelectedValue);
                    Commission com = new Commission();

                    com.CommissionId = 0;
                    com.Amount = Conversion.ParseDecimal(txtAmount.Text.Replace('$', ' ').Trim());
                    com.Percentage = Conversion.ParseDecimal(txtAmount.Text.Replace('%', ' ').Trim());
                    // com.Percentage = Conversion.ParseDecimal(txtPercentage.Text);
                    com.OrganizationSubTypeId = OrganizationSubTypeId;
                    com.CountryId = countryId;

                    com.StewardshipID = StewardshipID;
                    com.CreatedById = LoginMemberId;
                    com.TntCommissionType = Conversion.ParseInt(rbList.SelectedValue);

                    com.IsActive = true;

                    if (rbList.SelectedValue == "1")
                    {
                        com.Percentage = 0;
                    }
                    else if (rbList.SelectedValue == "2")
                    {
                        com.Amount = 0;
                        if (Conversion.ParseDouble(txtAmount.Text.Trim()) > 100)
                        {
                           Response.Write("<script> alert('invalid percentage'); </script>");
                           ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                           Flag = false;
                            //   ScriptManager.RegisterStartupScript(upnlsearch, upnlsearch.GetType(), "confirm", "return confirm('Changing the language will clear the text in the textboxes. Click OK to proceed.');", true);

                            //ScriptManager.RegisterClientScriptBlock
                        }
                    }
                    else
                    {
                        com.Amount = 0;
                        com.Percentage = 0;
                    }

                    if (!Commission.CommissionSetting(com))
                    {
                        lblerror.Text = "Please enter numeric/decimal only";
                        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                        Flag = false;
                   //     LoadCommissionInfo(Int32.Parse(ddlcountry.SelectedValue), Int32.Parse(ddlstewardshiptype.SelectedValue), 2);
                    } 
                }
            }
            catch (Exception ex)
            {
                Flag = false;
                new SqlLog().InsertSqlLog(0, "commission.txtAmount_TextChanged", ex);
            }
        }
    
}