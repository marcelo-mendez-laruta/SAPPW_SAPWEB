using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.SmartLink
{
    public class EnvioSLValidar:EnvioOperacion
    {
        public SLValidarData data { get; set; }
    }
    public class SLValidarData
    {
       public string tarjeta { get; set; }
       public string pin { get; set; }
       public string terminal { get; set; }
        public string numeroTerminal { get; set; }
    }
}
