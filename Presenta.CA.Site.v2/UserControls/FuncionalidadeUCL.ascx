<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FuncionalidadeUCL.ascx.cs" Inherits="Presenta.CA.Site.UserControls.FuncionalidadeUCL" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#FuncionalidadesGridL").jqGrid({
            url: '<%= ResolveClientUrl("~/UserControls/FuncionalidadeUCHandler.ashx") %>',
            editurl: '<%= ResolveClientUrl("~/UserControls/FuncionalidadeUCHandler.ashx") %>',
            datatype: 'json',
            postData: { ActionPage: 'TransportType', Action: 'FillFuncionalidadesLeft', IdPerfil: '', IdSistema: '', IdAplicativo: '' },
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
            pager: '#FuncionalidadesGridLPager',
            gridview: true,
            autowidth: true,
            sortname: 'DsFuncionalidade',
            prmNames: { nd: null },
            jsonReader: { cell: "", search: "isSearch" },
            viewrecords: true,
            sortorder: 'asc',
            caption: 'Funcionalidades Associadas',
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
        $("#FuncionalidadesGridL").jqGrid('navGrid', '#FuncionalidadesGridLPager', { view: true, edit: false, add: false, del: false },
            { /*Update*/ }, { /*Insert*/ }, { /*Delete*/ }, { /*View*/ width: 460 }
        );
    });
</script>
<div>
    <table id="FuncionalidadesGridL"></table>
    <div id="FuncionalidadesGridLPager"></div>
</div>