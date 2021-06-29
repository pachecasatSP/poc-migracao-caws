using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso
{
    public partial class tipo_senha_cadastro : System.Web.UI.Page
    {
        private bool _Insercao;
        public string _Erros = string.Empty;
        private int? idtiposenha;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Eventos do user control - toolbar
                ucCAToolbar._Save += new ucToolbar.SaveEvent(ucCAToolbar_Save);
                ucCAToolbar._Delete += new ucToolbar.DeleteEvent(ucCAToolbar_Delete);
                ucCAToolbar._Back += new ucToolbar.BackEvent(ucCAToolbar_Back);

                ucCAToolbar.ExibirBotoes(true, true, true, true, false, false, false, false);

                //usuario logado na aplicação
                //hfOperador.Value = clsCA._IDOPERADOR.ToString();
                hfOperador.Value = Session["idoperador"].ToString();

                if (Request.QueryString["codigo"] != null)
                    idtiposenha = int.Parse(Request.QueryString["codigo"]);

                if (Request.QueryString[clsCA._PARAM_NOVO] != null)
                {
                    if (int.Parse(Request.QueryString[clsCA._PARAM_NOVO]) == 1)
                        _Insercao = true;
                }

                if (!this.IsPostBack)
                {
                    if (_Insercao)
                    {
                        lblTitulo.Text += " - Inclusão";
                        ucCAToolbar.DeleteEnabled = false;
                    }
                    else if (idtiposenha != null)
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
            clsTipoSenha _clsPrincipal = new clsTipoSenha();
            List<clsEntTipoSenha> _clsListaEntidade = _clsPrincipal.Retornar(idtiposenha);
            _Erros = _clsPrincipal.Erros;
            if (_Erros == null || _Erros.Length == 0)
            {
                foreach (clsEntTipoSenha _clsEntidade in _clsListaEntidade)
                {
                    hfCodigo.Value = _clsEntidade.idtiposenha.ToString();
                    txtDsSituacao.Text = _clsEntidade.dstiposenha.ToString();
                    txtDhAtualizacao.Text = clsCA.FormatDataHora(_clsEntidade.dhatualizacao);
                    txtQtMaxTentativas.Text = _clsEntidade.qtmaxtentativas.ToString();
                    txtQtVerificacaoHistorico.Text = _clsEntidade.qtverificacaohistorico.ToString();
                    txtCdExpressaoRegular.Text = _clsEntidade.cdexpressaoregular;
                    txtDtInicioVigencia.Text = clsCA.FormatDataHora(_clsEntidade.dtiniciovigencia);
                    txtDtFimVigencia.Text = clsCA.FormatDataHora(_clsEntidade.dtfimvigencia);

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
                clsTipoSenha _clsPrincipal = new clsTipoSenha();
                clsEntTipoSenha _clsEntidade = new clsEntTipoSenha();

                if (_Insercao == false)
                    _clsEntidade.idtiposenha = Convert.ToInt32(hfCodigo.Value);

                _clsEntidade.dstiposenha = txtDsSituacao.Text;
                _clsEntidade.qtmaxtentativas = Convert.ToInt32(txtQtMaxTentativas.Text);
                _clsEntidade.qtverificacaohistorico = Convert.ToInt32(txtQtVerificacaoHistorico.Text);
                _clsEntidade.cdexpressaoregular = txtCdExpressaoRegular.Text;
                _clsEntidade.dtiniciovigencia = Convert.ToDateTime(txtDtInicioVigencia.Text);
                _clsEntidade.dtfimvigencia = Convert.ToDateTime(txtDtFimVigencia.Text);

                _clsEntidade.idoperador = Convert.ToInt32(hfOperador.Value);

                _clsPrincipal.Insercao = _Insercao;
                _clsPrincipal.Gravar(_clsEntidade);
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                {
                    Response.Redirect("tipo_senha_filtro.aspx?" + clsCA._PARAM_SAVE + "=1");
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
                clsTipoSenha _clsPrincipal = new clsTipoSenha();
                _clsPrincipal.Excluir(Convert.ToInt32(hfCodigo.Value));
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                    Response.Redirect("tipo_senha_filtro.aspx?" + clsCA._PARAM_DELETE + "=1");
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Back(object sender, EventArgs e)
        {
            Response.Redirect("tipo_senha_filtro.aspx?" + clsCA._PARAM_VOLTAR + "=1");
        }


    }
}