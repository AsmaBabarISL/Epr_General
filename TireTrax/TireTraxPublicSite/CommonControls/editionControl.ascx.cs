using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class CommonControls_editionControl : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BasePage objBase = new BasePage();

            DataSet ds = UserInfo.GetAllActiveLanguages();

            DataRow dr = ds.Tables[0].Select("LanguageId =" + objBase.LanguageId)[0];
            litLangauge.Text = Convert.ToString(dr["CountryName"]);
            img.Src = "/images/" + Convert.ToString(dr["Flag"]);

            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "LanguageId <>" + objBase.LanguageId;

            rptLanguage.DataSource = dv;
            rptLanguage.DataBind();

        }
    }

    protected void rptLanguage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ChangeLanguage")
        {
            Utils.SetCulture(Convert.ToString(e.CommandArgument), Convert.ToString(e.CommandArgument));
            Response.Redirect(Request.Url.ToString());
        }
    }
}