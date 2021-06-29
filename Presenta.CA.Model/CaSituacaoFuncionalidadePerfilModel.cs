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
    public class CaSituacaoFuncionalidadePerfilModel
    {
        public int StFuncionalidadePerfil { get; set; }
        public string DsSituacaoFuncionalidadePerfil { get; set; }
        public DateTime DhAtualizacao { get; set; }
        public int IdOperadorAtualizacao { get; set; }

        private static List<CaSituacaoFuncionalidadePerfilModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaSituacaoFuncionalidadePerfilModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaSituacaoFuncionalidadePerfilModel()
                    {
                        StFuncionalidadePerfil = dr["stfuncionalidadeperfil"].ToInt(),
                        DsSituacaoFuncionalidadePerfil = Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? dr["dssitfuncperfil"].ToString() : dr["dssituacaofuncionalidadeperfil"].ToString(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt()
                    });
            }

            return lista;
        }

        public List<CaSituacaoFuncionalidadePerfilModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaLst_casituacao_funcionalidade_perfil_Oracle : Constants.StpCaLst_casituacao_funcionalidade_perfil), CommandType.StoredProcedure, connection);

                    if (this.StFuncionalidadePerfil > 0)
                    {
                        DbConnect.CreateInputParameter("stfuncionalidadeperfil", this.StFuncionalidadePerfil, command);
                    }

                    if (!String.IsNullOrEmpty(this.DsSituacaoFuncionalidadePerfil))
                    {
                        DbConnect.CreateInputParameter(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "dssitfuncperfil" : "dssituacaofuncionalidadeperfil", this.DsSituacaoFuncionalidadePerfil.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0)))
                    {
                        DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    }

                    if (this.IdOperadorAtualizacao > 0)
                    {
                        DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaSituacaoFuncionalidadePerfilModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaSituacaoFuncionalidadePerfilModel>();

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

        public CaSituacaoFuncionalidadePerfilModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaSel_casituacao_funcionalidade_perfil_Oracle : Constants.StpCaSel_casituacao_funcionalidade_perfil), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("stfuncionalidadeperfil", this.StFuncionalidadePerfil, command);

                    var dr = command.ExecuteReader();

                    List<CaSituacaoFuncionalidadePerfilModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaSituacaoFuncionalidadePerfilModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaLst_casituacao_funcionalidade_perfil_Oracle : Constants.StpCaLst_casituacao_funcionalidade_perfil), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputOutputParameter("stfuncionalidadeperfil", this.StFuncionalidadePerfil, command);
                    DbConnect.CreateInputParameter(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "dssitfuncperfil" : "dssituacaofuncionalidadeperfil", this.DsSituacaoFuncionalidadePerfil.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    
                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("stfuncionalidadeperfil", command).ToString()))
                    {
                        this.StFuncionalidadePerfil = DbConnect.GetOutputParameterValue("stfuncionalidadeperfil", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaUpd_casituacao_funcionalidade_perfil_Oracle : Constants.StpCaUpd_casituacao_funcionalidade_perfil), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("stfuncionalidadeperfil", this.StFuncionalidadePerfil, command);
                    
                    if (!String.IsNullOrEmpty(this.DsSituacaoFuncionalidadePerfil))
                    {
                        DbConnect.CreateInputParameter(Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? "dssitfuncperfil" : "dssituacaofuncionalidadeperfil", this.DsSituacaoFuncionalidadePerfil.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0)))
                    {
                        DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    }

                    if (this.IdOperadorAtualizacao > 0)
                    {
                        DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? Constants.StpCaDel_casituacao_funcionalidade_perfil_Oracle : Constants.StpCaDel_casituacao_funcionalidade_perfil), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("stfuncionalidadeperfil", this.StFuncionalidadePerfil, command);

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
