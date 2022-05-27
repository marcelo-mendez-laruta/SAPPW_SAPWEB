using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Producto
{
    public class Aperturante
    {
        public string nroDoc { get; set; }
        public string tipoDoc { get; set; }
        public string extensionDoc { get; set; }
        public string nombreRazonSocial { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string fechaNac { get; set; }
        public string direccion { get; set; }
        public string codLocalidad { get; set; }
        public string telefono { get; set; }
        public string gremio { get; set; }
        public string codCiiu { get; set; }
    }
}
