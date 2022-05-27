using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.SmartLink
{
    public class EnvioSLCambio:EnvioOperacion
    {
        public SLCambioData data { get; set; }
    }
    public class SLCambioData
    {
        public string tarjetaAntigua { get; set; }
        public string tarjetaNueva { get; set; }
        public string cuenta_ch_me { get; set; }
        public string cuenta_ch_mn { get; set; }
        public string cuenta_cc_me { get; set; }
        public string cuenta_cc_mn { get; set; }
    }
}
