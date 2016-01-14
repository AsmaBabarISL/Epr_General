using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommonControls_permissionResource : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sLabel = ((Label)(FindControl("lblPermissionLabel"))).Text;
        if (sLabel == "Admin" || sLabel == "Home" || sLabel == "Inventory" || sLabel == "StakeHolder" || sLabel == "Revenue" || sLabel == "Applications" || sLabel == "Reports" || sLabel == "Users" || sLabel == "PTE" || sLabel == "Settings" || sLabel == "Account Management")
        {
            chkAdd.Attributes.Add("onclick", "PermissionOnTop('" + sLabel + "','" + chkView.ClientID + "')");
            chkUpdate.Attributes.Add("onclick", "PermissionOnTop('" + sLabel + "','" + chkView.ClientID + "')");
            chkDelete.Attributes.Add("onclick", "PermissionOnTop('" + sLabel + "','" + chkView.ClientID + "')");
            chkView.Attributes.Add("onclick", "PermissionDivOff('" + sLabel + "','" + chkAdd.ClientID + "','" + chkUpdate.ClientID + "','" + chkDelete.ClientID + "','" + chkView.ClientID + "')");
        }
        else
        {
            chkAdd.Attributes.Add("onclick", "PermissionOnBottom('" + chkView.ClientID + "')");
            chkUpdate.Attributes.Add("onclick", "PermissionOnBottom('" + chkView.ClientID + "')");
            chkDelete.Attributes.Add("onclick", "PermissionOnBottom('" + chkView.ClientID + "')");
            chkView.Attributes.Add("onclick", "PermissionOff('" + chkAdd.ClientID + "','" + chkUpdate.ClientID + "','" + chkDelete.ClientID + "','" + chkView.ClientID + "')");
        }
    }
}