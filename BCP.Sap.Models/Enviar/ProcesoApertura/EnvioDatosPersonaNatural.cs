using BCP.Sap.Models.Enviar.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.ProcesoApertura
{
    public class EnvioDatosPersonaNatural : EnvioDocumentoPN
    {
        public string paterno { get; set; }
        public string materno { get; set; }
        public string nombres { get; set; }
    }
    public class EnvioDatosPersonaJuridica : EnvioDocumentoPJ
    {
        public string razonSocial { get; set; }
    }
}
