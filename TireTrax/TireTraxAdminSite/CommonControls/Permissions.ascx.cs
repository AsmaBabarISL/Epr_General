using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TireTraxLib.Security;

public partial class CommonControls_Permissions : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.DataTable resourceTopics = GroupPages.GetAllResources();

        for (int i = 0; i < resourceTopics.Rows.Count; i++)
        {
            DataRow row = resourceTopics.Rows[i];
            Control permission = LoadControl("~/CommonControls/permissionResource.ascx");

            //this.Controls.Add(permission);
            if (row["intparentId"].ToString() == "0")
            {
                if (row["vchName"].ToString() == "Admin(Menu)")
                {
                    this.tdAdminTop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;
                }
                else if (row["vchName"].ToString() == "Home(Menu)")
                {
                    this.tdHomeTop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;

                }
                else if (row["vchName"].ToString() == "Inventory(Menu)")
                {
                    this.tdInventoryTop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;

                }
                else if (row["vchName"].ToString() == "Stakeholders(Menu)")
                {
                    this.tdStakeholderTop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;

                }
                else if (row["vchName"].ToString().StartsWith("Revenue"))
                {
                    this.tdRevenueTop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;

                }
                else if (row["vchName"].ToString().StartsWith("Applications"))
                {
                    this.tdApplicationsTop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;

                }
                else if (row["vchName"].ToString().StartsWith("Reports"))
                {
                    this.tdReportsTop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;

                }
                else if (row["vchName"].ToString().StartsWith("Users"))
                {
                    this.tdUsersTop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;

                }
                else if (row["vchName"].ToString().StartsWith("PTE"))
                {
                    this.tdPTETop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;

                }
                else if (row["vchName"].ToString().StartsWith("Settings"))
                {
                    this.tdSettingsTop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;

                }
                else if (row["vchName"].ToString().StartsWith("Account Management"))
                {
                    this.tdAccountManagementTop.Controls.Add(permission);
                    Label permissionLabel = (Label)permission.FindControl("lblPermissionLabel");
                    permissionLabel.Text = row["vchName"].ToString();

                    HiddenField resourceId = (HiddenField)permission.FindControl("hdResourceId");
                    resourceId.Value = row["intResourceId"].ToString();
                    continue;

                }
               
            }



            switch (row["intparentId"].ToString())
            {
                case "100":
                    this.tdAdminBottom.Controls.Add(permission);
                    break;
                case "105":
                    this.tdHomeBottom.Controls.Add(permission);
                    break;
                case "110":
                    this.tdInventoryBottom.Controls.Add(permission);
                    break;
                case "115":
                    this.tdStakeholderBottom.Controls.Add(permission);
                    break;
                case "120":
                    this.tdRevenueBottom.Controls.Add(permission);
                    break;
                case "125":
                    this.tdApplicationsBottom.Controls.Add(permission);
                    break;
                case "130":
                    this.tdReportsBottom.Controls.Add(permission);
                    break;
                case "135":
                    this.tdUsersBottom.Controls.Add(permission);
                    break;
                case "140":
                    this.tdPTEBottom.Controls.Add(permission);
                    break;
                case "145":
                    this.tdSettingsBottom.Controls.Add(permission);
                    break;
                case "285":
                    this.tdAccountManagementBottom.Controls.Add(permission);
                    break;
                default:
                    break;
            }

            Label permissionLabel2 = (Label)permission.FindControl("lblPermissionLabel");
            permissionLabel2.Text = row["vchName"].ToString();

            HiddenField resourceId2 = (HiddenField)permission.FindControl("hdResourceId");
            resourceId2.Value = row["intResourceId"].ToString();
        }
    }
}