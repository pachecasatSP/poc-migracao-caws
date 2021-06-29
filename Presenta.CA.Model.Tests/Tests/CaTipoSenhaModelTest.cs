using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaTipoSenhaModelTest
    {
        [TestMethod]
        public void TestarSelecionar()
        {
            var model = new CaTipoSenhaModel() { IdTipoSenha = 1 };
            var item = model.Selecionar();

            Assert.IsNotNull(item);
        }
    }
}
