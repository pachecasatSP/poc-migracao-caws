using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsLstPerfil
    {
        private string _Erro;
        public string Erros
        {
            get { return this._Erro; }
        }

        public DataSet Retornar(int? p_idperfil, string p_dsperfil, int? p_stperfil)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idperfil != null)
                    _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                if (p_dsperfil != null && p_dsperfil.Length != 0)
                    _DB.ParametroAdicionar("@dsperfil", SqlDbType.VarChar, 45, p_dsperfil, ParameterDirection.Input);
                if (p_stperfil != null)
                    _DB.ParametroAdicionar("@stperfil", SqlDbType.Int, 4, p_stperfil, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_caperfil");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }
    }
}