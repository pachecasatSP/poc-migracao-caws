using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaTipoSenhaTipoSenhaValidacaoModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new CaTipoSenhaTipoSenhaValidacaoModel();
            var lista = model.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var modelo = new CaTipoSenhaTipoSenhaValidacaoModel() { IdTipoSenha = 1, IdTipoSenhaValidacao = 1, NuOrdem = 1 };
            var lista = modelo.Listar();

            if (lista != null && lista.Count > 0)
            {
                var modelo1 = lista.First();
                
                modelo1.QtMinCaracteres = 6;
                modelo1.Atualizar();

                var modelo2 = new CaTipoSenhaTipoSenhaValidacaoModel() { IdTipoSenha = 1, IdTipoSenhaValidacao = 1, NuOrdem = 1 };
                var modelo3 = modelo2.Listar();

                Assert.AreEqual(modelo3.First().QtMinCaracteres, 6);
            }
        }
    }
}
