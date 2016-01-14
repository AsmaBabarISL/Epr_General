<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ViewUsers.aspx.cs" Inherits="Users_ViewUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
       
        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("form");
            //$(".ajax-loader").remove();
            $(".ajax-loader").hide();
        }

        function AjaxLoader() {
            $(".ajax-loader").show();
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
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div id="Div1" class="ajax-loader" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:Panel ID="pnlUserSearch" runat="server" DefaultButton="btnUserSearch" CssClass="search-filter_inner">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("Search Filters")%> </h5>
                    </div>
                    <div class="ibox-content">
                        <div role="form" class="row search-filter">
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("First Name")%></label>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Last Name")%></label>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Login")%></label>
                                <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Status")%></label>
                                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0" Selected="True" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Rejected"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Approved"></asp:ListItem>
                                </asp:DropDownList>
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

                            <div class="form-group col-md-12 mb0">
                                <asp:LinkButton ID="btnUserSearch" runat="server" CssClass="btn btn-primary btn-sm font-bold" OnClick="btnUserSearch_Click">
                                <%= ResourceMgr.GetMessage("Search")%></asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-white btn-sm font-bold" OnClick="btnCancel_Click">
                            <%= ResourceMgr.GetMessage("Reset")%></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upnlsearch" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Users")%></h5>
                            <div class="ibox-tools">
                                <div class="form-group" runat="server" id="btnadduser">
                                    <a class="btn btn-sm btn-primary" id="a1" href="editUser"><i class="fa fa-plus"></i><strong><%= ResourceMgr.GetMessage("Add User")%></strong></a>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvUserAdmin" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered epr-sec-table" DataKeyNames="UserId"
                                            EnableViewState="true" EmptyDataText="No data found" OnRowCommand="gvUserAdmin_RowCommand"
                                            EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>
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
                                                        <asp:Label ID="HyperLink1" runat="server" ToolTip="Approved" Visible='<%# Convert.ToBoolean(Eval("IsApproved")) %>'>
                                    <span class="badge badge-primary"> Approved </span>
                                                        </asp:Label>
                                                        <asp:Label ID="hyplnkStatusFalse" runat="server" ToolTip="Pending" Visible='<%# !Convert.ToBoolean(Eval("IsApproved")) %>'>
                                    <span class="badge badge-danger"> Rejected </span>
                                                        </asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="60" ItemStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Action")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <a href="editUser?UserId=<%#Eval("UserId")%>&OrganizationId=<%#Eval("OrganizationId")%>&OrganizationTypeId=<%#Eval("OrganizationTypeId")%>"
                                                            class="btn btn-white btn-bitbucket" title="Edit User">
                                                            <i class="fa fa-edit"></i>
                                                        </a>

                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnApprove" runat="server" Visible='<%# !Convert.ToBoolean(Eval("IsApproved"))%>'
                                                            ToolTip="Approve this record" OnClientClick="return confirm('Are you sure you want to Approve User?');"
                                                            CommandName="Approve" CommandArgument='<%#Bind("UserId")%>'
                                                            Enabled='<%# Convert.ToBoolean(Eval("IsApproved")) ==false ||Eval("IsApproved")==null ? true: false  %>'>
                                                                <i class="fa fa-thumbs-up"></i> 
                                                        </asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnDisApprove" runat="server" ToolTip="Reject this record"
                                                            Visible='<%# Convert.ToBoolean(Eval("IsApproved"))%>'
                                                            OnClientClick="return confirm('Are you sure you want to Reject User?');" CommandName="DisApprove" CommandArgument='<%# Bind("UserId") %>'
                                                            Enabled='<%#  Convert.ToBoolean(Eval("IsApproved")) == true ||Eval("IsApproved")==null ? true: false  %>'>
                                                                <i class="fa fa-thumbs-down"></i>
                                                        </asp:LinkButton>

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
            <asp:AsyncPostBackTrigger ControlID="btnUserSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function removeActiveClass() {
            document.getElementById("Uactive").removeAttribute("class")
            document.getElementById("Stakeholdeactive").className = "active"
        };
    </script>


</asp:Content>
