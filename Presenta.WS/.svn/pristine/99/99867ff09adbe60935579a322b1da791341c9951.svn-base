using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Presenta.WS.Panamericano.Model
{
    [Serializable]
    public class SolicitanteOf
    {
        [XmlIgnoreAttribute]
        public int IdSolicitante { get; set; }

        public string NomeSolicitante { get; set; }
        public string CodVaraJuizo { get; set; }
        public string NomeVaraJuizo { get; set; }

        public string Telefone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        public Endereco EndVaraJuizo { get; set; }

        public SolicitanteOf() 
        {
            this.EndVaraJuizo = new Endereco();
        }
    }
}