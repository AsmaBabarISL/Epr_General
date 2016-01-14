using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Configuration;
using System.IO;
using System.Drawing;

public partial class Settings_LogoSetting_UploadOrgLogo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetPermission(ResourceType.LogoSetting, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        if (!IsPostBack)
            LoadGrid();
        if (User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("/");
        }
        //ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('Logo Settings');", true);
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liLogoSettings','{0}');", ResourceMgr.GetMessage("Logo Settings")), true);

        lblError.Visible = false;
    }

    private void LoadGrid()
    {
        try
        {
            UserInfo user = UserInfo.GetCurrentUserInfo();
            gvImage.DataSource = OrganizationInfo.GetOrganizationLogo(user.OrganizationId, Convert.ToInt32(LookupsManagement.LookupType.Logo_Image));
            gvImage.DataBind();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "logo-setting.aspx LoadGrid", ex);
        }
    }
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard");
    }
    protected void upload_Click(object sender, EventArgs e)
    {
        try
        {

            if (fUpload.HasFile)
            {
                if (Utils.IsValidExtension(fUpload.FileName))
                {
                    if (fUpload.FileBytes.Length > Convert.ToDouble(ConfigurationManager.AppSettings["MaxfileSize"]))
                    {
                        UserInfo user = UserInfo.GetCurrentUserInfo();
                        if (!UpdateOrganizationLogo(user.OrganizationId))
                            return;
                        LoadGrid();
                        txtFileName.Text = "";
                        //lblError.ForeColor = System.Drawing.Color.Green;
                        //lblError.Text = "Logo is successfully uploaded.";
                        //lblError.Visible = true;
                        Response.Redirect("logosetting", false);
                        int i = 0;
                    }
                    else
                    {
;
                        lblError.Text = "Maximum file size allowed to upload is 2 MB";
                        lblError.Visible = true;
                        return;
                    }
                }
                else
                {

                    lblError.Text = "* Invalid Image File";
                    lblError.Visible = true;
                    return;
                }
            }

            else
            {
                lblError.Text = "* Please Upload Image File";
                lblError.Visible = true;
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "logo-setting.aspx upload_Click", ex);
        }

    }
    private bool UpdateOrganizationLogo(int orgId)
    {
        try
        {

            string fileName1 = Server.HtmlEncode(fUpload.FileName);
            string orgName = OrganizationInfo.GetOrgLegalNameByOrgId(orgId);
            string fileName = string.IsNullOrEmpty(txtFileName.Text) ? "temp" : txtFileName.Text;
            fileName += Guid.NewGuid().ToString();
            fileName +=  ".jpg";
            string RootPath = ConfigurationManager.AppSettings["LogoUploadLocation"];
            string Completepath = Server.MapPath(RootPath + orgId + @"\" + fileName);
            if (!Directory.Exists(Server.MapPath(RootPath + orgId)))
                Directory.CreateDirectory(Server.MapPath(RootPath + orgId));
            if (System.IO.File.Exists(Completepath))
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "File name exists";
                lblError.Visible = true;
                return false;
            }
            fUpload.SaveAs(Completepath);
            string path = CreateThumbnail(Completepath, txtFileName.Text, orgId);
            OrganizationInfo.UpdateOrganizationLogoPath(orgId, orgId + "/" + path);
            OrganizationInfo.InsertUpdateOrganizationImage(0, orgId, txtFileName.Text, orgId + @"\" + fileName, orgId + "/" + path, true, false, Convert.ToInt32(LookupsManagement.LookupType.Logo_Image));

            return true;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "logo-setting.aspx UpdateOrganizationLogo", ex);
            return false;
        }
    }
    private string CreateThumbnail(string path, string filename, int orgId)
    {
        try
        {
            System.Drawing.Image myThumbnail150;
            System.Drawing.Image imagesize = System.Drawing.Image.FromFile(path);
            Bitmap bitmapNew = new Bitmap(imagesize);
            if (imagesize.Width < imagesize.Height)
                myThumbnail150 = bitmapNew.GetThumbnailImage(100 * imagesize.Width / imagesize.Height, 100, myCallback, IntPtr.Zero);
            else
                myThumbnail150 = bitmapNew.GetThumbnailImage(100, imagesize.Height * 100 / imagesize.Width, myCallback, IntPtr.Zero);
            myThumbnail150.Save(Server.MapPath(ConfigurationManager.AppSettings["LogoUploadLocation"] + orgId.ToString() + "/" + filename + "-t.png"), System.Drawing.Imaging.ImageFormat.Png);
            return filename + "-t.png";
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "logo-setting.aspx CreateThumbnail", ex);
            return null;
        }
    }
    public bool myCallback()
    {
        return true;
    }


    protected void gvImage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton imgSt = (LinkButton)e.Row.FindControl("imgStatus");
                bool bitstatus = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitImage"));

                if (bitstatus == true)
                {
                    imgSt.ToolTip = "Active";
                    imgSt.CssClass = "badge badge-primary";
                    imgSt.Text = "Active";
                }
                else
                {
                    imgSt.CssClass = "badge badge-danger";
                    imgSt.ToolTip = "In active";
                    imgSt.Text = "In Active";
                }
                
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Generic Error", ex);

        }

    }

    protected void gvImage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Status")
        {
            LinkButton imgStatus = (LinkButton)e.CommandSource;    // the button
            GridViewRow grRow = (GridViewRow)imgStatus.Parent.Parent;  // the row
            GridView grimage = (GridView)sender; // the gridview
            string ID = grimage.DataKeys[grRow.RowIndex].Value.ToString();
            OrganizationInfo.ActiveDeactiveOrgImage(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(ID), 64);
            Response.Redirect("logosetting");
            lblError.Visible = false;

            LoadGrid();
        }
        if (e.CommandName == "Delete")
        {
            OrganizationInfo.DeleteOrganizationLogo(Convert.ToInt32(e.CommandArgument));

            lblError.Visible = false;
            Response.Redirect("logosetting");
            LoadGrid();
        }
    }



    protected void gvImage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvImage_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
}