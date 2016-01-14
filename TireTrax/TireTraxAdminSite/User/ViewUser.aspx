<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewUser.aspx.cs" Inherits="User_ViewUser" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
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

        function ShowUserInfo() {
            $("#dvUserInfo").show();
        }
        function HideUserInfo() {
            $("#dvUserInfo").hide();
        }
        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("body");
            $(".ajax-loader").remove();
        }

        function AjaxLoader() {
            $(".ajax-loader").appendTo("body");
        }
        $(document).ready(function () {
            $(".ajaxModal-popup").appendTo("body");
        });


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
                <asp:Panel ID="pnlUserSearch" runat="server" DefaultButton="btnUserSearch" CssClass="search-filter_inner">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row search-filter" id="">
                            <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("First Name")%></label>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Last Name")%></label>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Login")%></label>
                                <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Status")%></label>
                                <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Active" Selected="True" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div id="date_range">
                                <div class="input-daterange">
                                    <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("From")%></label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtCreatedFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("To")%></label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtCreatedToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="form-group col-md-12 mb0">
                                <asp:LinkButton ID="btnUserSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold" OnClick="btnUserSearch_Click"><%= ResourceMgr.GetMessage("Search")%></asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-sm btn-white font-bold" OnClick="btnCancel_Click"><%= ResourceMgr.GetMessage("Reset")%></asp:LinkButton>

                            </div>

                        </div>
                    </div>

                </asp:Panel>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="upnlsearch" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Users")%></h5>
                            <div class="ibox-tools ">
                                <div class="form-group hidden">
                                    <a class="ico_view btn btn-sm btn-primary font-bold" href="/User/AddUser.aspx?OrganizationId=<%=Convert.ToInt32(Request.QueryString["OrganizationId"]) %>&RoleId=<%=Convert.ToInt32(Request.QueryString["RoleId"])%>&OrganizationTypeID=<%=Convert.ToInt32(Request.QueryString["OrganizationTypeId"]) %>">
                                        <i class="fa fa-plus"></i>
                                        <%= ResourceMgr.GetMessage(" Add New User")%>
                                    </a>
                                </div>
                            </div>
                        </div>

                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">

                                        <asp:GridView ID="gvUserAdmin" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table"
                                            EmptyDataRowStyle-CssClass="alert alert-danger text-center" DataKeyNames="UserId" EnableViewState="true" EmptyDataText="No data found"
                                            OnRowCommand="gvUserAdmin_RowCommand">
                                            <Columns>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Login")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("Login") %>' CommandName="UserInfo" CommandArgument='<%# Eval("UserId") %>'></asp:LinkButton>
                                                        <%--    <a href="#" onclick="ShowUserInfo('<%# Eval("UserId") %>');"><%# Eval("Login") %></a>--%>
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
                                                        <%= ResourceMgr.GetMessage("Role Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("RoleName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Contact Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ContactName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Status")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="Approved" Visible='<%# Convert.ToBoolean(Eval("IsApproved")) %>'>
                                                                <span class="badge badge-primary"> Approved </span>
                                                        </asp:HyperLink>

                                                        <asp:HyperLink ID="hyplnkStatusFalse" runat="server" ToolTip="Pending" Visible='<%# !Convert.ToBoolean(Eval("IsApproved")) %>'>
                                                                <span class="badge badge-danger"> Rejected </span></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="" HeaderStyle-Width="40" HeaderStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Action") %>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnApprove" runat="server" ToolTip="Approve this record" 
                                                            OnClientClick="return confirm('Are you sure you want to Approve User?');" Visible='<%# !Convert.ToBoolean(Eval("IsApproved")) %>'
                                                            CommandName="Approve" CommandArgument='<%#Bind("UserId")%>' Enabled='<%# Convert.ToBoolean(Eval("IsApproved")) ==false ||Eval("IsApproved")==null ? true: false  %>'>
                                                            <i class="fa fa-thumbs-up"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnDisApprove" runat="server" ToolTip="Reject this record" 
                                                            OnClientClick="return confirm('Are you sure you want to Reject User?');" Visible='<%# Convert.ToBoolean(Eval("IsApproved")) %>'
                                                            CommandName="DisApprove" CommandArgument='<%# Bind("UserId") %>'
                                                            Enabled='<%#  Convert.ToBoolean(Eval("IsApproved")) == true ||Eval("IsApproved")==null ? true: false  %>'>
                                                            <i class="fa fa-thumbs-down"></i>
                                                        </asp:LinkButton>
                                                        <%-- <a class="btn btn-white btn-bitbucket" href="/User/EditUser.aspx?UserId=<%#Eval("UserId")%>&OrganizationId=<%=Convert.ToInt32(Request.QueryString["OrganizationId"]) %>&RoleId=<%=Convert.ToInt32(Request.QueryString["RoleId"])%>&OrganizationTypeID=<%=Convert.ToInt32(Request.QueryString["OrganizationTypeId"]) %>">
                                                                <i class="fa fa-pencil"></i> </a>--%>
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
            <div id="dvUserInfo" class="ajaxModal-popup inmodal" style="display: none;">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <button type="button" class="close" onclick="HideUserInfo()"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><%= ResourceMgr.GetMessage("User Information")%></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <dl class="dl-horizontal">
                                    <dt><%= ResourceMgr.GetMessage("User Name:")%> </dt>
                                    <dd>
                                        <asp:Label ID="lblLogin" runat="server"></asp:Label>
                                    </dd>

                                    <dt><%= ResourceMgr.GetMessage("First Name:")%> </dt>
                                    <dd>
                                        <asp:Literal ID="ltrFirstName" runat="server" Visible="true"></asp:Literal></dd>

                                    <dt><%= ResourceMgr.GetMessage("Middle Name:")%> </dt>
                                    <dd>
                                        <asp:Literal ID="ltrMiddleName" runat="server"></asp:Literal></dd>

                                    <dt><%= ResourceMgr.GetMessage("Last Name:")%> </dt>
                                    <dd>
                                        <asp:Literal ID="ltrLastName" runat="server"></asp:Literal></dd>

                                    <dt><%= ResourceMgr.GetMessage("Primary Email:")%> </dt>
                                    <dd>
                                        <asp:Literal ID="ltrprimaryEmail" runat="server" Text="Literal"></asp:Literal></dd>

                                    <dt><%= ResourceMgr.GetMessage("Phone Number:")%> </dt>
                                    <dd>
                                        <asp:Literal ID="ltrPhoneNumber" runat="server"></asp:Literal></dd>

                                    <dt><%= ResourceMgr.GetMessage("Language:")%></dt>
                                    <dd>
                                        <asp:Literal ID="ltrLanguage" runat="server"></asp:Literal></dd>

                                    <dt><%= ResourceMgr.GetMessage("Role Type:")%> </dt>
                                    <dd>
                                        <asp:Literal ID="ltrRoleType" runat="server"></asp:Literal></dd>

                                    <dt><%= ResourceMgr.GetMessage("Website:")%> </dt>
                                    <dd>
                                        <asp:Literal ID="ltrWebsite" runat="server"></asp:Literal></dd>

                                    <dt><%= ResourceMgr.GetMessage("Organization Name:")%> </dt>
                                    <dd>
                                        <asp:Literal ID="ltrOrganizationName" runat="server"></asp:Literal></dd>

                                    <dt><%= ResourceMgr.GetMessage("Franchise Name:")%></dt>
                                    <dd>
                                        <asp:Literal ID="ltrFranchiseName" runat="server"></asp:Literal></dd>

                                    <dt><%= ResourceMgr.GetMessage("Role Name:")%></dt>
                                    <dd>
                                        <asp:Literal ID="ltrRoleName" runat="server"></asp:Literal></dd>
                                </dl>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" onclick="HideUserInfo()">Close</button>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnUserSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

