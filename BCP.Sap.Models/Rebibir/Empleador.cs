using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaEmpleadorBusqueda: SapResponse
    {
        public RespuestaEmpleadorBusquedaData data { get; set; }
    }
    public class RespuestaEmpleadorBusquedaData : DatosEmpleador
    {
        public string nombreCliente { get; set; }
        public string nombreEmpleador { get; set; }
        public string idcEmpleador { get; set; }
        public string tipoIdcEmpleador { get; set; }
        public string extensionIdcEmpleador { get; set; }
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
}
