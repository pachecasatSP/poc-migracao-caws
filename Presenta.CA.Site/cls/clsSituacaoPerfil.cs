using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsSituacaoPerfil
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

        public DataSet Retornar(int? p_stperfil, string p_dssituacaoperfil)
        {
            //Retorna um DataSet - casituacao_perfil
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_stperfil != null)
                    _DB.ParametroAdicionar("@stperfil", SqlDbType.Int, 4, p_stperfil, ParameterDirection.Input);
                if (p_dssituacaoperfil != null && p_dssituacaoperfil.Length != 0)
                    _DB.ParametroAdicionar("@dssituacaoperfil", SqlDbType.VarChar, 45, p_dssituacaoperfil, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_casituacao_perfil");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntSituacaoPerfil> Retornar(int? p_stperfil)
        {
            //Retornar uma lista - casituacao_perfil
            List<clsEntSituacaoPerfil> _lstEntSituacaoPerfil = new List<clsEntSituacaoPerfil>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@stperfil", SqlDbType.Int, 4, p_stperfil, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_casituacao_perfil");
                while (_dr.Read())
                {
                    clsEntSituacaoPerfil _entSituacaoPerfil = new clsEntSituacaoPerfil();

                    _entSituacaoPerfil.stperfil = _dr["stperfil"] == null ? new int?() : Convert.ToInt32(_dr["stperfil"]); 
                    _entSituacaoPerfil.dssituacaoperfil = _dr["dssituacaoperfil"].ToString();
                    _entSituacaoPerfil.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _entSituacaoPerfil.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntSituacaoPerfil.Add(_entSituacaoPerfil);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntSituacaoPerfil;
        }

        public void Gravar(clsEntSituacaoPerfil _entSituacaoPerfil)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entSituacaoPerfil.dssituacaoperfil == null)
                    _Erro += "Campo 'Descrição da Situação do Perfil' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@dssituacaoperfil", SqlDbType.VarChar, 45, _entSituacaoPerfil.dssituacaoperfil, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entSituacaoPerfil.idoperador, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@stperfil", SqlDbType.Int, 4, _entSituacaoPerfil.stperfil, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_casituacao_perfil");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@stperfil", SqlDbType.Int, 4, _entSituacaoPerfil.stperfil, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_casituacao_perfil");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_stperfil)
        {
            try
            {
                //Consistência
                if (p_stperfil == null)
                    throw new Exception("Campo 'Situação do Perfil' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@stperfil", SqlDbType.Int, 4, p_stperfil, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_casituacao_perfil");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }
    }
}