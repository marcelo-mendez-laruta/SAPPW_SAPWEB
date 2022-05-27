using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Producto
{
    public class Custodia
    {
        /*ini varaiables locales*/
        public string indModalidad { get; set; }
        /*fin variables locales*/
        public string moneda { get; set; }
        public string nombreCuenta { get; set; }
        public string nroClientes { get; set; }
        public string cic01 { get; set; }
        public string cic02 { get; set; }
        public string cic03 { get; set; }
        public string montoInicial { get; set; }
        public string motivoCuenta { get; set; }
        public string nroCuenta { get; set; }
    }
}
