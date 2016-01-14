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


public partial class Facility_Controls_LotRows :BaseControl
{
    int SpaceId, LaneId;
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    /// <summary>
    /// Load Rows Info
    /// 
    /// </summary>

    public void loadFacilityandLotName(string facilityname, string lotname,string facilityId)
    {
        lblFacilityForSpace.Text = facilityname;
        lblLotNumberSpace.Text = lotname;
        hdnfacilityname.Value = facilityname;
        hdnlotname.Value = lotname;
        hdnidfaclityid.Value = facilityId;


    }


    public void loadLotRows(int lotid)
    {
        //int lotid = 90;
        //string.IsNullOrEmpty(hidLotId.Value) ? 0 : Convert.ToInt32(hidLotId.Value);
        try
        {
            Session["LotId"] = lotid.ToString();
            pnlSpace.Visible = true;
            //int lotid = Convert.ToInt32(hidLotId.Value);
            hidLotId.Value = lotid.ToString();
            DataSet ds = LotRows.getLotSpacesByLotId(1, 10, out totalRows, lotid);
            gvSetting.DataSource = ds.Tables[0];
            gvSetting.DataBind();
        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "Facility/Controls/LotRows.ascx loadLotRows", ex);
        }



    }


    protected void gvSetting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
       



     
      if (e.CommandName == "LaneInfoBySpaceIdPopUp")
        {
            ViewState["spaceid"] = e.CommandArgument;
            //lblErrorLane.Text = String.Empty;
            //lblErrorLot.Text = String.Empty;
            lblErrorSpace.Text = String.Empty;
            //pnlLot.Visible = false;
            pnlSpace.Visible = false;
            //pnlLane.Visible = true;
            //PanelLaneValue.Visible = true;
            //addmoreLane.Visible = false;
            //gvLane.Visible = true;
            //lblfacilityForLane.Text = lblfacilityname.Text;

            //lblLotNumberLane.Text = lblLotNumberSpace.Text;
            GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            HiddenField lbl = (HiddenField)gvSetting.Rows[RemoveAt].FindControl("hdnRowName");



            lotspace.loadFacilityLotAndRowName(hdnfacilityname.Value, hdnlotname.Value, lbl.Value, Convert.ToInt32(hidLotId.Value),Conversion.ParseInt(hdnidfaclityid.Value));
            lotspace.loadLaneText(Convert.ToInt32(e.CommandArgument));

            //lblSpaceName.Text = lbl.Text;
            //// DataSet ds = Space.GetParkingLotNumberByLotId(Convert.ToInt32(hidLotId.Value));

            // Label lbl = (Label)gvSetting.Rows[RemoveAt].FindControl("lblspacename");

            //loadLaneText(Convert.ToInt32(e.CommandArgument));


        }


        else if (e.CommandName == "AddSpaceLane")
        {
            ViewState["spaceid"] = e.CommandArgument;
            //lblErrorLane.Text = String.Empty;
            //lblErrorLot.Text = String.Empty;
            lblErrorSpace.Text = String.Empty;
            //pnlLot.Visible = false;
            pnlSpace.Visible = false;
            //pnlLane.Visible = true;
            //PanelLaneValue.Visible = true;
            //addmoreLane.Visible = false;
            //gvLane.Visible = true;
            //lblfacilityForLane.Text = lblfacilityname.Text;

            //lblLotNumberLane.Text = lblLotNumberSpace.Text;
            GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            LinkButton lbl = (LinkButton)gvSetting.Rows[RemoveAt].FindControl("lnkbtnspacenamepopup");
            //lblSpaceName.Text = lbl.Text;
            // DataSet ds = Space.GetParkingLotNumberByLotId(Convert.ToInt32(hidLotId.Value));

            // Label lbl = (Label)gvSetting.Rows[RemoveAt].FindControl("lblspacename");

            //loadLaneText(Convert.ToInt32(e.CommandArgument));

        }



    }
  

   



    /// <summary>
    /// Next Button Click For Row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbkCancelSpace_Click(object sender, EventArgs e)
    {

        //Response.Redirect("facility");
        pnlSpace.Visible = false;

    }

    protected void lnkbtnBackRows_Click(object sender, EventArgs e)
    {
        pnlSpace.Visible = false;

        
        this.Page.Controls[0].FindControl("ContentPlaceHolder1").FindControl("ucFacilityLots").GetType().InvokeMember("LoadLots", System.Reflection.BindingFlags.InvokeMethod, null, this.Page.Controls[0].FindControl("ContentPlaceHolder1").FindControl("ucFacilityLots"), new object[] { "1", hdnidfaclityid.Value });
        
    }




}
