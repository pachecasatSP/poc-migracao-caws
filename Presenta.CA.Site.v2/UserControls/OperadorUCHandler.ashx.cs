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
using Presenta.Util.ActiveDirectory;
using System.Data.SqlClient;

namespace Presenta.CA.Site.UserControls
{
    /// <summary>
    /// Summary description for OperadorUCHandler
    /// </summary>
    public class OperadorUCHandler : IHttpHandler, IRequiresSessionState
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
                case "FillOperadores":
                    {
                        FillOperadores(context, request, response);
                        break;
                    }
                case "FillOperadoresLeft":
                    {
                        FillOperadoresLeft(context, request, response);
                        break;
                    }
                case "FillOperadoresRight":
                    {
                        FillOperadoresRight(context, request, response);
                        break;
                    }
                case "InsertOperadores":
                    {
                        InsertOperadores(context, request, response);
                        break;
                    }
                case "UpdateOperadores":
                    {
                        UpdateOperadores(context, request);
                        break;
                    }
                case "DeleteOperadores":
                    {
                        DeleteOperadores(context, request);
                        break;
                    }
                case "ResetOperador":
                    {
                        ResetOperador(context, request);
                        break;
                    }

                default:
                    break;
            }
        }

        private void ResetOperador(HttpContext context, HttpRequest request)
        {
            try
            {
                
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

        private void DeleteOperadores(HttpContext context, HttpRequest request)
        {
            try
            {
                string id = request["id"].ToString(CultureInfo.InvariantCulture);

                if (id.ToInt() == 1)
                    throw new Exception("adm");

                if (id.ToInt() == context.Session[Constants.SessionIdUsuario].ToInt())
                    throw new Exception("logado");

                var caOperadorModelLog = new CaOperadorModel() { IdOperador = id.ToInt() }.Selecionar();

                var caOperadorModel = new CaOperadorModel()
                {
                    IdOperador = id.ToInt()
                };

                caOperadorModel.Excluir();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoLog, Constants.StdMsgInfoExcluiu, Constants.StdMsgInfoOperador, caOperadorModelLog != null ? caOperadorModelLog.CdOperador : id));
            }
            catch (SqlException ex)
            {
                string erro = "";
                string Mensagem = "";
                switch (ex.Errors[0].Number)
                {
                    case 547: // Foreign Key violation
                        {
                            erro = "Não foi possivel excluir o usuário, ";
                            Mensagem = "o usuário está associado a uma ou mais funcionalidades";
                            break;
                        }
                }

                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                context.Response.ContentType = "text/plain";
                context.Response.StatusDescription = Constants.ErrorCodeStatusDescription;
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        erro,
                        Environment.NewLine, Environment.NewLine,
                        Mensagem);
                context.Response.Write(errorMessage);

                try
                {
                    var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }

            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                string erro = "";
                string Mensagem = "";
                switch (ex.Message)
                {
                    case "adm":
                        {
                            erro = "Usuário Administrador, ";
                            Mensagem = "não é possivel excluir o administrador";
                            break;
                        }
                    case "logado":
                        {
                            erro = "Usuário logado, ";
                            Mensagem = "não é possivel excluir o seu próprio operador";
                            break;
                        }
                    default:
                        {
                            erro = ErrorHandler.GetMethodDetail(currentMethod);
                            Mensagem = ex.ToMessageAndCompleteStacktrace();
                            break;
                        }
                }

                context.Response.ContentType = "text/plain";
                context.Response.StatusDescription = Constants.ErrorCodeStatusDescription;
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        erro,
                        Environment.NewLine, Environment.NewLine,
                        Mensagem);
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

        private void UpdateOperadores(HttpContext context, HttpRequest request)
        {
            try
            {
                string id = request["id"].ToString(CultureInfo.InvariantCulture);
                string cdOperador = request["CdOperador"].ToString();
                string nmOperador = request["NmOperador"].ToString();
                string dsEmail = request["DsEmail"].ToString();
                string stOperador = request["StOperador"].ToString();

                var caOperadorModel = new CaOperadorModel()
                {
                    DhAtualizacao = DateTime.Now,
                    CdOperador = cdOperador.Trim(),
                    NmOperador = nmOperador.Trim(),
                    DsEmail = dsEmail.Trim(),
                    StOperador = stOperador.ToInt(),
                    CrSenha = new CaConfiguracaoModel() { IdConfiguracao = 1 }.Selecionar().SenhaPadrao,
                    DhSituacao = DateTime.Now,
                    DhUltimoLogin = null,
                    DtCadastro = DateTime.Now,
                    DtSenha = DateTime.Now,
                    IdOperadorAtualizacao = context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt(),
                    IdTipoSenha = new CaTipoSenhaModel() { IdTipoSenha = 1 }.Selecionar().IdTipoSenha,
                    QtLoginIncorreto = 0,
                    IdOperador = id.ToInt()
                };

                caOperadorModel.Atualizar();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoLog, Constants.StdMsgInfoAlterou, Constants.StdMsgInfoOperador, cdOperador));
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

        private void InsertOperadores(HttpContext context, HttpRequest request, HttpResponse response)
        {
            try
            {
                string cdOperador = request["CdOperador"].ToString();
                string nmOperador = request["NmOperador"].ToString();
                string dsEmail = request["DsEmail"].ToString();
                string stOperador = request["StOperador"].ToString();

                var caOperadorModel = new CaOperadorModel()
                {
                    DhAtualizacao = DateTime.Now,
                    CdOperador = cdOperador.Trim(),
                    NmOperador = nmOperador.Trim(),
                    DsEmail = dsEmail.Trim(),
                    StOperador = stOperador.ToInt(),
                    DhSituacao = DateTime.Now,
                    DhUltimoLogin = null,
                    DtCadastro = DateTime.Now,
                    DtSenha = DateTime.Now,
                    IdOperadorAtualizacao = context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt(),
                    IdTipoSenha = new CaTipoSenhaModel() { IdTipoSenha = 1 }.Selecionar().IdTipoSenha,
                    QtLoginIncorreto = 0
                };

                var configuracao = new CaConfiguracaoModel() { IdConfiguracao = 1 }.Selecionar();

                //caOperadorModel.CrSenha = configuracao.SenhaPadrao;
                caOperadorModel.CrSenha = null;
                caOperadorModel.CdGuid = null;// UserInfo.GetSID(caOperadorModel.CdOperador);

                caOperadorModel.Inserir();

                var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                caLog.LogarInfo(idAplicativo, String.Format(Constants.StdMsgInfoLog, Constants.StdMsgInfoInseriu, Constants.StdMsgInfoOperador, cdOperador));

                response.Write(caOperadorModel.IdOperador.ToString());
            }
            catch (SqlException ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                context.Response.ContentType = "text/plain";
                context.Response.StatusDescription = Constants.ErrorCodeStatusDescription; //"Usuário Existente";
                string errorMessage =
                    String.Format(
                        "{0}{1}{2}{3}",
                        "Usuário Existente",
                        Environment.NewLine, Environment.NewLine,
                        "Por favor insira um login Inexistente");
                context.Response.Write(errorMessage);

                try
                {
                    var caLog = new CaLogModel(context.Session[Constants.SessionIdUsuario].ToInt() == 0 ? 1 : context.Session[Constants.SessionIdUsuario].ToInt());
                    int idAplicativo = Config.GetKeyValue(Constants.KeyIdAplicativo).ToInt();
                    caLog.LogarErro(idAplicativo, errorMessage);
                }
                catch (Exception) { }
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

        private void FillOperadoresRight(HttpContext context, HttpRequest request, HttpResponse response)
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

                string output = ObterOperadoresRight(jqSearchData, idPerfil);

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

        private string ObterOperadoresRight(JqSearch jqSearchData, string idPerfil)
        {
            if (String.IsNullOrEmpty(idPerfil)) { return String.Empty; }

            IQueryable<CaOperadorModel> lista;
            List<CaOperadorModel> operadores;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caPerfilModel = new CaOperadorModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaOperadorModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caPerfilModel.NotListarAtivosPorPerfil(idPerfil.ToInt()).AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                operadores = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                operadores = caPerfilModel.NotListarAtivosPorPerfil(idPerfil.ToInt());

                if (operadores == null)
                {
                    return String.Empty;
                }

                operadores = operadores.OrderBy(orderBy).ToList();
            }

            operadores = operadores.FindAll(p => p.IdOperador > 1 && p.StOperador != 3);

            int totalRegistros = operadores.Count;

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
                rows = (from operador in operadores
                        select new[] {
                            operador.IdOperador.ToString(),
                            operador.CdOperador,
                            operador.NmOperador
                        }).ToList().GetRange(ini, fim)
            };

            int emptyRows = jqSearchData.rows - fim;

            for (int i = 0; i < emptyRows; i++)
            {
                result.rows.Add(new[] { String.Empty, String.Empty, String.Empty });
            }

            return new JavaScriptSerializer().Serialize(result);
        }

        private void FillOperadoresLeft(HttpContext context, HttpRequest request, HttpResponse response)
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

                string output = ObterOperadoresLeft(jqSearchData, idPerfil);

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

        private string ObterOperadoresLeft(JqSearch jqSearchData, string idPerfil)
        {
            if (String.IsNullOrEmpty(idPerfil)) { return String.Empty; }

            IQueryable<CaOperadorModel> lista;
            List<CaOperadorModel> operadores;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caPerfilModel = new CaOperadorModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaOperadorModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caPerfilModel.ListarAtivosPorPerfil(idPerfil.ToInt()).AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                operadores = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                operadores = caPerfilModel.ListarAtivosPorPerfil(idPerfil.ToInt());

                if (operadores == null)
                {
                    return String.Empty;
                }

                operadores = operadores.OrderBy(orderBy).ToList();
            }

            //operadores = operadores.FindAll(p => p.IdOperador > 1 && p.StOperador != 3);

            int totalRegistros = operadores.Count;

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
                rows = (from operador in operadores
                        select new[] {
                            operador.IdOperador.ToString(),
                            operador.CdOperador,
                            operador.NmOperador
                        }).ToList().GetRange(ini, fim)
            };

            int emptyRows = jqSearchData.rows - fim;

            for (int i = 0; i < emptyRows; i++)
            {
                result.rows.Add(new[] { String.Empty, String.Empty, String.Empty });
            }

            return new JavaScriptSerializer().Serialize(result);
        }

        private void FillOperadores(HttpContext context, HttpRequest request, HttpResponse response)
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

                bool apenasAtivos = request["ApenasAtivos"].ToString() == "1";

                string output = ObterOperadores(jqSearchData, apenasAtivos);

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

        private string ObterOperadores(JqSearch jqSearchData, bool apenasAtivos)
        {
            IQueryable<CaOperadorModel> lista;
            List<CaOperadorModel> operadores;

            string orderBy = String.Format("{0} {1}", jqSearchData.sidx, jqSearchData.sord.ToUpper());

            var caOperadorModel = new CaOperadorModel();

            if (jqSearchData._search && !String.IsNullOrEmpty(jqSearchData.filters))
            {
                var whereClause = jqSearchData.GenerateWhereClause(typeof(CaOperadorModel));

                if (String.IsNullOrEmpty(whereClause.Clause)) { return String.Empty; }

                lista = caOperadorModel.Listar().AsQueryable().Where(whereClause.Clause, whereClause.FormatObjects);

                if (lista == null)
                {
                    return String.Empty;
                }

                operadores = lista.ToList().OrderBy(orderBy).ToList();
            }
            else
            {
                operadores = caOperadorModel.Listar();

                if (operadores == null)
                {
                    return String.Empty;
                }

                operadores = operadores.OrderBy(orderBy).ToList();
            }

            // operadores = operadores.FindAll(p => p.IdOperador > 1 && p.StOperador != 3);
            // usuario administrador nao pode aparecer na listagem
            operadores = operadores.FindAll(p => p.IdOperador > 1);

            if (apenasAtivos)
                operadores = operadores.FindAll(p => p.StOperador == 1);

            int totalRegistros = operadores.Count;

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
                rows = (from operador in operadores
                        select new[] {
                            operador.IdOperador.ToString(),
                            operador.CdOperador,
                            operador.NmOperador, 
                            String.IsNullOrEmpty(operador.DsEmail) ? String.Empty : operador.DsEmail, 
                            ((CaSituacaoOperadorEnum)operador.StOperador).GetDescription()
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