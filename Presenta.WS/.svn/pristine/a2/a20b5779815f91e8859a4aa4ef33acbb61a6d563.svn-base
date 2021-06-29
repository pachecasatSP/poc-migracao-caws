using Presenta.CA.Model;
using Presenta.CA.Model.Enum;
using Presenta.Common.Util;
using Presenta.WS.CA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Services;
using static Presenta.WS.CA.Model.ListarOperadoresAplicativoStatusResponse;

namespace Presenta.WS.CA.CAWSV2
{
    /// <summary>
    /// Summary description for IDMService
    /// </summary>
    [WebService(Namespace = "http://presenta.com.br/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class IDMService : System.Web.Services.WebService
    {

        #region NovoService

        [WebMethod]
        public RetornoOperadorModel CriarOperador(OperadorModel criarOperadorRequest)
        {

            var retornoOperador = new RetornoOperadorModel();

            try
            {
                new Token(Config.GetKeyValue("Token"), criarOperadorRequest.Token);

                criarOperadorRequest.ValidarCriarOperador();

                criarOperadorRequest.IdOperadorAtualizacao = (criarOperadorRequest.IdOperadorAtualizacao == 0 ? 1 : criarOperadorRequest.IdOperadorAtualizacao);

                var caOperadorModel = new CaOperadorModel()
                {
                    DhAtualizacao = DateTime.Now,
                    CdOperador = criarOperadorRequest.CdOperador.Trim(),
                    NmOperador = criarOperadorRequest.NomeOperador.Trim(),
                    DsEmail = criarOperadorRequest.Email.Trim(),
                    StOperador = criarOperadorRequest.StatusOperador,
                    DhSituacao = DateTime.Now,
                    DhUltimoLogin = null,
                    DtCadastro = DateTime.Now,
                    DtSenha = DateTime.Now,
                    IdOperadorAtualizacao = criarOperadorRequest.IdOperadorAtualizacao,
                    IdTipoSenha = new CaTipoSenhaModel() { IdTipoSenha = 1 }.Selecionar().IdTipoSenha,
                    QtLoginIncorreto = 0
                };

                if (caOperadorModel.ExisteOperador())
                    throw new CAException(string.Format("Já existe um operador com o CdOperador {0}", caOperadorModel.CdOperador));

                var configuracao = new CaConfiguracaoModel() { IdConfiguracao = 1 }.Selecionar();

                caOperadorModel.CrSenha = null;
                caOperadorModel.CdGuid = null;

                caOperadorModel.Inserir();

                retornoOperador.IdOperador = caOperadorModel.IdOperador;
                retornoOperador.StatusOperador = caOperadorModel.StOperador;


                var caLog = new CaLogModel(criarOperadorRequest.IdOperadorAtualizacao);
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoLog, Constants.StdMsgInfoInseriu, Constants.StdMsgInfoOperador, criarOperadorRequest.IdOperadorAtualizacao));

                retornoOperador.Mensagem = "Operador criado com sucesso!";

                return retornoOperador;
            }

            catch (CAException cex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        cex.Mensagem);
                try
                {
                    var caLog = new CaLogModel(criarOperadorRequest.IdOperadorAtualizacao == 0 ? 1 : criarOperadorRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOperador.Mensagem = cex.Mensagem;
                return retornoOperador;
            }

            catch (SqlException ex)
            {
                string errorMessage =
                   String.Format(
                       "{0}{1}{2}{3}",
                       ex.Errors[0].Message,
                       Environment.NewLine, Environment.NewLine,
                       ex.Message);

                try
                {
                    var caLog = new CaLogModel(criarOperadorRequest.IdOperadorAtualizacao == 0 ? 1 : criarOperadorRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }

                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOperador.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retornoOperador;
            }

            catch (TokenException tex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        tex.ToMessageAndCompleteStacktrace());
                try
                {
                    var caLog = new CaLogModel(criarOperadorRequest.IdOperadorAtualizacao == 0 ? 1 : criarOperadorRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOperador.Mensagem = tex.Message;
                return retornoOperador;
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
                    var caLog = new CaLogModel(criarOperadorRequest.IdOperadorAtualizacao == 0 ? 1 : criarOperadorRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOperador.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retornoOperador;
            }
        }



        [WebMethod]
        public RetornoOperadorModel EditarOperador(OperadorModel editarOperadorRequest)
        {
            var retornoOperador = new RetornoOperadorModel();

            if (editarOperadorRequest.IdOperador == 0)
                throw new CAException("IdOperador não foi preenchido");

            try
            {
                new Token(Config.GetKeyValue("Token"), editarOperadorRequest.Token);

                var caOperadorModel = new CaOperadorModel();

                caOperadorModel.IdOperador = editarOperadorRequest.IdOperador;

                var operadorBase = caOperadorModel.Selecionar();

                if (operadorBase == null)
                    throw new CAException("Operador não encontrado para edição");

                //manter as informações caso não haja alteração ou o campo não for enviado com valores.
                caOperadorModel.CdOperador = (operadorBase.CdOperador == editarOperadorRequest.CdOperador || string.IsNullOrEmpty(editarOperadorRequest.CdOperador)) ? operadorBase.CdOperador : editarOperadorRequest.CdOperador;
                caOperadorModel.NmOperador = (operadorBase.NmOperador == editarOperadorRequest.NomeOperador || string.IsNullOrEmpty(editarOperadorRequest.NomeOperador)) ? operadorBase.NmOperador : editarOperadorRequest.NomeOperador;
                caOperadorModel.DsEmail = (operadorBase.DsEmail == editarOperadorRequest.Email || string.IsNullOrEmpty(editarOperadorRequest.Email)) ? operadorBase.DsEmail : editarOperadorRequest.Email;
                caOperadorModel.StOperador = (operadorBase.StOperador == editarOperadorRequest.StatusOperador || editarOperadorRequest.StatusOperador == 0) ? operadorBase.StOperador : editarOperadorRequest.StatusOperador;
                caOperadorModel.CrSenha = operadorBase.CrSenha;
                caOperadorModel.DhSituacao = DateTime.Now;
                caOperadorModel.DhUltimoLogin = operadorBase.DhUltimoLogin;
                caOperadorModel.DtCadastro = operadorBase.DtCadastro;
                caOperadorModel.DtSenha = operadorBase.DtSenha;
                caOperadorModel.IdTipoSenha = operadorBase.IdTipoSenha;
                caOperadorModel.QtLoginIncorreto = operadorBase.QtLoginIncorreto;
                caOperadorModel.DhAtualizacao = DateTime.Now;

                caOperadorModel.Atualizar();

                retornoOperador.IdOperador = caOperadorModel.IdOperador;
                retornoOperador.StatusOperador = caOperadorModel.StOperador;


                var caLog = new CaLogModel(editarOperadorRequest.IdOperadorAtualizacao == 0 ? 1 : editarOperadorRequest.IdOperadorAtualizacao);
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoLog, Constants.StdMsgInfoInseriu, Constants.StdMsgInfoOperador, editarOperadorRequest.IdOperadorAtualizacao));

                retornoOperador.Mensagem = "Operador editado com sucesso!";

                return retornoOperador;
            }
            catch (CAException cex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        cex.Mensagem);
                try
                {
                    var caLog = new CaLogModel(editarOperadorRequest.IdOperadorAtualizacao == 0 ? 1 : editarOperadorRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOperador.Mensagem = cex.Mensagem;
                return retornoOperador;
            }
            catch (SqlException ex)
            {
                string errorMessage =
                   String.Format(
                       "{0}{1}{2}{3}",
                       ex.Errors[0].Message,
                       Environment.NewLine, Environment.NewLine,
                       ex.Message);

                try
                {
                    var caLog = new CaLogModel(editarOperadorRequest.IdOperadorAtualizacao == 0 ? 1 : editarOperadorRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }

                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOperador.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retornoOperador;


            }
            catch (TokenException tex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        tex.ToMessageAndCompleteStacktrace());
                try
                {
                    var caLog = new CaLogModel(editarOperadorRequest.IdOperadorAtualizacao == 0 ? 1 : editarOperadorRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOperador.Mensagem = tex.Message;
                return retornoOperador;
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
                    var caLog = new CaLogModel(editarOperadorRequest.IdOperadorAtualizacao == 0 ? 1 : editarOperadorRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOperador.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retornoOperador;
            }
        }
        [WebMethod]
        public ListarOperadorPorPerfilResponse ListarOperadorPorPerfil(ListarOperadorPorPerfilRequest listarOperadorPorPerfilRequest)
        {
            var retorno = new ListarOperadorPorPerfilResponse();
            try
            {
                new Token(Config.GetKeyValue("Token"), listarOperadorPorPerfilRequest.Token);
                //implementar
                var caOperadorModel = new CaOperadorModel();
                var operadprPorPerfil = new List<CaOperadorModel>();
                
                if (listarOperadorPorPerfilRequest.IdPerfil > 0)
                 operadprPorPerfil = caOperadorModel.ListarPorPerfil(listarOperadorPorPerfilRequest.IdPerfil);

                else
                {
                    operadprPorPerfil = caOperadorModel.Listar();
                }


                foreach (var operadorPerfil in operadprPorPerfil)
                {
                    if (operadorPerfil.IdOperador == 1) continue;

                    retorno.LstOperadorPerfil.Add(new ListarOperadorResponse
                    {

                        IdOperador = operadorPerfil.IdOperador,
                        NomeOperador = operadorPerfil.NmOperador,
                        CdOperador = operadorPerfil.CdOperador,
                        Email = operadorPerfil.DsEmail,
                        StatusOperador = operadorPerfil.StOperador,
                        DataAtualizacao = operadorPerfil.DhAtualizacao,
                        IdOperadorAtualizacao = operadorPerfil.IdOperadorAtualizacao
                    });
                }
                retorno.Mensagem = "Consulta realizada com sucesso!";
                return retorno;
            }
            catch (SqlException ex)
            {
                string errorMessage =
                   String.Format(
                       "{0}{1}{2}{3}",
                       ex.Errors[0].Message,
                       Environment.NewLine, Environment.NewLine,
                       ex.Message);

                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }

                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retorno.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retorno;
            }
            catch (TokenException tex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        tex.ToMessageAndCompleteStacktrace());
                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retorno.Mensagem = tex.Message;
                return retorno;
            }
            catch (Exception)
            {

                throw;
            }

        }
        [WebMethod]
        public AssociarPerfilResponse AssociarPerfil(AssociarPerfilRequest associarPerfilRequest)
        {
            var retornoOpPerfilModel = new AssociarPerfilResponse();
            try
            {
                new Token(Config.GetKeyValue("Token"), associarPerfilRequest.Token);

                if (associarPerfilRequest.LstPerfil.Count == 0)
                {
                    Context.Response.StatusCode = 500;
                    retornoOpPerfilModel.Mensagem = "Selecione pelo menos um perfil para associação.";
                    return retornoOpPerfilModel;
                }

                foreach (var perfilAssociado in associarPerfilRequest.LstPerfil)
                {
                    var perfilOperador = new CaPerfilOperadorModel() { IdPerfil = perfilAssociado.IdPerfil, IdOperador = associarPerfilRequest.IdOperador }.Selecionar();

                    if (perfilOperador == null)
                    {
                        perfilOperador = new CaPerfilOperadorModel()
                        {
                            IdPerfil = perfilAssociado.IdPerfil,
                            IdOperador = associarPerfilRequest.IdOperador,
                            DhAtualizacao = DateTime.Now,
                            DhSituacao = DateTime.Now,
                            IdOperadorAtualizacao = associarPerfilRequest.IdOperadorAtualizacao,
                            StPerfilOperador = (int)CaSituacaoPerfilOperadorEnum.Ativo
                        };

                        perfilOperador.Inserir();
                    }
                    else
                    {
                        perfilOperador.StPerfilOperador = (int)CaSituacaoPerfilOperadorEnum.Ativo;
                        perfilOperador.DhAtualizacao = DateTime.Now;
                        perfilOperador.DhSituacao = DateTime.Now;
                        perfilOperador.IdOperador = associarPerfilRequest.IdOperadorAtualizacao;

                        perfilOperador.Atualizar();
                    }

                    var perfil = new CaPerfilModel() { IdPerfil = perfilAssociado.IdPerfil }.Selecionar();
                    var operador = new CaOperadorModel() { IdOperador = associarPerfilRequest.IdOperador }.Selecionar();

                    var caLog = new CaLogModel(associarPerfilRequest.IdOperadorAtualizacao == 0 ? 1 : associarPerfilRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoAssociacaoPerfilOperador, perfil.IdPerfil, perfil.DsPerfil, operador.IdOperador, operador.NmOperador));

                    retornoOpPerfilModel.IdOperador = perfilOperador.IdOperador;
                    retornoOpPerfilModel.LstPerfil.Add(new Perfil { IdPerfil = perfilOperador.IdPerfil });
                }

                if (associarPerfilRequest.LstPerfil.Count > 1)
                    retornoOpPerfilModel.Mensagem = "Perfil associado com sucesso!";
                else
                    retornoOpPerfilModel.Mensagem = "Perfis associados com sucesso!";

                return retornoOpPerfilModel;
            }
            catch (SqlException ex)
            {
                string errorMessage =
                   String.Format(
                       "{0}{1}{2}{3}",
                       ex.Errors[0].Message,
                       Environment.NewLine, Environment.NewLine,
                       ex.Message);

                try
                {
                    var caLog = new CaLogModel(associarPerfilRequest.IdOperadorAtualizacao == 0 ? 1 : associarPerfilRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }

                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOpPerfilModel.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retornoOpPerfilModel;
            }
            catch (TokenException tex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        tex.ToMessageAndCompleteStacktrace());
                try
                {
                    var caLog = new CaLogModel(associarPerfilRequest.IdOperadorAtualizacao == 0 ? 1 : associarPerfilRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOpPerfilModel.Mensagem = tex.Message;
                return retornoOpPerfilModel;
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
                    var caLog = new CaLogModel(associarPerfilRequest.IdOperadorAtualizacao == 0 ? 1 : associarPerfilRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOpPerfilModel.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retornoOpPerfilModel;
            }
        }
        [WebMethod]
        public AssociarPerfilResponse Desassociar(AssociarPerfilRequest dessacociarPerfilRequest)
        {
            var retornoOpPerfilModel = new AssociarPerfilResponse();

            try
            {
                new Token(Config.GetKeyValue("Token"), dessacociarPerfilRequest.Token);

                if (dessacociarPerfilRequest.LstPerfil.Count == 0)
                {
                    Context.Response.StatusCode = 500;
                    retornoOpPerfilModel.Mensagem = "Selecione pelo menos um perfil para associação.";
                    return retornoOpPerfilModel;
                }

                foreach (var perfilDessacociado in dessacociarPerfilRequest.LstPerfil)
                {
                    var perfilOperador = new CaPerfilOperadorModel() { IdPerfil = perfilDessacociado.IdPerfil, IdOperador = dessacociarPerfilRequest.IdOperador };
                    perfilOperador.Excluir();

                    var perfil = new CaPerfilModel() { IdPerfil = perfilDessacociado.IdPerfil }.Selecionar();
                    var operador = new CaOperadorModel() { IdOperador = dessacociarPerfilRequest.IdOperador }.Selecionar();

                    var caLog = new CaLogModel(dessacociarPerfilRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoDesassociacaoPerfilOperador, perfil.IdPerfil, perfil.DsPerfil, operador.IdOperador, operador.NmOperador));

                    retornoOpPerfilModel.IdOperador = perfilOperador.IdOperador;
                    retornoOpPerfilModel.LstPerfil.Add(new Perfil { IdPerfil = perfilDessacociado.IdPerfil });
                }

                if (dessacociarPerfilRequest.LstPerfil.Count > 1)
                    retornoOpPerfilModel.Mensagem = "Perfil desassociado com sucesso!";
                else
                    retornoOpPerfilModel.Mensagem = "Perfis desassociados com sucesso!";


                return retornoOpPerfilModel;
            }
            catch (SqlException ex)
            {
                string errorMessage =
                   String.Format(
                       "{0}{1}{2}{3}",
                       ex.Errors[0].Message,
                       Environment.NewLine, Environment.NewLine,
                       ex.Message);

                try
                {
                    var caLog = new CaLogModel(dessacociarPerfilRequest.IdOperadorAtualizacao == 0 ? 1 : dessacociarPerfilRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }

                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOpPerfilModel.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retornoOpPerfilModel;
            }
            catch (TokenException tex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        tex.ToMessageAndCompleteStacktrace());
                try
                {
                    var caLog = new CaLogModel(dessacociarPerfilRequest.IdOperadorAtualizacao == 0 ? 1 : dessacociarPerfilRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOpPerfilModel.Mensagem = tex.Message;
                return retornoOpPerfilModel;
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
                    var caLog = new CaLogModel(dessacociarPerfilRequest.IdOperadorAtualizacao == 0 ? 1 : dessacociarPerfilRequest.IdOperadorAtualizacao);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retornoOpPerfilModel.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retornoOpPerfilModel;
            }
        }
        [WebMethod]
        public ListarOperadoresAplicativoStatusResponse ListarOperadoresAplicativoStatus(ListarOperadoresAplicativoStatusRequest listarOperadoresRequest)
        {
            int idOperadorLog = 1;
            var ListarOperadoresAplicativoStatusResponse = new ListarOperadoresAplicativoStatusResponse();
            try
            {
                new Token(Config.GetKeyValue("Token"), listarOperadoresRequest.Token);

                var caFuncionalidade = new CaFuncionalidadeModel();
                caFuncionalidade.IdAplicativo = listarOperadoresRequest.IdAplicativo;
                var listaCaFuncionalidade = caFuncionalidade.Listar();

                var caFuncionalidadePerfil = new CaFuncionalidadePerfilModel();
                caFuncionalidadePerfil.StFuncionalidadePerfil = (int)CaSituacaoFuncionalidadePerfilEnum.Ativo;
                var listaCaFuncionalidadePerfil = caFuncionalidadePerfil.Listar();

                var caPerfilOperador = new CaPerfilOperadorModel();
                caPerfilOperador.StPerfilOperador = (int)CaSituacaoPerfilEnum.Ativo;
                var listaCaPerfilOperador = caPerfilOperador.Listar();

                var caOperador = new CaOperadorModel();
                caOperador.StOperador = listarOperadoresRequest.IdStatus;
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
                             select new OperadoresAplicativoResponse
                             {
                                 CdOperador = o.CdOperador,
                                 Email = o.DsEmail,
                                 IdOperador = o.IdOperador,
                                 NomeOperador = o.NmOperador,
                                 StatusOperador = o.StOperador
                             }).ToList()
                            .GroupBy(g => new
                            {
                                g.CdOperador,
                                g.Email,
                                g.IdOperador,
                                g.NomeOperador,
                                g.StatusOperador
                            })
                            .Select(s => s.First()).ToList();

                var admin = lista.Where(x => x.IdOperador == 1).FirstOrDefault();

                if (admin != null)
                    lista.Remove(admin);

                ListarOperadoresAplicativoStatusResponse.lstOperadores.AddRange(lista);
                ListarOperadoresAplicativoStatusResponse.Mensagem = "Consulta realizada com sucesso!";

                return ListarOperadoresAplicativoStatusResponse;
            }
            catch (SqlException ex)
            {
                string errorMessage =
                   String.Format(
                       "{0}{1}{2}{3}",
                       ex.Errors[0].Message,
                       Environment.NewLine, Environment.NewLine,
                       ex.Message);

                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }

                catch (Exception) { }

                Context.Response.StatusCode = 500;
                ListarOperadoresAplicativoStatusResponse.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return ListarOperadoresAplicativoStatusResponse;
            }
            catch (TokenException tex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        tex.ToMessageAndCompleteStacktrace());
                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                ListarOperadoresAplicativoStatusResponse.Mensagem = tex.Message;
                return ListarOperadoresAplicativoStatusResponse;
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
                    caLog.LogarErro(listarOperadoresRequest.IdAplicativo, errorMessage);
                }
                catch (Exception) { }



                throw ex;
            }
        }
        [WebMethod]
        public ListarPerfilPorStatusResponse ListarPerfilPorStatus(ListarPerfilPorStatusRequest listarPerfilRequest)
        {
            var retorno = new ListarPerfilPorStatusResponse();
            try
            {
                new Token(Config.GetKeyValue("Token"), listarPerfilRequest.Token);

                var caPerfilModel = new CaPerfilModel();
                
                var perfis = caPerfilModel.ListarPorStatus(listarPerfilRequest.Status);

                foreach (var p in perfis)
                {
                    retorno.LstPerfil.Add(new ListarPerfilResponse { IdPerfil = p.IdPerfil, IdAplicativo = p.IdAplicativo, DescricaoPerfil = p.DsPerfil, StatusPerfil = p.StPerfil, IdOperadorAtualizacao = p.IdOperadorAtualizacaoPerfilOperador, DataAtualizacao = p.DhAtualizacaoPerfil });
                }
              

                retorno.Mensagem = "Consulta realizada com sucesso!";
                return retorno;
            }
            catch (SqlException ex)
            {
                string errorMessage =
                   String.Format(
                       "{0}{1}{2}{3}",
                       ex.Errors[0].Message,
                       Environment.NewLine, Environment.NewLine,
                       ex.Message);

                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }

                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retorno.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retorno;
            }
            catch (TokenException tex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        tex.ToMessageAndCompleteStacktrace());
                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retorno.Mensagem = tex.Message;
                return retorno;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [WebMethod]
        public ObterSistemaAplicativoResponse ObterSistemaAplicativo(ObterSistemaAplicativoRequest obterSistemaAplicativoRequest)
        {
            var retorno = new ObterSistemaAplicativoResponse();
            try
            {
                new Token(Config.GetKeyValue("Token"), obterSistemaAplicativoRequest.Token);

                var caaplicativos = new CaAplicativoModel();
                var casistemas = new CaSistemaModel();

                var sistemas = casistemas.Listar();

                foreach (var sis in sistemas)
                {
                    caaplicativos.IdSistema = sis.IdSistema;

                    var appSis = caaplicativos.SelecionarPorSistema();

                    var sistemaResponse = new ListaSistemaResponse { Id = sis.IdSistema, DescricaoSistema = sis.DsSistema };

                    sistemaResponse.LstAplicativo.AddRange(appSis.Select(x => new ListaAplicativoReponse { Id = x.IdAplicativo, DescricaoAplicativo = x.DsAplicativo }));

                    retorno.LstSistema.Add(sistemaResponse);
                }


                return retorno;
            }
            catch (SqlException ex)
            {
                string errorMessage =
                   String.Format(
                       "{0}{1}{2}{3}",
                       ex.Errors[0].Message,
                       Environment.NewLine, Environment.NewLine,
                       ex.Message);

                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }

                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retorno.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retorno;
            }
            catch (TokenException tex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        tex.ToMessageAndCompleteStacktrace());
                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retorno.Mensagem = tex.Message;
                return retorno;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [WebMethod]
        public ObterIdOperadorResponse ObterIdOperador(ObterOperadorIdRequest obterIdOperadorRequest)
        {
            var retorno = new ObterIdOperadorResponse();

            try
            {
                new Token(Config.GetKeyValue("Token"), obterIdOperadorRequest.Token);

                CaOperadorModel caOperador = new CaOperadorModel();

                caOperador.DsEmail = obterIdOperadorRequest.Email;
                caOperador.CdOperador = obterIdOperadorRequest.CdOperador;

                var listaCaOperador = caOperador.Listar();

                foreach (var operador in listaCaOperador)
                {
                    var op = new OperadorIdResponse { IdOperador = operador.IdOperador };
                    retorno.LstOperadorResult.Add(op);
                }

                return retorno;

            }
            catch (SqlException ex)
            {
                string errorMessage =
                   String.Format(
                       "{0}{1}{2}{3}",
                       ex.Errors[0].Message,
                       Environment.NewLine, Environment.NewLine,
                       ex.Message);

                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }

                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retorno.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retorno;
            }
            catch (TokenException tex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        tex.ToMessageAndCompleteStacktrace());
                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retorno.Mensagem = tex.Message;
                return retorno;
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
        public ListarSistemaAplicativoPerfilOperadorResponse ListarSistemaAplicativoPerfilOperador(ListarSistemaAplicativoPerfilOperadorRequest listarSistemaAplicativoPerfilOperadorRequest)
        {
            var retorno = new ListarSistemaAplicativoPerfilOperadorResponse();

            try
            {
                var rpt = new RptPerfilOperadorModel();
                var queryResult = new List<RptPerfilOperadorModel>();

                rpt.IdAplicativo = listarSistemaAplicativoPerfilOperadorRequest.IdAplicativo;
                rpt.IdOperadorOperador = listarSistemaAplicativoPerfilOperadorRequest.IdOperador;
                rpt.IdPerfil = listarSistemaAplicativoPerfilOperadorRequest.IdPerfil;

                queryResult = rpt.ObterSistemaAplicativoPerfilOperador();

                var sistemas = queryResult.GroupBy(y => y.IdSistema).Select(z => z.First()).Select(x => new SistemaAplicativoResponse { Id = x.IdSistema, DescricaoSistema = x.DsSistema });
                var aplicativos = queryResult.GroupBy(y => y.IdAplicativo).Select(z => z.First()).Select(x => new AplicativoPerfilReponse { Id = x.IdAplicativo, DescricaoAplicativo = x.DsAplicativo, IdSistema = x.IdSistema });
                var perfis = queryResult.GroupBy(y => new { y.IdPerfil, y.IdAplicativo }).Select(z => z.First()).Select(x => new PerfilOperadorResponse { Id = x.IdPerfil, DescricaoPerfil = x.DsPerfil, IdAplicativo = x.IdAplicativo, IdOperador = x.IdOperadorOperador });
                var operadores = queryResult.GroupBy(y => new { y.IdOperadorOperador, y.IdPerfil }).Select(z => z.First()).Select(x => new OperadorResponse { Id = x.IdOperadorOperador, IdPerfil = x.IdPerfil }).Where(z => z.Id != 1);

                retorno.LstSistema.AddRange(sistemas);

                foreach (var sis in retorno.LstSistema)
                {
                    sis.LstAplicativo.AddRange(aplicativos.Where(y => (rpt.IdAplicativo == 0 || y.Id == rpt.IdAplicativo) && y.IdSistema == sis.Id).Select(x => new AplicativoPerfilReponse { Id = x.Id, DescricaoAplicativo = x.DescricaoAplicativo, IdSistema = x.IdSistema }));

                    foreach (var aplicativo in sis.LstAplicativo)
                    {
                        aplicativo.LstPerfil.AddRange(perfis.Where(x => (rpt.IdPerfil == 0 || x.Id == rpt.IdPerfil) && x.IdAplicativo == aplicativo.Id).Select(y => new PerfilOperadorResponse { Id = y.Id, DescricaoPerfil = y.DescricaoPerfil, IdAplicativo = y.IdAplicativo, IdOperador = y.IdOperador }));

                        foreach (var perfil in aplicativo.LstPerfil)
                        {
                            perfil.LstOperador.AddRange(operadores.Where(x => (rpt.IdOperadorOperador == 0 || x.Id == rpt.IdOperadorOperador) && x.IdPerfil == perfil.Id).Select(y => new OperadorResponse { Id = y.Id }));
                        }
                    }
                }


                retorno.Mensagem = "Consulta realizada com sucesso!";
                return retorno;
            }
            catch (SqlException ex)
            {
                string errorMessage =
                   String.Format(
                       "{0}{1}{2}{3}",
                       ex.Errors[0].Message,
                       Environment.NewLine, Environment.NewLine,
                       ex.Message);

                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }

                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retorno.Mensagem = "Ocorreu um erro, favor consultar Log do CA.";
                return retorno;
            }
            catch (TokenException tex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        tex.ToMessageAndCompleteStacktrace());
                try
                {
                    var caLog = new CaLogModel(1);
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                Context.Response.StatusCode = 500;
                retorno.Mensagem = tex.Message;
                return retorno;
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

        #endregion


    }


}
