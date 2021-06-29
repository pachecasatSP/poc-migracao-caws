using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaSistemaModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new CaSistemaModel();
            var lista = model.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarListarPorPerfil()
        {
            var model = new CaSistemaModel();
            var lista = model.ListarPorPerfil(1);

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarSelecionar()
        {
            var modelo = new CaSistemaModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var model = new CaSistemaModel() { IdSistema = lista.First().IdSistema };
                var item = model.Selecionar();

                Assert.IsNotNull(item);
            }
        }

        [TestMethod]
        public void TestarInserir()
        {
            var modelo = new CaSistemaModel();

            modelo.IdOperadorAtualizacao = 1;
            modelo.DsSistema = "INSERT - Auto Unit Test";
            modelo.DhAtualizacao = DateTime.Now;

            modelo.Inserir();

            Assert.AreNotEqual(modelo.IdSistema, 0);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var modelo = new CaSistemaModel() { DsSistema = "INSERT - Auto Unit Test" };
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo1 = lista.First();
                int idSistema = modelo1.IdSistema;

                modelo1.DsSistema = String.Format("UPDATED -> Auto Unit Test - {0}", idSistema);
                modelo1.Atualizar();

                var modelo2 = new CaSistemaModel() { IdSistema = idSistema };
                var modelo3 = modelo2.Selecionar();

                Assert.AreEqual(modelo3.DsSistema.Trim(), String.Format("UPDATED -> Auto Unit Test - {0}", idSistema));
            }
        }

        [TestMethod]
        public void TestarExcluir()
        {
            var modelo = new CaSistemaModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo2 = lista.Find(p => p.DsSistema.Contains("UPDATED -> Auto Unit Test"));
                int idSistema = modelo2.IdSistema;

                modelo2.Excluir();

                var modelo3 = new CaSistemaModel() { IdSistema = idSistema };
                lista = modelo3.Listar();

                Assert.IsTrue(lista == null || lista.Count == 0);
            }
        }
    }
}
