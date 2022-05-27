using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaConsultaAep: SapResponse
    {
        public DatosAep data { get; set; }
    }
}
