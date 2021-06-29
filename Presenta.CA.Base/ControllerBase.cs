using System;
using System.Collections.Generic;
using System.Text;

namespace Presenta.CA.Base
{
    public abstract class ControllerBase
    {
        /// <summary>
        /// Define se a classe deve disparar uma exceção ou apenas atribuir o conteúdo da exceção à variável correspondente.
        /// </summary>
        public bool ThrowException { get; set; }

        /// <summary>
        /// Propriedade criada para manter a compatibilidade das páginas criadas anteriormente.
        /// </summary>
        public string Erros { get; set; }
    }
}
