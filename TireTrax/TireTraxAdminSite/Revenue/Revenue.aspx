<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Revenue.aspx.cs" Inherits="Revenue_Revenue" MasterPageFile="~/adminMaster.master" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />
    <script src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>
    <%--Done for datepicker in update panel--%>
    <script type="text/javascript">
        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("form");
            $(".ajax-loader").remove();
        }

        function AjaxLoader() {
            $(".ajax-loader").appendTo("form");
        }
        $(document).ready(function () {
            $(".ajaxModal-popup").appendTo("form");

            //Done for datepicker in update panel
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $('.classTarget').datepicker({ dateFormat: 'dd-mm-yy' });
            }

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>


    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <label><%= ResourceMgr.GetMessage("Search Filter")%></label>
                        </div>
                        <div class="ibox-content">
                            <!-- Filters-->
                            <div role="form" class="row search-filter">

                                <div class="form-group col-md-3 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("State")%></label>
                                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="form-group col-md-3 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Organization Type")%></label>
                                    <asp:DropDownList ID="ddlOrganization" runat="server" Enabled="false" CssClass="form-control"></asp:DropDownList>
                                    <asp:Label ID="lblErrorOrganization" runat="server" CssClass="custom-block-error" Visible="false"></asp:Label>
                                </div>


                                <div id="date_range">
                                    <div class="input-daterange">
                                        <div class="form-group col-md-3 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("From")%></label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control classTarget"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lblFromDateError" runat="server" CssClass="custom-block-error" Visible="false"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("To")%></label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control classTarget"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lblToDateError" runat="server" CssClass="custom-block-error" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!--ENDs HERE -->
                            <cc1:ResourceLinkButton ID="btnRevenueSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                OnClick="btnRevenueSearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>

                            <cc1:ResourceLinkButton ID="btnRevenueCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                                OnClick="btnRevenueCancel_Click"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Revenue</h5>
                        </div>

                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvRevenue" runat="server" EmptyDataText="No data found"
                                            EmptyDataRowStyle-CssClass="alert alert-danger text-center" CssClass="table table-bordered epr-sec-table"
                                            OnRowCommand="gvRevenue_RowCommand" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Organization")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("OrgName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Period")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Period")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Amount")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("InvoiceAmount","{0:c}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Total Invoices")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("TotalInvoices")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Action")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgbtnViewRevenue" CssClass="btn btn-white btn-bitbucket" runat="server"
                                                            ToolTip="View Revenue" CommandName="RevenueInfo" CommandArgument='<%#Eval("OrganizationId")%>'>
                                                        <i class="fa fa-eye"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div id="dvRevenueDetail" runat="server" visible="false" class="ajaxModal-popup inmodal">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("Revenue Detail")%>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gvViewDetail" runat="server" EmptyDataText="No data found" AutoGenerateColumns="false"
                            EmptyDataRowStyle-CssClass="alert alert-danger text-center" CssClass="table table-bordered epr-sec-table">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Invoice #")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("InvoiceNumber")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Invoice Date")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("InvoiceDate","{0:d}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Product Type")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("ProductType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Product Sub Type")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("ProudctSubType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Invoice Amount")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("InvoiceAmount","{0:c}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="row">
                            <div class="col-md-12">
                                <uc2:Pager ID="pgrRevenueDetails" runat="server" />
                            </div>
                        </div>
                        <small class="font-bold">
                            <asp:Label ID="lblTotalProducts" runat="server" Visible=" false"></asp:Label>
                        </small>
                        <%--<div class="row">
                            <div class="col-lg-3">
                                <asp:Label ID="lblPagingLeft" runat="server" font-size="smaller"></asp:Label>
                            </div>
                            <div class="col-lg-9">
                                <asp:Literal ID="ltrlPaging" runat="server"></asp:Literal>
                            </div>
                        </div>--%>
                    </div>

                    <div class="modal-footer">
                        <asp:LinkButton ID="lnkCancel" runat="server" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnOrganizationID" />
                    <asp:HiddenField runat="server" ID="hdnStartDate" />
                    <asp:HiddenField runat="server" ID="hdnEndDate" />
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
