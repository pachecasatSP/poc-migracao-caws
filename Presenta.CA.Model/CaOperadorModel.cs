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
    public class CaOperadorModel
    {
        public int IdOperador { get; set; }
        public int IdTipoSenha { get; set; }
        public int StOperador { get; set; }
        public string CdOperador { get; set; }
        public string NmOperador { get; set; }
        public string DsEmail { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime DhSituacao { get; set; }
        public string CrSenha { get; set; }
        public DateTime DtSenha { get; set; }
        public DateTime? DhUltimoLogin { get; set; }
        public int QtLoginIncorreto { get; set; }
        public DateTime DhAtualizacao { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public string CdGuid { get; set; }

        private static List<CaOperadorModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaOperadorModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaOperadorModel()
                    {
                        IdOperador = dr["idoperador"].ToInt(),
                        IdTipoSenha = dr["idtiposenha"].ToInt(),
                        StOperador = dr["stoperador"].ToInt(),
                        CdOperador = dr["cdoperador"].ToString(),
                        NmOperador = dr["nmoperador"].ToString(),
                        DsEmail = dr["dsemail"].ToString(),
                        DtCadastro = dr["dtcadastro"].ToDateTime(),
                        DhSituacao = dr["dhsituacao"].ToDateTime(),
                        CrSenha = dr["crsenha"].ToString(),
                        DtSenha = dr["dtsenha"].ToDateTime(),
                        DhUltimoLogin = dr["dhultimologin"].ToDateTime(),
                        QtLoginIncorreto = dr["qtloginincorreto"].ToInt(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime(),
                        IdOperadorAtualizacao = dr["idoperadoratualizacao"].ToInt(),
                        CdGuid = dr["cdguid"].ToString()
                    });
            }

            return lista;
        }

        public List<CaOperadorModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_caoperador), CommandType.StoredProcedure, connection);

                    if (this.IdTipoSenha > 0) { DbConnect.CreateInputParameter("idtiposenha", this.IdTipoSenha, command); }
                    else { DbConnect.CreateInputParameter("idtiposenha", DBNull.Value, command); }

                    if (this.StOperador > 0) { DbConnect.CreateInputParameter("stoperador", this.StOperador, command); }
                    else { DbConnect.CreateInputParameter("stoperador", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.CdOperador)) { DbConnect.CreateInputParameter("cdoperador", this.CdOperador.Trim(), command, TextParamTypeEnum.VarChar, 25); }
                    else { DbConnect.CreateInputParameter("cdoperador", DBNull.Value, command, TextParamTypeEnum.VarChar, 25); }

                    if (!String.IsNullOrEmpty(this.NmOperador)) { DbConnect.CreateInputParameter("nmoperador", this.NmOperador.Trim(), command, TextParamTypeEnum.VarChar, 45); }
                    else { DbConnect.CreateInputParameter("nmoperador", DBNull.Value, command, TextParamTypeEnum.VarChar, 45); }

                    if (!String.IsNullOrEmpty(this.DsEmail)) { DbConnect.CreateInputParameter("dsemail", this.DsEmail.Trim(), command, TextParamTypeEnum.VarChar, 45); }
                    else { DbConnect.CreateInputParameter("dsemail", DBNull.Value, command, TextParamTypeEnum.VarChar, 45); }

                    if (!DateTime.Equals(this.DtCadastro, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dtcadastro", this.DtCadastro, command); }
                    else { DbConnect.CreateInputParameter("dtcadastro", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhSituacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhsituacao", this.DhSituacao, command); }
                    else { DbConnect.CreateInputParameter("dhsituacao", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.CrSenha)) { DbConnect.CreateInputParameter("crsenha", this.CrSenha.Trim(), command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("crsenha", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }

                    if (!DateTime.Equals(this.DtSenha, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dtsenha", this.DtSenha, command); }
                    else { DbConnect.CreateInputParameter("dtsenha", DBNull.Value, command); }

                    if (this.DhUltimoLogin != null && !DateTime.Equals(this.DhUltimoLogin, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhultimologin", this.DhUltimoLogin, command); }
                    else { DbConnect.CreateInputParameter("dhultimologin", DBNull.Value, command); }

                    if (this.QtLoginIncorreto > 0) { DbConnect.CreateInputParameter("qtloginincorreto", this.QtLoginIncorreto, command); }
                    else { DbConnect.CreateInputParameter("qtloginincorreto", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperadoratualizacao", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperadoratualizacao", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.CdGuid)) { DbConnect.CreateInputParameter("cdguid", this.CdGuid, command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("cdguid", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }

                    if (this.IdOperador > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperador, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaOperadorModel>();

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

        public bool ExisteOperador()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand(string.Format("SELECT cdoperador FROM caoperador WHERE cdoperador = '{0}'", this.CdOperador), CommandType.Text, connection);
                    
                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    var existe = dr.HasRows;

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }
                   
                    return existe;
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

        public List<CaOperadorModel> ListarAtivosPorPerfil(int idPerfil)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_operador_por_perfil), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idperfil", idPerfil, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaOperadorModel>();

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return lista.FindAll(p => p.StOperador == (int)CaSituacaoOperadorEnum.Ativo);
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

        public List<CaOperadorModel> ListarPorPerfil(int idPerfil)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_operador_por_perfil), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idperfil", idPerfil, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaOperadorModel>();

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

        public List<CaOperadorModel> NotListarAtivosPorPerfil(int idPerfil)
        {
            try
            {
                List<CaOperadorModel> operadoresPorPerfil = this.ListarAtivosPorPerfil(idPerfil);
                List<CaOperadorModel> operadores = this.Listar();
                var lista = operadores.Where(x => !operadoresPorPerfil.Any(l => l.IdOperador == x.IdOperador)).ToList();
                return lista.FindAll(p => p.StOperador == (int)CaSituacaoOperadorEnum.Ativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CaOperadorModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaSel_caoperador), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idoperador", this.IdOperador, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaOperadorModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaIns_caoperador), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idtiposenha", this.IdTipoSenha, command);
                    DbConnect.CreateInputParameter("stoperador", this.StOperador, command);
                    DbConnect.CreateInputParameter("cdoperador", this.CdOperador.Trim(), command, TextParamTypeEnum.VarChar, 25);
                    DbConnect.CreateInputParameter("nmoperador", this.NmOperador.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    if (!String.IsNullOrEmpty(this.DsEmail)) { DbConnect.CreateInputParameter("dsemail", this.DsEmail.Trim(), command, TextParamTypeEnum.VarChar, 45); }
                    else { DbConnect.CreateInputParameter("dsemail", DBNull.Value, command, TextParamTypeEnum.VarChar, 45); }
                    DbConnect.CreateInputParameter("dtcadastro", this.DtCadastro, command);
                    DbConnect.CreateInputParameter("dhsituacao", this.DhSituacao, command);
                    if (!String.IsNullOrEmpty(this.CrSenha)) { DbConnect.CreateInputParameter("crsenha", this.CrSenha.Trim(), command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("crsenha", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }
                    DbConnect.CreateInputParameter("dtsenha", this.DtSenha, command);
                    if (this.DhUltimoLogin != null && !DateTime.Equals(this.DhUltimoLogin, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhultimologin", this.DhUltimoLogin, command); }
                    else { DbConnect.CreateInputParameter("dhultimologin", DBNull.Value, command); }
                    DbConnect.CreateInputParameter("qtloginincorreto", this.QtLoginIncorreto, command);
                    DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command);
                    DbConnect.CreateInputParameter("idoperadoratualizacao", this.IdOperadorAtualizacao, command);
                    if (!String.IsNullOrEmpty(this.CdGuid)) { DbConnect.CreateInputParameter("cdguid", this.CdGuid, command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("cdguid", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }
                    DbConnect.CreateInputOutputParameter("idoperador", this.IdOperador, command);

                    //DbConnect.CreateInputParameter("staprovacao", 0, command);
                    //DbConnect.CreateInputParameter("idaprovacao", DBNull.Value, command);
                    //DbConnect.CreateInputParameter("idtipooperador", 3, command); //Comum validar regra

                    command.ExecuteNonQuery();

                    if (!String.IsNullOrEmpty(DbConnect.GetOutputParameterValue("idoperador", command).ToString()))
                    {
                        this.IdOperador = DbConnect.GetOutputParameterValue("idoperador", command).ToString().ToInt();
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaUpd_caoperador), CommandType.StoredProcedure, connection, transaction);

                    if (this.IdTipoSenha > 0) { DbConnect.CreateInputParameter("idtiposenha", this.IdTipoSenha, command); }
                    else { DbConnect.CreateInputParameter("idtiposenha", DBNull.Value, command); }

                    if (this.StOperador > 0) { DbConnect.CreateInputParameter("stoperador", this.StOperador, command); }
                    else { DbConnect.CreateInputParameter("stoperador", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.CdOperador)) { DbConnect.CreateInputParameter("cdoperador", this.CdOperador.Trim(), command, TextParamTypeEnum.VarChar, 25); }
                    else { DbConnect.CreateInputParameter("cdoperador", DBNull.Value, command, TextParamTypeEnum.VarChar, 25); }

                    if (!String.IsNullOrEmpty(this.NmOperador)) { DbConnect.CreateInputParameter("nmoperador", this.NmOperador.Trim(), command, TextParamTypeEnum.VarChar, 45); }
                    else { DbConnect.CreateInputParameter("nmoperador", DBNull.Value, command, TextParamTypeEnum.VarChar, 45); }

                    if (!String.IsNullOrEmpty(this.DsEmail)) { DbConnect.CreateInputParameter("dsemail", this.DsEmail.Trim(), command, TextParamTypeEnum.VarChar, 45); }
                    else { DbConnect.CreateInputParameter("dsemail", DBNull.Value, command, TextParamTypeEnum.VarChar, 45); }

                    if (!DateTime.Equals(this.DtCadastro, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dtcadastro", this.DtCadastro, command); }
                    else { DbConnect.CreateInputParameter("dtcadastro", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DhSituacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhsituacao", this.DhSituacao, command); }
                    else { DbConnect.CreateInputParameter("dhsituacao", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.CrSenha)) { DbConnect.CreateInputParameter("crsenha", this.CrSenha.Trim(), command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("crsenha", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }

                    if (!DateTime.Equals(this.DtSenha, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dtsenha", this.DtSenha, command); }
                    else { DbConnect.CreateInputParameter("dtsenha", DBNull.Value, command); }

                    if (this.DhUltimoLogin != null && !DateTime.Equals(this.DhUltimoLogin, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhultimologin", this.DhUltimoLogin, command); }
                    else { DbConnect.CreateInputParameter("dhultimologin", DBNull.Value, command); }

                    DbConnect.CreateInputParameter("qtloginincorreto", this.QtLoginIncorreto, command);

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperadoratualizacao", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperadoratualizacao", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.CdGuid)) { DbConnect.CreateInputParameter("cdguid", this.CdGuid, command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("cdguid", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }

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

        public void ResetarSenha()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaNgc_Reset_caoperador), CommandType.StoredProcedure, connection, transaction);

                    if (this.StOperador > 0) { DbConnect.CreateInputParameter("stoperador", this.StOperador, command); }
                    else { DbConnect.CreateInputParameter("stoperador", DBNull.Value, command); }

                    if (!DateTime.Equals(this.DtSenha, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dtsenha", this.DtSenha, command); }
                    else { DbConnect.CreateInputParameter("dtsenha", DBNull.Value, command); }

                    DbConnect.CreateInputParameter("qtloginincorreto", this.QtLoginIncorreto, command);

                    if (!DateTime.Equals(this.DhAtualizacao, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperadoratualizacao", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperadoratualizacao", DBNull.Value, command); }

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

        public void Excluir()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaDel_caoperador), CommandType.StoredProcedure, connection, transaction);

                    if (!String.IsNullOrEmpty(this.CdOperador)) { DbConnect.CreateInputParameter("cdoperador", this.CdOperador.Trim(), command, TextParamTypeEnum.VarChar, 25); }
                    else { DbConnect.CreateInputParameter("cdoperador", DBNull.Value, command, TextParamTypeEnum.VarChar, 25); }

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
