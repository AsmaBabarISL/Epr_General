using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "removeLanguageMenu", "removeLanguageMenu()", true);
        if (!IsPostBack)
        {
            Utils.GetLookUpData<DropDownList>(ref ddlCountry, LookUps.Country);

            int country = GetCountryIDByCulture();

            if (country > 0)
            {
                ddlCountry.SelectedValue = country.ToString();
            }
            if (Request.RawUrl.Contains("/USA") || Request.RawUrl.ToUpper().Contains("/US"))
            {
                ddlCountry.SelectedValue = "235";
            }
            else if (Request.RawUrl.Contains("/Mexico") || Request.RawUrl.ToUpper().Contains("/MX"))
            {
                ddlCountry.SelectedValue = "159";
            }
            else if (Request.RawUrl.Contains("/Canada") || Request.RawUrl.ToUpper().Contains("/CA"))
            {
                ddlCountry.SelectedValue = "39";
            }
            else if (Request.RawUrl.Contains("/Japan") || Request.RawUrl.ToUpper().Contains("/JA"))
            {
                ddlCountry.SelectedValue = "116";
            }
            else if (Request.RawUrl.Contains("/SouthKorea") || Request.RawUrl.ToUpper().Contains("/SK"))
            {
                ddlCountry.SelectedValue = "124";
            }
            else if (Request.RawUrl.Contains("/China") || Request.RawUrl.ToUpper().Contains("/CN"))
            {
                ddlCountry.SelectedValue = "49";
            }
            else if (Request.RawUrl.Contains("/Australia") || Request.RawUrl.ToUpper().Contains("/AU"))
            {
                ddlCountry.SelectedValue = "14";
            }
            ddlCountry_SelectedIndexChanged(null, null);


        }
        
    }

    protected void continue_Click(object sender, EventArgs e)
    {
        //CA = 39, MX = 159, US = 235,JP=116, SK=124, CH = 49, AU = 14
        int StewardShipID = Convert.ToInt32(ddlStewardship.SelectedValue.Split('-').First());
        int CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        string StateCode = "";
        if (StewardShipID > 0)
        {
           // StateCode = ddlStewardship.SelectedValue.Split('-').Last().ToLower();

            DataSet ds = OrganizationInfo.GetStateCodeByStateId(Conversion.ParseInt(ddlStewardship.SelectedValue));

            StateCode = ds.Tables[0].Rows[0][0].ToString().ToLower();
            //if (ds.Tables[1].Rows.Count > 0)
            //if (ds.Tables[1].Rows[0][0] != DBNull.Value)
            //    StewardShipID = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
        }
        if (ddlStewardship.SelectedItem.Value != "0")
        {
            if (CountryID == 39)
            {
                //if (Request.RawUrl.Contains("/fr/"))
                //{
                //    Utils.SetCulture("fr-CA", "fr-CA");
                //}
                if (LanguageId == 3)
                {
                    Utils.SetCulture("fr-CA", "fr-CA");
                }
                else
                {
                    Utils.SetCulture("en-CA", "en-CA");
                }

                if (StewardShipID <= 0)
                {
                    Response.Redirect("/ca/login");
                    //Response.Redirect("/ca/registrationform");
                }
                else
                {
                    Response.Redirect(String.Format("/ca/{0}/login?SSID={1}", StateCode, StewardShipID.ToString()));
                    //Response.Redirect(String.Format("/ca/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
                }
            }
            else if (CountryID == 159)
            {
                Utils.SetCulture("es-MX", "es-MX");
                if (StewardShipID <= 0)
                {
                    Response.Redirect("/mx/login");
                    //Response.Redirect("/mx/registrationform");
                }
                else
                {
                    Response.Redirect(String.Format("/mx/{0}/login?SSID={1}", StateCode, StewardShipID.ToString()));
                    //Response.Redirect(String.Format("/mx/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
                }
            }
            else if (CountryID == 235)
            {
                Utils.SetCulture("en-US", "en-US");
                if (StewardShipID <= 0)
                {
                    Response.Redirect("/us/login");
                    //Response.Redirect("/us/registrationform");
                }


                else
                {
                    Response.Redirect(String.Format("/us/{0}/login?SSID={1}", StateCode, StewardShipID.ToString()));
                    //Response.Redirect(String.Format("/us/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
                }
            }
            else if (CountryID == 116)
            {
                Utils.SetCulture("ja-JP", "ja-JP");
                if (StewardShipID <= 0)
                {
                    Response.Redirect("/ja/login");
                    //Response.Redirect("/ja/registrationform");
                }
                else
                {
                    Response.Redirect(String.Format("/ja/{0}/login?SSID={1}", StateCode, StewardShipID.ToString()));
                    //Response.Redirect(String.Format("/ja/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
                }
            }
            else if (CountryID == 124)
            {
                Utils.SetCulture("ko-KR", "ko-KR");
                if (StewardShipID <= 0)
                {
                    Response.Redirect("/sk/login");
                    //Response.Redirect("/sk/registrationform");
                }
                else
                {
                    Response.Redirect(String.Format("/sk/{0}/login?SSID={1}", StateCode, StewardShipID.ToString()));
                    //Response.Redirect(String.Format("/sk/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
                }
            }
            else if (CountryID == 49)
            {
                Utils.SetCulture("zh-CN", "zh-CN");
                if (StewardShipID <= 0)
                {
                    Response.Redirect("/cn/login");
                    //Response.Redirect("/cn/registrationform");
                }
                else
                {
                    Response.Redirect(String.Format("/cn/{0}/login?SSID={1}", StateCode, StewardShipID.ToString()));
                    //Response.Redirect(String.Format("/cn/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
                }
            }
            else if (CountryID == 14)
            {
                Utils.SetCulture("en-AU", "en-AU");
                if (StewardShipID <= 0)
                {
                    Response.Redirect("/au/login");
                    //Response.Redirect("/au/registrationform");
                }
                else if (StewardShipID > 0)
                {
                    Response.Redirect(String.Format("/au/{0}/login?SSID={1}", StateCode, StewardShipID.ToString()));
                    //Response.Redirect(String.Format("/au/{0}/registration?SSID={1}", StateCode, StewardShipID.ToString()));
                }
            }
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    
    {
        ddlStewardship.Items.Clear();
        if (ddlCountry.SelectedValue != "0")
        {
           // DataSet ds = OrganizationInfo.GetStewardshipByCountryID(Convert.ToInt32(ddlCountry.SelectedValue));

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        ListItem item = new ListItem();
            //        item.Value = dr["OrganizationId"].ToString() + "-" + dr["State"].ToString();
            //        item.Text = dr["StateName"].ToString();
            //        ddlStewardship.Items.Add(item);
            //    }
            //}
            Utils.GetLookUpData<DropDownList>(ref ddlStewardship, LookUps.StewardshipTypes, Convert.ToInt32(ddlCountry.SelectedValue));
            if (ddlStewardship.Items.Count > 1)
                ddlStewardshipRequired.Enabled = true;
            else
                ddlStewardshipRequired.Enabled = false;
        }
      //  ddlStewardship.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));
    }

    private int GetCountryIDByCulture()
    {
        if (HttpContext.Current.Request.Cookies["CultureCookie"] != null)
        {
            string Culture = Convert.ToString(HttpContext.Current.Request.Cookies["CultureCookie"]["UICulture"]);
            int Country = 0;
            switch (Culture.ToLower())
            {
                case "en-us":
                    Country = 235;
                    break;
                case "es-mx":
                    Country = 159;
                    break;
                case "fr-fr":
                    Country = 39;
                    break;
                case "fr-ca":
                    Country = 39;
                    break;
                case "en-ca":
                    Country = 39;
                    break;
                case "ja-jp":
                    Country = 116;
                    break;
                case "ko-kr":
                    Country = 124;
                    break;
                case "en-au":
                    Country = 14;
                    break;
                case "zh-cn":
                    Country = 49;
                    break;
                default:
                    Country = 0;
                    break;
            }
            return Country;
        }
        return 0;
    }
}
