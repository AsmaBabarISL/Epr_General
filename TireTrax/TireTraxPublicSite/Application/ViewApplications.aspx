<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ViewApplications.aspx.cs" Inherits="Application_ViewApplications" %>

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

            $("#<%=btnStakeSearch.ClientID%>")[0].click();
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

    <!-- Start Search Filters --------------------------------------------------------------------->
    <asp:Panel ID="pnlSearch" runat="server" CssClass="search-filter_inner" DefaultButton="btnStakeSearch">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("Search Application")%> </h5>
                    </div>
                    <div class="ibox-content">
                        <div role="form" class="row search-filter" id="">

                            <asp:Literal ID="ltrlPage" runat="server"></asp:Literal>

                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Company")%></label>
                                <asp:TextBox ID="txtStakeholderName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div id="date_range">
                                <div class="input-daterange">
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("From ")%></label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtCreatedFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("To ")%></label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtCreatedToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Email")%></label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3 clear-both">
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



                            <div class="form-group col-md-12 mb0">
                                <cc1:ResourceLinkButton ID="btnStakeSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                    OnClick="btnStakeSearch_Click"><%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                <cc1:ResourceLinkButton ID="btnStakeCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
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
                            <h5><%= ResourceMgr.GetMessage("Application")%></h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">

                                        <asp:GridView ID="gvApplicationNotApproved" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            CssClass="table table-bordered epr-sec-table" DataKeyNames="OrganizationId" EnableViewState="true"
                                            EmptyDataText="No data found" OnRowCommand="gvApplicationNotApproved_RowCommand">

                                            <Columns>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Company")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("LegalName") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Contact Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ContactName") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="30" ItemStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("View Application")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a class="btn btn-white btn-bitbucket" href='ViewStakeholderDetail?OrganizationId=<%# Eval("OrganizationId") %>&PageId=2'>
                                                            <i class="fa fa-eye"></i></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnStakeSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
