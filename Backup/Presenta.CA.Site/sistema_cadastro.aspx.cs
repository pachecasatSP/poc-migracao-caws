using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso
{
    public partial class sistema_cadastro : System.Web.UI.Page
    {
        private bool _Insercao;
        public string _Erros = string.Empty;
        private int? _idsistema;

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
                    _idsistema = Convert.ToInt32(Request.QueryString["codigo"]);

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
                    else if (_idsistema != null)
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
            clsSistema _clsPrincipal = new clsSistema();
            List<clsEntSistema> _clsListaEntidade = _clsPrincipal.Retornar(_idsistema);
            _Erros = _clsPrincipal.Erros;
            if (_Erros == null || _Erros.Length == 0)
            {
                foreach (clsEntSistema _clsEntidade in _clsListaEntidade)
                {
                    hfCodigo.Value = _clsEntidade.idsistema.ToString();
                    txtDsSistema.Text = _clsEntidade.dssistema;
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
                clsSistema _clsPrincipal = new clsSistema();
                clsEntSistema _clsEntidade = new clsEntSistema();

                if (_Insercao == false)
                    _clsEntidade.idsistema = Convert.ToInt32(hfCodigo.Value);

                _clsEntidade.dssistema = txtDsSistema.Text;
                _clsEntidade.idoperador = Convert.ToInt32(hfOperador.Value);
                
                _clsPrincipal.Insercao = _Insercao;
                _clsPrincipal.Gravar(_clsEntidade);
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                {
                    Response.Redirect("sistema_filtro.aspx?" + clsCA._PARAM_SAVE + "=1");
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
                clsSistema _clsPrincipal = new clsSistema();
                _clsPrincipal.Excluir(Convert.ToInt32(hfCodigo.Value));

                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                    Response.Redirect("sistema_filtro.aspx?" + clsCA._PARAM_DELETE + "=1");
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Back(object sender, EventArgs e)
        {
            Response.Redirect("sistema_filtro.aspx?" + clsCA._PARAM_VOLTAR + "=1");
        }


    }
}