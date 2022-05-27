using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Sap
{
    public class DatosEmpleador
    {
        public string fechaInicio { get; set; }
        public string fechaTermino { get; set; }
        public string codigoCargo { get; set; }
        public string fechaVerificacion { get; set; }
        public string rentaLiquida { get; set; }
        public string codigoMonedaRenta { get; set; }
        public string fechaRenta { get; set; }
        public string direccionEmpleador { get; set; }
        public string telefonoEmpleador { get; set; }
        public string codigoEstadoEmpleador { get; set; }
        public string codigoLocalidadEmpleador { get; set; }
    }
    public class EmpleadorBusquedaPNResponseData:DatosEmpleador
    {
        public string idcEmpleador { get; set; }
        public string tipoIdcEmpleador { get; set; }
        public string extensionIdcEmpleador { get; set; }
    }    
}
