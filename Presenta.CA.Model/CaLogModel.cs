using Presenta.CA.Model.Enum;
using Presenta.Common.Util;
using Presenta.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Presenta.CA.Model
{
    public class CaLogModel
    {
        public CaLogModel() { }

        public CaLogModel(int idOperador)
        {
            this.IdOperador = idOperador;
        }

        public int IdLog { get; set; }
        public int IdTipoLog { get; set; }
        public int IdOperador { get; set; }
        public DateTime DtLog { get; set; }
        public string DsMensagem { get; set; }
        public int? IdAplicativo { get; set; }

        public void LogarErro(int idAplicativo, string mensagem)
        {
            this.IdTipoLog = (int)CaTipoLogEnum.Erro;
            this.DtLog = DateTime.Now;
            this.DsMensagem = mensagem;
            this.IdAplicativo = idAplicativo;
            this.Inserir();
        }

        public void LogarInfo(int idAplicativo, string mensagem)
        {
            this.IdTipoLog = (int)CaTipoLogEnum.Info;
            this.DtLog = DateTime.Now;
            this.DsMensagem = mensagem;
            this.IdAplicativo = idAplicativo;
            this.Inserir();
        }

        public List<CaLogModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaLst_calog), CommandType.StoredProcedure, connection);

                    if (this.IdTipoLog > 0) { DbConnect.CreateInputParameter("idtipolog", this.IdTipoLog, command); }
                    else { DbConnect.CreateInputParameter("idtipolog", DBNull.Value, command); }
                    
                    if (this.IdOperador > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperador, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }
                    
                    if (!DateTime.Equals(this.DtLog, new DateTime(1, 1, 1, 0, 0, 0))) { DbConnect.CreateInputParameter("dhatualizacao", this.DtLog, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }
                    
                    if (!String.IsNullOrEmpty(this.DsMensagem)) { DbConnect.CreateInputParameter("dhatualizacao", this.DsMensagem.Trim(), command, TextParamTypeEnum.VarChar, 100); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command, TextParamTypeEnum.VarChar, 100); }
                    
                    if (this.IdAplicativo != null) { DbConnect.CreateInputParameter("idaplicativo", this.IdAplicativo, command); }
                    else { DbConnect.CreateInputParameter("idaplicativo", DBNull.Value, command); }

                    if (this.IdLog > 0) { DbConnect.CreateInputParameter("idlog", this.IdLog, command); }
                    else { DbConnect.CreateInputParameter("idlog", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaLogModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaLogModel>();

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

        private List<CaLogModel> GetFromDbDataReader(System.Data.Common.DbDataReader dr)
        {
            var lista = new List<CaLogModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaLogModel()
                    {
                        IdLog = dr["idlog"].ToInt(),
                        IdTipoLog = dr["idtipolog"].ToInt(),
                        IdOperador = dr["idoperador"].ToInt(),
                        DtLog = dr["dtlog"].ToDateTime(),
                        DsMensagem = dr["dsmensagem"].ToString(),
                        IdAplicativo = String.IsNullOrEmpty(dr["idaplicativo"].ToString()) ? new int?() : dr["idaplicativo"].ToNullableInt()
                    });
            }

            return lista;
        }

        public void Inserir()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                var transaction = connection.BeginTransaction();

                try
                {
                    bool maiorQue100 = false;

                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaIns_calog), CommandType.StoredProcedure, connection, transaction);

                    
                    DbConnect.CreateInputParameter("idtipolog", this.IdTipoLog, command);
                    DbConnect.CreateInputParameter("idoperador", this.IdOperador, command);
                    DbConnect.CreateInputParameter("dtlog", this.DtLog, command);

                    if (this.DsMensagem.Trim().Length > 100)
                    {
                        DbConnect.CreateInputParameter("dsmensagem", "Os detalhes estão em CALOG_DETALHE", command, TextParamTypeEnum.VarChar, 100);
                        maiorQue100 = true;
                    }
                    else
                    {
                        DbConnect.CreateInputParameter("dsmensagem", this.DsMensagem.Trim(), command, TextParamTypeEnum.VarChar, 100);
                    }

                    if (this.IdAplicativo != null) { DbConnect.CreateInputParameter("idaplicativo", this.IdAplicativo, command); }
                    else { DbConnect.CreateInputParameter("idaplicativo", DBNull.Value, command); }

                    DbConnect.CreateOutputParameter("idlog", this.IdLog, command);

                    int count = command.ExecuteNonQuery();

                    this.IdLog = DbConnect.GetOutputParameterValue("idlog", command).ToString().ToInt();

                    if (maiorQue100)
                    {
                        var caLogDetalheModel = new CaLogDetalheModel() { IdLog = this.IdLog, Detalhe = this.DsMensagem.Trim() };
                        caLogDetalheModel.Inserir(connection, transaction);
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
    }
}
