using BCP.Sap.Models.Enviar.Cliente;
using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioBusquedaAep : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public object cliente { get; set; }
    }
    public class EnvioOperacionAep : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public object cliente { get; set; }
        public DatosAep data { get; set; }
    }
    public class EnvioClientePN:EnvioDocumentoPN
    {
        public string fechaValor { get; set; }
    }
    public class EnvioClientePJ : EnvioDocumentoPJ
    {
        public string fechaValor { get; set; }
    }
}
