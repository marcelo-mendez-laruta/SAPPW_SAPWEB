using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Tarjeta
{
    public class Relacion
    {
        private string nroCuenta1;
        public string nroCuenta { get => nroCuenta1; set => nroCuenta1 = string.Join("", value.Split('-')); }
        public string tipoCuenta { get; set; }
        public string moneda { get; set; }
    }
}
