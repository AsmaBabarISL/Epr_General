<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true"
    CodeFile="ViewCreditCard.aspx.cs" Inherits="Settings_CreditCard_ViewCreditCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!-- Start Search Filters displayed none by dev team--------------------------------------------------------------------->
    <div class="row" style="display: none;">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= ResourceMgr.GetMessage("Search")%> </h5>
                </div>
                <div class="ibox-content" style="display: block;">
                    <!-- Filters Old UI as not used right now on page-->
                    <!-- it was placed in update panel displayed none-->
                    <div id="midSearch" style="display: none;">
                        <div class="search-filter-outer">
                            <div class="search-filter_inner">
                                <asp:Panel ID="pnlUserSearch" runat="server" DefaultButton="btnUserSearch" CssClass="search-filter_inner">
                                    <div class="search-filter_heading">
                                        <%= ResourceMgr.GetMessage("Search Filters")%>
                                    </div>
                                    <div class="search-filter-content-outer">
                                        <div class="content-txt">
                                            <%= ResourceMgr.GetMessage("First Name")%>
                                        </div>
                                        <div class="content-field">
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt-field"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="search-filter-content-outer">
                                        <div class="content-txt">
                                            <%= ResourceMgr.GetMessage("Last Name")%>
                                        </div>
                                        <div class="content-field">
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt-field"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="search-filter-content-outer">
                                        <div class="content-txt">
                                            <%= ResourceMgr.GetMessage("Login")%>
                                        </div>
                                        <div class="content-field">
                                            <asp:TextBox ID="txtLogin" runat="server" CssClass="txt-field"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="search-filter-content-outer">
                                        <div class="content-txt">
                                            <%= ResourceMgr.GetMessage("Created From Date")%>
                                        </div>
                                        <div class="content-field">
                                            <asp:TextBox ID="txtCreatedFromDate" runat="server" CssClass="txt-field"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="search-filter-content-outer">
                                        <div class="content-txt">
                                            <%= ResourceMgr.GetMessage("Created To Date")%>
                                        </div>
                                        <div class="content-field">
                                            <asp:TextBox ID="txtCreatedToDate" runat="server" CssClass="txt-field"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="btn-search_outer">
                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn-search"><%= ResourceMgr.GetMessage("Reset")%></asp:LinkButton>
                                        <asp:LinkButton ID="btnUserSearch" runat="server" CssClass="btn-search"><%= ResourceMgr.GetMessage("Search")%></asp:LinkButton>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <!-- placed in update panel-->
                </div>
            </div>
        </div>
    </div>
    <!-- end of filters -->
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= ResourceMgr.GetMessage("Credit Card Information")%> </h5>
                    <div class="ibox-tools" id="dvAdd" runat="server">
                       <div class="form-group">
                     <a href='addcreditcard' class="btn btn-sm btn-primary font-bold"><i class="fa fa-plus"></i> <%= ResourceMgr.GetMessage("Add Credit Card")%></a>
                           </div> 
                    </div>
                </div>
                <div class="ibox-content" style="display: block;">
                    <!-- Grid-->
                    <asp:UpdatePanel ID="upnlsearch" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvCreditCardInfo" runat="server" CssClass="table table-bordered epr-sec-table" AutoGenerateColumns="False"
                                EnableViewState="true" EmptyDataText="No data was found" DataKeyNames="intCreditCardid" OnRowCommand="gvCreditCardInfo_RowCommand" OnRowDeleted="gvCreditCardInfo_RowDeleted"
                                OnRowDeleting="gvCreditCardInfo_RowDeleting" EmptyDataRowStyle-CssClass="alert alert-danger text-center" OnRowDataBound="gvCreditCardInfo_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Credit Card Type") %>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("LookupTypeName")%>
                                            <asp:HiddenField ID="hdnfldId" Value='<%# Eval("intCreditCardid") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%= ResourceMgr.GetMessage("Card Number")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnCardNumber"  value='<%# Eval("vchCardNumber")%>' runat="server" />
                                            <asp:Label ID="lblCardNumber" runat="server"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%= ResourceMgr.GetMessage("CV2 Code")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("vchCV2")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%= ResourceMgr.GetMessage("Card Name")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("vchCreditCardName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="110">
                                        <HeaderTemplate>
                                            <%= ResourceMgr.GetMessage("Expiration Date")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--   <%# Convert.ToDateTime(Eval("dtmExpiryDate")).ToString("MM/dd/yy")%>--%>

                                            <%# Eval("ExpirationDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="50" ItemStyle-CssClass="text-center">
                                        <HeaderTemplate>
                                            <%= ResourceMgr.GetMessage("Status")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="HyperLink1" runat="server" Visible='<%# Convert.ToBoolean(Eval("bitIsPrimary")) %>' ToolTip="Primary"> 
                                                <span class="badge badge-primary">Primary</span> </asp:LinkButton>
                                            <asp:LinkButton ID="hyplnkStatusFalse" runat="server" Visible='<%# !Convert.ToBoolean(Eval("bitIsPrimary")) %>' ToolTip="Not Primary"> 
                                                <span class="badge badge-danger">Not Primary</span> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="95">
                                        <HeaderTemplate>
                                            <%= ResourceMgr.GetMessage("Primary Card")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkboxPrimary" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("bitIsPrimary"))%>'
                                                runat="server" OnCheckedChanged="chkboxPrimary_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="70" ItemStyle-Wrap="false">
                                        <HeaderTemplate>
                                            <%= ResourceMgr.GetMessage("Actions")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <a href='addcreditcard?CreditCardId=<%# Eval("intCreditCardid")%>' title="Edit Credit Card" class="btn btn-white btn-bitbucket">
                                                <i class="fa fa-edit"></i></a>
                                            <asp:LinkButton ID="imgbtnDeleteCreditInfo" ToolTip="Delete Credit Card" runat="server" CommandName="Delete" CommandArgument='<%# Eval("intCreditCardid") %>' CssClass="btn btn-white btn-bitbucket" OnClientClick="return confirm('Are you sure you want to delete Credit Card?');"> <i class="fa fa-trash"></i> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
