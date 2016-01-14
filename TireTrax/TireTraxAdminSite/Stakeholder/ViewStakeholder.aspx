<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewStakeholder.aspx.cs" Inherits="Stakeholder_ViewStakeholder" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        function ClearSearchFields() {
            $("#<%=txtStakeholderName.ClientID%>").val('');
            $("#<%=txtCreatedFromDate.ClientID%>").val('');
            $("#<%=txtCreatedToDate.ClientID%>").val('');
            $("#<%=txtDBAName.ClientID%>").val('');
            $("#<%=txtPrimaryCotnact.ClientID%>").val('');
            $("#<%=txtZipCode.ClientID%>").val('');

            $("#<%=btnStakeSearch.ClientID%>")[0].click();
        }

        function ListPaging(pageNo) {
            $('.sort-loading').show();
            var ListData = new Array();
            var pagingData = new Array();
            $.post("/ajax/get.ashx", {
                type: '<%=Convert.ToInt32(TireTraxLib.AJAXGet.StakeHolders) %>', pageNum: pageNo, uid: '<%= Request.QueryString["uid"] %>'
            }
            , function (data) {
                $('.sort-loading').hide();
                ListData = data.split('$$$');
                $('#<%= gvApplicationApproved.ClientID %>').html(ListData[0]);
                pagingData = ListData[1].split('###');

                $('#<%= lblApprovalStakeholder.ClientID %>').html(pagingData[1]);
                $('#<%= ltrApprovalStakeholder.ClientID %>').html(pagingData[0]);
            });
        }

        function SetFilterDateRange() {
            $("#<%=txtCreatedFromDate.ClientID %>").datepicker({
                numberOfMonths: 1,
                onSelect: function (selectedDate) {
                    $("#<%=txtCreatedToDate.ClientID %>").datepicker("option", "minDate", selectedDate);
                },
                onClose: function (dateText, inst) {
                    if (dateText == "") {
                        $("#<%=txtCreatedToDate.ClientID %>").datepicker("option", "minDate", null);
                    }
                }
            });
            $("#<%=txtCreatedToDate.ClientID %>").datepicker({
                numberOfMonths: 1,
                onSelect: function (selectedDate) {
                    $("#<%=txtCreatedFromDate.ClientID %>").datepicker("option", "maxDate", selectedDate);
                },
                onClose: function (dateText, inst) {
                    if (dateText == "") {
                        $("#<%=txtCreatedFromDate.ClientID %>").datepicker("option", "maxDate", null);
                    }
                }
            });
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

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <asp:Panel ID="pnlSearch" runat="server" CssClass="search-filter_inner" DefaultButton="btnStakeSearch">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                    </div>
                    <asp:Literal ID="ltrlPage" runat="server"></asp:Literal>
                    <div class="ibox-content">
                        <div class="row search-filter" id="">

                            <div class="form-group col-md-3">
                                <label><%= ResourceMgr.GetMessage("Organization Name")%></label>
                                <asp:TextBox ID="txtStakeholderName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div id="date_range">
                                <div class="input-daterange">

                                    <div class="form-group col-md-3">
                                        <label><%= ResourceMgr.GetMessage("Created From Date")%></label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtCreatedFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><%= ResourceMgr.GetMessage("Created To Date")%></label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtCreatedToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="form-group col-md-3">
                                <label><%= ResourceMgr.GetMessage("DBA Name")%></label>
                                <asp:TextBox ID="txtDBAName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-3">
                                <label><%= ResourceMgr.GetMessage("Contact")%></label>
                                <asp:TextBox ID="txtPrimaryCotnact" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-3">
                                <label><%= ResourceMgr.GetMessage("ZIP Code")%></label>
                                <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-12">
                                <cc1:ResourceLinkButton ID="btnStakeSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                    OnClick="btnStakeSearch_Click"><%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                <cc1:ResourceLinkButton ID="btnStakeCancel" runat="server" CssClass="btn btn-sm btn-white font-bold" OnClientClick="ClearSearchFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>

                            </div>

                        </div>
                    </div>

                </asp:Panel>
            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="upnlGrid" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Stakeholders")%></h5>
                            <div class="ibox-tools hidden">
                                <div class="form-group">
                                    <a class="ico_view btn btn-sm btn-primary font-bold" href="adminRegistrationForm.aspx">
                                        <i class="fa fa-plus"></i><%= ResourceMgr.GetMessage(" Add New Stakeholder")%>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">


                                        <asp:GridView ID="gvApplicationApproved" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table"
                                            EmptyDataRowStyle-CssClass="alert alert-danger text-center" DataKeyNames="OrganizationId" EnableViewState="true"
                                            EmptyDataText="No data found"
                                            OnRowCommand="gvApplicationApproved_RowCommand">
                                            <AlternatingRowStyle CssClass="highlighted-row" />

                                            <HeaderStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Organization Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <%# Eval("LegalName") %></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("DBA Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("DBAName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Contact Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ContactName") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("City")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("City") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("View Stakeholder")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a class="btn btn-white btn-bitbucket" href='/Stewardship/ViewDetailStewardship.aspx?OrganizationId=<%# Eval("OrganizationId") %>' title="View Stakeholder">
                                                            <i class="fa fa-eye"></i>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("User")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a class="btn btn-white btn-bitbucket" href='/User/ViewUser.aspx?p=1&OrganizationId=<%# Eval("OrganizationId") %>&RoleId=<%# Eval("RoleId") %>&OrganizationTypeID=<%#Eval("OrganizationTypeId")%>' title="View User">
                                                            <i class="fa fa-user"></i>
                                                        </a>
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
                                    <asp:Label ID="lblApprovalStakeholder" runat="server" CssClass="m-l-sm"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Literal ID="ltrApprovalStakeholder" runat="server"></asp:Literal>
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

