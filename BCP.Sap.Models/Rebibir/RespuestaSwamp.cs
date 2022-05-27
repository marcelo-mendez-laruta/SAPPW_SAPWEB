using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaRegistroSwampCuenta : SapResponse
    {
        public RespuestaRegistroSwampCuentaData data {get;set;}
    }
    public class RespuestaRegistroSwampCuentaData
    {
        public int idCuenta { get; set; }
    }
}
