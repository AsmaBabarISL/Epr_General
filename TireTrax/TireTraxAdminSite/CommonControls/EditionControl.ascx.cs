using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class CommonControls_EditionControl : System.Web.UI.UserControl
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
            bool reloadSamePage = false;
            string[] strArr = Convert.ToString(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"]).Split('-');
            string CountryCode = "";

            if (strArr.Length > 1)
            {
                CountryCode = strArr[1];
            }
            if (Convert.ToString(e.CommandArgument).Split('-').Last() == CountryCode)
            {
                reloadSamePage = true;
            }

            if (!reloadSamePage && Request.RawUrl.Contains("/Registration/"))
            {
                Utils.SetCulture("en-US", "en-US");
                string Country = GetCountryURLByCulture(Convert.ToString(e.CommandArgument));
                Response.Redirect(Country);
            }
            else if (!reloadSamePage && Request.RawUrl.Contains("/RegistrationForm/"))
            {
                Utils.SetCulture("en-US", "en-US");
                string Country = GetCountryURLByCulture(Convert.ToString(e.CommandArgument));
                Response.Redirect(Country);
            }
            else
            {
                Utils.SetCulture(Convert.ToString(e.CommandArgument), Convert.ToString(e.CommandArgument));
                Response.Redirect(Request.Url.ToString());
            }
        }
    }

    private string GetCountryURLByCulture(string Culture)
    {
        string Country = "";
        switch (Culture.ToLower())
        {
            case "en-us":
                Country = "/USA/Registration/";
                break;
            case "es-mx":
                Country = "/Mexico/Registration/";
                break;
            case "fr-fr":
                Country = "/Canada/Registration/fr/";
                break;
            case "fr-ca":
                Country = "/Canada/Registration/fr/";
                break;
            case "en-ca":
                Country = "/Canada/Registration/en/";
                break;
            case "ja-jp":
                Country = "/Japan/Registration/";
                break;
            case "ko-kr":
                Country = "/SouthKorea/Registration/";
                break;
            case "en-au":
                Country = "/Australia/Registration/";
                break;
            case "zh-cn":
                Country = "/China/Registration/";
                break;
            default:
                Country = "/Registration/";
                break;
        }
        return Country;
    }
}