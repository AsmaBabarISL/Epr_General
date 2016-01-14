<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="viewStakeholder.aspx.cs" Inherits="Application_viewStakeHolder" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />
    <script type="text/javascript" src="/Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-ui.min.js"></script>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>StakeHolder Information</h5>
                    <div class="ibox-tools">
                        <div class="form-group">
                                <asp:LinkButton CssClass="btn btn-sm btn-primary font-bold m-l-md" ID="lnkbtnBack" Text="Back" runat="server" OnClick="lnkbtnBack_Click"></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblinfo" runat="server" CssClass="custom-error" Visible="false"></asp:Label>
                            <p class="m0" style="display: none;">
                                <strong>Status: </strong>
                                <asp:Label CssClass="badge badge-primary" ID="lblStatus" runat="server"></asp:Label>
                            </p>

                            <asp:Panel ID="pnlDisplay" runat="server">
                                <div class="row">
                                    <div class="col-md-5">
                                        <h2><%= ResourceMgr.GetMessage("Primary Information")%></h2>
                                        <ul class="todo-list m-t custom-todo">
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("First Name:")%> </strong>
                                                    <asp:Literal ID="ltrFirstName" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Last Name:")%></strong>
                                                    <asp:Literal ID="ltrLastName" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Primary Email:")%></strong>
                                                    <asp:Literal ID="ltrprimaryEmail" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Phone Number:")%></strong>
                                                    <asp:Literal ID="ltrPhoneNumber" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Phone Extension:")%></strong>
                                                    <asp:Literal ID="ltrPhoneExtension" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Cell Phone Type:")%></strong>
                                                    <asp:Literal ID="ltrCellPhoneType" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Cell Phone Number:")%></strong>
                                                    <asp:Literal ID="ltrCellPhoneNumber" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Cell Phone Extension:")%></strong>
                                                    <asp:Literal ID="ltrCellPhoneExtension" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Cell Text Message:")%></strong>
                                                    <asp:Literal ID="ltrCellTextMessage" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Billing Contact:")%></strong>
                                                    <asp:Literal ID="ltrBillingContact" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Billing Mail Address:")%></strong>
                                                    <asp:Literal ID="ltrBillingMailAddress" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Address:")%></strong>
                                                    <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Fax:")%></strong>
                                                    <asp:Literal ID="ltrFax" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Contact Title:")%></strong>
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
                                                    <strong><%= ResourceMgr.GetMessage("State:")%></strong>
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
                                                    <strong><%= ResourceMgr.GetMessage("Country Abbreviation:")%></strong>
                                                    <asp:Literal ID="ltrCountryAbbreviation" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                        </ul>

                                    </div>
                                    <div class="col-md-5 col-md-offset-1">
                                        <h2><%= ResourceMgr.GetMessage("Organization Information")%></h2>
                                        <ul class="todo-list m-t custom-todo">
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Name:")%></strong>
                                                    <asp:Literal ID="ltrBusinessName" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("DBA Name:")%></strong>
                                                    <asp:Literal ID="ltrDBAName" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Organization:")%></strong>
                                                    <asp:Literal ID="ltrOrganization" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Type:")%></strong>
                                                    <asp:Literal ID="ltrBusinessType" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Website:")%></strong>
                                                    <asp:Literal ID="ltrwebsite" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Address1:")%></strong>
                                                    <asp:Literal ID="ltrBusinessAddress1" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Address2:")%></strong>
                                                    <asp:Literal ID="ltrBusinessAddress2" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li style="display: none;">
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Phone Type:")%></strong>
                                                    <asp:Literal ID="ltrBusinessPhoneType" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li style="display: none">
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Text Messages:")%></strong>
                                                    <asp:Literal ID="lrtBusinessTextMessage" runat="server"></asp:Literal>
                                                </p>
                                            </li>
                                            <li style="display: none">
                                                <p>
                                                    <strong>Previous Status Notes:</strong>
                                                    <asp:Label ID="lblStatusNotes" runat="server"></asp:Label>
                                                </p>
                                            </li>
                                        </ul>

                                        <div class="row m-t-md row m-b-md">
                                            <div class="col-md-12">
                                                <asp:LinkButton CssClass="btn btn-xs btn-white" ID="lnkbtnEdit" runat="server" Text="Edit" ToolTip="Edit Stakeholder Information" OnClick="lnkbtnEdit_Click">
                                                <i class="fa fa-pencil"></i> Edit
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="btn btn-xs btn-white" ID="imgBtnPending" runat="server" OnClientClick="return confirm('Are you sure you want to change status to pending of this record?');" Text="Pending" ToolTip="Mark as Pending" OnClick="lnkbtnPending_Click">
                                                <i class="fa fa-exclamation"></i> Pending
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="btn btn-xs btn-white" ID="imgbtnApprove" runat="server" OnClientClick="return confirm('Are you sure you want to approve this record?');" Text="Approve" ToolTip="Mark as Approved" OnClick="lnkbtnApprove_Click">
                                                <i class="fa fa-check"></i> Approve
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="btn btn-xs btn-white" ID="imgbtnReject" runat="server" Text="Reject" ToolTip="Mark as Rejected" OnClientClick="return confirm('Are you sure you want to reject this record?');" OnClick="lnkbtnReject_Click">
                                                <i class="fa fa-times"></i> Reject
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="btn btn-xs btn-white" ID="imgbtnDelete" runat="server" Text="Delete" ToolTip="Delete" OnClientClick=" return funcname()" OnClick="lnkbtnDelete_Click">
                                                <i class="fa fa-trash"></i> Delete
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label><%= ResourceMgr.GetMessage("Notes:")%></label>
                                                    <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="4" ID="txtNotes" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-5">
                                        <h2><%= ResourceMgr.GetMessage("Primary Information")%></h2>
                                        <ul class="todo-list m-t custom-todo">
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("First Name:")%> </strong>
                                                    <asp:TextBox ID="txtFirstName" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Last Name:")%></strong>
                                                    <asp:TextBox ID="txtLastName" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Primary Email:")%></strong>
                                                    <asp:TextBox ID="txtPrimaryEmail" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Phone Number:")%></strong>
                                                    <asp:TextBox ID="txtPhoneNumber" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Phone Extension:")%></strong>
                                                    <asp:TextBox ID="txtPhoneExtension" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Cell Phone Type:")%></strong>
                                                    <asp:TextBox ID="txtCellPhoneType" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Cell Phone Number:")%></strong>
                                                    <asp:TextBox ID="txtCellPhoneNumber" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Cell Phone Extension:")%></strong>
                                                    <asp:TextBox ID="txtCellPhoneExtension" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Cell Text Message:")%></strong>
                                                    <asp:DropDownList ID="dddlcelltextmsgs" Enabled="false" Visible="true" runat="server">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Billing Contact:")%></strong>
                                                    <asp:TextBox ID="txtBillingContact" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Billing Mail Address:")%></strong>
                                                    <asp:TextBox ID="txtBillingMailAddress" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Address:")%></strong>
                                                    <asp:TextBox ID="txtAddress" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Fax:")%></strong>
                                                    <asp:TextBox ID="txtFax" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Contact Title:")%></strong>
                                                    <asp:TextBox ID="txtContactTitle" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("ZIP Code:")%> </strong>
                                                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="" onblur="HideBusinessZipCodeLabel();" AutoPostBack="true" ontextchanged="txtBusinessZipCode_TextChanged" CausesValidation="true" ValidationGroup="AdditionalInfoBusinessZipCode" MaxLength="10"></asp:TextBox>
              <asp:HiddenField ID="hdnBusinessZipCodeId" runat="server" Value="" />
 <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator6" runat="server" ControlToValidate="txtZipCode" ValidationGroup="AdditionalInfoBusinessZipCode" ErrorText="Please enter Facility ZIP Code" CssClass="custom-error" Display="None"></cc1:ResourceRequiredFieldValidator>
  <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator2" runat="server" ControlToValidate="txtZipCode" ErrorText="Please enter valid Facility ZIP Code e.g; 06514, M3C 0C1" ValidationGroup="AdditionalInfoBusinessZipCode" CssClass="error_message" Display="None" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$"></cc1:ResourceRegularExpressionValidator>
 <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator7" runat="server" ControlToValidate="txtZipCode" ValidationGroup="PrimaryRegistartion" ErrorText="Please enter Facility ZIP Code"  Display="Dynamic" CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
<cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator3" runat="server" ControlToValidate="txtZipCode" ErrorText="Please enter valid Facility ZIP Code e.g; 06514, M3C 0C1" ValidationGroup="PrimaryRegistartion" CssClass="custom-error" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
    <asp:Label ID="lblBusinessZipCode" runat="server" CssClass="error_message" ></asp:Label>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("City:")%> </strong>
                                                    <asp:TextBox ID="txtCity" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("State:")%></strong>
                                                    <asp:DropDownList ID="ddlState" Enabled="false" runat="server" onchange="HideBusinessStateZipCode();" OnSelectedIndexChanged="dllState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator11" runat="server" ControlToValidate="ddlState" InitialValueErrorText="0" ValidationGroup="PrimaryRegistartion" ErrorText="Please select Facility State"  Display="Dynamic" CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                                        <asp:Label ID="lblStewardshipExists" runat="server" CssClass="custom-error" Text="Stewardship already exists for this state." Visible="false"></asp:Label>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Country:")%></strong>
                                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="" OnSelectedIndexChanged="dllCountry_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
<cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator12" runat="server"  ControlToValidate="ddlCountry" InitialValueErrorText="0" ValidationGroup="PrimaryRegistartion" ErrorText="Please select Facility Country"  Display="Dynamic" CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Country Abbreviation:")%></strong>
                                                    <asp:TextBox ID="txtCountryAbbreviation" runat="server" ></asp:TextBox>
                                                </p>
                                            </li>
                                        </ul>

                                    </div>
                                    <div class="col-md-5 col-md-offset-1">
                                        <h2><%= ResourceMgr.GetMessage("Organization Information")%></h2>
                                        <ul class="todo-list m-t custom-todo">
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Name:")%></strong>
                                                    <asp:TextBox ID="txtBusinessName" runat="server"></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("DBA Name:")%></strong>
                                                    <asp:TextBox ID="txtDBAName" runat="server"></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Organization:")%></strong>
                                                    <asp:TextBox ID="txtOrganization" runat="server"></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Type:")%></strong>
                                                    <asp:TextBox ID="txtBusinessType" runat="server"></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Website:")%></strong>
                                                    <asp:TextBox ID="txtWebsite" runat="server"></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Address1:")%></strong>
                                                    <asp:TextBox ID="txtBusinessAddress1" runat="server"></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Address2:")%></strong>
                                                    <asp:TextBox ID="txtBusinessAddress2" runat="server"></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Phone Type:")%></strong>
                                                    <asp:TextBox ID="txtBusinessPhoneType" runat="server"></asp:TextBox>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <strong><%= ResourceMgr.GetMessage("Business Text Messages:")%></strong>
                                                    <asp:DropDownList ID="ddlbusinesstextmsgs" Enabled="false" Visible="true" runat="server">
            <asp:ListItem Text="Yes" Value="1" ></asp:ListItem>
              <asp:ListItem Text="No" Value="2" ></asp:ListItem>
            </asp:DropDownList>
                                                </p>
                                            </li>
                                        </ul>
                                        <div class="row m-t-md row m-b-md">
                                            <div class="col-md-12">
                                                <asp:LinkButton CssClass="btn btn-xs btn-white" ID="lnkbtnUpdate" runat="server" Text="Update" ToolTip="Update stakeholder information" OnClick="lnkbtnUpdate_Click">
                                                    <i class="fa fa-upload"></i> Update
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="btn btn-xs btn-white" ID="lnkbtnCancel" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="lnkbtnCancel_Click">
                                                    <i class="fa fa-times"></i> Cancel
                                                </asp:LinkButton>
                                                <div style="visibility: hidden;">
                                                    <h5><%= ResourceMgr.GetMessage("Certifications")%></h5>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="stv_rightOuter">

    <div class="stv_grid-contain-outer">
     <div class="stv_txt-main-had">
            
        </div>
        

         
    </div>
    </div>
                                    </div>
                                </div>
    
    
    </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


<div class="stv_parent_gridouter">
 

            

        
   
    
    
    
</div>





</asp:Content>


