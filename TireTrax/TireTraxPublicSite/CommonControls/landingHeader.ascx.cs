using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class CommonControls_landingHeader : System.Web.UI.UserControl
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

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"]))
            {
                Utils.SetCulture(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"].ToString(), HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"].ToString());
            }

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

            Utils.SetCulture(Convert.ToString(e.CommandArgument), Convert.ToString(e.CommandArgument));
            //string s = Request.RawUrl.ToLower();
            if (!reloadSamePage && Request.RawUrl.ToLower().Contains("/registrationform"))
            {
                string Country = GetCountryURLByCulture(Convert.ToString(e.CommandArgument));
                Response.Redirect(Country.ToLower());
            }
            else if (!reloadSamePage && Request.RawUrl.ToLower().Contains("/registration"))
            {
                string Country = GetCountryURLByCulture(Convert.ToString(e.CommandArgument));
                Response.Redirect(Country.ToLower());
            }
            //else if (!reloadSamePage && Request.RawUrl.ToLower().Contains("/default"))
            //{
            //    string Country = GetCountryURLByCulture(Convert.ToString(e.CommandArgument));
            //    Response.Redirect(Country.ToLower());
            //}
            else
            {
                Response.Redirect(Request.Url.ToString().ToLower());
            }
        }
    }

    private string GetCountryURLByCulture(string Culture)
    {
        string Country = "";
        switch (Culture.ToLower())
        {
            case "en-us":
                Country = "/us/registration/";
                break;
            case "es-mx":
                Country = "/mx/registration/";
                break;
            case "fr-fr":
                Country = "/ca/registration/fr/";
                break;
            case "fr-ca":
                Country = "/ca/registration/fr/";
                break;
            case "en-ca":
                Country = "/ca/registration/en/";
                break;
            case "ja-jp":
                Country = "/ja/registration/";
                break;
            case "ko-kr":
                Country = "/sk/registration/";
                break;
            case "en-au":
                Country = "/au/registration/";
                break;
            case "zh-cn":
                Country = "/cn/registration/";
                break;
            default:
                Country = "/registration/";
                break;
        }
        return Country;
    }
}