using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Presenta.WS.Panamericano.Model
{
    [Serializable]
    [XmlType(TypeName = "Ordem")]    
    public class Oficio
    {
        [XmlIgnoreAttribute]
        public int IdInstituicao { get; set; }
        [XmlIgnoreAttribute]
        public int IdLote { get; set; }
        [XmlIgnoreAttribute]
        public int IdOficio { get; set; }
        [XmlIgnoreAttribute]
        public int IdTipoFonte { get; set; }
        [XmlIgnoreAttribute]
        public int IdSolicitante { get; set; }
        [XmlIgnoreAttribute]
        public int IdTipo { get; set; }
        [XmlIgnoreAttribute]
        public int? IdTipoNaturezaAcao { get; set; }

        public string Tipo { get; set; }
        public string DataProtocolo { get; set; }
        public string Protocolo { get; set; }
        public string DataSisbacen { get; set; }
        public string NumProcesso { get; set; }
        public string TipoJustica { get; set; }
        public string NomeAutor { get; set; }

        public SolicitanteOf Solicitante { get; set; }
        public List<Envolvido> Envolvidos { get; set; }

        [XmlIgnoreAttribute]
        public string ValorOrdem { get; set; }

        public Oficio()
        {
            this.Solicitante = new SolicitanteOf();
            this.Envolvidos = new List<Envolvido>();
        }
    }
}