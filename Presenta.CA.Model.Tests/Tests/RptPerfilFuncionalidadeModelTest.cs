using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class RptPerfilFuncionalidadeModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new RptPerfilFuncionalidadeModel();
            var lista = model.Listar(null, null);

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }
    }
}
