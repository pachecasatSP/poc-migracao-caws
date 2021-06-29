using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsPerfil
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

        public DataSet Retornar(int? p_idperfil, string p_dsperfil)
        {
            //Retornar dataset - perfil
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idperfil != null)
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.VarChar, 10, p_idperfil, ParameterDirection.Input);
                if (p_dsperfil != null && p_dsperfil.Length != 0)
                    _DB.ParametroAdicionar("@dsperfil", SqlDbType.VarChar, 45, p_dsperfil, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_caperfil");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntPerfil> Retornar(int? p_idperfil)
        {
            //Retornar lista - perfil
            List<clsEntPerfil> _entPerfil = new List<clsEntPerfil>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idperfil", SqlDbType.VarChar, 10, p_idperfil, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_caperfil");
                while (_dr.Read())
                {
                    clsEntPerfil _perfil = new clsEntPerfil();

                    _perfil.idperfil = _dr["idperfil"] == null ? new int?() : Convert.ToInt32(_dr["idperfil"]);
                    _perfil.dsperfil = _dr["dsperfil"].ToString();
                    _perfil.stperfil = Convert.ToInt32(_dr["stperfil"]);
                    _perfil.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _perfil.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);
                    _entPerfil.Add(_perfil);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _entPerfil;
        }

        public DataSet RetornarLista(int? p_idperfil, string p_dsperfil, int? p_stperfil)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idperfil != null)
                    _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                if (p_dsperfil != null && p_dsperfil.Length != 0)
                    _DB.ParametroAdicionar("@dsperfil", SqlDbType.VarChar, 45, p_dsperfil, ParameterDirection.Input);
                if (p_stperfil != null)
                    _DB.ParametroAdicionar("@stperfil", SqlDbType.Int, 4, p_stperfil, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_caperfil");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public void Gravar(clsEntPerfil _entPerfil)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entPerfil.dsperfil == null)
                    _Erro += "Campo 'Descrição do Perfil' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();
                    
                    _DB.ParametroAdicionar("@dsperfil", SqlDbType.VarChar, 45, _entPerfil.dsperfil, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@stperfil", SqlDbType.Int, 4, _entPerfil.stperfil, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entPerfil.idoperador, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, _entPerfil.idperfil, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_caperfil");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, _entPerfil.idperfil, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_caperfil");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_idperfil)
        {
            try
            {
                //Consistência
                if (p_idperfil == null)
                    throw new Exception("Campo 'Id' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.VarChar, 10, p_idperfil, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_caperfil");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }


    }
}