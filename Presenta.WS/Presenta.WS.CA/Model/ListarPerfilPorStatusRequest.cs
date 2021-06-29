using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.CA.Model
{
    public class ListarPerfilPorStatusRequest
    {
        public int Status { get; set; }
        public string Token { get; set; }
       
    }

    public class ListarPerfilPorStatusResponse
    {
        public string Mensagem { get; set; }
        public List<ListarPerfilResponse> LstPerfil { get; set; }

        public ListarPerfilPorStatusResponse()
        {
            LstPerfil = new List<ListarPerfilResponse>();
        }
    }


    public class ListarPerfilResponse
    {
        public int IdPerfil { get; set; }
        public int IdAplicativo { get; set; }
        public string DescricaoPerfil { get; set; }
        public int StatusPerfil { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}