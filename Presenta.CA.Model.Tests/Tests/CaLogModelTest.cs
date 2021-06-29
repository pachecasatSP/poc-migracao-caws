using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Presenta.CA.Model.Tests
{
    [TestClass]
    public class CaLogModelTest
    {
        [TestMethod]
        public void TestarListar()
        {
            var model = new CaLogModel();
            var lista = model.Listar();

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void TestarInserirInfo()
        {
            var modelo = new CaLogModel(1);

            modelo.LogarInfo(5, "Auto Unit Tests");

            Assert.AreNotEqual(modelo.IdLog, 0);
            Assert.AreEqual(modelo.IdTipoLog, 1);
        }

        [TestMethod]
        public void TestarInserirErroMensagemMenorQue100()
        {
            var modelo = new CaLogModel(1);

            modelo.LogarErro(5, "Auto Unit Tests");

            Assert.AreNotEqual(modelo.IdLog, 0);
            Assert.AreEqual(modelo.IdTipoLog, 2);
        }

        [TestMethod]
        public void TestarInserirErroMensagemMaiorQue100()
        {
            var modelo = new CaLogModel(1);

            modelo.LogarErro(5, "Auto Unit Tests @ Auto Unit Tests @ Auto Unit Tests @ Auto Unit Tests @ Auto Unit Tests @ Auto Unit Tests @ Auto Unit Tests @ Auto Unit Tests");

            Assert.AreNotEqual(modelo.IdLog, 0);
            Assert.AreEqual(modelo.IdTipoLog, 2);
        }
    }
}
