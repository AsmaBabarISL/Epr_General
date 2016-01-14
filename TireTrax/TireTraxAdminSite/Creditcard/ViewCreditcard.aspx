<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewCreditcard.aspx.cs" Inherits="Creditcard_ViewCreditcard" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!-- filters div visibility is false -->
    <div style="display:none">
    <asp:UpdatePanel ID="upnlsearch" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlUserSearch" runat="server" DefaultButton="btnUserSearch" CssClass="search-filter_inner">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                            </div>
                            <div class="ibox-content" style="display: block;">
                                <div class="row">
                                    <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("First Name")%></label>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("Last Name")%></label>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div id="date_range">
                                        <div class="input-daterange">
                                            <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                                <label><%= ResourceMgr.GetMessage("Created From Date")%></label>
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtCreatedFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                                <label><%= ResourceMgr.GetMessage("Created To Date")%></label>
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtCreatedToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("Login")%></label>
                                        <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-sm-12 mb0">
                                        <asp:LinkButton ID="btnUserSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"><%= ResourceMgr.GetMessage("Search")%></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"><%= ResourceMgr.GetMessage("Reset")%></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>





            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
    </div>


    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= ResourceMgr.GetMessage("Credit Card Information")%> </h5>
                    <div class="ibox-tools" id="dvAdd" runat="server">
                        <div class="form-group">
                            <a href='addcreditcard.aspx' class="btn btn-sm btn-primary font-bold"><i class="fa fa-plus"></i><%= ResourceMgr.GetMessage(" Credit Card")%></a>
                        </div>
                    </div>
                </div>
                <div class="ibox-content" style="display: block;">
                    <!-- Grid-->
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvCreditCardInfo" runat="server" CssClass="table table-bordered epr-sec-table" AutoGenerateColumns="False"
                                            EnableViewState="true" EmptyDataText="No data was found" DataKeyNames="intCreditCardid" OnRowCommand="gvCreditCardInfo_RowCommand" OnRowDeleted="gvCreditCardInfo_RowDeleted"
                                            OnRowDeleting="gvCreditCardInfo_RowDeleting" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
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
                                                        <%# Eval("vchCardNumber")%>
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
                                                        <asp:Label ID="HyperLink1" runat="server" CssClass="badge badge-primary" Visible='<%# Convert.ToBoolean(Eval("bitIsPrimary")) %>' ToolTip="Primary"> 
                                                        Primary</asp:Label>
                                                        <asp:Label ID="hyplnkStatusFalse" runat="server" CssClass="badge badge-danger" Visible='<%# !Convert.ToBoolean(Eval("bitIsPrimary")) %>' ToolTip="Not Primary"> 
                                                        Not Primary </asp:Label>
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
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="70">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a href='/Creditcard/AddCreditCard.aspx?CreditCardId=<%# Eval("intCreditCardid")%>' title="Edit Credit Card" class="btn btn-white btn-bitbucket">
                                                            <i class="fa fa-edit"></i></a>
                                                        <asp:LinkButton ID="imgbtnDeleteCreditInfo" ToolTip="Delete Credit Card" runat="server" CommandName="Delete" CommandArgument='<%# Eval("intCreditCardid") %>' CssClass="btn btn-white btn-bitbucket" OnClientClick="return confirm('Are you sure you want to DEACTIVATE this Credit Card?');"> <i class="fa fa-trash"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

