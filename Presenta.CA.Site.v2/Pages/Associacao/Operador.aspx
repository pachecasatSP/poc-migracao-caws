<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Operador.aspx.cs" Inherits="Presenta.CA.Site.Pages.Associacao.Operador" %>

<%@ Register Src="~/UserControls/PerfilUC.ascx" TagName="PerfiljqGrid" TagPrefix="pflGrid" %>
<%@ Register Src="~/UserControls/OperadorUCL.ascx" TagName="OperadorjqGridL" TagPrefix="oprGridL" %>
<%@ Register Src="~/UserControls/OperadorUCR.ascx" TagName="OperadorjqGridR" TagPrefix="oprGridR" %>
<%@ Register Src="~/UserControls/OperadorUC.ascx" TagName="OperadorjqGrid" TagPrefix="oprGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            configPage();
            configButtons();
            exibirNotificacoes();
            setTimeout('configGridButtons();', 100);
        });
        function disableBtnsAssociacao() {
            $('#btn-lr').button().button("disable");
            $('#btn-rl').button().button("disable");
            $('#btn-incluir').button().button("disable");
        }
        function configPagePermissions() {
            if (GetElemByClientID("hdfReadOnly").val() == '1') {
                setTimeout('disableBtnsAssociacao();', 100);
            }
        }
        function configPage() {
            setGrandMenuId(0);
            setMenuHover(0);
            setPageName('Associação - Perfil x Operador');
        }
        function configGridButtons() {
            $("#PerfisGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
            configPagePermissions();
        }
        function configButtons() {
            configBtnLR();
            configBtnRL();
            configBtnIncluir();
        }
        function configBtnRL() {
            $('#btn-rl').button({
                icons: {
                    primary: "ui-icon-seek-prev"
                },
                text: false
            });
            $('#btn-rl').width(48).height(28);
            $('#btn-rl').click(function () {
                var gridPfl = $("#PerfisGrid");
                var gridOR = $("#OperadoresRGrid");
                if (gridPfl.jqGrid('getGridParam', 'selrow') !== null && gridOR.jqGrid('getGridParam', 'selrow') !== null) {
                    var selRowId1 = gridPfl.jqGrid('getGridParam', 'selrow');
                    var idPerfil = gridPfl.jqGrid('getCell', selRowId1, 'IdPerfil');
                    var selRowId2 = gridOR.jqGrid('getGridParam', 'selrow');
                    var idOperador = gridOR.jqGrid('getCell', selRowId2, 'IdOperador');
                    $.blockUI();
                    PageMethods.Associar(
                        idPerfil,
                        idOperador,
                        onSuccessAssociar,
                        onErrorAssociar);
                }
                return false;
            });
        }
        function onSuccessAssociar(response) {
            $.unblockUI();
            var gridFunc = $("#PerfisGrid");
            var selRowId = gridFunc.jqGrid('getGridParam', 'selrow');
            var idPerfil = gridFunc.jqGrid('getCell', selRowId, 'IdPerfil');
            var subGridR = $("#OperadoresLGrid");
            subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillOperadoresLeft', IdPerfil: idPerfil } });
            subGridR.clearGridData(true).trigger("reloadGrid");
            var subGridR = $("#OperadoresRGrid");
            subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillOperadoresRight', IdPerfil: idPerfil } });
            subGridR.clearGridData(true).trigger("reloadGrid");
            GetElemByClientID('hdfUpdate').val('1');
            exibirNotificacoes();
        }
        function onErrorAssociar(err) {
            $.unblockUI();
            var error_msg = err.get_message();
            GetElemByClientID('hdfError').val('1');
            GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante a Associação');
            GetElemByClientID('hdfText2').val(error_msg);
            exibirNotificacoes();
        }
        function configBtnLR() {
            $('#btn-lr').button({
                icons: {
                    primary: "ui-icon-seek-next"
                },
                text: false
            });
            $('#btn-lr').width(48).height(28);
            $('#btn-lr').click(function () {
                var gridPfl = $("#PerfisGrid");
                var gridOL = $("#OperadoresLGrid");
                if (gridPfl.jqGrid('getGridParam', 'selrow') !== null && gridOL.jqGrid('getGridParam', 'selrow') !== null) {
                    var selRowId1 = gridPfl.jqGrid('getGridParam', 'selrow');
                    var idPerfil = gridPfl.jqGrid('getCell', selRowId1, 'IdPerfil');
                    var selRowId2 = gridOL.jqGrid('getGridParam', 'selrow');
                    var idOperador = gridOL.jqGrid('getCell', selRowId2, 'IdOperador');
                    $.blockUI();
                    PageMethods.Desassociar(
                        idPerfil,
                        idOperador,
                        onSuccessDesassociar,
                        onErrorDesassociar);
                }
                return false;
            });
        }
        function onSuccessDesassociar(response) {
            $.unblockUI();
            var gridFunc = $("#PerfisGrid");
            var selRowId = gridFunc.jqGrid('getGridParam', 'selrow');
            var idPerfil = gridFunc.jqGrid('getCell', selRowId, 'IdPerfil');
            var subGridR = $("#OperadoresLGrid");
            subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillOperadoresLeft', IdPerfil: idPerfil } });
            subGridR.clearGridData(true).trigger("reloadGrid");
            var subGridR = $("#OperadoresRGrid");
            subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillOperadoresRight', IdPerfil: idPerfil } });
            subGridR.clearGridData(true).trigger("reloadGrid");
            GetElemByClientID('hdfUpdate').val('1');
            exibirNotificacoes();
        }
        function onErrorDesassociar(err) {
            $.unblockUI();
            var error_msg = err.get_message();
            GetElemByClientID('hdfError').val('1');
            GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante a Desassociação');
            GetElemByClientID('hdfText2').val(error_msg);
            exibirNotificacoes();
        }
        function configBtnIncluir() {
            $('#btn-incluir').button({
                icons: {
                    primary: "ui-icon-person",
                    secondary: "ui-icon-plusthick"
                },
                text: true
            });
            GetElemByClientID('btn-incluir').click(function () {
                clearOperadores();
                var div = $('#div-operador');
                div.dialog(
                {
                    autoOpen: false,
                    modal: true,
                    resizable: false,
                    width: 775,
                    height: 300,
                    title: 'Cadastro de Operadores',
                    buttons: {
                        "Fechar": function () {
                            $(this).dialog("close");
                        }
                    },
                    beforeClose: function (event, ui) {
                        var grid = $("#PerfisGrid");
                        if (grid.jqGrid('getGridParam', 'selrow') !== null) {
                            var selRowId = grid.jqGrid('getGridParam', 'selrow');
                            var idPerfil = grid.jqGrid('getCell', selRowId, 'IdPerfil');
                            var subGrid = $("#OperadoresRGrid");
                            subGrid.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillOperadoresRight', IdPerfil: idPerfil } });
                            subGrid.clearGridData(true).trigger("reloadGrid");
                        }
                    }
                });
                $('#del_OperadoresGrid').attr('title', 'Inativar operador selecionado');
                $('#del_OperadoresGrid div').html('<span class="ui-icon ui-icon-alert"></span> ');
                div.dialog('open');
                return false;
            });
        }
        function clearOperadores() {
            var subGrid = $("#OperadoresGrid");
            subGrid.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillOperadores' } });
            subGrid.clearGridData(true).trigger("reloadGrid");
        }
        function executarReset() {
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
        <asp:HiddenField ID="hdfReadOnly" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfApenasAtivos" runat="server" EnableViewState="true" Value="1" />
    </div>
    <div>
        <div style="padding: 5px;">
            <pflGrid:PerfiljqGrid ID="perfilGrid" runat="server" />
        </div>
        <div style="padding: 5px;margin-top:10px;">
            <div style="float: left; width: 45%;">
                <oprGridL:OperadorjqGridL ID="OperadorjqGridL" runat="server" />
            </div>
            <div style="float: left; width: 10%; text-align: center; padding-top:50px;">
                <div>
                    <div style="padding:5px;">
                        <button id="btn-lr"></button>
                    </div>
                    <div style="padding:5px;">
                        <button id="btn-rl"></button>
                    </div>
                </div>
            </div>
            <div style="float: left; width: 45%; text-align: right;">
                <oprGridR:OperadorjqGridR ID="OperadorjqGridR" runat="server" />
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <div style="padding: 5px;margin-top:10px;">
        <div style="padding:5px;text-align:center;">
            <button id="btn-incluir">Incluir Operador</button>
        </div>
    </div>
    <div id="div-operador" style="display:none;">
        <oprGrid:OperadorjqGrid ID="OperadorjqGrid" runat="server" />
    </div>
</asp:Content>
