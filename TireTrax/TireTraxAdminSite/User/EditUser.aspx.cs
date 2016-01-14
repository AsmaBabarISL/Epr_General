using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using TireTraxLib;
using System.Configuration;
using System.IO;
using System.Drawing;


public partial class User_EditUser : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStewardship','{0}');", ResourceMgr.GetMessage("Users")), true);
            if (Request.QueryString["UP"] != null)
            {

                if (Request.QueryString["UP"] != null)
                {
                    if (Conversion.ParseInt(Request.QueryString["UP"]) == 1)
                        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStewardship','{0}');", ResourceMgr.GetMessage("Users")), true);
                    else
                        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStewardship','{0}');", ResourceMgr.GetMessage("Users")), true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStewardship','{0}');", ResourceMgr.GetMessage("Users")), true);
                }
            } 
                LoadDetail();
            
        }
    }
    private void LoadDetail()
    {
        int OrganizationTypeId = Convert.ToInt32(Request.QueryString["OrganizationTypeId"]);
        Utils.GetLookUpData<CheckBoxList>(ref chkboxList, LookUps.RoleName, new SqlParameter[] { new SqlParameter("@OrgTypeID", OrganizationTypeId) });
        UserInfo objUser = new UserInfo(Convert.ToInt32(Request.QueryString["UserId"]));
        lblLogin.Text = objUser.Login;
        txtEmail.Text = objUser.Email;
        txtEmail.CssClass = "form-control";
        txtFirstName.Text = objUser.FirstName;
        txtMiddleName.Text = objUser.MiddleName;
        txtLastName.Text = objUser.LastName;
        string phoneNumber = objUser.Number;
        txtPrimaryContactCellPhone1.Text = phoneNumber.Substring(0, 3);
        txtPrimaryContactCellPhone2.Text = phoneNumber.Substring(3, 3);
        txtPrimaryContactCellPhone3.Text = phoneNumber.Substring(6, 4);
        chkActive.Checked = Conversion.ParseBool(objUser.IsActive);
        chkboxList.DataSource = UserInfo.getRolesByGroupId(OrganizationTypeId);
        chkboxList.DataTextField = "RoleName";
        chkboxList.DataValueField = "RoleId";
        chkboxList.DataBind();
        foreach (ListItem item in chkboxList.Items)
        {
            if (item.Value == Convert.ToString(objUser.RoleId))
            {
                item.Selected = true;
                break;
            }
        }
        //if (objUser.UserRolesCommaSeprated != null)
        //{
        //    foreach (ListItem item in chkboxList.Items)
        //    {
        //        foreach (string id in UserInfo.getRolesByGroupId(OrganizationTypeId))
        //        {
        //            if (item.Value == id)
        //            {
        //                item.Selected = true;
        //                break;
        //            }
        //        }
        //    }
        //}
        //if(Convert.ToInt32(Request.QueryString["OrganizationTypeId"]) == 20)
        //     divImage.Visible=true;
    }
    protected void lnkbtnAddInventory_Click(object sender, EventArgs e)
    {
        //if (true == divImage.Visible)
        //{
        //    if (fUpload.HasFile)
        //    {
        //        if (Utils.IsValidExtension(fUpload.FileName))
        //        {
        //            if(fUpload.FileBytes.Length > Convert.ToDouble(ConfigurationManager.AppSettings["MaxfileSize"]))
        //            {
        //                UpdateOrganizationLogo(Convert.ToInt32(Request.QueryString["OrganizationId"]));
        //            }
        //            else
        //            {
        //                lblError.Text = "Maximum file size allowed to upload is 2 MB";
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            lblError.Text = "* Invalid Image File";
        //            return;
        //        }
        //    }
        //}

        UserInfo objUser = new UserInfo();
        objUser.UserId = Convert.ToInt32(Request.QueryString["UserId"]);
        objUser.Pwd = txtPassword.Text.Trim() == "" ? "" : Encryption.Encrypt(txtPassword.Text.Trim());
        objUser.Email = txtEmail.Text.Trim();
        objUser.FirstName = txtFirstName.Text.Trim();
        objUser.MiddleName = txtMiddleName.Text.Trim();
        objUser.LastName = txtLastName.Text.Trim();
        objUser.IsApproved = true;
        objUser.IsActive = chkActive.Checked;
        objUser.Number = txtPrimaryContactCellPhone1.Text.Trim() + txtPrimaryContactCellPhone2.Text.Trim() + txtPrimaryContactCellPhone3.Text.Trim();
        objUser.OrganizationId = Convert.ToInt32(Request.QueryString["OrganizationId"]);

        foreach (ListItem item in chkboxList.Items)
        {
            if (item.Selected)
            {
                objUser.RoleId = Convert.ToInt32(item.Value);
                break;
            }
        }

        if (UserInfo.UpdateUser(objUser))
        {
            if (Request.QueryString["UP"] != null)
            {
                if (Conversion.ParseInt(Request.QueryString["UP"]) == 1)
                    Response.Redirect(String.Format("/Permission/UsersPermission.aspx"));
                else
                    Response.Redirect(String.Format("/User/ViewUser.aspx?p=1&OrganizationId={0}&RoleId={1}&OrganizationTypeID={2}", Convert.ToInt32(Request.QueryString["OrganizationId"]), Convert.ToInt32(Request.QueryString["RoleId"]), Convert.ToInt32(Request.QueryString["OrganizationTypeId"])));
            }
            else
            {
                Response.Redirect(String.Format("/User/ViewUser.aspx?p=1&OrganizationId={0}&RoleId={1}&OrganizationTypeID={2}", Convert.ToInt32(Request.QueryString["OrganizationId"]), Convert.ToInt32(Request.QueryString["RoleId"]), Convert.ToInt32(Request.QueryString["OrganizationTypeId"])));
            }
        }
    }
    private void UpdateOrganizationLogo(int orgId)
    {
        try
        {
            string orgName = OrganizationInfo.GetOrgLegalNameByOrgId(orgId);
            string fileName = string.IsNullOrEmpty(orgName) ? "temp" : orgName + ".jpg";//Server.HtmlEncode(fUpload.FileName);
            string RootPath = ConfigurationManager.AppSettings["LogoUploadLocation"];
            string Completepath = RootPath + orgId + @"\" + fileName;// Server.MapPath(RootPath + orgId + @"\" + fileName);
            if (!Directory.Exists(RootPath + orgId))
                Directory.CreateDirectory(RootPath + orgId);
            if (System.IO.File.Exists(Completepath))
                System.IO.File.Delete(Completepath);
            //fUpload.SaveAs(Completepath);
            string stt = CreateThumbnail(Completepath, orgName);
            OrganizationInfo.UpdateOrganizationLogoPath(orgId, orgId + @"/" + stt);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "RegistrationFormUS.aspx UpdateOrganizationLogo", ex);

        }
    }
    private string CreateThumbnail(string path, string filename)
    {
        System.Drawing.Image myThumbnail150;
        System.Drawing.Image imagesize = System.Drawing.Image.FromFile(path);
        Bitmap bitmapNew = new Bitmap(imagesize);
        if (imagesize.Width < imagesize.Height)
            myThumbnail150 = bitmapNew.GetThumbnailImage(150 * imagesize.Width / imagesize.Height, 150, myCallback, IntPtr.Zero);
        else
            myThumbnail150 = bitmapNew.GetThumbnailImage(150, imagesize.Height * 150 / imagesize.Width, myCallback, IntPtr.Zero);

        myThumbnail150.Save(ConfigurationManager.AppSettings["LogoUploadLocation"] + Request.QueryString["OrganizationId"] + "/" + filename + "-t.png", System.Drawing.Imaging.ImageFormat.Png);
        return filename + "-t.png";
    }
    public bool myCallback()
    {
        return true;
    }

    protected void lnkbtnCancelInventory_Click(object sender, EventArgs e)
    {
        int RoleId = Convert.ToInt32(Request.QueryString["RoleId"]);
        int OrgId = Convert.ToInt32(Request.QueryString["OrganizationId"]);
        int OrgTypeId = Convert.ToInt32(Request.QueryString["OrganizationTypeID"]);
        
        if (Request.QueryString["UP"] != null)
        {
            if (Conversion.ParseInt(Request.QueryString["UP"]) == 1)
                Response.Redirect(String.Format("/Permission/UsersPermission.aspx"));
            else
                Response.Redirect("/User/ViewUser.aspx?p=1&OrganizationId=" + OrgId + "&RoleId=" + RoleId + "&OrganizationTypeID=" + OrgTypeId);
        }
        else
        {
            Response.Redirect("/User/ViewUser.aspx?p=1&OrganizationId=" + OrgId + "&RoleId=" + RoleId + "&OrganizationTypeID=" + OrgTypeId);
        }
    }
}
