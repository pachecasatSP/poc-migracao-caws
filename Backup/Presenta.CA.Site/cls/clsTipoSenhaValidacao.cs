using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsTipoSenhaValidacao
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

        public DataSet Retornar(int? p_idtiposenhavalidacao, string p_dstiposenhavalidacao)
        {
            //Retorna um DataSet - catiposenhavalidacao
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idtiposenhavalidacao != null)
                    _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, p_idtiposenhavalidacao, ParameterDirection.Input);
                if (p_dstiposenhavalidacao != null && p_dstiposenhavalidacao.Length != 0)
                    _DB.ParametroAdicionar("@dstiposenhavalidacao", SqlDbType.VarChar, 45, p_dstiposenhavalidacao, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_catiposenhavalidacao");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntTipoSenhaValidacao> Retornar(int? idtiposenhavalidacao)
        {
            //Retornar uma lista - catiposenhavalidacao
            List<clsEntTipoSenhaValidacao> _lstEntTipoSenhaValidacao = new List<clsEntTipoSenhaValidacao>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, idtiposenhavalidacao, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_catiposenhavalidacao");
                while (_dr.Read())
                {
                    clsEntTipoSenhaValidacao _EntTipoSenhaValidacao = new clsEntTipoSenhaValidacao();

                    _EntTipoSenhaValidacao.idtiposenhavalidacao = _dr["idtiposenhavalidacao"] == null ? new int?() : Convert.ToInt32(_dr["idtiposenhavalidacao"]); 
                    _EntTipoSenhaValidacao.dstiposenhavalidacao = _dr["dstiposenhavalidacao"].ToString();
                    _EntTipoSenhaValidacao.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _EntTipoSenhaValidacao.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntTipoSenhaValidacao.Add(_EntTipoSenhaValidacao);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntTipoSenhaValidacao;
        }

        public void Gravar(clsEntTipoSenhaValidacao _EntTipoSenhaValidacao)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_EntTipoSenhaValidacao.dstiposenhavalidacao == null)
                    _Erro += "Campo 'Descrição da Situação da Funcionalidade' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@dstiposenhavalidacao", SqlDbType.VarChar, 45, _EntTipoSenhaValidacao.dstiposenhavalidacao, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _EntTipoSenhaValidacao.idoperador, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, _EntTipoSenhaValidacao.idtiposenhavalidacao, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_catiposenhavalidacao");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, _EntTipoSenhaValidacao.idtiposenhavalidacao, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_catiposenhavalidacao");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int idtiposenhavalidacao)
        {
            try
            {
                //Consistência
                if (idtiposenhavalidacao == null)
                    throw new Exception("Campo 'Situação da Funcionalidade' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, idtiposenhavalidacao, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_catiposenhavalidacao");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }
    }
}