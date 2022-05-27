using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.SmartLink
{
    public class EnvioSLCuenta:EnvioOperacion
    {
        public SLCuenta data { get; set; }
    }
    public class SLCuenta
    {
       public string nombreCliente { get; set; }
       public string cuenta { get; set; }
       public string tarjeta { get; set; }
    }
}
