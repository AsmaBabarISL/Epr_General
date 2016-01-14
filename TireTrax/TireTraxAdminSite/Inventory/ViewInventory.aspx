<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewInventory.aspx.cs" Inherits="Inventory_ViewInventory" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="/Scripts/jquery-1.4.1.min.js"></script>
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#asearch').toggle(function () {
                $('#midSearch').slideDown();
            }, function () {
                $('#midSearch').slideUp();
            });
        });

        function ClearFields() {
            $("#<%=txtInventoryTX_Barcode.ClientID%>").val('');
            $("#<%=txtInventoryStewardship.ClientID%>").val('');
            $("#<%=txtPlantCode.ClientID%>").val('');
            $("#<%=txtSizeCode.ClientID%>").val('');
            $("#<%=ddlTireState.ClientID%>").val('0');

            $("#<%=btnInventorySearch.ClientID%>")[0].click();
        }

        function ToggleChilds(checked) {
            if (checked) {
                $("input[type=checkbox]", $("#<%=gvAdminInventory.ClientID%> td")).attr("checked", "checked");
            }
            else {
                $("input[type=checkbox]", $("#<%=gvAdminInventory.ClientID%> td")).removeAttr("checked");
            }
        }

        function CheckParent() {
            if ($("input[type=checkbox]", "#<%=gvAdminInventory.ClientID%> td").length == $("input:checked[type=checkbox]", "#<%=gvAdminInventory.ClientID%> td").length) {
                $("#chkhead").attr("checked", "checked");
            }
            else {
                $("#chkhead").removeAttr("checked");
            }
        }

        function PrintBarcodes() {
            var BarCodes = "";
            $("input:checked[type=checkbox]", "#<%=gvAdminInventory.ClientID%> td").each(function () {
                BarCodes = BarCodes + $(this).val() + ',';
            });
            if (BarCodes == "") {
                alert("Please select atleast one code to print.");
            }
            else {
                BarCodes = BarCodes.substring(0, BarCodes.length - 1);
                window.open("PrintBarCode.aspx?BarCodeIds=" + BarCodes);
            }
        }
    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <!-- Start Search Filters --------------------------------------------------------------------->
    <div class="grid-contain-outer">
        <div class="txt-main-had" style="height: 25px;">
            <div class="txt-had-left">
                <%= ResourceMgr.GetMessage("Inventory")%>
            </div>
            <div class="txt-had-right" style="padding-right:0px;">
                <a class="ico_add" href="addInventory.aspx">
                    <%= ResourceMgr.GetMessage("Add New Inventory")%>
                </a>
                <a class="ico_search" id="asearch" href="javascript:void();">
                    <%= ResourceMgr.GetMessage("Search")%>
                </a>
                <a class="ico_print" href="javascript:PrintBarcodes();">
                    <%= ResourceMgr.GetMessage("Print Bar Code(s)")%>
                </a>
            </div>
        </div>
        <div id="midSearch" style="display: none;">
            <div class="search-filter-outer">
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnInventorySearch" CssClass="search-filter_inner">
                    <div class="search-filter_heading">
                        <%= ResourceMgr.GetMessage("Search Filters")%>
                    </div>
                    <div class="search-filter-content-outer">
                        <div class="content-txt">
                            <%= ResourceMgr.GetMessage("Serial Number")%>
                        </div>
                        <div class="content-field">
                            <asp:TextBox ID="txtInventoryTX_Barcode" runat="server" CssClass="txt-field"></asp:TextBox>
                        </div>
                    </div>
                    <div class="search-filter-content-outer">
                        <div class="content-txt">
                            <%= ResourceMgr.GetMessage("Organization")%>
                        </div>
                        <div class="content-field">
                            <asp:TextBox ID="txtInventoryStewardship" runat="server" CssClass="txt-field"></asp:TextBox>
                        </div>
                    </div>
                    <div class="search-filter-content-outer">
                        <div class="content-txt">
                            <%= ResourceMgr.GetMessage("Plant Code")%>
                        </div>
                        <div class="content-field">
                            <asp:TextBox ID="txtPlantCode" runat="server" CssClass="txt-field"></asp:TextBox>
                        </div>
                    </div>
                    <div class="search-filter-content-outer">
                        <div class="content-txt">
                            <%= ResourceMgr.GetMessage("Size Code")%>
                        </div>
                        <div class="content-field">
                            <asp:TextBox ID="txtSizeCode" runat="server" CssClass="txt-field"></asp:TextBox>
                        </div>
                    </div>
                    <div class="search-filter-content-outer">
                        <div class="content-txt">
                            <%= ResourceMgr.GetMessage("Tire State")%>
                        </div>
                        <div class="content-field">
                            <asp:DropDownList ID="ddlTireState" runat="server">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Recycle" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Retread" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="btn-search_outer">
                        <cc1:ResourceLinkButton ID="btnInventoryCancel" runat="server" CssClass="btn-search"
                            OnClientClick="ClearFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                        <cc1:ResourceLinkButton ID="btnInventorySearch" runat="server" CssClass="btn-search"
                            OnClick="btnInventorySearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvAdminInventory" AutoGenerateColumns="False" GridLines="None"
                    CssClass="add-new-inventory" EnableViewState="true" EmptyDataText="No data found"
                    wrap="nowrap" CellPadding="0" Width="100%" runat="server" OnRowCommand="gvAdminInventory_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <input type="checkbox" id="chkhead" onclick="ToggleChilds(this.checked);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type="checkbox" value='<%#Eval("barCodeId") %>' onclick="CheckParent();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Serial Number")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("SerialNumber")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Organization")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("LegalName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Plant Code")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("PlantNumber")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Size Code")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("SizeNumber")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Week")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("MonthCode")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Year")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                20<%#Eval("YearCode")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Tire State")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("TireState")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("State Description")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("TireStateDescription")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Timeline")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnTimeline" Visible="false" ToolTip="Inventory Timeline" ImageUrl="../Images/timeline_icon.png" runat="server">
                                </asp:ImageButton>
                                <asp:ImageButton ID="ImageButton1" CommandArgument='<%# Eval("SerialNumber")%>' CommandName="Timeline" ImageUrl="../Images/timeline_icon.png" runat="server"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Revenue")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a href='adminInventoryRevenue.aspx?TireId=<%# Eval("TireId") %>'> <img title="Inventory Revenue" src="/Images/revenue_icon.png" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="txt-pagination">
                    <div class="pagination-left" style="margin-top: 9px;">
                        <asp:DropDownList runat="server" ID="ddlPageSize" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="75" Value="75"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            <asp:ListItem Text="250" Value="250"></asp:ListItem>
                            <asp:ListItem Text="500" Value="500"></asp:ListItem>
                        </asp:DropDownList>
                        <%=ResourceMgr.GetMessage("Records Per Page")%>
                        <asp:Label ID="lblPagingLeft" runat="server" style="padding-left:10px;"></asp:Label>
                    </div>
                    <div class="pagination-right">
                        <asp:Literal ID="ltrlPaging" runat="server"></asp:Literal>
                    </div>
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnInventorySearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>




</asp:Content>

