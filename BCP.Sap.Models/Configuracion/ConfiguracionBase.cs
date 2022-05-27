using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Configuracion
{
    public class ConfiguracionBase
    {
        public ConfiguracionAplicacion configuracionAplicacion { get; set; }
        public ConfiguracionLogs configuracionLog { get; set; }
        public List<ConexionBaseDeDatos> bases { get; set; }
        public ConfiguracionApi configuracionAPI { get; set; }
        public ConfiguracionProducto configuracionProducto { get; set; }
        public ConfiguracionMicroservicio configuracionMicroservicios { get;set;}
        public ConfiguracionGrupoDominio configuracionGrupoDominio { get; set; }
        public ConfiguracionQR configuracionQR { get; set; }
        public string AllowedHosts { get; set; }
        public List<string> Sucursales { get; set; }
        public List<ConfiguracionSeguros> configuracionSeguros { get; set; }
    }
}
