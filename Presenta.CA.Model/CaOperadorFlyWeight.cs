using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presenta.CA.Model
{
    [Serializable]
    public class CaOperadorFlyWeight
    {
        public int IdOperador { get; set; }
        public int StOperador { get; set; }
        public string CdOperador { get; set; }
        public string NmOperador { get; set; }
        public string DsEmail { get; set; }
    }
}
