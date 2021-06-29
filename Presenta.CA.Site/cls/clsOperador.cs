using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsOperador
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

        public DataSet Retornar(int? p_idoperador, string p_nmoperador)
        {
            //Retornar dataset - operador
            DataSet _ds = new DataSet();
            try
            {                
                clsDB _DB = new clsDB();

                if (p_idoperador != null)
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, p_idoperador, ParameterDirection.Input);
                if (p_nmoperador != null && p_nmoperador.Length != 0)
                    _DB.ParametroAdicionar("@nmoperador", SqlDbType.VarChar, 45, p_nmoperador, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_caoperador");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public SqlDataReader Retornar(string p_nmoperador)
        {
            //Retornar SqlDataReader - operador
            SqlDataReader _dr = null;
            try
            {
                clsDB _DB = new clsDB();
                
                _DB.ParametroAdicionar("@nmoperador", SqlDbType.VarChar, 45, p_nmoperador, ParameterDirection.Input);
                _dr = _DB.SQL_SP_RetornoDR("stpsel_caoperador");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }

            return _dr;
        }        

        public List<clsEntOperador> Retornar(int? p_idoperador)
        {
            //Retornar lista - operador
            List<clsEntOperador> _lstOperador = new List<clsEntOperador>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, p_idoperador, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_caoperador");
                while (_dr.Read())
                {
                    clsEntOperador _entOperador = new clsEntOperador();

                    _entOperador.idoperador = _dr["idoperador"] == null ? new int?() : Convert.ToInt32(_dr["idoperador"]); 
                    _entOperador.idtiposenha = Convert.ToInt32(_dr["idtiposenha"]);
                    _entOperador.stoperador = Convert.ToInt32(_dr["stoperador"]);
                    _entOperador.cdoperador = _dr["cdoperador"].ToString();
                    _entOperador.nmoperador = _dr["nmoperador"].ToString();

                    if (!Convert.IsDBNull(_dr["dsemail"]))
                        _entOperador.dsemail = _dr["dsemail"].ToString();

                    _entOperador.dtcadastro = Convert.ToDateTime(_dr["dtcadastro"]);
                    _entOperador.dhsituacao = Convert.ToDateTime(_dr["dhsituacao"]);
                    _entOperador.crsenha = _dr["crsenha"].ToString();
                    _entOperador.dtsenha = Convert.ToDateTime(_dr["dtsenha"]);

                    if (!Convert.IsDBNull(_dr["dhultimologin"]))
                        _entOperador.dhultimologin = Convert.ToDateTime(_dr["dhultimologin"]);

                    _entOperador.qtloginincorreto = Convert.ToInt32(_dr["qtloginincorreto"]);
                    _entOperador.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);
                    _entOperador.idoperadoratualizacao = Convert.ToInt32(_dr["idoperadoratualizacao"]);

                    _lstOperador.Add(_entOperador);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstOperador;
        }

        public DataSet RetornarLista(int? p_idoperador, string p_cdoperador, string p_nmoperador, int? p_stoperador)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idoperador != null)
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, p_idoperador, ParameterDirection.Input);
                if (p_cdoperador != null && p_cdoperador.Length != 0)
                    _DB.ParametroAdicionar("@cdoperador", SqlDbType.VarChar, 25, p_cdoperador, ParameterDirection.Input);
                if (p_nmoperador != null && p_nmoperador.Length != 0)
                    _DB.ParametroAdicionar("@nmoperador", SqlDbType.VarChar, 45, p_nmoperador, ParameterDirection.Input);
                if (p_stoperador != null)
                    _DB.ParametroAdicionar("@stoperador", SqlDbType.Int, 4, p_stoperador, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_caoperador");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public void Gravar(clsEntOperador _entOperador)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entOperador.nmoperador == null)
                    _Erro += "Campo 'Nome Operador' não informado.";
                if (_entOperador.cdoperador == null)
                    _Erro += "Campo 'Operador' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@idtiposenha", SqlDbType.Int, 4, _entOperador.idtiposenha, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@stoperador", SqlDbType.Int, 4, _entOperador.stoperador, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@cdoperador", SqlDbType.VarChar, 25, _entOperador.cdoperador, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@nmoperador", SqlDbType.VarChar, 45, _entOperador.nmoperador, ParameterDirection.Input);
                    
                    if (_entOperador.dsemail != null)
                        _DB.ParametroAdicionar("@dsemail", SqlDbType.VarChar, 45, _entOperador.dsemail, ParameterDirection.Input);
                    else
                        _DB.ParametroAdicionar("@dsemail", SqlDbType.VarChar, 45, DBNull.Value , ParameterDirection.Input);

                    _DB.ParametroAdicionar("@dtcadastro", SqlDbType.DateTime, 8, _entOperador.dtcadastro, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@dhsituacao", SqlDbType.DateTime, 8, _entOperador.dhsituacao, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@crsenha", SqlDbType.VarChar, 100, _entOperador.crsenha, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@dtsenha", SqlDbType.DateTime, 8, _entOperador.dtsenha, ParameterDirection.Input);

                    if (_entOperador.dhultimologin != null)
                        _DB.ParametroAdicionar("@dhultimologin", SqlDbType.DateTime, 8, _entOperador.dhultimologin, ParameterDirection.Input);
                    else
                        _DB.ParametroAdicionar("@dhultimologin", SqlDbType.DateTime, 8, DBNull.Value, ParameterDirection.Input);
                  
                    _DB.ParametroAdicionar("@qtloginincorreto", SqlDbType.Int, 4, _entOperador.qtloginincorreto, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperadoratualizacao", SqlDbType.Int, 4, _entOperador.idoperadoratualizacao, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entOperador.idoperador, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_caoperador");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entOperador.idoperador, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_caoperador");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_idoperador)
        {
            try
            {
                //Consistência
                if (p_idoperador == 0)
                    throw new Exception("Campo 'Id' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, p_idoperador, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_caoperador");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }


    }
}