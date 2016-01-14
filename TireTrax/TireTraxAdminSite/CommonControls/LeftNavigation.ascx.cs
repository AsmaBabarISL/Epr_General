using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommonControls_LeftNavigation : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ActivateSubMenus", "ActivateSelectedTab();", true);
    }
}