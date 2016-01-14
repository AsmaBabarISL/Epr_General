<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ViewRevenue.aspx.cs" Inherits="Revenue_ViewRevenue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>
    <%--Done for datepicker in update panel--%>
    <script type="text/javascript">
        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("form");
            //$(".ajax-loader").remove();
            $(".ajax-loader").hide();
        }

        function AjaxLoader() {
            $(".ajax-loader").show();
            $(".ajax-loader").appendTo("form");
        }
        
        $(document).ready(function () {
            $(".ajaxModal-popup").appendTo("form");

            //Done for datepicker in update panel
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $('.classTarget').datepicker({ dateFormat: 'dd-mm-yy' });
                if ($('#' + '<%=rdo1.ClientID%>').prop('checked')) {
                    $('#' + '<%=pnlDateoptions.ClientID%>').show();
                    $('#' + '<%=pnlPeriodoptions.ClientID%>').hide();
                }
                else if ($('#' + '<%=rdo2.ClientID%>').prop('checked')) {
                    $('#' + '<%=pnlDateoptions.ClientID%>').hide();
                    $('#' + '<%=pnlPeriodoptions.ClientID%>').show();
                }
        }
        });

    function DisablePanel() {

        $('#' + '<%=pnlDateoptions.ClientID%>').show();
        $('#' + '<%=pnlPeriodoptions.ClientID%>').hide();
        $("#<%=txtFromDate.ClientID%>").val('');
        $("#<%=txtToDate.ClientID%>").val('');
    }

    function DisablePanel2() {

        $('#' + '<%=pnlDateoptions.ClientID%>').hide();
        $('#' + '<%=pnlPeriodoptions.ClientID%>').show();
        $('#<%=ddlReportType.ClientID%>').val('-1');
        $('#<%=ddlYearly.ClientID%>').val('0');
    }

    function HideBoth() {
        $('#' + '<%=pnlPeriodoptions.ClientID%>').hide();
        $('#' + '<%=pnlDateoptions.ClientID%>').hide();
        $('#' + '<%=pnlDateoptions.ClientID%>').show();
        $('#' + '<%=rdo1.ClientID%>').prop('checked', true);

    }

    function CheckBoxes() {
        if ($('#' + '<%=rdo1.ClientID%>').prop('checked')) {
            $('#' + '<%=pnlDateoptions.ClientID%>').show();
            $('#' + '<%=pnlPeriodoptions.ClientID%>').hide();
        }
        else if ($('#' + '<%=rdo2.ClientID%>').prop('checked')) {
            $('#' + '<%=pnlDateoptions.ClientID%>').hide();
            $('#' + '<%=pnlPeriodoptions.ClientID%>').show();
        }
}

function HideDiv() {
    debugger;
    $('#' + '<%=dvRevenueDetail.ClientID%>').hide();

}
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>


    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" class="ajax-loader" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
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

                            <div class="row">
                                <div class="col-lg-2">
                                    <label><b><%= ResourceMgr.GetMessage("Transaction Period:")%></b></label>
                                </div>

                                <div class="col-lg-2">
                                    <asp:RadioButton ID="rdo1" runat="server" Text="Date Wise" GroupName="group1" CssClass="prod-type-chkbox" onclick="DisablePanel();" />
                                </div>
                                <div class="col-lg-2">
                                    <asp:RadioButton ID="rdo2" runat="server" Text="Period Wise" GroupName="group1" CssClass="prod-type-chkbox" onclick="DisablePanel2();" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Panel ID="pnlDateoptions" runat="server">
                                        <div role="form" class="row search-filter">
                                            <div id="date_range">
                                                <div class="input-daterange">
                                                    <div class="form-group col-md-4 col-lg-3">
                                                        <label><%= ResourceMgr.GetMessage("From")%></label>
                                                        <div class="input-group date">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control classTarget"></asp:TextBox>
                                                        </div>
                                                        <asp:Label ID="lblFromDateError" runat="server" CssClass="custom-block-error" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="form-group col-md-4 col-lg-3">
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

                                    </asp:Panel>


                                    <asp:Panel ID="pnlPeriodoptions" runat="server">
                                        <div role="form" class="row search-filter">
                                            <div class="form-group col-md-4 col-lg-3">
                                                <label><%= ResourceMgr.GetMessage("Report Type")%></label>
                                                <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                    <%--<asp:ListItem Text="All" Value="0"></asp:ListItem>--%>
                                                    <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                                    <asp:ListItem Text="Quarterly" Value="Quarterly"></asp:ListItem>
                                                    <asp:ListItem Text="Bi-annual" Value="Biannual"></asp:ListItem>
                                                    <asp:ListItem Text="Yearly" Value="Yearly"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="lblReportTypeError" runat="server" CssClass="custom-block-error" Visible="false"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-4 col-lg-3" id="divYear" runat="server" visible="false">
                                                <label><%= ResourceMgr.GetMessage("Year")%></label>
                                                <asp:DropDownList ID="ddlYearly" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>

                                            <div class="form-group col-md-4 col-lg-3" id="divChkboxesYear" runat="server" visible="false">
                                                <label><%= ResourceMgr.GetMessage("Year")%></label>
                                                <asp:CheckBoxList ID="chkboxYears" runat="server" CssClass="prod-type-chkbox"
                                                    RepeatColumns="3" RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <!--ENDs HERE -->

                                    <cc1:ResourceLinkButton ID="btnRevenueSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                        OnClick="btnRevenueSearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>

                                    <cc1:ResourceLinkButton ID="btnRevenueCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                                        OnClick="btnRevenueCancel_Click"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>

                                </div>
                            </div>
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
                                            OnRowCommand="gvRevenue_RowCommand" AutoGenerateColumns="false" OnSorting="gvRevenue_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Period")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Period")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                            ToolTip="View Revenue" CommandName="RevenueInfo" CommandArgument='<%# Eval("Period") + "," + Eval("Year") %>'>
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
                    </div>
                    <div class="modal-footer">
                        <%--Not visible beacuse of post back issue--%>
                        <a class="btn btn-primary" id="openStewardshipAgr" onclick="HideDiv();" style="display: none;"><%= ResourceMgr.GetMessage("Back")%> </a>
                        <%--Not visible beacuse of post back issue--%>


                        <asp:LinkButton ID="lnkCancel" runat="server" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click">
                            <%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnPeriodYear" />
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

