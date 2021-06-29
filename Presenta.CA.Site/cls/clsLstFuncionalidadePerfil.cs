using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsLstFuncionalidadePerfil
    {
        private string _Erro;
        public string Erros
        {
            get { return this._Erro; }
        }

        public DataSet Retornar(int? p_idfuncionalidade, int? p_idperfil, int? p_stfuncionalidadeperfil)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idfuncionalidade != null)
                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                if (p_idperfil != null)
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                if (p_stfuncionalidadeperfil != null)
                    _DB.ParametroAdicionar("@stfuncionalidadeperfil", SqlDbType.Int, 4, p_stfuncionalidadeperfil, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_cafuncionalidade_perfil");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }
    }
}