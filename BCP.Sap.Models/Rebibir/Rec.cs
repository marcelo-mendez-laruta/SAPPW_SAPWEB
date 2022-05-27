using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaConsultaRec : SapResponse
    {
        public RespuestaConsultaRecData data { get; set; }
    }
    public class RespuestaConsultaRecData
    {
        public List<DatosConsultaRec> rows { get; set; }
        public int total { get; set; }
    }

    public class DatosConsultaRec : DatosRec
    {       
        public string desconocido01 { get; set; }
    }
}
