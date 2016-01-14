using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class Templates_ViewTemplates : BasePage
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
        pageSize = 10;
        #region Permission
        GetPermission(ResourceType.Templates, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        else if (!canAdd)
        {
            btnaddTemplate.Visible = false;
        }
        #endregion

        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liBA','{0}');", ResourceMgr.GetMessage("Templates")), true);
        
        ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        if (!IsPostBack)
        {
            Utils.GetLookUpData<DropDownList>(ref ddlTemplateType, LookUps.TemplateType);
            TemplateInfo(1);
        }
        if (TotalItemsR > 0)
        {

            pgrTemplate.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        }
    }


    #region Load Function
    /// <summary>
    /// use for paging
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrTemplate.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.TemplateInfo(CurrentPage);
        }


        return base.OnBubbleEvent(source, args);
    }
    /// <summary>
    /// use to load the Main grid from database 
    /// </summary>
    /// <param name="pageNo"></param>
    protected void TemplateInfo(int pageNo)
    {
        try
        {
            
            gvTemplateinfo.PageSize = pageSize;
            CurrentPageR = pageNo;
            int count = 0;
            gvTemplateinfo.DataSource = Templates.LoadAllTemplatesByOrgID(UserOrganizationId, pageNo, pageSize, out count, txtTemplateName.Text, Conversion.ParseInt(ddlTemplateType.SelectedValue), Conversion.ParseInt(ddlInvoiceType.SelectedValue));
            gvTemplateinfo.DataBind();

            this.TotalItemsR = count;
            this.pgrTemplate.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewTemplates.TemplateInfo", ex);
        }

    }

    protected void LoadPopInfobyTemplateId(int templateId)
    {
        Templates objTemp = new Templates(templateId);
        lblTemplateID.Text = objTemp.Templateid.ToString();
        lblTemplateName.Text = objTemp.Name;
        lblTemplateDate.Text = objTemp.DateCreated.ToShortDateString();
        if (objTemp.InvoiceType == 1)
            lblInvoiceType.Text = InvoiceType.Single.ToString();
        else
            lblInvoiceType.Text = InvoiceType.Commulative.ToString();

        lblTemplateType.Text = objTemp.TemplateType;
        ltrBody.Text =  objTemp.Body;
    }

    #endregion

    #region Button Events
    /// <summary>
    /// search the specific record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        TemplateInfo(1);
    }
   
    protected void chkboxPrimary_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            string hdTemplateid = ((HiddenField)chk.Parent.FindControl("hdTemplateid")).Value;
            string hdTemplateTypeId = ((HiddenField)chk.Parent.FindControl("hdTemplateTypeId")).Value;
            Templates.makeTemplatePrimary(Conversion.ParseInt(hdTemplateid), Conversion.ParseInt(hdTemplateTypeId));
        }

        TemplateInfo(1);

    }
    /// <summary>
    /// Use to Redirect to Add Delivery Notes page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnaddTemplate_Click(object sender, EventArgs e)
    {
        Response.Redirect("addtemplate");
    }
    protected void btnDeliveryDetailBack_Click(object sender, EventArgs e)
    {
        dvMainTemplate.Visible = false;

    }
    #endregion
    #region GridView events
    protected void gvTemplateinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeliveryInfo")
        {
            dvMainTemplate.Visible = true;
            LoadPopInfobyTemplateId(Convert.ToInt32(e.CommandArgument));
        }
        else if (e.CommandName == "Edit")
        {
            Response.Redirect("addtemplate?Templateid=" + e.CommandArgument);
        }
        else if (e.CommandName == "DeActivate")
        {
            Templates.ActivateDeActivateTemplate(Convert.ToInt32(e.CommandArgument), false); TemplateInfo(1);
        }
        else if (e.CommandName == "Activate")
        {

            Templates.ActivateDeActivateTemplate(Convert.ToInt32(e.CommandArgument), true); TemplateInfo(1);
        }
       
    }
    protected void gvTemplateinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink LnkActivestatus = (HyperLink)e.Row.FindControl("LnkActivestatus");
            HyperLink LnkDeActivestatus = (HyperLink)e.Row.FindControl("LnkDeActivestatus");
            HyperLink lnkPrimaryStatus = (HyperLink)e.Row.FindControl("lnkPrimaryStatus");
            HyperLink lnkNotPrimaryStatus = (HyperLink)e.Row.FindControl("lnkNotPrimaryStatus");


            LinkButton Imgbtntemplate = (LinkButton)e.Row.FindControl("Imgbtntemplate");
            LinkButton imgbtnEdit = (LinkButton)e.Row.FindControl("imgbtnEdit");
            LinkButton imgBtneDeactivate = (LinkButton)e.Row.FindControl("imgBtneDeactivate");
            LinkButton imgbtnActivate = (LinkButton)e.Row.FindControl("imgbtnActivate");
            CheckBox chkboxPrimary = (CheckBox)e.Row.FindControl("chkboxPrimary");



            if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsActive")) == true)
            {
                LnkActivestatus.Visible = true;
                LnkDeActivestatus.Visible = false;
                imgBtneDeactivate.Visible = true;
                imgbtnActivate.Visible = false;
                chkboxPrimary.Enabled = true;
            }
            else if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsActive")) == false)
            {
                LnkActivestatus.Visible = false;
                LnkDeActivestatus.Visible = true;
                imgBtneDeactivate.Visible = false;
                imgbtnActivate.Visible = true;
                chkboxPrimary.Enabled = false;
            }
            if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "Isprimary")) == true)
            {
                lnkPrimaryStatus.Visible = true;
                lnkNotPrimaryStatus.Visible = false;
            }
            else if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "Isprimary")) == false)
            {
                lnkNotPrimaryStatus.Visible = true;
                lnkPrimaryStatus.Visible = false;
            }

        }
    }

    #endregion
    protected void ddlTemplateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTemplateType.SelectedItem.Text.ToString().Trim().ToLower() == "invoice")
        {
            //Invoice_Type.Visible = true;
            ddlInvoiceType.Enabled = true;
        }
        else
        {
            //Invoice_Type.Visible = false;
            ddlInvoiceType.Enabled = false;
            ddlInvoiceType.SelectedValue = "0";
        }
    }
    protected void btnTemplateCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("templates");
    }
}