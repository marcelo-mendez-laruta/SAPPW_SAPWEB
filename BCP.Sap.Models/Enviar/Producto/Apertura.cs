using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Producto
{
    public class Apertura
    {
        private string numeroCuenta1;

        public string codMoneda { get; set; }
        public string codTipoProducto { get; set; }
        public string numeroCuenta { get; set; }
        public string nombreCuenta { get; set; }
        public string codTipoBanca { get; set; }
        public string monto { get; set; }
        public string campoStr1 { get; set; }
        public string campoStr2 { get; set; }
        public string campoStr3 { get; set; }
        public string campoInt1 { get; set; }
        public string campoMoney1 { get; set; }
        public string campoReal1 { get; set; }
        public string campoDate1 { get; set; }
    }
}
