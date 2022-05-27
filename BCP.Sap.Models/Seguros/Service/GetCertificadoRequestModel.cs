using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Seguros.Service
{
    public class GetCertificadoRequestModel
    {
        public string IdPersona { get; set; }
        public long IdAfiliacion { get; set; }
        public string IdProducto { get; set; }
    }
}
