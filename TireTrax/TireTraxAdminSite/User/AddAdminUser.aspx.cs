using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class User_AddAdminUser : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (txtEmail.Text != string.Empty)
        {
            txtEmail.Enabled = false;
        }
        if (!IsPostBack)
        {

            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liPermission','{0}');", ResourceMgr.GetMessage("Admin Users")), true);
            
        }




    }


    protected void lnkbtnAddInventory_Click(object sender, EventArgs e)
    {

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
            objUser.Number = txtPhoneNumber.Text.Trim();

            string RoleIDs = string.Empty;



            UserInfo.InsertUser(objUser, 2, "1", true);

            if (objUser.UserId > 0)
            {
                Response.Redirect("/User/ViewAdminUsers.aspx");
            }
       
       
    }




    protected void lnkbtnCancelInventory_Click(object sender, EventArgs e)
    {
        Response.Redirect("/User/ViewAdminUsers.aspx");
    }
}



