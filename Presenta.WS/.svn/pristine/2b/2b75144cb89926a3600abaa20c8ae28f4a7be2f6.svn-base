using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.CA.Model
{
    public class ObterSistemaAplicativoRequest
    {
        public string Token { get; set; }
    }

    public class ObterSistemaAplicativoResponse
    {
        public string Mensagem { get; set; }
        public List<ListaSistemaResponse> LstSistema { get; set; }

        public ObterSistemaAplicativoResponse()
        {
            LstSistema = new List<ListaSistemaResponse>();
        }

    }

    public class ListaSistemaResponse
    {
        public int Id { get; set; }
        public string DescricaoSistema { get; set; }

        public List<ListaAplicativoReponse> LstAplicativo { get; set; }

        public ListaSistemaResponse()
        {
            LstAplicativo = new List<ListaAplicativoReponse>();
        }
    }

    public class ListaAplicativoReponse
    {
        public int Id { get; set; }
        public string DescricaoAplicativo { get; set; }
    }
}