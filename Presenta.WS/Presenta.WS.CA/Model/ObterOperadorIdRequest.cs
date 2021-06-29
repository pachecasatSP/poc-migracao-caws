using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.CA.Model
{
    public class ObterOperadorIdRequest
    {
        public string CdOperador { get; set; }    
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class ObterIdOperadorResponse
    {
        public string Mensagem { get; set; }

        public List<OperadorIdResponse> LstOperadorResult { get; set; }

        public ObterIdOperadorResponse()
        {
            LstOperadorResult = new List<OperadorIdResponse>();
        }
    }

    public class OperadorIdResponse
    {
        public int IdOperador { get; set; }
    }
}