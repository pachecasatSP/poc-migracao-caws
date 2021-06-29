using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsTipoSenha
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

        public DataSet Retornar(int? p_idtiposenha, string p_dstiposenha)
        {
            //Retorna um DataSet - catiposenha
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idtiposenha != null)
                    _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, p_idtiposenha, ParameterDirection.Input);
                if (p_dstiposenha != null && p_dstiposenha.Length != 0)
                    _DB.ParametroAdicionar("@dstiposenha", SqlDbType.VarChar, 45, p_dstiposenha, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_catiposenha");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntTipoSenha> Retornar(int? idtiposenha)
        {
            //Retornar uma lista - catiposenha
            List<clsEntTipoSenha> _lstEntTipoSenha = new List<clsEntTipoSenha>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, idtiposenha, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_catiposenha");
                while (_dr.Read())
                {
                    clsEntTipoSenha _EntTipoSenha = new clsEntTipoSenha();

                    _EntTipoSenha.idtiposenha = _dr["idtiposenha"] == null ? new int?() : Convert.ToInt32(_dr["idtiposenha"]); 
                    _EntTipoSenha.dstiposenha = _dr["dstiposenha"].ToString();
                    _EntTipoSenha.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _EntTipoSenha.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);
                    _EntTipoSenha.qtmaxtentativas = Convert.ToInt32(_dr["qtmaxtentativas"]);
                    _EntTipoSenha.qtverificacaohistorico = Convert.ToInt32(_dr["qtverificacaohistorico"]);

                    if (!Convert.IsDBNull(_dr["cdexpressaoregular"]))
                        _EntTipoSenha.cdexpressaoregular = _dr["cdexpressaoregular"].ToString();
                                       
                    _EntTipoSenha.dtiniciovigencia = Convert.ToDateTime(_dr["dtiniciovigencia"]);

                    if (!Convert.IsDBNull(_dr["dtfimvigencia"]))
                        _EntTipoSenha.dtfimvigencia = Convert.ToDateTime(_dr["dtfimvigencia"]);

                    _lstEntTipoSenha.Add(_EntTipoSenha);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntTipoSenha;
        }

        public void Gravar(clsEntTipoSenha _EntTipoSenha)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_EntTipoSenha.dstiposenha == null)
                    _Erro += "Campo 'Descrição da Situação da Funcionalidade' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@dstiposenha", SqlDbType.VarChar, 45, _EntTipoSenha.dstiposenha, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _EntTipoSenha.idoperador, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@qtmaxtentativas", SqlDbType.Int, 4, _EntTipoSenha.qtmaxtentativas, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@qtverificacaohistorico", SqlDbType.Int, 4, _EntTipoSenha.qtverificacaohistorico, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@cdexpressaoregular", SqlDbType.VarChar, 1000, _EntTipoSenha.cdexpressaoregular, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@dtiniciovigencia", SqlDbType.DateTime, 8, _EntTipoSenha.dtiniciovigencia, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@dtfimvigencia", SqlDbType.DateTime, 8, _EntTipoSenha.dtfimvigencia, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, _EntTipoSenha.idtiposenha, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_catiposenha");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, _EntTipoSenha.idtiposenha, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_catiposenha");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int idtiposenha)
        {
            try
            {
                //Consistência
                if (idtiposenha == null)
                    throw new Exception("Campo 'Situação da Funcionalidade' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, idtiposenha, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_catiposenha");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }
    }
}