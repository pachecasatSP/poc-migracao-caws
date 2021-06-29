
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.CA.Model
{
    public class TokenException: Exception
    {
        public int _codigo { get; set; }
       
        public TokenException(ExcecaoWSEnum excecaoWS) :
            base(String.Format("{0}", excecaoWS.GetTextDescription()))
        {
            _codigo = (int)excecaoWS;
        }
    }
}