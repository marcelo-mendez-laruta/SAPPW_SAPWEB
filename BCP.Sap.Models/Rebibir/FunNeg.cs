using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaCambioFunNeg:SapResponse
    {
        public RFunNeg data { get; set; }
    }
    public class RFunNeg
    {
        public object certificado { get; set; }
    }
}
