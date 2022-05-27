using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Tarjeta
{
    public class BancaExclusiva:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public BancaExclusivaData data { get; set; }
    }
    public class BancaExclusivaData
    {
        private string tarjeta1;

        public string tarjeta { get => tarjeta1; set => tarjeta1 = string.Join("", value.Split('-')); }
        public bool confirmar { get; set; }
        /*bd*/
        public string direccion { get; set; }
        public string tipoIDC { get; set; }
        public string idc { get; set; }
        public string nombreCompleto { get; set; }
        public string fecNac { get; set; }
        public string situacion { get; set; }
        //cuentas
        public string cuenta_ch_mn { get; set; }
        public string cuenta_ch_me { get; set; }
        public string cuenta_cc_mn { get; set; }
        public string cuenta_cc_me { get; set; }
    }
}
