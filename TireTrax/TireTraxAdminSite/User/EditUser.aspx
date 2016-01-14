<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true"
    CodeFile="EditUser.aspx.cs" Inherits="User_EditUser" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            if (typeof (evt) != "undefined") {
                var charCode = (evt.which) ? evt.which : evt.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
        function ClientValidateUserRole(src, args) {
            if ($("input:checked", $("#<%=chkboxList.ClientID%>")).length > 0) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        $(function () {
            $("input[type=checkbox]", $("#<%=chkboxList.ClientID%>")).each(function () {
                $(this).bind("click", function () {
                    $("input:checked", $("#<%=chkboxList.ClientID%>")).not(this).removeAttr("checked");
                });
            });

            SetPasswordField(false);
        });

        function SetPasswordField(obj) {
            if (obj) {
                $("#dvPassword").slideDown();
            }
            else {
                $("#dvPassword").slideUp();
            }
            ValidatorEnable(document.getElementById('<%=rfvPassword.ClientID%>'), obj);
            ValidatorEnable(document.getElementById('<%=rfvRepeatPassword.ClientID%>'), obj);
            if (obj) {
                $("#<%=rfvPassword.ClientID%>").hide();
                $("#<%=rfvRepeatPassword.ClientID%>").hide();
            }
        }

        function ShowLoginErrorMessage() {
            $("#LoginNameExists").show();
        }

        function HideLoginErrorMessage() {
            $("#LoginNameExists").hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= ResourceMgr.GetMessage("Edit User")%> </h5>
                </div>

                <div class="ibox-content" style="display: block;">
                    <div role="form" class="row" id="">
<div>
                        <div class="form-group col-md-12">
                             <label><%= ResourceMgr.GetMessage("Login:")%></label>
                             <asp:Label ID="lblLogin" runat="server"></asp:Label>
                        </div>

                        <div class="row">
                            <div class="col-md-4 col-sm-6 col-lg-3" >
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkUpdatePassword" runat="server" onclick="SetPasswordField(this.checked);" CssClass="m-l-md" />
                                        <label class="checkbox-inline p0" > <%= ResourceMgr.GetMessage("Update Password")%></label>
                                    </div>
                                    <div id="dvPassword" style="display: none; clear: both;">

                                        <div class="form-group col-md-12">
                                            <label><%= ResourceMgr.GetMessage("Password:")%> </label>
                                            <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvPassword" ValidationGroup="AddadminUserValidationGroup"
                                                CssClass="custom-error" runat="server" ErrorText="Enter Password" ControlToValidate="txtPassword"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-md-12">
                                            <label><%= ResourceMgr.GetMessage("Re Enter Password:")%></label>
                                            <asp:TextBox ID="txtRepeatPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvRepeatPassword" ValidationGroup="AddadminUserValidationGroup"
                                                CssClass="custom-error" runat="server" ErrorText="Re Enter Password"
                                                ControlToValidate="txtRepeatPassword" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                            <cc1:ResourceCompareValidator ID="cvPassword" ValidationGroup="AddadminUserValidationGroup"
                                                CssClass="custom-error" runat="server" ErrorText="Password does not match" Display="Dynamic"
                                                ControlToValidate="txtRepeatPassword" ControlToCompare="txtPassword" Type="String"></cc1:ResourceCompareValidator>
                                        </div>

                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("First Name:")%> </label>
                                        <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfvFirstName" ValidationGroup="AddadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Enter First Name" ControlToValidate="txtFirstName"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Middle Name:")%></label>
                                        <asp:TextBox ID="txtMiddleName" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Last Name:")%> </label>
                                        <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfvLastName" ValidationGroup="AddadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Enter Last Name" ControlToValidate="txtLastName"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Email:")%></label>
                                        <asp:TextBox ID="txtEmail" runat="server" Enabled="false" class="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfvEmail" ValidationGroup="AddadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Enter Email" ControlToValidate="txtEmail"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        <cc1:ResourceRegularExpressionValidator ID="RegularExpressionValidator1" CssClass="custom-error" runat="server" ControlToValidate="txtEmail"
                                            ErrorText="Enter Valid Email" ValidationGroup="AddadminUserValidationGroup"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                    </div>
                                    <div class="form-group  col-sm-12">
                                        <label><%= ResourceMgr.GetMessage("Phone Number:")%></label>
                                        <div class="row">
                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                <asp:TextBox ID="txtPrimaryContactCellPhone1" runat="server" CssClass="form-control text-center" MaxLength="3" WaterMarkText="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                  <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator2" ValidationGroup=""
                                            CssClass="custom-error" runat="server" ErrorText="*" ControlToValidate="txtPrimaryContactCellPhone1"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        <cc1:ResourceRequiredFieldValidator  ID="rfvExt" ControlToValidate="txtPrimaryContactCellPhone1" runat="server"
                                        ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>   
                                            </div>
                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                <asp:TextBox ID="txtPrimaryContactCellPhone2" runat="server" CssClass="form-control text-center" MaxLength="3" WaterMarkText="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator3" ValidationGroup="AddUserG"
                                            CssClass="custom-error" runat="server" ErrorText="*" ControlToValidate="txtPrimaryContactCellPhone2"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                         <cc1:ResourceRequiredFieldValidator  ID="ResourceRequiredFieldValidator1" ControlToValidate="txtPrimaryContactCellPhone2" runat="server"
                                        ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>  

                                            </div>
                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                <asp:TextBox ID="txtPrimaryContactCellPhone3" runat="server" CssClass="form-control text-center" MaxLength="4" WaterMarkText="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                               <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator4" ValidationGroup="AddUserG"
                                            CssClass="custom-error" runat="server" ErrorText="*" ControlToValidate="txtPrimaryContactCellPhone3"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                            <cc1:ResourceRequiredFieldValidator  ID="ResourceRequiredFieldValidator9" ControlToValidate="txtPrimaryContactCellPhone3" runat="server"
                                        ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>  
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                         <label><%= ResourceMgr.GetMessage("Active:")%></label>
                                          <asp:CheckBox ID="chkActive" runat="server" Text="" />
                                    </div>
                            </div>
                            <div class="col-md-6 col-sm-6">
                           <div class="form-group col-md-12">
                                            <label><%= ResourceMgr.GetMessage("User Role:")%></label>
                               <div class="col-md-12">
                                            <asp:CheckBoxList ID="chkboxList" runat="server" CellPadding="5"
                                                Style="width: 100%;" RepeatColumns="1" RepeatDirection="Horizontal" >
                                            </asp:CheckBoxList><br />
                                            <cc1:ResourceCustomValidator ID="rsvldcuschkboxList" runat="server" CssClass="custom-error"
                                                 Display="Dynamic" ClientValidationFunction="ClientValidateUserRole" ErrorText="Please select Role" 
                                                ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceCustomValidator></div>
                                        </div>
                             
                                </div>
                        </div>
                        <div class="form-group col-md-12 mb0">
                                <asp:LinkButton ID="lnkbtnAddInventory" runat="server" ValidationGroup="AddadminUserValidationGroup"
                                    CausesValidation="true" CssClass="btn btn-primary btn-sm font-bold" OnClientClick="HideLoginErrorMessage();" OnClick="lnkbtnAddInventory_Click"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnCancelInventory" runat="server" CausesValidation="false" OnClick="lnkbtnCancelInventory_Click"
                                    CssClass="btn btn-white btn-sm font-bold"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                            </div>
    </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
