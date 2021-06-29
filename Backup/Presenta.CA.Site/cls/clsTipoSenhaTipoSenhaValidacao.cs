using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace Controle_Acesso
{
    public class clsTipoSenhaTipoSenhaValidacao
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

        public DataSet Retornar(int? p_idtiposenha, int? p_idtiposenhavalidacao)
        {
            //Retorna um DataSet - stpsel_catiposenha_tiposenhavalidacao
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idtiposenha != null)
                    _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, p_idtiposenha, ParameterDirection.Input);
                if (p_idtiposenhavalidacao != null)
                    _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, p_idtiposenhavalidacao, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_catiposenha_tiposenhavalidacao");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntTipoSenhaTipoSenhaValidacao> Retornar(int? p_idtiposenha, int? p_idtiposenhavalidacao, int? p_nuordem)
        {
            //Retornar uma lista - catiposenha_tiposenhavalidacao
            List<clsEntTipoSenhaTipoSenhaValidacao> _lstEntTipoSenhaTipoSenhaValidacao = new List<clsEntTipoSenhaTipoSenhaValidacao>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, p_idtiposenha, ParameterDirection.Input);
                _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, p_idtiposenhavalidacao, ParameterDirection.Input);
                // matsu - fernandes rever essa procedure no banco de dados
                _DB.ParametroAdicionar("@nuordem", SqlDbType.Int, 4, p_nuordem, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_catiposenha_tiposenhavalidacao");
                while (_dr.Read())
                {
                    clsEntTipoSenhaTipoSenhaValidacao _EntTipoSenhaTipoSenhaValidacao = new clsEntTipoSenhaTipoSenhaValidacao();

                    _EntTipoSenhaTipoSenhaValidacao.idtiposenha = _dr["idtiposenha"] == null ? new int?() : Convert.ToInt32(_dr["idtiposenha"]);
                    _EntTipoSenhaTipoSenhaValidacao.idtiposenhavalidacao = _dr["idtiposenhavalidacao"] == null ? new int?() : Convert.ToInt32(_dr["idtiposenhavalidacao"]);
                    _EntTipoSenhaTipoSenhaValidacao.nuordem = Convert.ToInt32(_dr["nuordem"]);
                    _EntTipoSenhaTipoSenhaValidacao.qtmincaracteres = Convert.ToInt32(_dr["qtmincaracteres"]);
                    _EntTipoSenhaTipoSenhaValidacao.qtmaxcaracteres = Convert.ToInt32(_dr["qtmaxcaracteres"]);
                    _EntTipoSenhaTipoSenhaValidacao.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _EntTipoSenhaTipoSenhaValidacao.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntTipoSenhaTipoSenhaValidacao.Add(_EntTipoSenhaTipoSenhaValidacao);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntTipoSenhaTipoSenhaValidacao;
        }

        public DataSet RetornarLista(int? p_idtiposenha, int? p_idtiposenhavalidacao)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idtiposenha != null)
                    _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, p_idtiposenha, ParameterDirection.Input);
                if (p_idtiposenhavalidacao != null)
                    _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, p_idtiposenhavalidacao, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_catiposenha_tiposenhavalidacao");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public DataSet RetornarAssociacao(int? p_idtiposenha, int? p_idtiposenhavalidacao, string p_fltiposenhaassociado)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idtiposenha != null)
                    _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, p_idtiposenha, ParameterDirection.Input);
                if (p_idtiposenhavalidacao != null)
                    _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, p_idtiposenhavalidacao, ParameterDirection.Input);
                if (p_fltiposenhaassociado != null && p_fltiposenhaassociado.Length > 0)
                    _DB.ParametroAdicionar("@fltiposenhaassociado", SqlDbType.Char, 1, p_fltiposenhaassociado, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpass_catiposenha_tiposenhavalidacao");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public void Gravar(clsEntTipoSenhaTipoSenhaValidacao _entTipoSenhaTipoSenhaValidacao)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entTipoSenhaTipoSenhaValidacao.nuordem == null)
                    _Erro += "Campo 'nuordem' não informado.";
                if (_entTipoSenhaTipoSenhaValidacao.qtmincaracteres == null)
                    _Erro += "Campo 'qtmincaracteres' não informado.";
                if (_entTipoSenhaTipoSenhaValidacao.qtmaxcaracteres == null)
                    _Erro += "Campo 'qtmaxcaracteres' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@nuordem", SqlDbType.Int, 4, _entTipoSenhaTipoSenhaValidacao.nuordem, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@qtmincaracteres", SqlDbType.Int, 4, _entTipoSenhaTipoSenhaValidacao.qtmincaracteres, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@qtmaxcaracteres", SqlDbType.Int, 4, _entTipoSenhaTipoSenhaValidacao.qtmaxcaracteres, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entTipoSenhaTipoSenhaValidacao.idoperador, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, _entTipoSenhaTipoSenhaValidacao.idtiposenha, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, _entTipoSenhaTipoSenhaValidacao.idtiposenhavalidacao, ParameterDirection.Input);

                    if (_Insercao)
                    {                        
                        _DB.SQL_SP_Exec("stpins_catiposenha_tiposenhavalidacao");
                    }
                    else
                    {                        
                        _DB.SQL_SP_Exec("stpupd_catiposenha_tiposenhavalidacao");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_idtiposenha, int p_idtiposenhavalidacao)
        {
            try
            {
                //Consistência
                if (p_idtiposenha == null)
                    throw new Exception("Campo 'idtiposenha' não informado.");
                else if (p_idtiposenhavalidacao == null)
                    throw new Exception("Campo 'idtiposenhavalidacao' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, p_idtiposenha, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idtiposenhavalidacao", SqlDbType.Int, 4, p_idtiposenhavalidacao, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_catiposenha_tiposenhavalidacao");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public int ContarNuOrdem(int? p_idtiposenha)
        {
            try
            {
                // matsutami - rever com o fernandes
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, p_idtiposenha, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpcnt_catiposenha_tiposenhavalidacao");
                _dr.Read();
                return Convert.ToInt32(_dr["total"]);

                //while (_dr.Read())
                //{
                //    return Convert.ToInt32(_dr["total"]);
                //}
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
                return 0;
            }
        }




    }
}