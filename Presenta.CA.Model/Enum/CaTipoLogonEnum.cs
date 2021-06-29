using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Presenta.CA.Model.Enum
{
    public enum CaTipoLogonEnum
    {
        [Description("Autenticação do Windows")]
        Windows = 1,
        [Description("Usuário e Senha")]
        Senha = 2
    }
}
