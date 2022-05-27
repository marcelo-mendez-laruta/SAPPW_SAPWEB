using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Producto
{
    public class DPF
    {
        /*puden servir*/
        public string sucursal { get; set; }
        public string agencia { get; set; }
        public string usuario { get; set; }
        public string tipoCuentaDpf { get; set; }
        /**/
        public string moneda { get; set; }
        public string tipoDpf { get; set; }
        public string plazo { get; set; } //= "0";
        public string modoPagoInteres { get; set; }
        public string modoCuenta { get; set; }
        public string nombreCuenta { get; set; }
        public string cic01 { get; set; }
        public string cic02 { get; set; }
        public string cic03 { get; set; }
        public string indicadorCustodia { get; set; }
        public string tipoBanca { get; set; }
        public string nroClientes { get; set; }
        public string plazoManual { get; set; }
        public string tasaNominal { get; set; }// = "0.00";
        public string penalidad { get; set; }// = "000.000";
        public string conCuenta { get; set; }
        public string nroCuenta { get; set; }
        public string tipoCuenta { get; set; }
        public string monto { get; set; }
        public string dpfProducto { get; set; }
        public string tipoDeposito { get; set; }        
        /*aributos especiales*/
        public bool pagoCupon { get; set; }
        public string tipoPlazo { get; set; }
        public string cui { get; set; }
        public string tipoPagoCupon = "RDESM";
        /*reportes*/
        public string indModalidad { get; set; }
        public string renovacion { get; set; }
        public bool interesAdelantado { get; set; }
        public bool decreto { get; set; }
        /**/
        public string motivoCuenta { get; set; }
    }
    public class ContratoDPF
    {
        public string tipoCliente { get; set; }
        public string fechaApertura = DateTime.Now.ToString("dd/MM/yyyy");
        public string plazoManual { get; set; }
        public string tipoPlazo { get; set; }
        public string renovacion { get; set; }
        public string modoPagoInteres { get; set; }
        public string tasaNominal { get; set; }
        public string penalidad { get; set; }
        public string usuario { get; set; }
    }
}
