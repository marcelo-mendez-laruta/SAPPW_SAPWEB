using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaRegistro : SapResponse
    {
        public Registro data { get; set; }
    }
    public class Registro
    {
        public bool encontrado { get; set; }
        public string detalle { get; set; }
        public string constancia { get; set; }
        /*ARCHIVO NEGATIVO*/
        public string estado { get; set; }
        public int codigo { get; set; }
        public string descripcionCodigo { get; set; }
    }
}
