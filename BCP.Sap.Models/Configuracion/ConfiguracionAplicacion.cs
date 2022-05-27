using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Configuracion
{
    public class ConfiguracionAplicacion
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string equipoDesarrollo { get; set; }
        public string equipoDesarrolloContacto { get; set; }
        public string solicitudAutorizacion { get; set; }
        public string origenesPermitidos { get; set; }

    }
}