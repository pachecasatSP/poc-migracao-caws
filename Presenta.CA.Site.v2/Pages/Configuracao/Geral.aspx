<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Geral.aspx.cs" Inherits="Presenta.CA.Site.Pages.Configuracao.Geral" %>

<%@ Register Src="~/UserControls/ActionBar.ascx" TagName="ActionBar" TagPrefix="wuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            configPage();
            setActionBar();
            setInitialStateControls();
            GetElemByClientID('lkbAlterar').click(function () {
                actionBarAlterarClickBase();
                habilitarControles();
                return false;
            });
            GetElemByClientID('lkbCancelar').click(function () {
                getHiddenFieldsValues();
                setInitialStateControls();
                return false;
            });
            GetElemByClientID('lkbSalvar').click(function () {
                var tipo = GetSelectedValue('rblTipoAutenticacao');
                var $senha1 = GetElemByClientID('txtSenhaPadrao');
                var $senha2 = GetElemByClientID('txtSenhaPadraoConfirm');
                var $senha3 = GetElemByClientID('hdfSenhaAtual');
                var $senha4 = GetElemByClientID('txtSenhaAtual');
                if (tipo == '2' && $senha1.val() == '')  {
                    $senha1.tipsy({ fade: true, gravity: 'sw' });
                    $senha1.css('border-color', 'red').focus().tipsy('show');
                    return false;
                }
                else {
                    $senha1.css('border-color', '').tipsy('hide');
                }
                if (tipo == '2' && ($senha1.val() != $senha2.val())) {
                    $senha2.tipsy({ fade: true, gravity: 'sw' });
                    $senha2.css('border-color', 'red').focus().tipsy('show');
                    return false;
                }
                else {
                    $senha2.css('border-color', '').tipsy('hide');
                }
                if ($senha1.val().length < GetElemByClientID('hdfNumeroMinimoCaracteres').val()) {
                    $senha1.attr('title', 'A senha padrão deve ter no mínimo sete caracteres.');
                    $senha1.tipsy({ fade: true, gravity: 'sw' });
                    $senha1.css('border-color', 'red').focus().tipsy('show');
                    return false;
                }
                else {
                    $senha1.css('border-color', '').tipsy('hide');
                }
                if ($senha3.val() != $senha1.val()) {
                    if (tipo == '2' && $senha4.val() == '') {
                        $senha4.attr('title', 'Informe a senha atual para alterá-la.');
                        $senha4.tipsy({ fade: true, gravity: 'sw' });
                        $senha4.css('border-color', 'red').focus().tipsy('show');
                        return false;
                    }
                    else {
                        $senha4.css('border-color', '').tipsy('hide');
                    }
                    if (tipo == '2' && $senha3.val() != $senha4.val()) {
                        $senha4.attr('title', 'Senha atual incorreta.');
                        $senha4.tipsy({ fade: true, gravity: 'sw' });
                        $senha4.css('border-color', 'red').focus().tipsy('show');
                        return false;
                    }
                    else {
                        $senha4.css('border-color', '').tipsy('hide');
                    }
                }
                var $diasTS = GetElemByClientID('txtDiasTrocaSenha');
                if (!validInteger($diasTS)) {
                    $diasTS.tipsy({ fade: true, gravity: 'sw' });
                    $diasTS.css('border-color', 'red').focus().tipsy('show');
                    return false;
                }
                else {
                    $diasTS.css('border-color', '').tipsy('hide');
                }
                var $diasTIP = GetElemByClientID('txtTentivasInvalidasPermitidas');
                if (!validInteger($diasTIP)) {
                    $diasTIP.tipsy({ fade: true, gravity: 'sw' });
                    $diasTIP.css('border-color', 'red').focus().tipsy('show');
                    return false;
                }
                else {
                    $diasTIP.css('border-color', '').tipsy('hide');
                }
                var $diasDS = GetElemByClientID('txtDiasDesativarSenha');
                if (!validInteger($diasDS)) {
                    $diasDS.tipsy({ fade: true, gravity: 'sw' });
                    $diasDS.css('border-color', 'red').focus().tipsy('show');
                    return false;
                }
                else {
                    $diasDS.css('border-color', '').tipsy('hide');
                }
                var $minC = GetElemByClientID('txtNumeroMinimoCaracteres');
                if ($minC.val() != '') {
                    if (!validInteger($minC)) {
                        $minC.tipsy({ fade: true, gravity: 'sw' });
                        $minC.css('border-color', 'red').focus().tipsy('show');
                        return false;
                    }
                    else {
                        $minC.css('border-color', '').tipsy('hide');
                    }
                }
                else {
                    $minC.css('border-color', '').tipsy('hide');
                }
                GetElemByClientID('hdfTipoAutenticacao').val(tipo);
                GetElemByClientID('hdfSenhaPadrao').val(GetElemByClientID('txtSenhaPadrao').val());
                GetElemByClientID('hdfSenhaPadraoConfirm').val(GetElemByClientID('txtSenhaPadraoConfirm').val());
                GetElemByClientID('hdfDiasTrocaSenha').val(GetElemByClientID('txtDiasTrocaSenha').val());
                GetElemByClientID('hdfDiasDesativarSenha').val(GetElemByClientID('txtDiasDesativarSenha').val());
                GetElemByClientID('hdfTentivasInvalidasPermitidas').val(GetElemByClientID('txtTentivasInvalidasPermitidas').val());
                GetElemByClientID('hdfNumeroMinimoCaracteres').val(GetElemByClientID('txtNumeroMinimoCaracteres').val());
                GetElemByClientID('hdfPossuirLetrasMaiusculas').val(GetElemByClientID('chkPossuirLetrasMaiusculas')[0].checked ? '1' : '0');
                GetElemByClientID('hdfPossuirLetrasMinusculas').val(GetElemByClientID('chkPossuirLetrasMinusculas')[0].checked ? '1' : '0');
                GetElemByClientID('hdfPossuirAlgarismosArabicos').val(GetElemByClientID('chkPossuirAlgarismosArabicos')[0].checked ? '1' : '0');
                GetElemByClientID('hdfPossuirCaracteresEspeciais').val(GetElemByClientID('chkPossuirCaracteresEspeciais')[0].checked ? '1' : '0');
                GetElemByClientID('hdfDesabilitarCaracteresIdenticos').val(GetElemByClientID('chkDesabilitarCaracteresIdenticos')[0].checked ? '1' : '0');
                return true;
            });
            exibirNotificacoes();
            setTabelaSemConfiguracao();
            configPagePermissions();
        });
        function disableReadOnly() {
            desabilitarControles();
            $(this).attr('disabled', 'disabled');
            var $imgA = GetElemByClientID('imgAlterar');
            $imgA.attr('disabled', 'disabled');
            if ($imgA.attr('src').indexOf('24-pb') === -1) {
                $imgA.attr('src', $imgA.attr('src').replace('24', '24-pb'));
            }
            var $lblA = GetElemByClientID('lblAlterar');
            $lblA.attr('disabled', 'disabled');
            $lblA.css('color', 'gray');
        }
        function configPagePermissions() {
            if (GetElemByClientID("hdfReadOnly").val() == '1') {
                setTimeout('disableReadOnly();', 100);
            }
        }
        function setTabelaSemConfiguracao() {
            if (GetElemByClientID('hdfSemConfig').val() == '1') {
                actionBarAlterarClickBaseAlterar();
            }
        }
        function configPage() {
            setGrandMenuId(2);
            setMenuHover(0);
            setPageName('Configuração - Geral');
        }
        function setInitialStateControls() {
            setInitialStateControlsBase();
            desabilitarControles();
        }
        function habilitarControles() {
            GetElemByClientID('rblTipoAutenticacao').Enable();
            GetElemByClientID('txtSenhaAtual').Enable();
            GetElemByClientID('txtSenhaPadrao').Enable();
            GetElemByClientID('txtSenhaPadraoConfirm').Enable();
            GetElemByClientID('txtDiasTrocaSenha').Enable();
            GetElemByClientID('txtDiasDesativarSenha').Enable();
            GetElemByClientID('txtTentivasInvalidasPermitidas').Enable();
            GetElemByClientID('txtNumeroMinimoCaracteres').Enable();
            GetElemByClientID('chkPossuirLetrasMaiusculas').Enable();
            GetElemByClientID('chkPossuirLetrasMinusculas').Enable();
            GetElemByClientID('chkPossuirAlgarismosArabicos').Enable();
            GetElemByClientID('chkPossuirCaracteresEspeciais').Enable();
            GetElemByClientID('chkDesabilitarCaracteresIdenticos').Enable();

            GetElemByClientID('txtDiasTrocaSenha').focus();
        }
        function desabilitarControles() {
            GetElemByClientID('rblTipoAutenticacao').Disable();
            GetElemByClientID('txtSenhaAtual').Disable();
            GetElemByClientID('txtSenhaPadrao').Disable();
            GetElemByClientID('txtSenhaPadraoConfirm').Disable();
            GetElemByClientID('txtDiasTrocaSenha').Disable();
            GetElemByClientID('txtDiasDesativarSenha').Disable();
            GetElemByClientID('txtTentivasInvalidasPermitidas').Disable();
            GetElemByClientID('txtNumeroMinimoCaracteres').Disable();
            GetElemByClientID('chkPossuirLetrasMaiusculas').Disable();
            GetElemByClientID('chkPossuirLetrasMinusculas').Disable();
            GetElemByClientID('chkPossuirAlgarismosArabicos').Disable();
            GetElemByClientID('chkPossuirCaracteresEspeciais').Disable();
            GetElemByClientID('chkDesabilitarCaracteresIdenticos').Disable();
        }
        function getHiddenFieldsValues() {
            GetElemByClientID('rblTipoAutenticacao').Check(GetElemByClientID('hdfTipoAutenticacao').val() - 1);
            GetElemByClientID('txtSenhaPadrao').val(GetElemByClientID('hdfSenhaPadrao').val());
            GetElemByClientID('txtSenhaPadraoConfirm').val(GetElemByClientID('hdfSenhaPadraoConfirm').val());
            GetElemByClientID('txtDiasTrocaSenha').val(GetElemByClientID('hdfDiasTrocaSenha').val());
            GetElemByClientID('txtDiasDesativarSenha').val(GetElemByClientID('hdfDiasDesativarSenha').val());
            GetElemByClientID('txtTentivasInvalidasPermitidas').val(GetElemByClientID('hdfTentivasInvalidasPermitidas').val());
            GetElemByClientID('txtNumeroMinimoCaracteres').val(GetElemByClientID('hdfNumeroMinimoCaracteres').val());
            GetElemByClientID('chkPossuirLetrasMaiusculas').Check(GetElemByClientID('hdfPossuirLetrasMaiusculas').val() - 1);
            GetElemByClientID('chkPossuirLetrasMinusculas').Check(GetElemByClientID('hdfPossuirLetrasMinusculas').val() - 1);
            GetElemByClientID('chkPossuirAlgarismosArabicos').Check(GetElemByClientID('hdfPossuirAlgarismosArabicos').val() - 1);
            GetElemByClientID('chkPossuirCaracteresEspeciais').Check(GetElemByClientID('hdfPossuirCaracteresEspeciais').val() - 1);
            GetElemByClientID('chkDesabilitarCaracteresIdenticos').Check(GetElemByClientID('hdfDesabilitarCaracteresIdenticos').val() - 1);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="hf">
        <asp:HiddenField ID="hdfUpdate" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfInsert" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfDelete" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfError" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText1" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText2" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfSemConfig" runat="server" EnableViewState="true" />
    </div>
    <div id="hfDados">
        <asp:HiddenField ID="hdfSalvarClick" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfTipoAutenticacao" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfSenhaPadrao" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfSenhaPadraoConfirm" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfDiasTrocaSenha" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfDiasDesativarSenha" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfTentivasInvalidasPermitidas" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfSenhaAtual" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfNumeroMinimoCaracteres" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfPossuirLetrasMaiusculas" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfPossuirLetrasMinusculas" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfPossuirAlgarismosArabicos" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfPossuirCaracteresEspeciais" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfDesabilitarCaracteresIdenticos" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfReadOnly" runat="server" EnableViewState="true" />
    </div>
    <wuc:ActionBar ID="wucab" runat="server" />
    <div style="padding: 10px;">
        <div style="width:25%;float:left;padding:10px;display:none;">
            <fieldset style="width:95%;">
                <legend>Tipo de Autenticação</legend>
                <asp:RadioButtonList runat="server" ID="rblTipoAutenticacao" RepeatDirection="Vertical" Width="90%">
                    <asp:ListItem Text="Autenticação do Windows" Value="1" />
                    <asp:ListItem Text="Usuário e Senha" Value="2" />
                </asp:RadioButtonList>
            </fieldset>
        </div>
        <div style="width:60%;float:left;padding: 10px;">
            <fieldset>
                <div style="padding:5px;">
                    <div style="width: 30%; float: left;">
                        <div><span class="span_label">Senha Padrão Atual</span></div>
                        <div>
                            <asp:TextBox runat="server" ID="txtSenhaAtual" Width="90%" CssClass="text-center" TextMode="Password" title="Campo obrigatório" /></div>
                    </div>
                    <div style="width: 33%; float: left;">
                        <div><span class="span_label">Senha Padrão</span></div>
                        <div>
                            <asp:TextBox runat="server" ID="txtSenhaPadrao" Width="88%" CssClass="text-center" TextMode="Password" title="Campo obrigatório" /></div>
                    </div>
                    <div style="width: 33%; float: left;">
                        <div><span class="span_label">Redigite a Senha Padrão</span></div>
                        <div>
                            <asp:TextBox runat="server" ID="txtSenhaPadraoConfirm" Width="89%" CssClass="text-center" TextMode="Password" title="As senhas devem ser iguais" /></div>
                    </div>
                </div>
                <div class="clear"></div>
                <div style="padding:5px;">
                    <div style="width: 30%; float: left;">
                        <div><span class="span_label">Dias para Troca de Senha</span></div>
                        <div>
                            <asp:TextBox runat="server" ID="txtDiasTrocaSenha" Width="90%" CssClass="text-right" title="Informe um número válido" /></div>
                    </div>
                    <div style="width: 30%; float: left;">
                        <div><span class="span_label">Dias para Desativar a Senha</span></div>
                        <div>
                            <asp:TextBox runat="server" ID="txtDiasDesativarSenha" Width="90%" CssClass="text-right" title="Informe um número válido" /></div>
                    </div>
                    <div style="width: 38%; float: left;">
                        <div><span class="span_label">Tentativas Inválidas Permitidas</span></div>
                        <div>
                            <asp:TextBox runat="server" ID="txtTentivasInvalidasPermitidas" Width="85%" CssClass="text-right" title="Informe um número válido" /></div>
                    </div>
                </div>
            </fieldset>
        </div>
        <div style="width:60%;float:left;padding: 10px;">
            <fieldset>
                <legend>Regras de Senha</legend>
                <div style="padding:5px;">
                    <div style="width: 30%; float: left;">
                        <div><span class="span_label">Número Mínimo de Caracteres</span></div>
                        <div>
                            <asp:TextBox runat="server" ID="txtNumeroMinimoCaracteres" CssClass="text-right" /></div>
                    </div>
                </div>
                <div class="clear"></div>
                <div style="padding:5px;">
                    <div style="width: 40%; float: left;">
                        <div><span class="span_label"><asp:CheckBox Text="Possuir Letras maiúsculas de 'A' a 'Z'" runat="server" ID="chkPossuirLetrasMaiusculas" /></span></div>
                    </div>
                </div>
                <div class="clear"></div>
                <div style="padding:5px;">
                    <div style="width: 40%; float: left;">
                        <div><span class="span_label"><asp:CheckBox Text="Possuir Letras minúsculas de 'a' a 'z'" runat="server" ID="chkPossuirLetrasMinusculas" /></span></div>
                    </div>
                </div>
                <div class="clear"></div>
                <div style="padding:5px;">
                    <div style="width: 40%; float: left;">
                        <div><span class="span_label"><asp:CheckBox Text="Possuir Algarismos arábicos de '0' a '9'" runat="server" ID="chkPossuirAlgarismosArabicos" /></span></div>
                    </div>
                </div>
                <div class="clear"></div>
                <div style="padding:5px;">
                    <div style="width: 40%; float: left;">
                        <div><span class="span_label"><asp:CheckBox Text="Possuir Caracteres Especiais" runat="server" ID="chkPossuirCaracteresEspeciais" /></span></div>
                    </div>
                </div>
                <div class="clear"></div>
                <div style="padding:5px;">
                    <div style="width: 40%; float: left;">
                        <div><span class="span_label"><asp:CheckBox Text="Desabilitar Caracteres Idênticos" runat="server" ID="chkDesabilitarCaracteresIdenticos" /></span></div>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
