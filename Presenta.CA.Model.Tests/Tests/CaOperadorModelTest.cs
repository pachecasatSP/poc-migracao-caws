using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Presenta.CA.Model.Enum;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaOperadorModelTest : IModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var caOperador = new CaOperadorModel();
            var lista = caOperador.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarListarAtivosPorPerfil()
        {
            var caOperador = new CaOperadorModel();
            var lista = caOperador.ListarAtivosPorPerfil(1);

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarSelecionar()
        {
            var caOperador = new CaOperadorModel() { IdOperador = 1 };
            var item = caOperador.Selecionar();

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void TestarInserir()
        {
            var caOperador = new CaOperadorModel();

            caOperador.IdTipoSenha = 1;
            caOperador.StOperador = 1;
            caOperador.CdOperador = "ins-unit-test";
            caOperador.NmOperador = "Insert Unit Test";
            caOperador.DsEmail = "ins@unittest.com";
            caOperador.DtCadastro = DateTime.Now;
            caOperador.DhSituacao = DateTime.Now;
            caOperador.CrSenha = "r/sjal2z+0/xrXopblBoiiHkTw88TfFc";
            caOperador.DtSenha = DateTime.Now;
            caOperador.DhUltimoLogin = null;
            caOperador.QtLoginIncorreto = 0;
            caOperador.DhAtualizacao = DateTime.Now;
            caOperador.IdOperadorAtualizacao = 1;
            caOperador.CdGuid = null;

            caOperador.Inserir();

            Assert.AreNotEqual(caOperador.IdOperador, 0);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var caOperador = new CaOperadorModel() { CdOperador = "ins-unit-test" };
            var lista = caOperador.Listar();

            if (lista != null && lista.Count > 0)
            {
                var operador = lista.First();
                int idOperador = operador.IdOperador;

                operador.NmOperador = "UPDATED -> Insert Unit Test";
                operador.Atualizar();

                var caOperador2 = new CaOperadorModel() { IdOperador = idOperador };
                var operador2 = caOperador2.Selecionar();

                Assert.AreEqual(operador2.NmOperador.Trim(), "UPDATED -> Insert Unit Test");
            }
        }

        [TestMethod]
        public void TestarResetarSenha()
        {
            var caOperador = new CaOperadorModel() { CdOperador = "ins-unit-test" };
            var lista = caOperador.Listar();

            if (lista != null && lista.Count > 0)
            {
                var operador = lista.First();
                int idOperador = operador.IdOperador;

                operador.StOperador = (int)CaSituacaoOperadorEnum.Ativo;
                operador.DtSenha = DateTime.Now;
                operador.QtLoginIncorreto = 0;
                operador.DhAtualizacao = DateTime.Now;
                operador.IdOperadorAtualizacao = 1;

                operador.ResetarSenha();

                var caOperador2 = new CaOperadorModel() { IdOperador = idOperador };
                var operador2 = caOperador2.Selecionar();

                Assert.IsTrue(String.IsNullOrEmpty(operador2.CrSenha));
            }
        }

        [TestMethod]
        public void TestarExcluir()
        {
            var caOperador = new CaOperadorModel() { CdOperador = "ins-unit-test" };
            var lista = caOperador.Listar();

            if (lista != null && lista.Count > 0)
            {
                var operador = lista.First();
                int idOperador = operador.IdOperador;

                operador.Excluir();

                var caOperador2 = new CaOperadorModel() { IdOperador = idOperador };
                var operador2 = caOperador2.Selecionar();

                Assert.AreEqual(operador2.StOperador, 3); // Inativo
            }
        }
    }
}
