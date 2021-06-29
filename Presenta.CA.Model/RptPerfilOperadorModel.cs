using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presenta.CA.Model.Enum;
using Presenta.Common.Util;
using Presenta.DBConnection;
using System.Data;
using System.Data.SqlClient;

namespace Presenta.CA.Model
{
    public class RptPerfilOperadorModel
    {
        public int IdPerfil { get; set; }
        public string DsPerfil { get; set; }
        public int StPerfil { get; set; }
        public int IdOperadorPerfil { get; set; }
        public DateTime DhAtualizacaoPerfil { get; set; }
        public int? StPerfilOperador { get; set; }
        public DateTime DhSituacaoPerfilOperador { get; set; }
        public DateTime DhAtualizacaoPerfilOperador { get; set; }
        public int IdOperadorAtualizacaoPerfilOperador { get; set; }
        public int IdOperadorOperador { get; set; }
        public int IdTipoSenha { get; set; }
        public int? StOperador { get; set; }
        public string CdOperador { get; set; }
        public string NmOperador { get; set; }
        public string DsEmail { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime DhSituacaoOperador { get; set; }
        public DateTime DtSenha { get; set; }
        public DateTime DhUltimoLogin { get; set; }
        public int QtLoginIncorreto { get; set; }
        public DateTime DhAtualizacaoOperador { get; set; }
        public int IdOperadorAtualizacaoOperador { get; set; }
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
        public string StPerfilOperadorDescricao 
        {
            get
            {
                if (this.StPerfilOperador == null)
                {
                    return null;
                }

                return ((CaSituacaoPerfilOperadorEnum)this.StPerfilOperador).GetDescription();
            }            
        }
        public string StOperadorDescricao
        {
            get
            {
                if (this.StOperador == null)
                {
                    return null;
                }
                return ((CaSituacaoOperadorEnum)this.StOperador).GetDescription();
            }
        }


        public List<RptPerfilOperadorModel> Listar(int? idSistema, int? idAplicativo)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaRptPerfilOperador), CommandType.StoredProcedure, connection);

                    if (idSistema != null) { DbConnect.CreateInputParameter("idsistema", idSistema, command); }
                    else { DbConnect.CreateInputParameter("idsistema", DBNull.Value, command); }

                    if (idAplicativo != null) { DbConnect.CreateInputParameter("idaplicativo", idAplicativo, command); }
                    else { DbConnect.CreateInputParameter("idaplicativo", DBNull.Value, command); }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<RptPerfilOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<RptPerfilOperadorModel>();

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

        private List<RptPerfilOperadorModel> GetFromDbDataReader(System.Data.Common.DbDataReader dr)
        {
            var lista = new List<RptPerfilOperadorModel>();

            while (dr.Read())
            {
                lista.Add(
                    new RptPerfilOperadorModel()
                    {
                        IdPerfil = dr["idperfil"].ToInt(),
                        DsPerfil = dr["dsperfil"].ToString(),
                        StPerfil = dr["stperfil"].ToInt(),
                        IdOperadorPerfil = dr["idoperadorperfil"].ToInt(),
                        DhAtualizacaoPerfil = dr["dhatualizacaoperfil"].ToDateTime(),
                        StPerfilOperador = dr["stperfiloperador"].ToNullableInt(),
                        DhSituacaoPerfilOperador = dr["dhsituacaoperfiloperador"].ToDateTime(),
                        DhAtualizacaoPerfilOperador = dr["dhatualizacaoperfiloperador"].ToDateTime(),
                        IdOperadorAtualizacaoPerfilOperador = Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient ? dr["idoperatualizacaoperfiloper"].ToInt() : dr["idoperadoratualizacaoperfiloperador"].ToInt(),
                        IdOperadorOperador = dr["idoperadoroperador"].ToInt(),
                        IdTipoSenha = dr["idtiposenha"].ToInt(),
                        StOperador = dr["stoperador"].ToNullableInt(),
                        CdOperador = dr["cdoperador"].ToString(),
                        NmOperador = dr["nmoperador"].ToString(),
                        DsEmail = dr["dsemail"].ToString(),
                        DtCadastro = dr["dtcadastro"].ToDateTime(),
                        DhSituacaoOperador = dr["dhsituacaooperador"].ToDateTime(),
                        DtSenha = dr["dtsenha"].ToDateTime(),
                        DhUltimoLogin = dr["dhultimologin"].ToDateTime(),
                        QtLoginIncorreto = dr["qtloginincorreto"].ToInt(),
                        DhAtualizacaoOperador = dr["dhatualizacaooperador"].ToDateTime(),
                        IdOperadorAtualizacaoOperador = dr["idoperadoratualizacaooperador"].ToInt(),
                        IdSistema = dr["idsistema"].ToInt(),
                        DsSistema = dr["dssistema"].ToString(),
                        IdAplicativo = dr["idaplicativo"].ToInt(),
                        DsAplicativo = dr["dsaplicativo"].ToString()
                    });
            }

            return lista;
        }

