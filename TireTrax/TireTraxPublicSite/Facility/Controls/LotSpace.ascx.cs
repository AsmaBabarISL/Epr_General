using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class Facility_Controls_LotSpace : BaseControl
{

    int SpaceId, LaneId;
    protected void Page_Load(object sender, EventArgs e)
    {

    }







    public void loadFacilityLotAndRowName(string facilityname,string lotname,string rowname,int lotid,int facilityid)
    {
        lblfacilityForLane.Text = facilityname;
        lblLotNumberLane.Text = lotname;
        hdnlotid.Value = lotid.ToString();
        lblSpaceName.Text = rowname;
        hdnfacilityname.Value = facilityname;
        hdnlotname.Value = lotname;
        hdnidfaclityid.Value = facilityid.ToString();
        hdnrowname.Value = rowname;
        
        

    }



    public void loadLaneText(int spaceid)
    {
        try
        {
            int count = 0;
            hdnrowIds.Value = spaceid.ToString();
            pnlLane.Visible = true;
            gvLane.DataSource = LotSpace.getLane(1,0,out count,spaceid);
            gvLane.DataBind();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Facility/Controls/LotSpace.ascx.cs loadLaneText", ex);
        }
        //if (gvLane.Rows.Count == 0)
        //{
        //    PanelLaneValue.Visible = true;
        //}
        //else
        //{
        //    PanelLaneValue.Visible = false;
        //}



    }

    protected void lnkbtnCancelSpace_Click(object sender, EventArgs e)
    {

        //Response.Redirect("facility");
        pnlLane.Visible = false;

    }



    protected void lnkbtnBackSpace_Click(object sender, EventArgs e)
    {
        pnlLane.Visible = false;
        this.Page.Controls[0].FindControl("ContentPlaceHolder1").FindControl("LotRowControl").GetType().InvokeMember("loadLotRows", System.Reflection.BindingFlags.InvokeMethod, null, this.Page.Controls[0].FindControl("ContentPlaceHolder1").FindControl("LotRowControl"), new object[] { Conversion.ParseInt(hdnlotid.Value) });
        this.Page.Controls[0].FindControl("ContentPlaceHolder1").FindControl("LotRowControl").GetType().InvokeMember("loadFacilityandLotName", System.Reflection.BindingFlags.InvokeMethod, null, this.Page.Controls[0].FindControl("ContentPlaceHolder1").FindControl("LotRowControl"), new object[] {hdnfacilityname.Value,hdnlotname.Value,hdnidfaclityid.Value });

    }




   
    /// <summary>
    /// Next Button For Spaces
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

















    protected void gvLane_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "TireInfoBySpaceIdPopUp")
        {
            GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            HiddenField lbl = (HiddenField)gvLane.Rows[RemoveAt].FindControl("hdnspacename");
            pnlLane.Visible = false;
            ucSpaceTires.LoadTires(Conversion.ParseInt(e.CommandArgument));
            ucSpaceTires.LoadFacilityLotRowSpaceName(hdnfacilityname.Value, hdnlotid.Value,hdnlotname.Value, hdnrowIds.Value, hdnrowname.Value, lbl.Value);
        }
    }
}