using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaPerfilOperadorModelTest : IModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new CaPerfilOperadorModel();
            var lista = model.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarSelecionar()
        {
            var modelo = new CaPerfilOperadorModel();
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var model = new CaPerfilOperadorModel() { IdOperador = lista.First().IdOperador, IdPerfil = lista.First().IdPerfil };
                var item = model.Selecionar();

                Assert.IsNotNull(item);
            }
        }

        [TestMethod]
        public void TestarInserir()
        {
            var modelo = new CaPerfilOperadorModel();

            modelo.IdPerfil = 111;
            modelo.IdOperador = 1;
            modelo.StPerfilOperador = 1;
            modelo.DhSituacao = DateTime.Now;
            modelo.DhAtualizacao = DateTime.Now;
            modelo.IdOperadorAtualizacao = 1;

            modelo.Inserir();

            var model = new CaPerfilOperadorModel() { IdOperador = 1, IdPerfil = 111 };
            var item = model.Selecionar();

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var modelo = new CaPerfilOperadorModel() { IdOperador = 1, IdPerfil = 111 };
            var modelo1 = modelo.Selecionar();

            if (modelo1 != null)
            {
                modelo1.StPerfilOperador = 2;
                modelo1.Atualizar();

                var modelo2 = new CaPerfilOperadorModel() { IdOperador = 1, IdPerfil = 111 };
                var modelo3 = modelo2.Selecionar();

                Assert.AreEqual(modelo3.StPerfilOperador, 2);
            }
        }

        [TestMethod]
        public void TestarExcluir()
        {
            var modelo = new CaPerfilOperadorModel() { IdOperador = 1, IdPerfil = 111 };
            var modelo1 = modelo.Selecionar();

            if (modelo1 != null)
            {
                modelo1.Excluir();

                var modelo2 = new CaPerfilOperadorModel() { IdOperador = 1, IdPerfil = 111 };
                var modelo3 = modelo2.Selecionar();

                Assert.IsNull(modelo3);
            }
        }
        
    }
}
