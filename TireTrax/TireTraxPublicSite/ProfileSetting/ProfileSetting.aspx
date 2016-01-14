<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ProfileSetting.aspx.cs" Inherits="ProfileSetting_ProfileSetting" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mask.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mask.min.js" type="text/javascript"></script>
    <script type="text/javascript">


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


        jQuery(document).ready(function ($) {
            $("#<%=txtPhoneNumber.ClientID%>").mask("999-999-9999");

        });

        var profileimage=function (input) {
            if (document.getElementById('<%= inputprofileimg.ClientID %>').files.length > 0) {
                var url = input.value;
                    var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
                    if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
                        var reader = new FileReader();

                        reader.onload = function (e) {
                            $("#<%=imgprofile.ClientID%>").attr('src', e.target.result);
                            $('.profileimg').prop("width", '85px');
                            $('.profileimg').prop("height", '85px');
                            $("#<%=hdnimagePath.ClientID%>").val(e.target.result);

                        }
                        reader.readAsDataURL(input.files[0]);
                    }
                    else {
                        $("#<%=imgprofile.ClientID%>").prop("src", "/img/placeholder.png");
                        $("#<%=hdnimagePath.ClientID%>").val('');
                    }
                       

                    }
        }
        
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField runat="server" ID="hdnimagePath" />
    <!-- Start Login Container ------------------------------------------------------------------------------------>
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">

                    <h5><%= ResourceMgr.GetMessage("Edit User")%> </h5>
                    
                </div>

                <div class="ibox-content" style="display: block;">
                    <div role="form" class="row search-filter" id="">
                        

                        <div class="col-md-6 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblUpdateSuccesfully" runat="server" Visible="false" CssClass="alert-success"></asp:Label><br />
                                <label><%= ResourceMgr.GetMessage("Login")%></label>
                                <asp:Label ID="lblLogin" runat="server" Text="LoginName" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label><%= ResourceMgr.GetMessage("First Name")%></label>
                                <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvFirstName" ValidationGroup="AddadminUserValidationGroup"
                                    CssClass="custom-error" runat="server" ErrorText="Enter First Name" ControlToValidate="txtFirstName"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label><%= ResourceMgr.GetMessage("Middle Name")%></label>
                                <asp:TextBox ID="txtMiddleName" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label><%= ResourceMgr.GetMessage("Last Name")%></label>
                                <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvLastName" ValidationGroup="AddadminUserValidationGroup"
                                    CssClass="custom-error" runat="server" ErrorText="Enter Last Name" ControlToValidate="txtLastName"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label><%= ResourceMgr.GetMessage("Email")%></label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled ="false"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvEmail" ValidationGroup="AddadminUserValidationGroup"
                                    CssClass="custom-error" runat="server" ErrorText="Enter Email" ControlToValidate="txtEmail"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                <cc1:ResourceRegularExpressionValidator ID="RegularExpressionValidator1" CssClass="custom-error" runat="server" ControlToValidate="txtEmail"
                                    ErrorText="Enter Valid Email" ValidationGroup="AddadminUserValidationGroup"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                            </div>
                            <div class="form-group">
                                <label><%= ResourceMgr.GetMessage("Phone Number")%></label>
                                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" MaxLength ="12" placeholder="000-000-0000" ></asp:TextBox>
                                
                                <cc1:ResourceRequiredFieldValidator ID="rfvExt" ControlToValidate="txtPhoneNumber" runat="server"
                                    ErrorText="Please enter Phone" CssClass="custom-error" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>
                                <cc1:ResourceRegularExpressionValidator ID="revPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber"
                                    ErrorText="Please enter a valid Phone Number. e.g; 000-000-0000" CssClass="custom-error" ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$"
                                    ValidationGroup="AddadminUserValidationGroup" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                
                            </div>
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnAddInventory" runat="server" ValidationGroup="AddadminUserValidationGroup"
                                    CausesValidation="true" CssClass="btn btn-primary btn-sm font-bold" OnClick="lnkbtnAddInventory_Click"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnCancelInventory" runat="server" CausesValidation="false" OnClick="lnkbtnCancelInventory_Click"
                                    CssClass="btn btn-white btn-sm font-bold"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-6 col-sm-6" style="margin-top: 5px;">
                            <div class="form-group" style="margin-left: 20px;">
                                <label>&nbsp;</label>
                                <label class="checkbox">
                                    <asp:CheckBox ID="chkUpdatePassword" runat="server" onclick="SetPasswordField(this.checked);" AutoPostBack="false" />
                                    <%= ResourceMgr.GetMessage("Update Password")%>
                                </label>
                            </div>
                            <div id="dvPassword" style="display: none; clear: both;margin-top: 32px;">
                                <div class="form-group">
                                    <label><%= ResourceMgr.GetMessage("Password:")%></label>
                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvPassword" ValidationGroup="AddadminUserValidationGroup"
                                        CssClass="custom-error" runat="server" ErrorText="Enter Password" ControlToValidate="txtPassword"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                </div>
                                <div class="form-group">
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
                            <div class="form-group">
                                <div class="col-md-4">
                                    <img runat="server" src="/img/placeholder.png" id="imgprofile" width="85" height="85" />
                                    <label><%= ResourceMgr.GetMessage("Profile Image")%></label>
                                </div>
                                <input type="file" id="inputprofileimg" onchange="profileimage(this);" runat="server" class="form-control profileimg"/>
                            </div>
                        </div>



                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>

