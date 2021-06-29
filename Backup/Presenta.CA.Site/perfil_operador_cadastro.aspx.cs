using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso
{
    public partial class perfil_operador_cadastro : System.Web.UI.Page
    {
        private bool _Insercao;
        public string _Erros = string.Empty;
        private int? _idperfil;
        private int? _idoperador;
        private string _page = string.Empty;

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

                if (Request.QueryString["perfil"] != null)
                    _idperfil = Convert.ToInt32(Request.QueryString["perfil"]);

                if (Request.QueryString["operador"] != null)
                    _idoperador = Convert.ToInt32(Request.QueryString["operador"]);

                if (Request.QueryString["page"] != null)
                    _page = Request.QueryString["page"];

                if (Request.QueryString[clsCA._PARAM_NOVO] != null)
                {
                    if (int.Parse(Request.QueryString[clsCA._PARAM_NOVO]) == 1)
                        _Insercao = true;
                }

                if (!this.IsPostBack)
                {
                    ucPerfil.CarregarDados("Perfil");
                    ucOperador.CarregarDados("Operador");
                    ucSituacaoPerfilOperador.CarregarDados("SituacaoPerfilOperador");

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
                _Erros = ex.Source + " - " + ex.Message;
            }
        }       

        //Tratar erros na chamada
        private void CarregarDados()
        {
            //Exibir detalhes do registro
            clsPerfilOperador _clsPrincipal = new clsPerfilOperador();
            List<clsEntPerfilOperador> _clsListaEntidade = _clsPrincipal.Retornar(Convert.ToInt32(_idperfil), Convert.ToInt32(_idoperador));
            _Erros = _clsPrincipal.Erros;
            if (_Erros == null || _Erros.Length == 0)
            {
                foreach (clsEntPerfilOperador _clsEntidade in _clsListaEntidade)
                {
                    ucPerfil.Selecionar(Convert.ToInt32(_clsEntidade.idperfil));
                    ucOperador.Selecionar(Convert.ToInt32(_clsEntidade.idoperador));
                    ucSituacaoPerfilOperador.Selecionar(Convert.ToInt32(_clsEntidade.stperfiloperador));
                    txtDhSituacao.Text = clsCA.FormatDataHora(_clsEntidade.dhsituacao);
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
                clsPerfilOperador _clsPrincipal = new clsPerfilOperador();
                clsEntPerfilOperador _clsEntidade = new clsEntPerfilOperador();

                _clsEntidade.idperfil = Convert.ToInt32(ucPerfil.cboSituacao.SelectedValue);
                _clsEntidade.idoperador = Convert.ToInt32(ucOperador.cboSituacao.SelectedValue);
                _clsEntidade.stperfiloperador = Convert.ToInt32(ucSituacaoPerfilOperador.cboSituacao.SelectedValue);
                _clsEntidade.dhsituacao = DateTime.Now;
                _clsEntidade.idoperadoratualizacao = Convert.ToInt32(hfOperador.Value);

                _clsPrincipal.Insercao = _Insercao;
                _clsPrincipal.Gravar(_clsEntidade);
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                {
                    if (_page != null && _page.Length > 0)
                    {
                        if (_page == "01")
                            Response.Redirect("perfil_operador_associacao_01.aspx?" + clsCA._PARAM_SAVE + "=1");
                        else
                            Response.Redirect("perfil_operador_associacao_02.aspx?" + clsCA._PARAM_SAVE + "=1");
                    }
                    else
                    {
                        Response.Redirect("perfil_operador_associacao_01.aspx?" + clsCA._PARAM_SAVE + "=1");
                    }
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
                clsPerfilOperador _clsPrincipal = new clsPerfilOperador();
                _clsPrincipal.Excluir(Convert.ToInt32(ucPerfil.cboSituacao.SelectedValue), Convert.ToInt32(ucOperador.cboSituacao.SelectedValue));
                
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                {
                    if (_page != null && _page.Length > 0)
                    {
                        if (_page == "01")
                            Response.Redirect("perfil_operador_associacao_01.aspx?" + clsCA._PARAM_SAVE + "=1");
                        else
                            Response.Redirect("perfil_operador_associacao_02.aspx?" + clsCA._PARAM_SAVE + "=1");
                    }
                    else
                    {
                        Response.Redirect("perfil_operador_associacao_01.aspx?" + clsCA._PARAM_SAVE + "=1");
                    }
                }
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Back(object sender, EventArgs e)
        {
            if (_page != null && _page.Length > 0)
            {
                if (_page == "01")
                    Response.Redirect("perfil_operador_associacao_01.aspx?" + clsCA._PARAM_SAVE + "=1");
                else
                    Response.Redirect("perfil_operador_associacao_02.aspx?" + clsCA._PARAM_SAVE + "=1");
            }
            else
            {
                Response.Redirect("perfil_operador_associacao_01.aspx?" + clsCA._PARAM_SAVE + "=1");
            }
        }

    }
}
