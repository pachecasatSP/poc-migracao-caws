<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OperadorUC.ascx.cs" Inherits="Presenta.CA.Site.UserControls.OperadorUC" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#OperadoresGrid").jqGrid({
            url: '<%= ResolveClientUrl("~/UserControls/OperadorUCHandler.ashx") %>',
            editurl: '<%= ResolveClientUrl("~/UserControls/OperadorUCHandler.ashx") %>',
            datatype: 'json',
            postData: { ActionPage: 'TransportType', Action: 'FillOperadores', ApenasAtivos: GetElemByClientID('hdfApenasAtivos').val() },
            height: 'auto',
            colNames: ['', 'Login', 'Nome', 'Email', 'Situação'],
            colModel: [
                { name: 'IdOperador', index: 'IdOperador', width: 0, editable: false, key: true, hidden: true },
                { name: 'CdOperador', index: 'CdOperador', align: 'left', width: 120, editable: true, editoptions: { size: 20, maxlength: 25 } },
                { name: 'NmOperador', index: 'NmOperador', align: 'left', width: 260, editable: true, editoptions: { size: 50, maxlength: 45 } },
                { name: 'DsEmail', index: 'DsEmail', align: 'left', width: 220, editable: true, editoptions: { size: 50 } },
                { name: 'StOperador', index: 'StOperador', edittype: 'select', align: 'center', width: 120, editable: true, editoptions: { size: 1, value: "1:Ativo;2:Bloqueado;3:Inativo" }, stype: 'select', searchoptions: { value: "1:Ativo;2:Bloqueado;3:Inativo" } }
            ],
            rowNum: 5,
            rowList: [5, 10, 15],
            pager: '#OperadoresGridPager',
            gridview: true,
            autowidth: true,
            sortname: 'NmOperador',
            prmNames: { nd: null },
            jsonReader: { cell: "", search: "isSearch" },
            viewrecords: true,
            sortorder: 'asc',
            caption: 'Operadores',
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
        $("#OperadoresGrid").jqGrid('navGrid', '#OperadoresGridPager', { view: true, edit: true, add: true, del: true },
            { /*Update*/
                width: 400, editData: { ActionPage: 'TransportType', Action: 'UpdateOperadores' },
                beforeSubmit: function (postdata, formid) {
                    var grid = $('#OperadoresGrid');
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    var idOperador = grid.jqGrid('getCell', selRowId, 'IdOperador');
                    postdata.IdOperador = idOperador;
                    return [true, ''];
                },
                afterSubmit: function (response, postdata) {
                    afterSubmitUpdate(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*Insert*/
                width: 400, editData: { ActionPage: 'TransportType', Action: 'InsertOperadores' },
                beforeSubmit: function (postdata, formid) {
                    if (postdata.CdOperador == "") {
                        $("#CdOperador").css('border-color', 'red').focus().tipsy('show');
                        return [false, 'Preencha o Login'];
                    }
                    else {
                        $("#CdOperador").css('border-color', '').tipsy('hide');
                    }
                    if (postdata.NmOperador == "") {
                        $("#NmOperador").css('border-color', 'red').focus().tipsy('show');
                        return [false, 'Preencha o nome do operador'];
                    }
                    else {
                        $("#NmOperador").css('border-color', '').tipsy('hide');
                    }
                    return [true, ''];
                },
                afterSubmit: function (response, postdata) {
                    afterSubmitInsert(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*Delete*/
                delData: { ActionPage: 'TransportType', Action: 'DeleteOperadores' },
                beforeShowForm: function (formid) {
                    $('#delhdOperadoresGrid span').eq(0).text("Inativar");
                    $('.delmsg').text('Inativar operador(es) selecionado(s)?');
                    $('.DelButton a').eq(0).html('Inativar<span class="ui-icon ui-icon-alert"></span>');
                },
                afterSubmit: function (response, postdata) {
                    afterSubmitDelete(response, postdata);
                    postdata.Data = response.responseText;
                    return [true, '', response.responseText];
                }
            }, { /*View*/ width: 460 }

            ).navSeparatorAdd('#OperadoresGridPager', { sepclass: 'ui-separator', sepcontent: '' }
                        ).jqGrid('navButtonAdd', '#OperadoresGridPager',
                {
                    caption: '', title: 'Reset Senha', buttonicon: "ui-icon-arrowrefresh-1-e", onClickButton: executarReset, position: "last"
                    , editData: { ActionPage: 'TransportType', Action: 'Reset' }
                }
        );
    });  


</script>
<div>
    <table id="OperadoresGrid"></table>
    <div id="OperadoresGridPager"></div>
</div>
