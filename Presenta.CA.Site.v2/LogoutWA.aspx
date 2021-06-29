<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogoutWA.aspx.cs" Inherits="Presenta.CA.Site.LogoutWA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <link href="Icons/favicon.ico" rel="shortcut icon" type="text/css" />
    <title></title>
    <link href="Styles/Login.css" rel="stylesheet" type="text/css" />
    <script src='<%= ResolveClientUrl("~/Scripts/jquery-1.7.2.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Scripts/common.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Scripts/jquery.tipsy.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            exibirNotificacoes();
            configTitle();
        });
        function configTitle() {
            $('html head title').text(GetElemByClientID("hdfMajorMinorVersion").val());
        }
        function resizeWindow() {
            var windowHeight = getWindowHeight();
            var hDadosLogin = document.getElementById('dadosLogin').offsetHeight;
            var h = (windowHeight / 2) - (hDadosLogin / 2);
            if (h > 0) {
                document.getElementById('headerLogin').style.height =
                    (document.body.offsetHeight / 2) - (hDadosLogin / 2) + 100 + 'px';
            }
        }

        function getWindowHeight() {
            var windowHeight = 0;
            if (typeof (window.innerHeight) == 'number') {
                windowHeight = window.innerHeight;
            }
            else {
                if (document.documentElement && document.documentElement.clientHeight) {
                    windowHeight = document.documentElement.clientHeight;
                }
                else {
                    if (document.body && document.body.clientHeight) {
                        windowHeight = document.body.clientHeight;
                    }
                }
            }
            return windowHeight;
        }
    </script>
</head>
<body onresize="resizeWindow()" onload="resizeWindow()">
    <form id="form1" runat="server">
    <div id="hf">
        <asp:HiddenField ID="hdfError" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText1" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText2" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfMajorMinorVersion" runat="server" EnableViewState="true" />
    </div>
    <div id="headerLogin"></div>
    <div id="dadosLogin" align="center" class="dadosLogin">
        <h3 class="sessao-expirada">A sessão expirou.  Você foi deslogado por questões de segurança.</h3>
        <br />
        <h3 class="sessao-expirada">Para logar novamente, clique no botão abaixo:</h3>
        <asp:Button ID="btnEntrar" Text="Entrar" runat="server" OnClick="btnEntrar_Click" CssClass="loginButton" />
    </div>
    <div></div>
    <div class="copyright footer">&copy;&nbsp;2014&nbsp;-&nbsp;Presenta&nbsp;Sistemas&nbsp;Ltda.&nbsp;-&nbsp;Todos&nbsp;os&nbsp;Direitos&nbsp;Reservados.</div>
    </form>
</body>
</html>
