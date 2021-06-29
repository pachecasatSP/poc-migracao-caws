<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PerfilUC.ascx.cs" Inherits="Presenta.CA.Site.UserControls.PerfilUC" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#PerfisGrid").jqGrid({
            url: '<%= ResolveClientUrl("~/UserControls/PerfilUCHandler.ashx") %>',
            editurl: '<%= ResolveClientUrl("~/UserControls/PerfilUCHandler.ashx") %>',
            datatype: 'json',
            postData: { ActionPage: 'TransportType', Action: 'FillPerfis' },
            height: 'auto',
            colNames: ['', 'Nome do Perfil', 'Situação do Perfil'],
            colModel: [
                { name: 'IdPerfil', index: 'IdPerfil', width: 0, editable: false, key: true, hidden: true },
                { name: 'DsPerfil', index: 'DsPerfil', align: 'left', width: 485, editable: true, editoptions: { size: 50, maxlength: 45 } },
                { name: 'StPerfil', index: 'StPerfil', edittype: 'select', align: 'center', width: 250, editable: true, editoptions: { size: 1, value: "1:Ativo;2:Inativo" }, stype: 'select', searchoptions: { value: "1:Ativo;2:Inativo" } }
            ],
            rowNum: 5,
            rowList: [5, 10, 15],
            pager: '#PerfisGridPager',
            gridview: true,
            autowidth: true,
            sortname: 'DsPerfil',
            prmNames: { nd: null },
            jsonReader: { cell: "", search: "isSearch" },
            viewrecords: true,
            sortorder: 'asc',
            caption: 'Perfis',
            loadError: function (xhr, textStatus, errorThrown) {
                var error_msg = xhr.responseText;
                GetElemByClientID('hdfError').val('1');
                GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o processamento:');
                GetElemByClientID('hdfText2').val(error_msg);
                exibirNotificacoes();
            },
            gridComplete: function () {
                $(this).setSelection($(this).getDataIDs()[0], true);
                setTimeout('clickGridPerfis();', 100);
            },
            onSelectRow: function (ids) {
                clickGridPerfis();
            }
        });
        $.extend($.jgrid.edit, {
            savekey: [true, 13],
            closeOnEscape: true,
            closeAfterEdit: true,
            closeAfterAdd: true,
            reloadAfterSubmit: true,
            recreateForm: true
        });
        $.extend($.jgrid.del, {
            reloadAfterSubmit: true,
            closeOnEscape: true,
            afterComplete: function () {
                var p = $('#PerfisGrid')[0].p;
                var newPage = p.page;
                if (p.reccount === 0 && newPage > p.lastpage && newPage > 1) {
                    newPage--;
                }
                $(p.pager + " input.ui-pg-input").val(newPage);
                $('#PerfisGrid').trigger("reloadGrid", [{ page: newPage }]);
            }
        });
        $.extend($.jgrid.search, {
            multipleSearch: true,
            recreateFilter: true,
            reloadAfterSubmit: true,
            closeOnEscape: true,
            overlay: false
        });
        $("#PerfisGrid").jqGrid('navGrid', '#PerfisGridPager', { view: true, edit: true, add: true, del: true },
            { /*Update*/
                width: 400, editData: { ActionPage: 'TransportType', Action: 'UpdatePerfis' },
                afterSubmit: function (response, postdata) {
                    afterSubmitUpdate(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*Insert*/
                width: 400, editData: { ActionPage: 'TransportType', Action: 'InsertPerfis' },
                afterSubmit: function (response, postdata) {
                    afterSubmitInsert(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*Delete*/
                delData: { ActionPage: 'TransportType', Action: 'DeletePerfis' },
                afterSubmit: function (response, postdata) {
                    afterSubmitDelete(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*View*/ width: 460 }
        );
    });
    function clickGridPerfis() {
        var grid = $("#PerfisGrid");

        var hdfPerfilFuncionalidade = GetElemByClientID('hdfPerfilFuncionalidade').val();
        var hdfFuncionalidadePerfil = GetElemByClientID('hdfFuncionalidadePerfil').val();

        if (hdfPerfilFuncionalidade == "1") {
            if (grid.jqGrid('getGridParam', 'selrow') !== null) {
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                var idPerfil = grid.jqGrid('getCell', selRowId, 'IdPerfil');
                if ($("#SistemasGrid").length != 0) {
                    var subGridS = $("#SistemasGrid");
                    subGridS.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillSistemasPorPerfil', IdPerfil: idPerfil } });
                    subGridS.clearGridData(true).trigger("reloadGrid");
                }
            }
        } else if (hdfFuncionalidadePerfil == "1") {
            var selRowId, idAplicativo, idSistema, idPerfil;
            selRowId = grid.jqGrid('getGridParam', 'selrow');
            idAplicativo = grid.jqGrid('getCell', selRowId, 'IdAplicativo');
            idSistema = grid.jqGrid('getCell', selRowId, 'IdSistema');

            grid = $("#PerfisGrid");
            selRowId = grid.jqGrid('getGridParam', 'selrow');
            idPerfil = grid.jqGrid('getCell', selRowId, 'IdPerfil');
            if (idAplicativo > 0 && idSistema > 0 && idPerfil > 0) {
                if ($("#FuncionalidadesGridL").length != 0) {
                    var subGridL = $("#FuncionalidadesGridL");
                    subGridL.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillFuncionalidadesLeft', IdPerfil: idPerfil, IdSistema: idSistema, IdAplicativo: idAplicativo } });
                    subGridL.clearGridData(true).trigger("reloadGrid");
                }
                if ($("#FuncionalidadesGridR").length != 0) {
                    var subGridR = $("#FuncionalidadesGridR");
                    subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillFuncionalidadesRight', IdPerfil: idPerfil, IdSistema: idSistema, IdAplicativo: idAplicativo } });
                    subGridR.clearGridData(true).trigger("reloadGrid");
                }
            }
        }
        else {
            if (grid.jqGrid('getGridParam', 'selrow') !== null) {
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                var idPerfil = grid.jqGrid('getCell', selRowId, 'IdPerfil');
                if ($("#OperadoresLGrid").length != 0) {
                    var subGridL = $("#OperadoresLGrid");
                    subGridL.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillOperadoresLeft', IdPerfil: idPerfil } });
                    subGridL.clearGridData(true).trigger("reloadGrid");
                }
                if ($("#OperadoresRGrid").length != 0) {
                    var subGridR = $("#OperadoresRGrid");
                    subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillOperadoresRight', IdPerfil: idPerfil } });
                    subGridR.clearGridData(true).trigger("reloadGrid");
                }
            }
        }
    }
</script>
<div>
    <table id="PerfisGrid" ></table>
    <div id="PerfisGridPager"></div>
</div>