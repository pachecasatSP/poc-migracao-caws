using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using com.CryptoTools;

namespace Controle_Acesso
{
    public class clsDB
    {

        static SqlConnection _conexao;
        List<SqlParameter> _ParamSQL;

        /// <summary>
        /// No construtor da classe, abrir conexão com o banco de dados
        /// </summary>
        public clsDB()
        {
            ConexaoAbrir();
        }

        private void ConexaoAbrir()
        {
            //Descriptografar string de conexão
            CryptoBase64 _crypto64 = new CryptoBase64();
            CryptoDES _cryptoDes = new CryptoDES();
            _crypto64.Decrypt(ConfigurationManager.ConnectionStrings["ControleAcesso"].ToString());
            _cryptoDes.DeriveKeyFromPassword("present@passw$rdK&y", null, 1);
            string _connectionString = _cryptoDes.DecryptText(_crypto64.Result);
            //string _connectionString = ConfigurationManager.ConnectionStrings["controle_acesso"].ToString();
            //
            _conexao = new SqlConnection(_connectionString);
            _conexao.Open();
        }

        private void ConexaoFechar()
        {
            if (_conexao != null && _conexao.State == System.Data.ConnectionState.Open)
                _conexao.Close();
        }

        /// <summary>
        /// Zerar parâmetros na instância atual da classe
        /// </summary>
        public void ParametroZerar()
        {
            _ParamSQL = new List<SqlParameter>();
        }

        /// <summary>
        /// Adicionar parâmetros para a execução de stored procedura
        /// </summary>
        /// <param name="p_NmParam">Nome do parâmetro</param>
        /// <param name="p_TypeParam">Tipo do parâmetro</param>
        /// <param name="p_SizeParam">Tamanho do parâmetro</param>
        /// <param name="p_ValueParam">Valor</param>
        /// <param name="p_Direction">Direcao</param>
        public void ParametroAdicionar(string p_NmParam, SqlDbType p_TypeParam, int p_SizeParam, object p_ValueParam, ParameterDirection p_Direction)
        {
            if (_ParamSQL == null)
                _ParamSQL = new List<SqlParameter>();
            SqlParameter _param = new SqlParameter();
            _param.ParameterName = p_NmParam;
            _param.SqlDbType = p_TypeParam;
            _param.Direction = p_Direction;
            if (p_SizeParam > 0)
                _param.Size = p_SizeParam;
            _param.Value = p_ValueParam;
            _ParamSQL.Add(_param);
        }

        /// <summary>
        /// Retornar dados via comando SQL
        /// </summary>
        /// <param name="p_Query">Query SQL</param>
        /// <returns>Data Reader</returns>
        public SqlDataReader SQL_RetornoDR(string p_Query)
        {
            SqlCommand _command = new SqlCommand(p_Query, _conexao);
            _command.CommandType = CommandType.Text;
            SqlDataReader _dr = _command.ExecuteReader();
            return _dr;
        }

        /// <summary>
        /// Retorna dados via stored procedure
        /// </summary>
        /// <param name="p_StoredProcedure">nome da Stored Procedure</param>
        /// <returns>DataSet</returns>
        public DataSet SQL_SP_RetornoDS(string p_StoredProcedure)
        {
            DataSet _ds = new DataSet();
            SqlCommand _command = new SqlCommand(p_StoredProcedure, _conexao);
            _command.CommandType = CommandType.StoredProcedure;
            if (_ParamSQL != null)
            {
                foreach (SqlParameter _P in _ParamSQL)
                {
                    _command.Parameters.Add(_P.ParameterName, _P.SqlDbType, _P.Size).Value = _P.Value;
                }
            }
            using (SqlDataAdapter _da = new SqlDataAdapter())
            {
                _da.SelectCommand = _command;
                _da.Fill(_ds);
            }
            return _ds;
        }

