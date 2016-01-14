<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ViewInvoice.aspx.cs" Inherits="Invoices_ViewInvoice" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">


        function ClearSearchFields() {

            $("#<%=txtInvoiceNo.ClientID%>").val('');
            $("#<%=txtOrganizationName.ClientID%>").val('');
            $("#<%=txtFrmDate.ClientID%>").val('');
            $("#<%=txtToDate.ClientID%>").val('');
            $("#<%=ddlStatus.ClientID%>").val('2');
            $("#<%=btnInvoiceSearch.ClientID%>")[0].click();
        }

        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("form");
            $(".ajax-loader").remove();
        }

        function AjaxLoader() {
            $(".ajax-loader").appendTo("form");
        }

        $(document).ready(function () {
            $(".ajaxModal-popup").appendTo("form");
        });


        function fadeOut() {
            $(".custom-absolute-alert").delay(5000).fadeOut(500);
            $(".custom-absolute-alert").appendTo("form");
        }
        function visibleDiv() {
            document.getElementById("<%=dvSendEmail.ClientID%>").style.display = "block";
        }

        function dismissDiv() {
            document.getElementById("<%=dvSendEmail.ClientID%>").style.display = "none";
        }

        function ValidateNumberic(e) {
            
            if (e.which != 8 && e.which != 0 && e.which != 46 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        }
        function DoubleDotValidation() {

            if ($("#<%=txtPaymentAmount.ClientID%>").val().split('.').length > 2) {
                $("#<%=txtPaymentAmount.ClientID%>").val($("#<%=txtPaymentAmount.ClientID%>").val().slice(0, -1));
            }
        }
        function validateAmount(sender, args) {


            args.isValid = false;
            var controlToValidateId = document.getElementById(sender.controltovalidate);

            var invoicePaymentAmount = new Number($('#<%=lblInvoicePaymentAmount.ClientID%>').html());
            var enteredAmount = new Number($(controlToValidateId).val());

            console.log("InvoicePaymentAmount:" + invoicePaymentAmount);
            console.log("entered Amount:" + enteredAmount);

            if (enteredAmount > invoicePaymentAmount) {
                sender.innerHTML = "Payment Amount should not be greater than invoice amount.";
                sender.errormessage = "Payment Amount should not be greater than invoice amount.";
                args.IsValid = false;
                return;
            }
            else {
                args.IsValid = true;
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>



    <asp:Panel ID="pnlSearch" runat="server" CssClass="search-filter_inner" DefaultButton="btnInvoiceSearch">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Search Filters </h5>
                        <div class="ibox-tools">
                        </div>
                    </div>
                    <div class="ibox-content" style="display: block;">
                        <!-- Filters-->
                        <div role="form" class="row search-filter" id="">
                            <div class="form-group col-md-3">
                                <label>Invoice No</label>
                                <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-3">
                                <label>Status</label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="2">All</asp:ListItem>
                                    <asp:ListItem Value="1">Paid</asp:ListItem>
                                    <asp:ListItem Value="0">Outstanding</asp:ListItem>
                                </asp:DropDownList>
                            </div>


                            <div class="form-group col-md-3" style="display: none">
                                <label>Organization Name</label>
                                <asp:TextBox ID="txtOrganizationName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div id="date_range">
                                <div class="input-daterange">
                                    <div class="form-group col-md-3">
                                        <label>From</label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control "></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>To</label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control "></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-md-12 mb0">
                                <cc1:ResourceLinkButton ID="btnInvoiceSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                    OnClick="btnInvoiceSearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                <cc1:ResourceLinkButton ID="btnInvoiceCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                                    OnClientClick="ClearSearchFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>



    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Invoices</h5>
                            <div class="ibox-tools">
                                <div class="form-group">
                                    <cc1:ResourceLinkButton CssClass="btn btn-sm btn-primary font-bold" ID="addInvoicenote" runat="server" OnClick="addInvoicenote_Click">
                                    <i class="fa fa-eye"></i>&nbsp;<%= ResourceMgr.GetMessage(" View Aggregative Invoices")%></cc1:ResourceLinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvInvoicesinfo" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data found"
                                            runat="server" OnRowCommand="gvInvoicesinfo_RowCommand" OnRowDataBound="gvInvoicesinfo_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Invoice #")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("InvoiceNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Invoice From")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("OrganizationFrom")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Provider")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("OrganizationForTo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Invoice Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("InvoiceDate")).ToShortDateString()%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Invoice Amount")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#  Eval("InvoiceAmount","{0:c}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Check #")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ChequeNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Amount Paid")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                       <%#Convert.ToDecimal(Eval("InvoiceAmount")) - (Eval("BalanceAmount") == DBNull.Value? Convert.ToDecimal(Eval("InvoiceAmount")) :Convert.ToDecimal(Eval("BalanceAmount")))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Amount Due")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("BalanceAmount") == DBNull.Value? Eval("InvoiceAmount","{0:c}") :Eval("BalanceAmount","{0:c}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Paid Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("PaymentDate") == DBNull.Value? "" : Convert.ToDateTime(Eval("PaymentDate")).ToShortDateString()%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>







                                                <%--<asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Due Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("DueDate")).ToShortDateString()%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>


                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Status")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lnkApproved" runat="server"
                                                            CssClass="badge badge-primary"
                                                            Visible='<%#Convert.ToBoolean(Eval("IsPaid"))%>'>Invoice paid</asp:Label>

                                                        <asp:Label ID="lnkRejected" runat="server"
                                                            CssClass="badge badge-danger"
                                                            Visible='<%#!Convert.ToBoolean(Eval("IsPaid"))%>'>Invoice not yet paid</asp:Label>

                                                        <%--<asp:LinkButton ID="lnkPending" runat="server"
                                                            ToolTip="Waiting for Invoice response" CssClass="badge badge-navy"
                                                            Visible="false">Waiting for Invoice response</asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="ImgbtnInvoiceID" runat="server" ToolTip="View Invoice Detail"
                                                            CssClass="btn btn-white btn-bitbucket"
                                                            CommandArgument='<%# Eval("InvoiceID") %>' CommandName="InvoiceInfo">
                                        <i class="fa fa-eye"></i></asp:LinkButton>


                                                        <%-- Steve Requirement  --%>
                                                        <asp:LinkButton ID="ImgbtnEditInvoice" runat="server" CssClass="btn btn-white btn-bitbucket"
                                                            ToolTip="Edit Invoice" Visible="false"
                                                            CommandName="EditInvoice" CommandArgument='<%# Bind("InvoiceID")%>'>
                                        <i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="ImgbtnPaidFull" runat="server" CssClass="btn btn-white btn-bitbucket"
                                                            ToolTip="Paid In Full" Visible="false" OnClientClick="return confirm('Are you sure to pay full amount for this invoice?');"
                                                            CommandName="PaidInFull" CommandArgument='<%# Bind("InvoiceID")%>'>
                                        <i class="fa fa-dollar"></i></asp:LinkButton>
                                                         <%-- Steve Requirement  --%>

                                                        <asp:LinkButton ID="lnkbtnPaymentDetail" runat="server" ToolTip="View Payment Detail"
                                                            Visible='<%#(bool) Eval("IsPaid") == true ? true : false %>' CssClass="btn btn-white btn-bitbucket"
                                                            CommandArgument='<%# Eval("InvoiceID") %>' CommandName="PaymentDetail">
                                        <i class="fa fa-eye-slash"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnSend" runat="server" CssClass="btn btn-white btn-bitbucket"
                                                            ToolTip="Send Invoice" Visible="false"
                                                            CommandName="Send" CommandArgument='<%# Bind("InvoiceID")%>'>
                                        <i class="fa fa-dollar"></i></asp:LinkButton>

                                                        <asp:LinkButton ID="lnkbtnPaid" runat="server" CssClass="btn btn-white btn-bitbucket"
                                                            ToolTip="Send Invoice" Visible="false"
                                                            CommandName="Paid" CommandArgument='<%# Bind("InvoiceID")%>'>
                                        <i class="fa fa-mail-forward"></i></asp:LinkButton>



                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <uc2:Pager ID="pgrLoad" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Label runat="server" ID="lblSuccess" Visible="false" CssClass="custom-absolute-alert alert-success"></asp:Label>
            <div class="ajaxModal-popup inmodal" id="dvInvoicepopup" runat="server" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("Invoice Detail")%>
                        </h4>
                    </div>

                    <div class="modal-body">

                        <div role="form" class="row search-filter" id="">
                            <div class="form-group col-md-6">
                                <asp:Label ID="Label7" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("To:")%></asp:Label>
                                
                                <asp:Label ID="lblTo" runat="server"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblOrgForAddress" runat="server"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblOrgForPhone" runat="server"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblOrgForEmail" runat="server"></asp:Label>
                                <asp:Label ID="lblOrganizationId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblInvoiceId" runat="server" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group col-md-6">
                                <asp:Label ID="Label3" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Invoice Number:")%></asp:Label>
                                <asp:Label ID="lblInvoiceNumber" runat="server"></asp:Label>
                            </div>
                            <%-- <div class="form-group col-md-6">
                                <asp:Label ID="Label5" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Invoice Amount:")%></asp:Label>
                                <asp:Label ID="lblInvoiceAmount" runat="server"></asp:Label>
                            </div>--%>

                            <div class="form-group col-md-6">
                                <asp:Label ID="Label4" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Issue Date:")%></asp:Label>
                                <asp:Label ID="lblinvoiceDate" runat="server"></asp:Label>
                            </div>

                            <div class="form-group col-md-6">
                                <asp:Label ID="Label6" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Due Date:")%></asp:Label>
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </div>

                            <div class="form-group col-md-6">
                            </div>
                            <div class="form-group col-md-6">
                            </div>
                            <div class="form-group col-md-12">
                                <asp:Label ID="Label5" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Delivery Name:")%></asp:Label>
                                <asp:Label ID="lblDeliveryName" runat="server"></asp:Label>
                            </div>

                        </div>


                        <div class="row">

                            <div class="col-sm-12">
                                <div class="table-responsive">

                                    <asp:GridView ID="gvInvoicesDetails" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="alert alert-danger"
                                        CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data found"
                                        runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <HeaderTemplate>
                                                    DeliveryID
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("DeliveryID")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <HeaderTemplate>
                                                    Delivery Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("DeliveryName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    Size
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("TireSize")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    Units
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("NoOfTires")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    Fee
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("FeePerTire","{0:c}")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    Amount
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("PTEamount","{0:c}")%>' Style="float: right; margin-right: 0px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <asp:GridView ID="gvInvoiceDetailsForProduct" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="alert alert-danger"
                                        CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data found"
                                        runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <HeaderTemplate>
                                                    DeliveryID
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("DeliveryID")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <HeaderTemplate>
                                                    Delivery Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("DeliveryName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    Size
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("TireSize")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    Product
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("ProductCategoy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    Sub Product
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("ProductSubCategory")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    Units
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("NoOfTires")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    Fee
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("FeePerTire","{0:c}")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    Amount
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("PTEamount","{0:c}")%>' Style="float: right; margin-right: 0px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div style="float: right; margin-right: 25px">
                                        <asp:Label ID="Label8" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Sub Total :  ")%></asp:Label>
                                        <asp:Label ID="lblSubTotal" runat="server" Style="float: right"></asp:Label><br />
                                        <br />
                                        <asp:Label ID="Label10" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Balance :  ")%></asp:Label>
                                        <asp:Label ID="lblBalance" runat="server" Style="float: right"></asp:Label><br />
                                        <hr style="height: 1px; border: none; color: #333; background-color: #333;" />
                                        <asp:Label ID="Label12" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Total :  ")%></asp:Label>
                                        <asp:Label ID="lblTotal" runat="server" Style="float: right"></asp:Label><br />
                                        <br />
                                    </div>
                                    <br clear="all" />
                                    <asp:Label ID="lblOrgAddress" runat="server"></asp:Label><br />
                                    <br />
                                    <asp:Label ID="lblOrgPhone" runat="server"></asp:Label><br />
                                    <br />
                                    <asp:Label ID="lblOrgEmail" runat="server"></asp:Label><br />
                                    <br />

                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hdnlotid" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <cc1:ResourceLinkButton class="btn btn-white btn-sm" ID="ResourceLinkButton2" runat="server"
                            OnClick="btnInvoiceDetailBack_Click">Back</cc1:ResourceLinkButton>
                        <cc1:ResourceLinkButton class="btn btn-primary btn-sm" ID="btnPrint" runat="server"
                            OnClick="btnPrint_Click"><%=ResourceMgr.GetMessage("Print") %></cc1:ResourceLinkButton>
                    </div>
                </div>
            </div>


            <%-- Payment Detail --%>
            <div class="ajaxModal-popup inmodal" id="dvPaymentDetail" runat="server" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("Payment Detail")%>
                        </h4>
                    </div>

                    <div class="modal-body">
                        <asp:Label ID="lblPaymentDetail" CssClass="alert alert-danger text-center" Visible="false" runat="server" Text="No data found"></asp:Label>
                        <div role="form" class="row search-filter" runat="server" id="frmPaymentDetail">
                            <div class="form-group col-md-6">
                                <asp:Label ID="Label2" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Payment Id")%></asp:Label>
                                <asp:Label ID="lblPaymentId" runat="server"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group col-md-6">
                                <asp:Label ID="Label9" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Check Number:")%></asp:Label>
                                <asp:Label ID="lblCheckNumber" runat="server"></asp:Label>
                            </div>

                            <div class="form-group col-md-6">
                                <asp:Label ID="Label11" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Payment Date:")%></asp:Label>
                                <asp:Label ID="lblPaymentDate" runat="server"></asp:Label>
                            </div>
                            <div class="form-group col-md-6">
                                <asp:Label ID="Label13" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Balance Amount:")%></asp:Label>
                                <asp:Label ID="lblBalanceAmount" runat="server"></asp:Label>
                            </div>

                        </div>

                    </div>

                    <div class="modal-footer">
                        <cc1:ResourceLinkButton CssClass="btn btn-white" ID="ResourceLinkButton1" runat="server"
                            OnClick="btnPaymentDetailCancel_Click"><%=ResourceMgr.GetMessage("Cancel") %></cc1:ResourceLinkButton>
                    </div>

                </div>
            </div>




            <%-- Aggregative Invoices 1--%>
            <div class="ajaxModal-popup inmodal" id="AggrInvoices1" runat="server" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("Select Month and Year")%>
                        </h4>
                    </div>

                    <div class="modal-body">
                        <div role="form" class="row search-filter">
                            <div class="form-group col-md-12">
                                <asp:Label ID="lblErrorPopup" runat="server" CssClass="block">* Year is Mandatory. If month is not selected then data will be shown of selected year.</asp:Label>
                            </div>
                            <div class="form-group col-md-6">
                                <br />

                                <label><%= ResourceMgr.GetMessage("Select Month")%></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>

                                <br />
                                <br />
                                <label><%= ResourceMgr.GetMessage("Select Year")%></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:Label ID="lblYear" runat="server" CssClass="custom-block-error" Visible="false" Text="Please select Year"></asp:Label>

                            </div>
                        </div>

                    </div>

                    <div class="modal-footer">
                        <cc1:ResourceLinkButton CssClass="btn btn-white btn-sm" ID="lnkCancelAggrInvoices1" runat="server"
                            OnClick="lnkCancelAggrInvoices1_Click"><%=ResourceMgr.GetMessage("Cancel") %></cc1:ResourceLinkButton>
                        <cc1:ResourceLinkButton CssClass="btn btn-primary btn-sm" ID="lnkSearchAggrInvoices1" runat="server"
                            OnClick="lnkSearchAggrInvoices1_Click"><%=ResourceMgr.GetMessage("Search") %></cc1:ResourceLinkButton>
                    </div>

                </div>
            </div>



            <%-- Aggregative Invoices 2--%>
            <div class="ajaxModal-popup inmodal" id="AggrInvoices2" runat="server" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("Aggregative Invoices")%>
                        </h4>
                    </div>

                    <div class="modal-body">
                        <div role="form" class="row search-filter">
                            <asp:Label ID="lblEmailMsg" runat="server" Visible="false"></asp:Label><br />
                            <asp:GridView ID="gvAggrInvoice" runat="server" EmptyDataText="No data found" AutoGenerateColumns="false"
                                EmptyDataRowStyle-CssClass="alert alert-danger text-center" CssClass="table table-bordered epr-sec-table">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Invoice Number")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("InvoiceNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Organization Name")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("OrganizationForTo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Amount")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#  Eval("InvoiceAmount","{0:c}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Invoice Date")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Convert.ToDateTime(Eval("InvoiceDate")).ToShortDateString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Due Date")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Convert.ToDateTime(Eval("DueDate")).ToShortDateString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Status")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:Label ID="lnkApproved" runat="server"
                                                CssClass="badge badge-primary"
                                                Visible='<%#Convert.ToBoolean(Eval("IsPaid"))%>'>Invoice paid</asp:Label>

                                            <asp:Label ID="lnkRejected" runat="server"
                                                CssClass="badge badge-danger"
                                                Visible='<%#!Convert.ToBoolean(Eval("IsPaid"))%>'>Invoice not yet paid</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <small class="font-bold">
                            <asp:Label ID="lblTotalAmount" runat="server"></asp:Label><br />
                        </small>
                        <div class="row">
                            <div class="col-md-12">
                                <uc2:Pager ID="pgrAggregate" runat="server" />
                            </div>
                        </div>
                        <div id="dvSendEmail" runat="server" style="display: none">
                            <div style="display:none">
                                <label>Email ID:</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                                    CssClass="custom-block-error" Text="Please enter the email" ValidationGroup="email"></cc1:ResourceRequiredFieldValidator>
                                <cc1:ResourceRegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                    CssClass="custom-block-error" Text="Please enter correct email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="email" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                <br />
                                <cc1:ResourceLinkButton ID="btnSendEmail" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSendEmail_Click" ValidationGroup="email"
                                    CausesValidation="true">
                                <%=ResourceMgr.GetMessage("Send") %>
                                </cc1:ResourceLinkButton>
                                <%--<cc1:ResourceLinkButton ID="btnCancelSend" runat="server" CssClass="btn btn-white btn-sm" OnClientClick="dismissDiv()">
                                <%=ResourceMgr.GetMessage("Cancel") %>
                            </cc1:ResourceLinkButton>--%>
                            </div>
                            <label>Do you want to send email of these Invoices?</label>
                            <%--<cc1:ResourceLinkButton ID="btnSendEmail2" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSendEmail_Click"
                                    CausesValidation="false">
                                <%=ResourceMgr.GetMessage("Send") %>
                                </cc1:ResourceLinkButton>--%>
                            <asp:LinkButton ID="btnSendEmail2" runat="server" CssClass="btn btn-primary btn-xs btn-bitbucket" OnClick="btnSendEmail2_Click"
                                    CausesValidation="false"><i class="fa fa-send"></i></asp:LinkButton>
                            <a data-toggle="div" id="closedvSendEmail" onclick="dismissDiv()" class="btn btn-white btn-xs btn-bitbucket"
                                style="height: 22px"><i class="fa fa-close"></i></a>
                        </div>
                    </div>


                    <div class="modal-footer">
                        <cc1:ResourceLinkButton CssClass="btn btn-white btn-sm" ID="lnkBackAggrInvoices2" runat="server"
                            OnClick="lnkBackAggrInvoices2_Click"><%=ResourceMgr.GetMessage("Back") %></cc1:ResourceLinkButton>
                        <cc1:ResourceLinkButton CssClass="btn btn-white btn-sm" ID="lnkCloseAggrInvoices2" runat="server"
                            OnClick="lnkCloseAggrInvoices2_Click"><%=ResourceMgr.GetMessage("Close") %></cc1:ResourceLinkButton>

                        <cc1:ResourceLinkButton CssClass="btn btn-primary fa fa-print" ID="lnkPrint" runat="server"
                            OnClick="lnkPrint_Click" Style="height: 30px"><b><%=ResourceMgr.GetMessage("  Print") %></b></cc1:ResourceLinkButton>
                        <%--<cc1:ResourceLinkButton CssClass="btn btn-primary btn-sm" ID="lnkEmail" runat="server"
                            OnClick="lnkEmail_Click"><%=ResourceMgr.GetMessage("Email") %>
                            </cc1:ResourceLinkButton>--%>
                        <a data-toggle="div" id="opendvSendEmail" onclick="visibleDiv()" class="btn btn-primary fa fa-envelope"
                            style="height: 30px" runat="server">
                            <%= ResourceMgr.GetMessage("Email")%> </a>
                    </div>

                </div>
            </div>

            <asp:Label runat="server" ID="lblNoPrimaryBankAccountOrCreditCard" Visible="false" CssClass="custom-absolute-alert alert-danger"></asp:Label>
            <%-- Edit Invoice for payment--%>
            <div class="ajaxModal-popup inmodal" id="dvInvoicePaymentPopup" runat="server" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("Invoice Detail")%>
                        </h4>
                    </div>


                    <div class="modal-body">

                        <div role="form" class="row search-filter" id="">
                            <div class="form-group col-md-6">
                                <asp:Label ID="Label14" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Invoice ID:")%></asp:Label>
                                <asp:Label ID="lblInvoicePaymentId" runat="server"></asp:Label>
                                <asp:HiddenField runat="server" ID="hdnInvoiceId" />
                            </div>
                            <div class="form-group col-md-6">
                                <asp:Label ID="Label15" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Due Date")%></asp:Label>
                                <asp:Label ID="lblInvoicePaymentDueDate" runat="server"></asp:Label>
                            </div>

                            <div class="form-group col-md-6">
                                <asp:Label ID="Label16" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Invoice Date:")%></asp:Label>
                                <asp:Label ID="lblInvoicePaymentDate" runat="server"></asp:Label>
                            </div>
                            <div class="form-group col-md-6">
                                <asp:Label ID="Label17" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Invoice Amount:")%></asp:Label>
                                <asp:TextBox ID="txtValidation" runat="server" Visible="false"></asp:TextBox>
                                <asp:Label ID="lblInvoicePaymentAmount" runat="server"></asp:Label>
                            </div>

                            <div class="form-group col-md-6">
                                <asp:Label ID="Label18" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Balance Amount:")%></asp:Label>
                                <asp:Label ID="lblInvoicePaymentBalance" runat="server"></asp:Label>
                            </div>
                        </div>
                        <hr class="divider" />
                        
                        <div class="row" id="dvPaymentModeCheck" runat="server">


                            <div role="form" class="row search-filter col-md-12">

                                <div class="form-group col-md-6">
                                    <label>
                                        <%=ResourceMgr.GetMessage("Bank Name") %>
                                    </label>
                                    <asp:Label runat="server" ID="lblBankName"></asp:Label>
                                </div>

                                <div class="form-group col-md-6">
                                    <label>
                                        <%=ResourceMgr.GetMessage("Branch Name") %>
                                    </label>
                                    <asp:Label runat="server" ID="lblBranckName"></asp:Label>
                                </div>

                                <div class="form-group col-md-6">
                                    <label>
                                        <%=ResourceMgr.GetMessage("Account Title") %>
                                    </label>
                                    <asp:Label runat="server" ID="lblAccountTitle"></asp:Label>
                                </div>

                                <div class="form-group col-md-6">
                                    <label>
                                        <%=ResourceMgr.GetMessage("Account Number") %>
                                    </label>
                                    <asp:Label runat="server" ID="lblAccountNumber"></asp:Label>
                                </div>
                            </div>
                            <hr class="divider" />

                            <div class="row col-md-12">
                                <div class="form-group col-md-6">
                                    <label>
                                        <%=ResourceMgr.GetMessage("Check Number") %>
                                    </label>
                                    <asp:TextBox runat="server" ID="txtCheckNumber" CssClass="form-control" onkeypress="return ValidateNumberic(event);"  MaxLength="14"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator runat="server" ID="ResourceRequiredFieldValidator4" ControlToValidate="txtCheckNumber" ErrorMessage="Please Enter Check Number" CssClass="custom-error" ValidationGroup="Check"></cc1:ResourceRequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>
                                        <%=ResourceMgr.GetMessage("Payment Amount") %>
                                    </label>
                                    <asp:TextBox runat="server" ID="txtPaymentAmount" CssClass="form-control" onkeypress="return ValidateNumberic(event);" onkeyup="return DoubleDotValidation()" MaxLength="6"></asp:TextBox>
                                    <cc1:ResourceCustomValidator runat="server" ID="ResourceCustomValidator1" ControlToValidate="txtPaymentAmount" ClientValidationFunction="validateAmount" ValidationGroup="Check" ErrorMessage="Payment Amount should not be greater than invoice amount." CssClass="custom-error"></cc1:ResourceCustomValidator>
                                    <cc1:ResourceRequiredFieldValidator runat="server" ID="ResourceRequiredFieldValidator5" ControlToValidate="txtPaymentAmount" ErrorMessage="Please Enter Amount" CssClass="custom-error" ValidationGroup="Check"></cc1:ResourceRequiredFieldValidator>
                                    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <cc1:ResourceLinkButton CssClass="btn btn-white btn-sm" ID="btnInvoicePaymentCancel" runat="server"
                            OnClick="btnInvoicePaymentCancel_Click"><%=ResourceMgr.GetMessage("Cancel") %></cc1:ResourceLinkButton>
                        <cc1:ResourceLinkButton CssClass="btn btn-primary btn-sm" ID="btnInvoicePaymentCheck" runat="server" ValidationGroup="Check" 
                            OnClick="btnInvoicePaymentCheck_Click"><%=ResourceMgr.GetMessage("Pay") %></cc1:ResourceLinkButton>


                    </div>

                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnInvoiceSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>






</asp:Content>

