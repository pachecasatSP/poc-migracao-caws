<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="PerfilFuncionalidade.aspx.cs" Inherits="Presenta.CA.Site.Pages.Associacao.PerfilFuncionalidade" %>

<%@ Register Src="~/UserControls/SistemaUC.ascx" TagName="SistemajqGrid" TagPrefix="stmGrid" %>
<%@ Register Src="~/UserControls/AplicativoUC.ascx" TagName="AplicativojqGrid" TagPrefix="appGrid" %>
<%@ Register Src="~/UserControls/PerfilUC.ascx" TagName="PerfiljqGrid" TagPrefix="pflGrid" %>
<%@ Register Src="~/UserControls/FuncionalidadeUC.ascx" TagName="FuncionalidadejqGrid" TagPrefix="fncGrid" %>
<%@ Register Src="~/UserControls/FuncionalidadeUCL.ascx" TagName="FuncionalidadejqGridL" TagPrefix="fncGrid" %>
<%@ Register Src="~/UserControls/FuncionalidadeUCR.ascx" TagName="FuncionalidadejqGridR" TagPrefix="fncGrid" %>
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
            setMenuHover(2);
            setPageName('Associação - Perfil x Funcionalidade');
        }
        function configGridButtons() {
            $("#SistemasGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
            $("#AplicativosGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
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
                var gridFR = $("#FuncionalidadesGridR");
                var idFuncionalidadeArray = [];
                var rowDataFR = gridFR.jqGrid('getGridParam', 'selarrrow');
                var gridPerf = $("#PerfisGrid");
                var selRowId = gridPerf.jqGrid('getGridParam', 'selrow');
                var idPerfil = gridPerf.jqGrid('getCell', selRowId, 'IdPerfil');

                for (var i = 0; i < rowDataFR.length; i++) {
                    idFuncionalidadeArray.push(gridFR.jqGrid('getCell', rowDataFR[i], 'IdFuncionalidade'));
                }

                if (idFuncionalidadeArray.length > 0 && idPerfil > 0) {
                    $.blockUI();
                    PageMethods.AssociarLote(
                        idFuncionalidadeArray,
                        idPerfil,
                        onSuccessAssociar,
                        onErrorAssociar);
                }
                return false;
            });
        }
        function onSuccessAssociar(response) {
            $.unblockUI();
            var grid = $("#AplicativosGrid");
            var selRowId, idAplicativo, idSistema, idPerfil;
            selRowId = grid.jqGrid('getGridParam', 'selrow');
            idAplicativo = grid.jqGrid('getCell', selRowId, 'IdAplicativo');
            idSistema = grid.jqGrid('getCell', selRowId, 'IdSistema');

            grid = $("#PerfisGrid");
            selRowId = grid.jqGrid('getGridParam', 'selrow');
            idPerfil = grid.jqGrid('getCell', selRowId, 'IdPerfil');

            if (idAplicativo > 0 && idSistema > 0 && idPerfil > 0) {
                var subGridL = $("#FuncionalidadesGridL");
                subGridL.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillFuncionalidadesLeft', IdPerfil: idPerfil, IdSistema: idSistema, IdAplicativo: idAplicativo } });
                subGridL.clearGridData(true).trigger("reloadGrid");
                var subGridR = $("#FuncionalidadesGridR");
                subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillFuncionalidadesRight', IdPerfil: idPerfil, IdSistema: idSistema, IdAplicativo: idAplicativo } });
                subGridR.clearGridData(true).trigger("reloadGrid");
                GetElemByClientID('hdfUpdate').val('1');
                exibirNotificacoes();
            }
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
                var gridFL = $("#FuncionalidadesGridL");
                var gridPerf = $("#PerfisGrid");
                if (gridFL.jqGrid('getGridParam', 'selrow') !== null && gridPerf.jqGrid('getGridParam', 'selrow') !== null) {
                    var selRowId1 = gridFL.jqGrid('getGridParam', 'selrow');
                    var idFuncionalidade = gridFL.jqGrid('getCell', selRowId1, 'IdFuncionalidade');
                    var selRowId2 = gridPerf.jqGrid('getGridParam', 'selrow');
                    var idPerfil = gridPerf.jqGrid('getCell', selRowId2, 'IdPerfil');
                    $.blockUI();
                    PageMethods.Desassociar(
                        idFuncionalidade,
                        idPerfil,
                        onSuccessDesassociar,
                        onErrorDesassociar);
                }
                return false;
            });
        }
        function onSuccessDesassociar(response) {
            $.unblockUI();
            var grid = $("#AplicativosGrid");
            var selRowId, idAplicativo, idSistema, idPerfil;
            selRowId = grid.jqGrid('getGridParam', 'selrow');
            idAplicativo = grid.jqGrid('getCell', selRowId, 'IdAplicativo');
            idSistema = grid.jqGrid('getCell', selRowId, 'IdSistema');

            grid = $("#PerfisGrid");
            selRowId = grid.jqGrid('getGridParam', 'selrow');
            idPerfil = grid.jqGrid('getCell', selRowId, 'IdPerfil');

            if (idAplicativo > 0 && idSistema > 0 && idPerfil > 0) {
                var subGridL = $("#FuncionalidadesGridL");
                subGridL.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillFuncionalidadesLeft', IdPerfil: idPerfil, IdSistema: idSistema, IdAplicativo: idAplicativo } });
                subGridL.clearGridData(true).trigger("reloadGrid");
                var subGridR = $("#FuncionalidadesGridR");
                subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillFuncionalidadesRight', IdPerfil: idPerfil, IdSistema: idSistema, IdAplicativo: idAplicativo } });
                subGridR.clearGridData(true).trigger("reloadGrid");
                GetElemByClientID('hdfUpdate').val('1');
                exibirNotificacoes();
            }
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
                    primary: "ui-icon-contact",
                    secondary: "ui-icon-plusthick"
                },
                text: true
            });
            GetElemByClientID('btn-incluir').click(function () {
                clearFuncionalidades();
                var div = $('#div-operador');
                div.dialog(
                {
                    autoOpen: false,
                    modal: true,
                    resizable: false,
                    width: 775,
                    height: 300,
                    title: 'Cadastro de Funcionalidades',
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
                            var subGridR = $("#FuncionalidadesGridR");
                            subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillFuncionalidadesRight', IdPerfil: idPerfil } });
                            subGridR.clearGridData(true).trigger("reloadGrid");
                        }
                    }
                });
                div.dialog('open');
                return false;
            });
        }
        function clearFuncionalidades() {
            var subGrid = $("#PerfisGrid");
            subGrid.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillPerfis' } });
            subGrid.clearGridData(true).trigger("reloadGrid");
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
        <asp:HiddenField ID="hdfFuncionalidadePerfil" runat="server" EnableViewState="true" Value="1" />
    </div>
    <div>
        <div style="padding: 5px;">
            <pflGrid:PerfiljqGrid ID="operadorjqGrid" runat="server" />
        </div>
        <div style="padding: 5px;">
            <stmGrid:SistemajqGrid ID="sistemaGrid" runat="server" />
        </div>
        <div style="padding: 5px;">
            <appGrid:AplicativojqGrid ID="aplicativojqGrid" runat="server" />
        </div>
        <div style="padding: 5px;margin-top:10px;">
            <div style="float: left; width: 45%;">
                <fncGrid:FuncionalidadejqGridL ID="FuncionalidadesGridL" runat="server" />
            </div>
            <div style="float: left; width: 10%; text-align: center;padding-top:50px;">
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
                <fncGrid:FuncionalidadejqGridR ID="FuncionalidadesGridR" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
