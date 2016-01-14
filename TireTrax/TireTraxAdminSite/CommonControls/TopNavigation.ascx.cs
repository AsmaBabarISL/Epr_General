using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class CommonControls_TopNavigation : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo obj = UserInfo.GetCurrentUserInfo();
        litLoginName.Text = obj.Login;

        if (obj.LastLoginDate != null && obj.LastLoginDate != DateTime.MinValue)
        {
            //29 October, 2012//11:00am
            litLastLoginDate.Text = String.Format("Last Login on <b>{0}</b> at <b>{1}</b>", obj.LastLoginDate.ToString("dd MMMM, yyyy"), obj.LastLoginDate.ToString("hh:mm tt"));

            litLastLoginNotAvailable.Visible = false;
            litLastLoginDate.Visible = true;
        }
        else
        {
            litLastLoginNotAvailable.Visible = true;
            litLastLoginDate.Visible = false;
        }
    }
}