using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaReporte: SapResponse
    {
        public RespuestaReporteData data { get; set; }
    }
    public class RespuestaReporteData
    {
        public string pdf { get; set; }
    }
}
