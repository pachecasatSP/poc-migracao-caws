using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsLstTipoSenhaTipoSenhaValidacao
    {
        private string _Erro;
        public string Erros
        {
            get { return this._Erro; }
        }

        public DataSet Retornar(int? p_idtiposenha, int? p_idtiposenhavalidacao, string p_fltiposenhaassociado)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idtiposenha != null)
                    _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, p_idtiposenha, ParameterDirection.Input);
                if (p_idtiposenhavalidacao != null)
                    _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, p_idtiposenhavalidacao, ParameterDirection.Input);
                if (p_fltiposenhaassociado != null && p_fltiposenhaassociado.Length > 0)
                    _DB.ParametroAdicionar("@fltiposenhaassociado", SqlDbType.Char, 1, p_fltiposenhaassociado, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_catiposenha_tiposenhavalidacao");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }
    }
}