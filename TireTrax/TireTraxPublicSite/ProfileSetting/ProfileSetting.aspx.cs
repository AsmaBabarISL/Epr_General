using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class ProfileSetting_ProfileSetting : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetPermission(ResourceType.Profile, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liProfile','{0}');", ResourceMgr.GetMessage("Profile")), true);
        if (!canView)
        {
            Response.Redirect("error");
        }

        if (!IsPostBack)
        {
            rfvPassword.Enabled = false;
            rfvRepeatPassword.Enabled = false;
            //if (String.IsNullOrEmpty(Request.QueryString["OrganizationId"]) == true ||
            //    String.IsNullOrEmpty(Request.QueryString["OrganizationTypeId"]) == true ||
            //    String.IsNullOrEmpty(Request.QueryString["UserId"]) == true
            //    )
            //{
            //    Response.Redirect("/dashboard.aspx");
            //}

            int OrganizationTypeId = UserOrganizationId;
            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liCC');", true);
            //   Utils.GetLookUpData<CheckBoxList>(ref chkboxList, LookUps.RoleName, new SqlParameter[] { new SqlParameter("@OrgTypeID", OrganizationTypeId) });


            UserInfo objUser = new UserInfo(LoginMemberId);

            if (objUser == null)
            {
                Response.Redirect("/dashboard");
            }

            lblLogin.Text = objUser.Login;
            txtEmail.Text = objUser.Email;
            txtFirstName.Text = objUser.FirstName;
            txtMiddleName.Text = objUser.MiddleName;
            txtLastName.Text = objUser.LastName;
            txtPhoneNumber.Text = objUser.Number;
            if (objUser.UserProfileImage != null)
            {
                imgprofile.Src = "data:image/(gif|png|jpeg|jpg);base64," + Convert.ToBase64String(objUser.UserProfileImage);
            }
            else
            {
                imgprofile.Src = "/img/placeholder.png";
            }
            

        }

    }
    protected void lnkbtnCancelInventory_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard");

    }
    protected void lnkbtnAddInventory_Click(object sender, EventArgs e)
    {


        UserInfo objUser = new UserInfo();
        objUser.UserId = LoginMemberId;
        objUser.Pwd = txtPassword.Text.Trim() == "" ? "" : Encryption.Encrypt(txtPassword.Text.Trim());
        objUser.Email = txtEmail.Text.Trim();
        objUser.FirstName = txtFirstName.Text.Trim();
        objUser.MiddleName = txtMiddleName.Text.Trim();
        objUser.LastName = txtLastName.Text.Trim();
        objUser.IsApproved = true;
        objUser.Number = txtPhoneNumber.Text.Trim();
        objUser.OrganizationId = UserOrganizationId;
        if (!string.IsNullOrEmpty(hdnimagePath.Value))
        {
            string pattern = @"data:image/(gif|png|jpeg|jpg);base64,";
            string imgString = Regex.Replace(hdnimagePath.Value, pattern, string.Empty);
            byte[] imageBytes = Convert.FromBase64String(imgString);
            objUser.UserProfileImage = imageBytes;
        }
        

        //foreach (ListItem item in chkboxList.Items)
        //{
        //    if (item.Selected)
        //    {
        //        objUser.RoleId = Convert.ToInt32(item.Value);
        //        break;
        //    }
        //}

        if (UserInfo.UpdateUserProfile(objUser))
        {
            lblUpdateSuccesfully.Text = "Profile Updated Successfully";
            lblUpdateSuccesfully.Visible = true;
            chkUpdatePassword.Checked = false;
            //Response.Redirect("dashboard");

        }
    }
    /*  private void UpdateOrganizationLogo(int orgId)
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
        }*/
    public bool myCallback()
    {
        return true;
    }
}