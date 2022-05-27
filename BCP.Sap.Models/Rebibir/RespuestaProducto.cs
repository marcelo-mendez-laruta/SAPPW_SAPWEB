using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaConsultaProducto : SapResponse
    {
        public ConsultaProducto data { get; set; }
    }
    public class RespuestaConsultaProductoCliente : SapResponse
    {
        public ProductoClienteData data { get; set; }
    }
    public class ConsultaProducto
    {
        public string nombreCuenta { get; set; }
        public string masProductos { get; set; }
        public string codigoInicio { get; set; }
        public string codigoFin { get; set; }
        public List<Producto> rows { get; set; }
    }
}
