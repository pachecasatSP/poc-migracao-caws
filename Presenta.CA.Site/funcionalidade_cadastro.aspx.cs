using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso
{
    public partial class funcionalidade_cadastro : System.Web.UI.Page
    {
        private bool _Insercao;
        public string _Erros = string.Empty;
        private int? _idsistema;
        private int? _idaplicativo;
        private int? _idfuncionalidade;
        private clsEntFuncionalidade _clsEntFuncP;

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

                if (Request.QueryString["sistema"] != null)
                    _idsistema = Convert.ToInt32(Request.QueryString["sistema"]);
                
                if (Request.QueryString["aplicativo"] != null)
                    _idaplicativo = Convert.ToInt32(Request.QueryString["aplicativo"]);

                if (Request.QueryString["codigo"] != null)
                    _idfuncionalidade = Convert.ToInt32(Request.QueryString["codigo"]);

                if (Request.QueryString[clsCA._PARAM_NOVO] != null)
                {
                    if (int.Parse(Request.QueryString[clsCA._PARAM_NOVO]) == 1)
                        _Insercao = true;
                }

                if (!this.IsPostBack)
                {
                    CarregarCombo("Sistema", 0);

                    ucSituacaoFuncionalidade.CarregarDados("SituacaoFuncionalidade");

                    if (_Insercao)
                    {
                        lblTitulo.Text += " - Inclusão";
                        ucCAToolbar.DeleteEnabled = false;

                        CarregarCombo("Aplicativo", Convert.ToInt32(cboSistema.SelectedValue.ToString()));
                    }
                    else 
                    {
                        lblTitulo.Text += " - Detalhe";
                        ucCAToolbar.DeleteEnabled = true;

                        CarregarDados();

                        clsAplicativo _clsAplicativo = new clsAplicativo();
                        int id_sistema = _clsAplicativo.Retornar(_clsEntFuncP.idaplicativo)[0].idsistema;

                        cboSistema.SelectedValue = id_sistema.ToString();

                        CarregarCombo("Aplicativo", Convert.ToInt32(cboSistema.SelectedValue.ToString()));

                        cboAplicativo.SelectedValue = _clsEntFuncP.idaplicativo.ToString();

                        ucSituacaoFuncionalidade.Selecionar(_clsEntFuncP.stfuncionalidade);
                    }
                }
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        public void CarregarCombo(string p_objeto, int p_id)
        {
            if (p_objeto == "Sistema")
            {
                clsSistema _clsSistema = new clsSistema();
                cboSistema.Items.Clear();
                cboSistema.DataSource = _clsSistema.Retornar(null, null);
                cboSistema.DataTextField = "dssistema";
                cboSistema.DataValueField = "idsistema";
                cboSistema.DataBind();
            }

            if (p_objeto == "Aplicativo")
            {
                clsAplicativo _clsAplicativo = new clsAplicativo();
                cboAplicativo.Items.Clear();
                cboAplicativo.DataSource = _clsAplicativo.Retornar(null, p_id, null);
                cboAplicativo.DataTextField = "dsaplicativo";
                cboAplicativo.DataValueField = "idaplicativo";
                cboAplicativo.DataBind();
            }
        }

        //Tratar erros na chamada
        private void CarregarDados()
        {
            //Exibir detalhes do registro
            clsFuncionalidade _clsPrincipal = new clsFuncionalidade();
            List<clsEntFuncionalidade> _clsListaEntidade = _clsPrincipal.Retornar(_idfuncionalidade);
            _Erros = _clsPrincipal.Erros;
            if (_Erros == null || _Erros.Length == 0)
            {
                foreach (clsEntFuncionalidade _clsEntidade in _clsListaEntidade)
                {
                    _clsEntFuncP = _clsEntidade;

                    hfCodigo.Value = _clsEntidade.idfuncionalidade.ToString();
                    txtDsFuncionalidade.Text = _clsEntidade.dsfuncionalidade;
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
                clsFuncionalidade _clsPrincipal = new clsFuncionalidade();
                clsEntFuncionalidade _clsEntidade = new clsEntFuncionalidade();

                if(!_Insercao)
                    _clsEntidade.idfuncionalidade = Convert.ToInt32(hfCodigo.Value);

                _clsEntidade.idaplicativo = Convert.ToInt32(cboAplicativo.SelectedValue.ToString());
                _clsEntidade.stfuncionalidade = Convert.ToInt32(ucSituacaoFuncionalidade.cboSituacao.SelectedValue.ToString());
                _clsEntidade.dsfuncionalidade = txtDsFuncionalidade.Text;
                _clsEntidade.idoperador = Convert.ToInt32(hfOperador.Value);

                _clsPrincipal.Insercao = _Insercao;
                _clsPrincipal.Gravar(_clsEntidade);
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                {
                    Response.Redirect("funcionalidade_filtro.aspx?" + clsCA._PARAM_SAVE + "=1");
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
                clsFuncionalidade _clsPrincipal = new clsFuncionalidade();
                _clsPrincipal.Excluir(Convert.ToInt32(hfCodigo.Value));
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                    Response.Redirect("funcionalidade_filtro.aspx?" + clsCA._PARAM_DELETE + "=1");
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Back(object sender, EventArgs e)
        {
            Response.Redirect("funcionalidade_filtro.aspx?" + clsCA._PARAM_VOLTAR + "=1");
        }

        protected void cboSistema_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregarCombo("Aplicativo", Convert.ToInt32(cboSistema.SelectedValue.ToString()));
        }

    }
}