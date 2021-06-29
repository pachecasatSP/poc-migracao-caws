using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Presenta.WS.Panamericano.Model
{
    [Serializable]
    public class Envolvido
    {
        [XmlIgnoreAttribute]
        public int IdEnvolInst { get; set; }

        public string NomeEnvolvido { get; set; }
        public string CpfCnpj { get; set; }
        public string TipoPessoa { get; set; }
        // S / N : tanto faz o nivel, pode ser no bloqueio, desbloqueio ou transf
        public string Reiteracao { get; set; }
        public string SequencialBloqueio { get; set; }
        public string SeqReiteracaoBloqueio { get; set; }
        public string SequencialDesbloqueio { get; set; }
        public string SeqReiteracaoDesbloqueio { get; set; }
        public string SequencialTransferencia { get; set; }
        public string SeqReiteracaoTransf { get; set; }        
        public string DesbloquearSaldoRemanescente { get; set; }        
        public string TipoDeposito { get; set; }
        public string CodigoDeposito { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string ValorOrdem { get; set; }
        public string InstituicaoFinanceira { get; set; }
        public string NumeroReferencia { get; set; }

        public DadoProc DadosProcessamento { get; set; }
        public DadoTransf DadosTransferencia { get; set; }

        public Envolvido()
        {
            this.DadosProcessamento = new DadoProc();
            this.DadosTransferencia = new DadoTransf();
        }

    }
}