using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Tarjeta
{
    public class EnvioActualizacionTarjeta : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public ActualizacionData data{get;set;}
    }
    public class ActualizacionData
    {
        public string sucursal { get; set; }
        public string agencia { get; set; }
        public string aux { get; set; }
        public string tipoTarjeta { get; set; }
        public string stockMinimo { get; set; }
        public string stockReposicion { get; set; }
        public string estado { get; set; }
    }
}
