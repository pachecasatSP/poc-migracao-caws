using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsPerfilOperador
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

        public DataSet Retornar(int? p_idperfil, int? p_idoperador, int p_teste)
        {
            //Retornar dataset - Perfil x Operador
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idperfil != null)
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                if (p_idoperador != null)
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, p_idoperador, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_caperfil_operador");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntPerfilOperador> Retornar(int? p_idperfil, int? p_idoperador)
        {
            //Retornar lista - Perfil x Operador
            List<clsEntPerfilOperador> _lstEntPerfilOperador = new List<clsEntPerfilOperador>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, p_idoperador, ParameterDirection.Input);

                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_caperfil_operador");
                while (_dr.Read())
                {
                    clsEntPerfilOperador _entPerfilOperador = new clsEntPerfilOperador();

                    _entPerfilOperador.idperfil = _dr["idperfil"] == null ? new int?() : Convert.ToInt32(_dr["idperfil"]);
                    _entPerfilOperador.idoperador = _dr["idoperador"] == null ? new int?() : Convert.ToInt32(_dr["idoperador"]);
                    _entPerfilOperador.stperfiloperador = Convert.ToInt32(_dr["stperfiloperador"]);
                    _entPerfilOperador.dhsituacao = Convert.ToDateTime(_dr["dhsituacao"]);
                    _entPerfilOperador.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);
                    _entPerfilOperador.idoperadoratualizacao = Convert.ToInt32(_dr["idoperadoratualizacao"]);

                    _lstEntPerfilOperador.Add(_entPerfilOperador);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntPerfilOperador;
        }

        public DataSet RetornarLista(int? p_idperfil, int? p_idoperador, string p_flperfilassociado)
        {
            //Retorna um DataSet - stplst_caperfil_operador
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idperfil != null)
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                if (p_idoperador != null)
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, p_idoperador, ParameterDirection.Input);
                if (p_flperfilassociado != null && p_flperfilassociado.Length > 0)
                    _DB.ParametroAdicionar("@flperfilassociado", SqlDbType.Char, 1, p_flperfilassociado, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_caperfil_operador");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public void Gravar(clsEntPerfilOperador _entPerfil)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;

                if (_entPerfil.stperfiloperador == null)
                    _Erro += "Campo 'stperfiloperador' não informado.";
                if (_entPerfil.dhsituacao == null)
                    _Erro += "Campo 'dhsituacao' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, _entPerfil.idperfil, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entPerfil.idoperador, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@stperfiloperador", SqlDbType.Int, 4, _entPerfil.stperfiloperador, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@dhsituacao", SqlDbType.DateTime, 8, _entPerfil.dhsituacao, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperadoratualizacao", SqlDbType.Int, 4, _entPerfil.idoperadoratualizacao, ParameterDirection.Input);

                    if (_Insercao)
                        _DB.SQL_SP_Exec("stpins_caperfil_operador");
                    else
                        _DB.SQL_SP_Exec("stpupd_caperfil_operador");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_idperfil, int p_idoperador)
        {
            try
            {
                //Consistência
                if (p_idperfil == null)
                    throw new Exception("Campo 'idperfil' não informado.");
                else if (p_idoperador == null)
                    throw new Exception("Campo 'idoperador' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, p_idoperador, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_caperfil_operador");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }


    }
}

  