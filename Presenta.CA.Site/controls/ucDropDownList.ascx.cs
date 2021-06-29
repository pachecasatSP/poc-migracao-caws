using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso.controls
{
    public partial class ucDropDownList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.IsPostBack)
            //    CarregarDados();
        }

        public void Selecionar(int p_situacao)
        {
            try
            {
                //CarregarDados();
                cboSituacao.SelectedValue = p_situacao.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        public void CarregarDados(string p_dominio)
        {
            if (cboSituacao.Items.Count == 0)
            {
                if (p_dominio == "Padrao")
                {
                    cboSituacao.Items.Add(new ListItem("Ativo", "1"));
                    cboSituacao.Items.Add(new ListItem("Inativo", "2"));
                }

                if (p_dominio == "SituacaoFuncionalidade")
                {

                    clsSituacaoFuncionalidade _clsSituacaoFuncionalidade = new clsSituacaoFuncionalidade();
                    cboSituacao.DataSource = _clsSituacaoFuncionalidade.Retornar(null, null);
                    cboSituacao.DataTextField = "dssituacaofuncionalidade";
                    cboSituacao.DataValueField = "stfuncionalidade";
                }

                if (p_dominio == "SituacaoOperador")
                {
                    clsSituacaoOperador _SituacaoOperador = new clsSituacaoOperador();
                    cboSituacao.DataSource = _SituacaoOperador.Retornar(null, null);
                    cboSituacao.DataTextField = "dssituacaooperador";
                    cboSituacao.DataValueField = "stoperador";
                }

                if (p_dominio == "SituacaoPerfil")
                {
                    clsSituacaoPerfil _SituacaoPerfil = new clsSituacaoPerfil();
                    cboSituacao.DataSource = _SituacaoPerfil.Retornar(null, null);
                    cboSituacao.DataTextField = "dssituacaoperfil";
                    cboSituacao.DataValueField = "stperfil";
                }

                if (p_dominio == "SituacaoPerfilOperador")
                {
                    clsSituacaoPerfilOperador _SituacaoPerfilOperador = new clsSituacaoPerfilOperador();
                    cboSituacao.DataSource = _SituacaoPerfilOperador.Retornar(null, null);
                    cboSituacao.DataTextField = "dsperfiloperador";
                    cboSituacao.DataValueField = "stperfiloperador";
                }

                if (p_dominio == "SituacaoFuncionalidadePerfil")
                {
                    clsSituacaoFuncionalidadePerfil _SituacaoFuncionalidadePerfil = new clsSituacaoFuncionalidadePerfil();
                    cboSituacao.DataSource = _SituacaoFuncionalidadePerfil.Retornar(null, null);
                    cboSituacao.DataTextField = "dssituacaofuncionalidadeperfil";
                    cboSituacao.DataValueField = "stfuncionalidadeperfil";
                }                

                if (p_dominio == "TipoSenha")
                {
                    clsTipoSenha _TipoSenha = new clsTipoSenha();
                    cboSituacao.DataSource = _TipoSenha.Retornar(null, null);
                    cboSituacao.DataTextField = "dstiposenha";
                    cboSituacao.DataValueField = "idtiposenha";
                }

                if (p_dominio == "TipoSenhaValidacao")
                {
                    clsTipoSenhaValidacao _TipoSenhaValidacao = new clsTipoSenhaValidacao();
                    cboSituacao.DataSource = _TipoSenhaValidacao.Retornar(null, null);
                    cboSituacao.DataTextField = "dstiposenhavalidacao";
                    cboSituacao.DataValueField = "idtiposenhavalidacao";
                }

                if (p_dominio == "Aplicativo")
                {
                    clsAplicativo _Aplicativo = new clsAplicativo();
                    cboSituacao.DataSource = _Aplicativo.Retornar(null, null, null);
                    cboSituacao.DataTextField = "dsaplicativo";
                    cboSituacao.DataValueField = "idaplicativo";
                }


                if (p_dominio == "Operador")
                {
                    clsOperador _Operador = new clsOperador();
                    cboSituacao.DataSource = _Operador.Retornar(null, null);
                    cboSituacao.DataTextField = "nmoperador";
                    cboSituacao.DataValueField = "idoperador";
                }

                if (p_dominio == "Perfil")
                {
                    clsPerfil _Perfil = new clsPerfil();
                    cboSituacao.DataSource = _Perfil.Retornar(null, null);
                    cboSituacao.DataTextField = "dsperfil";
                    cboSituacao.DataValueField = "idperfil";
                }

                if (p_dominio == "Sistema")
                {
                    clsSistema _Sistema = new clsSistema();
                    cboSituacao.DataSource = _Sistema.Retornar(null, null);
                    cboSituacao.DataTextField = "dssistema";
                    cboSituacao.DataValueField = "idsistema";
                }

                if (p_dominio == "Funcionalidade")
                {
                    clsFuncionalidade _Funcionalidade = new clsFuncionalidade();
                    cboSituacao.DataSource = _Funcionalidade.Retornar(null, null);
                    cboSituacao.DataTextField = "dsfuncionalidade";
                    cboSituacao.DataValueField = "idfuncionalidade";
                }






                cboSituacao.DataBind();
            }
        }

        public bool Enabled
        {
            get{ return this.cboSituacao.Enabled; }
            set{ this.cboSituacao.Enabled = value; }
        }
    }
}