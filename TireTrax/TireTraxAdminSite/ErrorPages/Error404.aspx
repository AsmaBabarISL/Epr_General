<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error404.aspx.cs" Inherits="ErrorPages_Error404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Error 404</title>

     <link href="/css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="/font-awesome/css/font-awesome.css" rel="stylesheet" media="screen" />

    <link href="/css/style.css" rel="stylesheet" media="screen" />
    <link href="/css/eprAdmin-style.css" rel="stylesheet" media="screen" />

</head>
<body class="gray-bg">
    <form id="form1" runat="server">
        <div class="middle-box text-center animated fadeInDown">
            <h1>404</h1>
            <h3 class="font-bold">Page Not Found</h3>

            <div class="error-desc">
                <p>Sorry, but the page you are looking for might have been removed, had its name changed, or is temporarily unavailable. </p>
                <div class="m-t">
                    <a class="btn btn-primary block full-width m-b" href="/Dashboard/adminDashboard.aspx">Go back to Home Page</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
