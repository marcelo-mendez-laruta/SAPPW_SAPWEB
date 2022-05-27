using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioBusquedaEmpleador:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public DatosEnvioEmpleador data { get; set; }
        public object cliente { get; set; }
    }
    public class EnvioModificacionRegistroEmpleador : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public DatosEnvioEmpleador empleador { get; set; }
        public object cliente { get; set; }
        public DatosEmpleador data { get; set; }
    }

    public class DatosEnvioEmpleador
    {
        public string empleadorIdcNumero{ get; set; }
        public string empleadorIdcTipo{ get; set; }
        public string empleadorIdcExtension{ get; set; }
    }
}
