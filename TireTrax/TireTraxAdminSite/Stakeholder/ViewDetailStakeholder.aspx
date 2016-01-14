<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewDetailStakeholder.aspx.cs" Inherits="Stakeholder_ViewDetailStakeholder" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="stv_parent_gridouter">
        <div class="txt-main-had">
            <div class="txt-had-left" style="background: none; line-height: 2;">
                <b>Stakeholders </b>
            </div>
            <div class="stv_txt-had-right">
                <div class="stv-status_dropdown">
                    Status:
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="stv-status_button">Submit</asp:LinkButton>
            </div>
        </div>
<div class="stv_leftOuter">
        <div class="stv_grid-contain-outer topPosition">
            <div class="stv_txt-main-had">
                <div class="stv_txt-had-left" style="background: none;">
                    <b>
                        <%= ResourceMgr.GetMessage("Primary Information")%></b>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("First Name:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrFirstName" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Last Name:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrLastName" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Primary Email:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrprimaryEmail" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Business Phone Number:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrPhoneNumber" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Business Phone Extension:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrPhoneExtension" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Cell Phone Type:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrCellPhoneType" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Cell Phone Number:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrCellPhoneNumber" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Cell Phone Extension:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrCellPhoneExtension" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Cell Text Message:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrCellTextMessage" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Billing Contact:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrBillingContact" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Billing Mail Address:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrBillingMailAddress" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Contact Title:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrContactTitle" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("ZIP Code:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrZipCode" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Fax:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrFax" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Address:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("City:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrCity" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("State:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrState" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Country:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrCountry" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Country Abbreviation:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrCountryAbbreviation" runat="server"></asp:Literal>
                </div>
            </div>
            <br clear="all" />
        </div>

        <div class="stv_grid-contain-outer">
            <div class="stv_txt-main-had">
                <div class="stv_txt-had-left" style="background: none;">
                    <b>
                        <%= ResourceMgr.GetMessage("Client Trade Refrences")%></b>
                </div>
            </div>
            <br clear="all" />
            <asp:Repeater ID="RptrClntInfo" runat="server">
                <ItemTemplate>
                    <div class="stv_divider-outer">
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("Compnay Name")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrClntCompnayName" Text='<%#Eval("CompanyName") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("Country:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrClntCountryName" Text='<%#Eval("CountryName") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("State:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrClntStateName" Text='<%#Eval("StateName") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("City:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrClntCityName" Text='<%#Eval("City") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("ZIP Code:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrClntZipcode" Text='<%#Eval("Zipcode") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("Primary Contact Name:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrClntContactName" Text='<%#Eval("ContactName") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("Primary Contact Phone:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrClntBussinessPhone" Text='<%#Eval("BussinessPhone") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("Primary Contact Email:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrClntOwnerManagerEmail" Text='<%#Eval("OwnerManagerEmail") %>'
                                    runat="server"></asp:Literal>
                            </div>
                        </div>
                        <br clear="all" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <br clear="all" />
        </div>
</div>

<div class="stv_rightOuter">
        <div class="stv_grid-contain-outer topPosition">
            <div class="stv_txt-main-had">
                <div class="stv_txt-had-left" style="background: none;">
                    <b>
                        <%= ResourceMgr.GetMessage("Organization Information")%></b>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Business Name:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrBusinessName" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("DBA Name:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrDBAName" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Organization:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrOrganization" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Business Type:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrBusinessType" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Website:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrwebsite" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Business Address1:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrBusinessAddress1" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Business Address2:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrBusinessAddress2" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block alternate">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Business Phone Type:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="ltrBusinessPhoneType" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="stv_content_block">
                <div class="stv_login-had">
                    <%= ResourceMgr.GetMessage("Business Text Messages:")%>
                </div>
                <div class="stv_login_field">
                    <asp:Literal ID="lrtBusinessTextMessage" runat="server"></asp:Literal>
                </div>
            </div>
            <br clear="all" />
        </div>

        <div class="stv_grid-contain-outer">
            <div class="stv_txt-main-had">
                <div class="stv_txt-had-left" style="background: none;">
                    <b>
                        <%= ResourceMgr.GetMessage("Certifications")%></b>
                </div>
            </div>
            <br clear="all" />
            <asp:Repeater ID="RptrCertificationInfo" runat="server">
                <ItemTemplate>
                    <div class="stv_divider-outer addHeight">
                    <div class="stv_login-had">

                            </div>
                        <div class="stv_content_block">
                            <div class="stv_login_field">
                                <asp:Literal ID="certificationName" Text='<%#Eval("Name") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <br clear="all" />
                        </div>
                </ItemTemplate>
            </asp:Repeater>
            <br clear="all" />
        </div>

        <div class="stv_grid-contain-outer">
            <div class="stv_txt-main-had">
                <div class="stv_txt-had-left" style="background: none;">
                    <b>
                        <%= ResourceMgr.GetMessage("Supplier Trade References")%></b>
                </div>
            </div>
            <br clear="all" />
            <asp:Repeater ID="RptrsupplierInfo" runat="server">
                <ItemTemplate>
                    <div class="stv_divider-outer">
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("Company Name")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrSupplrCompnayName" Text='<%#Eval("CompanyName") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("Country:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrSupplrCountryName" Text='<%#Eval("CountryName") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("State:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrSupplrStateName" Text='<%#Eval("StateName") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("City:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrSupplrCityName" Text='<%#Eval("City") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("ZIP Code")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrSupplrZipcode" Text='<%#Eval("Zipcode") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("Primary Contact Name:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrSupplrContactName" Text='<%#Eval("ContactName") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("Primary Contact Phone:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrSupplrBussinessPhone" Text='<%#Eval("BussinessPhone") %>' runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="stv_content_block">
                            <div class="stv_login-had">
                                <%= ResourceMgr.GetMessage("Primary Contact Email:")%>
                            </div>
                            <div class="stv_login_field">
                                <asp:Literal ID="ltrSupplrOwnerManagerEmail" Text='<%#Eval("OwnerManagerEmail") %>'
                                    runat="server"></asp:Literal>
                            </div>
                        </div>
                        <br clear="all" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <br clear="all" />
        </div>
</div>
    </div>



</asp:Content>

