<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewLoads.aspx.cs" Inherits="Loads_ViewLoads" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript">
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
         Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>

    <div class="row" style="display:none;">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnInventorySearch">
                    <div class="ibox-title">
                    <h5> <%= ResourceMgr.GetMessage("Search Filters")%> </h5>
                </div>
                <div class="ibox-content">
                    <div role="form" class="row search-filter" id="">
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("Search Type")%> </label>
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="LOTS" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="LOADS" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("User Name")%> </label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("Quantity")%> </label>
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" onkeypress="return isNumeric(event);"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("DOT#")%> </label>
                            <asp:TextBox ID="txtDOT" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("Lane")%> </label>
                            <asp:TextBox ID="txtLane" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("Space")%> </label>
                            <asp:TextBox ID="txtSpace" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("From Date")%> </label>
                            <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("To Date")%> </label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("Latitude")%> </label>
                            <asp:TextBox ID="txtLatitude" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("Longitude")%> </label>
                            <asp:TextBox ID="txtLongitude" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label> <%= ResourceMgr.GetMessage("GUID")%> </label>
                            <asp:TextBox ID="txtGuid" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4 col-sm-6 col-lg-3" style="display:none;">
                            <label> <%= ResourceMgr.GetMessage("Tire State")%> </label>
                            <asp:DropDownList ID="ddlTireState" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Recycle" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Retread" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-12 mb0">
                            <cc1:ResourceLinkButton ID="btnInventorySearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                            OnClick="btnSearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                            <cc1:ResourceLinkButton ID="btnInventoryCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                            OnClientClick="ClearSearchFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                        </div>
                    </div>
                </div>


 

                    <div class="btn-search_outer">
                        
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("LOADS")%></h5>
                            <div class="ibox-tools">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlLoadStatus" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlLoadStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvloadsinfo" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data found"
                                            runat="server" OnRowCommand="gvloadsinfo_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Load Number")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblLoadNumber" runat="server" Text='<%# Eval("LoadNumber")%>'> </asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Quantity")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Quantity")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("LegalName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Type")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("LookupTypeName")%>
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
                                                <asp:TemplateField HeaderText="View Tires">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="lnkbtnLoadNumber" runat="server" ToolTip="View Load Tires" CommandArgument='<%# Eval("LoadId") %>' CommandName="LoadTireInfo">
                                <i class="fa fa-eye"></i>
                                                        </asp:LinkButton>
                                                        <asp:HiddenField ID="hdLoadNumber" runat="server" Value='<%# Eval("LoadNumber") %>' />
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

            <div id="dvMainLoad" runat="server" class="" visible="false">
                <div class="ajaxModal-popup inmodal">
                    <div class="ajaxModal-body animated bounceInRight">
                        <div id="dvLoadSummaryInfo" runat="server">
                            <div class="textTitle">
                                <%= ResourceMgr.GetMessage("Load Tires Info")%>
                            </div>
                            <div>
                                <asp:Label ID="lblLotlabelLane" runat="server" Text="Load Number:"></asp:Label>
                                <asp:Label ID="lblLoadNumber" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div>
                            <asp:Label ID="lbltirecount" runat="server" Text="Load Tire Count:"></asp:Label>
                            <asp:Label ID="lblLoadTireCount" runat="server"></asp:Label>
                        </div>
                        <div id="dvLoadTireInfo" runat="server">
                            <asp:GridView ID="grvLoadTireInfo" AutoGenerateColumns="False" GridLines="None"
                                CssClass="add-new-inventory" EnableViewState="true" EmptyDataText="No data available"
                                wrap="nowrap" CellPadding="0" Width="100%" runat="server">
                                <Columns>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Load ID#")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("SerialNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Load Name")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("LoadNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Organization")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("LegalName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Quantity")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("Quantity")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("DOT Number")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("DOTNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnInventorySearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

