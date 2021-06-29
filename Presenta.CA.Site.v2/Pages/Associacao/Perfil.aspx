<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Presenta.CA.Site.Pages.Associacao.Perfil" %>

<%@ Register Src="~/UserControls/SistemaUC.ascx" TagName="SistemajqGrid" TagPrefix="stmGrid" %>
<%@ Register Src="~/UserControls/AplicativoUC.ascx" TagName="AplicativojqGrid" TagPrefix="appGrid" %>
<%@ Register Src="~/UserControls/FuncionalidadeUC.ascx" TagName="FuncionalidadejqGrid" TagPrefix="fncGrid" %>
<%@ Register Src="~/UserControls/PerfilUC.ascx" TagName="PerfiljqGrid" TagPrefix="pflGrid" %>
<%@ Register Src="~/UserControls/PerfilUCL.ascx" TagName="PerfiljqGridL" TagPrefix="pflGrid" %>
<%@ Register Src="~/UserControls/PerfilUCR.ascx" TagName="PerfiljqGridR" TagPrefix="pflGrid" %>
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
            setMenuHover(1);
            setPageName('Associação - Funcionalidade x Perfil');
        }
        function configGridButtons() {
            $("#SistemasGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
            $("#AplicativosGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
            $("#FuncionalidadesGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
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
                var gridFunc = $("#FuncionalidadesGrid");
                var gridPR = $("#PerfisRGrid");
                var idFuncionalidadeArray = [];
                var idPerfilArray = [];
                var rowDataFunc = gridFunc.jqGrid('getGridParam', 'selarrrow');
                var rowDataPR = gridPR.jqGrid('getGridParam', 'selarrrow');
                
                for (var i = 0; i < rowDataFunc.length; i++) {
                    idFuncionalidadeArray.push(gridFunc.jqGrid('getCell', rowDataFunc[i], 'IdFuncionalidade'));
                }
                for (var i = 0; i < rowDataPR.length; i++) {
                    idPerfilArray.push(gridPR.jqGrid('getCell', rowDataPR[i], 'IdPerfil'));
                }

                if (idFuncionalidadeArray.length > 0 && idPerfilArray.length > 0) {
                    $.blockUI();
                    PageMethods.AssociarLote(
                        idFuncionalidadeArray,
                        idPerfilArray,
                        onSuccessAssociar,
                        onErrorAssociar);
                }
                return false;
            });
        }
        function onSuccessAssociar(response) {
            $.unblockUI();
            var gridFunc = $("#FuncionalidadesGrid");
            var selRowId = gridFunc.jqGrid('getGridParam', 'selrow');
            var idFuncionalidade = gridFunc.jqGrid('getCell', selRowId, 'IdFuncionalidade');
            var subGridR = $("#PerfisLGrid");
            subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillPerfisLeft', IdFuncionalidade: idFuncionalidade } });
            subGridR.clearGridData(true).trigger("reloadGrid");
            var subGridR = $("#PerfisRGrid");
            subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillPerfisRight', IdFuncionalidade: idFuncionalidade } });
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
                var gridFunc = $("#FuncionalidadesGrid");
                var gridPL = $("#PerfisLGrid");
                if (gridFunc.jqGrid('getGridParam', 'selrow') !== null && gridPL.jqGrid('getGridParam', 'selrow') !== null) {
                    var selRowId1 = gridFunc.jqGrid('getGridParam', 'selrow');
                    var idFuncionalidade = gridFunc.jqGrid('getCell', selRowId1, 'IdFuncionalidade');
                    var selRowId2 = gridPL.jqGrid('getGridParam', 'selrow');
                    var idPerfil = gridPL.jqGrid('getCell', selRowId2, 'IdPerfil');
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
            var gridFunc = $("#FuncionalidadesGrid");
            var selRowId = gridFunc.jqGrid('getGridParam', 'selrow');
            var idFuncionalidade = gridFunc.jqGrid('getCell', selRowId, 'IdFuncionalidade');
            var subGridR = $("#PerfisLGrid");
            subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillPerfisLeft', IdFuncionalidade: idFuncionalidade } });
            subGridR.clearGridData(true).trigger("reloadGrid");
            var subGridR = $("#PerfisRGrid");
            subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillPerfisRight', IdFuncionalidade: idFuncionalidade } });
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
                    primary: "ui-icon-contact",
                    secondary: "ui-icon-plusthick"
                },
                text: true
            });
            GetElemByClientID('btn-incluir').click(function () {
                clearPerfis();
                var div = $('#div-operador');
                div.dialog(
                {
                    autoOpen: false,
                    modal: true,
                    resizable: false,
                    width: 775,
                    height: 300,
                    title: 'Cadastro de Perfis',
                    buttons: {
                        "Fechar": function () {
                            $(this).dialog("close");
                        }
                    },
                    beforeClose: function (event, ui) {
                        var grid = $("#FuncionalidadesGrid");
                        var subGrid = $("#PerfisGrid");
                        if (grid.jqGrid('getGridParam', 'selrow') !== null) {
                            var selRowId = grid.jqGrid('getGridParam', 'selrow');
                            var idFuncionalidade = grid.jqGrid('getCell', selRowId, 'IdFuncionalidade');
                            var subGridR = $("#PerfisRGrid");
                            subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillPerfisRight', IdFuncionalidade: idFuncionalidade } });
                            subGridR.clearGridData(true).trigger("reloadGrid");
                        }
                    }
                });
                div.dialog('open');
                return false;
            });
        }
        function clearPerfis() {
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
    </div>
    <div>
        <div style="padding: 5px;">
            <stmGrid:SistemajqGrid ID="sistemaGrid" runat="server" />
        </div>
        <div style="padding: 5px;">
            <appGrid:AplicativojqGrid ID="aplicativojqGrid" runat="server" />
        </div>
        <div style="padding: 5px;">
            <fncGrid:FuncionalidadejqGrid ID="funcionalidadejqGrid" runat="server" />
        </div>
        <div style="padding: 5px;margin-top:10px;">
            <div style="float: left; width: 45%;">
                <pflGrid:PerfiljqGridL ID="PerfiljqGridL" runat="server" />
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
                <pflGrid:PerfiljqGridR ID="PerfiljqGridR" runat="server" />
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <div style="padding: 5px;margin-top:10px;">
        <div style="padding:5px;text-align:center;">
            <button id="btn-incluir">Incluir Perfil</button>
        </div>
    </div>
    <div id="div-operador" style="display:none;">
        <pflGrid:PerfiljqGrid ID="OperadorjqGrid" runat="server" />
    </div>
</asp:Content>
