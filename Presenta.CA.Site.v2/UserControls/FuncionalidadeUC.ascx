<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FuncionalidadeUC.ascx.cs" Inherits="Presenta.CA.Site.UserControls.FuncionalidadeUC" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#FuncionalidadesGrid").jqGrid({
            url: '<%= ResolveClientUrl("~/UserControls/FuncionalidadeUCHandler.ashx") %>',
            editurl: '<%= ResolveClientUrl("~/UserControls/FuncionalidadeUCHandler.ashx") %>',
            datatype: 'json',
            postData: { ActionPage: 'TransportType', Action: 'FillFuncionalidades', IdAplicativo: '' },
            height: 'auto',
            colNames: ['', 'Nome da Funcionalidade', 'Situação da Funcionalidade', ''],
            colModel: [
                { name: 'IdFuncionalidade', index: 'IdFuncionalidade', width: 0, editable: false, key: true, hidden: true },
                { name: 'DsFuncionalidade', index: 'DsFuncionalidade', align: 'left', width: 200, editable: true, editoptions: { size: 40, readonly: true } },
                { name: 'StFuncionalidade', index: 'StFuncionalidade', edittype: 'select', align: 'center', width: 100, editable: true, editoptions: { size: 1, value: "1:Ativo;2:Inativo" }, stype: 'select', searchoptions: { value: "1:Ativo;2:Inativo" } },
                { name: 'IdAplicativo', index: 'IdAplicativo', editable: false, hidden: true }
            ],
            rowNum: 5,
            rowList: [5, 10, 15],
            pager: '#FuncionalidadesGridPager',
            gridview: true,
            autowidth: true,
            sortname: 'DsFuncionalidade',
            prmNames: { nd: null },
            jsonReader: { cell: "", search: "isSearch" },
            viewrecords: true,
            sortorder: 'asc',
            caption: 'Funcionalidades',
            multiselect: true,
            loadError: function (xhr, textStatus, errorThrown) {
                var error_msg = xhr.responseText;
                GetElemByClientID('hdfError').val('1');
                GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o processamento:');
                GetElemByClientID('hdfText2').val(error_msg);
                exibirNotificacoes();
            },
            gridComplete: function () {
                $(this).setSelection($(this).getDataIDs()[0], true);
                setTimeout('clickGridFuncionalidades();', 100);
            },
            onSelectRow: function (ids) {
                clickGridFuncionalidades();
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
        $("#FuncionalidadesGrid").jqGrid('navGrid', '#FuncionalidadesGridPager', { view: true, edit: true, add: false, del: false },
            { /*Update*/
                width: 400, editData: { ActionPage: 'TransportType', Action: 'UpdateFuncionalidades' },
                beforeSubmit: function (postdata, formid) {
                    var grid = $('#AplicativosGrid');
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    var idAplicativo = grid.jqGrid('getCell', selRowId, 'IdAplicativo');
                    postdata.IdAplicativo = idAplicativo;
                    return [true, ''];
                },
                afterSubmit: function (response, postdata) {
                    afterSubmitUpdate(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*Insert*/
                width: 400, editData: { ActionPage: 'TransportType', Action: 'InsertFuncionalidades' },
                beforeSubmit: function (postdata, formid) {
                    var grid = $('#AplicativosGrid');
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    var idAplicativo = grid.jqGrid('getCell', selRowId, 'IdAplicativo');
                    postdata.IdAplicativo = idAplicativo;
                    return [true, ''];
                },
                afterSubmit: function (response, postdata) {
                    afterSubmitInsert(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*Delete*/
                delData: { ActionPage: 'TransportType', Action: 'DeleteFuncionalidades' },
                afterSubmit: function (response, postdata) {
                    afterSubmitDelete(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*View*/ width: 460 }
        );
    });
    function clickGridFuncionalidades() {
        var grid = $("#FuncionalidadesGrid");
        var subGrid = $("#PerfisGrid");

        var hdfPerfilFuncionalidade = GetElemByClientID('hdfPerfilFuncionalidade').val();
        if (hdfPerfilFuncionalidade != "1") {
            if (grid.jqGrid('getGridParam', 'selrow') !== null) {
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                var idFuncionalidade = grid.jqGrid('getCell', selRowId, 'IdFuncionalidade');
                subGrid.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillPerfisPorFuncionalidade', IdFuncionalidade: idFuncionalidade } });
                subGrid.clearGridData(true).trigger("reloadGrid");
                if ($("#PerfisLGrid").length != 0) {
                    var subGridL = $("#PerfisLGrid");
                    var rowDataFunc = grid.jqGrid('getGridParam', 'selarrrow');
                    if (rowDataFunc.length > 1) { idFuncionalidade = ''; }
                    subGridL.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillPerfisLeft', IdFuncionalidade: idFuncionalidade } });
                    subGridL.clearGridData(true).trigger("reloadGrid");
                }
                if ($("#PerfisRGrid").length != 0) {
                    var subGridR = $("#PerfisRGrid");
                    var idFuncionalidadeArray = [];
                    var rowDataFunc = grid.jqGrid('getGridParam', 'selarrrow');
                    for (var i = 0; i < rowDataFunc.length; i++) {
                        idFuncionalidadeArray = idFuncionalidadeArray + grid.jqGrid('getCell', rowDataFunc[i], 'IdFuncionalidade') + ", ";
                    }
                    idFuncionalidadeArray = idFuncionalidadeArray.substring(0, idFuncionalidadeArray.length - 2)
                    subGridR.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillPerfisRightLote', IdFuncionalidade: idFuncionalidadeArray } });
                    subGridR.clearGridData(true).trigger("reloadGrid");
                }
            } else {
                subGrid.clearGridData(true);
                if ($("#PerfisLGrid").length != 0) {
                    $("#PerfisLGrid").clearGridData(true);
                }
                if ($("#PerfisRGrid").length != 0) {
                    $("#PerfisRGrid").clearGridData(true);
                }
            }
        }
    }
</script>
<div>
    <table id="FuncionalidadesGrid"></table>
    <div id="FuncionalidadesGridPager"></div>
</div>