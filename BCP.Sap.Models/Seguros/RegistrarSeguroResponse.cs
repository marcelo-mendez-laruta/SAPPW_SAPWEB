using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCP.Sap.Models.Rebibir;

namespace BCP.Sap.Models.Seguros
{
    public class RegistrarSeguroResponse:SapResponse
    {
        public RegistrarSeguroResponseData data { get; set; }
    }
    public class RegistrarSeguroResponseData
    {
        public long IdAfiliacion { get; set; }
        public string IdCliente { get; set; }
        public string PDF { get; set; }
    }
}
