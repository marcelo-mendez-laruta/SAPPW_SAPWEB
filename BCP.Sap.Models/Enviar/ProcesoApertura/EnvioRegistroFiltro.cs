using BCP.Sap.Models.Enviar.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.ProcesoApertura
{
    public class EnvioRegistroFiltro:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public FiltroPersona cliente { get; set; }
        public EnvioRegistroFiltroData data { get; set; }
    }
    public class FiltroPersona: EnvioDocumentoPJ
    {
        public string nombresRazonSocial { get; set; }
    }
    public class EnvioRegistroFiltroData
    {
        public bool archivoNegativo { get; set; }
        public bool enameChecker { get; set; }
        public bool sinFiltro { get; set; }
    }
}
