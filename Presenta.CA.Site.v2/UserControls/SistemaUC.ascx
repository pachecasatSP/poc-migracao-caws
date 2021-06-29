<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SistemaUC.ascx.cs" Inherits="Presenta.CA.Site.UserControls.SistemaUC" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#SistemasGrid").jqGrid({
            url: '<%= ResolveClientUrl("~/UserControls/SistemaUCHandler.ashx") %>',
            editurl: '<%= ResolveClientUrl("~/UserControls/SistemaUCHandler.ashx") %>',
            datatype: 'json',
            postData: { ActionPage: 'TransportType', Action: 'FillSistemas' },
            height: 'auto',
            colNames: ['', 'Nome do Sistema'],
            colModel: [
                { name: 'IdSistema', index: 'IdSistema', width: 0, editable: false, key: true, hidden: true },
                { name: 'DsSistema', index: 'DsSistema', align: 'left', width: 200, editable: true, editoptions: { size: 50 } }
            ],
            rowNum: 5,
            rowList: [5, 10, 15],
            pager: '#SistemasGridPager',
            gridview: true,
            autowidth: true,
            sortname: 'DsSistema',
            prmNames: { nd: null },
            jsonReader: { cell: "", search: "isSearch" },
            viewrecords: true,
            sortorder: 'asc',
            caption: 'Sistemas',
            loadError: function (xhr, textStatus, errorThrown) {
                var error_msg = xhr.responseText;
                GetElemByClientID('hdfError').val('1');
                GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o processamento:');
                GetElemByClientID('hdfText2').val(error_msg);
                exibirNotificacoes();
            },
            gridComplete: function () {
                if (GetElemByClientID('hdfIdSistema').length != 0 && GetElemByClientID('hdfIdSistema').val() != '') {
                    //validação do retorno de uma busca
                    if ($(this).getDataIDs().length == 1 && GetElemByClientID('hdfIdSistema').val() != $(this).getDataIDs()[0]) {
                        $(this).setSelection($(this).getDataIDs()[0], true);
                    }
                    else {
                        $(this).setSelection(GetElemByClientID('hdfIdSistema').val(), true);
                    }
                }
                else {
                    $(this).setSelection($(this).getDataIDs()[0], true);
                }
                setTimeout('clickGridSistemas();', 100);
                if (GetElemByClientID('hdfIsPostBack').val() == '1') {
                    GetElemByClientID('reportViewer').show();
                    GetElemByClientID('hdfIsPostBack').val('0');
                }
                else {
                    GetElemByClientID('reportViewer').hide();
                }
            },
            onSelectRow: function (ids) {
                clickGridSistemas();
                setHdfIdSistema();
                GetElemByClientID('reportViewer').hide();
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
        $("#SistemasGrid").jqGrid('navGrid', '#SistemasGridPager', { view: true, edit: true, add: false, del: false },
            { /*Update*/
                width: 400, editData: { ActionPage: 'TransportType', Action: 'UpdateSistemas' },
                afterSubmit: function (response, postdata) {
                    afterSubmitUpdate(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*Insert*/
                width: 400, editData: { ActionPage: 'TransportType', Action: 'InsertSistemas' },
                afterSubmit: function (response, postdata) {
                    afterSubmitInsert(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*Delete*/
                delData: { ActionPage: 'TransportType', Action: 'DeleteSistemas' },
                afterSubmit: function (response, postdata) {
                    afterSubmitDelete(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*View*/ width: 460 }
        );
    });
    function clickGridSistemas() {
        var grid = $("#SistemasGrid");
        if (grid.jqGrid('getGridParam', 'selrow') !== null) {
            var selRowId = grid.jqGrid('getGridParam', 'selrow');
            var idSistema = grid.jqGrid('getCell', selRowId, 'IdSistema');
            var subGrid = $("#AplicativosGrid");
            subGrid.setGridParam({ 'postData': { ActionPage: 'TransportType', Action: 'FillAplicativos', IdSistema: idSistema } });
            subGrid.clearGridData(true).trigger("reloadGrid");
        }
    }
    function setHdfIdSistema() {
        var grid = $("#SistemasGrid");
        if (grid.jqGrid('getGridParam', 'selrow') !== null) {
            if (GetElemByClientID('hdfIdSistema').length != 0) {
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                var idSistema = grid.jqGrid('getCell', selRowId, 'IdSistema');
                GetElemByClientID('hdfIdSistema').val(idSistema);
                return true;
            }
        }
    }
</script>
<div>
    <table id="SistemasGrid"></table>
    <div id="SistemasGridPager"></div>
</div>
