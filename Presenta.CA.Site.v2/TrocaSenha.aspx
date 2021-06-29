<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrocaSenha.aspx.cs" Inherits="Presenta.CA.Site.TrocaSenha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <link href="Icons/favicon.ico" rel="shortcut icon" type="text/css" />
    <link href="../Styles/ui-lightness/jquery-ui-1.9.2.custom.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script src='<%= ResolveClientUrl("~/Scripts/jquery-1.7.2.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Scripts/jquery-ui-1.8.23.custom.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Scripts/common.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Scripts/jquery.tipsy.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            configDialogTrocaSenha();
            exibirNotificacoes();
            configTitle();
        });
        function configDialogTrocaSenha() {
            $("#dialog-message").dialog({
                modal: true,
                autoOpen: false,
                height: 300,
                width: 600,
                show: {
                    modal: true,
                    effect: "fade",
                    duration: 250
                },
                hide: {
                    effect: "fade",
                    duration: 250
                },
                buttons: {
                    OK: function () {
                        $(this).dialog("close");
                    },
                    Expandir: function () {
                        $(this).height(getWindowHeight() - 297);
                        $(this).dialog("widget").height(getWindowHeight() - 200);
                        $(this).dialog("widget").width(getWindowWidth() - 200);
                        $(this).dialog("widget").position({ my: 'center', at: 'center', of: window });
                    },
                    Contrair: function () {
                        $(this).height(203);
                        $(this).dialog("widget").height(300);
                        $(this).dialog("widget").width(600);
                        $(this).dialog("widget").position({ my: 'center', at: 'center', of: window });
                    }
                }
            }).parent().addClass("ui-state-error");
        }
        function configTitle() {
            $('html head title').text(GetElemByClientID("hdfMajorMinorVersion").val());
        }
        function btnAlterarClick() {
            var $current = GetElemByClientID('txtCurrentPassword');
            if ($current.val() == '') {
                $current.tipsy({ fade: true, gravity: 'w' });
                $current.css('border-color', 'red').focus().tipsy('show');
                return false;
            } else {
                $current.css('border-color', '').tipsy('hide');
            }
            var $new = GetElemByClientID('txtNewPassword');
            if ($new.val() == '') {
                $new.tipsy({ fade: true, gravity: 'w' });
                $new.css('border-color', 'red').focus().tipsy('show');
                return false;
            } else {
                $new.css('border-color', '').tipsy('hide');
            }
            var $conf = GetElemByClientID('txtConfirmNewPassword');
            if ($conf.val() == '' || $new.val() != $conf.val()) {
                $conf.tipsy({ fade: true, gravity: 'w' });
                $conf.css('border-color', 'red').focus().tipsy('show');
                return false;
            } else {
                $conf.css('border-color', '').tipsy('hide');
            }
            return true;
        }
        function resizeWindow() {
            var windowHeight = getWindowHeight();
            var hDadosLogin = document.getElementById('dadosLogin').offsetHeight;
            var h = (windowHeight / 2) - (hDadosLogin / 2);
            if (h > 0) {
                document.getElementById('headerLogin').style.height = '426px';
                    //(document.body.offsetHeight / 2) - (hDadosLogin / 2) + 35 + 'px';
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
        var keysIlc = {};
        $(document).keydown(function (e) { keysIlc[e.which] = true; printKeys(); });
        $(document).keyup(function (e) { delete keysIlc[e.which]; printKeys(); });
        function printKeys() {
            var alt = false;
            var shift = false;
            var i = false;
            var e = false;
            var w = false;
            for (var k in keysIlc) {
                if (!keysIlc.hasOwnProperty(k)) continue;
                if (k == 18) { alt = true; }
                if (k == 16) { shift = true; }
                if (k == 73) { i = true; }
                if (k == 69) { e = true; }
                if (k == 87) { w = true; }
            }
            if (alt && shift && w) {
                $("#dialog-message").dialog("open");
                for (var k in keysIlc) {
                    delete keysIlc[k];
                }
            }
        }
    </script>
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            if (document.getElementById('txtCurrentPassword').value == "") {
                document.getElementById('txtCurrentPassword').focus();
            }
            else {
                document.getElementById('txtNewPassword').focus();
            }
        }, false);
    </script>
    <link href="Styles/Login.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tipsy.css" rel="stylesheet" type="text/css" />
