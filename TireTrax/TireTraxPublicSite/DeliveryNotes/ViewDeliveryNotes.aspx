<%@ Page Title="View Delivery Notes" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ViewDeliveryNotes.aspx.cs" Inherits="DeliveryNotes_ViewDeliveryNotes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ClearSearchFields() {
            $("#<%=txtShipTo.ClientID%>").val('');
            $("#<%=txtFrmDeliveryDate.ClientID%>").val('');
            $("#<%=txtToDeliveryDate.ClientID%>").val('');
            $("#<%=txtDeliveryName.ClientID%>").val('');
            $("#<%=btnDeliverySearch.ClientID%>")[0].click();
        }
        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("body");
            $(".ajax-loader").remove();
        }

        function AjaxLoader() {
            $(".ajax-loader").appendTo("body");
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
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                </div>
                <div class="ibox-content">
                    <div id="midSearch" class="row search-filter">
                        <div class="search-filter-outer" style="height: auto;">
                            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnDeliverySearch">
                                <div class="form-group col-md-4 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Delivery Name")%></label>
                                    <asp:TextBox ID="txtDeliveryName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group col-md-4 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Ship To")%></label>
                                    <asp:TextBox ID="txtShipTo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div id="date_range">
                                    <div class="input-daterange">
                                        <div class="form-group col-md-4 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("From")%></label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFrmDeliveryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-4 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("To")%></label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtToDeliveryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div class="form-group col-md-12 mb0">
                                    <cc1:ResourceLinkButton ID="btnDeliverySearch" runat="server" CssClass="btn btn-sm btn-primary font-bold" OnClick="btnSearch_Click"> 
                                        <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                    <cc1:ResourceLinkButton ID="btnDeliveryCancel" runat="server" CssClass="btn btn-sm btn-white font-bold" OnClientClick="ClearSearchFields(); return false;">
                                         <%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                                </div>


                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Delivery Notes</h5>
                            <div class="ibox-tools">
                                <div class="form-group">
                                    <cc1:ResourceLinkButton CssClass="btn btn-sm btn-primary" ID="dvadddeliverynote" runat="server" OnClick="btnadddeliverynote_Click">
                                <i class="fa fa-plus"></i>
                    <strong><%= ResourceMgr.GetMessage("Add Delivery Notes")%></strong></cc1:ResourceLinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDeliveryinfo" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table"
                                            EnableViewState="true" EmptyDataText="No data found" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            runat="server" OnRowCommand="gvDeliveryinfo_RowCommand" OnRowDataBound="gvDeliveryinfo_RowDataBound">
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("DeliveryID")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("DeliveryID")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("DeliveryName")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("DeliveryName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("ShipTo")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("OrganizationShipTo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Transporter")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("OrganizationTransporter")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("DeliveryDate")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("DeliveryDate")).ToShortDateString()%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Vehicle Detail")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("vehicleDetails")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Status")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="imgApproved" runat="server" Visible="false" CssClass="badge badge-primary"> 
                                                    Delivery Accepted
                                                        </asp:Label>

                                                        <asp:Label ID="imgRejected" runat="server" Visible="false" CssClass="badge badge-danger">
                                                    Delivery Rejected
                                                        </asp:Label>

                                                        <asp:Label ID="imgPending" runat="server" Visible="false" CssClass="badge badge-warning-light">
                                                    Waiting for Delivery
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="90" ItemStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        Actions
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width: 75px;">
                                                            <asp:LinkButton ID="ImgbtnDeliveryID" CssClass="btn btn-white btn-bitbucket" runat="server" ToolTip="View Delivery"
                                                                CommandArgument='<%# Eval("DeliveryID") %>' CommandName="DeliveryInfo">
                                                        <i class="fa fa-eye"></i>
                                                            </asp:LinkButton>


                                                            <asp:LinkButton ID="imgbtnEditLoad" CssClass="btn btn-white btn-bitbucket" runat="server" ToolTip="Edit Delivery Info" CommandName="Edit"
                                                                CommandArgument='<%# Bind("DeliveryID")%>'>
                                                        <i class="fa fa-pencil"></i>
                                                            </asp:LinkButton>
                                                        </div>
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


                            <!-- Modal Popup -->



                        </div>
                    </div>
                </div>
            </div>



            <div id="dvMainLoad" runat="server" class="ajaxModal-popup inmodal" visible="false">
                <div class="ajaxModal-body animated bounceInRight">

                    <div id="dvLoadSummaryInfo" runat="server" class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("Delivery Notes Info")%>
                        </h4>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <dl class="dl-horizontal">
                                    <dt runat="server" visible="false">
                                        <asp:Label ID="Label3" runat="server"><%= ResourceMgr.GetMessage("Delivery Notes ID:")%></asp:Label></dt>
                                    <dd runat="server" visible="false">
                                        <asp:Label ID="lblDeliveryID" runat="server"></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="lblDeNam" runat="server"><%= ResourceMgr.GetMessage("Delivery Notes Name:")%></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblDeliveryName" runat="server"></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="lbltirecount" runat="server"><%= ResourceMgr.GetMessage("Delivery Date:")%></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblDeliveryDate" runat="server" CssClass=""></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label2" runat="server"><%= ResourceMgr.GetMessage("Delivery Estimate:")%></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblDeliveryEstimaDateTime" runat="server" CssClass=""></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label4" runat="server"><%= ResourceMgr.GetMessage("Ship To:")%></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblShipToidnumber" runat="server" CssClass=""></asp:Label></dd>

                                </dl>
                            </div>
                            <div class="col-md-6">
                                <dl class="dl-horizontal">
                                    <dt>
                                        <asp:Label ID="Label5" runat="server"><%= ResourceMgr.GetMessage("Vehicle Details:")%></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblVehicleDetails" runat="server" CssClass=""></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label6" runat="server"><%= ResourceMgr.GetMessage("Weight:")%></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblWeight" runat="server" CssClass=""></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label1" runat="server" Text="Load Type:"><%= ResourceMgr.GetMessage("Transporter:")%></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblTransporterName" runat="server"></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label7" runat="server" Text="Load Type:"><%= ResourceMgr.GetMessage("Total Loads:")%></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblTotalLoads" runat="server"></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label8" runat="server" Text="Load Type:" Visible ="false"><%= ResourceMgr.GetMessage("Total Tires:")%></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblLoadTireCount" runat="server" Visible ="false"></asp:Label></dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <div id="dvLoadTireInfo" runat="server">
                                        <asp:GridView ID="gvAllTire" AutoGenerateColumns="False" DataKeyNames="ProductId" CssClass="table table-bordered epr-sec-table" EnableViewState="true"
                                            EmptyDataText="No data Available" EmptyDataRowStyle-CssClass="alert alert-danger text-center" runat="server" Visible="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("ID")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ProductId")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Brand")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Brand")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Size")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ProductSize")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Week")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("MonthCode")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Year")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("YearCode")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("DOT")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="AllTireDotNumber" runat="server" Text='<%#Eval("DOTNumber")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("SKU")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("TireSerialNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:GridView ID="gvAllProdut" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table mb0"
                                            EnableViewState="true" EmptyDataText="No data available" runat="server"
                                            EmptyDataRowStyle-CssClass="alert alert-danger text-center" Visible="false">
                                            <Columns>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Product Serial #")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("SerialNumber").ToString().Split('.')[0]%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Company")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("LegalName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Size")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Size")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Shape")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Shape")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Material")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Material")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <cc1:ResourceLinkButton class="" ID="ResourceLinkButton2" runat="server" CssClass="btn btn-white btn-sm" OnClick="btnDeliveryDetailBack_Click">Back</cc1:ResourceLinkButton>
                    </div>
                    <asp:HiddenField ID="hdnlotid" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnDeliverySearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
