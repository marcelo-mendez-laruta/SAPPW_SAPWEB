using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioInteresAdelantado:EnvioOperacion
    {
        public EnvioIAData cuenta { get; set; }
    }
    public class EnvioIAData
    {
        public string monto { get; set; }
        public string plazo { get; set; }
        public string tipoPlazo { get; set; }
        public string tasa { get; set; }
        public string agencia { get; set; }
        public string usuario { get; set; }
    }
}
