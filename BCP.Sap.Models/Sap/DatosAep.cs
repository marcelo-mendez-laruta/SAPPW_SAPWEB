using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Sap
{
    public class DatosAep
    {
        public string fechaValor { get; set; }
        public string fechaVerificacion { get; set; }
        public string rentaBrutaDelCliente { get; set; }
        public string rentaBrutaDelConyuge { get; set; }
        public string otrosIngresos { get; set; }
        public string totalDeIngresos { get; set; }
        public string depositosAPlazo { get; set; }
        public string accionesYBonos { get; set; }
        public string bieneYRaices { get; set; }
        public string vehiculos { get; set; }
        public string aporteSociedades { get; set; }
        public string otrosActivos { get; set; }
        public string totalDeActivos { get; set; }
        public string gastoMensual { get; set; }
        public string arriendo { get; set; }
        public string otrosEgresos { get; set; }
        public string totalEgresos { get; set; }
        public string ingresoDisponible { get; set; }
        public string deudasBancComer { get; set; }
        public string deudaHipotecaria { get; set; }
        public string otrosPasivos { get; set; }
        public string totalPasivos { get; set; }
        public string patrimonio { get; set; }
        public string avalesOtorgados { get; set; }
    }
}
