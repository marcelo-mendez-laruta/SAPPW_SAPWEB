using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Producto
{
    public class EnvioProductoCliente:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public ProductoCliente data { get; set; }
    }
    public class ProductoCliente
    {
        public string tipoCuenta{ get; set; }
        public string cuenta { get; set; }
    }
}
