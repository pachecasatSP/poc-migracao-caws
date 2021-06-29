using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsLstAplicativo
    {
        private string _Erro;
        public string Erros
        {
            get { return this._Erro; }
        }

        public DataSet Retornar(int? p_idaplicativo, int? p_idsistema, string p_dsaplicativo)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idaplicativo != null)
                    _DB.ParametroAdicionar("@idaplicativo", SqlDbType.Int, 4, p_idaplicativo, ParameterDirection.Input);
                if (p_idsistema != null)
                    _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, p_idsistema, ParameterDirection.Input);
                if (p_dsaplicativo != null && p_dsaplicativo.Length != 0)
                    _DB.ParametroAdicionar("@dsaplicativo", SqlDbType.VarChar, 45, p_dsaplicativo, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_caaplicativo");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }
    }
}