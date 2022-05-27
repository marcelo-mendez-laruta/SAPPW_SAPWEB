using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.ProcesoApertura
{
    public class EnvioConsultaAlfabetica : EnvioOperacion
    {
        public object data { get; set; }
        public ParametrosHost usuario { get; set; }
    }
}
