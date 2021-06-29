using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.CA.Model
{
    public class AssociarPerfilRequest
    {
        public int IdOperador { get; set; }
        public List<Perfil> LstPerfil { get; set; }
        public int IdOperadorAtualizacao { get; set; }
        public string Token { get; set; }

       
    }

    public class AssociarPerfilResponse
    {
        public int IdOperador { get; set; }
        public List<Perfil> LstPerfil { get; set; }
        public string Mensagem { get; set; }

        public AssociarPerfilResponse()
        {
            LstPerfil = new List<Perfil>();
        }
    }

    public class Perfil
    {
       public int IdPerfil { get; set; }
    }
}