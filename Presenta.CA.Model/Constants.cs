using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presenta.CA.Model
{
    public static class Constants
    {
        #region Keys
        public const string KeyConnectionStringName = "ca";
        public const string SessionFuncKey = "func";
        public const string SessionGrandMenuKey = "selected_grand_menu";
        public const string SessionConglomeradoKey = "conglomerado";
        public const string SessionConglInstKey = "congl_inst";
        public const string SessionIdUsuario = "id_users_ca";
        public const string SessionNmUsuario = "nm_users_ca";
        public const string SessionLgUsuario = "lg_users";
        public const string NomeAplicativo = "CA_SITE";
        public const string KeyIdAplicativo = "id-aplicativo";
        public const string KeyTipoAutenticacao = "tipo-autenticacao";
        public const string KeySiteMinder = "site-minder";
        public const string KeyTimeoutInterface = "timeout-interface";

        public const string OwnerSchemaProc = "owner-schema-proc";
        public const string OwnerSchemaData = "owner-schema-data";
        public const string OwnerSchemaView = "owner-schema-view";
        #endregion

        #region Oracle
        public const string OracleDataAccessClient = "Oracle.DataAccess.Client";
        #endregion

        #region Stored Procedures CA
        public const string StpCaDel_calog = "STPCADEL_calog";
        public const string StpCaIns_calog = "STPCAINS_calog";
        public const string StpCaLst_calog = "STPCALST_calog";
        public const string StpCaSel_calog = "STPCASEL_calog";
        public const string StpCaUpd_calog = "STPCAUPD_calog";

        public const string StpCaDel_calog_detalhe = "STPCADEL_calog_detalhe";
        public const string StpCaIns_calog_detalhe = "STPCAINS_calog_detalhe";
        public const string StpCaLst_calog_detalhe = "STPCALST_calog_detalhe";
        public const string StpCaSel_calog_detalhe = "STPCASEL_calog_detalhe";
        public const string StpCaUpd_calog_detalhe = "STPCAUPD_calog_detalhe";

        public const string StpCaDel_casistema = "STPCADEL_casistema";
        public const string StpCaIns_casistema = "STPCAINS_casistema";
        public const string StpCaLst_casistema = "STPCALST_casistema";
        public const string StpCaSel_casistema = "STPCASEL_casistema";
        public const string StpCaUpd_casistema = "STPCAUPD_casistema";

        public const string StpCaLst_casistema_por_perfil = "STPCALST_casistema_por_perfil";

        public const string StpCaDel_casituacao_funcionalidade_perfil = "STPCADEL_casituacao_funcionalidade_perfil";
        public const string StpCaIns_casituacao_funcionalidade_perfil = "STPCAINS_casituacao_funcionalidade_perfil";
        public const string StpCaLst_casituacao_funcionalidade_perfil = "STPCALST_casituacao_funcionalidade_perfil";
        public const string StpCaSel_casituacao_funcionalidade_perfil = "STPCASEL_casituacao_funcionalidade_perfil";
        public const string StpCaUpd_casituacao_funcionalidade_perfil = "STPCAUPD_casituacao_funcionalidade_perfil";

        public const string StpCaDel_casituacao_funcionalidade_perfil_Oracle = "STPCADEL_casitfuncperfil";
        public const string StpCaIns_casituacao_funcionalidade_perfil_Oracle = "STPCAINS_casitfuncperfil";
        public const string StpCaLst_casituacao_funcionalidade_perfil_Oracle = "STPCALST_casitfuncperfil";
        public const string StpCaSel_casituacao_funcionalidade_perfil_Oracle = "STPCASEL_casitfuncperfil";
        public const string StpCaUpd_casituacao_funcionalidade_perfil_Oracle = "STPCAUPD_casitfuncperfil";
        
        public const string StpCaDel_casituacao_perfil = "STPCADEL_casituacao_perfil";
        public const string StpCaIns_casituacao_perfil = "STPCAINS_casituacao_perfil";
        public const string StpCaLst_casituacao_perfil = "STPCALST_casituacao_perfil";
        public const string StpCaSel_casituacao_perfil = "STPCASEL_casituacao_perfil";
        public const string StpCaUpd_casituacao_perfil = "STPCAUPD_casituacao_perfil";

        public const string StpCaDel_casituacao_operador = "STPCADEL_casituacao_operador";
        public const string StpCaIns_casituacao_operador = "STPCAINS_casituacao_operador";
        public const string StpCaLst_casituacao_operador = "STPCALST_casituacao_operador";
        public const string StpCaSel_casituacao_operador = "STPCASEL_casituacao_operador";
        public const string StpCaUpd_casituacao_operador = "STPCAUPD_casituacao_operador";

        public const string StpCaDel_cahistoricosenha = "STPCADEL_cahistoricosenha";
        public const string StpCaIns_cahistoricosenha = "STPCAINS_cahistoricosenha";
        public const string StpCaLst_cahistoricosenha = "STPCALST_cahistoricosenha";
        public const string StpCaSel_cahistoricosenha = "STPCASEL_cahistoricosenha";
        public const string StpCaUpd_cahistoricosenha = "STPCAUPD_cahistoricosenha";
 
        public const string StpCaDel_catiposenha = "STPCADEL_catiposenha";
        public const string StpCaIns_catiposenha = "STPCAINS_catiposenha";
        public const string StpCaLst_catiposenha = "STPCALST_catiposenha";
        public const string StpCaSel_catiposenha = "STPCASEL_catiposenha";
        public const string StpCaUpd_catiposenha = "STPCAUPD_catiposenha";

        public const string StpCaDel_catiposenhavalidacao = "STPCADEL_catiposenhavalidacao";
        public const string StpCaIns_catiposenhavalidacao = "STPCAINS_catiposenhavalidacao";
        public const string StpCaLst_catiposenhavalidacao = "STPCALST_catiposenhavalidacao";
        public const string StpCaSel_catiposenhavalidacao = "STPCASEL_catiposenhavalidacao";
        public const string StpCaUpd_catiposenhavalidacao = "STPCAUPD_catiposenhavalidacao";
        
        public const string StpCaDel_catiposenha_tiposenhavalidacao = "STPCADEL_catiposenha_tiposenhavalidacao";
        public const string StpCaIns_catiposenha_tiposenhavalidacao = "STPCAINS_catiposenha_tiposenhavalidacao";
        public const string StpCaLst_catiposenha_tiposenhavalidacao = "STPCALST_catiposenha_tiposenhavalidacao";
        public const string StpCaSel_catiposenha_tiposenhavalidacao = "STPCASEL_catiposenha_tiposenhavalidacao";
        public const string StpCaUpd_catiposenha_tiposenhavalidacao = "STPCAUPD_catiposenha_tiposenhavalidacao";

        public const string StpCaDel_catiposenha_tiposenhavalidacao_Oracle = "STPCADEL_tposenhatpsenhavalid";
        public const string StpCaIns_catiposenha_tiposenhavalidacao_Oracle = "STPCAINS_tposenhatpsenhavalid";
        public const string StpCaLst_catiposenha_tiposenhavalidacao_Oracle = "STPCALST_tposenhatpsenhavalid";
        public const string StpCaSel_catiposenha_tiposenhavalidacao_Oracle = "STPCASEL_tposenhatpsenhavalid";
        public const string StpCaUpd_catiposenha_tiposenhavalidacao_Oracle = "STPCAUPD_tposenhatpsenhavalid";

        public const string StpCaDel_casituacao_funcionalidade = "STPCADEL_casituacao_funcionalidade";
        public const string StpCaIns_casituacao_funcionalidade = "STPCAINS_casituacao_funcionalidade";
        public const string StpCaLst_casituacao_funcionalidade = "STPCALST_casituacao_funcionalidade";
        public const string StpCaSel_casituacao_funcionalidade = "STPCASEL_casituacao_funcionalidade";
        public const string StpCaUpd_casituacao_funcionalidade = "STPCAUPD_casituacao_funcionalidade";

        public const string StpCaDel_casituacao_funcionalidade_Oracle = "STPCADEL_casituacao_func";
        public const string StpCaIns_casituacao_funcionalidade_Oracle = "STPCAINS_casituacao_func";
        public const string StpCaLst_casituacao_funcionalidade_Oracle = "STPCALST_casituacao_func";
        public const string StpCaSel_casituacao_funcionalidade_Oracle = "STPCASEL_casituacao_func";
        public const string StpCaUpd_casituacao_funcionalidade_Oracle = "STPCAUPD_casituacao_func";

        public const string StpCaDel_cafuncionalidade = "STPCADEL_cafuncionalidade";
        public const string StpCaIns_cafuncionalidade = "STPCAINS_cafuncionalidade";
        public const string StpCaLst_cafuncionalidade = "STPCALST_cafuncionalidade";
        public const string StpCaSel_cafuncionalidade = "STPCASEL_cafuncionalidade";
        public const string StpCaUpd_cafuncionalidade = "STPCAUPD_cafuncionalidade";

        public const string StpCaDel_caperfil = "STPCADEL_caperfil";
        public const string StpCaIns_caperfil = "STPCAINS_caperfil";
        public const string StpCaLst_caperfil = "STPCALST_caperfil";
        public const string StpCaSel_caperfil = "STPCASEL_caperfil";
        public const string StpCaUpd_caperfil = "STPCAUPD_caperfil";
        public const string StpCaLst_perfil_por_func = "STPCALST_perfil_por_func";

        public const string StpCaDel_casituacao_perfil_operador = "STPCADEL_casituacao_perfil_operador";
        public const string StpCaIns_casituacao_perfil_operador = "STPCAINS_casituacao_perfil_operador";
        public const string StpCaLst_casituacao_perfil_operador = "STPCALST_casituacao_perfil_operador";
        public const string StpCaSel_casituacao_perfil_operador = "STPCASEL_casituacao_perfil_operador";
        public const string StpCaUpd_casituacao_perfil_operador = "STPCAUPD_casituacao_perfil_operador";

        public const string StpCaDel_casituacao_perfil_operador_Oracle = "STPCADEL_casitperfiloper";
        public const string StpCaIns_casituacao_perfil_operador_Oracle = "STPCAINS_casitperfiloper";
        public const string StpCaLst_casituacao_perfil_operador_Oracle = "STPCALST_casitperfiloper";
        public const string StpCaSel_casituacao_perfil_operador_Oracle = "STPCASEL_casitperfiloper";
        public const string StpCaUpd_casituacao_perfil_operador_Oracle = "STPCAUPD_casitperfiloper";

        public const string StpCaDel_caoperador = "STPCADEL_caoperador";
        public const string StpCaIns_caoperador = "STPCAINS_caoperador";
        public const string StpCaLst_caoperador = "STPCALST_caoperador";
        public const string StpCaSel_caoperador = "STPCASEL_caoperador";
        public const string StpCaUpd_caoperador = "STPCAUPD_caoperador";
        public const string StpCaLst_operador_por_perfil = "STPCALST_operador_por_perfil";
        public const string StpCaNgc_Reset_caoperador = "STPCANGC_RESET_caoperador";

        public const string StpCaDel_cafuncionalidade_executada = "STPCADEL_cafuncionalidade_executada";
        public const string StpCaIns_cafuncionalidade_executada = "STPCAINS_cafuncionalidade_executada";
        public const string StpCaLst_cafuncionalidade_executada = "STPCALST_cafuncionalidade_executada";
        public const string StpCaSel_cafuncionalidade_executada = "STPCASEL_cafuncionalidade_executada";
        public const string StpCaUpd_cafuncionalidade_executada = "STPCAUPD_cafuncionalidade_executada";

        public const string StpCaDel_cafuncionalidade_executada_Oracle = "STPCADEL_cafunc_exec";
        public const string StpCaIns_cafuncionalidade_executada_Oracle = "STPCAINS_cafunc_exec";
        public const string StpCaLst_cafuncionalidade_executada_Oracle = "STPCALST_cafunc_exec";
        public const string StpCaSel_cafuncionalidade_executada_Oracle = "STPCASEL_cafunc_exec";
        public const string StpCaUpd_cafuncionalidade_executada_Oracle = "STPCAUPD_cafunc_exec";

        public const string StpCaDel_caperfil_operador = "STPCADEL_caperfil_operador";
        public const string StpCaIns_caperfil_operador = "STPCAINS_caperfil_operador";
        public const string StpCaLst_caperfil_operador = "STPCALST_caperfil_operador";
        public const string StpCaSel_caperfil_operador = "STPCASEL_caperfil_operador";
        public const string StpCaUpd_caperfil_operador = "STPCAUPD_caperfil_operador";

        public const string StpCaDel_cafuncionalidade_perfil = "STPCADEL_cafuncionalidade_perfil";
        public const string StpCaIns_cafuncionalidade_perfil = "STPCAINS_cafuncionalidade_perfil";
        public const string StpCaLst_cafuncionalidade_perfil = "STPCALST_cafuncionalidade_perfil";
        public const string StpCaSel_cafuncionalidade_perfil = "STPCASEL_cafuncionalidade_perfil";
        public const string StpCaUpd_cafuncionalidade_perfil = "STPCAUPD_cafuncionalidade_perfil";
        public const string StpCaLst_cafuncionalidade_por_perfil = "STPCALST_cafuncionalidade_por_perfil";

        public const string StpCaDel_cafuncionalidade_perfil_Oracle = "STPCADEL_cafunc_perfil";
        public const string StpCaIns_cafuncionalidade_perfil_Oracle = "STPCAINS_cafunc_perfil";
        public const string StpCaLst_cafuncionalidade_perfil_Oracle = "STPCALST_cafunc_perfil";
        public const string StpCaSel_cafuncionalidade_perfil_Oracle = "STPCASEL_cafunc_perfil";
        public const string StpCaUpd_cafuncionalidade_perfil_Oracle = "STPCAUPD_cafunc_perfil";
        public const string StpCaLst_cafuncionalidade_por_perfil_Oracle = "STPCALST_cafunc_por_perfil";

        public const string StpCaDel_caaplicativo = "STPCADEL_caaplicativo";
        public const string StpCaIns_caaplicativo = "STPCAINS_caaplicativo";
        public const string StpCaLst_caaplicativo = "STPCALST_caaplicativo";
        public const string StpCaSel_caaplicativo = "STPCASEL_caaplicativo";
        public const string StpCaUpd_caaplicativo = "STPCAUPD_caaplicativo";

        public const string StpCaRptLog = "STPCARPT_LOG";
        public const string StpCaRptPerfilOperador = "STPCARPT_PERFIL_OPERADOR";
        public const string StpCaRptPerfilFuncionalidade = "STPCARPT_PERFIL_FUNCIONALIDADE";
        public const string StpCaRptSistemaAplicativoFuncionalidade = "STPCARPT_SIS_APL_FUN";

        public const string StpCaSel_caconfiguracao = "STPCASEL_caconfiguracao";
        public const string StpCaUpd_caconfiguracao = "STPCAUPD_caconfiguracao";
        #endregion

        #region Mensagens
        public const string ErrorCodeStatusDescription = "ERROR";
        #endregion

        #region Log

        public const string StdMsgInfoInseriu = "INSERIU";
        public const string StdMsgInfoAlterou = "ALTEROU";
        public const string StdMsgInfoExcluiu = "EXCLUIU";

        public const string StdMsgInfoSistema = "SISTEMA";
        public const string StdMsgInfoAplicativo = "APLICATIVO";
        public const string StdMsgInfoFuncionalidade = "FUNCIONALIDADE";
        public const string StdMsgInfoPerfil = "PERFIL";
        public const string StdMsgInfoOperador = "OPERADOR";

        public const string StdMsgInfo = "Usuário {0} dados de {1}.";
        public const string StdMsgInfoLog = "Usuário {0} o {1} '{2}'.";

        /// <summary>
        /// Sistema
        /// </summary>
        public static string SistemaInserir = String.Format(StdMsgInfo, StdMsgInfoInseriu, StdMsgInfoSistema);
        public static string SistemaAlterar = String.Format(StdMsgInfo, StdMsgInfoAlterou, StdMsgInfoSistema);
        public static string SistemaExcluir = String.Format(StdMsgInfo, StdMsgInfoExcluiu, StdMsgInfoSistema);

        /// <summary>
        /// Aplicativo
        /// </summary>
        public static string AplicativoInserir = String.Format(StdMsgInfo, StdMsgInfoInseriu, StdMsgInfoAplicativo);
        public static string AplicativoAlterar = String.Format(StdMsgInfo, StdMsgInfoAlterou, StdMsgInfoAplicativo);
        public static string AplicativoExcluir = String.Format(StdMsgInfo, StdMsgInfoExcluiu, StdMsgInfoAplicativo);

        /// <summary>
        /// Funcionalidade
        /// </summary>
        public static string FuncionalidadeInserir = String.Format(StdMsgInfo, StdMsgInfoInseriu, StdMsgInfoFuncionalidade);
        public static string FuncionalidadeAlterar = String.Format(StdMsgInfo, StdMsgInfoAlterou, StdMsgInfoFuncionalidade);
        public static string FuncionalidadeExcluir = String.Format(StdMsgInfo, StdMsgInfoExcluiu, StdMsgInfoFuncionalidade);

        /// <summary>
        /// Perfil
        /// </summary>
        public static string PerfilInserir = String.Format(StdMsgInfo, StdMsgInfoInseriu, StdMsgInfoPerfil);
        public static string PerfilAlterar = String.Format(StdMsgInfo, StdMsgInfoAlterou, StdMsgInfoPerfil);
        public static string PerfilExcluir = String.Format(StdMsgInfo, StdMsgInfoExcluiu, StdMsgInfoPerfil);

        /// <summary>
        /// Operador
        /// </summary>
        public static string OperadorInserir = String.Format(StdMsgInfo, StdMsgInfoInseriu, StdMsgInfoOperador);
        public static string OperadorAlterar = String.Format(StdMsgInfo, StdMsgInfoAlterou, StdMsgInfoOperador);
        public static string OperadorExcluir = String.Format(StdMsgInfo, StdMsgInfoExcluiu, StdMsgInfoOperador);

        /// <summary>
        /// Configuração Geral
        /// </summary>
        public const string StdMsgInfoConfigGeral = "Usuário alterou a Configuração Geral.";

        /// <summary>
        /// Associação
        /// </summary>
        public const string StdMsgInfoAssociacaoPerfilOperador = "Usuário associou o Perfil (ID {0}) {1} ao Operador (ID {2}) {3}.";
        public const string StdMsgInfoDesassociacaoPerfilOperador = "Usuário desassociou o Perfil (ID {0}) {1} ao Operador (ID {2}) {3}.";
        public const string StdMsgInfoAssociacaoFuncionalidadePerfil = "Usuário associou a Funcionalidade (ID {0}) {1} ao Perfil (ID {2}) {3}.";
        public const string StdMsgInfoDesassociacaoFuncionalidadePerfil = "Usuário desassociou a Funcionalidade (ID {0}) {1} ao Perfil (ID {2}) {3}.";
        #endregion
    }
}
