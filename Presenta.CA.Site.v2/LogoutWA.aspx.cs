using Presenta.CA.Model;
using Presenta.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presenta.CA.Site
{
    public partial class LogoutWA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdfMajorMinorVersion.Value =
                    String.Format(
                        "Controle de Acesso {0}.{1} - Presenta Sistemas",
                        System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(),
                        System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString()
                        );
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

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                string login = System.Web.HttpContext.Current.User.Identity.Name.ToUpper();
                login = login.IndexOf(@"\") != -1 ? login.Substring(login.IndexOf(@"\") + 1) : login;

                if (!String.IsNullOrEmpty(Config.GetKeyValue(Constants.KeySiteMinder)) && Session[Constants.SessionNmUsuario] == null)
                {
                    if (Request.Headers.Get(Config.GetKeyValue(Constants.KeySiteMinder)) != null && !String.IsNullOrEmpty(Request.Headers.Get(Config.GetKeyValue(Constants.KeySiteMinder))))
                    {
                        login = Request.Headers.Get(Config.GetKeyValue(Constants.KeySiteMinder));
                    }
                }

                var service = new CAWS.CAv2ServiceSoapClient();
                int tipoLogon = service.ObterTipoAutenticacao();
                int tipoAuth = Config.GetKeyValue(Constants.KeyTipoAutenticacao).ToInt();

                int[] funcionalidades;

                if (tipoAuth == 1 && tipoLogon == 1 && Session[Constants.SessionFuncKey] == null) // Windows
                {
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();

                    var funcs = service.Autorizar(login.ToLower(), null, idAplicativo);

                    if (funcs != null)
                    {
                        funcionalidades = funcs.ToArray();
                        Session[Constants.SessionFuncKey] = funcionalidades;
                    }
                }

                Response.Redirect("~/Pages/Associacao/Operador.aspx", false);
            }
            catch (ThreadAbortException) { }
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
    }
}