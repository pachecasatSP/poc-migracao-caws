using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public partial class tipo_senha_tipo_senha_validacao_associacao : System.Web.UI.Page
    {
        public string _Erros = string.Empty;
        public string _Avisos = string.Empty;

        private void CarregarCombo()
        {
            clsTipoSenha _clsTipoSenha = new clsTipoSenha();
            ddlTipoSenha.DataSource = _clsTipoSenha.Retornar(null, null);
            ddlTipoSenha.DataTextField = "dstiposenha";
            ddlTipoSenha.DataValueField = "idtiposenha";
            ddlTipoSenha.DataBind();
        }

        private void CarregarGrid(int p_idtiposenha)
        {
            clsTipoSenhaTipoSenhaValidacao _clsLstTipoSenhaTipoSenhaValidacaoIn = new clsTipoSenhaTipoSenhaValidacao();
            DataSet _dsIn = _clsLstTipoSenhaTipoSenhaValidacaoIn.RetornarAssociacao(p_idtiposenha, null, "S");

            if (_clsLstTipoSenhaTipoSenhaValidacaoIn.Erros != null && _clsLstTipoSenhaTipoSenhaValidacaoIn.Erros.Length > 0)
                _Erros = _clsLstTipoSenhaTipoSenhaValidacaoIn.Erros;
            else
            {
                if (_dsIn.Tables[0].Rows.Count != 0)
                {
                    gvValidacaoTipoSenhaIn.DataSource = _dsIn;
                    gvValidacaoTipoSenhaIn.DataBind();
                }
                else
                {
                    _dsIn.Tables[0].Rows.Add(_dsIn.Tables[0].NewRow());
                    gvValidacaoTipoSenhaIn.DataSource = _dsIn;
                    gvValidacaoTipoSenhaIn.DataBind();
                    gvValidacaoTipoSenhaIn.Rows[0].Visible = false;
                    _Avisos = "Nenhum registro encontrado !";
                }
            }

            clsTipoSenhaTipoSenhaValidacao _clsLstTipoSenhaTipoSenhaValidacaoOut = new clsTipoSenhaTipoSenhaValidacao();
            DataSet _dsOut = _clsLstTipoSenhaTipoSenhaValidacaoOut.RetornarAssociacao(p_idtiposenha, null, "N");

            if (_clsLstTipoSenhaTipoSenhaValidacaoOut.Erros != null && _clsLstTipoSenhaTipoSenhaValidacaoOut.Erros.Length > 0)
                _Erros = _clsLstTipoSenhaTipoSenhaValidacaoOut.Erros;
            else
            {
                if (_dsOut.Tables[0].Rows.Count != 0)
                {
                    gvValidacaoTipoSenhaOut.DataSource = _dsOut;
                    gvValidacaoTipoSenhaOut.DataBind();
                }
                else
                {
                    _dsOut.Tables[0].Rows.Add(_dsOut.Tables[0].NewRow());
                    gvValidacaoTipoSenhaOut.DataSource = _dsOut;
                    gvValidacaoTipoSenhaOut.DataBind();
                    gvValidacaoTipoSenhaOut.Rows[0].Visible = false;
                    _Avisos = "Nenhum registro encontrado !";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //usuario logado na aplicação
                //hfOperador.Value = clsCA._IDOPERADOR.ToString();
                hfOperador.Value = Session["idoperador"].ToString();

                if (!this.IsPostBack)
                {
                    CarregarCombo();
                    CarregarGrid(Convert.ToInt32(ddlTipoSenha.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void ddlTipoSenha_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(Convert.ToInt32(ddlTipoSenha.SelectedValue));
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void gvValidacaoTipoSenhaOut_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Adiciona
                if (e.CommandName == "Select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvValidacaoTipoSenhaOut.Rows[index];
                    TableCell tcCell = selectedRow.Cells[0];

                    int p_idtiposenhavalidacao = Convert.ToInt32(tcCell.Text);

                    // verifica se o ja existe o campo associado com o status desativado
                    // senao insere
                    clsTipoSenhaTipoSenhaValidacao _clsTipoSenhaTipoSenhaValidacaoSel = new clsTipoSenhaTipoSenhaValidacao();
                    bool _Insercao;
                    if (_clsTipoSenhaTipoSenhaValidacaoSel.Retornar(Convert.ToInt32(ddlTipoSenha.SelectedValue.ToString()), p_idtiposenhavalidacao).Tables[0].Rows.Count <= 0)
                    {
                        _Insercao = true;
                    }
                    else
                    {
                        _Insercao = false;
                    }

                    clsEntTipoSenhaTipoSenhaValidacao _clsEntTipoSenhaTipoSenhaValidacao = new clsEntTipoSenhaTipoSenhaValidacao();

                    _clsEntTipoSenhaTipoSenhaValidacao.idtiposenha = Convert.ToInt32(ddlTipoSenha.SelectedValue.ToString());
                    _clsEntTipoSenhaTipoSenhaValidacao.idtiposenhavalidacao = p_idtiposenhavalidacao;
                    // matsu
                    // rever os valores dos campos
                    _clsEntTipoSenhaTipoSenhaValidacao.nuordem = 0;
                    _clsEntTipoSenhaTipoSenhaValidacao.qtmincaracteres = 0;
                    _clsEntTipoSenhaTipoSenhaValidacao.qtmaxcaracteres = 0;

                    _clsEntTipoSenhaTipoSenhaValidacao.dhatualizacao = DateTime.Now;
                    _clsEntTipoSenhaTipoSenhaValidacao.idoperador = Convert.ToInt32(hfOperador.Value);

                    clsTipoSenhaTipoSenhaValidacao _clsTipoSenhaTipoSenhaValidacao = new clsTipoSenhaTipoSenhaValidacao();
                    _clsTipoSenhaTipoSenhaValidacao.Insercao = _Insercao;
                    _clsTipoSenhaTipoSenhaValidacao.Gravar(_clsEntTipoSenhaTipoSenhaValidacao);
                    _Erros = _clsTipoSenhaTipoSenhaValidacao.Erros;

                    CarregarGrid(Convert.ToInt32(ddlTipoSenha.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void gvValidacaoTipoSenhaIn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Remove
                if (e.CommandName == "Select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvValidacaoTipoSenhaIn.Rows[index];
                    TableCell tcCell = selectedRow.Cells[1];

                    int p_idtiposenhavalidacao = Convert.ToInt32(tcCell.Text);

                    clsTipoSenhaTipoSenhaValidacao _clsTipoSenhaTipoSenhaValidacao = new clsTipoSenhaTipoSenhaValidacao();
                    _clsTipoSenhaTipoSenhaValidacao.Excluir(Convert.ToInt32(ddlTipoSenha.SelectedValue.ToString()), p_idtiposenhavalidacao);
                    _Erros = _clsTipoSenhaTipoSenhaValidacao.Erros;

                    CarregarGrid(Convert.ToInt32(ddlTipoSenha.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }



    }
}