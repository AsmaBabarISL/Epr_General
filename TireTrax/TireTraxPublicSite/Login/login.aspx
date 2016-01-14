<%@ Page Title="" Language="C#" MasterPageFile="~/skeleton.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Login_login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="loginColumns animated fadeInDown text-center">
       <h1 class="logo-name"><img src="/img/main-Logo.png" id="logoimg" runat="server" style="height: 75px; width: auto; border-radius: 3px;" alt="Logo" /></h1> 
        <div class="row">
            <div class="col-md-3">  <%-- it was col-md-6 --%>
              <%--  <h3 class="font-bold m-t-md"><%= ResourceMgr.GetMessage("EPR Technology Solutions Benefits Include")%></h3>
                <p><%= ResourceMgr.GetMessage("EPR Technology Solutions is the complete solution for an Extended Product Responsibility Scrap Tire Stewardship's recycling operations and management challenges.")%></p>

                <p>
                    <%= ResourceMgr.GetMessage("100% Green Scrap Tire Recycling becomes a practical reality.")%>
                </p>
                <p>
                    <%= ResourceMgr.GetMessage("Cradle-to-Reincarnation complete accountability for each individual Scrap Tire.")%>
                </p>
                <p>
                    <%= ResourceMgr.GetMessage("Market-based and Self-sustaining Stewardship Funding and Earnings platform.")%>
                </p>
                <p>
                    <%= ResourceMgr.GetMessage("And much...much more!")%>
                </p>


                <asp:LinkButton ID="readMore" runat="server" CssClass="btn btn-primary btn-sm m-b" href="#readPopup" data-toggle="modal"><%= ResourceMgr.GetMessage("Read More")%></asp:LinkButton>--%>
            </div>
            <div class="col-md-6 text-center">
                <div class="ibox-content">
                    <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnLogin">
                        <h3><asp:Label runat="server" ID="lblState"></asp:Label> <%= ResourceMgr.GetMessage("Tire Stewardship")%></h3>
                        <div class="form-group">
                            <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control" ValidationGroup="Login" placeholder="Login"></asp:TextBox>


                            <cc1:ResourceRequiredFieldValidator ID="emailRequired1" ControlToValidate="txtLogin" Display="Dynamic"
                                runat="server" ErrorText="Please Enter Login" CssClass="custom-error" ValidationGroup="Login"></cc1:ResourceRequiredFieldValidator>
                            <asp:Label CssClass="custom-error" ID="lblError" runat="server"></asp:Label>

                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"
                                ValidationGroup="Login" placeholder="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="passwordRequired1" ControlToValidate="txtPassword"
                                runat="server" CssClass="custom-error" ErrorMessage="Please Enter Password" ValidationGroup="Login"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvPwd" runat="server" ControlToValidate="txtPassword" ErrorMessage="<, > chars not allowed in Password"
                                ValidationGroup="Login" CssClass="custom-error" ClientValidationFunction="checkForTag"></asp:CustomValidator>
                        </div>
                        <div class="form-group rememberMe">
                            <label class="checkbox" style="margin-left: -90px;">
                                <asp:CheckBox ID="chkremember" runat="server" />
                                <%= ResourceMgr.GetMessage("Remember me")%>
                            </label>
                            <a href="forgotPassword?SSID=<%=Request.QueryString["SSID"] %> " class="pull-right">
                                <%= ResourceMgr.GetMessage("Forgot password?")%></a>
                        </div>
                        <cc1:ResourceButton ID="btnLogin" runat="server" TextMessage="Login" CssClass="btn btn-primary block full-width m-b"
                            ValidationGroup="Login" OnClick="btnLogin_Click" />
                    </asp:Panel>

                    <cc1:ResourceButton ID="registerNow" runat="server" TextMessage="Register Now" CssClass="btn btn-sm btn-white btn-block"
                        OnClick="registerNow_Click" />
                    <%--<p class="m-t">
                        <small><%= ResourceMgr.GetMessage("Real World Solutions for Global Resource Recycling")%></small>
                    </p>--%>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-12 text-center">
                <%= ResourceMgr.GetMessage("*Existing Stewardship Stakeholders are granted Pre-Approved status and Instant Access to their new EPR Technology Solutions services by submitting an abbreviated online information update application.")%>
            </div>
        </div>
        <!-- end #mainContent -->
    </div>


    <div class="modal inmodal" id="readPopup" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title"><%= ResourceMgr.GetMessage("Scrap Tire Stewardships")%></h4>
                </div>
                <div class="modal-body">
                    <p>
                        <%= ResourceMgr.GetMessage("Mobile access to Stakeholder Accounts")%>
                    </p>
                    <p>
                        <%= ResourceMgr.GetMessage("\"Quick-Pay\" weekly compensation") %>
                    </p>
                    <p>
                        <%= ResourceMgr.GetMessage("Integrated Stewardship Compliance")%>
                    </p>
                    <p>
                        <%= ResourceMgr.GetMessage("Integrated Government Compliance")%>
                    </p>
                    <p><%= ResourceMgr.GetMessage("The EPR Technology Solutions Cradle-to-Reincarnation identification and tracking capabilities coupled with its complete systemic accountability provide the initial framework necessary to insure the successful transition from existing scrap tire diversion projects to 100% Green product utilization.")%></p>
                    <p><%= ResourceMgr.GetMessage("The EPR Technology Solutions Stewardship platform offers a market based concept that fully utilizes the existing tire industry's infrastructures.")%></p>
                    <p><%= ResourceMgr.GetMessage("EPR Technology Solutions provides the unified and incentivised operations and management solutions for businesses and governments to welcome the incorporation of a 100% Green Recycling initiative.")%></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal"><%= ResourceMgr.GetMessage("Close")%></button>
                </div>
            </div>
        </div>
    </div>

    <div class="ajaxModal-popup inmodal" id="dvProducts" runat="server" visible="false">
        <div class="ajaxModal-body animated bounceInRight">
            <div class="modal-header">
                <h4 class="modal-title">
                    <%= ResourceMgr.GetMessage("Product Types")%>
                </h4>
                <p>Kindly Select your product</p>
            </div>

            <div class="modal-body">
                <asp:RadioButtonList runat="server" ID="rblProducts" CssClass="radio radio-inline" DataTextField="PCategoryName" DataValueField="PCategoryType"></asp:RadioButtonList>
                <asp:RequiredFieldValidator runat="server" ID="rgvProduct" ControlToValidate="rblProducts" ErrorMessage="Please select any one product" CssClass="custom-error" ValidationGroup="ProductCategory"></asp:RequiredFieldValidator>
            </div>
            <div class="modal-footer">
                <cc1:ResourceLinkButton runat="server" ID="lnkContinue" CssClass="btn btn-primary" OnClick="lnkContinue_Click" ValidationGroup="ProductCategory"><%=ResourceMgr.GetMessage("Continue") %></cc1:ResourceLinkButton>
            </div>

        </div>
    </div>

</asp:Content>

