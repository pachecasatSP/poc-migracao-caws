using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace Controle_Acesso
{
    public class clsLstPerfilFuncionalidade
    {
        private string _Erro;
        public string Erros
        {
            get { return this._Erro; }
        }

        public DataSet Retornar(int? p_idperfil, int? p_idfuncionalidade, string p_flperfilassociado)
        {
            //Retorna um DataSet - stplst_caperfil_funcionalidade
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idperfil != null)
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                if (p_idfuncionalidade != null)
                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                if (p_flperfilassociado != null && p_flperfilassociado.Length > 0)
                    _DB.ParametroAdicionar("@flperfilassociado", SqlDbType.Char, 1, p_flperfilassociado, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_caperfil_funcionalidade");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }
    }
}