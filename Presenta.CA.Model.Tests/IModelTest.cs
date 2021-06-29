using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenta.CA.Model.Tests
{
    public interface IModelTest
    {
        void TestarListar();
        void TestarSelecionar();
        void TestarInserir();
        void TestarAtualizar();
        void TestarExcluir();
    }
}
