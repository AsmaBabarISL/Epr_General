using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class Revenue_DetailRevenue : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liRevenue','{0}');", ResourceMgr.GetMessage("Revenue")), true);
        GetPermission(ResourceType.Revenue, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }

        if (User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("/");
        }

        if (!IsPostBack)
        {
            LoadRevenue_Detail();

        }
    }
         
        private void LoadRevenue_Detail()
        {
           try
            {

               gvRevenue.DataSource = RevenuInventory.getDetailsRevenueByOrganizationId(UserOrganizationId, DateTime.Now);
                gvRevenue.DataBind();

            }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "DetailRevenue.aspx.LoadRevenueDetail", ex);
        }
    }

}
