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
    public class CaHistoricoSenhaModel
    {
        public int IdHistoricoSenha { get; set; }
        public int IdOperador { get; set; }
        public DateTime DhHistorico { get; set; }
        public string CrSenha { get; set; }
        public DateTime DhCadastro { get; set; }

        private static List<CaHistoricoSenhaModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaHistoricoSenhaModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaHistoricoSenhaModel()
                    {
                        IdHistoricoSenha = dr["idhistoricosenha"].ToInt(),
                        IdOperador = dr["idoperador"].ToInt(),
                        DhHistorico = dr["dhhistorico"].ToDateTime(),
                        CrSenha = dr["crsenha"].ToString(),
                        DhCadastro = dr["dhcadastro"].ToDateTime()
                    });
            }

            return lista;
        }

        public List<CaHistoricoSenhaModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_cahistoricosenha), CommandType.StoredProcedure, connection);

                    if (this.IdOperador > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperador, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhHistorico, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhhistorico", this.DhHistorico, command); }
                    else { DbConnect.CreateInputParameter("dhhistorico", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.CrSenha)) { DbConnect.CreateInputParameter("crsenha", this.CrSenha.Trim(), command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("crsenha", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }

                    if (!DateTime.Equals(this.DhCadastro, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhcadastro", this.DhCadastro, command); }
                    else { DbConnect.CreateInputParameter("dhcadastro", DBNull.Value, command); }

                    if (this.IdHistoricoSenha > 0) { DbConnect.CreateInputParameter("idhistoricosenha", this.IdHistoricoSenha, command); }
                    else { DbConnect.CreateInputParameter("idhistoricosenha", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaHistoricoSenhaModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaHistoricoSenhaModel>();

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

        public CaHistoricoSenhaModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaSel_cahistoricosenha), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idhistoricosenha", this.IdHistoricoSenha, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaHistoricoSenhaModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaHistoricoSenhaModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaIns_cahistoricosenha), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idoperador", this.IdOperador, command);
                    DbConnect.CreateInputParameter("dhhistorico", this.DhHistorico, command);
                    DbConnect.CreateInputParameter("crsenha", this.CrSenha.Trim(), command, TextParamTypeEnum.VarChar, 100);
                    DbConnect.CreateInputParameter("dhcadastro", this.DhCadastro, command);
                    DbConnect.CreateOutputParameter("idhistoricosenha", this.IdHistoricoSenha, command);

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("idhistoricosenha", command).ToString()))
                    {
                        this.IdHistoricoSenha = DbConnect.GetOutputParameterValue("idhistoricosenha", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaUpd_cahistoricosenha), CommandType.StoredProcedure, connection, transaction);

                    if (this.IdOperador > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperador, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhHistorico, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhhistorico", this.DhHistorico, command); }
                    else { DbConnect.CreateInputParameter("dhhistorico", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.CrSenha)) { DbConnect.CreateInputParameter("crsenha", this.CrSenha.Trim(), command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("crsenha", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }

                    if (!DateTime.Equals(this.DhCadastro, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhcadastro", this.DhCadastro, command); }
                    else { DbConnect.CreateInputParameter("dhcadastro", DBNull.Value, command); }

                    DbConnect.CreateInputParameter("idhistoricosenha", this.IdHistoricoSenha, command);

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaDel_cahistoricosenha), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idhistoricosenha", this.IdHistoricoSenha, command);

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
