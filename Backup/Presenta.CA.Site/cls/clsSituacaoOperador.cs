using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsSituacaoOperador
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

        public DataSet Retornar(int? p_stoperador, string p_dssituacaooperador)
        {
            //Retorna lista - Situacao Operador
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_stoperador != null)
                    _DB.ParametroAdicionar("@stoperador", SqlDbType.Int, 4, Convert.ToInt32(p_stoperador), ParameterDirection.Input);
                if (p_dssituacaooperador != null && p_dssituacaooperador.Length != 0)
                    _DB.ParametroAdicionar("@dssituacaooperador", SqlDbType.VarChar, 45, p_dssituacaooperador, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_casituacao_operador");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntSituacaoOperador> Retornar(int? p_stoperador)
        {
            //Retornar um registro - Situacao Operador
            List<clsEntSituacaoOperador> _lstEntSituacaoOperador = new List<clsEntSituacaoOperador>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@stoperador", SqlDbType.Int, 4, p_stoperador, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_casituacao_operador");
                while (_dr.Read())
                {
                    clsEntSituacaoOperador _entSituacaoOperador = new clsEntSituacaoOperador();

                    _entSituacaoOperador.stoperador = _dr["stoperador"] == null ? new int?() : Convert.ToInt32(_dr["stoperador"]); 
                    _entSituacaoOperador.dssituacaooperador = _dr["dssituacaooperador"].ToString();
                    _entSituacaoOperador.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _entSituacaoOperador.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntSituacaoOperador.Add(_entSituacaoOperador);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntSituacaoOperador;
        }

        public void Gravar(clsEntSituacaoOperador _entSituacaoOperador)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entSituacaoOperador.dssituacaooperador == null)
                    _Erro += "Campo 'Descrição da Situação' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@dssituacaooperador", SqlDbType.VarChar, 45, _entSituacaoOperador.dssituacaooperador, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entSituacaoOperador.idoperador, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@stoperador", SqlDbType.Int, 4, _entSituacaoOperador.stoperador, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_casituacao_operador");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@stoperador", SqlDbType.Int, 4, _entSituacaoOperador.stoperador, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_casituacao_operador");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_stoperador)
        {
            try
            {
                //Consistência
                if (p_stoperador == null)
                    throw new Exception("Campo 'Situação Operador' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@stoperador", SqlDbType.Int, 4, p_stoperador, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_casituacao_operador");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }


    }
}
 