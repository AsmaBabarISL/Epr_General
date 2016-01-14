<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true"
    CodeFile="UsersPermission.aspx.cs" Inherits="Permission_UsersPermission" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ClearSearchFields() {
            $("#<%=txtLegalName.ClientID%>").val('');
            $("#<%=txtUserName.ClientID%>").val('');
            $("#<%=txtLogin.ClientID%>").val('');

            $("#<%=ddlActive.ClientID %>").val('0');
            $("#<%=ddlCountry.ClientID %>").val('0');
            $("#<%=ddlOrganizationType.ClientID %>").val('0');
            $("#<%=ddlRole.ClientID %>").val('0');
            $("#<%=ddlState.ClientID %>").val('0');
            $("#<%=dvstate.ClientID %>").hide();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%=dvstate.ClientID %>").hide();
            });


            $("#<%=search.ClientID%>")[0].click();
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
        <div class="txt-main-had">
            <div class="txt-had-left" style="background: none;">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                            </div>
                            <div class="ibox-content">
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="search">
                                    <div class="row search-filter" id="">
                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("User Name")%></label>
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Login")%> </label>
                                            <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Legal Name")%> </label>
                                            <asp:TextBox ID="txtLegalName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:UpdatePanel ID="upServh" runat="server">
                                            <ContentTemplate>
                                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                                    <label><%= ResourceMgr.GetMessage("Organization Type")%> </label>
                                                    <asp:DropDownList ID="ddlOrganizationType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOrganizationType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                                    <label><%= ResourceMgr.GetMessage("Role")%> </label>
                                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                                    <label><%= ResourceMgr.GetMessage("Organization Country")%> </label>
                                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4 col-sm-6 col-lg-3" id="dvstate" runat="server" visible="false">
                                                    <label><%= ResourceMgr.GetMessage(" Organization State")%> </label>
                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                                    <label><%= ResourceMgr.GetMessage("User Active")%> </label>
                                                    <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control">
                                                        <asp:ListItem Selected="True" Text="Active" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="De-Active" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-sm-12 mb0">
                                                    <asp:LinkButton ID="search" runat="server" CssClass="btn btn-sm btn-primary font-bold" OnClick="btnUserSearch_Click"><%= ResourceMgr.GetMessage("Search")%></asp:LinkButton>
                                                    <cc1:ResourceLinkButton ID="Cancel" runat="server" OnClientClick="ClearSearchFields(); return false;"
                                                        CssClass="btn btn-sm btn-white font-bold"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("User Permissions")%></h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvUserPermission" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table"
                                                EmptyDataText="No data found" OnRowCommand="gvUserPermission_RowCommand" OnRowDataBound="gvUserPermission_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("User Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("UserName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Login")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Login")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Role")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("RoleName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Business Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("LegalName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Organization Type")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("OrganizationType")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Country")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Abbreviation")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("State")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("StateName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Active")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<asp:CheckBox ID="chkActivated" Enabled="false" runat="server" Checked='<%# Conversion.ParseBool(Eval("IsActive"))%>'></asp:CheckBox>--%>
                                                            <asp:Label ID="imgStatus" Text="" runat="server" CommandArgument='<%#Eval("IsActive") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="75" ItemStyle-Wrap="false">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Actions")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a class="btn btn-white btn-bitbucket" href="/User/EditUser.aspx?UserId=<%#Eval("UserId")%>&OrganizationId=<%#Eval("OrganizationId")%>&RoleId=<%#Eval("RoleId")%>&OrganizationTypeID=<%#Eval("OrganizationTypeID")%>&UP=1">
                                                                <i class="fa fa-edit"></i></a>
                                                            <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="lnkBtnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                                CommandName="DeleteStewardship" CommandArgument='<%#Eval("UserId") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" ID="ddlPageSize" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                            <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                            <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                            <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%=ResourceMgr.GetMessage("Records Per Page")%>
                                        <asp:Label ID="lblPagingLeft" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-6">
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
    </div>
</asp:Content>
