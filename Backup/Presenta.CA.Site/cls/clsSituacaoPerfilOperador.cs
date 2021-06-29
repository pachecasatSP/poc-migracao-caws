using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsSituacaoPerfilOperador
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

        public DataSet Retornar(int? p_stperfiloperador, string p_dsperfiloperador)
        {
            //Retorna um DataSet - casituacao_perfil_operador
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_stperfiloperador != null)
                    _DB.ParametroAdicionar("@stperfiloperador", SqlDbType.Int, 4, p_stperfiloperador, ParameterDirection.Input);
                if (p_dsperfiloperador != null && p_dsperfiloperador.Length != 0)
                    _DB.ParametroAdicionar("@dsperfiloperador", SqlDbType.VarChar, 45, p_dsperfiloperador, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_casituacao_perfil_operador");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntSituacaoPerfilOperador> Retornar(int? p_stperfiloperador)
        {
            //Retornar uma lista - casituacao_perfil_operador
            List<clsEntSituacaoPerfilOperador> _lstEntSituacaoPerfilOperador = new List<clsEntSituacaoPerfilOperador>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@stperfiloperador", SqlDbType.Int, 4, p_stperfiloperador, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_casituacao_perfil_operador");
                while (_dr.Read())
                {
                    clsEntSituacaoPerfilOperador _entSituacaoPerfilOperador = new clsEntSituacaoPerfilOperador();

                    _entSituacaoPerfilOperador.stperfiloperador = _dr["stperfiloperador"] == null ? new int?() : Convert.ToInt32(_dr["stperfiloperador"]); 
                    _entSituacaoPerfilOperador.dsperfiloperador = _dr["dsperfiloperador"].ToString();
                    _entSituacaoPerfilOperador.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _entSituacaoPerfilOperador.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntSituacaoPerfilOperador.Add(_entSituacaoPerfilOperador);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntSituacaoPerfilOperador;
        }

        public void Gravar(clsEntSituacaoPerfilOperador _entSituacaoPerfilOperador)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entSituacaoPerfilOperador.dsperfiloperador == null)
                    _Erro += "Campo 'Descrição da Situação do Perfil' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@dsperfiloperador", SqlDbType.VarChar, 45, _entSituacaoPerfilOperador.dsperfiloperador, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entSituacaoPerfilOperador.idoperador, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@stperfiloperador", SqlDbType.Int, 4, _entSituacaoPerfilOperador.stperfiloperador, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_casituacao_perfil_operador");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@stperfiloperador", SqlDbType.Int, 4, _entSituacaoPerfilOperador.stperfiloperador, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_casituacao_perfil_operador");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_stperfiloperador)
        {
            try
            {
                //Consistência
                if (p_stperfiloperador == null)
                    throw new Exception("Campo 'Situação do Perfil' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@stperfiloperador", SqlDbType.Int, 4, p_stperfiloperador, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_casituacao_perfil_operador");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }
    }
}