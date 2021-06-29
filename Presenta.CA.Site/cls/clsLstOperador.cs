using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsLstOperador
    {
        private string _Erro;
        public string Erros
        {
            get { return this._Erro; }
        }

        public DataSet Retornar(int? p_idoperador, string p_cdoperador, string p_nmoperador, int? p_stoperador)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idoperador != null)
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, p_idoperador, ParameterDirection.Input);
                if (p_cdoperador != null && p_cdoperador.Length != 0)
                    _DB.ParametroAdicionar("@cdoperador", SqlDbType.VarChar, 25, p_cdoperador, ParameterDirection.Input);
                if (p_nmoperador != null && p_nmoperador.Length != 0)
                    _DB.ParametroAdicionar("@nmoperador", SqlDbType.VarChar, 45, p_nmoperador, ParameterDirection.Input);
                if (p_stoperador != null)
                    _DB.ParametroAdicionar("@stoperador", SqlDbType.Int, 4, p_stoperador, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_caoperador");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }
    }
}