<%@ Page Title="" Language="C#" MasterPageFile="~/skeleton.master" AutoEventWireup="true" CodeFile="Error404.aspx.cs" Inherits="ErrorPages_Error404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="middle-box text-center animated fadeInDown">
        <h1>404</h1>
        <h3 class="font-bold">Page Not Found</h3>

        <div class="error-desc">
            <p>Sorry, but the page you are looking for might have been removed, had its name changed, or is temporarily unavailable. </p>
            <div class="m-t">
                <a class="btn btn-primary block full-width m-b" href="../Default.aspx">Go back to Home Page</a>
            </div>
        </div>
    </div>

</asp:Content>

