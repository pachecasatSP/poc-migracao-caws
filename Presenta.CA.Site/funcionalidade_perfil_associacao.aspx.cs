using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso
{
    public partial class funcionalidade_perfil_cadastro : System.Web.UI.Page
    {
        private bool _Insercao;
        public string _Erros = string.Empty;
        private int? _idfuncionalidade;
        private int? _idperfil;

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
                hfOperador.Value = clsCA._IDOPERADOR.ToString();

                if (Request.QueryString["perfil"] != null)
                    _idperfil = Convert.ToInt32(Request.QueryString["perfil"]);

                if (Request.QueryString["funcionalidade"] != null)
                    _idfuncionalidade = Convert.ToInt32(Request.QueryString["funcionalidade"]);

                if (Request.QueryString[clsCA._PARAM_NOVO] != null)
                {
                    if (int.Parse(Request.QueryString[clsCA._PARAM_NOVO]) == 1)
                        _Insercao = true;
                }
                if (!this.IsPostBack)
                {
                    ucFuncionalidade.CarregarDados("Funcionalidade");
                    ucPerfil.CarregarDados("Perfil");
                    ucSituacaoFuncionalidadePerfil.CarregarDados("SituacaoFuncionalidadePerfil");

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
            clsFuncionalidadePerfil _clsPrincipal = new clsFuncionalidadePerfil();
            List<clsEntFuncionalidadePerfil> _clsListaEntidade = _clsPrincipal.Retornar(Convert.ToInt32(_idfuncionalidade), Convert.ToInt32(_idperfil));
            _Erros = _clsPrincipal.Erros;
            if (_Erros == null || _Erros.Length == 0)
            {
                foreach (clsEntFuncionalidadePerfil _clsEntidade in _clsListaEntidade)
                {
                    ucFuncionalidade.Selecionar(Convert.ToInt32(_clsEntidade.idfuncionalidade));
                    ucPerfil.Selecionar(Convert.ToInt32(_clsEntidade.idperfil));                    
                    ucSituacaoFuncionalidadePerfil.Selecionar(Convert.ToInt32(_clsEntidade.stfuncionalidadeperfil));
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
                clsFuncionalidadePerfil _clsPrincipal = new clsFuncionalidadePerfil();
                clsEntFuncionalidadePerfil _clsEntidade = new clsEntFuncionalidadePerfil();

                _clsEntidade.idfuncionalidade = Convert.ToInt32(ucFuncionalidade.cboSituacao.SelectedValue);
                _clsEntidade.idperfil = Convert.ToInt32(ucPerfil.cboSituacao.SelectedValue);                
                _clsEntidade.stfuncionalidadeperfil = Convert.ToInt32(ucSituacaoFuncionalidadePerfil.cboSituacao.SelectedValue);
                _clsEntidade.dhsituacao = DateTime.Now;
                _clsEntidade.idoperador = Convert.ToInt32(hfOperador.Value);

                _clsPrincipal.Insercao = _Insercao;
                _clsPrincipal.Gravar(_clsEntidade);
                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                {
                    Response.Redirect("funcionalidade_perfil_filtro.aspx?" + clsCA._PARAM_SAVE + "=1");
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
                clsFuncionalidadePerfil _clsPrincipal = new clsFuncionalidadePerfil();
                _clsPrincipal.Excluir(Convert.ToInt32(ucFuncionalidade.cboSituacao.SelectedValue), Convert.ToInt32(ucPerfil.cboSituacao.SelectedValue));

                _Erros = _clsPrincipal.Erros;
                if (_Erros == null || _Erros.Length == 0)
                    Response.Redirect("funcionalidade_perfil_filtro.aspx?" + clsCA._PARAM_DELETE + "=1");
            }
            catch (Exception ex)
            {
                _Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void ucCAToolbar_Back(object sender, EventArgs e)
        {
            Response.Redirect("funcionalidade_perfil_filtro.aspx?" + clsCA._PARAM_VOLTAR + "=1");
        }

    }
}
