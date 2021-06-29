using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presenta.CA.Site.Pages.Base;
using System.Web.Security;
using Presenta.CA.Site.Enums;
using Presenta.CA.Model;
using Presenta.Common.Util;
using Presenta.Util.ActiveDirectory;

namespace Presenta.CA.Site
{
    public partial class AcessoRestrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO
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
                    var logSistema = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    logSistema.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        protected void loginCA_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                var service = new CAWS.CAv2ServiceSoapClient();
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();

                e.Authenticated = service.Autenticar(loginCA.UserName, loginCA.Password, idAplicativo);

                if (e.Authenticated)
                {
                    int[] funcionalidades = service.Autorizar(loginCA.UserName, loginCA.Password, idAplicativo).ToArray();

                    Session[Constants.SessionFuncKey] = funcionalidades;

                    bool temAcessoAoSite = false;

                    for (int i = 0; i < funcionalidades.Length; i++)
                    {
                        if (funcionalidades[i] == (int)AcessoGeralEnum.AcessoSite)
                        {
                            temAcessoAoSite = true;
                        }
                    }

                    if (temAcessoAoSite)
                    {
                        RegistrarUsuario(loginCA.UserName);
                        GravarCookieLogin(loginCA.UserName);

                        var dadosUsuarioCA = service.ObterInfo(loginCA.UserName, loginCA.Password, idAplicativo);

                        Session[Constants.SessionIdUsuario] = Convert.ToInt32(dadosUsuarioCA[0]);
                        Session[Constants.SessionNmUsuario] = loginCA.UserName;

                        if (service.DeveTrocarSenha(loginCA.UserName, loginCA.Password, idAplicativo))
                        {
                            Response.Redirect(String.Format(ResolveUrl("~/TrocaSenha.aspx?usr={0}"), loginCA.UserName), true);
                            return;
                        }

                        FormsAuthentication.RedirectFromLoginPage(loginCA.UserName, false);
                    }
                    else
                    {
                        e.Authenticated = false;
                        loginCA.FailureText = @"Você não possui acesso ao site! <br />Contate a equipe de Controle de Acessos do sistema.";
                    }
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
                    var logSistema = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    logSistema.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private void GravarCookieLogin(string userName)
        {
            var httpCookie = new HttpCookie("loginCA");

            httpCookie.Value = userName;
            httpCookie.Expires = DateTime.Today.AddDays(1);

            Response.Cookies.Add(httpCookie);
        }

        private void RegistrarUsuario(string userName)
        {
            var ticket =
                new FormsAuthenticationTicket(
                    1,
                    userName,
                    System.DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    userName
                );

            var httpCookie = new HttpCookie(".ASPXAUTHCA");

            httpCookie.Value = FormsAuthentication.Encrypt(ticket);

            Response.Cookies.Add(httpCookie);
        }

        protected void loginCA_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            // TODO
        }

        protected void loginCA_LoggedIn(object sender, EventArgs e)
        {
            // TODO
        }

        protected void loginCA_LoginError(object sender, EventArgs e)
        {
            // TODO
        }
    }
}