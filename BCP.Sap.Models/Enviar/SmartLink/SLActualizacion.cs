using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.SmartLink
{
    public class EnvioSLActualizacion:EnvioOperacion
    {
        public SLActualizacion data { get; set; }
    }
    public class SLActualizacion
    {
        public string tarjeta { get; set; }
        public string cuenta_ch_me { get; set; }
        public string cuenta_ch_mn { get; set; }
        public string cuenta_cc_me { get; set; }
        public string cuenta_cc_mn { get; set; }
    }
}
