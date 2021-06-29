using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Presenta.Security.Encryption;
using Presenta.CA.ClassLibrary.Controller;

namespace Presenta.WS.CA
{
    /// <summary>
    /// Summary description for UsuarioService
    /// </summary>
    [WebService(Namespace = "http://presenta.com.br/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class UsuarioService : System.Web.Services.WebService
    {
        [WebMethod]
        public bool Autenticar(string codigoOperador, string senhaOperador)
        {
            try
            {
                var operadorController = new OperadorController(false);

                var operadores = operadorController.List(null, codigoOperador.ToLower(), null, null);

                if (operadores.Count == 0)
                {
                    throw new Exception("Operador não cadastrado.");
                }

                if (operadores.Count > 1)
                {
                    throw new Exception("Existe mais de um operador cadastrado com o mesmo login.");
                }

                if (senhaOperador.Trim() == Cryptographer.Decrypt(operadores[0].Senha.Trim()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                // TODO Logging exception

                return false;
            }
        }

        [WebMethod]
        public int[] Autorizar(string codigoOperador, string senhaOperador)
        {
            try
            {
                var operadorController = new OperadorController(false);

                if (Autenticar(codigoOperador, senhaOperador))
                {
                    var lista = operadorController.ObterFuncionalidade(codigoOperador);

                    return lista == null ? new int[0] : lista.ToArray();
                }

                return null;
            }
            catch (Exception ex)
            {
                // TODO Logging exception

                throw ex;
            }
        }

        [WebMethod]
        public string[] ObterInfo(string codigoOperador, string senhaOperador)
        {
            try
            {
                var operadorController = new OperadorController(false);

                if (Autenticar(codigoOperador, senhaOperador))
                {
                    var lista = operadorController.List(null, codigoOperador, null, null);

                    if (lista != null && lista.Count == 1)
                    {
                        var dados = new List<string>();
                        dados.Add(lista[0].IdentificadorOperador.ToString());
                        dados.Add(lista[0].CodigoOperador.ToString());
                        dados.Add(lista[0].NomeOperador.ToString());
                        dados.Add(lista[0].Senha.ToString());

                        return dados.ToArray();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                // TODO Logging exception

                throw ex;
            }
        }

        [WebMethod]
        public int[] ObterPerfil(string codigoOperador, string senhaOperador)
        {
            try
            {
                var operadorController = new OperadorController(false);

                if (Autenticar(codigoOperador, senhaOperador))
                {
                    var lista = operadorController.ObterPerfil(codigoOperador);

                    if (lista != null && lista.Count == 1)
                    {
                        return lista.ToArray();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                // TODO Logging exception

                throw ex;
            }
        }
    }
}
