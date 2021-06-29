using Presenta.CA.Model;
using Presenta.CA.Site.Pages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presenta.Common.Util;
using Presenta.CA.Site.Enums;
using Presenta.CA.Site.Enums.Pages;

namespace Presenta.CA.Site.Pages.Configuracao
{
    public partial class Geral : PageBase, ISecurityBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarFuncionalidadesPagina(ConfiguracaoGeralEnum.SomenteLeitura);
                AtribuirAutorizacao();

                bool hasAuth = false;

                for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
                {
                    hasAuth = funcionalidadesPagina[i, 1] == 1;

                    if (hasAuth)
                        break;
                }

                if (hasAuth)
                {
                    bool readOnly = false, fullControl = false;

                    for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
                    {
                        bool ok = funcionalidadesPagina[i, 1] == 1 ? true : false;

                        switch ((ConfiguracaoGeralEnum)funcionalidadesPagina[i, 0])
                        {
                            case ConfiguracaoGeralEnum.ControleTotal:
                                fullControl = ok;
                                break;
                            case ConfiguracaoGeralEnum.SomenteLeitura:
                                readOnly = ok;
                                break;
                            default:
                                break;
                        }
                    }

                    if (readOnly && !fullControl)
                    {
                        hdfReadOnly.Value = "1";
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect(ResolveUrl("~/AccessDenied.aspx"), false);
                }

                if (!IsPostBack)
                {
                    CarregarDados();
                }

                ConfigurarAcessosEventos();

                txtSenhaAtual.Attributes["type"] = "password";
                txtSenhaPadrao.Attributes["type"] = "password";
                txtSenhaPadraoConfirm.Attributes["type"] = "password";

                if (hdfSalvarClick.Value != "1") { GetHiddenFieldsValues(); }
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                hdfError.Value = "1";
                hdfText1.Value = currentMethod.Name;
                hdfText2.Value = errorMessage;

                try
                {
                    var caLogModel = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLogModel.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private void GetHiddenFieldsValues()
        {
            rblTipoAutenticacao.SelectedValue = hdfTipoAutenticacao.Value;
            txtSenhaPadrao.Text = hdfSenhaPadrao.Value;
            txtSenhaPadraoConfirm.Text = hdfSenhaPadraoConfirm.Value;
            txtDiasTrocaSenha.Text = hdfDiasTrocaSenha.Value;
            txtDiasDesativarSenha.Text = hdfDiasDesativarSenha.Value;
            txtTentivasInvalidasPermitidas.Text = hdfTentivasInvalidasPermitidas.Value;
            txtNumeroMinimoCaracteres.Text = hdfNumeroMinimoCaracteres.Value;
            chkPossuirLetrasMaiusculas.Checked = hdfPossuirLetrasMaiusculas.Value == "1" ? true : false;
            chkPossuirLetrasMinusculas.Checked = hdfPossuirLetrasMinusculas.Value == "1" ? true : false;
            chkPossuirAlgarismosArabicos.Checked = hdfPossuirAlgarismosArabicos.Value == "1" ? true : false;
            chkPossuirCaracteresEspeciais.Checked = hdfPossuirCaracteresEspeciais.Value == "1" ? true : false;
            chkDesabilitarCaracteresIdenticos.Checked = hdfDesabilitarCaracteresIdenticos.Value == "1" ? true : false;
        }

        private void SetHiddenFieldsValues()
        {
            hdfTipoAutenticacao.Value = rblTipoAutenticacao.SelectedValue;
            hdfSenhaPadrao.Value = txtSenhaPadrao.Text;
            hdfSenhaPadraoConfirm.Value = txtSenhaPadraoConfirm.Text;
            hdfDiasTrocaSenha.Value = txtDiasTrocaSenha.Text;
            hdfDiasDesativarSenha.Value = txtDiasDesativarSenha.Text;
            hdfTentivasInvalidasPermitidas.Value = txtTentivasInvalidasPermitidas.Text;
            hdfNumeroMinimoCaracteres.Value = txtNumeroMinimoCaracteres.Text;
            hdfPossuirLetrasMaiusculas.Value = chkPossuirLetrasMaiusculas.Checked ? "1" : "0";
            hdfPossuirLetrasMinusculas.Value = chkPossuirLetrasMinusculas.Checked ? "1" : "0";
            hdfPossuirAlgarismosArabicos.Value = chkPossuirAlgarismosArabicos.Checked ? "1" : "0";
            hdfPossuirCaracteresEspeciais.Value = chkPossuirCaracteresEspeciais.Checked ? "1" : "0";
            hdfDesabilitarCaracteresIdenticos.Value = chkDesabilitarCaracteresIdenticos.Checked ? "1" : "0";
        }

        private void ConfigurarAcessosEventos()
        {
            CarregarFuncionalidadesPagina(ConfiguracaoGeralEnum.SomenteLeitura);
            AtribuirAutorizacao();
            TratarAutorizacaoControles();
            BindActionBarEvents();
        }

        private void CarregarDados()
        {
            var caConfiguracaoModel = new CaConfiguracaoModel(1).Selecionar();

            if (caConfiguracaoModel == null)
            {
                hdfError.Value = "1";
                hdfText1.Value = "Erro no carregamento dos dados.";
                hdfText2.Value = "Tabela(caConfiguracao) não configurada.";
                hdfSemConfig.Value = "1";
                return;
            }

            hdfSemConfig.Value = "0";

            rblTipoAutenticacao.SelectedValue = caConfiguracaoModel.IdTipoLogon.ToString();
            txtDiasTrocaSenha.Text = caConfiguracaoModel.DiasTrocaSenha.ToString();
            txtDiasDesativarSenha.Text = caConfiguracaoModel.DiasDesativSenha.ToString();
            txtTentivasInvalidasPermitidas.Text = caConfiguracaoModel.MaxTentinValidas.ToString();

            txtSenhaPadrao.Attributes.Add("value", caConfiguracaoModel.SenhaPadraoDescriptografada);
            txtSenhaPadraoConfirm.Attributes.Add("value", caConfiguracaoModel.SenhaPadraoDescriptografada);
            hdfSenhaAtual.Value = caConfiguracaoModel.SenhaPadraoDescriptografada;

            var caTipoSenhaTipoSenhaValidacaoModelList = new CaTipoSenhaTipoSenhaValidacaoModel { IdTipoSenha = 1 }.Listar();

            foreach (var item in caTipoSenhaTipoSenhaValidacaoModelList)
            {
                switch (item.NuOrdem)
                {
                    case 1:
                        txtNumeroMinimoCaracteres.Text = item.Ativo ? item.QtMinCaracteres.ToString() : string.Empty;
                        break;
                    case 2:
                        chkPossuirLetrasMaiusculas.Checked = item.Ativo;
                        break;
                    case 3:
                        chkPossuirLetrasMinusculas.Checked = item.Ativo;
                        break;
                    case 4:
                        chkPossuirAlgarismosArabicos.Checked = item.Ativo;
                        break;
                    case 5:
                        chkPossuirCaracteresEspeciais.Checked = item.Ativo;
                        break;
                    case 6:
                        chkDesabilitarCaracteresIdenticos.Checked = item.Ativo;
                        break;
                    default:
                        break;
                }
            }

            SetHiddenFieldsValues();
        }

        public void TratarAutorizacaoControles()
        {
            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                bool hasAuth = funcionalidadesPagina[i, 1] == 1 ? true : false;

                switch ((ConfiguracaoGeralEnum)funcionalidadesPagina[i, 0])
                {
                    case ConfiguracaoGeralEnum.SomenteLeitura:
                    case ConfiguracaoGeralEnum.ControleTotal:
                        if (hasAuth) { wucab.ShowAlterar = true; }
                        if (hasAuth) { wucab.ShowSalvar = true; }
                        if (hasAuth) { wucab.ShowCancelar = true; }
                        break;
                    default:
                        break;
                }
            }
        }

        public void BindActionBarEvents()
        {
            wucab.AlterarEvent += new UserControls.ActionBar.AlterarDelegate(AlterarAction);
            wucab.SalvarEvent += new UserControls.ActionBar.SalvarDelegate(SalvarAction);
            wucab.CancelarEvent += new UserControls.ActionBar.CancelarDelegate(CancelarAction);
        }

        public void SalvarAction(object sender, EventArgs e)
        {
            try
            {
                var caConfiguracaoModel = new CaConfiguracaoModel();

                caConfiguracaoModel.IdConfiguracao = 1;
                caConfiguracaoModel.IdTipoLogon = hdfTipoAutenticacao.Value.ToInt();
                caConfiguracaoModel.SenhaPadraoDescriptografada = hdfSenhaPadrao.Value;
                caConfiguracaoModel.DiasTrocaSenha = hdfDiasTrocaSenha.Value.ToInt();
                caConfiguracaoModel.DiasDesativSenha = hdfDiasDesativarSenha.Value.ToInt();
                caConfiguracaoModel.MaxTentinValidas = hdfTentivasInvalidasPermitidas.Value.ToInt();
                caConfiguracaoModel.IdOperadorAtualizacao = Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt();
                caConfiguracaoModel.DhAtualizacao = DateTime.Now;
                caConfiguracaoModel.Atualizar();
                
                var caLogModel = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLogModel.LogarInfo(idAplicativo, Constants.StdMsgInfoConfigGeral);

                var caTipoSenhaTipoSenhaValidacaoModel = new CaTipoSenhaTipoSenhaValidacaoModel { IdTipoSenha = 1, IdTipoSenhaValidacao = 1, NuOrdem = 1 };
                caTipoSenhaTipoSenhaValidacaoModel.QtMinCaracteres = string.IsNullOrEmpty(txtNumeroMinimoCaracteres.Text) ? 0 : txtNumeroMinimoCaracteres.Text.ToInt();
                caTipoSenhaTipoSenhaValidacaoModel.Ativo = string.IsNullOrEmpty(txtNumeroMinimoCaracteres.Text) ? false : txtNumeroMinimoCaracteres.Text.ToInt() > 0;
                caTipoSenhaTipoSenhaValidacaoModel.Atualizar();
                caTipoSenhaTipoSenhaValidacaoModel = new CaTipoSenhaTipoSenhaValidacaoModel { IdTipoSenha = 1, IdTipoSenhaValidacao = 1, NuOrdem = 2 };
                caTipoSenhaTipoSenhaValidacaoModel.Ativo = chkPossuirLetrasMaiusculas.Checked;
                caTipoSenhaTipoSenhaValidacaoModel.Atualizar();
                caTipoSenhaTipoSenhaValidacaoModel = new CaTipoSenhaTipoSenhaValidacaoModel { IdTipoSenha = 1, IdTipoSenhaValidacao = 1, NuOrdem = 3 };
                caTipoSenhaTipoSenhaValidacaoModel.Ativo = chkPossuirLetrasMinusculas.Checked;
                caTipoSenhaTipoSenhaValidacaoModel.Atualizar();
                caTipoSenhaTipoSenhaValidacaoModel = new CaTipoSenhaTipoSenhaValidacaoModel { IdTipoSenha = 1, IdTipoSenhaValidacao = 1, NuOrdem = 4 };
                caTipoSenhaTipoSenhaValidacaoModel.Ativo = chkPossuirAlgarismosArabicos.Checked;
                caTipoSenhaTipoSenhaValidacaoModel.Atualizar();
                caTipoSenhaTipoSenhaValidacaoModel = new CaTipoSenhaTipoSenhaValidacaoModel { IdTipoSenha = 1, IdTipoSenhaValidacao = 1, NuOrdem = 5 };
                caTipoSenhaTipoSenhaValidacaoModel.Ativo = chkPossuirCaracteresEspeciais.Checked;
                caTipoSenhaTipoSenhaValidacaoModel.Atualizar();
                caTipoSenhaTipoSenhaValidacaoModel = new CaTipoSenhaTipoSenhaValidacaoModel { IdTipoSenha = 1, IdTipoSenhaValidacao = 1, NuOrdem = 6 };
                caTipoSenhaTipoSenhaValidacaoModel.Ativo = chkDesabilitarCaracteresIdenticos.Checked;
                caTipoSenhaTipoSenhaValidacaoModel.Atualizar();
                
                CarregarDados();

                hdfUpdate.Value = "1";
                hdfSalvarClick.Value = "0";
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());

                hdfError.Value = "1";
                hdfText1.Value = currentMethod.Name;
                hdfText2.Value = errorMessage;

                try
                {
                    var caLogModel = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLogModel.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        public void InserirAction(object sender, EventArgs e) { throw new NotImplementedException(); }
        public void AlterarAction(object sender, EventArgs e) { }
        public void CancelarAction(object sender, EventArgs e) { }
        public void ExcluirAction(object sender, EventArgs e) { throw new NotImplementedException(); }
        public void ImprimirAction(object sender, EventArgs e) { throw new NotImplementedException(); }
        public void LocalizarAction(object sender, EventArgs e) { throw new NotImplementedException(); }
    }
}