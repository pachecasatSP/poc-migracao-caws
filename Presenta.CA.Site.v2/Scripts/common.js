function setPreHeight() {
    $('#pre-msg').height((getWindowHeight() - 384) + 'px');
}
function removeOption(elemName, index) {
    GetElemByClientID(elemName)[0].remove(index);
}
function openDialogBrowseForFolder(txtOutputName) {
    try {
        var objShell = new ActiveXObject("shell.application");
        var ssfWINDOWS = 0;
        var objFolder;
        objFolder = objShell.BrowseForFolder(0, "Selecione o Diretório abaixo:", 0, ssfWINDOWS);
        if (objFolder != null) {
            GetElemByClientID(txtOutputName).val(objFolder.Self.Path);
        }
    } catch (e) {
        GetElemByClientID('hdfError').val('1');
        GetElemByClientID('hdfText1').val(e.message);
        GetElemByClientID('hdfText2').val($('#post-text').text());
        exibirNotificacoes();
    }
}
function addOption(elemName, text, value) {
    var option = document.createElement('option');
    option.value = value;
    option.innerHTML = text;
    GetElemByClientID(elemName)[0].options.add(option);
}
function GetSelectedValue(elemName) {
    var elem = GetElemByClientID(elemName);
    var objects = $('input[name^="' + $(elem).prop('id').replace(/\_/g, '$') + '"]');
    for (var i = 0; i < objects.length; i++) {
        if (objects[i].checked) {
            return $(objects[i]).val();
        }
    }
    return '';
}
function GetSelectedValueListBox(elemName) {
    var elem = GetElemByClientID(elemName);
    var objects = elem[0].options;
    for (var i = 0; i < objects.length; i++) {
        if (objects[i].selected) {
            return $(objects[i]).val();
        }
    }
    return '';
}
function GetSelectedIndexListBox(elemName) {
    var elem = GetElemByClientID(elemName);
    var objects = elem[0].options;
    for (var i = 0; i < objects.length; i++) {
        if (objects[i].selected) {
            return i;
        }
    }
    return '';
}
function GetSelectedTextListBox(elemName) {
    var elem = GetElemByClientID(elemName);
    var objects = elem[0].options;
    for (var i = 0; i < objects.length; i++) {
        if (objects[i].selected) {
            return $(objects[i]).text();
        }
    }
    return '';
}
function isChecked(elemName) {
    var checked = false;
    var elem = GetElemByClientID(elemName);
    var objects = $('input[name^="' + $(elem).prop('id').replace(/\_/g, '$') + '"]');
    for (var i = 0; i < objects.length; i++) {
        if (objects[i].checked) {
            checked = true;
        }
    }
    return checked;
}
function limitText(limitField, limitNum) {
    if (limitField.value != undefined && limitField.value.length > limitNum) {
        limitField.value = limitField.value.substring(0, limitNum);
    }
}
function getWindowHeight() {
    var windowHeight = 0;
    if (typeof (window.innerHeight) == 'number') {
        windowHeight = window.innerHeight;
    }
    else {
        if (document.documentElement && document.documentElement.clientHeight) {
            windowHeight = document.documentElement.clientHeight;
        }
        else {
            if (document.body && document.body.clientHeight) {
                windowHeight = document.body.clientHeight;
            }
        }
    }
    return windowHeight;
}
function getWindowWidth() {
    var windowWidth = 0;
    if (typeof (window.innerWidth) == 'number') {
        windowWidth = window.innerWidth;
    }
    else {
        if (document.documentElement && document.documentElement.clientWidth) {
            windowWidth = document.documentElement.clientWidth;
        }
        else {
            if (document.body && document.body.clientWidth) {
                windowWidth = document.body.clientWidth;
            }
        }
    }
    return windowWidth;
}
function getDesconhecidoMessageHeight() {
    var refHeight = 734;
    if ($('#content').height() > refHeight) {
        return ($('#content').height() - refHeight);
    } else {
        return 0;
    }
}
function getOficiosGridRowsHeight() {
    return ($("#OficiosGrid").getGridParam("reccount") * 23); /* row height */
}
function resizeDesconhecido() {
    if (GetElemByClientID('desconhecido').is(':visible')) {
        var refHeight = 734;
        if (GetElemByClientID('div_tipo_oficio').is(':visible')) {
            $('#div-txt-desc').height(320 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        } else if (GetElemByClientID('div_dados_oficio').is(':visible')) {
            $('#div-txt-desc').height(320 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        } else if (GetElemByClientID('div_envolvido').is(':visible')) {
            $('#div-txt-desc').height(280 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        } else if (GetElemByClientID('div_numero_oficio').is(':visible')) {
            $('#div-txt-desc').height(340 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        } else if (GetElemByClientID('div_numero_processo').is(':visible')) {
            $('#div-txt-desc').height(340 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        } else if (GetElemByClientID('div_numero_jud').is(':visible')) {
            $('#div-txt-desc').height(340 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        } else if (GetElemByClientID('div_interessado').is(':visible')) {
            $('#div-txt-desc').height(340 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        } else if (GetElemByClientID('div_nome_solic').is(':visible')) {
            $('#div-txt-desc').height(340 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        } else if (GetElemByClientID('div_cargo_solic').is(':visible')) {
            $('#div-txt-desc').height(340 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        } else if (GetElemByClientID('div_lier').is(':visible')) {
            $('#div-txt-desc').height(340 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        } else if (GetElemByClientID('div_lfer').is(':visible')) {
            $('#div-txt-desc').height(340 + 228 - oficiosGridEmptyHeight - getOficiosGridRowsHeight() + getDesconhecidoMessageHeight());
            $('#div-anexo-desc').height($('#div-txt-desc').height());
        }
    }
}
function resizeTabs() {
    var pageName = getPageName();
    if (pageName == 'Gerenciamento de Ofícios - Utilitários - Cadastro - Ofícios em Papel') {
        if ($('#tabs-1').length > 0) {
            setTimeout("$('#tabs-1').height($('#contentPanel').height() - $('#frame-superior').height() - $('#barra-botoes').height() - 65);", 100);
        }
        if ($('#tabs-2').length > 0) {
            setTimeout("$('#tabs-2').height($('#contentPanel').height() - $('#frame-superior').height() - $('#barra-botoes').height() - 65);", 100);
        }
        if ($('#tabs-3').length > 0) {
            setTimeout("$('#tabs-3').height($('#contentPanel').height() - $('#frame-superior').height() - $('#barra-botoes').height() - 65);", 100);
        }
        if ($('#tabs-4').length > 0) {
            setTimeout("$('#tabs-4').height($('#contentPanel').height() - $('#frame-superior').height() - $('#barra-botoes').height() - 65);", 100);
        }
    }
}
function clearSelecionRadioButtonList(rblName) {
    var rbl = GetElemByName(rblName);
    for (var x = 0; x < rbl.length; x++) {
        $(rbl[x]).removeAttr('checked');
    }
}
function enableRadioButtonList(rblName) {
    var $rbl = GetElemByName(rblName);
    for (var x = 0; x < $rbl.length; x++) {
        $($rbl[x]).removeAttr('disabled');
    }
}
function disableRadioButtonList(rblName) {
    var $rbl = GetElemByName(rblName);
    for (var x = 0; x < $rbl.length; x++) {
        $($rbl[x]).attr('disabled', 'disabled');
    }
}
function GetActionBarImgName(btnName) {
    switch (btnName) {
        case 'Inserir':
            return '21';
        case 'Alterar':
            return '24';
        case 'Salvar':
            return '45';
        case 'Cancelar':
            return 'cancel';
        case 'Excluir':
            return 'bin_closed';
        case 'Imprimir':
            return 'printer';
        case 'Localizar':
            return 'find';
        default:
            return '';
    }
}
function EnableActionBarButton(btnName) {
    var nomeImagem = GetActionBarImgName(btnName);
    var $lkbS = GetElemByClientID('lkb' + btnName);
    $lkbS.removeAttr('disabled');
    var $imgS = GetElemByClientID('img' + btnName);
    $imgS.removeAttr('disabled');
    if ($imgS.attr('src').indexOf(nomeImagem + '-pb') !== -1) { $imgS.attr('src', $imgS.attr('src').replace(nomeImagem + '-pb', nomeImagem)); }
    var $lblS = GetElemByClientID('lbl' + btnName);
    $lblS.removeAttr('disabled');
    $lblS.css('color', '#10428C');
}
function DisableActionBarButton(btnName) {
    var nomeImagem = GetActionBarImgName(btnName);
    var $lkbS = GetElemByClientID('lkb' + btnName);
    $lkbS.attr('disabled', 'disabled');
    var $imgS = GetElemByClientID('img' + btnName);
    if ($imgS.attr('src').indexOf(nomeImagem + '-pb') === -1) { $imgS.attr('src', $imgS.attr('src').replace(nomeImagem, nomeImagem + '-pb')); }
    $imgS.attr('disabled', 'disabled');
    var $lblS = GetElemByClientID('lbl' + btnName);
    $lblS.css('color', 'gray');
    $lblS.attr('disabled', 'disabled');
}
function toEnabled(elemName) {
    GetElemByClientID(elemName).removeAttr('disabled');
}
function toDisabled(elemName) {
    GetElemByClientID(elemName).attr('disabled', 'disabled');
}
function setDatePicker() {
    $(".datepicker").datepicker({ option: $.datepicker.regional["pt-BR"] });
}
function configOnChange() {
    GetElemByClientID("rblTipoPessoa").change(function () {
        if ($('#' + GetClientID("rblTipoPessoa") + ' input[type=radio]:checked').val() == "F") {
            GetElemByClientID("span-documento").text("CPF");
        } else {
            GetElemByClientID("span-documento").text("CNPJ");
        }
    });
    GetElemByClientID("rblTipoOficio").change(function () {
        switch ($('#' + GetClientID("rblTipoOficio") + ' input[type=radio]:checked').val()) {
            case "1": // BLOQUEIO
                event.preventDefault();
                GetElemByClientID('rblModalidade').Check(0);
                GetElemByClientID('rblBloqueioOriginal').Check(3);
                $("#btn-localizar").button();
                $("#btn-nao-existe-duplicado").button().button("disable");
                $("#btn-duplicidade-encontrada").button().button("disable");
                $('#dialog-loc-duplic').dialog('open');
                GetElemByClientID('ckbOEInfSaldoContaCarta').attr('checked', 'checked');
                GetElemByClientID('ckbOEInfSaldoContaCarta').attr('disabled', 'disabled');

                // matsu - remover
                GetElemByClientID('hdfIdOficio').val('14550');
                GetElemByClientID('hdfIdLote').val('1662');
                GetElemByClientID('hdfIlc').val('20');
                GetElemByClientID('txtNumeroRequisicaoProtocolo').val('201400002');
                GetElemByClientID('ddlInstituicaoRecebeuOficio').val('20');
                GetElemByClientID('txtNumeroOficio').val('456');
                GetElemByClientID('txtNumeroProcesso').val('456');
                GetElemByClientID('txtValor').val('4,56');
                GetElemByClientID('hdfIdVaraJuizo').val('14275');
                GetElemByClientID('ddlDSJuizoVaraDelegacia')[0].options.length = 0;
                addOption("ddlDSJuizoVaraDelegacia", 'JUIZO WEBJUD', GetElemByClientID('hdfIdVaraJuizo').val());
                GetElemByClientID('txtDSEndereco').val('END WEBJUD');
                GetElemByClientID('txtDSBairro').val('BAIRRO WEBJUD');
                GetElemByClientID('txtDSCidade').val('WEBJUD');
                GetElemByClientID('txtDSUF').val('WJ');
                GetElemByClientID('txtDSCEP').val('00000000');
                GetElemByClientID('txtTribunalSolic').val('TRIBUNAL WEBJUD');
                GetElemByClientID('ddlDSNomeJuizSolicitante')[0].options.length = 0;
                addOption("ddlDSNomeJuizSolicitante", 'NOME 2', '506');
                GetElemByClientID('txtDSCargo').val('JUIZ');


                

                return false;
            case "22": // CRÉDITO FUTURO
                event.preventDefault();
                var div = $('#div-credito-futuro');
                div.dialog(
                {
                    autoOpen: false,
                    modal: true,
                    resizable: false,
                    width: 350,
                    height: 160,
                    title: 'Aviso',
                    buttons: {
                        "Fechar": function () {
                            $(this).dialog("close");
                        }
                    }
                });
                div.dialog('open');
                return false;
            default:
                break;
        }
    });
}
function configDialog() {
    $('#dialog').dialog({
        autoOpen: false,
        width: 600,
        modal: true,
        buttons: {
            "Confirmar": function () {
                $(this).dialog("close");
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#dialog-loc-duplic').dialog({
        autoOpen: false,
        height: 500,
        width: 700,
        modal: true,
        closeOnEscape: true,
        buttons: {
            "Fechar": function () {
                $(this).dialog("close");
            }
        },
        beforeClose: function (event, ui) {
            return confirm('A Verificação de Duplicidade de Ofícios NÃO foi concluída. Deseja realmente continuar a digitação?');
        },
        open: function (event, ui) { /*$.unblockUI();*/ }
    });

    $('#dialog_link').click(function (event) {
        event.preventDefault();
        $('#dialog').dialog({
            open: function (type, data) {
                $(this).parent().appendTo("form");
            },
            buttons: {
                "Confirmar": function () {
                    $.blockUI();
                    var idEnvolvido, cdDocumento, tipoDocumento, nomeEnvolvido, grid;
                    grid = $("#EnvolvidosGrid");
                    if (grid.jqGrid('getGridParam', 'selrow') !== null) {
                        selRowId = grid.jqGrid('getGridParam', 'selrow');
                        idEnvolvido = grid.jqGrid('getCell', selRowId, 'IdEnvolvido');
                        cdDocumento = grid.jqGrid('getCell', selRowId, 'CdDocumento');
                        tipoDocumento = grid.jqGrid('getCell', selRowId, 'TipoDocumento');
                        nomeEnvolvido = grid.jqGrid('getCell', selRowId, 'NomeEnvolvido');
                        var env = {
                            IdEnvolvido: idEnvolvido,
                            TipoPessoa: tipoDocumento,
                            CdDocumento: cdDocumento,
                            NomeEnvolvido: nomeEnvolvido,
                            Agencia: '',
                            Conta: '',
                            Certificado: '',
                            Valor: '',
                            IdTransferencia: ''
                        };
                        envData.push(env);
                        var grid = $('#EnvolvidosOutGrid');
                        grid.jqGrid('clearGridData');
                        if (grid.get(0).p.treeGrid) { grid.get(0).addJSONData({ total: 1, page: 1, records: envData.length, rows: envData }); }
                        else { grid.jqGrid('setGridParam', { datatype: 'local', data: envData, rowNum: envData.length }); }
                        grid.trigger('reloadGrid');
                    } else {
                        $.unblockUI();
                        alert('Selecione um Envolvido antes de prosseguir...');
                        return;
                    }
                    $.unblockUI();
                    $(this).dialog("close");
                },
                "Cancelar": function () {
                    $(this).dialog("close");
                }
            }
        });
        $('#dialog').dialog('open');
        return false;
    });

    $('#dialog_link, ul#icons li').hover(
        function () { $(this).addClass('ui-state-hover'); },
        function () { $(this).removeClass('ui-state-hover'); }
    );

    $("#dialogIlc").dialog({
        autoOpen: false,
        //height: 300,
        //width: 350,
        width: 300,
        modal: true,
        show: {
            modal: true,
            effect: "fade",
            duration: 1000
        },
        hide: {
            effect: "fold",
            duration: 1000
        },
        buttons: {
            "OK": function () {
                if (ValidarForm()) {
                    GetElemByClientID('ctl00_ctl00_btnOK').click();
                    $(this).dialog("close");
                }
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            GetElemByClientID('nome-usuario').tipsy('hide');
            GetElemByClientID('senha-usuario').tipsy('hide');
            //allFields.val("").removeClass("ui-state-error");
        }
    });

    $("#dialog-message").dialog({
        modal: true,
        autoOpen: false,
        height: 300,
        width: 600,
        show: {
            modal: true,
            effect: "fade",
            duration: 250
        },
        hide: {
            effect: "fade",
            duration: 250
        },
        buttons: {
            OK: function () {
                $(this).dialog("close");
            },
            Expandir: function () {
                $(this).height(getWindowHeight() - 297);
                $(this).dialog("widget").height(getWindowHeight() - 200);
                $(this).dialog("widget").width(getWindowWidth() - 200);
                $(this).dialog("widget").position({ my: 'center', at: 'center', of: window });
            },
            Contrair: function () {
                $(this).height(203);
                $(this).dialog("widget").height(300);
                $(this).dialog("widget").width(600);
                $(this).dialog("widget").position({ my: 'center', at: 'center', of: window });
            }
        }
    }).parent().addClass("ui-state-error");
    $('#link-sobre').click(function () {
        var div = $('#dialog-sobre');
        div.dialog(
        {
            autoOpen: false,
            modal: true,
            resizable: false,
            width: 400,
            height: 520,
            title: 'Sobre',
            buttons: [{
                text: 'Fechar',
                id: 'btn-sobre-fechar',
                click: function () {
                    $(this).dialog("close");
                }
            }],
            open: function () {
                $("#btn-sobre-fechar").button({ icons: { primary: "ui-icon-closethick" } });
            }
        });
        div.dialog('open');
        return false;
    });
}
function $$(id, context) {
    var el = $("#" + id, context);
    if (el.length < 1)
        el = $("[id$=_" + id + "]", context);
    return el;
}
function habilitarDesabilitarInput(txt, btn) {
    if ($.trim($(txt).val()) == "") {
        $(btn).attr('disabled', 'disabled').removeClass('ui-state-default').addClass('ui-state-disabled');
    }
    else {
        $(btn).removeAttr('disabled').removeClass('ui-state-disabled').addClass('ui-state-default');
    }
}
function execmascara() {
    v_obj.value = v_fun(v_obj.value);
}
function telefone(v) {
    v = v.replace(/\D/g, ""); //Remove tudo o que não é dígito
    v = v.replace(/^(\d\d)(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
    v = v.replace(/(\d{4})(\d)/, "$1-$2"); //Coloca hífen entre o quarto e o quinto dígitos
    return v;
}
function cpf(v) {
    v = v.replace(/\D/g, ""); //Remove tudo o que não é dígito
    v = v.replace(/(\d{3})(\d)/, "$1.$2"); //Coloca um ponto entre o terceiro e o quarto dígitos
    v = v.replace(/(\d{3})(\d)/, "$1.$2"); //Coloca um ponto entre o terceiro e o quarto dígitos
    //de novo (para o segundo bloco de números)
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2"); //Coloca um hífen entre o terceiro e o quarto dígitos
    return v;
}
function cep(v) {
    v = v.replace(/D/g, ""); //Remove tudo o que não é dígito
    //v = v.replace( /^(\d{2})(\d)/ , "$1.$2");
    v = v.replace(/(\d{5})(\d{1,2})$/, "$1-$2"); //Esse é tão fácil que não merece explicações
    return v;
}
function cnpj(v) {
    v = v.replace(/\D/g, ""); //Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/, "$1.$2"); //Coloca ponto entre o segundo e o terceiro dígitos
    v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3"); //Coloca ponto entre o quinto e o sexto dígitos
    v = v.replace(/\.(\d{3})(\d)/, ".$1/$2"); //Coloca uma barra entre o oitavo e o nono dígitos
    v = v.replace(/(\d{4})(\d)/, "$1-$2"); //Coloca um hífen depois do bloco de quatro dígitos
    return v;
}
function validarCNPJ(cnpj) {
    cnpj = cnpj.replace(/[^\d]+/g, '');//eliminamos todos os caracteres não númericos do CNPJ
    if (cnpj == '') return false;
    if (cnpj.length != 14) return false;
    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;
    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;
    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;
    return true;
}
/// TODO Pablo
function validarEmail(email) {
    var re = /[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}/igm;
    if (email == '' || !re.test(email)) {
        return false;
    }
    return true;
}
function validarCpf(numero) {
    var CPF, pos, i, soma, dv, dv_informado;
    var digito = new Array(10);// Cria array para armazenar os valores
    if (numero.length > 0) {
        CPF = numero.replace(/[.-]/gi, "");// Retira os '.' e '-' do número
        dv_informado = CPF.substr(9, 2);// Armazena os dois últimos dígito do CPF
        // Elimina CPFs invalidos conhecidos
        if (CPF == "00000000000" ||
            CPF == "11111111111" ||
            CPF == "22222222222" ||
            CPF == "33333333333" ||
            CPF == "44444444444" ||
            CPF == "55555555555" ||
            CPF == "66666666666" ||
            CPF == "77777777777" ||
            CPF == "88888888888" ||
            CPF == "99999999999")
            return false;
        /* Desmembra o número do CPF na array digito */
        for (i = 0; i <= 8; i++) {
            digito[i] = CPF.substr(i, 1);
        }
        /* Calcula o valor do 10° dígito da verificação */
        posicao = 10;
        soma = 0;
        for (i = 0; i <= 8; i++) {
            soma += digito[i] * posicao;
            posicao = posicao - 1;
        }
        digito[9] = soma % 11;
        if (digito[9] < 2) {
            digito[9] = 0;
        } else {
            digito[9] = 11 - digito[9];
        }
        /* Calcula o valor do 11° dígito da verificação */
        posicao = 11;
        soma = 0;
        for (i = 0; i <= 9; i++) {
            soma += digito[i] * posicao;
            posicao = posicao - 1;
        }
        digito[10] = soma % 11;
        if (digito[10] < 2) {
            digito[10] = 0;
        } else {
            digito[10] = 11 - digito[10];
        }
        /* Verifica se os dígitos verificadores conferem */
        dv = digito[9] * 10 + digito[10];
        if (dv != dv_informado) {
            return false;
        } else {
            return true;
        }
    } else {
        return false;
    }
}
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
function getHojeMenos(hoje) { return (hoje.getDate() < 10 ? '0' + hoje.getDate() : hoje.getDate()) + '/' + ((hoje.getMonth() + 1) < 10 ? '0' + (hoje.getMonth() + 1) : (hoje.getMonth() + 1)) + '/' + hoje.getFullYear(); }
function getHoje() { var hoje = new Date(); return (hoje.getDate() < 10 ? '0' + hoje.getDate() : hoje.getDate()) + '/' + ((hoje.getMonth() + 1) < 10 ? '0' + (hoje.getMonth() + 1) : (hoje.getMonth() + 1)) + '/' + hoje.getFullYear(); }
function getFormattedDate(date) { var hoje = date; return (hoje.getDate() < 10 ? '0' + hoje.getDate() : hoje.getDate()) + '/' + ((hoje.getMonth() + 1) < 10 ? '0' + (hoje.getMonth() + 1) : (hoje.getMonth() + 1)) + '/' + hoje.getFullYear(); }
function getAgora() { var hoje = new Date(); return (hoje.getHours() < 10 ? '0' + hoje.getHours() : hoje.getHours()) + ':' + ((hoje.getMinutes() + 1) < 10 ? '0' + (hoje.getMinutes() + 1) : (hoje.getMinutes() + 1)) + ':' + ((hoje.getSeconds() + 1) < 10 ? '0' + (hoje.getSeconds() + 1) : (hoje.getSeconds() + 1)); }
function validInteger(el) {
    var intRegex = /^[0-9]{1,10}$/;
    if ($(el).val() == "" || !intRegex.test($(el).val()) || $(el).val() > 2147483647) {
        return false;
    }
    else {
        return true;
    }
}
function validHour(el) {
    var hourRegex = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/;
    if ($(el).val() == "" || !hourRegex.test($(el).val())) {
        return false;
    }
    else {
        return true;
    }
}
function validDate(el) {
    var dateRegex = /^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;
    if ($(el).val() == "" || !dateRegex.test($(el).val())) {
        return false;
    }
    else {
        return true;
    }
}
function checkIfJsonObject(toTestObject) {
    var result = false;
    if (typeof toTestObject === "string") {
        if (!typeof toTestObject !== "object") {
            result = true;
        }
    }
    return result;
}
function ucwords(str) {
    return (str + '').replace(/^([a-z])|\s+([a-z])/g, function ($1) {
        return $1.toUpperCase();
    });
}
function setPageName(name) {
    GetElemByClientID("lblPageName", $('.subHeader')).text(name);
}
function getPageName() {
    return GetElemByClientID("lblPageName", $('.subHeader')).text();
}
function setMenuHover(id) {
    $('.navPanel a').eq(id).css('background-color', '#FFE88C');
    $('.navPanel a').eq(id).css('border', 'solid 1px #D69C00');
    $('.navPanel a').eq(id).css('font-weight', 'bold');
}
function setNavigationName(id) {
    switch (id) {
        case '0':
            $('#histNavBarMain').text('GERENCIAMENTO DE ROBÔS');
            break;
        case '1':
            $('#histNavBarMain').text('ADMINISTRAÇÃO DO CORREIO');
            break;
        case '2':
            $('#histNavBarMain').text('LOG DO SISTEMA');
            break;
        case '3':
            $('#histNavBarMain').text('GERENCIAMENTO DE OFÍCIOS');
            break;
        case '4':
            $('#histNavBarMain').text('MENSAGENS');
            break;
        default:
            break;
    }
}
function setGrandMenuId(id) {
    GetElemByClientID("hdfIdGrandMenu", $('#hiddenFields')).val(id);
    return true;
}
function expandOrCollapseConteudoPage(obj_dom) {
    if (obj_dom.is('.conteudo_collapsed')) {
        obj_dom.removeClass('conteudo_collapsed').addClass('conteudo_expanded');
    }
    else {
        obj_dom.removeClass('conteudo_expanded').addClass('conteudo_collapsed');
    }
}
function GetClientID(id, context) {
    var el = $("#" + id, context);
    if (el.length < 1) { el = $("[id$=_" + id + "]", context); }
    return el.attr('id');
}
function GetElemByClientID(id, context) {
    var el = $("#" + id, context);
    if (el.length < 1) { el = $("[id$=_" + id + "]", context); }
    return el;
}
function GetElemByName(name, context) {
    var el = $("input[name$=" + name + "]", context);
    if (el.length < 1) { el = $("[name$=_" + name + "]", context); }
    return el;
}
function GetSelectedText(id, context) {
    return $("#" + GetClientID(id, context) + " option:selected").text();
}
function configMenu() {
    if (GetElemByClientID("hdfIdGrandMenu", $('#hiddenFields')).val() == "") {
        GetElemByClientID("hdfIdGrandMenu", $('#hiddenFields')).val(0);
    }
    var idGrandMenu = GetElemByClientID("hdfIdGrandMenu", $('#hiddenFields')).val();
    var bgImage = $('.navSelect a').eq(idGrandMenu).css('background-image');
    if (bgImage != undefined) {
        $('.navSelect a').eq(idGrandMenu).css('background-image', bgImage.replace('nav_link', 'nav_link_hover'));
    }
    return;
    if (idGrandMenu == 3) {
        for (var i = 3; i <= 6; i++) {
            $('.navPanel a').eq(i).css('margin-left', '20px');
            $('.navPanel a').eq(i).wrap('<div class="cons" />');
        }
        $('.navPanel a').eq(8).css('margin-left', '20px');
        $('.navPanel a').eq(8).wrap('<div class="relat" />');
        for (var i = 10; i <= 15; i++) {
            $('.navPanel a').eq(i).css('margin-left', '20px');
            $('.navPanel a').eq(i).wrap('<div class="config" />');
        }
        for (var i = 17; i <= 21; i++) {
            $('.navPanel a').eq(i).css('margin-left', '20px');
            $('.navPanel a').eq(i).wrap('<div class="util" />');
        }
        for (var i = 22; i <= 35; i++) {
            $('.navPanel a').eq(i).css('margin-left', '40px');
            $('.navPanel a').eq(i).wrap('<div class="cad" />');
        }
        $('.navPanel a').eq(2).click(function () {
            $('.cons').slideToggle(0);
            GetElemByClientID("hdfIdSubMenuCons", $('#hiddenFields')).val('true');
        });
        $('.navPanel a').eq(7).click(function () {
            $('.relat').slideToggle(0);
            GetElemByClientID("hdfIdSubMenuRelat", $('#hiddenFields')).val('true');
        });
        $('.navPanel a').eq(9).click(function () {
            $('.config').slideToggle(0);
            GetElemByClientID("hdfIdSubMenuConfig", $('#hiddenFields')).val('true');
        });
        $('.navPanel a').eq(16).click(function () {
            $('.util').slideToggle(0);
            GetElemByClientID("hdfIdSubMenuUtil", $('#hiddenFields')).val('true');
        });
        $('.navPanel a').eq(21).click(function () {
            $('.cad').slideToggle(0);
            GetElemByClientID("hdfIdSubMenuCad", $('#hiddenFields')).val('true');
        });
        if (GetElemByClientID("hdfIdSubMenuCons", $('#hiddenFields')).val() == "") { GetElemByClientID("hdfIdSubMenuCons", $('#hiddenFields')).val('false'); }
        if (GetElemByClientID("hdfIdSubMenuRelat", $('#hiddenFields')).val() == "") { GetElemByClientID("hdfIdSubMenuRelat", $('#hiddenFields')).val('false'); }
        if (GetElemByClientID("hdfIdSubMenuConfig", $('#hiddenFields')).val() == "") { GetElemByClientID("hdfIdSubMenuConfig", $('#hiddenFields')).val('false'); }
        if (GetElemByClientID("hdfIdSubMenuUtil", $('#hiddenFields')).val() == "") { GetElemByClientID("hdfIdSubMenuUtil", $('#hiddenFields')).val('false'); }
        if (GetElemByClientID("hdfIdSubMenuCad", $('#hiddenFields')).val() == "") { GetElemByClientID("hdfIdSubMenuCad", $('#hiddenFields')).val('false'); }
        if (GetElemByClientID("hdfIdSubMenuCons", $('#hiddenFields')).val() == 'false') { $('.cons').slideToggle(0); }
        if (GetElemByClientID("hdfIdSubMenuRelat", $('#hiddenFields')).val() == 'false') { $('.relat').slideToggle(0); }
        if (GetElemByClientID("hdfIdSubMenuConfig", $('#hiddenFields')).val() == 'false') { $('.config').slideToggle(0); }
        if (GetElemByClientID("hdfIdSubMenuUtil", $('#hiddenFields')).val() == 'false') {
            $('.util').slideToggle(0);
            $('.cad').slideToggle(0);
        }
        else {
            if (GetElemByClientID("hdfIdSubMenuCad", $('#hiddenFields')).val() == 'false') {
                $('.cad').slideToggle(0);
            }
        }
    }
}
function showSuccess(text) {
    $('.growlUI h3').text(text);
    showDataSaved();
}
function showInfo(text) {
    $('.growlUIInfo h3').text(text);
    showDataInfo();
}
function showError(text) {
    $('.growlUIErr h3').text(text);
    showDataErrorOnSave();
}
function showDialogError() {
    $('#error-text-1').text(GetElemByClientID("hdfText1").val());
    $('#error-text-2').text(GetElemByClientID("hdfText2").val());
    $("#dialog-message").dialog("open");
}
function showDataSaved() {
    $.blockUI({
        message: $('div.growlUI'),
        fadeIn: 700,
        fadeOut: 700,
        timeout: 4000,
        showOverlay: false,
        centerY: false,
        css: {
            width: '350px',
            top: '10px',
            left: '',
            right: '10px',
            border: 'none',
            padding: '5px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .6,
            color: '#fff'
        }
    });
}
function showDataInfo() {
    $.blockUI({
        message: $('div.growlUIInfo'),
        fadeIn: 700,
        fadeOut: 700,
        timeout: 4000,
        showOverlay: false,
        centerY: false,
        css: {
            width: '350px',
            top: '10px',
            left: '',
            right: '10px',
            border: 'none',
            padding: '5px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .6,
            color: '#fff'
        }
    });
}
function showDataErrorOnSave() {
    $.blockUI({
        message: $('div.growlUIErr'),
        fadeIn: 700,
        fadeOut: 700,
        timeout: 4000,
        showOverlay: false,
        centerY: false,
        css: {
            width: '350px',
            top: '10px',
            left: '',
            right: '10px',
            border: 'none',
            padding: '5px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .6,
            color: '#fff'
        }
    });
}

function setActionBar() {
    //    GetElemByClientID("lkbSalvar", $('.actionButton')).click(function () {
    //        $.blockUI({
    //            message: $('div.growlUI'),
    //            fadeIn: 700,
    //            fadeOut: 700,
    //            timeout: 4000,
    //            showOverlay: false,
    //            centerY: false,
    //            css: {
    //                width: '350px',
    //                top: '10px',
    //                left: '',
    //                right: '10px',
    //                border: 'none',
    //                padding: '5px',
    //                backgroundColor: '#000',
    //                '-webkit-border-radius': '10px',
    //                '-moz-border-radius': '10px',
    //                opacity: .6,
    //                color: '#fff'
    //            }
    //        });
    //        return false;
    //    });

    //    GetElemByClientID("lkbErro", $('.actionButton')).click(function () {
    //        $.blockUI({
    //            message: $('div.growlUIErr'),
    //            fadeIn: 700,
    //            fadeOut: 700,
    //            timeout: 4000,
    //            showOverlay: false,
    //            centerY: false,
    //            css: {
    //                width: '350px',
    //                top: '10px',
    //                left: '',
    //                right: '10px',
    //                border: 'none',
    //                padding: '5px',
    //                backgroundColor: '#000',
    //                '-webkit-border-radius': '10px',
    //                '-moz-border-radius': '10px',
    //                opacity: .6,
    //                color: '#fff'
    //            }
    //        });
    //        return false;
    //    });
}
function actionBarAlterarClickBase() {
    $(this).attr('disabled', 'disabled');
    var $imgA = GetElemByClientID('imgAlterar');
    $imgA.attr('disabled', 'disabled');
    if ($imgA.attr('src').indexOf('24-pb') === -1) {
        $imgA.attr('src', $imgA.attr('src').replace('24', '24-pb'));
    }
    var $lblA = GetElemByClientID('lblAlterar');
    $lblA.attr('disabled', 'disabled');
    $lblA.css('color', 'gray');
    var $lkbS = GetElemByClientID('lkbSalvar');
    $lkbS.removeAttr('disabled');
    var $imgS = GetElemByClientID('imgSalvar');
    $imgS.removeAttr('disabled');
    if ($imgS.attr('src').indexOf('45-pb') !== -1) {
        $imgS.attr('src', $imgS.attr('src').replace('45-pb', '45'));
    }
    var $lblS = GetElemByClientID('lblSalvar');
    $lblS.removeAttr('disabled');
    $lblS.css('color', '#10428C');
    var $lkbC = GetElemByClientID('lkbCancelar');
    $lkbC.removeAttr('disabled');
    var $imgC = GetElemByClientID('imgCancelar');
    $imgC.removeAttr('disabled');
    if ($imgC.attr('src').indexOf('cancel-pb') !== -1) {
        $imgC.attr('src', $imgC.attr('src').replace('cancel-pb', 'cancel'));
    }
    var $lblC = GetElemByClientID('lblCancelar');
    $lblC.removeAttr('disabled');
    $lblC.css('color', '#10428C');
}
function setInitialStateControlsBase() {
    var $lkbA = GetElemByClientID('lkbAlterar');
    $lkbA.removeAttr('disabled');
    var $imgA = GetElemByClientID('imgAlterar');
    $imgA.removeAttr('disabled');
    if ($imgA.attr('src').indexOf('24-pb') !== -1) {
        $imgA.attr('src', $imgA.attr('src').replace('24-pb', '24'));
    }
    var $lblA = GetElemByClientID('lblAlterar');
    $lblA.removeAttr('disabled');
    $lblA.css('color', '#10428C');
    var $lkbS = GetElemByClientID('lkbSalvar');
    $lkbS.attr('disabled', 'disabled');
    var $imgS = GetElemByClientID('imgSalvar');
    if ($imgS.attr('src').indexOf('45-pb') === -1) {
        $imgS.attr('src', $imgS.attr('src').replace('45', '45-pb'));
    }
    $imgS.attr('disabled', 'disabled');
    var $lblS = GetElemByClientID('lblSalvar');
    $lblS.css('color', 'gray');
    $lblS.attr('disabled', 'disabled');
    var $lkbC = GetElemByClientID('lkbCancelar');
    $lkbC.attr('disabled', 'disabled');
    var $imgC = GetElemByClientID('imgCancelar');
    if ($imgC.attr('src').indexOf('cancel-pb') === -1) {
        $imgC.attr('src', $imgC.attr('src').replace('cancel', 'cancel-pb'));
    }
    $imgC.attr('disabled', 'disabled');
    var $lblC = GetElemByClientID('lblCancelar');
    $lblC.css('color', 'gray');
    $lblC.attr('disabled', 'disabled');
}
function exibirNotificacoes() {
    // insert
    if (GetElemByClientID("hdfInsert").val() == "1") {
        setTimeout('showSuccess("Dado(s) inserido(s) com sucesso!");', 1000);
        setTimeout("GetElemByClientID('hdfInsert').val('');", 5000);
    }
    // update
    if (GetElemByClientID("hdfUpdate").val() == "1") {
        setTimeout('showSuccess("Dado(s) atualizado(s) com sucesso!");', 1000);
        setTimeout("GetElemByClientID('hdfUpdate').val('');", 5000);
    }
    // delete
    if (GetElemByClientID("hdfDelete").val() == "1") {
        setTimeout('showSuccess("Dado(s) excluído(s) com sucesso!");', 1000);
        setTimeout("GetElemByClientID('hdfDelete').val('');", 5000);
    }
    // info
    if (GetElemByClientID("hdfInfo").val() == "1") {
        setTimeout('showInfo("' + GetElemByClientID("hdfText").val() + '");', 1000);
        setTimeout("GetElemByClientID('hdfInfo').val('');", 5000);
        setTimeout("GetElemByClientID('hdfText').val('');", 6000);
    }
    // error
    if (GetElemByClientID("hdfError").val() == "1") {
        setTimeout('showDialogError();', 1000);
        setTimeout("GetElemByClientID('hdfError').val('');", 5000);
        setTimeout("GetElemByClientID('hdfText1').val('');", 6000);
        setTimeout("GetElemByClientID('hdfText2').val('');", 7000);
    }
}
function afterSubmitUpdate(response, postdata) {
    if (response.statusText == "ERROR") {
        GetElemByClientID('hdfError').val('1');
        GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o processamento:');
        GetElemByClientID('hdfText2').val(response.responseText);
    }
    else {
        GetElemByClientID('hdfUpdate').val('1');
    }
    exibirNotificacoes();
}
function afterSubmitInsert(response, postdata) {
    if (response.statusText == "ERROR") {
        GetElemByClientID('hdfError').val('1');
        GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o processamento:');
        GetElemByClientID('hdfText2').val(response.responseText);
    }
    else {
        GetElemByClientID('hdfInsert').val('1');
    }
    exibirNotificacoes();
}
function afterSubmitDelete(response, postdata) {
    if (response.statusText == "ERROR") {
        GetElemByClientID('hdfError').val('1');
        GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o processamento:');
        GetElemByClientID('hdfText2').val(response.responseText);
    }
    else {
        GetElemByClientID('hdfDelete').val('1');
    }
    exibirNotificacoes();
}