using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Reporte
{
    public class ReporteApertura:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public ReporteAperturaData consulta { get; set; }
    }
    public class ReporteAperturaData
    {
        public string fecha { get; set; }
        public string usuario { get; set; }
        public string tipoOperacion { get; set; }
        public bool personaNatural { get; set; }
        public string indModalidad { get; set; }
    }
}
