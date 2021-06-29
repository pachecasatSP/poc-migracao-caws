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
    public class CaFuncionalidadePerfilModel
    {
        public int IdFuncionalidade { get; set; }
        public int IdPerfil { get; set; }
        public int StFuncionalidadePerfil { get; set; }
        public DateTime DhSituacao { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public DateTime DhAtualizacao { get; set; }

        private static List<CaFuncionalidadePerfilModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaFuncionalidadePerfilModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaFuncionalidadePerfilModel()
                    {
                        IdFuncionalidade = dr["idfuncionalidade"].ToInt(),
                        IdPerfil = dr["idperfil"].ToInt(),
                        StFuncionalidadePerfil = dr["stfuncionalidadeperfil"].ToInt(),
                        DhSituacao = dr["dhsituacao"].ToDateTime(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime()
                    });
            }

            return lista;
        }

        public List<CaFuncionalidadePerfilModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaLst_cafuncionalidade_perfil_Oracle : Constants.StpCaLst_cafuncionalidade_perfil), CommandType.StoredProcedure, connection);

                    if (this.IdFuncionalidade > 0) { DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command); }
                    else { DbConnect.CreateInputParameter("idfuncionalidade", DBNull.Value, command); }

                    if (this.IdPerfil > 0) { DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command); }
                    else { DbConnect.CreateInputParameter("idperfil", DBNull.Value, command); }

                    if (this.StFuncionalidadePerfil > 0) { DbConnect.CreateInputParameter("stfuncionalidadeperfil", this.StFuncionalidadePerfil, command); }
                    else { DbConnect.CreateInputParameter("stfuncionalidadeperfil", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhSituacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhsituacao", this.DhSituacao, command); }
                    else { DbConnect.CreateInputParameter("dhsituacao", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaFuncionalidadePerfilModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaFuncionalidadePerfilModel>();

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

        public CaFuncionalidadePerfilModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaSel_cafuncionalidade_perfil_Oracle : Constants.StpCaSel_cafuncionalidade_perfil), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command);
                    DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaFuncionalidadePerfilModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaFuncionalidadePerfilModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaIns_cafuncionalidade_perfil_Oracle : Constants.StpCaIns_cafuncionalidade_perfil), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command);
                    DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command);
                    DbConnect.CreateInputParameter("stfuncionalidadeperfil", this.StFuncionalidadePerfil, command);
                    DbConnect.CreateInputParameter("dhsituacao", this.DhSituacao, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("idfuncionalidade", command).ToString()))
                    {
                        this.IdFuncionalidade = DbConnect.GetOutputParameterValue("idfuncionalidade", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaUpd_cafuncionalidade_perfil_Oracle : Constants.StpCaUpd_cafuncionalidade_perfil), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command);
                    DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command);

                    if (this.StFuncionalidadePerfil > 0) { DbConnect.CreateInputParameter("stfuncionalidadeperfil", this.StFuncionalidadePerfil, command); }
                    else { DbConnect.CreateInputParameter("stfuncionalidadeperfil", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhSituacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhsituacao", this.DhSituacao, command); }
                    else { DbConnect.CreateInputParameter("dhsituacao", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaDel_cafuncionalidade_perfil_Oracle : Constants.StpCaDel_cafuncionalidade_perfil), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command);
                    DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command);

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
