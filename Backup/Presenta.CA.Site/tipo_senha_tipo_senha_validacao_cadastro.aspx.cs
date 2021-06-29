using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso
{
    public partial class tipo_senha_tipo_senha_validacao_cadastro : System.Web.UI.Page
    {
        private bool _Insercao;
        public string _Erros = string.Empty;
        private int? _idtiposenha;
        private int? _idtiposenhavalidacao;
        private int? _nuordem;

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

                if (Request.QueryString["tiposenha"] != null)
                    _idtiposenha = Convert.ToInt32(Request.QueryString["tiposenha"]);

                if (Request.QueryString["tiposenhavalidacao"] != null)
                    _idtiposenhavalidacao = Convert.ToInt32(Request.QueryString["tiposenhavalidacao"]);

                if (Request.QueryString["ordem"] != null)
                    _nuordem = Convert.ToInt32(Request.QueryString["ordem"]);

                if (Request.QueryString[clsCA._PARAM_NOVO] != null)
                {
                    if (int.Parse(Request.QueryString[clsCA._PARAM_NOVO]) == 1)
                        _Insercao = true;
                }
                if (!this.IsPostBack)
                {
                    ucTipoSenha.CarregarDados("TipoSenha");
                    ucTipoSenhaValidacao.CarregarDados("TipoSenhaValidacao");

                    if (_Insercao)
                    {
                        lblTitulo.Text += " - Inclusão";
                        ucCAToolbar.DeleteEnabled = false;
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
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        //Tratar erros na chamada
        private void CarregarDados()
        {
            //Exibir detalhes do registro
            clsTipoSenhaTipoSenhaValidacao _clsPrincipal = new clsTipoSenhaTipoSenhaValidacao();
            List<clsEntTipoSenhaTipoSenhaValidacao> _clsListaEntidade = _clsPrincipal.Retornar(_idtiposenha, _idtiposenhavalidacao, _nuordem);
            _Erros = _clsPrincipal.Erros;
            if (_Erros == null || _Erros.Length == 0)
            {
                foreach (clsEntTipoSenhaTipoSenhaValidacao _clsEntidade in _clsListaEntidade)
                {
                    ucTipoSenha.Selecionar(Convert.ToInt32(_clsEntidade.idtiposenha));
                    ucTipoSenhaValidacao.Selecionar(Convert.ToInt32(_clsEntidade.idtiposenhavalidacao));                    
                    txtNuOrdem.Text = _clsEntidade.nuordem.ToString();
                    txtQtMinCaracteres.Text = _clsEntidade.qtmincaracteres.ToString();
                    txtQtMaxCaracteres.Text = _clsEntidade.qtmaxcaracteres.ToString();
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
                clsTipoSenhaTipoSenhaValidacao _clsPrincipal = new clsTipoSenhaTipoSenhaValidacao();
                clsEntTipoSenhaTipoSenhaValidacao _clsEntidade = new clsEntTipoSenhaTipoSenhaValidacao();

                _clsEntidade.idtiposenha = Convert.ToInt32(ucTipoSenha.cboSituacao.SelectedValue.ToString());
                _clsEntidade.idtiposenhavalidacao = Convert.ToInt32(ucTipoSenhaValidacao.cboSituacao.SelectedValue.ToString());

                if (_Insercao)
                { _clsEntidade.nuordem = _clsPrincipal.ContarNuOrdem(_clsEntidade.idtiposenha) + 1; }
                else
                { _clsEntidade.nuordem = Convert.ToInt32(txtNuOrdem.Text); }
                
                _clsEntidade.qtmincaracteres = Convert.ToInt32(txtQtMinCaracteres.Text);
                _clsEntidade.qtmaxcaracteres = Convert.ToInt32(txtQtMaxCaracteres.Text);
                _clsEntidade.idoperador = Convert.ToInt32(hfOperador.Value);

                _clsPrincipal.Insercao = _Insercao;
                _clsPrincipal.Gravar(_clsEntidade);
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                {
                    Response.Redirect("tipo_senha_tipo_senha_validacao_filtro.aspx?" + clsCA._PARAM_SAVE + "=1");
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Delete(object sender, EventArgs e)
        {
            try
            {
                clsTipoSenhaTipoSenhaValidacao _clsPrincipal = new clsTipoSenhaTipoSenhaValidacao();
                _clsPrincipal.Excluir(Convert.ToInt32(ucTipoSenha.cboSituacao.SelectedValue), Convert.ToInt32(ucTipoSenhaValidacao.cboSituacao.SelectedValue));
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                    Response.Redirect("tipo_senha_tipo_senha_validacao_filtro.aspx?" + clsCA._PARAM_DELETE + "=1");
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Back(object sender, EventArgs e)
        {
            Response.Redirect("tipo_senha_tipo_senha_validacao_filtro.aspx?" + clsCA._PARAM_VOLTAR + "=1");
        }


    }
}