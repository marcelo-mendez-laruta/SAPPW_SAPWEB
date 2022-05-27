using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Seguros.Service
{
    public class AfiliacionResponseModel
    {
        public string CODIGO_ERROR { get; set; }

        public string ERROR_TECNICO { get; set; }

        public long ID_AFILIACION { get; set; }

        public string ID_CLIENTE { get; set; }

        public string MENSAJE_ERROR { get; set; }
    }
}
