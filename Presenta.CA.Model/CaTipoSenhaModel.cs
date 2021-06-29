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
    public class CaTipoSenhaModel
    {
        public int IdTipoSenha { get; set; }
        public string DsTipoSenha { get; set; }
        public int QtMaxTentativas { get; set; }
        public int QtVerificacaoHistorico { get; set; }
        public string CdExpressaoRegular { get; set; }
        public DateTime DtInicioVigencia { get; set; }
        public DateTime? DtFimVigencia { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public DateTime DhAtualizacao { get; set; }

        private static List<CaTipoSenhaModel> GetFromDbDataReader(DbDataReader dr)
        {
            var lista = new List<CaTipoSenhaModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaTipoSenhaModel()
                    {
                        IdTipoSenha = dr["idtiposenha"].ToInt(),
                        DsTipoSenha = dr["dstiposenha"].ToString(),
                        QtMaxTentativas = dr["qtmaxtentativas"].ToInt(),
                        QtVerificacaoHistorico = dr["qtverificacaohistorico"].ToInt(),
                        CdExpressaoRegular = dr["cdexpressaoregular"].ToString(),
                        DtInicioVigencia = dr["dtiniciovigencia"].ToDateTime(),
                        DtFimVigencia = dr["dtfimvigencia"].ToDateTime(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime()
                    });
            }

            return lista;
        }

        public List<CaTipoSenhaModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_catiposenha), CommandType.StoredProcedure, connection);


                    if (this.IdTipoSenha > 0)
                    {
                        DbConnect.CreateInputParameter("idtiposenha", this.IdTipoSenha, command);
                    }

                    if (!String.IsNullOrEmpty(this.DsTipoSenha))
                    {
                        DbConnect.CreateInputParameter("dstiposenha", this.DsTipoSenha.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    }

                    if (this.QtMaxTentativas > 0)
                    {
                        DbConnect.CreateInputParameter("qtmaxtentativas", this.QtMaxTentativas, command);
                    }

                    if (this.QtVerificacaoHistorico > 0)
                    {
                        DbConnect.CreateInputParameter("qtverificacaohistorico", this.QtVerificacaoHistorico, command);
                    }

                    if (!String.IsNullOrEmpty(this.CdExpressaoRegular))
                    {
                        DbConnect.CreateInputParameter("cdexpressaoregular", this.CdExpressaoRegular.Trim(), command, TextParamTypeEnum.VarChar, 1000);
                    }

                    if (!DateTime.Equals(this.DtInicioVigencia, new DateTime(1, 1, 1, 0, 0, 0)))
                    {
                        DbConnect.CreateInputParameter("dtiniciovigencia", this.DtInicioVigencia, command);
                    }

                    if (!DateTime.Equals(this.DtFimVigencia, new DateTime(1, 1, 1, 0, 0, 0)))
                    {
                        DbConnect.CreateInputParameter("dtfimvigencia", this.DtFimVigencia, command);
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

                    List<CaTipoSenhaModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaTipoSenhaModel>();

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

        public CaTipoSenhaModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaSel_catiposenha), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idtiposenha", this.IdTipoSenha, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaTipoSenhaModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaTipoSenhaModel>();

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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_catiposenha), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputOutputParameter("idtiposenha", this.IdTipoSenha, command);
                    DbConnect.CreateInputParameter("dstiposenha", this.DsTipoSenha.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    DbConnect.CreateInputParameter("qtmaxtentativas", this.QtMaxTentativas, command);
                    DbConnect.CreateInputParameter("qtverificacaohistorico", this.QtVerificacaoHistorico, command);
                    DbConnect.CreateInputParameter("cdexpressaoregular", this.CdExpressaoRegular.Trim(), command, TextParamTypeEnum.VarChar, 1000);
                    DbConnect.CreateInputParameter("dtiniciovigencia", this.DtInicioVigencia, command);
                    DbConnect.CreateInputParameter("dtfimvigencia", this.DtFimVigencia, command);
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaUpd_catiposenha), CommandType.StoredProcedure, connection, transaction);

                    DbConnect.CreateInputParameter("idtiposenha", this.IdTipoSenha, command);
                    
                    if (!String.IsNullOrEmpty(this.DsTipoSenha))
                    {
                        DbConnect.CreateInputParameter("dstiposenha", this.DsTipoSenha.Trim(), command, TextParamTypeEnum.VarChar, 45);
                    }

                    if (this.QtMaxTentativas > 0)
                    {
                        DbConnect.CreateInputParameter("qtmaxtentativas", this.QtMaxTentativas, command);
                    }

                    if (this.QtVerificacaoHistorico > 0)
                    {
                        DbConnect.CreateInputParameter("qtverificacaohistorico", this.QtVerificacaoHistorico, command);
                    }

                    if (!String.IsNullOrEmpty(this.CdExpressaoRegular))
                    {
                        DbConnect.CreateInputParameter("cdexpressaoregular", this.CdExpressaoRegular.Trim(), command, TextParamTypeEnum.VarChar, 1000);
                    }

                    if (!DateTime.Equals(this.DtInicioVigencia, new DateTime(1, 1, 1, 0, 0, 0)))
                    {
                        DbConnect.CreateInputParameter("dtiniciovigencia", this.DtInicioVigencia, command);
                    }

                    if (!DateTime.Equals(this.DtFimVigencia, new DateTime(1, 1, 1, 0, 0, 0)))
                    {
                        DbConnect.CreateInputParameter("dtfimvigencia", this.DtFimVigencia, command);
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
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaDel_catiposenha), CommandType.StoredProcedure, connection, transaction);

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
