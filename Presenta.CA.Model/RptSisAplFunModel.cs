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
    public class RptSisAplFunModel
    {
        public int IdSistema { get; set; }
        public string DsSistema { get; set; }
        public int IdOperadorSistema { get; set; }
        public DateTime DhAtualizacaoSistema { get; set; }
        public int IdAplicativo { get; set; }
        public int IdOperadorAplicativo { get; set; }
        public string DsAplicativo { get; set; }
        public DateTime DhAtualizacaoAplicativo { get; set; }
        public int IdFuncionalidade { get; set; }
        public int IdOperadorFuncionalidade { get; set; }
        public int? StFuncionalidade { get; set; }
        public string DsFuncionalidade { get; set; }
        public DateTime DhAtualizacaoFuncionalidade { get; set; }
        
        public string StFuncionalidadeDescricao 
        {
            get
            {
                if (this.StFuncionalidade == null)
                {
                    return null;                    
                }

                return ((CaSituacaoFuncionalidadeEnum)this.StFuncionalidade).GetDescription();
            }            
        }

        public List<RptSisAplFunModel> Listar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaRptSistemaAplicativoFuncionalidade), CommandType.StoredProcedure, connection);

                    if (this.IdSistema > 0) { DbConnect.CreateInputParameter("idsistema", this.IdSistema, command); }
                    else { DbConnect.CreateInputParameter("idsistema", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<RptSisAplFunModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<RptSisAplFunModel>();

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

        private List<RptSisAplFunModel> GetFromDbDataReader(System.Data.Common.DbDataReader dr)
        {
            var lista = new List<RptSisAplFunModel>();

            while (dr.Read())
            {
                lista.Add(
                    new RptSisAplFunModel()
                    {
                        IdSistema = dr["idsistema"].ToInt(),
                        DsSistema = dr["dssistema"].ToString(),
                        IdOperadorSistema = dr["idoperadorsistema"].ToInt(),
                        DhAtualizacaoSistema = dr["dhatualizacaosistema"].ToDateTime(),
                        IdAplicativo = dr["idaplicativo"].ToInt(),
                        IdOperadorAplicativo = dr["idoperadoraplicativo"].ToInt(),
                        DsAplicativo = dr["dsaplicativo"].ToString(),
                        DhAtualizacaoAplicativo = dr["dhatualizacaoaplicativo"].ToDateTime(),
                        IdFuncionalidade = dr["idfuncionalidade"].ToInt(),
                        IdOperadorFuncionalidade = dr["idoperadorfuncionalidade"].ToInt(),
                        StFuncionalidade = dr["stfuncionalidade"].ToNullableInt(),
                        DsFuncionalidade = dr["dsfuncionalidade"].ToString(),
                        DhAtualizacaoFuncionalidade = dr["dhatualizacaofuncionalidade"].ToDateTime(),
                    });
            }

            return lista;
        }
    }
}
