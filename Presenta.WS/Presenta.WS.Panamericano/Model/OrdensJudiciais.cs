using Presenta.Common.Util;
using Presenta.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Presenta.WS.Panamericano.Model
{
    [Serializable]
    public class OrdensJudiciais
    {
        private string OwnerTable { get; set; }

        [XmlElement("Mensagem")]
        public Mensagem DsMensagem { get; set; }

        [XmlElement("Ordem")]
        public List<Oficio> Oficios { get; set; }

        public OrdensJudiciais()
        {
            this.OwnerTable = Config.GetKeyValue(Constants.KeyOwnerTable).Trim();
            if (this.OwnerTable != "")
            {
                if (this.OwnerTable[this.OwnerTable.Length - 1].ToString() != ".")
	            {
                    this.OwnerTable = this.OwnerTable + ".";
	            }
            }
        }

        public OrdensJudiciais ProcessarOrdensJudiciais(string dataDe, string dataAte, string tipo)
        {
            if (dataDe.Trim() == "")
            {
                this.DsMensagem = new Mensagem()
                {
                    Descricao = "O parâmetro DataDe deve ser preenchido!"
                };
                return this;
            }
            if (dataAte.Trim() == "")
            {
                this.DsMensagem = new Mensagem()
                {
                    Descricao = "O parâmetro DataAte deve ser preenchido!"
                };
                return this;
            } 
            if (tipo.Trim() == "")
            {
                this.DsMensagem = new Mensagem()
                {
                    Descricao = "O parâmetro Tipo deve ser preenchido!"
                };
                return this;
            }

            int dd;
            int mm;
            int aaaa;

            dd = dataDe.Substring(0, 2).ToInt();
            mm = dataDe.Substring(3, 2).ToInt();
            aaaa = dataDe.Substring(6, 4).ToInt();
            DateTime dtDe = new DateTime(aaaa, mm, dd);

            dd = dataAte.Substring(0, 2).ToInt();
            mm = dataAte.Substring(3, 2).ToInt();
            aaaa = dataAte.Substring(6, 4).ToInt();
            DateTime dtAte = new DateTime(aaaa, mm, dd);

            // validações
            if (dtDe > dtAte)
            {
                this.DsMensagem = new Mensagem()
                {
                    Descricao = "A data inicial deverá ser menor do que a data final!"
                };

                return this;
            }

            string idTipoIn = "";

            switch (tipo)
            {
                case "B":
                    idTipoIn = "1";
                    break;
                case "D":
                    idTipoIn = "2";
                    break;
                case "T":
                    idTipoIn = "6";
                    break;
                case "A":
                    if (dtDe != dtAte)
                    {
                        this.DsMensagem = new Mensagem()
                        {
                            Descricao = "Se a data inicial for diferente da data final, não poderá ser utilizado o tipo=A, para se evitar problema de tamanho do XML!"
                        };
                        return this;
                    }
                    idTipoIn = "A";
                    break;
                default:
                    this.DsMensagem = new Mensagem()
                    {
                        Descricao = "Parâmetro Tipo(" + tipo + ") definido incorretamente!"
                    };
                    return this;
            }

            this.Oficios = this.Processar(dtDe, dtAte, idTipoIn);

            return this;
        }

        public List<Oficio> ListarOficios(DateTime dtIni, DateTime dtFim, string idTipo)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    string sSql = "SELECT DISTINCT " +
                                "   OFINST.ID_INSTITUICAO, " +
                                "   OFINST.ID_LOTE, " +
                                "   OFI.ID_OFICIO, " +
                                "   OFI.ID_TIPO_FONTE, " +
                                "   OFI.ID_SOLICITANTE, " +
                                "   OFI.ID_TIPO, " +
                                "   ID_TIPO_NATUREZA_ACAO, " +
                                "   OFI.IDENTIFICADOR, " +
                                "   OFINST.DT_ENVIO_BC, " +
                                "   OFI.CD_PROCESSO, " +
                                "   ENVINST.NOME_AUTOR_EXEQUENTE, " +
                                "   OFI.VALOR_ORDEM " +
                                "FROM " +
                                    OwnerTable + "OFICIOS OFI, " +
                                    OwnerTable + "OFICIOS_POR_INSTITUICAO OFINST, " +
                                    OwnerTable + "ENVOLVIDOS_POR_INST ENVINST, " +
                                    OwnerTable + "ENVOLVIDOS_NOMES NOM, " +
                                    OwnerTable + "ENVOLVIDOS ENV, " +
                                    OwnerTable + "PAN_CTA_UNICA PAN " +
                                "WHERE " +
                                "   OFI.ID_TIPO = @ID_TIPO " +
                                "   AND OFI.ID_OFICIO = OFINST.ID_OFICIO " +
                                "   AND OFINST.ID_INSTITUICAO = ENVINST.ID_INSTITUICAO " +
                                "   AND OFINST.ID_LOTE = ENVINST.ID_LOTE " +
                                "   AND OFINST.ID_OFICIO = ENVINST.ID_OFICIO " +
                                "   AND ENVINST.ID_ENVOLVIDO = NOM.ID_ENVOLVIDO " +
                                "   AND NOM.ID_PESSOA = ENV.ID_PESSOA " +
                                "   AND PAN.CNPJCPF = ENV.CD_DOCUMENTO " +
                                "   AND (OFINST.DT_ENVIO_BC >= @DT_ENVIO_BC_INI " +
                                "   AND OFINST.DT_ENVIO_BC <= @DT_ENVIO_BC_FIM)";

                    if (idTipo == "A")
                    {
                        sSql = "SELECT DISTINCT " +
                                "   OFINST.ID_INSTITUICAO, " +
                                "   OFINST.ID_LOTE, " +
                                "   OFI.ID_OFICIO, " +
                                "   OFI.ID_TIPO_FONTE, " +
                                "   OFI.ID_SOLICITANTE, " +
                                "   OFI.ID_TIPO, " +
                                "   ID_TIPO_NATUREZA_ACAO, " +
                                "   OFI.IDENTIFICADOR, " +
                                "   OFINST.DT_ENVIO_BC, " +
                                "   OFI.CD_PROCESSO, " +
                                "   ENVINST.NOME_AUTOR_EXEQUENTE, " +
                                "   OFI.VALOR_ORDEM " +
                                "FROM " +
                                    OwnerTable + "OFICIOS OFI, " +
                                    OwnerTable + "OFICIOS_POR_INSTITUICAO OFINST, " +
                                    OwnerTable + "ENVOLVIDOS_POR_INST ENVINST, " +
                                    OwnerTable + "ENVOLVIDOS_NOMES NOM, " +
                                    OwnerTable + "ENVOLVIDOS ENV, " +
                                    OwnerTable + "PAN_CTA_UNICA PAN " +
                                "WHERE " +
                                "   OFI.ID_TIPO IN (1, 2, 6) " +
                                "   AND OFI.ID_OFICIO = OFINST.ID_OFICIO " +
                                "   AND OFINST.ID_INSTITUICAO = ENVINST.ID_INSTITUICAO " +
                                "   AND OFINST.ID_LOTE = ENVINST.ID_LOTE " +
                                "   AND OFINST.ID_OFICIO = ENVINST.ID_OFICIO " +
                                "   AND ENVINST.ID_ENVOLVIDO = NOM.ID_ENVOLVIDO " +
                                "   AND NOM.ID_PESSOA = ENV.ID_PESSOA " +
                                "   AND PAN.CNPJCPF = ENV.CD_DOCUMENTO " +
                                "   AND ( OFINST.DT_ENVIO_BC >= @DT_ENVIO_BC_INI " +
                                "   AND OFINST.DT_ENVIO_BC <= @DT_ENVIO_BC_FIM ) ";
                    }

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        sSql = sSql.Replace("@", ":PARAM_");
                    }

                    var command = DbConnect.CreateCommand(sSql, CommandType.Text, connection);

                    if (idTipo != "A")
                    {
                        DbConnect.CreateInputParameter("ID_TIPO", idTipo.ToInt(), command);
                    }

                    DbConnect.CreateInputParameter("DT_ENVIO_BC_INI", dtIni, command);
                    DbConnect.CreateInputParameter("DT_ENVIO_BC_FIM", dtFim, command);

                    var dr = command.ExecuteReader();

                    var lista = new List<Oficio>();

                    while (dr.Read())
                    {
                        lista.Add(new Oficio()
                        {
                            IdInstituicao = dr["ID_INSTITUICAO"].ToInt(),
                            IdLote = dr["ID_LOTE"].ToInt(),
                            IdOficio = dr["ID_OFICIO"].ToInt(),
                            IdTipoFonte = dr["ID_TIPO_FONTE"].ToInt(),
                            IdSolicitante = dr["ID_SOLICITANTE"].ToInt(),
                            IdTipo = dr["ID_TIPO"].ToInt(),
                            IdTipoNaturezaAcao = dr["ID_TIPO_NATUREZA_ACAO"].ToNullableInt(),
                            Protocolo = dr["IDENTIFICADOR"].ToString(),
                            DataSisbacen = dr["DT_ENVIO_BC"].ToDateTime().ToString("dd/MM/yyyy"),
                            NumProcesso = dr["CD_PROCESSO"].ToString(),
                            NomeAutor = dr["NOME_AUTOR_EXEQUENTE"].ToString(),
                            ValorOrdem = dr["VALOR_ORDEM"].ToDecimal().ToString("0.00")
                        });
                    }

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


        public string ObterDataProtocoloBacendjud(int idOficio)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    string sSql = "SELECT " +
                                "   DT_PROTOCOLO " +
                                "FROM " +
                                    OwnerTable + "OFICIOS_BACENJUD2 " +
                                "WHERE " +
                                "   ID_OFICIO = @ID_OFICIO ";

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        sSql = sSql.Replace("@", ":PARAM_");
                    }

                    var command = DbConnect.CreateCommand(sSql, CommandType.Text, connection);

                    DbConnect.CreateInputParameter("ID_OFICIO", idOficio, command);

                    var dr = command.ExecuteReader();

                    var lista = new List<Oficio>();

                    string dt;

                    if (dr.Read())
                    {
                        dt = dr["DT_PROTOCOLO"].ToDateTime().ToString("dd/MM/yyyy hh:mm:ss");
                    }
                    else
                    {
                        dt = "";
                    }

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return dt;
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

        public SolicitanteOf SelecionarSolicitante(int idSolicitante)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    string sSql = "SELECT DISTINCT " +
                                "   NOME_JUIZ, " +
                                "   COD_VARA_JUIZO, " +
                                "   TRIBUNAL, " +
                                "   JUIZO, " +
                                "   ENDERECO, " +
                                "   COMPLEMENTO, " +
                                "   NUMERO, " +
                                "   BAIRRO, " +
                                "   CIDADE, " +
                                "   UF, " +
                                "   CEP, " +
                                "   DDD_TELEFONE, " +
                                "   TELEFONE, " +
                                "   DDD_FAX, " +
                                "   FAX, " +
                                "   EMAIL " +
                                "FROM " +
                                    OwnerTable + "SOLICITANTES SOL, " +
                                    OwnerTable + "VARAS_JUIZOS VJ " +
                                "WHERE " +
                                "   SOL.ID_VARA_JUIZO = VJ.ID_VARA_JUIZO " +
                                "   AND ID_SOLICITANTE = @ID_SOLICITANTE";

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        sSql = sSql.Replace("@", ":PARAM_");
                    }

                    var command = DbConnect.CreateCommand(sSql, CommandType.Text, connection);

                    DbConnect.CreateInputParameter("ID_SOLICITANTE", idSolicitante, command);

                    var dr = command.ExecuteReader();

                    var listaSol = new SolicitanteOf();
                    var listaEnd = new Endereco();

                    if (dr.Read())
                    {
                        listaEnd.Logradouro = dr["ENDERECO"].ToString() + " " + dr["COMPLEMENTO"].ToString();
                        listaEnd.Numero = dr["NUMERO"].ToString();
                        listaEnd.Bairro = dr["BAIRRO"].ToString();
                        listaEnd.Cidade = dr["CIDADE"].ToString();
                        listaEnd.UF = dr["UF"].ToString();
                        listaEnd.Cep = dr["CEP"].ToString();
                        listaEnd.Complemento = dr["COMPLEMENTO"].ToString();

                        listaSol.NomeSolicitante = dr["NOME_JUIZ"].ToString();
                        listaSol.CodVaraJuizo = dr["COD_VARA_JUIZO"].ToString();
                        listaSol.NomeVaraJuizo = dr["TRIBUNAL"].ToString() + " " + dr["JUIZO"].ToString();

                        listaSol.Telefone = String.IsNullOrEmpty(dr["TELEFONE"].ToString()) ? String.Empty : String.Format("({0}) {1}", dr["DDD_TELEFONE"].ToString(), dr["TELEFONE"].ToString());
                        listaSol.Fax = String.IsNullOrEmpty(dr["FAX"].ToString()) ? String.Empty : String.Format("({0}) {1}", dr["DDD_FAX"].ToString(), dr["FAX"].ToString());
                        listaSol.Email = dr["EMAIL"].ToString();

                        listaSol.EndVaraJuizo = listaEnd;
                    }

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return listaSol;
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

        public DadoTransf SelecionarDadosTransferencia(Oficio oficio, Envolvido envolvido)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    string sSql = "SELECT " +
                                "   ID_TRANSFERENCIA, " +
                                "   BANCO, " +
                                "   AGENCIA, " +
                                "   CPF_CNPJ_FAVORECIDO, " +
                                "   NOME_FAVORECIDO " +
                                "FROM " +
                                    OwnerTable + "ENVOLVIDOS_POR_INST " +
                                "WHERE " +
                                "   ID_ENVOL_INST = @ID_ENVOL_INST ";

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        sSql = sSql.Replace("@", ":PARAM_");
                    }

                    var command = DbConnect.CreateCommand(sSql, CommandType.Text, connection);

                    DbConnect.CreateInputParameter("ID_ENVOL_INST", envolvido.IdEnvolInst, command);

                    var dr = command.ExecuteReader();

                    var dadoTransf = new DadoTransf();

                    if (dr.Read())
                    {
                        dadoTransf.IdTransferencia = dr["ID_TRANSFERENCIA"].ToString();
                        dadoTransf.CnpjBancoDestino = dr["BANCO"].ToString();
                        dadoTransf.AgenciaDestino = dr["AGENCIA"].ToString();
                        dadoTransf.CpfCnpjBeneficiario = dr["CPF_CNPJ_FAVORECIDO"].ToString();
                        dadoTransf.NomeBeneficiario = dr["NOME_FAVORECIDO"].ToString();
                    }

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return dadoTransf;
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

        public string SelecionarNomeBancoDestino(string cnpj)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    string sSql = "SELECT " +
                                "   NOME_INSTITUICAO_TRANSF " +
                                "FROM " +
                                    OwnerTable + "INSTITUICOES_TRANSFERENCIA " +
                                "WHERE " +
                                "   CNPJ_INSTITUICAO_TRANSF = @CNPJ_INSTITUICAO_TRANSF ";

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        sSql = sSql.Replace("@", ":PARAM_");
                    }

                    var command = DbConnect.CreateCommand(sSql, CommandType.Text, connection);

                    DbConnect.CreateInputParameter("CNPJ_INSTITUICAO_TRANSF", cnpj, command, TextParamTypeEnum.VarChar, 10);

                    var dr = command.ExecuteReader();

                    string retorno = "";

                    if (dr.Read())
                    {
                        retorno = dr["NOME_INSTITUICAO_TRANSF"].ToString();
                    }

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return retorno;
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

        public DadoTransf AtualizarDadosTransferencia(Envolvido envolvido, DadoTransf dadosTransf)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    string sSql = "SELECT " +
                            "   NROORIGEM, " +
                            "   STATUS_REQUISICAO " +
                            "FROM " +
                                OwnerTable + "AUTKINTMOV_GRA_REQ_TES " +
                            "WHERE " +
                            "   ID_ENVOL_INST = @ID_ENVOL_INST ";

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        sSql = sSql.Replace("@", ":PARAM_");
                    }

                    var command = DbConnect.CreateCommand(sSql, CommandType.Text, connection);

                    DbConnect.CreateInputParameter("ID_ENVOL_INST", envolvido.IdEnvolInst, command);

                    var dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        dadosTransf.IdPagamento = dr["NROORIGEM"].ToString();
                        // matsutami 
                        // atualmente o campo que grava o status é FG_STATUS_REQUISICAO
                        // mas grava somente na conclusao
                        // precisa criar um novo campo que grave os status da requisição
                        // no momento da pesquisa
                        dadosTransf.SituacaoPagamento = dr["STATUS_REQUISICAO"].ToString(); 
                    }

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return dadosTransf;
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

        public string SelecionarDadosProcessamentoValorExecutado(Oficio oficio, Envolvido envolvido)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    string sSql = "SELECT " +
                                "   SUM(SALDO_BLOQUEADO) AS TOTAL_EFETIVADO " +
                                "FROM " +
                                    OwnerTable + "TRANSACOES_CONTA " +
                                "WHERE " +
                                "   FG_CONCLUIDA = 1 " +
                                "   AND FG_ANULADA = 0 " +
                                "   AND ID_INSTITUICAO_PESQUISA = @ID_INSTITUICAO_PESQUISA " +
                                "   AND ID_OFICIO = @ID_OFICIO " +
                                "   AND ID_ENVOL_INST = @ID_ENVOL_INST ";

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        sSql = sSql.Replace("@", ":PARAM_");
                    }

                    var command = DbConnect.CreateCommand(sSql, CommandType.Text, connection);

                    DbConnect.CreateInputParameter("ID_INSTITUICAO_PESQUISA", oficio.IdInstituicao, command);
                    DbConnect.CreateInputParameter("ID_OFICIO", oficio.IdOficio, command);
                    DbConnect.CreateInputParameter("ID_ENVOL_INST", envolvido.IdEnvolInst, command);

                    var dr = command.ExecuteReader();

                    decimal retorno = 0;

                    if (dr.Read())
                    {
                        if (dr["TOTAL_EFETIVADO"] != DBNull.Value)
                        {
                            retorno = dr["TOTAL_EFETIVADO"].ToDecimal();
                        }
                    }

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return retorno.ToString("0.00");
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

        public string SelecionarDadosProcessamentoDataProcessamento(Oficio oficio, Envolvido envolvido)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    string sSql = "SELECT " +
                                "   MAX(DATA_TRANSACAO) AS MAX_DATA_TRANSACAO " +
                                "FROM " +
                                    OwnerTable + "TRANSACOES_CONTA " +
                                "WHERE " +
                                "   FG_CONCLUIDA = 1 " +
                                "   AND FG_ANULADA = 0 " +
                                "   AND ID_INSTITUICAO_PESQUISA = @ID_INSTITUICAO_PESQUISA " +
                                "   AND ID_OFICIO = @ID_OFICIO " +
                                "   AND ID_ENVOL_INST = @ID_ENVOL_INST ";

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        sSql = sSql.Replace("@", ":PARAM_");
                    }

                    var command = DbConnect.CreateCommand(sSql, CommandType.Text, connection);

                    DbConnect.CreateInputParameter("ID_INSTITUICAO_PESQUISA", oficio.IdInstituicao, command);
                    DbConnect.CreateInputParameter("ID_OFICIO", oficio.IdOficio, command);
                    DbConnect.CreateInputParameter("ID_ENVOL_INST", envolvido.IdEnvolInst, command);

                    var dr = command.ExecuteReader();

                    string retorno = "";

                    if (dr.Read())
                    {
                        if (dr["MAX_DATA_TRANSACAO"] != DBNull.Value)
                        {
                            retorno = dr["MAX_DATA_TRANSACAO"].ToDateTime().ToString("dd/MM/yyyy");
                        }
                    }

                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }

                    return retorno;
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

        public List<Envolvido> ListarEnvolvidos(Oficio oficio)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    string sSql = "SELECT DISTINCT " +
                            "   ENVINST.ID_ENVOL_INST, " +
                            "   NOM.NOME_ENVOLVIDO, " +
                            "   ENV.CD_DOCUMENTO, " +
                            "   ENV.ID_TIPO_DOCUMENTO, " +
                            "   ENVINST.SEQUENCIAL, " +
                            "   ENVINST.REITERACAO_BLOQUEIO, " +
                            "   ENVINST.SEQUENCIA_DESBLOQUEIO, " +
                            "   ENVINST.REITERACAO_DESBLOQUEIO, " +
                            "   ENVINST.SEQUENCIA_TRANSFERENCIA, " +
                            "   ENVINST.REITERACAO_TRANSF, " +
                            "   ENVINST.TIPO_DEPOSITO_JUDICIAL, " +
                            "   ENVINST.VALOR_POR_ENV, " +
                            "   ENVINST.FG_DESBLOQUEAR_CONTAS, " +
                            "   ENVINST.FG_DESBLOQUEAR_SALDO, " +
                            "   ENVINST.AGENCIA, " +
                            "   ENVINST.CONTA, " +
                            "   ENVINST.CNPJ_IF_RESPONDER, " +
                            "   ENVINST.COD_DEPOSITO_RECEITA, " +
                            "   ENVINST.NUM_REF_JUDICIAL " +
                            " FROM " +
                                OwnerTable + "ENVOLVIDOS_POR_INST ENVINST, " +
                                OwnerTable + "ENVOLVIDOS_NOMES NOM, " +
                                OwnerTable + "ENVOLVIDOS ENV, " +
                                OwnerTable + "PAN_CTA_UNICA PAN " +
                            " WHERE " +
                            "   ENVINST.ID_ENVOLVIDO = NOM.ID_ENVOLVIDO " +
                            "   AND NOM.ID_PESSOA = ENV.ID_PESSOA " +
                            "   AND PAN.CNPJCPF = ENV.CD_DOCUMENTO " +
                            "   AND ENVINST.ID_INSTITUICAO = @ID_INSTITUICAO " +
                            "   AND ENVINST.ID_LOTE = @ID_LOTE " +
                            "   AND ENVINST.ID_OFICIO = @ID_OFICIO ";

                    if (Config.GetProviderName(Constants.KeyConnectionStringName) == Constants.OracleDataAccessClient)
                    {
                        sSql = sSql.Replace("@", ":PARAM_");
                    }

                    var command = DbConnect.CreateCommand(sSql, CommandType.Text, connection);

                    DbConnect.CreateInputParameter("ID_INSTITUICAO", oficio.IdInstituicao, command);
                    DbConnect.CreateInputParameter("ID_LOTE", oficio.IdLote, command);
                    DbConnect.CreateInputParameter("ID_OFICIO", oficio.IdOficio, command);

                    var dr = command.ExecuteReader();

                    var lista = new List<Envolvido>();

                    while (dr.Read())
                    {
                        var env = new Envolvido();
                        env.IdEnvolInst = dr["ID_ENVOL_INST"].ToInt();
                        env.NomeEnvolvido = dr["NOME_ENVOLVIDO"].ToString();
                        env.CpfCnpj = dr["CD_DOCUMENTO"].ToString();
                        env.TipoPessoa = dr["ID_TIPO_DOCUMENTO"].ToString();
                        env.Reiteracao = "N";
                        env.SequencialBloqueio = dr["SEQUENCIAL"].ToString();
                        env.SeqReiteracaoBloqueio = dr["REITERACAO_BLOQUEIO"].ToString();
                        env.SequencialDesbloqueio = "";
                        env.SeqReiteracaoDesbloqueio = "";
                        env.SequencialTransferencia = "";
                        env.SeqReiteracaoTransf = "";
                        env.TipoDeposito = dr["TIPO_DEPOSITO_JUDICIAL"].ToString();
                        env.Agencia = "";
                        env.Conta = "";

                        if (oficio.IdTipoFonte == 2)
                        {
                            env.ValorOrdem = dr["VALOR_POR_ENV"].ToDecimal().ToString("0.00");
                        }
                        else
                        {
                            env.ValorOrdem = oficio.ValorOrdem;
                        }

                        env.InstituicaoFinanceira = dr["CNPJ_IF_RESPONDER"].ToString();
                        env.CodigoDeposito = dr["COD_DEPOSITO_RECEITA"].ToString();
                        env.NumeroReferencia = dr["NUM_REF_JUDICIAL"].ToString();

                        if ((new[] { 2, 6 }.Contains(oficio.IdTipo)) &&
                            (dr["FG_DESBLOQUEAR_CONTAS"].ToInt() == 1) ||
                            (dr["FG_DESBLOQUEAR_SALDO"].ToInt() == 1))
                        {
                            env.DesbloquearSaldoRemanescente = "S";
                        }
                        else
                        {
                            env.DesbloquearSaldoRemanescente = "N";
                        }

                        if (oficio.IdTipo == 1)
                        {
                            env.Agencia = dr["AGENCIA"].ToString();
                            env.Conta = dr["CONTA"].ToString();

                            if (dr["REITERACAO_BLOQUEIO"].ToInt() > 0)
                            {
                                env.Reiteracao = "S";
                            }
                        }
                        else if (oficio.IdTipo == 2)
                        {
                            env.SequencialDesbloqueio = dr["SEQUENCIA_DESBLOQUEIO"].ToString();
                            env.SeqReiteracaoDesbloqueio = dr["REITERACAO_DESBLOQUEIO"].ToString();

                            if (dr["REITERACAO_DESBLOQUEIO"].ToInt() > 0)
                            {
                                env.Reiteracao = "S";
                            }
                        }
                        else if (oficio.IdTipo == 6)
                        {
                            env.SequencialTransferencia = dr["SEQUENCIA_TRANSFERENCIA"].ToString();
                            env.SeqReiteracaoTransf = dr["REITERACAO_TRANSF"].ToString();

                            if (dr["REITERACAO_TRANSF"].ToInt() > 0)
                            {
                                env.Reiteracao = "S";
                            }
                        }

                        lista.Add(env);
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

        public List<Oficio> Processar(DateTime dtDe, DateTime dtAte, string idTipo)
        {
            this.Oficios = this.ListarOficios(dtDe, dtAte, idTipo);

            foreach (var oficio in this.Oficios)
            {
                if (oficio.IdTipoFonte == 2)
                {
                    oficio.DataProtocolo = this.ObterDataProtocoloBacendjud(oficio.IdOficio);
                }
                else
                {
                    oficio.DataProtocolo = oficio.DataSisbacen;
                }

                switch (oficio.IdTipo)
                {
                    case 1:
                        oficio.Tipo = "Bloqueio";
                        break;
                    case 2:
                        oficio.Tipo = "Desbloqueio";
                        break;
                    case 6:
                        oficio.Tipo = "Transferencia";
                        break;
                    default:
                        oficio.Tipo = "";
                        break;
                }

                switch (oficio.IdTipoNaturezaAcao)
                {
                    case 1:
                        oficio.TipoJustica = "Ação cível";
                        break;
                    case 2:
                        oficio.TipoJustica = "Ação criminal";
                        break;
                    case 3:
                        oficio.TipoJustica = "Ação trabalhista";
                        break;
                    case 4:
                        oficio.TipoJustica = "Execução fiscal";
                        break;
                    case 5:
                        oficio.TipoJustica = "Execução de alimentos";
                        break;
                    default:
                        oficio.TipoJustica = "";
                        break;
                }

                oficio.Solicitante = this.SelecionarSolicitante(oficio.IdSolicitante);

                oficio.Envolvidos = this.ListarEnvolvidos(oficio);

                foreach (var envolvido in oficio.Envolvidos)
                {
                    envolvido.DadosProcessamento.ValorExecutado = this.SelecionarDadosProcessamentoValorExecutado(oficio, envolvido);
                    envolvido.DadosProcessamento.DataProcessamento = this.SelecionarDadosProcessamentoDataProcessamento(oficio, envolvido);

                    if (oficio.IdTipo == 6)
                    {
                        envolvido.DadosTransferencia = this.SelecionarDadosTransferencia(oficio, envolvido);
                        envolvido.DadosTransferencia.NomeBancoDestino = this.SelecionarNomeBancoDestino(envolvido.DadosTransferencia.CnpjBancoDestino);
                        envolvido.DadosTransferencia = this.AtualizarDadosTransferencia(envolvido, envolvido.DadosTransferencia);
                    }

                }
            }
            return this.Oficios;
        }
    }
}