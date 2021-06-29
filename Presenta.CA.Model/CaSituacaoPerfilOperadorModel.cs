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
    public class CaSituacaoPerfilOperadorModel
    {
        public int StPerfilOperador { get; set; }
        public string DsPerfilOperador { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public DateTime DhAtualizacao { get; set; }

        private static List<CaSituacaoPerfilOperadorModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaSituacaoPerfilOperadorModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaSituacaoPerfilOperadorModel()
                    {
                        StPerfilOperador = dr["stperfiloperador"].ToInt(),
                        DsPerfilOperador = dr["dsperfiloperador"].ToString(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime()
                    });
            }

            return lista;
        }

        public List<CaSituacaoPerfilOperadorModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaLst_casituacao_perfil_operador_Oracle : Constants.StpCaLst_casituacao_perfil_operador), CommandType.StoredProcedure, connection);

                    if (this.StPerfilOperador > 0)
                    {
                        DbConnect.CreateInputParameter("stperfiloperador", this.StPerfilOperador, command);
                    }

                    if (!String.IsNullOrEmpty(this.DsPerfilOperador))
                    {
                        DbConnect.CreateInputParameter("dsperfiloperador", this.DsPerfilOperador.Trim(), command, TextParamTypeEnum.VarChar, 45);
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

                    List<CaSituacaoPerfilOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaSituacaoPerfilOperadorModel>();

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

        public CaSituacaoPerfilOperadorModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaSel_casituacao_perfil_operador_Oracle : Constants.StpCaSel_casituacao_perfil_operador), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("stperfiloperador", this.StPerfilOperador, command);

                    var dr = command.ExecuteReader();

                    List<CaSituacaoPerfilOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaSituacaoPerfilOperadorModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaIns_casituacao_perfil_operador_Oracle : Constants.StpCaIns_casituacao_perfil_operador), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputOutputParameter("stperfiloperador", this.StPerfilOperador, command);
                    DbConnect.CreateInputParameter("dsperfiloperador", this.DsPerfilOperador.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("stperfiloperador", command).ToString()))
                    {
                        this.StPerfilOperador = DbConnect.GetOutputParameterValue("stperfiloperador", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaUpd_casituacao_perfil_operador_Oracle : Constants.StpCaUpd_casituacao_perfil_operador), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("stperfiloperador", this.StPerfilOperador, command);

                    if (!String.IsNullOrEmpty(this.DsPerfilOperador))
                    {
                        DbConnect.CreateInputParameter("dsperfiloperador", this.DsPerfilOperador.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    }

                    if (this.IdOperadorAtualizacao > 0)
                    {
                        DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    }

                    if (this.DhAtualizacao != null)
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaDel_casituacao_perfil_operador_Oracle : Constants.StpCaDel_casituacao_perfil_operador), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("stperfiloperador", this.StPerfilOperador, command);

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
