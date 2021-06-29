<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Presenta.CA.Site.Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src='<%# ResolveClientUrl("~/Scripts/jquery-1.7.2.min.js") %>' type="text/javascript"></script>
    <script src='<%# ResolveClientUrl("~/Scripts/common.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            exibirNotificacoes();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="hf">
        <asp:HiddenField ID="hdfError" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText1" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText2" runat="server" EnableViewState="true" />
    </div>
    <div>
    
    </div>
    </form>
</body>
</html>
