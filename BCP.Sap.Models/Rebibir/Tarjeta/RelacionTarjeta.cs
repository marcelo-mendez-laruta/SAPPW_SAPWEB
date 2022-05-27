using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir.Tarjeta
{
    public class RespuestaRelacionTarjeta: SapResponse
    {
        public TarjetaRelacionData data { get; set; }
    }
    public class TarjetaRelacionData
    {
        public List<TarjetaRelacionItem> rows { get; set; }
        public int total { get; set; }
    }
    public class TarjetaRelacionItem
    {
        private string nroTarjeta1;
        public string nroTarjeta { get => LimpiarDatos.credimas(nroTarjeta1); set => nroTarjeta1 = string.Join("", value.Split('-')); }
        public string nombreTarjeta { get; set; }
        public string codSituacion { get; set; }
        public string desSituacion { get; set; }
    }

}
