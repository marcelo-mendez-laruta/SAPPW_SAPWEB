using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaInteresAdelantado:SapResponse
    {
        public RespuestaIntAde data { get; set; }
    }
    public class RespuestaIntAde
    {
        public string intAnticipado { get; set; }
        public string fechaVencimiento { get; set; }
        public string plazoFinal { get; set; }
        public string montoLimite { get; set; }
        public string plazoLimite { get; set; }
        public string nroCuenta { get; set; }
        public string tipoCuenta { get; set; }
        public string moneda { get; set; }
        public string tazaLimite { get; set; }
    }
}
