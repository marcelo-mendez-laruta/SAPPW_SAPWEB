using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir.Tarjeta
{
    public class RespuestaConsultaTarjeta : SapResponse
    {
         public ConsultaTarjeta data { get; set; }
    }
    public class ConsultaTarjeta
    {
        private string tarjeta1;

        public string tarjeta { get => LimpiarDatos.credimas(tarjeta1); set => tarjeta1 = string.Join("", value.Split('-')); }
        public string ahorro1 { get; set; }
        public string ahorro2 { get; set; }
        public string corriente1 { get; set; }
        public string corriente2 { get; set; }
        public string descrpBloqueo { get; set; }
        public string direccion { get; set; }
        public string tipoIDC { get; set; }
        public string idc { get; set; }
        public string extension { get; set; }
        public string estado { get; set; }
        public string fecApertura { get; set; }
        public string fecBloqueo { get; set; }
        public string fecNac { get; set; }
        public string fechaExpiracion { get; set; }
        public string fechaUso { get; set; }
        public string idcCompleto { get; set; }
        public string nombreCompleto { get; set; }
        public string ofBloqueo { get; set; }
        public string situacion { get; set; }
        public string sucursal { get; set; }
        public string tarjetaAnterior { get; set; }
        public string estadoCta1 { get; set; }
        public string estadoCta2 { get; set; }
        public string estadoCta3 { get; set; }
        public string estadoCta4 { get; set; }
        public string indTipoTarjeta { get; set; }
        public string pinBloqueo { get; set; }
        public string horaBloqueo { get; set; }
        public string flagCobro { get; set; }
        public string nombreTarjeta { get; set; }
        public string fechaUltimaActualizacion { get; set; }
        public string tipoBloqueo { get; set; }
        public string codigoBloqueo { get; set; }

        public bool flagCta1 { get; set; }
        public bool flagCta2 { get; set; }
        public bool flagCta3 { get; set; }
        public bool flagCta4 { get; set; }
    }
}
