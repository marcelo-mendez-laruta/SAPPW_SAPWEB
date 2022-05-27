using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Sap
{
    public class Stock
    {
        public string estadoTarjeta { get; set; }
        public string nombreOficina { get; set; }
        public string sucursal { get; set; }
        public string agencia { get; set; }

        public string cantidadActiva { get; set; }
        public string fechaRecepcion { get; set; }
        private string loteInicialAct1;
        public string loteInicialAct { get => LimpiarDatos.credimas(loteInicialAct1); set => loteInicialAct1 = LimpiarDatos.credimas(value); }
        private string loteFinalAct1;
        public string loteFinalAct { get => LimpiarDatos.credimas(loteFinalAct1); set => loteFinalAct1 = LimpiarDatos.credimas(value); }

        public string stockOficina { get; set; }
        public string stockReposicion { get; set; }
        public string stockMinimo { get; set; }
        public string stockBloqueo { get; set; }

        public string fechaUltimoEnvio { get; set; }
        public string cantidad { get; set; }
        private string loteInicialAnt1;
        public string loteInicialAnt { get => LimpiarDatos.credimas(loteInicialAnt1); set => loteInicialAnt1 = LimpiarDatos.credimas(value); }
        private string loteFinalAnt1;
        public string loteFinalAnt { get => LimpiarDatos.credimas(loteFinalAnt1); set => loteFinalAnt1 = LimpiarDatos.credimas(value); }

        public string fechaRebajaDeStock { get; set; }
        public string cantidadTarjetasRebajadas { get; set; }

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
