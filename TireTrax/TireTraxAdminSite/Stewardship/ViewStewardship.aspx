<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewStewardship.aspx.cs" Inherits="Stewardship_ViewStewardship" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function ShowOrganization(OrgID, PageName) {
            if (OrgID)
                $("#<%=hdnOrganizationId.ClientID %>").val(OrgID);

            if (PageName)
                $("#<%=hdnPageName.ClientID %>").val(PageName);



            $("#<%=ddlOrganization.ClientID%>").val("0");
        }

        function OpenNewPage() {
            if ($("#<%=ddlOrganizationType.ClientID%>").val() != "0") {
                var path = $("#<%=hdnPageName.ClientID %>").val() + '?p=1&OrganizationId=' + $("#<%=hdnOrganizationId.ClientID %>").val() + '&OrganizationTypeId=' + $("#<%=ddlOrganizationType.ClientID%>").val();
                window.location = path;
            }
        }

        function ShowOrganizationForUser(OrgID, PageName) {
            if (OrgID)
                $("#<%=hdnOrganizationId.ClientID %>").val(OrgID);

            if (PageName)
                $("#<%=hdnPageName.ClientID %>").val(PageName);

            $("#<%=ddlOrganizationTypeForUser.ClientID%>").val("0");
            $("#<%=ddlOrganization.ClientID%>").val("0");
        }

        function OpenNewPageForUser() {
            if ($("#<%=ddlOrganizationTypeForUser.ClientID%>").val() != "0" && $("#<%=ddlOrganization.ClientID%>").val() != "0" && $("#<%=ddlOrganization.ClientID%>").val() != null) {
                var path = $("#<%=hdnPageName.ClientID %>").val() + '&OrganizationId=' + $("#<%=ddlOrganization.ClientID %>").val() + '&OrganizationTypeId=' + $("#<%=ddlOrganizationTypeForUser.ClientID%>").val();
              window.location = path;
          }
      }

      function ClearSearchFields() {
          $("#<%=txtLegalName.ClientID%>").val('');
            $("#<%=txtTX_ID.ClientID%>").val('');
            $("#<%=txtPhone.ClientID%>").val('');
            $("#<%=txtEmail.ClientID%>").val('');
            $("#<%=txtContact.ClientID%>").val('');

            $("#<%=search.ClientID%>")[0].click();
        }
    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" alt="loader" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>






    <div class="grid-contain-outer">
        <asp:UpdatePanel ID="upnlGrid" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="search" CssClass="">
                                <div class="ibox-title">
                                    <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                                </div>

                                <div class="ibox-content">
                                    <div class="row search-filter" id="">
                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Legal Name")%></label>
                                            <asp:TextBox ID="txtLegalName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("TX-ID")%></label>
                                            <asp:TextBox ID="txtTX_ID" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Phone")%></label>
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Email")%></label>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Contact")%></label>
                                            <asp:TextBox ID="txtContact" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Status")%></label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="All" Selected="True" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-12 mb0">
                                            <cc1:ResourceLinkButton ID="search" runat="server" CssClass="btn btn-sm btn-primary font-bold" OnClick="search_Click"><%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                            <cc1:ResourceLinkButton ID="Cancel" runat="server" OnClientClick="ClearSearchFields(); return false;"
                                                CssClass="btn btn-sm btn-white font-bold"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
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
                                <h5><%= ResourceMgr.GetMessage("Stewardships")%></h5>
                                <div class="ibox-tools">
                                    <div class="form-group">
                                        <a href="/Registration/" style="visibility: hidden" class="btn btn-sm btn-primary"><i class="fa fa-plus"></i><strong><%= ResourceMgr.GetMessage("Add New Stewardship")%></strong></a>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvStewardship" runat="server" AutoGenerateColumns="false"
                                                CssClass="table table-bordered epr-sec-table" EmptyDataText="No data found"
                                                OnRowDataBound="gvLatestSteward_RowDataBound"
                                                DataKeyNames="OrganizationId" AllowPaging="true" OnRowCommand="gvStewardship_RowCommand"
                                                EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Legal Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a href='/Stewardship/ViewDetailStewardship.aspx?OrganizationId=<%# Eval("OrganizationId") %>&PageId=2'>
                                                                <%# Eval("LegalName") %></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("State Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("StateName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Contact Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("ContactName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" ItemStyle-Wrap="false">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Phone")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Phone") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" ItemStyle-Wrap="false">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Email")%>
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
                                                            <%= ResourceMgr.GetMessage("View")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a class="btn btn-white btn-bitbucket" href="#dvOrganizationTypes" data-toggle="modal" onclick="ShowOrganization('<%# Eval("OrganizationId") %>', '/Application/ViewApplication.aspx');" title="View Applications">
                                                                <i class="fa fa-file-text"></i></a>
                                                            <a class="btn btn-white btn-bitbucket" href="#dvOrganizationTypes" data-toggle="modal" onclick="ShowOrganization('<%# Eval("OrganizationId") %>', '/Stakeholder/ViewStakeholder.aspx');" title="View Organization">
                                                                <i class="fa fa-building"></i></a>
                                                            <a class="btn btn-white btn-bitbucket" href="#divOrganizationTypesForUser" data-toggle="modal" onclick="ShowOrganizationForUser('<%# Eval("OrganizationId") %>', '/User/ViewUser.aspx?p=1&RoleId=4');" title="View Users">
                                                                <i class="fa fa-users"></i></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("User")%>
                                                        </HeaderTemplate>


                                                        <ItemTemplate>
                                                            <a href='/User/ViewUser.aspx?p=1&OrganizationId=<%# Eval("OrganizationId") %>&RoleId=<%# Convert.ToInt32(TireTraxLib.UserInfo.UserRole.Stewardship) %>&OrganizationTypeID=<%#Eval("OrganizationTypeId")%>' class="btn btn-white btn-bitbucket" title="View User">
                                                                <i class="fa fa-user"></i></a>
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="search" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlPageSize" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>



        <div class="modal inmodal" id="dvOrganizationTypes" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
            <div class="modal-dialog">
                <div class="modal-content animated bounceInRight">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">View</h4>
                        <small class="font-bold">Select type below to view application / organization</small>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hdnOrganizationId" runat="server" />
                        <asp:HiddenField ID="hdnPageName" runat="server" />
                        <div class="form-group">
                            <label>Select Type</label>
                            <asp:DropDownList CssClass="form-control" ID="ddlOrganizationType" runat="server">
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Close</button>
                        <button onclick="OpenNewPage();" type="button" class="btn btn-primary">View</button>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal inmodal" id="divOrganizationTypesForUser" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
            <div class="modal-dialog">
                <div class="modal-content animated bounceInRight">
                    <asp:UpdatePanel ID="upnl" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                <h4 class="modal-title">View Users</h4>
                                <small class="font-bold">Select type below to view users</small>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label>Select Type</label>
                                    <asp:DropDownList ID="ddlOrganizationTypeForUser" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlOrganizationTypeForUser_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label>Select Organization</label>
                                    <asp:DropDownList ID="ddlOrganization" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-white" data-dismiss="modal">Close</button>
                                <button onclick="OpenNewPageForUser();" type="button" class="btn btn-primary">View</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlOrganizationTypeForUser" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>


    </div>
</asp:Content>

