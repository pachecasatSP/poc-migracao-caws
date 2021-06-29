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
    public class CaSistemaModel
    {
        public int IdSistema { get; set; }
        public string DsSistema { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public DateTime DhAtualizacao { get; set; }

        private static List<CaSistemaModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaSistemaModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaSistemaModel()
                    {
                        IdSistema = dr["idsistema"].ToInt(),
                        DsSistema = dr["dssistema"].ToString(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime()
                    });
            }

            return lista;
        }

        public List<CaSistemaModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_casistema), CommandType.StoredProcedure, connection);

                    if (!String.IsNullOrEmpty(this.DsSistema)) { DbConnect.CreateInputParameter("dssistema", this.DsSistema.Trim(), command, TextParamTypeEnum.VarChar, 45); }
                    else { DbConnect.CreateInputParameter("dssistema", DBNull.Value, command, TextParamTypeEnum.VarChar, 45); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1,1,1,0,0,0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (this.IdSistema > 0) { DbConnect.CreateInputParameter("idsistema", this.IdSistema, command); }
                    else { DbConnect.CreateInputParameter("idsistema", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaSistemaModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaSistemaModel>();

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

        public CaSistemaModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaSel_casistema), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idsistema", this.IdSistema, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaSistemaModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaSistemaModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaIns_casistema), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("dssistema", this.DsSistema.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    DbConnect.CreateInputOutputParameter("idsistema", this.IdSistema, command);

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("idsistema", command).ToString()))
                    {
                        this.IdSistema = DbConnect.GetOutputParameterValue("idsistema", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaUpd_casistema), CommandType.StoredProcedure, connection, transaction);

                    if (!String.IsNullOrEmpty(this.DsSistema)) { DbConnect.CreateInputParameter("dssistema", this.DsSistema.Trim(), command, TextParamTypeEnum.VarChar, 45); }
                    else { DbConnect.CreateInputParameter("dssistema", DBNull.Value, command, TextParamTypeEnum.VarChar, 45); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    DbConnect.CreateInputParameter("idsistema", this.IdSistema, command);

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaDel_casistema), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idsistema", this.IdSistema, command);

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

        public List<CaSistemaModel> ListarPorPerfil(int idPerfil)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_casistema_por_perfil), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idperfil", idPerfil, command);

                    if (!String.IsNullOrEmpty(this.DsSistema)) { DbConnect.CreateInputParameter("dssistema", this.DsSistema.Trim(), command, TextParamTypeEnum.VarChar, 45); }
                    else { DbConnect.CreateInputParameter("dssistema", DBNull.Value, command, TextParamTypeEnum.VarChar, 45); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (this.IdSistema > 0) { DbConnect.CreateInputParameter("idsistema", this.IdSistema, command); }
                    else { DbConnect.CreateInputParameter("idsistema", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaSistemaModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaSistemaModel>();

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
    }
}
