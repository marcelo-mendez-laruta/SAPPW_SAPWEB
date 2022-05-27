using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaProducto : SapResponse
    {
        public List<TipoProducto> data { get; set; }
    }
    public class TipoProducto : DatosCuentaProducto
    {
        public string tipoPersona { get; set; }
        public string modalidadId { get; set; }
    }
}
