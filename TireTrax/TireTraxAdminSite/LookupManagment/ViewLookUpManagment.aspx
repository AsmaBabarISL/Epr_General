<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewLookUpManagment.aspx.cs" Inherits="LookupManagment_ViewLookUpManagment" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">


        function fill() {

            var lstvalue = document.getElementById('<%= lstvalue.ClientID %>');

            var selectedindex = lstvalue.selectedIndex;
            var selectedItem = lstvalue.options[selectedindex].value;

            var btnadd = document.getElementById('<%= divadd.ClientID %>');
            var btnupdate = document.getElementById('<%= divupdate.ClientID %>');
            var btndelete = document.getElementById('<%= divdelete.ClientID %>');
            var btnCancel = document.getElementById('<%= divCancel.ClientID %>');
            if (selectedindex == -1) {
                btndelete.style.display = 'none';
                btnadd.style.display = 'inline';
                btnupdate.style.display = 'none';
                btnCancel.style.display = 'none';
            }
            else {
                btnadd.style.display = 'none';
                btnupdate.style.display = 'inline';
                btndelete.style.display = 'inline';
                btnCancel.style.display = 'inline';
            }
            var txtLegalName = document.getElementById('<%= txtLegalName.ClientID %>');

            txtLegalName.value = lstvalue.options[selectedindex].text;
        }

        function fillSubType() {

            var lstvalue = document.getElementById('<%= lstSubType.ClientID %>');

         var selectedindex = lstvalue.selectedIndex;
         var selectedItem = lstvalue.options[selectedindex].value;

         var btnadd = document.getElementById('<%= divSubAdd.ClientID %>');
         var btnupdate = document.getElementById('<%= divSubUpdate.ClientID %>');
         var btndelete = document.getElementById('<%= divSubDelete.ClientID %>');
         var btnCancel = document.getElementById('<%= divSubCancel.ClientID %>');
         if (selectedindex == -1) {

             btndelete.style.display = 'none';
             btnadd.style.display = 'inline';
             btnupdate.style.display = 'none';
             btnCancel.style.display = 'none';
         }
         else {
             btnadd.style.display = 'none';
             btnupdate.style.display = 'inline';
             btndelete.style.display = 'inline';
             btnCancel.style.display = 'inline';
         }
         var txtLegalName = document.getElementById('<%= txtSubType.ClientID %>');

         txtLegalName.value = lstvalue.options[selectedindex].text;
     }

     function fillTypeSubType() {

         var lstvalue = document.getElementById('<%= lstSubType_Type.ClientID %>');

         var selectedindex = lstvalue.selectedIndex;
         var selectedItem = lstvalue.options[selectedindex].value;

         var btnadd = document.getElementById('<%= divAddSubType_Type.ClientID %>');
         var btnupdate = document.getElementById('<%= divUpdateSubType_Type.ClientID %>');
         var btndelete = document.getElementById('<%= divDeleteSubType_Type.ClientID %>');
         var btnCancel = document.getElementById('<%= divCancelSubType_Type.ClientID %>');
         var btnUpperDelete = document.getElementById('<%= divSubDelete.ClientID %>');
         if (selectedindex == -1) {
             btndelete.style.display = 'none';
             btnadd.style.display = 'inline';
             btnupdate.style.display = 'none';
             btnCancel.style.display = 'none';

         }
         else {
             btnadd.style.display = 'none';
             btnupdate.style.display = 'inline';
             btndelete.style.display = 'inline';
             btnCancel.style.display = 'inline';
             btnUpperDelete.style.display = 'none';
         }
         var txtLegalName = document.getElementById('<%= txtSubType_Type.ClientID %>');

         txtLegalName.value = lstvalue.options[selectedindex].text;
     }

     function ConfirmUpdate() {
         if (!confirm("This action will update the selected lookup value.\n\nAre you sure you want to continue?")) return false;
         else return true;
     }
     function ConfirmDelete() {
         if (!confirm("This action will delete the selected lookup value.\n\nAre you sure you want to continue?")) return false;
         else return true;
     }
     function Validate() {
         var txtdescription = document.getElementById('<%= txtdescription.ClientID %>');
         if (txtdescription.value == "") {
             alert("Please specify the description.");
             txtdescription.focus();
             return false;
         }
         return true;
     }



     function validateRole(sender, args) {
         args.IsValid = false;
         var objCheckBoxList = document.getElementById('<%=chkRoles.ClientID%>');
         var arrayOfCheckBoxes = objCheckBoxList.getElementsByTagName("input");
         if (objCheckBoxList != null) {
             for (var i = 0; i < arrayOfCheckBoxes.length; i++) {
                 if (arrayOfCheckBoxes[i].checked == true) {
                     args.IsValid = true;
                     return true;
                 }
             }
         }
         return false;

     }
    </script>
    <%--    function ShowInsertTable() {
       var divInsertTable = document.getElementById('<%= divInsertTable.ClientID %>');
        if (divInsertTable.style.display == "none")
            divInsertTable.style.display = "inline";
        else
            divInsertTable.style.display = "none";
                
    }--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Lookup Management") %></h5>
                            </div>
                            <div class="ibox-content" style="display: block;">
                                <div role="form" class="row" id="">
                                    <div>
                                        <asp:ValidationSummary ID="vldsum" CssClass="alert alert-danger" runat="server" ValidationGroup="Add" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" />
                                        <asp:Label ID="lblerror" CssClass="custom-error" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lbl_msg" runat="server" CssClass="messagewithgreen" Text="[lbl_msg]"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <asp:Label ID="lbllookuptable" runat="server" Text='<%# ResourceMgr.GetMessage("Lookup Table:")%>' class="text_gnrc"></asp:Label></label>
                                                <asp:DropDownList CssClass="form-control" AutoPostBack="true" ID="ddllookupstable" runat="server"
                                                    OnSelectedIndexChanged="ddllookupstable_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="vldreqddllookupstable" runat="server" Text="*" ErrorMessage="Please select Lookup Table" InitialValue="0"
                                                    CssClass="custom-error" ControlToValidate="ddllookupstable" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <lable> <asp:Label ID="lbl_SectionType" runat="server" Text='<%# ResourceMgr.GetMessage("Section Type:")%>' class="text_gnrc"
                                                        Visible="false"></asp:Label></lable>
                                                <asp:DropDownList CssClass="form-control" AutoPostBack="true" ID="ddl_SectionType" runat="server"
                                                    Visible="false" OnSelectedIndexChanged="ddl_SectionType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <lable>  <asp:Label ID="lbl_ComponentType" runat="server" Text='<%# ResourceMgr.GetMessage("Component Type:")%>' class="text_gnrc"
                                                        Visible="false"></asp:Label></lable>
                                                <asp:DropDownList CssClass="form-control" AutoPostBack="true" ID="ddl_ComponentType" runat="server"
                                                    Visible="false" OnSelectedIndexChanged="ddl_ComponentType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <lable> <asp:Label ID="lbl_SchoolType" runat="server" Text='<%# ResourceMgr.GetMessage("School Type:")%>' class="text_gnrc"
                                                        Visible="false"></asp:Label></lable>
                                                <asp:DropDownList CssClass="form-control" AutoPostBack="true" ID="ddl_SchoolType" runat="server"
                                                    Visible="false" OnSelectedIndexChanged="ddl_SchoolType_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="form-group col-md-12">
                                                <lable><asp:Label ID="lbl_LocationType" runat="server" Text='<%# ResourceMgr.GetMessage("Location Type:")%>' class="text_gnrc"
                                                        Visible="false"></asp:Label></lable>
                                                <asp:DropDownList CssClass="form-control" AutoPostBack="true" ID="ddl_LocationType" runat="server"
                                                    Visible="false" OnSelectedIndexChanged="ddl_LocationType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:Panel runat="server" ID="pnl_material" Visible="false">
                                                    <div class="form-group col-md-12">
                                                        <lable>  M2:</lable>
                                                        <asp:DropDownList ID="ddlMaterial2" CssClass="form-control" runat="server" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlMaterial2_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <lable> M3: </lable>
                                                        <asp:DropDownList ID="ddlMaterial3" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>

                                                </asp:Panel>
                                            </div>
                                        </div>

                                        <div class="col-md-5  col-sm-6">
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <asp:Label class="text_gnrc" ID="lblvalue" runat="server" Text='<%# ResourceMgr.GetMessage("Select Value:")%>'></asp:Label></label>
                                                <div class="row">
                                                    <div class="col-md-10 col-sm-11 col-xs-11">
                                                        <asp:ListBox ID="lstvalue" wrap='on' SelectionMode="Single" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstvalue_SelectedIndexChanged"></asp:ListBox>
                                                    </div>
                                                    <div class="col-md-2 col-xs-1 p0 m-t-md">
                                                        <asp:LinkButton CssClass="btn btn-xs btn-white btn-bitbucket" ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" Visible="False">
                                                            <i class="fa fa-arrow-up"></i>
                                                        </asp:LinkButton>
                                                        <br />
                                                        <asp:LinkButton CssClass="btn btn-xs btn-white btn-bitbucket" ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" Visible="False">
                                                            <i class="fa fa-arrow-down"></i>
                                                        </asp:LinkButton>

                                                    </div>
                                                </div>
                                            </div>


                                            <div class="form-group col-md-10 col-sm-11">
                                                <label>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# ResourceMgr.GetMessage("Name:")%>'></asp:Label>
                                                </label>
                                                <asp:TextBox ID="txtLegalName" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*" ErrorMessage="Please enter Name"
                                                    CssClass="custom-error" ControlToValidate="txtLegalName" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-10 col-sm-11">
                                                <label>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# ResourceMgr.GetMessage("Description:")%>'></asp:Label>
                                                </label>
                                                <asp:TextBox ID="txtdescription" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-md-10 col-sm-11" runat="server" id="trcommercial" visible="false">
                                                <label><%= ResourceMgr.GetMessage(" Business Type:")%></label>
                                                <asp:CheckBoxList ID="chkRoles" CssClass="form-control" runat="server" RepeatColumns="2"
                                                    RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="* Please select role(business type) first"
                                                    ClientValidationFunction="validateRole" Display="Dynamic" ValidationGroup="Add" CssClass="custom-error"></asp:CustomValidator>
                                                <asp:CheckBox ID="chk_IsCaller" runat="server" Text="IsCaller" />
                                                <asp:CheckBox ID="chk_IsInternet" runat="server" Text="IsInternet" />
                                                <asp:CheckBox ID="chk_OrderList" runat="server" Text="Want to Reorder Section" AutoPostBack="true"
                                                    Visible="false" OnCheckedChanged="chk_OrderList_CheckedChanged" />
                                            </div>


                                            <div class="form-group col-sm-12">
                                                <div id="divadd" runat="server" style="float: left; margin-right: 5px;">
                                                    <cc1:ResourceLinkButton ID="btnAdd" runat="server" CssClass="btn btn-sm btn-primary font-bold" ValidationGroup="Add" TextMessage="Add"
                                                        OnClick="btnAdd_Click"></cc1:ResourceLinkButton>
                                                </div>
                                                <div id="divupdate" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                    <cc1:ResourceLinkButton ID="btnUpdate" runat="server" CssClass="btn btn-sm btn-primary font-bold" ValidationGroup="Add" TextMessage="Update"
                                                        OnClick="btnUpdate_Click"></cc1:ResourceLinkButton>
                                                </div>
                                                <div id="divdelete" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                    <cc1:ResourceLinkButton ID="btnDelete" runat="server" CssClass="btn btn-sm btn-primary font-bold" TextMessage="Delete" OnClick="btnDelete_Click"></cc1:ResourceLinkButton>
                                                </div>
                                                <div id="divCancel" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                    <cc1:ResourceLinkButton ID="btnCancel" runat="server" CssClass="btn btn-sm btn-white font-bold" class="btn" TextMessage="Cancel" CausesValidation="False"
                                                        OnClick="btnCancel_Click"></cc1:ResourceLinkButton>
                                                </div>
                                            </div>

                                            <asp:Panel ID="pnlSubType" runat="server" Visible="false">
                                                <div class="form-group col-md-12">
                                                    <label>
                                                        <asp:Label ID="Label3" runat="server" Text="Select LookUp Sub Type:"></asp:Label></label>

                                                    <div class="row">
                                                        <div class="col-md-10 col-sm-11 col-xs-11">
                                                            <asp:ListBox ID="lstSubType" wrap='on' SelectionMode="Single" CssClass="form-control"
                                                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstSubType_SelectedIndexChanged"></asp:ListBox>
                                                        </div>
                                                        <div class="col-md-2 col-xs-1 p0 m-t-md">
                                                            <asp:LinkButton CssClass="btn btn-xs btn-white btn-bitbucket" ID="ImageButton3" runat="server" OnClick="ImageButton3_Click">
                                                                <i class="fa fa-arrow-up"></i>
                                                            </asp:LinkButton>
                                                            <br />
                                                            <asp:LinkButton CssClass="btn btn-xs btn-white btn-bitbucket" ID="ImageButton4" runat="server" OnClick="ImageButton4_Click">
                                                                <i class="fa fa-arrow-down"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="form-group col-md-10 col-sm-11">
                                                    <label>
                                                        <asp:Label ID="Label4" runat="server" Text="Name:"></asp:Label></label>
                                                    <asp:TextBox ID="txtSubType" CssClass="form-control" runat="server" MaxLength="59"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*" ErrorMessage="Please enter Name"
                                                        CssClass="custom-error" ControlToValidate="txtSubType" ValidationGroup="SubAdd"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10 col-sm-11">
                                                    <label>
                                                        <asp:Label ID="Label5" runat="server" Text="Description:"></asp:Label></label>
                                                    <asp:TextBox ID="txtSubDescription" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*" ErrorMessage="Please enter Description"
                                                    CssClass="error" ControlToValidate="txtSubDescription" ValidationGroup="SubAdd"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-md-10 col-sm-11">
                                                    <div id="divSubAdd" runat="server" style="float: left; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton1" runat="server" CssClass="btn btn-sm btn-primary font-bold" ValidationGroup="SubAdd" TextMessage="Add"
                                                            OnClick="btnSubAdd_Click"></cc1:ResourceLinkButton>
                                                    </div>
                                                    <div id="divSubUpdate" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton2" runat="server" CssClass="btn btn-sm btn-primary font-bold" ValidationGroup="SubAdd" TextMessage="Update"
                                                            OnClick="btnSubAdd_Click"></cc1:ResourceLinkButton>
                                                    </div>
                                                    <div id="divSubDelete" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton3" runat="server" CssClass="btn btn-sm btn-primary font-bold" TextMessage="Delete" OnClick="btnSubDelete_Click"></cc1:ResourceLinkButton>
                                                    </div>
                                                    <div id="divSubCancel" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton4" runat="server" CssClass="btn btn-sm btn-white font-bold" TextMessage="Cancel" CausesValidation="False"
                                                            OnClick="btnSubCancel_Click"></cc1:ResourceLinkButton>
                                                    </div>

                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label></label>

                                                </div>

                                            </asp:Panel>

                                            <asp:Panel ID="pnlSubType_Type" runat="server" Visible="false">
                                                <div class="form-group col-md-12">
                                                    <label>
                                                        <asp:Label ID="Label6" runat="server" Text="Select LookUp Sub Type Type:"></asp:Label></label>
                                                    <div class="row">
                                                        <div class="col-md-10 col-sm-11 col-xs-11">
                                                            <asp:ListBox ID="lstSubType_Type" wrap='on' SelectionMode="Single" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstSubType_Type_SelectedIndexChanged"></asp:ListBox>
                                                        </div>


                                                        <div class="col-md-2 col-xs-1 p0 m-t-md">
                                                            <asp:LinkButton CssClass="btn btn-xs btn-white btn-bitbucket" ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                                                                <i class="fa fa-arrow-up"></i>
                                                            </asp:LinkButton>
                                                            <br />
                                                            <asp:LinkButton CssClass="btn btn-xs btn-white btn-bitbucket" ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">
                                                                <i class="fa fa-arrow-down"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="form-group col-md-10 col-sm-11">
                                                    <label>
                                                        <asp:Label ID="Label7" runat="server" Text="Name:"></asp:Label>
                                                        <asp:TextBox ID="txtSubType_Type" CssClass="form-control" runat="server" MaxLength="59"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ErrorMessage="Please enter Name"
                                                            CssClass="custom-error" ControlToValidate="txtSubType" ValidationGroup="SubType_Type"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10 col-sm-11">
                                                    <lable><asp:Label ID="Label8" runat="server" Text="Description:"></asp:Label></lable>
                                                    <asp:TextBox ID="txtSubType_TypeDes" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-10 col-sm-11">
                                                    <div id="divAddSubType_Type" runat="server" style="float: left; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton5" runat="server" CssClass="btn btn-sm btn-primary font-bold" ValidationGroup="SubType_Type" TextMessage="Add"
                                                            OnClick="btnSubTypeAdd_Click"></cc1:ResourceLinkButton>
                                                    </div>
                                                    <div id="divUpdateSubType_Type" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton6" runat="server" CssClass="btn btn-sm btn-primary font-bold" ValidationGroup="SubType_Type" TextMessage="Update"
                                                            OnClick="btnSubTypeAdd_Click"></cc1:ResourceLinkButton>
                                                    </div>

                                                    <div id="divDeleteSubType_Type" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton7" runat="server" CssClass="btn btn-sm btn-primary font-bold" TextMessage="Delete" OnClick="btnSubTypeDelete_Click"></cc1:ResourceLinkButton>
                                                    </div>

                                                    <div id="divCancelSubType_Type" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton8" runat="server" CssClass="btn btn-sm btn-white font-bold" TextMessage="Cancel" CausesValidation="False"
                                                            OnClick="btnSubTypeCancel_Click"></cc1:ResourceLinkButton>
                                                    </div>
                                                </div>

                                            </asp:Panel>

                                             <asp:Panel ID="pnlSubTypeSubType_Type" runat="server" Visible="false">
                                                <div class="form-group col-md-12">
                                                    <label>
                                                        <asp:Label ID="Label9" runat="server" Text="Select LookUp Sub Type Sub Type:"></asp:Label></label>
                                                    <div class="row">
                                                        <div class="col-md-10 col-sm-11 col-xs-11">
                                                            <asp:ListBox ID="lstSubTypeSubType_Type" wrap='on' SelectionMode="Single" class="form-control" runat="server"></asp:ListBox>
                                                        </div>


                                                        <div class="col-md-2 col-xs-1 p0 m-t-md">
                                                            <asp:LinkButton CssClass="btn btn-xs btn-white btn-bitbucket" ID="lnSubTypeSubType" runat="server" OnClick="lnSubTypeSubType_Click">
                                                                <i class="fa fa-arrow-up"></i>
                                                            </asp:LinkButton>
                                                            <br />
                                                            <asp:LinkButton CssClass="btn btn-xs btn-white btn-bitbucket" ID="lnSubTypeSubType2" runat="server" OnClick="lnSubTypeSubType2_Click">
                                                                <i class="fa fa-arrow-down"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="form-group col-md-10 col-sm-11">
                                                    <label>
                                                        <asp:Label ID="Label10" runat="server" Text="Name:"></asp:Label>
                                                        <asp:TextBox ID="txtSubTypeSubType_Type" CssClass="form-control" runat="server" MaxLength="59"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*" ErrorMessage="Please enter Name"
                                                            CssClass="custom-error" ControlToValidate="txtSubTypeSubType_Type" ValidationGroup="SubTypeSubType_Type"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10 col-sm-11">
                                                    <lable><asp:Label ID="Label11" runat="server" Text="Description:"></asp:Label></lable>
                                                    <asp:TextBox ID="txtSubTypeSubType_TypeDes" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-10 col-sm-11">
                                                    <div id="divAddSubTypeSubType" runat="server" style="float: left; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton9" runat="server" CssClass="btn btn-sm btn-primary font-bold" ValidationGroup="SubTypeSubType_Type" TextMessage="Add"
                                                            OnClick="btnSubTypeSubTypeAdd_Click"></cc1:ResourceLinkButton>
                                                    </div>
                                                    <div id="divUpdateSubTypeSubType" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton10" runat="server" CssClass="btn btn-sm btn-primary font-bold" ValidationGroup="SubTypeSubType_Type" TextMessage="Update"
                                                            OnClick="btnSubTypeSubTypeAdd_Click"></cc1:ResourceLinkButton>
                                                    </div>

                                                    <div id="divDeleteSubTypeSubType" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton11" runat="server" CssClass="btn btn-sm btn-primary font-bold" TextMessage="Delete" OnClick="btnSubTypeSubTypeDelete_Click"></cc1:ResourceLinkButton>
                                                    </div>

                                                    <div id="divCancelSubTypeSubType" runat="server" style="float: left; display: none; margin-right: 5px;">
                                                        <cc1:ResourceLinkButton ID="ResourceLinkButton12" runat="server" CssClass="btn btn-sm btn-white font-bold" TextMessage="Cancel" CausesValidation="False"
                                                            OnClick="btnSubTypeSubTypeCancel_Click"></cc1:ResourceLinkButton>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- grid but hidden-->

                <div id="divOrderList" runat="server" style="display: none;">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView DataKeyNames="intSectionTypeId" ID="gvData" CssClass="MyGridView" runat="server"
                                    AutoGenerateColumns="False" Width="100%" RowStyle-CssClass="rowOdd" AlternatingRowStyle-CssClass="rowEven"
                                    HeaderStyle-CssClass="grid-heading" GridLines="None">
                                    <RowStyle CssClass="rowOdd" />
                                    <EmptyDataTemplate>
                                        <span><%= ResourceMgr.GetMessage(" No record(s) found.")%><   </span>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="vchSectionType" HeaderText="">
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOrder" runat="server" ValidationGroup="txt" Width="30px" Text='<%#DataBinder.Eval(Container.DataItem, "intOrderId") %>'
                                                    MaxLength="3" OnTextChanged="txtOrder_TextChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="grid-heading" />
                                    <AlternatingRowStyle CssClass="rowEven" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>















</asp:Content>

