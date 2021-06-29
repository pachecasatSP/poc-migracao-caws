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
    /// Summary description for SistemasUCHandler
    /// </summary>
    public class SistemaUCHandler : IHttpHandler, IRequiresSessionState
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
                case "FillSistemas":
                    {
                        FillSistemas(context, request, response);
                        break;
                    }
                case "InsertSistemas":
                    {
                        InsertSistemas(context, request, response);
                        break;
                    }
                case "UpdateSistemas":
                    {
                        UpdateSistemas(context, request);
                        break;
                    }
                case "DeleteSistemas":
                    {
                        DeleteSistemas(context, request);
                        break;
                    }
                case "FillSistemasPorPerfil":
                    {
                        FillSistemasPorPerfil(context, request, response);
                        break;
                    }
                default:
                    break;
            }
        }

        private void DeleteSistemas(HttpContext context, HttpRequest request)
        {
            string id = request["id"].ToString(CultureInfo.InvariantCulture);
            
            var caSistemaModel = new CaSistemaModel()
            {
                IdSistema = id.ToInt()
            };

            caSistemaModel.Excluir();

            var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
            int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
            caLog.LogarInfo(idAplicativo, Constants.SistemaExcluir);
        }

        private void UpdateSistemas(HttpContext context, HttpRequest request)
        {
            try
            {
                string id = request["id"].ToString(CultureInfo.InvariantCulture);
                string descricao = request["DsSistema"].ToString();

                var caSistemaModel = new CaSistemaModel()
                {
                    DsSistema = descricao,
                    IdOperadorAtualizacao = context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt(),
                    DhAtualizacao = DateTime.Now,
                    IdSistema = id.ToInt()
                };

                caSistemaModel.Atualizar();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, Constants.SistemaAlterar);
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

        private void InsertSistemas(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string descricao = request["DsSistema"].ToString();
                
                var caSistemaModel = new CaSistemaModel()
                {
                    DsSistema = descricao,
                    IdOperadorAtualizacao = context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt(),
                    DhAtualizacao = DateTime.Now
                };

                caSistemaModel.Inserir();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, Constants.SistemaInserir);

                response.Write(caSistemaModel.IdSistema.ToString());
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

        private void FillSistemas(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
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

                string output = ObterSistemas(jqSearchData);

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

        private string ObterSistemas(JqSearch jqSearchData)
        {
            IQueryable<CaSistemaModel> lista;
            List<CaSistemaModel> sistemas;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caSistemaModel = new CaSistemaModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaSistemaModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caSistemaModel.Listar().AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                sistemas = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                sistemas = caSistemaModel.Listar();

                if (sistemas == null)
                {
                    return String.Empty;
                }

                sistemas = sistemas.OrderBy(orderBy).ToList();
            }

            int totalRegistros = sistemas.Count;

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
                rows = (from sistema in sistemas
                        select new[] {
                            sistema.IdSistema.ToString(),
                            sistema.DsSistema
                        }).ToList().GetRange(ini, fim)
            };

            return new JavaScriptSerializer().Serialize(result);
        }

        private void FillSistemasPorPerfil(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string idPerfil = request["IdPerfil"];

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

                string output = ObterSistemasPorPerfil(jqSearchData, idPerfil);

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

        private string ObterSistemasPorPerfil(JqSearch jqSearchData, string idPerfil)
        {
            IQueryable<CaSistemaModel> lista;
            List<CaSistemaModel> sistemas;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caSistemaModel = new CaSistemaModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaSistemaModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caSistemaModel.ListarPorPerfil(idPerfil.ToInt()).AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                sistemas = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                sistemas = caSistemaModel.ListarPorPerfil(idPerfil.ToInt());

                if (sistemas == null)
                {
                    return String.Empty;
                }

                sistemas = sistemas.OrderBy(orderBy).ToList();
            }

            int totalRegistros = sistemas.Count;

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
                rows = (from sistema in sistemas
                        select new[] {
                            sistema.IdSistema.ToString(),
                            sistema.DsSistema
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