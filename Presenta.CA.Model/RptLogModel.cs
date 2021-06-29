using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presenta.CA.Model.Enum;
using Presenta.Common.Util;
using Presenta.DBConnection;
using System.Data;

namespace Presenta.CA.Model
{
    public class RptLogModel
    {
        public int IdLog { get; set; }
        public int IdTipoLog { get; set; }
        public int IdOperador { get; set; }
        public string NmOperador { get; set; }
        public DateTime DtLog { get; set; }
        public string DsMensagem { get; set; }
        public string TipoLog 
        {
            get
            {
                return ((CaTipoLogEnum)this.IdTipoLog).GetDescription();
            }
        }
        public string DsAplicativo { get; set; }
        public string DsSistema { get; set; }

        public List<RptLogModel> Listar(DateTime? de, DateTime? ate, int? idSistema, int? idAplicativo)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaRptLog), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("de", de, command);

                    DbConnect.CreateInputParameter("ate", ate, command);
                    
                    if (idSistema != null) { DbConnect.CreateInputParameter("idsistema", idSistema, command); }
                    else { DbConnect.CreateInputParameter("idsistema", DBNull.Value, command); }
                    
                    if (idAplicativo != null) { DbConnect.CreateInputParameter("idaplicativo", idAplicativo, command); }
                    else { DbConnect.CreateInputParameter("idaplicativo", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<RptLogModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<RptLogModel>();

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

        private List<RptLogModel> GetFromDbDataReader(System.Data.Common.DbDataReader dr)
        {
            var lista = new List<RptLogModel>();

            while (dr.Read())
            {
                lista.Add(
                    new RptLogModel()
                    {
                        IdLog = dr["idlog"].ToInt(),
                        IdTipoLog = dr["idtipolog"].ToInt(),
                        IdOperador = dr["idoperador"].ToInt(),
                        DtLog = dr["dtlog"].ToDateTime(),
                        DsMensagem = dr["dsmensagem"].ToString(),
                        DsAplicativo = dr["dsaplicativo"].ToString(),
                        DsSistema = dr["dssistema"].ToString(),
                        NmOperador = dr["nmoperador"].ToString()
                    });
            }

            return lista;
        }
    }
}
