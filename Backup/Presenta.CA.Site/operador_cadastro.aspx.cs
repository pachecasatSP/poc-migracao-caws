using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presenta.Security.Encryption;

namespace Controle_Acesso
{
    public partial class operador_cadastro : System.Web.UI.Page
    {
        private bool _Insercao;
        public string _Erros = string.Empty;
        private int? _idoperador;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Eventos do user control - toolbar
                ucCAToolbar._Save += new ucToolbar.SaveEvent(ucCAToolbar_Save);
                ucCAToolbar._Delete += new ucToolbar.DeleteEvent(ucCAToolbar_Delete);
                ucCAToolbar._Back += new ucToolbar.BackEvent(ucCAToolbar_Back);
                //
                ucCAToolbar.ExibirBotoes(true, true, true, true, false, false, false, false);

                //usuario logado na aplicação
                //hfOperador.Value = clsCA._IDOPERADOR.ToString();
                hfOperador.Value = Session["idoperador"].ToString();

                if (Request.QueryString["codigo"] != null)
                    _idoperador = Convert.ToInt32(Request.QueryString["codigo"]);

                if (Request.QueryString[clsCA._PARAM_NOVO] != null)
                {
                    if (int.Parse(Request.QueryString[clsCA._PARAM_NOVO]) == 1)
                        _Insercao = true;
                }

                if (!this.IsPostBack)
                {
                    ucSituacaOperador.CarregarDados("SituacaoOperador");
                    ucTipoSenha.CarregarDados("TipoSenha");

                    if (_Insercao)
                    {
                        lblTitulo.Text += " - Inclusão";
                        ucCAToolbar.DeleteEnabled = false;

                        // default Ativo=1
                        ucSituacaOperador.Selecionar(1);
                        txtDtCadastro.Text = DateTime.Now.ToString();
                        txtDhSituacao.Text = DateTime.Now.ToString();
                        txtDtSenha.Text = DateTime.Now.ToString();
                        txtQtLoginIncorreto.Text = "0";
                        txtDhAtualizacao.Text = DateTime.Now.ToString();
                    }
                    else
                    {
                        lblTitulo.Text += " - Detalhe";
                        ucCAToolbar.DeleteEnabled = true;

                        CarregarDados();
                    }
                }
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        //Tratar erros na chamada
        private void CarregarDados()
        {
            //Exibir detalhes do registro
            clsOperador _clsPrincipal = new clsOperador();
            List<clsEntOperador> _clsListaEntidade = _clsPrincipal.Retornar(_idoperador);
            _Erros = _clsPrincipal.Erros;
            if (_Erros == null || _Erros.Length == 0)
            {
                foreach (clsEntOperador _clsEntidade in _clsListaEntidade)
                {
                    hfCodigo.Value = _clsEntidade.idoperador.ToString();
                    ucTipoSenha.Selecionar(_clsEntidade.idtiposenha);
                    ucSituacaOperador.Selecionar(_clsEntidade.stoperador);
                    txtCodOperador.Text = _clsEntidade.cdoperador;
                    txtNmOperador.Text = _clsEntidade.nmoperador;
                    txtDsEmail.Text = _clsEntidade.dsemail;
                    txtDtCadastro.Text = clsCA.FormatDataHora(_clsEntidade.dtcadastro);
                    txtDhSituacao.Text = clsCA.FormatDataHora(_clsEntidade.dhsituacao);
                    txtCrSenha.Attributes["value"] = Cryptographer.Decrypt(_clsEntidade.crsenha);
                    txtCrConfirmaSenha.Attributes["value"] = Cryptographer.Decrypt(_clsEntidade.crsenha);
                    txtDtSenha.Text = clsCA.FormatDataHora(_clsEntidade.dtsenha);
                    txtDhUltimoLogin.Text = clsCA.FormatDataHora(_clsEntidade.dhultimologin);                    
                    txtQtLoginIncorreto.Text = _clsEntidade.qtloginincorreto.ToString();
                    txtDhAtualizacao.Text = clsCA.FormatDataHora(_clsEntidade.dhatualizacao);

                    clsOperador _Operador = new clsOperador();
                    clsEntOperador _entOperador = _Operador.Retornar(Convert.ToInt32(_clsEntidade.idoperador.ToString()))[0];
                    txtOperador.Text = _entOperador.nmoperador;
                    txtLogin.Text = _entOperador.cdoperador;
                }
            }
            
            if (_clsPrincipal.Erros != null && _clsPrincipal.Erros.Length > 0)
                _Erros = _clsPrincipal.Erros;
        }

        protected void ucCAToolbar_Save(object sender, EventArgs e)
        {
            try
            {
                if ((txtCrSenha.Text.Trim() == "") || (txtCrSenha.Text.Length == 0))
                {
                    _Erros = "Senhas não pode ser branco";
                    return;
                }
                if (txtCrSenha.Text != txtCrConfirmaSenha.Text)
                {
                    _Erros = "Senhas não conferem";
                    return;
                }

                clsOperador _clsPrincipal = new clsOperador();
                clsEntOperador _clsEntidade = new clsEntOperador();
                
                if (_Insercao == false)
                    _clsEntidade.idoperador = Convert.ToInt32(hfCodigo.Value);

                _clsEntidade.idtiposenha = Convert.ToInt32(ucTipoSenha.cboSituacao.SelectedValue.ToString());
                _clsEntidade.stoperador = Convert.ToInt32(ucSituacaOperador.cboSituacao.SelectedValue.ToString());
                _clsEntidade.cdoperador = txtCodOperador.Text;
                _clsEntidade.nmoperador = txtNmOperador.Text;
                
                if (txtDsEmail.Text != "")
                    _clsEntidade.dsemail = txtDsEmail.Text;

                _clsEntidade.dtcadastro = Convert.ToDateTime(txtDtCadastro.Text);
                _clsEntidade.dhsituacao = Convert.ToDateTime(txtDhSituacao.Text);
                _clsEntidade.crsenha = Cryptographer.Encrypt(txtCrSenha.Text);
                _clsEntidade.dtsenha = Convert.ToDateTime(txtDtSenha.Text);

                if (txtDhUltimoLogin.Text != "")
                    _clsEntidade.dhultimologin = Convert.ToDateTime(txtDhUltimoLogin.Text);
                
                _clsEntidade.qtloginincorreto = Convert.ToInt32(txtQtLoginIncorreto.Text);
                _clsEntidade.dhatualizacao = Convert.ToDateTime(txtDhAtualizacao.Text);
                _clsEntidade.idoperadoratualizacao = Convert.ToInt32(hfOperador.Value);

                _clsPrincipal.Insercao = _Insercao;
                _clsPrincipal.Gravar(_clsEntidade);
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                {
                    Response.Redirect("operador_filtro.aspx?" + clsCA._PARAM_SAVE + "=1");
                }
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Delete(object sender, EventArgs e)
        {
            try
            {
                clsOperador _clsPrincipal = new clsOperador();
                _clsPrincipal.Excluir(Convert.ToInt32(hfCodigo.Value));
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                    Response.Redirect("operador_filtro.aspx?" + clsCA._PARAM_DELETE + "=1");
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Back(object sender, EventArgs e)
        {
            Response.Redirect("operador_filtro.aspx?" + clsCA._PARAM_VOLTAR + "=1");
        }


    }
}