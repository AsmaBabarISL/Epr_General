<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="AddCreditCard.aspx.cs" Inherits="Creditcard_AddCreditCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />

    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowLoginErrorMessage() {
            $("#LoginNameExists").show();
        }


        function HideLoginErrorMessage() {
            $("#LoginNameExists").hide();
        }

        function isNumeric(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;

        }

        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }



    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SetDatePicket() {

            $(".datepicker").datepicker({ minDate: new Date });

        }

        function CardRangeValidator(sender, args) {
            args.IsValid = false;
            var val = $('#<%=txtcardNo.ClientID %>').val();
            if (val.length >= 14 && val.length <= 16) {
                args.IsValid = true;
                return true;
            }
            return false;

        }

        function cvRangeValidator(sender, args) {
            args.IsValid = false;
            var val = $('#<%=txtCV2Code.ClientID %>').val();
            if (val.length >= 3 && val.length <= 4) {
                args.IsValid = true;
                return true;
            }
            return false;
        }


    </script>

    <div id="dverror" visible="false" runat="server" class="custom-error">
        <asp:Label ID="lblerror" runat="server" CssClass="custom-error"></asp:Label>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>
                        <asp:Label ID="lblCreditCardTitle" runat="server"></asp:Label></h5>
                </div>
                <div class="ibox-content" style="display: block;">
                    <!-- Form-->
                    <div role="form" id="">
                        <div class="row">
                                <div class="form-group col-md-4 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Card Type")%></label>
                                    <asp:DropDownList ID="ddlCardType" runat="server"
                                        OnSelectedIndexChanged="ddlCardType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" ValidationGroup="AddadminUserValidationGroup"
                                        InitialValue="0" CssClass="custom-error" runat="server" ErrorText="Select Card Type" ControlToValidate="ddlCardType"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Card#")%></label>
                                    <asp:TextBox ID="txtcardNo" runat="server" MaxLength="16" class="form-control" onkeypress="return isNumeric(event);"></asp:TextBox>
                                    <asp:Label ID="lblerrorcredit" runat="server"></asp:Label>
                                    <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator2" ControlToValidate="txtcardNo" ErrorText="Only numeric values with length 14-16." ValidationExpression="^(0|[1-9][0-9]{13,16})$" ValidationGroup="AddadminUserValidationGroup" Display="Dynamic" CssClass="custom-error"
                                        runat="server"></cc1:ResourceRegularExpressionValidator>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvLogin" ValidationGroup="AddadminUserValidationGroup"
                                        CssClass="custom-error" runat="server" ErrorText="Enter Card#" ControlToValidate="txtCardNo"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    <%--<cc1:ResourceCustomValidator ID="CustomValidator1" runat="server" CssClass="custom-error" ErrorMessage="*Enter range between 14-16" ControlToValidate="txtcardNo"
                                        ClientValidationFunction="CardRangeValidator" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceCustomValidator>--%>
                                </div>

                                

                                <div class="form-group col-md-4 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Card Name")%></label>
                                    <asp:TextBox ID="txtCardName" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvFirstName" ValidationGroup="AddadminUserValidationGroup"
                                        CssClass="custom-error" runat="server" ErrorText="Enter Card Name" ControlToValidate="txtCardName"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>

                                    <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator3" ControlToValidate="txtCardName" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" ValidationGroup="AddadminUserValidationGroup" Display="Dynamic" CssClass="custom-error"
                                        runat="server"></cc1:ResourceRegularExpressionValidator>
                                </div>
                                <div class="form-group col-md-4 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("CV2 Code")%></label>
                                    <asp:TextBox ID="txtCV2Code" runat="server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                                    <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator1" ControlToValidate="txtCV2Code" ErrorText="Only numeric values with length 3-4" ValidationExpression="^(0|[1-9][0-9]{2,4})$" ValidationGroup="AddadminUserValidationGroup" Display="Dynamic" CssClass="custom-error"
                                        runat="server"></cc1:ResourceRegularExpressionValidator>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvCV2Code" ValidationGroup="AddadminUserValidationGroup"
                                        CssClass="custom-error" runat="server" ErrorText="Enter CV2 Code" ControlToValidate="txtCV2Code"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>

                                   <%-- <cc1:ResourceCustomValidator ID="CustomValidator2" runat="server" CssClass="custom-error" ErrorMessage="*CV2 code is 3-4 digit code" ControlToValidate="txtCV2Code"
                                        ClientValidationFunction="cvRangeValidator" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceCustomValidator>--%>

                                </div>

                            <div class="form-group col-md-4 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Expiration Date")%></label>
                                    <div class="row">
                                        <div class="col-xs-4 pr0">
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">00</asp:ListItem>
                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                <asp:ListItem Value="09">09</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                            </asp:DropDownList>
                                            <sup class="text-muted">Month</sup>
                                        </div>
                                        <div class="col-xs-1" style="font-size: 20px;">/</div>
                                        <div class="col-xs-4 pl0">
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="13">13</asp:ListItem>
                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                            </asp:DropDownList>
                                            <sup class="text-muted">Year</sup>
                                        </div>
                                    </div>
                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator2" ValidationGroup="AddadminUserValidationGroup"
                                        InitialValue="0" CssClass="custom-error" runat="server" ErrorText="Select Month" ControlToValidate="ddlMonth"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                <br />
                                </div>

                            <div class="form-group col-md-12 mb0">
                                  <asp:LinkButton ID="lnkbtnAddCreditCard" runat="server" ValidationGroup="AddadminUserValidationGroup"
                                        CausesValidation="true" CssClass="btn btn-primary btn-sm font-bold"
                                        OnClientClick="HideLoginErrorMessage();" OnClick="lnkbtnAddInventory_Click" >Add</asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnUpdateCreditCard" runat="server" ValidationGroup="AddadminUserValidationGroup"
                                        CausesValidation='true' CssClass="btn btn-primary btn-sm font-bold"
                                        OnClientClick="HideLoginErrorMessage();"
                                        OnClick="lnkbtnUpdateCreditInfo_Click" Visible="false">Update</asp:LinkButton>
                                      <asp:LinkButton ID="lnkbtnCancelInventory" runat="server" CausesValidation="false"
                                        CssClass="btn btn-white btn-sm font-bold" OnClick="lnkbtnCancelInventory_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>

