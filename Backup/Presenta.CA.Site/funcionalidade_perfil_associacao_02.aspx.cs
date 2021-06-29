using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public partial class funcionalidade_perfil_associacao_02 : System.Web.UI.Page
    {
        public string _Erros = string.Empty;
        public string _Avisos = string.Empty;

        private void CarregarCombo()
        {
            clsPerfil _clsPerfil = new clsPerfil();
            ddlPerfil.DataSource = _clsPerfil.Retornar(null, null);
            ddlPerfil.DataTextField = "dsperfil";
            ddlPerfil.DataValueField = "idperfil";
            ddlPerfil.DataBind();
        }

        private void CarregarGrid(int p_idperfil)
        {
            clsFuncionalidadePerfil _clsPerfilIn = new clsFuncionalidadePerfil();
            DataSet _dsIn = _clsPerfilIn.RetornarAssociacao(p_idperfil, null, "S");

            if (_clsPerfilIn.Erros != null && _clsPerfilIn.Erros.Length > 0)
                _Erros = _clsPerfilIn.Erros;
            else
            {
                if (_dsIn.Tables[0].Rows.Count != 0)
                {
                    gvFuncionalidadeIn.DataSource = _dsIn;
                    gvFuncionalidadeIn.DataBind();
                }
                else
                {
                    _dsIn.Tables[0].Rows.Add(_dsIn.Tables[0].NewRow());
                    gvFuncionalidadeIn.DataSource = _dsIn;
                    gvFuncionalidadeIn.DataBind();
                    gvFuncionalidadeIn.Rows[0].Visible = false;
                    _Avisos = "Nenhum registro encontrado !";
                }
            }

            clsFuncionalidadePerfil _clsPerfilOut = new clsFuncionalidadePerfil();
            DataSet _dsOut = _clsPerfilOut.RetornarAssociacao(p_idperfil, null, "N");

            if (_clsPerfilOut.Erros != null && _clsPerfilOut.Erros.Length > 0)
                _Erros = _clsPerfilOut.Erros;
            else
            {
                if (_dsOut.Tables[0].Rows.Count != 0)
                {
                    gvFuncionalidadeOut.DataSource = _dsOut;
                    gvFuncionalidadeOut.DataBind();
                }
                else
                {
                    _dsOut.Tables[0].Rows.Add(_dsOut.Tables[0].NewRow());
                    gvFuncionalidadeOut.DataSource = _dsOut;
                    gvFuncionalidadeOut.DataBind();
                    gvFuncionalidadeOut.Rows[0].Visible = false;
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
                    CarregarGrid(Convert.ToInt32(ddlPerfil.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void ddlPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(Convert.ToInt32(ddlPerfil.SelectedValue));
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void gvFuncionalidadeOut_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Adiciona
                if (e.CommandName == "Select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvFuncionalidadeOut.Rows[index];
                    TableCell tcCell = selectedRow.Cells[0];

                    int p_idfuncionalidade = Convert.ToInt32(tcCell.Text);

                    // verifica se o ja existe o campo associado com o status desativvado
                    // senao insere
                    clsFuncionalidadePerfil _clsFuncionalidadePerfilSel = new clsFuncionalidadePerfil();
                    bool _Insercao;
                    if (_clsFuncionalidadePerfilSel.Retornar(Convert.ToInt32(ddlPerfil.SelectedValue.ToString()), p_idfuncionalidade, 0).Tables[0].Rows.Count <= 0)
                    {
                        _Insercao = true;
                    }
                    else
                    {
                        _Insercao = false;
                    }

                    clsEntFuncionalidadePerfil _clsEntFuncionalidadePerfil = new clsEntFuncionalidadePerfil();

                    _clsEntFuncionalidadePerfil.idperfil = Convert.ToInt32(ddlPerfil.SelectedValue.ToString());
                    _clsEntFuncionalidadePerfil.idfuncionalidade = p_idfuncionalidade;
                    _clsEntFuncionalidadePerfil.stfuncionalidadeperfil = Convert.ToInt32(TipoSituacaoFuncionalidadePerfil.Ativo);
                    _clsEntFuncionalidadePerfil.dhsituacao = DateTime.Now;
                    _clsEntFuncionalidadePerfil.idoperador = Convert.ToInt32(hfOperador.Value);

                    clsFuncionalidadePerfil _clsFuncionalidadePerfil = new clsFuncionalidadePerfil();
                    _clsFuncionalidadePerfil.Insercao = _Insercao;
                    _clsFuncionalidadePerfil.Gravar(_clsEntFuncionalidadePerfil);
                    _Erros = _clsFuncionalidadePerfil.Erros;

                    CarregarGrid(Convert.ToInt32(ddlPerfil.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void gvFuncionalidadeIn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Remove
                if (e.CommandName == "Select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvFuncionalidadeIn.Rows[index];
                    TableCell tcCell = selectedRow.Cells[1];

                    int p_idfuncionalidade = Convert.ToInt32(tcCell.Text);

                    bool _Insercao = false;

                    clsEntFuncionalidadePerfil _clsEntFuncionalidadePerfil = new clsEntFuncionalidadePerfil();

                    _clsEntFuncionalidadePerfil.idperfil = Convert.ToInt32(ddlPerfil.SelectedValue.ToString());
                    _clsEntFuncionalidadePerfil.idfuncionalidade = p_idfuncionalidade;
                    _clsEntFuncionalidadePerfil.stfuncionalidadeperfil = Convert.ToInt32(TipoSituacaoFuncionalidadePerfil.Inativo);
                    _clsEntFuncionalidadePerfil.dhsituacao = DateTime.Now;
                    _clsEntFuncionalidadePerfil.idoperador = Convert.ToInt32(hfOperador.Value);

                    clsFuncionalidadePerfil _clsFuncionalidadePerfil = new clsFuncionalidadePerfil();
                    _clsFuncionalidadePerfil.Insercao = _Insercao;
                    _clsFuncionalidadePerfil.Gravar(_clsEntFuncionalidadePerfil);
                    _Erros = _clsFuncionalidadePerfil.Erros;

                    CarregarGrid(Convert.ToInt32(ddlPerfil.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }



    }
}