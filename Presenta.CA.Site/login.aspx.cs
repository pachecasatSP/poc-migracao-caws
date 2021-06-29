using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using Presenta.Security.Encryption;


namespace Controle_Acesso
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                HttpCookie cookie = Request.Cookies["loginCA"];
                if (cookie != null)
                {
                    loginCA.UserName = cookie.Value;
                }
                // matsutami - remover
                else
                {
                    loginCA.UserName = "admin";
                }
            }
        }

        protected void loginCA_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (ValidaUsuario(loginCA.UserName, loginCA.Password))
            {
                RegistraUsuario();
                GravaCookieLogin();

                e.Authenticated = true;
                FormsAuthentication.RedirectFromLoginPage(loginCA.UserName, false);
            }
            else 
            {
                e.Authenticated = false;
            }
        }

        private bool ValidaUsuario(string fUsuario, string fSenha)
        {
            // matsutami
            // rever com o fernandes
            if ((loginCA.UserName == "admin") && (loginCA.Password == "123"))
            {
                Session["idoperador"] = 1; 
                return true;
            }

            clsOperador _clsOperador = new clsOperador();
            SqlDataReader _dr = _clsOperador.Retornar(fUsuario);

            if (_dr.Read())
            {
                string zSenha = Cryptographer.Decrypt(_dr["crsenha"].ToString());
                
                if (zSenha == fSenha)
                {
                    Session["idoperador"] = _dr["idoperador"].ToString(); 
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void RegistraUsuario()
        {
            FormsAuthenticationTicket objTicket = new FormsAuthenticationTicket(1, 
                                                                                loginCA.UserName, 
                                                                                System.DateTime.Now, 
                                                                                DateTime.Now.AddMinutes(30), 
                                                                                false, 
                                                                                loginCA.UserName);
            HttpCookie objCookie = new HttpCookie(".ASPXAUTHCA");
            objCookie.Value = FormsAuthentication.Encrypt(objTicket);
            Response.Cookies.Add(objCookie);
        }

        private void GravaCookieLogin()
        {
            HttpCookie cookie = new HttpCookie("loginCA");
            cookie.Value = loginCA.UserName;
            cookie.Expires = DateTime.Today.AddMonths(1);

            Response.Cookies.Add(cookie);
        }

        



    }
}