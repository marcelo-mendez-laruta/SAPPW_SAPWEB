using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Tarjeta
{
    public class Cambio: EnvioNumeroTarjeta
    {
        private string tarjetaNueva1;
        public string direccion { get; set; }
        public string tarjetaNueva { get => tarjetaNueva1; set => tarjetaNueva1 = string.Join("", value.Split('-')); }
        public string codigoBloqueo { get; set; }
        //public string descripcionCambio { get; set; }        
        public bool cobro { get; set; }
        public string pinEncriptado { get; set; }
        /*BD*/
        public string tipoIDC { get; set; }
        public string idc { get; set; }
        public string nombreCompleto { get; set; }
        public string indMenor { get; set; }
    }
    public class CambioRapido
    {
        private string tarjetaAntigua1;
        private string tarjetaNueva1;

        public string tarjetaAntigua { get => tarjetaAntigua1; set => tarjetaAntigua1 = string.Join("", value.Split('-')); }
        public string tarjetaNueva { get => tarjetaNueva1; set => tarjetaNueva1 = string.Join("", value.Split('-')); }
        public string pin { get; set; }
        public string situacionTarjeta { get; set; }
        public string usuarioAutorizador { get; set; }
        public string passwordAutorizador { get; set; }
    }
}
