using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Presenta.Common.Util;
using Presenta.CA.Model;
using System.Threading;

namespace Presenta.CA.Site
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                FormsAuthentication.SignOut();

                Session.Clear();
                Session.Abandon();

                var service = new CAWS.CAv2ServiceSoapClient();
                int tipoLogon = service.ObterTipoAutenticacao();

                int tipoAuth = Config.GetKeyValue(Constants.KeyTipoAutenticacao).ToInt();

                if (tipoAuth == 1 && tipoLogon == 1 && Request.ServerVariables["LOGON_USER"] != "") // Windows
                {
                    Response.Redirect("~/LogoutWA.aspx", false);
                }
                else
                {
                    Response.Redirect("~/Pages/Associacao/Operador.aspx", false);
                }
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