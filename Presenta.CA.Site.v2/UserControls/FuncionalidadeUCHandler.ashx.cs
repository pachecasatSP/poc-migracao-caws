using Presenta.CA.Model;
using Presenta.CA.Model.Enum;
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
    /// Summary description for FuncionalidadeUCHandler
    /// </summary>
    public class FuncionalidadeUCHandler : IHttpHandler, IRequiresSessionState
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
                case "FillFuncionalidades":
                    {
                        FillFuncionalidades(context, request, response);
                        break;
                    }
                case "FillFuncionalidadesLeft":
                    {
                        FillFuncionalidadesLeft(context, request, response);
                        break;
                    }
                case "FillFuncionalidadesRight":
                    {
                        FillFuncionalidadesRight(context, request, response);
                        break;
                    }
                case "InsertFuncionalidades":
                    {
                        InsertFuncionalidades(context, request, response);
                        break;
                    }
                case "UpdateFuncionalidades":
                    {
                        UpdateFuncionalidades(context, request);
                        break;
                    }
                case "DeleteFuncionalidades":
                    {
                        DeleteFuncionalidades(context, request);
                        break;
                    }
                case "FillFuncionalidadesPorPerfil":
                    {
                        FillFuncionalidadesPorPerfil(context, request, response);
                        break;
                    }
                default:
                    break;
            }
        }

        private void DeleteFuncionalidades(HttpContext context, HttpRequest request)
        {
            try
            {
                string id = request["id"].ToString(CultureInfo.InvariantCulture);
                
                var caFuncionalidadeModel = new CaFuncionalidadeModel()
                {
                    IdFuncionalidade = id.ToInt()
                };

                caFuncionalidadeModel.Excluir();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, Constants.FuncionalidadeExcluir);
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

        private void UpdateFuncionalidades(HttpContext context, HttpRequest request)
        {
            try
            {
                string id = request["id"].ToString(CultureInfo.InvariantCulture);
                string descricao = request["DsFuncionalidade"].ToString();
                string stFuncionalidade = request["StFuncionalidade"].ToString();
                string idAplicativo = request["IdAplicativo"].ToString();

                var caFuncionalidadeModel = new CaFuncionalidadeModel()
                {
                    DsFuncionalidade = descricao,
                    IdOperadorAtualizacao = context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt(),
                    DhAtualizacao = DateTime.Now,
                    IdAplicativo = idAplicativo.ToInt(),
                    StFuncionalidade = stFuncionalidade.ToInt(),
                    IdFuncionalidade = id.ToInt()
                };

                caFuncionalidadeModel.Atualizar();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativoLog = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativoLog, Constants.FuncionalidadeAlterar);
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

        private void InsertFuncionalidades(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string descricao = request["DsFuncionalidade"].ToString();
                string stFuncionalidade = request["StFuncionalidade"].ToString();
                string idAplicativo = request["IdAplicativo"].ToString();

                var caFuncionalidadeModel = new CaFuncionalidadeModel()
                {
                    DsFuncionalidade = descricao,
                    IdOperadorAtualizacao = context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt(),
                    DhAtualizacao = DateTime.Now,
                    IdAplicativo = idAplicativo.ToInt(),
                    StFuncionalidade = stFuncionalidade.ToInt()
                };

                caFuncionalidadeModel.Inserir();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativoLog = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativoLog, Constants.FuncionalidadeInserir);

                response.Write(caFuncionalidadeModel.IdFuncionalidade.ToString());
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

        private void FillFuncionalidades(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string idAplicativo = request["IdAplicativo"];

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

                string output = ObterFuncionalidades(jqSearchData, idAplicativo);

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

        private string ObterFuncionalidades(JqSearch jqSearchData, string idAplicativo)
        {
            if (String.IsNullOrEmpty(idAplicativo)) { return String.Empty; }

            IQueryable<CaFuncionalidadeModel> lista;
            List<CaFuncionalidadeModel> funcionalidades;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caFuncionalidadeModel = new CaFuncionalidadeModel() { IdAplicativo = idAplicativo.ToInt() };

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaFuncionalidadeModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caFuncionalidadeModel.Listar().AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                funcionalidades = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                funcionalidades = caFuncionalidadeModel.Listar();

                if (funcionalidades == null)
                {
                    return String.Empty;
                }

                funcionalidades = funcionalidades.OrderBy(orderBy).ToList();
            }

            int totalRegistros = funcionalidades.Count;

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
                rows = (from funcionalidade in funcionalidades
                        select new[] {
                            funcionalidade.IdFuncionalidade.ToString(),
                            funcionalidade.DsFuncionalidade,
                            ((CaSituacaoFuncionalidadeEnum)funcionalidade.StFuncionalidade).GetDescription(),
                            funcionalidade.IdAplicativo.ToString()
                        }).ToList().GetRange(ini, fim)
            };

            return new JavaScriptSerializer().Serialize(result);
        }

        private void FillFuncionalidadesPorPerfil(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string idPerfil = request["IdPerfil"];
                string idSistema = request["IdSistema"];
                string idAplicativo = request["IdAplicativo"];

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

                string output = ObterFuncionalidadesPorPerfil(jqSearchData, idPerfil, idSistema, idAplicativo);

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

        private string ObterFuncionalidadesPorPerfil(JqSearch jqSearchData, string idPerfil, string idSistema, string idAplicativo)
        {
            if (String.IsNullOrEmpty(idPerfil)) { return String.Empty; }

            IQueryable<CaFuncionalidadeModel> lista;
            List<CaFuncionalidadeModel> funcionalidades;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caFuncionalidadeModel = new CaFuncionalidadeModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaFuncionalidadeModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caFuncionalidadeModel.ListarPorPerfil(idPerfil.ToInt(), idSistema.ToInt(), idAplicativo.ToInt()).AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                funcionalidades = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                funcionalidades = caFuncionalidadeModel.ListarPorPerfil(idPerfil.ToInt(), idSistema.ToInt(), idAplicativo.ToInt());

                if (funcionalidades == null)
                {
                    return String.Empty;
                }

                funcionalidades = funcionalidades.OrderBy(orderBy).ToList();
            }

            int totalRegistros = funcionalidades.Count;

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
                rows = (from funcionalidade in funcionalidades
                        select new[] {
                            funcionalidade.IdFuncionalidade.ToString(),
                            funcionalidade.DsFuncionalidade,
                            ((CaSituacaoFuncionalidadeEnum)funcionalidade.StFuncionalidade).GetDescription(),
                            funcionalidade.IdAplicativo.ToString()
                        }).ToList().GetRange(ini, fim)
            };

            return new JavaScriptSerializer().Serialize(result);
        }

        private void FillFuncionalidadesLeft(HttpContext context, HttpRequest request, HttpResponse response)
        {
            FillFuncionalidadesPorPerfil(context, request, response);
        }

        private void FillFuncionalidadesRight(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string idPerfil = request["IdPerfil"];
                string idSistema = request["IdSistema"];
                string idAplicativo = request["IdAplicativo"];

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

                string output = ObterFuncionalidadesRight(jqSearchData, idPerfil, idSistema, idAplicativo);

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

        private string ObterFuncionalidadesRight(JqSearch jqSearchData, string idPerfil, string idSistema, string idAplicativo)
        {
            if (String.IsNullOrEmpty(idPerfil)) { return String.Empty; }

            IQueryable<CaFuncionalidadeModel> lista;
            List<CaFuncionalidadeModel> funcionalidades;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caFuncionalidadeModel = new CaFuncionalidadeModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaFuncionalidadeModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caFuncionalidadeModel.NotListarAtivosPorPerfil(idPerfil.ToInt(), idSistema.ToInt(), idAplicativo.ToInt()).AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                funcionalidades = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                funcionalidades = caFuncionalidadeModel.NotListarAtivosPorPerfil(idPerfil.ToInt(), idSistema.ToInt(), idAplicativo.ToInt());

                if (funcionalidades == null)
                {
                    return String.Empty;
                }

                funcionalidades = funcionalidades.OrderBy(orderBy).ToList();
            }

            int totalRegistros = funcionalidades.Count;

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
                rows = (from funcionalidade in funcionalidades
                        select new[] {
                            funcionalidade.IdFuncionalidade.ToString(),
                            funcionalidade.DsFuncionalidade,
                            ((CaSituacaoFuncionalidadeEnum)funcionalidade.StFuncionalidade).GetDescription(),
                            funcionalidade.IdAplicativo.ToString()
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