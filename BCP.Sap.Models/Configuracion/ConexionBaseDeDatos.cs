using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Configuracion
{
    public class ConexionBaseDeDatos
    {
        public string servidorBd { get; set; }
        public string nombreBd { get; set; }
        public string usuarioBd { get; set; }
        public string passwordBd { get; set; }
    }
}
