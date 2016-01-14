<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ViewStakeholder.aspx.cs" Inherits="Stakeholder_ViewStakeholder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function ClearSearchFields() {
            $("#<%=txtStakeholderName.ClientID%>").val('');
            $("#<%=txtCreatedFromDate.ClientID%>").val('');
            $("#<%=txtCreatedToDate.ClientID%>").val('');
            $("#<%=txtDBAName.ClientID%>").val('');
            $("#<%=txtPrimaryCotnact.ClientID%>").val('');
            $("#<%=txtZipCode.ClientID%>").val('');
            $("#<%=txtEmail.ClientID%>").val('');
            $('#<%=ddlStatus.ClientID%>').val('0');


            $("#<%=btnStakeSearch.ClientID%>")[0].click();
            $("#<%=btnStakeCancel.ClientID%>")[0].click();
        }

        $(document).ready(function () {
            SetHeaderMenu('liStakeholders', '<%=ResourceMgr.GetMessage("Stakeholders")%>');
        });
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

    <asp:Panel ID="pnlSearch" runat="server" CssClass="search-filter_inner" DefaultButton="btnStakeSearch">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("Search Filters")%> </h5>
                    </div>
                    <div class="ibox-content">
                        <asp:Literal ID="ltrlPage" runat="server"></asp:Literal>
                        <div role="form" class="row search-filter" id="">
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Company")%></label>
                                <asp:TextBox ID="txtStakeholderName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Email")%></label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div id="date_range">
                                <div class="input-daterange">
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("From")%></label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtCreatedFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("To")%></label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtCreatedToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("DBA Name")%></label>
                                <asp:TextBox ID="txtDBAName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Contact")%></label>
                                <asp:TextBox ID="txtPrimaryCotnact" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("ZIP Code")%></label>
                                <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Status")%></label>
                                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="All" Selected="True" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Approved" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div class="form-group col-md-12 mb0">
                                <cc1:ResourceLinkButton ID="btnStakeSearch" runat="server" CssClass="btn btn-primary  btn-sm font-bold"
                                    OnClick="btnStakeSearch_Click"><%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                <cc1:ResourceLinkButton ID="btnStakeCancel" runat="server" CssClass="btn btn-white btn-sm font-bold"
                                    OnClientClick="ClearSearchFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>



    <asp:UpdatePanel runat="server" ID="upnlGrid" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Stakeholders")%></h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvApplicationApproved" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            CssClass="table table-bordered epr-sec-table" DataKeyNames="OrganizationId" EnableViewState="true"
                                            EmptyDataText="No data found" OnRowCommand="gvApplicationApproved_RowCommand" OnRowDataBound="gvLatestSteward_RowDataBound">
                                            <AlternatingRowStyle CssClass="highlighted-row" />
                                            <HeaderStyle CssClass="txt-had" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderStyle />
                                                    <ItemStyle />
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Company")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("LegalName") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderStyle />
                                                    <ItemStyle />
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Contact")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ContactName") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderStyle />
                                                    <ItemStyle />
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Phone")%> <%=ResourceMgr.GetMessage("(Extension)")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a title="Click to call" href="skype:'<%# Eval("Number") %> '?call"><%# Eval("Number") %> </a>
                                                        (<%# Eval("Extension") %>)
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderStyle />
                                                    <ItemStyle />
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Email")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Email") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">

                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Status")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("intStatus") %>' />
                                                        <asp:Label runat="server" ID="status"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="90" ItemStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a title="View User" href='Users?p=1&OrganizationId=<%# Eval("OrganizationId") %>&OrganizationTypeID=<%#Eval("OrganizationTypeId")%>' class="btn btn-white btn-bitbucket">
                                                            <i class="fa fa-user"></i></a>
                                                        <a title="View & Manage Stakeholder Information" href='ViewStakeholderDetail?OrganizationId=<%# Eval("OrganizationId") %>&PageId=1' class="btn btn-white btn-bitbucket">
                                                            <i class="fa fa-list"></i></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <uc2:Pager ID="pager" runat="server" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnStakeSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


