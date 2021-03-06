<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OperadorUCL.ascx.cs" Inherits="Presenta.CA.Site.UserControls.OperadorUCL" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#OperadoresLGrid").jqGrid({
            url: '<%= ResolveClientUrl("~/UserControls/OperadorUCHandler.ashx") %>',
            editurl: '<%= ResolveClientUrl("~/UserControls/OperadorUCHandler.ashx") %>',
            datatype: 'json',
            postData: { ActionPage: 'TransportType', Action: 'FillOperadoresLeft', IdPerfil: '' },
            height: 'auto',
            colNames: ['', 'Login', 'Nome'],
            colModel: [
                { name: 'IdOperador', index: 'IdOperador', width: 0, editable: false, key: true, hidden: true },
                { name: 'CdOperador', index: 'CdOperador', align: 'left', width: 50, editable: false },
                { name: 'NmOperador', index: 'NmOperador', align: 'left', width: 100, editable: false }
            ],
            rowNum: 5,
            rowList: [5, 10, 15],
            pager: '#OperadoresLGridPager',
            gridview: true,
            autowidth: true,
            sortname: 'NmOperador',
            prmNames: { nd: null },
            jsonReader: { cell: "", search: "isSearch" },
            viewrecords: true,
            sortorder: 'asc',
            caption: 'Operadores Associados',
            loadError: function (xhr, textStatus, errorThrown) {
                var error_msg = xhr.responseText;
                GetElemByClientID('hdfError').val('1');
                GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o processamento:');
                GetElemByClientID('hdfText2').val(error_msg);
                exibirNotificacoes();
            },
            gridComplete: function () {
                $(this).setSelection($(this).getDataIDs()[0], true);
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
            delData: { ActionPage: 'TransportType', Action: 'Delete' },
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
        $("#OperadoresLGrid").jqGrid('navGrid', '#OperadoresLGridPager', { view: true, edit: false, add: false, del: false },
            { /*Update*/ }, { /*Insert*/ }, { /*Delete*/ }, { /*View*/ width: 460 }
        );
    });
</script>
<div>
    <table id="OperadoresLGrid" ></table>
    <div id="OperadoresLGridPager"></div>
</div>