        /// <summary>
        /// Executar Stored Procedure
        /// </summary>
        /// <param name="p_StoredProcedure">nome da Stored Procedure</param>
        public void SQL_SP_Exec(string p_StoredProcedure)
        {
            try
            {
                SqlCommand _command = new SqlCommand(p_StoredProcedure, _conexao);
                _command.CommandType = CommandType.StoredProcedure;
                if (_ParamSQL != null)
                {
                    foreach (SqlParameter _P in _ParamSQL)
                    {
                        SqlParameter _param = new SqlParameter();
                        _param.ParameterName = _P.ParameterName;
                        _param.SqlDbType =  _P.SqlDbType;
                        _param.Direction = _P.Direction;
                        _param.Value = _P.Value;
                        _command.Parameters.Add(_param);

//                        _command.Parameters.Add( (_P.ParameterName, _P.SqlDbType, _P.Size).Value = _P.Value;
                    }
                }
                //Parâmetro de retorno da SP
                SqlParameter _paramOutRet = new SqlParameter();
                _paramOutRet.ParameterName = "@ReturnValue";
                _paramOutRet.SqlDbType = SqlDbType.Int;
                _paramOutRet.Direction = ParameterDirection.ReturnValue;
                _command.Parameters.Add(_paramOutRet);
                //
                _command.ExecuteReader();
                if (_paramOutRet != null && Convert.ToInt32(_paramOutRet.Value) != 0)
                    throw new Exception(clsMensagem.MensagemRetornar(Convert.ToInt32(_paramOutRet.Value)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Executar Stored Procedure com 1 parâmetro de saída
        /// </summary>
        /// <param name="p_StoredProcedure">nome da Stored Procedure</param>
        /// <param name="p_ParamOut">parâmetro SQL de saída</param>
        public void SQL_SP_Exec(string p_StoredProcedure, ref SqlParameter p_ParamOut)
        {
            try
            {
                SqlCommand _command = new SqlCommand(p_StoredProcedure, _conexao);
                _command.CommandType = CommandType.StoredProcedure;
                if (_ParamSQL != null)
                {
                    foreach (SqlParameter _P in _ParamSQL)
                    {
                        _command.Parameters.Add(_P.ParameterName, _P.SqlDbType, _P.Size).Value = _P.Value;
                    }
                }
                _command.Parameters.Add(p_ParamOut);
                //Parâmetro de retorno da SP
                SqlParameter _paramOutRet = new SqlParameter();
                _paramOutRet.ParameterName = "@ReturnValue";
                _paramOutRet.SqlDbType = SqlDbType.Int;
                _paramOutRet.Direction = ParameterDirection.ReturnValue;
                _command.Parameters.Add(_paramOutRet);
                //
                _command.ExecuteReader();
                if (_paramOutRet != null && Convert.ToInt32(_paramOutRet.Value) != 0)
                    throw new Exception(clsMensagem.MensagemRetornar(Convert.ToInt32(_paramOutRet.Value)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Executar Stored Procedure com 2 parâmetros de saída
        /// </summary>
        /// <param name="p_StoredProcedure">nome da Stored Procedure</param>
        /// <param name="p_ParamOut1">parâmetro SQL de saída 1</param>
        /// <param name="p_ParamOut2">parâmetro SQL de saída 2</param>
        public void SQL_SP_Exec(string p_StoredProcedure, ref SqlParameter p_ParamOut1, ref SqlParameter p_ParamOut2)
        {
            try
            {
                SqlCommand _command = new SqlCommand(p_StoredProcedure, _conexao);
                _command.CommandType = CommandType.StoredProcedure;
                if (_ParamSQL != null)
                {
                    foreach (SqlParameter _P in _ParamSQL)
                    {
                        _command.Parameters.Add(_P.ParameterName, _P.SqlDbType, _P.Size).Value = _P.Value;
                    }
                }
                _command.Parameters.Add(p_ParamOut1);
                _command.Parameters.Add(p_ParamOut2);
                //Parâmetro de retorno da SP
                SqlParameter _paramOutRet = new SqlParameter();
                _paramOutRet.ParameterName = "@ReturnValue";
                _paramOutRet.SqlDbType = SqlDbType.Int;
                _paramOutRet.Direction = ParameterDirection.ReturnValue;
                _command.Parameters.Add(_paramOutRet);
                //
                _command.ExecuteReader();
                if (_paramOutRet != null && Convert.ToInt32(_paramOutRet.Value) != 0)
                    throw new Exception(clsMensagem.MensagemRetornar(Convert.ToInt32(_paramOutRet.Value)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Retornar dados via stored procedure
        /// </summary>
        /// <param name="p_StoredProcedure">nome da Stored Procedure</param>
        /// <returns>Data Reader</returns>
        public SqlDataReader SQL_SP_RetornoDR(string p_StoredProcedure)
        {
            SqlCommand _command = new SqlCommand(p_StoredProcedure, _conexao);
            _command.CommandType = CommandType.StoredProcedure;
            if (_ParamSQL != null)
            {
                foreach (SqlParameter _P in _ParamSQL)
                {
                    _command.Parameters.Add(_P.ParameterName, _P.SqlDbType, _P.Size).Value = _P.Value;
                }
            }
            //Parâmetro de retorno da SP
            SqlParameter _paramOutRet = new SqlParameter();
            _paramOutRet.ParameterName = "@ReturnValue";
            _paramOutRet.SqlDbType = SqlDbType.Int;
            _paramOutRet.Direction = ParameterDirection.ReturnValue;
            _command.Parameters.Add(_paramOutRet);
            SqlDataReader _dr = _command.ExecuteReader();
            if (_paramOutRet != null && Convert.ToInt32(_paramOutRet.Value) != 0)
                throw new Exception(clsMensagem.MensagemRetornar(Convert.ToInt32(_paramOutRet.Value)));
            return _dr;
        }




    }
}