<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewAccounting.aspx.cs" Inherits="Accounting_ViewAccounting" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script type="text/javascript">

        function ClearFields() {
            $("#<%=txtLegalName.ClientID%>").val('');
            $("#<%=txtUserName.ClientID%>").val('');
            $("#<%=txtLocation.ClientID%>").val('');
            $("#<%=btnAccountingSearch.ClientID%>")[0].click();
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

    <div class="grid-contain-outer">
        <asp:UpdatePanel ID="upnlGrid" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <asp:Panel ID="pnlSearch" runat="server">
                                <div class="ibox-title">
                                    <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                                </div>

                                <div class="ibox-content">
                                    <div class="row search-filter" id="">
                                        <div class="form-group col-sm-6 col-md-4 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Organization Name")%></label>
                                            <asp:TextBox ID="txtLegalName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-sm-6 col-md-4 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("User Name")%></label>
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-sm-6 col-md-4 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Location")%></label>
                                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group mb0 col-sm-12">
                                            <cc1:ResourceLinkButton ID="btnAccountingSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                                OnClick="btnAccountingSearch_Click"><%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                            <cc1:ResourceLinkButton ID="btnAccountingCancel" runat="server" CssClass="btn btn-sm btn-white font-bold" OnClientClick="ClearFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Accounting")%></h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">


                                            <asp:GridView ID="gvAccounting" runat="server" GridLines="None" AutoGenerateColumns="false"
                                                CssClass="table table-bordered epr-sec-table" CellPadding="0" CellSpacing="0" BorderWidth="0" Width="100%"
                                                EmptyDataText="No data found" wrap="nowrap" DataKeyNames="TransactionId"
                                                AllowPaging="true" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                                PageSize="15">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="">

                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("User Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Login")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">

                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Organization")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("LegalName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">

                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Amount")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Convert.ToDecimal(Eval("Amount")).ToString("C")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">

                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Location")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <%# Eval("Location")%>
                                                        </ItemTemplate>



                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Date")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffactiveDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DateCreated")).ToShortDateString() %>'> </asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">

                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Currency")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("CurrencyName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:DropDownList runat="server" ID="ddlPageSize" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                            <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                            <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                            <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%=ResourceMgr.GetMessage("Records Per Page")%>
                                        <asp:Label ID="lblPagingLeft" runat="server" CssClass="m-l-sm"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Literal ID="ltrlPaging" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
           
        </asp:UpdatePanel>
    </div>


</asp:Content>

