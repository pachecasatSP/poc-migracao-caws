<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presenta.CA.Site.Login" %>

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
                document.getElementById('headerLogin').style.height = '426px';
                    //(document.body.offsetHeight / 2) - (hDadosLogin / 2) + 100 + 'px';
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

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            if (document.getElementById('loginCA_UserName').value == "") {
                document.getElementById('loginCA_UserName').focus();
            }
            else {
                document.getElementById('loginCA_Password').focus();
            }
        }, false);
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
        <asp:Login 
            ID="loginCA" runat="server" TextLayout="TextOnTop" DestinationPageUrl="~/Pages/Associacao/Operador.aspx"
            CssClass="loginGeral" LoginButtonText="Entrar" TextBoxStyle-CssClass="loginTextBox"
            TitleText="" UserNameLabelText="Usuário:"
            PasswordLabelText="Senha:" LoginButtonStyle-CssClass="loginButton" LabelStyle-CssClass="loginText" 
            InstructionTextStyle-CssClass="loginText" 
            TitleTextStyle-CssClass="loginTitle" Orientation="Vertical" 
            onauthenticate="loginCA_Authenticate" VisibleWhenLoggedIn="true" 
            onloggedin="loginCA_LoggedIn" onloggingin="loginCA_LoggingIn" 
            onloginerror="loginCA_LoginError" FailureTextStyle-ForeColor="DarkRed" DisplayRememberMe="false" >
            <TitleTextStyle CssClass="loginTitle" />
        </asp:Login>
    </div>
    <div></div>
    <div class="copyright footer">&copy;&nbsp;2014&nbsp;-&nbsp;Presenta&nbsp;Sistemas&nbsp;Ltda.&nbsp;-&nbsp;Todos&nbsp;os&nbsp;Direitos&nbsp;Reservados.</div>
    </form>
</body>
</html>
