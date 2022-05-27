using BCP.Framework.Common;
using BCP.Sap.Models.Enviar.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioConsultaProducto:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public PaginacionCuenta data { get; set; }
    }
    public class EnvioRegistroProductoDireccion : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public EnvioRegistroProductoDireccionData data { get; set; }
    }
    public class EnvioRegistroProductoDireccionData
    {
        public string numeroDeCuentaExtra { get; set; }
        public int numeroDireccion { get; set; }
    }
    public class PaginacionCuenta : EnvioDocumentoPJ
    {
        public string codigoInicio { get; set; }
        public string codigoFin { get; set; }
    }
    
}
