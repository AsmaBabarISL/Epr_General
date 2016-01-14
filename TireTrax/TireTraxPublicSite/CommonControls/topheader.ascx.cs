using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Configuration;

public partial class CommonControls_topheader : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        
        
        UserInfo userinfoobj = new UserInfo(LoginMemberId);
        lblRoleName.Text = "("+userinfoobj.RoleName+")";
        lblSubRoleName.Text = userinfoobj.SubRoleName;

        UserInfo obj = UserInfo.GetCurrentUserInfo();
        litLoginName.Text = obj.Login;

        //lblRoleName.Text = obj.RoleName;
        //lblSubRoleName.Text = obj.SubRoleName;


        if (obj.LastLoginDate != null && obj.LastLoginDate != DateTime.MinValue)
        {
            //29 October, 2012//11:00am
            litLastLoginDate.Text = String.Format("Last Login on <b>{0}</b>at &nbsp; <b>{1}</b>", obj.LastLoginDate.ToString("dd MMMM, yyyy"), obj.LastLoginDate.ToString("hh:mm tt"));

            litLastLoginNotAvailable.Visible = false;
            litLastLoginDate.Visible = true;
        }
        else
        {
            litLastLoginNotAvailable.Visible = true;
            litLastLoginDate.Visible = false;
        }
        if (!IsPostBack)
        {
           
            lblcompanyname.Text = OrganizationInfo.GetOrgLegalNameByOrgId(UserOrganizationId);
                int stateid = OrganizationInfo.getStateId(obj.OrganizationId);
                try
                {
                    DataSet ds1 = OrganizationInfo.GetStateCodeByStateId(stateid);
                    litRole.Text = ds1.Tables[0].Rows[0]["StateName"].ToString();
                }
                catch { }
                DataTable dt = OrganizationInfo.GetLogoPath(obj.OrganizationId);
           
            if (dt != null && dt.Rows.Count > 0)
            {
                string str = ConfigurationManager.AppSettings["LogoUploadLocation"] + dt.Rows[0]["vchLogoPath"].ToString();
                if (string.IsNullOrEmpty(dt.Rows[0]["vchLogoPath"].ToString()))
                {
                    imgLogo.Visible = false;
                    imgLogoDefault.Visible = true;
                }
                else
                {
                    imgLogo.Visible = true;
                    imgLogoDefault.Visible = false;
                    str = str.Replace("//", "/");
                    imgLogo.ImageUrl = str;
                }
            

              //  litRole.Text = dt.Rows[0]["StateName"].ToString();// +" " + dt.Rows[0]["RoleName"].ToString();

            }
            else
            {
                imgLogo.Visible = false;
                imgLogoDefault.Visible = true;
            }
            DataSet ds = Notifications.getAllNotifications(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId, false, true, 100000, 1);
            int count = ds.Tables[0].Rows.Count;
            if (count == 0) lblnotficationcount.Text = "";
            else lblnotficationcount.Text = count.ToString();
            BasePage objBase = new BasePage();

            ds = UserInfo.GetAllActiveLanguages();

            DataRow dr = ds.Tables[0].Select("LanguageId =" + objBase.LanguageId)[0];
            //litLangauge.Text = Convert.ToString(dr["CountryName"]);
            img.Src = "/images/" + Convert.ToString(dr["Flag"]);
            lvmessagesmain.DataSource = Notifications.getAllNotifications(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId, false, true, 10, 1);
            lvmessagesmain.DataBind();
            
        }
    }
    protected void lvmessagesmain_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
       
                if (e.CommandName == "MarkRead")
                {
                    Notifications objNotifications = new Notifications(Conversion.ParseInt(e.CommandArgument));
                    objNotifications.BitIsReaded = true;
                    objNotifications.DtmDateReaded = DateTime.Now;
                    objNotifications.InsertUpdate();
                }
                else
                    if (e.CommandName == "Delete")
                    {
                        Notifications objNotifications = new Notifications(Conversion.ParseInt(e.CommandArgument));
                        objNotifications.BitIsActive = false;
                        objNotifications.InsertUpdate();
                    }

        lvmessagesmain.DataSource = Notifications.getAllNotifications(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId, false, true, 10, 1);
        lvmessagesmain.DataBind();
    }
   
}