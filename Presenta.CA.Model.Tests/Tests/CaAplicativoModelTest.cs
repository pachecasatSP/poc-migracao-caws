using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaAplicativoModelTest : IModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var caOperador = new CaAplicativoModel();
            var lista = caOperador.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarSelecionar()
        {
            var caOperador = new CaAplicativoModel() { IdAplicativo = 5 };
            var item = caOperador.Selecionar();

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void TestarInserir()
        {
            var caAplicativo = new CaAplicativoModel();

            caAplicativo.IdSistema = 5;
            caAplicativo.IdOperadorAtualizacao = 1;
            caAplicativo.DsAplicativo = "Auto Unit Test";
            caAplicativo.DhAtualizacao = DateTime.Now;

            caAplicativo.Inserir();

            Assert.AreNotEqual(caAplicativo.IdAplicativo, 0);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var caAplicativo = new CaAplicativoModel() { DsAplicativo = "Auto Unit Test" };
            var lista = caAplicativo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var aplicativo = lista.First();
                int idAplicativo = aplicativo.IdAplicativo;

                aplicativo.DsAplicativo = "UPDATED -> Auto Unit Test";
                aplicativo.Atualizar();

                var caAplicativo2 = new CaAplicativoModel() { IdAplicativo = idAplicativo };
                var aplicativo2 = caAplicativo2.Selecionar();

                Assert.AreEqual(aplicativo2.DsAplicativo.Trim(), "UPDATED -> Auto Unit Test");
            }
        }

        [TestMethod]
        public void TestarExcluir()
        {
            var caAplicativo = new CaAplicativoModel() { DsAplicativo = "UPDATED -> Auto Unit Test" };
            var lista = caAplicativo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var aplicativo = lista.First();
                int idAplicativo = aplicativo.IdAplicativo;

                aplicativo.Excluir();

                var aplicativo2 = new CaAplicativoModel() { IdAplicativo = idAplicativo };
                lista = aplicativo2.Listar();

                Assert.IsTrue(lista == null || lista.Count == 0);
            }
        }
    }
}
