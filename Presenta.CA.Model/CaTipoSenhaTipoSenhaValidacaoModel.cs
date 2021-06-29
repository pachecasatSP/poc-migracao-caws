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
    public class CaTipoSenhaTipoSenhaValidacaoModel
    {
        public int IdTipoSenha { get; set; }
        public int IdTipoSenhaValidacao { get; set; }
        public int NuOrdem { get; set; }
        public int QtMinCaracteres { get; set; }
        public int QtMaxCaracteres { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public DateTime DhAtualizacao { get; set; }
        public string CdExpressaoRegular { get; set; }
        public bool Ativo { get; set; }

        private static List<CaTipoSenhaTipoSenhaValidacaoModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaTipoSenhaTipoSenhaValidacaoModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaTipoSenhaTipoSenhaValidacaoModel()
                    {
                        IdTipoSenha = dr["idtiposenha"].ToInt(),
                        IdTipoSenhaValidacao = dr["idtiposenhavalidacao"].ToInt(),
                        NuOrdem = dr["nuordem"].ToInt(),
                        QtMinCaracteres = dr["qtmincaracteres"].ToInt(),
                        QtMaxCaracteres = dr["qtmaxcaracteres"].ToInt(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime(),
                        CdExpressaoRegular = dr["cdexpressaoregular"].ToString(),
                        Ativo = dr["ativo"].ToBoolean()
                    });
            }

            return lista;
        }

        public List<CaTipoSenhaTipoSenhaValidacaoModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaLst_catiposenha_tiposenhavalidacao_Oracle : Constants.StpCaLst_catiposenha_tiposenhavalidacao), CommandType.StoredProcedure, connection);

                    if (this.IdTipoSenha > 0) { DbConnect.CreateInputParameter("idtiposenha", this.IdTipoSenha, command); }
                    else { DbConnect.CreateInputParameter("idtiposenha", DBNull.Value, command); }

                    if (this.IdTipoSenhaValidacao > 0) { DbConnect.CreateInputParameter("idtiposenhavalidacao", this.IdTipoSenhaValidacao, command); }
                    else { DbConnect.CreateInputParameter("idtiposenhavalidacao", DBNull.Value, command); }

                    if (this.NuOrdem > 0) { DbConnect.CreateInputParameter("nuordem", this.NuOrdem, command); }
                    else { DbConnect.CreateInputParameter("nuordem", DBNull.Value, command); }

                    if (this.QtMinCaracteres > 0) { DbConnect.CreateInputParameter("qtmincaracteres", this.QtMinCaracteres, command); }
                    else { DbConnect.CreateInputParameter("qtmincaracteres", DBNull.Value, command); }

                    if (this.QtMaxCaracteres > 0) { DbConnect.CreateInputParameter("qtmaxcaracteres", this.QtMaxCaracteres, command); }
                    else { DbConnect.CreateInputParameter("qtmaxcaracteres", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaTipoSenhaTipoSenhaValidacaoModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaTipoSenhaTipoSenhaValidacaoModel>();

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

        public CaTipoSenhaTipoSenhaValidacaoModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaSel_catiposenha_tiposenhavalidacao_Oracle : Constants.StpCaSel_catiposenha_tiposenhavalidacao), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idtiposenha", this.IdTipoSenha, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaTipoSenhaTipoSenhaValidacaoModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaTipoSenhaTipoSenhaValidacaoModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaLst_catiposenha_tiposenhavalidacao_Oracle : Constants.StpCaLst_catiposenha_tiposenhavalidacao), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputOutputParameter("idtiposenha", this.IdTipoSenha, command);
                    DbConnect.CreateInputParameter("idtiposenhavalidacao", this.IdTipoSenhaValidacao, command);
                    DbConnect.CreateInputParameter("nuordem", this.NuOrdem, command);
                    DbConnect.CreateInputParameter("qtmincaracteres", this.QtMinCaracteres, command);
                    DbConnect.CreateInputParameter("qtmaxcaracteres", this.QtMaxCaracteres, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("idtiposenha", command).ToString()))
                    {
                        this.IdTipoSenha = DbConnect.GetOutputParameterValue("idtiposenha", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaUpd_catiposenha_tiposenhavalidacao_Oracle : Constants.StpCaUpd_catiposenha_tiposenhavalidacao), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idtiposenha", this.IdTipoSenha, command);

                    if (this.IdTipoSenhaValidacao > 0) { DbConnect.CreateInputParameter("idtiposenhavalidacao", this.IdTipoSenhaValidacao, command); }
                    else { DbConnect.CreateInputParameter("idtiposenhavalidacao", DBNull.Value, command); }

                    if (this.NuOrdem > 0) { DbConnect.CreateInputParameter("nuordem", this.NuOrdem, command); }
                    else { DbConnect.CreateInputParameter("nuordem", DBNull.Value, command); }

                    if (this.QtMinCaracteres > 0) { DbConnect.CreateInputParameter("qtmincaracteres", this.QtMinCaracteres, command); }
                    else { DbConnect.CreateInputParameter("qtmincaracteres", DBNull.Value, command); }

                    if (this.QtMaxCaracteres > 0) { DbConnect.CreateInputParameter("qtmaxcaracteres", this.QtMaxCaracteres, command); }
                    else { DbConnect.CreateInputParameter("qtmaxcaracteres", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    DbConnect.CreateInputParameter("ativo", this.Ativo, command);

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaDel_catiposenha_tiposenhavalidacao_Oracle : Constants.StpCaDel_catiposenha_tiposenhavalidacao), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idtiposenha", this.IdTipoSenha, command);

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
