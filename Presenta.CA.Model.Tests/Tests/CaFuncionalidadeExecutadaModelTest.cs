using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaFuncionalidadeExecutadaModelTest : IModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new CaFuncionalidadeExecutadaModel();
            var lista = model.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarSelecionar()
        {
            var modelo = new CaFuncionalidadeExecutadaModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var model = new CaFuncionalidadeExecutadaModel() { IdCaFuncionalidadeExecutada = lista.First().IdCaFuncionalidadeExecutada };
                var item = model.Selecionar();

                Assert.IsNotNull(item);
            }
        }

        [TestMethod]
        public void TestarInserir()
        {
            var modelo = new CaFuncionalidadeExecutadaModel();

            modelo.IdFuncionalidade = 1;
            modelo.IdOperadorAtualizacao = 1;
            modelo.DsComplemento = "INSERT - Auto Unit Test";
            modelo.DhExecucao = DateTime.Now;

            modelo.Inserir();

            Assert.AreNotEqual(modelo.IdCaFuncionalidadeExecutada, 0);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var modelo = new CaFuncionalidadeExecutadaModel() { DsComplemento = "INSERT - Auto Unit Test" };
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo1 = lista.First();
                int idCaFuncionalidadeExecutada = modelo1.IdCaFuncionalidadeExecutada;

                modelo1.DsComplemento = String.Format("UPDATED -> Auto Unit Test - {0}", idCaFuncionalidadeExecutada);
                modelo1.Atualizar();

                var modelo2 = new CaFuncionalidadeExecutadaModel() { IdCaFuncionalidadeExecutada = idCaFuncionalidadeExecutada };
                var modelo3 = modelo2.Selecionar();

                Assert.AreEqual(modelo3.DsComplemento.Trim(), String.Format("UPDATED -> Auto Unit Test - {0}", idCaFuncionalidadeExecutada));
            }
        }

        [TestMethod]
        public void TestarExcluir()
        {
            var modelo = new CaFuncionalidadeExecutadaModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo2 = lista.First();
                int idCaFuncionalidadeExecutada = modelo2.IdCaFuncionalidadeExecutada;

                modelo2.Excluir();

                var modelo3 = new CaFuncionalidadeExecutadaModel() { IdCaFuncionalidadeExecutada = idCaFuncionalidadeExecutada };
                lista = modelo3.Listar();

                Assert.IsTrue(lista == null || lista.Count == 0);
            }
        }
    }
}
