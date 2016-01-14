using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data.SqlClient;
using System.Data;

public partial class Users_AddUser : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        loginStatus.Visible = false;
        GetPermission(ResourceType.Users, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }

        if (!IsPostBack)
        {
            //if (String.IsNullOrEmpty(Request.QueryString["OrganizationId"]) == true ||
            //    String.IsNullOrEmpty(Request.QueryString["OrganizationTypeId"]) == true ||
            //    String.IsNullOrEmpty(Request.QueryString["UserId"]) == true
            //    )
            //{
            //    Response.Redirect("/dashboard.aspx");
            //}

           
            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStewardship','{0}');",ResourceMgr.GetMessage("Add User")), true);
            //   Utils.GetLookUpData<CheckBoxList>(ref chkboxList, LookUps.RoleName, new SqlParameter[] { new SqlParameter("@OrgTypeID", OrganizationTypeId) });

            if (Request.QueryString["UserId"] != null && Request.QueryString["OrganizationId"] != null && Request.QueryString["OrganizationTypeId"] != null)
            {
                int OrganizationTypeId = Convert.ToInt32(Request.QueryString["OrganizationTypeId"]);
                ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStewardship','{0}');", ResourceMgr.GetMessage("Users")), true);
                Utils.GetLookUpData<RadioButtonList>(ref chkboxList, LookUps.RoleName, new SqlParameter[] { new SqlParameter("@OrgTypeID", OrganizationTypeId) });

                int UserId = Conversion.ParseInt(Request.QueryString["UserId"]);

                UserInfo objUser = new UserInfo(UserId);

                if (objUser == null)
                {
                    Response.Redirect("Users");
                }

                string orgSubType = "";

                if (!string.IsNullOrEmpty(Session["SubTypeId"].ToString()))
                {
                    if (Conversion.ParseInt(Session["SubTypeId"].ToString()) == 4)
                        orgSubType = "Collector";
                    else if (Conversion.ParseInt(Session["SubTypeId"].ToString()) == 5)
                        orgSubType = "Generator";
                    else if (Conversion.ParseInt(Session["SubTypeId"].ToString()) == 6)
                        orgSubType = "Transporter";
                    else if (Conversion.ParseInt(Session["SubTypeId"].ToString()) == 7)
                        orgSubType = "Processor";
                    else if (Conversion.ParseInt(Session["SubTypeId"].ToString()) == 8)
                        orgSubType = "Manufacturer";
                }

                DataSet dt = UserInfo.getRolesByGroupId(OrganizationTypeId);
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    if (!dt.Tables[0].Rows[i][1].ToString().Contains(orgSubType))
                    {
                        dt.Tables[0].Rows[i].Delete();
                    }
                }




                chkboxList.DataSource = dt;
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
                lblLogin.Text = objUser.Login;
                txtEmail.Text = objUser.Email;
                txtEmail.CssClass = "form-control";
                txtFirstName.Text = objUser.FirstName;
                txtMiddleName.Text = objUser.MiddleName;
                txtLastName.Text = objUser.LastName;
                string phoneNumber = objUser.Number;
                if(phoneNumber.ToString().Contains("-"))
                {
                    txtPrimaryContactCellPhone1.Text = phoneNumber.Substring(0, 3);
                    txtPrimaryContactCellPhone2.Text = phoneNumber.Substring(4, 3);
                    txtPrimaryContactCellPhone3.Text = phoneNumber.Substring(8, 4);
                }
                else
                {
                    txtPrimaryContactCellPhone1.Text = phoneNumber.Substring(0, 3);
                    txtPrimaryContactCellPhone2.Text = phoneNumber.Substring(3, 3);
                    txtPrimaryContactCellPhone3.Text = phoneNumber.Substring(6, 4);
                }
              //  txtPhoneNumber.Text = objUser.Number;
            }
            else
            {
                int OrgId = 0;

                if (!string.IsNullOrEmpty(Session["OrganizationTypeID"].ToString()))
                    OrgId = Conversion.ParseInt(Session["OrganizationTypeID"].ToString());
                else
                    OrgId = UserOrganizationRoleId;
                
                Utils.GetLookUpData<RadioButtonList>(ref chkUserRole, LookUps.RoleName, new SqlParameter[] { new SqlParameter("@OrgTypeID", UserOrganizationRoleId) });

                string orgSubType = "";

                if (!string.IsNullOrEmpty(Session["SubTypeId"].ToString()))
                {
                    if (Conversion.ParseInt(Session["SubTypeId"].ToString()) == 4)
                        orgSubType = "Collector";
                    else if (Conversion.ParseInt(Session["SubTypeId"].ToString()) == 5)
                        orgSubType = "Generator";
                    else if (Conversion.ParseInt(Session["SubTypeId"].ToString()) == 6)
                        orgSubType = "Transporter";
                    else if (Conversion.ParseInt(Session["SubTypeId"].ToString()) == 7)
                        orgSubType = "Processor";
                    else if (Conversion.ParseInt(Session["SubTypeId"].ToString()) == 8)
                        orgSubType = "Manufacturer";
                }


                 DataSet dt = UserInfo.getRolesByGroupId(OrgId);
                 for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                 {
                     if (!dt.Tables[0].Rows[i][1].ToString().Contains(orgSubType))
                     {
                         dt.Tables[0].Rows[i].Delete();
                     }
                 }
                
                chkUserRole.DataSource = dt;

                chkUserRole.DataTextField = "RoleName";
                chkUserRole.DataValueField = "RoleId";
                chkUserRole.DataBind();

                pnlEditUser.Visible = false;
                pnlAddUser.Visible = true;
            }
        }
    }
    protected void lnkbtnCancelInventory_Click(object sender, EventArgs e)
    {
        Response.Redirect("Users");

    }
    protected void lnkbtnAddUser_Click(object sender, EventArgs e)
    {

        //loginStatus.Visible = false;
        //if (UserInfo.CheckLoginNameAvailable(txtUserLogin.Text.Trim()) == false)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "LoginError", "ShowLoginErrorMessage();", true);
        //}
        //else
        //{

            UserInfo objUser = new UserInfo();
            objUser.UserId = 0;
            objUser.Login = txtUserLogin.Text.Trim();
            objUser.Pwd = Encryption.Encrypt(txtUserPassword.Text.Trim());
            objUser.Email = txtUserEmail.Text.Trim();
            objUser.FirstName = txtUserFirstName.Text.Trim();
            objUser.MiddleName = txtUserMiddleName.Text.Trim();
            objUser.LastName = txtUserLastName.Text.Trim();
            objUser.DateCreated = DateTime.Now;
            objUser.CreatedByUserId = currentUserInfo.UserId;
            objUser.LanguageId = LanguageId;
            objUser.IsApproved = true;
            objUser.Number = txtphoneNumber1Add.Text + txtphoneNumber2Add.Text + txtphoneNumber3Add.Text;

            string RoleIDs = string.Empty;

            foreach (ListItem item in chkUserRole.Items)
            {
                if (item.Selected)
                {
                    RoleIDs += item.Value + ",";
                }
            }

            RoleIDs = RoleIDs.TrimEnd(',');
            if (!string.IsNullOrEmpty(Session["OrganizationId"].ToString()))
                objUser.UserId = UserInfo.InsertUser(objUser, Conversion.ParseInt(Session["OrganizationId"].ToString()), RoleIDs); 
            else
            objUser.UserId = UserInfo.InsertUser(objUser, UserOrganizationId, RoleIDs);

            if (objUser.UserId > 0)
            {

                if (!string.IsNullOrEmpty(Session["OrganizationId"].ToString()) && !string.IsNullOrEmpty(Session["OrganizationTypeID"].ToString()))
                {
                    string OrgID = Session["OrganizationId"].ToString();
                    string OrgTpID = Session["OrganizationTypeID"].ToString();
                    
                    Session["OrganizationId"] = "";
                    Session["OrganizationTypeID"] = "";

                    Session["OrganizationId"] = "";
                Response.Redirect("Users?p=1" + "&OrganizationId=" + OrgID + "&OrganizationTypeID=" + OrgTpID);
                }
                else
                {
                    Session["OrganizationId"] = "";
                    Session["OrganizationTypeID"] = "";
                    Response.Redirect("Users");
                }
            }
            else
            {

                loginStatus.Visible = true;
                loginStatus.Text = "Given login already exists.";
              
                    ClientScriptManager script = Page.ClientScript;
                    //prevent duplicate script
                    if (!script.IsStartupScriptRegistered(this.GetType(), "HideLabel"))
                    {
                        script.RegisterStartupScript(this.GetType(), "HideLabel",
                        "<script type='text/javascript'>HideLabel('" + loginStatus.ClientID + "')</script>");       
                    }
                

            }



    }

    protected void AtLeastOneContact_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtPrimaryContactCellPhone1.Text != "" && txtPrimaryContactCellPhone2.Text != "" && txtPrimaryContactCellPhone3.Text != "")
            args.IsValid = true;
        else
        {
            args.IsValid = false;
        }
    }


    //protected bool IsGroupValid(string sValidationGroup)
    //{
    //    foreach (BaseValidator validator in Page.Validators)
    //    {
    //        if (validator.ValidationGroup == sValidationGroup)
    //        {
    //            bool fValid = validator.IsValid;
    //            if (fValid)
    //            {
    //                validator.Validate();
    //                fValid = validator.IsValid;
    //                validator.IsValid = true;
    //            }
    //            if (!fValid)
    //                return false;
    //        }

    //    }
    //    return true;
    //}


    protected void lnkbtnAddInventory_Click(object sender, EventArgs e)
    {


        UserInfo objUser = new UserInfo();
        int UserId = Conversion.ParseInt(Request.QueryString["UserId"]);
        int OrganizationId = Conversion.ParseInt(Request.QueryString["OrganizationId"]);
        objUser.UserId = UserId;
        objUser.Pwd = txtPassword.Text.Trim() == "" ? "" : Encryption.Encrypt(txtPassword.Text.Trim());
        objUser.Email = txtEmail.Text.Trim();
        txtEmail.CssClass = "form-control";
        objUser.FirstName = txtFirstName.Text.Trim();
        objUser.MiddleName = txtMiddleName.Text.Trim();
        objUser.LastName = txtLastName.Text.Trim();
        objUser.IsApproved = true;
        objUser.IsActive = true;
        objUser.Number = txtPrimaryContactCellPhone1.Text + txtPrimaryContactCellPhone2.Text + txtPrimaryContactCellPhone3.Text;
        objUser.OrganizationId = OrganizationId;

   
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
            if (!string.IsNullOrEmpty(Session["OrganizationId"].ToString()) && !string.IsNullOrEmpty(Session["OrganizationTypeID"].ToString()))
            {
                string OrgID = Session["OrganizationId"].ToString();
                string OrgTpID = Session["OrganizationTypeID"].ToString();

                Session["OrganizationId"] = "";
                Session["OrganizationTypeID"] = "";

                Session["OrganizationId"] = "";
                Response.Redirect("Users?p=1" + "&OrganizationId=" + OrgID + "&OrganizationTypeID=" + OrgTpID);
            }
            else
            {
                Session["OrganizationId"] = "";
                Session["OrganizationTypeID"] = "";
            Response.Redirect("Users");
            }
        }
           
            //lblUpdateSuccesfully.Text = "Profile Updated Successfully";
            //lblUpdateSuccesfully.Visible = true;
            //Response.Redirect("Users");


        }
    
    public bool myCallback()
    {
        return true;
    }
    protected void dsf_Click(object sender, EventArgs e)
    {

    }
}