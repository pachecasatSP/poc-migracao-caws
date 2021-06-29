using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.CA.Site.Enums
{
    [Flags]
    public enum ActionBarEnum
    {
        Inserir = 1,
        Alterar = 2,
        Salvar = 4,
        Cancelar = 8,
        Excluir = 16,
        Imprimir = 32
    }
}