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
    public class RptPerfilFuncionalidadeModel
    {
        public int IdPerfil { get; set; }
        public string DsPerfil { get; set; }
        public int StPerfil { get; set; }
        public int IdOperadorPerfil { get; set; }
        public DateTime DhAtualizacaoPerfil { get; set; }
        public int? StFuncionalidadePerfil { get; set; }
        public DateTime DhSituacaoFuncionalidadePerfil { get; set; }
        public int IdOperadorFuncionalidadePerfil { get; set; }
        public DateTime DhAtualizacaoFuncionalidadePerfil { get; set; }
        public int IdFuncionalidade { get; set; }
        public int IdOperadorFuncionalidade { get; set; }
        public int? StFuncionalidade { get; set; }
        public string DsFuncionalidade { get; set; }
        public DateTime DhAtualizacaoFuncionalidade { get; set; }
        public int IdSistema { get; set; }
		public string DsSistema { get; set; }
		public int IdAplicativo { get; set; }
        public string DsAplicativo { get; set; }

        public string StPerfilDescricao
        {
            get
            {
                return ((CaSituacaoPerfilEnum)this.StPerfil).GetDescription();
            }
        }
        public string StFuncionalidadePerfilDescricao
        {
            get
            {
                return ((CaSituacaoFuncionalidadePerfilEnum)this.StFuncionalidadePerfil).GetDescription();
            }
        }
        public string StFuncionalidadeDescricao
        {
            get
            {
                return ((CaSituacaoFuncionalidadeEnum)this.StFuncionalidade).GetDescription();
            }
        }


        public List<RptPerfilFuncionalidadeModel> Listar(int? idSistema, int? idAplicativo)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaRptPerfilFuncionalidade), CommandType.StoredProcedure, connection);

                    if (idSistema != null) { DbConnect.CreateInputParameter("idsistema", idSistema, command); }
                    else { DbConnect.CreateInputParameter("idsistema", DBNull.Value, command); }

                    if (idAplicativo != null) { DbConnect.CreateInputParameter("idaplicativo", idAplicativo, command); }
                    else { DbConnect.CreateInputParameter("idaplicativo", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<RptPerfilFuncionalidadeModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<RptPerfilFuncionalidadeModel>();

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

        private List<RptPerfilFuncionalidadeModel> GetFromDbDataReader(System.Data.Common.DbDataReader dr)
        {
            var lista = new List<RptPerfilFuncionalidadeModel>();

            while (dr.Read())
            {
                lista.Add(
                    new RptPerfilFuncionalidadeModel()
                    {
                        IdPerfil = dr["idperfil"].ToInt(),
                        DsPerfil = dr["dsperfil"].ToString(),
                        StPerfil = dr["stperfil"].ToInt(),
                        IdOperadorPerfil = dr["idoperadorperfil"].ToInt(),
                        DhAtualizacaoPerfil = dr["dhatualizacaoperfil"].ToDateTime(),
                        StFuncionalidadePerfil = dr["stfuncionalidadeperfil"].ToNullableInt(),
                        DhSituacaoFuncionalidadePerfil = dr["dhsituacaofuncionalidadeperfil"].ToDateTime(),
                        IdOperadorFuncionalidadePerfil = dr["idoperadorfuncionalidadeperfil"].ToInt(),
                        DhAtualizacaoFuncionalidadePerfil = Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? dr["dhatualizacaofuncperfil"].ToDateTime() : dr["dhatualizacaofuncionalidadeperfil"].ToDateTime(),
                        IdFuncionalidade = dr["idfuncionalidade"].ToInt(),
                        IdOperadorFuncionalidade = dr["idoperadorfuncionalidade"].ToInt(),
                        StFuncionalidade = dr["stfuncionalidade"].ToNullableInt(),
                        DsFuncionalidade = dr["dsfuncionalidade"].ToString(),
                        DhAtualizacaoFuncionalidade = dr["dhatualizacaofuncionalidade"].ToDateTime(),
                        IdSistema = dr["idsistema"].ToInt(),
                        DsSistema = dr["dssistema"].ToString(),
		                IdAplicativo = dr["idaplicativo"].ToInt(),
                        DsAplicativo = dr["dsaplicativo"].ToString()
                    });
            }

            return lista;
        }
    }
}
