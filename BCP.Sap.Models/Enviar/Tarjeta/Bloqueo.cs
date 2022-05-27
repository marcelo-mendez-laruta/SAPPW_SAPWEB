using BCP.Sap.Models.Enviar.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Tarjeta
{
    public class Bloqueo: EnvioNumeroTarjeta
    {
        public string tipoBloqueo { get; set; }
        public string descripcionBloqueo { get; set; }
        public string pin { get; set; }
        /*registro bd*/
        public string direccion { get; set; }
        public string tipoIDC { get; set; }
        public string idc { get; set; }
        public string nombreCompleto { get; set; }
        public string fecNac { get; set; }
        public string bcex { get; set; }
        //cuentas
        public string cuenta_ch_mn { get; set; }
        public string cuenta_ch_me { get; set; }
        public string cuenta_cc_mn { get; set; }
        public string cuenta_cc_me { get; set; }
    }

    public class EnvioRegistroBloqueo : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public EnvioDocumentoPJ cliente { get; set; }
        public BloqueoDBData data { get; set; }
    }
    public class EnvioConsultaBloqueo : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public BloqueoDBDataCondicional condicionales { get; set; }
    }
    public class BloqueoDBData
    {
        public string tarjeta { get; set; }
        public string codBloqueo { get; set; }
        public string motivo { get; set; }
        public string fechaBloqueo { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string nombreReporta { get; set; }
        public string fechaVenTarj { get; set; }
        public string observaciones { get; set; }
        //
        public string idcDB { get; set; }
        public string tidcDB { get; set; }
    }
    public class BloqueoDBDataCondicional
    {
        public string tarjeta { get; set; }
        public string codBloqueo { get; set; }
        public string fechaBloqueo { get; set; }
        public string nombre { get; set; }
        public string idc { get; set; }
        public string tidc { get; set; }
        public string sucursal { get; set; }
        public string agencia { get; set; }
        public string usuario { get; set; }
    }
}
