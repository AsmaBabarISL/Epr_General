using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class User_AddUser : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (txtEmail.Text != string.Empty)
        {
            txtEmail.Enabled = false;
        }
        if (!IsPostBack)
        {
            //if (String.IsNullOrEmpty(Request.QueryString["OrganizationId"]) == true)
            //{
            //    Response.Redirect("adminStewardship.aspx");
            //}
            //if ((Request.QueryString["OrganizationId"]) == "0")
            //{
            //    lblUserRole.Visible = false;
            //    rsvldcuschkboxList.Enabled = false;
            //    lblAddUser.Text = "Add New Admin User";
            //}

            //if (String.IsNullOrEmpty(Request.QueryString["OrganizationTypeId"]) == true)
            //{
            //    Response.Redirect("adminStewardship.aspx");

            ////}
            ////else
            ////{
            //    rsvldcuschkboxList.Enabled = true;
            //    lblUserRole.Visible = true;
                int OrganizationTypeId = Convert.ToInt32(Request.QueryString["OrganizationTypeId"]);
                ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStewardship','{0}');", ResourceMgr.GetMessage("Users")), true);
                Utils.GetLookUpData<CheckBoxList>(ref chkboxList, LookUps.RoleName, OrganizationTypeId);
            //}
            }
    }

    protected void lnkbtnAddInventory_Click(object sender, EventArgs e)
    {

        //if (UserInfo.CheckLoginNameAvailable(txtLogin.Text.Trim()) == false)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "LoginError", "ShowLoginErrorMessage();", true);
        //}
        //else
        //{

        //if ((Request.QueryString["OrganizationId"]) == "0")
        //{
        //    UserInfo objUser = new UserInfo();
        //    objUser.UserId = 0;
        //    objUser.Login = Utils.CleanHTML(txtLogin.Text.Trim());
        //    objUser.Pwd = Encryption.Encrypt(txtPassword.Text.Trim());
        //    objUser.Email = txtEmail.Text.Trim();
        //    objUser.FirstName = txtFirstName.Text.Trim();
        //    objUser.MiddleName = txtMiddleName.Text.Trim();
        //    objUser.LastName = txtLastName.Text.Trim();
        //    objUser.DateCreated = DateTime.Now;
        //    objUser.CreatedByUserId = currentUserInfo.UserId;
        //    objUser.LanguageId = LanguageId;
        //    objUser.IsApproved = true;
        //    objUser.Number = txtPhoneNumber.Text.Trim();

        //    string RoleIDs = string.Empty;

         

        //    UserInfo.InsertUser(objUser, 2, "1",true);

        //    if (objUser.UserId > 0)
        //    {
        //        Response.Redirect("/User/ViewAdminUsers.aspx");
        //    }
        //}
        //else
        //{

            UserInfo objUser = new UserInfo();
            objUser.UserId = 0;
            objUser.Login = Utils.CleanHTML(txtLogin.Text.Trim());
            objUser.Pwd = Encryption.Encrypt(txtPassword.Text.Trim());
            objUser.Email = txtEmail.Text.Trim();
            objUser.FirstName = txtFirstName.Text.Trim();
            objUser.MiddleName = txtMiddleName.Text.Trim();
            objUser.LastName = txtLastName.Text.Trim();
            objUser.DateCreated = DateTime.Now;
            objUser.CreatedByUserId = currentUserInfo.UserId;
            objUser.LanguageId = LanguageId;
            objUser.IsApproved = true;
            objUser.Number = txtPrimaryContactCellPhone1.Text.Trim() + txtPrimaryContactCellPhone2.Text.Trim() + txtPrimaryContactCellPhone3.Text.Trim();
    

            string RoleIDs = string.Empty;

            foreach (ListItem item in chkboxList.Items)
            {
                if (item.Selected)
                {
                    RoleIDs += item.Value + ",";
                }
            }

            RoleIDs = RoleIDs.TrimEnd(',');

            UserInfo.InsertUser(objUser, Convert.ToInt32(Request.QueryString["OrganizationId"]), RoleIDs);

            if (objUser.UserId > 0)
            {
                Response.Redirect(String.Format("/User/ViewUser.aspx?p=1&OrganizationId={0}&RoleId={1}&OrganizationTypeID={2}", Convert.ToInt32(Request.QueryString["OrganizationId"]), Convert.ToInt32(Request.QueryString["RoleId"]), Convert.ToInt32(Request.QueryString["OrganizationTypeId"])));
            }
       // }
    }
    protected void lnkbtnCancelInventory_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("/User/ViewUser.aspx?p=1&OrganizationId={0}&RoleId={1}&OrganizationTypeID={2}", Convert.ToInt32(Request.QueryString["OrganizationId"]), Convert.ToInt32(Request.QueryString["RoleId"]), Convert.ToInt32(Request.QueryString["OrganizationTypeId"])));
    }
}