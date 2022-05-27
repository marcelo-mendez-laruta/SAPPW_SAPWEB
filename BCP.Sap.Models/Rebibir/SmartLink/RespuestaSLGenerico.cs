using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir.SmartLink
{
    public class RespuestaSLGenerico: SapResponse
    {
    }

    public class RespuestaSLCredenciales : SapResponse
    {
        public RespuestaSLCredencialesData data { get;set; }
    }
   public class RespuestaSLCredencialesData
    {
        public string usuario { get; set; }
        public string contrasena { get; set; }
    }
}
