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

public partial class Facility_Controls_FacilityLots :BaseControl
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        pageSize = 5;
        if (!IsPostBack)
        {
        }
        else
        {
            if (TotalItems > 0)
            {
                pgrLots.DrawPager(CurrentPage, TotalItems, pageSize, MaxPagesToShow);
            }
        }
    }
    //public void LoadLots(string pageNo, string facilityid)
    //{
    //    LoadLots(Conversion.ParseInt(pageNo), Conversion.ParseInt(facilityid));
    //}
    public void LoadLots(string pageNo, string facilityid="0")
    {

        try
        {
            
            
            pnlLot.Visible = true;
            hdnidfaclityid.Value = facilityid;
            int pageSize = 5;
            gvLot.PageSize = pageSize;
            CurrentPage = Conversion.ParseInt(pageNo);
            //dvpopupfacilityinfo.Visible = true;
            DataSet ds1 = Facility.GetFacilityNameByFacilityId(Conversion.ParseInt(facilityid));
            lblfacilityname.Text = ds1.Tables[0].Rows[0][0].ToString();
            hdnfacilityname.Value = lblfacilityname.Text;
            hdnidfaclityid.Value = facilityid.ToString();
            int count = 0;

            DataSet ds = Lots.getParkingLot(Conversion.ParseInt(pageNo), pageSize, UserOrganizationId, out count, null, Conversion.ParseInt(facilityid));


            gvLot.DataSource = ds;
            gvLot.DataBind();

            this.TotalItems = count;
            this.pgrLots.DrawPager(Conversion.ParseInt(pageNo), this.TotalItems, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Facility/Controls/FacilityLots.ascx.cs LoadLots", ex);
        }
    }



    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrLots.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.LoadLots(CurrentPage.ToString(), hdnidfaclityid.Value);
        }


        return base.OnBubbleEvent(source, args);
    }





    protected void gvLot_RowCommand(object sender, GridViewCommandEventArgs e)
    {

   

        if (e.CommandName == "RowInfobyLotIdPopUp")
        {

            //lblErrorLane.Text = String.Empty;
            lblErrorLot.Text = String.Empty;
            //lblErrorSpace.Text = String.Empty;

            
            pnlLot.Visible = false;
            
            hidLotId.Value = Convert.ToString(e.CommandArgument);

            GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            HiddenField lbl = (HiddenField)gvLot.Rows[RemoveAt].FindControl("hdGVLotNumber");
            LotRowControl.loadFacilityandLotName(hdnfacilityname.Value, lbl.Value,hdnidfaclityid.Value);
            LotRowControl.loadLotRows(Convert.ToInt32(e.CommandArgument));
            //dvpopupfacilityinfo.Visible = true;
            //lblFacilityForSpace.Text = lblfacilityname.Text;
            //loadGridAndHeaderText();
        }

        if (e.CommandName == "AddLotSpace")
        {
            //lblErrorLane.Text = String.Empty;
            lblErrorLot.Text = String.Empty;
            //lblErrorSpace.Text = String.Empty;


            pnlLot.Visible = false;
            //pnlSpace.Visible = true;
            hidLotId.Value = Convert.ToString(e.CommandArgument);

            GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            LinkButton lbl = (LinkButton)gvLot.Rows[RemoveAt].FindControl("lnkbtnlotinfopop");
            //lblLotNumberSpace.Text = lbl.Text;
            //lblFacilityForSpace.Text = lblfacilityname.Text;
            //loadGridAndHeaderText();
        }


   
        else if (e.CommandName == "Edit")
        {

            hidLotId.Value = Convert.ToString(e.CommandArgument);
        }

        else if (e.CommandName == "Delete")
        {
            Lots.deleteLot(Convert.ToInt32(e.CommandArgument));
            LoadLots("1");

        }

        else if (e.CommandName == "Cancel")
        {
            gvLot.EditIndex = -1;
            LoadLots("1");

        }

    }








    protected void lnkbtnCancelLot_Click(object sender, EventArgs e)
    {
        hidLotId.Value = "";
        hdnidfaclityid.Value = "";
        Response.Redirect("facility");

    }
}