using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaHistoricoSenhaModelTest : IModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new CaHistoricoSenhaModel();
            var lista = model.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarSelecionar()
        {
            var modelo = new CaHistoricoSenhaModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var model = new CaHistoricoSenhaModel() { IdHistoricoSenha = lista.First().IdHistoricoSenha };
                var item = model.Selecionar();

                Assert.IsNotNull(item);
            }
        }

        [TestMethod]
        public void TestarInserir()
        {
            var modelo = new CaHistoricoSenhaModel();

            modelo.IdOperador = 1;
            modelo.DhHistorico = DateTime.Now;
            modelo.CrSenha = "oqwiqoieqoieqowieuqowieu";
            modelo.DhCadastro = DateTime.Now;

            modelo.Inserir();

            Assert.AreNotEqual(modelo.IdHistoricoSenha, 0);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var modelo = new CaHistoricoSenhaModel() { CrSenha = "oqwiqoieqoieqowieuqowieu" };
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo1 = lista.First();
                int idHistoricoSenha = modelo1.IdHistoricoSenha;

                modelo1.CrSenha = "xxxxxxxxxxxxxxxxxxxxxxxxx";
                modelo1.Atualizar();

                var modelo2 = new CaHistoricoSenhaModel() { IdHistoricoSenha = idHistoricoSenha };
                var modelo3 = modelo2.Selecionar();

                Assert.AreEqual(modelo3.CrSenha.Trim(), "xxxxxxxxxxxxxxxxxxxxxxxxx");
            }
        }

        [TestMethod]
        public void TestarExcluir()
        {
            var modelo = new CaHistoricoSenhaModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo2 = lista.Find(p => p.CrSenha.Contains("xxxxxxxxxxxxxxxxxxxxxxxxx"));
                int idHistoricoSenha = modelo2.IdHistoricoSenha;

                modelo2.Excluir();

                var modelo3 = new CaHistoricoSenhaModel() { IdHistoricoSenha = idHistoricoSenha };
                lista = modelo3.Listar();

                Assert.IsTrue(lista == null || lista.Count == 0);
            }
        }
    }
}
