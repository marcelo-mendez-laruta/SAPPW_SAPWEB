using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Sap
{
    public class DatosPersonaRec
    {
        public string codigoRelacion { get; set; }
        public string idcPersona { get; set; }
        public string tipoDocumentoPersona { get; set; }
        public string extensionPersona { get; set; }
    }
    public class DatosRec: DatosPersonaRec
    {
       
        public string codigoAsociado { get; set; }
        public string vigente { get; set; }
        public string nombre { get; set; }
        public string fechaInicio { get; set; }
        public string fechaTermino { get; set; }
        public string fechaVerificacion { get; set; }
        public string valorAsociado { get; set; }
        public string fechaValor { get; set; }
        public string codMoneda { get; set; }       
        public string correlativoRelacion { get; set; }
    }
}
