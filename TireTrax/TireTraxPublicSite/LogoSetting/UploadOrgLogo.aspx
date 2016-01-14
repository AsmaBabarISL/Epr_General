<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true"
    CodeFile="UploadOrgLogo.aspx.cs" Inherits="Settings_LogoSetting_UploadOrgLogo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Scripts/themes/ui-lightness/jquery.ui.all.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        $(function () {
            $(".ImgClick").click(function () {
                showImage($(this).attr('src'), "200px", "200px")
            });
        });

        function showImage(path, imgWidth, imgHeight) {
            var vPageSize = fnGetPageSize();

            document.getElementById('idScreen').style.display = "block";
            document.getElementById('idScreen').style.width = vPageSize[0];
            document.getElementById('idScreen').style.height = vPageSize[1];

            var vImagePath = "<table cellspacing=\"1\" cellpadding=\"2\">";
            vImagePath += "<tr><td><span style=\"cursor:pointer;\"><img src=\"" + path + "\" height=\"350\" weight=\"350\" ></span></td></tr>";
            vImagePath += "<tr><td align=\"right\">";
            //vImagePath += "<input  type=\"button\" onclick=\"javascript:hideImage();\" value=\"Close\" class=\"btn\" />";

            vImagePath += "<INPUT TYPE=\"image\" SRC=\"/images/crossmark.gif\" width=\"25\"   BORDER=\"0\" onclick=\"javascript:hideImage();\"/>";
            vImagePath += "</td></tr></table>";


            var vleftDiv, vtopDiv;
            var yScroll, xScroll;
            if (window.innerWidth) {
                xScroll = window.pageXOffset + 400;
                yScroll = window.pageYOffset + 250;

            } else if (document.body) { // all but Explorer Mac
                xScroll = document.body.parentElement.scrollLeft + 400;
                yScroll = document.body.parentElement.scrollTop + 250;

            } else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
                xScroll = document.body.offsetWidth;
                yScroll = document.body.offsetHeight;

            }

            vtopDiv = yScroll;
            vleftDiv = xScroll
            document.getElementById('spanImgId').innerHTML = vImagePath;
            document.getElementById('divImage').style.display = "block";
            document.getElementById('divImage').style.top = vtopDiv + "px";
            document.getElementById('divImage').style.left = vleftDiv + "px";
            document.getElementById('divImage').style.position = "absolute";
        }

        function hideImage() {
            document.getElementById('spanImgId').innerHTML = "";
            document.getElementById('divImage').style.display = "none";
            document.getElementById('idScreen').style.display = "none";
        }

        function fnGetPageSize() {
            var xScroll, yScroll;

            if (window.innerHeight && window.scrollMaxY) {
                xScroll = window.innerWidth + window.scrollMaxX;
                yScroll = window.innerHeight + window.scrollMaxY;
            } else if (document.body.scrollHeight > document.body.offsetHeight) { // all but Explorer Mac
                xScroll = document.body.scrollWidth;
                yScroll = document.body.scrollHeight;
            } else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
                xScroll = document.body.offsetWidth;
                yScroll = document.body.offsetHeight;
            }

            var windowWidth, windowHeight;

            if (self.innerHeight) { // all except Explorer
                if (document.documentElement.clientWidth) {
                    windowWidth = document.documentElement.clientWidth;
                }
                else {
                    windowWidth = self.innerWidth;
                }

                windowHeight = self.innerHeight;
            }
            else if (document.documentElement && document.documentElement.clientHeight) { // Explorer 6 Strict Mode
                windowWidth = document.documentElement.clientWidth;
                windowHeight = document.documentElement.clientHeight;
            }
            else if (document.body) { // other Explorers
                windowWidth = document.body.clientWidth;
                windowHeight = document.body.clientHeight;
            }


            if (yScroll < windowHeight) {
                pageHeight = windowHeight;
            } else {
                pageHeight = yScroll;
            }


            if (xScroll < windowWidth) {
                pageWidth = xScroll;
            } else {
                pageWidth = windowWidth;
            }

            return [pageWidth, pageHeight];
        }


    </script>


    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= ResourceMgr.GetMessage("Logo Settings")%> </h5>
                </div>
                <div class="ibox-content" style="display: block;">
                    <div role="form" class="search-filter" id="">
                        <div class="row">
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Logo Image Name")%></label>
                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator18" runat="server"
                                    ControlToValidate="txtFileName" InitialValueErrorText="" ValidationGroup="upload"
                                    ErrorText="*" Display="Dynamic" ForeColor="red">
                                </cc1:ResourceRequiredFieldValidator>
                                <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Upload Logo Image")%></label>
                                <asp:FileUpload ID="fUpload" runat="server" CssClass="form-control" />
                                <asp:Label ID="lblError" CssClass="custom-error" runat="server" Text=""></asp:Label>

                            </div>

                            <div class="col-md-12 mb0">
                                <cc1:ResourceLinkButton ID="btnInventorySearch" runat="server" ValidationGroup="upload"
                                    CssClass="btn btn-sm btn-primary font-bold" OnClick="upload_Click"> <%= ResourceMgr.GetMessage("Upload")%></cc1:ResourceLinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




    <div class="div_Transparent" id="idScreen" style="display: none;">&nbsp;</div>
    <div id="divImage">
        <span id="spanImgId"></span>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= ResourceMgr.GetMessage("Logo Settings")%> </h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <!-- grid -->
                                <asp:GridView ID="gvImage" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data was found"
                                    EmptyDataRowStyle-CssClass="alert alert-danger text-center" DataKeyNames="OrganizationId" OnRowDeleted="gvImage_RowDeleted" OnRowDeleting="gvImage_RowDeleting"
                                    OnRowDataBound="gvImage_RowDataBound" OnRowCommand="gvImage_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%= ResourceMgr.GetMessage("Organization Name")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("LegalName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%= ResourceMgr.GetMessage("Logo Name")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("vchName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Image")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <%-- <%# Eval("vchImagePath")%>--%>
                                                <asp:Image ID="image1" runat="server" Width="50px" ImageUrl='<%# String.Format("~/uploads/logo/stewardship/" + Eval("vchImagePath")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="60">

                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Status")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgStatus" Text="" runat="server" CommandName="Status" CommandArgument='<%#Eval("intImageId") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="40" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Delete")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgDelete" ToolTip="Deactivate" runat="server" CommandName="Delete" CommandArgument='<%#Eval("intImageId") %>' CssClass="btn btn-white btn-bitbucket" OnClientClick="javascript:return confirm('Are you sure to disable this image?');"> <i class="fa fa-trash"></i> </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
