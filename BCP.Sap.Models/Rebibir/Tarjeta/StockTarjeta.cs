using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir.Tarjeta
{
    public class RespuestaStockTarjeta : SapResponse
    {
        public int total { get; set; }
        public Stock data { get; set; }
    }



    public class StockTarjeta
    {
        public string nombreOficina { get; set; }
        public string sucursal { get; set; }
        public string agencia { get; set; }
        public string estado { get; set; }
        public string tipoTarjeta { get; set; }
        public string recepFecha { get; set; }
        public string recepCantidad { get; set; }
        public string recepIniLote { get; set; }
        public string recepFinLote { get; set; }

        public string stockOficina { get; set; }
        public string stockReposicion { get; set; }
        public string stockMinimo { get; set; }
        public string stockUltimaRecep { get; set; }

        public string anteriorFecha { get; set; }
        public string anteriorCantidad { get; set; }
        public string anteriorIniLote { get; set; }
        public string anteriorFinLote { get; set; }

        public string rebajadasFecha { get; set; }
        public string rebajadasCantidad { get; set; }

        public string tarjCon01 { get; set; }
        public string tarjCon02 { get; set; }
        public string tarjCon03 { get; set; }
        public string tarjCon04 { get; set; }
        public string tarjCon05 { get; set; }
        public string tarjCon06 { get; set; }

        public string fechaCon01 { get; set; }
        public string fechaCon02 { get; set; }
        public string fechaCon03 { get; set; }
        public string fechaCon04 { get; set; }
        public string fechaCon05 { get; set; }
        public string fechaCon06 { get; set; }
    }
}
