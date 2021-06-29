using Presenta.CA.Model;
using Presenta.CA.Model.Enum;
using Presenta.Common.Security;
using Presenta.Common.Util;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Linq;
using System.Linq.Dynamic;
using Presenta.Util.ActiveDirectory;
using System.IO;
using Presenta.WS.CA.Model;
using System.Data.SqlClient;
using static Presenta.WS.CA.Model.ListarOperadoresAplicativoStatusResponse;

namespace Presenta.WS.CA
{
    /// <summary>
    /// Summary description for CAv2Service
    /// </summary>
    [WebService(Namespace = "http://presenta.com.br/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class CAv2Service : System.Web.Services.WebService
    {
        [WebMethod]
        public List<AplicativoFlyWeight> ListarAplicativosPorSistema(int idSistema)
        {
            int idOperadorLog = 1;
            try
            {
                var lista = new List<AplicativoFlyWeight>();
                var caAplicativoModel = new CaAplicativoModel() { IdSistema = idSistema }.Listar();
                foreach (var item in caAplicativoModel)
                {
                    lista.Add(new AplicativoFlyWeight() { Id = item.IdAplicativo, Descricao = item.DsAplicativo });
                }

                return lista;
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        [WebMethod]
        public List<AplicativoFlyWeight> ListarParesDeAplicativos(int idAplicativo)
        {
            int idOperadorLog = 1;
            try
            {
                var lista = new List<AplicativoFlyWeight>();
                var idSistema = new CaAplicativoModel() { IdAplicativo = idAplicativo }.Selecionar().IdSistema;
                var caAplicativoModel = new CaAplicativoModel() { IdSistema = idSistema }.Listar();
                foreach (var item in caAplicativoModel)
                {
                    lista.Add(new AplicativoFlyWeight() { Id = item.IdAplicativo, Descricao = item.DsAplicativo });
                }

                return lista;
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    int idAppp = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAppp, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        [WebMethod]
        public List<OperadorFlyWeight> ListarOperadores()
        {
            int idOperadorLog = 1;
            try
            {
                var lista = new List<OperadorFlyWeight>();
                var caOperadores = new CaOperadorModel() { }.Listar();
                foreach (var item in caOperadores)
                {
                    lista.Add(new OperadorFlyWeight() { Id = item.IdOperador, Nome = item.NmOperador });
                }

                return lista;
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    int idAppp = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAppp, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        [WebMethod]
        public List<CaOperadorFlyWeight> ListarOperadoresAtivosPorAplicativo(int idAplicativo)
        {
            int idOperadorLog = 1;

            try
            {
                var caFuncionalidade = new CaFuncionalidadeModel();
                caFuncionalidade.IdAplicativo = idAplicativo;
                var listaCaFuncionalidade = caFuncionalidade.Listar();

                var caFuncionalidadePerfil = new CaFuncionalidadePerfilModel();
                caFuncionalidadePerfil.StFuncionalidadePerfil = (int)CaSituacaoFuncionalidadePerfilEnum.Ativo;
                var listaCaFuncionalidadePerfil = caFuncionalidadePerfil.Listar();

                var caPerfilOperador = new CaPerfilOperadorModel();
                caPerfilOperador.StPerfilOperador = (int)CaSituacaoPerfilEnum.Ativo;
                var listaCaPerfilOperador = caPerfilOperador.Listar();

                var caOperador = new CaOperadorModel();
                caOperador.StOperador = (int)CaSituacaoOperadorEnum.Ativo;
                var listaCaOperador = caOperador.Listar();

                var lista = (from f in listaCaFuncionalidade
                             join fp in listaCaFuncionalidadePerfil
                             on f.IdFuncionalidade equals fp.IdFuncionalidade into listaFPF
                             from fpf in listaFPF
                             join po in listaCaPerfilOperador
                             on fpf.IdPerfil equals po.IdPerfil into listaFPFPO
                             from fpfpo in listaFPFPO
                             join o in listaCaOperador
                             on fpfpo.IdOperador equals o.IdOperador
                             select new CaOperadorFlyWeight
                             {
                                 CdOperador = o.CdOperador,
                                 DsEmail = o.DsEmail,
                                 IdOperador = o.IdOperador,
                                 NmOperador = o.NmOperador,
                                 StOperador = o.StOperador
                             }).ToList()
                            .GroupBy(g => new
                            {
                                g.CdOperador,
                                g.DsEmail,
                                g.IdOperador,
                                g.NmOperador,
                                g.StOperador
                            })
                            .Select(s => s.First()).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                File.WriteAllText(Path.Combine(Server.MapPath("~/bin"), "Erro.log"), errorMessage);

                throw ex;
            }
        }

        [WebMethod]
        public string[] ListarEmails()
        {
            int idOperadorLog = 1;
            try
            {
                var dados = new List<string>();
                var caoperador = new CaOperadorModel().Listar();
                foreach (var item in caoperador)
                {
                    if ((!item.DsEmail.IsDBNull()) && (item.DsEmail.Trim() != ""))
                    {
                        dados.Add(item.DsEmail);
                    }
                }
                dados.Sort();
                return dados.ToArray();
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        [WebMethod]
        public bool DeveTrocarSenha(string login, string senha, int idAplicativo)
        {
            int idOperadorLog = 1;

            try
            {
                idOperadorLog = ObterIdOperadorLog(login);

                if (Autenticar(login, senha, idAplicativo))
                {
                    var caConfiguracao = new CaConfiguracaoModel(1).Selecionar();

                    if (caConfiguracao != null)
                    {
                        var caLog = new CaLogModel(idOperadorLog);

                        if (caConfiguracao.IdTipoLogon == (int)CaTipoLogonEnum.Windows)
                        {
                            caLog.LogarInfo(idAplicativo, "Login via windows authentication.");
                            return false;
                        }

                        CaOperadorModel caOperador = new CaOperadorModel();
                        caOperador.CdOperador = login;
                        var listaCaOperador = caOperador.Listar();

                        if (listaCaOperador == null)
                        {
                            throw new Exception("Usuário (" + login + ") erro ao acessar os dados do operador.");
                        }


                        if ((listaCaOperador[0].IdOperador != 1) && (listaCaOperador[0].CrSenha == null) || (listaCaOperador[0].CrSenha.Trim() == ""))
                        {
                            if (caConfiguracao.SenhaPadraoDescriptografada == senha)
                            {
                                caLog.LogarInfo(idAplicativo, "É o primeiro login ou a senha foi resetada.");
                                return true;
                            }
                        }

                        if ((listaCaOperador[0].IdOperador != 1) && (listaCaOperador[0].DtSenha != null) && (((DateTime)listaCaOperador[0].DtSenha).AddDays(caConfiguracao.DiasTrocaSenha.ToDouble()) <= DateTime.Now))
                        {
                            caLog.LogarInfo(idAplicativo, "A troca de senha é obrigatória.");
                            return true;
                        }

                        caLog.LogarInfo(idAplicativo, "A troca de senha não é necessária.");
                        return false;
                    }
                    else
                    {
                        throw new Exception("Usuário (" + login + ") Erro ao Obter Configuração do CA.");
                    }
                }
                else
                {
                    throw new Exception("Usuário (" + login + ") Erro ao Verificar Primeiro Login.");
                }
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        [WebMethod]
        public bool IsSenhaPadrao(string senha)
        {
            int idOperadorLog = 1;

            try
            {
                var caConfiguracao = new CaConfiguracaoModel(1).Selecionar();

                if (caConfiguracao != null)
                {
                    if (caConfiguracao.SenhaPadraoDescriptografada == senha)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        [WebMethod]
        public bool TrocarSenha(string login, string senha, string novaSenha, int idAplicativo)
        {
            int idOperadorLog = 1;

            try
            {
                idOperadorLog = ObterIdOperadorLog(login);

                if (Autenticar(login, senha, idAplicativo))
                {
                    if (IsSenhaPadrao(novaSenha))
                    {
                        return false;
                    }

                    CaOperadorModel caOperador = new CaOperadorModel();
                    caOperador.CdOperador = login;
                    var listaCaOperador = caOperador.Listar();

                    if (listaCaOperador != null && listaCaOperador.Count == 1)
                    {
                        listaCaOperador[0].StOperador = (int)CaSituacaoOperadorEnum.Ativo;
                        listaCaOperador[0].QtLoginIncorreto = 0;
                        listaCaOperador[0].CrSenha = Cryptographer.Encrypt(novaSenha);
                        listaCaOperador[0].DtSenha = DateTime.Now;
                        listaCaOperador[0].DhAtualizacao = DateTime.Now;
                        listaCaOperador[0].IdOperadorAtualizacao = idOperadorLog;
                        if (listaCaOperador[0].DhUltimoLogin == null)
                        {
                            listaCaOperador[0].DhUltimoLogin = new DateTime?();
                        }
                        listaCaOperador[0].Atualizar();

                        CaHistoricoSenhaModel caHistoricoSenhaModel = new CaHistoricoSenhaModel();
                        caHistoricoSenhaModel.IdOperador = listaCaOperador[0].IdOperador;
                        caHistoricoSenhaModel.DhHistorico = DateTime.Now;
                        caHistoricoSenhaModel.CrSenha = Cryptographer.Encrypt(novaSenha);
                        caHistoricoSenhaModel.DhCadastro = DateTime.Now;
                        caHistoricoSenhaModel.Inserir();

                        var caLog = new CaLogModel(idOperadorLog);
                        caLog.LogarInfo(idAplicativo, "Troca de senha efetuada com sucesso.");

                        return true;
                    }
                    else
                    {
                        throw new Exception("Usuário (" + login + ") Erro ao dados do Operador CA.");
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        [WebMethod]
        public int ObterTipoAutenticacao()
        {
            try
            {
                var config = new CaConfiguracaoModel(1).Selecionar();
                return config.IdTipoLogon;
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;

            }

        }

        [WebMethod]
        public bool Autenticar(string login, string senha, int idAplicativo)
        {
            int idOperadorLog = 1;

            try
            {
                var caConfiguracao = new CaConfiguracaoModel(1).Selecionar();
                var caLog = new CaLogModel(idOperadorLog);

                idOperadorLog = ObterIdOperadorLog(login);

                CaOperadorModel caOperador = new CaOperadorModel();

                var listaCaOperador = new List<CaOperadorModel>();

                if (!String.IsNullOrEmpty(login))
                {
                    caOperador.CdOperador = login;
                    listaCaOperador = caOperador.Listar();
                }

                if (listaCaOperador.Count == 0)
                {
                    caLog.LogarErro(idAplicativo, "Usuário (" + login + ") inexistente.");
                    return false;
                }

                if (caConfiguracao.IdTipoLogon == (int)CaTipoLogonEnum.Senha)
                {
                    if ((listaCaOperador[0].CrSenha == null) || (listaCaOperador[0].CrSenha.Trim() == ""))
                    {
                        // novo usuario ou reset de senha
                        if (caConfiguracao.SenhaPadraoDescriptografada != senha)
                        {
                            CalcularLoginIncorreto(login, idAplicativo, caConfiguracao, caLog, listaCaOperador);
                            listaCaOperador[0].Atualizar();

                            return false;
                        }
                        listaCaOperador[0].DhUltimoLogin = DateTime.Now;
                        listaCaOperador[0].QtLoginIncorreto = 0;
                        listaCaOperador[0].Atualizar();

                        caLog.IdOperador = listaCaOperador[0].IdOperador;
                        caLog.LogarInfo(idAplicativo, "Autenticado com sucesso.");
                        return true;
                    }

                    if (Cryptographer.Decrypt(listaCaOperador[0].CrSenha) != senha)
                    {
                        if (listaCaOperador[0].DhUltimoLogin == null)
                        {
                            listaCaOperador[0].DhUltimoLogin = new DateTime?();
                        }

                        CalcularLoginIncorreto(login, idAplicativo, caConfiguracao, caLog, listaCaOperador);

                        listaCaOperador[0].Atualizar();

                        return false;
                    }
                }

                if ((listaCaOperador[0].IdOperador != 1) && (listaCaOperador[0].DhUltimoLogin != null && !DateTime.Equals(listaCaOperador[0].DhUltimoLogin, new DateTime(1, 1, 1, 0, 0, 0))) && (((DateTime)listaCaOperador[0].DhUltimoLogin).AddDays(caConfiguracao.DiasDesativSenha.ToDouble()) < DateTime.Now))
                {
                    listaCaOperador[0].StOperador = (int)CaSituacaoOperadorEnum.Inativo;
                    listaCaOperador[0].Atualizar();

                    caLog.LogarErro(idAplicativo, "Usuário (" + login + ") inativado por tempo de logon.");
                    return false;
                }

                if ((listaCaOperador[0].IdOperador != 1) && (listaCaOperador[0].StOperador == (int)CaSituacaoOperadorEnum.Bloqueado))
                {
                    caLog.LogarErro(idAplicativo, "Usuário (" + login + ") bloqueado.");
                    return false;
                }

                if ((listaCaOperador[0].IdOperador != 1) && (listaCaOperador[0].StOperador == (int)CaSituacaoOperadorEnum.Inativo))
                {
                    caLog.LogarErro(idAplicativo, "Usuário (" + login + ") inativo.");
                    return false;
                }

                if (listaCaOperador[0].IdOperador == 1)
                {
                    listaCaOperador[0].StOperador = (int)CaSituacaoOperadorEnum.Ativo;
                }

                listaCaOperador[0].DhUltimoLogin = DateTime.Now;
                listaCaOperador[0].QtLoginIncorreto = 0;
                listaCaOperador[0].Atualizar();

                caLog.IdOperador = listaCaOperador[0].IdOperador;
                caLog.LogarInfo(idAplicativo, "Autenticado com sucesso.");

                return true;
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                File.WriteAllText(Path.Combine(Server.MapPath("~/bin"), "Erro.log"), errorMessage);

                throw ex;
            }
        }

        private static void CalcularLoginIncorreto(string login, int idAplicativo, CaConfiguracaoModel caConfiguracao, CaLogModel caLog, List<CaOperadorModel> listaCaOperador)
        {
            listaCaOperador[0].QtLoginIncorreto++;

            if ((listaCaOperador[0].IdOperador != 1) && (listaCaOperador[0].QtLoginIncorreto >= caConfiguracao.MaxTentinValidas))
            {
                listaCaOperador[0].StOperador = (int)CaSituacaoOperadorEnum.Bloqueado;
                caLog.LogarErro(idAplicativo, "Usuário (" + login + ") bloqueado por senha inválida.");
            }
            else
            {
                caLog.LogarErro(idAplicativo, "Usuário (" + login + ") senha inválida.");
            }
        }

        [WebMethod]
        public int[] Autorizar(string login, string senha, int idAplicativo)
        {
            int idOperadorLog = 1;

            try
            {
                idOperadorLog = ObterIdOperadorLog(login);

                if (Autenticar(login, senha, idAplicativo))
                {
                    var caFuncionalidade = new CaFuncionalidadeModel();
                    caFuncionalidade.IdAplicativo = idAplicativo;
                    var listaCaFuncionalidade = caFuncionalidade.Listar();

                    var caPerfilOperador = new CaPerfilOperadorModel();
                    caPerfilOperador.IdOperador = idOperadorLog;
                    caPerfilOperador.StPerfilOperador = (int)CaSituacaoPerfilEnum.Ativo;
                    var listaCaPerfilOperador = caPerfilOperador.Listar();

                    var dados = new List<int>();

                    foreach (var itemCaPerfilOperador in listaCaPerfilOperador)
                    {
                        var caFuncionalidadePerfil = new CaFuncionalidadePerfilModel();
                        caFuncionalidadePerfil.IdPerfil = itemCaPerfilOperador.IdPerfil;
                        caFuncionalidadePerfil.StFuncionalidadePerfil = (int)CaSituacaoFuncionalidadePerfilEnum.Ativo;
                        var listaCaFuncionalidadePerfil = caFuncionalidadePerfil.Listar();

                        foreach (var itemCaFuncionalidadePerfil in listaCaFuncionalidadePerfil)
                        {
                            foreach (var itemCaFuncionalidade in listaCaFuncionalidade)
                            {
                                if (itemCaFuncionalidadePerfil.IdFuncionalidade == itemCaFuncionalidade.IdFuncionalidade)
                                {
                                    dados.Add(itemCaFuncionalidadePerfil.IdFuncionalidade);
                                    break;
                                }
                            }
                        }
                    }

                    var caLog = new CaLogModel(idOperadorLog);

                    if (dados.Count == 0)
                    {
                        caLog.LogarErro(idAplicativo, "Usuário (" + login + ") sem permissões.");
                        return null;
                    }

                    caLog.LogarInfo(idAplicativo, "Autorizado com sucesso.");

                    return dados.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                File.WriteAllText(Path.Combine(Server.MapPath("~/bin"), "Erro.log"), errorMessage);

                throw ex;
            }
        }

        [WebMethod]
        public int[] ObterPerfil(string login, string senha, int idAplicativo)
        {
            int idOperador = 1;

            try
            {
                idOperador = ObterIdOperadorLog(login);

                if (Autenticar(login, senha, idAplicativo))
                {
                    var caPerfilOperador = new CaPerfilOperadorModel()
                    {
                        IdOperador = idOperador,
                        StPerfilOperador = (int)CaSituacaoPerfilOperadorEnum.Ativo
                    };

                    var caLog = new CaLogModel(idOperador);
                    var listaCaPerfilOperador = caPerfilOperador.Listar();

                    if (listaCaPerfilOperador != null && listaCaPerfilOperador.Count > 0)
                    {
                        var dados = listaCaPerfilOperador.GroupBy(p => p.IdPerfil).Select(g => g.First()).Select(s => s.IdPerfil).ToList();

                        caLog.LogarInfo(idAplicativo, "Perfis listados com sucesso.");

                        return dados.ToArray();
                    }
                    else
                    {
                        caLog.LogarErro(idAplicativo, "Usuário (" + login + ") não possui perfil associado.");
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperador);
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        [WebMethod]
        public string[] ObterInfo(string login, string senha, int idAplicativo)
        {
            int idOperadorLog = 1;

            try
            {
                idOperadorLog = ObterIdOperadorLog(login);

                if (Autenticar(login, senha, idAplicativo))
                {
                    CaOperadorModel caOperador = new CaOperadorModel();
                    caOperador.CdOperador = login;

                    var caLog = new CaLogModel(idOperadorLog);
                    var listaCaOperador = caOperador.Listar();

                    if (listaCaOperador != null && listaCaOperador.Count == 1)
                    {
                        var dados = new List<string>();
                        dados.Add(listaCaOperador[0].IdOperador.ToString());
                        dados.Add(listaCaOperador[0].NmOperador.ToString());

                        caLog.LogarInfo(idAplicativo, "Informações obtidas com sucesso.");

                        return dados.ToArray();
                    }
                    else
                    {
                        caLog.LogarErro(idAplicativo, "Usuário (" + login + ") erro ao acessar os dados do operador.");
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }







        private int ObterIdOperadorLog(string login)
        {
            int idOperadorLog = 1;

            try
            {
                CaOperadorModel caOperador = new CaOperadorModel();
                caOperador.CdOperador = login;
                var listaCaOperador = caOperador.Listar();

                if (listaCaOperador != null && listaCaOperador.Count == 1)
                {
                    idOperadorLog = listaCaOperador[0].IdOperador;
                    return listaCaOperador[0].IdOperador;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(idOperadorLog);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        private CaOperadorModel ObterOperador(string login)
        {
            try
            {
                CaOperadorModel caOperador = new CaOperadorModel();
                caOperador.CdOperador = login;
                var listaCaOperador = caOperador.Listar();

                if (listaCaOperador != null && listaCaOperador.Count == 1)
                {
                    return listaCaOperador[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }
    }
}
