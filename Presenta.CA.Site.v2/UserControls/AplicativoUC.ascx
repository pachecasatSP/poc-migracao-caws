<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AplicativoUC.ascx.cs" Inherits="Presenta.CA.Site.UserControls.AplicativoUC" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#AplicativosGrid").jqGrid({
            url: '<%= ResolveClientUrl("~/UserControls/AplicativoUCHandler.ashx") %>',
            editurl: '<%= ResolveClientUrl("~/UserControls/AplicativoUCHandler.ashx") %>',
            datatype: 'json',
            postData: { ActionPage: 'TransportType', Action: 'FillAplicativos', IdSistema: '' },
            height: 'auto',
            colNames: ['', 'Nome do Aplicativo', ''],
            colModel: [
                { name: 'IdAplicativo', index: 'IdAplicativo', width: 0, editable: false, key: true, hidden: true },
                { name: 'DsAplicativo', index: 'DsAplicativo', align: 'left', width: 200, editable: true, editoptions: { size: 50 } },
                { name: 'IdSistema', index: 'IdSistema', editable: false, hidden: true }
            ],
            rowNum: 5,
            rowList: [5, 10, 15],
            pager: '#AplicativosGridPager',
            gridview: true,
            autowidth: true,
            sortname: 'DsAplicativo',
            prmNames: { nd: null },
            jsonReader: { cell: "", search: "isSearch" },
            viewrecords: true,
            sortorder: 'asc',
            caption: 'Aplicativos',
            loadError: function (xhr, textStatus, errorThrown) {
                var error_msg = xhr.responseText;
                GetElemByClientID('hdfError').val('1');
                GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o processamento:');
                GetElemByClientID('hdfText2').val(error_msg);
                exibirNotificacoes();
            },
            gridComplete: function () {
                $(this).setSelection($(this).getDataIDs()[0], true);
                setTimeout('clickGridAplicativos();', 100);
            },
            onSelectRow: function (ids) {
                clickGridAplicativos();
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
            closeOnEscape: true
        });
        $.extend($.jgrid.search, {
            multipleSearch: true,
            recreateFilter: true,
            reloadAfterSubmit: true,
            closeOnEscape: true,
            overlay: false
        });
        $("#AplicativosGrid").jqGrid('navGrid', '#AplicativosGridPager', { view: true, edit: true, add: false, del: false },
            { /*Update*/
                width: 400, editData: { ActionPage: 'TransportType', Action: 'UpdateAplicativos' },
                beforeSubmit: function (postdata, formid) {
                    var grid = $('#SistemasGrid');
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    var idSistema = grid.jqGrid('getCell', selRowId, 'IdSistema');
                    postdata.IdSistema = idSistema;
                    return [true, ''];
                },
                afterSubmit: function (response, postdata) {
                    afterSubmitUpdate(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*Insert*/
                width: 400, editData: { ActionPage: 'TransportType', Action: 'InsertAplicativos' },
                beforeSubmit: function (postdata, formid) {
                    var grid = $('#SistemasGrid');
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    var idSistema = grid.jqGrid('getCell', selRowId, 'IdSistema');
                    postdata.IdSistema = idSistema;
                    return [true, ''];
                },
                afterSubmit: function (response, postdata) {
                    afterSubmitInsert(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*Delete*/
                delData: { ActionPage: 'TransportType', Action: 'DeleteAplicativos' },
                afterSubmit: function (response, postdata) {
                    afterSubmitDelete(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*View*/ width: 460 }
        );
    });
    function clickGridAplicativos() {
        var grid = $("#AplicativosGrid");

        var hdfPerfilFuncionalidade = GetElemByClientID('hdfPerfilFuncionalidade').val();
        var hdfFuncionalidadePerfil = GetElemByClientID('hdfFuncionalidadePerfil').val();

        if (hdfPerfilFuncionalidade == "1") {
            var selRowId, idAplicativo, idSistema, idPerfil;
            selRowId = grid.jqGrid('getGridParam', 'selrow');
            idAplicativo = grid.jqGrid('getCell', selRowId, 'IdAplicativo');
            idSistema = grid.jqGrid('getCell', selRowId, 'IdSistema');

            grid = $("#PerfisGrid");
            selRowId = grid.jqGrid('getGridParam', 'selrow');
            idPerfil = grid.jqGrid('getCell', selRowId, 'IdPerfil');
            if ($("#FuncionalidadesGrid").length != 0 && idAplicativo > 0 && idSistema > 0 && idPerfil > 0) {
                var subGridF = $("#FuncionalidadesGrid");
                subGridF.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillFuncionalidadesPorPerfil', IdPerfil: idPerfil, IdSistema: idSistema, IdAplicativo: idAplicativo } });
                subGridF.clearGridData(true).trigger("reloadGrid");
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
                var idAplicativo = grid.jqGrid('getCell', selRowId, 'IdAplicativo');
                var subGrid = $("#FuncionalidadesGrid");
                subGrid.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillFuncionalidades', IdAplicativo: idAplicativo } });
                subGrid.clearGridData(true).trigger("reloadGrid");
            }
        }
    }
</script>
<div>
    <table id="AplicativosGrid"></table>
    <div id="AplicativosGridPager"></div>
</div>