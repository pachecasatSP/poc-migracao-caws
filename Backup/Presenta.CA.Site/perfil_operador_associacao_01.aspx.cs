using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public partial class perfil_operador_associacao : System.Web.UI.Page
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
            clsPerfilOperador _clsOperadorIn = new clsPerfilOperador();
            DataSet _dsIn = _clsOperadorIn.RetornarLista(p_idperfil, null, "S");

            if (_clsOperadorIn.Erros != null && _clsOperadorIn.Erros.Length > 0)
                _Erros = _clsOperadorIn.Erros;
            else
            {
                if (_dsIn.Tables[0].Rows.Count != 0)
                {
                    gvOperadorIn.DataSource = _dsIn;
                    gvOperadorIn.DataBind();
                }
                else
                {
                    _dsIn.Tables[0].Rows.Add(_dsIn.Tables[0].NewRow());
                    gvOperadorIn.DataSource = _dsIn;
                    gvOperadorIn.DataBind();
                    gvOperadorIn.Rows[0].Visible = false;
                    _Avisos = "Nenhum registro encontrado !";
                }
            }

            clsPerfilOperador _clsOperadorOut = new clsPerfilOperador();
            DataSet _dsOut = _clsOperadorOut.RetornarLista(p_idperfil, null, "N");

            if (_clsOperadorOut.Erros != null && _clsOperadorOut.Erros.Length > 0)
                _Erros = _clsOperadorOut.Erros;
            else
            {
                if (_dsOut.Tables[0].Rows.Count != 0)
                {
                    gvOperadorOut.DataSource = _dsOut;
                    gvOperadorOut.DataBind();
                }
                else
                {
                    _dsOut.Tables[0].Rows.Add(_dsOut.Tables[0].NewRow());
                    gvOperadorOut.DataSource = _dsOut;
                    gvOperadorOut.DataBind();
                    gvOperadorOut.Rows[0].Visible = false;
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

        protected void gvOperadorOut_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Adiciona
                if (e.CommandName == "Select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvOperadorOut.Rows[index];
                    TableCell tcCell = selectedRow.Cells[0];

                    int p_idoperador = Convert.ToInt32(tcCell.Text);

                    // verifica se o ja existe o campo associado com o status desativado
                    // senao insere
                    clsPerfilOperador _clsPerfilOperadorSel = new clsPerfilOperador();
                    bool _Insercao;
                    if (_clsPerfilOperadorSel.Retornar(Convert.ToInt32(ddlPerfil.SelectedValue.ToString()), p_idoperador, 0).Tables[0].Rows.Count <= 0)
                    {
                        _Insercao = true;
                    }
                    else
                    {
                        _Insercao = false;
                    }

                    clsEntPerfilOperador _clsEntPerfilOperador = new clsEntPerfilOperador();

                    _clsEntPerfilOperador.idperfil = Convert.ToInt32(ddlPerfil.SelectedValue.ToString());
                    _clsEntPerfilOperador.idoperador = p_idoperador;
                    _clsEntPerfilOperador.stperfiloperador = Convert.ToInt32(TipoSituacaoPerfilOperador.Ativo);
                    _clsEntPerfilOperador.dhsituacao = DateTime.Now;
                    _clsEntPerfilOperador.dhatualizacao = DateTime.Now;
                    _clsEntPerfilOperador.idoperadoratualizacao = Convert.ToInt32(hfOperador.Value);

                    clsPerfilOperador _clsPerfilOperador = new clsPerfilOperador();
                    _clsPerfilOperador.Insercao = _Insercao;
                    _clsPerfilOperador.Gravar(_clsEntPerfilOperador);
                    _Erros = _clsPerfilOperador.Erros;

                    CarregarGrid(Convert.ToInt32(ddlPerfil.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void gvOperadorIn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Remove
                if (e.CommandName == "Select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvOperadorIn.Rows[index];
                    TableCell tcCell = selectedRow.Cells[1];

                    int p_idoperador = Convert.ToInt32(tcCell.Text);

                    bool _Insercao = false;

                    clsEntPerfilOperador _clsEntPerfilOperador = new clsEntPerfilOperador();

                    _clsEntPerfilOperador.idperfil = Convert.ToInt32(ddlPerfil.SelectedValue.ToString());
                    _clsEntPerfilOperador.idoperador = p_idoperador;
                    _clsEntPerfilOperador.stperfiloperador = Convert.ToInt32(TipoSituacaoPerfilOperador.Inativo);
                    _clsEntPerfilOperador.dhsituacao = DateTime.Now;
                    _clsEntPerfilOperador.dhatualizacao = DateTime.Now;
                    _clsEntPerfilOperador.idoperadoratualizacao = Convert.ToInt32(hfOperador.Value);

                    clsPerfilOperador _clsPerfilOperador = new clsPerfilOperador();
                    _clsPerfilOperador.Insercao = _Insercao;
                    _clsPerfilOperador.Gravar(_clsEntPerfilOperador);
                    _Erros = _clsPerfilOperador.Erros;

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