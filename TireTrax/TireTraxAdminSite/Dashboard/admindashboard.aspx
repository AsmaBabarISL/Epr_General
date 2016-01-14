<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="admindashboard.aspx.cs" Inherits="Dashboard_admindashboard" %>

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
    <asp:UpdatePanel ID="upnlsearch" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-9">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Pending Stewards")%></h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvOrganizationAlert" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered epr-sec-table no-margins" DataKeyNames="OrganizationId" EnableViewState="true"
                                            EmptyDataText="No data found" OnRowCommand="gvOrganizationAlert_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="250px">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Company")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%#  Eval("LegalName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="250px">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Type(s)")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%# Eval("LookupTypeName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="250px">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEffactiveDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StakeholderDate")).ToShortDateString() %>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnApprove" runat="server" ToolTip="Approve this record" OnClientClick="return confirm('Are you sure you want to Approve Pending Steward?');" CommandName="Approve" CommandArgument='<%# Bind("OrganizationId") %>'>
                                                            <i class="fa fa-check"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("EPR User Name")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%# Eval("Login")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Pending Stewardships")%></h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvLatestSteward" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table no-margins"
                                            DataKeyNames="OrganizationId" EnableViewState="true" EmptyDataText="No data found" OnRowCommand="gvLatestSteward_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Company")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("LegalName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Type(s)")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%# Eval("LookupTypeName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEffactiveDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StewardshipDate")).ToShortDateString() %>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-Width="150px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnApprove" runat="server" ToolTip="Approve this record" OnClientClick="return confirm('Are you sure you want to Approve Pending Steward?');" CommandName="Approve" CommandArgument='<%# Eval("OrganizationId") %>'>
                                        <i class="fa fa-check"></i>
                                                        </asp:LinkButton>
                                                        <a href='/Stewardship/ViewDetailStewardship.aspx?OrganizationId=<%# Eval("OrganizationId") %>' title="View Details" class="btn btn-white btn-bitbucket">
                                                            <i class="fa fa-list"></i>
                                                        </a>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("EPR User Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Login")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Pending Stakeholder")%></h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvLatestStakeholder" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered epr-sec-table no-margins" DataKeyNames="OrganizationId"
                                            EnableViewState="true" EmptyDataText="No data found" wrap="nowrap"
                                            OnRowCommand="gvLatestStakeholder_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Company")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%# Eval("LegalName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Contact")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <%# Eval("StakeholderName")%>
                                                    </ItemTemplate>


                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Type(s)")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <%# Eval("LookupTypeName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEffactiveDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StakeholderDate")).ToShortDateString() %>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnApprove" runat="server" ToolTip="Approve this record" OnClientClick="return confirm('Are you sure you want to Approve Pending Stakeholder?');" CommandName="Approve" CommandArgument='<%# Bind("OrganizationId") %>'><i class="fa fa-check"></i></asp:LinkButton>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("EPR User Name")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <%# Eval("Login")%>
                                                    </ItemTemplate>


                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Pending Government Agency")%></h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvGovernmentAgency" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table no-margins" DataKeyNames="OrganizationId" EnableViewState="true" EmptyDataText="No data found" OnRowCommand="gvGovernmentAgency_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Company")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <%# Eval("LegalName")%>
                                                    </ItemTemplate>


                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Type(s)")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <%# Eval("LookupTypeName")%>
                                                    </ItemTemplate>


                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEffactiveDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StakeholderDate")).ToShortDateString() %>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnApprove" runat="server" ToolTip="Approve this record" OnClientClick="return confirm('Are you sure you want to Approve Pending Government Agency?');" CommandName="Approve" CommandArgument='<%# Bind("OrganizationId") %>'>
                                        <i class="fa fa-check"></i> 
                                                        </asp:LinkButton>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("EPR User Name")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%# Eval("Login")%>
                                                    </ItemTemplate>


                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Pending Law Enforcement Agency")%></h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvLawEnforcement" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table no-margins"
                                            DataKeyNames="OrganizationId" EnableViewState="true" EmptyDataText="No data found" OnRowCommand="gvLawEnforcement_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Company")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <%# Eval("LegalName")%>
                                                    </ItemTemplate>


                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Type(s)")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%# Eval("LookupTypeName")%>
                                                    </ItemTemplate>


                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="250px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEffactiveDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StakeholderDate")).ToShortDateString() %>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnApprove" runat="server" ToolTip="Approve this record" OnClientClick="return confirm('Are you sure you want to Approve Pending Law Enforcement Agency?');" CommandName="Approve" CommandArgument='<%# Bind("OrganizationId") %>'>
                                        <i class="fa fa-check"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("EPR User Name")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <%# Eval("Login")%>
                                                    </ItemTemplate>


                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Pending ProductTypes")%></h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvPendingProducts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table no-margins"
                                            EmptyDataText="No data found" EmptyDataRowStyle-CssClass="alert alert-danger text-center" OnRowCommand="gvPendingProducts_RowCommand">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Company")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("LegalName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Product")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("CategoryName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Sub Category")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ProductSubCat") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <HeaderTemplate>

                                                        <%= ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnApprove" runat="server" ToolTip="Approve this record" CommandName="Approve" CommandArgument='<%# Bind("CategoryId") %>'>
                                        <i class="fa fa-check"></i>
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
                <div class="col-lg-3">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <span class="label label-success pull-right">Monthly</span>
                                    <h5>Income</h5>
                                </div>
                                <div class="ibox-content">
                                    <h1 class="no-margins">40 886,200</h1>
                                    <div class="stat-percent font-bold text-success">98% <i class="fa fa-bolt"></i></div>
                                    <small>Total income</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <span class="label label-info pull-right">Annual</span>
                                    <h5>Orders</h5>
                                </div>
                                <div class="ibox-content">
                                    <h1 class="no-margins">275,800</h1>
                                    <div class="stat-percent font-bold text-info">20% <i class="fa fa-level-up"></i></div>
                                    <small>New orders</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <span class="label label-primary pull-right">Today</span>
                                    <h5>Visits</h5>
                                </div>
                                <div class="ibox-content">
                                    <h1 class="no-margins">106,120</h1>
                                    <div class="stat-percent font-bold text-navy">44% <i class="fa fa-level-up"></i></div>
                                    <small>New visits</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <span class="label label-danger pull-right">Low value</span>
                                    <h5>User activity</h5>
                                </div>
                                <div class="ibox-content">
                                    <h1 class="no-margins">80,600</h1>
                                    <div class="stat-percent font-bold text-danger">38% <i class="fa fa-level-down"></i></div>
                                    <small>In first month</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12" style="display: none;">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Averages</h5>
                                </div>
                                <div class="ibox-content">
                                    <small>S&P 500 Stocks Furthest Above and Below their 50-200 Day Moving</small>
                                    <table class="inventory-grid" width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <span class="style1">%From</span>
                                            </td>
                                            <td>
                                                <span class="style1">%From</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="style3">Stock</span>
                                            </td>
                                            <td>
                                                <span class="style3">Price</span>
                                            </td>
                                            <td>
                                                <span class="style3">50-Day</span>
                                            </td>
                                            <td>
                                                <span class="style3">200-Day</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>GT
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>70.02
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RSH
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>90.14
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>GT
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>70.02
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RSH
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>90.14
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>GT
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>70.02
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RSH
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>90.14
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>GT
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>70.02
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RSH
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>90.14
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>GT
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>70.02
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RSH
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>90.14
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>GT
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>70.02
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RSH
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>90.14
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>GT
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>20.02
                                            </td>
                                            <td>70.02
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RSH
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>40.57
                                            </td>
                                            <td>90.14
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5><%= ResourceMgr.GetMessage("EPR Technology Solution")%></h5>
                                </div>
                                <div class="ibox-content ibox-heading">
                                    <h3 class="mb0"><i class="fa fa-refresh"></i>Updates</h3>
                                </div>
                                <div class="ibox-content">
                                    <div class="feed-activity-list">
                                        <div class="feed-element pb0">
                                            <div>
                                                <div>The GreenEquation.org's launches December 1, 2014. Earning Certified Carbon Credits begins.</div>
                                                <a href="#"><small class="pull-right text-navy"><%= ResourceMgr.GetMessage("view details")%></small></a>
                                            </div>
                                        </div>

                                        <div class="feed-element pb0">
                                            <div>
                                                <div>EPRTechnologySolutionsEX.com welcomes 553 new Japanese Green Recycling partners in December 2014.</div>
                                                <a href="#"><small class="pull-right text-navy"><%= ResourceMgr.GetMessage("view details")%></small></a>
                                            </div>
                                        </div>

                                        <div class="feed-element pb0">
                                            <div>
                                                <div>EPRTechnologySolutionsEX.com  553 new Japanese Green Recycling partners in December 2014.</div>
                                                <a href="#"><small class="pull-right text-navy"><%= ResourceMgr.GetMessage("view details")%></small></a>
                                            </div>
                                        </div>

                                        <div class="feed-element pb0">
                                            <div>
                                                <div>
                                                    Happy 30th Birthday to the Tire Stewardship of British
                    Columbia. Thanks for the 3 decades of Keeping it Green!
                                                </div>
                                                <a href="#"><small class="pull-right text-navy"><%= ResourceMgr.GetMessage("view details")%></small></a>
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
    </asp:UpdatePanel>
</asp:Content>
