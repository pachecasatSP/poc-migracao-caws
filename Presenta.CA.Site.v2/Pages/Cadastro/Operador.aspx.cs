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

namespace Presenta.CA.Site.Pages.Cadastro
{
    public partial class Operador : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarFuncionalidadesPagina(CadastroOperadorEnum.SomenteLeitura);
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

                        switch ((CadastroOperadorEnum)funcionalidadesPagina[i, 0])
                        {
                            case CadastroOperadorEnum.ControleTotal:
                                fullControl = ok;
                                break;
                            case CadastroOperadorEnum.SomenteLeitura:
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

                try
                {
                    var caLog = new CaLogModel(HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        [WebMethod(EnableSession = true)]
        public static void ResetarSenha(int idOperador)
        {
            try
            {
                int idOperadorAtualizacao = HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt();

                CaOperadorModel caOperadorModel = new CaOperadorModel();

                caOperadorModel.IdOperador = idOperador;
                caOperadorModel.StOperador = (int)CaSituacaoOperadorEnum.Ativo;
                caOperadorModel.DtSenha = DateTime.Now;
                caOperadorModel.QtLoginIncorreto = 0;
                caOperadorModel.DhAtualizacao = DateTime.Now;
                caOperadorModel.IdOperadorAtualizacao = idOperadorAtualizacao;

                caOperadorModel.ResetarSenha();

                var caLog = new CaLogModel(HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : HttpContext.Current.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, Constants.OperadorAlterar);
                caLog.LogarInfo(idAplicativo, String.Format("Operador {0} foi resetado", caOperadorModel.CdOperador));
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