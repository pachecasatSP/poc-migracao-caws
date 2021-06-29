using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class RptLogModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var de = new DateTime(2015, 8, 24);
            var ate = new DateTime(2015, 8, 26);

            var model = new RptLogModel();
            var lista = model.Listar(de, ate, null, null);

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }
    }
}
