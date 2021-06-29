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
    public class CaTipoSenhaValidacaoModel
    {
        public int IdTipoSenhaValidacao { get; set; }
        public string DsTipoSenhaValidacao { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public DateTime DhAtualizacao { get; set; }

        private static List<CaTipoSenhaValidacaoModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaTipoSenhaValidacaoModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaTipoSenhaValidacaoModel()
                    {
                        IdTipoSenhaValidacao = dr["idtiposenhavalidacao"].ToInt(),
                        DsTipoSenhaValidacao = dr["dstiposenhavalidacao"].ToString(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime()
                    });
            }

            return lista;
        }

        public List<CaTipoSenhaValidacaoModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_catiposenhavalidacao), CommandType.StoredProcedure, connection);

                    if (this.IdTipoSenhaValidacao > 0)
                    {
                        DbConnect.CreateInputParameter("idtiposenhavalidacao", this.IdTipoSenhaValidacao, command);
                    }

                    if (!String.IsNullOrEmpty(this.DsTipoSenhaValidacao))
                    {
                        DbConnect.CreateInputParameter("dstiposenhavalidacao", this.DsTipoSenhaValidacao.Trim(), command, TextParamTypeEnum.VarChar, 45);
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

                    List<CaTipoSenhaValidacaoModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaTipoSenhaValidacaoModel>();

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

        public CaTipoSenhaValidacaoModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaSel_catiposenhavalidacao), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idtiposenhavalidacao", this.IdTipoSenhaValidacao, command);

                    var dr = command.ExecuteReader();

                    List<CaTipoSenhaValidacaoModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaTipoSenhaValidacaoModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_catiposenhavalidacao), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputOutputParameter("idtiposenhavalidacao", this.IdTipoSenhaValidacao, command);
                    DbConnect.CreateInputParameter("dstiposenhavalidacao", this.DsTipoSenhaValidacao.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("idtiposenhavalidacao", command).ToString()))
                    {
                        this.IdTipoSenhaValidacao = DbConnect.GetOutputParameterValue("idtiposenhavalidacao", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaUpd_catiposenhavalidacao), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idtiposenhavalidacao", this.IdTipoSenhaValidacao, command);

                    if (!String.IsNullOrEmpty(this.DsTipoSenhaValidacao))
                    {
                        DbConnect.CreateInputParameter("dstiposenhavalidacao", this.DsTipoSenhaValidacao.Trim(), command, TextParamTypeEnum.VarChar, 45);
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaDel_catiposenhavalidacao), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idtiposenhavalidacao", this.IdTipoSenhaValidacao, command);

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
