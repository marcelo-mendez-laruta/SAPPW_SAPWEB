using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaParametro : SapResponse
    {
        public List<Parametro> data { get; set; }
    }
}
