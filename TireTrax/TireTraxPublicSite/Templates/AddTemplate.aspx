<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="AddTemplate.aspx.cs" Inherits="Templates_AddTemplate" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="/Scripts/themes/ui-lightness/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="/ckeditor/ckeditor.js"></script>
    <script type="text/javascript">

        $(function () { ApplyWaterMarkOnTextBoxes(); });

        function ApplyWaterMarkOnTextBoxes() {
            
            $("input[WaterMarkText],textarea[WaterMarkText]").each(function () {
                var $this = $(this);
                if ($this.attr("WaterMarkText") != null) {
                    var maxLen = $this.attr("MaxLength");

                    if (maxLen) {
                        $this.attr("tempMaxLength", maxLen);
                        $this.removeAttr("MaxLength");
                    }

                    $this.blur(function () {
                        
                        if ($this.val() == '') {
                            var maxLen = $this.attr("MaxLength");
                            if (maxLen) {
                                $this.attr("tempMaxLength", maxLen);
                                $this.removeAttr("MaxLength");
                            }
                            $this.val($this.attr("WaterMarkText")).addClass("WaterMark");
                        }
                    });
                    $this.focus(function () {
                        if ($this.val() == $this.attr("WaterMarkText")) {
                            if ($this.attr("tempMaxLength")) {
                                $this.attr("MaxLength", $this.attr("tempMaxLength"));
                            }
                            $this.val('').removeClass("WaterMark");
                        }
                    });

                    if ($(this).val() == '' || $(this).val() == $(this).attr("WaterMarkText")) {
                        $this.val($this.attr("WaterMarkText")).addClass("WaterMark");
                    }
                }
            });
        }

        function AddTage(tag_value) {
            
            
            var oEditor = CKEDITOR.instances.<%=CKEditor1.ClientID %>;
            oEditor.insertText(tag_value);
        }
        $(function () {
            $('.descTooltip').tooltip();
        });

        
        function noScriptInValue(sender, args) {
            args.IsValid = false;
            var val = document.getElementById(sender.controltovalidate).value;
            
            if (!(val.indexOf("script") > -1)) {
                args.IsValid = true;
                return true;
            }
            return false;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%=ResourceMgr.GetMessage("Create Template")%></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content" style="display: block;">
                    <!-- Form-->
                    <div role="form" id="">
                        <div class="row">

                            <div class="form-group col-md-9 col-sm-6">
                                <label><%=ResourceMgr.GetMessage("Template Name")%> </label>
                                <asp:TextBox ID="txtTemplateName" runat="server" class="form-control"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator4" ValidationGroup="AddTemplateValidationGroup"
                                    CssClass="custom-error" runat="server" ErrorText="Please enter template Name!" ControlToValidate="txtTemplateName"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                <%--<cc1:ResourceCustomValidator ID="CustomValidator39" runat="server" CssClass="custom-error" ErrorMessage="Template name should not contain script" ControlToValidate="txtTemplateName"
                                    ClientValidationFunction="noScriptInValue" Display="Dynamic" ValidationGroup="AddTemplateValidationGroup" ></cc1:ResourceCustomValidator>--%>
                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator2"
                                    ControlToValidate="txtTemplateName" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$"
                                    ValidationGroup="AddTemplateValidationGroup" Display="Dynamic" CssClass="custom-error"
                                    runat="server"></cc1:ResourceRegularExpressionValidator>

                            </div>
                            <asp:UpdatePanel runat="server" ID="updatePanel1">
                                <ContentTemplate>
                                    <div class="form-group col-md-3 col-sm-6">
                                        <label><%=ResourceMgr.GetMessage("Template Type")%> </label>
                                        <asp:DropDownList class="form-control" ID="ddlTemplateType" OnSelectedIndexChanged="ddlTemplateType_SelectedIndexChanged" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>

                                        <cc1:ResourceRequiredFieldValidator ID="rsvldreqddlRecycleState" runat="server" InitialValue="0"
                                            ValidationGroup="AddTemplateValidationGroup" ErrorText="Please select Template Type"
                                            ControlToValidate="ddlTemplateType" CssClass="custom-error" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>

                                    </div>
                                    <div id="divInvoiceType" runat="server" visible="false" class="form-group col-md-9 col-sm-6">
                                        <label><%=ResourceMgr.GetMessage("Invoice Type")%></label>
                                        <asp:RadioButtonList ID="rdInvoiceType" runat="server">
                                            <asp:ListItem Value="2" Text='Commulative' Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text='Single'></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <div class="row">
                            <div class="col-md-9">
                                <div class="tmEditor form-group">
                                    <CKEditor:CKEditorControl ID="CKEditor1" runat="server" EditingBlock="true" CssClass="maxH"></CKEditor:CKEditorControl>
                                </div>
                                <div class="form-group">
                                    <asp:LinkButton ID="lnkbtnAddDelivery" runat="server" ValidationGroup="AddTemplateValidationGroup"
                                        CausesValidation="true" CssClass="btn btn-sm btn-primary font-bold" OnClick="lnkbtnAddDelivery_Click"><%= ResourceMgr.GetMessage("Save")%></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn btn-sm btn-white font-bold" OnClick="lnkbtnCancel_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <h4>Insert Data Tags</h4>
                                <div class="tmEditorTag project-manager">
                                    <ul class="tag-list" style="padding: 0">
                                        <li><a title="Organization Name to which you are sending the invoice" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_To_Name")%>]]');">
                                            <i class="fa fa-tag"></i>[[<%= ResourceMgr.GetMessage("Organization_To_Name")%>]]</a>
                                        </li>
                                        <li><a title="Organization Phone to which you are sending the invoice" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_To_Phone")%>]]');">
                                            <i class="fa fa-tag"></i>[[<%= ResourceMgr.GetMessage("Organization_To_Phone")%>]]</a>
                                        </li>
                                        <li><a title="Your Organization Logo" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_To_Logo")%>]]');">
                                            <i class="fa fa-tag"></i>[[<%= ResourceMgr.GetMessage("Organization_To_Logo")%>]]</a>
                                        </li>
                                        <li><a title="Your Organization Name" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_From_Name")%>]]');">
                                            <i class="fa fa-tag"></i>[[<%= ResourceMgr.GetMessage("Organization_From_Name")%>]]</a>
                                        </li>
                                        <li><a title="Your Organization Address" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_From_Address")%>]]');">
                                            <i class="fa fa-tag"></i>[[<%= ResourceMgr.GetMessage("Organization_From_Address")%>]]</a>
                                        </li>
                                        <li><a title="Your Organization Logo" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_From_Logo")%>]]');">
                                            <i class="fa fa-tag"></i>[[<%= ResourceMgr.GetMessage("Organization_From_Logo")%>]]</a>
                                        </li>
                                        <li><a title="Your Organization Phone Number" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("Organization_From_Phone")%>]]');">
                                            <i class="fa fa-tag"></i>[[<%= ResourceMgr.GetMessage("Organization_From_Phone")%>]]</a>
                                        </li>
                                        <li><a title="Your Organization Invoice data" onclick="javascript:AddTage('[[<%= ResourceMgr.GetMessage("InvoiceData")%>]]');">
                                            <i class="fa fa-tag"></i>[[<%= ResourceMgr.GetMessage("InvoiceData")%>]]</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>


</asp:Content>

