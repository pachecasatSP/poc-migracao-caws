using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.Panamericano.Model
{
    [Serializable]
    public class DadoTransf
    {
        public string IdTransferencia { get; set; }
        public string CnpjBancoDestino { get; set; }
        public string NomeBancoDestino { get; set; }
        public string AgenciaDestino { get; set; }
        public string CpfCnpjBeneficiario { get; set; }
        public string NomeBeneficiario { get; set; }
        public string IdPagamento { get; set; }
        public string SituacaoPagamento { get; set; }
    }
}