using Presenta.CA.Model;
using Presenta.CA.Model.Enum;
using Presenta.CA.Site.Enums.Pages;
using Presenta.CA.Site.Pages.Base;
using Presenta.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presenta.CA.Site.Pages.Associacao
{
    public partial class Operador : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarFuncionalidadesPagina(AssociacoesPerfilOperadorEnum.SomenteLeitura);
                AtribuirAutorizacao();

                bool hasAuth = false;

                for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
                {
                    hasAuth = funcionalidadesPagina[i, 1] == 1;

                    if (hasAuth)
                        break;
                }

                if (hasAuth)
                {
                    bool readOnly = false, fullControl = false;

                    for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
                    {
                        bool ok = funcionalidadesPagina[i, 1] == 1 ? true : false;

                        switch ((AssociacoesPerfilOperadorEnum)funcionalidadesPagina[i, 0])
                        {
                            case AssociacoesPerfilOperadorEnum.ControleTotal:
                                fullControl = ok;
                                break;
                            case AssociacoesPerfilOperadorEnum.SomenteLeitura:
                                readOnly = ok;
                                break;
                            default:
                                break;
                        }
                    }

                    if (readOnly && !fullControl)
                    {
                        hdfReadOnly.Value = "1";
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect(ResolveUrl("~/AccessDenied.aspx"), false);
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

                hdfError.Value = "1";
                hdfText1.Value = currentMethod.Name;
                hdfText2.Value = errorMessage;

                try
                {
                    var caLog = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        [WebMethod(EnableSession = true)]
        public static void Associar(int idPerfil, int idOperador)
        {
            try
            {
                var perfilOperador = new CaPerfilOperadorModel() { IdPerfil = idPerfil, IdOperador = idOperador }.Selecionar();

                if (perfilOperador == null)
                {
                    perfilOperador = new CaPerfilOperadorModel()
                    {
                        IdPerfil = idPerfil,
                        IdOperador = idOperador,
                        DhAtualizacao = DateTime.Now,
                        DhSituacao = DateTime.Now,
                        IdOperadorAtualizacao = HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt(),
                        StPerfilOperador = (int)CaSituacaoPerfilOperadorEnum.Ativo
                    };

                    perfilOperador.Inserir();
                }
                else
                {
                    perfilOperador.StPerfilOperador = (int)CaSituacaoPerfilOperadorEnum.Ativo;
                    perfilOperador.DhAtualizacao = DateTime.Now;
                    perfilOperador.DhSituacao = DateTime.Now;
                    perfilOperador.IdOperador = HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt();

                    perfilOperador.Atualizar();
                }

                var perfil = new CaPerfilModel() { IdPerfil = idPerfil }.Selecionar();
                var operador = new CaOperadorModel() { IdOperador = idOperador }.Selecionar();

                var caLog = new CaLogModel(HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoAssociacaoPerfilOperador, perfil.IdPerfil, perfil.DsPerfil, operador.IdOperador, operador.NmOperador));
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
                    var caLog = new CaLogModel(HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public static void Desassociar(int idPerfil, int idOperador)
        {
            try
            {
                var perfilOperador = new CaPerfilOperadorModel() { IdPerfil = idPerfil, IdOperador = idOperador };
                perfilOperador.Excluir();

                var perfil = new CaPerfilModel() { IdPerfil = idPerfil }.Selecionar();
                var operador = new CaOperadorModel() { IdOperador = idOperador }.Selecionar();

                var caLog = new CaLogModel(HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoDesassociacaoPerfilOperador, perfil.IdPerfil, perfil.DsPerfil, operador.IdOperador, operador.NmOperador));
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
                    var caLog = new CaLogModel(HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }
    }
}