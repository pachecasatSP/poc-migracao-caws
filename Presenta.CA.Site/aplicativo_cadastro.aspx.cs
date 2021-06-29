using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso
{
    public partial class aplicativo_cadastro : System.Web.UI.Page
    {
        private bool _Insercao;
        public string _Erros = string.Empty;
        private int? _idaplicativo;

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
                    _idaplicativo = Convert.ToInt32(Request.QueryString["codigo"]);

                if (Request.QueryString[clsCA._PARAM_NOVO] != null)
                {
                    if (int.Parse(Request.QueryString[clsCA._PARAM_NOVO]) == 1)
                        _Insercao = true;
                }
                
                if (!this.IsPostBack)
                {
                    ucIDSistema.CarregarDados("Sistema");

                    if (_Insercao)
                    {
                        lblTitulo.Text += " - Inclusão";
                        ucCAToolbar.DeleteEnabled = false;
                    }
                    else if (_idaplicativo != null)
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
            clsAplicativo _clsPrincipal = new clsAplicativo();
            List<clsEntAplicativo> _clsListaEntidade = _clsPrincipal.Retornar(_idaplicativo);
            _Erros = _clsPrincipal.Erros;
            if (_Erros == null || _Erros.Length == 0)
            {
                foreach (clsEntAplicativo _clsEntidade in _clsListaEntidade)
                {
                    hfCodigo.Value = _clsEntidade.idaplicativo.ToString();
                    ucIDSistema.Selecionar(_clsEntidade.idsistema);
                    txtDsAplicativo.Text = _clsEntidade.dsaplicativo;                    
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
                clsAplicativo _clsPrincipal = new clsAplicativo();
                clsEntAplicativo _clsEntidade = new clsEntAplicativo();

                if (_Insercao == false)
                    _clsEntidade.idaplicativo = Convert.ToInt32(hfCodigo.Value);

                _clsEntidade.idsistema = Convert.ToInt32(ucIDSistema.cboSituacao.SelectedValue);
                _clsEntidade.dsaplicativo = txtDsAplicativo.Text;
                _clsEntidade.idoperador = Convert.ToInt32(hfOperador.Value);

                _clsPrincipal.Insercao = _Insercao;
                _clsPrincipal.Gravar(_clsEntidade);
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                {
                    Response.Redirect("aplicativo_filtro.aspx?" + clsCA._PARAM_SAVE + "=1");
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
                clsAplicativo _clsPrincipal = new clsAplicativo();
                _clsPrincipal.Excluir(Convert.ToInt32(hfCodigo.Value));
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                    Response.Redirect("aplicativo_filtro.aspx?" + clsCA._PARAM_DELETE + "=1");
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Back(object sender, EventArgs e)
        {
            Response.Redirect("aplicativo_filtro.aspx?" + clsCA._PARAM_VOLTAR + "=1");
        }


    }
}