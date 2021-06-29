using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;

namespace Controle_Acesso
{
    public partial class controle_acesso : System.Web.UI.MasterPage
    {

        protected override void OnInit(EventArgs e)
        {
            if ((Session["idoperador"] == null) || 
                (!HttpContext.Current.User.Identity.IsAuthenticated))
            {
                //Response.Redirect("~/login.aspx");
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }




        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    lblUsuario.Text = HttpContext.Current.User.Identity.Name;
                }

                //Exibir ambiente
                if (ConfigurationManager.AppSettings["ambiente"] != null)
                {
                    switch (Convert.ToInt32(ConfigurationManager.AppSettings["ambiente"].ToString()))
                    {
                        case (int)clsCA._AmbienteTipo._AT_Desenv:
                            {
                                lblAmbienteDsc.Text = "Desenvolvimento";
                                break;
                            }
                        case (int)clsCA._AmbienteTipo._AT_Homolog:
                            {
                                lblAmbienteDsc.Text = "Homologação";
                                break;
                            }
                        case (int)clsCA._AmbienteTipo._AT_Prod:
                            {
                                lblAmbienteDsc.Text = "Produção";
                                break;
                            }
                    }
                }


            }
        }
    }
}