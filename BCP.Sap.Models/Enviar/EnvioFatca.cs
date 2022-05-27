using BCP.Sap.Models.Enviar.Cliente;
using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioConsultaFatca : EnvioDocumentoCic
    {
        public readonly string operation = DateTime.Now.ToString("yyyyMMddhhmmss");
        public ParametrosHost usuario { get; set; }
    }
    public class EnviarCreacionFatca : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public Fatca data { get; set; }
    }

}
