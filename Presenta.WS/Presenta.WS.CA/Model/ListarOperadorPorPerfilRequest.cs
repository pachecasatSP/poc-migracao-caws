using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.CA.Model
{
    public class ListarOperadorPorPerfilRequest
    {
        public int IdPerfil { get; set; }
        public string Token { get; set; }
    }


    public class ListarOperadorPorPerfilResponse
    {
        public string Mensagem { get; set; }
        public List<ListarOperadorResponse> LstOperadorPerfil { get; set; }

        public ListarOperadorPorPerfilResponse()
        {
            LstOperadorPerfil = new List<ListarOperadorResponse>();
            Mensagem = "";
        }
    }

    public class ListarOperadorResponse
    {
        public int IdOperador { get; set; }
        public string NomeOperador { get; set; }
        public string CdOperador { get; set; }
        public string Email { get; set; }
        public int StatusOperador { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int IdOperadorAtualizacao { get; set; }
    }
}