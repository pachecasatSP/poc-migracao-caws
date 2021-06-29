using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsAplicativo
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

        public DataSet Retornar(int? p_idaplicativo, int? p_idsistema, string p_dsaplicativo)
        {
            //Retorna um DataSet - caaplicativos
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idaplicativo != null)
                    _DB.ParametroAdicionar("@idaplicativo", SqlDbType.Int, 4, p_idaplicativo, ParameterDirection.Input);
                if (p_idsistema != null)
                    _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, p_idsistema, ParameterDirection.Input);
                if (p_dsaplicativo != null && p_dsaplicativo.Length != 0)
                    _DB.ParametroAdicionar("@dsaplicativo", SqlDbType.VarChar, 45, p_dsaplicativo, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_caaplicativo");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntAplicativo> Retornar(int? p_idaplicativo)
        {
            //Retornar uma lista - caaplicativo
            List<clsEntAplicativo> _lstEntAplicativo = new List<clsEntAplicativo>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idaplicativo", SqlDbType.Int, 4, p_idaplicativo, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_caaplicativo");
                while (_dr.Read())
                {
                    clsEntAplicativo _entAplicativo = new clsEntAplicativo();

                    _entAplicativo.idaplicativo = _dr["idaplicativo"] == null ? new int?() : Convert.ToInt32(_dr["idaplicativo"]);
                    _entAplicativo.idsistema = Convert.ToInt32(_dr["idsistema"]);
                    _entAplicativo.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _entAplicativo.dsaplicativo = _dr["dsaplicativo"].ToString();
                    _entAplicativo.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntAplicativo.Add(_entAplicativo);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntAplicativo;
        }

        public DataSet RetornarLista(int? p_idaplicativo, int? p_idsistema, string p_dsaplicativo)
        {
            //Retorna um DataSet - caaplicativo
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idaplicativo != null)
                    _DB.ParametroAdicionar("@idaplicativo", SqlDbType.Int, 4, p_idaplicativo, ParameterDirection.Input);
                if (p_idsistema != null)
                    _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, p_idsistema, ParameterDirection.Input);
                if (p_dsaplicativo != null && p_dsaplicativo.Length != 0)
                    _DB.ParametroAdicionar("@dsaplicativo", SqlDbType.VarChar, 45, p_dsaplicativo, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_caaplicativo");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public void Gravar(clsEntAplicativo _entAplicativo)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                
                if (_entAplicativo.idsistema == null)
                    _Erro += "Campo 'Id Sistema' não informado.";
                if (_entAplicativo.dsaplicativo == null)
                    _Erro += "Campo 'Descrição do Aplicativo' não informado.";
                
                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, _entAplicativo.idsistema, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@dsaplicativo", SqlDbType.VarChar, 45, _entAplicativo.dsaplicativo, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entAplicativo.idoperador, ParameterDirection.Input);
                    
                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@idaplicativo", SqlDbType.Int, 4, _entAplicativo.idaplicativo, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_caaplicativo");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@idaplicativo", SqlDbType.Int, 4, _entAplicativo.idaplicativo, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_caaplicativo");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int? p_idaplicativo)
        {
            try
            {
                //Consistência
                if (p_idaplicativo == null)
                    throw new Exception("Campo 'Id' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@idaplicativo", SqlDbType.Int, 4, p_idaplicativo, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_caaplicativo");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }
    }
}