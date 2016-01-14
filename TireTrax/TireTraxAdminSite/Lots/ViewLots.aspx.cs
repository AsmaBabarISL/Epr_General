using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Text;
using System.Data;

public partial class Lots_ViewLots : BasePage
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


    private void Load_AllAdminInventory(int pageNo)
    {
        try
        {
            pageSize = 25;// Convert.ToInt32(ddlPageSize.SelectedValue);
            gvAdminInventory.PageSize = pageSize;
            CurrentPage = pageNo;
            int status = 1;
            int TireState = Convert.ToInt32(ddlTireState.SelectedValue);

            //string userName = txt.Text.Trim();
            //string stewardship = txtInventoryStewardship.Text.Trim();
            //string PlantCode = txtPlantCode.Text.Trim();
            //string SizeCode = txtSizeCode.Text.Trim();
            string size = txtSize.Text.Trim();
            string brand = txtBrand.Text.Trim();
            string lot = txtLot.Text.Trim();

            DateTime frmDate = string.IsNullOrEmpty(txtFrmDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFrmDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txtToDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);
            int quantity = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? 0 : Convert.ToInt32(txtQuantity.Text.Trim());
            int count = 0;
            DataSet ds = Lots.SearchInventory(pageNo, pageSize, out count, "", UserOrganizationId, "", "", TireState, status, txtUserName.Text.Trim(), quantity,
                txtSpace.Text.Trim(), txtLane.Text.Trim(), txtGuid.Text.Trim(), txtLatitude.Text.Trim(), txtLongitude.Text.Trim(), frmDate, toDate, lot, size, brand,CatId);


            gvAdminInventory.DataSource = ds;
            gvAdminInventory.DataBind();

            // GridPaging();
            this.TotalItems = count;
            this.pgrLots.DrawPager(pageNo, this.TotalItems, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Admin site ViewLots.aspx Load_AllAdminInventory", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //GetPermission(ResourceType.LotsInventory, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        //if (!canView)
        //{
        //    Response.Redirect("error");
        //}
        //else if (!canAdd)
        //{
        //    divaddhrfaddInv.Visible = false;
        //}
        //else if (!canUpdate)
        //{
        //    gvAdminInventory.DataBind();
        //    gvAdminInventory.Columns[9].Visible = false;
        //}
        //ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        pageSize = 25;
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
        //ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liInventory');", true);
        if (!IsPostBack)
        {
            Load_AllAdminInventory(1);
            //LoadsInfo(1);
        }
        else if (TotalItems > 0)
        {
            pgrLots.DrawPager(CurrentPage, this.TotalItems, pageSize, MaxPagesToShow);

        }

    }

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrLots.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.Load_AllAdminInventory(CurrentPage);
        }
        //else if (this.pgrLoad.Equals(source))
        //{
        //    CommandEventArgs cmdArgs = (CommandEventArgs)args;
        //    CurrentPageR = Convert.ToInt32(cmdArgs.CommandArgument);

        //    this.Load_AllAdminInventory(CurrentPageR);
        //}

        return base.OnBubbleEvent(source, args);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        Load_AllAdminInventory(1);

        // LoadsInfo(1);

    }

    protected void gvAdminInventory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {

            Lots.updateLotQuantity(Convert.ToInt32(e.CommandArgument));
            //Load_AllAdminInventory();
            Response.Redirect(Request.RawUrl);
        }

        else if (e.CommandName == "LotRedirect")
        {
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            HiddenField status = (HiddenField)row.FindControl("hidLotStatus");
            Response.Redirect("inventory-tire?OpenLotId=" + e.CommandArgument + "&status=" + status.Value);
            // Server.Transfer("inventory-tire.aspx?tireid="+e.CommandArgument+"&islot=true",true);
            //  HttpContext.Current.RewritePath("inventory-tire.aspx?tireid=" + e.CommandArgument + "&islot=true");
        }
        else if (e.CommandName == "transfer")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            HiddenField hid = (HiddenField)row.FindControl("hidLotSelectId");
            Session["TemporaryLotId"] = "";
            Session["SpaceId"] = "";
            Session["TemporaryLotId"] = hid.Value.ToString();
            hidSelectedOrgId.Value = e.CommandArgument.ToString();
            LoadPermanentGrid();
        }


    }
 

   
    protected void lnkPermanentLot_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidSelectedLot.Value))
        {
            lblErrorPermanentLotdv.Text = "Please select Parking Lot";
            return;
        }
        LoadSpaces();
        dvParkingLot1.Visible = false;
        dvSpace.Visible = true;
        dvlane.Visible = false;
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Session["TemporaryLotId"] = "";
        Session["SpaceId"] = "";
        dvPermanentLot.Visible = false;

        dvSpace.Visible = false;
        dvlane.Visible = false;



    }
    protected void lnkBackPermanentLotSpace_Click(object sender, EventArgs e)
    {


        Utils.SetSelectedIdsGridView(ref grvPermanentLot, "", "Radio1", "", true, hidSelectedLot.Value);

        pnlPermanentLot.Visible = true;
        dvParkingLot1.Visible = true;
        grvPermanentLot.Visible = true;


        dvlane.Visible = false;

        dvSpace.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;





    }
    protected void lnkBackPermanentLotLane_Click(object sender, EventArgs e)
    {

        Utils.SetSelectedIdsGridView(ref grdSpaces, "", "Radio1", "", true, hidSelectedSpace.Value);



        //  LinkButton2.Visible = true;
        grdSpaces.Visible = true;
        dvlane.Visible = false;
        dvSpace.Visible = true;
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;
        lnkCancel2.Visible = true;

        dvParkingLot1.Visible = false;





    }
    protected void lnkSpacePerLot_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidSelectedSpace.Value))
        {
            lblErrorPermanentLotSpacedv.Text = "Please select a Space";
            return;
        }
        string str = hidSelectedSpace.Value.Replace(",", "");
        Session["SpaceId"] = str;
        dvParkingLot1.Visible = false;
        dvSpace.Visible = false;
        dvlane.Visible = true;
        LoadLanes();
    }


    protected void lnkLanePerLot_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidSelectedLane.Value))
        {
            lblErrorPermanentLotLanedv.Text = "Please select a Lane";
            return;
        }
        string str = hidSelectedSpace.Value.Replace(",", "");
        string str2 = hidSelectedLane.Value.Replace(",", "");

        Lots.transferTemporaryLot(Convert.ToInt32(Session["TemporaryLotId"]), Convert.ToInt32(str), Convert.ToInt32(str2));
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;
        dvParkingLot1.Visible = true;
        dvPermanentLot.Visible = false;
        dvSpace.Visible = false;
        dvlane.Visible = false;
        Session["TemporaryLotId"] = "";
        Session["SpaceId"] = "";
        Load_AllAdminInventory(1);

    }
    private void LoadSpaces()
    {
        try
        {

            string str = hidSelectedLot.Value.Replace(",", "");
            DataSet ds = Lots.getPermanentLotSpace(Convert.ToInt32(str));
            grdSpaces.DataSource = ds;
            grdSpaces.DataBind();

            if (ds == null && ds.Tables.Count <= 0)
            {

                lnkSpacePerLot.Visible = false;
            }
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                    lnkSpacePerLot.Visible = false;
                else
                    lnkSpacePerLot.Visible = true;
            }
            dvPermanentLot.Visible = true;
            dvPermanentLot.Visible = true;
            dvSpace.Visible = true;
            dvlane.Visible = false;

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadSpaces", ex);
        }
    }
    private void LoadLanes()
    {
        try
        {
            string str = hidSelectedSpace.Value.Replace(",", "");
            DataSet ds = Lots.getPermanentLotSpaceLane(Convert.ToInt32(str));
            gvlane.DataSource = ds;
            gvlane.DataBind();
            if (ds == null && ds.Tables.Count <= 0)
            {

                lnkLanePerLot.Visible = false;
            }
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                    lnkLanePerLot.Visible = false;
                else
                    lnkLanePerLot.Visible = true;
            }



            dvPermanentLot.Visible = true;
            dvSpace.Visible = false;
            dvlane.Visible = true;



        }
        catch (Exception e)
        {


        }

    }
    private void LoadPermanentGrid()
    {
        try
        {

            Load_AllPermanentLot(1);
            //grvPermanentLot.DataSource = ds;
            //grvPermanentLot.DataBind();
            dvPermanentLot.Visible = true;
            grvPermanentLot.Visible = true;
            dvParkingLot1.Visible = true;

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadPermanentGrid", ex);
        }
    }

    private void Load_AllPermanentLot(int pageNo)
    {

        pageSize = 25;// Convert.ToInt32(ddlPageSize.SelectedValue);
        grvPermanentLot.PageSize = pageSize;
        CurrentPage = pageNo;
        int count = 0;
        DataSet ds = Lots.getPermanentLot(pageNo, pageSize, out count, Convert.ToInt32(hidSelectedOrgId.Value),CatId);

        this.TotalItemsR = count;

        //pgrFacilityLot.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
        

        grvPermanentLot.DataSource = ds;
        grvPermanentLot.DataBind();
    }
}