﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PerfilUCL.ascx.cs" Inherits="Presenta.CA.Site.UserControls.PerfilUCL" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#PerfisLGrid").jqGrid({
            url: '<%= ResolveClientUrl("~/UserControls/PerfilUCHandler.ashx") %>',
            editurl: '<%= ResolveClientUrl("~/UserControls/PerfilUCHandler.ashx") %>',
            datatype: 'json',
            postData: { ActionPage: 'TransportType', Action: 'FillPerfisLeft', IdFuncionalidade: '' },
            height: 'auto',
            colNames: ['', 'Nome do Perfil'],
            colModel: [
                { name: 'IdPerfil', index: 'IdPerfil', width: 0, editable: false, key: true, hidden: true },
                { name: 'DsPerfil', index: 'DsPerfil', align: 'left', width: 200, editable: false }
            ],
            rowNum: 5,
            rowList: [5, 10, 15],
            pager: '#PerfisLGridPager',
            gridview: true,
            autowidth: true,
            sortname: 'DsPerfil',
            prmNames: { nd: null },
            jsonReader: { cell: "", search: "isSearch" },
            viewrecords: true,
            sortorder: 'asc',
            caption: 'Perfis Associados',
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
        $("#PerfisLGrid").jqGrid('navGrid', '#PerfisLGridPager', { view: true, edit: false, add: false, del: false },
            { /*Update*/ }, { /*Insert*/ }, { /*Delete*/ }, { /*View*/ width: 460 }
        );
    });
</script>
<div>
    <table id="PerfisLGrid" ></table>
    <div id="PerfisLGridPager"></div>
</div>