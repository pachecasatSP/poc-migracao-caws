using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Presenta.Common.Util;
using Presenta.DBConnection;
using Presenta.CA.Model.Enum;

namespace Presenta.CA.Model
{
    public class CaFuncionalidadeModel
    {
        public int IdFuncionalidade { get; set; }
        public int IdAplicativo { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public int StFuncionalidade { get; set; }
        public string DsFuncionalidade { get; set; }
        public DateTime DhAtualizacao { get; set; }

        private static List<CaFuncionalidadeModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaFuncionalidadeModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaFuncionalidadeModel()
                    {
                        IdFuncionalidade = dr["idfuncionalidade"].ToInt(),
                        IdAplicativo = dr["idaplicativo"].ToInt(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        StFuncionalidade = dr["stfuncionalidade"].ToInt(),
                        DsFuncionalidade = dr["dsfuncionalidade"].ToString(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime()
                    });
            }

            return lista;
        }

        public List<CaFuncionalidadeModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_cafuncionalidade), CommandType.StoredProcedure, connection);

                    if (this.IdAplicativo > 0) { DbConnect.CreateInputParameter("idaplicativo", this.IdAplicativo, command); }
                    else { DbConnect.CreateInputParameter("idaplicativo", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (this.StFuncionalidade > 0) { DbConnect.CreateInputParameter("stfuncionalidade", this.StFuncionalidade, command); }
                    else { DbConnect.CreateInputParameter("stfuncionalidade", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.DsFuncionalidade)) { DbConnect.CreateInputParameter("dsfuncionalidade", this.DsFuncionalidade.Trim(), command, TextParamTypeEnum.VarChar, 255); }
                    else { DbConnect.CreateInputParameter("dsfuncionalidade", DBNull.Value, command, TextParamTypeEnum.VarChar, 255); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (this.IdFuncionalidade > 0) { DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command); }
                    else { DbConnect.CreateInputParameter("idfuncionalidade", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaFuncionalidadeModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaFuncionalidadeModel>();

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

        public List<CaFuncionalidadeModel> ListarPorPerfil(int idPerfil, int idSistema, int idAplicativo)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaLst_cafuncionalidade_por_perfil_Oracle : Constants.StpCaLst_cafuncionalidade_por_perfil), CommandType.StoredProcedure, connection);

                    if (idPerfil > 0) { DbConnect.CreateInputParameter("idperfil", idPerfil, command); }
                    else { DbConnect.CreateInputParameter("idperfil", DBNull.Value, command); }

                    if (idSistema > 0) { DbConnect.CreateInputParameter("idsistema", idSistema, command); }
                    else { DbConnect.CreateInputParameter("idsistema", DBNull.Value, command); }

                    if (idAplicativo > 0) { DbConnect.CreateInputParameter("idaplicativo", idAplicativo, command); }
                    else { DbConnect.CreateInputParameter("idaplicativo", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaFuncionalidadeModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaFuncionalidadeModel>();

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

        public CaFuncionalidadeModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaSel_cafuncionalidade), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaFuncionalidadeModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaFuncionalidadeModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaIns_cafuncionalidade), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idaplicativo", this.IdAplicativo, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    DbConnect.CreateInputParameter("stfuncionalidade", this.StFuncionalidade, command);
                    DbConnect.CreateInputParameter("dsfuncionalidade", this.DsFuncionalidade.Trim(), command, TextParamTypeEnum.VarChar, 255);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    DbConnect.CreateInputOutputParameter("idfuncionalidade", this.IdFuncionalidade, command);

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaUpd_cafuncionalidade), CommandType.StoredProcedure, connection, transaction);

                    if (this.IdAplicativo > 0) { DbConnect.CreateInputParameter("idaplicativo", this.IdAplicativo, command); }
                    else { DbConnect.CreateInputParameter("idaplicativo", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (this.StFuncionalidade > 0) { DbConnect.CreateInputParameter("stfuncionalidade", this.StFuncionalidade, command); }
                    else { DbConnect.CreateInputParameter("stfuncionalidade", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.DsFuncionalidade)) { DbConnect.CreateInputParameter("dsfuncionalidade", this.DsFuncionalidade.Trim(), command, TextParamTypeEnum.VarChar, 255); }
                    else { DbConnect.CreateInputParameter("dsfuncionalidade", DBNull.Value, command, TextParamTypeEnum.VarChar, 255); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command);

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaDel_cafuncionalidade), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idfuncionalidade", this.IdFuncionalidade, command);

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

        public List<CaFuncionalidadeModel> NotListarAtivosPorPerfil(int idPerfil, int idSistema, int idAplicativo)
        {
            try
            {
                List<CaFuncionalidadeModel> funcionalidadesPorPerfil = this.ListarPorPerfil(idPerfil, idSistema, idAplicativo);
                List<CaFuncionalidadeModel> funcionalidades = this.Listar();
                var lista = funcionalidades.Where(x => !funcionalidadesPorPerfil.Any(l => l.IdFuncionalidade == x.IdFuncionalidade) && x.IdAplicativo == idAplicativo).ToList();
                return lista.FindAll(p => p.StFuncionalidade == (int)CaSituacaoFuncionalidadeEnum.Ativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
