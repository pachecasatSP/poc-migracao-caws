using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsSistema
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

        public DataSet Retornar(int? p_idsistema, string p_dssistema)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idsistema != null)
                    _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, p_idsistema, ParameterDirection.Input);
                if (p_dssistema != null && p_dssistema.Length != 0)
                    _DB.ParametroAdicionar("@dssistema", SqlDbType.VarChar, 45, p_dssistema, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_casistema");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntSistema> Retornar(int? p_idsistema)
        {
            //Retornar uma lista - casistema
            List<clsEntSistema> _lstEntSistema = new List<clsEntSistema>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, p_idsistema, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_casistema");
                while (_dr.Read())
                {
                    clsEntSistema _entSistema = new clsEntSistema();

                    _entSistema.idsistema = _dr["idsistema"] == null ? new int?() : Convert.ToInt32(_dr["idsistema"]); 
                    _entSistema.dssistema = _dr["dssistema"].ToString();
                    _entSistema.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _entSistema.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntSistema.Add(_entSistema);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntSistema;
        }

        public void Gravar(clsEntSistema _entSistema)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entSistema.dssistema == null)
                    _Erro += "Campo 'Descrição do sistema' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@dssistema", SqlDbType.VarChar, 45, _entSistema.dssistema, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entSistema.idoperador, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, _entSistema.idsistema, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_casistema");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, _entSistema.idsistema, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_casistema");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_idsistema)
        {
            try
            {
                //Consistência
                if (p_idsistema == null)
                    throw new Exception("Campo 'Id' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, p_idsistema, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_casistema");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }
    }
}