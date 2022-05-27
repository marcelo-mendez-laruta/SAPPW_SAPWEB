using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaConsultaFatca : SapResponse
    {
        public Fatca data { get; set; }
    }
}
