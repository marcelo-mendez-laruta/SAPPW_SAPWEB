using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Producto
{
    public class CajaAhorro
    {
        /*uso local*/
        public string indModalidad { get; set; }
        public string sucursal { get; set; }
        public string agencia { get; set; }
        /*fin uso local*/
        public string tipoPersona { get; set; }
        public string tipoCuenta { get; set; }
        public string moneda { get; set; }
        public string tipoProducto { get; set; }
        public string subTipoProducto { get; set; }
        public string nombreCuenta { get; set; }
        public string nroClientes { get; set; }
        public string cic { get; set; }
        public string cicSegundoCliente { get; set; }
        public string cicTercerCliente { get; set; }
        public string montoInicial { get; set; }
        public string codProducto { get; set; }
        public string motivoCuenta { get; set; }
        public bool filtroArchivoNegativo { get; set; }
        public bool filtroEnameChecker { get; set; }
    }
}
