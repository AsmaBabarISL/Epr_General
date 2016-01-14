<%@ Page Title="" Language="C#" MasterPageFile="~/skeleton.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Registration_ChangePassword" %>


<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register src="/CommonControls/footer.ascx" tagname="footer" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    <div id="Div1" runat="server" style=" background:url(/images/bg_shadow.png) repeat;width:100%;height:100%;position:fixed;
    z-index:999;top:0;left:0;z-index:99999;display:block;"> 
           <img src="/images/ajax-loader.gif" style="position:fixed; left:0; right:0; top:0; bottom:0; margin:auto;" />
            </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <div id="dvChangePassword" runat="server" class="middle-box loginscreen animated fadeInDown">
                <h1 class="logo-name text-center">
                    <%--<img src="/images/NewTTlogo.png" width="300"/>--%>
                    <img src="img/main-Logo.png" style="height: 75px; width: auto; border-radius: 3px;" alt="Logo"/>
                </h1>
                <div class="login-box">
                    <asp:Label ID="lblStatus" ForeColor="Green" runat="server" Text="Label"></asp:Label>
                    <h3 class="text-center">
                        <%=ResourceMgr.GetMessage("Reset Password")%>
                    </h3>
                    <p>To change your password, enter your new password below.</p>
                    <div class="m-t" role="form">
                        <div class="form-group ">
                            <asp:TextBox ID="txtUserName" placeholder="Username" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtNewPwd" placeholder="Password" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtNewPwd" ValidationGroup="submit" ErrorMessage="*" 
                    ForeColor="#CC0000" CssClass="custom-error"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtRePwd" placeholder="Confirm Password" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtRePwd" ValidationGroup="submit" ErrorMessage="*" 
                    ForeColor="#CC0000" CssClass="custom-error"></asp:RequiredFieldValidator>
                    <br clear="all" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ValidationGroup="submit" ErrorMessage="*Password Mismatch"
                  ControlToValidate="txtRePwd" Operator="Equal" 
                    ControlToCompare="txtNewPwd" ForeColor="#CC0000" ></asp:CompareValidator>
                        </div>

                <cc1:ResourceLinkButton ID="btnContinue" runat="server" CssClass="btn btn-primary block full-width m-b" TextMessage="Continue" ValidationGroup="submit" 
                   CausesValidation="true" onclick="btnContinue_Click"></cc1:ResourceLinkButton>
                        <cc1:ResourceLinkButton ID="ResourceLinkButton1" runat="server" CssClass="btn btn-primary block full-width m-b" TextMessage="Cancel"  CausesValidation="false"
                    onclick="btnCancel_Click"></cc1:ResourceLinkButton>
                    </div>
                </div>
            </div>

        <asp:UpdatePanel ID="upnl" runat="server"  UpdateMode="Conditional" >
        <ContentTemplate>
         <div id="dvthankyou" style="top:-50px; left:285px" class="login-block-outer" runat="server" visible="false">
        <div class="title_block">
            <div class="error_title">
                <%= ResourceMgr.GetMessage("Thank You")%>
            </div>
        </div>
        <div class="error_block_inner thankYou_icon">
            <p>
                <%= ResourceMgr.GetMessage("Thank You, Your application has been received. You will be contacted within 1 business day to confirm the approval.")%></p>
            <li><a href="/default.aspx">
                <%= ResourceMgr.GetMessage("Go back to Home Page")%></a> </li>
        </div>
    </div>
         </ContentTemplate>
        </asp:UpdatePanel>
    
   

</asp:Content>

