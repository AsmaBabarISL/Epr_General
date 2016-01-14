<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ViewTemplates.aspx.cs" Inherits="Templates_ViewTemplates" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("form");
            $(".ajax-loader").remove();
        }

        function AjaxLoader() {
            $(".ajax-loader").appendTo("form");
        }

        function ClearSearchFields() {
            $("#<%=txtTemplateName.ClientID%>").val('');
            $('#<%=ddlInvoiceType.ClientID%>').val('0');
            $('#<%=ddlTemplateType.ClientID%>').val('0');
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                    <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">

            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Search Filters")%> </h5>
                        </div>
                        <div class="ibox-content" style="display: block;">
                            <!-- Form-->
                            <div role="form" id="">
                                <div class="row">
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("Template Name")%></label>
                                        <asp:TextBox ID="txtTemplateName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("Template Type")%></label>
                                        <asp:DropDownList class="form-control" ID="ddlTemplateType" runat="server" 
                                            OnSelectedIndexChanged="ddlTemplateType_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-4 col-lg-3" id="Invoice_Type" runat="server" >
                                        <label><%= ResourceMgr.GetMessage("Invoice Type")%></label>
                                        <asp:DropDownList ID="ddlInvoiceType" runat="server" CssClass="form-control" Enabled="false">
                                            <asp:ListItem Value="0" Selected="True" Text="All"></asp:ListItem>
                                            <asp:ListItem Value="1" Text='Single'></asp:ListItem>
                                            <asp:ListItem Value="2" Text='Commulative'></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-12 mb0">
                                        <cc1:ResourceLinkButton ID="btnSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                            OnClick="btnSearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                        <%--OnClientClick="ClearSearchFields();"--%>
                                        <cc1:ResourceLinkButton ID="btnTemplateCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                                            
                                            OnClick="btnTemplateCancel_Click"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
            <ContentTemplate>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Templates</h5>
                                <div class="ibox-tools">
                                    <div class="form-group">
                                        <cc1:ResourceLinkButton CssClass="btn btn-sm btn-primary" ID="btnaddTemplate" runat="server" OnClick="btnaddTemplate_Click">
                                            <i class="fa fa-plus"></i> <strong><%= ResourceMgr.GetMessage("Create Templates")%></strong> </cc1:ResourceLinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox-content" style="display: block;">
                                <!-- Grid here-->
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">

                                            <asp:GridView ID="gvTemplateinfo" AutoGenerateColumns="False" GridLines="None" 
                                                CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data was found"
                                                runat="server" OnRowCommand="gvTemplateinfo_RowCommand" OnRowDataBound="gvTemplateinfo_RowDataBound"
                                                 EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Template ID")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Templateid")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Name")%>
                                                            <asp:HiddenField ID="hdTemplateid" Value='<%# Eval("Templateid") %>' runat="server" />
                                                            <asp:HiddenField ID="hdTemplateTypeId" Value='<%# Eval("TemplateTypeID") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Template Type")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("TemplateTypeName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("InvoiceType")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("InvoiceType")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Date")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Convert.ToDateTime(Eval("DateCreated")).ToShortDateString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Status")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="LnkActivestatus" runat="server" CssClass="badge badge-primary"
                                                                ToolTip="Active" Visible='<%# Convert.ToBoolean(Eval("IsActive")) %>'>
                                                                Active
                                                            </asp:HyperLink>
                                                            <asp:HyperLink ID="LnkDeActivestatus" runat="server" CssClass="badge"
                                                                ToolTip="Not Active" Visible='<%# !Convert.ToBoolean(Eval("IsActive")) %>'>
                                                                Not Active
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Primary")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkPrimaryStatus" runat="server" ToolTip="Primary" Visible='<%# Convert.ToBoolean(Eval("Isprimary")) %>'> 
                                                                <span class="badge badge-primary">Primary</span> </asp:HyperLink>
                                                            <asp:HyperLink ID="lnkNotPrimaryStatus" runat="server" ToolTip="Not Primary" Visible='<%# !Convert.ToBoolean(Eval("Isprimary")) %>'> 
                                                                <span class="badge badge-danger">Not Primary</span> </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Primary")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkboxPrimary" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("Isprimary"))%>'
                                                                runat="server" OnCheckedChanged="chkboxPrimary_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="90" ItemStyle-Wrap="false">
                                                        <HeaderTemplate>
                                                            Actions
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Imgbtntemplate" runat="server" ToolTip="View Template" CssClass="btn btn-white btn-bitbucket" CommandArgument='<%# Eval("Templateid") %>' CommandName="DeliveryInfo"> <i class="fa fa-eye"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="imgbtnEdit" runat="server" ToolTip="Edit Template Info" CssClass="btn btn-white btn-bitbucket" CommandName="Edit"
                                                                CommandArgument='<%# Bind("Templateid")%>'> <i class="fa fa-edit"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="imgBtneDeactivate" runat="server" ToolTip="De-activate Template" CssClass="btn btn-white btn-bitbucket"
                                                                OnClientClick="return confirm('Are you sure you want to deactivate template?');" CommandName="DeActivate"
                                                                CommandArgument='<%# Bind("Templateid")%>' Visible='<%# Convert.ToBoolean(Eval("IsActive")) %>'>
                                                                <i class="fa fa-thumbs-down"></i>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="imgbtnActivate" runat="server" ToolTip="Activate Template" CssClass="btn btn-white btn-bitbucket"
                                                                OnClientClick="return confirm('Are you sure you want to activate template?');" CommandName="Activate"
                                                                CommandArgument='<%# Bind("Templateid")%>' Visible='<%# !Convert.ToBoolean(Eval("IsActive")) %>'>
                                                                <i class="fa fa-thumbs-up"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                            <div class="txt-pagination">
                                                <uc2:Pager ID="pgrTemplate" runat="server" />
                                            </div>

                                            <div id="dvMainTemplate" runat="server" visible="false">
                                                <div class="ajaxModal-popup inmodal">
                                                    <div class="ajaxModal-body animated bounceInRight" id="dvParkingLot1" runat="server">
                                                        <div class="modal-header">
                                                            <h4 class="modal-title">
                                                                <%= ResourceMgr.GetMessage("Template Info")%>
                                                            </h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <dl class="dl-horizontal mb0">
                                                                        <dt>
                                                                            <asp:Label ID="lblDeNam" runat="server"><%= ResourceMgr.GetMessage("Template Name:")%></asp:Label></dt>
                                                                        <dd>
                                                                            <asp:Label ID="lblTemplateName" runat="server"></asp:Label>
                                                                        </dd>
                                                                    </dl>


                                                                </div>
                                                                <div class="col-md-6 col-sm-6">
                                                                    <dl class="dl-horizontal">
                                                                        <dt>
                                                                            <asp:Label ID="Label3" runat="server"><%= ResourceMgr.GetMessage("Template ID:")%></asp:Label></dt>
                                                                        <dd>
                                                                            <asp:Label ID="lblTemplateID" runat="server"></asp:Label>
                                                                        </dd>

                                                                        <dt>
                                                                            <asp:Label ID="lbltirecount" runat="server"><%= ResourceMgr.GetMessage("Template Date:")%></asp:Label></dt>
                                                                        <dd>
                                                                            <asp:Label ID="lblTemplateDate" runat="server"></asp:Label>
                                                                        </dd>
                                                                    </dl>
                                                                </div>
                                                                <div class="col-md-6 col-sm-6">
                                                                    <dl class="dl-horizontal">
                                                                        <dt>
                                                                            <asp:Label ID="Label4" runat="server"><%= ResourceMgr.GetMessage("Invoice Type:")%></asp:Label></dt>
                                                                        <dd>
                                                                            <asp:Label ID="lblInvoiceType" runat="server"></asp:Label>
                                                                        </dd>
                                                                        <dt>
                                                                            <asp:Label ID="Label6" runat="server"><%= ResourceMgr.GetMessage("Template Type:")%></asp:Label></dt>
                                                                        <dd>
                                                                            <asp:Label ID="lblTemplateType" runat="server"></asp:Label>
                                                                        </dd>
                                                                    </dl>
                                                                </div>

                                                                <!-- info div-->
                                                                <div>
                                                                    <div id="dvLoadTireInfo" runat="server">
                                                                        <div class="col-md-12">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    <asp:Label ID="Label5" runat="server"><b><%= ResourceMgr.GetMessage("Body")%></b></asp:Label>
                                                                                </div>
                                                                                <div class="panel-body" style="height: 200px; overflow-y: auto;">
                                                                                    <asp:Literal ID="ltrBody" runat="server"></asp:Literal>
                                                                                </div>

                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <cc1:ResourceLinkButton class="btn btn-white btn-sm" ID="ResourceLinkButton2" runat="server"
                                                                OnClick="btnDeliveryDetailBack_Click">Back</cc1:ResourceLinkButton>

                                                            <asp:HiddenField ID="hdnlotid" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

    </div>




</asp:Content>

