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
    public class CaSituacaoFuncionalidadeModel
    {
        public int StFuncionalidade { get; set; }
        public string DsSituacaoFuncionalidade { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public DateTime DhAtualizacao { get; set; }

        private static List<CaSituacaoFuncionalidadeModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaSituacaoFuncionalidadeModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaSituacaoFuncionalidadeModel()
                    {
                        StFuncionalidade = dr["stfuncionalidade"].ToInt(),
                        DsSituacaoFuncionalidade = dr["dssituacaofuncionalidade"].ToString(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime()
                    });
            }

            return lista;
        }

        public List<CaSituacaoFuncionalidadeModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaLst_casituacao_funcionalidade_Oracle : Constants.StpCaLst_casituacao_funcionalidade), CommandType.StoredProcedure, connection);

                    if (this.StFuncionalidade > 0)
                    {
                        DbConnect.CreateInputParameter("stfuncionalidade", this.StFuncionalidade, command);
                    }

                    if (!String.IsNullOrEmpty(this.DsSituacaoFuncionalidade))
                    {
                        DbConnect.CreateInputParameter("dssituacaofuncionalidade", this.DsSituacaoFuncionalidade.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    }

                    if (this.IdOperadorAtualizacao > 0)
                    {
                        DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0)))
                    {
                        DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaSituacaoFuncionalidadeModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaSituacaoFuncionalidadeModel>();

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

        public CaSituacaoFuncionalidadeModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaSel_casituacao_funcionalidade_Oracle : Constants.StpCaSel_casituacao_funcionalidade), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("stfuncionalidade", this.StFuncionalidade, command);

                    var dr = command.ExecuteReader();

                    List<CaSituacaoFuncionalidadeModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaSituacaoFuncionalidadeModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaLst_casituacao_funcionalidade_Oracle : Constants.StpCaLst_casituacao_funcionalidade), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputOutputParameter("stfuncionalidade", this.StFuncionalidade, command);
                    DbConnect.CreateInputParameter("dssituacaofuncionalidade", this.DsSituacaoFuncionalidade.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("stfuncionalidade", command).ToString()))
                    {
                        this.StFuncionalidade = DbConnect.GetOutputParameterValue("stfuncionalidade", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaUpd_casituacao_funcionalidade_Oracle : Constants.StpCaUpd_casituacao_funcionalidade), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("stfuncionalidade", this.StFuncionalidade, command);

                    if (!String.IsNullOrEmpty(this.DsSituacaoFuncionalidade))
                    {
                        DbConnect.CreateInputParameter("dssituacaofuncionalidade", this.DsSituacaoFuncionalidade.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    }

                    if (this.IdOperadorAtualizacao > 0)
                    {
                        DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0)))
                    {
                        DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    }

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaDel_casituacao_funcionalidade_Oracle : Constants.StpCaDel_casituacao_funcionalidade), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("stfuncionalidade", this.StFuncionalidade, command);

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
