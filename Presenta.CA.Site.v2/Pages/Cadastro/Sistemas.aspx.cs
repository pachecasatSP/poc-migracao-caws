using Presenta.CA.Model;
using Presenta.CA.Site.Enums.Pages;
using Presenta.CA.Site.Pages.Base;
using Presenta.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presenta.CA.Site.Pages.Cadastro
{
    public partial class Sistemas : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarFuncionalidadesPagina(CadastroSistemaAplicativoFuncionalidadeEnum.Acesso);
                AtribuirAutorizacao();

                bool hasAuth = false;

                for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
                {
                    hasAuth = funcionalidadesPagina[i, 1] == 1;

                    if (hasAuth)
                        break;
                }

                if (!hasAuth)
                    HttpContext.Current.Response.Redirect(ResolveUrl("~/AccessDenied.aspx"), false);
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
    }
}