</head>
<body onresize="resizeWindow()" onload="resizeWindow()">
    <form id="form1" runat="server">
        <div id="dialog-message" class="ui-widget ui-state-error" title="Ocorreu um erro durante o processamento da requisição" style="display:none;">
            <p class="ui-state-error ui-state-error-text">
                <span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 50px 0;"></span>
                <span id="error-text-1">Não há erros.</span>
            </p>
            <p class="ui-state-error ui-state-error-text" id="error-text-2">
                Sem detalhes.
            </p>
        </div>
        <div id="hf">
            <asp:HiddenField ID="hdfInfo" runat="server" EnableViewState="true" />
            <asp:HiddenField ID="hdfText" runat="server" EnableViewState="true" />
            <asp:HiddenField ID="hdfError" runat="server" EnableViewState="true" />
            <asp:HiddenField ID="hdfText1" runat="server" EnableViewState="true" />
            <asp:HiddenField ID="hdfText2" runat="server" EnableViewState="true" />
            <asp:HiddenField ID="hdfMajorMinorVersion" runat="server" EnableViewState="true" />
        </div>
        <div id="headerLogin"></div>
        <div align="center" class="dadosLogin" id="dadosLogin">
            <table class="loginGeral" id="cpwWebjud" style="border-collapse: collapse;" border="0" cellspacing="0" cellpadding="1" orientation="Vertical" textlayout="TextOnTop">
                <tbody>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td align="center" class="loginTitle" colspan="2">Altere Sua Senha</td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="loginText">
                                            <label for="cpwWebjud_ChangePasswordContainerID_CurrentPassword">Senha:&nbsp;</label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtCurrentPassword" CssClass="changePasswordTextBox" TextMode="Password" title="A senha é obrigatória." />
                                            <span title="A senha é obrigatória." id="cpwWebjud_ChangePasswordContainerID_CurrentPasswordRequired" style="color: red; visibility: hidden;">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="loginText">
                                            <label for="cpwWebjud_ChangePasswordContainerID_NewPassword">Nova Senha:&nbsp;</label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtNewPassword" CssClass="changePasswordTextBox" TextMode="Password" title="A Nova Senha é obrigatória." />
                                            <span title="A Nova Senha é obrigatória." id="cpwWebjud_ChangePasswordContainerID_NewPasswordRequired" style="color: red; visibility: hidden;">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="loginText">
                                            <label for="cpwWebjud_ChangePasswordContainerID_ConfirmNewPassword">Confirme a Nova Senha:&nbsp;</label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtConfirmNewPassword" CssClass="changePasswordTextBox" TextMode="Password" title="A confirmação deve ser igual à Nova Senha informada!" />
                                            <span title="A confirmação da Nova Senha é obrigatória" id="cpwWebjud_ChangePasswordContainerID_ConfirmNewPasswordRequired" style="color: red; visibility: hidden;">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2"><span id="cpwWebjud_ChangePasswordContainerID_NewPasswordCompare" style="color: red; display: none;">Senha incorreta ou Nova Senha inválida.
                                            <br>
                                            Tamanho mínimo da Nova Senha: {0}.
                                            <br>
                                            Caracteres não-alfanuméricos obrigatórios: {1}.</span></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button Text="Alterar Senha" runat="server" CssClass="changePasswordButton" ID="btnAlterar" OnClick="btnAlterar_Click" OnClientClick="javascript:return btnAlterarClick();" />
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="changePasswordButton" OnClick="btnCancelar_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
