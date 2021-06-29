using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Presenta.CA.Model.Enum
{
    public enum CaTipoLogEnum : int
    {
        [Description("Informação")]
        Info = 1,
        Erro = 2
    }
}
