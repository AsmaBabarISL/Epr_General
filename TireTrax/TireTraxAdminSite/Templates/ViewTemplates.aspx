<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewTemplates.aspx.cs" Inherits="Templates_ViewTemplates" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#asearch').toggle(function () {
                $('#midSearch').slideDown();
                SetFilterDateRange();
            }, function () {
                $('#midSearch').slideUp();
            });
        });
        function ClearSearchFields() {
            $("#<%=txtTemplateName.ClientID%>").val('');
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>

    <div class="grid-contain-outer">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                    <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="txt-main-had">

            <div class="txt-had-left" style="background: none;">
                Templates
            </div>
            <div class="txt-had-right">
                <a class="ico_search" id="asearch" href="javascript:void();"><%= ResourceMgr.GetMessage("Search")%></a>
                 <a class="ico_add" href='/Templates/AddTemplate.aspx'>
                    <%= ResourceMgr.GetMessage("Create Templates")%></a>
            </div>
        </div>
        <div id="midSearch" style="display: none;">
            <div class="search-filter-outer" style="height: auto;">
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" CssClass="search-filter_inner">

                    <div class="search-filter_heading">
                        <%= ResourceMgr.GetMessage("Search Filters")%>
                    </div>


                    <div class="search-filter-content-outer">
                        <div class="content-txt">
                            <%= ResourceMgr.GetMessage("Template Name")%>
                        </div>
                        <div class="content-field">
                            <asp:TextBox ID="txtTemplateName" runat="server" CssClass="txt-field"></asp:TextBox>
                        </div>

                    </div>

                    <div class="search-filter-content-outer">
                        <div class="content-txt">
                            <%= ResourceMgr.GetMessage("Template Type")%>
                        </div>
                        <div class="content-field">
                            <asp:DropDownList class="sct_field sct_field" ID="ddlTemplateType" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="search-filter-content-outer">
                        <div class="content-txt">
                            <%= ResourceMgr.GetMessage("Invoice Type")%>
                        </div>
                        <div class="content-field">
                            <asp:DropDownList ID="ddlInvoiceType" runat="server" CssClass="txt-field">
                                <asp:ListItem Value="0" Selected="True" Text="All"></asp:ListItem>
                                <asp:ListItem Value="1" Text='Single'></asp:ListItem>
                                <asp:ListItem Value="2" Text='Commulative'></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="reg_button-outer" style="right: 0px; bottom: -9px;">

                        <cc1:ResourceLinkButton ID="btnTemplateCancel" runat="server" CssClass="btn-search"
                            OnClientClick="ClearSearchFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                        <cc1:ResourceLinkButton ID="btnSearch" runat="server" CssClass="btn-search"
                            OnClick="btnSearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>

                    </div>
                    <br clear="all" />
                </asp:Panel>
            </div>
        </div>
        <br />
        <div>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:GridView ID="gvTemplateinfo" AutoGenerateColumns="False" GridLines="None"
                        CssClass="add-new-inventory" EnableViewState="true" EmptyDataText="No data found"
                        wrap="nowrap" CellPadding="0" Width="100%" runat="server" OnRowCommand="gvTemplateinfo_RowCommand"
                        OnRowDataBound="gvTemplateinfo_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Template ID")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("Templateid")%>
                                    <asp:HiddenField ID="hdTemplateid" Value='<%# Eval("Templateid") %>' runat="server" />
                                    <asp:HiddenField ID="hdTemplateTypeId" Value='<%# Eval("TemplateTypeID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Name")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("Name")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Template Type")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("TemplateTypeName")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("InvoiceType")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("InvoiceType")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Date")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToDateTime(Eval("DateCreated")).ToShortDateString()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <%= ResourceMgr.GetMessage("Active")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="LnkActivestatus" runat="server" ImageUrl="/Images/thumb-up_icon.png"
                                        ToolTip="Active" Visible='<%# Convert.ToBoolean(Eval("IsActive")) %>'></asp:HyperLink>
                                    <asp:HyperLink ID="LnkDeActivestatus" runat="server" ImageUrl="/Images/thumb-down_icon.png"
                                        ToolTip="Not Active" Visible='<%# !Convert.ToBoolean(Eval("IsActive")) %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <%= ResourceMgr.GetMessage("Primary")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkPrimaryStatus" runat="server" ImageUrl="/Images/thumb-up_icon.png"
                                        ToolTip="Primary" Visible='<%# Convert.ToBoolean(Eval("Isprimary")) %>'></asp:HyperLink>
                                    <asp:HyperLink ID="lnkNotPrimaryStatus" runat="server" ImageUrl="/Images/thumb-down_icon.png"
                                        ToolTip="Not Primary" Visible='<%# !Convert.ToBoolean(Eval("Isprimary")) %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <%= ResourceMgr.GetMessage("Primary")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkboxPrimary" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("Isprimary"))%>'
                                        runat="server" OnCheckedChanged="chkboxPrimary_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="left"
                                ItemStyle-HorizontalAlign="left">
                                <HeaderTemplate>
                                    Actions
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="Imgbtntemplate" runat="server" ToolTip="View Delivery" ImageUrl="/Images/ico_View.png" Style="position: relative; top: 3px;"
                                        CommandArgument='<%# Eval("Templateid") %>' CommandName="DeliveryInfo"></asp:ImageButton>&nbsp;
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ImageUrl="../Images/edit_icon.png" Style="position: relative; top: 3px;"
                                        ToolTip="Edit Delivery Info" CommandName="Edit" CommandArgument='<%# Bind("Templateid")%>'></asp:ImageButton>

                                    <asp:ImageButton ID="imgBtneDeactivate" runat="server" ImageUrl="~/Images/inactive.png" Style="position: relative; top: 3px;"
                                        ToolTip="De-activate Template" OnClientClick="return confirm('Are you sure you want to deactivate template?');"
                                        CommandName="DeActivate" CommandArgument='<%# Bind("Templateid")%>' Visible='<%# Convert.ToBoolean(Eval("IsActive")) %>'></asp:ImageButton>
                                    <asp:ImageButton ID="imgbtnActivate" runat="server" ImageUrl="~/Images/active.png" Style="position: relative; top: 3px;"
                                        ToolTip="Activate Template" OnClientClick="return confirm('Are you sure you want to activate template?');"
                                        CommandName="Activate" CommandArgument='<%# Bind("Templateid")%>' Visible='<%# !Convert.ToBoolean(Eval("IsActive")) %>'></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>
                    <div class="txt-pagination">
                        <uc2:Pager ID="pgrTemplate" runat="server" />
                    </div>
                    <br />





                    <div id="dvMainTemplate" runat="server" class="box_blockCmp" visible="false">
                        <div class="popUp_lotInfo">

                            <div id="dvLoadSummaryInfo" runat="server">
                                <div class="textTitle" style="border-bottom: solid 1px #ddd; padding-bottom: 5px; margin-bottom: 20px;">
                                    <%= ResourceMgr.GetMessage("Template Info")%>
                                </div>
                            </div>
                            <div class="view-label">
                                <asp:Label ID="Label3" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Template ID:")%></asp:Label>
                                <asp:Label ID="lblTemplateID" runat="server"></asp:Label>
                            </div>
                            <div class="view-label" >
                                <asp:Label ID="lblDeNam" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Template Name:")%></asp:Label>
                                <asp:Label ID="lblTemplateName" runat="server"></asp:Label>
                            </div>
                            <div class="view-label" >
                                <asp:Label ID="lbltirecount" runat="server"  Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Template Date:")%></asp:Label>
                                <asp:Label ID="lblTemplateDate" runat="server" CssClass="lefts"></asp:Label>
                            </div>
                            <div class="view-label" >
                                <asp:Label ID="Label4" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Invoice Type:")%></asp:Label>
                                <asp:Label ID="lblInvoiceType" runat="server" CssClass="lefts"></asp:Label>
                            </div>
                             <div class="view-label" >
                                <asp:Label ID="Label6" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Template Type:")%></asp:Label>
                                <asp:Label ID="lblTemplateType" runat="server" CssClass="lefts"></asp:Label>
                            </div>
                             
                             <br clear="all" />
                            <div id="dvLoadTireInfo" runat="server" style="overflow-y: scroll; height: 228px; padding-top: 20px;">
                                <asp:Label ID="Label5" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Body:")%></asp:Label>
                                <asp:Literal ID="ltrBody" runat="server"></asp:Literal>
                            </div>
                            <div style="padding-left: 29px; padding-bottom: 10px;">

                                <cc1:ResourceLinkButton class="reg_button" ID="ResourceLinkButton2" runat="server"
                                    OnClick="btnDeliveryDetailBack_Click">Back</cc1:ResourceLinkButton>
                               
                            </div>
                            <asp:HiddenField ID="hdnlotid" runat="server" />
                        </div>
                    </div>







                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

