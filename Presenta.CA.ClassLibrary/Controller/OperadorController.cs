using System;
using System.Collections.Generic;
using System.Text;
using Presenta.CA.Base;
using Presenta.CA.ClassLibrary.Entity;
using Presenta.DBConnection;
using Presenta.CA.Common;
using Presenta.CA.ClassLibrary.Common;
using Presenta.Utils;

namespace Presenta.CA.ClassLibrary.Controller
{
    public class OperadorController : ControllerBase
    {
        public OperadorController(bool throwException)
        {
            base.ThrowException = throwException;
        }

        public List<OperadorEntity> List(int? identificadorOperador, string codigoOperador, string nomeOperador, int? situacaoOperador)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var operadorEntity = new OperadorEntity();

                    return operadorEntity.List(identificadorOperador, codigoOperador, nomeOperador, situacaoOperador, connection);
                }
                catch (Exception ex)
                {
                    if (ThrowException)
                    {
                        throw ex;
                    }
                    else
                    {
                        Erros = ErrorHandler.GetError(ex);
                        return null;
                    }
                }
                finally
                {
                    DbConnect.CloseConnection(connection);
                }
            }
        }

        public List<int> ObterFuncionalidade(string codigoOperador)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var operadorEntity = new OperadorEntity();

                    return operadorEntity.ObterFuncionalidade(codigoOperador, connection);
                }
                catch (Exception ex)
                {
                    if (ThrowException)
                    {
                        throw ex;
                    }
                    else
                    {
                        Erros = ErrorHandler.GetError(ex);
                        return null;
                    }
                }
                finally
                {
                    DbConnect.CloseConnection(connection);
                }
            }
        }

        public List<int> ObterPerfil(string codigoOperador)
        {
            using (var connection = DbConnect.CreateConnection(Config.GetConnectionString(Constants.KeyConnectionStringName), Config.GetProviderName(Constants.KeyConnectionStringName)))
            {
                DbConnect.OpenConnection(connection);

                try
                {
                    var operadorEntity = new OperadorEntity();

                    return operadorEntity.ObterPerfil(codigoOperador, connection);
                }
                catch (Exception ex)
                {
                    if (ThrowException)
                    {
                        throw ex;
                    }
                    else
                    {
                        Erros = ErrorHandler.GetError(ex);
                        return null;
                    }
                }
                finally
                {
                    DbConnect.CloseConnection(connection);
                }
            }
        }
    }
}
