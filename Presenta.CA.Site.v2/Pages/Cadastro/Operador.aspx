<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Operador.aspx.cs" Inherits="Presenta.CA.Site.Pages.Cadastro.Operador" %>

<%@ Register Src="~/UserControls/OperadorUC.ascx" TagName="OperadorjqGrid" TagPrefix="oprGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            configPage();
            exibirNotificacoes();
            configPagePermissions();
        });
        function configPagePermissions() {
            if (GetElemByClientID("hdfReadOnly").val() == '1') {
                setTimeout('$("#OperadoresGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();', 100);
                setTimeout('jqGridHideReset();', 150);
            }
        }
        function jqGridHideReset() {
            $('span[class="ui-icon ui-icon-arrowrefresh-1-e"]').hide();
        }
        function configPage() {
            setGrandMenuId(1);
            setMenuHover(2);
            setPageName('Cadastro - Operador');
        }

        function executarReset() {
            var grid = $("#OperadoresGrid");
            if (grid.jqGrid('getGridParam', 'selrow') !== null) {
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                idOperador = grid.jqGrid('getCell', selRowId, 'IdOperador');

                if (confirm("Deseja realmente resetar a senha do operador?")) {
                    $.blockUI();
                    PageMethods.ResetarSenha(
                        idOperador,
                        onSuccessResetarSenha,
                        onErrorResetarSenha);
                }
            }
            else {
                alert('Selecione um registro antes de prosseguir!');
                return false;
            }
        }

        function onSuccessResetarSenha(response) {
            $.unblockUI();
            GetElemByClientID('hdfInfo').val('1');
            GetElemByClientID('hdfText').val('Senha Resetada com sucesso');
            exibirNotificacoes();
            $("#OperadoresGrid").clearGridData(true).trigger("reloadGrid");
        }

        function onErrorResetarSenha(err) {
            $.unblockUI();
            var error_msg = err.get_message();
            GetElemByClientID('hdfError').val('1');
            GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o Reset da Senha');
            GetElemByClientID('hdfText2').val(error_msg);
            exibirNotificacoes();
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="hf">
        <asp:HiddenField ID="hdfInfo" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfUpdate" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfInsert" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfDelete" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfError" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText1" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText2" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfReadOnly" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfApenasAtivos" runat="server" EnableViewState="true" Value="0" />
    </div>
    <div>
        <div style="padding: 5px;">
            <oprGrid:OperadorjqGrid ID="perfilGrid" runat="server" />
        </div>
    </div>
</asp:Content>
