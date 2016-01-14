using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Drawing;

public partial class Inventory_Lots_ViewLots : BasePage
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
            pageSize = 10;
            gvAdminInventory.PageSize = pageSize;
            CurrentPageR = pageNo;
            int status = 1;
            int TireState = Convert.ToInt32(ddlTireState.SelectedValue);
            string size = txtSize.Text.Trim();
            string brand = txtBrand.Text.Trim();
            string lot = txtLot.Text.Trim();

            DateTime frmDate = string.IsNullOrEmpty(txtFrmDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFrmDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txtToDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);
            int quantity = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? 0 : Convert.ToInt32(txtQuantity.Text.Trim());
            int count = 0;

            DataSet ds = null;

            if ((int)ProductCategory.Tire == CatId)
            {
                ds = Lots.SearchInventory(pageNo, pageSize, out count, "", UserOrganizationId, "", "", TireState, status, txtUserName.Text.Trim(), quantity,
                txtSpace.Text.Trim(), txtLane.Text.Trim(), txtGuid.Text.Trim(), txtLatitude.Text.Trim(), txtLongitude.Text.Trim(), frmDate, toDate, lot, size, brand, CatId);
            }
            else
            {
                ds = Lots.GetLotInfo(pageNo, pageSize, UserOrganizationId, out count, CatId, txtUserName.Text.Trim(), txtLot.Text.Trim(), frmDate, toDate, quantity, txtGuid.Text.Trim());
            }
            gvAdminInventory.DataSource = ds;
            gvAdminInventory.DataBind();
            this.TotalItemsR = count;
            this.pgrLots.DrawPager(pageNo, this.TotalItemsR, pageSize, MaxPagesToShow);
            if (gvAdminInventory.Rows.Count != 0)
            {
                LinkButton lnkBtn = (LinkButton)pgrLots.FindControl("Button_" + pageNo.ToString());
                if (lnkBtn != null)
                {
                    lnkBtn.Font.Bold = true;
                }

            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "PublicSite ViewLots.aspx.cs Load_AllAdminInventory", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo objUser = new UserInfo(LoginMemberId);
        if (objUser.SubRoleName.ToString().Trim().ToLower().Contains("transporter-admin"))
        {
            Response.Redirect("deliverynotes");
        }
        GetPermission(ResourceType.LotsInventory, ref canView, ref canAdd, ref canUpdate, ref canDelete); 
        if (!canView)
        {
            Response.Redirect("error");
        }

        pageSize = 10;
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
        if (!IsPostBack)
        {

            Load_AllAdminInventory(1);

        }


        else
        {
            pageSize = 10;
            if (TotalItemsR > 0)
            {
                pgrLots.DrawPager(CurrentPage, this.TotalItemsR, pageSize, MaxPagesToShow);
                pgrFacilityLot.DrawPager(CurrentPageR, this.TotalItemsR, pageSize, MaxPagesToShow);
                pgrLotProduct.DrawPager(CurrentPageR, this.TotalItemsR, pageSize, MaxPagesToShow);
            }
        }

        lblsuccessfull.CssClass = "";
        lblsuccessfull.Text = "";
    }

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrLots.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.Load_AllAdminInventory(CurrentPage);
        }


        if (this.pgrFacilityLot.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPageR = Convert.ToInt32(cmdArgs.CommandArgument);

            this.Load_AllPermanentLot(CurrentPageR);
        }

        if (this.pgrLotProduct.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPageR = Convert.ToInt32(cmdArgs.CommandArgument);

            this.GetAllProducts(CurrentPageR);
        }

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
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
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
            hidSelectedOrgId.Value = UserOrganizationId.ToString();

            LoadPermanentGrid();
        }
        else if (e.CommandName == "ViewTires")
        {
            if (CatId == (int)ProductCategory.Tire)
            {
                gvAllTire.DataSource = Tire.getAllTireByLotId(Conversion.ParseInt(e.CommandArgument));
                gvAllTire.DataBind();
                Lots objlot = new Lots(Conversion.ParseInt(e.CommandArgument));
                lblLotNumber.Text = objlot.LotNumber;
                dvPermanentLot.Visible = false;
                dvAllProducts.Visible = false;
                dvAllTire.Visible = true;
            }
            else
            {
                hdnLotId.Value = e.CommandArgument.ToString();
                GetAllProducts(1);
            }
        }

    }

    private void GetAllProducts(int pageNo)
    {
        int count;
        gvAllProducts.DataSource = Product.getAllProductsByLotId(Conversion.ParseInt(hdnLotId.Value), CatId, pageNo, pageSize, out count);
        gvAllProducts.DataBind();
        Lots objlot = new Lots(Conversion.ParseInt(hdnLotId.Value));
        lblLotNumberProduct.Text = objlot.LotNumber;
        dvPermanentLot.Visible = false;
        dvAllTire.Visible = false;
        dvAllProducts.Visible = true;


        this.TotalItemsR = count;
        pgrLotProduct.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);

        LinkButton lnkBtn = (LinkButton)pgrLotProduct.FindControl("Button_" + pageNo.ToString());
        lnkBtn.Font.Bold = true;
    }

    protected void gvloadsinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "LoadRedirect")
        {
            Response.Redirect("inventory-tire?tireid=" + e.CommandArgument + "&islot=false");
            // Server.Transfer("inventory-tire.aspx?tireid="+e.CommandArgument+"&islot=true",true);
            //  HttpContext.Current.RewritePath("inventory-tire.aspx?tireid=" + e.CommandArgument + "&islot=false");
        }


    }

    protected void lnkPermanentLot_Click(object sender, EventArgs e)
    {
        string str = hidSelectedLot.Value.Replace(",", "");

        if (string.IsNullOrEmpty(str))
        {
            lblErrorPermanentLotdv.Text = "Please select Facility Storage Lot";
            lblErrorPermanentLotdv.ForeColor = Color.Red;
            return;
        }
        LoadSpaces();

    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Session["TemporaryLotId"] = "";
        Session["SpaceId"] = "";
        dvPermanentLot.Visible = false;

        dvSpace.Visible = false;
        dvlane.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;



    }

    protected void lnkCancelAllTire_Click(object sender, EventArgs e)
    {
        dvAllTire.Visible = false;
        Response.Redirect("lotinfo");
    }

    protected void lnkCancelAllProduct_Click(object sender, EventArgs e)
    {
        dvAllProducts.Visible = false;
        Response.Redirect("lotinfo");
    }


    protected void lnkBackPermanentLotSpace_Click(object sender, EventArgs e)
    {
        hidSelectedSpace.Value = string.Empty;
        string str = hidSelectedLot.Value.Replace(",", "");
        Utils.SetSelectedIdsGridView(ref grvPermanentLot, "", "Radio1", "", true, str);

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
        string str = hidSelectedSpace.Value.Replace(",", "");
        Utils.SetSelectedIdsGridView(ref grdSpaces, "", "Radio1", "", true, str);



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
        hidSelectedLane.Value = string.Empty;
        string str = hidSelectedSpace.Value.Replace(",", "");
        if (string.IsNullOrEmpty(str))
        {
            lblErrorPermanentLotSpacedv.Text = "Please select a Row";
            lblErrorPermanentLotSpacedv.ForeColor = Color.Red;
            return;
        }

        Session["SpaceId"] = str;
        dvParkingLot1.Visible = false;
        dvSpace.Visible = false;
        dvlane.Visible = true;
        LoadLanes();
    }


    protected void lnkLanePerLot_Click(object sender, EventArgs e)
    {
        string str2 = hidSelectedLane.Value.Replace(",", "");
        if (string.IsNullOrEmpty(str2))
        {

            lblErrorPermanentLotLanedv.Text = "Please select a Space";
            lblErrorPermanentLotLanedv.ForeColor = Color.Red;
            return;
        }
        string str = hidSelectedSpace.Value.Replace(",", "");


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

        lblsuccessfull.CssClass = "custom-absolute-alert alert-success";
        lblsuccessfull.Text = "Lots Transferred Successfully";
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

    }

    private void LoadSpaces()
    {
        try
        {

            string str = hidSelectedLot.Value.Replace(",", "");

            if (Conversion.ParseInt(Session["TemporaryLotId"].ToString()) == Conversion.ParseInt(str))
            {
                lblErrorPermanentLotdv.Text = "You have slected to shift this Lot. Please shift the tires in any other Storage Lot ";
                lblErrorPermanentLotdv.ForeColor = Color.Red;
                return;
            }



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

            dvSpace.Visible = true;

            dvParkingLot1.Visible = false;

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
            DataSet dscount = Tire.getPermenentLotTiresCount(UserOrganizationId, CatId);
            int totaltires = Conversion.ParseInt(dscount.Tables[0].Rows[0][0].ToString());
            lblTotalTire.Text = totaltires.ToString();

            //if (totaltires == 0)
            //{
            //    Label1.Visible = false;
            //    lblTotalTire.Visible = false;
            //}
            //else
            //{
            //    lblTotalTire.Visible = true;
            //    Label1.Visible = true;
            //}

            dvAllTire.Visible = false;
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

        pageSize = 10;// Convert.ToInt32(ddlPageSize.SelectedValue);
        grvPermanentLot.PageSize = pageSize;
        CurrentPageR = pageNo;
        int count = 0;
        DataSet ds = Lots.getPermanentLot(pageNo, pageSize, out count, Convert.ToInt32(hidSelectedOrgId.Value), CatId);


        //this.FacilityStorageLOTSPager.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grvPermanentLot.DataSource = ds;
            grvPermanentLot.DataBind();

            this.TotalItemsR = count;
            pgrFacilityLot.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);

            LinkButton lnkBtn = (LinkButton)pgrFacilityLot.FindControl("Button_" + pageNo.ToString());
            lnkBtn.Font.Bold = true;
        }
        else
        {
            grvPermanentLot.DataSource = null;
            grvPermanentLot.DataBind();
        }


    }

    protected void gvAllTire_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnfeilddotnumber = e.Row.FindControl("hdndotnumber") as HiddenField;
            Label lbldot = e.Row.FindControl("AllTireDotNumber") as Label;
            if (hdnfeilddotnumber.Value.Length <= 11)
            {
                lbldot.Text = '0' + hdnfeilddotnumber.Value.Substring(0, 1) + ' ' + hdnfeilddotnumber.Value.Substring(1, 2) + ' ' + hdnfeilddotnumber.Value.Substring(3, 4) + ' ' + hdnfeilddotnumber.Value.Substring(7, 2) + ' ' + hdnfeilddotnumber.Value.Substring(9, 2);
            }
            else
            {

                lbldot.Text = hdnfeilddotnumber.Value.Substring(0, 2) + ' ' + hdnfeilddotnumber.Value.Substring(2, 2) + ' ' + hdnfeilddotnumber.Value.Substring(4, 4) + ' ' + hdnfeilddotnumber.Value.Substring(8, 2) + ' ' + hdnfeilddotnumber.Value.Substring(10, 2);
            }


        }
    }

}