using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
public partial class Loads_ViewLoads : BasePage
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


        //GetPermission(ResourceType.LoadsInventory, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        //if (!canView)
        //{
        //    Response.Redirect("error");
        //}
        Utils.GetLookUpData<DropDownList>(ref ddlLoadStatus, LookUps.LoadType);
        if (ddlLoadStatus.SelectedValue == "0")
        {
            ddlLoadStatus.Items.RemoveAt(0);
            ddlLoadStatus.Items.Add(new ListItem("All Loads", "0"));
            //ddlLoadStatus.Items.Remove(new ListItem("--Select--", "0"));
        }

        ddlLoadStatus.SelectedValue = "0";
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
        //ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        pageSize = 25;

        if (!IsPostBack)
        {
            //Load_AllAdminInventory(1);
            LoadsInfo(1);
        }

        //if (TotalItems > 0)
        //{
        //    pgrLots.DrawPager(CurrentPage, TotalItems, pageSize, MaxPagesToShow);

        //}
        if (TotalItemsR > 0)
        {

            pgrLoad.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        }
    }

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrLoad.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.LoadsInfo(CurrentPage);
        }


        return base.OnBubbleEvent(source, args);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //if (ddlType.SelectedItem.Text.ToLower().Contains("lots"))
        //{

        //    Load_AllAdminInventory(1);
        //}
        //else
        //{
        //    LoadsInfo(1);
        //}
    }

    protected void LoadsInfo(int pageNo)
    {   
        pageSize = 25;// Convert.ToInt32(ddlloadsinfo.SelectedValue);
        gvloadsinfo.PageSize = pageSize;
        CurrentPageR = pageNo;
        DateTime frmDate = string.IsNullOrEmpty(txtFrmDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFrmDate.Text);  
        DateTime toDate = string.IsNullOrEmpty(txtToDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);
       
        int count = 0;
        gvloadsinfo.DataSource = Loads.loadsInfo(pageNo, pageSize, out count, txtUserName.Text.Trim(), frmDate, toDate, UserOrganizationId, Conversion.ParseInt(ddlLoadStatus.SelectedValue),null,CatId);
        gvloadsinfo.DataBind();

        this.TotalItemsR = count;
        this.pgrLoad.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
    }

    protected void gvloadsinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "LoadTireInfo")
        {
            dvMainLoad.Visible = true;

            GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            HiddenField lnkbtn = (HiddenField)gvloadsinfo.Rows[RemoveAt].FindControl("hdLoadNumber");
            lblLoadNumber.Text = lnkbtn.Value;
            LoadPopInfobyLoadId(Convert.ToInt32(e.CommandArgument));

        }


    }

    /// <summary>
    /// Load All Pop Info By Load Id on LowCommand Click
    /// </summary>
    /// <param name="loadid"></param>

    public void LoadPopInfobyLoadId(int loadid)
    {

        lblLoadTireCount.Text = Convert.ToString(Loads.getCountLoadByLoadId(loadid));



        DataSet ds = Loads.getLoadTireInfoByLoadId(loadid);
        grvLoadTireInfo.DataSource = ds;
        grvLoadTireInfo.DataBind();


    }

    protected void ddlLoadStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadsInfo(1);
    }
}
