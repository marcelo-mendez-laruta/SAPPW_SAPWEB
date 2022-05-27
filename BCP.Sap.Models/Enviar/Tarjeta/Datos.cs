using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Tarjeta
{
    public class EnvioNumeroTarjeta
    {
        private string tarjeta1;
        public string tarjeta { get => tarjeta1; set => tarjeta1 = string.Join("", value.Split('-')); }
    }
    public class EnvioNombreTarjeta
    {
        public string nombreTarjeta { get; set; }
    }
    public class Datos
    {
        private string cuenta_ch_me1;
        private string cuenta_ch_mn1;
        private string cuenta_cc_me1;
        private string cuenta_cc_mn1;
        private string tarjeta1;
        public string cuenta_ch_me { get => cuenta_ch_me1; set => cuenta_ch_me1 = LimpiarDatos.relimpiarNumeros(value); }
        public string cuenta_ch_mn { get => cuenta_ch_mn1; set => cuenta_ch_mn1 = LimpiarDatos.relimpiarNumeros(value); }
        public string cuenta_cc_me { get => cuenta_cc_me1; set => cuenta_cc_me1 = LimpiarDatos.relimpiarNumeros(value); }
        public string cuenta_cc_mn { get => cuenta_cc_mn1; set => cuenta_cc_mn1 = LimpiarDatos.relimpiarNumeros(value); }
        public string tarjeta { get => tarjeta1; set => tarjeta1 = LimpiarDatos.relimpiarNumeros(value); }
        public string direccion { get; set; }
        public string idcNumero { get; set; }
        public string idcTipo { get; set; }
        public string idcExtension { get; set; }
        public string fechaNacimiento { get; set; }
        public string nombreCompleto { get; set; }
        public string cboCtsPagare { get; set; }
        public string confirmado { get; set; }
        public string nombrePersonalizado { get; set; }
        public string flag_ch_me { get; set; }
        public string flag_ch_mn { get; set; }
        public string flag_cc_me { get; set; }
        public string flag_cc_mn { get; set; }
        public string tipoCliente { get; set; }
        public string cic { get; set; }
    }
}
