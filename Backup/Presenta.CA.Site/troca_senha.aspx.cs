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
    public partial class troca_senha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hfOperador.Value = Session["idoperador"].ToString();
            }

        }

        private bool ValidaUsuario(string fSenha, 
                                   string fNSenha,
                                   string fCNSenha)
        {
            // matsutami
            // rever com o fernandes

            if (fNSenha != fCNSenha)
            {
                // nova senha nao confere
                return false;
            }


            clsOperador _clsOperador = new clsOperador();
            SqlDataReader _dr = _clsOperador.Retornar(changePasswordCA.UserName);

            if (_dr.Read())
            {
                string zSenha = Cryptographer.Decrypt(_dr["crsenha"].ToString());

                if (zSenha != fSenha)
                {
                    // senha atual incorreta
                    return false;
                }

                // fernades rotina para validar o tipo de senha


                //tudo certo grava no banco
                clsOperador _clsOperadorUp = new clsOperador();
                clsEntOperador _clsEntidade = new clsEntOperador();

                _clsEntidade.idoperador = Convert.ToInt32(_dr["idoperador"]);

                _clsEntidade.idtiposenha = Convert.ToInt32(_dr["idtiposenha"]);
                _clsEntidade.stoperador = Convert.ToInt32(_dr["stoperador"]);
                _clsEntidade.cdoperador = _dr["cdoperador"].ToString();
                _clsEntidade.nmoperador = _dr["nmoperador"].ToString();
                _clsEntidade.dsemail = _dr["dsemail"].ToString();

                _clsEntidade.dtcadastro = Convert.ToDateTime(_dr["dtcadastro"]);
                _clsEntidade.dhsituacao = Convert.ToDateTime(DateTime.Now);
                _clsEntidade.crsenha = Cryptographer.Encrypt(fNSenha);
                _clsEntidade.dtsenha = Convert.ToDateTime(DateTime.Now);
                _clsEntidade.dhultimologin = Convert.ToDateTime(_dr["dhultimologin"]);

                _clsEntidade.qtloginincorreto = Convert.ToInt32(_dr["qtloginincorreto"]);
                _clsEntidade.dhatualizacao = Convert.ToDateTime(_dr["dhatualizacao"]);
                _clsEntidade.idoperadoratualizacao = Convert.ToInt32(hfOperador.Value);

                _clsOperadorUp.Insercao = false;
                _clsOperadorUp.Gravar(_clsEntidade);

                //_Erros = _clsPrincipal.Erros;
                //if (_Erros == null || _Erros.Length == 0)
                //{
                //    Response.Redirect("operador_filtro.aspx?" + clsCA._PARAM_SAVE + "=1");
                //}


                return true;
            }
            else
            {
                // usuario nao encontrado
                return false;
            }
        }

        protected void changePasswordCA_ChangedPassword(object sender, EventArgs e)
        {
            if (ValidaUsuario(changePasswordCA.PasswordRecoveryText, changePasswordCA.NewPassword, changePasswordCA.ConfirmNewPassword))
            {
                FormsAuthentication.RedirectFromLoginPage(changePasswordCA.UserName, false);
            }
            
        }

        protected void changePasswordCA_ChangingPassword(object sender, LoginCancelEventArgs e)
        {
            if (ValidaUsuario(changePasswordCA.PasswordRecoveryText, changePasswordCA.NewPassword, changePasswordCA.ConfirmNewPassword))
            {
                FormsAuthentication.RedirectFromLoginPage(changePasswordCA.UserName, false);
            }
        }
    }
}