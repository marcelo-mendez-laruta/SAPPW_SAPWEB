using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Configuracion
{
    public class ConfiguracionApi
    {
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public string canal { get; set; }
        public string token { get; set; }
        public string usuarioApp { get; set; }
    }
}
