using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Sap
{
    public class Fatca
    {
        public string idcNumero { get; set; }
        public string idcTipo { get; set; }
        public string idcExtension { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string razonSocial { get; set; }
        public string tipoPersona { get; set; }
        public bool residenciaFiscal { get; set; }
        public int paisNacionalidadId { get; set; }
        public int paisNacimientoId { get; set; }
        public int paisSegundaNacionalidadId { get; set; }
        public int paisResidenciaId { get; set; }
        public int paisResidenciaFiscalId { get; set; }
        public string paisNacimientoText { get; set; }
        public string paisNacionalidadText { get; set; }
        public string paisSegundaNacionalidadText { get; set; }
        public string paisResidenciaText { get; set; }
        public string paisResidenciaFiscalText { get; set; }
        public string paisNacimientoHost { get; set; }
        public string paisNacionalidadHost { get; set; }
        public string paisSegundaNacionalidadHost { get; set; }
        public string paisResidenciaHost { get; set; }
        public string paisResidenciaFiscalHost { get; set; }
        public string numeroIdentificacionContribuyente { get; set; }
        public string numeroSeguroSocial { get; set; }
        public int preguntaNroUno { get; set; }
        public int respuestaNroUno { get; set; }
        public int preguntaNroDos { get; set; }
        public int respuestaNroDos { get; set; }
        public int preguntaNroTres { get; set; }
        public int respuestaNroTres { get; set; }
        public int preguntaNroCuatro { get; set; }
        public int respuestaNroCuatro { get; set; }
        public int porcentajeParticipacion { get; set; }
    }
}
