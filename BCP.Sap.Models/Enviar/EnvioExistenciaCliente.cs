using BCP.Sap.Models.Enviar.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioExistenciaPersonaNatural : EnvioOperacion
    {
        public EnvioDocumentoPN cliente { get; set; }
        public ParametrosHost usuario { get; set; }
    }
    public class EnvioExistenciaPersonaJuridica : EnvioOperacion
    {
        public EnvioDocumentoPJ cliente { get; set; }
        public ParametrosHost usuario { get; set; }
    }
}
