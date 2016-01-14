<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceAggregate.aspx.cs" Inherits="InvoiceAggregate" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:75%;height:100%;  margin-left: 130px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="rptVwrInvoiceReport" runat="server" style="width:100%;height:100%;" AsyncRendering="False" SizeToReportContent="True"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
