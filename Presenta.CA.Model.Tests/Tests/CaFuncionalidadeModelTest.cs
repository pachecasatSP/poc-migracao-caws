using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaFuncionalidadeModelTest : IModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new CaFuncionalidadeModel();
            var lista = model.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarListarPorPerfil()
        {
            var model = new CaFuncionalidadeModel();
            var lista = model.ListarPorPerfil(0, 0, 0);

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarSelecionar()
        {
            var modelo = new CaFuncionalidadeModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var model = new CaFuncionalidadeModel() { IdFuncionalidade = lista.First().IdFuncionalidade };
                var item = model.Selecionar();

                Assert.IsNotNull(item);
            }
        }

        [TestMethod]
        public void TestarInserir()
        {
            var modelo = new CaFuncionalidadeModel();

            modelo.IdAplicativo = 5;
            modelo.IdOperadorAtualizacao = 1;
            modelo.StFuncionalidade = 1;
            modelo.DsFuncionalidade = "INSERT - Auto Unit Test";
            modelo.DhAtualizacao = DateTime.Now;

            modelo.Inserir();

            Assert.AreNotEqual(modelo.IdFuncionalidade, 0);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var modelo = new CaFuncionalidadeModel() { DsFuncionalidade = "INSERT - Auto Unit Test" };
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo1 = lista.First();
                int idFuncionalidade = modelo1.IdFuncionalidade;

                modelo1.DsFuncionalidade = String.Format("UPDATED -> Auto Unit Test - {0}", idFuncionalidade);
                modelo1.Atualizar();

                var modelo2 = new CaFuncionalidadeModel() { IdFuncionalidade = idFuncionalidade };
                var modelo3 = modelo2.Selecionar();

                Assert.AreEqual(modelo3.DsFuncionalidade.Trim(), String.Format("UPDATED -> Auto Unit Test - {0}", idFuncionalidade));
            }
        }

        [TestMethod]
        public void TestarExcluir()
        {
            var modelo = new CaFuncionalidadeModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo2 = lista.Find(p => p.DsFuncionalidade.Contains("UPDATED -> Auto Unit Test"));
                int idFuncionalidade = modelo2.IdFuncionalidade;

                modelo2.Excluir();

                var modelo3 = new CaFuncionalidadeModel() { IdFuncionalidade = idFuncionalidade };
                lista = modelo3.Listar();

                Assert.IsTrue(lista == null || lista.Count == 0);
            }
        }
    }
}
