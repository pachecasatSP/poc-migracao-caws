using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public partial class tipo_senha_tipo_senha_validacao_associacao_02 : System.Web.UI.Page
    {
        public string _Erros = string.Empty;
        public string _Avisos = string.Empty;

        private void CarregarCombo()
        {
            clsTipoSenhaValidacao _clsTipoSenhaValidacao = new clsTipoSenhaValidacao();
            ddlValidacaoTipoSenha.DataSource = _clsTipoSenhaValidacao.Retornar(null, null);
            ddlValidacaoTipoSenha.DataTextField = "dstiposenhavalidacao";
            ddlValidacaoTipoSenha.DataValueField = "idtiposenhavalidacao";
            ddlValidacaoTipoSenha.DataBind();
        }

        private void CarregarGrid(int p_idtiposenhavalidacao)
        {
            clsTipoSenhaTipoSenhaValidacao _clsLstTipoSenhaTipoSenhaValidacaoIn = new clsTipoSenhaTipoSenhaValidacao();
            DataSet _dsIn = _clsLstTipoSenhaTipoSenhaValidacaoIn.RetornarAssociacao(null, p_idtiposenhavalidacao, "S");

            if (_clsLstTipoSenhaTipoSenhaValidacaoIn.Erros != null && _clsLstTipoSenhaTipoSenhaValidacaoIn.Erros.Length > 0)
                _Erros = _clsLstTipoSenhaTipoSenhaValidacaoIn.Erros;
            else
            {
                if (_dsIn.Tables[0].Rows.Count != 0)
                {
                    gvTipoSenhaIn.DataSource = _dsIn;
                    gvTipoSenhaIn.DataBind();
                }
                else
                {
                    _dsIn.Tables[0].Rows.Add(_dsIn.Tables[0].NewRow());
                    gvTipoSenhaIn.DataSource = _dsIn;
                    gvTipoSenhaIn.DataBind();
                    gvTipoSenhaIn.Rows[0].Visible = false;
                    _Avisos = "Nenhum registro encontrado !";
                }
            }

            clsTipoSenhaTipoSenhaValidacao _clsLstTipoSenhaTipoSenhaValidacaoOut = new clsTipoSenhaTipoSenhaValidacao();
            DataSet _dsOut = _clsLstTipoSenhaTipoSenhaValidacaoOut.RetornarAssociacao(null, p_idtiposenhavalidacao, "N");

            if (_clsLstTipoSenhaTipoSenhaValidacaoOut.Erros != null && _clsLstTipoSenhaTipoSenhaValidacaoOut.Erros.Length > 0)
                _Erros = _clsLstTipoSenhaTipoSenhaValidacaoOut.Erros;
            else
            {
                if (_dsOut.Tables[0].Rows.Count != 0)
                {
                    gvTipoSenhaOut.DataSource = _dsOut;
                    gvTipoSenhaOut.DataBind();
                }
                else
                {
                    _dsOut.Tables[0].Rows.Add(_dsOut.Tables[0].NewRow());
                    gvTipoSenhaOut.DataSource = _dsOut;
                    gvTipoSenhaOut.DataBind();
                    gvTipoSenhaOut.Rows[0].Visible = false;
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
                    CarregarGrid(Convert.ToInt32(ddlValidacaoTipoSenha.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void ddlValidacaoTipoSenha_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(Convert.ToInt32(ddlValidacaoTipoSenha.SelectedValue));
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void gvTipoSenhaOut_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Adiciona
                if (e.CommandName == "Select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvTipoSenhaOut.Rows[index];
                    TableCell tcCell = selectedRow.Cells[0];

                    int p_idtiposenha = Convert.ToInt32(tcCell.Text);

                    // verifica se o ja existe o campo associado com o status desativado
                    // senao insere
                    clsTipoSenhaTipoSenhaValidacao _clsTipoSenhaTipoSenhaValidacaoSel = new clsTipoSenhaTipoSenhaValidacao();
                    bool _Insercao;
                    if (_clsTipoSenhaTipoSenhaValidacaoSel.Retornar(Convert.ToInt32(ddlValidacaoTipoSenha.SelectedValue.ToString()), p_idtiposenha).Tables[0].Rows.Count <= 0)
                    {
                        _Insercao = true;
                    }
                    else
                    {
                        _Insercao = false;
                    }

                    clsEntTipoSenhaTipoSenhaValidacao _clsEntTipoSenhaTipoSenhaValidacao = new clsEntTipoSenhaTipoSenhaValidacao();

                    _clsEntTipoSenhaTipoSenhaValidacao.idtiposenhavalidacao = Convert.ToInt32(ddlValidacaoTipoSenha.SelectedValue.ToString());
                    _clsEntTipoSenhaTipoSenhaValidacao.idtiposenha = p_idtiposenha;
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

                    CarregarGrid(Convert.ToInt32(ddlValidacaoTipoSenha.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void gvTipoSenhaIn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Remove
                if (e.CommandName == "Select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvTipoSenhaIn.Rows[index];
                    TableCell tcCell = selectedRow.Cells[1];

                    int p_idtiposenha = Convert.ToInt32(tcCell.Text);

                    clsTipoSenhaTipoSenhaValidacao _clsTipoSenhaTipoSenhaValidacao = new clsTipoSenhaTipoSenhaValidacao();
                    _clsTipoSenhaTipoSenhaValidacao.Excluir(p_idtiposenha, Convert.ToInt32(ddlValidacaoTipoSenha.SelectedValue.ToString()));
                    _Erros = _clsTipoSenhaTipoSenhaValidacao.Erros;

                    CarregarGrid(Convert.ToInt32(ddlValidacaoTipoSenha.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }



    }
}