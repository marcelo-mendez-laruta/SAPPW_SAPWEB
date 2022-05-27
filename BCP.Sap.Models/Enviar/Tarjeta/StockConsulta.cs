using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Tarjeta
{
    public class StockConsulta
    {
        public string sucursal { get; set; }
        public string agencia { get; set; }
        public string aux { get; set; }
        public string tipoTarjeta { get; set; }
    }
}
