using Presenta.CA.Model;
using Presenta.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presenta.CA.Site
{
    public partial class TrocaSenha : System.Web.UI.Page
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
                    var logSistema = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    logSistema.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ResolveUrl("~/Login.aspx"), false);
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

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                string user = Session[Constants.SessionLgUsuario].ToString();
                string currentpw = txtCurrentPassword.Text;
                string newpw = txtNewPassword.Text;
                string confirmpw = txtConfirmNewPassword.Text;

                string mensagemErro = string.Empty;
                if (!ValidarPoliticaSenha(newpw, ref mensagemErro))
                {
                    hdfError.Value = "1";
                    hdfText1.Value = "Erro";
                    hdfText2.Value = mensagemErro;
                }

                var service = new Presenta.CA.Site.CAWS.CAv2ServiceSoapClient();

                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();

                if (service.TrocarSenha(user, currentpw, newpw, idAplicativo))
                {
                    hdfInfo.Value = "1";
                    hdfText.Value = "Senha alterada com sucesso";
                    Response.Redirect(ResolveUrl("~/Login.aspx"), false);
                }
                else
                {
                    hdfError.Value = "1";
                    hdfText1.Value = "Erro";
                    hdfText2.Value = "Ocorreu um erro durante a Troca de Senha. Consulte o Log do Controle de Acessos para obter os detalhes.";
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

        private bool ValidarPoliticaSenha(string novaSenha, ref string mensagemErro)
        {
            var caTipoSenhaTipoSenhaValidacaoModelList = new CaTipoSenhaTipoSenhaValidacaoModel { IdTipoSenha = 1 }.Listar();

            foreach (var item in caTipoSenhaTipoSenhaValidacaoModelList)
            {
                switch (item.NuOrdem)
                {
                    case 1:
                        if (item.QtMinCaracteres > 0)
                        {
                            if (novaSenha.Length < item.QtMinCaracteres)
                            {
                                mensagemErro = string.Format("A senha deve possuir no mínimo {0} caracteres", item.QtMinCaracteres);
                                return false;
                            }
                        }
                        if (item.QtMaxCaracteres > 0)
                        {
                            if (novaSenha.Length > item.QtMaxCaracteres)
                            {
                                mensagemErro = string.Format("A senha deve possuir no máximo {0} caracteres", item.QtMaxCaracteres);
                                return false;
                            }
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(item.CdExpressaoRegular))
                        {
                            Regex regEx = new Regex(item.CdExpressaoRegular);
                            if (!regEx.IsMatch(novaSenha))
                            {
                                mensagemErro = "A senha deve conter pelo menos uma letra maiúscula.";
                            }
                        }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(item.CdExpressaoRegular))
                        {
                            Regex regEx = new Regex(item.CdExpressaoRegular);
                            if (!regEx.IsMatch(novaSenha))
                            {
                                mensagemErro = "A senha deve conter pelo menos uma letra minúscula.";
                            }
                        }
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(item.CdExpressaoRegular))
                        {
                            Regex regEx = new Regex(item.CdExpressaoRegular);
                            if (!regEx.IsMatch(novaSenha))
                            {
                                mensagemErro = "A senha deve conter pelo menos um algarismo.";
                            }
                        }
                        break;
                    case 5:
                        if (!string.IsNullOrEmpty(item.CdExpressaoRegular))
                        {
                            Regex regEx = new Regex(item.CdExpressaoRegular);
                            if (!regEx.IsMatch(novaSenha))
                            {
                                mensagemErro = "A senha deve conter pelo menos um caracter especial.";
                            }
                        }
                        break;
                    case 6:
                        for (int i = 0; i < novaSenha.Length; i++)
                        {
                            if (i == novaSenha.Length - 1) break;

                            if (novaSenha.IndexOf(novaSenha[i], i + 1) > -1)
                            {
                                mensagemErro = "A senha não deve conter caracteres repetidos.";
                                break;
                            }
                        }                        
                        break;
                    default:
                        break;
                }
            }
            return true;
        }
    }
}