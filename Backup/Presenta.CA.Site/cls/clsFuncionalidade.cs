using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsFuncionalidade
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

        public DataSet Retornar(int? p_idfuncionalidade, string p_dsfuncionalidade)
        {
            //Retornar dataset - funcionalidade
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idfuncionalidade != null)
                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                if (p_dsfuncionalidade != null && p_dsfuncionalidade.Length != 0)
                    _DB.ParametroAdicionar("@dsfuncionalidade", SqlDbType.VarChar, 45, p_dsfuncionalidade, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stpsel_cafuncionalidade");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public List<clsEntFuncionalidade> Retornar(int? p_idfuncionalidade)
        {
            //Retornar lista - funcionalidade
            List<clsEntFuncionalidade> _lstEntFuncionalidade = new List<clsEntFuncionalidade>();
            try
            {
                clsDB _DB = new clsDB();
                _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                SqlDataReader _dr = _DB.SQL_SP_RetornoDR("stpsel_cafuncionalidade");
                while (_dr.Read())
                {
                    clsEntFuncionalidade _EntFuncionalidade = new clsEntFuncionalidade();

                    _EntFuncionalidade.idfuncionalidade = _dr["idfuncionalidade"] == null ? new int?() : Convert.ToInt32(_dr["idfuncionalidade"]); 
                    _EntFuncionalidade.idaplicativo = Convert.ToInt32(_dr["idaplicativo"]);
                    _EntFuncionalidade.stfuncionalidade = Convert.ToInt32((_dr["stfuncionalidade"]));
                    _EntFuncionalidade.dsfuncionalidade = _dr["dsfuncionalidade"].ToString();
                    _EntFuncionalidade.idoperador = Convert.ToInt32(_dr["idoperador"]);
                    _EntFuncionalidade.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);

                    _lstEntFuncionalidade.Add(_EntFuncionalidade);
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _lstEntFuncionalidade;
        }

        public DataSet RetornarLista(int? p_idfuncionalidade, int? p_idaplicativo, int? p_idsistema, string p_dsfuncionalidade)
        {
            //Retorna um DataSet - casistemas
            DataSet _ds = new DataSet();
            try
            {
                clsDB _DB = new clsDB();

                if (p_idfuncionalidade != null)
                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                if (p_idaplicativo != null)
                    _DB.ParametroAdicionar("@idaplicativo", SqlDbType.Int, 4, p_idaplicativo, ParameterDirection.Input);
                if (p_idsistema != null)
                    _DB.ParametroAdicionar("@idsistema", SqlDbType.Int, 4, p_idsistema, ParameterDirection.Input);
                if (p_dsfuncionalidade != null && p_dsfuncionalidade.Length != 0)
                    _DB.ParametroAdicionar("@dsfuncionalidade", SqlDbType.VarChar, 45, p_dsfuncionalidade, ParameterDirection.Input);

                _ds = _DB.SQL_SP_RetornoDS("stplst_cafuncionalidade_by_func_sist_apl");
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
            return _ds;
        }

        public void Gravar(clsEntFuncionalidade _entFuncionalidade)
        {
            try
            {
                //Consistência
                _Erro = string.Empty;
                if (_entFuncionalidade.idaplicativo == null)
                    _Erro += "Campo 'Id Aplicativo' não informado.";
                if (_entFuncionalidade.stfuncionalidade == null)
                    _Erro += "Campo 'Situação' não informado.";
                if (_entFuncionalidade.dsfuncionalidade == null)
                    _Erro += "Campo 'Descrição da Funcionalidade' não informado.";

                if (_Erro.Length > 0)
                    throw new Exception(_Erro);
                else
                {
                    clsDB _DB = new clsDB();

                    _DB.ParametroAdicionar("@idaplicativo", SqlDbType.Int, 4, _entFuncionalidade.idaplicativo, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@stfuncionalidade", SqlDbType.Int, 4, _entFuncionalidade.stfuncionalidade, ParameterDirection.Input);                  
                    _DB.ParametroAdicionar("@dsfuncionalidade", SqlDbType.VarChar, 45, _entFuncionalidade.dsfuncionalidade, ParameterDirection.Input);
                    _DB.ParametroAdicionar("@idoperador", SqlDbType.Int, 4, _entFuncionalidade.idoperador, ParameterDirection.Input);

                    if (_Insercao)
                    {
                        _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, _entFuncionalidade.idfuncionalidade, ParameterDirection.Output);
                        _DB.SQL_SP_Exec("stpins_cafuncionalidade");
                    }
                    else
                    {
                        _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, _entFuncionalidade.idfuncionalidade, ParameterDirection.Input);
                        _DB.SQL_SP_Exec("stpupd_cafuncionalidade");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }

        public void Excluir(int p_idfuncionalidade)
        {
            try
            {
                //Consistência
                if (p_idfuncionalidade == null)
                    throw new Exception("Campo 'Id Funcionalidade' não informado.");
                else
                {
                    clsDB _DB = new clsDB();
                    _DB.ParametroAdicionar("@idfuncionalidade", SqlDbType.Int, 4, p_idfuncionalidade, ParameterDirection.Input);
                    _DB.SQL_SP_Exec("stpdel_cafuncionalidade");
                }
            }
            catch (Exception ex)
            {
                _Erro = ex.Message;
            }
        }


    }
}