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
    /// Summary description for PerfilUCHandler
    /// </summary>
    public class PerfilUCHandler : IHttpHandler, IRequiresSessionState
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
                case "FillPerfis":
                    {
                        FillPerfis(context, request, response);
                        break;
                    }
                case "FillPerfisLeft":
                    {
                        FillPerfisLeft(context, request, response);
                        break;
                    }
                case "FillPerfisRight":
                    {
                        FillPerfisRight(context, request, response);
                        break;
                    }
                case "FillPerfisRightLote":
                    {
                        FillPerfisRightLote(context, request, response);
                        break;
                    }
                case "FillPerfisPorFuncionalidade":
                    {
                        FillPerfisPorFuncionalidade(context, request, response);
                        break;
                    }
                case "InsertPerfis":
                    {
                        InsertPerfis(context, request, response);
                        break;
                    }
                case "UpdatePerfis":
                    {
                        UpdatePerfis(context, request);
                        break;
                    }
                case "DeletePerfis":
                    {
                        DeletePerfis(context, request);
                        break;
                    }
                default:
                    break;
            }
        }

        private void FillPerfisPorFuncionalidade(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string idFuncionalidade = request["IdFuncionalidade"];

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

                string output = ObterPerfisPorFuncionalidade(jqSearchData, idFuncionalidade);

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

        private string ObterPerfisPorFuncionalidade(JqSearch jqSearchData, string idFuncionalidade)
        {
            if (String.IsNullOrEmpty(idFuncionalidade)) { return String.Empty; }

            IQueryable<CaPerfilModel> lista;
            List<CaPerfilModel> perfis;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caPerfilModel = new CaPerfilModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaPerfilModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caPerfilModel.ListarAtivosPorFuncionalidade(idFuncionalidade.ToInt()).AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                perfis = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                perfis = caPerfilModel.ListarAtivosPorFuncionalidade(idFuncionalidade.ToInt());

                if (perfis == null)
                {
                    return String.Empty;
                }

                perfis = perfis.OrderBy(orderBy).ToList();
            }

            // perfil administradores na pode aparecer na listagem
            perfis = perfis.FindAll(p => p.IdPerfil > 1);

            int totalRegistros = perfis.Count;

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
                rows = (from perfil in perfis
                        select new[] {
                            perfil.IdPerfil.ToString(),
                            perfil.DsPerfil,
                            ((CaSituacaoPerfilEnum)perfil.StPerfil).GetDescription()
                        }).ToList().GetRange(ini, fim)
            };

            return new JavaScriptSerializer().Serialize(result);
        }

        private void DeletePerfis(HttpContext context, HttpRequest request)
        {
            try
            {
                string id = request["id"].ToString(CultureInfo.InvariantCulture);

                var caPerfilModelLog = new CaPerfilModel() { IdPerfil = id.ToInt() }.Selecionar();

                var caPerfilModel = new CaPerfilModel()
                {
                    IdPerfil = id.ToInt()
                };

                caPerfilModel.Excluir();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoLog, Constants.StdMsgInfoExcluiu, Constants.StdMsgInfoPerfil, caPerfilModelLog != null ? caPerfilModelLog.DsPerfil : id));
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

        private void UpdatePerfis(HttpContext context, HttpRequest request)
        {
            try
            {
                string id = request["id"].ToString(CultureInfo.InvariantCulture);
                string descricao = request["DsPerfil"].ToString();
                string situacao = request["StPerfil"].ToString();

                var caPerfilModel = new CaPerfilModel()
                {
                    DsPerfil = descricao,
                    StPerfil = situacao.ToInt(),
                    IdOperadorAtualizacao = context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt(),
                    DhAtualizacao = DateTime.Now,
                    IdPerfil = id.ToInt()
                };

                caPerfilModel.Atualizar();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoLog, Constants.StdMsgInfoAlterou, Constants.StdMsgInfoPerfil, descricao));
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

        private void InsertPerfis(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string descricao = request["DsPerfil"].ToString().Trim();
                string situacao = request["StPerfil"].ToString();

                var caPerfilModel = new CaPerfilModel()
                {
                    DsPerfil = descricao,
                    StPerfil = situacao.ToInt(),
                    IdOperadorAtualizacao = context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt(),
                    DhAtualizacao = DateTime.Now
                };

                var caPerfilModelList = new CaPerfilModel(){ DsPerfil = descricao }.Listar();

                if (caPerfilModelList == null || caPerfilModelList.Count == 0)
                {
                    caPerfilModel.Inserir();

                    var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoLog, Constants.StdMsgInfoInseriu, Constants.StdMsgInfoPerfil, descricao));

                    response.Write(caPerfilModel.IdPerfil.ToString());
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

        private void FillPerfisLeft(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string idFuncionalidade = request["IdFuncionalidade"];

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

                string output = ObterPerfisLeft(jqSearchData, idFuncionalidade);

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

        private string ObterPerfisLeft(JqSearch jqSearchData, string idFuncionalidade)
        {
            if (String.IsNullOrEmpty(idFuncionalidade)) { return String.Empty; }

            IQueryable<CaPerfilModel> lista;
            List<CaPerfilModel> perfis;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caPerfilModel = new CaPerfilModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaPerfilModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caPerfilModel.ListarAtivosPorFuncionalidade(idFuncionalidade.ToInt()).AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                perfis = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                perfis = caPerfilModel.ListarAtivosPorFuncionalidade(idFuncionalidade.ToInt());

                if (perfis == null)
                {
                    return String.Empty;
                }

                perfis = perfis.OrderBy(orderBy).ToList();
            }

            // perfil administradores na pode aparecer na listagem
            perfis = perfis.FindAll(p => p.IdPerfil > 1);

            int totalRegistros = perfis.Count;

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
                rows = (from perfil in perfis
                        select new[] {
                            perfil.IdPerfil.ToString(),
                            perfil.DsPerfil,
                            ((CaSituacaoPerfilEnum)perfil.StPerfil).GetDescription()
                        }).ToList().GetRange(ini, fim)
            };

            int emptyRows = jqSearchData.rows - fim;

            for (int i = 0; i < emptyRows; i++)
            {
                result.rows.Add(new[] { String.Empty, String.Empty, String.Empty });
            }

            return new JavaScriptSerializer().Serialize(result);
        }
        
        private void FillPerfisRight(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string idFuncionalidade = request["IdFuncionalidade"];

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

                string output = ObterPerfisRight(jqSearchData, idFuncionalidade);

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

        private void FillPerfisRightLote(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string idsFuncionalidade = request["IdFuncionalidade"];

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

                string output = ObterPerfisRightLote(jqSearchData, idsFuncionalidade);

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

        private string ObterPerfisRight(JqSearch jqSearchData, string idFuncionalidade)
        {
            if (String.IsNullOrEmpty(idFuncionalidade)) { return String.Empty; }

            IQueryable<CaPerfilModel> lista;
            List<CaPerfilModel> perfis;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caPerfilModel = new CaPerfilModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaPerfilModel));
                
                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caPerfilModel.NotListarAtivosPorFuncionalidade(idFuncionalidade.ToInt()).AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                perfis = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                perfis = caPerfilModel.NotListarAtivosPorFuncionalidade(idFuncionalidade.ToInt());

                if (perfis == null)
                {
                    return String.Empty;
                }

                perfis = perfis.OrderBy(orderBy).ToList();
            }

            // perfil administradores na pode aparecer na listagem
            perfis = perfis.FindAll(p => p.IdPerfil > 1);

            int totalRegistros = perfis.Count;

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
                rows = (from perfil in perfis
                        select new[] {
                            perfil.IdPerfil.ToString(),
                            perfil.DsPerfil,
                            ((CaSituacaoPerfilEnum)perfil.StPerfil).GetDescription()
                        }).ToList().GetRange(ini, fim)
            };

            int emptyRows = jqSearchData.rows - fim;

            for (int i = 0; i < emptyRows; i++)
            {
                result.rows.Add(new[] { String.Empty, String.Empty, String.Empty });
            }

            return new JavaScriptSerializer().Serialize(result);
        }

        private string ObterPerfisRightLote(JqSearch jqSearchData, string idsFuncionalidade)
        {
            if (String.IsNullOrEmpty(idsFuncionalidade)) { return String.Empty; }

            List<CaPerfilModel> perfis = new List<CaPerfilModel>();

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caPerfilModel = new CaPerfilModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaPerfilModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                foreach (string item in idsFuncionalidade.Split(','))
                {
                    IQueryable<CaPerfilModel> perfisAtivos = caPerfilModel.NotListarAtivosPorFuncionalidade(item.ToInt()).AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);
                    perfis.AddRange(perfisAtivos.ToList());
                }

                if (perfis.Count() == 0)
                {
                    return String.Empty;
                }

                perfis = perfis.GroupBy(p => new { p.IdPerfil, p.DsPerfil }).Select(p => p.First()).OrderBy(orderBy).ToList();
            }
            else
            {
                foreach (string item in idsFuncionalidade.Split(','))
                {
                    List<CaPerfilModel> perfisAtivos = caPerfilModel.NotListarAtivosPorFuncionalidade(item.ToInt());
                    perfis.AddRange(perfisAtivos);
                }

                if (perfis.Count() == 0)
                {
                    return String.Empty;
                }

                perfis = perfis.GroupBy(p => new { p.IdPerfil, p.DsPerfil }).Select(p => p.First()).OrderBy(orderBy).ToList();
            }

            // perfil administradores na pode aparecer na listagem
            perfis = perfis.FindAll(p => p.IdPerfil > 1);

            int totalRegistros = perfis.Count;

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
                rows = (from perfil in perfis
                        select new[] {
                            perfil.IdPerfil.ToString(),
                            perfil.DsPerfil,
                            ((CaSituacaoPerfilEnum)perfil.StPerfil).GetDescription()
                        }).ToList().GetRange(ini, fim)
            };

            int emptyRows = jqSearchData.rows - fim;

            for (int i = 0; i < emptyRows; i++)
            {
                result.rows.Add(new[] { String.Empty, String.Empty, String.Empty });
            }

            return new JavaScriptSerializer().Serialize(result);
        }

        private void FillPerfis(HttpContext context, HttpRequest request, HttpResponse response)
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

                string output = ObterPerfis(jqSearchData);

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

        private string ObterPerfis(JqSearch jqSearchData)
        {
            IQueryable<CaPerfilModel> lista;
            List<CaPerfilModel> perfis;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caPerfilModel = new CaPerfilModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaPerfilModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caPerfilModel.Listar().AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null) { return String.Empty; }

                perfis = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                perfis = caPerfilModel.Listar();

                if (perfis == null)
                {
                    return String.Empty;
                }

                perfis = perfis.OrderBy(orderBy).ToList();
            }

            // perfil administradores na pode aparecer na listagem
            perfis = perfis.FindAll(p => p.IdPerfil > 1);

            int totalRegistros = perfis.Count;

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
                rows = (from perfil in perfis
                        select new[] {
                            perfil.IdPerfil.ToString(),
                            perfil.DsPerfil,
                            ((CaSituacaoPerfilEnum)perfil.StPerfil).GetDescription()
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