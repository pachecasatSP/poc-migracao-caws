using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presenta.CA.Model.Enum;
using Presenta.Common.Util;
using Presenta.DBConnection;
using System.Data;
using Presenta.Common.Security;

namespace Presenta.CA.Model
{
    public class CaConfiguracaoModel
    {
        public int IdConfiguracao { get; set; }
        public int IdTipoLogon { get; set; }
        public string SenhaPadrao { get; set; }
        public int? DiasTrocaSenha { get; set; }
        public int? DiasDesativSenha { get; set; }
        public int? MaxTentinValidas { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public DateTime DhAtualizacao { get; set; }

        public string SenhaPadraoDescriptografada 
        {
            get
            {
                return Cryptographer.Decrypt(this.SenhaPadrao);
            }

            set
            {
                this.SenhaPadrao = Cryptographer.Encrypt(value);
            }        
        }

        public CaConfiguracaoModel(){}
        public CaConfiguracaoModel(int idConfiguracao)
        {
            this.IdConfiguracao = idConfiguracao;        
        }

        public string IdTipoLogonDescricao 
        {
            get
            {
                return ((CaTipoLogonEnum)this.IdTipoLogon).GetDescription();
            }            
        }

        public CaConfiguracaoModel Selecionar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaSel_caconfiguracao), CommandType.StoredProcedure, connection);

                    DbConnect.CreateInputParameter("idconfiguracao", this.IdConfiguracao, command);

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        DbConnect.CreateOutputCursorParameter(command);
                    }

                    var dr = command.ExecuteReader();

                    List<CaConfiguracaoModel> lista;

                    lista = dr.HasRows ? GetFromDbDataReader(dr) : new List<CaConfiguracaoModel>();

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

        public void Atualizar()
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = DbConnect.CreateCommand((Config.GetKeyValue(Constants.OwnerSchemaProc) == "" ? "" : Config.GetKeyValue(Constants.OwnerSchemaProc) + ".") + (Constants.StpCaUpd_caconfiguracao), CommandType.StoredProcedure, connection, transaction);

                    if (this.IdTipoLogon > 0) { DbConnect.CreateInputParameter("idtipologon", this.IdTipoLogon, command); }
                    else { DbConnect.CreateInputParameter("idtipologon", DBNull.Value, command); }

                    if (!String.IsNullOrEmpty(this.SenhaPadrao)) { DbConnect.CreateInputParameter("senhapadrao", this.SenhaPadrao.Trim(), command, TextParamTypeEnum.VarChar, 250); }
                    else { DbConnect.CreateInputParameter("senhapadrao", DBNull.Value, command, TextParamTypeEnum.VarChar, 250); }

                    if ((this.DiasTrocaSenha != null) && (this.DiasTrocaSenha > 0)) { DbConnect.CreateInputParameter("diastrocasenha", this.DiasTrocaSenha, command); }
                    else { DbConnect.CreateInputParameter("diastrocasenha", DBNull.Value, command); }

                    if ((this.DiasDesativSenha != null) && (this.DiasDesativSenha > 0)) { DbConnect.CreateInputParameter("diasdesativsenha", this.DiasDesativSenha, command); }
                    else { DbConnect.CreateInputParameter("diasdesativsenha", DBNull.Value, command); }

                    if ((this.MaxTentinValidas != null) && (this.MaxTentinValidas > 0)) { DbConnect.CreateInputParameter("maxtentinvalidas", this.MaxTentinValidas, command); }
                    else { DbConnect.CreateInputParameter("maxtentinvalidas", DBNull.Value, command); }

                    if (this.IdOperadorAtualizacao > 0) { DbConnect.CreateInputParameter("idoperador", this.IdOperadorAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("idoperador", DBNull.Value, command); }

                    if (this.DhAtualizacao != null) { DbConnect.CreateInputParameter("dhatualizacao", this.DhAtualizacao, command); }
                    else { DbConnect.CreateInputParameter("dhatualizacao", DBNull.Value, command); }

                    DbConnect.CreateInputParameter("idconfiguracao", this.IdConfiguracao, command);

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

        private List<CaConfiguracaoModel> GetFromDbDataReader(System.Data.Common.DbDataReader dr)
        {
            var lista = new List<CaConfiguracaoModel>();

            while (dr.Read())
            {
                lista.Add(
                    new CaConfiguracaoModel()
                    {
                        IdConfiguracao = dr["idconfiguracao"].ToInt(),
                        IdTipoLogon = dr["idtipologon"].ToInt(),
                        SenhaPadrao = dr["senhapadrao"].ToString(),
                        DiasTrocaSenha = dr["diastrocasenha"].ToInt(),
                        DiasDesativSenha = dr["diasdesativsenha"].ToInt(),
                        MaxTentinValidas = dr["maxtentinvalidas"].ToInt(),
                        IdOperadorAtualizacao = dr["idoperador"].ToInt(),
                        DhAtualizacao = dr["dhatualizacao"].ToDateTime(),
                    });
            }

            return lista;
        }
    }
}
