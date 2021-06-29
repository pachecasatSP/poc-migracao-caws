using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsLstFuncionalidadeByFuncSistApl
    {
        private string _Erro;
        public string Erros
        {
            get { return this._Erro; }
        }

        public DataSet Retornar(int? p_idfuncionalidade, int? p_idaplicativo, int? p_idsistema, string p_dsfuncionalidade)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idfuncionalidade != null)
                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                if (p_idaplicativo != null)
                    _DB.ParametroAdicionar("@idaplicativo", SqlDbType.Int, 4, p_idaplicativo, ParameterDirection.Input);
                if (p_idsistema != null)
                    _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, p_idsistema, ParameterDirection.Input);
                if (p_dsfuncionalidade != null && p_dsfuncionalidade.Length != 0)
                    _DB.ParametroAdicionar("@dsfuncionalidade", SqlDbType.VarChar, 45, p_dsfuncionalidade, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_cafuncionalidade_by_func_sist_apl");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }
    }
}