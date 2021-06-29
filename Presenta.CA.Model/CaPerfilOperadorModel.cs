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
    public class CaPerfilOperadorModel
    {
        public int IdPerfil { get; set; }
        public int IdOperador { get; set; }
        public int StPerfilOperador { get; set; }
        public DateTime DhSituacao { get; set; }
        public DateTime DhAtualizacao { get; set; }
        public int IdOperadorAtualizacao { get; set; }

        private static List<CaPerfilOperadorModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaPerfilOperadorModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaPerfilOperadorModel()
                    {
                        IdPerfil = dr["idperfil"].ToInt(),
                        IdOperador = dr["idoperador"].ToInt(),
                        StPerfilOperador = dr["stperfiloperador"].ToInt(),
                        DhSituacao = dr["dhsituacao"].ToDateTime(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime(),
                        IdOperadorAtualizacao = dr["idoperadoratualizacao"].ToInt()
                    });
            }

            return lista;
        }

        public List<CaPerfilOperadorModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_caperfil_operador), CommandType.StoredProcedure, connection);

                    if (this.IdPerfil > 0) { DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command); }
                    else { DbConnect.CreateInputParameter("idperfil", DBNull.Value, command); }

                    if (this.IdOperador > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperador, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (this.StPerfilOperador > 0) { DbConnect.CreateInputParameter("stperfiloperador", this.StPerfilOperador, command); }
                    else { DbConnect.CreateInputParameter("stperfiloperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhSituacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhsituacao", this.DhSituacao, command); }
                    else { DbConnect.CreateInputParameter("dhsituacao", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperadoratualizacao", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperadoratualizacao", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaPerfilOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaPerfilOperadorModel>();

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

        public CaPerfilOperadorModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaSel_caperfil_operador), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperador, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaPerfilOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaPerfilOperadorModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaIns_caperfil_operador), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperador, command);
                    DbConnect.CreateInputParameter("stperfiloperador", this.StPerfilOperador, command);
                    DbConnect.CreateInputParameter("dhsituacao", this.DhSituacao, command);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    DbConnect.CreateInputParameter("idoperadoratualizacao", this.IdOperadorAtualizacao, command);

                    //DbConnect.CreateInputParameter("staprovacao", 0, command);

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

        public void Atualizar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaUpd_caperfil_operador), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperador, command);

                    if (this.StPerfilOperador > 0) { DbConnect.CreateInputParameter("stperfiloperador", this.StPerfilOperador, command); }
                    else { DbConnect.CreateInputParameter("stperfiloperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhSituacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhsituacao", this.DhSituacao, command); }
                    else { DbConnect.CreateInputParameter("dhsituacao", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperadoratualizacao", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperadoratualizacao", DBNull.Value, command); }

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaDel_caperfil_operador), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperador, command);

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
