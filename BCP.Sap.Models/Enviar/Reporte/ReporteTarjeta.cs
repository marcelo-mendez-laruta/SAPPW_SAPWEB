using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Reporte
{
    public class ReporteTarjeta:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public ReporteTarjetaData consulta { get; set; }
    }
    public class ReporteTarjetaData
    {
        public string fecha { get; set; }
        public string usuario { get; set; }
        public string tipoOperacion { get; set; }
    }
}
