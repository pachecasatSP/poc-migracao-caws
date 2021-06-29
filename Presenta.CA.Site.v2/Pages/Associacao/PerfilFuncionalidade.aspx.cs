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
    public partial class PerfilFuncionalidade : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarFuncionalidadesPagina(AssociacoesPerfilFuncionalidadeEnum.SomenteLeitura);
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

                        switch ((AssociacoesPerfilFuncionalidadeEnum)funcionalidadesPagina[i, 0])
                        {
                            case AssociacoesPerfilFuncionalidadeEnum.ControleTotal:
                                fullControl = ok;
                                break;
                            case AssociacoesPerfilFuncionalidadeEnum.SomenteLeitura:
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

        // TODO: alterar
        [WebMethod(EnableSession = true)]
        public static void Associar(int idFuncionalidade, int idPerfil)
        {
            try
            {
                var funcionalidadePerfil = new CaFuncionalidadePerfilModel() { IdFuncionalidade = idFuncionalidade, IdPerfil = idPerfil }.Selecionar();

                if (funcionalidadePerfil == null)
                {
                    funcionalidadePerfil = new CaFuncionalidadePerfilModel()
                    {
                        IdFuncionalidade = idFuncionalidade,
                        IdPerfil = idPerfil,
                        DhAtualizacao = DateTime.Now,
                        DhSituacao = DateTime.Now,
                        IdOperadorAtualizacao = HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt(),
                        StFuncionalidadePerfil = (int)CaSituacaoFuncionalidadePerfilEnum.Ativo
                    };

                    funcionalidadePerfil.Inserir();
                }
                else
                {
                    funcionalidadePerfil.StFuncionalidadePerfil = (int)CaSituacaoFuncionalidadePerfilEnum.Ativo;
                    funcionalidadePerfil.DhAtualizacao = DateTime.Now;
                    funcionalidadePerfil.DhSituacao = DateTime.Now;
                    funcionalidadePerfil.IdOperadorAtualizacao = HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt();

                    funcionalidadePerfil.Atualizar();
                }

                var funcionalidade = new CaFuncionalidadeModel() { IdFuncionalidade = idFuncionalidade }.Selecionar();
                var perfil = new CaPerfilModel() { IdPerfil = idPerfil }.Selecionar();

                var caLog = new CaLogModel(HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoAssociacaoFuncionalidadePerfil, funcionalidade.IdFuncionalidade, funcionalidade.DsFuncionalidade, perfil.IdPerfil, perfil.DsPerfil));
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
        public static void AssociarLote(int[] idsFuncionalidade, int idPerfil)
        {
            try
            {
                foreach (int idFuncionalidade in idsFuncionalidade)
                {
                    var funcionalidadePerfil = new CaFuncionalidadePerfilModel() { IdFuncionalidade = idFuncionalidade, IdPerfil = idPerfil }.Selecionar();

                    if (funcionalidadePerfil == null)
                    {
                        funcionalidadePerfil = new CaFuncionalidadePerfilModel()
                        {
                            IdFuncionalidade = idFuncionalidade,
                            IdPerfil = idPerfil,
                            DhAtualizacao = DateTime.Now,
                            DhSituacao = DateTime.Now,
                            IdOperadorAtualizacao = HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt(),
                            StFuncionalidadePerfil = (int)CaSituacaoFuncionalidadePerfilEnum.Ativo
                        };

                        funcionalidadePerfil.Inserir();
                    }
                    else
                    {
                        funcionalidadePerfil.StFuncionalidadePerfil = (int)CaSituacaoFuncionalidadePerfilEnum.Ativo;
                        funcionalidadePerfil.DhAtualizacao = DateTime.Now;
                        funcionalidadePerfil.DhSituacao = DateTime.Now;
                        funcionalidadePerfil.IdOperadorAtualizacao = HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt();

                        funcionalidadePerfil.Atualizar();
                    }

                    var funcionalidade = new CaFuncionalidadeModel() { IdFuncionalidade = idFuncionalidade }.Selecionar();
                    var perfil = new CaPerfilModel() { IdPerfil = idPerfil }.Selecionar();

                    var caLog = new CaLogModel(HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoAssociacaoFuncionalidadePerfil, funcionalidade.IdFuncionalidade, funcionalidade.DsFuncionalidade, perfil.IdPerfil, perfil.DsPerfil));
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
                    var caLog = new CaLogModel(HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public static void Desassociar(int idFuncionalidade, int idPerfil)
        {
            try
            {
                var funcionalidadePerfil = new CaFuncionalidadePerfilModel() { IdFuncionalidade = idFuncionalidade, IdPerfil = idPerfil };
                funcionalidadePerfil.Excluir();

                var funcionalidade = new CaFuncionalidadeModel() { IdFuncionalidade = idFuncionalidade }.Selecionar();
                var perfil = new CaPerfilModel() { IdPerfil = idPerfil }.Selecionar();

                var caLog = new CaLogModel(HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoDesassociacaoFuncionalidadePerfil, funcionalidade.IdFuncionalidade, funcionalidade.DsFuncionalidade, perfil.IdPerfil, perfil.DsPerfil));
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