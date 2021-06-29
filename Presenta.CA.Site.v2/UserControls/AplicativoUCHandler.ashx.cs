using Presenta.CA.Model;
using Presenta.Common.Util;
using Presenta.Web.JqGrid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace Presenta.CA.Site.UserControls
{
    /// <summary>
    /// Summary description for AplicativosUCHandler
    /// </summary>
    public class AplicativoUCHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            string actionPage = request["ActionPage"];
            string action = request["Action"];

            if (actionPage != "TransportType")
                return;

            switch (action)
            {
                case "FillAplicativos":
                    {
                        FillAplicativos(context, request, response);
                        break;
                    }
                case "InsertAplicativos":
                    {
                        InsertAplicativos(context, request, response);
                        break;
                    }
                case "UpdateAplicativos":
                    {
                        UpdateAplicativos(context, request);
                        break;
                    }
                case "DeleteAplicativos":
                    {
                        DeleteAplicativos(context, request);
                        break;
                    }
                default:
                    break;
            }
        }

        private void DeleteAplicativos(HttpContext context, HttpRequest request)
        {
            try
            {
                string id = request["id"].ToString(CultureInfo.InvariantCulture);
                
                var caAplicativoModel = new CaAplicativoModel()
                {
                    IdAplicativo = id.ToInt()
                };

                caAplicativoModel.Excluir();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, Constants.AplicativoExcluir);
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                context.Response.ContentType = "text/plain";
                context.Response.StatusDescription = Constants.ErrorCodeStatusDescription;
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());
                context.Response.Write(errorMessage);

                try
                {
                    var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private void UpdateAplicativos(HttpContext context, HttpRequest request)
        {
            try
            {
                string id = request["id"].ToString(CultureInfo.InvariantCulture);
                string descricao = request["DsAplicativo"].ToString();
                string idSistema = request["IdSistema"].ToString();

                var caAplicativoModel = new CaAplicativoModel()
                {
                    DsAplicativo = descricao,
                    IdOperadorAtualizacao = context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt(),
                    DhAtualizacao = DateTime.Now,
                    IdSistema = idSistema.ToInt(),
                    IdAplicativo = id.ToInt()
                };

                caAplicativoModel.Atualizar();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, Constants.AplicativoAlterar);
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                context.Response.ContentType = "text/plain";
                context.Response.StatusDescription = Constants.ErrorCodeStatusDescription;
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());
                context.Response.Write(errorMessage);

                try
                {
                    var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private void InsertAplicativos(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string descricao = request["DsAplicativo"].ToString();
                string idSistema = request["IdSistema"].ToString();

                var caAplicativoModel = new CaAplicativoModel()
                {
                    DsAplicativo = descricao,
                    IdOperadorAtualizacao = context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt(),
                    DhAtualizacao = DateTime.Now,
                    IdSistema = idSistema.ToInt()
                };

                caAplicativoModel.Inserir();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, Constants.AplicativoInserir);

                response.Write(caAplicativoModel.IdAplicativo.ToString());
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                context.Response.ContentType = "text/plain";
                context.Response.StatusDescription = Constants.ErrorCodeStatusDescription;
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());
                context.Response.Write(errorMessage);

                try
                {
                    var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private void FillAplicativos(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string idSistema = request["IdSistema"];

                var jqSearchData = new JqSearch
                {
                    sidx = request["sidx"],
                    sord = request["sord"],
                    page = Convert.ToInt32(request["page"]),
                    rows = Convert.ToInt32(request["rows"]),
                    _search = request["_search"] != null && String.Compare(request["_search"], "true", StringComparison.Ordinal) == 0,
                    searchField = request["searchField"],
                    searchOper = request["searchOper"],
                    searchString = request["searchString"],
                    filters = request["filters"]
                };

                string output = ObterAplicativos(jqSearchData, idSistema);

                response.Cache.SetMaxAge(new TimeSpan(0));
                response.ContentType = "application/json";

                if (String.IsNullOrEmpty(output))
                {
                    response.Write("{\"page\":1,\"total\":0,\"records\":0,\"rows\":[]}");
                }
                else
                {
                    response.Write(output);
                }
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                context.Response.ContentType = "text/plain";
                context.Response.StatusDescription = Constants.ErrorCodeStatusDescription;
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        ErrorHandler.GetMethodDetail(currentMethod),
                        Environment.NewLine, Environment.NewLine,
                        ex.ToMessageAndCompleteStacktrace());
                context.Response.Write(errorMessage);

                try
                {
                    var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
            }
        }

        private string ObterAplicativos(JqSearch jqSearchData, string idSistema)
        {
            if (String.IsNullOrEmpty(idSistema)) { return String.Empty; }

            IQueryable<CaAplicativoModel> lista;
            List<CaAplicativoModel> aplicativos;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caAplicativoModel = new CaAplicativoModel() { IdSistema = idSistema.ToInt() };

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaAplicativoModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caAplicativoModel.Listar().AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                aplicativos = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                aplicativos = caAplicativoModel.Listar();

                if (aplicativos == null)
                {
                    return String.Empty;
                }

                aplicativos = aplicativos.OrderBy(orderBy).ToList();
            }

            int totalRegistros = aplicativos.Count;

            var totalPags = totalRegistros / jqSearchData.rows;

            if (totalRegistros % jqSearchData.rows > 0)
            {
                totalPags++;
            }

            var ini = (jqSearchData.page - 1) * jqSearchData.rows;
            var fim = totalRegistros > jqSearchData.rows ? jqSearchData.rows : totalRegistros;

            if (ini > 0)
            {
                fim = jqSearchData.rows;
                fim = (totalRegistros - ((jqSearchData.page - 1) * jqSearchData.rows) < jqSearchData.rows) ? totalRegistros % jqSearchData.rows : fim;
            }

            var result = new JqGridResults
            {
                page = jqSearchData.page,
                total = totalPags,
                records = totalRegistros,
                rows = (from aplicativo in aplicativos
                        select new[] {
                            aplicativo.IdAplicativo.ToString(),
                            aplicativo.DsAplicativo,
                            aplicativo.IdSistema.ToString()
                        }).ToList().GetRange(ini, fim)
            };

            return new JavaScriptSerializer().Serialize(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}