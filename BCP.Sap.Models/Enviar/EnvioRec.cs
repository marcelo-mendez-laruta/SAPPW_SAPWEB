using BCP.Sap.Models.Enviar.Cliente;
using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioConsultaRec : EnvioOperacion
    {
        public object cliente { get; set; }
        public ParametrosHost usuario { get; set; }
    }
    public class EnvioOperacionesRec : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public object cliente { get; set; }
        public DatosRec data { get; set; }
    }
    public class EnvioEliminacionRec:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public object cliente { get; set; }
        public DatosPersonaRec data { get; set; }
    }     
    
}
