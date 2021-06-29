using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Presenta.WS.CA.Model
{
    public class ListarSistemaAplicativoPerfilOperadorRequest
    {       
        public int IdOperador { get; set; }
        public int IdAplicativo { get; set; }
        public int IdPerfil { get; set; }        
        public string Token { get; set; }
    }

    public class ListarSistemaAplicativoPerfilOperadorResponse
    {
        public string Mensagem { get; set; }
        public List<SistemaAplicativoResponse> LstSistema { get; set; }

        public ListarSistemaAplicativoPerfilOperadorResponse()
        {
            LstSistema = new List<SistemaAplicativoResponse>();
        }
    }

    public class SistemaAplicativoResponse
    {
        public int Id { get; set; }
        public string DescricaoSistema { get; set; }

        public List<AplicativoPerfilReponse> LstAplicativo { get; set; }

        public SistemaAplicativoResponse()
        {
            LstAplicativo = new List<AplicativoPerfilReponse>();
        }
    }

    public class AplicativoPerfilReponse
    {
        public int Id { get; set; }

        [XmlIgnore]
        public int IdSistema { get; set; }
        public string DescricaoAplicativo { get; set; }
        public List<PerfilOperadorResponse> LstPerfil { get; set; }

        public AplicativoPerfilReponse()
        {
            LstPerfil = new List<PerfilOperadorResponse>();
        }

    }

    public class PerfilOperadorResponse 
    {
        public int Id { get; set; }
        
        [XmlIgnore]
        public int IdAplicativo { get; set; }

        [XmlIgnore]
        public int IdOperador { get; set; }
        public string DescricaoPerfil { get; set; }

        public List<OperadorResponse> LstOperador { get; set; }

        public PerfilOperadorResponse()
        {
            LstOperador = new List<OperadorResponse>();
        }

    }

    public class OperadorResponse
    {
        public int Id { get; set; }
        [XmlIgnore]
        public int IdPerfil { get; set; }
    }

}