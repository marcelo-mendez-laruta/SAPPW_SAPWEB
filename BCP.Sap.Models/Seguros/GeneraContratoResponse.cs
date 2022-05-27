using BCP.Sap.Models.Comunes;
using BCP.Sap.Models.Rebibir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Seguros
{
    public class GeneraContratoResponse: SapResponse
    {
        public GeneraContratoResponseData data { get;set;}
    }
    public class GeneraContratoResponseData
    {
        public string PDF { get; set; }
    }
}
