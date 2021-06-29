using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Presenta.Common.Util;
using Presenta.CA.Model;
using System.Web.Services;
using Presenta.CA.Site.Pages.Base;
using Presenta.CA.Site.Enums.Pages;

namespace Presenta.CA.Site.Pages.Relatorio
{
    public partial class PerfilFuncionalidade : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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

                if (!hasAuth)
                    HttpContext.Current.Response.Redirect(ResolveUrl("~/AccessDenied.aspx"), false);
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
                    var caLog = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                int? sistema = hdfIdSistema.Value == "-1" ? null : hdfIdSistema.Value.ToNullableInt();
                int? aplicativo = hdfIdAplicativo.Value == "-1" ? null : hdfIdAplicativo.Value.ToNullableInt();

                var dt = new RptPerfilFuncionalidadeModel().Listar(sistema, aplicativo);

                if (dt == null)
                {
                    lblResultado.Visible = true;
                    return;
                }

                reportViewer.LocalReport.ReportPath = @"Pages\Relatorio\PerfilFuncionalidadeReport.rdlc";

                reportViewer.LocalReport.DataSources.Clear();

                reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt));

                reportViewer.DataBind();
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
                    var caLog = new CaLogModel(Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<CaSistemaModel> ObterSistemas()
        {
            return new CaSistemaModel().Listar();
        }

        [WebMethod(EnableSession = true)]
        public static List<CaAplicativoModel> ObterAplicativos(int idSistema)
        {
            return new CaAplicativoModel() { IdSistema = idSistema }.Listar();
        }

    }
}