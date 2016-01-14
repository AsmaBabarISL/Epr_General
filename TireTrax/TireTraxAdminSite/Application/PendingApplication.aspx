<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="PendingApplication.aspx.cs" Inherits="Application_PendingApplication" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />
    <script type="text/javascript">
        function ClearSearchFields() {
            $("#<%=txtStakeholderName.ClientID%>").val('');
            $("#<%=txtCreatedFromDate.ClientID%>").val('');
            $("#<%=txtCreatedToDate.ClientID%>").val('');
            $("#<%=txtDBAName.ClientID%>").val('');
            $("#<%=txtPrimaryCotnact.ClientID%>").val('');
            $("#<%=btnStakeSearch.ClientID%>")[0].click();
        }
    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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
        <asp:UpdatePanel ID="upnlGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnStakeSearch" CssClass="">
                                <div class="ibox-title">
                                    <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                                </div>

                                <div class="ibox-content">
                                    <div class="row search-filter" id="">
                                        <div class="form-group col-sm-6 col-md-4 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Organization Name")%></label>
                                            <asp:TextBox ID="txtStakeholderName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div id="date_range">
                                            <div class="input-daterange">
                                                <div class="form-group col-sm-6 col-md-4 col-lg-3">
                                                    <label>
                                                        <%= ResourceMgr.GetMessage("From")%>
                                                    </label>
                                                    <div class="input-group date">
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="txtCreatedFromDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="form-group col-sm-6 col-md-4 col-lg-3">
                                                    <label>
                                                        <%= ResourceMgr.GetMessage("To")%>
                                                    </label>
                                                    <div class="input-group date">
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox ID="txtCreatedToDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-6 col-md-4 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("DBA Name")%></label>
                                            <asp:TextBox ID="txtDBAName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-sm-6 col-md-4 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("Contact")%></label>
                                            <asp:TextBox ID="txtPrimaryCotnact" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-12 col-sm-12 mb0">

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

                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Applications")%></h5>
                                <div class="ibox-tools">
                                    <div class="form-group">
                                        <a class="btn btn-sm btn-primary" href="adminRegistrationForm.aspx" style="visibility: hidden;">
                                            <i class="fa fa-plus"></i><strong><%= ResourceMgr.GetMessage("Add New Stakeholder")%></strong></a>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">


                                            <asp:GridView ID="gvLatestSteward" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                CssClass="table table-bordered epr-sec-table" DataKeyNames="OrganizationId" EnableViewState="true"
                                                EmptyDataText="No application was found" wrap="nowrap" CellPadding="0" Width="100%"
                                                ShowFooter="true" OnRowCommand="gvLatestSteward_RowCommand"
                                                EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Organization Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a href='/Stewardship/ViewDetailStewardship.aspx?OrganizationId=<%# Eval("OrganizationId") %>&PageId=1'>
                                                                <%# Eval("LegalName") %></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Stewardship Type(s)")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("LookupTypeName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Received Date")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffactiveDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StewardshipDate")).ToShortDateString() %>'> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- <asp:TemplateField HeaderText="" HeaderStyle-CssClass="pnl-hdr-grid2" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                
                                                    <%= ResourceMgr.GetMessage("Status")%>
                
                                                        </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyplnkStatusTrue" runat="server" ImageUrl="../Images/thumb-up_icon.png"
                                                                    Visible='<%#  Convert.ToBoolean(Eval("IsApproved")) %>'></asp:HyperLink>
                                                                <asp:HyperLink ID="hyplnkStatusFalse" runat="server" ImageUrl="../Images/thumb-down_icon.png"
                                                                    Visible='<%# !Convert.ToBoolean(Eval("IsApproved")) %>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    <%--    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="pnl-hdr-grid2" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                
                                                    <%= ResourceMgr.GetMessage("Status")%>
                
                                                        </HeaderTemplate>
                                                            <ItemTemplate>
                                                            <asp:HiddenField ID="hfOrganizationId" runat="server" Value='<%#Eval("OrganizationId").ToString() %>' />
                                                             <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("intStatus").ToString() %>' />
                                                               <asp:DropDownList  ID="ddlStatus" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"  >
                                                               <asp:ListItem Text="Pending" Value="1" ></asp:ListItem>
                                                               <asp:ListItem Text="Approved" Value="2" ></asp:ListItem>
                                                               <asp:ListItem Text="Rejected" Value="3" ></asp:ListItem>
                                                               </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    <%--  <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                       <HeaderTemplate>
                
                                                    <%= ResourceMgr.GetMessage("Approve")%>
                
                                                        </HeaderTemplate>
                                                            <ItemTemplate>
                                                               <asp:ImageButton ID="imgbtnApprove" runat="server" ImageUrl="../Images/approval_icon.png"
                                                                        ToolTip="Approve this record" OnClientClick="return confirm('Are you sure you want to Approve Pending Stewardship?');" CommandName="Approve" CommandArgument='<%# Eval("OrganizationId") %>' Enabled='<%# Convert.ToBoolean(Eval("IsApproved")) ==false ||Eval("IsApproved")==null ? true: false  %>'></asp:ImageButton>
                                                            </ItemTemplate>
                   
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="" Visible="false">
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
                <asp:AsyncPostBackTrigger ControlID="btnStakeSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlPageSize" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>


</asp:Content>

