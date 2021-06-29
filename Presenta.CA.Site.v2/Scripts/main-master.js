function configDDLConglomerado() {
    if (GetElemByClientID('hdfUsesDDLConglomerado').val() == '1') {
        GetElemByClientID('div-conglomerado').css('display', 'block');
        GetElemByClientID('ddlConglomerados').change(function () {
            setHdfConglomerado();
        });
    } else {
        GetElemByClientID('div-conglomerado').css('display', 'none');
        GetElemByClientID('hdfConglomerado').val('');
    }
}
function setHdfConglomerado() {
    if (GetElemByClientID('hdfUsesDDLConglomerado').val() == '1') {
        GetElemByClientID('hdfConglomerado').val(GetElemByClientID('ddlConglomerados').val());
        if (typeof (loadIlc) === "function") { loadIlc(); }
    }
}