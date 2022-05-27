using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Configuracion
{
    public class ConfiguracionMicroservicio
    {
        public string url { get; set; }
    }
    public class ConfiguracionGrupoDominio
    {
        public string grupos { get; set; }
    }
    public class ConfiguracionQR
    {
        public string modificacion { get; set; }
    }
}
