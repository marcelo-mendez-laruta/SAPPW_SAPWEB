using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Sap
{
    public class Producto
    {
        private string fechaInicio1;
        private string fechaFin1;
        private string fechaReinicio1;

        public string numeroDeCuenta { get; set; }
        public string numeroDeCuentaExtra { get; set; }
        public string moneda { get; set; }
        public string tipoCuenta { get; set; }
        public string oficina { get; set; }
        public string funcionarioDeNegocios { get; set; }
        public string relacion { get; set; }
        public string fechaInicio { get => fechaInicio1; set => fechaInicio1 = LimpiarDatos.formatoFecha(value); }
        public string fechaFin { get => fechaFin1; set => fechaFin1 = LimpiarDatos.formatoFecha(value); }
        public string motivo { get; set; }
        public string fechaReinicio { get => fechaReinicio1; set => fechaReinicio1 = LimpiarDatos.formatoFecha(value); }
        public string estado { get; set; }
        public string porcentaje { get; set; }
        public string saldo { get; set; }
        public string direccion { get; set; }
    }
    public class ProductoClienteData
    {
        public string numeroDeCuentaExtra { get; set; }
        public string moneda { get; set; }
        public string tipoCuenta { get; set; }
        public string oficina { get; set; }
        public string funcionarioDeNegocios { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string estado { get; set; }
        public string subTipoCuenta { get; set; }
        public string nroClientes { get; set; }
        public List<ProductoClientes> clientes { get; set; }
    }
    public class ProductoClientes
    {
        public string idcNumero { get; set; }
        public string idcTipo { get; set; }
        public string idcExtension { get; set; }
        public string cic { get; set; }
        public string nombresRazonSocial { get; set; }
        public int direccion { get; set; }
        public string relacion { get; set; }
    }
}
