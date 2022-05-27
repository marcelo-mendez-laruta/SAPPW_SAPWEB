using BCP.Sap.Models.Comunes;
using BCP.Sap.Models.Enviar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Seguros
{
    public class GeneraContratoRequest: EnvioOperacion
    {
        public GeneraContratoRequestData data { get; set; }
    }
    public class GeneraContratoRequestData
    {
        public long IdAfiliacion { get; set; }
        public string IdPersona { get; set; }
        public string CodigoProducto { get; set; }
        public string IpOrigen { get; set; }
    }
}
