function validarTxtDe() {
    var $txtDe = GetElemByClientID('txtDe');
    if (!validDate($txtDe)) {
        $txtDe.tipsy({ fade: true, gravity: 'sw' });
        $txtDe.css('border-color', 'red').focus().tipsy('show');
        return false;
    }
    else {
        $txtDe.css('border-color', '').tipsy('hide');
    }

    return true;
}
function validarTxtAte() {
    var $txtAte = GetElemByClientID('txtAte');
    if (!validDate($txtAte)) {
        $txtAte.tipsy({ fade: true, gravity: 'sw' });
        $txtAte.css('border-color', 'red').focus().tipsy('show');
        return false;
    }
    else {
        $txtAte.css('border-color', '').tipsy('hide');
    }

    return true;
}
function validarTxtDeTxtAte() {
    var dataDe = GetElemByClientID('txtDe').val();
    var dataAte = GetElemByClientID('txtAte').val();
    var $txtDe = GetElemByClientID('txtDe');
    if (dataDe > dataAte) {
        $txtDe.attr('title', 'A data inicial deve ser inferior à data final');
        $txtDe.tipsy({ fade: true, gravity: 'sw' });
        $txtDe.css('border-color', 'red').focus().tipsy('show');
        return false;
    }
    else {
        $txtDe.css('border-color', '').tipsy('hide');
    }

    return true;
}
function configClickbtnImprimir() {
    GetElemByClientID('btnImprimir').click(function () {
        if (!validarTxtDe()) {
            return false;
        }
        if (!validarTxtAte()) {
            return false;
        }
        if (!validarTxtDeTxtAte()) {
            return false;
        }

        return true;
    });
}
function carregarDdlSistema() {
    ddlSistema = GetElemByClientID('ddlSistema')[0];
    ddlSistema.options.length = 0;

    PageMethods.ObterSistemas(
        ObterSistemasSuccess,
        ObterSistemasError
    );
}
function ObterSistemasSuccess(response) {
    ddlSistema.options.length = 0;
    addOption("ddlSistema", "Todos", "-1");
    addOption("ddlAplicativo", "Todos", "-1");
    for (var i in response) {
        addOption("ddlSistema", response[i].DsSistema, response[i].IdSistema);
    }

    configLoad();
}
function ObterSistemasError(err) {
    var error_msg = err.get_message();
    GetElemByClientID('hdfError').val('1');
    GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o processamento: ObterSistemas');
    GetElemByClientID('hdfText2').val(error_msg);
    exibirNotificacoes();
}
function carregarDdlAplicativo() {
    ddlAplicativo = GetElemByClientID('ddlAplicativo')[0];
    ddlAplicativo.options.length = 0;

    PageMethods.ObterAplicativos(
        GetElemByClientID('hdfIdSistema').val(),
        ObterAplicativosSuccess,
        ObterAplicativosError
    );
}
function ObterAplicativosSuccess(response) {
    ddlAplicativo.options.length = 0;
    addOption("ddlAplicativo", "Todos", "-1");
    for (var i in response) {
        addOption("ddlAplicativo", response[i].DsAplicativo, response[i].IdAplicativo);
    }
    if (GetElemByClientID('hdfIdAplicativo').val() != "-1") {
        setTimeout("GetElemByClientID('ddlAplicativo').val(GetElemByClientID('hdfIdAplicativo').val());", 100);
    }
}
function ObterAplicativosError(err) {
    var error_msg = err.get_message();
    GetElemByClientID('hdfError').val('1');
    GetElemByClientID('hdfText1').val('Alguns erros ocorreram durante o processamento: ObterAplicativos');
    GetElemByClientID('hdfText2').val(error_msg);
    exibirNotificacoes();
}
function configChangeLog() {
    GetElemByClientID('ddlSistema').change(function () {
        //correção da carga de TODOS na mudança de sistemas
        if (GetElemByClientID('hdfIdSistema').val() != GetElemByClientID('ddlSistema').val()) {
            GetElemByClientID('hdfIdAplicativo').val(-1);
        }

        GetElemByClientID('hdfIdSistema').val(GetElemByClientID('ddlSistema').val());
        if (GetElemByClientID('ddlSistema').val() != -1) {
            carregarDdlAplicativo();
        }
        else {
            ddlAplicativo.options.length = 0;
            addOption("ddlAplicativo", "Todos", "-1");
            GetElemByClientID('hdfIdAplicativo').val(-1);
        }
    });
    GetElemByClientID('ddlAplicativo').change(function () {
        GetElemByClientID('hdfIdAplicativo').val(GetElemByClientID('ddlAplicativo').val());
    });
}
function configLoad() {
    if (GetElemByClientID('hdfIdSistema').val() != "-1") {
        setTimeout("GetElemByClientID('ddlSistema').val(GetElemByClientID('hdfIdSistema').val());", 50);
        setTimeout("carregarDdlAplicativo();", 60);        
    }    
}
