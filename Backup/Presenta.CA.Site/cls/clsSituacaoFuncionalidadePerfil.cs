using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsSituacaoFuncionalidadePerfil
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

        public DataSet Retornar(int? p_stfuncionalidadeperfil, string p_dssituacaofuncionalidadeperfil)
        {
            //Retorna um DataSet - casituacao_funcionalidade_perfil
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_stfuncionalidadeperfil != null)
                    _DB.ParametroAdicionar("@stfuncionalidadeperfil", SqlDbType.Int, 4, p_stfuncionalidadeperfil, ParameterDirection.Input);
                if (p_dssituacaofuncionalidadeperfil != null && p_dssituacaofuncionalidadeperfil.Length != 0)
                    _DB.ParametroAdicionar("@dssituacaofuncionalidade", SqlDbType.VarChar, 45, p_dssituacaofuncionalidadeperfil, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_casituacao_funcionalidade_perfil");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntSituacaoFuncionalidadePerfil> Retornar(int? p_stfuncionalidadeperfil)
        {
            //Retornar uma lista - casituacao_funcionalidade_perfil
            List<clsEntSituacaoFuncionalidadePerfil> _lstEntSituacaoFuncionalidadePerfil = new List<clsEntSituacaoFuncionalidadePerfil>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@stfuncionalidadeperfil", SqlDbType.Int, 4, p_stfuncionalidadeperfil, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_casituacao_funcionalidade_perfil");
                while (_dr.Read())
                {
                    clsEntSituacaoFuncionalidadePerfil _entSituacaoFuncionalidadePerfil = new clsEntSituacaoFuncionalidadePerfil();

                    _entSituacaoFuncionalidadePerfil.stfuncionalidadeperfil = _dr["stfuncionalidadeperfil"] == null ? new int?() : Convert.ToInt32(_dr["stfuncionalidadeperfil"]); 
                    _entSituacaoFuncionalidadePerfil.dssituacaofuncionalidadeperfil = _dr["dssituacaofuncionalidadeperfil"].ToString();
                    _entSituacaoFuncionalidadePerfil.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _entSituacaoFuncionalidadePerfil.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntSituacaoFuncionalidadePerfil.Add(_entSituacaoFuncionalidadePerfil);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntSituacaoFuncionalidadePerfil;
        }

        public void Gravar(clsEntSituacaoFuncionalidadePerfil _entSituacaoFuncionalidadePerfil)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entSituacaoFuncionalidadePerfil.dssituacaofuncionalidadeperfil == null)
                    _Erro += "Campo 'Descrição da Situação da Funcionalidade' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@dssituacaofuncionalidadeperfil", SqlDbType.VarChar, 45, _entSituacaoFuncionalidadePerfil.dssituacaofuncionalidadeperfil, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entSituacaoFuncionalidadePerfil.idoperador, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@stfuncionalidadeperfil", SqlDbType.Int, 4, _entSituacaoFuncionalidadePerfil.stfuncionalidadeperfil, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_casituacao_funcionalidade_perfil");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@stfuncionalidadeperfil", SqlDbType.Int, 4, _entSituacaoFuncionalidadePerfil.stfuncionalidadeperfil, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_casituacao_funcionalidade_perfil");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_stfuncionalidadeperfil)
        {
            try
            {
                //Consistência
                if (p_stfuncionalidadeperfil == null)
                    throw new Exception("Campo 'Situação da Funcionalidade' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@stfuncionalidadeperfil", SqlDbType.Int, 4, p_stfuncionalidadeperfil, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_casituacao_funcionalidade_perfil");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }
    }
}