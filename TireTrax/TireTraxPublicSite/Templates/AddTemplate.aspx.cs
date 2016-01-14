using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Configuration;
public partial class Templates_AddTemplate : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liBA','{0}');", ResourceMgr.GetMessage("Templates")), true);
        if (!IsPostBack)
        {
            Utils.GetLookUpData<DropDownList>(ref ddlTemplateType, LookUps.TemplateType);
        }
        if (Request.QueryString["Templateid"] != null)
            LoadTemplate(Conversion.ParseInt(Request.QueryString["Templateid"]));
    }

    #region Insert Update Function
    private int InsertUpdateTemplate()
    {
        Templates objTemp = new Templates();
        if (Request.QueryString["Templateid"] != null)
        {
            objTemp.Templateid = Conversion.ParseInt(Request.QueryString["Templateid"]);
        }
        else
            objTemp.IsActive = true;

        objTemp.Name = txtTemplateName.Text;
        objTemp.OrganizationId = UserOrganizationId;
        string PhyImageRoot = Conversion.ParseString(ConfigurationManager.AppSettings["PhyImageRoot"]);
        string PhyFilesRoot = Conversion.ParseString(ConfigurationManager.AppSettings["PhyFilesRoot"]);
        objTemp.Body = CKEditor1.Text;
        objTemp.CreatedBy = currentUserInfo.UserId;
        objTemp.DateCreated = DateTime.Now;
        objTemp.TemplateTypeID =Conversion.ParseInt( ddlTemplateType.SelectedValue);
        if (Convert.ToInt32(ddlTemplateType.SelectedValue) == Convert.ToInt32(TemplateTypes.Invoice))
            objTemp.InvoiceType = Convert.ToInt32(rdInvoiceType.SelectedValue);
        

        int result = Templates.TemplateInsertUpdate(objTemp);
        return result;
    }
    #endregion

    #region Load Function
    private void LoadTemplate(int templateId)
    {
        Utils.GetLookUpData<DropDownList>(ref ddlTemplateType, LookUps.TemplateType);

        Templates objTemp = new Templates(templateId);
        objTemp.Templateid = templateId;
        txtTemplateName.Text = objTemp.Name;
        ddlTemplateType.SelectedValue = Conversion.ParseString(objTemp.TemplateTypeID);
        CKEditor1.Text = objTemp.Body;
        if(ddlTemplateType.SelectedItem.Text.ToLower().Trim() == "invoice")
            rdInvoiceType.SelectedValue = Conversion.ParseString(objTemp.InvoiceType);

    }
    #endregion
    #region Button Event
    protected void lnkbtnAddDelivery_Click(object sender, EventArgs e)
    {
        
        if(InsertUpdateTemplate()>0)
            Response.Redirect("templates");
        


    }
    protected void lnkbtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("templates");

    }
    #endregion
    protected void ddlTemplateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTemplateType.SelectedItem.Text.Contains("Invoice"))
            divInvoiceType.Visible = true;
        else if (ddlTemplateType.SelectedItem.Text.Contains("Email"))
            divInvoiceType.Visible = false;
	    
        
    }
}