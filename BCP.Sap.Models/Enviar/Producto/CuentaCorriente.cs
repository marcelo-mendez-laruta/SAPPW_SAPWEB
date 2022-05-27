using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Producto
{
    public class CuentaCorriente
    {
        /*variables locales*/
        public string indModalidad { get; set; }
        public string montoInicial { get; set; }
        public string sucursal { get; set; }
        public string agencia { get; set; }
        /*fin variables lcoales*/
        public string moneda { get; set; }
        public string tipoCliente { get; set; }
        public string nombreCuenta { get; set; }
        public string cic { get; set; }
        public string cicSegundoCliente { get; set; }
        public string cicTercerCliente { get; set; }
        public string indicadorMancomunos { get; set; }
        public string nroClientes { get; set; }
        public string tipoBanca { get; set; }
        public string segmento { get; set; }
        public string chequera { get; set; }
        public string tipoCuenta { get; set; }
        public string ciiu { get; set; }
        public string motivoCuenta { get; set; }
    }
}
