using BCP.Sap.Models.Enviar;
using BCP.Sap.Models.Rebibir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Sap
{
    public class DatosUsurio
    {
        public string usuario { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string cargo { get; set; }
        public string politicas { get; set; }
    }
}
