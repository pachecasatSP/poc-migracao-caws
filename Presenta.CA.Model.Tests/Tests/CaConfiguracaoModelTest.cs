using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaConfiguracaoModelTest
    {
        [TestMethod]
        public void TestarSelecionar()
        {
            var caConfiguracao = new CaConfiguracaoModel() { IdConfiguracao = 1 };
            var item = caConfiguracao.Selecionar();

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            var caConfiguracao = new CaConfiguracaoModel() { IdConfiguracao = 1 };
            var configuracao = caConfiguracao.Selecionar();

            if (configuracao != null)
            {
                int? novaValor = configuracao.DiasDesativSenha = configuracao.DiasDesativSenha + 1;
                configuracao.Atualizar();

                var caConfiguracao2 = new CaConfiguracaoModel() { IdConfiguracao = 1 };
                var configuracao2 = caConfiguracao2.Selecionar();

                Assert.AreEqual(configuracao2.DiasDesativSenha, novaValor);
            }
        }
    }
}
