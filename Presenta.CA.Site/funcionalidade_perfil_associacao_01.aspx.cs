using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public partial class funcionalidade_perfil_associacao : System.Web.UI.Page
    {
        public string _Erros = string.Empty;
        public string _Avisos = string.Empty;

        private void CarregarCombo()
        {
            clsFuncionalidade _clsFuncionalidade = new clsFuncionalidade();
            ddlFuncionalidade.DataSource = _clsFuncionalidade.Retornar(null, null);
            ddlFuncionalidade.DataTextField = "dsfuncionalidade";
            ddlFuncionalidade.DataValueField = "idfuncionalidade";
            ddlFuncionalidade.DataBind();
        }

        private void CarregarGrid(int p_idfuncionalidade)
        {
            clsFuncionalidadePerfil _clsPerfilIn = new clsFuncionalidadePerfil();
            DataSet _dsIn = _clsPerfilIn.RetornarAssociacao(null, p_idfuncionalidade, "S");

            if (_clsPerfilIn.Erros != null && _clsPerfilIn.Erros.Length > 0)
                _Erros = _clsPerfilIn.Erros;
            else
            {
                if (_dsIn.Tables[0].Rows.Count != 0)
                {
                    gvPerfilIn.DataSource = _dsIn;
                    gvPerfilIn.DataBind();
                }
                else
                {
                    _dsIn.Tables[0].Rows.Add(_dsIn.Tables[0].NewRow());
                    gvPerfilIn.DataSource = _dsIn;
                    gvPerfilIn.DataBind();
                    gvPerfilIn.Rows[0].Visible = false;
                    _Avisos = "Nenhum registro encontrado !";
                }
            }

            clsFuncionalidadePerfil _clsPerfilOut = new clsFuncionalidadePerfil();
            DataSet _dsOut = _clsPerfilOut.RetornarAssociacao(null, p_idfuncionalidade, "N");

            if (_clsPerfilOut.Erros != null && _clsPerfilOut.Erros.Length > 0)
                _Erros = _clsPerfilOut.Erros;
            else
            {
                if (_dsOut.Tables[0].Rows.Count != 0)
                {
                    gvPerfilOut.DataSource = _dsOut;
                    gvPerfilOut.DataBind();
                }
                else
                {
                    _dsOut.Tables[0].Rows.Add(_dsOut.Tables[0].NewRow());
                    gvPerfilOut.DataSource = _dsOut;
                    gvPerfilOut.DataBind();
                    gvPerfilOut.Rows[0].Visible = false;
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
                    CarregarGrid(Convert.ToInt32(ddlFuncionalidade.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void ddlFuncionalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(Convert.ToInt32(ddlFuncionalidade.SelectedValue));
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void gvPerfilOut_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Adiciona
                if (e.CommandName == "Select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvPerfilOut.Rows[index];
                    TableCell tcCell = selectedRow.Cells[0];

                    int p_idperfil = Convert.ToInt32(tcCell.Text);

                    // verifica se o ja existe o campo associado com o status desativvado
                    // senao insere
                    clsFuncionalidadePerfil _clsFuncionalidadePerfilSel = new clsFuncionalidadePerfil();
                    bool _Insercao;
                    if (_clsFuncionalidadePerfilSel.Retornar(Convert.ToInt32(ddlFuncionalidade.SelectedValue.ToString()), p_idperfil, 0).Tables[0].Rows.Count <= 0)
                    {
                        _Insercao = true;
                    }
                    else
                    {
                        _Insercao = false;
                    }

                    clsEntFuncionalidadePerfil _clsEntFuncionalidadePerfil = new clsEntFuncionalidadePerfil();

                    _clsEntFuncionalidadePerfil.idfuncionalidade = Convert.ToInt32(ddlFuncionalidade.SelectedValue.ToString());
                    _clsEntFuncionalidadePerfil.idperfil = p_idperfil;
                    _clsEntFuncionalidadePerfil.stfuncionalidadeperfil = Convert.ToInt32(TipoSituacaoFuncionalidadePerfil.Ativo); //clsCA._stfuncionalidadeperfil_ativo;
                    _clsEntFuncionalidadePerfil.dhsituacao = DateTime.Now;
                    _clsEntFuncionalidadePerfil.idoperador = Convert.ToInt32(hfOperador.Value);

                    clsFuncionalidadePerfil _clsFuncionalidadePerfil = new clsFuncionalidadePerfil();
                    _clsFuncionalidadePerfil.Insercao = _Insercao;
                    _clsFuncionalidadePerfil.Gravar(_clsEntFuncionalidadePerfil);
                    _Erros = _clsFuncionalidadePerfil.Erros;

                    CarregarGrid(Convert.ToInt32(ddlFuncionalidade.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void gvPerfilIn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Remove
                if (e.CommandName == "Select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvPerfilIn.Rows[index];
                    TableCell tcCell = selectedRow.Cells[1];

                    int p_idperfil = Convert.ToInt32(tcCell.Text);

                    bool _Insercao = false;

                    clsEntFuncionalidadePerfil _clsEntFuncionalidadePerfil = new clsEntFuncionalidadePerfil();

                    _clsEntFuncionalidadePerfil.idfuncionalidade = Convert.ToInt32(ddlFuncionalidade.SelectedValue.ToString());
                    _clsEntFuncionalidadePerfil.idperfil = p_idperfil;
                    _clsEntFuncionalidadePerfil.stfuncionalidadeperfil = Convert.ToInt32(TipoSituacaoFuncionalidadePerfil.Inativo); //clsCA._stfuncionalidadeperfil_inativo;
                    _clsEntFuncionalidadePerfil.dhsituacao = DateTime.Now;
                    _clsEntFuncionalidadePerfil.idoperador = Convert.ToInt32(hfOperador.Value);

                    clsFuncionalidadePerfil _clsFuncionalidadePerfil = new clsFuncionalidadePerfil();
                    _clsFuncionalidadePerfil.Insercao = _Insercao;
                    _clsFuncionalidadePerfil.Gravar(_clsEntFuncionalidadePerfil);
                    _Erros = _clsFuncionalidadePerfil.Erros;

                    CarregarGrid(Convert.ToInt32(ddlFuncionalidade.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }



    }
}