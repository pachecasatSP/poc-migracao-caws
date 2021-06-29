using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Presenta.Common.Util;
using Presenta.DBConnection;
using Presenta.CA.Model.Enum;
using System.Data.SqlClient;

namespace Presenta.CA.Model
{
    public class CaPerfilModel
    {
        public int IdPerfil { get; set; }
        public string DsPerfil { get; set; }
        public int StPerfil { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public DateTime DhAtualizacao { get; set; }

        private static List<CaPerfilModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaPerfilModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaPerfilModel()
                    {
                        IdPerfil = dr["idperfil"].ToInt(),
                        DsPerfil = dr["dsperfil"].ToString(),
                        StPerfil = dr["stperfil"].ToInt(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime()
                    });
            }

            return lista;
        }

        public List<CaPerfilModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_caperfil), CommandType.StoredProcedure, connection);

                    if (!String.IsNullOrEmpty(this.DsPerfil)) { DbConnect.CreateInputParameter("dsperfil", this.DsPerfil.Trim(), command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("dsperfil", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }

                    if (this.StPerfil > 0) { DbConnect.CreateInputParameter("stperfil", this.StPerfil, command); }
                    else { DbConnect.CreateInputParameter("stperfil", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (this.IdPerfil > 0) { DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command); }
                    else { DbConnect.CreateInputParameter("idperfil", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaPerfilModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaPerfilModel>();

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

        public List<CaPerfilModel> NotListarAtivosPorFuncionalidade(int idFuncionalidade)
        {
            try
            {
                List<CaPerfilModel> perfisPorFunc = this.ListarAtivosPorFuncionalidade(idFuncionalidade);
                List<CaPerfilModel> perfis = this.Listar();
                var lista = perfis.Where(x => !perfisPorFunc.Any(l => l.IdPerfil == x.IdPerfil)).ToList();
                return lista.FindAll(p => p.StPerfil == (int)CaSituacaoPerfilEnum.Ativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CaPerfilModel> ListarAtivosPorFuncionalidade(int idFuncionalidade)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_perfil_por_func), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idfuncionalidade", idFuncionalidade, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaPerfilModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaPerfilModel>();

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return lista.FindAll(p => p.StPerfil == (int)CaSituacaoPerfilEnum.Ativo);
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

        public CaPerfilModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaSel_caperfil), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idperfil", this.IdPerfil, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaPerfilModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaPerfilModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaIns_caperfil), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("dsperfil", this.DsPerfil.Trim(), command, TextParamTypeEnum.VarChar, 100);
                    DbConnect.CreateInputParameter("stperfil", this.StPerfil, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    DbConnect.CreateInputOutputParameter("idperfil", this.IdPerfil, command);

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("idperfil", command).ToString()))
                    {
                        this.IdPerfil = DbConnect.GetOutputParameterValue("idperfil", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaUpd_caperfil), CommandType.StoredProcedure, connection, transaction);

                    if (!String.IsNullOrEmpty(this.DsPerfil)) { DbConnect.CreateInputParameter("dsperfil", this.DsPerfil.Trim(), command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("dsperfil", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }

                    if (this.StPerfil > 0) { DbConnect.CreateInputParameter("stperfil", this.StPerfil, command); }
                    else { DbConnect.CreateInputParameter("stperfil", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

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

        public void Excluir()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaDel_caperfil), CommandType.StoredProcedure, connection, transaction);

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


        public List<RptPerfilOperadorModel> ListarPorStatus(int status)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand(@"
                                                                Select DISTINCT
                                                                  fp.idperfil,
                                                                  c.idaplicativo,  
                                                                  p.dsperfil,
                                                                  p.stperfil,
                                                                  p.idoperador,
                                                                  p.dhatualizacao
                                                                from 
	                                                                cafuncionalidade_perfil fp
		                                                                INNER JOIN cafuncionalidade c ON
			                                                                c.idfuncionalidade = fp.idfuncionalidade
		                                                                INNER JOIN caperfil p ON
			                                                                p.idperfil = fp.idperfil

	                                                                WHERE 
	                                                                  ( @stperfil IS NULL ) OR ( p.stperfil = @stperfil )", CommandType.Text, connection);

               

                    if (status > 0)
                        command.Parameters.Add(new SqlParameter("@stperfil", SqlDbType.Int) { Value = status });
                    else
                        command.Parameters.Add(new SqlParameter("@stperfil", SqlDbType.Int) { Value = DBNull.Value });


                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<RptPerfilOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReaderService(dr) : new List<RptPerfilOperadorModel>();

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

        private List<RptPerfilOperadorModel> GetFromDbDataReaderService(DbDataReader dr)
        {
            var lista = new List<RptPerfilOperadorModel>();

            while (dr.Read())
            {
                lista.Add(
                    new RptPerfilOperadorModel()
                    {
                        IdPerfil = dr["idperfil"].ToInt(),
                        IdAplicativo = dr["idaplicativo"].ToInt(),
                        DsPerfil = dr["dsperfil"].ToString(),
                        StPerfil = dr["stperfil"].ToInt(),                      
                        IdOperadorAtualizacaoPerfilOperador = dr["idoperador"].ToInt(),                      
                        DhAtualizacaoPerfil = dr["dhatualizacao"].ToDateTime()
                    });
            }

            return lista;
        }
    }
}
