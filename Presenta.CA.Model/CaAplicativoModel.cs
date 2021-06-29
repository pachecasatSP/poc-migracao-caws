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
    public class CaAplicativoModel
    {
        public int IdAplicativo { get; set; }
        public int IdSistema { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public string DsAplicativo { get; set; }
        public DateTime DhAtualizacao { get; set; }

        private static List<CaAplicativoModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaAplicativoModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaAplicativoModel()
                    {
                        IdAplicativo = dr["idaplicativo"].ToInt(),
                        IdSistema = dr["idsistema"].ToInt(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        DsAplicativo = dr["dsaplicativo"].ToString(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime()
                    });
            }

            return lista;
        }

        public List<CaAplicativoModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_caaplicativo), CommandType.StoredProcedure, connection);

                    if (this.IdSistema > 0) { DbConnect.CreateInputParameter("idsistema", this.IdSistema, command); }
                    else { DbConnect.CreateInputParameter("idsistema", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.DsAplicativo)) { DbConnect.CreateInputParameter("dsaplicativo", this.DsAplicativo.Trim(), command, TextParamTypeEnum.VarChar, 45); }
                    else { DbConnect.CreateInputParameter("dsaplicativo", DBNull.Value, command, TextParamTypeEnum.VarChar, 45); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (this.IdAplicativo > 0) { DbConnect.CreateInputParameter("idaplicativo", this.IdAplicativo, command); }
                    else { DbConnect.CreateInputParameter("idaplicativo", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaAplicativoModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaAplicativoModel>();

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

        public CaAplicativoModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaSel_caaplicativo), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idaplicativo", this.IdAplicativo, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaAplicativoModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaAplicativoModel>();

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

        public List<CaAplicativoModel> SelecionarPorSistema()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand(string.Format("Select * From caaplicativo where idsistema = {0}", IdSistema), CommandType.Text, connection);
                  
                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaAplicativoModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaAplicativoModel>();

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

        public void Inserir()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaIns_caaplicativo), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idsistema", this.IdSistema, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    DbConnect.CreateInputParameter("dsaplicativo", this.DsAplicativo.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    DbConnect.CreateInputOutputParameter("idaplicativo", this.IdAplicativo, command);

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("idaplicativo", command).ToString()))
                    {
                        this.IdAplicativo = DbConnect.GetOutputParameterValue("idaplicativo", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaUpd_caaplicativo), CommandType.StoredProcedure, connection, transaction);

                    if (this.IdSistema > 0) { DbConnect.CreateInputParameter("idsistema", this.IdSistema, command); }
                    else { DbConnect.CreateInputParameter("idsistema", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.DsAplicativo)) { DbConnect.CreateInputParameter("dsaplicativo", this.DsAplicativo.Trim(), command, TextParamTypeEnum.VarChar, 45); }
                    else { DbConnect.CreateInputParameter("dsaplicativo", DBNull.Value, command, TextParamTypeEnum.VarChar, 45); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    DbConnect.CreateInputParameter("idaplicativo", this.IdAplicativo, command);

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaDel_caaplicativo), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idaplicativo", this.IdAplicativo, command);

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
