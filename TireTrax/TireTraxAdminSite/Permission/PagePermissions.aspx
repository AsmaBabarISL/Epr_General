<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="PagePermissions.aspx.cs" Inherits="Permission_PagePermissions" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Permissions.ascx" TagPrefix="pre" TagName="resourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%----%>
    <script type="text/javascript">

        function PermissionOnTop(sLabel, controlID) {
            var control = document.getElementById(controlID);
            if (!control.checked) {
                control.checked = true;
                if (sLabel == "Admin") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdAdminBottom').disabled = false;
                } else if (sLabel == "Home") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdHomeBottom').disabled = false;
                }
                else if (sLabel == "Inventory") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdInventoryBottom').disabled = false;
                }
                else if (sLabel == "StakeHolder") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdStakeholderBottom').disabled = false;
                }
                else if (sLabel == "Revenue") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdRevenueBottom').disabled = false;
                }
                else if (sLabel == "Applations") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdApplationsBottom').disabled = false;
                }
                else if (sLabel == "Reports") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdReportsBottom').disabled = false;
                }
                else if (sLabel == "Users") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdUsersBottom').disabled = false;
                }
                else if (sLabel == "PTE") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdPTEBottom').disabled = false;
                }
                else if (sLabel == "Settings") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdSettingsBottom').disabled = false;
                }
            }
        }
        function PermissionOnBottom(controlID) {
            var control = document.getElementById(controlID);
            if (!control.checked) {
                control.checked = true;
            }
        }

        function PermissionOff(addControl, updateControl, deleteControl, viewControl) {
            var viewCont = document.getElementById(viewControl);
            if (!viewCont.checked) {
                var addCont = document.getElementById(addControl);
                addCont.checked = false;
                var updateCont = document.getElementById(updateControl);
                updateCont.checked = false;
                var deleteCont = document.getElementById(deleteControl);
                deleteCont.checked = false;
            }

        }

        function PermissionDivOff(sLabel, addControl, updateControl, deleteControl, viewControl) {
            var viewCont = document.getElementById(viewControl);
            if (!viewCont.checked) {

                var addCont = document.getElementById(addControl);
                addCont.checked = false;
                var updateCont = document.getElementById(updateControl);
                updateCont.checked = false;
                var deleteCont = document.getElementById(deleteControl);
                deleteCont.checked = false;

                if (sLabel == "Admin") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdAdminBottom').disabled = true;
                } else if (sLabel == "Home") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdHomeBottom').disabled = true;
                }
                else if (sLabel == "Inventory") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdInventoryBottom').disabled = true;
                }
                else if (sLabel == "StakeHolder") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdStakeholderBottom').disabled = true;
                }
                else if (sLabel == "Revenue") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdRevenueBottom').disabled = true;
                }
                else if (sLabel == "Applations") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdApplationsBottom').disabled = true;
                }
                else if (sLabel == "Reports") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdReportsBottom').disabled = true;
                }
                else if (sLabel == "Users") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdUsersBottom').disabled = true;
                }
                else if (sLabel == "PTE") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdPTEBottom').disabled = true;
                }
                else if (sLabel == "Settings") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdSettingsBottom').disabled = true;
                }

            }
            else {
                if (sLabel == "Admin") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdAdminBottom').disabled = false;
                } else if (sLabel == "Home") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdHomeBottom').disabled = false;
                }
                else if (sLabel == "Inventory") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdInventoryBottom').disabled = false;
                }
                else if (sLabel == "StakeHolder") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdStakeholderBottom').disabled = false;
                }
                else if (sLabel == "Revenue") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdRevenueBottom').disabled = false;
                }
                else if (sLabel == "Applations") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdApplationsBottom').disabled = false;
                }
                else if (sLabel == "Reports") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdReportsBottom').disabled = false;
                }
                else if (sLabel == "Users") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdUsersBottom').disabled = false;
                }
                else if (sLabel == "PTE") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdPTEBottom').disabled = false;
                }
                else if (sLabel == "Settings") {
                    document.getElementById('ctl00_cntPlaceHolderMain_permission_tdSettingsBottom').disabled = false;
                }
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Page Permissions")%></h5>
                            <div class="ibox-tools">
                                <div class="form-group form-horizontal">
                                    <strong class="m-r-xs">Group Name:</strong>
                                    <asp:DropDownList ID="ddlGroupName" runat="server" AutoPostBack="True" CausesValidation="True"
                                        TabIndex="1" class="" OnSelectedIndexChanged="ddlGroupName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <cc1:ResourceRequiredFieldValidator ID="ValidatorType" runat="server" ControlToValidate="ddlGroupName"
                                        Display="Dynamic" ErrorMessage="*" ForeColor="Red" InitialValue="0" ValidationGroup="save" Style="position: relative;"></cc1:ResourceRequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <asp:Label ID="lblInfo" runat="server"></asp:Label>
                            <div class="setAsAdmin">
                                <pre:resourse ID="permission" runat="server" />
                                <div class="form-group">
                                    <label class="checkbox-inline">
                                        <asp:CheckBox ID="chkAdmin" runat="server" AutoPostBack="True" OnCheckedChanged="chkAdmin_CheckedChanged"
                                            Text="Set As Admin" />
                                    </label>
                                </div>

                            </div>
                            <div class="form-group mb0">
                                <asp:Button ID="btnAddResources" runat="server" Text="Add Pages" CssClass="btn btn-sm btn-primary font-bold" OnClick="btnAddResources_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

