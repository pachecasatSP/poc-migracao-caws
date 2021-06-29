using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaPerfilModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new CaPerfilModel();
            var lista = model.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarListarAtivosPorFuncionalidade()
        {
            var model = new CaPerfilModel();
            var lista = model.ListarAtivosPorFuncionalidade(95);

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarSelecionar()
        {
            var modelo = new CaPerfilModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var model = new CaPerfilModel() { IdPerfil = lista.First().IdPerfil };
                var item = model.Selecionar();

                Assert.IsNotNull(item);
            }
        }

        [TestMethod]
        public void TestarInserir()
        {
            var modelo = new CaPerfilModel();

            modelo.IdOperadorAtualizacao = 1;
            modelo.StPerfil = 1;
            modelo.DsPerfil = "INSERT - Auto Unit Test";
            modelo.DhAtualizacao = DateTime.Now;

            modelo.Inserir();

            Assert.AreNotEqual(modelo.IdPerfil, 0);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var modelo = new CaPerfilModel() { DsPerfil = "INSERT - Auto Unit Test" };
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo1 = lista.First();
                int idPerfil = modelo1.IdPerfil;

                modelo1.DsPerfil = String.Format("UPDATED -> Auto Unit Test - {0}", idPerfil);
                modelo1.Atualizar();

                var modelo2 = new CaPerfilModel() { IdPerfil = idPerfil };
                var modelo3 = modelo2.Selecionar();

                Assert.AreEqual(modelo3.DsPerfil.Trim(), String.Format("UPDATED -> Auto Unit Test - {0}", idPerfil));
            }
        }

        [TestMethod]
        public void TestarExcluir()
        {
            var modelo = new CaPerfilModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo2 = lista.Find(p => p.DsPerfil.Contains("UPDATED -> Auto Unit Test"));
                int idFuncionalidade = modelo2.IdPerfil;

                modelo2.Excluir();

                var modelo3 = new CaPerfilModel() { IdPerfil = idFuncionalidade };
                lista = modelo3.Listar();

                Assert.IsTrue(lista == null || lista.Count == 0);
            }
        }
    }
}
