using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class DeliveryNotes_ViewDeliveryNotes : BasePage
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
        #region Permission
        GetPermission(ResourceType.DeliveryNotes, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        else if (!canAdd)
        {
            dvadddeliverynote.Visible = false;
        }
        #endregion
        pageSize = 10;
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liBA','{0}');", ResourceMgr.GetMessage("Delivery Notes")), true);
      
        //ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        if (!IsPostBack)
        {
            DeliveryInfo(1);
        }
        if (TotalItemsR > 0)
        {

            pgrLoad.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        }
    }

    #region Button Events

    /// <summary>
    /// Use to Redirect to Add Delivery Notes page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnadddeliverynote_Click(object sender, EventArgs e)
    {
        Response.Redirect("adddeliverynote");
    }
    /// <summary>
    /// search the specific record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DeliveryInfo(1);
    }
    protected void btnDeliveryDetailBack_Click(object sender, EventArgs e)
    {
        dvMainLoad.Visible = false;
    }
   
    #endregion
    #region Load Function

    /// <summary>
    /// use for paging
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrLoad.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.DeliveryInfo(CurrentPage);
        }


        return base.OnBubbleEvent(source, args);
    }
    /// <summary>
    /// use to load the Main grid from database 
    /// </summary>
    /// <param name="pageNo"></param>
    protected void DeliveryInfo(int pageNo)
    {
        try
        {
            pageSize = 10;
            gvDeliveryinfo.PageSize = pageSize;
            CurrentPageR = pageNo;
            DateTime frmDate = string.IsNullOrEmpty(txtFrmDeliveryDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFrmDeliveryDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txtToDeliveryDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtToDeliveryDate.Text);

            int count = 0;


            gvDeliveryinfo.DataSource = Delivery.LoadAllDeliveriesByOrgID(UserOrganizationId, pageNo, pageSize, out count, frmDate, toDate, txtShipTo.Text, txtDeliveryName.Text,CatId);
            gvDeliveryinfo.DataBind();

            this.TotalItemsR = count;
            this.pgrLoad.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
            if (gvDeliveryinfo.Rows.Count > 0)
            {
                LinkButton lnkBtn = (LinkButton)pgrLoad.FindControl("Button_" + pageNo.ToString());
                lnkBtn.Font.Bold = true;
            }
            
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewDeliveryNotes.DeliveryInfo", ex);
        }

    }
    /// <summary>
    /// use to show the Delivery detail in popup
    /// </summary>
    /// <param name="DeliveryID"></param>
    public void LoadPopInfobyDeliveryId(int DeliveryID)
    {
        Delivery objDelivery = new Delivery(DeliveryID);
        lblDeliveryDate.Text = Conversion.ParseString(objDelivery.DeliveryDate.ToShortDateString());
        lblDeliveryEstimaDateTime.Text = Conversion.ParseString(objDelivery.DeliveryEstimateDates.ToShortDateString());
        lblDeliveryID.Text = Conversion.ParseString(objDelivery.DeliveryID);
        lblDeliveryName.Text = Conversion.ParseString(objDelivery.DeliveryName);
        lblShipToidnumber.Text = Conversion.ParseString(objDelivery.OrganizationShipTo);
        lblTransporterName.Text = Conversion.ParseString(objDelivery.OrganizationTransporter);
        lblVehicleDetails.Text = Conversion.ParseString(objDelivery.VehicleDetails);
        lblWeight.Text = objDelivery.Weight.ToString();

        try
        {
            string[] loads = objDelivery.LoadIds.Split(',');
            lblTotalLoads.Text = Conversion.ParseString(loads.Length);
            int count = 0;
            if (CatId == (int) ProductCategory.Tire)
            {
                gvAllTire.Visible = true;
                gvAllTire.DataSource = Loads.getTiresInfoByLoadIds(objDelivery.LoadIds, 1, 0, out count);
                gvAllTire.DataBind();
                lblLoadTireCount.Text = Conversion.ParseString(count);
                lblLoadTireCount.Visible = true;
                Label8.Visible = true;
            }
            else
            {
                gvAllProdut.Visible = true;
                gvAllProdut.DataSource = Loads.getProductInfoByLoadIds(objDelivery.LoadIds, CatId);
                gvAllProdut.DataBind();
            }
            
        }
        catch (Exception ex)
        {
        }

    }
    #endregion
    #region Grid Events
    protected void gvDeliveryinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeliveryInfo")
        {
            dvMainLoad.Visible = true;
            LoadPopInfobyDeliveryId(Convert.ToInt32(e.CommandArgument));
        }
        else if (e.CommandName == "Edit")
        {
            Response.Redirect("adddeliverynote?DeliveryId=" + e.CommandArgument);
        }
        

    }
    protected void gvDeliveryinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label imgApproved = (Label)e.Row.FindControl("imgApproved");
            Label imgRejected = (Label)e.Row.FindControl("imgRejected");
            Label imgPending = (Label)e.Row.FindControl("imgPending");
            LinkButton imgbtnEditLoad = (LinkButton)e.Row.FindControl("imgbtnEditLoad");
            

            if (Conversion.ParseDBNullInt(DataBinder.Eval(e.Row.DataItem, "Organizationid")) == UserOrganizationId)
            {
                if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsShipToAccepted")) == true)
                {
                    imgApproved.Visible = true;
                    imgRejected.Visible = false;
                    imgPending.Visible = false;
                    imgbtnEditLoad.Visible = false;

                    
                }
                else if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsShipToRejected")) == true)
                {
                    imgApproved.Visible = false;
                    imgRejected.Visible = true;
                    imgPending.Visible = false;
                    imgbtnEditLoad.Visible = false;
                }
                else
                {
                    imgApproved.Visible = false;
                    imgRejected.Visible = false;
                    imgPending.Visible = true;
                }
            }
            else if (Conversion.ParseDBNullInt(DataBinder.Eval(e.Row.DataItem, "OrganizationTransporterId")) == UserOrganizationId)
            {
                if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsTranspoterAccepted")) == true)
                {
                    imgApproved.Visible = true;
                    imgRejected.Visible = false;
                    imgPending.Visible = false;
                    imgbtnEditLoad.Visible = false;
                }
                else if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsTranspoterRejected")) == true)
                {
                    imgApproved.Visible = false;
                    imgRejected.Visible = true;
                    imgPending.Visible = false;
                    imgbtnEditLoad.Visible = false;
                }
                else
                {
                    imgApproved.Visible = false;
                    imgRejected.Visible = false;
                    imgPending.Visible = true;
                }
            }
        }
    }

    #endregion
}