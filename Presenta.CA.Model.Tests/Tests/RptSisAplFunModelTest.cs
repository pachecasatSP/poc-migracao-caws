using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class RptSisAplFunModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new RptSisAplFunModel();
            var lista = model.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }
    }
}
