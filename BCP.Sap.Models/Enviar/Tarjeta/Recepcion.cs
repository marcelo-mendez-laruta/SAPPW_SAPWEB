using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Tarjeta
{
    public class Recepcion
    {
        public string fecha { get; set; }
        public string cantidad { get; set; }
        public string tipoTarjeta { get; set; }
        public string aux { get; set; }
    }
}
