using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsFuncionalidadePerfil
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

        public DataSet Retornar(int? p_idfuncionalidade, int? p_idperfil, int p_teste)
        {
            //Retornar dataset - cafuncionalidade_perfil
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idfuncionalidade != null)
                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                if (p_idperfil != null)
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_cafuncionalidade_perfil");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntFuncionalidadePerfil> Retornar(int? p_idfuncionalidade, int? p_idperfil)
        {
            //Retornar lista - funcionalidade
            List<clsEntFuncionalidadePerfil> _lstEntFuncionalidadePerfil = new List<clsEntFuncionalidadePerfil>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);                
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_cafuncionalidade_perfil");
                while (_dr.Read())
                {
                    clsEntFuncionalidadePerfil _EntFuncionalidade = new clsEntFuncionalidadePerfil();

                    _EntFuncionalidade.idfuncionalidade = _dr["idfuncionalidade"] == null ? new int?() : Convert.ToInt32(_dr["idfuncionalidade"]);
                    _EntFuncionalidade.idperfil = _dr["idperfil"] == null ? new int?() : Convert.ToInt32(_dr["idperfil"]);
                    _EntFuncionalidade.stfuncionalidadeperfil = Convert.ToInt32(_dr["stfuncionalidadeperfil"]);
                    _EntFuncionalidade.dhsituacao = Convert.ToDateTime(_dr["dhsituacao"]);
                    _EntFuncionalidade.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _EntFuncionalidade.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntFuncionalidadePerfil.Add(_EntFuncionalidade);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntFuncionalidadePerfil;
        }

        public DataSet RetornarLista(int? p_idfuncionalidade, int? p_idperfil, int? p_stfuncionalidadeperfil)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idfuncionalidade != null)
                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                if (p_idperfil != null)
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                if (p_stfuncionalidadeperfil != null)
                    _DB.ParametroAdicionar("@stfuncionalidadeperfil", SqlDbType.Int, 4, p_stfuncionalidadeperfil, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_cafuncionalidade_perfil");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public DataSet RetornarAssociacao(int? p_idperfil, int? p_idfuncionalidade, string p_flperfilassociado)
        {
            //Retorna um DataSet - stplst_caperfil_funcionalidade
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idperfil != null)
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                if (p_idfuncionalidade != null)
                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                if (p_flperfilassociado != null && p_flperfilassociado.Length > 0)
                    _DB.ParametroAdicionar("@flperfilassociado", SqlDbType.Char, 1, p_flperfilassociado, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_caperfil_funcionalidade");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public void Gravar(clsEntFuncionalidadePerfil _entFuncionalidadePerfil)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entFuncionalidadePerfil.stfuncionalidadeperfil == null)
                    _Erro += "Campo 'stfuncionalidadeperfil' não informado.";
                if (_entFuncionalidadePerfil.dhsituacao == null)
                    _Erro += "Campo 'dhsituacao' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, _entFuncionalidadePerfil.idfuncionalidade, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, _entFuncionalidadePerfil.idperfil, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@stfuncionalidadeperfil", SqlDbType.Int, 4, _entFuncionalidadePerfil.stfuncionalidadeperfil, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@dhsituacao", SqlDbType.DateTime, 8, _entFuncionalidadePerfil.dhsituacao, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entFuncionalidadePerfil.idoperador, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.SQL_SP_Exec("stpins_cafuncionalidade_perfil");
                    }
                    else
                    {
                        _DB.SQL_SP_Exec("stpupd_cafuncionalidade_perfil");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_idfuncionalidade, int p_idperfil)
        {
            try
            {
                //Consistência
                if (p_idfuncionalidade == null)
                    throw new Exception("Campo 'Id Funcionalidade' não informado.");
                else if (p_idperfil == null)
                    throw new Exception("Campo 'Id Perfil' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idperfil", SqlDbType.Int, 4, p_idperfil, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_cafuncionalidade_perfil");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        


    }
}