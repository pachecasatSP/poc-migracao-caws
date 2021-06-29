using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsSituacaoFuncionalidade
    {
        private bool _Insercao = false;
        public bool Insercao
        {
            set { this._Insercao = value; }
        }
        private int _IdNew;
        public int IdNew
        {
            get { return this._IdNew; }
            set { this._IdNew = value; }
        }

        private string _Erro;
        public string Erros
        {
            get { return this._Erro; }
        }

        public DataSet Retornar(int? p_stfuncionalidade, string p_dssituacaofuncionalidade)
        {
            //Retorna um DataSet - casituacao_funcionalidade
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_stfuncionalidade != null)
                    _DB.ParametroAdicionar("@stfuncionalidade", SqlDbType.Int, 4, p_stfuncionalidade, ParameterDirection.Input);
                if (p_dssituacaofuncionalidade != null && p_dssituacaofuncionalidade.Length != 0)
                    _DB.ParametroAdicionar("@dssituacaofuncionalidade", SqlDbType.VarChar, 45, p_dssituacaofuncionalidade, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_casituacao_funcionalidade");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntSituacaoFuncionalidade> Retornar(int? p_stfuncionalidade)
        {
            //Retornar uma lista - casituacao_funcionalidade
            List<clsEntSituacaoFuncionalidade> _lstEntSituacaoFuncionalidade = new List<clsEntSituacaoFuncionalidade>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@stfuncionalidade", SqlDbType.Int, 4, p_stfuncionalidade, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_casituacao_funcionalidade");
                while (_dr.Read())
                {
                    clsEntSituacaoFuncionalidade _entSituacaoFuncionalidade = new clsEntSituacaoFuncionalidade();

                    _entSituacaoFuncionalidade.stfuncionalidade = _dr["stfuncionalidade"] == null ? new int?() : Convert.ToInt32(_dr["stfuncionalidade"]);
                    _entSituacaoFuncionalidade.dssituacaofuncionalidade = _dr["dssituacaofuncionalidade"].ToString();
                    _entSituacaoFuncionalidade.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _entSituacaoFuncionalidade.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntSituacaoFuncionalidade.Add(_entSituacaoFuncionalidade);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntSituacaoFuncionalidade;
        }

        public void Gravar(clsEntSituacaoFuncionalidade _entSituacaoFuncionalidade)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entSituacaoFuncionalidade.dssituacaofuncionalidade == null)
                    _Erro += "Campo 'Descrição da Situação da Funcionalidade' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@dssituacaofuncionalidade", SqlDbType.VarChar, 45, _entSituacaoFuncionalidade.dssituacaofuncionalidade, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entSituacaoFuncionalidade.idoperador, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@stfuncionalidade", SqlDbType.Int, 4, _entSituacaoFuncionalidade.stfuncionalidade, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_casituacao_funcionalidade");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@stfuncionalidade", SqlDbType.Int, 4, _entSituacaoFuncionalidade.stfuncionalidade, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_casituacao_funcionalidade");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_stfuncionalidade)
        {
            try
            {
                //Consistência
                if (p_stfuncionalidade == null)
                    throw new Exception("Campo 'Situação da Funcionalidade' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@stfuncionalidade", SqlDbType.Int, 4, p_stfuncionalidade, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_casituacao_funcionalidade");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }
    }
}