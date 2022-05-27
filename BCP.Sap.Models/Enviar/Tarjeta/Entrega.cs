using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Tarjeta
{
    public class Entrega : EnvioNumeroTarjeta
    {
        public string tipoEntrega { get; set; }
        public string usuarioAutorizador { get; set; }
        public string usuarioPassword { get; set; }
        /*registro bd*/
        public string direccion { get; set; }
        public string tipoIDC { get; set; }
        public string idc { get; set; }
        public string nombreCompleto { get; set; }
        //public string situacion { get; set; }
        public string fecNac { get; set; }
        public string bcex { get; set; }
        //cuentas
        public string cuenta_ch_mn { get; set; }
        public string cuenta_ch_me { get; set; }
        public string cuenta_cc_mn { get; set; }
        public string cuenta_cc_me { get; set; }
    }
}
