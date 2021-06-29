using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.CA.Model
{
    public class ListarOperadoresAplicativoStatusRequest
    {
        public int IdAplicativo { get; set; }
        public int IdStatus { get; set; }
        public string Token { get; set; }
    }


    public class ListarOperadoresAplicativoStatusResponse
    {

        public string Mensagem { get; set; }

        public List<OperadoresAplicativoResponse> lstOperadores { get; set; }

        public ListarOperadoresAplicativoStatusResponse()
        {
            lstOperadores = new List<OperadoresAplicativoResponse>();
        }
       

    public class OperadoresAplicativoResponse
    {
            public int IdOperador { get; set; }
            public int StatusOperador { get; set; }
            public string CdOperador { get; set; }
            public string NomeOperador { get; set; }
            public string Email { get; set; }
           
        }
    }
}