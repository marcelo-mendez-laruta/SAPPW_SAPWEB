using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.SmartLink
{
    public class EnvioSLDesafiliar:EnvioOperacion
    {
        public SLDesafiliar data { get; set; }
    }
    public class SLDesafiliar
    {
        public string tarjeta { get; set; }
        public string cuenta { get; set; }
    }
}
