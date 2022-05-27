using BCP.Framework.Common;
using BCP.Sap.Models.Enviar.Tarjeta;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioBusquedaTarjeta:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public EnvioNombreTarjeta data { get; set; }
    }

    public class EnvioConsultaTarjeta : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public EnvioNumeroTarjeta data { get; set; }

    }
    public class EnvioReporteStockTarjeta : EnvioOperacion
    {
        public BCP.Sap.Models.Sap.Stock data { get; set; }
    }
    public class EnvioStockTarjeta : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public StockConsulta data { get; set; }
    }
    public class EnvioCambioTarjeta : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public Cambio data { get; set; }
    }
    public class EnvioCambioRapido : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public CambioRapido data { get; set; }
    }
    public class EnvioEntregaTarjeta:EnvioOperacion 
    {
        public ParametrosHost usuario { get; set; }
        public Entrega data { get; set; }
    }
    public class EnvioRecepcionTarjeta:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public Recepcion data { get; set; }
    }

    public class EnvioOperacionTarjeta:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public Datos data { get; set; }
    }
    public class EnvioBloqueoTarjeta: EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public Bloqueo data { get; set; }
    }
    public class EnvioRelacionTarjeta : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public Relacion data { get; set; }
    }
}
