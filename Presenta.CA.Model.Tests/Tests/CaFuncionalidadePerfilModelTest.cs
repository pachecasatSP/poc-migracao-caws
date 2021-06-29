using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaFuncionalidadePerfilModelTest : IModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new CaFuncionalidadePerfilModel();
            var lista = model.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarSelecionar()
        {
            var modelo = new CaFuncionalidadePerfilModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var model = new CaFuncionalidadePerfilModel() { IdFuncionalidade = lista.First().IdFuncionalidade, IdPerfil = lista.First().IdPerfil };
                var item = model.Selecionar();

                Assert.IsNotNull(item);
            }
        }

        [TestMethod]
        public void TestarInserir()
        {
            var modelo = new CaFuncionalidadePerfilModel();

            modelo.IdPerfil = 1;
            modelo.IdFuncionalidade = 1000;
            modelo.StFuncionalidadePerfil = 1;
            modelo.DhSituacao = DateTime.Now;
            modelo.DhAtualizacao = DateTime.Now;
            modelo.IdOperadorAtualizacao = 1;

            modelo.Inserir();

            var model = new CaFuncionalidadePerfilModel() { IdFuncionalidade = 1000, IdPerfil = 1 };
            var item = model.Selecionar();

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var modelo = new CaFuncionalidadePerfilModel() { IdFuncionalidade = 1000, IdPerfil = 1 };
            var modelo1 = modelo.Selecionar();

            if (modelo1 != null)
            {
                modelo1.StFuncionalidadePerfil = 2;
                modelo1.Atualizar();

                var modelo2 = new CaFuncionalidadePerfilModel() { IdFuncionalidade = 1000, IdPerfil = 1 };
                var modelo3 = modelo2.Selecionar();

                Assert.AreEqual(modelo3.StFuncionalidadePerfil, 2);
            }
        }

        [TestMethod]
        public void TestarExcluir()
        {
            var modelo = new CaFuncionalidadePerfilModel() { IdFuncionalidade = 1000, IdPerfil = 1 };
            var modelo1 = modelo.Selecionar();

            if (modelo1 != null)
            {
                modelo1.Excluir();

                var modelo2 = new CaFuncionalidadePerfilModel() { IdFuncionalidade = 1000, IdPerfil = 1 };
                var modelo3 = modelo2.Selecionar();

                Assert.IsNull(modelo3);
            }
        }
    }
}
