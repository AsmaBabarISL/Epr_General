<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true"
    CodeFile="ViewBankAccount.aspx.cs" Inherits="Settings_BankAccount_ViewBankAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">

        function DeactivateBankAccount(IsChecked) {

            if (IsChecked == "True") {
                alert('Please select another primary account and then deactivate');
                return false;
            }
            else if (IsChecked == "False") {
                return confirm('Are you sure you want to Deactivate Bank Account?');
            }

        }


        function ClearSearchFields() {
            $('#<%=ddlBankAccountStatus.ClientID%>').val('2');
        }
        function fadeOut() {
            $(".custom-absolute-alert").delay(3000).fadeOut(300);
            $(".custom-absolute-alert").appendTo("form");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                    <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">

            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Search Filters")%> </h5>
                        </div>
                        <div class="ibox-content" style="display: block;">
                            <!-- Form-->
                            <div role="form" id="">
                                <div class="row">
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("Bank account status")%></label>
                                        <asp:DropDownList ID="ddlBankAccountStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="2" Selected="True" Text="All"></asp:ListItem>
                                            <asp:ListItem Value="1" Text='Activated'></asp:ListItem>
                                            <asp:ListItem Value="0" Text='Deactivated'></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-12 mb0">
                                        <cc1:ResourceLinkButton ID="btnSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                            OnClick="btnSearch_Click"><%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                        <cc1:ResourceLinkButton ID="btnCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                                            OnClientClick="ClearSearchFields();" OnClick="btnSearch_Click" ><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Bank Account Information")%> </h5>
                                <div id="dvAdd" runat="server" class="ibox-tools">
                                    <a class="ico_search" id="asearch" style='display: none; float: left;' href="javascript:void();">
                                        <%= ResourceMgr.GetMessage("Search")%></a>
                                    <%--visible="false"--%>
                                    <div class="form-group">
                                        <a href='addbankaccount' class="btn btn-sm btn-primary"><i class="fa fa-plus"></i><strong><%= ResourceMgr.GetMessage("Add Bank Account")%></strong></a>
                                    </div>
                                </div>

                            </div>
                            <div class="ibox-content" style="display: block;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:Label ID="lblSuccess" runat="server" CssClass="custom-absolute-alert alert-success" Visible="false"></asp:Label>
                                            <!-- grid-->
                                            <asp:UpdatePanel ID="upnlsearch" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvBankAccountInfo" runat="server" AutoGenerateColumns="False"
                                                        CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data was found"
                                                        DataKeyNames="intBankAccountId" OnRowCommand="gvBankAccountInfo_RowCommand" OnRowDataBound="gvBankAccountInfo_RowDataBound"
                                                        OnRowDeleted="gvBankAccountInfo_RowDeleted" OnRowDeleting="gvBankAccountInfo_RowDeleting"
                                                        EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <%= ResourceMgr.GetMessage("Account Type")%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Eval("LookupTypeName")%>
                                                                    <asp:HiddenField ID="HdnfldAcountId" Value='<%# Eval("intBankAccountId") %>' runat="server" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                <HeaderTemplate>
                                                                    <%= ResourceMgr.GetMessage("Account Title")%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Eval("vchAccountTitle")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <%= ResourceMgr.GetMessage("Account Number")%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnAccountNumber" Value='<%# Eval("vchAccountNumber")%>' runat="server" />
                                                                    <asp:Label ID="lblAccountNumber" runat="server"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <%=ResourceMgr.GetMessage("Bank Name") %>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Eval("vchBankName")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                <HeaderTemplate>
                                                                    <%= ResourceMgr.GetMessage("Branch")%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Eval("vchBranch")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <HeaderTemplate>
                                                                    <%=ResourceMgr.GetMessage("Street Name") %>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Eval("vchStreetName")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                <HeaderTemplate>
                                                                    <%= ResourceMgr.GetMessage("Street Number")%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Eval("vchStreetNumber")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <%= ResourceMgr.GetMessage("Primary Phone")%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Eval("vchPrimaryPhone")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="" HeaderStyle-Width="40">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Status")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="HyperLink1" runat="server" Visible='<%# Convert.ToBoolean(Eval("bitIsPrimary")) %>' ToolTip="Primary"> 
                                                            <span class="badge badge-primary">Primary</span> </asp:LinkButton>
                                                        <asp:LinkButton ID="hyplnkStatusFalse" runat="server" Visible='<%# !Convert.ToBoolean(Eval("bitIsPrimary")) %>' ToolTip="Not Primary"> 
                                                            <span class="badge badge-danger">Not Primary</span> </asp:LinkButton>

                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <%= ResourceMgr.GetMessage("ETF")%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkboxetf" AutoPostBack="true" Enabled="false"
                                                                        Checked='<%# Convert.ToBoolean(Eval("IsETF"))%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <%= ResourceMgr.GetMessage("Primary Account")%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkboxPrimary" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("bitIsPrimary"))%>'
                                                                        runat="server" OnCheckedChanged="chkboxPrimary_CheckedChanged" ClientIDMode="Static" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <%=ResourceMgr.GetMessage("Status")%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lnkApproved" runat="server"
                                                                        ToolTip="Account is active" CssClass="badge badge-primary"
                                                                        Visible='<%#Convert.ToBoolean(Eval("bitIsActive"))%>'>Active</asp:Label>

                                                                    <asp:Label ID="lnkRejected" runat="server"
                                                                        ToolTip="Account is deactive " CssClass="badge badge-danger"
                                                                        Visible='<%#!Convert.ToBoolean(Eval("bitIsActive"))%>'>Deactivated</asp:Label>


                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="70" ItemStyle-Wrap="false">
                                                                <HeaderTemplate>
                                                                    <%= ResourceMgr.GetMessage("Actions")%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="imgbtnEditSetting" runat="server" CausesValidation="false" CommandName="Edit"
                                                                        ToolTip="Edit BankAccount" CommandArgument='<%# Eval("intBankAccountId") %>'
                                                                        CssClass="btn btn-white btn-bitbucket"> 
                                                            <i class="fa fa-edit"></i> </asp:LinkButton>

                                                                    <asp:LinkButton ID="imgbtnActiveSetting" ToolTip="Activate This Bank Account" runat="server"
                                                                        CausesValidation="false" CommandName="Active" CommandArgument='<%# Eval("intBankAccountId") %>'
                                                                        CssClass="btn btn-white btn-bitbucket"
                                                                        OnClientClick="return confirm('Are you sure you want to Activate Bank Account?');">
                                                             <i class="fa fa-check"></i> </asp:LinkButton>

                                                                    <asp:LinkButton ID="imgbtnDeactiveSetting" ToolTip="Deactivate This Bank Account" runat="server"
                                                                        CausesValidation="false" CommandName="Delete" CommandArgument='<%# Eval("intBankAccountId") %>'
                                                                        CssClass="btn btn-white btn-bitbucket" OnClientClick='<%# String.Format("javascript:return DeactivateBankAccount(\"{0}\")", Eval("bitIsPrimary").ToString()) %>'>
                                                             <i class="fa fa-trash"></i> </asp:LinkButton>


                                                                    <asp:HiddenField runat="server" ID="hdnActiveDeactive" Value='<%# Convert.ToBoolean(Eval("bitIsActive")) %>' />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
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
