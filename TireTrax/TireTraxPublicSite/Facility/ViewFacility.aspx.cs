using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Drawing;
using System.Data;
using System.Text;
using System.Drawing.Text;

public partial class Inventory_Facility_ViewFacility : BasePage
{
    private int _facilityId;

    public int FacilityId
    {
        get { return _facilityId; }
        set { _facilityId = value; }
    }


    private int _lotId;

    public int LotId
    {
        get { return _lotId; }
        set { _lotId = value; }
    }

    private int _rowId;

    public int RowId
    {
        get { return _rowId; }
        set { _rowId = value; }
    }
    private int _spaceId;

    public int SpaceId
    {
        get { return _spaceId; }
        set { _spaceId = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
        GetPermission(ResourceType.FacilityInventory, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        if (!canAdd)
        {
            aAdd.Visible = false;
        }
        lblErrorMessage.Text = "";//On next page causing error 
        lblFacilityNameError.Text = ""; //On next page causing error 
        if (!IsPostBack)
        {
            LoadFacilityData(1);
            if (!canDelete)
            {
                gvFacility.DataBind();
                gvFacility.Columns[3].Visible = false;

            }
            if (!canUpdate)
            {
                gvFacility.DataBind();
                gvFacility.Columns[3].Visible = false;
            }
        }

        if (TotalItemsR > 0)
        {

            pgrFacility.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        }

    }

    protected override bool OnBubbleEvent(object source, EventArgs args) 
    {
        if (this.pgrFacility.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.LoadFacilityData(CurrentPage);
        }


        return base.OnBubbleEvent(source, args);
    }

    private void LoadFacilityData(int PageNumber)
    {
        
        gvFacility.PageSize = pageSize;
        CurrentPageR = PageNumber;
        int count = 0;
        string facilityname = txtFaciliyNameForSearch.Text.Trim();
        gvFacility.DataSource = Facility.GetFacility(PageNumber, pageSize, UserOrganizationId, out count,facilityname,CatId);
        gvFacility.DataBind();
        this.TotalItemsR = count;
        this.pgrFacility.DrawPager(PageNumber, TotalItemsR, pageSize, MaxPagesToShow);

    }

    protected void lnkbtnClear_Click(object sender, EventArgs e)
    {
        lblFacilityNameError.Text = string.Empty;
        txtFacilityName.Text = String.Empty;
        lblErrorMessage.Text = string.Empty;
        lblErrorMessage.Visible = false;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "bilala", "ClearErrorFileds();", true);
    }
    protected void lnkbtnClearSearch_Click(object sender, EventArgs e)
    {
         lblFacilityNameError.Text = string.Empty;
        txtFacilityName.Text = String.Empty;
        lblErrorMessage.Text = string.Empty;
        lblErrorMessage.Visible = false;
        txtFaciliyNameForSearch.Text = string.Empty;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "bilala", "ClearErrorFiledsForSearch();", true);
    
    }

    protected void lbkSaveFacility_Click(object sender, EventArgs e)
    {
        int status=Facility.GetFacilityNameStatus(txtFacilityName.Text.Trim(),UserOrganizationId);
        if (status == 0)
        {
            
            Facility facility = new Facility();
            facility.FacilityID = 0;
            facility.FacilityName = txtFacilityName.Text.Trim();
            facility.OrganizationID = UserOrganizationId;
            facility.UserID = LoginMemberId;
            facility.RoleID = UserOrganizationRoleId;
            facility.ProductCategoryID = CatId;
            if (Facility.InsertUpdate(facility) > 0)
            {
                lblErrorMessage.Text = "Facility added successfully!";
                 txtFacilityName.Text = string.Empty;
                lblErrorMessage.Visible = true;
                lblFacilityNameError.Text = string.Empty;
                lblFacilityNameError.Visible = false;
                LoadFacilityData(1);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "bilala", "ClearErrorFileds();", true);
            }
            else
            { lblErrorMessage.Text = "Facility can not be saved at the moment. Please try later!";
            lblErrorMessage.Visible = true;
            txtFacilityName.Text = string.Empty;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "bilala", "ClearErrorFileds();", true);

            lblFacilityNameError.Text ="Facility Name Already Exists";
            lblErrorMessage.Text = string.Empty;
            lblErrorMessage.Visible = false;
            lblFacilityNameError.Visible = true;
            txtFacilityName.Text = string.Empty;
        }
    }
    protected void lnkbtnSearchFacility_Click(object sender, EventArgs e)
    {
        LoadFacilityData(1);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "bilala", "ClearErrorFiledsForSearch()", true);

    }




    protected void gvFacility_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int FacilityID =Conversion.ParseInt(e.CommandArgument.ToString());
        if (e.CommandName == "AddLot")
        {
            Response.Redirect("addparkinglot?fid=" + FacilityID);
        }
        else if (e.CommandName == "FacilityInfoPopUp")
        {

            hdnidfaclityid.Value = e.CommandArgument.ToString();

            LoadPopupLotInfobyFacilityId(Convert.ToInt32(hdnidfaclityid.Value));
        }

        else if (e.CommandName== "Delete")
        {
            Facility.DeleteFacilityByFacilityId(Conversion.ParseInt(e.CommandArgument.ToString()));

            LoadFacilityData(1);
            Response.Redirect("facility");
        }
    }

    protected void gvFacility_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvFacility.EditIndex = -1;
        LoadFacilityData(1);
    }

    protected void gvFacility_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvFacility.EditIndex = e.NewEditIndex;
        LoadFacilityData(1);

    }

    protected void gvFacility_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtFacilityName = (TextBox)gvFacility.Rows[e.RowIndex].FindControl("txtFacilityName");
        HiddenField hdnFacilityID = (HiddenField)gvFacility.Rows[e.RowIndex].FindControl("hdnFacilityID");
        Facility.UpdateFacility(Convert.ToInt64(hdnFacilityID.Value), txtFacilityName.Text.Trim());
        gvFacility.EditIndex = -1;
        LoadFacilityData(1);
    }


    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        dvpopupfacilityinfo.Visible = false;
        //hidLotId.Value = string.Empty;

    }


    ////////////////////////////////////////////Facility PopUp funcations////////////////////////////


    ////////////main popup funcation////

    public void LoadPopupLotInfobyFacilityId(int facilityId)
    {
        dvpopupfacilityinfo.Visible = true;
        LoadLotsbyFacilityId(facilityId);


    }

    //////////////////////////////////// pop up lot info funcations //////////


    public void LoadLotsbyFacilityId(int facilityid)
    {
        ucFacilityLots.LoadLots("1", Conversion.ParseString(facilityid));

    }
   



   

}
