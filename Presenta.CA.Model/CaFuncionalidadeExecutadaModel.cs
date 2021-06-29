using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Presenta.Common.Util;
using Presenta.DBConnection;

namespace Presenta.CA.Model
{
    public class CaFuncionalidadeExecutadaModel
    {
        public int IdCaFuncionalidadeExecutada { get; set; }
        public DateTime DhExecucao { get; set; }
        public string DsComplemento { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public int IdFuncionalidade { get; set; }

        private static List<CaFuncionalidadeExecutadaModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaFuncionalidadeExecutadaModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaFuncionalidadeExecutadaModel()
                    {
                        IdCaFuncionalidadeExecutada = dr["idcafuncionalidade_executada"].ToInt(),
                        DhExecucao = dr["dhexecucao"].ToDateTime(),
                        DsComplemento = dr["dscomplemento"].ToString(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        IdFuncionalidade = dr["idfuncionalidade"].ToInt()
                    });
            }

            return lista;
        }

        public List<CaFuncionalidadeExecutadaModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaLst_cafuncionalidade_executada_Oracle : Constants.StpCaLst_cafuncionalidade_executada), CommandType.StoredProcedure, connection);

                    if (!DateTime.Equals(this.DhExecucao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhexecucao", this.DhExecucao, command); }
                    else { DbConnect.CreateInputParameter("dhexecucao", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.DsComplemento)) { DbConnect.CreateInputParameter("dscomplemento", this.DsComplemento.Trim(), command, TextParamTypeEnum.VarChar, 255); }
                    else { DbConnect.CreateInputParameter("dscomplemento", DBNull.Value, command, TextParamTypeEnum.VarChar, 255); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (this.IdFuncionalidade > 0) { DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command); }
                    else { DbConnect.CreateInputParameter("idfuncionalidade", DBNull.Value, command); }

                    if (this.IdCaFuncionalidadeExecutada > 0) { DbConnect.CreateInputParameter(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "idcafunc_executada" : "idcafuncionalidade_executada", this.IdCaFuncionalidadeExecutada, command); }
                    else { DbConnect.CreateInputParameter(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "idcafunc_executada" : "idcafuncionalidade_executada", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaFuncionalidadeExecutadaModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaFuncionalidadeExecutadaModel>();

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return lista;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    DbConnect.CloseConnection(connection);
                }
            }
        }

        public CaFuncionalidadeExecutadaModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaSel_cafuncionalidade_executada_Oracle : Constants.StpCaSel_cafuncionalidade_executada), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "idcafunc_executada" : "idcafuncionalidade_executada", this.IdCaFuncionalidadeExecutada, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaFuncionalidadeExecutadaModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaFuncionalidadeExecutadaModel>();

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return lista.Count > 0 ? lista.First() : null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    DbConnect.CloseConnection(connection);
                }
            }
        }

        public void Inserir()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaIns_cafuncionalidade_executada_Oracle : Constants.StpCaIns_cafuncionalidade_executada), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("dhexecucao", this.DhExecucao, command);
                    DbConnect.CreateInputParameter("dscomplemento", this.DsComplemento.Trim(), command, TextParamTypeEnum.VarChar, 255);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command);
                    DbConnect.CreateInputOutputParameter(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "idcafunc_executada" : "idcafuncionalidade_executada", this.IdCaFuncionalidadeExecutada, command);

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "idcafunc_executada" : "idcafuncionalidade_executada", command).ToString()))
                    {
                        this.IdCaFuncionalidadeExecutada = DbConnect.GetOutputParameterValue(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "idcafunc_executada" : "idcafuncionalidade_executada", command).ToString().ToInt();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    DbConnect.CloseConnection(connection);
                }
            }
        }

        public void Atualizar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaUpd_cafuncionalidade_executada_Oracle : Constants.StpCaUpd_cafuncionalidade_executada), CommandType.StoredProcedure, connection, transaction);

                    if (!DateTime.Equals(this.DhExecucao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhexecucao", this.DhExecucao, command); }
                    else { DbConnect.CreateInputParameter("dhexecucao", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.DsComplemento)) { DbConnect.CreateInputParameter("dscomplemento", this.DsComplemento.Trim(), command, TextParamTypeEnum.VarChar, 255); }
                    else { DbConnect.CreateInputParameter("dscomplemento", DBNull.Value, command, TextParamTypeEnum.VarChar, 255); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (this.IdFuncionalidade > 0) { DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command); }
                    else { DbConnect.CreateInputParameter("idfuncionalidade", DBNull.Value, command); }

                    DbConnect.CreateInputParameter(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "idcafunc_executada" : "idcafuncionalidade_executada", this.IdCaFuncionalidadeExecutada, command);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    DbConnect.CloseConnection(connection);
                }
            }
        }

        public void Excluir()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaDel_cafuncionalidade_executada_Oracle : Constants.StpCaDel_cafuncionalidade_executada), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "idcafunc_executada" : "idcafuncionalidade_executada", this.IdCaFuncionalidadeExecutada, command);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    DbConnect.CloseConnection(connection);
                }
            }
        }
    }
}
