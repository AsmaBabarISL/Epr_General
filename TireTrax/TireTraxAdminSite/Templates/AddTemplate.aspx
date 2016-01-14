<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="AddTemplate.aspx.cs" Inherits="Templates_AddTemplate" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <link href="/Scripts/themes/ui-lightness/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="/ckeditor/ckeditor.js"></script>
    <script type="text/javascript">

        function AddTage(tag_value) {
            
            
            var oEditor = CKEDITOR.instances.<%=CKEditor1.ClientID %>;
            oEditor.insertText(tag_value);
        }
        $(function () {
            $('.descTooltip').tooltip();
        });
    </script>

    <div class="add-inventory-outer_block">
        <div class="add-inventory-title_block">
            <span><%=ResourceMgr.GetMessage("Create Template")%></span>

        </div>



        <div class="new_inventory-block">
            <div class="inv_title">
               <%=ResourceMgr.GetMessage("Template Name:")%> 
            </div>
            <div class="inv_field">
                <asp:TextBox ID="txtTemplateName" runat="server" class="field_block"></asp:TextBox><br />
                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator4" ValidationGroup="AddTemplateValidationGroup"
                    CssClass="error_message" runat="server" ErrorText="Please enter template Name!" ControlToValidate="txtTemplateName"
                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
            </div>
        </div>
        <div class="new_inventory-block">
            <div class="inv_title">
                <%=ResourceMgr.GetMessage("Template Type:")%> 
            </div>
            <div class="inv_field">
                <asp:DropDownList class="sct_field sct_field" ID="ddlTemplateType" runat="server" >
                </asp:DropDownList>
                <br />
                <cc1:ResourceRequiredFieldValidator ID="rsvldreqddlRecycleState" runat="server" InitialValue="0"
                    ValidationGroup="AddTemplateValidationGroup" ErrorText="Please select Template Type"
                    ControlToValidate="ddlTemplateType" CssClass="error_message" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
            </div>
        </div>
        <div class="new_inventory-block">
            <div class="inv_title">
                <%=ResourceMgr.GetMessage("Invoice Type:")%> 
            </div>
            <div class="inv_field">
               <asp:RadioButtonList ID="rdInvoiceType" runat="server">
                   <asp:ListItem Value="2" Text='Commulative' Selected="True"></asp:ListItem>          
                   <asp:ListItem Value="1" Text='Single'></asp:ListItem>
                              
               </asp:RadioButtonList>
            </div>
        </div>
        
        <div class="new_inventory-block">
        </div>

        <div class="tmEditor" style="float: left; width: 810px;">
            <CKEditor:CKEditorControl ID="CKEditor1" runat="server"  EditingBlock="true"  Width="810"  CssClass="maxH" ></CKEditor:CKEditorControl>

            
        </div>
        <div class="tmEditorTag" style="float: right;">
            <h1>Insert Data Tags</h1>
            <div class="tmEditorTag_inner">
                <a title="Organization Name to which you are sending the invoice" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_To_Name")%>]]');">[[<%= ResourceMgr.GetMessage("Organization_To_Name")%>]]</a>
            </div>
            <div class="tmEditorTag_inner">
                <a title="Organization Address to which you are sending the invoice" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_To_Address")%>]]');">[[<%= ResourceMgr.GetMessage("Organization_To_Address")%>]]</a>
            </div>
            <div class="tmEditorTag_inner">
                <a title="Organization Phone to which you are sending the invoice" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_To_Phone")%>]]');">[[<%= ResourceMgr.GetMessage("Organization_To_Phone")%>]]</a>
            </div>
            <div class="tmEditorTag_inner">
                <a title="Your Organization Logo" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_To_Logo")%>]]');">[[<%= ResourceMgr.GetMessage("Organization_To_Logo")%>]]</a>
            </div>
            <div class="tmEditorTag_inner">
                <a title="Your Organization Name" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_From_Name")%>]]');">[[<%= ResourceMgr.GetMessage("Organization_From_Name")%>]]</a>
            </div>
            <div class="tmEditorTag_inner">
                <a title="Your Organization Address" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_From_Address")%>]]');">[[<%= ResourceMgr.GetMessage("Organization_From_Address")%>]]</a>
            </div>
            <div class="tmEditorTag_inner">
                <a title="Your Organization Logo" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_From_Logo")%>]]');">[[<%= ResourceMgr.GetMessage("Organization_From_Logo")%>]]</a>
            </div>
            <div class="tmEditorTag_inner">
                <a title="Your Organization Phone Number" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_From_Phone")%>]]');">[[<%= ResourceMgr.GetMessage("Organization_From_Phone")%>]]</a>
            </div>
            <div class="tmEditorTag_inner">
                <a title="Your Organization Invoice data" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("InvoiceData")%>]]');">[[<%= ResourceMgr.GetMessage("InvoiceData")%>]]</a>
            </div>


        </div>

        <br clear="all" />
        <div class="inv_field">
            <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="reg_button" OnClick="lnkbtnCancel_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
            <asp:LinkButton ID="lnkbtnAddDelivery" runat="server" ValidationGroup="AddTemplateValidationGroup"
                CausesValidation="true" CssClass="reg_button" OnClick="lnkbtnAddDelivery_Click"><%= ResourceMgr.GetMessage("Save")%></asp:LinkButton>
        </div>

        <br clear="all" />
    </div>
</asp:Content>

