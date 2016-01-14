using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class Invoices_AddInvoice : BasePage
{
    #region Properties
    private DataTable dt
    {
        get
        {
            return (DataTable)Session["dt"];
        }
        set
        {
            Session["dt"] = value;
        }
    }
    private string _DeliveryIds
    {
        get
        {
            return Conversion.ParseString(Session["DeliveryIds"]);
        }
        set
        {
            Session["DeliveryIds"] = value;
        }
    }
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
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Permission
        GetPermission(ResourceType.AddInvoice, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canAdd)
        {
            Response.Redirect("error");
        }
        #endregion

        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liBA','{0}');", ResourceMgr.GetMessage("Add Invoice")), true);

        if (!IsPostBack)
        {
            
            _DeliveryIds = "";
            if (Request.QueryString["InvoiceId"] != null)
            {
                LoadByInvoiceID(Conversion.ParseInt(Request.QueryString["InvoiceId"]));
            }

        }
        LoadBasicInfo();
        #region Script Load and Pager setting
        pageSize = 10;
        //if (TotalItemsR > 0)
        //{
        //    pgrDeliveryLoad.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        //}
        #endregion

    }

    /// <summary>
    /// Use for update the Delivery Records
    /// </summary>
    /// <param name="deliveryId">Delivery Id sent from View Delivery Notes Page</param>
    protected void LoadByInvoiceID(int InvoiceId)
    {
        Invoice objInvoice = new Invoice(InvoiceId);
        txtInvoiceDate.Text = Conversion.ParseString(objInvoice.DateCreated.ToShortDateString());
        //txtDeliveryEstimaDateTime.Text = Conversion.ParseString(objDelivery.DeliveryEstimateDates.ToShortDateString());
        ////txtDeliveryID.Text = Conversion.ParseString(objDelivery.DeliveryID);
        //txtDeliveryName.Text = Conversion.ParseString(objDelivery.DeliveryName);
        //txtShipToidnumber.Text = Conversion.ParseString(objDelivery.OrganizationShipTo);
        //txtTransporterID.Text = Conversion.ParseString(objDelivery.OrganizationTransporter);
        //txtVehicleDetails.Text = Conversion.ParseString(objDelivery.VehicleDetails);
        //txtWeight.Text = objDelivery.Weight.ToString();

        //if (objDelivery.TranspotationType == 1)
        //    rdTransportor.SelectedValue = "1";
        //else if (objDelivery.TranspotationType == 2)
        //    rdTransportor.SelectedValue = "2";
        //else if (objDelivery.TranspotationType == 3)
        //    rdTransportor.SelectedValue = "3";

        //hdLoadIDs.Value = objDelivery.LoadIds;
        //_LoadIds = objDelivery.LoadIds;
        //hidOrgID.Value = objDelivery.OrganizationShipToId.ToString();
        //hdTranspoterID.Value = objDelivery.OrganizationTransporterId.ToString();
        //LoadsInfo(1);
        //getLoads(objDelivery.LoadIds);
        //LoadTireInfoByLoadIds(objDelivery.LoadIds, 1);

    }

    protected void LoadBasicInfo()
    {

        DataSet ds = Lots.GetAllCompanieswithAddressWithinSameStewardship(UserOrganizationId, CatId);
        grvOrganizations.DataSource = ds;
        grvOrganizations.DataBind();
    }
    /// <summary>
    /// Use for ShipTo field to show the companies in popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkSearchCompany_Click(object sender, EventArgs e)
    {
        LoadCompaniesGrid();
        dvOrganization.Visible = true;
    }
    private void LoadCompaniesGrid()
    {
        try
        {
            //string standardstewardshipIds = "";
            //standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            //OrganizationInfo objorg = new OrganizationInfo(UserOrganizationId);
            //string[] arr = standardstewardshipIds.Split(',');
            //bool isstewardship = false;
            //foreach (var id in arr)
            //{
            //    if (Conversion.ParseInt(id) == objorg.OrganizationTypeId)
            //        isstewardship = true;
            //}
            ////  UserInfo userobj = new UserInfo(LoginMemberId);
            //if (isstewardship)
            //{
            //    DataSet ds = Lots.GetAllCompanieswithAddressbyStewardshipId(UserOrganizationId);
            //    grvOrganizations.DataSource = ds;
            //    grvOrganizations.DataBind();
            //    grvTanspoterOrganizations.DataSource = ds;
            //    grvTanspoterOrganizations.DataBind();
            //}
            //else
            //{
            DataSet ds = Lots.GetAllCompanieswithAddressWithinSameStewardship(UserOrganizationId,CatId);
            grvOrganizations.DataSource = ds;
            grvOrganizations.DataBind();
            //}

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadCompaniesGrid", ex);
        }
    }
    /// <summary>
    /// to hid the Company popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        dvOrganization.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
    }

    protected void lnkCompany_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidOrgID.Value) && (grvOrganizations.Rows.Count > 0))
        {
            lblErrorPermanentLotdv.Text = "Please select Organization";
            return;
        }

        else if (grvOrganizations.Rows.Count == 0)
        {
            lblErrorPermanentLotdv.Text = "You Did not have any Organization";
            return;
        }
        txtShipToidnumber.Text = hidText.Value;
        ltrAddress.Text = hidStreetAddress.Value;
        ltrAddress.Text += "<br/>" + hidStateAddress.Value;
        dvOrganization.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;

    }

    /// <summary>
    /// use to Add Batch Button to show the Loads in popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkGetAllLoads_Click(object sender, EventArgs e)
    {
        UnAssignedDeliveriesInfo(1);
        dvGetAllDelivery.Visible = true;


    }
    private void UnAssignedDeliveriesInfo(int pageNo)
    {
        try
        {
            pageSize = 10;
            gvGetAllDeliveries.PageSize = pageSize;
            CurrentPageR = pageNo;
            // DateTime frmDate = string.IsNullOrEmpty(txtInvoiceDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFrmDate.Text);
            int count = 0;
            DataSet ds = Invoice.getAllUnAssignedDeliveries(UserOrganizationId);
            dt = ds.Tables[0];
            gvGetAllDeliveries.DataSource = dt;
            gvGetAllDeliveries.DataBind();

            this.TotalItemsR = count;
            //this.pgrDeliveryLoad.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "addinvoice.UnAssignedDeliveriesInfo", ex);
        }
    }


    /// <summary>
    /// Use to get the selected load details from Load dataset of popup
    /// </summary>
    /// <param name="_LoadIds"></param>
    private void getLoads(string _deliveryIds)
    {

        //DataTable dtGetAll = dt;
        //dtGetAll.DefaultView.RowFilter = "[deliveryId] in (" + _deliveryIds + ")";
        //DataTable dtOutput = dtGetAll.DefaultView.ToTable();
        //gvInvoicesinfo.DataSource = dtOutput;
        DataSet ds = Invoice.getDeliveryInvoices(UserOrganizationId, _deliveryIds);

        gvInvoicesinfo.DataSource = ds;
        gvInvoicesinfo.DataBind();
        if (ds != null & ds.Tables.Count > 0)
        {
            int orgId = Conversion.ParseInt(ds.Tables[0].Rows[0]["OrganizationId"]);
            OrganizationInfo objOrg = new OrganizationInfo(orgId);
            lblOrgName.Text = objOrg.LegalName;
            lblOrgstreet.Text = objOrg.Address;
            lblOrgstate.Text = (objOrg.City == string.Empty ? "" : objOrg.City + ", ") + (string.IsNullOrEmpty(objOrg.StateName) ? "" : objOrg.StateName + ", ") + (string.IsNullOrEmpty(objOrg.ZipCode) ? "" : objOrg.ZipCode);
            lblInvoiceDate.Text = DateTime.Now.ToShortDateString();
            lnkbtnAddInvoice.Visible = true;
        }
    }


    /// <summary>
    /// Use to set the selected load from popup to main page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkGetAllDeliveryContinue_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hdDeliveryIDs.Value) && (gvGetAllDeliveries.Rows.Count > 0))
        {
            lblGetAllDeliveriesError.Text = "Please select Delivery";
            return;
        }
        else if (gvGetAllDeliveries.Rows.Count == 0)
        {
            lblGetAllDeliveriesError.Text = "You Did not have any Delivery";
            return;
        }
        try
        {
            string _deliveryIds = hdDeliveryIDs.Value;
            _deliveryIds = _deliveryIds.Replace("\r\n", "").Replace(" ", "").Trim(',');
            string removeids = hdRemoveDeliveryIds.Value;
            removeids = removeids.Replace("\r\n", "").Replace(" ", "").Trim(',');
            string[] remList = removeids.Split(',');
            if (remList.Length > 0)
                for (int i = 0; i <= remList.Length - 1; i++)
                {
                    if (_deliveryIds.Contains("," + remList[i].ToString() + ","))
                    {
                        _deliveryIds = _deliveryIds.Replace("," + remList[i].ToString() + ",", ",");
                        _deliveryIds = _deliveryIds.Replace(" ", "").Trim(',');
                    }
                    else if (_deliveryIds.Contains(remList[i].ToString() + ","))
                    {
                        _deliveryIds = _deliveryIds.Replace(remList[i].ToString() + ",", ",");
                        _deliveryIds = _deliveryIds.Replace(" ", "").Trim(',');
                    }
                    else if (_deliveryIds.Contains("," + remList[i].ToString()))
                    {
                        _deliveryIds = _deliveryIds.Replace("," + remList[i].ToString(), ",");
                        _deliveryIds = _deliveryIds.Replace(" ", "").Trim(',');
                    }
                }
            _DeliveryIds = _deliveryIds;
            hdDeliveryIDs.Value = "";
        }
        catch (Exception ex) { }

        dvGetAllDelivery.Visible = false;
        lblGetAllDeliveriesError.Text = string.Empty;
        lblErrorMessageLoad.Visible = false;
        lblErrorMessageLoad.Text = "";
        getLoads(_DeliveryIds);
        //DataTable dtGetAll = dt;
        //dtGetAll.DefaultView.RowFilter = "[loadid] in (" + _LoadIds + ")";
        //DataTable dtOutput = dtGetAll.DefaultView.ToTable();
        //gvAllLoads.DataSource = dtOutput;
        //gvAllLoads.DataBind();
        //LoadTireInfoByLoadIds(_DeliveryIds, 1);

    }


    /// <summary>
    /// Use to save the data in delivery notes table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtnAddInvoice_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(txtDeliveryName.Text) || string.IsNullOrEmpty(txtDeliveryDate.Text) || string.IsNullOrEmpty(txtShipToidnumber.Text))
        //{
        //    return;
        //}
        //else if (gvAllLoads.Rows.Count == 0)
        //{
        //    lblErrorMessageLoad.Visible = true;
        //    lblErrorMessageLoad.Text = "Please select Load/Batch";
        //    return;
        //}

        //int result = InserUpdateDelivery();
        //if (result > 0)
        //{
        //    SendNotification(result, Conversion.ParseInt(hidOrgID.Value));
        //    if (rdTransportor.SelectedValue == "3")
        //        SendNotification(result, Conversion.ParseInt(hdTranspoterID.Value));

        //    Response.Redirect("invoices");
        //}

    }

    /// <summary>
    /// use to cancel the new delivery and redirect to View Delivery Notes page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("invoices");
    }


    /// <summary>
    /// Cancel the Load Popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkGetAllDeliveryCancel_Click(object sender, EventArgs e)
    {
        dvGetAllDelivery.Visible = false;
        lblGetAllDeliveriesError.Text = string.Empty;
    }
    protected void gvInvoicesinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        decimal totalAmt = Convert.ToDecimal(lblInvoiceAmount.Text);
        decimal Profitprice = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Label lblAmt = (Label)e.Row.FindControl("lblAmt");
            lblAmt.Text = Conversion.ParseDBNullString(DataBinder.Eval(e.Row.DataItem, "PTEamount"));
            if (string.IsNullOrEmpty(lblInvoiceAmount.Text))
                totalAmt = 0;
            else
                totalAmt =Convert.ToDecimal(lblInvoiceAmount.Text);

            Profitprice = Conversion.ParseDBNullDecimal(lblAmt.Text);
                totalAmt =totalAmt + Profitprice;
        }
        lblInvoiceAmount.Text=totalAmt.ToString();
    }
    protected void gvInvoicesinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "Edit1")
        { }
        if (e.CommandName == "DeliveryInfo")
        { }
    }
}