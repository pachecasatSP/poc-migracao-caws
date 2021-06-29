<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presenta.CA.Site.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Styles/Login.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            window.location.href = "<%= ResolveClientUrl("~/Pages/Associacao/Operador.aspx") %>";
            exibirNotificacoes();
        });
    </script>
    <div id="initialMessage" style="font-size:large; padding:30px;">
    </div>
    <div id="redirectMessage" style="font-size:medium; padding:30px;">
    </div>
    <div id="hf">
        <asp:HiddenField ID="hdfError" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText1" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText2" runat="server" EnableViewState="true" />
    </div>
</asp:Content>
