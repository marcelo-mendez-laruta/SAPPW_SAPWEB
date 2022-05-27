using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaConsultaAlfabetica : SapResponse
    {
        public List<ConsultaAlfabetica> data { get; set; }
    }
    public class ConsultaAlfabetica
    {
        private string fechaNacimiento1;

        public string idcNumero { get; set; }
        public string idcTipo { get; set; }
        public string idcExtension { get; set; }
        public string nombreCompleto { get; set; }
        public string tipoCliente { get; set; }
        public string tipoBanca { get; set; }
        public string oficina { get; set; }
        public string codigo { get; set; }
        public string fechaNacimiento { get => fechaNacimiento1; set => fechaNacimiento1 = LimpiarDatos.formatoFecha(value); }
    }
}