        public List<RptPerfilOperadorModel> ObterSistemaAplicativoPerfilOperador()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand(@"


                                                                SELECT DISTINCT

                                                                    P.idperfil,
                                                                    P.dsperfil,
                                                                    P.stperfil,
                                                                    P.idoperador AS idoperadorperfil,
                                                                    P.dhatualizacao AS dhatualizacaoperfil,
                                                                    PO.stperfiloperador,
                                                                    PO.dhsituacao AS dhsituacaoperfiloperador,
                                                                    PO.dhatualizacao AS dhatualizacaoperfiloperador,
                                                                    PO.idoperadoratualizacao AS idoperadoratualizacaoperfiloperador,
                                                                    O.idoperador AS idoperadoroperador,
                                                                    O.idtiposenha,
                                                                    O.stoperador,
                                                                    O.cdoperador,
                                                                    O.nmoperador,
                                                                    O.dsemail,
                                                                    O.dtcadastro,
                                                                    O.dhsituacao AS dhsituacaooperador,
                                                                    O.dtsenha,
                                                                    O.dhultimologin,
                                                                    O.qtloginincorreto,
                                                                    O.dhatualizacao AS dhatualizacaooperador,
                                                                    O.idoperadoratualizacao as idoperadoratualizacaooperador,
                                                                    S.idsistema,
                                                                    S.dssistema,
                                                                    A.idaplicativo,
                                                                    A.dsaplicativo

                                                                FROM

                                                                    caperfil AS P

                                                                INNER JOIN

                                                                    caperfil_operador AS PO ON P.idperfil = PO.idperfil

                                                                INNER JOIN

                                                                    caoperador AS O ON PO.idoperador = O.idoperador

                                                                INNER JOIN

                                                                    cafuncionalidade_perfil AS FP ON P.idperfil = FP.idperfil

                                                                INNER JOIN

                                                                    cafuncionalidade AS F ON FP.idfuncionalidade = F.idfuncionalidade

                                                                INNER JOIN

                                                                    caaplicativo AS A ON F.idaplicativo = A.idaplicativo

                                                                INNER JOIN

                                                                    casistema AS S ON A.idsistema = S.idsistema

                                                                WHERE
                                                                    ((@idoperador IS NULL) OR(O.idoperador = @idoperador))
                                                                    AND((@idaplicativo IS NULL) OR(A.idaplicativo = @idaplicativo))	
		                                                            AND((@idperfil IS NULL) OR(P.idperfil = @idperfil))	
	                                                            ORDER BY

                                                                    P.dsperfil,
		                                                            O.nmoperador", CommandType.Text, connection);

                    if (IdOperadorOperador > 0)
                        command.Parameters.Add(new SqlParameter("@idoperador", SqlDbType.Int) { Value = IdOperadorOperador });
                    else
                        command.Parameters.Add(new SqlParameter("@idoperador", SqlDbType.Int) { Value = DBNull.Value });

                    if (IdAplicativo > 0)
                        command.Parameters.Add(new SqlParameter("@idaplicativo", SqlDbType.Int) { Value = IdAplicativo });
                    else
                        command.Parameters.Add(new SqlParameter("@idaplicativo", SqlDbType.Int) { Value = DBNull.Value });

                    if (IdPerfil > 0)
                        command.Parameters.Add(new SqlParameter("@idperfil", SqlDbType.Int) { Value = IdPerfil });
                    else
                        command.Parameters.Add(new SqlParameter("@idperfil", SqlDbType.Int) { Value = DBNull.Value });


                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<RptPerfilOperadorModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<RptPerfilOperadorModel>();

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
    }
}
