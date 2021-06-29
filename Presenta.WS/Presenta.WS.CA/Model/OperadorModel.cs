using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.CA.Model
{
    public class OperadorModel
    {
        public int IdOperador { get; set; }
        public string CdOperador { get; set; }

        public int IdOperadorAtualizacao { get; set; }
        public int StatusOperador { get; set; }
        public string NomeOperador { get; set; }
        public string Email { get; set; }
        
        public string Token { get; set; }

        public void ValidarCriarOperador()
        {
            if (string.IsNullOrEmpty(CdOperador))
                throw new CAException("CdOperador não informado");

            if (IdOperadorAtualizacao == 0)
                throw new CAException("IdOperadorAtualizacao não informado");

            if (StatusOperador == 0)
                throw new CAException("StatusOperador não informado");

            if (string.IsNullOrEmpty(NomeOperador))
                throw new CAException("NomeOperador não informado");

            if (string.IsNullOrEmpty(Email))
                throw new CAException("Email não informado");
        }

      
    }

    public class RetornoOperadorModel
    {
        public int IdOperador { get; set; }       
        public int StatusOperador { get; set; }

        public string Mensagem { get; set; }
    }

   
}