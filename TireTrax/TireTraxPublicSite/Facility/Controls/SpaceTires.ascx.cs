using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class Facility_Controls_SpaceTires : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadFacilityLotRowSpaceName(string facilityname,string lotid,string lotname,string rowid,string rowname,string spacename)
    {
        hdnspacename.Value = spacename;
        hdnrowname.Value = rowname;
        hdnlotname.Value = lotname;
        hdnlotid.Value = lotid;
        hdnfacilityname.Value = facilityname;
        lblfacilityForLane.Text = facilityname;
        hdnrowIds.Value = rowid;
        
            lblLotNumberLane.Text=lotname;

            lblSpaceName.Text = rowname;
            lblLaneName.Text = spacename;


    }


    public void LoadTires(int spaceId)
    {

        try
        {

            
            pnlTires.Visible = true;
            hndspaceId.Value = spaceId.ToString();
            gvTires.DataSource = Tire.getTireBySpaceId(spaceId);
            gvTires.DataBind();
        }

        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Facility/Controls/LotSpace.ascx.cs loadLaneText", ex);
        }

    }



    protected void lnkbtnCancelTire_Click(object sender, EventArgs e)
    {
        pnlTires.Visible = false;
        //Response.Redirect("facility");

    }



    protected void lnkbtnBackTire_Click(object sender, EventArgs e)
    {
        pnlTires.Visible = false;
        this.Page.Controls[0].FindControl("ContentPlaceHolder1").FindControl("ucFacilitySpaces").GetType().InvokeMember("loadLaneText", System.Reflection.BindingFlags.InvokeMethod, null, this.Page.Controls[0].FindControl("ContentPlaceHolder1").FindControl("ucFacilitySpaces"), new object[] { Conversion.ParseInt(hdnrowIds.Value) });
        this.Page.Controls[0].FindControl("ContentPlaceHolder1").FindControl("ucFacilitySpaces").GetType().InvokeMember("loadFacilityLotAndRowName", System.Reflection.BindingFlags.InvokeMethod, null, this.Page.Controls[0].FindControl("ContentPlaceHolder1").FindControl("ucFacilitySpaces"), new object[] { hdnfacilityname.Value, hdnlotname.Value,hdnrowname.Value,Conversion.ParseInt(hdnlotid.Value),Conversion.ParseInt(hdnidfaclityid.Value) });

    }



}