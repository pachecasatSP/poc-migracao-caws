using System;
using System.Collections.Generic;
using System.Text;
using Presenta.CA.Base;
using Presenta.CA.Common;
using System.Data.SqlClient;
using System.Data;
using Presenta.DBConnection;
using Presenta.Utils;
using System.Data.Common;

namespace Presenta.CA.ClassLibrary.Entity
{
    public class OperadorEntity : EntityBase
    {
        public OperadorEntity()
        {
            base.StoredProcedureListName = Constants.SpListarOperador;
            base.StoredProcedureSelectName = Constants.SpSelecionarOperador;
            base.StoredProcedureInsertName = Constants.SpInserirOperador;
            base.StoredProcedureUpdateName = Constants.SpAtualizarOperador;
            base.StoredProcedureDeleteName = Constants.SpExcluirOperador;
        }

        private const string StoredProcedureListFuncionalidades = Constants.SpListarFuncionalidadesPorOperador;
        private const string StoredProcedureListPerfis = Constants.SpListarPerfisPorOperador;

        public int? IdentificadorOperador { get; set; }

        public int IdentificadorTipoSenha { get; set; }

        public string DescricaoTipoSenha { get; set; }

        public int SituacaoOperador { get; set; }

        public string DescricaoSituacaoOperador { get; set; }

        public string CodigoOperador { get; set; }

        public string NomeOperador { get; set; }

        public string DescricaoEmail { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataHoraSituacao { get; set; }

        public string Senha { get; set; }

        public DateTime DataSenha { get; set; }

        public DateTime? DataHoraUltimoLogin { get; set; }

        public int QuantidadeLoginIncorreto { get; set; }

        public DateTime DataHoraAtualizacao { get; set; }

        public int IdentificadorOperadorAtualizacao { get; set; }

        public List<OperadorEntity> List(int? identificadorOperador, string codigoOperador, string nomeOperador, int? situacaoOperador, DbConnection connection)
        {
            try
            {
                var command = DbConnect.CreateCommand(base.StoredProcedureListName, CommandType.StoredProcedure, connection); 
                
                if (identificadorOperador != null)
                {
                    DbConnect.CreateInputParameter("idoperador", identificadorOperador, command);
                }

                if (!String.IsNullOrEmpty(codigoOperador))
                {
                    DbConnect.CreateInputParameter("cdoperador", codigoOperador, command, TextParamTypeEnum.VarChar, 25);
                }

                if (!String.IsNullOrEmpty(nomeOperador))
                {
                    DbConnect.CreateInputParameter("nmoperador", nomeOperador, command, TextParamTypeEnum.VarChar, 45);
                }

                if (situacaoOperador != null)
                {
                    DbConnect.CreateInputParameter("stoperador", situacaoOperador, command);
                }

                var dr = command.ExecuteReader();

                List<OperadorEntity> lista;

                lista = dr.HasRows ? GetFromSqlDataReader(dr) : null;

                if (!dr.IsClosed)
                {
                    dr.Close();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<OperadorEntity> GetFromSqlDataReader(DbDataReader dr)
        {
            var lista = new List<OperadorEntity>();

            while (dr.Read())
            {
                lista.Add(
                    new OperadorEntity()
                    {
                        IdentificadorOperador = Convertion.ToNullableInt(dr["idoperador"]),
                        CodigoOperador = dr["cdoperador"].ToString(),
                        NomeOperador = dr["nmoperador"].ToString(),
                        DescricaoEmail = dr["dsemail"].ToString(),
                        DataCadastro = Convertion.ToDateTime(dr["dtcadastro"].ToString()),
                        Senha = dr["crsenha"].ToString(),
                        DataSenha = Convertion.ToDateTime(dr["dtsenha"]),
                        DataHoraUltimoLogin = Convertion.ToNullableDateTime(dr["dhultimologin"]),
                        QuantidadeLoginIncorreto = Convertion.ToInt(dr["qtloginincorreto"]),
                        SituacaoOperador = Convertion.ToInt(dr["stoperador"]),
                        DescricaoSituacaoOperador = dr["dssituacaooperador"].ToString(),
                        DataHoraSituacao = Convertion.ToDateTime(dr["dhsituacao"]),
                        IdentificadorOperadorAtualizacao = Convertion.ToInt(dr["idoperadoratualizacao"]),
                        DataHoraAtualizacao = Convertion.ToDateTime(dr["dhatualizacao"]),
                        IdentificadorTipoSenha = Convertion.ToInt(dr["idtiposenha"]),
                        DescricaoTipoSenha = dr["dstiposenha"].ToString()
                    });
            }

            return lista;
        }

        public List<int> ObterFuncionalidade(string codigoOperador, DbConnection connection)
        {
            try
            {
                var command = DbConnect.CreateCommand(StoredProcedureListFuncionalidades, CommandType.StoredProcedure, connection);
                
                if (!String.IsNullOrEmpty(codigoOperador))
                {
                    DbConnect.CreateInputParameter("cdoperador", codigoOperador, command, TextParamTypeEnum.VarChar, 25);
                }

                var dr = command.ExecuteReader();

                List<int> lista;

                lista = dr.HasRows ? GetFuncionalidadesFromSqlDataReader(dr) : null;

                if (!dr.IsClosed)
                {
                    dr.Close();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<int> GetFuncionalidadesFromSqlDataReader(DbDataReader dr)
        {
            var lista = new List<int>();

            while (dr.Read())
            {
                lista.Add(Convertion.ToInt(dr["idfuncionalidade"]));
            }

            return lista;
        }

        public List<int> ObterPerfil(string codigoOperador, DbConnection connection)
        {
            try
            {
                var command = DbConnect.CreateCommand(StoredProcedureListPerfis, CommandType.StoredProcedure, connection);

                if (!String.IsNullOrEmpty(codigoOperador))
                {
                    DbConnect.CreateInputParameter("cdoperador", codigoOperador, command, TextParamTypeEnum.VarChar, 25);
                }

                DbConnect.CreateInputParameter("flperfilassociado", 'S', command, TextParamTypeEnum.Char, 1);

                var dr = command.ExecuteReader();

                List<int> lista;

                lista = dr.HasRows ? GetPerfisFromSqlDataReader(dr) : null;

                if (!dr.IsClosed)
                {
                    dr.Close();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<int> GetPerfisFromSqlDataReader(DbDataReader dr)
        {
            var lista = new List<int>();

            while (dr.Read())
            {
                lista.Add(Convertion.ToInt(dr["idperfil"]));
            }

            return lista;
        }
    }
}
