using Presenta.Common.Util;
using Presenta.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Presenta.CA.Model
{
    public class CaLogDetalheModel
    {
        public int IdLogDetalhe { get; set; }
        public int IdLog { get; set; }
        public string Detalhe { get; set; }

        public void Inserir(DbConnection connection, DbTransaction transaction)
        {
            try
            {
                var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaIns_calog_detalhe), CommandType.StoredProcedure, connection, transaction);

                DbConnect.CreateInputParameter("idlog", this.IdLog, command);

                if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                {
                    DbConnect.CreateInputParameter("detalhe", this.Detalhe, command, TextParamTypeEnum.VarChar, 4000);
                }
                else
                {
                    DbConnect.CreateInputParameter("detalhe", this.Detalhe, command, TextParamTypeEnum.VarChar, -1);
                }
                
                DbConnect.CreateOutputParameter("idlogdetalhe", this.IdLogDetalhe, command);

                command.ExecuteNonQuery();

                this.IdLogDetalhe = DbConnect.GetOutputParameterValue("idlogdetalhe", command).ToString().ToInt();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
