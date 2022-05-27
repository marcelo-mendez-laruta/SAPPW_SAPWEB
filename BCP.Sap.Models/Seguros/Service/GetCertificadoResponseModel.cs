using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Seguros.Service
{
    public class GetCertificadoResponseModel
    {
        public string CODIGO_ERROR { get; set; }
        public string ERROR_TECNICO { get; set; }
        public byte[] IMAGEN { get; set; }
        public string MENSAJE_ERROR { get; set; }
    }
}
