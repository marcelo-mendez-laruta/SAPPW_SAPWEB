using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir.Tarjeta
{
    public class RespuestaCambioTarjeta : SapResponse
    {
        public TarjetaEntregaData data { get; set; }
    }
    public class RespuestaCambioRapidoTarjeta : SapResponse
    {
        public ConsultaTarjeta data { get; set; }
    }
    public class TarjetaEntregaData
    {
        public string tipoEntrega { get; set; }
    }
}
