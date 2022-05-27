using BCP.Sap.Models.Enviar;
using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaCreacionCuenta : SapResponse
    {
        public Cuenta data { get; set; }
    }
   
    public class RespuestaTipoCuenta : SapResponse
    {
        public List<TipoCuenta> data;
    }

    public class TipoCuenta : DatosCuentaProducto
    {
        public string comando { get; set; }
    }
}
