<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/skeleton.Master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="CommonControls/footer.ascx" TagName="footer" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function removeLanguageMenu() {
            var d = document.getElementById("languageMenu");
            d.className = "ubltools";
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="middle-box text-center loginscreen  animated fadeInDown">
        <h1 class="logo-name">
            <%--<img src="/images/NewTTlogo.png" width="300" />--%>
           <%-- <img src="//res.cloudinary.com/hrscywv4p/image/upload/c_limit,f_auto,h_1440,q_90,w_720/v1/97956/8548b84fd804400bb5edc97213b62da7_kgeuuo.png"  />--%>
            <img src="img/main-Logo.png" style="height: 95px; width: auto; border-radius: 3px;" alt="Logo"/>
        </h1>
        
        <div class="login-box">
            <h3><%= ResourceMgr.GetMessage("Welcome to Product Loop") %> </h3>

            <p><%= TireTraxLib.ResourceMgr.GetMessage("Select Country and Stewardship to continue")%></p>

            <div class="m-t" role="form">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div id="Div1" runat="server">
                            <img src="/images/ajax-loader.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" required="required">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="ddlCountryRequired" runat="server" ControlToValidate="ddlCountry" InitialValue="0"
                                ForeColor="red" ErrorMessage="Please select Country"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlStewardship" runat="server" CssClass="form-control" required="required"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="ddlStewardshipRequired" runat="server" ControlToValidate="ddlStewardship" InitialValue="0"
                                ForeColor="red" ErrorMessage="Please select Stewardship"></asp:RequiredFieldValidator>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <cc1:ResourceLinkButton ID="ResourceLinkButton1" runat="server" CssClass="btn btn-primary block full-width m-b" OnClick="continue_Click">
                    <%=ResourceMgr.GetMessage("Continue")%>
                </cc1:ResourceLinkButton>
                <div style="display: none;">
                    <div class="login-content_block">
                        <div class="basic_login-had"><%=TireTraxLib.ResourceMgr.GetMessage("Current OTS Stakeholder?")%> </div>
                        <div class="login_field">
                            <%=TireTraxLib.ResourceMgr.GetMessage("Yes")%> &nbsp; 
                <asp:CheckBox ID="ots" runat="server" CssClass="box"
                    onclick="if (this.checked) this.parentElement.nextElementSibling.children.item(0).checked = false; else return false;" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <%=TireTraxLib.ResourceMgr.GetMessage("No")%> &nbsp;
                    <asp:CheckBox ID="otsno" runat="server" Checked="true" CssClass="box"
                        onclick="if (this.checked) this.parentElement.previousElementSibling.children.item(0).checked = false; else return false;" />
                        </div>
                    </div>
                    <div class="login-content_block">
                        <div class="basic_login-had"><%=TireTraxLib.ResourceMgr.GetMessage("Enter Stakeholder ID")%> </div>
                        <div class="login_field">
                            <asp:TextBox ID="txtstakeholder" runat="server" CssClass="field"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="txtstakeholderRequired" runat="server" Enabled="false"
                                ControlToValidate="txtstakeholder" ErrorMessage="*" CssClass="error-validator"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="dvLoading" style="position: absolute; display: none;">
        <img src="~/images/lightbox-ico-loading.gif" alt="Loading" />
    </div>

</asp:Content>
