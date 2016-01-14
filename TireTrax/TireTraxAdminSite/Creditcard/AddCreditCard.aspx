<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="AddCreditCard.aspx.cs" Inherits="Creditcard_AddCreditCard" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">   
<script type="text/javascript">
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


    function ShowLoginErrorMessage() {
        $("#LoginNameExists").show();
    }


    function HideLoginErrorMessage() {
        $("#LoginNameExists").hide();
    }


    
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>
                        <asp:Label ID="lblCreditCardTitle" runat="server"></asp:Label>
                    </h5>
                </div>
                <div class="ibox-content">
                    <!-- Form-->
                    <div role="form" id="" class="row">
                        <div id="dverror" visible="false" runat="server">
                            <asp:Label ID="lblerror" runat="server" CssClass="alert-danger custom-absolute-alert"></asp:Label>
                        </div>

                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label><%= ResourceMgr.GetMessage("Card Type")%></label>
                            <asp:DropDownList ID="ddlCardType" runat="server"
                                OnSelectedIndexChanged="ddlCardType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                            </asp:DropDownList>
                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" ValidationGroup="AddadminUserValidationGroup"
                                InitialValue="0" CssClass="custom-error" runat="server" ErrorText="Select Card Type" ControlToValidate="ddlCardType"
                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                        </div>

                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label><%= ResourceMgr.GetMessage("Card#")%></label>
                            <asp:TextBox ID="txtcardNo" runat="server" MaxLength="16" class="form-control"></asp:TextBox>
                            <asp:Label ID="lblerrorcreditt" CssClass="custom-error" runat="server"></asp:Label>
                            <cc1:ResourceRequiredFieldValidator ID="rfvLogin" ValidationGroup="AddadminUserValidationGroup"
                                CssClass="custom-error" runat="server" ErrorText="Enter Card#" ControlToValidate="txtcardNo"
                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator2" ControlToValidate="txtcardNo"
                                ErrorText="Enter Only Numeric" ValidationExpression="^(0|[1-9][0-9]*)$" ValidationGroup="AddadminUserValidationGroup" Display="Dynamic"
                                CssClass="custom-error" runat="server"></cc1:ResourceRegularExpressionValidator>
                            <cc1:ResourceCustomValidator ID="CustomValidator1" runat="server" CssClass="custom-error" ErrorMessage="*Enter range between 14-16" ControlToValidate="txtcardNo"
                                ClientValidationFunction="CardRangeValidator" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceCustomValidator>
                        </div>

                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label><%= ResourceMgr.GetMessage("Card Name")%></label>
                            <asp:TextBox ID="txtCardName" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:ResourceRequiredFieldValidator ID="rfvFirstName" ValidationGroup="AddadminUserValidationGroup"
                                CssClass="custom-error" runat="server" ErrorText="Enter Card Name" ControlToValidate="txtCardName"
                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>

                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator3" ControlToValidate="txtCardName" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" ValidationGroup="AddadminUserValidationGroup" Display="Dynamic" CssClass="custom-error"
                                runat="server"></cc1:ResourceRegularExpressionValidator>
                        </div>

                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label><%= ResourceMgr.GetMessage("CV2 Code")%></label>
                            <asp:TextBox ID="txtCV2Code" runat="server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator1" ControlToValidate="txtCV2Code"
                                ErrorText="Enter Only Numeric" ValidationExpression="^(0|[1-9][0-9]*)$" ValidationGroup="AddadminUserValidationGroup"
                                Display="Dynamic" CssClass="custom-error"
                                runat="server"></cc1:ResourceRegularExpressionValidator>
                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator6" ValidationGroup="AddadminUserValidationGroup"
                                CssClass="custom-error" runat="server" ErrorText="Enter CV2 Code" ControlToValidate="txtCV2Code"
                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>

                            <cc1:ResourceCustomValidator ID="CustomValidator2" runat="server" CssClass="custom-error"
                                ErrorMessage="*CV2 code is 3-4 digit code" ControlToValidate="txtCV2Code"
                                ClientValidationFunction="cvRangeValidator" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup">

                            </cc1:ResourceCustomValidator>

                        </div>

                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                            <label><%= ResourceMgr.GetMessage("Expiration Date")%></label>
                            <div class="row">
                                <div class="col-xs-3 col-md-4 pr0">
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
                                <div class="col-xs-3 col-md-4 pl0">
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
                        </div>

                        <div class="form-group col-sm-12 mb0">
                            <asp:LinkButton ID="lnkbtnAddCreditCard" runat="server" ValidationGroup="AddadminUserValidationGroup"
                                CausesValidation="true" CssClass="btn btn-primary btn-sm font-bold"
                                OnClientClick="HideLoginErrorMessage();" OnClick="lnkbtnAddInventory_Click">Add</asp:LinkButton>
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




            



</asp:Content>

