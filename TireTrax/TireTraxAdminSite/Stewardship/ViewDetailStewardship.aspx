<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewDetailStewardship.aspx.cs" Inherits="Stewardship_ViewDetailStewardship" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">


        function HideBusinessZipCodeLabel() {
            $("#<%=lblBusinessZipCode.ClientID%>").hide();
}
    </script>
    <script type="text/javascript">
        function funcname() {
            var txt = document.getElementById('<%=txtNotes.ClientID %>').value;
            if (txt == null || txt == "") {
                alert("Cannot delete please provide notes for deleting");
                return false;
            }
            else {
                return confirm('Are you sure you want to delete this record?');
            }
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





    <asp:UpdatePanel runat="server" ID="upnlGrid" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Organization Detail Information</h5>
                            <div class="ibox-tools">
                                <div class="form-group">
                                    <asp:LinkButton CssClass="btn btn-primary btn-sm font-bold" ID="lnkbtnBack" Text="Back" runat="server" OnClick="lnkbtnBack_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-md-12">
                                    <p class="m0"><strong>Status:</strong>
                                        <asp:Label CssClass="badge badge-primary" ID="lblStatus" runat="server"></asp:Label></p>
                                    <asp:Panel ID="pnlDisplay" runat="server">
                                        <div class="row">
                                            <asp:Label ID="lblinfo" runat="server" Visible="false" CssClass="custom-error"></asp:Label>
                                            <div class="col-md-5">
                                                <h2><%= ResourceMgr.GetMessage("Primary Information")%> </h2>
                                                <ul class="todo-list m-t custom-todo">
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("First Name:")%></strong>
                                                            <asp:Literal ID="ltrFirstName" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Last Name:")%> </strong>
                                                            <asp:Literal ID="ltrLastName" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Primary Email:")%> </strong>
                                                            <asp:Literal ID="ltrprimaryEmail" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Phone Number:")%> </strong>
                                                            <asp:Literal ID="ltrPhoneNumber" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Phone Extension:")%> </strong>
                                                            <asp:Literal ID="ltrCellPhoneType" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Cell Phone Type:")%> </strong>
                                                            <asp:Literal ID="ltrPhoneExtension" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Cell Phone Number:")%> </strong>
                                                            <asp:Literal ID="ltrCellPhoneNumber" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Cell Text Message:")%> </strong>
                                                            <asp:Literal ID="ltrCellTextMessage" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Contact Title:")%> </strong>
                                                            <asp:Literal ID="ltrContactTitle" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("ZIP Code:")%> </strong>
                                                            <asp:Literal ID="ltrZipCode" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("City:")%> </strong>
                                                            <asp:Literal ID="ltrCity" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("State:")%> </strong>
                                                            <asp:Literal ID="ltrState" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Country:")%></strong>
                                                            <asp:Literal ID="ltrCountry" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Country Abbreviation:")%> </strong>
                                                            <asp:Literal ID="ltrCountryAbbreviation" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-md-5 col-md-offset-1">
                                                <h2><%= ResourceMgr.GetMessage("Organization Information")%> </h2>
                                                <ul class="todo-list m-t custom-todo">
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Name:")%> </strong>
                                                            <asp:Literal ID="ltrBusinessName" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("DBA Name:")%> </strong>
                                                            <asp:Literal ID="ltrDBAName" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Organization:")%> </strong>
                                                            <asp:Literal ID="ltrOrganization" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Website:")%> </strong>
                                                            <asp:Literal ID="ltrwebsite" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Address1:")%> </strong>
                                                            <asp:Literal ID="ltrBusinessAddress1" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Address2:")%> </strong>
                                                            <asp:Literal ID="ltrBusinessAddress2" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Phone Type:")%> </strong>
                                                            <asp:Literal ID="ltrBusinessPhoneType" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Text Messages:")%> </strong>
                                                            <asp:Literal ID="lrtBusinessTextMessage" runat="server"></asp:Literal>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong>Previous Status Notes: </strong>
                                                            <asp:Label ID="lblStatusNotes" runat="server"></asp:Label>
                                                        </p>
                                                    </li>
                                                </ul>

                                                <div class="row m-t-md row m-b-md">
                                                    <div class="col-md-12">
                                                        <asp:LinkButton CssClass="btn btn-xs btn-white" ID="lnkbtnEdit" runat="server" ToolTip="Edit Organization" OnClick="lnkbtnEdit_Click">
                                                            <i class="fa fa-pencil"></i> Edit
                                                        </asp:LinkButton>

                                                        <asp:LinkButton CssClass="btn btn-xs btn-white" ID="imgBtnPending" runat="server" OnClientClick="return confirm('Are you sure you want to change status to pending of this record?');" ToolTip="Mark as Pending" OnClick="lnkbtnPending_Click">
                                                            <i class="fa fa-exclamation"></i> Pending
                                                        </asp:LinkButton>

                                                        <asp:LinkButton CssClass="btn btn-xs btn-white" ID="imgbtnApprove" runat="server" OnClientClick="return confirm('Are you sure you want to approve this record?');" ToolTip="Mark as Approved" OnClick="lnkbtnApprove_Click">
                                                            <i class="fa fa-check"></i> Approve
                                                        </asp:LinkButton>

                                                        <asp:LinkButton CssClass="btn btn-xs btn-white" ID="imgbtnReject" runat="server" ToolTip="Mark as Rejected" OnClientClick="return confirm('Are you sure you want to Reject this record?');" OnClick="lnkbtnReject_Click">
                                                            <i class="fa fa-times"></i> Reject
                                                        </asp:LinkButton>

                                                        <asp:LinkButton CssClass="btn btn-xs btn-white" ID="imgbtnDelete" runat="server" ToolTip="Delete" OnClick="lnkbtnDelete_Click">
                                                            <i class="fa fa-trash"></i> Delete
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label><%= ResourceMgr.GetMessage("Notes:")%></label>
                                                            <asp:TextBox TextMode="MultiLine" Rows="4" cols="20" ID="txtNotes" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-5">
                                                <h2><%= ResourceMgr.GetMessage("Primary Information")%> </h2>
                                                <ul class="todo-list m-t custom-todo">
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("First Name:")%> </strong>
                                                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                                            <br />
                                                            <div class="text-right">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="txtFirstName" CssClass="custom-error"
                                                                    ErrorMessage="Please Type Name"></asp:RequiredFieldValidator>
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator33"
                                                                    ControlToValidate="txtFirstName" ErrorText="Enter Only text"
                                                                    ValidationExpression="^[a-zA-Z ]*$" Display="Dynamic" CssClass="custom-error"
                                                                    runat="server"></cc1:ResourceRegularExpressionValidator>

                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Last Name:")%> </strong>
                                                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator44" ControlToValidate="txtLastName" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" Display="Dynamic" CssClass="custom-error"
                                                                    runat="server"></cc1:ResourceRegularExpressionValidator>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Primary Email:")%> </strong>
                                                            <asp:TextBox Enabled="false" ID="txtPrimaryEmail" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <asp:Label ID="lblemailalreadyexists" runat="server" Visible="false" CssClass="custom-error"></asp:Label>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Phone Number:")%> </strong>
                                                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator1" ErrorText="Enter Valid Phone#" CssClass="hidden custom-error" ValidationExpression="^([\(]{1}[0-9]{3}[\)]{1}[\.| |\-]{0,1}|^[0-9]{3}[\.|\-| ]?)?[0-9]{3}(\.|\-| )?[0-9]{4}$" Display="Dynamic" ControlToValidate="txtPhoneNumber" runat="server"></cc1:ResourceRegularExpressionValidator>
                                                                
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator23" ErrorText="Enter Valid format(000-000-0000)" CssClass="custom-error" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" Display="Dynamic" ControlToValidate="txtPhoneNumber" runat="server"></cc1:ResourceRegularExpressionValidator>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Phone Extension:")%> </strong>
                                                            <asp:TextBox ID="txtPhoneExtension" runat="server" MaxLength="4"></asp:TextBox>
                                                            <div class="text-right">
                                                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator4" ControlToValidate="txtPhoneExtension" ErrorText="Enter Only Numeric" ValidationExpression="^(0|[1-9][0-9]*)$" Display="Dynamic" CssClass="custom-error" runat="server"></cc1:ResourceRegularExpressionValidator>
                                                                </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Cell Phone Type:")%> </strong>
                                                            <asp:TextBox ID="txtCellPhoneType" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator5" ControlToValidate="txtCellPhoneType" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" Display="Dynamic" CssClass="custom-error"
                                                                    runat="server"></cc1:ResourceRegularExpressionValidator>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Cell Phone Number:")%> </strong>
                                                            <asp:TextBox ID="txtCellPhoneNumber" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator6" ErrorText="Enter Valid Phone#" CssClass="hidden custom-error" ValidationExpression="^([\(]{1}[0-9]{3}[\)]{1}[\.| |\-]{0,1}|^[0-9]{3}[\.|\-| ]?)?[0-9]{3}(\.|\-| )?[0-9]{4}$" Display="Dynamic" ControlToValidate="txtCellPhoneNumber" runat="server"></cc1:ResourceRegularExpressionValidator>
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator7" ErrorText="Enter Valid format(000-000-0000)" CssClass="custom-error" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" Display="Dynamic" ControlToValidate="txtCellPhoneNumber" runat="server"></cc1:ResourceRegularExpressionValidator>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Cell Text Message:")%> </strong>
                                                            <asp:DropDownList ID="dddlcelltextmsgs" Enabled="false" Visible="true" runat="server">
                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Contact Title:")%> </strong>
                                                            <asp:TextBox ID="txtContactTitle" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator13" ControlToValidate="txtContactTitle" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" Display="Dynamic" CssClass="custom-error"
                                                                    runat="server"></cc1:ResourceRegularExpressionValidator>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("ZIP Code:")%> </strong>
                                                            <asp:TextBox ID="txtZipCode" runat="server" CssClass="field-0" onblur="HideBusinessZipCodeLabel();" AutoPostBack="true" OnTextChanged="txtBusinessZipCode_TextChanged" CausesValidation="true" ValidationGroup="AdditionalInfoBusinessZipCode" MaxLength="10"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnBusinessZipCodeId" runat="server" Value="" />
                                                            <div class="text-right">
                                                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator6" runat="server" ControlToValidate="txtZipCode" ValidationGroup="AdditionalInfoBusinessZipCode" ErrorText="Please enter Facility ZIP Code" CssClass="custom-error" Display="None"></cc1:ResourceRequiredFieldValidator>
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator2" runat="server" ControlToValidate="txtZipCode" ErrorText="Please enter valid Facility ZIP Code e.g; 06514, M3C 0C1" ValidationGroup="AdditionalInfoBusinessZipCode" CssClass="custom-error" Display="None" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$"></cc1:ResourceRegularExpressionValidator>
                                                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator7" runat="server" ControlToValidate="txtZipCode" ValidationGroup="PrimaryRegistartion" ErrorText="Please enter Facility ZIP Code" Display="Dynamic" CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator3" runat="server" ControlToValidate="txtZipCode" ErrorText="Please enter valid Facility ZIP Code e.g; 06514, M3C 0C1" ValidationGroup="PrimaryRegistartion" CssClass="custom-error" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                                                <asp:Label ID="lblBusinessZipCode" runat="server" CssClass="custom-error" Style="float: left !important; clear: both"></asp:Label>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("City:")%> </strong>
                                                            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("State:")%> </strong>
                                                            <asp:DropDownList ID="ddlState" Enabled="false" runat="server" onchange="HideBusinessStateZipCode();" CssClass="field-0" Style="widht: 145px;" OnSelectedIndexChanged="dllState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            <div class="text-right">
                                                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator11" runat="server" ControlToValidate="ddlState" InitialValueErrorText="0" ValidationGroup="PrimaryRegistartion" ErrorText="Please select Facility State" Display="Dynamic" CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                                                                <asp:Label ID="lblStewardshipExists" runat="server" CssClass="custom-error" Text="Organization already exists for this state." Visible="false"></asp:Label>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Country:")%> </strong>
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="field-0" OnSelectedIndexChanged="dllCountry_SelectedIndexChanged" Style="widht: 145px;" AutoPostBack="true"></asp:DropDownList>
                                                            <div class="text-right">
                                                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator12" runat="server" ControlToValidate="ddlCountry" InitialValueErrorText="0" ValidationGroup="PrimaryRegistartion" ErrorText="Please select Facility Country" Display="Dynamic" CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                                                            </div>                                                            
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Country Abbreviation:")%> </strong>
                                                            <asp:TextBox ID="txtCountryAbbreviation" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator19" ControlToValidate="txtCountryAbbreviation" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" Display="Dynamic" CssClass="custom-error"
                                                                    runat="server"></cc1:ResourceRegularExpressionValidator>
                                                            </div>
                                                        </p>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-md-5 col-md-offset-1">
                                                <h2><%= ResourceMgr.GetMessage("Organization Information")%> </h2>
                                                <ul class="todo-list m-t custom-todo">
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Name:")%> </strong>
                                                            <asp:TextBox ID="txtBusinessName" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator14" ControlToValidate="txtBusinessName" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" Display="Dynamic" CssClass="custom-error"
                                                                    runat="server"></cc1:ResourceRegularExpressionValidator>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("DBA Name:")%> </strong>
                                                            <asp:Literal ID="Literal21" runat="server"></asp:Literal>
                                                            <asp:TextBox ID="txtDBAName" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator15" ControlToValidate="txtDBAName" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" Display="Dynamic" CssClass="custom-error"
                                                                    runat="server"></cc1:ResourceRegularExpressionValidator>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Organization:")%> </strong>
                                                            <asp:TextBox ID="txtOrganization" runat="server"></asp:TextBox>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Website:")%> </strong>
                                                            <asp:TextBox ID="txtWebsite" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator16" ControlToValidate="txtWebsite" ErrorText="Enter Valid Format" ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?" Display="Dynamic" CssClass="custom-error"
                                                                    runat="server"></cc1:ResourceRegularExpressionValidator>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Address1:")%> </strong>
                                                            <asp:TextBox ID="txtBusinessAddress1" runat="server"></asp:TextBox>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Address2:")%> </strong>
                                                            <asp:TextBox ID="txtBusinessAddress2" runat="server"></asp:TextBox>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Phone Type:")%> </strong>
                                                            <asp:TextBox ID="txtBusinessPhoneType" runat="server"></asp:TextBox>
                                                            <div class="text-right">
                                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator20" ControlToValidate="txtBusinessPhoneType" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" Display="Dynamic" CssClass="custom-error"
                                                                    runat="server"></cc1:ResourceRegularExpressionValidator>
                                                            </div>
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            <strong><%= ResourceMgr.GetMessage("Business Text Messages:")%> </strong>
                                                            <asp:DropDownList ID="ddlbusinesstextmsgs" Enabled="false" Visible="true" runat="server">
                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </p>
                                                    </li>
                                                </ul>

                                                <div class="row m-t-md row m-b-md">
                                                    <div class="col-md-12">
                                                        <asp:LinkButton CssClass="btn btn-xs btn-white" ID="lnkbtnUpdate" runat="server" Text="Update" ToolTip="Update Organization Data" OnClick="lnkbtnUpdate_Click">
                                                            <i class="fa fa-upload"></i> Update
                                                        </asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-xs btn-white" ID="lnkbtnCancel" runat="server" Text="Cancel" CausesValidation="False" ToolTip="Cancel" OnClick="lnkbtnCancel_Click">
                                                            <i class="fa fa-times"></i> Cancel
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="stv_txt-had-left" style="background: none; visibility: hidden;">
                                            <b><%= ResourceMgr.GetMessage("Certifications")%></b>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>





            <div id="dlgcnfdel" runat="server" class="box_blockCmp" visible="false">
                <div class="popUp_lotInfo" style="height: auto; top: 25%; min-height: inherit;">
                    <div class="">Are you sure you want to delete this record?</div>
                    <cc1:ResourceLinkButton class="reg_button" ID="ResourceLinkButton1" runat="server"
                        OnClick="btndelYes_Click">Yes</cc1:ResourceLinkButton>
                    <cc1:ResourceLinkButton class="reg_button" ID="ResourceLinkButton3" runat="server"
                        OnClick="btndelCancel_Click">Cancel </cc1:ResourceLinkButton>
                </div>
            </div>
            <div id="dlg" runat="server" class="box_blockCmp" visible="false">
                <div class="popUp_lotInfo">
                    <div class="textTitle" style="border-bottom: solid 1px #ddd; padding-bottom: 5px; margin-bottom: 20px;">Stakeholders list</div>
                    <p>
                        <asp:GridView ID="gvApplicationApproved" runat="server" AutoGenerateColumns="False"
                            GridLines="None" CssClass="add-new-inventory" DataKeyNames="OrganizationId" EnableViewState="true"
                            EmptyDataText="No data found" wrap="nowrap" CellPadding="0" Width="100%"
                            ShowFooter="true">
                            <AlternatingRowStyle CssClass="highlighted-row" />
                            <%--<RowStyle CssClass="highlighted-row" />--%>
                            <HeaderStyle CssClass="txt-had" />
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="17%">
                                    <HeaderTemplate>
                                        <%= ResourceMgr.GetMessage("Organization Name")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <%# Eval("LegalName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="17%">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("DBA Name")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("DBAName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="17%">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Contact Name")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("ContactName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="17%">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("City")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("City") %>
                                    </ItemTemplate>
                                </asp:TemplateField>




                            </Columns>
                        </asp:GridView>
                    </p>
                    <p>
                        <b>Cannot delete due to above stakeholders</b>.
                    </p>
                    <cc1:ResourceLinkButton class="reg_button" ID="ResourceLinkButton2" runat="server"
                        OnClick="btnPopupOK_Click">OK</cc1:ResourceLinkButton>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

