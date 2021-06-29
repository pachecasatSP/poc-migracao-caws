using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.CA.Model
{
    public class CAException : Exception
    {
        public string Mensagem { get; set; }

        public CAException(string _mensagem)
        {
            Mensagem = _mensagem;
        }
    }
}