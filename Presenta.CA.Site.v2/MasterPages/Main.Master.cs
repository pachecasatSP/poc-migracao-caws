using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Presenta.CA.Site.MasterPages.Items;
using Presenta.CA.Site.Enums;
using Presenta.Common.Security;
using Presenta.Common.Util;
using Presenta.CA.Model;
using Presenta.CA.Site.MasterPages.Base;
using Presenta.CA.Site.Enums.Pages;

namespace Presenta.CA.Site.MasterPages
{
    public partial class Principal : MasterPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Header.DataBind();

                if (!IsPostBack)
                {
                    string login = "";
                    
                    if (Session[Constants.SessionNmUsuario] != null)
                    {
                        login = Session[Constants.SessionNmUsuario].ToString().ToUpper();
                    }
                    else
                    {
                        login = System.Web.HttpContext.Current.User.Identity.Name.ToUpper();
                    }

                    lblNomeOperador.Text = login = login.IndexOf(@"\") != -1 ? login.Substring(login.IndexOf(@"\") + 1) : login;

                    if (!String.IsNullOrEmpty(Config.GetKeyValue(Constants.KeySiteMinder)) && Session[Constants.SessionNmUsuario] == null)
                    {
                        if (Request.Headers.Get(Config.GetKeyValue(Constants.KeySiteMinder)) != null && !String.IsNullOrEmpty(Request.Headers.Get(Config.GetKeyValue(Constants.KeySiteMinder))))
                        {
                            login = Request.Headers.Get(Config.GetKeyValue(Constants.KeySiteMinder));
                            lblNomeOperador.Text = login = login.IndexOf(@"\") != -1 ? login.Substring(login.IndexOf(@"\") + 1) : login;
                        }
                    }

                    var service = new CAWS.CAv2ServiceSoapClient();
                    int tipoLogon = service.ObterTipoAutenticacao();
                    int tipoAuth = Config.GetKeyValue(Constants.KeyTipoAutenticacao).ToInt();

                    int[] funcionalidades;

                    if (tipoAuth == 1 && tipoLogon == 1 && Session[Constants.SessionFuncKey] == null) // Windows
                    {
                        int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();

                        var funcs = service.Autorizar(login.ToLower(), null, idAplicativo);

                        if (funcs != null)
                        {
                            funcionalidades = funcs.ToArray();
                            Session[Constants.SessionFuncKey] = funcionalidades;

                            var dadosUsuarioCA = service.ObterInfo(login.ToLower(), null, idAplicativo);
                            Session[Constants.SessionIdUsuario] = Convert.ToInt32(dadosUsuarioCA[0]);
                        }
                    }

                    hdfTimeoutInterface.Value = Config.GetKeyValue(Constants.KeyTimeoutInterface);

                    lblVersao.Text = String.Format("Versão {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                    hdfMajorMinorVersion.Value =
                        String.Format(
                            "Controle de Acesso {0}.{1} - Presenta Sistemas",
                            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(),
                            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString()
                            );
                }

                if (Session[Constants.SessionGrandMenuKey] == null || String.IsNullOrEmpty(Session[Constants.SessionGrandMenuKey].ToString()))
                {
                    GetSetListaAssociacao();
                }
                else
                {
                    switch ((GrandMenuEnum)Session[Constants.SessionGrandMenuKey])
                    {
                        case GrandMenuEnum.Cadastro:
                            GetSetListaCadastro();
                            break;
                        case GrandMenuEnum.Configuracoes:
                            GetSetListaConfiguracoes();
                            break;
                        case GrandMenuEnum.Associacao:
                            GetSetListaAssociacao();
                            break;
                        case GrandMenuEnum.Relatorios:
                            GetSetListaRelatorios();
                            break;
                        default:
                            GetSetListaAssociacao();
                            break;
                    }
                }
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

                try
                {
                    var caLog = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private List<LinkButtonPageItem> GetSetListaConfiguracoes()
        {
            var lista = GetListaConfiguracoes();

            rptPages.DataSource = lista;
            rptPages.DataBind();

            rptToolbar.DataSource = lista;
            rptToolbar.DataBind();

            Session[Constants.SessionGrandMenuKey] = GrandMenuEnum.Configuracoes;
            hdfIdGrandMenu.Value = (((int)GrandMenuEnum.Configuracoes) - 1).ToString();

            return lista;
        }

        private List<LinkButtonPageItem> GetListaConfiguracoes()
        {
            var lista = new List<LinkButtonPageItem>();

            AddConfiguracoesItem(lista);

            return lista;
        }

        private void AddConfiguracoesItem(List<LinkButtonPageItem> lista)
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

            lista.Add(
                new LinkButtonPageItem(
                    "Geral",
                    ResolveUrl("~/Images/application_form.png"),
                    ResolveUrl("~/Pages/Configuracao/Geral.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        protected void lbtConfiguracoes_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = GetSetListaConfiguracoes();

                lista = lista.FindAll(p => p.CssClass != "ocultar");

                if (lista != null && lista.Count > 0)
                    Response.Redirect(lista.First().UrlPage, false);
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

                try
                {
                    var caLog = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private List<LinkButtonPageItem> GetListaAssociacao()
        {
            var lista = new List<LinkButtonPageItem>();

            AddAssociacaoPerfilOperadorItem(lista);
            AddAssociacaoFuncionalidadePerfilItem(lista);
            AddAssociacaoPerfilFuncionalidadeItem(lista);
            
            return lista;
        }

        private void AddAssociacaoPerfilFuncionalidadeItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(AssociacoesPerfilFuncionalidadeEnum.SomenteLeitura);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Perfil x Funcionalidade",
                    ResolveUrl("~/Images/group.png"),
                    ResolveUrl("~/Pages/Associacao/PerfilFuncionalidade.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        private void AddAssociacaoFuncionalidadePerfilItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(AssociacoesFuncionalidadePerfilEnum.SomenteLeitura);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Funcionalidade x Perfil",
                    ResolveUrl("~/Images/group.png"),
                    ResolveUrl("~/Pages/Associacao/Perfil.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        private void AddAssociacaoPerfilOperadorItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(AssociacoesPerfilOperadorEnum.SomenteLeitura);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Perfil x Operador",
                    ResolveUrl("~/Images/user.png"),
                    ResolveUrl("~/Pages/Associacao/Operador.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        protected void lbtAssociacao_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = GetSetListaAssociacao();

                lista = lista.FindAll(p => p.CssClass != "ocultar");

                if (lista != null && lista.Count > 0)
                    Response.Redirect(lista.First().UrlPage, false);
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

                try
                {
                    var caLog = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private List<LinkButtonPageItem> GetSetListaAssociacao()
        {
            var lista = GetListaAssociacao();

            rptPages.DataSource = lista;
            rptPages.DataBind();

            rptToolbar.DataSource = lista;
            rptToolbar.DataBind();

            Session[Constants.SessionGrandMenuKey] = GrandMenuEnum.Associacao;
            hdfIdGrandMenu.Value = (((int)GrandMenuEnum.Associacao) - 1).ToString();

            return lista;
        }

        protected void lbtRelatorios_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = GetSetListaRelatorios();

                lista = lista.FindAll(p => p.CssClass != "ocultar");

                if (lista != null && lista.Count > 0)
                    Response.Redirect(lista.First().UrlPage, false);
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

                try
                {
                    var caLog = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private List<LinkButtonPageItem> GetSetListaRelatorios()
        {
            var lista = GetListaRelatorios();

            rptPages.DataSource = lista;
            rptPages.DataBind();

            rptToolbar.DataSource = lista;
            rptToolbar.DataBind();

            Session[Constants.SessionGrandMenuKey] = GrandMenuEnum.Relatorios;
            hdfIdGrandMenu.Value = (((int)GrandMenuEnum.Relatorios) - 1).ToString();

            return lista;
        }

        private List<LinkButtonPageItem> GetListaRelatorios()
        {
            var lista = new List<LinkButtonPageItem>();

            AddRelatorioLogItem(lista);
            AddRelatorioPerfilFuncionalidadeItem(lista);
            AddRelatorioPerfilOperadorItem(lista);
            AddRelatorioSistemaItem(lista);
            
            return lista;
        }

        private void AddRelatorioSistemaItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(RelatorioSistemaAplicativoFuncionalidadeEnum.Acesso);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Sistema / Aplicativo / Funcionalidade",
                    ResolveUrl("~/Images/application_side_tree.png"),
                    ResolveUrl("~/Pages/Relatorio/Sistemas.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        private void AddRelatorioPerfilOperadorItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(RelatorioPerfilOperadorEnum.Acesso);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Perfil x Operador",
                    ResolveUrl("~/Images/user.png"),
                    ResolveUrl("~/Pages/Relatorio/PerfilOperador.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        private void AddRelatorioPerfilFuncionalidadeItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(RelatorioPerfilFuncionalidadeEnum.Acesso);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Perfil x Funcionalidade",
                    ResolveUrl("~/Images/group.png"),
                    ResolveUrl("~/Pages/Relatorio/PerfilFuncionalidade.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        private void AddRelatorioLogItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(RelatorioLogEnum.Acesso);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Log",
                    ResolveUrl("~/Images/log.png"),
                    ResolveUrl("~/Pages/Relatorio/Log.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        protected void lbtCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = GetSetListaCadastro();

                lista = lista.FindAll(p => p.CssClass != "ocultar");

                if (lista != null && lista.Count > 0)
                    Response.Redirect(lista.First().UrlPage, false);
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

                try
                {
                    var caLog = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private List<LinkButtonPageItem> GetSetListaCadastro()
        {
            var lista = GetListaCadastro();

            rptPages.DataSource = lista;
            rptPages.DataBind();

            rptToolbar.DataSource = lista;
            rptToolbar.DataBind();

            Session[Constants.SessionGrandMenuKey] = GrandMenuEnum.Cadastro;
            hdfIdGrandMenu.Value = (((int)GrandMenuEnum.Cadastro) - 1).ToString();

            return lista;
        }

        private List<LinkButtonPageItem> GetListaCadastro()
        {
            var lista = new List<LinkButtonPageItem>();

            AddCadastroSistemasItem(lista);
            AddCadastroPerfilFuncionalidadeItem(lista);
            AddCadastroOperadorItem(lista);
            AddCadastroPerfilItem(lista);
            
            return lista;
        }

        private void AddCadastroPerfilFuncionalidadeItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(CadastroPerfilFuncionalidadeEnum.Acesso);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Perfil / Funcionalidades",
                    ResolveUrl("~/Images/application_view_list.png"),
                    ResolveUrl("~/Pages/Cadastro/PerfilFuncionalidade.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        private void AddCadastroPerfilItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(CadastroPerfilEnum.SomenteLeitura);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Perfil",
                    ResolveUrl("~/Images/group.png"),
                    ResolveUrl("~/Pages/Cadastro/Perfil.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        private void AddCadastroOperadorItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(CadastroOperadorEnum.SomenteLeitura);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Operador",
                    ResolveUrl("~/Images/user.png"),
                    ResolveUrl("~/Pages/Cadastro/Operador.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }

        private void AddCadastroSistemasItem(List<LinkButtonPageItem> lista)
        {
            CarregarFuncionalidadesPagina(CadastroSistemaAplicativoFuncionalidadeEnum.Acesso);
            AtribuirAutorizacao();

            bool hasAuth = false;

            for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
            {
                hasAuth = funcionalidadesPagina[i, 1] == 1;

                if (hasAuth)
                    break;
            }

            lista.Add(
                new LinkButtonPageItem(
                    "Sistema / Aplicativo / Funcionalidade / Perfis",
                    ResolveUrl("~/Images/application_side_tree.png"),
                    ResolveUrl("~/Pages/Cadastro/Sistemas.aspx"),
                    "$.blockUI();",
                    hasAuth ? String.Empty : "ocultar"
                    )
                );
        }
    }
}