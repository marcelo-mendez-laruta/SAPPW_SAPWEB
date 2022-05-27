using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir.Tarjeta
{
    public class RespuestaBloqueo : SapResponse
    {
        public RespuestaBloqueoData data { get; set; }
    }
    public class RespuestaBloqueoData
    {
        public string codBloqueo { get; set; }
        public string fechaBloqueo { get; set; }
        public string oficinaBloqueo { get; set; }
    }
    public class RespuestaConsultaBloqueo: SapResponse
    {
        public ConsultaBloqueoData data { get; set; }
    }
    public class ConsultaBloqueoData
    {
        public int total { get; set; }
        public List<ConsultaBloqueoDataItem> tarjetas { get; set; }
    }
    public class ConsultaBloqueoDataItem
    {
        public string usuario { get; set; }
        public string tarjeta { get; set; }
        public string codBloqueo { get; set; }
        public string motivo { get; set; }
        public string fechaBloqueo { get; set; }
        public string nombre { get; set; }
        public string idc { get; set; }
        public string telefono { get; set; }
        public string nombreReporta { get; set; }
        public string sucursal { get; set; }
        public string agencia { get; set; }
        public string observaciones { get; set; }
    }
}
