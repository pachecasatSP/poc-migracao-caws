using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso
{
    public partial class funcionalidade_perfil_filtro : System.Web.UI.Page
    {
        public int _id = 0;
        public string _Erros = string.Empty;
        public string _Avisos = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Eventos do user control - toolbar
                ucCAToolbar._Search += new ucToolbar.SearchEvent(ucCAToolbar_Search);
                ucCAToolbar._New += new ucToolbar.NewEvent(ucCAToolbar_New);
                //
                ucCAToolbar.ExibirBotoes(false, false, false, false, false, true, true, false);
                if (!this.IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        _id = int.Parse(Request.QueryString["id"]);
                        System.Collections.Specialized.NameValueCollection _httpreq = new System.Collections.Specialized.NameValueCollection(Request.QueryString);
                        _httpreq.Remove("id");
                    }

                    //Verificar se chamada foi pelo menu (zerar Session com valores utilizados pelo botão voltar)
                    bool _voltar = false;   //validar se veio da página anterior de detalhe
                    if (clsCA.QuerystringIntRetornar(clsCA._PARAM_ZERARSESSIONVOLTAR) == 1)
                    {
                        clsCA.SessionExcluir(clsCA._PARAM_RET_ID);
                        clsCA.SessionExcluir(clsCA._PARAM_RET_DSC);
                    }
                    else
                    {
                        //Verificar se chamada foi pelo botão voltar
                        if (clsCA.QuerystringIntRetornar(clsCA._PARAM_VOLTAR) == 1)
                        {
                            txtFiltroDescricao.Text = clsCA.SessionRetornar(clsCA._PARAM_RET_DSC);
                            _voltar = true;
                        }
                        if (this.txtFiltroDescricao.Text.Length > 0 || _voltar)
                            Filtrar();
                    }
                    //
                }
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Search(object sender, EventArgs e)
        {
            try
            {
                //Armazenar parâmetros da busca na Session
                clsCA.SessionInserir(clsCA._PARAM_RET_DSC, txtFiltroDescricao.Text);
                //
                Filtrar();
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_New(object sender, EventArgs e)
        {
            Server.Transfer("funcionalidade_perfil_cadastro.aspx?" + clsCA._PARAM_NOVO + "=1");
        }

        private void Filtrar()
        {
            clsFuncionalidadePerfil _clsPrincipal = new clsFuncionalidadePerfil();
            gvPerfilOperador.DataSource = _clsPrincipal.RetornarLista(null, null, null);

            if (_clsPrincipal.Erros != null && _clsPrincipal.Erros.Length > 0)
                _Erros = _clsPrincipal.Erros;
            else
            {
                gvPerfilOperador.DataBind();
                if (gvPerfilOperador.Rows.Count == 0)
                    _Avisos = "Nenhum registro encontrado !";
            }
        }


        protected void gvPerfil_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPerfilOperador.PageIndex = e.NewPageIndex;
                Filtrar();
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }
    }
}