using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public partial class perfil_operador_associacao_02 : System.Web.UI.Page
    {
        public string _Erros = string.Empty;
        public string _Avisos = string.Empty;

        private void CarregarCombo()
        {
            clsOperador _clsOperador = new clsOperador();
            ddlOperador.DataSource = _clsOperador.Retornar(null, null);
            ddlOperador.DataTextField = "nmoperador";
            ddlOperador.DataValueField = "idoperador";
            ddlOperador.DataBind();
        }

        private void CarregarGrid(int p_idperfil)
        {
            clsPerfilOperador _clsPerfilIn = new clsPerfilOperador();
            DataSet _dsIn = _clsPerfilIn.RetornarLista(null, p_idperfil, "S");

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

            clsPerfilOperador _clsPerfilOut = new clsPerfilOperador();
            DataSet _dsOut = _clsPerfilOut.RetornarLista(null, p_idperfil, "N");

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
                    CarregarGrid(Convert.ToInt32(ddlOperador.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }

        protected void ddlOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(Convert.ToInt32(ddlOperador.SelectedValue));
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

                    // verifica se o ja existe o campo associado com o status desativado
                    // senao insere
                    clsPerfilOperador _clsPerfilOperadorSel = new clsPerfilOperador();
                    bool _Insercao;
                    if (_clsPerfilOperadorSel.Retornar(Convert.ToInt32(ddlOperador.SelectedValue.ToString()), p_idperfil, 0).Tables[0].Rows.Count <= 0)
                    {
                        _Insercao = true;
                    }
                    else
                    {
                        _Insercao = false;
                    }

                    clsEntPerfilOperador _clsEntPerfilOperador = new clsEntPerfilOperador();

                    _clsEntPerfilOperador.idoperador = Convert.ToInt32(ddlOperador.SelectedValue.ToString());
                    _clsEntPerfilOperador.idperfil = p_idperfil;
                    _clsEntPerfilOperador.stperfiloperador = Convert.ToInt32(TipoSituacaoPerfilOperador.Ativo);
                    _clsEntPerfilOperador.dhsituacao = DateTime.Now;
                    _clsEntPerfilOperador.dhatualizacao = DateTime.Now;
                    _clsEntPerfilOperador.idoperadoratualizacao = Convert.ToInt32(hfOperador.Value);

                    clsPerfilOperador _clsPerfilOperador = new clsPerfilOperador();
                    _clsPerfilOperador.Insercao = _Insercao;
                    _clsPerfilOperador.Gravar(_clsEntPerfilOperador);
                    _Erros = _clsPerfilOperador.Erros;

                    CarregarGrid(Convert.ToInt32(ddlOperador.SelectedValue));
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

                    clsEntPerfilOperador _clsEntPerfilOperador = new clsEntPerfilOperador();

                    _clsEntPerfilOperador.idoperador = Convert.ToInt32(ddlOperador.SelectedValue.ToString());
                    _clsEntPerfilOperador.idperfil = p_idperfil;
                    _clsEntPerfilOperador.stperfiloperador = Convert.ToInt32(TipoSituacaoPerfilOperador.Inativo);
                    _clsEntPerfilOperador.dhsituacao = DateTime.Now;
                    _clsEntPerfilOperador.dhatualizacao = DateTime.Now;
                    _clsEntPerfilOperador.idoperadoratualizacao = Convert.ToInt32(hfOperador.Value);

                    clsPerfilOperador _clsPerfilOperador = new clsPerfilOperador();
                    _clsPerfilOperador.Insercao = _Insercao;
                    _clsPerfilOperador.Gravar(_clsEntPerfilOperador);
                    _Erros = _clsPerfilOperador.Erros;

                    CarregarGrid(Convert.ToInt32(ddlOperador.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                _Erros = clsLog.WhoCalledMe() + " - " + ex.Source + " - " + ex.Message;
            }
        }



    }
}